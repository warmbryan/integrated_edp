<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="PublicAppointmentBookingSuccess.aspx.cs" Inherits="EDP_Project.PublicAppointmentBookingSuccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #all {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="all">
        <img src="https://i.pinimg.com/originals/f7/8f/87/f78f879def6bd3af945ce11279d243b0.png" />
        <h2>Thank You! </h2>
        <p>Your booking has been confirmed</p>
        <asp:Button runat="server" BackColor="#FFE2AD" BorderColor="#FFE2AD" Text="Back to home" Width="450px" Height="60px" OnClick="Unnamed1_Click" />
    </div>
</asp:Content>
