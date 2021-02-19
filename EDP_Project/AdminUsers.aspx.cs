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
            List<CustomerClass> cList = client.SelectAllCustomer().ToList<CustomerClass>();

            lvUsers.Visible = true;
            lvUsers.DataSource = cList;
            lvUsers.DataBind();
        }

        protected void filterBtn_Click(object sender, EventArgs e)
        {
            String tmpValue = dpMenu.SelectedValue.Trim();
            if (tmpValue == "0")
            {
                Service1Client client = new Service1Client();
                List<CustomerClass> cList = client.SelectAllCustomer().ToList<CustomerClass>();

                lvUsers.Visible = true;
                lvUsers.DataSource = cList;
                lvUsers.DataBind();
                lvBusiness.DataSource = null;
                lvBusiness.DataBind();
                lvAdmin.DataSource = null;
                lvAdmin.DataBind();
            }
            else if (tmpValue == "1")
            {
                Service1Client client = new Service1Client();
                List<BusinessUser> cList = client.SelectAllBusiness().ToList<BusinessUser>();

                lvBusiness.Visible = true;
                lvBusiness.DataSource = cList;
                lvBusiness.DataBind();
                lvUsers.DataSource = null;
                lvUsers.DataBind();
                lvAdmin.DataSource = null;
                lvAdmin.DataBind();
            }
            else if (tmpValue == "2")
            {
                Service1Client client = new Service1Client();
                List<AdminClass> cList = client.SelectAllAdmin().ToList<AdminClass>();

                lvAdmin.Visible = true;
                lvAdmin.DataSource = cList;
                lvAdmin.DataBind();
                lvUsers.DataSource = null;
                lvUsers.DataBind();
                lvBusiness.DataSource = null;
                lvBusiness.DataBind();
            }
        }
    }
}