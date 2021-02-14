using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DBService.Models
{
    public class BlackListClass
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public String Reason { get; set; }
        public String CustomerID { get; set; }
        public String CustomerName { get; set; }
        public Boolean Deleted { get; set; }
        public Boolean created { get; set; }

        public BlackListClass()
        {

        }

        public BlackListClass(Int32 duration, String reason, String customerId, String customerName)
        {
            DateTime currentTime = DateTime.Now;
            this.EndedAt = currentTime.AddDays(duration);
            this.Reason = reason;
            this.CustomerID = customerId;
            this.CustomerName = customerName;
            this.created = true;
        }

        public Int16 InsertIntoDatabase()
        {
            if (this.created == true)
            {
                using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString()))
                {
                    using (SqlCommand cmdOne = new SqlCommand("InsertBlackList", connOne))
                    {
                        Int16 result = 0;
                        CustomerClass tmpClass = new CustomerClass();
                        tmpClass = tmpClass.SelectOneCustomer(this.CustomerID);
                        tmpClass.generateEncryptor(this.CustomerName);
                        cmdOne.CommandType = CommandType.StoredProcedure;
                        cmdOne.Parameters.AddWithValue("@endedAt", this.EndedAt);
                        cmdOne.Parameters.AddWithValue("@reason", this.Reason);
                        cmdOne.Parameters.AddWithValue("@customerId", this.CustomerID);
                        cmdOne.Parameters.AddWithValue("@customerName", tmpClass.generateEncryptor(this.CustomerName));
                        try
                        {
                            connOne.Open();
                            result = (Int16)cmdOne.ExecuteNonQuery();
                            if (result != 1)
                            {
                                throw new NullReferenceException();
                            }
                            result = tmpClass.UpdateCustomerStatus(tmpClass.ID, tmpClass.Email, "blackListedStatus", true);
                        }
                        catch (SqlException err)
                        {
                            Console.WriteLine(err);
                            result = -3;
                        }
                        catch (NullReferenceException)
                        {
                            result = -3;
                        }
                        catch (OverflowException)
                        {
                            result = -2;
                        }
                        catch
                        {
                            result = -1;
                        }
                        finally
                        {
                            connOne.Close();
                        }
                        return result;
                    }
                }
            }
            else
            {
                return 0;
            }
        }

        public BlackListClass SelectOneBlacklist(Guid ID, String customerId)
        {
            using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString()))
            {
                using (SqlCommand cmdOne = new SqlCommand("SelectOneBlackList", connOne))
                {
                    BlackListClass tmpClass = new BlackListClass();
                    cmdOne.CommandType = CommandType.StoredProcedure;
                    cmdOne.Parameters.AddWithValue("@ID", ID);
                    cmdOne.Parameters.AddWithValue("@customerId", customerId);
                    CustomerClass powerClass = new CustomerClass();
                    powerClass = powerClass.SelectOneCustomer(customerId);
                    try
                    {
                        connOne.Open();
                        using (SqlDataReader reader = cmdOne.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                tmpClass.CreatedAt = (DateTime)reader["createdAt"];
                                tmpClass.EndedAt = (DateTime)reader["endedAt"];
                                tmpClass.Reason = (String)reader["reason"];
                                tmpClass.CustomerID = (String)reader["customerId"];
                                tmpClass.CustomerName = powerClass.generateDecryptor((String)reader["customerName"]);
                                tmpClass.Deleted = (Boolean)reader["deleted"];
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine(err);
                        return null;
                    }
                    finally
                    {
                        connOne.Close();
                    }
                    return tmpClass;
                }
            }
        }

        public List<BlackListClass> SelectAllBlacklist(String customerId)
        {
            using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString()))
            {
                using (SqlConnection connTwo = new SqlConnection(ConfigurationManager.ConnectionStrings["MySecretDB"].ConnectionString.ToString()))
                {
                    using (SqlDataAdapter cmdOne = new SqlDataAdapter("SelectAllBlackList", connOne))
                    {
                        connOne.Open();
                        connTwo.Open();
                        cmdOne.SelectCommand.CommandType = CommandType.StoredProcedure;
                        cmdOne.SelectCommand.Parameters.AddWithValue("@customerId", customerId);
                        DataSet newDataSet = new DataSet();
                        cmdOne.Fill(newDataSet);
                        List<BlackListClass> blacklistList = new List<BlackListClass>();
                        int rec_cnt = newDataSet.Tables[0].Rows.Count;
                        CustomerClass powerClass = new CustomerClass();
                        powerClass = powerClass.SelectOneCustomer(customerId);
                        for (int i = 0; i < rec_cnt; i++)
                        {
                            DataRow row = newDataSet.Tables[0].Rows[i];
                            BlackListClass tmpClass = new BlackListClass();
                            tmpClass.ID = (Guid)row["ID"];
                            tmpClass.CreatedAt = (DateTime)row["createdAt"];
                            tmpClass.EndedAt = (DateTime)row["endedAt"];
                            tmpClass.Reason = (String)row["reason"];
                            tmpClass.CustomerID = (String)row["customerId"];
                            tmpClass.CustomerName = powerClass.generateDecryptor((String)row["customerName"]);
                            tmpClass.Deleted = (Boolean)row["deleted"];
                            blacklistList.Add(tmpClass);
                        }
                        connOne.Close();
                        connTwo.Close();
                        return blacklistList;
                    }
                }
            }
        }

        public Int16 UpdateBlacklistEmails(String oldId, String customerId)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString()))
            {
                Int16 result = 0;
                using (SqlCommand cmd = new SqlCommand("UpdateBlacklistEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PastEmail", oldId);
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    try
                    {
                        conn.Open();
                        result = (Int16)cmd.ExecuteNonQuery();
                    }
                    catch (SqlException err)
                    {
                        Console.WriteLine(err);
                        result = -3;
                    }
                    catch (OverflowException)
                    {
                        result = -2;
                    }
                    catch
                    {
                        result = -1;
                    }
                    finally
                    {
                        conn.Close();
                    }
                    return result;
                }
            }
        }

        public Int16 UpdateBlacklist(Guid ID, String customerId, Boolean status)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString()))
            {
                Int16 result = 0;
                using (SqlCommand cmd = new SqlCommand("UpdateBlacklistDeleted", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    cmd.Parameters.AddWithValue("@valueOne", status);
                    BlackListClass powerClass = SelectOneBlacklist(ID, customerId);
                    if (DateTime.Now.CompareTo(powerClass.EndedAt) > 0)
                    {
                        try
                        {
                            conn.Open();
                            result = (Int16)cmd.ExecuteNonQuery();
                        }
                        catch (SqlException err)
                        {
                            Console.WriteLine(err);
                            result = -3;
                        }
                        catch (OverflowException)
                        {
                            result = -2;
                        }
                        catch
                        {
                            result = -1;
                        }
                        finally
                        {
                            conn.Close();
                        }
                        return result;
                    }
                    else
                    {
                        return -4;
                    }
                }
            }
        }
    }
}
