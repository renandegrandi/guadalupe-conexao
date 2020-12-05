using Guadalupe.Conexao.App.Extensions;
using Guadalupe.Conexao.App.Repository.DTO;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.App.Repository
{
    public static class ConexaoHttpClient 
    {
        public const string PrettyMessage = "Estamos com um problema de indisponibilidade, por favor tente novamente se o erro persistir, contante o adminstrador do serviço!";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded", Justification = "Está pendente a implementação de um arquivo de configuração.")]
        private static readonly HttpClient HttpClient = new HttpClient
        {
            //TODO : Implementar configuração para a url da aplicação.

            Timeout = TimeSpan.FromSeconds(30),
            BaseAddress = new Uri("http://192.168.1.113:49981/api/")
        };

        public static async Task<AuthenticationTokenDto> AuthenticationAsync(AuthenticationDto authentication, CancellationToken cancellationToken)
        {
            var url = $"user/token";

            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                var json = JsonConvert.SerializeObject(authentication);

                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await HttpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);

                return await result.GetResultAsync<AuthenticationTokenDto>()
                    .ConfigureAwait(false);
            }
        }

        public static async Task SendNewCodeByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var url = $"user/{email}/send_code";

            using (var request = new HttpRequestMessage(HttpMethod.Put, url)) 
            {
                var result = await HttpClient.SendAsync(request, cancellationToken)
                    .ConfigureAwait(false);

                await result.GetResultAsync<int>()
                    .ConfigureAwait(false);
            }
        }
    }
}
