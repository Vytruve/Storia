using Storia.Core.Enums;
using System;
using System.Collections.Generic;

namespace Storia.Core.Entities
{
    /// <summary>
    /// Representa um produto no sistema de inventário.
    /// Esta é uma entidade central do domínio.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Identificador único global para o produto.
        /// Usar Guid evita conflitos em sistemas distribuídos ou cenários de importação/exportação.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Nome comercial do produto.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descrição detalhada do produto.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// SKU (Stock Keeping Unit) - Código único interno para controle de estoque.
        /// Deve ser único no sistema.
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// Código de barras do produto (EAN, UPC, etc.).
        /// Pode ser nulo se o produto não tiver um.
        /// </summary>
        public string? Barcode { get; set; }

        /// <summary>
        /// Preço de venda padrão do produto.
        /// Usamos 'decimal' para precisão financeira.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// A unidade de medida na qual este produto é vendido (Unidade, Kg, Litro, etc.).
        /// </summary>
        public UnitOfMeasure UnitOfMeasure { get; set; }

        /// <summary>
        /// Data e hora em que o registro do produto foi criado.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// Data e hora da última atualização do registro do produto.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Propriedade de navegação para os lotes associados a este produto.
        /// Um produto pode ter vários lotes (ex: recebidos em datas diferentes com validades diferentes).
        /// </summary>
        public ICollection<Lot> Lots { get; private set; }

        /// <summary>
        /// Construtor para a entidade Produto.
        /// </summary>
        /// <param name="name">Nome do produto.</param>
        /// <param name="sku">SKU do produto.</param>
        /// <param name="price">Preço de venda.</param>
        /// <param name="unitOfMeasure">Unidade de medida.</param>
        public Product(string name, string sku, decimal price, UnitOfMeasure unitOfMeasure)
        {
            Id = Guid.NewGuid();
            Name = name;
            Sku = sku;
            Price = price;
            UnitOfMeasure = unitOfMeasure;
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
            Lots = new List<Lot>();
        }

        // Construtor privado para uso do Entity Framework.
        // ...
        // Construtor privado para uso do Entity Framework.
        private Product()
        {
            // Inicializa as propriedades para satisfazer o compilador.
            // O '!' (null-forgiving operator) diz ao compilador para não se preocupar.
            Name = null!;
            Sku = null!;
            Lots = null!;
        }
    }
}