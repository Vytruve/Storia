using Microsoft.EntityFrameworkCore;
using Storia.Core.Entities;
using Storia.Core.Interfaces.Repositories;
using Storia.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storia.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementação do repositório de vendas usando Entity Framework Core.
    /// </summary>
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados via injeção de dependência.
        /// </summary>
        /// <param name="context">A instância do AppDbContext.</param>
        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Sale?> GetByIdAsync(Guid id)
        {
            // Include(s => s.Items) instrui o EF Core a carregar também os SaleItems relacionados.
            // ThenInclude(i => i.Product) carrega o Produto associado a cada SaleItem.
            // Isso é chamado de "eager loading" (carregamento ansioso).
            return await _context.Sales
                                 .Include(s => s.Items)
                                 .ThenInclude(i => i.Product)
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            // Novamente, usamos Include para carregar os dados relacionados.
            // Em uma aplicação real, seria crucial adicionar .Take() e .Skip() para paginação.
            return await _context.Sales
                                 .Include(s => s.Items)
                                 .OrderByDescending(s => s.TransactionDate) // Ordena as mais recentes primeiro
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return await _context.Sales
                                 .Include(s => s.Items)
                                 .ThenInclude(i => i.Product)
                                 .Where(s => s.TransactionDate >= startDate && s.TransactionDate <= endDate)
                                 .OrderByDescending(s => s.TransactionDate)
                                 .ToListAsync();
        }

        /// <summary>
        /// Adiciona uma nova entidade de venda ao contexto do EF Core.
        /// A operação de salvar (commit) será realizada pelo Unit of Work.
        /// </summary>
        /// <param name="sale">A entidade Sale a ser adicionada.</param>
        public void Add(Sale sale)
        {
            // Apenas adiciona a entidade e suas filhas ao Change Tracker do EF Core.
            // Nenhuma chamada ao banco de dados é feita aqui.
            _context.Sales.Add(sale);
        }
    }
}