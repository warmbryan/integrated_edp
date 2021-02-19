using System;

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

            ServiceReference1.IService1 client = new ServiceReference1.Service1Client();

            lv_businesses.DataSource = client.GetAllBusinessByUserId(Session["userId"].ToString());
            lv_businesses.DataBind();
        }
    }
}