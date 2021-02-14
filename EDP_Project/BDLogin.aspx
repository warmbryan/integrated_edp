<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticate.Master" AutoEventWireup="true" CodeBehind="BDLogin.aspx.cs" Inherits="EDP_Project.BDLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="mx-auto" style="max-width: 400px;">
        <h2>Business Dashboard Login</h2>
        <hr />
        <asp:Label ID="lbl_feedback" runat="server" ForeColor="Red"/>
        <form runat="server">
            <div class="form-group">
                <label for="tb_email" class="form-label">Email address</label>

                <asp:TextBox runat="server" ID="tb_email" CssClass="form-control" placeholder="name@example.com" TextMode="Email"/>
            </div>

            <div class="form-group">
                <label for="tb_password" class="form-label">Password</label>
                <asp:TextBox runat="server" ID="tb_password" CssClass="form-control" TextMode="Password"/>
            </div>

            <asp:Button Text="Submit" runat="server" CssClass="btn btn-primary btn-block" OnClick="Login_Me"/>
            <asp:HyperLink NavigateUrl="~/BDRegister" runat="server" Text="Register" CssClass="btn btn-secondary btn-block"/>

            <!--
            <p class="mt-2 mb-1">Forgotten your credentials?</p>
            <asp:HyperLink NavigateUrl="~/BDHome.aspx" runat="server" Text="Reset my password" CssClass="btn btn-secondary btn-block"/>
                -->
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
