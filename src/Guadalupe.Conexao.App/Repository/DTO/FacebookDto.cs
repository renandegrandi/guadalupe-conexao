using Newtonsoft.Json;

namespace Guadalupe.Conexao.App.Repository.DTO
{
    public class FacebookDto
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("id")]
        public string UserId { get; set; }
    }
}
