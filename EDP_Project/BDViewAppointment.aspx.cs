using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace EDP_Project
{
    public partial class BDViewApppointment : System.Web.UI.Page
    {
        string global_aptDate = "";
        string global_aptTime = "";
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthRequire.CheckIfUserLoggedIn())
            {
                AuthRequire.Logout();
                Response.Redirect("/CustomerLogin");
                return;
            }

            if (!IsPostBack)
            {
                RefreshGridView();
            }
        }
        private void RefreshGridView()
        {
            List<string> aList = new List<string>();
            aList = client.selectAllAppointmentDateAscend().ToList<string>();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("aptDate"), new DataColumn("count") });
            foreach (string aptDate in aList)
            {
                int count = client.selectCountByDate(aptDate);
                dt.Rows.Add(aptDate, count);
            }
            //aList = client.GetAllAppointmentByTodayDateAscend().ToList<Appointment>();
            GV_appointment.Visible = true;
            GV_appointment.DataSource = dt;
            GV_appointment.DataBind();
        }

        protected void GV_appointment_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GV_appointment.Rows[rowIndex];
                string aptDate = row.Cells[0].Text;
                Response.Redirect("BDViewApt.aspx?aptDate=" + aptDate);
            }

        }
        protected void getRowData(string apttime, string aptdate)
        {
            global_aptDate = aptdate;
            global_aptTime = apttime;
        }
        protected void arrivedButton(object sender, EventArgs e)
        {

            int feedback = client.setArrived(global_aptTime, global_aptDate);
            if (feedback == 1)
            {
                Response.Redirect("BDViewAppointment.aspx");
            }
        }
        protected void todayAppointment(object sender, EventArgs e)
        {
            string todaydate = DateTime.Today.ToShortDateString();
            Response.Redirect("BDViewApt.aspx?aptDate=" + todaydate);
        }
    }
}