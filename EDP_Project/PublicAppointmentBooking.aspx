<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="PublicAppointmentBooking.aspx.cs" Inherits="EDP_Project.PublicAppointmentBooking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 103px;
        }

        .auto-style2 {
            margin-right: 15px;
        }

        .auto-style3 {
            margin-top: 14px;
            margin-bottom: 16px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="border: solid 2px #ffe2ad;">
        <div style="margin-left: 15px;">
            <h2 class="auto-style3"><strong>Booking</strong></h2>
            <hr style="background-color: #ffe2ad;" class="auto-style2" />

            <asp:Label ID="Label1" runat="server" Text="Date: " Font-Size="Large"></asp:Label>

            <asp:Label ID="lbl_date" runat="server" BorderColor="#FFE2AD" BorderStyle="Solid" BorderWidth="2px"></asp:Label>

            <asp:Calendar runat="server" ID="calendar" SelectionMode="Day" OnSelectionChanged="Calender_SelectionChanged" CssClass="auto-style1" OnDayRender="Calendar_DayRender"></asp:Calendar>
            <asp:Label ID="lbl_error" runat="server"></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Time: " Font-Size="Large"></asp:Label>
            <asp:DropDownList runat="server" ID="dd_time"></asp:DropDownList>
            <asp:Label ID="lbl_checker" runat="server" Text="Invalid Time"></asp:Label>

            <br />
            <asp:Label ID="Label3" runat="server" Text="Party Size: " Font-Size="Large"></asp:Label>
            <asp:Label ID="lbl_party_size" runat="server" Text="Invalid Time" Visible="false"></asp:Label>
            <asp:DropDownList ID="party_size" runat="server">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>7</asp:ListItem>
                <asp:ListItem>8</asp:ListItem>
            </asp:DropDownList>
            <br />

            <asp:Button runat="server" BackColor="#FFE2AD" BorderColor="#FFE2AD" Text="Submit" OnClick="Submit" />
        </div>
    </div>
    <%--<script type="text/javascript">

         window.onload = function validate() {
             var str = document.getElementById('<%=tb_time.ClientID %>').value;
             if (str.search(/^([0-1][0-9]|2[0-3]):([0-5][0-9])$/) == -1) {
                 document.getElementById('lbl_checker').innerHTML = "Helllo"

                 return ('invalid date');
             } else {

             }
         }
        </script>--%>
</asp:Content>
