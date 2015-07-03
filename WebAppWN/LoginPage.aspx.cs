using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppWN.ServerSide;

/*
 * This Is The Login Page Class.
 */
namespace WebAppWN
{
    
    public partial class LoginPage : System.Web.UI.Page
    {
        public int businessID;

        /*
         * On Page Load, If The Session Exists, Redirect The User To The Main Page
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            if((Session["business"]!=null))
            {
                Response.Redirect("MainPage.aspx",false);
            }
        }

        /*
         * Login Button Clicked, This Will Now Check The User Username And Password.
         * If The Input Is Vallid And The Username And Password Exists, Redirect To The Main Page
         */
        protected void loginButtonClick(object sender, EventArgs e)
        {
            if (userNameLogin.Text == "" || passwordLogin.Text == "") // If One Or Both Of The Fields Are Empty 
            {
                ErrorLabel.Text = "אנא הכנס שם משתמש וססמא";
                ErrorLabel.Visible = true;
            }
            else
            {
                // If The Input Includes Only Letters Or Digits, Check If The Username And Password Exists.
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
                        // The Username And Password Exists
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