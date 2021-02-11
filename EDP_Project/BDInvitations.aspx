<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticated.Master" AutoEventWireup="true" CodeBehind="BDInvitations.aspx.cs" Inherits="EDP_Project.BDInvitations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h2>My invitations</h2>
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="/BDHome.aspx">Home</a></li>
            <li class="breadcrumb-item active" aria-current="page">Invitations</li>
        </ol>
    </nav>
    <hr />
    <asp:ListView ID="lv_invitations" runat="server">
        <LayoutTemplate>
            <div class="accordion accordion-flush" id="invitationsAccordion">
                <div id="itemPlaceholder" runat="server"></div>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="accordion-item">
                <h2 class="accordion-header" id="flush-heading-<%# Eval("id") %>">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-collapse-<%# Eval("id") %>" aria-expanded="false" aria-controls="flush-collapse-<%# Eval("id") %>">
                        <%# Eval("business.name") %>
                    </button>
                </h2>
                <div id="flush-collapse-<%# Eval("id") %>" class="accordion-collapse collapse" aria-labelledby="flush-heading-<%# Eval("id") %>" data-bs-parent="#invitationsAccordion">
                    <div class="accordion-body">Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS.</div>
                    <div class="d-flex justify-content-center gap-2 p-2">
                        <a class="btn btn-success" role="button" href="<%# "/BDAcceptInvitation?invitation=" + Eval("id") %>">Accept</a>
                        <a class="btn btn-danger" role="button" href="<%# "/BDRejectInvitation?invitation=" + Eval("id") %>">Reject</a>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
