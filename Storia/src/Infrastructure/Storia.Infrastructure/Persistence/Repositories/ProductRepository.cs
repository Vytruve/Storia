using Microsoft.EntityFrameworkCore;
using Storia.Core.Entities;
using Storia.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storia.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementação do repositório de produtos usando Entity Framework Core.
    /// As operações de escrita (Add, Update, Delete) apenas modificam o Change Tracker do DbContext.
    /// A persistência real é delegada ao IUnitOfWork.CompleteAsync().
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados via injeção de dependência.
        /// </summary>
        /// <param name="context">A instância do AppDbContext.</param>
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            // Usamos Include para garantir que a coleção de Lotes seja carregada junto com o Produto.
            // Isso é essencial para lógicas de negócio como o cálculo de estoque total.
            return await _context.Products
                                 .Include(p => p.Lots)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> GetBySkuAsync(string sku)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Sku == sku);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            // O Include aqui pode ser opcional dependendo da tela de listagem.
            // Para uma lista simples, não carregar os lotes é mais performático.
            // Para uma lista detalhada, o Include seria necessário.
            // Vamos manter sem Include por padrão para a listagem geral.
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public void Add(Product product)
        {
            // Apenas adiciona ao contexto, não salva.
            _context.Products.Add(product);
        }

        public void Update(Product product)
        {
            // Apenas marca a entidade como modificada, não salva.
            _context.Products.Update(product);
        }

        public void Delete(Product product)
        {
            // Apenas marca a entidade para remoção, não salva.
            _context.Products.Remove(product);
        }
    }
}