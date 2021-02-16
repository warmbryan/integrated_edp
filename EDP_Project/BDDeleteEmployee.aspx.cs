using System;

namespace EDP_Project
{
    public partial class BDDeleteEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthRequire.CheckIfUserLoggedIn())
            {
                AuthRequire.Logout();
                Response.Redirect("/CustomerLogin");
                return;
            }

            if (Request.Params["employee"] != null && Request.Params["business"] != null)
            {
                ServiceReference1.IService1 client = new ServiceReference1.Service1Client();
            }

            Response.Redirect("~/BDEmployees.aspx");
        }
    }
}