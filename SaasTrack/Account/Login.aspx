<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Blank.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SaasTrack.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
        <p class="text-danger">
            <asp:Literal runat="server" ID="FailureText" />
        </p>
    </asp:PlaceHolder>
    <div class="wrapper-page">

        <div class="text-center">
            <a href="account/Dashboard" class="logo">
                <img src="https://nugget.one/design/beam/member/css/img/logo.png" style="width: 180px;" /></a>
        </div>

        <div class="form-horizontal m-t-20">

            <div class="form-group">
                <div class="col-xs-12">
                    <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" placeholder="Email"/>
                    <i class="md md-account-circle form-control-feedback l-h-34"></i>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                        CssClass="text-danger" ErrorMessage="The email field is required." />
                </div>
            </div>

            <div class="form-group">
                <div class="col-xs-12">
                    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" placeholder="Password"/>
                    <i class="md md-vpn-key form-control-feedback l-h-34"></i>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                </div>
            </div>

            <div class="form-group">
                <div class="col-xs-12">
                    <div class="checkbox checkbox-primary">
                        <asp:CheckBox runat="server" ID="RememberMe" />
                        <asp:Label runat="server" AssociatedControlID="RememberMe">Remember me?</asp:Label>
                    </div>

                </div>
            </div>

            <div class="form-group text-right m-t-20">
                <div class="col-xs-12">
                    <asp:LinkButton runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-primary btn-custom w-md waves-effect waves-light" />
                </div>
            </div>

            <div class="form-group m-t-30">
                <div class="col-sm-7">
                    <a href="account/forgot" class="text-muted"><i class="fa fa-lock m-r-5"></i>Forgot your
                            password?</a>
                </div>
                <div class="col-sm-5 text-right">
                    <a href="account/register" class="text-muted">Create an account</a>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
