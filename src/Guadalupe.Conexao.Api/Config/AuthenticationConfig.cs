namespace Guadalupe.Conexao.Api.Config
{
    public class AuthenticationConfig
    {
        #region Constants
        public const string Key = "Authentication";
        #endregion

        #region Properties
        public JwtConfig Jwt { get; set; }
        #endregion
    }
}
