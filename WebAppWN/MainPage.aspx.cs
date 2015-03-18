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
        static int LineNum=0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["business"] != null)
            {
                SessionLabel.Text = Session["business"].ToString();
                SessionLabel.Visible = true;
            }
            else
                Response.Redirect("LoginPage.aspx");
            Console.WriteLine(LoginPage.businessID);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            new DbDAL().IncreaseCurr(Convert.ToInt16( Session["business"]));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            new DbDAL().resetCurr();
        }
        
        
    }
}