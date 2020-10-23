using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace Guadalupe.Conexao.App.Model
{
    [Table("notice")]
    public class Notice
    {
        [Ignore]
        public Guid Id { get; set; }
        [Column("image")]
        public string Image { get; set; }
        [Column("message")]
        public string Message { get; set; }
        [Column("posted")]
        public DateTime Posted { get; set; }
        [Column("idPostedBy")]
        public int IdPostedBy { get; set; }
        [ManyToOne("idPostedBy")]
        public Person PostedBy { get; set; }
    }
}
