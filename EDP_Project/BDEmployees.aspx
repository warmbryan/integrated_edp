<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticated.Master" AutoEventWireup="true" CodeBehind="BDEmployees.aspx.cs" Inherits="EDP_Project.BDEmployees" %>
<asp:Content ID="Content3" ContentPlaceHolderID="stylesheets" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/css/all.min.css" integrity="sha512-HK5fgLBL+xu6dm/Ii3z4xhlSUyZgTT9tuc/hSrtw6uzJOvgRr2a9jyxxT1ely+B+xFAmJKVSTbpM/CuL7qxO8w==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="d-flex justify-content-between">
        <h2>Business Employees</h2>
        <div class="my-auto">
            <asp:HyperLink NavigateUrl="/BDAddEmployee.aspx" runat="server" CssClass="btn btn-secondary" role="button">Add <i class="fas fa-plus"></i></asp:HyperLink>
        </div>
    </div>
    <hr />
    <h4>Employees in <asp:Label ID="lbl_businessName" Text="" runat="server" /></h4>
    <asp:Label ID="lbl_feedback" Text="" runat="server" Visible="false"/>
    <form runat="server">
        <asp:ListView ID="lv_employees" runat="server">
            <LayoutTemplate>
                <table class="table">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Email</th>
                            <th>Accepted</th>
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
                        <asp:Label ID="lbl_name" runat="server" Text='<%# ((bool)Eval("accepted")) ? Eval("employee.name") : "Redacted until employee accepts" %>' />
                    </td>
                    <td>
                        <asp:Label ID="lbl_regNum" runat="server" Text='<%# Eval("employee.email") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lbl_type" runat="server" Text='<%# ((bool)Eval("accepted")) ? "Yes" : "No" %>' />
                    </td>
                    <td>
                        <button id="">Delete</button>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" runat="server">
<script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/js/all.min.js" integrity="sha512-UwcC/iaz5ziHX7V6LjSKaXgCuRRqbTp1QHpbOJ4l1nw2/boCfZ2KlFIqBUA/uRVF0onbREnY9do8rM/uT/ilqw==" crossorigin="anonymous"></script>
<script>
</script>
</asp:Content>