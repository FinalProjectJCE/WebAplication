using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
 * This Class Is The Data Access Layer For The Users.
 */

namespace WebAppWN.ServerSide
{
    public class UsersDAL
    {
        private MySqlConnection conn = null;
        private MySqlConnectionStringBuilder sb = null;
        private MySqlCommand cmd = null;

        /*
         * The Constructor Thats Sets The Connction.
         */
        public UsersDAL()
        {
            
            sb = new MySqlConnectionStringBuilder();
            sb.Password = DatabaseConstants.PASSWORD;
            sb.Database = DatabaseConstants.DATABASE;
            sb.Server = DatabaseConstants.SERVER;
            sb.UserID = DatabaseConstants.USER_ID;
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
         * This Method Returns The Business Uniqe Id If Exist, By The Users "UserName" And "Password"
         */
        public int GetBusinessID(string userName, string userPassword) 
        {
            int businessId=0;
            conn.Open();
            string query =
                "select businessID from Users where userName = '" + userName + "' and userPassword = '" + userPassword + "'";
            cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            
            if(rdr.Read())
            {
                if (rdr[0] != DBNull.Value)
                    businessId = rdr.GetInt32(0);
                else
                    businessId = 0;              
            }
            rdr.Close();
            conn.Close();  
            return businessId;
        }

        /*
         * This Method Sets The Number Of Clerks In The Business.
         */
        public void setNumberOfClerk(int businessID,int numOfClerks) 
        {
            conn.Open();
            string query =
                "UPDATE Queue SET NumberOfClerks  = '" + numOfClerks + "' WHERE BusinessId = '" + businessID + "'";
            cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}