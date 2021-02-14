<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticated.Master" AutoEventWireup="true" CodeBehind="BDBusinesses.aspx.cs" Inherits="EDP_Project.BDBusinesses" %>
<asp:Content ID="stylesheetss" ContentPlaceHolderID="stylesheets" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/css/all.min.css" integrity="sha512-HK5fgLBL+xu6dm/Ii3z4xhlSUyZgTT9tuc/hSrtw6uzJOvgRr2a9jyxxT1ely+B+xFAmJKVSTbpM/CuL7qxO8w==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="d-flex justify-content-between">
        <h2>My Businesses</h2>
        <div class="my-auto">
            <asp:HyperLink NavigateUrl="/BDAddBusiness.aspx" runat="server" CssClass="btn btn-secondary" role="button">Add <i class="fas fa-plus"></i></asp:HyperLink>
        </div>
    </div>
    <hr />
    <form runat="server">
        <asp:GridView ID="gv_businesses" runat="server"></asp:GridView>
        <asp:ListView ID="lv_businesses" runat="server">
            <LayoutTemplate>
                <table class="table">
                    <thead>
                        <tr>
                            <th>Business Name</th>
                            <th>Business Registration Number</th>
                            <th>Business Type</th>
                            <th>Business Website URL</th>
                            <th>Approved by PinPoint</th>
                            <th></th>
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
                        <asp:Label ID="lbl_url" runat="server" Text='<%# Eval("Url") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Verified") %>' />
                    </td>
                    <td>
                        <button type="button" class="btn btn-link roles" id="<%# Eval("Id") %>">Link</button>
                        <asp:HyperLink NavigateUrl='<%# "~/BDEmployees.aspx?business=" + Eval("Id") %>' runat="server">View Employees</asp:HyperLink>
                        <asp:HyperLink NavigateUrl='<%# "~/BDUpdateBusiness.aspx?business=" + Eval("Id") %>' runat="server">Update</asp:HyperLink>
                        <asp:HyperLink NavigateUrl='<%# "~/BDDeleteBusiness.aspx?business=" + Eval("Id") %>' class="action-delete" runat="server">Delete</asp:HyperLink>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </form>
    <div id="roleModal" class="modal fade" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <table id="chinese">
                        <thead>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/js/all.min.js" integrity="sha512-UwcC/iaz5ziHX7V6LjSKaXgCuRRqbTp1QHpbOJ4l1nw2/boCfZ2KlFIqBUA/uRVF0onbREnY9do8rM/uT/ilqw==" crossorigin="anonymous"></script>
    <script>

        /*$(function () {
            
        })*/

        $('.roles').click(e => {
            var rolesModal = $('#roleModal');
            rolesModal.modal('show');

            var requestOptions = {
                headers: {
                    accept: 'application/json'
                }
            }

            fetch(`/api/businessrole?businessId=${e.target.id}`, requestOptions)
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                });
        });
    </script>
</asp:Content>
