<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/AdminDashboard.Master" AutoEventWireup="true" CodeBehind="AdminUsers.aspx.cs" Inherits="EDP_Project.AdminUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="extraHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p class="display-4">
        Users
    </p>
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><asp:HyperLink runat="server" NavigateUrl="~/AdminHome">Home</asp:HyperLink></li>
            <li class="breadcrumb-item active" aria-current="page">List of users</li>
        </ol>
    </nav>
    <div class="">
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
        <asp:ListView ID="lvUsers" runat="server">
            <LayoutTemplate>
                <table class="table" id="businesses">
                    <thead>
                        <tr>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Email</th>
                            <th>Created On</th>
                            <th>Deleting (T/F)</th>
                            <th>Blacklisted (T/F)</th>
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
                        <asp:Label ID="lbl_name" runat="server" Text='<%# Eval("FirstName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lbl_regNum" runat="server" Text='<%# Eval("LastName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lbl_type" runat="server" Text='<%# Eval("Email") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"createdAt","{0:d}") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("delete") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("blackListed") %>' />
                    </td>
                    <td>
                        <asp:HyperLink NavigateUrl='<%# "~/AdminUserDetailed.aspx?email=" + Eval("Email") + "&purpose=Customer" %>' runat="server" CssClass="btn-sm btn btn-link">More Details</asp:HyperLink>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        <asp:ListView ID="lvBusiness" runat="server">
            <LayoutTemplate>
                <table class="table" id="businesses">
                    <thead>
                        <tr>
                            <th>Full Name</th>
                            <th>Email</th>
                            <th>Created On</th>
                            <th>Deleting (T/F)</th>
                            <th>Blacklisted (T/F)</th>
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
                        <asp:Label ID="lbl_regNum" runat="server" Text='<%# Eval("Name") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lbl_type" runat="server" Text='<%# Eval("Email") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"createdAt","{0:d}") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("delete") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("blackListed") %>' />
                    </td>
                    <td>
                        <asp:HyperLink NavigateUrl='<%# "~/AdminUserDetailed.aspx?email=" + Eval("Email") + "&purpose=Business" %>' runat="server" CssClass="btn-sm btn btn-link">More Details</asp:HyperLink>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        <asp:ListView ID="lvAdmin" runat="server">
            <LayoutTemplate>
                <table class="table" id="businesses">
                    <thead>
                        <tr>
                            <th>Admin Name</th>
                            <th>User Name</th>
                            <th>Created On</th>
                            <th>Role</th>
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
                        <asp:Label ID="lbl_name" runat="server" Text='<%# Eval("AdminName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lbl_regNum" runat="server" Text='<%# Eval("UserName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CreatedAt","{0:d}") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Role") %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        <asp:GridView runat="server" ID="gvBusiness" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="3" CssClass="table table-responsive text-start" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
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
        <asp:GridView runat="server" ID="gvAdmin" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="3" CssClass="table table-responsive text-start" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
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
