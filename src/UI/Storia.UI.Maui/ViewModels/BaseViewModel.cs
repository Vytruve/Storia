using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Storia.UI.Maui.ViewModels
{
    /// <summary>
    /// Uma classe base para todos os ViewModels da aplica��o.
    /// Implementa INotifyPropertyChanged para suportar o data binding do MAUI
    /// e fornece funcionalidades comuns como o gerenciamento do estado "IsBusy".
    /// Esta � uma pe�a fundamental para a arquitetura MVVM.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        // Evento que notifica a UI sobre a mudan�a de uma propriedade.
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _isBusy;
        /// <summary>
        /// Obt�m ou define um valor que indica se o ViewModel est�
        /// executando uma opera��o longa (ex: carregando dados da rede).
        /// A UI pode fazer binding a esta propriedade para mostrar um ActivityIndicator.
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _title = string.Empty;
        /// <summary>
        /// Obt�m ou define o t�tulo da p�gina associada a este ViewModel.
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// M�todo auxiliar para definir o valor de uma propriedade e notificar a UI.
        /// Ele verifica se o valor realmente mudou antes de disparar o evento,
        /// o que otimiza o desempenho.
        /// </summary>
        /// <typeparam name="T">O tipo da propriedade.</typeparam>
        /// <param name="backingStore">A vari�vel de campo de apoio (backing field).</param>
        /// <param name="value">O novo valor.</param>
        /// <param name="propertyName">O nome da propriedade (preenchido automaticamente).</param>
        /// <returns>True se o valor foi alterado, False caso contr�rio.</returns>
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Dispara o evento PropertyChanged.
        /// </summary>
        /// <param name="propertyName">O nome da propriedade que mudou.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}