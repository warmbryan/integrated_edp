using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

using System.Configuration;

namespace EDP_Project
{
    public partial class PublicAppointmentBooking : System.Web.UI.Page
    {
        string MYDBConnectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
        List<TimeSpan> availableTime = new List<TimeSpan>();
        TimeSpan twelvehours = new TimeSpan(12, 0, 0);
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(MYDBConnectionString);

            List<TimeSpan> bookedTime = new List<TimeSpan>();
            string sql = "SELECT aptTime,aptDate FROM Appointment ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string aptsDate = reader["aptDate"].ToString();
                DateTime aptDate = Convert.ToDateTime(aptsDate);
                string aptsTime = reader["aptTime"].ToString();
                TimeSpan aptTime = TimeSpan.Parse(aptsTime.Substring(0, (aptsTime.Length - 2)));
                if (aptsTime.Substring(aptsTime.Length - 2, 2) == "pm" && (aptsTime != "12:00pm"))
                {
                    aptTime = aptTime.Add(twelvehours);
                }
                bookedTime.Add(aptTime);
            }
            conn.Close();

            TimeSpan time = new TimeSpan(12, 0, 0);
            TimeSpan time_now = DateTime.Now.TimeOfDay;
            TimeSpan minutes = new TimeSpan(0, 30, 0);

            for (int i = 0; i < 20; i++)
            {
                if (time_now < time)
                {
                    availableTime.Add(time);
                }

                time = time.Add(minutes);
            }

            for (int i = 0; i < availableTime.Count; i++)
            {
                TimeSpan timing = availableTime[i];
                DateTime x = DateTime.Today.Add(timing);
                string result_time = x.ToString("hh:mm tt");

                dd_time.Items.Add(new ListItem(result_time, result_time));
            }

            lbl_date.Text = calendar.TodaysDate.ToShortDateString();

        }
        protected void Calender_SelectionChanged(object sender, EventArgs e)
        {
            lbl_date.Text = calendar.SelectedDate.ToShortDateString();
            SqlConnection conn = new SqlConnection(MYDBConnectionString);

            string sql = "SELECT aptTime FROM Appointment WHERE aptDate=@aptDate";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@aptDate", calendar.SelectedDate.ToShortDateString());
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                dd_time.Items.Clear();

                while (reader.Read())
                {
                    string aptsTime = reader["aptTime"].ToString();
                    TimeSpan aptTime = TimeSpan.Parse(aptsTime.Substring(0, (aptsTime.Length - 2)));
                    if (aptsTime.Substring(aptsTime.Length - 2, 2) == "pm" && (aptsTime != "12:00pm"))
                    {
                        aptTime = aptTime.Add(twelvehours);
                    }
                    for (int i = 0; i < availableTime.Count; i++)
                    {
                        TimeSpan timing = availableTime[i];
                        System.Diagnostics.Debug.WriteLine(aptTime);
                        System.Diagnostics.Debug.WriteLine(timing);
                        if (timing != aptTime)
                        {
                            DateTime x = DateTime.Today.Add(timing);
                            string result_time = x.ToString("hh:mm tt");
                            dd_time.Items.Add(new ListItem(result_time, result_time));
                        }

                    }

                }
            }
        }
        protected void Calendar_DayRender(object sender, DayRenderEventArgs e)
        {
            e.Cell.ForeColor = System.Drawing.Color.LightGray;
            if (e.Day.Date < DateTime.Today)
            {

                e.Day.IsSelectable = false;
            }
            else
            {
                e.Cell.ForeColor = System.Drawing.Color.Black;
            }
        }
        protected void Submit(object sender, EventArgs e)
        {
            lbl_party_size.Text = party_size.SelectedValue;
            string partysize = lbl_party_size.Text;
            string date = calendar.SelectedDate.ToShortDateString();
            string time = dd_time.SelectedValue;
            string current_date = DateTime.Now.ToShortDateString();
            string current_time = DateTime.Now.ToShortTimeString();

            try
            {
                using (SqlConnection con = new SqlConnection(MYDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Appointment(aptTime, aptDate, bookTime, bookDate, partySize) VALUES(@aptTime, @aptDate, @bookTime, @bookDate, @partySize)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {

                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@aptTime", time);
                            cmd.Parameters.AddWithValue("@aptDate", date);
                            cmd.Parameters.AddWithValue("@bookTime", current_time);
                            cmd.Parameters.AddWithValue("@bookDate", current_date);
                            cmd.Parameters.AddWithValue("@partySize", partysize);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            Response.Redirect("Success.aspx");
        }
    }
}