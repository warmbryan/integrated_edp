using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDP_Project
{
    public partial class AppointmentPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Book_now(object sender, EventArgs e)
        {
            Response.Redirect("BookingPage.aspx");
        }
    }
}