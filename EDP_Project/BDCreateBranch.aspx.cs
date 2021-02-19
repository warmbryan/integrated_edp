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
    public partial class BDCreateBranch : System.Web.UI.Page
    {
        Guid businessId = new Guid();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthRequire.CheckIfUserLoggedIn())
            {
                AuthRequire.Logout();
                Response.Redirect("~/CustomerLogin");
                return;
            }

            try
            {
                businessId = Guid.Parse(Request.Params["business"]);
            }
            catch (Exception ex)
            {
                Response.Redirect("/business/my-businesses");
            }

            hl_branch.NavigateUrl = "/business/branches?business="+businessId.ToString();
        }

        protected void Add_Branch(object sender, EventArgs e)
        {
            if (Validate_Form() > 0)
                return;
            else
            {
                Service1Client client = new Service1Client(); 
                Business business = client.GetSingleBusinessByBusinessId(businessId.ToString());
                if (business.UserId == Session["userId"].ToString())
                {
                    client.CreateBranch(
                        businessId,
                        HttpUtility.HtmlEncode(tb_name.Text),
                        HttpUtility.HtmlEncode(tb_email.Text),
                        HttpUtility.HtmlEncode(tb_addr.Text),
                        HttpUtility.HtmlEncode(tb_addr2.Text),
                        HttpUtility.HtmlEncode(tb_city.Text),
                        HttpUtility.HtmlEncode(tb_state.Text),
                        HttpUtility.HtmlEncode(tb_zip.Text),
                        HttpUtility.HtmlEncode(tb_country.Text),
                        HttpUtility.HtmlEncode(tb_phone.Text),
                        HttpUtility.HtmlEncode(tb_email.Text),
                        cb_mainBranch.Checked
                    );
                    Response.Redirect("/business/branches?business="+businessId.ToString());
                    return;
                }
                else
                    Response.Redirect("/business/my-businesses");
            }
        }

        protected int Validate_Form()
        {
            int errors = 0;
            if (string.IsNullOrWhiteSpace(tb_name.Text))
            {
                lbl_feedback.Text += "Name field is empty\n";
                errors++;
            }

            if (string.IsNullOrWhiteSpace(tb_desc.Text))
            {
                lbl_feedback.Text += "Description field is empty\n";
                errors++;
            }

            if (string.IsNullOrWhiteSpace(tb_addr.Text))
            {
                lbl_feedback.Text += "Address field is empty\n";
                errors++;
            }

            if (string.IsNullOrWhiteSpace(tb_addr2.Text))
            {
                lbl_feedback.Text += "Address 2 field is empty\n";
                errors++;
            }

            if (string.IsNullOrWhiteSpace(tb_city.Text))
            {
                lbl_feedback.Text += "City field is empty\n";
                errors++;
            }

            if (string.IsNullOrWhiteSpace(tb_state.Text))
            {
                lbl_feedback.Text += "State field is empty\n";
                errors++;
            }

            if (string.IsNullOrWhiteSpace(tb_zip.Text))
            {
                lbl_feedback.Text += "Zip field is empty\n";
                errors++;
            }

            if (string.IsNullOrWhiteSpace(tb_phone.Text))
            {
                lbl_feedback.Text += "Phone field is empty\n";
                errors++;
            }

            if (string.IsNullOrWhiteSpace(tb_email.Text))
            {
                lbl_feedback.Text += "Email field is empty\n";
                errors++;
            }

            return errors;
        }
    }
}