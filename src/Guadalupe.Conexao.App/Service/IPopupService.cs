using System.Threading.Tasks;

namespace Guadalupe.Conexao.App.Service
{
    public interface IPopupService
    {
        Task ShowErrorMessageAsync(string message);
        Task ShowAsync(string title, string message, string cancel);
    }
}
