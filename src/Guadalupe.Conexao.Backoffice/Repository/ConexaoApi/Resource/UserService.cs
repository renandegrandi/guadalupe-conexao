using Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Extension;
using Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Resource
{
    sealed class UserService: IUserService
    {
        public const string Controller = "api/user";

        #region Dependencies

        private readonly IHttpClientFactory _clientFactory;

        #endregion

        public UserService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<AuthenticationTokenDto> AuthenticationAsync(AuthenticationDto authentication, CancellationToken cancellationToken)
        {
            var url = $"{Controller}/token";

            var httpContent = new StringContent(JsonSerializer.Serialize(authentication));

            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var client = _clientFactory.CreateClient("guadalupe-conexao-api");

            var response = await client.PostAsync(url, httpContent, cancellationToken);

            return await response.GetResultAsync<AuthenticationTokenDto>();
        }
    }
}
