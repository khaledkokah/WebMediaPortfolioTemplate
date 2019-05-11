<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Site1.Master" AutoEventWireup="true" CodeBehind="FEChangePassword.aspx.cs" Inherits="WebMediaPortfolioTemplate.FrontEnd.FEChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpContent" runat="server">
    <div>
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
</asp:Content>
