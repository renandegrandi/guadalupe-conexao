using Newtonsoft.Json;

namespace Guadalupe.Conexao.App.Repository.DTO
{
    public class AuthenticationErroDto
    {
        [JsonProperty("error_description")]
        public string Descricao { get; set; }
    }
}
