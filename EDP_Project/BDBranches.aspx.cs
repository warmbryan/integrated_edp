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
    public partial class BDBranches : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthRequire.CheckIfUserLoggedIn())
            {
                AuthRequire.Logout();
                Response.Redirect("/CustomerLogin");
                return;
            }

            if (string.IsNullOrEmpty(Request.Params["business"]))
            {
                Response.Redirect("/business/my-businesses");
            }

            try
            {
                Guid businessId = Guid.Parse(Request.Params["business"]);
                LoadBusinessBranches(businessId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in BDBranches.aspx Page_Load " + ex + " message: " + ex.Message);
            }
        }

        protected void LoadBusinessBranches(Guid businessId)
        {
            Service1Client client = new Service1Client();
            List<Branch> branches = client.SelectBranchesByBusinessId(businessId).ToList();

            lv_branches.DataSource = branches;
            lv_branches.DataBind();
        }
    }
}