using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace FarmaciaCRUD.dados
{
    public class Conexao
    {
        public string CaminhoBanco { get; }
        private readonly string _connectionString;

        public Conexao()
        {
            string pasta = AppContext.BaseDirectory;

            // No Visual Studio, usa o banco que acompanha o projeto no GitHub.
            var diretorio = new DirectoryInfo(AppContext.BaseDirectory);
            while (diretorio != null)
            {
                if (File.Exists(Path.Combine(diretorio.FullName, "Farmacia.csproj")))
                {
                    pasta = diretorio.FullName
                    break;
                }

                diretorio = diretorio.Parent;
            }

            Directory.CreateDirectory(pasta);
            CaminhoBanco = Path.Combine(pasta, "farmacia.db");
            _connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = CaminhoBanco,
                ForeignKeys = true
            }.ToString();

            using var conexao = Abrir();
            using var comando = conexao.CreateCommand();
            comando.CommandText = @"
                CREATE TABLE IF NOT EXISTS Remedios (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome TEXT NOT NULL CHECK(length(trim(Nome)) > 0),
                    Descricao TEXT NOT NULL DEFAULT '',
                    PrecoCentavos INTEGER NOT NULL
                        CHECK(typeof(PrecoCentavos) = 'integer' AND PrecoCentavos > 0)
                );

                CREATE TABLE IF NOT EXISTS CarrinhoItens (
                    RemedioId INTEGER PRIMARY KEY REFERENCES Remedios(Id) ON DELETE CASCADE,
                    Quantidade INTEGER NOT NULL CHECK(Quantidade BETWEEN 1 AND 999),
                    PrecoUnitarioCentavos INTEGER NOT NULL
                        CHECK(typeof(PrecoUnitarioCentavos) = 'integer' AND PrecoUnitarioCentavos > 0)
                );

                CREATE TABLE IF NOT EXISTS Compras (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Operacao TEXT NOT NULL UNIQUE,
                    DataUtc TEXT NOT NULL,
                    TotalCentavos INTEGER NOT NULL
                        CHECK(typeof(TotalCentavos) = 'integer' AND TotalCentavos > 0)
                );

                CREATE TABLE IF NOT EXISTS CompraItens (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    CompraId INTEGER NOT NULL REFERENCES Compras(Id),
                    RemedioId INTEGER NOT NULL,
                    NomeRemedio TEXT NOT NULL,
                    Quantidade INTEGER NOT NULL CHECK(Quantidade BETWEEN 1 AND 999),
                    PrecoUnitarioCentavos INTEGER NOT NULL
                        CHECK(typeof(PrecoUnitarioCentavos) = 'integer' AND PrecoUnitarioCentavos > 0)
                );";
            comando.ExecuteNonQuery();
        }

        public SqliteConnection Abrir()
        {
            var conexao = new SqliteConnection(_connectionString);
            try
            {
                conexao.Open();
                return conexao;
            }
            catch
            {
                conexao.Dispose();
                throw;
            }
        }
    }
}
