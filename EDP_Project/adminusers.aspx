<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard.Master" AutoEventWireup="true" CodeBehind="AdminUsers.aspx.cs" Inherits="EDP_Project.AdminUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="extraHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; height: 400px; overflow: auto">
        <asp:GridView runat="server" ID="gvUsers" AutoGenerateColumns="False" CellPadding="4" CssClass="table table-responsive text-start" OnRowDataBound="gvUsers_RowDataBound" OnSelectedIndexChanged="gvUsers_SelectedIndexChanged" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <RowStyle Width="10%" />
            <Columns>
                <asp:BoundField DataField="FirstName" HeaderText="First Name" NullDisplayText="True" ReadOnly="True" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" ReadOnly="True" />
                <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="True" />
                <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" ReadOnly="True" />
                <asp:BoundField DataField="DateOfBirth" DataFormatString="{0:d}" HeaderText="Date Of Birth" />
                <asp:BoundField DataField="blackListed" HeaderText="Black Listed (T/F)" ReadOnly="True" />
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
    </div>
</asp:Content>
