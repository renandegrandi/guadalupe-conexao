using System.ComponentModel;
using Xamarin.Forms;

namespace Guadalupe.Conexao.App.ViewModel
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Dependencies

        public readonly INavigation _navigation;

        #endregion

        protected ViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
