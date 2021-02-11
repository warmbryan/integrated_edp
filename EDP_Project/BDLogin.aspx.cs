using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace EDP_Project
{
    public partial class BDLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["registration"] == "success")
            {
                lbl_feedback.ForeColor = Color.Green;
                lbl_feedback.Text = "Thank you for registering, you may login now.";
            }
        }

        protected void Login_Me(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            string email = tb_email.Text.Trim();
            string password = tb_password.Text.Trim();

            ServiceReference1.IService1 client = new ServiceReference1.Service1Client();

            if (client.GetBusinessUserByEmail(email) == null)
            {
                lbl_feedback.Text = "Invalid Email or Password";
                return;
            }

            using (SqlConnection connection = new SqlConnection(conStr))
            {
                SqlCommand command = new SqlCommand("SELECT [id], [password] FROM [dbo].[BusinessUser] WHERE email = @Email", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Email", email);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader["password"].Equals(password))
                {
                    Session["userId"] = reader["id"];
                    Response.Redirect("/BDHome.aspx", false);
                }
                else
                {
                    lbl_feedback.ForeColor = Color.Red;
                    lbl_feedback.Text = "Invalid email address or password";
                }
            }
        }
    }
}