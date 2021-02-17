<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticated.Master" AutoEventWireup="true" CodeBehind="BDInvitations.aspx.cs" Inherits="EDP_Project.BDInvitations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2>My invitations</h2>
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="/BDHome.aspx">Home</a></li>
            <li class="breadcrumb-item active" aria-current="page">Invitations</li>
        </ol>
    </nav>
    <hr />
    <form id="form1" runat="server">
        <asp:ListView runat="server" ID="lv_invitations">
            <LayoutTemplate>
                <table class="table">
                    <thead>
                        <tr>
                            <th>Busines Name</th>
                            <th>Registration Number</th>
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
                    <td><%# Eval("business.name")%></td>
                    <td><%# Eval("business.registrationNumber") %></td>
                    <td>
                        <div>
                            <a href="/business/my-invitations/accept?invite=<%# Eval("id")%>" role="button" class="btn btn-success btn-sm accept-invitation">Accept</a>
                            <a href="/business/my-invitations/reject?invite=<%# Eval("id")%>" role="button" class="btn btn-warning btn-sm reject-invitation">Reject</a>
                        </div>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </form>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
