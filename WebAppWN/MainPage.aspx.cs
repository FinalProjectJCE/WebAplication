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
           // clientNum.Text = LineNum.ToString();
            //Response.Write("1 ");
            Session["LN"] = LineNum;    
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            
            LineNum++;
            //clientNum.Text = LineNum.ToString();
            Session["LN"] = LineNum;

        }
        
    }
}