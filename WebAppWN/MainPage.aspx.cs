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
        static int flagForQueue = 0;
        static AverageCalculate ac = new AverageCalculate();
        static DbDAL dbd = new DbDAL();
        static bool start = true;
        static int lastLine;
        static bool servingClient = false;
       


        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("On Page Load");
            if (Session["business"] != null)
            {
                if (!servingClient) // If Not Serving Any Client Always Show The Current Queue Number
                {
                    System.Diagnostics.Debug.WriteLine("On Session");
                    setLabels();               
                }
                else
                {
                    QUEUE.Text = lastLine.ToString(); // If Serving A Client,, Show His Number
                    this.ImageButton1.ImageUrl = "img/StopImgForButton.png";
                    StartAndStopLabel.Text = "סיים טיפול בלקוח";
                }
            }
            else
                Response.Redirect("LoginPage.aspx");

            System.Diagnostics.Debug.WriteLine("After Else");
            Console.WriteLine(LoginPage.businessID);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("On Button Clicked");

            if (!(new WebAppWN.DbDAL().noOneInQueue()))
            // There Are Clients Waiting And The Clerck Is Not Giving Any Service
            {
                if(!servingClient){
                servingClient = true;
                this.ImageButton1.ImageUrl = "img/StopImgForButton.png";
                lastLine=dbd.getCurr(Convert.ToInt16(Session["business"]));
                dbd.IncreaseCurr(Convert.ToInt16(Session["business"]));
                ac.setTime(DateTime.Now, Convert.ToInt16(Session["business"]));
                hasOneInQueue.Text = "";
                StartAndStopLabel.Text = "סיים טיפול בלקוח";
                }
                else
                {
                ac.setTime(DateTime.Now, Convert.ToInt16(Session["business"]));             
                this.ImageButton1.ImageUrl = "img/ArrowButtonImg.png";
                int i = dbd.getCurr(Convert.ToInt16(Session["business"]));
                QUEUE.Text = i.ToString();
                ac.setClientTimes();
                setLabels();
                start = true;
                servingClient = false;
                }
            }   
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            new DbDAL().resetCurr();
        }

        public void setLabels() 
        {
            System.Diagnostics.Debug.WriteLine("On Session");
            DbDAL dbd = new DbDAL();
            int i = dbd.getCurr(Convert.ToInt16(Session["business"]));
            QUEUE.Text = i.ToString();
            SessionLabel.Text = dbd.getName();
            if (dbd.noOneInQueue()) // If No One Is Waiting
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