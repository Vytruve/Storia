using Storia.Application.DTOs;
using Storia.Application.Interfaces;
using Storia.Core.Entities;
using Storia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storia.Application.Services
{
    /// <summary>
    /// Implementa��o do servi�o de aplica��o para gerenciar vendas.
    /// </summary>
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInventoryService _inventoryService;

        public SaleService(IUnitOfWork unitOfWork, IInventoryService inventoryService)
        {
            _unitOfWork = unitOfWork;
            _inventoryService = inventoryService;
        }

        public async Task<SaleDetailDto> CreateSaleAsync(SaleDetailDto saleDto)
        {
            // 1. Criar a entidade principal da Venda
            var newSale = new Sale();

            // 2. Processar cada item da venda
            foreach (var itemDto in saleDto.Items)
            {
                // 2a. Buscar o produto completo, incluindo seus lotes, do reposit�rio.
                var product = await _unitOfWork.Products.GetByIdAsync(itemDto.ProductId);
                if (product == null)
                {
                    throw new KeyNotFoundException($"Produto com ID '{itemDto.ProductId}' n�o encontrado.");
                }

                // 2b. Delegar a l�gica de baixa de estoque para o InventoryService.
                // Ele valida o estoque, atualiza as quantidades dos lotes em mem�ria e retorna o custo.
                decimal costAtTimeOfSale = _inventoryService.DeductStock(product, itemDto.QuantitySold);

                // 2c. Criar a entidade SaleItem com todos os dados da transa��o.
                var saleItem = new SaleItem(
                    newSale.Id,
                    product.Id,
                    itemDto.QuantitySold,
                    product.Price, // Pre�o de venda atual do produto
                    costAtTimeOfSale // Custo calculado pelo InventoryService
                );

                newSale.Items.Add(saleItem);
            }

            // 3. Adicionar a venda (com seus itens) ao reposit�rio.
            _unitOfWork.Sales.Add(newSale);

            // 4. Persistir TODAS as altera��es (nova venda, novos itens, estoque atualizado nos lotes)
            // em uma �nica transa��o at�mica. A genialidade do Unit of Work.
            await _unitOfWork.CompleteAsync();

            // 5. Mapear a entidade salva para o DTO de retorno.
            return MapToDto(newSale);
        }

        public async Task<SaleDetailDto?> GetSaleByIdAsync(Guid saleId)
        {
            var sale = await _unitOfWork.Sales.GetByIdAsync(saleId);
            return sale == null ? null : MapToDto(sale);
        }

        public async Task<IEnumerable<SaleDetailDto>> GetAllSalesAsync()
        {
            var sales = await _unitOfWork.Sales.GetAllAsync();
            return sales.Select(MapToDto);
        }

        public async Task<IEnumerable<SaleDetailDto>> GetSalesByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var sales = await _unitOfWork.Sales.GetSalesByDateRangeAsync(startDate, endDate);
            return sales.Select(MapToDto);
        }

        // --- M�todo de Mapeamento Privado ---

        private SaleDetailDto MapToDto(Sale sale)
        {
            return new SaleDetailDto
            {
                Id = sale.Id,
                TransactionDate = sale.TransactionDate,
                TotalAmount = sale.TotalAmount, // O c�lculo � feito na entidade
                Items = sale.Items.Select(item => new SaleItemDto
                {
                    ProductId = item.ProductId,
                    // O reposit�rio j� carregou o Produto, ent�o podemos acess�-lo.
                    ProductName = item.Product?.Name ?? "Produto n�o encontrado",
                    Sku = item.Product?.Sku ?? "N/A",
                    QuantitySold = item.QuantitySold,
                    PriceAtTimeOfSale = item.PriceAtTimeOfSale
                }).ToList()
            };
        }
    }
}