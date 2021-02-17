using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DBService.Models
{
    public class BusinessUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        protected String salt { get; set; }
        protected byte[] key { get; set; }
        protected byte[] iv { get; set; }

        public BusinessUser() { }

        public BusinessUser(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public BusinessUser(string name, string email, string password, string phone)
        {
            Name = name;
            Email = email;
            hashingAlgorithm(password);
            Phone = phone;
        }

        public bool Create()
        {
            Int16 result = 0;
            using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlConnection connTwo = new SqlConnection(ConfigurationManager.ConnectionStrings["MySecretDB"].ConnectionString.ToString()))
                {
                    using (SqlCommand cmdOne = new SqlCommand("InsertBusiness", connOne))
                    {
                        using (SqlCommand cmdTwo = new SqlCommand("InsertEncryption", connTwo))
                        {
                            cmdOne.CommandType = CommandType.StoredProcedure;
                            cmdTwo.CommandType = CommandType.StoredProcedure;
                            cmdOne.Parameters.AddWithValue("@Name", generateEncryptor(this.Name));
                            cmdOne.Parameters.AddWithValue("@Email", this.Email);
                            cmdOne.Parameters.AddWithValue("@Password", this.Password);
                            cmdOne.Parameters.AddWithValue("@Phone", generateEncryptor(this.Phone));
                            cmdTwo.Parameters.AddWithValue("@Salt", this.salt);
                            cmdTwo.Parameters.AddWithValue("@Key", Convert.ToBase64String(this.key));
                            cmdTwo.Parameters.AddWithValue("@Iv", Convert.ToBase64String(this.iv));
                            cmdTwo.Parameters.AddWithValue("@Identity", this.Email);
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
                        }
                    }
                }
                return result == 1;
            }
        }

        public BusinessUser SelectOneByUserId(string userId)
        {
            using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
            {
                using (SqlConnection connTwo = new SqlConnection(ConfigurationManager.ConnectionStrings["MySecretDB"].ConnectionString.ToString()))
                {
                    using (SqlCommand cmdOne = new SqlCommand("SelectOneBusinessById", connOne))
                    {
                        using (SqlCommand cmdTwo = new SqlCommand("SelectOneEncryption", connTwo))
                        {
                            BusinessUser bUser = new BusinessUser();
                            cmdOne.CommandType = CommandType.StoredProcedure;
                            cmdOne.Parameters.AddWithValue("@UserId", userId);

                            string cipherName = "";
                            string cipherPhone = "";

                            try
                            {
                                connOne.Open();

                                using (SqlDataReader reader = cmdOne.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        bUser.Id = userId.Trim();
                                        cipherName = reader["name"].ToString();
                                        bUser.Email = reader["email"].ToString();
                                        cipherPhone = reader["phone"].ToString();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                                return null;
                            }
                            finally
                            {
                                connOne.Close();
                            }

                            cmdTwo.CommandType = CommandType.StoredProcedure;
                            cmdTwo.Parameters.AddWithValue("@Identity", bUser.Email);
                            try
                            {
                                connTwo.Open();
                                
                                using (SqlDataReader reader = cmdTwo.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        bUser.salt = (String)reader["salt"];
                                        bUser.key = Convert.FromBase64String((String)reader["key"]);
                                        bUser.iv = Convert.FromBase64String((String)reader["iv"]);
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
                                connTwo.Close();
                            }

                            bUser.Name = bUser.generateDecryptor(cipherName);
                            bUser.Phone = bUser.generateDecryptor(cipherPhone);

                            return bUser;
                        }
                    }
                }
            }
        }

        public List<BusinessUser> SelectAllBusiness()
        {
            using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
            {
                using (SqlConnection connTwo = new SqlConnection(ConfigurationManager.ConnectionStrings["MySecretDB"].ConnectionString.ToString()))
                {
                    using (SqlDataAdapter cmdOne = new SqlDataAdapter("SelectAllBusiness", connOne))
                    {
                        connOne.Open();
                        connTwo.Open();
                        cmdOne.SelectCommand.CommandType = CommandType.StoredProcedure;
                        DataSet newDataSet = new DataSet();
                        cmdOne.Fill(newDataSet);
                        List<BusinessUser> custList = new List<BusinessUser>();
                        int rec_cnt = newDataSet.Tables[0].Rows.Count;
                        for (int i = 0; i < rec_cnt; i++)
                        {
                            DataRow row = newDataSet.Tables[0].Rows[i];
                            BusinessUser tmpClass = new BusinessUser();
                            tmpClass.Name = (String)row["name"];
                            tmpClass.Email = (String)row["email"];
                            tmpClass.Phone = (String)row["phone"];
                            using (SqlCommand cmdTwo = new SqlCommand("SelectOneEncryption", connTwo))
                            {
                                cmdTwo.CommandType = CommandType.StoredProcedure;
                                cmdTwo.Parameters.AddWithValue("@Identity", tmpClass.Email);
                                using (SqlDataReader reader = cmdTwo.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        tmpClass.salt = (String)reader["salt"];
                                        tmpClass.key = Convert.FromBase64String((String)reader["key"]);
                                        tmpClass.iv = Convert.FromBase64String((String)reader["iv"]);
                                    }
                                }
                            }
                            tmpClass.Name = tmpClass.generateDecryptor((String)row["name"]);
                            tmpClass.Phone = tmpClass.generateDecryptor((String)row["phone"]);
                            tmpClass.salt = "";
                            tmpClass.iv = new byte[0];
                            tmpClass.key = new byte[0];
                            custList.Add(tmpClass);
                        }
                        connOne.Close();
                        connTwo.Close();
                        return custList;
                    }
                }
            }
        }

        public BusinessUser SelectOneByEmail(string email)
        {
            using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
            {
                using (SqlConnection connTwo = new SqlConnection(ConfigurationManager.ConnectionStrings["MySecretDB"].ConnectionString.ToString()))
                {
                    using (SqlCommand cmdOne = new SqlCommand("SelectOneBusinessByEmail", connOne))
                    {
                        using (SqlCommand cmdTwo = new SqlCommand("SelectOneEncryption", connTwo))
                        {
                            BusinessUser bUser = new BusinessUser();
                            cmdOne.CommandType = CommandType.StoredProcedure;
                            cmdTwo.CommandType = CommandType.StoredProcedure;
                            cmdOne.Parameters.AddWithValue("@Email", email);
                            cmdTwo.Parameters.AddWithValue("@Identity", email);
                            try
                            {
                                connOne.Open();
                                connTwo.Open();
                                using (SqlDataReader reader = cmdTwo.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        bUser.salt = (String)reader["salt"];
                                        bUser.key = Convert.FromBase64String((String)reader["key"]);
                                        bUser.iv = Convert.FromBase64String((String)reader["iv"]);
                                    }
                                }
                                using (SqlDataReader reader = cmdOne.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        bUser.Id = reader["id"].ToString();
                                        bUser.Password = reader["password"].ToString();
                                        bUser.Name = bUser.generateDecryptor(reader["name"].ToString());
                                        bUser.Email = email;
                                        bUser.Phone = bUser.generateDecryptor(reader["phone"].ToString());
                                    }
                                }
                            }
                            catch (Exception err)
                            {
                                throw err;
                                Console.WriteLine(err);
                                return null;
                            }
                            finally
                            {
                                connOne.Close();
                                connTwo.Close();
                            }
                            return bUser;
                        }
                    }
                }
            }
        }

        public bool Exists(string email)
        {
            using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
            {
                using (SqlCommand cmdOne = new SqlCommand("VerifyBusinessUser", connOne))
                {
                    Boolean exists = false;
                    cmdOne.CommandType = CommandType.StoredProcedure;
                    cmdOne.Parameters.AddWithValue("@Email", email);
                    try
                    {
                        connOne.Open();
                        using (SqlDataReader reader = cmdOne.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                exists = true;
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine(err);
                        exists = false;
                    }
                    finally
                    {
                        connOne.Close();
                    }
                    return exists;
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

        public String generateEncryptor(String value)
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

        public String generateDecryptor(String value)
        {
            using (Aes myAes = Aes.Create())
            {
                if (this.key != null && this.key.Length > 0 && this.iv != null && this.iv.Length > 0)
                {
                    myAes.Key = this.key;
                    myAes.IV = this.iv;
                    byte[] creditCard = Convert.FromBase64String(value);
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
