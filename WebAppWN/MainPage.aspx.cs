using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppWN.ServerSide;

/*
 * This Is Main Page Class
 */

namespace WebAppWN
{
    public partial class MainPage : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session.Count == 0) // If One Is Trying To Accese This Page Without Login, Redirect Him To Login
            {
                Response.Redirect("LoginPage.aspx", false);
            }
            else // If The User Has Loged In
            {
                //recountConnectedClerks(); // This Will Count The Connected Clerks
                BusinessNameLabel.Text = new DbBL().getName((int)Session["business"]);

                if (ServingClients.Text.Equals(StringConstant.NOT)) // If Not Serving Any Client Always Show The Current Queue Number
                {
                    setLabels();
                }
                else // If The Clerk Is Servicing A Client
                {
                    QueueNumberLabel.Text = Session["LastLine"].ToString(); // Show The Clients Number.
                    this.ImageButton1.ImageUrl = StringConstant.STOP_IMAGE_BUTTON_URL; // Set The Stop Image For The Button
                    StartAndStopLabel.Text = StringConstant.END_SERVING; // Show The End Service Label
                }

            }
        }

        // This Method Is Invoked When The Clerk Is Clicking The Button
        protected void UpdateButton_Click(object sender, EventArgs e)
        { 
            if (!(new DbBL().noOneInQueue((int)Session["business"])))
            // There Are Clients Waiting
            {
                if (ServingClients.Text.Equals(StringConstant.NOT)) // The Clerck Is Not Serving Clients
                {
                    ServingClients.Text = StringConstant.YES;
                    this.ImageButton1.ImageUrl = StringConstant.STOP_IMAGE_BUTTON_URL;
                    Session["LastLine"] = new DbBL().getCurr((int)Session["business"]); // Save The Last Queue Number And Only Than, Increase It
                    new DbBL().IncreaseCurr((int)Session["business"]);
                    Session["receiveClientTime"] = DateTime.Now; // Save The Current Time (Receive Client Time)  
                    hasOneInQueue.Text = "";
                    StartAndStopLabel.Text = StringConstant.END_SERVING;
                }
                else
                {
                    setAverageAndButton();  // Finish Serving, Start Average Time Calculation And Set The Back The Labels
                }
            }
            else  // If No One Is Waiting In Queue But Maybe One Is One Getting Service
            {
                if (ServingClients.Text.Equals(StringConstant.YES)) // If Serving Clients
                {
                    setAverageAndButton(); // Finish Serving, Start Average Time Calculation And Set The Back The Labels
                }
            }
        }

        // This Method, Change The Current Queue Number If Changed, Only If The Clerk Is Not Giving Any Service
        protected void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (ServingClients.Text.Equals(StringConstant.NOT))
            {
                int getCurrLine = new DbBL().getCurr((int)Session["business"]);
                QueueNumberLabel.Text = getCurrLine.ToString();
            }
            Debug.WriteLine("On Update");
        }

        protected void TTT(object sender, EventArgs e)
        {
            Debug.WriteLine("On TTT");
        }


        /* This Method, Is Called When The Clerk Is Finished To Served The Client
         * It Will Save The Realse Time Of The Client And Send It Along With Te Receive Client Time To The Average Calculation
         * The Method Will Also Set The Back The Correct Labels
         */
        public void setAverageAndButton()
        {
            AverageCalculate ac = new AverageCalculate();
            Session["releaseClientTime"] = DateTime.Now;
            ac.setTime((DateTime)Session["receiveClientTime"], (DateTime)Session["releaseClientTime"], (int)Session["business"]);
            this.ImageButton1.ImageUrl = StringConstant.ARROW_IMAGE_BUTTON_URL;
            int getCurrLine = new DbBL().getCurr((int)Session["business"]);
            QueueNumberLabel.Text = getCurrLine.ToString();
            setLabels();
            ServingClients.Text = StringConstant.NOT;
        }

        /* This Method First Activets A Script Thats Display A Message To The User That The Queue Will Be Reset
         * If The User Agrees Than It Will Restars The Queue`s Field In The Database.
         * It Will Reset The Current Queue, The Total Clients And The Treated Clients
         */
        protected void Restart_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Successfully added');</script>");
            new DbBL().resetCurr((int)Session["business"]);
        }
        /*
         * The Method Sets The Labels Of The Page 
         */
        public void setLabels()
        {
            int getCurrLine = new DbBL().getCurr((int)Session["business"]);
            QueueNumberLabel.Text = getCurrLine.ToString();
            BusinessNameLabel.Text = new DbBL().getName((int)Session["business"]);
            if (new DbBL().noOneInQueue((int)Session["business"])) // If No One Is Waiting
            {
                hasOneInQueue.Text = StringConstant.NO_CLIENTS_ARE_WAITING;
                StartAndStopLabel.Text = "";
            }
            else // If There Is One Waiting
            {
                hasOneInQueue.Text = StringConstant.CLIENTS_ARE_WAITING;
                StartAndStopLabel.Text = StringConstant.START_SERVING;

            }
        }

        /*
         * The Method Counts The Number Of Connected Clerk To The Business Page 
         * It Counts The Current Sessions 
         * And Then It Sets The Number Of Clerks To The Database
         */
        private void recountConnectedClerks()
        {
            List<int> activeSessions = new List<int>();
            object obj = typeof(HttpRuntime).GetProperty("CacheInternal", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null, null);
            object[] obj2 = (object[])obj.GetType().GetField("_caches", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj);
            for (int i = 0; i < obj2.Length; i++)
            {
                Hashtable c2 = (Hashtable)obj2[i].GetType().GetField("_entries", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj2[i]);
                foreach (DictionaryEntry entry in c2)
                {
                    object o1 = entry.Value.GetType().GetProperty("Value", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(entry.Value, null);
                    if (o1.GetType().ToString() == "System.Web.SessionState.InProcSessionState")
                    {
                        SessionStateItemCollection session = (SessionStateItemCollection)o1.GetType().GetField("_sessionItems", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(o1);
                        if (session != null)
                        {
                            if (session["business"] != null)
                            {
                                activeSessions.Add((int)session["business"]);
                            }
                        }
                    }
                }
            }
            int count = 0;
            foreach (int i in activeSessions)
                if (i == (int)Session["business"])
                    count++;
            new UsersBL().setNumberOfClerk((int)Session["business"],count);
        }
    }


}
