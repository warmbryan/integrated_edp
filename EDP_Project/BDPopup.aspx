<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticated.Master" AutoEventWireup="true" CodeBehind="BDPopup.aspx.cs" Inherits="EDP_Project.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
          <h1>Are you sure?</h1>
            <p>Please refresh your page after clicking yes</p>
            <asp:Button runat="server" Text="Yes" OnClick="Unnamed1_Click" width="80" Height="25"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
