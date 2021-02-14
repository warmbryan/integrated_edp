<%@ Page Title="" Language="C#" MasterPageFile="~/WRCustomerLogin.Master" AutoEventWireup="true" CodeBehind="CustomerLogin.aspx.cs" Inherits="EDP_Project.CustomerLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="extraHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-12 col-sm-12 col-md-1 col-lg-3 col-xl-3"></div>
        <div class="col-12 col-sm-12 col-md-10 col-lg-6 col-xl-6">
            <div class="alert alert-danger" runat="server" id="divErrorMsg" visible="false" role="alert">
                <asp:Label runat="server" ID="lbErrorMsg" />
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="mb-3" style="font-size: 2.0rem;">
                        Login to PinPoint
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <ul class="nav nav-tabs">
                                <li class="nav-item">
                                    <asp:Button runat="server" CssClass="nav-link active" Text="Customer" CausesValidation="false" ID="CustomerSide" OnClick="CustomerSide_Click"/>
                                </li>
                                <li class="nav-item">
                                    <asp:Button runat="server" CssClass="nav-link" Text="Staff" CausesValidation="false" ID="BusinessSide" OnClick="BusinessSide_Click"/>
                                </li>
                                <li class="nav-item">
                                    <asp:Button runat="server" CssClass="nav-link" Text="Admin" CausesValidation="false" ID="AdminSide" OnClick="AdminSide_Click"/>
                                </li>
                            </ul>
                            <asp:TextBox runat="server" Visible="false" ID="Submit_Role_Value" TextMode="Number" Text="0"/>
                            <div runat="server">
                                <div class="card-body">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-12 col-sm-12 col-md-7 col-lg-4 col-xl-4 d-flex align-items-center justify-content-end" style="text-align:right;">
                                                <asp:Label runat="server" ID="lbUserName" Text="Email:" />
                                            </div>
                                            <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8">
                                                <asp:TextBox runat="server" TextMode="Email" placeholder="Email" ID="tbUsername" CssClass="form-control" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbUsername" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Email is required!" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-12 col-sm-12 col-md-4 col-lg-4 col-xl-4 d-flex align-items-center justify-content-end" style="text-align:right;">
                                                <asp:Label runat="server" Text="Password:" />
                                            </div>
                                            <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8">
                                                <asp:TextBox runat="server" TextMode="Password" placeholder="Password" ID="tbPassword" CssClass="form-control" />
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPassword" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Password is required!" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-12 col-sm-12 col-md-4 col-lg-4 col-xl-4" style="text-align:right;">
                                            </div>
                                            <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8 d-flex justify-content-between">
                                                <div>
                                                    <asp:CheckBox runat="server" Text="&nbspRemember Me" ID="cbRememberMe"/>
                                                </div>
                                                <a href="#register" id="loginHelp">Have trouble logging in?</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="text-right">
                                        <button type="button" class="btn btn-secondary">Close</button>
                                        <asp:Button runat="server" CssClass="btn btn-primary" Text="Login" ID="submit" OnClick="submit_Click"/>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                   </asp:UpdatePanel>
                </div>
                <div class="card-footer text-center">
                    <p class="text-muted">Already have an account? <a runat="server" href="~/Customer/Registration">Click here</a></p>
                </div>
            </div>
        </div>
        <div class="col-12 col-sm-12 col-md-1 col-lg-3 col-xl-3"></div>
    </div>
    <script>
        $("#<%: CustomerSide.ClientID %>").click(function () {
            $("#<%: CustomerSide.ClientID %>").addClass("active");
            $("#<%: BusinessSide.ClientID %>").removeClass("active");
            $("#<%: AdminSide.ClientID %>").removeClass("active");
            $("#<%: lbUserName.ClientID%>").text("Email:")
            $("#<%: tbUsername.ClientID %>").prop("type", "email");
            return true;
        });
        $("#<%: BusinessSide.ClientID %>").click(function () {
            $("#<%: CustomerSide.ClientID %>").removeClass("active");
            $("#<%: BusinessSide.ClientID %>").addClass("active");
            $("#<%: AdminSide.ClientID %>").removeClass("active");
            $("#<%: lbUserName.ClientID%>").text("Username:")
            $("#<%: tbUsername.ClientID %>").prop("type", "text");
            return true;
        });
        $("#<%: AdminSide.ClientID %>").click(function () {
            $("#<%: CustomerSide.ClientID %>").removeClass("active");
            $("#<%: BusinessSide.ClientID %>").removeClass("active");
            $("#<%: AdminSide.ClientID %>").addClass("active");
            $("#<%: lbUserName.ClientID%>").text("Username:")
            $("#<%: tbUsername.ClientID %>").prop("type", "text");
            return true;
        });
    </script>
</asp:Content>
