using DBService.Models;
using EDP_Project.ServiceReference1;
using MimeTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace EDP_Project
{
    public partial class BDUpdateBusiness : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthRequire.CheckIfUserLoggedIn())
            {
                AuthRequire.Logout();
                Response.Redirect("/CustomerLogin");
                return;
            }

            if (Request.Params["business"] == null)
            {
                Response.Redirect("~/BDBusinesses.aspx");
                return;
            }

            Service1Client client = new Service1Client();
            Business bo = client.GetSingleBusinessByBusinessId(Request.Params["business"].Trim());

            if (bo == null)
            {
                Response.Redirect("~/BDBusinesses.aspx");
                return;
            }

            tb_businessName.Text = bo.Name;
            tb_businessRegNum.Text = bo.RegistrationNumber;
            tb_businessType.Text = bo.Type;
            tb_businessUrl.Text = bo.Url;
        }

        protected List<String> Validate_Submission()
        {
            List<String> errors = new List<String>();

            if (tb_businessName.Text.Trim().Equals(String.Empty))
            {
                errors.Add("Empty business name field");
            }

            if (tb_businessRegNum.Text.Trim().Equals(String.Empty))
            {
                errors.Add("Empty business registration number field");
            }

            if (tb_businessType.Text.Trim().Equals(String.Empty))
            {
                errors.Add("Empty business type field");
            }

            if (file_acraRegistration.HasFile && file_acraRegistration.PostedFile.ContentType != "application/pdf")
            {
                errors.Add("Invalid ACRA registration document file format");
            }
            else if (file_acraRegistration.PostedFile.ContentLength > 8388608)
            {
                errors.Add("ACRA registration document exceeded the file size limit of 8MB");
            }

            if (file_logoId.HasFile && file_logoId.PostedFile.ContentType != "image/png" && file_logoId.PostedFile.ContentType != "image/jpeg")
            {
                errors.Add("Invalid logo file format");
            }
            else if (file_logoId.PostedFile.ContentLength > 2097152)
            {
                errors.Add("Logo exceeded the file size limit of 2MB");
            }

            return errors;
        }

        protected void Update_Business(object sender, EventArgs e)
        {
            string businessId = Request.Params["business"].Trim();

            string name = HttpUtility.HtmlEncode(tb_businessName.Text.Trim());
            string registrationNumber = HttpUtility.HtmlEncode(tb_businessRegNum.Text.Trim());
            string url = HttpUtility.HtmlEncode(tb_businessUrl.Text.Trim());
            string type = HttpUtility.HtmlEncode(tb_businessType.Text.Trim());

            string logoId, acraCertificate = null;

            List<String> errors = Validate_Submission();

            if (errors.Count > 0)
            {
                lv_feedback.Visible = true;
                lv_feedback.DataSource = errors;
                lv_feedback.DataBind();
                return;
            }

            if (file_acraRegistration.HasFile)
            {
                acraCertificate = Guid.NewGuid().ToString();

                string documentDir = Server.MapPath(@"\assets\Documents");
                if (!Directory.Exists(documentDir))
                {
                    Directory.CreateDirectory(documentDir);
                }
                
                file_acraRegistration.SaveAs(Path.Combine(documentDir, acraCertificate + ".pdf"));
            }
            else
                acraCertificate = null;

            if (file_logoId.HasFile)
            {
                string logoDir = Server.MapPath(@"\assets\Logos");
                if (!Directory.Exists(logoDir))
                {
                    Directory.CreateDirectory(logoDir);
                }

                logoId = Guid.NewGuid().ToString();
                file_acraRegistration.SaveAs(Path.Combine(logoDir, logoId + MimeTypeMap.GetExtension(file_logoId.PostedFile.ContentType)));
            }
            else
                logoId = null;

            if (Session["userId"] != null)
            {
                ServiceReference1.IService1 client = new ServiceReference1.Service1Client();
                if (client.UpdateBusinessDetails(businessId, name, registrationNumber, url, type, acraCertificate, logoId))
                {
                    Response.Redirect("/BDBusinesses.aspx?updated=" + businessId, false);
                }
            }
        }
    }
}