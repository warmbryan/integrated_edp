using System;

namespace EDP_Project
{
    public partial class PublicAppointmentBookingSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewAppointment.aspx");
        }
    }
}