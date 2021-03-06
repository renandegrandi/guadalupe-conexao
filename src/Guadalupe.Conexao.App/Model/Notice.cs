﻿using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace Guadalupe.Conexao.App.Model
{
    [Table("notice")]
    public class Notice
    {
        [PrimaryKey, Column("id")]
        public Guid Id { get; set; }

        [Column("image")]
        public string Image { get; set; }

        [Column("message")]
        public string Message { get; set; }

        [Column("posted")]
        public DateTime Posted { get; set; }

        [ForeignKey(typeof(Person)), Column("id_posted_by")]
        public Guid IdPostedBy { get; set; }
        
        [ManyToOne]
        public Person PostedBy { get; set; }

        [Ignore]
        public string PathImage { 
            get 
            {
                return $"{Configuration.Assets}{Image}";
            }
        }
    }
}
