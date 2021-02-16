using System;

namespace EDP_Project
{
    public partial class BDDeleteBusiness : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthRequire.CheckIfUserLoggedIn())
            {
                AuthRequire.Logout();
                Response.Redirect("/CustomerLogin");
                return;
            }

            if (Request.Params["business"] != null)
            {
                ServiceReference1.IService1 client = new ServiceReference1.Service1Client();
                client.DeleteBusiness(Request.Params["business"].Trim());
            }

            Response.Redirect("~/BDBusinesses.aspx");
        }
    }
}