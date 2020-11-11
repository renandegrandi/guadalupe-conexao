using Guadalupe.Conexao.App.Extensions;
using Guadalupe.Conexao.App.Repository.DTO;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.App.Repository
{
    public static class ConexaoHttpClient 
    {
        private static readonly HttpClient HttpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30),
            BaseAddress = new Uri("https://localhost:44307/api", UriKind.Absolute)
        };

        public static Task AuthenticationAsync(AuthenticationDto authentication, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public static async Task SendNewCodeByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var url = $"user/{email}/send_code";

            using (var request = new HttpRequestMessage(HttpMethod.Put, url)) 
            {
                var result =  await HttpClient.SendAsync(request, cancellationToken);

                await result.GetResultAsync<int>();
            }
        }
    }
}
