<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Site1.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="WebMediaPortfolioTemplate.FrontEnd.Categories" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cpHeader" runat="server">
 <div class="breadcumb-area bg-img"  style="background-image: url(/frontend/assets/img/bg-img/banner.png);">
        <div class="container h-100">
            <div class="row h-100 align-items-center">
                <div class="col-12">
                    <div class="breadcumb-text text-center">
                        <h2 >
                            <asp:Label id="lblTitle" runat="server" /></h2>
                        <br />
<a class="btn btn-dark" href="javascript:history.go(-1)">Back</a>

                        </div>
                </div>
            </div>
                </div>
       </div>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cpContent" runat="server">
    <div class="row">
        <asp:Repeater id="lstItems" runat="server">
            <ItemTemplate>
        <!-- Single Best Receipe Area -->
        <div class="col-12 col-sm-6 col-lg-4">
            <div class="single-best-receipe-area mb-30" style="margin-bottom: 20px">
                <a href='Content.aspx?Id=<%#Eval("CODING_ID")%>'>
                    <img class="backup_picture"  src="<%# Eval("URL")%>"  >
                    <div class="receipe-content" style="padding-top: 10px;">
                        <h5>
                            <asp:Label id="lblTitle" text='<%#Eval("CODING_DESCRIPTION")%>' runat="server" /></h5>
                    </div>
                </a>
            </div>
        </div>
                </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
