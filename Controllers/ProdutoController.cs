using Microsoft.AspNetCore.Mvc;
using CP5.Models;
using CP5.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CP5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            await _produtoRepository.AdicionarProduto(produto);
            return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Produto produto)
        {
            await _produtoRepository.AlterarProduto(id, produto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _produtoRepository.ExcluirProduto(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> Get(string id)
        {
            var produto = await _produtoRepository.ObterProduto(id);
            if (produto == null) return NotFound();
            return produto;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> Get()
        {
            var produtos = await _produtoRepository.ObterProdutos();
            return produtos;
        }
    }
}
