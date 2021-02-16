using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DBService.Models
{
    public class Appointment
    {
        public string aptDate { get; set; }
        public string aptTime { get; set; }
        public string bookDate { get; set; }
        public string bookTime { get; set; }
        public string partySize { get; set; }
        public DateTime aptDateTime { get; set; }
        public string customerId { get; set; }
        public string branchId { get; set; }
        public string appointmentSettingId { get; set; }
        public Appointment() { }
        public Appointment(string aptdate, string apttime, string bookdate, string booktime, string partysize, DateTime aptdatetime, string customerid, string branchid, string appointmentsettingid)
        {
            aptDate = aptdate;
            aptTime = apttime;
            bookDate = bookdate;
            bookTime = booktime;
            partySize = partysize;
            aptDateTime = aptdatetime;
            customerId = customerid;
            branchId = branchid;
            appointmentSettingId = appointmentsettingid;
        }
        public int Insert()
        {
            string MYDBConnectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection myConn = new SqlConnection(MYDBConnectionString);
            string sqlStmt = "INSERT INTO Appointment(aptTime, aptDate, bookTime, bookDate, partySize,arrived,aptDateTime,customerId,branchId,appointmentSettingId) VALUES(@paraaptTime, @paraaptDate, @parabookTime, @parabookDate, @parapartySize,@paraarrived,@aptDateTime,@customerId,@branchId,@appointmentSettingId)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);
            sqlCmd.Parameters.AddWithValue("@paraaptTime", aptDate);
            sqlCmd.Parameters.AddWithValue("@paraaptDate", aptTime);
            sqlCmd.Parameters.AddWithValue("@parabookTime", bookTime);
            sqlCmd.Parameters.AddWithValue("@parabookDate", bookDate);
            sqlCmd.Parameters.AddWithValue("@parapartySize", partySize);
            sqlCmd.Parameters.AddWithValue("@paraarrived", 0);
            sqlCmd.Parameters.AddWithValue("@aptDateTime", aptDateTime);
            sqlCmd.Parameters.AddWithValue("@customerId", aptDateTime);
            sqlCmd.Parameters.AddWithValue("@branchId", aptDateTime);
            sqlCmd.Parameters.AddWithValue("@appointmentSettingId", aptDateTime);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();
            myConn.Close();
            return result;

        }
        public int Modify(string userid)
        {
            string MYDBConnectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection myConn = new SqlConnection(MYDBConnectionString);
            string sqlStmt = "UPDATE Appointment SET aptTime=@aptTime,aptDate=@aptDate,bookTime=@bookTime,bookDate=@bookDate,partySize=@partySize,aptDateTime=@aptDateTime WHERE id=@ID";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);
            sqlCmd.Parameters.AddWithValue("@ID", userid);
            sqlCmd.Parameters.AddWithValue("@aptTime", aptDate);
            sqlCmd.Parameters.AddWithValue("@aptDate", aptTime);
            sqlCmd.Parameters.AddWithValue("@bookTime", bookTime);
            sqlCmd.Parameters.AddWithValue("@bookDate", bookDate);
            sqlCmd.Parameters.AddWithValue("@partySize", partySize);
            sqlCmd.Parameters.AddWithValue("@aptDateTime", aptDateTime);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();
            myConn.Close();
            return result;
        }
        public List<Appointment> SelectByTodayDate()
        {
            string MYDBConnectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection myConn = new SqlConnection(MYDBConnectionString);
            string sqlStmt = "Select * from Appointment WHERE aptDate=@paraaptDate AND branchdId=@branchId";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            string todaydate = (DateTime.Now.ToShortDateString());
            da.SelectCommand.Parameters.AddWithValue("@paraaptDate", todaydate);
            da.SelectCommand.Parameters.AddWithValue("@branchId", branchId);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<Appointment> aptList = new List<Appointment>();
            int rec_cnt = ds.Tables[0].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables[0].Rows[0];  // Sql command returns only one record
                string aptTime = row["aptTime"].ToString();
                string aptDate = row["aptDate"].ToString();
                string partySize = row["partySize"].ToString();
                string bookTime = row["bookTime"].ToString();
                string bookDate = row["bookDate"].ToString();
                DateTime aptDateTime = (DateTime)row["aptDateTime"];
                string customerId = row["customerId"].ToString();
                string branchId = row["branchId"].ToString();
                string appointmentSettingId = row["appointmentSettingId"].ToString();
                Appointment apt = new Appointment(aptDate, aptTime, bookDate, bookTime, partySize, aptDateTime, customerId, branchId, appointmentSettingId);
                aptList.Add(apt);
            }
            return aptList;
        }
        public List<Appointment> SelectByTodayDateAscend(string todaydate)
        {
            string MYDBConnectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection myConn = new SqlConnection(MYDBConnectionString);
            string sqlStmt = "Select * from Appointment WHERE aptDate = @aptDate AND arrived=@arrived AND branchId=@branchId ORDER BY aptDateTime ";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            System.Diagnostics.Debug.WriteLine(todaydate);
            da.SelectCommand.Parameters.AddWithValue("@aptDate", todaydate);
            da.SelectCommand.Parameters.AddWithValue("@arrived", 0);
            da.SelectCommand.Parameters.AddWithValue("@branchId", branchId);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<Appointment> aptList = new List<Appointment>();
            int rec_cnt = ds.Tables[0].Rows.Count;
            if (rec_cnt > 0)
            {
                for (int i = 0; i < rec_cnt; i++)
                {
                    DataRow row = ds.Tables[0].Rows[i];  // Sql command returns only one record
                    string aptTime = row["aptTime"].ToString();
                    string aptDate = row["aptDate"].ToString();
                    string partySize = row["partySize"].ToString();
                    string bookTime = row["bookTime"].ToString();
                    string bookDate = row["bookDate"].ToString();
                    DateTime aptDateTime = (DateTime)row["aptDateTime"];
                    string customerId = row["customerId"].ToString();
                    string branchId = row["branchId"].ToString();
                    string appointmentSettingId = row["appointmentSettingId"].ToString();
                    Appointment apt = new Appointment(aptDate, aptTime, bookDate, bookTime, partySize, aptDateTime, customerId, branchId, appointmentSettingId);
                    aptList.Add(apt);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No data");
            }
            return aptList;
        }
        public List<Appointment> SelectAll()
        {
            string MYDBConnectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection myConn = new SqlConnection(MYDBConnectionString);
            string sqlStmt = "Select aptDate from Appointment WHERE branchId=@branchId";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@branchId", branchId);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<Appointment> aptList = new List<Appointment>();
            int rec_cnt = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rec_cnt; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];  // Sql command returns only one record
                string aptTime = row["aptTime"].ToString();
                string aptDate = row["aptDate"].ToString();
                string bookTime = row["bookTime"].ToString();
                string bookDate = row["bookDate"].ToString();
                string partySize = row["partySize"].ToString();
                DateTime aptDateTime = (DateTime)row["aptDateTime"];
                string customerId = row["customerId"].ToString();
                string branchId = row["branchId"].ToString();
                string appointmentSettingId = row["appointmentSettingId"].ToString();
                Appointment apt = new Appointment(aptDate, aptTime, bookDate, bookTime, partySize, aptDateTime, customerId, branchId, appointmentSettingId);
                aptList.Add(apt);
            }
            return aptList;
        }
        public int setArrived(string aptTime, string aptDate)
        {
            string MYDBConnectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection myConn = new SqlConnection(MYDBConnectionString);
            string sqlStmt = "UPDATE Appointment SET arrived = @paraarrived WHERE aptTime=@aptTime AND aptDate=@aptDate AND branchId=@branchId";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);
            sqlCmd.Parameters.AddWithValue("@aptTime", aptTime);
            sqlCmd.Parameters.AddWithValue("@aptDate", aptDate);
            sqlCmd.Parameters.AddWithValue("@paraarrived", 1);
            sqlCmd.Parameters.AddWithValue("@branchdId", branchId);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();
            myConn.Close();
            return result;
        }

        public int deleteAppointment(string aptTime, string aptDate)
        {
            string MYDBConnectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection myConn = new SqlConnection(MYDBConnectionString);
            string sqlStmt = "DELETE FROM Appointment WHERE aptTime=@apttime and aptDate=@aptdate and branchId=@branchId";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);
            sqlCmd.Parameters.AddWithValue("@aptTime", aptTime);
            sqlCmd.Parameters.AddWithValue("@aptDate", aptDate);
            sqlCmd.Parameters.AddWithValue("@branchId", branchId);
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();
            myConn.Close();
            return result;
        }
        public List<string> selectAllAppointmentDateAscend()
        {
            string MYDBConnectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection myConn = new SqlConnection(MYDBConnectionString);
            string sqlStmt = "Select DISTINCT aptDate from Appointment WHERE branchId=@branchId ";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.Parameters.AddWithValue("@branchId", branchId);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<string> aptDateList = new List<string>();
            int rec_cnt = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rec_cnt; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];  // Sql command returns only one record
                string aptDate = row["aptDate"].ToString();

                aptDateList.Add(aptDate);
            }
            return aptDateList;
        }
        public int selectCountByDate(string aptDate)
        {
            string MYDBConnectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
            SqlConnection myConn = new SqlConnection(MYDBConnectionString);
            string sqlStmt = "Select * from Appointment WHERE aptDate=@aptDate AND branchId=@branchId";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@aptDate", aptDate);
            da.SelectCommand.Parameters.AddWithValue("@branchId", branchId);
            DataSet ds = new DataSet();
            da.Fill(ds);

            int rec_cnt = ds.Tables[0].Rows.Count;
            return rec_cnt;
        }
    }
}
