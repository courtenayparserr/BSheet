<%@ Page Title="Register" Language="C#" MasterPageFile="~/Blank.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SaasTrack.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">    
    <div class="wrapper-page">

        <div class="text-center">
            <a href="/account/Dashboard" class="logo">
                <img src="https://nugget.one/design/beam/member/css/img/logo.png" style="width: 180px;" /></a>
        </div>

        <div class="form-horizontal m-t-20">
            <div class="alert alert-success alert-dismissable" id="success" runat="server" visible="false">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                <b>Thanks for registering</b>Email instructions have been sent to you!
            </div>
            <asp:Label CssClass="alert alert-danger alert-dismissable" runat="server" ID="ErrorMessage" Visible="false" style="color: #ef5350;padding:0px !important; background-color:#e8e8e8 !important;border:none !important;" />
            <br/><br/>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" placeholder="Email" />
                    <i class="md md-email form-control-feedback l-h-34"></i>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                        CssClass="text-danger" ErrorMessage="The email field is required." />
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:TextBox runat="server" ID="phoneNumber" CssClass="form-control" TextMode="Phone" placeholder="Phone number" />
                    <i class="md md-phone form-control-feedback l-h-34"></i>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="phoneNumber"
                        CssClass="text-danger" ErrorMessage="The phone field is required." />
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" placeholder="Password" />
                    <i class="md md-vpn-key form-control-feedback l-h-34"></i>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                        CssClass="text-danger" ErrorMessage="The password field is required." />
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" placeholder="Confirm password" />
                    <i class="md md-vpn-key form-control-feedback l-h-34"></i>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                        CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                    <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                        CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                </div>
            </div>

            <div class="form-group">
                <div class="col-xs-12">
                    <div class="checkbox checkbox-primary">
                        <input id="checkbox-signup" type="checkbox" checked="checked">
                        <label for="checkbox-signup">
                            I accept <a href="#">Terms and Conditions</a>
                        </label>
                    </div>
                </div>
            </div>

            <div class="form-group text-right m-t-20">
                <div class="col-xs-12">
                    <asp:LinkButton runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-primary btn-custom waves-effect waves-light w-md" />

                </div>
            </div>

            <div class="form-group m-t-30">
                <div class="col-sm-12 text-center">
                    <a href="/account/login" class="text-muted">Already have account?</a>
                </div>
            </div>
        </div>

    </div>
    
</asp:Content>
