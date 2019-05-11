<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd/Site1.Master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="WebMediaPortfolioTemplate.ContactUs" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cpHeader" runat="server">
    <!-- Styles Start -->
    <style>
        .container {
            width: 90%;
            text-align: center;
            margin: auto;
        }

        .pad50 {
            padding-top: 50px;
        }

        .button {
            display: inline-block;
            height: 40px;
            line-height: 40px;
            padding-right: 30px;
            padding-left: 70px;
            position: relative;
            background-color: rgb(0,0,0);
            color: rgb(255,255,255);
            text-decoration: none;
            text-transform: none letter-spacing: 1px;
            margin-bottom: 14px;
            text-shadow: 1px 1px 0px rgba(0,0,0,0.5);
        }

        .button:hover {
            text-decoration: none;
            color: #fff;
            text-shadow: none;
        }

        .button p {
            font-size: 18px;
        }

        .button span {
            position: absolute;
            left: 0;
            width: 50px;
            font-size: 30px;
            -webkit-border-top-left-radius: 5px;
            -webkit-border-bottom-left-radius: 5px;
            -moz-border-radius-topleft: 5px;
            -moz-border-radius-bottomleft: 5px;
            border-top-left-radius: 5px;
            border-bottom-left-radius: 5px;
            border-right: 1px solid rgba(0,0,0,0.15);
            text-decoration: none;
        }

        .button.twitter {
            background: #00acee;
        }

        .button.facebook {
            background: #3b5998;
        }

        .button.google-plus {
            background: #db4a39;
        }

        .button.linkedin {
            background: #0e76a8;
        }

        .button.youtube {
            background: #c4302b;
        }

        .button.github {
            background: #171515;
        }

        .button.android {
            background: #a4c639;
        }

        .button.skype {
            background: #00aff0;
        }

        .button.dropbox {
            background: #3d9ae8;
        }

        .button.foursquare {
            background: #25a0ca;
        }

        .button.apple {
            background: #cdcdcd;
        }

        .button.dribbble {
            background: #ea4c89;
        }

        .button.instagram {
            background: #3f729b;
        }

        .button.pinterest {
            background: #c8232c;
        }

        .button.stackexchange {
            background: #ef8236;
        }

        .button.flickr {
            background: #ff0084;
        }

        .button.snapchat {
            background: #f6d40a;
        }

        .text-center {
            text-align: center;
        }
    </style>
    <!-- Styles End -->

    <div class="breadcumb-area bg-img" style="background-image: url(/frontend/assets/img/bg-img/breadcumb4.png);">
        <div class="container h-100">
            <div class="row h-100 align-items-center">
                <div class="col-12">
                    <div class="breadcumb-text text-center">
                        <h2 style="color: black;">
                            <asp:Label ID="lblTitle" Text="Contact us" runat="server" /></h2>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cpContent" runat="server">
    <div class="row">
        <!-- Contact Text -->
        <div class="col-12 col-lg-5">
            <!-- Single Contact Information -->
            <div class="single-contact-information mb-30">
                <h6>Address:</h6>
                <p>481 Creekside Lane Avila
                    <br>
                    Beach, CA 93424</p>
            </div>
            <!-- Single Contact Information -->
            <div class="single-contact-information mb-30">
                <h6>Phone:</h6>
                <p>00965 - 96929322
                    <br>
                    00965 - 23915117<br>
                    00965 - 55330015</p>
            </div>
            <!-- Single Contact Information -->
            <div class="single-contact-information mb-30">
                <h6>Email:</h6>
                <p>yourmail@gmail.com</p>
            </div>

        </div>

        <div class="col-12 col-lg-3">
            <div id="googleMap">
                <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3479.245289585859!2d47.48395468542447!3d29.304479660102576!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0x0!2zMjnCsDE4JzE2LjEiTiA0N8KwMjgnNTQuNCJF!5e0!3m2!1sar!2skw!4v1542131771687" width="600" height="450" frameborder="0" style="border: 0" allowfullscreen></iframe>
            </div>
        </div>
    </div>

    <div>

        <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
        <link href="https://netdna.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.css" rel="stylesheet">
        <link href='https://fonts.googleapis.com/css?family=Inconsolata:400,700' rel='stylesheet' type='text/css'>

        <h2>Follow us on Social Media</h2>

        <div>
            <a href="https://www.facebook.com/dj.alrashed" target="_blank" class="button facebook"><span><i class="fa fa-facebook" aria-hidden="true"></i></span>
                <p style="color: #fff;">dj.alrashed</p>
            </a>

            <a href="https://twitter.com/eissa_alrashed" target="_blank" class="button twitter"><span><i class="fa fa-twitter" aria-hidden="true"></i></i></span><p style="color: #fff;">eissa_alrashed</p>
            </a>
            <a href="https://twitter.com/ft_alrashed" target="_blank" class="button twitter"><span><i class="fa fa-twitter" aria-hidden="true"></i></i></span><p style="color: #fff;">ft_alrashed</p>
            </a>

            <a href="https://www.instagram.com/eissa_alrashed/" class="button instagram"><span><i class="fa fa-instagram" aria-hidden="true"></i></span>
                <p style="color: #fff;">eissa_alrashed</p>
            </a>
            <a href="https://www.instagram.com/zafty_/" class="button instagram"><span><i class="fa fa-instagram" aria-hidden="true"></i></span>
                <p style="color: #fff;">zafty_</p>
            </a>
            <a href="https://www.instagram.com/b6aqty/" class="button instagram"><span><i class="fa fa-instagram" aria-hidden="true"></i></span>
                <p style="color: #fff;">b6aqty</p>
            </a>

            <a href="https://www.youtube.com/user/manalol100" target="_blank" class="button youtube"><span><i class="fa fa-youtube" aria-hidden="true"></i></span>
                <p style="color: #fff;">manalol100</p>
            </a>
            <a href="https://www.youtube.com/channel/UC4Y-ELK8TZ86-u-ccFAezVA" target="_blank" class="button youtube"><span><i class="fa fa-youtube" aria-hidden="true"></i></span>
                <p style="color: #fff;">NO Music</p>
            </a>

            <a href="https://www.snapchat.com/add/eissa_alrashed" target="_blank" class="button snapchat"><span><i class="fa fa-snapchat" aria-hidden="true"></i></span>
                <p style="color: #fff;">eissa_alrashed</p>
            </a>
            <a href="https://www.snapchat.com/add/ft_alrashed" target="_blank" class="button snapchat"><span><i class="fa fa-snapchat" aria-hidden="true"></i></span>
                <p style="color: #fff;">ft_alrashed</p>
            </a>
        </div>
    </div>
</asp:Content>
