using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace DBService.Models
{
    public class CustomerClass
    {
        public Guid ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        protected String Password { get; set; }
        public String PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Boolean emailVerified { get; set; }
        public Boolean delete { get; set; }
        public DateTime deleteDate { get; set; }
        protected Boolean created { get; set; }
        protected String salt { get; set; }
        protected byte[] key { get; set; }
        protected byte[] iv { get; set; }

        public CustomerClass()
        {
            this.created = false;
        }

        public CustomerClass(String firstName, String lastName, String email, String password, String phoneNumber, DateTime dateOfBirth)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.DateOfBirth = dateOfBirth;
            hashingAlgorithm(password);
            this.created = true;
        }

        public Int16 InsertIntoDatabase()
        {
            if (this.created == true)
            {
                using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
                {
                    using (SqlConnection connTwo = new SqlConnection(ConfigurationManager.ConnectionStrings["MySecretDB"].ConnectionString.ToString()))
                    {
                        using (SqlCommand cmdOne = new SqlCommand("InsertCustomer", connOne))
                        {
                            using (SqlCommand cmdTwo = new SqlCommand("InsertEncryption", connTwo))
                            {
                                Int16 result = 0;
                                cmdOne.CommandType = CommandType.StoredProcedure;
                                cmdTwo.CommandType = CommandType.StoredProcedure;
                                cmdOne.Parameters.AddWithValue("@FirstName", generateEncryptor(this.FirstName));
                                cmdOne.Parameters.AddWithValue("@LastName", generateEncryptor(this.LastName));
                                cmdOne.Parameters.AddWithValue("@PhoneNumber", generateEncryptor(this.PhoneNumber));
                                cmdOne.Parameters.AddWithValue("@DateOfBirth", generateEncryptor(this.DateOfBirth.ToString()));
                                cmdOne.Parameters.AddWithValue("@Email", this.Email);
                                cmdOne.Parameters.AddWithValue("@Password", this.Password);
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
                                return result;
                            }
                        }
                    }
                }
            }
            else
            {
                return 0;
            }
        }

        public CustomerClass VerifyUser(String emailVal)
        {
            using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
            {
                using (SqlConnection connTwo = new SqlConnection(ConfigurationManager.ConnectionStrings["MySecretDB"].ConnectionString.ToString()))
                {
                    using (SqlCommand cmdOne = new SqlCommand("SelectOneCustomerVerification", connOne))
                    {
                        using (SqlCommand cmdTwo = new SqlCommand("SelectOneEncryption", connTwo))
                        {
                            CustomerClass tmpClass = new CustomerClass();
                            cmdOne.CommandType = CommandType.StoredProcedure;
                            cmdTwo.CommandType = CommandType.StoredProcedure;
                            cmdOne.Parameters.AddWithValue("@Email", emailVal);
                            cmdTwo.Parameters.AddWithValue("@Identity", emailVal);
                            try
                            {
                                connOne.Open();
                                using (SqlDataReader reader = cmdOne.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        tmpClass.ID = (Guid)reader["id"];
                                        tmpClass.Email = (String)reader["email"];
                                        tmpClass.emailVerified = (Boolean)reader["emailVerified"];
                                        tmpClass.Password = (String)reader["password"];
                                        tmpClass.delete = (Boolean)reader["delete"];
                                        tmpClass.deleteDate = (DateTime)reader["deleteDate"];
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
                                        tmpClass.created = true;
                                    }
                                }
                            }
                            catch (SqlException err)
                            {
                                return null;
                            }
                            catch
                            {
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

        public Int16 FullDeleteCustomer(Guid ID, String Email, DateTime deleteDate)
        {
            using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
            {
                using (SqlConnection connTwo = new SqlConnection(ConfigurationManager.ConnectionStrings["MySecretDB"].ConnectionString.ToString()))
                {
                    using (SqlCommand cmdOne = new SqlCommand("DeleteCustomer", connOne))
                    {
                        using (SqlCommand cmdTwo = new SqlCommand("DeleteEncryption", connTwo))
                        {
                            Int16 timeDiff = (Int16)(DateTime.Now - deleteDate).Days;
                            Int16 result = 0;
                            if (timeDiff >= 30)
                            {
                                cmdOne.CommandType = CommandType.StoredProcedure;
                                cmdTwo.CommandType = CommandType.StoredProcedure;
                                cmdOne.Parameters.AddWithValue("@ID", ID);
                                cmdOne.Parameters.AddWithValue("@Email", Email);
                                cmdTwo.Parameters.AddWithValue("@Identity", Email);
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
                            else
                            {
                                result = -1;
                            }
                            return result;
                        }
                    }
                }
            }
        }

        public Int16 UpdateCustomer(Guid ID, String PastEmail, String purpose, Object valueOne, Object valueTwo)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
            {
                Int16 result = 0;
                CustomerClass tmpClass = new CustomerClass();
                tmpClass = tmpClass.SelectOneCustomer(ID, PastEmail);
                purpose = purpose.ToLower().Trim().Replace(" ", "");
                if (purpose == "name")
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateCustomerName", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@PastEmail", PastEmail);
                        cmd.Parameters.AddWithValue("@ValueOne", tmpClass.generateEncryptor((String)valueOne));
                        cmd.Parameters.AddWithValue("@ValueTwo", tmpClass.generateEncryptor((String)valueTwo));
                        try
                        {
                            conn.Open();
                            result = (Int16)cmd.ExecuteNonQuery();
                        }
                        catch (SqlException)
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
                            conn.Close();
                        }
                    }
                }
                else if (purpose == "email")
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateCustomerEmail", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@PastEmail", PastEmail);
                        cmd.Parameters.AddWithValue("@ValueOne", (String)valueOne);
                        try
                        {
                            conn.Open();
                            result = (Int16)cmd.ExecuteNonQuery();
                        }
                        catch (SqlException)
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
                            conn.Close();
                        }
                    }
                }
                else if (purpose == "phonenumber")
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateCustomerPhoneNumber", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@PastEmail", PastEmail);
                        cmd.Parameters.AddWithValue("@ValueOne", tmpClass.generateEncryptor((String)valueOne));
                        try
                        {
                            conn.Open();
                            result = (Int16)cmd.ExecuteNonQuery();
                        }
                        catch (SqlException)
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
                            conn.Close();
                        }
                    }
                }
                else if (purpose == "date")
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateCustomerDateOfBirth", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@PastEmail", PastEmail);
                        cmd.Parameters.AddWithValue("@ValueOne", tmpClass.generateEncryptor((String)valueOne));
                        try
                        {
                            conn.Open();
                            result = (Int16)cmd.ExecuteNonQuery();
                        }
                        catch (SqlException)
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
                            conn.Close();
                        }
                    }
                }
                else if (purpose == "password")
                {

                    using (SqlConnection connTwo = new SqlConnection(ConfigurationManager.ConnectionStrings["MySecretDB"].ConnectionString.ToString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("UpdateCustomerPassword", conn))
                        {
                            using (SqlCommand cmdTwo = new SqlCommand("UpdateEncryptionHash", connTwo))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmdTwo.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@ID", ID);
                                cmd.Parameters.AddWithValue("@PastEmail", PastEmail);
                                tmpClass.hashingAlgorithm((String)valueOne);
                                cmd.Parameters.AddWithValue("@ValueOne", tmpClass.Password);
                                cmdTwo.Parameters.AddWithValue("@Salt", tmpClass.salt);
                                cmdTwo.Parameters.AddWithValue("@Identity", tmpClass.Email);
                                try
                                {
                                    conn.Open();
                                    connTwo.Open();
                                    result = (Int16)cmd.ExecuteNonQuery();
                                    result = (Int16)cmdTwo.ExecuteNonQuery();
                                }
                                catch (SqlException)
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
                                    conn.Close();
                                    connTwo.Close();
                                }
                            }
                        }
                    }
                }
                else if (purpose == "verifyemail")
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateCustomerEmailVerified", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@PastEmail", PastEmail);
                        cmd.Parameters.AddWithValue("@ValueOne", (Boolean)valueOne);
                        try
                        {
                            conn.Open();
                            result = (Int16)cmd.ExecuteNonQuery();
                        }
                        catch (SqlException)
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
                            conn.Close();
                        }
                    }
                }
                else if (purpose == "deletestatus")
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateCustomerDeleteStatus", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@PastEmail", PastEmail);
                        cmd.Parameters.AddWithValue("@ValueOne", (Boolean)valueOne);
                        try
                        {
                            conn.Open();
                            result = (Int16)cmd.ExecuteNonQuery();
                        }
                        catch (SqlException)
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
                            conn.Close();
                        }
                    }
                }
                return result;
            }
        }

        public List<CustomerClass> SelectAllCustomers()
        {
            using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
            {
                using (SqlConnection connTwo = new SqlConnection(ConfigurationManager.ConnectionStrings["MySecretDB"].ConnectionString.ToString()))
                {
                    using (SqlDataAdapter cmdOne = new SqlDataAdapter("SelectAllCustomer", connOne))
                    {
                        connOne.Open();
                        connTwo.Open();
                        cmdOne.SelectCommand.CommandType = CommandType.StoredProcedure;
                        DataSet newDataSet = new DataSet();
                        cmdOne.Fill(newDataSet);
                        List<CustomerClass> custList = new List<CustomerClass>();
                        int rec_cnt = newDataSet.Tables[0].Rows.Count;
                        for (int i = 0; i < rec_cnt; i++)
                        {
                            DataRow row = newDataSet.Tables[0].Rows[i];
                            CustomerClass tmpClass = new CustomerClass();
                            tmpClass.ID = (Guid)row["id"];
                            tmpClass.Email = (String)row["email"];
                            tmpClass.deleteDate = DateTime.Parse((String)row["deleteDate"]);
                            tmpClass.emailVerified = (Boolean)row["emailVerified"];
                            tmpClass.delete = (Boolean)row["delete"];
                            using (SqlCommand cmdTwo = new SqlCommand("SelectOneEncryption", connTwo))
                            {
                                cmdTwo.Parameters.AddWithValue("@ForeignKeyId", this.ID);
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
                            tmpClass.FirstName = tmpClass.generateDecryptor((String)row["firstName"]);
                            tmpClass.LastName = tmpClass.generateDecryptor((String)row["lastName"]);
                            tmpClass.PhoneNumber = tmpClass.generateDecryptor((String)row["phoneNumber"]);
                            tmpClass.DateOfBirth = DateTime.Parse(tmpClass.generateDecryptor((String)row["dateOfBirth"]));
                            tmpClass.salt = "";
                            tmpClass.iv = new byte[0];
                            tmpClass.key = new byte[0];
                            custList.Add(tmpClass);
                        }
                        return custList;
                    }
                }
            }
        }

        public CustomerClass SelectOneCustomer(Guid ID, String Email)
        {
            using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
            {
                using (SqlConnection connTwo = new SqlConnection(ConfigurationManager.ConnectionStrings["MySecretDB"].ConnectionString.ToString()))
                {
                    using (SqlCommand cmdOne = new SqlCommand("SelectOneCustomer", connOne))
                    {
                        using (SqlCommand cmdTwo = new SqlCommand("SelectOneEncryption", connTwo))
                        {
                            CustomerClass tmpClass = new CustomerClass();
                            cmdOne.CommandType = CommandType.StoredProcedure;
                            cmdTwo.CommandType = CommandType.StoredProcedure;
                            cmdOne.Parameters.AddWithValue("@ID", ID);
                            cmdOne.Parameters.AddWithValue("@Email", Email);
                            cmdTwo.Parameters.AddWithValue("@Identity", Email);
                            try
                            {
                                connOne.Open();
                                connTwo.Open();
                                String tmpFirstName = "";
                                String tmpLastName = "";
                                String tmpPhoneNumber = "";
                                String tmpDateOfBirth = "";
                                using (SqlDataReader reader = cmdOne.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        tmpClass.ID = (Guid)reader["id"];
                                        tmpClass.Email = (String)reader["email"];
                                        tmpClass.emailVerified = (Boolean)reader["emailVerified"];
                                        tmpClass.deleteDate = (DateTime)reader["deleteDate"];
                                        tmpClass.delete = (Boolean)reader["delete"];
                                        tmpFirstName = (String)reader["firstName"];
                                        tmpLastName = (String)reader["lastName"];
                                        tmpPhoneNumber = (String)reader["phoneNumber"];
                                        tmpDateOfBirth = (String)reader["dateOfBirth"];
                                    }
                                }
                                using (SqlDataReader reader = cmdTwo.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        tmpClass.salt = (String)reader["salt"];
                                        tmpClass.key = Convert.FromBase64String((String)reader["key"]);
                                        tmpClass.iv = Convert.FromBase64String((String)reader["iv"]);
                                        tmpClass.FirstName = tmpClass.generateDecryptor(tmpFirstName);
                                        tmpClass.LastName = tmpClass.generateDecryptor(tmpLastName);
                                        tmpClass.PhoneNumber = tmpClass.generateDecryptor(tmpPhoneNumber);
                                        tmpClass.DateOfBirth = DateTime.Parse(tmpClass.generateDecryptor(tmpDateOfBirth));
                                        tmpClass.created = true;
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
                                connTwo.Close();
                            }
                            return tmpClass;
                        }
                    }
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
