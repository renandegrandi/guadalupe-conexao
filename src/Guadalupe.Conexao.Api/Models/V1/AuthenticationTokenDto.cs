using Guadalupe.Conexao.Api.Core;
using System.Text.Json.Serialization;

namespace Guadalupe.Conexao.Api.Models.V1
{
    public class AuthenticationTokenDto : IDto
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
