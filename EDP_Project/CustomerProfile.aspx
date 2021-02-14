<%@ Page Title="" Language="C#" MasterPageFile="~/WRSite.Master" AutoEventWireup="true" CodeBehind="CustomerProfile.aspx.cs" Inherits="EDP_Project.CustomerProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="extraHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            My Profile
        </div>
        <div class="card-body">
            <div runat="server" visible="false" id="errorDiv" class="alert alert-danger" role="alert">
                <asp:Label runat="server" ID="lbErrorMsg" ForeColor="Red" />
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align: right;">
                        <asp:Label runat="server">First Name: <span style="color:red">*</span></asp:Label>
                    </div>
                    <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
                        <asp:TextBox runat="server" TextMode="SingleLine" placeholder="First Name" ID="tbFirstName" CssClass="form-control" MaxLength="100" />
                        <asp:Label runat="server" ID="lbFirstNameError" ForeColor="Red" Font-Bold="true" CssClass="incorrect" Visible="false" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbFirstName" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" First name is required!" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align: right;">
                        <asp:Label runat="server">Last Name: <span style="color:red">*</span></asp:Label>
                    </div>
                    <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
                        <asp:TextBox runat="server" TextMode="SingleLine" placeholder="Last Name" ID="tbLastName" CssClass="form-control" MaxLength="100" />
                        <asp:Label runat="server" ID="lbLastNameError" ForeColor="Red" Font-Bold="true" CssClass="incorrect" Visible="false" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbLastName" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Last name is required!" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align: right;">
                        <asp:Label runat="server">Email: <span style="color:red">*</span></asp:Label>
                    </div>
                    <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:TextBox runat="server" TextMode="Email" OnTextChanged="tbEmail_TextChanged" placeholder="Email" ID="tbEmail" CssClass="form-control" AutoPostBack="true" />
                                <asp:Label runat="server" ID="lbEmailAddressErrors" ForeColor="Red" Font-Bold="true" CssClass="incorrect" Visible="false" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbEmail" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Email is required!" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="tbEmail" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Email is not in the correct format" ValidationExpression="^\w+[\+\.\w-]*@([\w-]+\.)*\w+[\w-]*\.([a-z]{2,4}|\d+)$" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align: right;">
                        <asp:Label runat="server">Phone Number: <span style="color:red">*</span></asp:Label>
                    </div>
                    <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
                        <asp:TextBox runat="server" TextMode="Phone" placeholder="Phone Number" ID="tbPhoneNumber" CssClass="form-control" />
                        <asp:Label runat="server" ID="lbPhoneNumberError" ForeColor="Red" Font-Bold="true" CssClass="incorrect" Visible="false" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPhoneNumber" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Phone number is required!" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align: right;">
                        <asp:Label runat="server">Birth Date:</asp:Label>
                    </div>
                    <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
                        <asp:TextBox runat="server" TextMode="Date" placeholder="Birth Date" ID="tbBirthDate" CssClass="form-control" />
                        <asp:Label runat="server" ID="lbDateOfBirthError" ForeColor="Red" Font-Bold="true" CssClass="incorrect" Visible="false" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbBirthDate" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Birth date is required!" />
                        <asp:RangeValidator runat="server" ControlToValidate="tbBirthDate" ForeColor="Red" Font-Bold="true" CssClass="incorrect" ErrorMessage="  Date must be within 1/1/1753 to today's date" MinimumValue="1/1/1753" MaximumValue="31/12/9999" Type="Date" Display="Dynamic" />
                    </div>
                </div>
            </div>
            
            <div class="text-right">
                <asp:Button runat="server" CssClass="btn btn-primary" ID="UpdateParticulars" OnClick="UpdateParticulars_Click" Text="Update Particulars" UseSubmitBehavior="false" CausesValidation="false" />
            </div>
        </div>
        <div class="card-footer">
            <div class="form-group">
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align: right;">
                        <asp:Label runat="server">Current Password: <span style="color:red">*</span></asp:Label>
                    </div>
                    <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
                        <asp:TextBox runat="server" TextMode="Password" placeholder="Current Password" ID="tbOldPassword" CssClass="form-control" />
                        <asp:Label runat="server" ID="lbOldPasswordError" ForeColor="Red" Font-Bold="true" CssClass="incorrect" Visible="false" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbOldPassword" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Password is required!" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align: right;">
                        <asp:Label runat="server">Password: <span style="color:red">*</span></asp:Label>
                    </div>
                    <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
                        <asp:TextBox runat="server" TextMode="Password" placeholder="Password" ID="tbPassword" CssClass="form-control" />
                        <asp:Label runat="server" ID="lbPasswordError" ForeColor="Red" Font-Bold="true" CssClass="incorrect" Visible="false" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPassword" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Password is required!" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="tbPassword" ForeColor="Red" Font-Bold="true" CssClass="incorrect" ErrorMessage="  Invalid password!" Display="Dynamic" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,20}" />                   
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align:right;">
                    </div>
                    <div class="col-12 col-sm-12 col-md-8 col-lg-8 col-xl-8">
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
            <div class="form-group">
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-2 col-lg-2 col-xl-2 d-flex align-items-center justify-content-end" style="text-align: right;">
                        <asp:Label runat="server">Confirm Password: <span style="color:red">*</span></asp:Label>
                    </div>
                    <div class="col-12 col-sm-12 col-md-10 col-lg-10 col-xl-10">
                        <asp:TextBox runat="server" TextMode="Password" placeholder="Confirm Password" ID="tbPasswordCfm" CssClass="form-control" />
                        <asp:Label runat="server" ID="lbCfmPasswordError" ForeColor="Red" Font-Bold="true" CssClass="incorrect" Visible="false" />
                        <asp:CompareValidator runat="server" ControlToCompare="tbPassword" ControlToValidate="tbPasswordCfm" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Password does not match" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPasswordCfm" ForeColor="Red" Display="Dynamic" Font-Bold="true" CssClass="incorrect" ErrorMessage=" Confirm password is required!" />
                    </div>
                </div>
            </div>
            
            <div class="text-right">
                <asp:Button runat="server" CssClass="btn btn-danger" ID="UpdateDelete" OnClick="UpdateDelete_Click" Text="Delete Account" UseSubmitBehavior="false" CausesValidation="false" />
                <asp:Button runat="server" CssClass="btn btn-secondary" ID="UpdatePassword" OnClick="UpdatePassword_Click" Text="Update Password" UseSubmitBehavior="false" CausesValidation="false" />
            </div>
        </div>
    </div>
    <script type="text/javascript">

        $("#<%= tbPassword.ClientID %>").focusin(() => {
            $("#passwordValidator").fadeIn("fast");
        });

        $("#<%= tbPassword.ClientID %>").focusout(() => {
            $("#passwordValidator").fadeOut("fast");
        });

        const uppercaseValidator = (password) => {
            var uppercaseRegex = /[A-Z]/g;
            if (uppercaseRegex.test(password)) {
                $("#uppercase").addClass("correct");
                $("#uppercase").removeClass("incorrect");
            } else {
                $("#uppercase").addClass("incorrect");
                $("#uppercase").removeClass("correct");
            };
        };

        const lowercaseValidator = (password) => {
            var lowercaseRegex = /[a-z]/g;
            if (lowercaseRegex.test(password)) {
                $("#lowercase").addClass("correct");
                $("#lowercase").removeClass("incorrect");
            } else {
                $("#lowercase").addClass("incorrect");
                $("#lowercase").removeClass("correct");
            }
        };

        const numberValidator = (password) => {
            var numberRegex = /[0-9]/g;
            if (numberRegex.test(password)) {
                $("#number").addClass("correct");
                $("#number").removeClass("incorrect");
            } else {
                $("#number").addClass("incorrect");
                $("#number").removeClass("correct");
            }
        };

        const lengthValidator = (password) => {
            if (password.length > 8) {
                $("#character").addClass("correct");
                $("#character").removeClass("incorrect");
            } else {
                $("#character").addClass("incorrect");
                $("#character").removeClass("correct");
            }
        };

        const specialCharacterValidator = (password) => {
            var specialCharacterRegex = /[$@$!%*?&]/g;
            if (specialCharacterRegex.test(password)) {
                $("#symbols").addClass("correct");
                $("#symbols").removeClass("incorrect");
            } else {
                $("#symbols").addClass("incorrect");
                $("#symbols").removeClass("correct");
            }
        };

        $("#<%= tbPassword.ClientID %>").on("keyup", (event) => {
            const passwordValue = $("#<%= tbPassword.ClientID %>").val();
            uppercaseValidator(passwordValue);
            lowercaseValidator(passwordValue);
            numberValidator(passwordValue);
            lengthValidator(passwordValue);
            specialCharacterValidator(passwordValue);
        });
    </script>
</asp:Content>