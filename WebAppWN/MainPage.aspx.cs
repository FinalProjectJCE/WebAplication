using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebAppWN
{
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["business"] != null)
            {
                SessionLabel.Text= new DbDAL().getName((int)Session["business"]);

                if (!(bool)Session["servingClient"]) // If Not Serving Any Client Always Show The Current Queue Number
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
            else
                Response.Redirect("LoginPage.aspx",false);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("On Button Clicked");

            if (!(new DbDAL().noOneInQueue((int)Session["business"])))
            // There Are Clients Waiting And The Clerck Is Not Giving Any Service
            {
                if (!(bool)Session["servingClient"]) // If Not Serving Clients
                {
                    Session["servingClient"] = true;
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
                if ((bool)Session["servingClient"]) // If Serving Clients
                {
                    tempF();
                }                
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
            Session["servingClient"] = false;     
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

    }
}