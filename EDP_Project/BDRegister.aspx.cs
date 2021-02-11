using System;
using System.Collections.Generic;
using System.Web;

namespace EDP_Project
{
    public partial class BDRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void Register_Me(object sender, EventArgs e)
        {
            string name = HttpUtility.HtmlEncode(tb_name.Text.Trim());
            string email = tb_email.Text.Trim();
            string password = tb_password.Text.Trim();
            string password2 = tb_password.Text.Trim();

            List<String> errors = ValidationCheck();

            if (errors.Count > 0)
            {
                lv_feedback.DataSource = errors;
                lv_feedback.DataBind();
                return;
            }

            string phone = tb_phone.Text.Trim();

            ServiceReference1.IService1 client = new ServiceReference1.Service1Client();
            if (client.CreateBusinessUser(name, email, password, phone))
            {
                Response.Redirect("~/BDLogin.aspx?registration=success");
            }
        }

        protected List<String> ValidationCheck()
        {
            List<String> errors = new List<String>();

            if (String.IsNullOrEmpty(tb_name.Text.Trim()))
            {
                errors.Add("Name field cannot be empty.");
            }

            if (String.IsNullOrEmpty(tb_email.Text.Trim()))
            {
                errors.Add("Email field cannot be empty.");
            }

            if (String.IsNullOrEmpty(tb_password.Text.Trim()) || String.IsNullOrEmpty(tb_confirmPassword.Text.Trim()))
            {
                errors.Add("Password field cannot be empty.");
            }

            if (String.IsNullOrEmpty(tb_phone.Text.Trim()))
            {
                errors.Add("Phone field cannot");
            }

            if (!tb_password.Text.Trim().Equals(tb_confirmPassword.Text.Trim()))
            {
                errors.Add("Both password fields must be the same.");
            }

            return errors;
        }
    }
}