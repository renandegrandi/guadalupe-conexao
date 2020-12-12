using Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Extension;
using Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Resource
{
    public class NoticeService: INoticeService
    {
        public const string Controller = "api/notice";

        #region Dependecies

        private readonly IUserService _userService;
        private readonly IHttpClientFactory _clientFactory;

        #endregion

        public NoticeService(IUserService userService,
            IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _userService = userService;
        }

        public async Task<Guid> AdicionarAsync(NoticeCreateDto notice, CancellationToken cancellationToken) 
        {
            var token = await _userService.AuthenticationAsync(Startup.authentication, cancellationToken)
                .ConfigureAwait(false);

            var url = $"{Controller}";

            var httpContent = new StringContent(JsonSerializer.Serialize(notice));

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var client = _clientFactory.CreateClient("guadalupe-conexao-api");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var result = await client.PostAsync(url, httpContent, cancellationToken)
                .ConfigureAwait(false);

            return await result.GetResultAsync<Guid>()
                .ConfigureAwait(false);
        }

        public async Task<PaginatedResult<NoticeDto>> GetAsync(string title, int index, int size, CancellationToken cancellationToken) 
        {
            var token = await _userService.AuthenticationAsync(Startup.authentication, cancellationToken)
                .ConfigureAwait(false);

            var url = $"{Controller}";

            var @params = new List<string>();

            if (!string.IsNullOrWhiteSpace(title))
                @params.Add($"title={title}");

            if (index > 0)
                @params.Add($"index={index}");

            if (size > 0)
                @params.Add($"size={size}");

            if (@params.Any())
                url += $"?{string.Join("&", @params)}";

            var client = _clientFactory.CreateClient("guadalupe-conexao-api");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var result = await client.GetAsync(url, cancellationToken)
                .ConfigureAwait(false);

            var registers =  await result.GetResultAsync<List<NoticeDto>>()
                .ConfigureAwait(false);

            var total = result.Headers.GetTotalCount();

            return new PaginatedResult<NoticeDto>
            {
                Registers = registers,
                TotalRegisters = total
            };
        }

        public async Task<NoticeDto> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
        {
            var token = await _userService.AuthenticationAsync(Startup.authentication, cancellationToken)
                .ConfigureAwait(false);

            var url = $"{Controller}/{id}";

            var client = _clientFactory.CreateClient("guadalupe-conexao-api");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var result = await client.GetAsync(url, cancellationToken)
                .ConfigureAwait(false);

            return await result.GetResultAsync<NoticeDto>()
                .ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var token = await _userService.AuthenticationAsync(Startup.authentication, cancellationToken)
                .ConfigureAwait(false);

            var url = $"{Controller}/{id}";

            var client = _clientFactory.CreateClient("guadalupe-conexao-api");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, url)) 
            {
                var result = await client.SendAsync(httpRequestMessage, cancellationToken)
                    .ConfigureAwait(false);

                await result.GetResultAsync()
                    .ConfigureAwait(false);
            }
        }
    }
}
