using apiFullUni.Application.Interfaces;
using apiFullUni.domain.Entities;
using apiFullUni.Infrastructure.Data.DAO;
using apiFullUni.Infrastructure.Logging;

namespace apiFullUni.Infrastructure.Data
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutoDAO _produtoDAO;
        private readonly SerilogLogger _Logger;

        public ProdutoRepository(ProdutoDAO produtoDAO, SerilogLogger logger)
        {
            _produtoDAO = produtoDAO;
            _Logger = logger;
        }

       
        public async Task<Produto?> GetByIdAsync(int id)
        {
            try
            {
                return await Task.Run(() => _produtoDAO.GetById(id)); // Não é necessário Task.Run, pois GetById já é assíncrono
            }
            catch (Exception ex)
            {
                _Logger.LogError("Erro ao buscar o produto pelo ID {ProductId}", ex);
                throw; // Re-lança a exceção para propagá-la
            }
        }

        
        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            try
            {
                return await Task.Run(() => _produtoDAO.GetAll());  // Chamando o método de ProdutoDAO
            }
            catch (Exception ex)
            {
                _Logger.LogError("Erro ao buscar todos os produtos.", ex);
                throw;
            }
        }

        
        public async Task AddAsync(Produto produto)
        {
            try
            {
                if (produto == null)
                {
                    throw new ArgumentNullException(nameof(produto), "Produto não pode ser nulo.");
                }

                await Task.Run(() => _produtoDAO.Insert(produto));  // Chamando o método de ProdutoDAO
            }
            catch (Exception ex)
            {
                _Logger.LogError("Erro ao adicionar o produto {ProductName}", ex);
                throw;
            }
            
        }

   
        public async Task UpdateAsync(Produto produto)
        {
            try
            {
                if (produto == null)
                {
                    throw new ArgumentNullException(nameof(produto), "Produto não pode ser nulo.");
                }

                await Task.Run(() => _produtoDAO.Update(produto));  // Chamando o método de ProdutoDAO
            }
            catch (Exception ex)
            {
                _Logger.LogError("Erro ao atualizar o produto {ProductName}", ex);
                throw;
            }
            
        }

        
        public async Task DeleteAsync(int id)
        {
            try
            {
                await Task.Run(() => _produtoDAO.Delete(id));  // Chamando o método Delete no ProdutoDAO de forma assíncrona
            }
            catch (Exception ex)
            {
                _Logger.LogError("Erro ao deletar o produto com ID {ProductId}", ex);
                throw;
            }
            
        }

    }
}
