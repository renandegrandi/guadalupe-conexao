using System.Threading.Tasks;

namespace Guadalupe.Conexao.App.Service
{
    public interface IWhatsappService
    {
        Task OpenAsync(string phone, string message);
    }
}
