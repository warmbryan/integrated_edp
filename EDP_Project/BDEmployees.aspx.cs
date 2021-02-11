using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

using EDP_Project.ServiceReference1;
using DBService.Models;

namespace EDP_Project
{
    public partial class BDEmployees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("~/BDLogin");
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-12);
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