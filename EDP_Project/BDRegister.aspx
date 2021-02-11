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
            <div class="mb-3">
                <label for="tb_name" class="form-label">Name</label>
                <asp:TextBox runat="server" ID="tb_name" CssClass="form-control" placeholder="Johnny Appleseed"/>
            </div>

            <div class="mb-3">
                <label for="tb_email" class="form-label">Email address</label>
                <asp:TextBox runat="server" ID="tb_email" CssClass="form-control" placeholder="name@example.com" TextMode="Email"/>
            </div>

            <div class="mb-3">
                <label for="tb_password" class="form-label">Password</label>
                <asp:TextBox runat="server" ID="tb_password" CssClass="form-control" TextMode="Password"/>
            </div>

            <div class="mb-3">
                <label for="tb_confirmPassword" class="form-label">Password Again</label>
                <asp:TextBox runat="server" ID="tb_confirmPassword" CssClass="form-control" TextMode="Password"/>
            </div>

            <div class="mb-3">
                <label for="tb_phone" class="form-label">Phone Number (include your country code e.g. +65)</label>
                <asp:TextBox runat="server" ID="tb_phone" CssClass="form-control" TextMode="Phone"/>
            </div>

            <div class="d-grid gap-2">
                <asp:Button Text="Register" runat="server" CssClass="btn btn-secondary" OnClick="Register_Me"/>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
