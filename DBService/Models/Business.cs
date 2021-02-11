using System;
using System.Collections.Generic;

using System.Configuration;

using System.Data;
using System.Data.SqlClient;

namespace DBService.Models
{
    public class Business
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public string LogoId { get; set; }
        public string AcraCertificate { get; set; }
        public bool Verified { get; set; }
        public string UserId { get; set; }
        public string AdminId { get; set; }
        public string CategoryId { get; set; }

        public Business() { }

        public Business(string id, string name, string registrationNumber, string url, string type, string logoId, string acraCertificate, bool verified, string userId, string adminId, string categoryId)
        {
            Id = id;
            Name = name;
            RegistrationNumber = registrationNumber;
            Url = url;
            Type = type;
            LogoId = logoId;
            AcraCertificate = acraCertificate;
            Verified = verified;
            UserId = userId;
            AdminId = adminId;
            CategoryId = categoryId;
        }

        public bool Register(string name, string registrationNumber, string url, string type, string acra, string logoId, string userId)
        {
            bool success;
            string queryString = "INSERT INTO [dbo].[Business] ([name], [registrationNumber], [type], [url], [userId], [acraCertificate], [logoId]) VALUES (@Name, @RegistrationNumber, @Type, @Url, @UserId, @Acra, @LogoId);";

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryString, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Name", name.Trim());
                        cmd.Parameters.AddWithValue("@RegistrationNumber", registrationNumber.Trim());
                        cmd.Parameters.AddWithValue("@Type", type.Trim());
                        cmd.Parameters.AddWithValue("@Url", url.Trim());
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@Acra", acra);
                        if (logoId == null)
                            cmd.Parameters.AddWithValue("@LogoId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LogoId", logoId);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                throw ex;
            }

            return success;
        }

        public Business SelectOne(string businessId)
        {
            Business business;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [dbo].[Business] WHERE [id] = @BusinessId;", con))
                {
                    sda.SelectCommand.Parameters.AddWithValue("@BusinessId", businessId);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    int rec_cnt = ds.Tables[0].Rows.Count;
                    if (rec_cnt == 0)
                    {
                        business = null;
                    }
                    else
                    {
                        DataRow row = ds.Tables[0].Rows[0];

                        string name = row["name"].ToString();
                        string registrationNumber = row["registrationNumber"].ToString();
                        string type = row["type"].ToString();
                        string url = row["url"].ToString();
                        string logoId = row["logoId"].ToString();
                        string acraCertificate = row["acraCertificate"].ToString();
                        bool verified = Convert.ToBoolean(row["verified"]);
                        string customerId = row["userId"].ToString();
                        string adminId = row["adminId"].ToString();
                        string categoryId = row["categoryId"].ToString();
                        business = new Business(businessId, name, registrationNumber, url, type, logoId, acraCertificate, verified, customerId, adminId, categoryId);
                    }
                }
            }
            return business;
        }

        // for admins
        public List<Business> SelectAll()
        {
            List<Business> businesses = new List<Business>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [dbo].[Business];", con))
                {
                    // sda.SelectCommand.Parameters.AddWithValue("@Email", email.Trim());
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    int rec_cnt = ds.Tables[0].Rows.Count;
                    if (rec_cnt == 0)
                    {
                        businesses = null;
                    }
                    else
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            string id = row["id"].ToString();
                            string name = row["name"].ToString();
                            string registrationNumber = row["registrationNumber"].ToString();
                            string type = row["type"].ToString();
                            string url = row["url"].ToString();
                            string logoId = row["logoId"].ToString();
                            string acraCertificate = row["acraCertificate"].ToString();
                            bool verified = Convert.ToBoolean(row["verified"]);
                            string userId = row["userId"].ToString();
                            string adminId = row["adminId"].ToString();
                            string categoryId = row["categoryId"].ToString();
                            Business business = new Business(id, name, registrationNumber, url, type, logoId, acraCertificate, verified, userId, adminId, categoryId);
                            businesses.Add(business);
                        }
                    }
                }
            }
            return businesses;
        }

        public List<Business> SelectAllByUserId(string userId)
        {
            List<Business> businesses = new List<Business>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [dbo].[Business] WHERE [userId] = @UserId;", con))
                {
                    sda.SelectCommand.Parameters.AddWithValue("@UserId", userId.Trim());
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    int rec_cnt = ds.Tables[0].Rows.Count;
                    if (rec_cnt == 0)
                    {
                        businesses = null;
                    }
                    else
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            string id = row["id"].ToString();
                            string name = row["name"].ToString();
                            string registrationNumber = row["registrationNumber"].ToString();
                            string type = row["type"].ToString();
                            string url = row["url"].ToString();
                            string logoId = row["logoId"].ToString();
                            string acraCertificate = row["acraCertificate"].ToString();
                            bool verified = Convert.ToBoolean(row["verified"]);
                            string adminId = row["adminId"].ToString();
                            string categoryId = row["categoryId"].ToString();
                            Business business = new Business(id, name, registrationNumber, url, type, logoId, acraCertificate, verified, userId, adminId, categoryId);
                            businesses.Add(business);
                        }
                    }
                }
            }
            return businesses;
        }

        public bool Update(string businessId, string name, string registrationNumber, string url, string type, string acra, string logoId)
        {
            bool success;
            string queryString = "UPDATE [dbo].[Business] " +
                "SET [name] = @Name, [registrationNumber] = @RegistrationNumber, " +
                "[type] = @Type, [url] = @Url, " +
                "[acraCertificate] = @AcraCertificate, [logoid] = @LogoId " +
                "WHERE [id] = @BusinessId;";
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryString, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@BusinessId", businessId);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@RegistrationNumber", registrationNumber);
                        cmd.Parameters.AddWithValue("@Type", type);
                        cmd.Parameters.AddWithValue("@Url", url);

                        if (acra == null)
                            cmd.Parameters.AddWithValue("@AcraCertificate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@AcraCertificate", acra);

                        if (logoId == null)
                            cmd.Parameters.AddWithValue("@LogoId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LogoId", logoId);

                        con.Open();
                        int affected = cmd.ExecuteNonQuery();
                        success = (affected > 0);
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                throw ex;
            }
            return success;
        }

        public bool Delete(string businessId)
        {
            bool success = true;
            string queryString = "DELETE FROM [dbo].[Business] " +
                "WHERE [id] = @BusinessId;";
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryString, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@BusinessId", businessId.Trim());
                        con.Open();
                        int affected = cmd.ExecuteNonQuery();
                        success = (affected > 0);
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                throw ex;
            }
            return success;
        }
    }
}
