<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard.Master" AutoEventWireup="true" CodeBehind="AdminBusinessAcceptance.aspx.cs" Inherits="EDP_Project.AdminBusinessAcceptance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="extraHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><asp:HyperLink runat="server" NavigateUrl="~/AdminHome">Home</asp:HyperLink></li>
            <li class="breadcrumb-item"><asp:HyperLink runat="server" NavigateUrl="~/AdminListBusinesses">Verify Businesses</asp:HyperLink></li>
            <li class="breadcrumb-item active" aria-current="page">Business</li>
        </ol>
    </nav>
    <div class="text-left">
        <asp:HyperLink runat="server" NavigateUrl="~/AdminListBusinesses"><i class="fas fa-angle-left"></i> Back </asp:HyperLink>
    </div>
    <div class="row">
        <div class="col-12 col-sm-12 col-md-3 col-lg-3 col-xl-3">
            <asp:Image runat="server" ID="imgBusinessLogo" AlternateText="Business Logo" CssClass="img-thumbnail"/>
        </div>
        <div class="col-12 col-sm-12 col-md-9 col-lg-9 col-xl-9">
            <p>Name: <asp:Label runat="server" ID="lbName" /></p>
            <p>Registration Number: <asp:Label runat="server" ID="lbRegistrationNumber" /></p>
            <a>Url: <asp:Label runat="server" ID="lbUrl" /></a>
            <asp:HyperLink runat="server" ID="urlLink" Target="_blank">URL <i class="fas fa-external-link-alt"></i></asp:HyperLink>
            <p>Type: <asp:Label runat="server" ID="lbType" /></p>
            <p>Registered By: <asp:Label runat="server" ID="lbRegisteredBy" /></p>
        </div>
    </div>
    <div class="text-right">
        <asp:Button runat="server" ID="update_business_verified" OnClick="update_business_verified_Click" CssClass="btn btn-secondary" Text="Validate" />
    </div>
    <asp:Image runat="server" ID="testImage" AlternateText="Business Logo" CssClass="img-thumbnail"/>
</asp:Content>
