using System;

using EDP_Project.ServiceReference1;

namespace EDP_Project
{
    public partial class BDBusinesses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthRequire.CheckIfUserLoggedIn())
            {
                AuthRequire.Logout();
                Response.Redirect("~/CustomerLogin");
                return;
            }

            Service1Client client = new Service1Client();

            lv_businesses.DataSource = client.GetAllBusinessByUserId(Session["userId"].ToString());
            lv_businesses.DataBind();


            lv_sharedBusinesses.DataSource = client.GetAcceptedBusinessInviteByUserId(Session["userId"].ToString());
            lv_sharedBusinesses.DataBind();
        }
    }
}