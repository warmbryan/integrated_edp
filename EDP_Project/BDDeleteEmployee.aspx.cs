using System;

using DBService.Models;

namespace EDP_Project
{
    public partial class BDDeleteEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthRequire.CheckIfUserLoggedIn())
            {
                AuthRequire.Logout();
                Response.Redirect("/CustomerLogin");
                return;
            }

            Guid beaId, businessId;

            BusinessEmployeeAccess bea = null;

            try
            {
                beaId = Guid.Parse(Request.Params["id"]);
                ServiceReference1.IService1 client = new ServiceReference1.Service1Client();

                bea = client.GetOneEmployeeAccess(beaId.ToString());
                client.DeleteEmployeeAccess(bea.UserId, bea.BusinessId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + "\n" + ex.Message);
                // log?
            }
            finally
            {
                if (bea != null)
                    Response.Redirect("~/business/employees?business=" + bea.BusinessId);
                else
                    Response.Redirect("~/business/employees?business");
            }

            
        }
    }
}