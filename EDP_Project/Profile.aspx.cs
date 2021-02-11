using System;
using System.Text.RegularExpressions;

using EDP_Project.App_Code;
using EDP_Project.ServiceReference1;
using DBService.Models;

namespace EDP_Project
{
    public partial class Profile : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AuthRequire.CheckIfUserLoggedIn())
                {
                    Service1Client client = new Service1Client();
                    CustomerClass cust = client.SelectOneCustomer(Guid.Parse(Session["di"].ToString()), Session["ae"].ToString());
                    if (cust != null)
                    {
                        tbFirstName.Text = cust.FirstName;
                        tbLastName.Text = cust.LastName;
                        tbEmail.Text = cust.Email;
                        tbPhoneNumber.Text = cust.PhoneNumber;
                        tbBirthDate.Text = cust.DateOfBirth.ToString("yyyy-MM-dd");
                    }
                }
                else
                {
                    Response.Redirect("~/Customer/Login");
                }
            }

        }
        protected void tbEmail_TextChanged(object sender, EventArgs e)
        {
            CustomerClass cust = new CustomerClass();
            Service1Client client = new Service1Client();
            cust = client.VerifyCustomer(tbEmail.Text.Trim().ToLower());
            if (cust != null)
            {
                if (cust.emailVerified == true)
                {
                    lbEmaiLExists.Text = $"{tbEmail.Text.Trim()} already exists";
                }
            }
            else
            {
                lbEmaiLExists.Visible = false;
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();
            CustomerClass cust = client.SelectOneCustomer(Guid.Parse(Session["di"].ToString()), Session["ae"].ToString());
            if (cust != null)
            {
                client.UpdateCustomer(cust.ID, cust.Email, "deletestatus", true, null);
                AuthRequire.Logout();
            }
        }

        protected void formEditName_Click(object sender, EventArgs e)
        {
            String firstName = (String)tbFirstName.Text.Trim();
            String lastName = (String)tbLastName.Text.Trim();
            if (!String.IsNullOrEmpty(firstName) && !String.IsNullOrEmpty(lastName))
            {
                if (firstName.Length < 100 && lastName.Length < 100)
                {
                    Service1Client client = new Service1Client();
                    CustomerClass cust = client.SelectOneCustomer(Guid.Parse(Session["di"].ToString()), Session["ae"].ToString());
                    if (cust != null)
                    {
                        client.UpdateCustomer(cust.ID, cust.Email, "name", firstName, lastName);
                    }
                }

            }
        }

        protected void formEditEmail_Click(object sender, EventArgs e)
        {
            String email = (String)tbEmail.Text.Trim().ToLower();
            if (email.Length < 100 && !String.IsNullOrEmpty(email))
            {
                SMTPMailer smtpemail = new SMTPMailer();
                Boolean result = false;
                result = smtpemail.addEmail(email);
                result = smtpemail.addSubject("Welcome!");
                String link = $"https://localhost:44376/Customer/VerifyEmail?email={email}";
                result = smtpemail.addBody($"<a href='{link}'> Click here to verify your account</a>");
                smtpemail.SetHTML(true);
                result = smtpemail.sendEmail();
                if (result)
                {
                    Service1Client client = new Service1Client();
                    CustomerClass cust = client.SelectOneCustomer(Guid.Parse(Session["di"].ToString()), Session["ae"].ToString());
                    if (cust != null)
                    {
                        client.UpdateCustomer(cust.ID, cust.Email, "verifyEmail", false, null);
                        client.UpdateCustomer(cust.ID, cust.Email, "email", email, null);
                    }
                }
            }

        }

        protected void editFormPhoneNumber_Click(object sender, EventArgs e)
        {
            String phoneNumber = (String)tbPhoneNumber.Text.Trim();
            if (!String.IsNullOrEmpty(phoneNumber))
            {
                Service1Client client = new Service1Client();
                CustomerClass cust = client.SelectOneCustomer(Guid.Parse(Session["di"].ToString()), Session["ae"].ToString());
                if (cust != null)
                {
                    client.UpdateCustomer(cust.ID, cust.Email, "phonenumber", phoneNumber, null);
                }
            }
        }

        protected void editFormBirthDate_Click(object sender, EventArgs e)
        {
            String tmpBirthDate = (String)tbBirthDate.Text.Trim();
            DateTime tmpDate;
            if (!String.IsNullOrEmpty(tmpBirthDate) && DateTime.TryParse(tmpBirthDate, out tmpDate))
            {
                DateTime BirthDate = DateTime.Parse(tmpBirthDate);
                Service1Client client = new Service1Client();
                CustomerClass cust = client.SelectOneCustomer(Guid.Parse(Session["di"].ToString()), Session["ae"].ToString());
                if (cust != null)
                {
                    client.UpdateCustomer(cust.ID, cust.Email, "date", BirthDate, null);
                }
            }

        }

        protected void editFormPassword_Click(object sender, EventArgs e)
        {
            String password = (String)tbPassword.Text.Trim();
            String cfmPassword = (String)tbPasswordCfm.Text.Trim();
            if (password.Equals(cfmPassword) && Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,20}"))
            {
                Service1Client client = new Service1Client();
                CustomerClass cust = client.SelectOneCustomer(Guid.Parse(Session["di"].ToString()), Session["ae"].ToString());
                if (cust != null)
                {
                    client.UpdateCustomer(cust.ID, cust.Email, "password", password, null);
                    AuthRequire.Logout();
                }
            }

        }
    }
}