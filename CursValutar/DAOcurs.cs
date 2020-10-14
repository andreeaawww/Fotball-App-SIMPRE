using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CursValutar
{
     

    class DAOcurs
    {
        SQLiteConnection sqliteConnection;

        public DAOcurs()
        {

            string connectionString = 
                Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), 
                     "curs.db");

            sqliteConnection = new SQLiteConnection(connectionString);
            sqliteConnection.CreateTable<Curs>();


        }

        public int AdaugaCurs(Curs curs)
        {
            return sqliteConnection.Insert(curs);
            
        }

        public List<Curs> ObtineCursDinData(string data)
        {
            return sqliteConnection.Query<Curs>("SELECT * FROM curs_valutar WHERE data = ?", 
                new string[] {data});
        }
    }
}
