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

        protected Boolean PasswordValidation(String Password, String CfmPassword, Boolean split)
        {
            Int32 look = 0;
            if (split)
            {
                if (String.IsNullOrEmpty(Password))
                {
                    String errorMsg = " Password field is empty";
                    if (Password.Length >= 20)
                    {
                        errorMsg = " Password should not exceed 20 characters";
                    }
                    if (!Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,20}"))
                    {
                        errorMsg = " Invalid password supplied, please try again";
                    }
                    look++;
                    lbPasswordError.Visible = true;
                    lbPasswordError.Text = errorMsg;
                }
                if (String.IsNullOrEmpty(CfmPassword))
                {
                    String errorMsg = " Confirm password field is empty";
                    look++;
                    lbCfmPasswordError.Visible = true;
                    lbCfmPasswordError.Text = errorMsg;
                }
                if (!Password.Equals(CfmPassword))
                {
                    String errorMsg = " Password does not match";
                    look++;
                    lbCfmPasswordError.Visible = true;
                    lbCfmPasswordError.Text = errorMsg;
                }
            }
            else
            {
                if (String.IsNullOrEmpty(Password))
                {
                    String errorMsg = " Password field is empty";
                    if (Password.Length >= 20)
                    {
                        errorMsg = " Password should not exceed 20 characters";
                    }
                    if (!Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,20}"))
                    {
                        errorMsg = " Invalid password supplied, please try again";
                    }
                    look++;
                    lbPasswordErrorTwo.Visible = true;
                    lbPasswordErrorTwo.Text = errorMsg;
                }
                if (String.IsNullOrEmpty(CfmPassword))
                {
                    String errorMsg = " Confirm password field is empty";
                    look++;
                    lbCfmPasswordErrorTwo.Visible = true;
                    lbCfmPasswordErrorTwo.Text = errorMsg;
                }
                if (!Password.Equals(CfmPassword))
                {
                    String errorMsg = " Password does not match";
                    look++;
                    lbCfmPasswordErrorTwo.Visible = true;
                    lbCfmPasswordErrorTwo.Text = errorMsg;
                }

            }
            return look == 0;
        }

        protected Boolean validateExtra(String Fullname, String Email, String PhoneNumber)
        {
            Int32 look = 0;
            if (String.IsNullOrEmpty(Fullname))
            {
                String errorMsg = " Fullname field is empty";
                if (Fullname.Length >= 100)
                {
                    errorMsg = " First name should not exceed 100 characters";
                }
                look++;
                lbFirstNameErrorTwo.Visible = true;
                lbFirstNameErrorTwo.Text = errorMsg;
            }
            if (String.IsNullOrEmpty(PhoneNumber))
            {
                String errorMsg = " Last name field is empty";
                if (PhoneNumber.Length >= 100)
                {
                    errorMsg = " Last name should not exceed 100 characters";
                }
                look++;
                lbPhoneNumberErrorTwo.Visible = true;
                lbPhoneNumberErrorTwo.Text = errorMsg;
            }
            if (String.IsNullOrEmpty(Email))
            {
                String errorMsg = " Email field is empty";
                if (Email.Length >= 200)
                {
                    errorMsg = " Email should not exceed 200 characters";
                }
                if (!Regex.IsMatch(Email, @"^\w+[\+\.\w-]*@([\w-]+\.)*\w+[\w-]*\.([a-z]{2,4}|\d+)$"))
                {
                    errorMsg = " Please provide a valid email";
                }
                look++;
                lbEmailAddressErrorsTwo.Visible = true;
                lbEmailAddressErrorsTwo.Text = errorMsg;
            }
            return look == 0;
        }

        protected Boolean validate(String firstName, String lastName, String email, String PhoneNumber, String dateOfBirth, Boolean TermsAndConditions)
        {
            Int32 look = 0;
            if (String.IsNullOrEmpty(firstName))
            {
                String errorMsg = " First name field is empty";
                if (firstName.Length >= 100)
                {
                    errorMsg = " First name should not exceed 100 characters";
                }
                look++;
                lbFirstNameError.Visible = true;
                lbFirstNameError.Text = errorMsg;
            }
            if (String.IsNullOrEmpty(lastName))
            {
                String errorMsg = " Last name field is empty";
                if (lastName.Length >= 100)
                {
                    errorMsg = " Last name should not exceed 100 characters";
                }
                look++;
                lbLastNameError.Visible = true;
                lbLastNameError.Text = errorMsg;
            }
            if (String.IsNullOrEmpty(PhoneNumber))
            {
                String errorMsg = " Last name field is empty";
                if (lastName.Length >= 100)
                {
                    errorMsg = " Last name should not exceed 100 characters";
                }
                look++;
                lbPhoneNumberError.Visible = true;
                lbPhoneNumberError.Text = errorMsg;
            }
            if (String.IsNullOrEmpty(email))
            {
                String errorMsg = " Email field is empty";
                if (email.Length >= 100)
                {
                    errorMsg = " Email should not exceed 100 characters";
                }
                if (!Regex.IsMatch(email, @"^\w+[\+\.\w-]*@([\w-]+\.)*\w+[\w-]*\.([a-z]{2,4}|\d+)$"))
                {
                    errorMsg = " Please provide a valid email";
                }
                look++;
                lbEmailAddressErrors.Visible = true;
                lbEmailAddressErrors.Text = errorMsg;
            }
            if (String.IsNullOrEmpty(dateOfBirth))
            {
                String errorMsg = " Date of birth field is empty";
                DateTime tmpDOB;
                if (DateTime.TryParse(dateOfBirth, out tmpDOB))
                {
                    errorMsg = " Please provide a proper date of birth";
                }
                if (DateTime.Now.CompareTo(tmpDOB) >= 0)
                {
                    errorMsg = " Please provide a proper date of birth";
                }
                look++;
                lbDateOfBirthError.Visible = true;
                lbDateOfBirthError.Text = errorMsg;
            }
            return look == 0;
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            
            String role = (String)Submit_Role_Value.Text.Trim();
            if (role == "0")
            {
                String FirstName = (String)tbFirstName.Text.Trim();
                String LastName = (String)tbLastName.Text.Trim();
                String Email = (String)tbEmail.Text.Trim().ToLower();
                tbEmail_TextChanged(Email);
                String Password = (String)tbPassword.Text.Trim();
                String cfmPassword = (String)tbPasswordCfm.Text.Trim();
                String PhoneNumber = (String)tbPhoneNumber.Text.Trim();
                String tmpBirthDate = (String)tbBirthDate.Text.Trim();
                Boolean TermsAndConditions = cbTermsAndConditions.Checked;
                Boolean One = validate(FirstName, LastName, Email, PhoneNumber, tmpBirthDate, TermsAndConditions);
                Boolean Two = PasswordValidation(Password, cfmPassword, true);
                if (One && Two)
                {
                    Service1Client client = new Service1Client();
                    DateTime BirthDate = DateTime.Parse(tmpBirthDate);
                    Int16 result = 0;
                    try
                    {
                        SMTPMailer smtpemail = new SMTPMailer();
                        smtpemail.addEmail(Email);
                        smtpemail.addSubject("Welcome!");
                        String link = $"https://localhost:{Request.Url.Port}/ CustomerVerifyEmail?email={Email}&purpose=Customer";
                        smtpemail.addBody($"<a href='{link}'> Click here to verify your account</a>");
                        smtpemail.SetHTML(true);
                        result = client.InsertCustomer(FirstName, LastName, Email, Password, PhoneNumber, BirthDate);
                        if (result >= 0)
                        {
                            Boolean resultTmp = smtpemail.sendEmail();
                            lbErrorMsg.Text = "Email failed to register!";
                        }
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
                        Response.Redirect("~/CustomerLogin?Feedback=1");
                    }
                    else
                    {
                        errorDiv.Visible = true;
                        lbErrorMsg.Text = "Something went wrong, please try again";
                    }
                }
            }
            else if (role == "1")
            {
                String FullName = (String)tbFullname.Text.Trim();
                String Email = (String)tbEmailTwo.Text.Trim().ToLower();
                tbEmail_TextChanged(Email);
                String Password = (String)tbPasswordTwo.Text.Trim();
                String cfmPassword = (String)tbPasswordCfmTwo.Text.Trim();
                String PhoneNumber = (String)tbPhoneNumberTwo.Text.Trim();
                Boolean One = validateExtra(FullName, Email, PhoneNumber);
                Boolean Two = PasswordValidation(Password, cfmPassword, false);
                if (One && Two)
                {
                    Service1Client client = new Service1Client();
                    Boolean result = false;
                    try
                    {
                        SMTPMailer smtpemail = new SMTPMailer();
                        smtpemail.addEmail(Email);
                        smtpemail.addSubject("Welcome!");
                        String link = $"https://localhost:{Request.Url.Port}/ CustomerVerifyEmail?email={Email}&purpose=Business";
                        smtpemail.addBody($"<a href='{link}'> Click here to verify your account</a>");
                        smtpemail.SetHTML(true);
                        result = client.CreateBusinessUser(FullName, Email, Password, PhoneNumber);
                        if (result == true)
                        {
                            Boolean resultTmp = smtpemail.sendEmail();
                            if (resultTmp != true)
                            {
                                lbErrorMsg.Text = "Email failed to register!";
                            }
                        }
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
                    if (result == true)
                    {
                        Response.Redirect("~/CustomerLogin?Feedback=1");
                    }
                    else
                    {
                        errorDiv.Visible = true;
                        lbErrorMsg.Text = "Something went wrong, please try again";
                    }
                }
            }
        }

        private Boolean tbEmail_TextChanged(String value)
        {
            CustomerClass cust = new CustomerClass();
            Service1Client client = new Service1Client();
            cust = client.VerifyCustomer(value);
            if (cust != null)
            {
                if (cust.emailVerified == true)
                {
                    lbEmaiLExists.Visible = true;
                    lbEmaiLExists.Text = $"{value} already exists";
                    return false;
                }
                return true;
            }
            else
            {
                lbEmaiLExists.Visible = false;
                return true;
            }
        }

        protected void CustomerSide_Click(object sender, EventArgs e)
        {
            Submit_Role_Value.Text = "0";
            CustomerSide.CssClass = "nav-link active";
            BusinessSide.CssClass = "nav-link";
            custRegister.Visible = true;
            busiRegister.Visible = false;
        }

        protected void BusinessSide_Click(object sender, EventArgs e)
        {
            Submit_Role_Value.Text = "1";
            CustomerSide.CssClass = "nav-link";
            BusinessSide.CssClass = "nav-link active";
            custRegister.Visible = false;
            busiRegister.Visible = true;
        }
    }
}