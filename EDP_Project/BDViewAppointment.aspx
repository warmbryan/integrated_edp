<%@ Page Title="" Language="C#" MasterPageFile="~/BDAuthenticated.Master" AutoEventWireup="true" CodeBehind="BDViewAppointment.aspx.cs" Inherits="EDP_Project.BDViewApppointment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="stylesheets" runat="server">
      <style type="text/css">
        .hidden{
            display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
      <div>
        <asp:Button runat="server" Text="Today's appointment " OnClick="todayAppointment"/>
    </div>
      <asp:GridView ID="GV_appointment" runat="server" autogeneratecolumns="false" emptydatatext="No data available" OnRowCommand="GV_appointment_OnRowCommand" >
         <columns>
          <asp:boundfield datafield="aptDate" headertext="Appointment Date"/>
          <asp:boundfield datafield="count" headertext="Number of Appointments"/>
             <asp:TemplateField ShowHeader="false">
                 <ItemTemplate>
                     <asp:Button runat="server" CommandName="View" Text="View"  CommandArgument='<%#Container.DataItemIndex %>' CausesValidation="false"  OnClientClick="javascript:showPopup();return false;"/>
                 </ItemTemplate>
             </asp:TemplateField>
        </columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
