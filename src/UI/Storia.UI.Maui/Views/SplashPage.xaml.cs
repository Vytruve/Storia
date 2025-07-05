using Storia.UI.Maui.ViewModels;

namespace Storia.UI.Maui.Views
{
    public partial class SplashPage : ContentPage
    {
        public SplashPage(SplashViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}