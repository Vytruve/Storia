using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Storia.Application.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Storia.UI.Maui.ViewModels
{
    /// <summary>
    /// ViewModel para a p�gina do Dashboard.
    /// Herda de ObservableObject, que � uma implementa��o otimizada de INotifyPropertyChanged
    /// fornecida pelo CommunityToolkit.Mvvm.
    /// </summary>
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;

        // O atributo [ObservableProperty] gera automaticamente a propriedade
        // completa com backing field e chamada a OnPropertyChanged.
        // Ex: private decimal _revenueToday; public decimal RevenueToday { get => ... set => ... }
        [ObservableProperty]
        private decimal _revenueToday;

        [ObservableProperty]
        private int _salesCountToday;

        [ObservableProperty]
        private int _lowStockProductsCount;

        [ObservableProperty]
        private bool _isRefreshing;

        public DashboardViewModel(ISaleService saleService, IProductService productService)
        {
            _saleService = saleService;
            _productService = productService;
        }

        /// <summary>
        /// O atributo [RelayCommand] gera um ICommand chamado 'LoadDataCommand'.
        /// Este comando pode ser vinculado a um evento da p�gina (ex: OnAppearing)
        /// ou a um bot�o de "Atualizar".
        /// </summary>
        [RelayCommand]
        private async Task LoadDataAsync()
        {
            if (IsRefreshing)
                return;

            try
            {
                IsRefreshing = true;

                // 1. Carregar dados de vendas
                var today = DateTimeOffset.Now;
                var sales = await _saleService.GetSalesByDateRangeAsync(today.Date, today.Date.AddDays(1).AddTicks(-1));

                RevenueToday = sales.Sum(s => s.TotalAmount);
                SalesCountToday = sales.Count();

                // 2. Carregar dados de produtos para alerta de estoque
                var products = await _productService.GetAllProductsAsync();

                // Regra de neg�cio: Considera "estoque baixo" como <= 5 unidades.
                // Isso poderia vir de um appsettings.json.
                LowStockProductsCount = products.Count(p => p.TotalStockQuantity <= 5);

                // Futuramente: Adicionar l�gica para produtos perto de expirar.
            }
            catch (Exception ex)
            {
                // Em uma aplica��o real, logar�amos o erro e mostrar�amos um alerta ao usu�rio.
                await Shell.Current.DisplayAlert("Erro", $"N�o foi poss�vel carregar os dados do dashboard: {ex.Message}", "OK");
            }
            finally
            {
                IsRefreshing = false;
            }
        }
    }
}