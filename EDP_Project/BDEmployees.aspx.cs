using DBService.Models;
using EDP_Project.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace EDP_Project
{
    public partial class BDEmployees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthRequire.CheckIfUserLoggedIn())
            {
                AuthRequire.Logout();
                Response.Redirect("/CustomerLogin");
                return;
            }

            if (Request.Params["business"] != null)
            {
                string businessId = Request.Params["business"].Trim();
                Service1Client client = new ServiceReference1.Service1Client();

                Business business = client.GetSingleBusinessByBusinessId(businessId);

                if (business == null)
                {
                    Response.Redirect("~/BDBusinesses.aspx");
                    return;
                }

                // business name
                lbl_businessName.Text = business.Name;

                // bind the data from wcf
                List<BusinessEmployeeAccess> employees = client.GetAllEmployeeByBusinessId(businessId).ToList();

                if (employees.Count == 0)
                {
                    lbl_feedback.Text = "You currently don't have any employees, add one?";
                    return;
                }

                lv_employees.DataSource = client.GetAllEmployeeByBusinessId(businessId);
                lv_employees.DataBind();
            }
        }
    }
}