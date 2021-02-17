using System;
using System.Collections.Generic;
using System.Linq;

using DBService.Models;

namespace EDP_Project
{
    public partial class BDInvitations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthRequire.CheckIfUserLoggedIn())
            {
                AuthRequire.Logout();
                Response.Redirect("/CustomerLogin");
                return;
            }

            ServiceReference1.IService1 client = new ServiceReference1.Service1Client();
            List<BusinessEmployeeAccess> invites = client.GetAllInvitationsByUserId(Session["userId"].ToString()).ToList();
            lv_invitations.DataSource = invites;

            lv_invitations.DataBind();
        }
    }
}