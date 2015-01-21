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
          
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            new DbDAL().IncreaseCurr();
        }
        
        
    }
}