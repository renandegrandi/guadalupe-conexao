using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Services
{
    public interface INotificationService
    {
        Task SendAsync(string title, string body, string image, Dictionary<string, string> data, CancellationToken cancellationToken);
        Task SendByTopicAsync(string title, string body, string image, string topic, Dictionary<string, string> data, CancellationToken cancellationToken);
    }
}
