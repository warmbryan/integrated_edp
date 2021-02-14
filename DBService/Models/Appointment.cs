using System.Configuration;
using System.Data.SqlClient;

namespace DBService.Models
{
    class Appointment
    {
        public string aptDate { get; set; }
        public string aptTime { get; set; }
        public string bookDate { get; set; }
        public string bookTime { get; set; }
        public string partySize { get; set; }
        public Appointment() { }
        public Appointment(string aptdate, string apttime, string bookdate, string booktime, string partysize)
        {
            aptDate = aptdate;
            aptTime = apttime;
            bookDate = bookdate;
            bookTime = booktime;
            partySize = partysize;
        }
        public int Insert()
        {
            string MYDBConnectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection myConn = new SqlConnection(MYDBConnectionString);
            string sqlStmt = "INSERT INTO Appointment(aptTime, aptDate, bookTime, bookDate, partySize) VALUES(@paraaptTime, @paraaptDate, @parabookTime, @parabookDate, @parapartySize)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);
            sqlCmd.Parameters.AddWithValue("@paraaptTime", aptDate);
            sqlCmd.Parameters.AddWithValue("@paraaptDate", aptTime);
            sqlCmd.Parameters.AddWithValue("@parabookTime", bookTime);
            sqlCmd.Parameters.AddWithValue("@parabookDate", bookDate);
            sqlCmd.Parameters.AddWithValue("@parapartySize", partySize);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();
            myConn.Close();
            return result;

        }
    }
}
