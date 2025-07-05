using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Storia.Application.DTOs;
using Storia.Application.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Storia.UI.Maui.ViewModels
{
    public partial class ReportsViewModel : ObservableObject
    {
        private readonly ISaleService _saleService;

        [ObservableProperty]
        private DateTime _startDate;

        [ObservableProperty]
        private DateTime _endDate;

        [ObservableProperty]
        private ObservableCollection<SaleDetailDto> _salesReport = new();

        [ObservableProperty]
        private decimal _totalRevenueInPeriod;

        [ObservableProperty]
        private int _totalSalesCountInPeriod;

        [ObservableProperty]
        private bool _isBusy;

        // Propriedade para controlar a visibilidade da área de resultados
        [ObservableProperty]
        private bool _isReportGenerated = false;

        public ReportsViewModel(ISaleService saleService)
        {
            _saleService = saleService;
            var now = DateTime.Now;
            // CORREÇÃO: Usar as propriedades com letra maiúscula
            StartDate = new DateTime(now.Year, now.Month, 1);
            EndDate = now;
        }

        [RelayCommand]
        private async Task GenerateReportAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                IsReportGenerated = false;
                SalesReport.Clear();

                // CORREÇÃO FINAL: Usar as propriedades com letra maiúscula aqui também.
                var endDateWithTime = EndDate.Date.AddDays(1).AddTicks(-1);
                var results = await _saleService.GetSalesByDateRangeAsync(new DateTimeOffset(StartDate.Date), new DateTimeOffset(endDateWithTime));

                foreach (var sale in results)
                {
                    SalesReport.Add(sale);
                }

                TotalRevenueInPeriod = SalesReport.Sum(s => s.TotalAmount);
                TotalSalesCountInPeriod = SalesReport.Count;

                IsReportGenerated = true;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", $"Falha ao gerar relatório: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}