using System;
using System.Web;

namespace EDP_Project
{
    public class AuthRequire
    {
        public static String RetrieveUserRole()
        {
            if (HttpContext.Current.Session["r"] != null)
            {
                String role = HttpContext.Current.Session["r"].ToString();
                return role;
            }
            return "";
        }
        public static void Logout()
        {
            if (HttpContext.Current.Session["di"] != null &&
                HttpContext.Current.Session["ae"] != null &&
                HttpContext.Current.Session["ta"] != null &&
                HttpContext.Current.Request.Cookies["ta"] != null)
            {
                if (HttpContext.Current.Session["ta"].Equals(HttpContext.Current.Request.Cookies.Get("ta").Value))
                {
                    if (HttpContext.Current.Request.Cookies["ASP.NET_SessionId"] != null)
                    {
                        HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Value = String.Empty;
                        HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
                    }
                    if (HttpContext.Current.Request.Cookies["ta"] != null)
                    {
                        HttpContext.Current.Response.Cookies["ta"].Value = String.Empty;
                        HttpContext.Current.Response.Cookies["ta"].Expires = DateTime.Now.AddMonths(-20);
                    }
                    HttpContext.Current.Session.Clear();
                    HttpContext.Current.Session.Abandon();
                    HttpContext.Current.Session.RemoveAll();

                    HttpContext.Current.Response.Redirect("~/CustomerLogin");
                }
            }
        }
        
        public static Boolean SetUserSession(Guid ID, String Email, String Role)
        {
            try
            {
                HttpContext.Current.Session["di"] = ID;
                HttpContext.Current.Session["userId"] = ID;
                HttpContext.Current.Session["ae"] = Email;
                string guid = Guid.NewGuid().ToString();
                HttpContext.Current.Session["ta"] = guid;
                HttpContext.Current.Session["r"] = Role;
                HttpContext.Current.Response.Cookies.Add(new HttpCookie("ta", guid));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Boolean CheckIfUserLoggedIn()
        {
            if (HttpContext.Current.Session["di"] != null &&
                HttpContext.Current.Session["ae"] != null &&
                HttpContext.Current.Session["ta"] != null &&
                HttpContext.Current.Request.Cookies["ta"] != null)
            {
                return (HttpContext.Current.Session["ta"].Equals(HttpContext.Current.Request.Cookies.Get("ta").Value));
            }
            else
            {
                return false;
            }
        }
    }
}