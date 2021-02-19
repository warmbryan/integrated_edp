<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticated.Master" AutoEventWireup="true" CodeBehind="BDCreateBranch.aspx.cs" Inherits="EDP_Project.BDCreateBranch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2>Create a Branch</h2>
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="/business">Home</a></li>
            <li class="breadcrumb-item"><a href="/business/my-businesses">Business</a></li>
            <li class="breadcrumb-item"><asp:HyperLink ID="hl_branch" NavigateUrl="/business" runat="server" /></li>
            <li class="breadcrumb-item active" aria-current="page">Create</li>
        </ol>
    </nav>
    <hr />
    <asp:Label ID="lbl_feedback" Text="" runat="server" ForeColor="Red" />
    <div class="form-group">
        <label>Name</label>
        <asp:TextBox ID="tb_name" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Description</label>
        <asp:TextBox ID="tb_desc" runat="server" TextMode="MultiLine" CssClass="form-control" />
    </div>


    <div class="form-group">
        <label>Address</label>
        <asp:TextBox ID="tb_addr" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Address 2</label>
        <asp:TextBox ID="tb_addr2" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>City</label>
        <asp:TextBox ID="tb_city" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>State</label>
        <asp:TextBox ID="tb_state" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Zip</label>
        <asp:TextBox ID="tb_zip" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Country</label>
        <asp:TextBox ID="tb_country" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Country</label>
        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Phone</label>
        <asp:TextBox ID="tb_phone" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <label>Email address for contact</label>
        <asp:TextBox ID="tb_email" runat="server" CssClass="form-control" />
    </div>

    <div class="form-group">
        <asp:CheckBox ID="cb_mainBranch" Text="This is a main branch" runat="server" CssClass="form-check-input" />
    </div>

    <asp:Button Text="Add" runat="server" CssClass="btn btn-primary btn-block" OnClick="Add_Branch" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
