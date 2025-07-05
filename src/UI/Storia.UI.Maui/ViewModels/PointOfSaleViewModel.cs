using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Storia.Application.DTOs;
using Storia.Application.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Storia.UI.Maui.ViewModels
{
    public partial class PointOfSaleViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        private readonly ISaleService _saleService;

        [ObservableProperty]
        private string _searchQuery = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalAmount))] // Notifica que TotalAmount também mudou
        private ObservableCollection<CartItemDto> _cartItems = new();

        public decimal TotalAmount => CartItems.Sum(item => item.Subtotal);

        [ObservableProperty]
        private bool _isBusy;

        public PointOfSaleViewModel(IProductService productService, ISaleService saleService)
        {
            _productService = productService;
            _saleService = saleService;

            // Assina o evento de mudança de propriedade de qualquer item no carrinho
            CartItems.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalAmount));
        }

        [RelayCommand]
        private async Task AddProductToCartAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery) || IsBusy) return;

            try
            {
                IsBusy = true;
                // Busca o produto pelo SKU. Em uma versão futura, poderia buscar por nome também.
                var product = await _productService.GetAllProductsAsync();
                var foundProduct = product.FirstOrDefault(p => p.Sku.Equals(SearchQuery, StringComparison.OrdinalIgnoreCase));

                if (foundProduct == null)
                {
                    await Shell.Current.DisplayAlert("Não Encontrado", "Nenhum produto encontrado com este SKU.", "OK");
                    return;
                }

                // Verifica se o produto já está no carrinho
                var existingCartItem = CartItems.FirstOrDefault(item => item.ProductId == foundProduct.Id);
                if (existingCartItem != null)
                {
                    // Se já existe, apenas incrementa a quantidade
                    existingCartItem.Quantity++;
                }
                else
                {
                    // Se não existe, adiciona um novo item ao carrinho
                    var cartItem = new CartItemDto
                    {
                        ProductId = foundProduct.Id,
                        Name = foundProduct.Name,
                        Sku = foundProduct.Sku,
                        Price = foundProduct.Price,
                        Quantity = 1
                    };
                    // Assina o evento para recalcular o total quando a quantidade do item mudar
                    cartItem.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(CartItemDto.Quantity)) OnPropertyChanged(nameof(TotalAmount)); };
                    CartItems.Add(cartItem);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", $"Falha ao adicionar produto: {ex.Message}", "OK");
            }
            finally
            {
                SearchQuery = string.Empty; // Limpa a busca
                IsBusy = false;
            }
        }

        [RelayCommand]
        private void RemoveFromCart(CartItemDto itemToRemove)
        {
            if (itemToRemove != null)
            {
                CartItems.Remove(itemToRemove);
            }
        }

        [RelayCommand]
        private async Task FinalizeSaleAsync()
        {
            if (!CartItems.Any())
            {
                await Shell.Current.DisplayAlert("Carrinho Vazio", "Adicione produtos antes de finalizar a venda.", "OK");
                return;
            }

            bool confirmed = await Shell.Current.DisplayAlert("Confirmar Venda", $"Total: {TotalAmount:C}. Deseja finalizar?", "Sim", "Não");
            if (!confirmed) return;

            IsBusy = true;
            try
            {
                var saleDto = new SaleDetailDto
                {
                    Items = CartItems.Select(ci => new SaleItemDto
                    {
                        ProductId = ci.ProductId,
                        QuantitySold = ci.Quantity,
                        PriceAtTimeOfSale = ci.Price
                    }).ToList()
                };

                await _saleService.CreateSaleAsync(saleDto);

                await Shell.Current.DisplayAlert("Sucesso", "Venda registrada com sucesso!", "OK");
                CartItems.Clear(); // Limpa o carrinho para a próxima venda
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro na Venda", $"Não foi possível registrar a venda: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}