using DBService.Models;
using EDP_Project.ServiceReference1;
using System;

namespace EDP_Project
{
    public partial class BDAddEmployee : System.Web.UI.Page
    {
        Guid businessId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthRequire.CheckIfUserLoggedIn())
            {
                AuthRequire.Logout();
                Response.Redirect("/CustomerLogin");
                return;
            }

            try
            {
                businessId = Guid.Parse(Request.Params["business"]);
            }
            catch (Exception ex)
            {
                Response.Redirect("/business/my-businesses");
            }

            Service1Client client = new Service1Client();
            lv_roles.DataSource = client.GetBusinessRoles(businessId.ToString());
            lv_roles.DataBind();
            lv_roles.Visible = true;
        }

        protected void Add_Employee(object sender, EventArgs e)
        {
            string email = tb_email.Text.Trim();

            var theForm = Request.Form;

            // permissions
            bool rApp = (Request.Form["rApp"] == "on");
            bool wApp = (Request.Form["wApp"] == "on");
            bool rCC = (Request.Form["rCC"] == "on");
            bool wCC = (Request.Form["wCC"] == "on");

            string roleId = Request.Form["role"].Trim();

            Service1Client client = new Service1Client();
            BusinessUser employee = client.GetBusinessUserByEmail(email);

            if (employee == null)
            {
                lbl_feedback.Text = "Employee doesn't have an account on this site. Ask your employee to register for an account!";
                return;
            }

            if (client.AddEmployeeToBusinessByEmail(employee.Id, businessId.ToString(), roleId, rApp, wApp, rCC, wCC))
                Response.Redirect("~/business/employees?business=" + businessId.ToString());
        }
    }
}