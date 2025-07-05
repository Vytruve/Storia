using Storia.Core.Enums;
using Storia.UI.Maui.ViewModels;

namespace Storia.UI.Maui.Views
{
    public partial class ProductFormPage : ContentPage
    {
        public ProductFormPage(ProductFormViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            // Popula o Picker de Unidade de Medida com os valores do Enum.
            // Isso � feito no code-behind porque � uma preocupa��o puramente da View.
            UnitOfMeasurePicker.ItemsSource = Enum.GetNames(typeof(UnitOfMeasure));
        }
    }
}