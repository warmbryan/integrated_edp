using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EDP_Project.ServiceReference1;

namespace EDP_Project
{
	public partial class ClientHistory : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("ClientSearch.aspx", false);
            }
            else
            {
                if (!IsPostBack)
                {
                    populate_listview();
                }
            }

        }

        protected override void OnPreRender(EventArgs e)
        {
            populate_listview();

            base.OnPreRender(e);
        }

        protected void populate_listview()
        {
            DataSet ds = client.SelectByCustomerIdFromView(Guid.Parse(Session["userId"].ToString()));
            ListViewViewHistory.DataSource = ds;
            ListViewViewHistory.DataBind();

            DateTime previousDate = DateTime.MinValue;
            foreach (ListViewItem li in ListViewViewHistory.Items)
            {
                Label lblDate = (Label)li.FindControl("lblDate");
                Label lblDatetime = (Label)li.FindControl("lblViewDateTime");

                if (DateTime.Parse(lblDatetime.Text).Date != previousDate.Date)
                {
                    lblDate.Text = DateTime.Parse(lblDatetime.Text).ToString("dddd, dd MMMM yyyy");
                    lblDate.Visible = true;
                }

                previousDate = DateTime.Parse(lblDatetime.Text);

            }

            ds = client.SelectByCustomerIdFromSearch(Guid.Parse(Session["userId"].ToString()));
            ListViewSearchHistory.DataSource = ds;
            ListViewSearchHistory.DataBind();

            previousDate = DateTime.MinValue;
            foreach (ListViewItem li in ListViewSearchHistory.Items)
            {
                Label lblDate = (Label)li.FindControl("lblDate");
                Label lblDatetime = (Label)li.FindControl("lblSearchDateTime");

                if (DateTime.Parse(lblDatetime.Text).Date != previousDate.Date)
                {
                    lblDate.Text = DateTime.Parse(lblDatetime.Text).ToString("dddd, dd MMMM yyyy");
                    lblDate.Visible = true;
                }

                previousDate = DateTime.Parse(lblDatetime.Text);
            }
        }

        protected void RdgroupHistoryType_CheckedChanged(object sender, EventArgs e)
        {
            if (RdbtnViewHistory.Checked)
            {
                lblType.Text = "View History";
                ListViewViewHistory.Visible = true;
                ListViewSearchHistory.Visible = false;
                //PanelReviewHistory.Visible = false;
            }

            if (RdbtnSearchHistory.Checked)
            {

                lblType.Text = "Search History";
                ListViewViewHistory.Visible = false;
                ListViewSearchHistory.Visible = true;
                //PanelReviewHistory.Visible = false;

            }

            //if (RdbtnReviewHistory.Checked)
            //{
            //    lblType.Text = "Review History";
            //    PanelViewHistory.Visible = false;
            //    PanelSearchHistory.Visible = false;
            //    PanelReviewHistory.Visible = true;
            //}
        }

        protected void ButtonMoreInfomation_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (Session["userId"] != null)
            {
                int id = client.HaveDateFromView(Guid.Parse(btn.CommandArgument.ToString()), Guid.Parse(Session["userId"].ToString()));
                if (id == 0)
                {
                    client.InsertView(Guid.Parse(btn.CommandArgument.ToString()), Guid.Parse(Session["userId"].ToString()));
                }
                else
                {
                    client.UpdateView(id);
                }

            }

            //Response.Redirect("moreInfo?id=" + btn.CommandArgument.ToString());
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientSearch.aspx");
        }

        protected void LinkButtonClear_Click(object sender, EventArgs e)
        {
            if (RdbtnViewHistory.Checked)
            {
                client.DeleteFromView(Guid.Parse(Session["userId"].ToString()));
            }

            if (RdbtnSearchHistory.Checked)
            {
                client.DeleteFromSearch(Guid.Parse(Session["userId"].ToString()));
            }
            //            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}