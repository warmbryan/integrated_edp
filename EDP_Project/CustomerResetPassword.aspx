<%@ Page Title="" Language="C#" MasterPageFile="~/WRSite.Master" AutoEventWireup="true" CodeBehind="CustomerResetPassword.aspx.cs" Inherits="EDP_Project.CustomerResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="extraHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-12 col-sm-12 col-md-3 col-lg-3 col-xl-3"></div>
        <div class="col-12 col-sm-12 col-md-6 col-lg-6 col-xl-6">
            <div class="card">
                <div class="card-header">
                    <div class="text-center mb-3" style="font-size: 2.0rem;">
                        Login to PinPoint
                    </div>
                </div>
                <div class="card-body">
                    <div class="form-group">
                            <div class="row">
                                <div class="col-12 col-sm-12 col-md-4 col-lg-4 col-xl-4 d-flex align-items-center justify-content-end" style="text-align:right;">
                                    <asp:Label runat="server" Text="Email:" />
                                </div>
                                <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8">
                                    <asp:TextBox runat="server" TextMode="Email" placeholder="First Name" ID="tbEmail" CssClass="form-control" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbEmail" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Email is required!" />
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="tbEmail" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Email is not in the correct format" ValidationExpression="^\w+[\+\.\w-]*@([\w-]+\.)*\w+[\w-]*\.([a-z]{2,4}|\d+)$" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-12 col-sm-12 col-md-4 col-lg-4 col-xl-4 d-flex align-items-center justify-content-end" style="text-align:right;">
                                    <asp:Label runat="server" Text="Password:" />
                                </div>
                                <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8">
                                    <asp:TextBox runat="server" TextMode="Password" placeholder="Last Name" ID="tbPassword" CssClass="form-control" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPassword" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Password is required!" />
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="tbPassword" ForeColor="Red" Font-Bold="true" CssClass="incorrect" ErrorMessage="  Invalid password!" Display="Dynamic" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,20}" />
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
                <div class="card-footer text-center">
                    <p class="text-muted">Already have an account? <a runat="server" href="~/Customer/Registration">Click here</a></p>
                </div>
            </div>
        </div>
        <div class="col-12 col-sm-12 col-md-3 col-lg-3 col-xl-3"></div>
    </div>
</asp:Content>
