using System;

namespace EDP_Project
{
    public partial class BDAuthenticated : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("~/BDLogin");
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-12);
                return;
            }
        }
    }
}