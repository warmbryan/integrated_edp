using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DBService.Models
{
    public class Search
    {
        public string SearchString { get; set; }
        public Guid CustomerId { get; set; }

        public Search() { }

        public Search(string searchString, Guid customerId)
        {
            SearchString = searchString;
            CustomerId = customerId;
        }

        public int Insert()
        {
            string SQL = "INSERT INTO dbo.SearchHistory (searchString, customerId) VALUES (@searchString, @customerId)";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@searchString", SearchString);
                    cmd.Parameters.AddWithValue("@customerId", CustomerId);
                    cmd.Parameters.AddWithValue("@searchDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    return rowsAffected;
                }
            }
        }

        public DataSet SelectByCustomerId(Guid customerId)
        {
            SqlDataAdapter sda = new SqlDataAdapter();
            string SQL = "SELECT searchString, searchDateTime FROM dbo.SearchHistory WHERE customerId = @customerId ORDER BY searchDateTime DESC";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    conn.Open();
                    DataSet ds = new DataSet();
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    conn.Close();
                    return ds;
                }
            }
        }

        public int HaveDate(string searchString, Guid customerId)
        {
            int id = 0;
            string SQL = "SELECT searchDateTime,id from SearchHistory where searchString = @searchString AND customerId = @customerId";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@searchString", searchString);
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetDateTime(0).Date == DateTime.Now.Date)
                        {
                            id = reader.GetInt32(1);
                        }
                    }
                    conn.Close();
                }
            }
            return id;
        }

        public int Update(int id)
        {
            string SQL = "UPDATE SearchHistory SET searchDateTime = @searchDateTime where id = @id";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@searchDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    return rowsAffected;
                }
            }
        }

        public int Delete(Guid customerId)
        {
            string SQL = "DELETE FROM SearchHistory WHERE customerId = @customerId";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    return rowsAffected;
                }
            }
        }
    }
}
