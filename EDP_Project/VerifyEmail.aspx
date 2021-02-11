<%@ Page Title="" Language="C#" MasterPageFile="~/WRSite.Master" AutoEventWireup="true" CodeBehind="VerifyEmail.aspx.cs" Inherits="EDP_Project.VerifyEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="extraHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div runat="server" visible="true" id="successAlert" class="alert alert-success">
        Your email has been verified, <a runat="server" href="~/Customer/Login">login now!</a>
    </div>
    <div runat="server" visible="false" id="dangerAlert" class="alert alert-danger">
        There was an error verifying your email, please try again! <asp:Button Text="Retry" runat="server" CssClass="btn btn-primary" ID="retry" OnClick="retry_Click"/>
    </div>
    <div runat="server" visible="false" id="ErrorMessage" class="alert alert-danger">
        <asp:Label runat="server" ID="ErrorMessageLB" />
    </div>
</asp:Content>
