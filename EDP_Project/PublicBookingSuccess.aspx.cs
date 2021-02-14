using System;

namespace EDP_Project
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            Response.Redirect("PublicViewAppointment.aspx");
        }
    }
}