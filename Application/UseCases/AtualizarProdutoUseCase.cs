using apiFullUni.domain.Entities;
using apiFullUni.Infrastructure.Data;
using apiFullUni.Infrastructure.Logging;
using System.Threading.Tasks;


namespace apiFullUni.Application.UseCases
{
    public class AtualizarProdutoUseCase
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly SerilogLogger _Logger;

        public AtualizarProdutoUseCase(ProdutoRepository produtoRepository, SerilogLogger logger)
        {
            _produtoRepository = produtoRepository;
            _Logger = logger;
        }

 
        public async Task ExecuteAsync(Produto produto)
        {
            try
            {
                // Tentando atualizar o produto
                await _produtoRepository.UpdateAsync(produto); // Chama o repositório para atualizar o produto
                _Logger.LogInfo($"Produto com ID {produto.Id} atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                // Caso ocorra algum erro, loga o erro e relança uma exceção personalizada
                _Logger.LogError($"Erro ao atualizar o produto com ID {produto.Id}.", ex);
                throw new ApplicationException($"Erro ao atualizar o produto com ID {produto.Id}.", ex);
            }
        }
    }
}

