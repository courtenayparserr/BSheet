<%@ Page Title="" Language="C#" MasterPageFile="~/Beam.Master" AutoEventWireup="true" CodeBehind="AddBankAccount.aspx.cs" Inherits="SaasTrack.Account.AddBankAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="container">

            <div class="heading">
                <a href="#">Share Report <i class="fa fa-envelope-o" aria-hidden="true"></i></a>
                <a href="#" id="liveLinkButton">Add <i class="fa fa-plus-circle" aria-hidden="true"></i></a>
                <a href="#" id="sandboxLinkButton"  style="display:none;">Sand <i class="fa fa-plus-circle" aria-hidden="true"></i></a>
                <h1>Accounts</h1>
            </div>
            <br />
            <div class="alert alert-info" id="emptyRepeater" runat="server" visible="false">
                <strong>Beam me up Scotty!</strong> Add your bank account by clicking the "Add" link above.
            </div>
            <asp:Repeater ID="bankAccounts" runat="server">
                <ItemTemplate>
                    <div class="kpi-big stacked">

                        <div class="kpi-header">
                            <i class="fa fa-cog cog" aria-hidden="true"></i>
                            <h1><a href="#"><%# Eval("meta_name") + " (" + Eval("meta_number") + ")"%></a></h1>
                        </div>
                        <div class="kpi-data">
                            <div class="kpi" style="text-align: left; width: 37%;">
                                <div class="stat">
                                    <!--
                                    -->
                                    <div class="number">
                                        <%# GetAmount(Eval("Monthly")) %>
                                        <div class="title">
                                            <a href="#">Monthly</a>
                                        </div>
                                    </div>
                                    <div class="trend"><i class="fa fa-caret-<%# GetUpDown(Eval("MonthlyIncreaseDecrease")) %>" aria-hidden="true"></i><%# GetAmount(Eval("MonthlyIncreaseDecrease")) %></div>
                                </div>

                            </div>
                            <div class="kpi" style="text-align: center; width: 37%;">
                                <div class="stat">
                                    <!--
                                    -->
                                    <div class="number">
                                        <%# GetAmount(Eval("Annual")) %>
                                        <div class="title">
                                            <a href="#">Annual</a>
                                        </div>
                                    </div>
                                    <div class="trend"><i class="fa fa-caret-<%# GetUpDown(Eval("AnnualIncreaseDecrease")) %>" aria-hidden="true"></i><%# GetAmount(Eval("AnnualIncreaseDecrease")) %></div>
                                </div>

                            </div>
                            <div class="kpi" style="text-align: right; width: 26%;">
                                <div class="stat">
                                    <div class="number">
                                        <%# GetSubs(Eval("Subs")) %>
                                        <div class="title">
                                            <a href="#">Subs</a>
                                        </div>
                                    </div>
                                    <div class="trend"><i class="fa fa-caret-<%# GetUpDown(Eval("Subs")) %>" aria-hidden="true"></i><%# GetUpDown(Eval("Subs")) %></div>
                                </div>

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
                        $("div[id$='emptyRepeater']").html('<div class="fa fa-spinner fa-spin"></div>&nbsp;Give us a moment while we sort through your data. Your subscriptions should be ready in the next 10 minutes');
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
                        $("div[id$='emptyRepeater']").html('<div class="fa fa-spinner fa-spin"></div>&nbsp;Give us a moment while we sort through your data. Your subscriptions should be ready in the next 10 minutes');
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
