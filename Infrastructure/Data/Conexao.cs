using System.Data;
using Microsoft.Data.SqlClient;


namespace apiFullUni.Infrastructure.Data
{
    public class Conexao
    {
        private readonly ConnectionManager _connectionManager;

        public Conexao(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager; // Gerenciamento de conexão
        }

        public DataView Execute(string sql, params SqlParameter[] parameters)
        {
            using (var connection = _connectionManager.GetConnection())
            using (var command = CreateCommand(connection, sql, parameters))
            using (var dataAdapter = new SqlDataAdapter(command))
            {
                var dataSet = new DataSet();
                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataSet);
                    return dataSet.Tables[0].DefaultView;
                }
                catch (SqlException ex)
                {
                    LogSqlException(ex, sql, parameters);
                    throw;
                }
            }
        }

        public SqlDataReader ExecuteReader(string sql, params SqlParameter[] parameters)
        {
            var connection = _connectionManager.GetConnection();
            var command = CreateCommand(connection, sql, parameters);

            try
            {
                connection.Open();
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException ex)
            {
                connection.Close();
                LogSqlException(ex, sql, parameters);
                throw;
            }
        }

        private SqlCommand CreateCommand(SqlConnection connection, string sql, SqlParameter[] parameters)
        {
            var command = new SqlCommand(sql, connection)
            {
                CommandType = CommandType.Text,
                CommandTimeout = 30
            };

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }

        private void LogSqlException(SqlException ex, string sql, SqlParameter[] parameters)
        {
            Console.Error.WriteLine($"Erro SQL: {ex.Message}");
            Console.Error.WriteLine($"Consulta: {sql}");
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    Console.Error.WriteLine($"Parâmetro: {param.ParameterName} = {param.Value}");
                }
            }
        }
    }
}
