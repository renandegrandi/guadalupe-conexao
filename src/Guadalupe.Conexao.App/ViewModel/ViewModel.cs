using System.ComponentModel;
using System.Threading;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App.ViewModel
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Dependencies

        public readonly INavigation _navigation;
        public readonly CancellationToken _cancellationToken;

        #endregion

        protected ViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _cancellationToken = new CancellationToken();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
