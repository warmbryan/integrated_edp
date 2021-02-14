using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EDP_Project.ServiceReference1;
using DBService.Models;

namespace EDP_Project
{
    public partial class AdminUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();
            List<CustomerClass> cList = client.SelectAllCustomer().ToList<CustomerClass>();

            gvUsers.Visible = true;
            gvUsers.DataSource = cList;
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
            Response.Redirect("~/AdminiUserDetailed?Email=" + Email);
        }

        protected void filterBtn_Click(object sender, EventArgs e)
        {
            String tmpValue = dpMenu.SelectedValue.Trim();
            if (tmpValue == "0")
            {
                Service1Client client = new Service1Client();
                List<CustomerClass> cList = client.SelectAllCustomer().ToList<CustomerClass>();

                gvUsers.Visible = true;
                gvUsers.DataSource = cList;
                gvUsers.DataBind();
                gvAdmin.DataSource = null;
                gvAdmin.DataBind();
            }
            else if (tmpValue == "2")
            {
                Service1Client client = new Service1Client();
                List<AdminClass> cList = client.SelectAllAdmin().ToList<AdminClass>();

                gvAdmin.Visible = true;
                gvAdmin.DataSource = cList;
                gvAdmin.DataBind();
                gvUsers.DataSource = null;
                gvUsers.DataBind();
            }
        }
    }
}