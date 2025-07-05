using Storia.UI.Maui.ViewModels;

namespace Storia.UI.Maui.Views
{
	public partial class ReportsPage : ContentPage
	{
		public ReportsPage(ReportsViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = viewModel;
		}
	}
}