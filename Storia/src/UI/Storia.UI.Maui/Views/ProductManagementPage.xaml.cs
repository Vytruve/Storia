using Storia.UI.Maui.ViewModels;

namespace Storia.UI.Maui.Views
{
    public partial class ProductManagementPage : ContentPage
    {
        public ProductManagementPage(ProductManagementViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}