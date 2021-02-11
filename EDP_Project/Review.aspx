<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Review.aspx.cs" Inherits="EDP_Project.Review" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>ABC Shop - Review</h2>
    <div class="row">
        <div class="col-5 text-center border border-dark" style="padding: 5px;">
            <img alt="Image" src="/Public/Image/SearchBG.png" style="height: 150px;" /></div>
        <div class="col-7 text-center border border-dark"><a href="#">Information</a></div>
    </div>
    <br />
    <div class="row">
        <div class="col-2 text-center border border-dark">Username</div>
        <div class="col-1 text-center border border-dark">Rating</div>
        <div class="col-6 text-center border border-dark">Comment</div>
        <div class="col-2 text-center border border-dark">Date dd/mm/yy</div>
        <div class="col-1 text-center border border-dark">Likes</div>
    </div>
    <div class="row">
        <div class="col-2 text-center border border-dark">Nixon</div>
        <div class="col-1 text-center border border-dark">4/5</div>
        <div class="col-6 border border-dark">comment 1 comment 2 comment 3</div>
        <div class="col-2 text-center border border-dark">30/11/20</div>
        <div class="col-1 text-center border border-dark">30</div>

        <div class="col-2 text-center border border-dark">Nixon</div>
        <div class="col-1 text-center border border-dark">4/5</div>
        <div class="col-6 border border-dark">comment 1 comment 2 comment 3</div>
        <div class="col-2 text-center border border-dark">11/11/20</div>
        <div class="col-1 text-center border border-dark">20</div>
    </div>
</asp:Content>
