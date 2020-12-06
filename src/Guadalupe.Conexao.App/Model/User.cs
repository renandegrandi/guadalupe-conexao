using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace Guadalupe.Conexao.App.Model
{
    [Table("user")]
    public class User
    {
        #region Propriedades

        [PrimaryKey, Column("id")]
        public Guid Id { get; set; }

        [Column("conexao_token")]
        public string ConexaoToken { get; set; }

        [Column("conexao_refresh_token")]
        public string ConexaoRefreshToken { get; set; }

        [Column("notice_last_update")]
        public DateTime? NoticeLastUpdate  { get; set; }

        [ForeignKey(typeof(Person)), Column("id_person")]
        public Guid IdPerson { get; set; }

        [OneToOne]
        public Person Person { get; set; }

        #endregion

        public User()
        {

        }

        public User(Person person, string conexaoToken, string conexaoRefreshToken): this()
        {
            Id = Guid.NewGuid();
            IdPerson = person.Id;
            Person = person;
            ConexaoToken = conexaoToken;
            ConexaoRefreshToken = conexaoRefreshToken;
        }

        public void RefreshToken(string accessToken, string refreshToken) 
        {
            ConexaoToken = accessToken;
            ConexaoRefreshToken = refreshToken;
        }

        public void SetNoticeUpdated() 
        {
            NoticeLastUpdate = DateTime.Now;
        }
    }
}
