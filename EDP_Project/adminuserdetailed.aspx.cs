using DBService.Models;
using EDP_Project.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDP_Project
{
    public partial class AdminUserDetailed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String email = Request.Params["email"];
            String purpose = Request.Params["purpose"];
            if (email != null)
            {
                Service1Client client = new Service1Client();
                if (purpose == "Customer")
                {
                    customerDetails.Visible = true;
                    CustomerClass tmpClass = client.SelectOneCustomer(email);
                    List<BlackListClass> tmpListClass = client.SelectAllBlacklist(email).ToList<BlackListClass>();
                    lbFirstName.Text = HttpUtility.HtmlEncode(tmpClass.FirstName);
                    lbLastName.Text = HttpUtility.HtmlEncode(tmpClass.LastName);
                    lbEmail.Text = HttpUtility.HtmlEncode(tmpClass.Email);
                    lbPhoneNumber.Text = HttpUtility.HtmlEncode(tmpClass.PhoneNumber);
                    lbDateOfBirth.Text = HttpUtility.HtmlEncode(tmpClass.DateOfBirth.ToString(@"dd/MM/yyyy"));
                    lbDeleted.Text = HttpUtility.HtmlEncode(tmpClass.delete.ToString());
                    lbDeletedDate.Text = HttpUtility.HtmlEncode(tmpClass.deleteDate.AddDays(30).ToString(@"dd/MM/yyyy"));
                    lbVerified.Text = HttpUtility.HtmlEncode(tmpClass.emailVerified.ToString());
                    lbBlacklisted.Text = HttpUtility.HtmlEncode(tmpClass.blackListed.ToString());
                    lbCreatedOn.Text = HttpUtility.HtmlEncode(tmpClass.createdAt.ToString(@"dd/MM/yyyy"));
                    if (tmpClass.delete)
                    {
                        lbDeletedDate.Text = HttpUtility.HtmlEncode(tmpClass.deleteDate.ToString(@"dd/MM/yyyy"));
                    }

                    if (tmpClass.blackListed)
                    {
                        divAddButton.Visible = false;
                    }

                    gvBlackList.Visible = true;
                    gvBlackList.DataSource = tmpListClass;
                    gvBlackList.DataBind();

                }
                else if (purpose == "Business")
                {
                    businessDetails.Visible = true;
                    BusinessUser tmpClass = client.GetBusinessUserByEmail(email);
                    List<BlackListClass> tmpListClass = client.SelectAllBlacklist(email).ToList<BlackListClass>();
                    lbFullName.Text = HttpUtility.HtmlEncode(tmpClass.Name);
                    lbEmail.Text = HttpUtility.HtmlEncode(tmpClass.Email);
                    lbDeleted.Text = HttpUtility.HtmlEncode(tmpClass.delete.ToString());
                    lbDeletedDate.Text = HttpUtility.HtmlEncode(tmpClass.deleteDate.AddDays(30).ToString(@"dd/MM/yyyy"));
                    lbVerified.Text = HttpUtility.HtmlEncode(tmpClass.emailVerified.ToString());
                    lbBlacklisted.Text = HttpUtility.HtmlEncode(tmpClass.blackListed.ToString());
                    lbCreatedOn.Text = HttpUtility.HtmlEncode(tmpClass.createdAt.ToString(@"dd/MM/yyyy"));
                    if (tmpClass.delete)
                    {
                        lbDeletedDate.Text = HttpUtility.HtmlEncode(tmpClass.deleteDate.ToString(@"dd/MM/yyyy"));
                    }

                    if (tmpClass.blackListed)
                    {
                        divAddButton.Visible = false;
                    }

                    gvBlackList.Visible = true;
                    gvBlackList.DataSource = tmpListClass;
                    gvBlackList.DataBind();
                }

                
            }
        }

        protected void AddBlackListBtn_Click(object sender, EventArgs e)
        {
            String email = Request.QueryString["Email"];
            if (email != null)
            {
                Service1Client client = new Service1Client();
                CustomerClass tmpClass = client.SelectOneCustomer(email);
                Int16 duration;
                Int16.TryParse(tbDuration.Text, out duration);
                String reason = (String)tbReason.Text.Trim();
                if (!String.IsNullOrEmpty(reason) && duration != 0 && tmpClass.blackListed != true)
                {
                    Int16 result = client.InsertOneBlacklist(duration, reason, tmpClass.Email, tmpClass.FirstName + tmpClass.LastName);
                    if (result != 1 || tmpClass.blackListed == true)
                    {
                        divError.Visible = false;
                        lbError.Text = "Insert failed";
                    }
                    else
                    {
                        result = client.UpdateCustomerStatus(tmpClass.ID, tmpClass.Email, "blackListedStatus", true);
                        if (result != 1 || tmpClass.blackListed == true)
                        {
                            divError.Visible = false;
                            lbError.Text = "Insert failed";
                        }
                        else
                        {
                            Response.Redirect("~/AdminUserDetailed?=" + email);
                        }
                    }
                }
            }

        }
    }
}