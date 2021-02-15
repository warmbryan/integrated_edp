<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/AdminDashboard.Master" AutoEventWireup="true" CodeBehind="AdminUsers.aspx.cs" Inherits="EDP_Project.AdminUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="extraHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="">
        <p class="display-4">
            Users
        </p>
        <div class="d-flex justify-content-center align-items-center mb-3">
            <style>
                .no-wrap{
                    white-space:nowrap;
                }
            </style>
            <asp:Label runat="server" Text="Filter by:&nbsp&nbsp" CssClass="no-wrap" AssociatedControlID="dpMenu"></asp:Label>
            <asp:DropDownList runat="server" ID="dpMenu" CssClass="custom-select">
                <asp:ListItem Enabled="true" Selected="True" Value="0" Text="Customer"></asp:ListItem>
                <asp:ListItem Enabled="true" Value="1" Text="Business"></asp:ListItem>
                <asp:ListItem Enabled="true" Value="2" Text="Admin"></asp:ListItem>
            </asp:DropDownList>
            &nbsp
            <asp:Button runat="server" CssClass="btn btn-secondary" Text="Filter" ID="filterBtn" OnClick="filterBtn_Click" />
        </div>
    </div>
    <div style="text-align:center; width: 100%; height: 400px; overflow: auto;">
        <asp:GridView runat="server" ID="gvUsers" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="4" CssClass="table table-responsive text-start" OnRowDataBound="gvUsers_RowDataBound" OnSelectedIndexChanged="gvUsers_SelectedIndexChanged" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <RowStyle Width="100%" />
            <Columns>
                <asp:BoundField DataField="FirstName" HeaderText="First Name" NullDisplayText="True" ReadOnly="True" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" ReadOnly="True" />
                <asp:BoundField DataField="Email"  HeaderText="Email" ReadOnly="True" />
                <asp:BoundField DataField="PhoneNumber"  HeaderText="Phone Number" ReadOnly="True" />
                <asp:BoundField DataField="DateOfBirth"  DataFormatString="{0:d}" HeaderText="Date Of Birth" />
                <asp:BoundField DataField="blackListed"  HeaderText="Black Listed (T/F)" ReadOnly="True" />
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" HorizontalAlign="Left" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" HorizontalAlign="Left" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
        <asp:GridView runat="server" ID="gvBusiness" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="4" CssClass="table table-responsive text-start" OnRowDataBound="gvUsers_RowDataBound" OnSelectedIndexChanged="gvUsers_SelectedIndexChanged" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <RowStyle Width="100%" />
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" NullDisplayText="True" ReadOnly="True" />
                <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="True" />
                <asp:BoundField DataField="Phone"  HeaderText="Phone" ReadOnly="True" />
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" HorizontalAlign="Left" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" HorizontalAlign="Left" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
        <asp:GridView runat="server" ID="gvAdmin" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="4" CssClass="table table-responsive text-start" OnRowDataBound="gvUsers_RowDataBound" OnSelectedIndexChanged="gvUsers_SelectedIndexChanged" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <RowStyle Width="100%" />
            <Columns>
                <asp:BoundField DataField="AdminName" HeaderText="Admin Name" NullDisplayText="True" ReadOnly="True" />
                <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True" />
                <asp:BoundField DataField="CreatedAt" DataFormatString="{0:d}"  HeaderText="Created On" ReadOnly="True" />
                <asp:BoundField DataField="Role"  HeaderText="Role" ReadOnly="True" />
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" HorizontalAlign="Left" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" HorizontalAlign="Left" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
    </div>
</asp:Content>
