using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Storia.UI.Maui.ViewModels
{
    /// <summary>
    /// Uma classe base para todos os ViewModels da aplicação.
    /// Implementa INotifyPropertyChanged para suportar o data binding do MAUI
    /// e fornece funcionalidades comuns como o gerenciamento do estado "IsBusy".
    /// Esta é uma peça fundamental para a arquitetura MVVM.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        // Evento que notifica a UI sobre a mudança de uma propriedade.
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _isBusy;
        /// <summary>
        /// Obtém ou define um valor que indica se o ViewModel está
        /// executando uma operação longa (ex: carregando dados da rede).
        /// A UI pode fazer binding a esta propriedade para mostrar um ActivityIndicator.
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _title = string.Empty;
        /// <summary>
        /// Obtém ou define o título da página associada a este ViewModel.
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// Método auxiliar para definir o valor de uma propriedade e notificar a UI.
        /// Ele verifica se o valor realmente mudou antes de disparar o evento,
        /// o que otimiza o desempenho.
        /// </summary>
        /// <typeparam name="T">O tipo da propriedade.</typeparam>
        /// <param name="backingStore">A variável de campo de apoio (backing field).</param>
        /// <param name="value">O novo valor.</param>
        /// <param name="propertyName">O nome da propriedade (preenchido automaticamente).</param>
        /// <returns>True se o valor foi alterado, False caso contrário.</returns>
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