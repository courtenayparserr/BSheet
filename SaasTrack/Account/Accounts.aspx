<%@ Page Title="" Language="C#" MasterPageFile="~/Insignia.Master" AutoEventWireup="true" CodeBehind="Accounts.aspx.cs" Inherits="SaasTrack.Account.Accounts" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row border-bottom">
        <nav class="navbar navbar-static-top white-bg" role="navigation" style="margin-bottom: 0">
            <div class="navbar-header">
                <a class="navbar-minimalize minimalize-styl-2 btn btn-primary " href="#"><i class="fa fa-bars"></i></a>
            </div>
            <ul class="nav navbar-top-links navbar-right">
                <li>
                    <span class="m-r-sm text-muted welcome-message">Welcome to Beam</span>
                </li>
                <li>
                    <a href="#">
                        <i class="fa fa-sign-out"></i>Log out
                    </a>
                </li>
            </ul>
        </nav>
    </div>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
    <script type="text/javascript">
        $(document).ready(function () {
            $('a[id$="InitInsertButton"]').hide();
            $('input[id$="AddNewRecordButton"]').hide();
        });
    </script>
    <div class="mail-body text-right tooltip-demo">
        <a href="#" id="liveLinkButton" class="btn btn-sm btn-primary" data-toggle="tooltip" data-placement="top" title="" data-original-title="Send"><i class="fa fa-plus-circle"></i>&nbsp;&nbsp;Add New Account</a>
        <a href="#" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" title="Share this report"><i class="fa fa-envelope-o"></i>&nbsp;&nbsp;Share report</a>
        <a href="#" id="sandboxLinkButton" style="display: none;">Sand <i class="fa fa-plus-circle" aria-hidden="true"></i></a>
    </div>
    <div class="wrapper wrapper-content">
        <telerik:RadGrid ID="gridAccounts" runat="server" AllowPaging="True"
            AllowSorting="True" OnNeedDataSource="gridAccounts_NeedDataSource" Skin="Bootstrap">
            <MasterTableView AutoGenerateColumns="false" AllowSorting="true" EditMode="InPlace" CommandItemDisplay="Top">
                <EditFormSettings InsertCaption="Add new item"></EditFormSettings>
                <Columns>
                    <telerik:GridBoundColumn DataField="id" UniqueName="id" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="Account" UniqueName="account">
                        <ItemTemplate>
                            <%# Eval("meta_name") + " (" + Eval("meta_number") + ")"%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Monthly Spend" UniqueName="monthlyspend">
                        <ItemTemplate>
                            <%# GetAmount(Eval("Monthly")) %>
                            <div class="trend"><i class="fa fa-caret-<%# GetUpDown(Eval("MonthlyIncreaseDecrease")) %>" aria-hidden="true"></i><%# GetAmount(Eval("MonthlyIncreaseDecrease")) %></div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Annual Spend" UniqueName="annualspend">
                        <ItemTemplate>
                            <%# GetAmount(Eval("Annual")) %>
                            <div class="trend"><i class="fa fa-caret-<%# GetUpDown(Eval("AnnualIncreaseDecrease")) %>" aria-hidden="true"></i><%# GetAmount(Eval("AnnualIncreaseDecrease")) %></div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Subscriptions" UniqueName="subscriptions">
                        <ItemTemplate>
                            <%# GetSubs(Eval("Subs")) %>
                            <div class="trend"><i class="fa fa-caret-<%# GetUpDown(Eval("Subs")) %>" aria-hidden="true"></i><%# GetUpDown(Eval("Subs")) %></div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    
    <script src="https://cdn.plaid.com/link/stable/link-initialize.js"></script>
    <script>
        // This demo uses two custom initializers so that we can provide
        // both a sandbox and live demo on the same page.
        // See: https://github.com/plaid/link/blob/master/README.md#step-2-custom-integration
        var sandboxHandler = Plaid.create({
            clientName: 'Beam',
            env: 'tartan',
            product: 'connect',
            key: 'test_key',
            onSuccess: function (public_token, metadata)
            {
                $("div[id$='emptyRepeater']").html('<div class="fa fa-spinner fa-spin"></div>&nbsp;Give us a moment while we sort through your data. Your subscriptions should be ready in the next 10 minutes');
                $.get('/api/bankaccount/' + public_token + ";" + <% Response.Write("'" + GetLocalUser() + "'"); %>, function (data, status) {
                    alert("Thanks for submitting your bank account. We're busy processing your data right now! Status: " + status);
                });

                //window.location = '/api/get/' + public_token;
            },
        });
        var liveHandler = Plaid.create({
            clientName: 'Beam',
            env: 'tartan',
            product: 'connect',
            key: 'ca9eeb4133f494b49bd2cb72f902ab',
            onSuccess: function (public_token, metadata)
            {
                $("div[id$='emptyRepeater']").html('<div class="fa fa-spinner fa-spin"></div>&nbsp;Give us a moment while we sort through your data. Your subscriptions should be ready in the next 10 minutes');
                $.get('/api/bankaccount/' + public_token + ";" + <% Response.Write("'" + GetLocalUser() + "'"); %>, function (data, status) {
                    alert("Thanks for submitting your bank account. We're busy processing your data right now! Status: " + status);
                });
            },
        });
        // Open the "Institution Select" view using the sandbox Link handler.
        document.getElementById('sandboxLinkButton').onclick = function() {
            sandboxHandler.open();
        };
        // Open the "Institution Select" view using the live Link handler.
        document.getElementById('liveLinkButton').onclick = function() {
            liveHandler.open();
        };
    </script>
</asp:Content>
