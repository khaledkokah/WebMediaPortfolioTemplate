<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Site1.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="WebMediaPortfolioTemplate.Default" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cpHeader" runat="server">
    <div class="breadcumb-area bg-img" style="background-image: url(/frontend/assets/img/bg-img/breadcumb4.png);">
        <div class="container h-100">
            <div class="row h-100 align-items-center">
                <div class="col-12">
                    <div class="breadcumb-text text-center">
                        <h2 style="color: black;">
                            <asp:Label ID="lblTitle" runat="server" /></h2>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cpContent" runat="server">
    <div class="row">
        <asp:Repeater ID="lstItems" runat="server">
            <ItemTemplate>
                <!-- Main Categories (Coding) Area -->
                <div class="col-12 col-sm-6 col-lg-4">
                    <div class="single-best-receipe-area mb-30" style="margin-bottom: 20px">
                        <a href='/FrontEnd/Categories.aspx?Id=<%#Eval("CODINGTYPE_ID")%>'>
                            <img src='<%#Eval("URL")%>'>
                            <div class="receipe-content" style="padding-top: 10px;">
                                <h5>
                                    <asp:Label ID="lblTitle" Text='<%#Eval("DESCRIPTION")%>' runat="server" /></h5>
                            </div>
                        </a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
