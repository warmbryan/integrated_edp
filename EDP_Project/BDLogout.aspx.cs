using System;

namespace EDP_Project
{
    public partial class BDLogout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // make sure to clear user's cookies
            // TODO: clear user cookies
            if (Response.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-12);
            }
            Response.Redirect("/BDLogin.aspx");
        }
    }
}