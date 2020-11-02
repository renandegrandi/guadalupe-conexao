using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Guadalupe.Conexao.App.Model
{
    [Table("person")]
    public class Person
    {
        [PrimaryKey, Column("id")]
        public Guid Id { get; set; }

        [Column("image")]
        public string Image { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Notice> Notices { get; set; }
    }
}
