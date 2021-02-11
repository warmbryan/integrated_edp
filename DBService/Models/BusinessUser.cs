using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DBService.Models
{
    public class BusinessUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public BusinessUser() { }

        public BusinessUser(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public BusinessUser(string id, string name, string email, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
        }

        public static bool Create(string name, string email, string password, string phone)
        {
            bool success;
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[BusinessUser] ([name], [email], [password], [phone]) VALUES (@Name, @Email, @Password, @Phone);", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        con.Open();
                        success = Convert.ToBoolean(cmd.ExecuteNonQuery());
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            return success;
        }

        public BusinessUser SelectOneByUserId(string userId)
        {
            BusinessUser bUser;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT [id], [name], [email], [phone] FROM [dbo].[BusinessUser] WHERE [id] = @UserId;", con))
                {
                    sda.SelectCommand.Parameters.AddWithValue("@UserId", userId.Trim());
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    int rec_cnt = ds.Tables[0].Rows.Count;
                    if (rec_cnt == 0)
                    {
                        bUser = null;
                    }
                    else
                    {
                        DataRow row = ds.Tables[0].Rows[0];

                        string name = row["name"].ToString();
                        string email = row["email"].ToString();
                        string phone = row["phone"].ToString();
                        bUser = new BusinessUser(userId, name, email, phone);
                    }
                }
            }
            return bUser;
        }

        public BusinessUser SelectOneByEmail(string email)
        {
            BusinessUser bUser;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT [id], [name], [email], [phone] FROM [dbo].[BusinessUser] WHERE [email] = @Email;", con))
                {
                    sda.SelectCommand.Parameters.AddWithValue("@Email", email.Trim());
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    int rec_cnt = ds.Tables[0].Rows.Count;
                    if (rec_cnt == 0)
                    {
                        bUser = null;
                    }
                    else
                    {
                        DataRow row = ds.Tables[0].Rows[0];

                        string id = row["id"].ToString();
                        string name = row["name"].ToString();
                        string phone = row["phone"].ToString();
                        bUser = new BusinessUser(id, name, email.Trim(), phone);
                    }
                }
            }
            return bUser;
        }

        public bool Exists(string email)
        {
            bool exists;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT [email] FROM [dbo].[BusinessUser] WHERE [email] = @Email;", con))
                {
                    sda.SelectCommand.Parameters.AddWithValue("@Email", email.Trim());
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    int rec_cnt = ds.Tables[0].Rows.Count;
                    exists = (rec_cnt > 0);
                }
            }
            return exists;
        }
    }
}
