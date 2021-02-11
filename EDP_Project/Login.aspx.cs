using System;

using EDP_Project.ServiceReference1;
using DBService.Models;

namespace EDP_Project.@public
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) {}

        protected void submit_Click(object sender, EventArgs e)
        {
            String email = (String)tbEmail.Text.Trim().ToLower();
            String password = (String)tbPassword.Text.Trim();
            Service1Client client = new Service1Client();
            CustomerClass cust = client.VerifyCustomer(email);
            if (cust.ID != null)
            {
                if (client.VerifyPassword(cust.Email, password) && cust.emailVerified)
                {
                    if (cust.delete)
                    {
                        Int16 resultOne = client.DeleteCustomer(cust.ID, cust.Email, cust.deleteDate);
                        if (resultOne != 1)
                        {
                            client.UpdateCustomer(cust.ID, cust.Email, "deletestatus", false, null);
                            Boolean result = AuthRequire.SetUserSession(cust.ID, cust.Email);
                            if (result == true)
                            {
                                Response.Redirect("~/Customer/Profile");
                            }
                            else
                            {

                            }
                        }
                    }
                    else
                    {
                        Boolean result = AuthRequire.SetUserSession(cust.ID, cust.Email);
                        if (result == true)
                        {
                            Response.Redirect("~/Customer/Profile");
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("~/Customer/Registration");
            }
        }

        protected void Reset_Password_Click(object sender, EventArgs e)
        {
        }
    }
}