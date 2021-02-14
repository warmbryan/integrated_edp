<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticate.Master" AutoEventWireup="true" CodeBehind="BDRegister.aspx.cs" Inherits="EDP_Project.BDRegister" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="mx-auto" style="max-width: 400px;">
        <h2>Register for a business account</h2>
        <hr />

        <asp:ListView ID="lv_feedback" runat="server">
            <LayoutTemplate>
                <div class="alert alert-danger" role="alert">
                    <h4 class="alert-heading">Invalid data!</h4>
                    <ul class="mb-0">
                        <div id="itemPlaceholder" runat="server"></div>
                    </ul>
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <li><%# GetDataItem() %></li>
            </ItemTemplate>
        </asp:ListView>

        <form runat="server">
            <asp:Label ID="lbl_feedback" runat="server" ForeColor="Red"/>
            <div class="form-group">
                <label for="tb_name" class="form-label">Name</label>
                <asp:TextBox runat="server" ID="tb_name" CssClass="form-control" placeholder="Johnny Appleseed"/>
            </div>

            <div class="form-group">
                <label for="tb_email" class="form-label">Email address</label>
                <asp:TextBox runat="server" ID="tb_email" CssClass="form-control" placeholder="name@example.com" TextMode="Email"/>
            </div>

            <div class="form-group">
                <label for="tb_password" class="form-label">Password</label>
                <asp:TextBox runat="server" ID="tb_password" CssClass="form-control" TextMode="Password"/>
            </div>

            <div class="form-group">
                <label for="tb_confirmPassword" class="form-label">Password Again</label>
                <asp:TextBox runat="server" ID="tb_confirmPassword" CssClass="form-control" TextMode="Password"/>
            </div>

            <div class="form-group">
                <label for="tb_phone" class="form-label">Phone Number (include your country code e.g. +65)</label>
                <asp:TextBox runat="server" ID="tb_phone" CssClass="form-control" TextMode="Phone"/>
            </div>

            <asp:Button Text="Register" runat="server" CssClass="btn btn-primary btn-block" OnClick="Register_Me"/>
            <asp:HyperLink NavigateUrl="~/BDLogin" runat="server" Text="I have an account already." CssClass="btn btn-secondary btn-block"/>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
