using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace Storia.Application.DTOs
{
    /// <summary>
    /// DTO que representa um item no carrinho de compras do Ponto de Venda.
    /// Herda de ObservableObject para permitir que a UI reaja a mudanças na quantidade.
    /// Agora inclui comandos para modificar sua própria quantidade.
    /// </summary>
    public partial class CartItemDto : ObservableObject
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public decimal Price { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Subtotal))]
        private decimal _quantity;

        public decimal Subtotal => Price * Quantity;

        /// <summary>
        /// Comando para incrementar a quantidade do item.
        /// </summary>
        [RelayCommand]
        private void IncrementQuantity()
        {
            Quantity++;
        }

        /// <summary>
        /// Comando para decrementar a quantidade do item, com um mínimo de 1.
        /// </summary>
        [RelayCommand]
        private void DecrementQuantity()
        {
            if (Quantity > 1)
            {
                Quantity--;
            }
        }
    }
}