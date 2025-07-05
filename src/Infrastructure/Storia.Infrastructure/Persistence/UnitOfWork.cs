using Storia.Core.Interfaces;
using Storia.Core.Interfaces.Repositories;
using Storia.Infrastructure.Persistence.Repositories;
using System.Threading.Tasks;

namespace Storia.Infrastructure.Persistence
{
    /// <summary>
    /// Implementação concreta do padrão Unit of Work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IProductRepository? _productRepository;
        private ISaleRepository? _saleRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Expõe o repositório de produtos.
        /// Utiliza lazy loading para instanciar o repositório apenas quando for necessário.
        /// </summary>
        public IProductRepository Products => _productRepository ??= new ProductRepository(_context);

        /// <summary>
        /// Expõe o repositório de vendas.
        /// Utiliza lazy loading.
        /// </summary>
        public ISaleRepository Sales => _saleRepository ??= new SaleRepository(_context);

        /// <summary>
        /// Confirma todas as alterações feitas nesta unidade de trabalho para o banco de dados
        /// em uma única transação.
        /// </summary>
        /// <returns>O número de objetos de estado escritos no banco de dados.</returns>
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Libera os recursos do DbContext.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}