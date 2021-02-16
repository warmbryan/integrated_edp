using DBService.Models;
using EDP_Project.ServiceReference1;
using System;

namespace EDP_Project
{
    public partial class WRSite : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AuthRequire.CheckIfUserLoggedIn() && AuthRequire.RetrieveUserRole() == "Admin")
            {
                
            }
            else
            {
                if (AuthRequire.RetrieveUserRole() == "Customer")
                {
                    Response.Redirect("~/CustomerProfile");
                }
                else if (AuthRequire.RetrieveUserRole() == "Business")
                {
                    Response.Redirect("~/BusinessProfile");
                }
            }
        }
    }
}