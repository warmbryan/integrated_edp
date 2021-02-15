<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="PublicAppointmentView.aspx.cs" Inherits="EDP_Project.PublicAppointmentView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:GridView ID="GV_appointment" runat="server" DataSourceID="AppointmentSource" AutoGenerateColumns="false" EmptyDataText="No data available" OnRowCommand="GV_appointment_OnRowCommand" DataKeyNames="ID">
        <Columns>
            <asp:BoundField DataField="id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
            <asp:BoundField DataField="aptTime" HeaderText="Appointment Time" />
            <asp:BoundField DataField="aptDate" HeaderText="Appointment Date" />
            <asp:BoundField DataField="bookTime" HeaderText="Book Time" />
            <asp:BoundField DataField="bookDate" HeaderText="Book Date" />
            <asp:BoundField DataField="partySize" HeaderText="Party Size" />
            <asp:TemplateField ShowHeader="false">
                <ItemTemplate>
                    <asp:Button runat="server" CommandName="Modify" Text="Modify" CausesValidation="false" CommandArgument='<%#Container.DataItemIndex %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="AppointmentSource" runat="server" SelectCommand="SELECT [id],[aptTime],[aptDate],[bookTime],[bookDate],[partySize] from Appointment" ConnectionString='Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\integrated_edp\EDP_Project\App_Data\MyDB.mdf;Integrated Security=True' />
</asp:Content>
