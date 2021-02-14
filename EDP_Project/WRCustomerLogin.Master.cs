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