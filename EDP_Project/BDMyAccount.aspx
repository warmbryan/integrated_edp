<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticated.Master" AutoEventWireup="true" CodeBehind="BDMyAccount.aspx.cs" Inherits="EDP_Project.BDMyAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
    <style>
        table tr th {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2>My Account</h2>
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="/BDHome.aspx">Home</a></li>
            <li class="breadcrumb-item active" aria-current="page">My Account</li>
        </ol>
    </nav>
    <hr />
    <form runat="server">
        <div class="mb-3">
            <label class="form-label" for="tb_name">Name</label>
            <asp:TextBox runat="server" ID="tb_name" CssClass="form-control" ReadOnly="true"/>
        </div>

        <div class="mb-3">
            <label class="form-label" for="tb_email">Email</label>
            <asp:TextBox runat="server" ID="tb_email" CssClass="form-control" ReadOnly="true"/>
        </div>

        <div class="mb-3">
            <label class="form-label" for="tb_phone">Phone Number</label>
            <asp:TextBox runat="server" ID="tb_phone" CssClass="form-control" ReadOnly="true"/>
        </div>
    </form>


    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
