using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using System.Configuration;

namespace DBService.Models
{
    class Branch
    {
        public string ShopName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }

        public Branch() { }

        public Branch(string shopName, string phoneNumber, string email, string description, string location, string address)
        {
            ShopName = shopName;
            PhoneNumber = phoneNumber;
            Email = email;
            Description = description;
            Location = location;
            Address = address;
        }

        public List<string> SelectDistinctShopName()
        {
            string SQL = "SELECT DISTINCT shopName from Branch";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<string> Names = new List<string>();
                    while (reader.Read())
                    {
                        Names.Add(reader.GetString(0));
                    }
                    conn.Close();
                    return Names;
                }
            }
        }

        public DataSet SelectDistinctLocation()
        {
            string SQL = "SELECT DISTINCT branchLocation from Branch ORDER BY branchLocation";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(SQL, conn))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return ds;
                }
            }
        }

        public DataSet Search(string search, string location)
        {
            if (location != "All")
            {
                string SQL = "SELECT * FROM (SELECT *, 1 as n from Branch where shopName like @paraSearch and branchLocation = @paraLocation" +
                " UNION Select *, 2 as n from Branch where branchAddress like @paraSearch and branchLocation = @paraLocation" +
                " UNION Select *, 3 as n from Branch where description like @paraSearch and branchLocation = @paraLocation) DerivedTable order by n";
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(SQL, conn))
                    {
                        conn.Open();
                        sda.SelectCommand.Parameters.AddWithValue("@paraSearch", "%" + search + "%");
                        sda.SelectCommand.Parameters.AddWithValue("@paraLocation", location);
                        DataSet ds = new DataSet();
                        sda.Fill(ds);
                        conn.Close();
                        return ds;
                    }
                }
            }
            else
            {
                string SQL = "SELECT * FROM (SELECT *, 1 as n from Branch where shopName like @paraSearch" +
                    " UNION Select *, 2 as n from Branch where branchAddress like @paraSearch" +
                    " UNION Select *, 3 as n from Branch where description like @paraSearch) DerivedTable order by n";
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn1"].ConnectionString))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(SQL, conn))
                    {
                        conn.Open();
                        sda.SelectCommand.Parameters.AddWithValue("@paraSearch", "%" + search + "%");
                        DataSet ds = new DataSet();
                        sda.Fill(ds);
                        conn.Close();
                        return ds;
                    }
                }
            }
        }
    }
}
