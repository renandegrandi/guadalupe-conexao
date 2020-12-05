using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace Guadalupe.Conexao.App.Model
{
    [Table("user")]
    public class User
    {
        #region Propriedades

        [Column("conexao_token")]
        public string ConexaoToken { get; private set; }

        [Column("conexao_refresh_token")]
        public string ConexaoRefreshToken { get; private set; }

        [ForeignKey(typeof(Person), Name = "id_person")]
        public Guid IdPerson { get; set; }

        [OneToOne]
        public Person Person { get; private set; }

        #endregion

        public User()
        {

        }

        public User(Person person, string conexaoToken, string conexaoRefreshToken): this()
        {
            Person = person;
            ConexaoToken = conexaoToken;
            ConexaoRefreshToken = conexaoRefreshToken;
        }
    }
}
