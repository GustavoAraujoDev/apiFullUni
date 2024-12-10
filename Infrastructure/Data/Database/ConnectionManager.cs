using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace apiFullUni.Infrastructure.Data
{
    // A ConnectionManager gerencia a conexão com o banco de dados
    public class ConnectionManager
    {
        private readonly string _connectionString;

        public ConnectionManager(IConfiguration configuration)
        {
            // Lê a string de conexão a partir do arquivo de configurações (web.config ou app.config)
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        // Método para obter uma nova conexão com o banco de dados
        public SqlConnection GetConnection()
        {
            // Retorna uma nova instância de SqlConnection usando a string de conexão
            return new SqlConnection(_connectionString);
        }
    }
}
