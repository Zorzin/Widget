using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widget
{
    class DBConnect
    {
        private SQLiteConnection sqlite;

        public DBConnect()
        {
            sqlite = new SQLiteConnection("Data Source=WidgetDB.db;New=False;Compress=True;");

        }

        public DataTable selectQuery(string query)
        {
            SQLiteDataAdapter ad;
            var dt = new DataTable();
            SQLiteCommand cmd;
            sqlite.Open(); //Initiate connection to the db
            cmd = sqlite.CreateCommand();
            cmd.CommandText = query; //set the passed query
            ad = new SQLiteDataAdapter(cmd);
            ad.Fill(dt); //fill the datasource
            sqlite.Close();
            return dt;
        }
    }
}
