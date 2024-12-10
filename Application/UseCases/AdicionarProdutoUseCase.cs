
using apiFullUni.domain.Entities;
using apiFullUni.Infrastructure.Data;
using apiFullUni.Infrastructure.Logging;


namespace apiFullUni.Application.UseCases
{
    public class AdicionarProdutoUseCase
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly SerilogLogger _Logger;

        public AdicionarProdutoUseCase(ProdutoRepository produtoRepository, SerilogLogger logger)
        {
            _produtoRepository = produtoRepository;
            _Logger = logger;
        }

        public async Task ExecuteAsync(string nome, decimal preco, int id)
        {
            try
            {
                // Criando o produto a ser adicionado
                var produto = new Produto(nome, preco, id);
                await _produtoRepository.AddAsync(produto);

                _Logger.LogInfo($"Produto {nome} adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                // Logando erro em caso de falha
                _Logger.LogError("Erro ao adicionar o produto.", ex);
                throw new ApplicationException("Erro ao adicionar o produto", ex); // Lançando exceção personalizada para o consumidor da API ou serviço
            }
        }
    }
}
