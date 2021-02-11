﻿using System;

using EDP_Project.App_Code;
using EDP_Project.ServiceReference1;
using DBService.Models;

namespace EDP_Project
{
    public partial class VerifyEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String email = (String)Request.Params["email"];
            Int16 result = 0;
            Service1Client client = new Service1Client();
            if (!String.IsNullOrEmpty(email))
            {

                CustomerClass cust = client.VerifyCustomer(email);
                if (cust != null)
                {
                    result = client.UpdateCustomer(cust.ID, cust.Email, "verifyEmail", true, null);
                }
                else
                {
                    successAlert.Visible = false;
                    dangerAlert.Visible = true;
                }
                if (result == 1)
                {
                    successAlert.Visible = true;
                    dangerAlert.Visible = false;
                }
                else
                {
                    successAlert.Visible = false;
                    dangerAlert.Visible = true;
                }
            }
            else
            {
                successAlert.Visible = false;
                dangerAlert.Visible = true;
            }
        }

        protected void retry_Click(object sender, EventArgs e)
        {
            String email = (String)Request.Params["email"];
            Service1Client client = new Service1Client();
            if (!String.IsNullOrEmpty(email))
            {

                SMTPMailer smtpemail = new SMTPMailer();
                Boolean result = false;
                smtpemail.addEmail(email);
                smtpemail.addSubject("Welcome!");
                String link = $"https://localhost:44376/Customer/VerifyEmail?email={email}";
                smtpemail.addBody($"<a href='{link}'> Click here to verify your account</a>");
                smtpemail.SetHTML(true);
                result = smtpemail.sendEmail();
                if (result != true)
                {
                    ErrorMessage.Visible = true;
                    ErrorMessageLB.Text = "Email was not provided";
                }
            }
            else
            {
                successAlert.Visible = false;
                dangerAlert.Visible = true;
            }
        }
    }
}