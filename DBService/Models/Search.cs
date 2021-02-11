using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DBService.Models
{
    class Search
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
            string SQL = "insertSearch";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@searchString", SearchString);
                    cmd.Parameters.AddWithValue("@searchDateTime", DateTime.Now.ToString());
                    cmd.Parameters.AddWithValue("@customerId", CustomerId);
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
            string SQL = "selectSearchByCustomerId";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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
            string SQL = "SELECT searchDateTime,id from Search where searchString = @searchString AND customerId = @customerId";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
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
            string SQL = "UPDATE Search SET searchDateTime = @searchDateTime where id = @id";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@searchDateTime", DateTime.Now.ToString());
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    return rowsAffected;
                }
            }
        }

        public int Delete(Guid customerId)
        {
            string SQL = "DELETE FROM Search WHERE customerId = @customerId";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
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
