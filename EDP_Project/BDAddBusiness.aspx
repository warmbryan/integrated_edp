<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticated.Master" AutoEventWireup="true" CodeBehind="BDAddBusiness.aspx.cs" Inherits="EDP_Project.BDAddBusiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-xl-8 col-md-12">
            <form id="form" runat="server">
                <h1>Business Registration</h1>
                <hr />

                <asp:ListView ID="lv_feedback" runat="server" Visible="false">
                    <LayoutTemplate>
                        <div class="alert alert-danger" role="alert">
                            <h4 class="alert-heading">There are invalid fields, please correct it.</h4>
                            <ul class="mb-0">
                                <div id="itemPlaceholder" runat="server"></div>
                            </ul>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li><%# GetDataItem() %></li>
                    </ItemTemplate>
                </asp:ListView>

                <h4>Business Information</h4>
                <p>Field with asterisk are required fields.</p>
                <div class="mb-3">
                    <label class="form-label" for="tb_businessName">Name*</label>
                    <asp:TextBox ID="tb_businessName" runat="server" CssClass="form-control" placeholder="Business Name" required></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label" for="tb_businessRegNum">Registration Number*</label>
                    <asp:TextBox ID="tb_businessRegNum" runat="server" CssClass="form-control" placeholder="Business Name"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label" for="tb_businessType">Type*</label>
                    <asp:TextBox ID="tb_businessType" runat="server" CssClass="form-control" placeholder="Business Type"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label" for="tb_businessUrl">Website URL</label>
                    <asp:TextBox ID="tb_businessUrl" runat="server" CssClass="form-control" placeholder="Business Name"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label" for="logoId">Business Logo (Max 2MB)</label>
                    <asp:FileUpload ID="file_logoId" runat="server" CssClass="form-control-file"/>
                    <small>File format must be in png or jpeg. Logo Photo can also be uploaded after registration.</small>
                </div>

                <div class="mb-3">
                    <label class="form-label" for="acraRegistration">Registration Document* (Max 8MB)</label>
                    <asp:FileUpload ID="file_acraRegistration" runat="server" CssClass="form-control-file"/>
                    <small>File format must be in pdf document.</small>
                </div>

                <div class="mb-3 form-check">
                    <input class="form-check-input" type="checkbox" value="" id="agreement" runat="server" required>
                    <label class="form-check-label" for="flexCheckDefault">
                        I have read the terms of service and the information I provided are accurate to my best abilities.
                    </label>
                </div>

                <asp:Button ID="btnSubmit" runat="server" Text="Submit Registration Form" CssClass="btn btn-block btn-primary" OnClick="Register_Business"/>
            </form>
        </div>

        <div class="col-xl-4 col-md-12">
            <h1>Terms of Service</h1>
            <hr />
            <p>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer placerat bibendum tempus. Nullam nisl magna, posuere eu commodo vel, semper et elit. Proin vehicula mollis nunc, vel varius massa rutrum et. In hac habitasse platea dictumst. Etiam vel odio purus. Suspendisse at congue magna. In placerat quis diam eget euismod. Integer ornare hendrerit posuere. Nam at dictum mi. Vestibulum quis pharetra turpis. Nam sollicitudin mollis lacus in blandit.
                <br />
                Praesent eget semper leo. Quisque sit amet leo non urna ullamcorper maximus. Vestibulum tristique vehicula magna in tempus. Aenean tristique eros nec sollicitudin vulputate. Duis sapien mi, imperdiet et maximus ornare, ultricies sed sem. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec sem turpis, suscipit vitae sapien vel, facilisis sagittis tellus. Vivamus eu porta ipsum, quis dignissim dui. Nullam sollicitudin ligula vel elementum scelerisque. Nunc tempor mauris eu metus mollis, nec venenatis dolor pulvinar.
                <br />
                Aenean nisi lacus, elementum eu purus id, dapibus semper ante. In orci turpis, cursus sed ligula et, condimentum cursus purus. Donec velit mi, efficitur a malesuada ac, pretium pellentesque augue. Nunc accumsan urna et congue semper. Etiam metus odio, egestas et pretium quis, fermentum tincidunt nisi. Fusce quis nulla rutrum, lacinia nisi fringilla, pharetra urna. Nunc malesuada nulla mauris, eu iaculis massa accumsan a. Vivamus consectetur tortor nisi, a venenatis neque mollis sed. Aliquam at diam maximus, viverra massa finibus, faucibus ipsum. Cras cursus ex sit amet nulla porta, at bibendum mauris fermentum. Nulla dictum ligula ac quam hendrerit fringilla a at ante. Curabitur ultricies erat erat, ac venenatis nisi blandit et. Ut et auctor tortor. Maecenas sollicitudin eu purus non dictum. Nam laoreet facilisis cursus.
                <br />
            </p>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">

</asp:Content>
