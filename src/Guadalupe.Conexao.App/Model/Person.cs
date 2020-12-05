using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Guadalupe.Conexao.App.Model
{
    [Table("person")]
    public class Person
    {
        #region Properties

        [PrimaryKey, Column("id")]
        public Guid Id { get; set; }

        [Column("image")]
        public string Image { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Notice> Notices { get; set; }

        #endregion

        #region Constructor

        public Person() { }

        public Person(Guid id, string image, string name): this()
        {
            Id = id;
            Image = image;
            Name = name;
        }

        #endregion

    }
}
