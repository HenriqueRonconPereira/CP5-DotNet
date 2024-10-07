using MongoDB.Driver;
using CP5.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CP5.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IMongoCollection<Produto> _produtos;

        public ProdutoRepository(IMongoClient client)
        {
            var database = client.GetDatabase("BD_CP5");
            _produtos = database.GetCollection<Produto>("Produtos");
        }

        public async Task<Produto> AdicionarProduto(Produto produto)
        {
            await _produtos.InsertOneAsync(produto);
            return produto;
        }

        public async Task<Produto> AlterarProduto(string id, Produto produto)
        {
            await _produtos.ReplaceOneAsync(p => p.Id == id, produto);
            return produto;
        }

        public async Task<bool> ExcluirProduto(string id)
        {
            var resultado = await _produtos.DeleteOneAsync(p => p.Id == id);
            return resultado.DeletedCount > 0;
        }

        public async Task<Produto> ObterProduto(string id)
        {
            return await _produtos.Find<Produto>(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Produto>> ObterProdutos()
        {
            return await _produtos.Find(p => true).ToListAsync();
        }
    }
}
