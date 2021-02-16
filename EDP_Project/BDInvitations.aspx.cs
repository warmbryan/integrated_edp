using System;

namespace EDP_Project
{
    public partial class BDInvitations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthRequire.CheckIfUserLoggedIn())
            {
                AuthRequire.Logout();
                Response.Redirect("/CustomerLogin");
                return;
            }

            ServiceReference1.IService1 client = new ServiceReference1.Service1Client();
            lv_invitations.DataSource = client.GetAllInvitationsByUserId(Session["userId"].ToString());
            lv_invitations.DataBind();
        }
    }
}