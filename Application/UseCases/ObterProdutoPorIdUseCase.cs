using apiFullUni.Infrastructure.Data;
using apiFullUni.domain.Entities;
using System.Threading.Tasks;
using apiFullUni.Application.Interfaces;
using apiFullUni.Infrastructure.Logging;

namespace apiFullUni.Application.UseCases
{
    public class ObterProdutoPorIdUseCase
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly SerilogLogger _Logger;

        public ObterProdutoPorIdUseCase(ProdutoRepository produtoRepository, SerilogLogger logger)
        {
            _produtoRepository = produtoRepository;
            _Logger = logger;
        }

        
        public async Task<Produto?> ExecuteAsync(int id)
        {
            try
            {
                // Tentando obter o produto por ID
                var produto = await _produtoRepository.GetByIdAsync(id);

                if (produto == null)
                {
                    _Logger.LogInfo($"Produto com ID {id} não encontrado.");
                }
                else
                {
                    _Logger.LogInfo($"Produto com ID {id} encontrado.");
                }

                return produto; // Retorna o produto ou null caso não encontrado
            }
            catch (Exception ex)
            {
                // Caso ocorra algum erro, loga o erro
                _Logger.LogError($"Erro ao obter o produto com ID {id}.", ex);
                throw new ApplicationException($"Erro ao obter o produto com ID {id}.", ex);
            }
        }
    }
}
