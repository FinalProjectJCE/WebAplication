using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppWN.ServerSide;

namespace WebAppWN
{
    
    public partial class LoginPage : System.Web.UI.Page
    {
        public int businessID;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (userNameLogin.Text == "" || passwordLogin.Text == "")
            {
                ErrorLabel.Text = "אנא הכנס שם משתמש וססמא";
                ErrorLabel.Visible = true;
            }
            else
            {

                UsersDAL ud = new UsersDAL();
                    businessID = ud.GetBusinessID(userNameLogin.Text, passwordLogin.Text);
                if (businessID == 0) // Wrong User Name Or ID
                {
                    ErrorLabel.Text = "שם משתמש או ססמא לא נכונים";
                    ErrorLabel.Visible = true;
                }
                else
                    Response.Write(businessID);
                    //Session["business"]= businessID;
                    Session.Add("business", businessID);
                    Session["servingClient"] = false;
                    AverageCalculate ac = new AverageCalculate();
                    Session["Average"] = ac;
                    HttpContext.Current.Session["ac"] = ac;
                    System.Diagnostics.Debug.WriteLine(Session.SessionID);

                    Response.Redirect("MainPage.aspx",false);
            }
        }
    }
}