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
        public Boolean blackListed { get; set; }
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
                using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
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
                                    if (result < 0)
                                    {
                                        throw new OverflowException();
                                    }
                                    result = (Int16)cmdTwo.ExecuteNonQuery();
                                    if (result <0)
                                    {
                                        CustomerClass tmpClass = SelectOneCustomer(this.Email);
                                        FullDeleteCustomer(tmpClass.ID, tmpClass.Email, DateTime.Now.AddDays(-30));
                                        throw new OverflowException();
                                    }
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

        protected Int16 DeleteEncryption(String emailVal)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MySecretDB"].ConnectionString.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteEncryption", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Identity", emailVal);
                    Int16 result;
                    try
                    {
                        conn.Open();
                        result = (Int16)cmd.ExecuteNonQuery();
                        if (result < 0)
                        {
                            throw new OverflowException();
                        }
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

        public CustomerClass VerifyUser(String emailVal)
        {
            using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
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
                                        tmpClass.blackListed = (Boolean)reader["blackListed"];
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
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteCustomer", conn))
                {
                    Int16 result = 0;
                    if (DateTime.Now.CompareTo(deleteDate) > 0)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        try
                        {
                            conn.Open();
                            result = (Int16)cmd.ExecuteNonQuery();
                            if (result < 0)
                            {
                                throw new OverflowException();
                            }
                            result = DeleteEncryption(Email);
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
                    }
                    else
                    {
                        result = -1;
                    }
                    return result;
                }
            }
        }

        public Int16 UpdateCustomer(Guid ID, String PastEmail, String firstName, String lastName, String email, String PhoneNumber, DateTime dateOfBirth)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
            {
                Int16 result = 0;
                CustomerClass tmpClass = new CustomerClass();
                tmpClass = tmpClass.SelectOneCustomer(PastEmail);
                using (SqlCommand cmd = new SqlCommand("UpdateCustomerParticulars", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@PastEmail", PastEmail);
                    cmd.Parameters.AddWithValue("@FirstName", tmpClass.generateEncryptor(firstName));
                    cmd.Parameters.AddWithValue("@LastName", tmpClass.generateEncryptor(lastName));
                    cmd.Parameters.AddWithValue("@NewEmail", email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", tmpClass.generateEncryptor(PhoneNumber));
                    cmd.Parameters.AddWithValue("@BirthDate", tmpClass.generateEncryptor(dateOfBirth.ToString()));
                    try
                    {
                        conn.Open();
                        BlackListClass tmpPower = new BlackListClass();
                        result = tmpPower.UpdateBlacklistEmails(PastEmail, email);
                        if (result < 0)
                        {
                            throw new OverflowException();
                        }
                        result = (Int16)cmd.ExecuteNonQuery();
                        if (result != 1)
                        {
                            throw new OverflowException();
                        }
                        if (email != PastEmail)
                        {
                            result = UpdateCustomerStatus(ID, PastEmail, "emailStatus", false);
                        }
                    }
                    catch (SqlException err)
                    {
                        Console.WriteLine(err);
                        result = -3;
                    }
                    catch (OverflowException err)
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

        public Int16 UpdateCustomerPassword(Guid ID, String PastEmail, String Password)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
            {
                Int16 result = 0;
                CustomerClass tmpClass = new CustomerClass();
                tmpClass = tmpClass.SelectOneCustomer(PastEmail);
                using (SqlCommand cmd = new SqlCommand("UpdateCustomerPassword", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@PastEmail", PastEmail);
                    cmd.Parameters.AddWithValue("@ValueOne", tmpClass.updateHashPassword(Password));
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

        public Int16 UpdateCustomerStatus(Guid ID, String PastEmail, String purpose, Boolean status)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
            {
                Int16 result = 0;
                CustomerClass tmpClass = new CustomerClass();
                if (purpose == "deleteStatus")
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateCustomerDeleteStatus", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@PastEmail", PastEmail);
                        cmd.Parameters.AddWithValue("@ValueOne", status);
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
                else if (purpose == "emailStatus")
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateCustomerEmailVerified", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@PastEmail", PastEmail);
                        cmd.Parameters.AddWithValue("@ValueOne", status);
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
                else if (purpose == "blackListedStatus")
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateCustomerBlackListed", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@PastEmail", PastEmail);
                        cmd.Parameters.AddWithValue("@ValueOne", status);
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
                return result;
            }
        }

        public List<CustomerClass> SelectAllCustomers()
        {
            using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
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
                            tmpClass.deleteDate = (DateTime)row["deleteDate"];
                            tmpClass.emailVerified = (Boolean)row["emailVerified"];
                            tmpClass.delete = (Boolean)row["delete"];
                            tmpClass.blackListed = (Boolean)row["blackListed"];
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
                        connOne.Close();
                        connTwo.Close();
                        return custList;
                    }
                }
            }
        }

        public CustomerClass SelectOneCustomer(String Email)
        {
            using (SqlConnection connOne = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString.ToString()))
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
                                        tmpClass.blackListed = (Boolean)reader["blackListed"];
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
                                try
                                {
                                    return srDecrypt.ReadToEnd();
                                }
                                catch
                                {
                                    return "";
                                }
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
