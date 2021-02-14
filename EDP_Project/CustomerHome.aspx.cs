using System;

namespace EDP_Project
{
    public partial class CustomerHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("Search.aspx");
        }
    }
}