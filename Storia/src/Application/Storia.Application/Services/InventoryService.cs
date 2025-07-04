using Storia.Application.Interfaces; // <-- ADICIONE ESTA LINHA
using Storia.Core.Entities;
using System;
using System.Linq;

namespace Storia.Application.Services
{
    /// <summary>
    /// Implementa a lógica de gerenciamento de inventário, como a baixa de estoque.
    /// </summary>
    public class InventoryService : IInventoryService
    {
        public decimal DeductStock(Product product, decimal quantityToDeduct)
        {
            var totalStock = product.Lots.Sum(l => l.Quantity);
            if (totalStock < quantityToDeduct)
            {
                throw new InvalidOperationException($"Estoque insuficiente para o produto '{product.Name}'. Disponível: {totalStock}, Solicitado: {quantityToDeduct}.");
            }

            // Ordena os lotes: primeiro por data de validade (os mais próximos de vencer primeiro),
            // depois por data de chegada (os mais antigos primeiro). Nulos na validade vão para o fim.
            var sortedLots = product.Lots
                .Where(l => l.Quantity > 0)
                .OrderBy(l => l.ExpirationDate.HasValue ? 0 : 1) // Prioriza lotes com data de validade
                .ThenBy(l => l.ExpirationDate)
                .ThenBy(l => l.ArrivalDate)
                .ToList();

            decimal remainingToDeduct = quantityToDeduct;
            decimal totalCostOfDeductedItems = 0;

            foreach (var lot in sortedLots)
            {
                if (remainingToDeduct <= 0) break;

                decimal quantityFromThisLot = Math.Min(lot.Quantity, remainingToDeduct);

                lot.Quantity -= quantityFromThisLot;
                totalCostOfDeductedItems += quantityFromThisLot * lot.PurchasePrice;
                remainingToDeduct -= quantityFromThisLot;
            }

            // Retorna o custo médio ponderado dos itens que foram baixados.
            return totalCostOfDeductedItems / quantityToDeduct;
        }
    }
}