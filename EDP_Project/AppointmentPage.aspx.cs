using System;

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