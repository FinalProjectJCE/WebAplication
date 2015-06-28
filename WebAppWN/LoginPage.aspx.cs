using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if((Session["business"]!=null))
            {
                Response.Redirect("MainPage.aspx",false);
            }
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
                if (userNameLogin.Text.All(char.IsLetterOrDigit) && passwordLogin.Text.All(char.IsLetterOrDigit))
                {
                    UsersBL ud = new UsersBL();
                    businessID = ud.GetBusinessID(userNameLogin.Text, passwordLogin.Text);
                    if (businessID == 0) // Wrong User Name Or ID
                    {

                        ErrorLabel.Text = "שם משתמש או ססמא לא נכונים";
                        ErrorLabel.Visible = true;
                    }
                    else
                    {
                        Session.Add("business", businessID);
                        Session["servingClient"] = false;
                        AverageCalculate ac = new AverageCalculate();
                        Session["Average"] = ac;
                        HttpContext.Current.Session["ac"] = ac;
                        System.Diagnostics.Debug.WriteLine(Session.SessionID);
                        Response.Redirect("MainPage.aspx", false);
                    }
                }
                else
                {
                    ErrorLabel.Text = "לא חוקי - נא לא להזין אך ורק מספרים ואותיות";
                    ErrorLabel.Visible = true;
                }
            }
        }


    }


}