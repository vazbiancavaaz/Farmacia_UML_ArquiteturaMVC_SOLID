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

            Directory.CreateDirectory(pasta);
            CaminhoBanco = Path.Combine(pasta, "farmacia.db");
            _connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = CaminhoBanco
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
