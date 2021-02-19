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
        
        <asp:GridView runat="server" ID="gvUsers" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="3" CssClass="table table-responsive text-start" OnRowDataBound="gvUsers_RowDataBound" OnSelectedIndexChanged="gvUsers_SelectedIndexChanged" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <RowStyle Width="100%" />
            <Columns>
                <asp:BoundField DataField="FirstName" HeaderText="First Name" NullDisplayText="True" ReadOnly="True" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" ReadOnly="True" />
                <asp:BoundField DataField="Email"  HeaderText="Email" ReadOnly="True" />
                <asp:BoundField DataField="PhoneNumber"  HeaderText="Phone Number" ReadOnly="True" />
                <asp:BoundField DataField="DateOfBirth"  DataFormatString="{0:d}" HeaderText="Date Of Birth" />
                <asp:BoundField DataField="blackListed"  HeaderText="Black Listed (T/F)" ReadOnly="True" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" HorizontalAlign="Left" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" HorizontalAlign="Left" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
        <asp:GridView runat="server" ID="gvBusiness" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="3" CssClass="table table-responsive text-start" OnRowDataBound="gvUsers_RowDataBound" OnSelectedIndexChanged="gvUsers_SelectedIndexChanged" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <RowStyle Width="100%" />
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" NullDisplayText="True" ReadOnly="True" />
                <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="True" />
                <asp:BoundField DataField="Phone"  HeaderText="Phone" ReadOnly="True" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" HorizontalAlign="Left" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" HorizontalAlign="Left" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
        <asp:GridView runat="server" ID="gvAdmin" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="3" CssClass="table table-responsive text-start" OnRowDataBound="gvUsers_RowDataBound" OnSelectedIndexChanged="gvUsers_SelectedIndexChanged" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <RowStyle Width="100%" />
            <Columns>
                <asp:BoundField DataField="AdminName" HeaderText="Admin Name" NullDisplayText="True" ReadOnly="True" />
                <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True" />
                <asp:BoundField DataField="CreatedAt" DataFormatString="{0:d}"  HeaderText="Created On" ReadOnly="True" />
                <asp:BoundField DataField="Role"  HeaderText="Role" ReadOnly="True" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" HorizontalAlign="Left" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" HorizontalAlign="Left" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
    </div>
</asp:Content>
