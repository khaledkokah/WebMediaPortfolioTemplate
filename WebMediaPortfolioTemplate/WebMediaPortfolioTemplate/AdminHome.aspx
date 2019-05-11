<%@ Page Language="C#" Title="Home" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AdminHome.aspx.cs" Inherits="WebMediaPortfolioTemplate.AdminHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp_pageTitle" runat="server">
    Home
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp_pageDesc" runat="server">
    Welcome to your Homepage
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cp_content" runat="server">
       <asp:button ID="btnMain" PostBackUrl="~/Views/CodingType.aspx" runat="server" Text="Main Categories" class="btn btn-dark btn-lg m-btn  m-btn m-btn--icon" />
   <asp:button  ID="btnSub" PostBackUrl="~/Views/Coding.aspx" runat="server" Text="Sub Categories" class="btn btn-info btn-lg m-btn  m-btn m-btn--icon" />
       <asp:button  ID="btnFrontEnd" PostBackUrl="~/Default.aspx" runat="server" Text="Jump to Front End " class="btn btn-success btn-lg m-btn  m-btn m-btn--icon" />
</asp:Content>
