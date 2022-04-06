using System;

namespace Guadalupe.Conexao.Api.Infrastructure.Config
{
    public class SmtpConfig
    {
        #region Constants

        public const string Key = "Smtp";

        #endregion

        #region Properties

        public String PrimaryDomain { get; set; }
        public int PrimaryPort { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String FromEmail { get; set; }
        public String DisplayName { get; set; }


        #endregion
    }
}
