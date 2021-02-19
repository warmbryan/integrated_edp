using DBService.Models;
using EDP_Project.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDP_Project
{
    public partial class AdminBusinessAcceptance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();
            String id = (String)Request.Params["id"];
            if (id != null)
            {
                Business tmpClass = client.GetSingleBusinessByBusinessId(id);
                if (tmpClass != null)
                {
                    BusinessUser tmpUser = client.GetBusinessUserByUserId(tmpClass.UserId);
                    lbName.Text = tmpClass.Name;
                    lbRegistrationNumber.Text = tmpClass.RegistrationNumber;
                    lbType.Text = tmpClass.Type;
                    urlLink.NavigateUrl = tmpClass.Url;
                    lbRegisteredBy.Text = tmpUser.Name;
                    if (tmpClass.LogoId != null)
                    {

                    }
                    else
                    {
                        imgBusinessLogo.ImageUrl = "~/" + tmpClass.LogoId +".png";
                    }
                    testImage.ImageUrl = "~/" +tmpClass.AcraCertificate + ".pdf";
                }
            }
            else
            {
                Response.Redirect("~/AdminListBusinesses");
            }
        }

        protected void update_business_verified_Click(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();
            String id = (String)Request.Params["id"];
            if (id != null)
            {
                Business tmpClass = client.GetSingleBusinessByBusinessId(id);
                if (tmpClass != null)
                {
                    Int16 result = client.UpdateBusinessVerification(tmpClass.Id, true);
                }
            }
        }
    }
}