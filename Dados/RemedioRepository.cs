using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using FarmaciaCRUD.model;

namespace FarmaciaCRUD.dados
{
    public class RemedioRepository : IRepository<Remedio>
    {
        private readonly Conexao _banco = new Conexao();

        public List<Remedio> Listar()
        {
            using var conexao = _banco.Abrir();
            using var comando = conexao.CreateCommand();
            comando.CommandText = @"
                SELECT Id, Nome, Descricao, PrecoCentavos
                FROM Remedios ORDER BY Id;";

            using var leitor = comando.ExecuteReader();
            var remedios = new List<Remedio>();

            while (leitor.Read())
            {
                remedios.Add(new Remedio
                {
                    Id = leitor.GetInt32(0),
                    Nome = leitor.GetString(1),
                    Descricao = leitor.GetString(2),
                    Preco = leitor.GetInt64(3) / 100m
                });
            }

            return remedios;
        }

        public void Adicionar(Remedio remedio)
        {
            using var conexao = _banco.Abrir();
            using var comando = conexao.CreateCommand();
            comando.CommandText = @"
                INSERT INTO Remedios (Nome, Descricao, PrecoCentavos)
                VALUES ($nome, $descricao, $preco);
                SELECT last_insert_rowid();";
            PreencherParametros(comando, remedio);
            remedio.Id = Convert.ToInt32(comando.ExecuteScalar());
        }

        public bool Atualizar(Remedio remedio)
        {
            using var conexao = _banco.Abrir();
            using var comando = conexao.CreateCommand();
            comando.CommandText = @"
                UPDATE Remedios
                SET Nome = $nome, Descricao = $descricao, PrecoCentavos = $preco
                WHERE Id = $id;";
            PreencherParametros(comando, remedio);
            comando.Parameters.AddWithValue("$id", remedio.Id);
            return comando.ExecuteNonQuery() > 0;
        }

        public bool Excluir(int id)
        {
            using var conexao = _banco.Abrir();
            using var comando = conexao.CreateCommand();
            comando.CommandText = "DELETE FROM Remedios WHERE Id = $id;";
            comando.Parameters.AddWithValue("$id", id);
            return comando.ExecuteNonQuery() > 0;
        }

        private static void PreencherParametros(SqliteCommand comando, Remedio remedio)
        {
            if (string.IsNullOrWhiteSpace(remedio.Nome))
                throw new ArgumentException("Informe o nome do remédio.");

            if (remedio.Preco <= 0 || remedio.Preco != decimal.Round(remedio.Preco, 2))
                throw new ArgumentException("Informe um preço positivo com até duas casas decimais.");

            if (remedio.Preco > long.MaxValue / 100m)
                throw new ArgumentException("O preço informado é muito alto.");

            long centavos = decimal.ToInt64(remedio.Preco * 100m);
            comando.Parameters.AddWithValue("$nome", remedio.Nome.Trim());
            comando.Parameters.AddWithValue("$descricao", remedio.Descricao ?? string.Empty);
            comando.Parameters.AddWithValue("$preco", centavos);
        }
    }
}
