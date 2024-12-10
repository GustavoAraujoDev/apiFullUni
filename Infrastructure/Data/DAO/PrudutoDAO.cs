using apiFullUni.domain.Entities;
using apiFullUni.Infrastructure.Logging;
using Microsoft.Data.SqlClient;
using System.Data;

namespace apiFullUni.Infrastructure.Data.DAO
{
    public class ProdutoDAO
    {
        private readonly Conexao _conexao;
        

        public ProdutoDAO(ConnectionManager connectionManager)
        {
            _conexao = new Conexao(connectionManager);
        }

        public Produto? GetById(int id)
        {
            var sql = "SELECT * FROM Produto WHERE Id = @Id";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = id }
            };

            var dataView = _conexao.Execute(sql, parameters);
            var dataTable = dataView.ToTable();

            if (dataTable.Rows.Count > 0)
            {
                var row = dataTable.Rows[0];
                return new Produto(
                    nome: row["Nome"].ToString()!,
                    preco: Convert.ToDecimal(row["Preco"]),
                    id: Convert.ToInt32(row["Id"])
                );
            }
            return null;
        }

       
        public List<Produto> GetAll()
        {
            var sql = "SELECT * FROM Produto";
            var dataView = _conexao.Execute(sql);
            var dataTable = dataView.ToTable();
            var produtos = new List<Produto>();

            foreach (DataRow row in dataTable.Rows)
            {
                produtos.Add(new Produto(
                    nome: row["Nome"].ToString()!,
                    preco: Convert.ToDecimal(row["Preco"]),
                    id: Convert.ToInt32(row["Id"])
                ));
            }
            return produtos;
        }

       
        public void Insert(Produto produto)
        {
            var sql = "INSERT INTO Produto (Id, Nome, Preco) VALUES (@Id, @Nome, @Preco)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = produto.Id },
                new SqlParameter("@Nome", SqlDbType.NVarChar) { Value = produto.Nome },
                new SqlParameter("@Preco", SqlDbType.Decimal) { Value = produto.Preco }
            };
            _conexao.ExecuteReader(sql, parameters);
        }

      
        public void Update(Produto produto)
        {
            var sql = "UPDATE Produto SET Nome = @Nome, Preco = @Preco WHERE Id = @Id";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = produto.Id },
                new SqlParameter("@Nome", SqlDbType.NVarChar) { Value = produto.Nome },
                new SqlParameter("@Preco", SqlDbType.Decimal) { Value = produto.Preco }
            };
            _conexao.ExecuteReader(sql, parameters);
        }

        
        public void Delete(int id)
        {
            var sql = "DELETE FROM Produto WHERE Id = @Id";
            var parameters = new SqlParameter[]
            {
        new SqlParameter("@Id", SqlDbType.Int) { Value = id }
            };
            _conexao.ExecuteReader(sql, parameters);
        }

    }
}
