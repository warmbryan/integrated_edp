using DBService.Models;
using EDP_Project.ServiceReference1;
using System;

namespace EDP_Project
{
    public partial class BDAddEmployee : System.Web.UI.Page
    {
        private string businessId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("~/BDLogin");
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-12);
                return;
            }

            Service1Client client = new Service1Client();

            if (Request.Params["business"] == null)
            {
                string userId = Session["userId"].ToString();
                lv_businesses.DataSource = client.GetAllBusinessByUserId(userId);
                lv_businesses.DataBind();
                lv_businesses.Visible = true;
            }
        }

        protected void Add_Employee(object sender, EventArgs e)
        {
            string email = tb_email.Text.Trim();

            string businessId = Request.Form["business"].Trim();

            // permissions
            bool rApp = (Request.Form["rApp"] == "on");
            bool wApp = (Request.Form["wApp"] == "on");
            bool rCC = (Request.Form["rCC"] == "on");
            bool wCC = (Request.Form["wCC"] == "on");

            string role = tb_role.Text.Trim();

            Service1Client client = new Service1Client();
            BusinessUser employee = client.GetBusinessUserByEmail(email);

            if (employee == null)
            {
                lbl_feedback.Text = "Employee doesn't have an account on this site. Ask your employee to register for an account!";
                return;
            }

            if (client.AddEmployeeToBusinessByEmail(employee.Id, businessId, rApp, wApp, rCC, wCC, role))
            {
                Response.Redirect("~/BDEmployees.aspx");
            }
        }
    }
}