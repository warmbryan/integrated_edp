using System;

using System.Web.Routing;

namespace EDP_Project
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // page route
            RouteTable.Routes.MapPageRoute("Business Login", "business/login", "~/BDLogin.aspx");
            RouteTable.Routes.MapPageRoute("Business Registration", "business/register", "~/BDRegister.aspx");
            
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