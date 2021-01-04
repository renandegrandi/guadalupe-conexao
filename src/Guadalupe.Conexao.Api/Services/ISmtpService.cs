using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Services
{
    public interface ISmtpService
    {
        Task SendAsync(string email, string subject, string body);
    }
}
