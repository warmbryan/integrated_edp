<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard.Master" AutoEventWireup="true" CodeBehind="AdminListBusinesses.aspx.cs" Inherits="EDP_Project.AdminListBusinesses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="extraHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p class="display-4">
        Verify Businesses
    </p>
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><asp:HyperLink runat="server" NavigateUrl="~/AdminHome">Home</asp:HyperLink></li>
            <li class="breadcrumb-item active" aria-current="page">Verify Businesses</li>
        </ol>
    </nav>
    <asp:ListView ID="lv_businesses" runat="server">
        <LayoutTemplate>
            <table class="table" id="businesses">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Registration Number</th>
                        <th>Type</th>
                        <th>Approved</th>
                        <th>Options</th>
                    </tr>
                </thead>
                <tbody>
                    <tr runat="server" id="itemPlaceholder" />
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr runat="server">
                <td>
                    <asp:Label ID="lbl_name" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:Label ID="lbl_regNum" runat="server" Text='<%# Eval("RegistrationNumber") %>' />
                </td>
                <td>
                    <asp:Label ID="lbl_type" runat="server" Text='<%# Eval("Type") %>' />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Verified") %>' />
                </td>
                <td>
                    <asp:HyperLink NavigateUrl='<%# "~/AdminBusinessAcceptance.aspx?id=" + Eval("Id") %>' runat="server" CssClass="btn-sm btn btn-link">Validate</asp:HyperLink>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
