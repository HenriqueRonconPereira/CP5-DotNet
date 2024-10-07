using MinhaAPI.Models;
using MinhaAPI.Services;
using Moq;
using Xunit;

public class ProdutoServiceTests
{
    private readonly Mock<ProdutoService> _mockProdutoService;

    public ProdutoServiceTests()
    {
        _mockProdutoService = new Mock<ProdutoService>(null);
    }

    [Fact]
    public async Task TesteCriarProduto()
    {
        var novoProduto = new Produto { Nome = "Produto Teste", Preco = 10.0M, Quantidade = 5 };
        await _mockProdutoService.Object.CreateAsync(novoProduto);

        _mockProdutoService.Verify(service => service.CreateAsync(novoProduto), Times.Once);
    }

    [Fact]
    public async Task TesteObterProduto()
    {
        var produto = new Produto { Id = "123", Nome = "Produto Teste", Preco = 10.0M, Quantidade = 5 };
        _mockProdutoService.Setup(service => service.GetByIdAsync("123")).ReturnsAsync(produto);

        var resultado = await _mockProdutoService.Object.GetByIdAsync("123");

        Assert.NotNull(resultado);
        Assert.Equal("Produto Teste", resultado.Nome);
    }
}
