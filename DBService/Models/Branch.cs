using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DBService.Models
{
    public class Branch
    {
        public Guid Id { get; set; }
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
            string SQL = "SELECT DISTINCT name from Branch UNION SELECT DISTINCT business.name from Branch INNER JOIN business on Branch.BusinessId = business.id";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
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
            string SQL = "SELECT DISTINCT city from Branch ORDER BY city";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(SQL, conn))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return ds;
                }
            }
        }
        public DataSet SelectDistinctCategory()
        {
            string SQL = "SELECT DISTINCT bc.name as catrgory from ((Branch br INNER JOIN Business bu on br.businessId = bu.id) INNER JOIN BusinessCategory bc on bu.categoryId = bc.id) ORDER BY bc.name";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(SQL, conn))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return ds;
                }
            }
        }

        public DataSet Search(string search, string location, string category)
        {
            string[] items = search.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            if (items.Length == 0)
            {
                items = new string[] { "" };
            }
            var parameters = new string[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                parameters[i] = string.Format("@search{0}", i);
                items[i] = "%" + items[i] + "%";
            }

            string SQL = string.Format("SELECT *,(SELECT cast(AVG(rating) as decimal(10,2)) FROM review r WHERE r.branchId = DerivedTable.id) as avgRating FROM " +
                "(SELECT br.id, br.name,br.phone,br.email,br.description,br.country,br.address,br.address2,br.city,bc.name as category,bu.name as businessName from ((Branch br INNER JOIN Business bu on br.businessId = bu.id) INNER JOIN BusinessCategory bc on bu.categoryId = bc.id)" +
                " where br.name like @paraSearch" +
                " or bu.name like @paraSearch" +
                " or br.address like @paraSearch" +
                " or br.description like @paraSearch" +
                " or bc.name like @paraSearch" +
                " or (br.name like {0})" +
                " or (br.description like {1})" +
                ") DerivedTable where (@paraLocation='All' or city = @paraLocation) and (@paraCategory='All' or category = @paraCategory)" +
                " order by case when name like @paraSearch then 1" +
                " when businessName like @paraSearch then 2" +
                " when address like @paraSearch then 3" +
                " when description like @paraSearch then 4" +
                " when category like @paraSearch then 5" +
                " when (name like {2}) then 6 else 7 end", string.Join(" or br.name like ", parameters), string.Join(" or br.description like ", parameters), string.Join(" or name like ", parameters));

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString()))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(SQL, conn))
                {
                    conn.Open();
                    sda.SelectCommand.Parameters.AddWithValue("@paraSearch", "%" + search + "%");
                    sda.SelectCommand.Parameters.AddWithValue("@paraLocation", location);
                    sda.SelectCommand.Parameters.AddWithValue("@paraCategory", category);
                    for (int i = 0; i < items.Length; i++)
                    {
                        sda.SelectCommand.Parameters.AddWithValue(parameters[i], items[i]);
                    }

                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    conn.Close();
                    return ds;
                }
            }
        }
        public Branch SelectById(Guid id)
        {
            string SQL = "SELECT TOP 1 * from Branch where id = @paraId";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@paraId", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Id = Guid.Parse(reader["id"].ToString());
                        ShopName = reader["name"].ToString();
                        PhoneNumber = reader["phone"].ToString();
                        Email = reader["email"].ToString();
                        Description = reader["description"].ToString();
                        Location = reader["city"].ToString();
                        Address = reader["address"].ToString();
                    }
                    conn.Close();
                    return this;
                }
            }
        }
    }
}
