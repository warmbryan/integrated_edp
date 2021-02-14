using DBService.Models;
using EDP_Project.ServiceReference1;
using System;

namespace EDP_Project
{
    public partial class AdminDashboard : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AuthRequire.CheckIfUserLoggedIn() && AuthRequire.RetrieveUserRole() == "Admin")
            {
                Service1Client client = new Service1Client();
                CustomerClass cust = client.SelectOneCustomer(Session["ae"].ToString());
                if (!IsPostBack)
                {
                    if (cust != null)
                    {
                        lbWelcomeName.Text = cust.FirstName + " " + cust.LastName;
                    }
                }
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
                else
                {
                    Response.Redirect("~/CustomerLogin");
                }
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