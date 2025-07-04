using FluentValidation;
//using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Storia.Application.Interfaces;
using Storia.Application.Services;
using Storia.Application.Validation;
using Storia.Core.Interfaces;
using Storia.Infrastructure.Persistence;
using Storia.UI.Maui.ViewModels;
using Storia.UI.Maui.Views;
using System.Reflection;
using CommunityToolkit.Maui; // Adicionando o using necessário

namespace Storia.UI.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp(serviceProvider => serviceProvider.GetRequiredService<App>())
                .UseMauiCommunityToolkit() // A chamada para o Community Toolkit está aqui, no lugar certo.
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("FontAwesome6-Solid.otf", "FASolid");
                });

            // --- 1. Carregar Configurações do appsettings.json ---
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("Storia.UI.Maui.appsettings.json");
            if (stream != null)
            {
                var config = new ConfigurationBuilder()
                                .AddJsonStream(stream)
                                .Build();
                builder.Configuration.AddConfiguration(config);
            }

            // --- 2. Configuração da Injeção de Dependência (DI) ---
            ConfigureServices(builder.Services, builder.Configuration);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // --- 3. Executar Migrações do Banco de Dados na Inicialização ---
            // Esta lógica foi movida para o App.xaml.cs para funcionar com a tela de splash.
            // Se não estivéssemos usando a tela de splash, este seria o lugar certo.

            return app;
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // --- Camada de Infraestrutura ---
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // --- Camada de Aplicação ---
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IInventoryService, InventoryService>();

            // --- Validação ---
            services.AddValidatorsFromAssemblyContaining<ProductValidator>();

            // --- Camada de UI (ViewModels e Pages) ---
            services.AddSingleton<App>();
            services.AddSingleton<AppShell>();

            services.AddTransient<SplashViewModel>();
            services.AddTransient<SplashPage>();

            services.AddTransient<DashboardViewModel>();
            services.AddTransient<DashboardPage>();

            services.AddTransient<ProductManagementViewModel>();
            services.AddTransient<ProductManagementPage>();

            services.AddTransient<ProductFormViewModel>();
            services.AddTransient<ProductFormPage>();

            services.AddTransient<PointOfSaleViewModel>();
            services.AddTransient<PointOfSalePage>();

            services.AddTransient<ReportsViewModel>();
            services.AddTransient<ReportsPage>();
        }
    }
}