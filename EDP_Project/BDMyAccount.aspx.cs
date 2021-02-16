using DBService.Models;
using EDP_Project.ServiceReference1;
using System;

namespace EDP_Project
{
    public partial class BDMyAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthRequire.CheckIfUserLoggedIn())
            {
                AuthRequire.Logout();
                Response.Redirect("/CustomerLogin");
                return;
            }

            Service1Client client = new ServiceReference1.Service1Client();
            BusinessUser bu = client.GetBusinessUserByUserId(Session["userId"].ToString());

            tb_name.Text = bu.Name;
            tb_email.Text = bu.Email;
            tb_phone.Text = bu.Phone;
        }
    }
}