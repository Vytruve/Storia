using Microsoft.EntityFrameworkCore;
using Storia.Core.Entities;
using Storia.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storia.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementa��o do reposit�rio de produtos usando Entity Framework Core.
    /// As opera��es de escrita (Add, Update, Delete) apenas modificam o Change Tracker do DbContext.
    /// A persist�ncia real � delegada ao IUnitOfWork.CompleteAsync().
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados via inje��o de depend�ncia.
        /// </summary>
        /// <param name="context">A inst�ncia do AppDbContext.</param>
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            // Usamos Include para garantir que a cole��o de Lotes seja carregada junto com o Produto.
            // Isso � essencial para l�gicas de neg�cio como o c�lculo de estoque total.
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
            // Para uma lista simples, n�o carregar os lotes � mais perform�tico.
            // Para uma lista detalhada, o Include seria necess�rio.
            // Vamos manter sem Include por padr�o para a listagem geral.
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public void Add(Product product)
        {
            // Apenas adiciona ao contexto, n�o salva.
            _context.Products.Add(product);
        }

        public void Update(Product product)
        {
            // Apenas marca a entidade como modificada, n�o salva.
            _context.Products.Update(product);
        }

        public void Delete(Product product)
        {
            // Apenas marca a entidade para remo��o, n�o salva.
            _context.Products.Remove(product);
        }
    }
}