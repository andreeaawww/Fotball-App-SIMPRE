using SQLite;//sqlite-net-pcl
using System;
using System.Collections.Generic;
using System.Text;

namespace CursValutar
{
    [Table("Players")]
    class Player
    {
        [PrimaryKey, AutoIncrement, Column("PlayerId")]
        public int PlayerId { get; set; }

        [Column("PlayerName")]
        public string PlayerName { get; set; }

        [Column("Nationality")]
        public string Nationality { get; set; }

        [Column("Age")]
        public int Age { get; set; }

        [Column("TeamName")]
        public string TeamName { get; set; }

        public override string ToString()
        {
            return PlayerName + " is a player from "+ Nationality + ", has the age of " + Age.ToString() + " and plays for " + TeamName;
        }
    }
}
