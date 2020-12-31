using Guadalupe.Conexao.App.Extensions;
using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Repository.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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

            Timeout = TimeSpan.FromSeconds(3),
            BaseAddress = new Uri(Configuration.ConexaoApi)
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
        public static async Task<List<NoticeDto>> GetByDateAsync(DateTime? last, CancellationToken cancellationToken) 
        {
            var url = $"notice/last_updated";

            if (last.HasValue && !last.Equals(DateTime.MinValue)) 
            {
                url += $"?data_hora={last.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.sssZ")}";
            }

            var user = App.SessionService.GetUser();

            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("bearer", user.ConexaoToken);

                    var result = await HttpClient.SendAsync(request, cancellationToken)
                        .ConfigureAwait(false);

                    return await result.GetResultAsync<List<NoticeDto>>()
                        .ConfigureAwait(false);
                }
            }
            catch (UnauthorizedException)
            {
                await RefreshTokenAsync(user, cancellationToken);

                return await GetByDateAsync(last, cancellationToken);
            }
        }
        private static async Task RefreshTokenAsync(User user, CancellationToken cancellationToken) 
        {
            var autenticated = await AuthenticationAsync(new AuthenticationDto
            {
                GrantType = AuthenticationDto.GrantTypes.refresh_token,
                RefreshToken = user.ConexaoRefreshToken
            }, cancellationToken);

            user.RefreshToken(autenticated.AccessToken, autenticated.RefreshToken);

            await App.UserRepository.UpdateAsync(user);

            App.SessionService.SetUser(user);
        }
    }
}
