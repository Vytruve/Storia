using System;
using System.Collections.Generic;

namespace Storia.Application.DTOs
{
    /// <summary>
    /// DTO para representar os detalhes completos de uma transa��o de venda.
    /// Este objeto � usado para transferir dados de vendas para a camada de UI.
    /// </summary>
    public class SaleDetailDto
    {
        /// <summary>
        /// O ID da transa��o de venda.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// A data e hora em que a venda foi realizada.
        /// </summary>
        public DateTimeOffset TransactionDate { get; set; }

        /// <summary>
        /// O valor total da venda.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// A lista de itens que foram vendidos nesta transa��o.
        /// </summary>
        public List<SaleItemDto> Items { get; set; } = new List<SaleItemDto>();
    }

    /// <summary>
    /// DTO para representar um �nico item dentro de uma venda.
    /// </summary>
    public class SaleItemDto
    {
        /// <summary>
        /// ID do produto que foi vendido.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Nome do produto vendido.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// SKU do produto vendido.
        /// </summary>
        public string Sku { get; set; } = string.Empty;

        /// <summary>
        /// Quantidade do produto que foi vendida.
        /// </summary>
        public decimal QuantitySold { get; set; }

        /// <summary>
        /// Pre�o unit�rio do produto no momento da venda.
        /// </summary>
        public decimal PriceAtTimeOfSale { get; set; }

        /// <summary>
        /// Subtotal para esta linha de item (Quantidade * Pre�o).
        /// </summary>
        public decimal LineTotal => QuantitySold * PriceAtTimeOfSale;
    }
}