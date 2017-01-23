<%@ Page Title="" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="AddBankAccount - Copy.aspx.cs" Inherits="SaasTrack.Account.AddBankAccountCopy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="container">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <ol class="breadcrumb pull-right">
                            <li><a id="sandboxLinkButton">Sandbox</a></li>
                            <li><a id="liveLinkButton">Add Bank Account</a></li>
                            <li><a>Share Report</a></li>
                        </ol>
                        <h4 class="page-title">Accounts</h4>
                    </div>
                </div>
            </div>
            <asp:Repeater ID="bankAccounts" runat="server">
                <ItemTemplate>
                    <div class="row">
                        <div class="col-sm-12">
                            <h2 class="text-dark header-title"><%# Eval("meta_name") + " (" + Eval("meta_number") + ")"%></h2>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="widget-simple text-center card-box">
                                <div>
                                    <h2 class="counter inline">
                                        <%# GetAmount(Eval("Monthly")) %></h2>
                                    <i class="fa fa-caret-<%# GetUpDown(Eval("AnnualIncreaseDecrease")) %>" aria-hidden="true" style="font-size:18px;"></i><%# GetAmount(Eval("MonthlyIncreaseDecrease")) %>
                                </div>
                                <p class="text-muted">Monthly</p>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="widget-simple-chart text-center card-box">
                                <h2 class="counter inline">
                                    <%# GetAmount(Eval("Annual")) %></h2>
                                <i class="fa fa-caret-<%# GetUpDown(Eval("AnnualIncreaseDecrease")) %>" aria-hidden="true" style="font-size:18px;"></i><%# GetAmount(Eval("AnnualIncreaseDecrease")) %>
                                <p class="text-muted">Annual</p>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="widget-simple-chart text-center card-box">
                                <h2 class="counter inline">
                                    <%# GetSubs(Eval("Subs")) %></h2>
                                <i class="fa fa-caret-<%# GetUpDown(Eval("Subs")) %>" aria-hidden="true" style="font-size:18px;"></i><%# GetAmount(Eval("AnnualIncreaseDecrease")) %>
                                <p class="text-muted">Subs</p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>


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
                    longtail: true,
                    onSuccess: function (public_token, metadata)
                    {
                        debugger;
                        $.get('/api/bankaccount/' + public_token + ";" + <% Response.Write("'" + GetLocalUser() + "'"); %>, function (data, status) {
                            alert("Data: " + data + "\nStatus: " + status);
                        });

                        //window.location = '/api/get/' + public_token;
                    },
                });
                var liveHandler = Plaid.create({
                    clientName: 'Beam',
                    env: 'tartan',
                    product: 'connect',
                    longtail: true,
                    key: 'ca9eeb4133f494b49bd2cb72f902ab',
                    onSuccess: function (public_token, metadata)
                    {
                        debugger;
                        $.get('/api/bankaccount/' + public_token + ";" + <% Response.Write("'" + GetLocalUser() + "'"); %>, function (data, status) {
                            alert("Data: " + data + "\nStatus: " + status);
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
        </div>
    </div>
</asp:Content>
