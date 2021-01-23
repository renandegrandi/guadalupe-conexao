using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guadalupe.Conexao.App.Model
{
    [Table("mobile_info")]
    public class MobileInfo
    {
        #region Properties

        [PrimaryKey, Column("id")]
        public Guid Id { get; set; }

        [Column("fcm_token")]
        public string FCMToken { get; set; }

        #endregion

        public MobileInfo() { }

        public MobileInfo(string fcm) : this()
        {
            FCMToken = fcm;
        }
    }
}
