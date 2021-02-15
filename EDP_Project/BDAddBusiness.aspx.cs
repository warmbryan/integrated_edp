using EDP_Project.ServiceReference1;
using MimeTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace EDP_Project
{
    public partial class BDAddBusiness : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("~/BDLogin");
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-12);
                return;
            }
        }

        protected void Register_Business(object sender, EventArgs e)
        {
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

            acraCertificate = Guid.NewGuid().ToString();
            file_acraRegistration.SaveAs(Server.MapPath(Path.Combine(@"assets\Documents", acraCertificate + ".pdf")));

            if (file_logoId.HasFile)
            {
                logoId = Guid.NewGuid().ToString();
                file_acraRegistration.SaveAs(Server.MapPath(Path.Combine(@"Public\Logos", logoId + MimeTypeMap.GetExtension(file_logoId.PostedFile.ContentType))));
            }
            else
                logoId = null;

            if (Session["userId"] != null)
            {
                Service1Client client = new Service1Client();
                if (client.CreateBusiness(name, registrationNumber, url, type, acraCertificate, logoId, Session["userId"].ToString()))
                {
                    Response.Redirect("/BDBusinesses.aspx", false);
                }
            }
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

            if (!file_acraRegistration.HasFile)
            {
                errors.Add("ACRA registration document is required");
            }

            if (file_acraRegistration.PostedFile.ContentLength > 8388608)
            {
                errors.Add("ACRA registration document exceeded the file size limit of 8MB");
            }
            else if (file_acraRegistration.PostedFile.ContentType != "application/pdf")
            {
                errors.Add("Invalid ACRA registration document file format");
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
    }
}