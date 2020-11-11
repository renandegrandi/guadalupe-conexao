using Newtonsoft.Json;
using System.ComponentModel;

namespace Guadalupe.Conexao.App.Repository.DTO
{
    public class AuthenticationDto
    {
        #region Properties

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonConverter(typeof(StringConverter))]
        [JsonProperty("grant_type")]
        public GrantTypes GrantType { get; set; } = GrantTypes.password;

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        #endregion

        public enum GrantTypes
        {
            password,
            refresh_token
        }
    }
}
