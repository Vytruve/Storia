using System;
using System.Collections.Generic;
using System.Linq;

namespace Storia.Core.Entities
{
    /// <summary>
    /// Representa uma única transação de venda (cabeçalho do pedido).
    /// Agrupa todos os itens vendidos em uma única operação.
    /// </summary>
    public class Sale
    {
        /// <summary>
        /// Identificador único da transação de venda.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Data e hora em que a venda foi concluída.
        /// </summary>
        public DateTimeOffset TransactionDate { get; private set; }

        /// <summary>
        /// O valor total calculado da venda.
        /// É a soma dos preços de todos os SaleItems.
        /// </summary>
        public decimal TotalAmount => Items.Sum(item => item.QuantitySold * item.PriceAtTimeOfSale);

        /// <summary>
        /// Propriedade de navegação para os itens detalhados desta venda.
        /// Esta é a coleção de "linhas" do recibo.
        /// </summary>
        public ICollection<SaleItem> Items { get; private set; }

        /// <summary>
        /// Construtor para uma nova Venda.
        /// </summary>
        public Sale()
        {
            Id = Guid.NewGuid();
            TransactionDate = DateTimeOffset.UtcNow;
            Items = new List<SaleItem>();

        }

        // Construtor privado para uso do Entity Framework.
        // ...
        // Construtor privado para uso do Entity Framework.
        private Sale(Guid id, DateTimeOffset transactionDate)
        {
            Id = id;
            TransactionDate = transactionDate;
            Items = new List<SaleItem>(); // Este já está correto, mas se não estivesse...
                                          // Se 'Items' não fosse inicializado, adicionaríamos: Items = null!;
        }
    }
}