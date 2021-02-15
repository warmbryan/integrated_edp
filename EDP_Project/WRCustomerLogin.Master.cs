using DBService.Models;
using EDP_Project.ServiceReference1;
using System;

namespace EDP_Project
{
    public partial class WRCustomerLogin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AuthRequire.CheckIfUserLoggedIn() && AuthRequire.RetrieveUserRole() == "Customer")
            {
                
            }
        }
        protected void logout_Click(object sender, EventArgs e)
        {
            if (AuthRequire.CheckIfUserLoggedIn())
            {
                AuthRequire.Logout();
            }
        }
    }
}