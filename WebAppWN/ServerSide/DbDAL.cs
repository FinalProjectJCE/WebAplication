using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebAppWN.ServerSide;

namespace WebAppWN
{
    /*
     * This Class Is The Data Access Layer For The Queue.
     */
    public class DbDAL:MainPage
    {
        private MySqlConnection conn = null;
        private MySqlConnectionStringBuilder sb = null;
        private MySqlCommand cmd = null;
        private DatabaseConstants dbc;

        /*
         * The Constructor Thats Sets The Connction.
         */
        public DbDAL()
        {
            sb = new MySqlConnectionStringBuilder();
            sb.Password = DatabaseConstants.PASSWORD;
            sb.Database=DatabaseConstants.DATABASE;
            sb.Server=DatabaseConstants.SERVER;
            sb.UserID=DatabaseConstants.USER_ID;
            try
            {
                conn = new MySqlConnection(sb.ToString());
            }
            catch(MySqlException e)
            {
                Console.WriteLine("Error: {0}", e.ToString());
            }
        }

        /*
         * This Method Increase The Current Queue In The Business By 1;
         */
        public void IncreaseCurr(int businessID)
        {
            while (conn.State != ConnectionState.Closed) ;
            conn.Open();
            string query =
                "UPDATE Queue SET CurrentQueue = CurrentQueue + 1 WHERE BusinessId = '" + businessID + "'";
            cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        /*
         * This Method Returns The Current Queue In The Business.
         */
        public int getCurr(int businessID)
        {
            while (conn.State != ConnectionState.Closed) ;

            conn.Open();
            string query =
                "SELECT CurrentQueue FROM Queue WHERE BusinessId = '" + businessID + "'";
            int curr=0;
            cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if(rdr.Read())
                curr = rdr.GetInt32(0);
            rdr.Close();
            conn.Close();
            return curr;
        }

        /*
         * This Method Resets The Queue Data In The Business.
         */
        public void resetCurr(int businessID)
        {
            while (conn.State != ConnectionState.Closed) ;

            conn.Open();
            string query =
                "UPDATE Queue SET CurrentQueue = 1 , TotalQueue = 0 , AverageTime = '00:00:00' , TreatedClients = 0 WHERE BusinessId = '" + businessID + "'"; 
            cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        /*
         * This Method Returns The Name Of The Business.
         */
        public string getName(int businessID)
        {
            while (conn.State != ConnectionState.Closed) ;

            string toReturn;
            conn.Open();
            string query =
                "SELECT BusinessName,City,Branch FROM Queue WHERE BusinessId = '" + businessID + "'";
            cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            string businessName = rdr[0].ToString();
            string city = rdr[1].ToString();
            string branch = rdr[2].ToString();
            toReturn = businessName + "  סניף " + branch + ", " + city;
            rdr.Close();
            conn.Close();
            return toReturn;
        }


        /*
         * This Method Check If There Are Clients Waiting For Service
         */
        public bool noOneInQueue(int businessID)
        {
            while (conn.State != ConnectionState.Closed) ;

            bool toReturn;
            conn.Open();
            string query =
                "SELECT CurrentQueue , TotalQueue FROM Queue WHERE BusinessId = '" + businessID + "'";
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

        /*
         * This Method Returns The Number Clients That Has Been Treated.
         */
        public int getTreatedClients(int businessID)
        {
            while (conn.State != ConnectionState.Closed) ;

            conn.Open();
            string query =
                "SELECT TreatedClients FROM Queue WHERE BusinessId = '" + businessID + "'";

            cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            int treatedClients = rdr.GetInt32(0);
            rdr.Close();
            conn.Close();
            return treatedClients;
        }

        /*
         * This Method Increases The Treated Clients Of The Business.
         */
        public void IncreaseTreatedClients(int businessID)
        {
            while (conn.State != ConnectionState.Closed) ;

            conn.Open();
            string query =
                "UPDATE Queue SET TreatedClients = TreatedClients + 1 WHERE BusinessId = '" + businessID + "'";
            cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        /*
         * This Method Returns The Average Time Of A Queue In The Business.
         */
        public TimeSpan getAverageTime(int businessID)
        {
            while (conn.State != ConnectionState.Closed) ;

            conn.Open();
            string query =
                "SELECT AverageTime FROM Queue WHERE BusinessId = '" + businessID + "'";
            cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            TimeSpan averageTime = rdr.GetTimeSpan(0);
            rdr.Close();
            conn.Close();
            return averageTime;
        }

        /*
         * This Method Updates The Average Time Of A Queue In The Business.
         */
        public void updateAverage(TimeSpan currentAverage, int businessID)
        {
            while (conn.State != ConnectionState.Closed) ;

            conn.Open();
            string query =
                "UPDATE Queue SET AverageTime = '" + currentAverage + "' WHERE BusinessId = '" + businessID + "'"; 
            cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        /*
         * This Method Returns The Number Of All The Clients That Had Take A Line
         */
        public int getTotalQueue(int businessID)
        {
            while (conn.State != ConnectionState.Closed) ;
            bool toReturn;
            conn.Open();
            string query =
                "SELECT TotalQueue FROM Queue WHERE BusinessId = '" + businessID + "'";
            cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            int totalQueue = rdr.GetInt32(0);

            rdr.Close();
            conn.Close();
            return totalQueue;
        }
    }
}