using DBService.Models;
using EDP_Project.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace EDP_Project
{
    public partial class CustomerLogin : System.Web.UI.Page
    {
        protected void submit_Click(object sender, EventArgs e)
        {
            String username = (String)tbUsername.Text.Trim().ToLower();
            String password = (String)tbPassword.Text.Trim();
            String role = (String)Submit_Role_Value.Text.Trim();
            Service1Client client = new Service1Client();
            if (role == "0")
            {
                CustomerClass cust = client.VerifyCustomer(username);
                if (cust.ID != Guid.Empty)
                {
                    if (client.VerifyPassword(cust.Email, password, "Customer"))
                    {
                        Boolean setSession = true;
                        if (cust.blackListed)
                        {
                            List<BlackListClass> resultOne = client.SelectAllBlacklist(username).ToList();
                            for (int i = 0; i < resultOne.Count; i++)
                            {
                                if (resultOne[i].Deleted == false)
                                {
                                    Int16 rtmpesult = client.UpdateBlacklistDeleted(resultOne[i].ID, resultOne[i].CustomerID, true);
                                    if (rtmpesult == -4)
                                    {
                                        divErrorMsg.Visible = true;
                                        lbErrorMsg.Text = "You have been blacklisted. Reason: " + HttpUtility.HtmlEncode(resultOne[i].Reason);
                                        setSession = false;
                                    }
                                    else if (rtmpesult > 0)
                                    {
                                        client.UpdateCustomerStatus(cust.ID, cust.Email, "blackListedStatus", false);
                                    }
                                    else if (rtmpesult != 1)
                                    {
                                        setSession = false;
                                    }
                                }
                            }
                        }
                        if (!cust.emailVerified)
                        {
                            divErrorMsg.Visible = true;
                            lbErrorMsg.Text = "You have changed your email, please verify before proceeding";
                            setSession = false;
                        }
                        if (cust.delete)
                        {
                            Int16 resultOne = client.DeleteCustomer(cust.ID, cust.Email, cust.deleteDate.AddDays(30));
                            if (resultOne != 1)
                            {
                                Int16 rtmpesult = client.UpdateCustomerStatus(cust.ID, cust.Email, "deleteStatus", false);
                                if (rtmpesult != 1)
                                {
                                    setSession = false;
                                }
                            }
                        }
                        if (setSession)
                        {
                            Boolean result = AuthRequire.SetUserSession(cust.ID, cust.Email, "Customer");
                            if (result == true)
                            {
                                Response.Redirect("~/Customer/Profile");
                            }
                        }
                    }
                    else
                    {
                        divErrorMsg.Visible = true;
                        lbErrorMsg.Text = "Invalid email or password";
                    }
                }
                else
                {
                    Response.Redirect("~/Customer/Registration");
                }
            }
            else if (role == "1")
            {

            }
            else if (role == "2")
            {
                AdminClass admin = client.SelectOneAdmin(username);
                if (admin != null)
                {
                    if (client.VerifyPassword(admin.UserName, password, "Admin"))
                    {
                        Boolean result = AuthRequire.SetUserSession(admin.ID, admin.UserName, "Admin");
                        if (result == true)
                        {
                            Response.Redirect("~/Administrator/ProfilePage");
                        }
                    }
                    else
                    {
                        divErrorMsg.Visible = true;
                        lbErrorMsg.Text = "Invalid email or password";
                    }
                }
            }
            else
            {

            }
        }

        protected void Reset_Password_Click(object sender, EventArgs e)
        {

        }

        protected void CustomerSide_Click(object sender, EventArgs e)
        {
            Submit_Role_Value.Text = "0";
            CustomerSide.CssClass = "nav-link active";
            BusinessSide.CssClass = "nav-link";
            AdminSide.CssClass = "nav-link";
            lbUserName.Text = "Email:";
            tbUsername.TextMode = TextBoxMode.Email;
        }

        protected void BusinessSide_Click(object sender, EventArgs e)
        {
            Submit_Role_Value.Text = "1";
            CustomerSide.CssClass = "nav-link";
            BusinessSide.CssClass = "nav-link active";
            AdminSide.CssClass = "nav-link";
            lbUserName.Text = "Username:";
            tbUsername.TextMode = TextBoxMode.SingleLine;
        }

        protected void AdminSide_Click(object sender, EventArgs e)
        {
            Submit_Role_Value.Text = "2";
            CustomerSide.CssClass = "nav-link";
            BusinessSide.CssClass = "nav-link";
            lbUserName.Text = "Username:";
            AdminSide.CssClass = "nav-link active";
            tbUsername.TextMode = TextBoxMode.SingleLine;
        }
    }
}