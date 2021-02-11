<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticated.Master" AutoEventWireup="true" CodeBehind="BDAddEmployee.aspx.cs" Inherits="EDP_Project.BDAddEmployee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2>Add an employee</h2>
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="/BDHome.aspx">Home</a></li>
            <li class="breadcrumb-item"><a href="/BDEmployees.aspx">Employees</a></li>
            <li class="breadcrumb-item active" aria-current="page">Add an employee</li>
        </ol>
    </nav>
    <hr />
    <div>
        <asp:Label ID="lbl_feedback" Text="" runat="server" ForeColor="Red" />
        <form runat="server">
            <div class="col-5">
                <div class="mb-3">
                    <label class="form-label" for="tb_email">Employees Email Address</label>
                    <asp:TextBox runat="server" ID="tb_email" CssClass="form-control"/>
                </div>

                <asp:ListView ID="lv_businesses" runat="server" Visible="false">
                    <LayoutTemplate>
                        <select class="form-select mb-3" aria-label="Default select example" name="business">
                            <option selected>Open this select menu</option>
                            <div runat="server" id="itemPlaceholder"></div>
                        </select>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <option value='<%# Eval("id") %>'><%# Eval("name") %></option>
                    </ItemTemplate>
                </asp:ListView>

                <div class="mb-3">
                    <div class="form-check form-switch">
                        <input class="form-check-input" type="checkbox" id="rApp" name="rApp">
                        <label class="form-check-label" for="rApp">Read Appointments</label>
                    </div>

                    <div class="form-check form-switch">
                        <input class="form-check-input" type="checkbox" id="wApp" name="wApp">
                        <label class="form-check-label" for="wApp">Write Appointments</label>
                    </div>

                    <div class="form-check form-switch">
                        <input class="form-check-input" type="checkbox" id="rCC" name="rCC">
                        <label class="form-check-label" for="rCC">Read Customer Chat (Read past customer conversations)</label>
                    </div>

                    <div class="form-check form-switch">
                        <input class="form-check-input" type="checkbox" id="wCC" name="wCC">
                        <label class="form-check-label" for="wCC">Write Customer Chat (Accept new customer conversation, Reply to customer, End conversation on active chats)</label>
                    </div>
                </div>

                <div class="mb-3">
                    <label class="form-label" for="tb_role">Employees Role</label>
                    <asp:TextBox runat="server" ID="tb_role" CssClass="form-control"/>
                </div>
                

                <asp:Button Text="Add employee" runat="server" CssClass="btn btn-secondary" OnClick="Add_Employee"/>
                
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
