using System;

namespace Storia.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object para a entidade Produto.
    /// Usado para transferir dados de produto de e para a camada de UI,
    /// desacoplando a UI das entidades do dom�nio.
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
        /// Descri��o do produto.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// SKU (Stock Keeping Unit) do produto.
        /// </summary>
        public string Sku { get; set; } = string.Empty;

        /// <summary>
        /// C�digo de barras do produto.
        /// </summary>
        public string? Barcode { get; set; }

        /// <summary>
        /// Pre�o de venda do produto.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Unidade de medida do produto (como uma string para f�cil consumo pela UI).
        /// Ex: "und", "Kg", "Lt".
        /// </summary>
        public string UnitOfMeasure { get; set; } = string.Empty;

        /// <summary>
        /// A quantidade total em estoque, somando todos os lotes.
        /// Este � um campo calculado, preenchido pelo servi�o de aplica��o.
        /// </summary>
        public decimal TotalStockQuantity { get; set; }

        // Nota: O DTO n�o exp�e cole��es de entidades complexas como 'Lots'.
        // Ele fornece uma vis�o agregada e simplificada para a UI.
    }
}