using System;

namespace Storia.Core.Entities
{
    /// <summary>
    /// Representa um item individual dentro de uma transação de venda.
    /// Esta entidade é a "ponte" entre uma Venda (Sale) e um Produto (Product),
    /// armazenando dados específicos da transação.
    /// </summary>
    public class SaleItem
    {
        /// <summary>
        /// Identificador único para este item da venda.
        /// </summary>
        public Guid Id { get; private set; }

        // --- Chaves Estrangeiras e Navegação ---

        /// <summary>
        /// Chave estrangeira para a Venda à qual este item pertence.
        /// </summary>
        public Guid SaleId { get; private set; }

        /// <summary>
        /// Propriedade de navegação para a Venda.
        /// </summary>
        public Sale Sale { get; private set; }

        /// <summary>
        /// Chave estrangeira para o Produto que foi vendido.
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Propriedade de navegação para o Produto.
        /// </summary>
        public Product Product { get; private set; }

        // --- Dados da Transação (Snapshot) ---

        /// <summary>
        /// A quantidade do produto que foi vendida nesta transação.
        /// </summary>
        public decimal QuantitySold { get; private set; }

        /// <summary>
        /// O preço unitário do produto no exato momento da venda.
        /// Essencial para a integridade histórica, pois o preço do produto pode mudar.
        /// </summary>
        public decimal PriceAtTimeOfSale { get; private set; }

        /// <summary>
        /// O custo unitário do produto no momento da venda.
        /// Este valor é obtido do lote (Lot) de onde o produto foi retirado.
        /// Crucial para calcular a margem de lucro exata por item.
        /// </summary>
        public decimal CostAtTimeOfSale { get; private set; }

        /// <summary>
        /// Construtor para um novo item de venda.
        /// </summary>
        public SaleItem(Guid saleId, Guid productId, decimal quantitySold, decimal priceAtTimeOfSale, decimal costAtTimeOfSale)
        {
            Id = Guid.NewGuid();
            SaleId = saleId;
            ProductId = productId;
            QuantitySold = quantitySold;
            PriceAtTimeOfSale = priceAtTimeOfSale;
            CostAtTimeOfSale = costAtTimeOfSale;
            Product = null!; // Garanta que esta linha existe
            Sale = null!;
        }

        // Construtor privado para uso do Entity Framework.
        // ...
        // Construtor privado para uso do Entity Framework.
        private SaleItem()
        {
            Sale = null!;
            Product = null!;
        }
    }
}