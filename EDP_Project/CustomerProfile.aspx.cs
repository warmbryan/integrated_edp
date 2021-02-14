using DBService.Models;
using EDP_Project.App_Code;
using EDP_Project.ServiceReference1;
using System;
using System.Text.RegularExpressions;

namespace EDP_Project
{
    public partial class CustomerProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AuthRequire.CheckIfUserLoggedIn())
                {
                    Service1Client client = new Service1Client();
                    CustomerClass cust = client.SelectOneCustomer(Session["ae"].ToString());
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
                    Response.Redirect("~/CustomerLogin");
                }
            }

        }

        protected Boolean Validation(String firstName, String lastName, String email, String PhoneNumber, String dateOfBirth)
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


        protected Boolean PasswordValidation(String OldPassword, String Password, String CfmPassword)
        {
            Int32 look = 0;
            if (String.IsNullOrEmpty(OldPassword))
            {
                String errorMsg = " Old Password field is empty";
                look++;
                lbOldPasswordError.Visible = true;
                lbOldPasswordError.Text = errorMsg;
            }
            Service1Client client = new Service1Client();
            Boolean tmpResult = client.VerifyPassword(Session["ae"].ToString(), OldPassword, "Customer");
            if (!tmpResult)
            {
                String errorMsg = " Invalid Password";
                look++;
                lbOldPasswordError.Visible = true;
                lbOldPasswordError.Text = errorMsg;
            }
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
            return look == 0;
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
                    lbEmailAddressErrors.Visible = true;
                    lbEmailAddressErrors.Text = $"{tbEmail.Text.Trim()} already exists";
                }
            }
            else
            {
                lbEmailAddressErrors.Visible = false;
            }
        }

        protected void UpdateParticulars_Click(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();
            String firstName = (String)tbFirstName.Text.Trim();
            String lastName = (String)tbLastName.Text.Trim();
            String email = (String)tbEmail.Text.Trim().ToLower();
            String phoneNumber = (String)tbPhoneNumber.Text.Trim();
            String tmpBirthDate = (String)tbBirthDate.Text.Trim();
            Boolean resultValidated = Validation(firstName, lastName, email, phoneNumber, tmpBirthDate);
            if (resultValidated)
            {
                lbFirstNameError.Visible = false;
                lbLastNameError.Visible = false;
                lbEmailAddressErrors.Visible = false;
                lbPhoneNumberError.Visible = false;
                lbDateOfBirthError.Visible = false;
                DateTime birthDate = DateTime.Parse(tmpBirthDate);
                CustomerClass cust = client.SelectOneCustomer(Session["ae"].ToString());
                if (cust != null)
                {
                    if (email.Equals(Session["ae"].ToString()))
                    {
                        Int16 result = client.UpdateCustomer(cust.ID, cust.Email, firstName, lastName, email, phoneNumber, birthDate);
                        if (result != 1)
                        {
                            errorDiv.Visible = true;
                            lbErrorMsg.Text = "Something went wrong";
                        }
                    }
                    else
                    {
                        Int16 result = client.UpdateCustomer(cust.ID, cust.Email, firstName, lastName, email, phoneNumber, birthDate);
                        if (result != 1)
                        {
                            errorDiv.Visible = true;
                            lbErrorMsg.Text = "Something went wrong";
                        }
                        result = client.UpdateCustomerStatus(cust.ID, cust.Email, "emailStatus", true);
                        if (result != 1)
                        {
                            errorDiv.Visible = true;
                            lbErrorMsg.Text = "Something went wrong";
                        }
                    }

                }
            }

        }

        protected void UpdatePassword_Click(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();
            String oldPassword = (String)tbOldPassword.Text.Trim();
            String password = (String)tbPassword.Text.Trim();
            String cfmPassword = (String)tbPasswordCfm.Text.Trim();
            Boolean resultValidation = PasswordValidation(oldPassword, password, cfmPassword);
            if (resultValidation)
            {
                lbOldPasswordError.Visible = false;
                lbPasswordError.Visible = false;
                lbCfmPasswordError.Visible = false;
                CustomerClass cust = client.SelectOneCustomer(Session["ae"].ToString());
                if (cust != null)
                {
                    client.UpdateCustomerPassword(cust.ID, cust.Email, password);
                    AuthRequire.Logout();
                }
            }
        }

        protected void UpdateDelete_Click(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();
            CustomerClass cust = client.SelectOneCustomer(Session["ae"].ToString());
            if (cust != null)
            {
                client.UpdateCustomerStatus(cust.ID, cust.Email, "deleteStatus", true);
                AuthRequire.Logout();
            }
        }
    }
}