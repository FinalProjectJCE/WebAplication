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


namespace WebAppWN
{
    public partial class MainPage : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
            {
                Response.Redirect("LoginPage.aspx", false);
            }
            else
            {
                recountConnectedClerks();

                SessionLabel.Text = new DbDAL().getName((int)Session["business"]);

                if (catchedQueueNum.Text.Equals("0")) // If Not Serving Any Client Always Show The Current Queue Number
                {
                    setLabels();
                }
                else
                {
                    QUEUE.Text = Session["LastLine"].ToString(); // If Serving A Client,, Show His Number
                    this.ImageButton1.ImageUrl = "img/StopImgForButton.png";
                    StartAndStopLabel.Text = "סיים טיפול בלקוח";
                }
            }
        }
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            if (!(new DbDAL().noOneInQueue((int)Session["business"])))
            // There Are Clients Waiting And The Clerck Is Not Giving Any Service
            {
                if (catchedQueueNum.Text.Equals("0")) // If Not Serving Clients
                {
                    catchedQueueNum.Text = "1";
                    this.ImageButton1.ImageUrl = "img/StopImgForButton.png";
                    Session["LastLine"] = new DbDAL().getCurr((int)Session["business"]);
                    new DbDAL().IncreaseCurr((int)Session["business"]);
                    Session["receiveClientTime"] = DateTime.Now;
                    hasOneInQueue.Text = "";
                    StartAndStopLabel.Text = "סיים טיפול בלקוח";
                }
                else
                {
                    tempF();
                }
            }
            else  // If No One Is Waiting In Line But There Is One Getting Service
            {
                if (catchedQueueNum.Text.Equals("1")) // If Serving Clients
                {
                    tempF();
                }
            }
        }
        protected void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (catchedQueueNum.Text.Equals("0"))
            {
                int getCurrLine = new DbDAL().getCurr((int)Session["business"]);
                QUEUE.Text = getCurrLine.ToString();
            }


        }
       
        public void tempF()
        {
            AverageCalculate ac = new AverageCalculate();
            Session["releaseClientTime"] = DateTime.Now;
            ac.setTime((DateTime)Session["receiveClientTime"], (DateTime)Session["releaseClientTime"], (int)Session["business"]);
            this.ImageButton1.ImageUrl = "img/ArrowButtonImg.png";
            int getCurrLine = new DbDAL().getCurr((int)Session["business"]);
            QUEUE.Text = getCurrLine.ToString();
            setLabels();
            catchedQueueNum.Text="0";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            new DbDAL().resetCurr((int)Session["business"]);
        }

        public void setLabels()
        {
            int getCurrLine = new DbDAL().getCurr((int)Session["business"]);
            QUEUE.Text = getCurrLine.ToString();
            SessionLabel.Text = new DbDAL().getName((int)Session["business"]);
            if (new DbDAL().noOneInQueue((int)Session["business"])) // If No One Is Waiting
            {
                hasOneInQueue.Text = "אין לקוחות ממתינים";
                StartAndStopLabel.Text = "";
            }
            else // If There Is One Waiting
            {
                hasOneInQueue.Text = "ישנם לקוחות ממתינים";
                StartAndStopLabel.Text = "התחל טיפול בלקוח";

            }
        }

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
            new UsersDAL().setNumberOfClerk((int)Session["business"],count);

        }

    }


}
