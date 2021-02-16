using DBService.Models;
using EDP_Project.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace EDP_Project
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();
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
            string aptDate = Request.QueryString["aptDate"];
            List<Appointment> aList = new List<Appointment>();
            aList = client.GetAllAppointmentByTodayDateAscend(aptDate).ToList<Appointment>();
            //aList = client.GetAllAppointmentByTodayDateAscend().ToList<Appointment>();
            GV_appointment.Visible = true;
            GV_appointment.DataSource = aList;
            GV_appointment.DataBind();
        }
        protected void GV_appointment_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Arrived")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GV_appointment.Rows[rowIndex];
                string aptTime = row.Cells[0].Text;
                string aptDate = row.Cells[1].Text;
                string url = "BDPopup.aspx?aptDate=" + aptDate + "&aptTime=" + aptTime;
                string s = "window.open('" + url + "', 'popup_window', 'width=350,height=130,left=600,top=280,resizable=yes')";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
            else if (e.CommandName == "Delete")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GV_appointment.Rows[rowIndex];
                string aptTime = row.Cells[0].Text;
                string aptDate = row.Cells[1].Text;
                int feedback = client.deleteAppointment(aptTime, aptDate);
                if (feedback == 1)
                {
                    Response.Redirect("BDViewApt.aspx");
                }
            }
        }
    }
}