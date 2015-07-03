using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppWN.ServerSide;

namespace WebAppWN.ServerSide
{
    /*
     * This Class Is The Business Logic Layer For The Queue.
     */
    public class DbBL
    {
        DbDAL dbDal = new DbDAL();


        public void IncreaseCurr(int businessID)
        {
            dbDal.IncreaseCurr(businessID);
        }

        public int getCurr(int businessID)
        {
            return dbDal.getCurr(businessID);
        }

        public void resetCurr(int businessID)
        {
            dbDal.resetCurr(businessID);
        }

        public string getName(int businessID)
        {
            return dbDal.getName(businessID);
        }

        public bool noOneInQueue(int businessID)
        {
            return dbDal.noOneInQueue(businessID);
        }

        public int getTreatedClients(int businessID)
        {
            return dbDal.getTreatedClients(businessID);
        }

        public void IncreaseTreatedClients(int businessID)
        {
            dbDal.IncreaseTreatedClients(businessID);
        }

        public TimeSpan getAverageTime(int businessID)
        {
            return dbDal.getAverageTime(businessID);
        }

        public void updateAverage(TimeSpan currentAverage, int businessID)
        {
            dbDal.updateAverage(currentAverage, businessID);
        }

        public int getTotalQueue(int businessID)
        {
            return dbDal.getTotalQueue(businessID);
        }


    }
}