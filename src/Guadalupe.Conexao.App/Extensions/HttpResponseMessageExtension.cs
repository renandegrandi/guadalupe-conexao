using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Repository;
using Guadalupe.Conexao.App.Repository.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.App.Extensions
{
    static class HttpResponseMessageExtension
    {
        public static async Task<T> GetResultAsync<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return JsonConvert.DeserializeObject<T>(content);
                case HttpStatusCode.NoContent:
                    return default;
                case HttpStatusCode.Created:
                    throw new NotImplementedException("Método Created not implemented!");
                case HttpStatusCode.Unauthorized:
                    var authenticationError = JsonConvert.DeserializeObject<AuthenticationErroDto>(content);
                    throw new DomainException(authenticationError.Descricao);
                case HttpStatusCode.BadRequest:

                    var erros = JsonConvert.DeserializeObject<IDictionary<string, string[]>>(content);

                    var messages = new List<string>();

                    foreach (var item in erros)
                    {
                        messages.Add($"({item.Key}: {string.Join(",", item.Value)})");
                    }

                    throw new DomainException(string.Join(",", messages));
                default:
                    throw new DomainException(ConexaoHttpClient.PrettyMessage);
            }
        }
    }
}
