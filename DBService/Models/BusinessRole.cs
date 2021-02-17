using System;
using System.Collections.Generic;

using System.Configuration;

using System.Data;
using System.Data.SqlClient;

namespace DBService.Models
{
    public class BusinessRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BusinessId { get; set; }
        public bool Deleted { get; set; }
        public int EmployeeCount { get; set; }

        public BusinessRole() { }

        public BusinessRole(string id, string name, string businessId, bool deleted)
        {
            Id = id;
            Name = name;
            BusinessId = businessId;
            Deleted = deleted;
        }

        public BusinessRole GetBusinessRole(string brId)
        {
            BusinessRole br = null;

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[BusinessRole] WHERE [id] = @Id", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Id", brId);

                        con.Open();

                        SqlDataReader sdr = cmd.ExecuteReader();

                        while (sdr.Read())
                        {
                            string name = sdr["name"].ToString();
                            string id = sdr["id"].ToString();
                            bool deleted = Convert.ToBoolean(sdr["deleted"]);
                            br = new BusinessRole(id, name, brId, deleted);
                            br.EmployeeCount = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // log?
                Console.WriteLine(ex.Message, ex.ToString());
                return br;
            }

            return br;
        }

        public List<BusinessRole> GetBusinessRoles(string businessId)
        {
            List<BusinessRole> businessRoles = new List<BusinessRole>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter("SELECT br.id, br.[name], br.businessId, br.deleted, count(bea.id) employeeCount FROM BusinessRole br LEFT JOIN BusinessEmployeeAccess bea ON br.id = bea.roleId WHERE br.businessId = @BusinessId AND br.deleted = 0 GROUP BY br.id, br.[name], br.businessId, br.deleted;", con))
                    {
                        DataSet ds = new DataSet();

                        sda.SelectCommand.CommandType = CommandType.Text;
                        sda.SelectCommand.Parameters.AddWithValue("@BusinessId", businessId);

                        sda.Fill(ds);
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            string id = row["id"].ToString();
                            string name = row["name"].ToString();
                            bool deleted = (bool)row["deleted"];

                            int employeeCount = (int)row["employeeCount"];

                            BusinessRole br = new BusinessRole(id, name, businessId, deleted);
                            br.EmployeeCount = employeeCount;

                            businessRoles.Add(br);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.ToString());
                // log?
            }
            return businessRoles;
        }

        public BusinessRole CreateBusinessRole(string name, string businessId)
        {
            BusinessRole br = null;
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[BusinessRole] (name, businessId) OUTPUT Inserted.id VALUES (@Name, @BusinessId);", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Name", name.Trim());
                        cmd.Parameters.AddWithValue("@BusinessId", businessId.Trim());

                        con.Open();

                        Guid businessRoleId = (Guid)cmd.ExecuteScalar();

                        if (businessRoleId != null)
                        {
                            br = new BusinessRole(businessRoleId.ToString(), name, businessId, false);
                            br.EmployeeCount = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // log?
                Console.WriteLine(ex.Message, ex.ToString());
                return br;
            }

            return br;
        }

        public bool UpdateBusinessRole(string businessRoleId, string name)
        {
            bool success = false;
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE [dbo].[BusinessRole] SET [name] = @Name WHERE [id] = @Id AND [deleted] = 0;", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Id", businessRoleId);
                        cmd.Parameters.AddWithValue("@Name", name);

                        con.Open();

                        success = (cmd.ExecuteNonQuery() > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                // log?
                Console.WriteLine(ex.Message, ex.ToString());
                return success;
            }

            return success;
        }

        public bool DeleteBusinessRole(string businessRoleId)
        {
            bool success = false;
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE [dbo].[BusinessRole] SET [deleted] = 1 WHERE [id] = @Id;", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Id", businessRoleId);

                        con.Open();

                        success = (cmd.ExecuteNonQuery() > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                // log?
                // throw ex;
                Console.WriteLine(ex.Message, ex.ToString());
                return success;
            }

            return success;
        }

        public bool CheckBusinessRoleExists(string businessRoleId, string businessId)
        {
            bool exists = false;

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [dbo].[BusinessRole] WHERE [id] = @BusinessRoleId AND [businessId] = @BusinessId;", con))
                    {
                        sda.SelectCommand.CommandType = CommandType.Text;
                        sda.SelectCommand.Parameters.AddWithValue("@BusinessRoleId", businessRoleId);
                        sda.SelectCommand.Parameters.AddWithValue("@BusinessId", businessId);

                        DataSet ds = new DataSet();
                        sda.Fill(ds);

                        exists = (ds.Tables[0].Rows.Count > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                // log?
                Console.WriteLine(ex.Message, ex.ToString());
                return exists;
            }

            return exists;
        }
    }
}
