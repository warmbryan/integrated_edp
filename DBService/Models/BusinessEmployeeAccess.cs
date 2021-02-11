﻿using System;
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
        public bool ReadAppointment { get; set; }
        public bool WriteAppointment { get; set; }
        public bool ReadCustomerChat { get; set; }
        public bool WriteCustomerChat { get; set; }
        public bool Accepted { get; set; }
        public BusinessUser employee { get; set; }
        public string Role { get; set; }
        public Business Business { get; set; }

        public BusinessEmployeeAccess() { }

        public BusinessEmployeeAccess(string userId, string businessId, bool rApp, bool wApp, bool rCC, bool wCC, string role, bool accepted)
        {
            UserId = userId;
            BusinessId = businessId;
            ReadAppointment = rApp;
            WriteAppointment = wApp;
            ReadCustomerChat = rCC;
            WriteCustomerChat = wCC;
            Accepted = accepted;
            Role = role;
        }

        public BusinessEmployeeAccess(string userId, string businessId, bool rApp, bool wApp, bool rCC, bool wCC, string role, bool accepted, BusinessUser user)
        {
            UserId = userId;
            BusinessId = businessId;
            ReadAppointment = rApp;
            WriteAppointment = wApp;
            ReadCustomerChat = rCC;
            WriteCustomerChat = wCC;
            Accepted = accepted;
            Role = role;
            employee = user;
        }

        public BusinessEmployeeAccess(Business business, string id, bool rApp, bool wApp, bool rCC, bool wCC, string role, bool accepted)
        {
            Business = business;
            Id = id;
            ReadAppointment = rApp;
            WriteAppointment = wApp;
            ReadCustomerChat = rCC;
            WriteCustomerChat = wCC;
            Accepted = accepted;
            Role = role;
        }

        public bool Create(string userId, string businessId, bool rApp, bool wApp, bool rCC, bool wCC, string role)
        {
            bool success = true;
            string queryString = "INSERT INTO [dbo].[BusinessEmployeeAccess] " +
                "([userId], [businessId], [readAppointment], [writeAppointment], " +
                "[readCustomerChat], [writeCustomerChat], [role])" +
                " VALUES (@UserId, @BusinessId, @RApp, @WApp, @RCC, @WCC, @Role)";
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryString, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@UserId", userId.Trim());
                        cmd.Parameters.AddWithValue("@BusinessId", businessId.Trim());
                        cmd.Parameters.AddWithValue("@RApp", rApp);
                        cmd.Parameters.AddWithValue("@WApp", wApp);
                        cmd.Parameters.AddWithValue("@RCC", rCC);
                        cmd.Parameters.AddWithValue("@WCC", wCC);
                        cmd.Parameters.AddWithValue("@Role", role.Trim());
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
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
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

                        string userId = row["userId"].ToString();
                        string businessId = row["businessId"].ToString();
                        bool readAppointment = Convert.ToBoolean(row["readAppointment"]);
                        bool writeAppointment = Convert.ToBoolean(row["acraCertificate"]);
                        bool readCustomerChat = Convert.ToBoolean(row["readCustomerChat"]);
                        bool writeCustomerChat = Convert.ToBoolean(row["writeCustomerChat"]);
                        string role = row["role"].ToString();
                        bool accepted = Convert.ToBoolean(row["accepted"]);
                        bea = new BusinessEmployeeAccess(userId, businessId, readAppointment, writeAppointment, readCustomerChat, writeCustomerChat, role, accepted);
                    }
                }
            }
            return bea;
        }

        public List<BusinessEmployeeAccess> SelectAllByBusinessId(string businessId)
        {
            List<BusinessEmployeeAccess> employees = new List<BusinessEmployeeAccess>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
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
                    if (rec_cnt == 0)
                    {
                        employees = null;
                    }
                    else
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            string userId = row["userId"].ToString();
                            bool rApp = Convert.ToBoolean(row["readAppointment"]);
                            bool wApp = Convert.ToBoolean(row["writeAppointment"]);
                            bool rCC = Convert.ToBoolean(row["readCustomerChat"]);
                            bool wCC = Convert.ToBoolean(row["writeCustomerChat"]);
                            string role = row["role"].ToString();
                            bool accepted = Convert.ToBoolean(row["accepted"]);

                            string eId = row["userId"].ToString();
                            string eName = row["userName"].ToString();
                            string eEmail = row["userEmail"].ToString();
                            string ePhone = row["userPhone"].ToString();
                            BusinessEmployeeAccess bea = new BusinessEmployeeAccess(userId, businessId, rApp, wApp, rCC, wCC, role, accepted, new BusinessUser(eId, eName, eEmail, ePhone));
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
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(
                        "SELECT * " +
                        "FROM [dbo].[BusinessEmployeeAccess] " +
                        "WHERE [userId] = @UserId;", con))
                    {
                        sda.SelectCommand.Parameters.AddWithValue("@UserId", userId);
                        DataSet ds = new DataSet();
                        sda.Fill(ds);

                        int rec_cnt = ds.Tables[0].Rows.Count;
                        if (rec_cnt == 0)
                        {
                            employeeBeas = null;
                        }
                        else
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {

                                string businessId = row["businessId"].ToString();
                                string id = row["id"].ToString();
                                bool rApp = Convert.ToBoolean(row["readAppointment"]);
                                bool wApp = Convert.ToBoolean(row["writeAppointment"]);
                                bool rCC = Convert.ToBoolean(row["readCustomerChat"]);
                                bool wCC = Convert.ToBoolean(row["writeCustomerChat"]);
                                string role = row["role"].ToString();
                                bool accepted = Convert.ToBoolean(row["accepted"]);

                                BusinessEmployeeAccess bea = new BusinessEmployeeAccess(new Business().SelectOne(businessId), id, rApp, wApp, rCC, wCC, role, accepted);
                                employeeBeas.Add(bea);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                employeeBeas = null;
                throw e;
            }

            return employeeBeas;
        }

        public bool Update(string userId, string businessId, bool rApp, bool wApp, bool rCC, bool wCC, string role)
        {
            bool success = true;
            string queryString = "UPDATE [dbo].[BusinessEmployeeAccess] " +
                "SET [readAppointment] = @RApp, [writeAppointment] = @WApp, " +
                "[readCustomerChat] = @RCC, [writeCustomerChat] = @WCC, " +
                "[role] = @Role WHERE userId = @UserId, businessId = @BusinessId;";
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryString, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@UserId", userId.Trim());
                        cmd.Parameters.AddWithValue("@BusinessId", businessId.Trim());
                        cmd.Parameters.AddWithValue("@RApp", rApp);
                        cmd.Parameters.AddWithValue("@WApp", wApp);
                        cmd.Parameters.AddWithValue("@RCC", rCC);
                        cmd.Parameters.AddWithValue("@WCC", wCC);
                        cmd.Parameters.AddWithValue("@Role", role.Trim());
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
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
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
    }
}