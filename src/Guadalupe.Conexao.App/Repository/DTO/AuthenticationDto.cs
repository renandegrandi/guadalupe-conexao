using Newtonsoft.Json;

namespace Guadalupe.Conexao.App.Repository.DTO
{
    public class AuthenticationDto
    {
        #region Constructor

        public AuthenticationDto()
        {
            GrantType = GrantTypes.password;
        }


        #endregion

        #region Properties

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("grant_type")]
        public GrantTypes GrantType { get; set; } 

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
