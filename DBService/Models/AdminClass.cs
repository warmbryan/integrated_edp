using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace DBService.Models
{
    public class AdminClass
    {
        public Guid ID { get; set; }
        public String AdminName { get; set; }
        public String UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public String Password { get; set; }
        public String Role { get; set; }
        protected String salt { get; set; }
        protected byte[] key { get; set; }
        protected byte[] iv { get; set; }
        protected Boolean created { get; set; }

        public AdminClass()
        {

        }

        public AdminClass(String adminName, String userName, String password, String role)
        {
            this.AdminName = adminName;
            this.UserName = userName;
            hashingAlgorithm(password);
            this.Role = role;
            this.created = true;
        }

        public Int16 InsertIntoDatabase()
        {
            if (this.created == true && this.Role != "Main")
            {
                using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
                {
                    using (SqlConnection connTwo = new SqlConnection(ConfigurationManager.ConnectionStrings["MySecretDB"].ConnectionString.ToString()))
                    {
                        using (SqlCommand cmdOne = new SqlCommand("InsertAdmin", connOne))
                        {
                            using (SqlCommand cmdTwo = new SqlCommand("InsertEncryption", connTwo))
                            {
                                Int16 result = 0;
                                cmdOne.CommandType = CommandType.StoredProcedure;
                                cmdTwo.CommandType = CommandType.StoredProcedure;
                                cmdOne.Parameters.AddWithValue("@AdminName", generateEncryptor(this.AdminName));
                                cmdOne.Parameters.AddWithValue("@UserName", this.UserName);
                                cmdOne.Parameters.AddWithValue("@Password", this.Password);
                                cmdOne.Parameters.AddWithValue("@Role", generateEncryptor(this.Role));
                                cmdTwo.Parameters.AddWithValue("@Salt", this.salt);
                                cmdTwo.Parameters.AddWithValue("@Key", Convert.ToBase64String(this.key));
                                cmdTwo.Parameters.AddWithValue("@Iv", Convert.ToBase64String(this.iv));
                                cmdTwo.Parameters.AddWithValue("@Identity", this.UserName);
                                try
                                {
                                    connOne.Open();
                                    connTwo.Open();
                                    result = (Int16)cmdOne.ExecuteNonQuery();
                                    result = (Int16)cmdTwo.ExecuteNonQuery();
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
                                    connOne.Close();
                                    connTwo.Close();
                                }
                                return result;
                            }
                        }
                    }
                }
            }
            else
            {
                return -1;
            }
        }

        public AdminClass SelectOneAdmin(String userName)
        {
            using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
            {
                using (SqlConnection connTwo = new SqlConnection(ConfigurationManager.ConnectionStrings["MySecretDB"].ConnectionString.ToString()))
                {
                    using (SqlCommand cmdOne = new SqlCommand("SelectOneAdmin", connOne))
                    {
                        using (SqlCommand cmdTwo = new SqlCommand("SelectOneEncryption", connTwo))
                        {
                            AdminClass tmpClass = new AdminClass();
                            cmdOne.CommandType = CommandType.StoredProcedure;
                            cmdTwo.CommandType = CommandType.StoredProcedure;
                            cmdOne.Parameters.AddWithValue("@UserName", userName);
                            cmdTwo.Parameters.AddWithValue("@Identity", userName);
                            try
                            {
                                connOne.Open();
                                using (SqlDataReader reader = cmdOne.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        tmpClass.ID = (Guid)reader["ID"];
                                        tmpClass.AdminName = (String)reader["adminName"];
                                        tmpClass.UserName = (String)reader["userName"];
                                        tmpClass.Password = (String)reader["password"];
                                        tmpClass.Role = (String)reader["role"];
                                    }
                                }
                                connTwo.Open();
                                using (SqlDataReader reader = cmdTwo.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        tmpClass.salt = (String)reader["salt"];
                                        tmpClass.key = Convert.FromBase64String((String)reader["key"]);
                                        tmpClass.iv = Convert.FromBase64String((String)reader["iv"]);
                                        String One = tmpClass.generateEncryptor(tmpClass.AdminName);
                                        String Two = tmpClass.generateEncryptor(tmpClass.Role);
                                        tmpClass.AdminName = tmpClass.generateDecryptor(tmpClass.AdminName);
                                        tmpClass.Role = tmpClass.generateDecryptor(tmpClass.Role);
                                        tmpClass.created = true;
                                    }
                                }
                            }
                            catch (SqlException err)
                            {
                                return null;
                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err);
                                return null;
                            }
                            finally
                            {
                                connOne.Close();
                                connTwo.Close();
                            }
                            return tmpClass;
                        }
                    }
                }
            }
        }

        public List<AdminClass> SelectAllAdmin()
        {
            using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
            {
                using (SqlConnection connTwo = new SqlConnection(ConfigurationManager.ConnectionStrings["MySecretDB"].ConnectionString.ToString()))
                {
                    using (SqlDataAdapter cmdOne = new SqlDataAdapter("SelectAllAdmin", connOne))
                    {
                        connOne.Open();
                        connTwo.Open();
                        cmdOne.SelectCommand.CommandType = CommandType.StoredProcedure;
                        DataSet newDataSet = new DataSet();
                        cmdOne.Fill(newDataSet);
                        List<AdminClass> adminList = new List<AdminClass>();
                        int rec_cnt = newDataSet.Tables[0].Rows.Count;
                        for (int i = 0; i < rec_cnt; i++)
                        {
                            DataRow row = newDataSet.Tables[0].Rows[i];
                            AdminClass tmpClass = new AdminClass();
                            tmpClass.ID = (Guid)row["ID"];
                            tmpClass.UserName = (String)row["userName"];
                            tmpClass.CreatedAt = (DateTime)row["createdAt"];
                            using (SqlCommand cmdTwo = new SqlCommand("SelectOneEncryption", connTwo))
                            {
                                cmdTwo.CommandType = CommandType.StoredProcedure;
                                cmdTwo.Parameters.AddWithValue("@Identity", tmpClass.UserName);
                                using (SqlDataReader reader = cmdTwo.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        tmpClass.salt = (String)reader["salt"];
                                        tmpClass.key = Convert.FromBase64String((String)reader["key"]);
                                        tmpClass.iv = Convert.FromBase64String((String)reader["iv"]);
                                        tmpClass.created = true;
                                    }
                                }
                            }
                            tmpClass.AdminName = (String)row["adminName"];
                            tmpClass.Role = (String)row["role"];
                            tmpClass.salt = "";
                            tmpClass.iv = new byte[0];
                            tmpClass.key = new byte[0];
                            adminList.Add(tmpClass);
                        }
                        connOne.Close();
                        connTwo.Close();
                        return adminList;
                    }
                }
            }
        }

        public Int16 UpdateAdminPassword(String Username, String Password)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
            {
                Int16 result = 0;
                AdminClass tmpClass = new AdminClass();
                tmpClass = tmpClass.SelectOneAdmin(Username);
                using (SqlCommand cmd = new SqlCommand("UpdateAdminPassword", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", Username);
                    cmd.Parameters.AddWithValue("@Password", tmpClass.updateHashPassword(Password));
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

        public Int16 UpdateAdminRole(String Username, String Role)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
            {
                Int16 result = 0;
                AdminClass tmpClass = new AdminClass();
                tmpClass = tmpClass.SelectOneAdmin(Username);
                using (SqlCommand cmd = new SqlCommand("UpdateAdminRole", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", Username);
                    cmd.Parameters.AddWithValue("@Role", tmpClass.generateEncryptor(Role));
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

        public Int16 DeleteAdmin(String Username)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
            {
                Int16 result = 0;
                AdminClass tmpClass = new AdminClass();
                using (SqlCommand cmd = new SqlCommand("DeleteAdmin", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", Username);
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

        protected void hashingAlgorithm(String password)
        {
            Int16 rounds = 15;
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[16];
            rng.GetBytes(saltByte);
            String salt = Convert.ToBase64String(saltByte);
            this.salt = salt;
            this.Password = password;
            this.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(this.Password + this.salt, workFactor: rounds);
        }

        protected String updateHashPassword(String Password)
        {
            Int16 rounds = 15;
            return BCrypt.Net.BCrypt.EnhancedHashPassword(Password + this.salt, workFactor: rounds);
        }

        public Boolean decryptHashPassword(String Password)
        {
            Boolean result;
            try
            {
                result = BCrypt.Net.BCrypt.EnhancedVerify(Password + this.salt, this.Password);
                if (result)
                {
                    this.Password = "";
                    this.salt = "";
                    this.iv = new byte[0];
                    this.key = new byte[0];
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        protected String generateEncryptor(String value)
        {
            using (Aes myAes = Aes.Create())
            {
                if (this.key != null && this.key.Length > 0 && this.iv != null && this.iv.Length > 0)
                {
                    myAes.Key = this.key;
                    myAes.IV = this.iv;
                }
                else
                {
                    this.key = myAes.Key;
                    this.iv = myAes.IV;
                }
                ICryptoTransform encryptor = myAes.CreateEncryptor(myAes.Key, myAes.IV);
                using (MemoryStream msEncrypt = new MemoryStream())

                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(value);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        protected String generateDecryptor(String value)
        {
            using (Aes myAes = Aes.Create())
            {
                if (this.key != null && this.key.Length > 0 && this.iv != null && this.iv.Length > 0)
                {
                    myAes.Key = this.key;
                    myAes.IV = this.iv;
                    byte[] creditCard;
                    try
                    {
                        creditCard = Convert.FromBase64String(value);
                    }
                    catch
                    {
                        return "";
                    }
                    ICryptoTransform decryptor = myAes.CreateDecryptor(myAes.Key, myAes.IV);
                    using (MemoryStream msDecrypt = new MemoryStream(creditCard))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
