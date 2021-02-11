<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticate.Master" AutoEventWireup="true" CodeBehind="BDLogin.aspx.cs" Inherits="EDP_Project.BDLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="mx-auto" style="max-width: 400px;">
        <h2>Business Dashboard Login</h2>
        <hr />
        <asp:Label ID="lbl_feedback" runat="server" ForeColor="Red"/>
        <form runat="server">
            <div class="mb-3">
                <label for="tb_email" class="form-label">Email address</label>

                <asp:TextBox runat="server" ID="tb_email" CssClass="form-control" placeholder="name@example.com" TextMode="Email"/>
            </div>

            <div class="mb-3">
                <label for="tb_password" class="form-label">Password</label>
                <asp:TextBox runat="server" ID="tb_password" CssClass="form-control" TextMode="Password"/>
            </div>

            <div class="d-grid gap-2">
                <asp:Button Text="Submit" runat="server" CssClass="btn btn-secondary" OnClick="Login_Me"/>
                <asp:HyperLink NavigateUrl="~/BDRegister.aspx" runat="server">Register for an account.</asp:HyperLink>
            </div>

            <div class="py-3">
                <p>Forgotten your credentials?</p>
                <div class="d-grid gap-2">
                    <asp:HyperLink NavigateUrl="~/BDHome.aspx" runat="server" Text="Reset my password" CssClass="btn btn-secondary"/>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
