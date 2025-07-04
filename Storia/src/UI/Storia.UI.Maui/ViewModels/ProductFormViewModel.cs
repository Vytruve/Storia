using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using Storia.Application.DTOs;
using Storia.Application.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Storia.UI.Maui.ViewModels
{
    /// <summary>
    /// ViewModel para o formul�rio de cria��o/edi��o de produtos.
    /// Implementa IQueryAttributable para receber o ID do produto via navega��o.
    /// </summary>
    public partial class ProductFormViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IProductService _productService;
        private readonly IValidator<ProductDto> _validator;

        [ObservableProperty]
        private ProductDto _product = new();

        [ObservableProperty]
        private string _title = "Novo Produto";

        [ObservableProperty]
        private bool _isBusy;

        private bool _isNewProduct = true;

        public ProductFormViewModel(IProductService productService, IValidator<ProductDto> validator)
        {
            _productService = productService;
            _validator = validator;
        }

        /// <summary>
        /// M�todo da interface IQueryAttributable. � chamado automaticamente pelo MAUI
        /// quando a p�gina � navegada, passando os par�metros da URL.
        /// </summary>
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("id", out var idObject) && Guid.TryParse(idObject.ToString(), out var id))
            {
                _isNewProduct = false;
                Title = "Editar Produto";
                await LoadProductAsync(id);
            }
        }

        private async Task LoadProductAsync(Guid id)
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                var productDto = await _productService.GetProductByIdAsync(id);
                if (productDto != null)
                {
                    Product = productDto;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", $"Falha ao carregar produto: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            var validationResult = _validator.Validate(Product);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                await Shell.Current.DisplayAlert("Dados Inv�lidos", errors, "OK");
                return;
            }

            if (IsBusy) return;
            try
            {
                IsBusy = true;
                if (_isNewProduct)
                {
                    await _productService.CreateProductAsync(Product);
                }
                else
                {
                    await _productService.UpdateProductAsync(Product);
                }
                // Navega de volta para a p�gina anterior (a lista de produtos)
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro ao Salvar", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task CancelAsync()
        {
            // Navega de volta
            await Shell.Current.GoToAsync("..");
        }
    }
}   