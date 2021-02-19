<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard.Master" AutoEventWireup="true" CodeBehind="AdminUserDetailed.aspx.cs" Inherits="EDP_Project.AdminUserDetailed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="extraHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><asp:HyperLink runat="server" NavigateUrl="~/AdminHome">Home</asp:HyperLink></li>
            <li class="breadcrumb-item"><asp:HyperLink runat="server" NavigateUrl="~/AdminUsers">List of users</asp:HyperLink></li>
            <li class="breadcrumb-item active" aria-current="page">User</li>
        </ol>
    </nav>
    
    <div class="alert alert-danger" runat="server" id="divError" visible="false">
        <asp:Label runat="server" ID="lbError" />
    </div>
    <div class="row">
        <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
            Created On: 
        </div>
        <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
            <asp:Label runat="server" ID="lbCreatedOn" />
        </div>
    </div>
    <div class="row">
        <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
            Email: 
        </div>
        <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
            <asp:Label runat="server" ID="lbEmail" />
        </div>
    </div>
    <div runat="server" id="customerDetails" visible="false">
        <div class="row">
            <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
                First Name: 
            </div>
            <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
                <asp:Label runat="server" ID="lbFirstName" />
            </div>
        </div>
        <div class="row">
            <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
                Last Name: 
            </div>
            <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
                <asp:Label runat="server" ID="lbLastName" />
            </div>
        </div>
        <div class="row">
            <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
                Phone Number: 
            </div>
            <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
                <asp:Label runat="server" ID="lbPhoneNumber" />
            </div>
        </div>
        <div class="row">
            <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
                Date Of Birth:   
            </div>
            <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
                <asp:Label runat="server" ID="lbDateOfBirth" />
            </div>
        </div>
    </div>
    <div runat="server" id="businessDetails" visible="false">
        <div class="row">
            <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
                Full Name: 
            </div>
            <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
                <asp:Label runat="server" ID="lbFullName" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
            Deleted (T/F): 
        </div>
        <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
            <asp:Label runat="server" ID="lbDeleted" />
        </div>
    </div>
    <div class="row" runat="server">
        <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
            Delete Date: 
        </div>
        <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
            <asp:Label runat="server" ID="lbDeletedDate" />
        </div>
    </div>
    <div class="row" runat="server">
        <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
            Verified: 
        </div>
        <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
            <asp:Label runat="server" ID="lbVerified" />
        </div>
    </div>
    <div class="row" runat="server">
        <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
            Blacklisted: 
        </div>
        <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
            <asp:Label runat="server" ID="lbBlacklisted" />
        </div>
    </div>
    
    <asp:GridView runat="server" ID="gvBlackList" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
        <alternatingrowstyle backcolor="#CCCCCC" />
        <columns>
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False" />
            <asp:BoundField DataField="CreatedAt" DataFormatString="{0:d}" HeaderText="Created On" ReadOnly="True" />
            <asp:BoundField DataField="EndedAt" DataFormatString="{0:d}" HeaderText="End On" ReadOnly="True" />
            <asp:BoundField DataField="Reason" HeaderText="Reason" ReadOnly="True" />
            <asp:BoundField DataField="Deleted" HeaderText="Obsoleted (T/F)" ReadOnly="True" />
        </columns>
        <footerstyle backcolor="#CCCCCC" />
        <headerstyle backcolor="Black" font-bold="True" forecolor="White" />
        <pagerstyle backcolor="#999999" forecolor="Black" horizontalalign="Center" />
        <selectedrowstyle backcolor="#000099" font-bold="True" forecolor="White" />
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="Gray" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#383838" />

    </asp:GridView>
    <div style="text-align: right" runat="server" id="divAddButton">
        <button type="button" id="addBlackList" class="btn btn-primary">Add blacklist record</button>
        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                    <button type="button" class="close closeBlackList" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-12 col-sm-12 col-md-7 col-lg-4 col-xl-4 d-flex align-items-center justify-content-end" style="text-align: right;">
                            <asp:Label runat="server" ID="lbUserName" Text="Duration: " />
                        </div>
                        <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8">
                            <asp:TextBox runat="server" TextMode="Number" placeholder="Duration" ID="tbDuration" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-sm-12 col-md-7 col-lg-4 col-xl-4 d-flex align-items-center justify-content-end" style="text-align: right;">
                            <asp:Label runat="server" ID="Label1" Text="Reason: " />
                        </div>
                        <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8">
                            <asp:TextBox runat="server" TextMode="MultiLine" Rows="3" placeholder="Reason" ID="tbReason" CssClass="form-control" MaxLength="200" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary closeBlackList" data-dismiss="modal">Close</button>
                    <asp:Button runat="server" ID="AddBlackListBtn" Visible="true" OnClick="AddBlackListBtn_Click" CssClass="btn btn-success" Text="Add blacklist record" />

                </div>
            </div>
        </div>
    </div>
    </div>
    <script>
        $("#addBlackList").click(function () {
            $('#exampleModal').modal('toggle')
        });
        $(".closeBlackList").click(function () {
            $('#exampleModal').modal('toggle')
        });
    </script>
</asp:Content>
