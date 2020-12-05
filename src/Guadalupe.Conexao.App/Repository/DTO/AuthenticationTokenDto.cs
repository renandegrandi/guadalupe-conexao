using Newtonsoft.Json;

namespace Guadalupe.Conexao.App.Repository.DTO
{
    public class AuthenticationTokenDto
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("user_info")]
        public PersonDto UserInfo { get; set; }
    }
}
