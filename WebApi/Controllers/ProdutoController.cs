using apiFullUni.Application.UseCases;
using apiFullUni.domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apiFullUni.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AdicionarProdutoUseCase _adicionarProdutoUseCase;
        private readonly AtualizarProdutoUseCase _atualizarProdutoUseCase;
        private readonly DeletarProdutoUseCase _deletarProdutoUseCase;
        private readonly ObterProdutoPorIdUseCase _obterProdutoPorIdUseCase;
        private readonly ObterTodosProdutosUseCase _obterTodosProdutosUseCase;

        public ProdutoController(
            AdicionarProdutoUseCase adicionarProdutoUseCase,
            AtualizarProdutoUseCase atualizarProdutoUseCase,
            DeletarProdutoUseCase deletarProdutoUseCase,
            ObterProdutoPorIdUseCase obterProdutoPorIdUseCase,
            ObterTodosProdutosUseCase obterTodosProdutosUseCase)
        {
            _adicionarProdutoUseCase = adicionarProdutoUseCase;
            _atualizarProdutoUseCase = atualizarProdutoUseCase;
            _deletarProdutoUseCase = deletarProdutoUseCase;
            _obterProdutoPorIdUseCase = obterProdutoPorIdUseCase;
            _obterTodosProdutosUseCase = obterTodosProdutosUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduto([FromBody] Produto produto)
        {
            if (produto == null)
            {
                return BadRequest("Produto inválido.");
            }

            await _adicionarProdutoUseCase.ExecuteAsync(produto.Nome, produto.Preco, produto.Id);
            return CreatedAtAction(nameof(AddProduto), new { id = produto.Id }, produto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduto([FromBody] Produto produto)
        {
            if (produto == null)
            {
                return BadRequest("Produto inválido.");
            }

            await _atualizarProdutoUseCase.ExecuteAsync(produto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            await _deletarProdutoUseCase.ExecuteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProdutoById(int id)
        {
            var produto = await _obterProdutoPorIdUseCase.ExecuteAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProdutos()
        {
            var produtos = await _obterTodosProdutosUseCase.ExecuteAsync();
            return Ok(produtos);
        }
    }
}
