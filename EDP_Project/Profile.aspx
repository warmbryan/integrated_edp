<%@ Page Title="" Language="C#" MasterPageFile="~/WRCustomerLogin.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="EDP_Project.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="extraHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-body">
            <div runat="server" visible="false" id="errorDiv" class="alert alert-danger" role="alert">
                        <asp:Label runat="server" ID="lbErrorMsg" ForeColor="Red"/>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align:right;">
                                <asp:Label runat="server">First Name: <span style="color:red">*</span></asp:Label>
                            </div>
                            <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8">
                                <asp:TextBox runat="server" TextMode="SingleLine" placeholder="First Name" ID="tbFirstName" CssClass="form-control" MaxLength="100"/>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbFirstName" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" First name is required!" />
                            </div>
                        </div>
                    </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align:right;">
                                    <asp:Label runat="server">Last Name: <span style="color:red">*</span></asp:Label>
                                </div>
                                <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8">
                                    <asp:TextBox runat="server" TextMode="SingleLine" placeholder="Last Name" ID="tbLastName" CssClass="form-control" MaxLength="100"/>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbLastName" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Last name is required!" />
                                </div>
                                <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
                                    <asp:Button CssClass="btn btn-secondary" runat="server" Text="Edit Name" ID="formEditName" OnClick="formEditName_Click" UseSubmitBehavior="false" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align:right;">
                                    <asp:Label runat="server">Email: <span style="color:red">*</span></asp:Label>
                                </div>
                                <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" TextMode="Email" OnTextChanged="tbEmail_TextChanged" placeholder="Email" ID="tbEmail" CssClass="form-control" AutoPostBack="true"/>
                                            <asp:Label runat="server" ForeColor="Red" ID="lbEmaiLExists" Visible="false"/>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbEmail" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Email is required!" />
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="tbEmail" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Email is not in the correct format" ValidationExpression="^\w+[\+\.\w-]*@([\w-]+\.)*\w+[\w-]*\.([a-z]{2,4}|\d+)$" />
                                </div>
                                <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
                                    <asp:Button CssClass="btn btn-secondary" runat="server" Text="Edit Email" ID="formEditEmail" OnClick="formEditEmail_Click" UseSubmitBehavior="false" CausesValidation="false"/>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align:right;">
                                    <asp:Label runat="server">Phone Number: <span style="color:red">*</span></asp:Label>
                                </div>
                                <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8">
                                    <asp:TextBox runat="server" TextMode="Phone" placeholder="Phone Number" ID="tbPhoneNumber" CssClass="form-control" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPhoneNumber" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Phone number is required!" />
                                </div>
                                <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
                                    <asp:Button CssClass="btn btn-secondary" runat="server" Text="Edit Phone Number" ID="editFormPhoneNumber" OnClick="editFormPhoneNumber_Click" UseSubmitBehavior="false" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align:right;">
                                    <asp:Label runat="server">Birth Date:</asp:Label>
                                </div>
                                <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8">
                                    <asp:TextBox runat="server" TextMode="Date" placeholder="Birth Date" ID="tbBirthDate" CssClass="form-control" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbBirthDate" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Birth date is required!" />
                                    <asp:RangeValidator runat="server" ControlToValidate="tbBirthDate" ForeColor="Red" Font-Bold="true" CssClass="incorrect" ErrorMessage="  Date must be within 1/1/1753 to today's date" MinimumValue="1/1/1753" MaximumValue="31/12/9999" Type="Date" Display="Dynamic" />
                                </div>
                                <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
                                    <asp:Button CssClass="btn btn-secondary" runat="server" Text="Edit Birth Date" ID="editFormBirthDate" OnClick="editFormBirthDate_Click" UseSubmitBehavior="false" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
            
                        <div class="form-group">
                            <div class="row">
                                <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align:right;">
                                    <asp:Label runat="server">Password: <span style="color:red">*</span></asp:Label>
                                </div>
                                <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8">
                                    <asp:TextBox runat="server" TextMode="Password" placeholder="Password" ID="tbPassword" CssClass="form-control" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPassword" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Password is required!" />
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="tbPassword" ForeColor="Red" Font-Bold="true" CssClass="incorrect" ErrorMessage="  Invalid password!" Display="Dynamic" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,20}" />
                                </div>
                                <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align:right;">
                                    <asp:Label runat="server">Confirm Password: <span style="color:red">*</span></asp:Label>
                                </div>
                                <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8">
                                    <asp:TextBox runat="server" TextMode="Password" placeholder="Confirm Password" ID="tbPasswordCfm" CssClass="form-control" />
                                    <asp:CompareValidator runat="server" ControlToCompare="tbPassword" ControlToValidate="tbPasswordCfm" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Password does not match" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPasswordCfm" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Confirm password is required!" />
                                </div>
                                <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2">
                                    <asp:Button CssClass="btn btn-secondary" runat="server" Text="Edit Password" ID="editFormPassword" OnClick="editFormPassword_Click" />
                                </div>
                            </div>
                        </div>
            
                            <div class="row">
                                <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align:right;">
                                </div>
                                <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
                                    <div id="passwordValidator" class="passwordValidator">
                                        <ul class="passwordRequirements">
                                            <li id="lowercase" class="incorrect"> At least <b>one lowercase letter</b>
                                            </li>
                                            <li id="uppercase" class="incorrect"> At least <b>one uppercase letter</b>
                                            </li>
                                            <li id="number" class="incorrect"> At least <b>one number</b>
                                            </li>
                                            <li id="symbols" class="incorrect"> At least <b>one special character (@$!%*?&)</b>
                                            </li>
                                            <li id="character" class="incorrect"> At least <b>8 characters long</b>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="text-right">
                            <asp:Button runat="server" CssClass="btn btn-secondary" ID="Delete" OnClick="Delete_Click" Text="Delete Account" UseSubmitBehavior="false" CausesValidation="false" />
                        </div>
                </div>
        </div>
    </div>
</asp:Content>
