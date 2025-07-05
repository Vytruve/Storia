using Storia.UI.Maui.Views;

namespace Storia.UI.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registra as rotas para navegação.
            // Isso conecta a string de rota (ex: "ProductManagementPage") com o tipo da página.
            // É uma boa prática para uma navegação robusta e programática.
            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
            Routing.RegisterRoute(nameof(PointOfSalePage), typeof(PointOfSalePage));
            Routing.RegisterRoute(nameof(ProductManagementPage), typeof(ProductManagementPage));
            Routing.RegisterRoute(nameof(ReportsPage), typeof(ReportsPage));
            Routing.RegisterRoute(nameof(ProductFormPage), typeof(ProductFormPage));

        }
    }
}