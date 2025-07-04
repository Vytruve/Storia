using Storia.UI.Maui.ViewModels;

namespace Storia.UI.Maui.Views
{
	public partial class DashboardPage : ContentPage
	{
		public DashboardPage(DashboardViewModel viewModel)
		{
			InitializeComponent();
			// Conecta a View ao seu ViewModel. Toda a lógica e dados
			// agora fluem do viewModel para o XAML através do BindingContext.
			BindingContext = viewModel;
		}
	}
}