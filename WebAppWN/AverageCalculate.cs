using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppWN.ServerSide;

/*
 * This Class Calculates The Average Time For A Queue In The Business.
 */

namespace WebAppWN
{
    public class AverageCalculate
    {
        private DateTime  receiveClientTime; // The Time That The User Has Started To Get A Service.
        private DateTime releaseClientTime;  // The Time That The User Has Finished To Get A Service.
        private int bussinesID; // The Business Uniqe ID

        /*
         * The Constructor Thats Sets Fields And Starts The Calculations
         */
        public void setTime(DateTime receiveClientTime, DateTime releaseClientTime, int bussinesID)
        {
            this.receiveClientTime = receiveClientTime;
            this.releaseClientTime = releaseClientTime;
            this.bussinesID = bussinesID;
            setAvarage();
        }

        /*
         * The Method Sets The Average Time.
         * It Firsts Multiplies The Current Queue Service Time By The Number Of Treated Clients
         * If No Client Were Treated, Thats Means That This Is The First Average Time Calculation
         * And The Average Time For A Queue Will Be The Current Queue Service Time
         * If There Is Clients That Were Treated, The Average Time Calculation Will Be :
         * Average Time = ( (Last Average Time * Number Of Treated Clients) + Current Queue Service Time)/ Number Of Treated Clients + 1
         */
        public void setAvarage() 
        {
            TimeSpan currentQueueServiceTime = releaseClientTime.Subtract(receiveClientTime); // The  Service Time For This Queue
            int treatedClients = new DbBL().getTreatedClients(bussinesID); // The Number Of Clients That Has Been Served.
            TimeSpan averageFromDB = new DbBL().getAverageTime(bussinesID); // The Average Time From Datebase

            TimeSpan totalAverage = TimeSpan.FromTicks(averageFromDB.Ticks * treatedClients); // ( (The Average Time From DB) * (Treated Clients) ) , If No Client Were Treated, This Will Be 0.
            totalAverage = totalAverage.Add(currentQueueServiceTime); 
            totalAverage = TimeSpan.FromTicks(totalAverage.Ticks / (treatedClients + 1));
            new DbBL().updateAverage(totalAverage, bussinesID); // Update The Average On DB
            new DbBL().IncreaseTreatedClients(bussinesID); // Increase Treated Clients On DB
        }

    }
}