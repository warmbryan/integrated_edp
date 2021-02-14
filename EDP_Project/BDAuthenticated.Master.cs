using System;

namespace EDP_Project
{
    public partial class BDAuthenticated : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthRequire.CheckIfUserLoggedIn())
            {
                Response.Redirect("~/CustomerLogin");
                AuthRequire.Logout();
                return;
            }
        }
    }
}