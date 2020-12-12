using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Extension
{
    static class HttpResponseMessageExtension
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S112:General exceptions should never be thrown", Justification = "<Pending>")]
        public static async Task GetResultAsync(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.NoContent:
                    break;
                case HttpStatusCode.Unauthorized:
                    throw new NotImplementedException();
                    //throw new UnauthorizedException();
                case HttpStatusCode.Forbidden:
                    throw new NotImplementedException();
                    //throw new ForbiddenException();
                case HttpStatusCode.BadRequest:

                    var erros = JsonSerializer.Deserialize<IDictionary<string, string[]>>(content);

                    var messages = new List<string>();

                    foreach (var item in erros)
                    {
                        messages.Add($"({item.Key}: {string.Join(",", item.Value)})");
                    }

                    throw new Exception(string.Join(",", messages));

                    //throw new BrokenRuleException(string.Join(",", messages));
                default:
                    throw new Exception("Houve algum problema na comunicação com a API-Legacy");


                    //try
                    //{
                    //    erro = JsonConvert.DeserializeObject<ErroDto>(content);
                    //}
                    //catch (Exception)
                    //{
                    //    throw new Exception("Houve algum problema na comunicação com a API-Legacy");
                    //}

                    //throw new BrokenRuleException(erro.Message ?? erro.Descricao);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S112:General exceptions should never be thrown", Justification = "<Pending>")]
        public static async Task<T> GetResultAsync<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { 
                        PropertyNameCaseInsensitive = true
                    });
                case HttpStatusCode.NoContent:
                    return default;
                case HttpStatusCode.Created:
                    var id = response.Headers.GetIdByLocation();
                    return (T)Convert.ChangeType(id, typeof(T));
                case HttpStatusCode.Unauthorized:
                    throw new NotImplementedException();
                //throw new UnauthorizedException();
                case HttpStatusCode.Forbidden:
                    throw new NotImplementedException();
                //throw new ForbiddenException();
                case HttpStatusCode.BadRequest:

                    var erros = JsonSerializer.Deserialize<IDictionary<string, string[]>>(content);

                    var messages = new List<string>();

                    foreach (var item in erros)
                    {
                        messages.Add($"({item.Key}: {string.Join(",", item.Value)})");
                    }

                    throw new Exception(string.Join(",", messages));

                //throw new BrokenRuleException(string.Join(",", messages));
                default:
                    throw new Exception("Houve algum problema na comunicação com a API-Legacy");


                    //try
                    //{
                    //    erro = JsonConvert.DeserializeObject<ErroDto>(content);
                    //}
                    //catch (Exception)
                    //{
                    //    throw new Exception("Houve algum problema na comunicação com a API-Legacy");
                    //}

                    //throw new BrokenRuleException(erro.Message ?? erro.Descricao);
            }
        }
        public static int GetTotalCount(this HttpResponseHeaders headers)
        {
            int totalRegistros;
            try
            {
                totalRegistros = int.Parse(headers.GetValues("X-Total-Count")
                     .FirstOrDefault());
            }
            catch (Exception)
            {
                totalRegistros = 0;
            }

            return totalRegistros;

        }
        public static Guid GetIdByLocation(this HttpResponseHeaders headers)
        {
            if (headers.Location == null)
                throw new ArgumentException("Header Location não preenchido!");

            var locationArray = headers.Location.ToString().Split('/');

            return Guid.Parse(locationArray.Last());
        }
    }
}
