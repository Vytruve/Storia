using Storia.UI.Maui.ViewModels;

namespace Storia.UI.Maui.Views
{
    public partial class PointOfSalePage : ContentPage
    {
        public PointOfSalePage(PointOfSaleViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}