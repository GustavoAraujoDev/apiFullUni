using apiFullUni.Application.Interfaces;
using apiFullUni.Infrastructure.Data;
using apiFullUni.Infrastructure.Logging;
using System.Threading.Tasks;


namespace apiFullUni.Application.UseCases
{
    public class DeletarProdutoUseCase
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly SerilogLogger _Logger;

        public DeletarProdutoUseCase(ProdutoRepository produtoRepository, SerilogLogger logger)
        {
            _produtoRepository = produtoRepository;
            _Logger = logger;
        }

        public async Task ExecuteAsync(int id)
        {
            try
            {
                // Tentando deletar o produto
                await _produtoRepository.DeleteAsync(id); // Chama o repositório para deletar o produto
                _Logger.LogInfo($"Produto com ID {id} deletado com sucesso.");
            }
            catch (Exception ex)
            {
                // Caso ocorra algum erro, loga o erro e relança uma exceção personalizada
                _Logger.LogError($"Erro ao deletar o produto com ID {id}.", ex);
                throw new ApplicationException($"Erro ao deletar o produto com ID {id}.", ex);
            }
        }
    }
}
