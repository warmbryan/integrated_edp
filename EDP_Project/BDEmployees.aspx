<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticated.Master" AutoEventWireup="true" CodeBehind="BDEmployees.aspx.cs" Inherits="EDP_Project.BDEmployees" %>
<asp:Content ID="Content3" ContentPlaceHolderID="stylesheets" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/css/all.min.css" integrity="sha512-HK5fgLBL+xu6dm/Ii3z4xhlSUyZgTT9tuc/hSrtw6uzJOvgRr2a9jyxxT1ely+B+xFAmJKVSTbpM/CuL7qxO8w==" crossorigin="anonymous" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="d-flex justify-content-between">
        <h2>Business Employees</h2>
        <div class="my-auto">
            <a href="/BDAddEmployee?business=<%= Request.Params["business"] %>" role="button" class="btn btn-secondary">Add <i class="fas fa-plus"></i></a>
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
                        <asp:Label ID="lbl_regNum" runat="server" Text='<%# Eval("employee.email") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lbl_type" runat="server" Text='<%# ((bool)Eval("accepted")) ? "Yes" : "No" %>' />
                    </td>
                    <td>
                        <a href="/business/update/employee?id=<%# (string)Eval("id") %>" class="btn btn-secondary btn-sm" role="button">Update</a>
                        <button id="<%# (string)Eval("id") %>" class="btn btn-secondary btn-sm btn-delete-employee" type="button">Delete</button>
                    </td> 
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </form>
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalLabel"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <a id="btn-delete-employee" role="button" class="btn btn-danger" href="">Delete</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts" runat="server">
<script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/js/all.min.js" integrity="sha512-UwcC/iaz5ziHX7V6LjSKaXgCuRRqbTp1QHpbOJ4l1nw2/boCfZ2KlFIqBUA/uRVF0onbREnY9do8rM/uT/ilqw==" crossorigin="anonymous"></script>
<script>
    $('.btn-delete-employee').click(e => {
        var id = e.target.id;
        var deleteUrl = `/business/delete/employee?id=${id}`;

        var modal = $('.modal');
        modal.find('#modalLabel').text('Delete Employee Prompt');
        modal.find('.modal-body').text('Are you sure you want to delete this employee?');
        modal.find('#btn-delete-employee').attr('href', deleteUrl);

        modal.modal('show');
    })
</script>
</asp:Content>