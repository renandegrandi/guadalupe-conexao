using System.Threading.Tasks;

namespace Guadalupe.Conexao.App.Service
{
    sealed class PopupService : IPopupService
    {
        public Task ShowErrorMessageAsync(string message)
        {
            return ShowAsync("Ops, algo deu errado!", message, "Fechar");
        }

        public Task ShowAsync(string title, string message, string cancel)
        {
            return App.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
