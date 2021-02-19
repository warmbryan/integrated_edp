using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EDP_Project.ServiceReference1;
using DBService.Models;

namespace EDP_Project
{
    public partial class BDBranches : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthRequire.CheckIfUserLoggedIn())
            {
                AuthRequire.Logout();
                Response.Redirect("/CustomerLogin");
                return;
            }
        }

        protected void LoadBusinessBranches(string businessId)
        {
            //Service1Client client = new Service1Client();
            //client.
        }
    }
}