using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppWN
{
    public class DbDAL
    {
        private MySqlConnection conn = null;
        private MySqlConnectionStringBuilder sb = null;
        private MySqlCommand cmd = null;

        public DbDAL()
        {
            sb = new MySqlConnectionStringBuilder();
            sb.Server = "f37fa280-507d-4166-b70e-a427013f0c94.mysql.sequelizer.com";
            sb.UserID = "lewtprebbcrycgkb";
            sb.Password = "S5zS2ExvQqZQrUK8dwSJvpv5dSvED4RwmijLrG55TEesXBTrAR3QDXPCGDPijZZU";
            sb.Database = "dbf37fa280507d4166b70ea427013f0c94";
            try
            {
                conn = new MySqlConnection(sb.ToString());
            }
            catch(MySqlException e)
            {
                Console.WriteLine("Error: {0}", e.ToString());
            }
        }
        
        public void IncreaseCurr(int businessID)
        {
            conn.Open();
            string query =
                "UPDATE Queue SET CurrentQueue = CurrentQueue + 1 WHERE BusinessId = '" + businessID + "'";
            cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public int getCurr(int businessID)
        {
            conn.Open();
            string query =
                "SELECT CurrentQueue FROM Queue WHERE BusinessId = '" + businessID + "'"; 	
            
            cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            int curr = rdr.GetInt32(0);
            rdr.Close();
            conn.Close();
            return curr;
        }
        public void resetCurr()
        {
            conn.Open();
            string query =
                "UPDATE Queue SET CurrentQueue = 0 WHERE BusinessId = '" + LoginPage.businessID + "'"; 
            cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public string getName()
        {
            conn.Open();
            string query =
                "SELECT City FROM Queue WHERE BusinessId = '" + LoginPage.businessID + "'";
            cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            string curr = rdr[0].ToString();
            rdr.Close();
            conn.Close();
            return curr;
        }

// Checks If The There Is No One Waiting In Line, If So, Return True 
        public bool noOneInQueue()
        {
            bool toReturn;
            conn.Open();
            string query =
                "SELECT CurrentQueue , TotalQueue FROM Queue WHERE BusinessId = '" + LoginPage.businessID + "'";
            cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            int currentQueue=rdr.GetInt32(0);
            int totalQueue = rdr.GetInt32(1);
        
            if (currentQueue > totalQueue || totalQueue == 0)
                toReturn = true;
            else
                toReturn = false;
            rdr.Close();
            conn.Close();
            return toReturn ;
        }

    }
}