using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppWN.ServerSide;

namespace WebAppWN
{
    public partial class TempLogin : System.Web.UI.Page
    {
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
                int businessID = ud.GetBusinessID(userNameLogin.Text, passwordLogin.Text);
                if (businessID == 0) // Wrong User Name Or ID
                {
                    ErrorLabel.Text = "שם משתמש או ססמא לא נכונים";
                    ErrorLabel.Visible = true;
                }
                else
                    //Response.Write(businessID);
                    Response.Redirect("MainPage.aspx");
            }
        }
    }
}