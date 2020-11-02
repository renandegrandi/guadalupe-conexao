using Guadalupe.Conexao.Api.Core;
using Guadalupe.Conexao.Api.Domain;
using System.Text.Json.Serialization;

namespace Guadalupe.Conexao.Api.Models.V1
{
    public class AuthenticationErroDto: IDto
    {
        [JsonPropertyName("error")]
        public OAuthError Erro { get; set; }

        [JsonPropertyName("error_description")]
        public string Descricao { get; set; }
    }
}
