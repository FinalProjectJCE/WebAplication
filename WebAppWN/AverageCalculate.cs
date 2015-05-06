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

        public void setTime(DateTime clientTime , int numOfClients)
        {
            if (receiveClientTime == DateTime.MinValue)
            {              
                receiveClientTime = clientTime;
                System.Diagnostics.Debug.WriteLine("receiveClientTime is = " + receiveClientTime);
            }
            else if (receiveClientTime != DateTime.MinValue && releaseClientTime == DateTime.MinValue)
            {
                System.Diagnostics.Debug.WriteLine("Second Set");
                releaseClientTime = clientTime;
                System.Diagnostics.Debug.WriteLine("releaseClientTime = " + releaseClientTime);
                setAvarage(numOfClients);
            }
        }

        public void setAvarage(int numOfClients) 
        {
            TimeSpan currentQueueAverage = releaseClientTime.Subtract(receiveClientTime); // The Time Took For This Line
            int busId = numOfClients;
            System.Diagnostics.Debug.WriteLine("Business Id Is:  = " + numOfClients);

            int treatedClients = new WebAppWN.DbDAL().getTreatedClients(busId); // The Treated Clients
            System.Diagnostics.Debug.WriteLine("Treated Clients = " + treatedClients);

            TimeSpan averageFromDB = new WebAppWN.DbDAL().getAverageTime(busId); // The Average Time From DB
            System.Diagnostics.Debug.WriteLine("Average From DB IS: Hours = " + averageFromDB.Hours);
            System.Diagnostics.Debug.WriteLine("Average From DB IS: Minutes = " + averageFromDB.Minutes);
            System.Diagnostics.Debug.WriteLine("Average From DB IS: Seconds = " + averageFromDB.Seconds);

            TimeSpan totalAverage = TimeSpan.FromTicks(averageFromDB.Ticks * treatedClients); // The Average Time From DB * Treated Clients -> (If Treated Clients=0 So It Will Be The First Average)
            System.Diagnostics.Debug.WriteLine("Average From DB After Multiply IS: Hours = " + totalAverage.Hours);
            System.Diagnostics.Debug.WriteLine("Average From DB After Multiply: Minutes = " + totalAverage.Minutes);
            System.Diagnostics.Debug.WriteLine("Average From DB After Multiply: Seconds = " + totalAverage.Seconds);
            totalAverage = totalAverage.Add(currentQueueAverage);
            totalAverage = TimeSpan.FromTicks(totalAverage.Ticks / (treatedClients + 1));
            new WebAppWN.DbDAL().updateAverage(totalAverage); // Update The Average On DB
            System.Diagnostics.Debug.WriteLine("Division By Num Of Clients= " + totalAverage);
            new WebAppWN.DbDAL().IncreaseTreatedClients(busId); // Increase Treated Clients On DB
        }

        public void setClientTimes() 
        {
            this.receiveClientTime = DateTime.MinValue;
            this.releaseClientTime = DateTime.MinValue;
        }
    }
}