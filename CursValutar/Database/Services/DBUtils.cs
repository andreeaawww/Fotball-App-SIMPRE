using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CursValutar
{
    class DBUtils
    {
        static SQLiteConnection sqliteConnection;

        public void InitializeDb()
        {
            string connectionString =
                Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                     "football.db");

            sqliteConnection = new SQLiteConnection(connectionString);
            sqliteConnection.CreateTable<Player>();
            sqliteConnection.CreateTable<Team>();
        }

        public int AddPlayer(Player player)
        {
            return sqliteConnection.Insert(player);
        }

        public List<Player> GetSavedPlayers()
        {
            return sqliteConnection.Query<Player>("SELECT * FROM Players");
        }
    }
}
