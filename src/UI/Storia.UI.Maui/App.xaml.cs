using Microsoft.EntityFrameworkCore;
using Storia.Infrastructure.Persistence;
using Storia.UI.Maui.ViewModels;
using Storia.UI.Maui.Views;
using System.Diagnostics; // Adicionado para logar o erro

namespace Storia.UI.Maui
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        private readonly IServiceProvider _serviceProvider;
        private Window? _splashWindow;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        protected override async void OnStart()
        {
            await ShowSplashScreen();

            if (_splashWindow == null)
            {
                MainPage = _serviceProvider.GetRequiredService<AppShell>();
                return;
            }

            // Executa a inicialização e captura qualquer erro
            bool initializationSucceeded = await InitializeInBackground();

            if (initializationSucceeded)
            {
                MainPage = _serviceProvider.GetRequiredService<AppShell>();
                if (_splashWindow != null)
                {
                    await Task.Delay(100);
                    CloseWindow(_splashWindow);
                }
            }
            else
            {
                // Se a inicialização falhou, o splash screen já mostrou o erro.
                // Podemos fechar o app ou mostrar uma página de erro mínima.
                // Por enquanto, vamos apenas fechar a janela do splash.
                await Task.Delay(5000); // Deixa o usuário ler o erro
                CloseWindow(_splashWindow);
            }
        }

        private async Task<bool> InitializeInBackground()
        {
            var splashViewModel = _splashWindow?.Page?.BindingContext as SplashViewModel;

            try
            {
                await Task.Run(async () =>
                {
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
                return true; // Sucesso
            }
            catch (Exception ex)
            {
                // Loga o erro para o Output de depuração
                Debug.WriteLine($"FATAL INITIALIZATION ERROR: {ex}");

                // Atualiza a UI para mostrar a falha
                await UpdateStatus(splashViewModel, $"Erro na inicialização: {ex.Message}");
                return false; // Falha
            }
        }

        private async Task ShowSplashScreen()
        {
            // ... (este método permanece o mesmo)
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
            // ... (este método permanece o mesmo)
            if (vm != null)
            {
                MainThread.BeginInvokeOnMainThread(() => vm.StatusMessage = message);
            }
            await Task.Delay(500);
        }
    }
}