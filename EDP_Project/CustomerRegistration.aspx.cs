using DBService.Models;
using EDP_Project.App_Code;
using EDP_Project.ServiceReference1;
using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace EDP_Project
{
    public partial class CustomerRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected String validate(String FirstName, String LastName, String Email, String Password, String cfmPassword, String PhoneNumber, String BirthDate, Boolean TermsAndConditions)
        {
            String ErrorMsg = "";
            DateTime tmpDateOfBirth;
            if (!String.IsNullOrEmpty(FirstName) &&
                !String.IsNullOrEmpty(LastName) &&
                !String.IsNullOrEmpty(Email) &&
                !String.IsNullOrEmpty(Password) &&
                !String.IsNullOrEmpty(PhoneNumber) &&
                DateTime.TryParse(BirthDate, out tmpDateOfBirth) &&
                TermsAndConditions == true)
            {
                if (Password.Equals(cfmPassword) && Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,20}$"))
                {

                    if (Email.Length < 100 && FirstName.Length < 100 && LastName.Length < 100)
                    {
                        SMTPMailer smtpemail = new SMTPMailer();
                        Boolean result = false;
                        smtpemail.addEmail(Email);
                        smtpemail.addSubject("Welcome!");
                        String link = $"https://localhost:44376/Customer/VerifyEmail?email={Email}";
                        smtpemail.addBody($"<a href='{link}'> Click here to verify your account</a>");
                        smtpemail.SetHTML(true);
                        result = smtpemail.sendEmail();
                        if (result != true)
                        {
                            ErrorMsg = "Please provide a valid email";
                        }
                    }
                    else
                    {
                        ErrorMsg = "Please keep to the maximum length of each field!";
                    }
                }
                else
                {
                    ErrorMsg = "Something went wrong, please try again.";
                }
            }
            else
            {
                ErrorMsg = "Please remember to fill up all the required fields.";
            }
            return ErrorMsg;
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            String FirstName = (String)tbFirstName.Text.Trim();
            String LastName = (String)tbLastName.Text.Trim();
            String Email = (String)tbEmail.Text.Trim().ToLower();
            String Password = (String)tbPassword.Text.Trim();
            String cfmPassword = (String)tbPasswordCfm.Text.Trim();
            String PhoneNumber = (String)tbPhoneNumber.Text.Trim();
            String tmpBirthDate = (String)tbBirthDate.Text.Trim();
            Boolean TermsAndConditions = cbTermsAndConditions.Checked;
            String ErrorMsg = validate(FirstName, LastName, Email, Password, cfmPassword, PhoneNumber, tmpBirthDate, TermsAndConditions);
            if (!String.IsNullOrEmpty(ErrorMsg))
            {
                errorDiv.Visible = true;
                lbErrorMsg.Text = ErrorMsg;
            }
            else
            {
                Service1Client client = new Service1Client();
                DateTime BirthDate = DateTime.Parse(tmpBirthDate);
                Int16 result = 0;
                try
                {
                    result = client.InsertCustomer(FirstName, LastName, Email, Password, PhoneNumber, BirthDate);
                }
                catch (SqlException)
                {
                    errorDiv.Visible = true;
                    lbErrorMsg.Text = "Are you trying to perform an illegal operation?";
                }
                catch (TimeoutException)
                {
                    errorDiv.Visible = true;
                    lbErrorMsg.Text = "Something went wrong, please try again";
                }
                if (result == 1)
                {
                    Response.Redirect("~/Customer/Login?Feedback=1");
                }
                else
                {
                    errorDiv.Visible = true;
                    lbErrorMsg.Text = "Something went wrong, please try again";
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
    }
}