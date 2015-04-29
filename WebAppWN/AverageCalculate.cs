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
        private DateTime delete;
        private TimeSpan  avarage;
        int c = 0;
        int lastNumOfClients = 0;

        public void setTime(DateTime clientTime , int numOfClients)
        {
            c++; 

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
            else if (receiveClientTime != DateTime.MinValue && releaseClientTime != DateTime.MinValue)
            {
                System.Diagnostics.Debug.WriteLine("Final Set");
                receiveClientTime = releaseClientTime;
                releaseClientTime = clientTime;
                System.Diagnostics.Debug.WriteLine("receiveClientTime is = " + receiveClientTime);
                System.Diagnostics.Debug.WriteLine("releaseClientTime = " + releaseClientTime);

                setAvarage(numOfClients);
            }
        }

        public void setAvarage(int numOfClients) 
        {
        //    System.Diagnostics.Debug.WriteLine("Set Avarage");
          //  avarage = releaseClientTime.Ticks - receiveClientTime.Ticks;
            //TimeSpan elapsedSpan = new TimeSpan(avarage);

            TimeSpan span = releaseClientTime.Subtract(receiveClientTime);
            
            System.Diagnostics.Debug.WriteLine("minutes are : = " + span.Minutes);
            System.Diagnostics.Debug.WriteLine("Seconds are are : = " + span.Seconds);
            System.Diagnostics.Debug.WriteLine("Total Seconds are are : = " + span.Seconds);
            if (numOfClients == 1)
            {
                avarage = span;
                System.Diagnostics.Debug.WriteLine("Avarage Is = " + avarage + "Number Of Clients : "+numOfClients);

                lastNumOfClients = 1;
            }
            else
            {
                
                
               // avarage = new TimeSpan(0,7,30);
                System.Diagnostics.Debug.WriteLine("Last Avarage Is = " + avarage);
                System.Diagnostics.Debug.WriteLine("Last Num Of Clients Is = " + lastNumOfClients);

                avarage = TimeSpan.FromTicks(avarage.Ticks*lastNumOfClients);
                System.Diagnostics.Debug.WriteLine("Last Avarage After Multiply By The Last Num Of Clients = " + avarage);
                avarage= avarage.Add(span);
                System.Diagnostics.Debug.WriteLine("Last Avarage After Add New Avarage Is = " + avarage);

                avarage = TimeSpan.FromTicks(avarage.Ticks / numOfClients);
                System.Diagnostics.Debug.WriteLine("Num Of Clients Is = " + numOfClients);

                System.Diagnostics.Debug.WriteLine("Division By Num Of Clients= " + avarage);
                System.Diagnostics.Debug.WriteLine("#########################################################3");

                lastNumOfClients = numOfClients;
   
                
            }
        }

        public void setClientTimes() 
        {
            this.receiveClientTime = DateTime.MinValue;
            this.releaseClientTime = DateTime.MinValue;
        }



    }
}