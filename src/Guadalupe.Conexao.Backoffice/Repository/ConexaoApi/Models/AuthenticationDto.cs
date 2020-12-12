using System.Text.Json.Serialization;

namespace Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Models
{
    public class AuthenticationDto
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("grant_type")]
        public GrantTypes GrantType { get; set; } = GrantTypes.password;

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
