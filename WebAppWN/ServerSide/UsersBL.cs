using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppWN.ServerSide;

/*
 * This Class Is The Business Logic Layer For The Users Data.
 */

namespace WebAppWN.ServerSide
{
    public class UsersBL
    {
        UsersDAL usersDal = new UsersDAL();

        public int GetBusinessID(string userName, string userPassword) // Return User Full Name If Exists
        {

            return usersDal.GetBusinessID(userName,userPassword);
        }

        public void setNumberOfClerk(int businessID, int numOfClerks)
        {
            usersDal.setNumberOfClerk(businessID, numOfClerks);
        }


    }
}