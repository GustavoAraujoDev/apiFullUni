using apiFullUni.Infrastructure.Data;
using apiFullUni.domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using apiFullUni.Application.Interfaces;
using apiFullUni.Infrastructure.Logging;

namespace apiFullUni.Application.UseCases
{
    public class ObterTodosProdutosUseCase
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly SerilogLogger _Logger;

        public ObterTodosProdutosUseCase(ProdutoRepository produtoRepository, SerilogLogger logger)
        {
            _produtoRepository = produtoRepository;
            _Logger = logger;
        }

        public async Task<IEnumerable<Produto>?> ExecuteAsync()
        {
            try
            {
                // Tentando obter todos os produtos
                var produtos = await _produtoRepository.GetAllAsync();

                // Verifica se a lista de produtos está vazia
                if (produtos == null || !produtos.Any())
                {
                    _Logger.LogInfo("Nenhum produto encontrado.");
                }
                else
                {
                    _Logger.LogInfo($"Total de {produtos.Count()} produtos encontrados.");
                }

                return produtos; // Retorna a lista de produtos
            }
            catch (Exception ex)
            {
                // Caso ocorra algum erro, loga o erro
                _Logger.LogError("Erro ao obter todos os produtos.", ex);
                throw new ApplicationException("Erro ao obter todos os produtos.", ex);
            }

        }
    }
}
