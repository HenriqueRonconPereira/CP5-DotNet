using Xunit;
using CP5.Repositories;
using CP5.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace CP5.Tests
{
	public class ProdutoRepositoryTests
	{
		private readonly ProdutoRepository _repository;

		public ProdutoRepositoryTests()
		{
			var client = new MongoClient("mongodb://localhost:27017");
			_repository = new ProdutoRepository(client);
		}

		[Fact]
		public async Task AdicionarProduto_ProdutoValido_DeveRetornarProduto()
		{
			var produto = new Produto { Nome = "Produto Teste", Preco = 10.5m, Quantidade = 5 };
			var resultado = await _repository.AdicionarProduto(produto);

			Assert.NotNull(resultado);
			Assert.Equal(produto.Nome, resultado.Nome);
		}

		// Adicione mais testes conforme necessário
	}
}
