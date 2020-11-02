using Guadalupe.Conexao.Api.Core;
using Guadalupe.Conexao.Api.Domain;
using Guadalupe.Conexao.Api.Validation;
using System.Text.Json.Serialization;

namespace Guadalupe.Conexao.Api.Models.V1
{
    public class AuthenticationDto: IDto
    {
        #region Properties

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [GrantTypeValidation]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("grant_type")]
        public GrantTypes GrantType { get; set; } = GrantTypes.password;

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        #endregion
    }
}
