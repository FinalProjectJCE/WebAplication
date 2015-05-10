using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppWN
{
    public class AverageCalculate
    {
        private DateTime  receiveClientTime;
        private DateTime releaseClientTime;
        private int bussinesID;

        public void setTime(DateTime receiveClientTime, DateTime releaseClientTime, int bussinesID)
        {
            this.receiveClientTime = receiveClientTime;
            this.releaseClientTime = releaseClientTime;
            this.bussinesID = bussinesID;
            setAvarage();
        }
        public void setAvarage() 
        {
            TimeSpan currentQueueAverage = releaseClientTime.Subtract(receiveClientTime); // The Time Took For This Line
            int treatedClients = new WebAppWN.DbDAL().getTreatedClients(bussinesID); // The Treated Clients
            System.Diagnostics.Debug.WriteLine("Treated Clients = " + treatedClients);
            TimeSpan averageFromDB = new WebAppWN.DbDAL().getAverageTime(bussinesID); // The Average Time From DB
            System.Diagnostics.Debug.WriteLine("Average From DB IS: Hours = " + averageFromDB.Hours);
            System.Diagnostics.Debug.WriteLine("Average From DB IS: Minutes = " + averageFromDB.Minutes);
            System.Diagnostics.Debug.WriteLine("Average From DB IS: Seconds = " + averageFromDB.Seconds);
            TimeSpan totalAverage = TimeSpan.FromTicks(averageFromDB.Ticks * treatedClients); // The Average Time From DB * Treated Clients -> (If Treated Clients=0 So It Will Be The First Average)
            System.Diagnostics.Debug.WriteLine("Average From DB After Multiply IS: Hours = " + totalAverage.Hours);
            System.Diagnostics.Debug.WriteLine("Average From DB After Multiply: Minutes = " + totalAverage.Minutes);
            System.Diagnostics.Debug.WriteLine("Average From DB After Multiply: Seconds = " + totalAverage.Seconds);
            totalAverage = totalAverage.Add(currentQueueAverage);
            totalAverage = TimeSpan.FromTicks(totalAverage.Ticks / (treatedClients + 1));
            new WebAppWN.DbDAL().updateAverage(totalAverage, bussinesID); // Update The Average On DB
            System.Diagnostics.Debug.WriteLine("Division By Num Of Clients= " + totalAverage);
            new WebAppWN.DbDAL().IncreaseTreatedClients(bussinesID); // Increase Treated Clients On DB
        }

    }
}