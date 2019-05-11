<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs"
    Inherits="User_Change_Password_CS.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp_pageTitle" runat="server">
    Change Password
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp_pageDesc" runat="server">
    Change your account password
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cp_content" runat="server">
    <div class="x_panel">
        <div class="x_title">
            <h2>
                <asp:Label Text=" ► Account password" runat="server" />
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
            <asp:ChangePassword ChangePasswordTitleText="" PasswordHintStyle-Font-Size="Smaller"
                PasswordHintText="Please enter a password at least 7 characters long, 
    containing a number and one special character."
                NewPasswordRegularExpression='^.*(?=.{7,})(?=.*[a-zA-Z])(?=.*[@#$%^&!+=]).*$'
                NewPasswordRegularExpressionErrorMessage="Error: Your password must be at least 7 characters long, 
    and contain at least one number and one special character."
                ID="ChangePassword1" runat="server" OnChangingPassword="OnChangingPassword" CancelDestinationPageUrl="/Home.aspx"
                RenderOuterTable="false" ChangePasswordButtonStyle-CssClass="btn-primary">
            </asp:ChangePassword>
            <br />
            <asp:Label ID="lblMessage" runat="server" />

        </div>
    </div>
</asp:Content>
