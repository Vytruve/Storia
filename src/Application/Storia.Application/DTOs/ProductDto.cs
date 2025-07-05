using System;

namespace Storia.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object para a entidade Produto.
    /// Usado para transferir dados de produto de e para a camada de UI,
    /// desacoplando a UI das entidades do domínio.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// ID do produto.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do produto.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descrição do produto.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// SKU (Stock Keeping Unit) do produto.
        /// </summary>
        public string Sku { get; set; } = string.Empty;

        /// <summary>
        /// Código de barras do produto.
        /// </summary>
        public string? Barcode { get; set; }

        /// <summary>
        /// Preço de venda do produto.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Unidade de medida do produto (como uma string para fácil consumo pela UI).
        /// Ex: "und", "Kg", "Lt".
        /// </summary>
        public string UnitOfMeasure { get; set; } = string.Empty;

        /// <summary>
        /// A quantidade total em estoque, somando todos os lotes.
        /// Este é um campo calculado, preenchido pelo serviço de aplicação.
        /// </summary>
        public decimal TotalStockQuantity { get; set; }

        // Nota: O DTO não expõe coleções de entidades complexas como 'Lots'.
        // Ele fornece uma visão agregada e simplificada para a UI.
    }
}