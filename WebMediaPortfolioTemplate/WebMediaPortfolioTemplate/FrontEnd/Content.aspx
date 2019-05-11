<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Site1.Master" AutoEventWireup="true" CodeBehind="Content.aspx.cs" Inherits="WebMediaPortfolioTemplate.FrontEnd.Content" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cpHeader" runat="server">
    <div class="breadcumb-area bg-img" style="background-image: url(/frontend/assets/img/bg-img/banner2.png);">
        <div class="container h-100">
            <div class="row h-100 align-items-center">
                <div class="col-12">
                    <div class="breadcumb-text text-center">
                        <h2>
                            <asp:Label ID="lblTitle" runat="server" /></h2>
                        <br />
                        <a class="btn btn-dark" href="javascript:history.go(-1)">Back</a>

                    </div>
                </div>
            </div>
        </div>
    </div>

      <script type="text/javascript">
                        function changeSrc(url){
                            if(url.includes('youtube.com'))
                             return 'Assets/img/bg-img/vid.jpg';
                            var ext = url.split('.').pop(); 
                           
                            if(ext == "pdf"){
                            return 'Assets/img/bg-img/pdf.jpg';
                            }
                            else if(ext == "mp3"){
                            return 'Assets/img/bg-img/media.jpg';
                            }
                            else 
                       return url;
                        };

      </script>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cpContent" runat="server">
    <div class="row">
        <asp:Repeater ID="lstItems" runat="server">
            <ItemTemplate>
                <!-- Single Best Receipe Area -->
                <div class="col-12 col-sm-6 col-lg-4">
                    <div class="single-best-receipe-area mb-30" style="margin-bottom: 20px">
                        <a href='<%# Eval("URL")%>' data-lity data-lity-desc="Preview">
                            <img class="backup_picture" src='Assets/img/bg-img/media.jpg' onload="this.onload=null; this.src=changeSrc('<%# Eval("URL")%>');"  onerror="this.src='Assets/img/bg-img/mediaerr.jpg';">
                          
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
