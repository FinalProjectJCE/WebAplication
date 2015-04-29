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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["business"] != null)
            {
                //SessionLabel.Text = Session["business"].ToString();
                DbDAL dbd = new DbDAL();
                //SessionLabel.Text = dbd.getName();
                //SessionLabel.Visible = true;
            }
            else
                Response.Redirect("LoginPage.aspx");
            Console.WriteLine(LoginPage.businessID);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Flag := " + flagForQueue);

            if (!(new WebAppWN.DbDAL().noOneInQueue()))
            { // If The Queue Is Not Empty.. Increase The Current Queue Number
                if (flagForQueue == 0)
                {
                    ac.setTime(DateTime.Now, dbd.getCurr(Convert.ToInt16(Session["business"])));
                    flagForQueue = 1;
                    System.Diagnostics.Debug.WriteLine("HERE : HereRER");
                }
                else
                {
                    ac.setTime(DateTime.Now, dbd.getCurr(Convert.ToInt16(Session["business"])));
                    System.Diagnostics.Debug.WriteLine("Num Of People Send To Set Time:  = " + dbd.getCurr(Convert.ToInt16(Session["business"])));
                    dbd.IncreaseCurr(Convert.ToInt16(Session["business"]));
                    if ((new WebAppWN.DbDAL().noOneInQueue()))
                    {
                        flagForQueue = 0;
                        ac.setClientTimes();

                    }


                }
            }
            else
            {
                flagForQueue = 0;
                ac.setClientTimes();
                System.Diagnostics.Debug.WriteLine("No Body In Line Whene Clicked ");

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            new DbDAL().resetCurr();
        }


    }
}