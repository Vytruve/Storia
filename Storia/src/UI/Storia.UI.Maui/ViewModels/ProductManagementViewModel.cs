using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Storia.Application.DTOs;
using Storia.Application.Interfaces;
using Storia.UI.Maui.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Storia.UI.Maui.ViewModels
{
    /// <summary>
    /// ViewModel para a p�gina de gerenciamento de produtos.
    /// Gerencia a lista de produtos e as a��es de CRUD.
    /// </summary>
    public partial class ProductManagementViewModel : ObservableObject
    {
        private readonly IProductService _productService;

        [ObservableProperty]
        private ObservableCollection<ProductDto> _products = new();

        [ObservableProperty]
        private bool _isBusy;

        public ProductManagementViewModel(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Comando para carregar ou recarregar a lista de produtos do servi�o.
        /// </summary>
        [RelayCommand]
        private async Task LoadProductsAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                var productList = await _productService.GetAllProductsAsync();

                Products.Clear();
                foreach (var product in productList)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", $"Falha ao carregar produtos: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Comando para navegar para a p�gina de formul�rio para adicionar um novo produto.
        /// </summary>
        [RelayCommand]
        private async Task GoToAddProductPageAsync()
        {
            // Navega para a p�gina de formul�rio sem passar nenhum ID.
            await Shell.Current.GoToAsync(nameof(ProductFormPage));
        }

        /// <summary>
        /// Comando para navegar para a p�gina de formul�rio para editar um produto existente.
        /// </summary>
        /// <param name="product">O DTO do produto a ser editado.</param>
        [RelayCommand]
        private async Task GoToEditProductPageAsync(ProductDto product)
        {
            if (product == null) return;

            // Navega para a p�gina de formul�rio passando o ID do produto como um par�metro de consulta.
            // Esta � a maneira elegante de comunicar o contexto para a pr�xima p�gina.
            await Shell.Current.GoToAsync($"{nameof(ProductFormPage)}?id={product.Id}");
        }

        /// <summary>
        /// Comando para deletar um produto.
        /// </summary>
        /// <param name="product">O DTO do produto a ser deletado.</param>
        [RelayCommand]
        private async Task DeleteProductAsync(ProductDto product)
        {
            if (product == null) return;

            bool confirmed = await Shell.Current.DisplayAlert(
                "Confirmar Exclus�o",
                $"Voc� tem certeza que deseja excluir o produto '{product.Name}'?",
                "Sim, Excluir", "Cancelar");

            if (confirmed)
            {
                try
                {
                    await _productService.DeleteProductAsync(product.Id);
                    // Remove da cole��o local para uma atualiza��o instant�nea da UI.
                    Products.Remove(product);
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Erro", $"Falha ao excluir produto: {ex.Message}", "OK");
                }
            }
        }
    }
}