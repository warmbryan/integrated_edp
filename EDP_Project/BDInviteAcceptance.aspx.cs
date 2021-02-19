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
    public partial class BDInviteAcceptance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["accept"] != "accept" && Request.Params["accept"] != "reject" || Request.Params["invite"] == null)
            {
                Response.Redirect("/business/my-invitations");
                return;
            }

            Guid inviteId;
            try
            {
                inviteId = Guid.Parse(Request.Params["invite"]);
                string sInviteId = inviteId.ToString();

                Service1Client client = new Service1Client();

                BusinessEmployeeAccess bea = client.GetOneEmployeeAccess(sInviteId);
                if (bea != null)
                {
                    if (bea.UserId == Session["userId"].ToString())
                    {
                        switch (Request.Params["accept"])
                        {
                            case "accept":
                                Console.WriteLine("accepted");
                                client.AcceptBusinessInvite(sInviteId);
                                break;
                            case "reject":
                                Console.WriteLine("rejected");
                                client.RejectBusinessInvite(sInviteId);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in BDInviteAcceptance Page_Load" + ex + " message: " + ex.Message);
            }
            finally
            {
                Response.Redirect("/business/my-invitations");
            }
        }
    }
}