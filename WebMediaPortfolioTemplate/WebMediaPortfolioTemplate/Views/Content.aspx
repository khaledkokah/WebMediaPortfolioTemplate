<%@ Page Title="Content" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Content.aspx.cs" Inherits="WebMediaPortfolioTemplate.Views.Content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp_pageTitle" runat="server">
    <asp:Label ID="lblPageTitle" runat="server" />

    <script src="../assets/vendors/jquery/dist/jquery.min.js"></script>
    <script src="../App_JS/template.js"></script>
    <script src="../App_JS/Setup/content.js"></script>
    <link href="../assets/lity.css" rel="stylesheet">
    <script src="../assets/jquery.js"></script>
    <script src="../assets/lity.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp_pageDesc" runat="server">
    <asp:Label ID="lblPageDesc" runat="server" />
    <br />
    <asp:Button Text="Back" ID="btnRefresh" PostBackUrl="~/Views/Coding.aspx" CssClass="btn-dark btn-sm" runat="server" />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cp_content" runat="server">
    <asp:SqlDataSource ID="sqldatasource" runat="server" ConnectionString="Data Source=.\SQLEXPRESS;Initial Catalog=WebMediaPortfolioTemplate -DB;Persist Security Info=True;User ID=sa;Password=sa+123++" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [PRO_PROPERTIES]" />

    <div class="x_panel">
        <div class="x_title">
            <h2>
                <asp:Label ID="lblPageTitle_sub" runat="server" />
                <button runat="server" onclick="showModal(0,true);" id="btnAdd" type="button" class="btn btn-warning">Add New</button>
            </h2>
            <ul class="nav navbar-right panel_toolbox">
                <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                </li>
                <li class="dropdown">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                    <ul class="dropdown-menu" role="menu">
                        <li><a href="#">Settings 1</a>
                        </li>
                        <li><a href="#">Settings 2</a>
                        </li>
                    </ul>
                </li>
                <li><a class="close-link"><i class="fa fa-close"></i></a>
                </li>
            </ul>
            <div class="clearfix"></div>
        </div>
        <div class="x_content">

            <p>
                <asp:Label ID="lblPageDesc_sub" runat="server" />
            </p>

            <!-- === Start List  === <-->
            <table id="datatable" class="table table-striped table-bordered">
                <thead>
                    <tr>
                        <th>Description</th>
                        <th>File Preview</th>
                        <!-- Keep extra th for delete button -->
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <!-- === Insert here the required list control, like repeater or gridivew  === <-->
                    <asp:Repeater ID="GridView1" runat="server">
                        <ItemTemplate>
                            <td>
                                <a href='#' onclick="showModal('<%# Eval("CONTENT_ID").ToString() %>',false)"><%# Eval("DESCRIPTION")%></a> </td>
                            </td>      
                            <td>
                                <a href='<%# Eval("URL")%>' data-lity data-lity-desc="Preview">Preview</a>

                            </td>

                            <td>
                                <asp:LinkButton OnClientClick='if (!confirm("Are you sure you want to delete this record?")) return false;' CommandArgument='<%# Eval("CONTENT_ID") %>' ID="btnDelete" OnCommand="btnDelete_Command" CommandName="Del" runat="server">
                         <img src="/Assets/images/delete.png" alt="delete" /><br />Delete
                                </asp:LinkButton>
                            </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                </tbody>
            </table>
            <!-- End  list -->
        </div>
    </div>

    <div id="modalRec" class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="myModalLabel">Record Details</h4>
                </div>
                <div class="modal-body">
                    <div id="modalContent">
                        <!-- === Insert form controls === -->
                        <div class="form-group">
                            <table id="tblForm" class="table table-striped table-bordered">
                                <tr>
                                    <td>Description
                                        <asp:TextBox ClientIDMode="static" ID="txtDesc" runat="server" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>File 
                                        <asp:FileUpload ID="fileUpload" runat="server" />
                                    </td>
                                    <td>Video URL
                                        <asp:TextBox ClientIDMode="static" ID="txtVideoUrl" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <div id="divStamp">
                                <kokahUC:ModalStamp ID="modalStamp" runat="server" />

                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">

                    <div class="col-md-1 col-sm-1 col-xs-1">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                    <div class="col-md-1 col-sm-1 col-xs-1">
                        <button class="btn btn-danger" type="button" id="btnClear" onclick="clearModal(document.getElementById('modalRec'),true);">Clear</button>
                    </div>

                    <div style="text-align: right;" class="col-md-10 col-sm-10 col-xs-10">
                        <asp:Button Text="Save" runat="server" ID="btnSave" OnCommand="btnSave_Command" CommandArgument="save" class="btn btn-default" OnClientClick='return validate();' />
                        <asp:HiddenField ID="hdRecId" ClientIDMode="Static" runat="server" />
                    </div>

                </div>

            </div>
        </div>
    </div>

</asp:Content>
