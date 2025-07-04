using System;

namespace Storia.Core.Entities
{
    /// <summary>
    /// Representa um lote específico de um produto.
    /// Essencial para rastreabilidade, controle de validade (FIFO/FEFO) e cálculo de custo.
    /// </summary>
    public class Lot
    {
        /// <summary>
        /// Identificador único para o lote.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Chave estrangeira para o produto ao qual este lote pertence.
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Propriedade de navegação para o produto associado.
        /// O Entity Framework usará isso para criar o relacionamento.
        /// </summary>
        public Product Product { get; private set; }

        /// <summary>
        /// Quantidade de itens do produto neste lote.
        /// Usamos 'decimal' para suportar quantidades fracionadas (ex: 10.5 Kg).
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Preço de custo por unidade do produto neste lote.
        /// Permite calcular a margem de lucro de forma precisa.
        /// </summary>
        public decimal PurchasePrice { get; private set; }

        /// <summary>
        /// Número ou código de identificação do lote, fornecido pelo fabricante ou distribuidor.
        /// </summary>
        public string? LotNumber { get; set; }

        /// <summary>
        /// Data em que o lote foi recebido no estoque.
        /// </summary>
        public DateTimeOffset ArrivalDate { get; private set; }

        /// <summary>
        /// Data de validade dos produtos neste lote.
        /// Pode ser nulo para produtos que não expiram.
        /// </summary>
        public DateTimeOffset? ExpirationDate { get; set; }

        /// <summary>
        /// Data e hora de criação do registro do lote.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// Construtor para a entidade Lote.
        /// </summary>
        /// <param name="productId">ID do produto associado.</param>
        /// <param name="quantity">Quantidade inicial no lote.</param>
        /// <param name="purchasePrice">Preço de custo por unidade.</param>
        /// <param name="arrivalDate">Data de chegada do lote.</param>
        public Lot(Guid productId, decimal quantity, decimal purchasePrice, DateTimeOffset arrivalDate)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            PurchasePrice = purchasePrice;
            ArrivalDate = arrivalDate;
            CreatedAt = DateTimeOffset.UtcNow;
            Product = null!; // Garanta que esta linha existe
        }

        // Construtor privado para uso do Entity Framework.
        private Lot()
        {
            Product = null!;
        }
    }
}