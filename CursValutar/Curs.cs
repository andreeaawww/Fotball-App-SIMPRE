using SQLite;//sqlite-net-pcl
using System;
using System.Collections.Generic;
using System.Text;

namespace CursValutar
{
    [Table("curs_valutar")]
    class Curs
    {

        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
       [Column("valuta")]
        public string Valuta { get; set; }
        [Column("valoare")]
        public double Valoare { get; set; }
        [Column("multiplicator")]
        public int Multiplicator { get; set; }
        [Column("data")]
        public string Data { get; set; }
        [Ignore]
        public string ResursaDrapel { get { return Valuta.ToLower().Substring(0, 2) + ".png"; }  }

        public Curs() {
            Multiplicator = 1;
        }

     

    }
}
