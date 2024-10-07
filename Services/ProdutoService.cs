using MinhaAPI.Models;
using MongoDB.Driver;

namespace MinhaAPI.Services
{
    public class ProdutoService
    {
        private readonly IMongoCollection<Produto> _produtos;

        public ProdutoService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDB"));
            var database = client.GetDatabase("MinhaBaseDeDados");
            _produtos = database.GetCollection<Produto>("Produtos");
        }

        public async Task<List<Produto>> GetAsync() => await _produtos.Find(produto => true).ToListAsync();

        public async Task<Produto> GetByIdAsync(string id) => await _produtos.Find(produto => produto.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Produto novoProduto) => await _produtos.InsertOneAsync(novoProduto);

        public async Task UpdateAsync(string id, Produto produtoAtualizado) => await _produtos.ReplaceOneAsync(produto => produto.Id == id, produtoAtualizado);

        public async Task RemoveAsync(string id) => await _produtos.DeleteOneAsync(produto => produto.Id == id);
    }
}
