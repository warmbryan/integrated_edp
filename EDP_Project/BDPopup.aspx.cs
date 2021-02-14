using EDP_Project.ServiceReference1;
using System;
using System.Web.UI;

namespace EDP_Project
{
    public partial class BDPopup : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            string aptDate = Request.QueryString["aptDate"];
            string aptTime = Request.QueryString["aptTime"];
            int feedback = client.setArrived(aptTime, aptDate);
            if (feedback == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "window.close()", true);
            }

        }
    }
}