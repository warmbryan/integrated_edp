using DBService.Models;
using EDP_Project.ServiceReference1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace EDP_Project
{
    public partial class CustomerLogin : System.Web.UI.Page
    {
        protected class MyObject
        {
            public String success { get; set; }
            public List<String> ErrorMessage { get; set; }
        }

        public Boolean ValidateCaptcha()
        {
            Boolean result = true;
            string captchaResponse = Request.Form["g-recaptcha-response"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
            ("https://www.google.com/recaptcha/api/siteverify?secret=6LexESQaAAAAAOhm5aUm3hFjU0yuxeUQSnngRzAJ&response=" + captchaResponse);
            try
            {
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(wResponse.GetResponseStream()))
                    {
                        String jsonResponse = sr.ReadToEnd();

                        JavaScriptSerializer js = new JavaScriptSerializer();

                        MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);

                        result = Convert.ToBoolean(jsonObject.success);
                    }
                }
                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            String username = (String)tbUsername.Text.Trim().ToLower();
            String password = (String)tbPassword.Text.Trim();
            String role = (String)Submit_Role_Value.Text.Trim();
            Service1Client client = new Service1Client();
            if (role == "0" && ValidateCaptcha())
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
                                Response.Redirect("~/CustomerProfile");
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
                    Response.Redirect("~/CustomerRegistration");
                }
            }
            else if (role == "1" && ValidateCaptcha())
            {
                BusinessUser business = client.GetBusinessUserByEmail(username);
                if (business != null)
                {
                    if (client.VerifyPassword(business.Email, password, "Business"))
                    {
                        Boolean result = AuthRequire.SetUserSession(Guid.Parse(business.Id), business.Email, "Business");
                        if (result == true)
                        {
                            Response.Redirect("/BDHome.aspx", false);
                        }
                    }
                    else
                    {
                        divErrorMsg.Visible = true;
                        lbErrorMsg.Text = "Invalid email or password";
                    }
                }
            }
            else if (role == "2" && ValidateCaptcha())
            {
                AdminClass admin = client.SelectOneAdmin(username);
                if (admin != null)
                {
                    if (client.VerifyPassword(admin.UserName, password, "Admin"))
                    {
                        Boolean result = AuthRequire.SetUserSession(admin.ID, admin.UserName, "Admin");
                        if (result == true)
                        {
                            Response.Redirect("~/AdminHome");
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