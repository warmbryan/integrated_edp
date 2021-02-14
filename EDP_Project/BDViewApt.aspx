<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticated.Master" AutoEventWireup="true" CodeBehind="BDViewApt.aspx.cs" Inherits="EDP_Project.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
     <style type="text/css">
        .hidden{
            display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
     <asp:GridView ID="GV_appointment" runat="server" autogeneratecolumns="false" emptydatatext="No data available" OnRowCommand="GV_appointment_OnRowCommand"  DataKeyNames = "aptDate">
         <columns>
          <asp:boundfield datafield="aptTime" headertext="Appointment Time"/>
               <asp:boundfield datafield="aptTime" headertext="Party Size" ItemStyle-CssClass="hidden" ShowHeader="false" />
             <asp:boundfield datafield="partySize" />
             <asp:TemplateField ShowHeader="false">
                 <ItemTemplate>
                     <asp:Button runat="server" CommandName="Arrived" Text="Arrived"  CommandArgument='<%#Container.DataItemIndex %>' CausesValidation="false"  OnClientClick="javascript:showPopup();return false;"/>
                 </ItemTemplate>
             </asp:TemplateField>
               <asp:TemplateField ShowHeader="false">
                 <ItemTemplate>
                     <asp:Button runat="server" CommandName="Delete" Text="Delete"  CommandArgument='<%#Container.DataItemIndex %>' CausesValidation="false"  />
                 </ItemTemplate>
             </asp:TemplateField>
        </columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
