<%@ Page Title="Account Confirmation" Language="C#" MasterPageFile="~/Blank.Master" AutoEventWireup="true" CodeBehind="Confirm.aspx.cs" Inherits="SaasTrack.Account.Confirm" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="wrapper-page">

        <div class="text-center">
            <a href="account/Dashboard" class="logo">
                <img src="https://nugget.one/design/beam/member/css/img/logo.png" style="width: 180px;" /></a>
        </div>
        <asp:PlaceHolder runat="server" ID="successPanel" ViewStateMode="Disabled" Visible="true">
            <div class="form-horizontal m-t-20">
                <div class="alert alert-success alert-dismissable" id="success" runat="server" >
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                    Thank you for confirming your account. Click
                <asp:HyperLink ID="login" runat="server" NavigateUrl="~/Account/Login">here</asp:HyperLink>
                    to login             
                </div>
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="errorPanel" ViewStateMode="Disabled" Visible="false">
            <p class="text-danger">
                An error has occurred.
            </p>
        </asp:PlaceHolder>

    </div>
</asp:Content>
