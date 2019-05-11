<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true" EnableEventValidation="false" CodeBehind="Login.aspx.cs" Inherits="WebMediaPortfolioTemplate.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- Styles Start -->
    <link rel="icon" type="image/png" href="../FrontEnd/Assets/img/core-img/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="/Assets/vendors/bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="/Assets/fonts/font-awesome-4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="/Assets/fonts/Linearicons-Free-v1.0.0/icon-font.min.css">
    <link rel="stylesheet" type="text/css" href="/Assets/vendors/animate/animate.css">
    <link rel="stylesheet" type="text/css" href="/Assets/vendors/css-hamburgers/hamburgers.min.css">
    <link rel="stylesheet" type="text/css" href="/Assets/vendors/select2/select2.min.css">
    <link rel="stylesheet" type="text/css" href="/Assets/css/util.css">
    <link rel="stylesheet" type="text/css" href="/Assets/css/main.css">
    <!-- Styles End -->

</head>

<body>
    <div class="limiter">
        <div class="container-login100" style="background-image: url('/Assets/images/img-01.jpg');">
            <div class="wrap-login100 p-t-190 p-b-30">
                <form runat="server" class="login100-form validate-form">
                    <asp:Login ID="Login1" OnAuthenticate="ValidateUser" runat="server" EnableViewState="false" RenderOuterTable="false">
                        <LayoutTemplate>
                            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                                ValidationGroup="LoginUserValidationGroup" ForeColor="Red" />
                            <div class="login100-form-avatar" style="border-radius: unset;">
                                <img src="/Assets/images/icon.png" alt="AVATAR">
                            </div>

                            <span class="login100-form-title p-t-10 p-b-15">WebMediaPortfolioTemplate  Login
                            </span>

                            <div style="color: white;" class="text-center w-full p-b-10">

                                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                            </div>


                            <div class="wrap-input100 validate-input m-b-10" data-validate="Username is required">
                                <asp:TextBox ID="UserName" placeholder="Username" runat="server" class="input100"></asp:TextBox>
                                <span class="focus-input100"></span>
                                <span class="symbol-input100">
                                    <i class="fa fa-user"></i>
                                </span>
                            </div>

                            <div class="wrap-input100 validate-input m-b-10" data-validate="Password is required">

                                <asp:TextBox ID="Password" placeholder="Password" runat="server" class="input100" TextMode="Password"></asp:TextBox>

                                <span class="focus-input100"></span>
                                <span class="symbol-input100">
                                    <i class="fa fa-lock"></i>
                                </span>
                            </div>

                            <div class="container-login100-form-btn p-t-10">

                                <asp:LinkButton ID="LoginButton"
                                    runat="server" CommandName="Login" Text="Login"
                                    class="login100-form-btn"
                                    ValidationGroup="LoginUserValidationGroup" />

                            </div>

                            <div class="text-center w-full p-t-25 p-b-230">
                                <a href="#" class="txt1">Forgot Username / Password?
                                </a>
                            </div>
                        </LayoutTemplate>
                    </asp:Login>
                </form>
            </div>
        </div>
    </div>

    <!-- Scripts Start -->
    <script src="/Assets/vendors/jquery/jquery-3.2.1.min.js"></script>
    <script src="/Assets/vendors/bootstrap/js/popper.js"></script>
    <script src="/Assets/vendors/bootstrap/js/bootstrap.min.js"></script>
    <script src="/Assets/vendors/select2/select2.min.js"></script>
    <script src="/Assets/js/main.js"></script>
    <!-- Scripts End -->

</body>
</html>
