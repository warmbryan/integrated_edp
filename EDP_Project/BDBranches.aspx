<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticated.Master" AutoEventWireup="true" CodeBehind="BDBranches.aspx.cs" Inherits="EDP_Project.BDBranches" %>
<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2>
        <asp:Label ID="lbl_header" Text="text" runat="server" />
    </h2>
    <asp:ListView ID="lv_branches" runat="server">
        <LayoutTemplate>
            <table class="table">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Address</th>
                        <th>Options</th>
                    </tr>
                </thead>
                <tbody>
                    <tr runat="server" id="itemPlaceholder"></tr>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr runat="server">
                <td><%# Eval("shopname") %></td>
                <td><%# Eval("address") %></td>
                <td>
                    <asp:Button ID="btn_updateBranch" Text="Delete" runat="server" CssClass="btn btn-danger btn-sm" />
                    <asp:Button ID="btn_deleteBranch" Text="Delete" runat="server" CssClass="btn btn-danger btn-sm" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
