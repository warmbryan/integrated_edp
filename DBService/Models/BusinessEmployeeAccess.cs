using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DBService.Models
{
    public class BusinessEmployeeAccess
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string BusinessId { get; set; }
        public string RoleId { get; set; }
        public bool ReadAppointment { get; set; }
        public bool WriteAppointment { get; set; }
        public bool ReadCustomerChat { get; set; }
        public bool WriteCustomerChat { get; set; }
        public bool Accepted { get; set; }
        public BusinessUser employee { get; set; }
        public string Role { get; set; }
        public Business Business { get; set; }

        public BusinessEmployeeAccess() { }

        public BusinessEmployeeAccess(string id, string userId, string businessId, string roleId, bool rApp, bool wApp, bool rCC, bool wCC, bool accepted)
        {
            Id = id;
            UserId = userId;
            BusinessId = businessId;
            RoleId = roleId;
            ReadAppointment = rApp;
            WriteAppointment = wApp;
            ReadCustomerChat = rCC;
            WriteCustomerChat = wCC;
            Accepted = accepted;
        }

        public BusinessEmployeeAccess(string id, string userId, string businessId, string roleId, bool rApp, bool wApp, bool rCC, bool wCC, bool accepted, BusinessUser user)
        {
            Id = id;
            UserId = userId;
            BusinessId = businessId;
            RoleId = roleId;
            ReadAppointment = rApp;
            WriteAppointment = wApp;
            ReadCustomerChat = rCC;
            WriteCustomerChat = wCC;
            Accepted = accepted;
            employee = user;
        }

        public BusinessEmployeeAccess(Business business, string id, string roleId, bool rApp, bool wApp, bool rCC, bool wCC, bool accepted)
        {
            Business = business;
            Id = id;
            RoleId = roleId;
            ReadAppointment = rApp;
            WriteAppointment = wApp;
            ReadCustomerChat = rCC;
            WriteCustomerChat = wCC;
            Accepted = accepted;
        }

        public bool Create(string userId, string businessId, string roleId, bool rApp, bool wApp, bool rCC, bool wCC)
        {
            bool success = true;
            string queryString = "INSERT INTO [dbo].[BusinessEmployeeAccess] " +
                "([userId], [businessId], [roleId], [readAppointment], [writeAppointment], " +
                "[readCustomerChat], [writeCustomerChat])" +
                " VALUES (@UserId, @BusinessId, @RoleId, @RApp, @WApp, @RCC, @WCC)";
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryString, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@UserId", userId.Trim());
                        cmd.Parameters.AddWithValue("@BusinessId", businessId.Trim());
                        cmd.Parameters.AddWithValue("@RoleId", roleId.Trim());
                        cmd.Parameters.AddWithValue("@RApp", rApp);
                        cmd.Parameters.AddWithValue("@WApp", wApp);
                        cmd.Parameters.AddWithValue("@RCC", rCC);
                        cmd.Parameters.AddWithValue("@WCC", wCC);
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
            }
            return success;
        }

        public BusinessEmployeeAccess SelectOne(string beaId)
        {
            BusinessEmployeeAccess bea;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [dbo].[BusinessEmployeeAccess] WHERE [id] = @BeaId;", con))
                {
                    sda.SelectCommand.Parameters.AddWithValue("@BeaId", beaId);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    int rec_cnt = ds.Tables[0].Rows.Count;
                    if (rec_cnt == 0)
                    {
                        bea = null;
                    }
                    else
                    {
                        DataRow row = ds.Tables[0].Rows[0];

                        string id = row["id"].ToString();
                        string userId = row["userId"].ToString();
                        string businessId = row["businessId"].ToString();
                        string roleId = row["roleId"].ToString();
                        bool readAppointment = Convert.ToBoolean(row["readAppointment"]);
                        bool writeAppointment = Convert.ToBoolean(row["writeAppointment"]);
                        bool readCustomerChat = Convert.ToBoolean(row["readCustomerChat"]);
                        bool writeCustomerChat = Convert.ToBoolean(row["writeCustomerChat"]);
                        bool accepted = Convert.ToBoolean(row["accepted"]);
                        bea = new BusinessEmployeeAccess(id, userId, businessId, roleId, readAppointment, writeAppointment, readCustomerChat, writeCustomerChat, accepted);
                    }
                }
            }
            return bea;
        }

        public List<BusinessEmployeeAccess> SelectAllByBusinessId(string businessId)
        {
            List<BusinessEmployeeAccess> employees = new List<BusinessEmployeeAccess>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(
                    "SELECT [dbo].[BusinessEmployeeAccess].*, " +
                    "[dbo].[BusinessUser].[id] userId, " +
                    "[dbo].[BusinessUser].[name] userName, " +
                    "[dbo].[BusinessUser].[email] userEmail, " +
                    "[dbo].[BusinessUser].[phone] userPhone " +
                    "FROM [dbo].[BusinessEmployeeAccess] " +
                    "INNER JOIN [dbo].[BusinessUser] " +
                    "ON [dbo].[BusinessEmployeeAccess].[userId] = dbo.BusinessUser.id " +
                    "WHERE [businessId] = @BusinessId;", con))
                {
                    sda.SelectCommand.Parameters.AddWithValue("@BusinessId", businessId.Trim());
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    int rec_cnt = ds.Tables[0].Rows.Count;
                    if (rec_cnt > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            string id = row["id"].ToString();
                            string userId = row["userId"].ToString();
                            string roleId = row["roleId"].ToString();
                            bool rApp = Convert.ToBoolean(row["readAppointment"]);
                            bool wApp = Convert.ToBoolean(row["writeAppointment"]);
                            bool rCC = Convert.ToBoolean(row["readCustomerChat"]);
                            bool wCC = Convert.ToBoolean(row["writeCustomerChat"]);
                            bool accepted = Convert.ToBoolean(row["accepted"]);

                            string eId = row["userId"].ToString();
                            string eName = row["userName"].ToString();
                            string eEmail = row["userEmail"].ToString();
                            string ePhone = row["userPhone"].ToString();
                            BusinessEmployeeAccess bea = new BusinessEmployeeAccess(id, userId, businessId, roleId, rApp, wApp, rCC, wCC, accepted, new BusinessUser(eName, eEmail, "bruhbruhbruh", ePhone));
                            employees.Add(bea);
                        }
                    }
                }
            }
            return employees;
        }

        public List<BusinessEmployeeAccess> SelectAllByUserId(string userId)
        {
            List<BusinessEmployeeAccess> employeeBeas = new List<BusinessEmployeeAccess>();

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(
                        "SELECT * " +
                        "FROM BusinessEmployeeAccess " +
                        "WHERE [userId] = @UserId;", con))
                    {
                        sda.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                        DataSet ds = new DataSet();
                        sda.Fill(ds);

                        int rec_cnt = ds.Tables[0].Rows.Count;
                        if (rec_cnt > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {

                                string businessId = row["businessId"].ToString();
                                string id = row["id"].ToString();
                                string roleId = row["roleId"].ToString();
                                bool rApp = Convert.ToBoolean(row["readAppointment"]);
                                bool wApp = Convert.ToBoolean(row["writeAppointment"]);
                                bool rCC = Convert.ToBoolean(row["readCustomerChat"]);
                                bool wCC = Convert.ToBoolean(row["writeCustomerChat"]);
                                bool accepted = Convert.ToBoolean(row["accepted"]);

                                BusinessEmployeeAccess bea = new BusinessEmployeeAccess(new Business().SelectOne(businessId), id, roleId, rApp, wApp, rCC, wCC, accepted);
                                employeeBeas.Add(bea);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in BusinessEmployeeAccess SelectAllByUserId - " + ex.Message + " " + ex.ToString());
            }

            return employeeBeas;
        }

        public List<BusinessEmployeeAccess> SelectAcceptedByUserId(string userId)
        {
            List<BusinessEmployeeAccess> employeeBeas = new List<BusinessEmployeeAccess>();

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(
                        "SELECT * " +
                        "FROM BusinessEmployeeAccess " +
                        "WHERE [userId] = @UserId AND [accepted] = 1;", con))
                    {
                        sda.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                        DataSet ds = new DataSet();
                        sda.Fill(ds);

                        int rec_cnt = ds.Tables[0].Rows.Count;
                        if (rec_cnt > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {

                                string businessId = row["businessId"].ToString();
                                string id = row["id"].ToString();
                                string roleId = row["roleId"].ToString();
                                bool rApp = Convert.ToBoolean(row["readAppointment"]);
                                bool wApp = Convert.ToBoolean(row["writeAppointment"]);
                                bool rCC = Convert.ToBoolean(row["readCustomerChat"]);
                                bool wCC = Convert.ToBoolean(row["writeCustomerChat"]);
                                bool accepted = Convert.ToBoolean(row["accepted"]);

                                BusinessEmployeeAccess bea = new BusinessEmployeeAccess(new Business().SelectOne(businessId), id, roleId, rApp, wApp, rCC, wCC, accepted);
                                employeeBeas.Add(bea);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in BusinessEmployeeAccess SelectAcceptedByUserId - " + ex.Message + " " + ex.ToString());
            }

            return employeeBeas;
        }

        public bool Update(string userId, string businessId, string roleId, bool rApp, bool wApp, bool rCC, bool wCC)
        {
            bool success = true;
            string queryString = "UPDATE [dbo].[BusinessEmployeeAccess] " +
                "SET [roleId] = @RoleId, [readAppointment] = @RApp, [writeAppointment] = @WApp, " +
                "[readCustomerChat] = @RCC, [writeCustomerChat] = @WCC, " +
                "WHERE userId = @UserId, businessId = @BusinessId;";
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryString, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@UserId", userId.Trim());
                        cmd.Parameters.AddWithValue("@BusinessId", businessId.Trim());
                        cmd.Parameters.AddWithValue("@RoleId", roleId.Trim());
                        cmd.Parameters.AddWithValue("@RApp", rApp);
                        cmd.Parameters.AddWithValue("@WApp", wApp);
                        cmd.Parameters.AddWithValue("@RCC", rCC);
                        cmd.Parameters.AddWithValue("@WCC", wCC);

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
            }
            return success;
        }

        public bool Delete(string userId, string businessId)
        {
            bool success = true;
            string queryString = "DELETE FROM [dbo].[BusinessEmployeeAccess] " +
                "WHERE userId = @userId, businessId = @BusinessId;";
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryString, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@UserId", userId.Trim());
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
            }
            return success;
        }

        public bool AcceptInvitation(string beaId)
        {
            bool success = false;

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE BusinessEmployeeAccess SET accepted = 1 WHERE [id] = @Id;", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", beaId);
                        con.Open();
                        success = cmd.ExecuteNonQuery() > 0;
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in BusinessEmployeeAccess AcceptInvitation" + ex + " message: " + ex.Message);
            }

            return success;
        }

        public bool RejectInvitation(string beaId)
        {
            bool success = false;

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM BusinessEmployeeAccess WHERE [Id] = @Id;", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", beaId);
                        con.Open();
                        success = cmd.ExecuteNonQuery() > 0;
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in BusinessEmployeeAccess RejectInvitation" + ex + " message: " + ex.Message);
            }

            return success;
        }
    }
}
