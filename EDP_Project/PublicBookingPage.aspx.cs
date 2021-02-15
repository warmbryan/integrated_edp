using EDP_Project.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EDP_Project
{
    public partial class PublicBookingPage : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
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
                    //working
                    //availableTime.Add(time);
                }
                //for testing (Remove when done)
                availableTime.Add(time);
                //
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
            List<TimeSpan> bookedTime = new List<TimeSpan>();
            dd_time.Items.Clear();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    string aptsTime = reader["aptTime"].ToString();
                    TimeSpan aptTime = TimeSpan.Parse(aptsTime.Substring(0, (aptsTime.Length - 2)));
                    if (aptsTime.Substring(aptsTime.Length - 2, 2) == "PM" && (aptsTime != "12:00pm"))
                    {
                        aptTime = aptTime.Add(twelvehours);
                    }
                    bookedTime.Add(aptTime);
                }
                for (int i = 0; i < availableTime.Count; i++)
                {
                    TimeSpan avail_timing = availableTime[i];
                    for (int y = 0; y < bookedTime.Count; y++)
                    {
                        TimeSpan book_timing = bookedTime[y];
                        if (avail_timing == book_timing)
                        {
                            availableTime.RemoveAt(i);
                        }
                    }

                }
                for (int p = 0; p < availableTime.Count; p++)
                {
                    TimeSpan timing = availableTime[p];
                    DateTime x = DateTime.Today.Add(timing);
                    string result_time = x.ToString("hh:mm tt");
                    dd_time.Items.Add(new ListItem(result_time, result_time));
                }

            }
            else
            {
                for (int i = 0; i < availableTime.Count; i++)
                {
                    TimeSpan timing = availableTime[i];
                    DateTime x = DateTime.Today.Add(timing);
                    string result_time = x.ToString("hh:mm tt");
                    dd_time.Items.Add(new ListItem(result_time, result_time));
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
            string datetime = date + " " + time;
            DateTime aptDateTime = DateTime.Parse(datetime);
            Service1Client client = new Service1Client();
            //NEEED THESE
            string customerid = Request.QueryString["customerid"];
            string branchid = Request.QueryString["branchid"];
            string appointmentsettingid = Request.QueryString["appointmentsettingid"];
            int feedback = client.CreateAppointment(time, date, current_date, current_time, partysize, aptDateTime, customerid, branchid, appointmentsettingid);
            if (feedback == 1)
            {
                Response.Redirect("PublicBookingSuccess.aspx");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("SQL Error. Insert Unsuccesfull!");
            }


        }
    }
}