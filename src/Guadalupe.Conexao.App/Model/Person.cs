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

        [Column("profile_image")]
        public string ProfileImage { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Notice> Notices { get; set; }

        #endregion

        #region Constructor

        public Person() { }

        public Person(Guid id, string email, string image, string name): this()
        {
            Id = id;
            ProfileImage = image;
            Name = name;
            Email = email;
        }

        #endregion

    }
}
