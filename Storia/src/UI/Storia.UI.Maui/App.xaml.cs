using Microsoft.EntityFrameworkCore;
using Storia.Infrastructure.Persistence;
using Storia.UI.Maui.ViewModels;
using Storia.UI.Maui.Views;

namespace Storia.UI.Maui
{
    // A palavra-chave 'partial' é a correção mais importante.
    public partial class App : Microsoft.Maui.Controls.Application
    {
        private readonly IServiceProvider _serviceProvider;
        private Window? _splashWindow;

        public App(IServiceProvider serviceProvider)
        {
            // Esta chamada conecta o XAML ao C#. Ela SÓ funciona se a classe for 'partial'
            // e o 'x:Class' no XAML estiver correto.
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        protected override async void OnStart()
        {
            await ShowSplashScreen();

            // Verificação de nulo para garantir que a janela do splash foi criada.
            if (_splashWindow == null)
            {
                // Se algo deu errado, apenas carregue a página principal para evitar um crash.
                MainPage = _serviceProvider.GetRequiredService<AppShell>();
                return;
            }

            await Task.Run(async () =>
            {
                // Acessa o ViewModel de forma segura.
                var splashViewModel = _splashWindow.Page?.BindingContext as SplashViewModel;

                await UpdateStatus(splashViewModel, "Verificando banco de dados...");
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    await dbContext.Database.MigrateAsync();
                }

                await UpdateStatus(splashViewModel, "Iniciando serviços...");
                await Task.Delay(1000);

                await UpdateStatus(splashViewModel, "Pronto!");
                await Task.Delay(500);
            });

            MainPage = _serviceProvider.GetRequiredService<AppShell>();

            // A verificação de nulo aqui já existia e está correta.
            if (_splashWindow != null)
            {
                await Task.Delay(100);
                CloseWindow(_splashWindow);
            }
        }

        private async Task ShowSplashScreen()
        {
            var splashPage = _serviceProvider.GetRequiredService<SplashPage>();
            _splashWindow = new Window(splashPage) { Width = 640, Height = 400 };

#if WINDOWS
            var nativeWindow = _splashWindow.Handler.PlatformView as Microsoft.UI.Xaml.Window;
            if (nativeWindow != null)
            {
                var presenter = nativeWindow.AppWindow.Presenter as Microsoft.UI.Windowing.OverlappedPresenter;
                presenter?.SetBorderAndTitleBar(false, false);
            }
#endif

            OpenWindow(_splashWindow);
            await Task.CompletedTask;
        }

        private async Task UpdateStatus(SplashViewModel? vm, string message)
        {
            if (vm != null)
            {
                MainThread.BeginInvokeOnMainThread(() => vm.StatusMessage = message);
            }
            await Task.Delay(500);
        }
    }
}