using System;

using EDP_Project.ServiceReference1;
using DBService.Models;

namespace EDP_Project
{
    public partial class BDMyAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("~/BDLogin");
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-12);
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