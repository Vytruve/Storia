using Storia.UI.Maui.Views;

namespace Storia.UI.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registra as rotas para navega��o.
            // Isso conecta a string de rota (ex: "ProductManagementPage") com o tipo da p�gina.
            // � uma boa pr�tica para uma navega��o robusta e program�tica.
            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
            Routing.RegisterRoute(nameof(PointOfSalePage), typeof(PointOfSalePage));
            Routing.RegisterRoute(nameof(ProductManagementPage), typeof(ProductManagementPage));
            Routing.RegisterRoute(nameof(ReportsPage), typeof(ReportsPage));
            Routing.RegisterRoute(nameof(ProductFormPage), typeof(ProductFormPage));

        }
    }
}