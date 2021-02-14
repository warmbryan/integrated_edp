using DBService.Models;
using EDP_Project.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDP_Project
{
    public partial class AdminUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();
            List<CustomerClass> eList = client.SelectAllCustomer().ToList<CustomerClass>();

            gvUsers.Visible = true;
            gvUsers.DataSource = eList;
            gvUsers.DataBind();
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvUsers, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }

        protected void gvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Email = gvUsers.SelectedRow.Cells[2].Text;
            Response.Redirect("~/Administrator/UserDetailed?Email=" + Email);
        }
    }
}