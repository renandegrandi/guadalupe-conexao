using System.Text.Json.Serialization;

namespace Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Models
{
    public class AuthenticationTokenDto
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        //[JsonPropertyName("user_info")]
        //public PersonDto UserInfo { get; set; }
    }
}
