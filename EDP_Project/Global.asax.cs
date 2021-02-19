using System;
using System.Web.Http;
using System.Web.Routing;

namespace EDP_Project
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // page route
            RouteTable.Routes.MapPageRoute("Home", "", "~/CustomerHome.aspx");
            RouteTable.Routes.MapPageRoute("Search", "search/", "~/CustomerSearch.aspx");

            // business side
            RouteTable.Routes.MapPageRoute("Business Home", "business/", "~/BDHome.aspx");
            RouteTable.Routes.MapPageRoute("Business Login", "business/login", "~/BDLogin.aspx");
            RouteTable.Routes.MapPageRoute("Business Registration", "business/register", "~/BDRegister.aspx");

            RouteTable.Routes.MapPageRoute("Business - My Businesses", "business/my-businesses", "~/BDBusinesses.aspx");
            RouteTable.Routes.MapPageRoute("Business - Add new business", "business/add/business", "~/BDAddBusiness.aspx");
            RouteTable.Routes.MapPageRoute("Business Update Business", "business/update/business", "~/BDUpdateBusiness.aspx");

            RouteTable.Routes.MapPageRoute("Business Employees", "business/employees", "~/BDEmployees.aspx");
            RouteTable.Routes.MapPageRoute("Business Add Employees", "business/add/employee", "~/BDAddEmployee.aspx");

            RouteTable.Routes.MapPageRoute("Business Customer Chat", "business/customer-chat", "~/BDCustomerChat.aspx");

            RouteTable.Routes.MapPageRoute("Business Account Profile", "business/my-account", "~/BDMyAccount.aspx");
            RouteTable.Routes.MapPageRoute("Business Invitations", "business/my-invitations", "~/BDInvitations.aspx");
            RouteTable.Routes.MapPageRoute("Business Invitation Acceptance", "business/my-invitations/acceptance", "~/BDInviteAccptance.aspx");

            RouteTable.Routes.MapPageRoute("Business Appointments", "business/appointments", "~/BDViewAppointments.aspx");

            RouteTable.Routes.MapPageRoute("Business Delete Business", "business/delete/business", "~/BDDeleteBusiness.aspx");
            RouteTable.Routes.MapPageRoute("Business Delete Employee", "business/delete/employee", "~/BDDeleteEmployee.aspx");

            RouteTable.Routes.MapPageRoute("Business Branches", "business/branches", "~/BDBranches.aspx");
            RouteTable.Routes.MapPageRoute("Business - Add new branch", "business/add/branch", "~/BDCreateBranch.aspx");
            RouteTable.Routes.MapPageRoute("Business Update Branch", "business/update/branch", "~/BDUpdateBranch.aspx");

            RouteTable.Routes.MapPageRoute("Business Logout", "logout", "~/BDLogout.aspx");

            RouteTable.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}", defaults: new { id = System.Web.Http.RouteParameter.Optional });

            // removes the aspx extensions from the url when the page loads
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}