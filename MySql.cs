using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Assets
{
    class MySql
    {
        public static string SqlConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Hazem\Documents\YuGiOh\Assets\DB\CDB.mdf;Integrated Security=True;Connect Timeout=30";
        public static List<Monsters> newMonsters;
        public static void ReadData()
        {
            newMonsters = new List<Monsters>();
            SqlConnection con = new SqlConnection(SqlConnectionString);
            SqlCommand cmd = new SqlCommand("Select * From Monsters",con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            int i = 0;
            while(rdr.Read())
                {
                
                Monsters m = new Monsters();
                m.ID = (string)rdr["Id"];
                m.CardName = (string)rdr["Name"];
                m.CardDesc = (string)rdr["Description"];
                m.Rank= Convert.ToInt32((string)rdr["Level"]);
                m.Attribute = (string)rdr["Attribute"];
                m.Race = (string)rdr["Race"];
                m.AttackPoints = Convert.ToInt32((string)rdr["Attack"]);
                m.DefencePoints = Convert.ToInt32((string)rdr["Defense"]);
                newMonsters.Add(m);

                }
            rdr.Close();
            con.Close();
        }
    }
}
