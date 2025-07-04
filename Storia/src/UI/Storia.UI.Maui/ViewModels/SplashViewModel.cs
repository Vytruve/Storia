using CommunityToolkit.Mvvm.ComponentModel;

namespace Storia.UI.Maui.ViewModels
{
    /// <summary>
    /// ViewModel para a tela de carregamento (splash screen).
    /// </summary>
    public partial class SplashViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _statusMessage = "Inicializando...";
    }
}