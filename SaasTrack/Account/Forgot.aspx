<%@ Page Title="Forgot password" Language="C#" MasterPageFile="~/Blank.Master" AutoEventWireup="true" CodeBehind="Forgot.aspx.cs" Inherits="SaasTrack.Account.ForgotPassword" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="wrapper-page">

        <div class="text-center">
            <a href="account/Dashboard" class="logo">
                <img src="https://nugget.one/design/beam/member/css/img/logo.png" style="width: 180px;" /></a>
        </div>

        <div class="text-center m-t-20">
            <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                <div class="alert alert-danger alert-dismissable">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                    <asp:Literal runat="server" ID="FailureText" />
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="DisplayEmail" Visible="false">
                <div class="alert alert-success alert-dismissable">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                    Please check your email to reset your password.
                </div>
            </asp:PlaceHolder>
            <div class="alert alert-success alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                Enter your <b>Email</b> and instructions will be sent to you!
            </div>
            <div class="form-group m-b-0">
                <div class="input-group">
                    <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" placeholder="Email address" />
                    <i class="md md-email form-control-feedback l-h-34" style="left: 6px;"></i>

                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                        CssClass="text-danger" ErrorMessage="The email field is required." />

                </div>
            </div>
            <div class="form-group text-center m-t-20">
                <div class="col-xs-12">
                    <asp:LinkButton runat="server" OnClick="Forgot" Text="Reset" CssClass="btn btn-email btn-primary waves-effect waves-light" />
                </div>
            </div>

        </div>
    </div>
    <div class="row">
        <div class="col-md-8">
            <asp:PlaceHolder ID="loginForm" runat="server">
                <div class="form-horizontal">
                    <h4>Forgot your password?</h4>
                    <hr />

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
                        <div class="col-md-10">
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

        </div>
    </div>
</asp:Content>
