using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;
using FarmaciaCRUD.model;

namespace FarmaciaCRUD.dados
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly Conexao _banco = new Conexao();

        public List<ItemCarrinho> Listar()
        {
            using var conexao = _banco.Abrir();
            using var comando = conexao.CreateCommand();
            comando.CommandText = @"
                SELECT c.RemedioId, r.Nome, c.Quantidade, c.PrecoUnitarioCentavos
                FROM CarrinhoItens c
                INNER JOIN Remedios r ON r.Id = c.RemedioId
                ORDER BY r.Nome, c.RemedioId;";
            return LerItens(comando);
        }

        public void Adicionar(int remedioId)
        {
            using var conexao = _banco.Abrir();
            using var transacao = conexao.BeginTransaction(deferred: false);
            using var comando = conexao.CreateCommand();
            comando.Transaction = transacao;
            comando.Parameters.AddWithValue("$id", remedioId);
            comando.CommandText = "SELECT Quantidade FROM CarrinhoItens WHERE RemedioId = $id;";
            object? quantidade = comando.ExecuteScalar();

            if (quantidade != null && Convert.ToInt32(quantidade) >= 999)
                throw new InvalidOperationException("O limite é de 999 unidades por remédio.");

            // O primeiro acréscimo fixa o preço do item. Repetições somam uma unidade.
            comando.CommandText = @"
                INSERT INTO CarrinhoItens (RemedioId, Quantidade, PrecoUnitarioCentavos)
                SELECT Id, 1, PrecoCentavos FROM Remedios WHERE Id = $id
                ON CONFLICT(RemedioId) DO UPDATE
                SET Quantidade = CarrinhoItens.Quantidade + 1;";

            if (comando.ExecuteNonQuery() == 0)
                throw new InvalidOperationException("Esse remédio foi removido do catálogo. Atualize a lista.");

            transacao.Commit();
        }

        public void AlterarQuantidade(int remedioId, int quantidade)
        {
            if (quantidade < 1 || quantidade > 999)
                throw new ArgumentException("A quantidade deve estar entre 1 e 999.");

            using var conexao = _banco.Abrir();
            using var comando = conexao.CreateCommand();
            comando.CommandText = @"
                UPDATE CarrinhoItens SET Quantidade = $quantidade
                WHERE RemedioId = $id;";
            comando.Parameters.AddWithValue("$id", remedioId);
            comando.Parameters.AddWithValue("$quantidade", quantidade);

            if (comando.ExecuteNonQuery() == 0)
                throw new InvalidOperationException("O item já saiu do carrinho. Atualize a lista.");
        }

        public void Remover(int remedioId)
        {
            using var conexao = _banco.Abrir();
            using var comando = conexao.CreateCommand();
            comando.CommandText = "DELETE FROM CarrinhoItens WHERE RemedioId = $id;";
            comando.Parameters.AddWithValue("$id", remedioId);
            comando.ExecuteNonQuery();
        }

        public Compra FinalizarCompra(string operacao)
        {
            using var conexao = _banco.Abrir();
            // A transação reserva a escrita antes de ler o carrinho.
            using var transacao = conexao.BeginTransaction(deferred: false);

            // Repetir uma tentativa com a mesma identificação devolve a compra já salva.
            using (var existente = conexao.CreateCommand())
            {
                existente.Transaction = transacao;
                existente.CommandText = "SELECT Id, TotalCentavos FROM Compras WHERE Operacao = $operacao;";
                existente.Parameters.AddWithValue("$operacao", operacao);
                using var leitor = existente.ExecuteReader();
                if (leitor.Read())
                {
                    return new Compra
                    {
                        Id = leitor.GetInt64(0),
                        Total = leitor.GetInt64(1) / 100m
                    };
                }
            }

            List<ItemCarrinho> itens;
            using (var consulta = conexao.CreateCommand())
            {
                consulta.Transaction = transacao;
                consulta.CommandText = @"
                    SELECT c.RemedioId, r.Nome, c.Quantidade, c.PrecoUnitarioCentavos
                    FROM CarrinhoItens c
                    INNER JOIN Remedios r ON r.Id = c.RemedioId
                    ORDER BY c.RemedioId;";
                itens = LerItens(consulta);
            }

            if (itens.Count == 0)
                throw new InvalidOperationException("O carrinho está vazio.");

            long totalCentavos = SomarCentavos(itens);
            long compraId;
            using (var compra = conexao.CreateCommand())
            {
                compra.Transaction = transacao;
                compra.CommandText = @"
                    INSERT INTO Compras (Operacao, DataUtc, TotalCentavos)
                    VALUES ($operacao, $data, $total);
                    SELECT last_insert_rowid();";
                compra.Parameters.AddWithValue("$operacao", operacao);
                compra.Parameters.AddWithValue("$data", DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture));
                compra.Parameters.AddWithValue("$total", totalCentavos);
                compraId = Convert.ToInt64(compra.ExecuteScalar());
            }

            foreach (ItemCarrinho item in itens)
            {
                using var detalhe = conexao.CreateCommand();
                detalhe.Transaction = transacao;
                detalhe.CommandText = @"
                    INSERT INTO CompraItens
                        (CompraId, RemedioId, NomeRemedio, Quantidade, PrecoUnitarioCentavos)
                    VALUES ($compra, $remedio, $nome, $quantidade, $preco);";
                detalhe.Parameters.AddWithValue("$compra", compraId);
                detalhe.Parameters.AddWithValue("$remedio", item.RemedioId);
                detalhe.Parameters.AddWithValue("$nome", item.Nome);
                detalhe.Parameters.AddWithValue("$quantidade", item.Quantidade);
                detalhe.Parameters.AddWithValue("$preco", decimal.ToInt64(item.PrecoUnitario * 100m));
                detalhe.ExecuteNonQuery();
            }

            using (var limpar = conexao.CreateCommand())
            {
                limpar.Transaction = transacao;
                limpar.CommandText = "DELETE FROM CarrinhoItens;";
                limpar.ExecuteNonQuery();
            }

            // Compra, itens e limpeza do carrinho são confirmados juntos.
            transacao.Commit();
            return new Compra { Id = compraId, Total = totalCentavos / 100m };
        }

        private static List<ItemCarrinho> LerItens(SqliteCommand comando)
        {
            using var leitor = comando.ExecuteReader();
            var itens = new List<ItemCarrinho>();
            while (leitor.Read())
            {
                itens.Add(new ItemCarrinho
                {
                    RemedioId = leitor.GetInt32(0),
                    Nome = leitor.GetString(1),
                    Quantidade = leitor.GetInt32(2),
                    PrecoUnitario = leitor.GetInt64(3) / 100m
                });
            }
            return itens;
        }

        private static long SomarCentavos(List<ItemCarrinho> itens)
        {
            try
            {
                long total = 0;
                foreach (ItemCarrinho item in itens)
                {
                    long preco = decimal.ToInt64(item.PrecoUnitario * 100m);
                    total = checked(total + checked(preco * item.Quantidade));
                }
                return total;
            }
            catch (OverflowException)
            {
                throw new InvalidOperationException("O valor do carrinho ultrapassa o limite do banco.");
            }
        }
    }
}
