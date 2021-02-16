<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticated.Master" AutoEventWireup="true" CodeBehind="BDUpdateBusiness.aspx.cs" Inherits="EDP_Project.BDUpdateBusiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2>Add an employee</h2>
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="/BDHome.aspx">Home</a></li>
            <li class="breadcrumb-item"><a href="/BDBusinesses.aspx">Businesses</a></li>
            <li class="breadcrumb-item active" aria-current="page">Update Business Details</li>
        </ol>
    </nav>
    <hr />
    <form runat="server">
        <asp:ListView ID="lv_feedback" runat="server" Visible="false">
            <LayoutTemplate>
                <div class="alert alert-danger" role="alert">
                    <h4 class="alert-heading">There are invalid fields, please correct it.</h4>
                    <ul class="mb-0">
                        <div id="itemPlaceholder" runat="server"></div>
                    </ul>
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <li><%# GetDataItem() %></li>
            </ItemTemplate>
        </asp:ListView>

        <h4>Business Information</h4>
        <p>Field with asterisk are required fields.</p>
        <div class="mb-3">
            <label class="form-label" for="tb_businessName">Name*</label>
            <asp:TextBox ID="tb_businessName" runat="server" CssClass="form-control" placeholder="Business Name" required></asp:TextBox>
        </div>

        <div class="mb-3">
            <label class="form-label" for="tb_businessRegNum">Registration Number*</label>
            <asp:TextBox ID="tb_businessRegNum" runat="server" CssClass="form-control" placeholder="Business Name"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label class="form-label" for="tb_businessType">Type*</label>
            <asp:TextBox ID="tb_businessType" runat="server" CssClass="form-control" placeholder="Business Type"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label class="form-label" for="tb_businessUrl">Website URL</label>
            <asp:TextBox ID="tb_businessUrl" runat="server" CssClass="form-control" placeholder="Business Name"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label class="form-label" for="logoId">Business Logo (Max 2MB)</label>
            <asp:FileUpload ID="file_logoId" runat="server" CssClass="form-control-file" />
            <small>File format must be in png or jpeg. Logo Photo can also be uploaded after registration.</small>
        </div>

        <div class="mb-3">
            <label class="form-label" for="acraRegistration">Registration Document (Max 8MB)</label>
            <asp:FileUpload ID="file_acraRegistration" runat="server" CssClass="form-control-file" />
            <small>File format must be in pdf document.</small>
        </div>

        <div class="mb-3 form-check">
            <input class="form-check-input" type="checkbox" value="" id="agreement" runat="server" required>
            <label class="form-check-label" for="flexCheckDefault">
                I have read the terms of service and the information I provided are accurate to my best abilities.
            </label>
        </div>

        <asp:Button ID="btnSubmit" runat="server" Text="Submit Registration Form" CssClass="btn btn-block btn-primary" OnClick="Update_Business" />
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
