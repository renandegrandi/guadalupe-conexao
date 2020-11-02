namespace Guadalupe.Conexao.Api.Config
{
    public class JwtConfig
    {
        #region Constants
        public const string Key = "Jwt";
        #endregion

        #region Properties
        public string SymmetricKey { get; set; }
        public string Authority { get; set; }
        public string Audience { get; set; }
        #endregion

    }
}
