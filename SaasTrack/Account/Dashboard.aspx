<%@ Page Title="" Language="C#" MasterPageFile="~/Insignia.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SaasTrack.Account.Dashboard" %>

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
    <div class="wrapper wrapper-content">
        <asp:Panel ID="addAccountsOrServices" Visible="false" runat="server" CssClass="dropdown-menu dropdown-messages asAModal">
            <div class="ibox ">
                <div class="ibox-title">
                    <h4>Welcome to Beam</h4>
                </div>
                <div class="ibox-content" style="word-wrap: break-word;">
                    <p class="font-bold  alert alert-success m-b-sm" style="display: block;">
                        Want to see how you can save money and have total control? Read on...
                    </p>
                    <p style="display: block;">
                        Beam automatically connects to your bank account and finds all your subscriptions for you. We've invested heavily in making sure our platform uses the most up-to-date industry protocols for storing your account data including 256-bit SSL encryption.
                        <br /><br />
                        When requesting statements from your financial institutions, bank-level security with read only access is utilized. This means your login credentials are never stored on our servers, and we never have access to make any changes to your accounts. If you’d like to know more about the API used to retrieve statement information, click <a href="http://plaid.com" target="_blank">here</a>.   
                    </p>
                    <div>
                        <a href="#" id="liveLinkButton" class="btn btn-sm btn-primary" data-toggle="tooltip" title="" data-original-title="Send"><i class="fa fa-plus-circle"></i>&nbsp;&nbsp;Add New Account</a>
                        <a href="/app/account/services" class="btn btn-white btn-sm" data-toggle="tooltip" title="Cancel">&nbsp;&nbsp;Cancel</a>
                        <a href="#" id="sandboxLinkButton" style="display: none;">Sand <i class="fa fa-plus-circle" aria-hidden="true"></i></a>
                    </div>
                </div>

            </div>
        </asp:Panel>
        <div class="row">
            <div class="col-lg-3">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <span class="label label-success pull-right">Monthly</span>
                        <h5>Total Spend</h5>
                    </div>
                    <div class="ibox-content">
                        <h2 class="no-margins" style="font-weight:bold;">
                            <asp:Literal ID="monthlyAmount" runat="server"></asp:Literal><small>/month</small></h2>
                        <asp:Literal ID="monthlySubsIncreaseDecrease" runat="server" Visible="false"></asp:Literal>
                        <small>You have <asp:Literal ID="monthlySubs" runat="server"></asp:Literal>&nbsp;total subscriptions across all of your employees</small>
                    </div>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <span class="label label-danger pull-right">Fix now</span>
                        <h5>Potential Savings</h5>
                    </div>
                    <div class="ibox-content">
                        <h1 class="no-margins" style="color:#ec4758; font-weight:bold;">$2,140
                            </h1>
                        <asp:Literal ID="monthlyIncreaseDecrease" runat="server" Visible="false"></asp:Literal>
                        <small>Calculated by inactive and under-utilized services/accounts. Click <a href="#">here</a> for a comprehensive report</small>
                    </div>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <span class="label label-danger pull-right">Fix now</span>
                        <h5>Inactive seats</h5>
                    </div>
                    <div class="ibox-content">
                        <h1 class="no-margins" style="color:#ec4758; font-weight:bold;">
                            46 <small style="color:#ec4758;">inactive seats</small><asp:Literal ID="annualAmount" runat="server" Visible="false"></asp:Literal></h1>
                        <asp:Literal ID="annualIncreaseDecrease" runat="server" Visible="false"></asp:Literal>
                        <small>Total user seats/subscriptions which have not been used for 1 month or more</small>
                    </div>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <span class="label label-info pull-right">Monthly Visits</span>
                        <h5>Employee engagement</h5>
                    </div>
                    <div class="ibox-content">
                        <h2 class="no-margins" style="font-weight:bold;">14hrs 05 mins</h2>
                        <div class="stat-percent font-bold text-info">5% <i class="fa fa-level-up"></i></div>
                        <small>Total time spent in hours and minutes across all applications</small>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Applications</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                            <a class="dropdown-toggle" data-toggle="dropdown" href="http://webapplayers.com/inspinia_admin-v2.5/table_basic.html#">
                                <i class="fa fa-wrench"></i>
                            </a>
                            <ul class="dropdown-menu dropdown-user">
                                <li>
                                    <a href="http://webapplayers.com/inspinia_admin-v2.5/table_basic.html#">Config option 1</a>
                                </li>
                                <li>
                                    <a href="http://webapplayers.com/inspinia_admin-v2.5/table_basic.html#">Config option 2</a>
                                </li>
                            </ul>
                            <a class="close-link">
                                <i class="fa fa-times"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Application</th>
                                    <th>Spend</th>
                                    <th>Account</th>
                                    <th>Renewal</th>
                                    <th>Last Billed</th>
                                    <th>Period</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="subscriptions" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="brand-image" style='<%# String.Format("height:70px; width:108px; background-size:contains; background-image: url({0});", Eval("MotherSubscriptionLogoUrl")) %>'></td>
                                            <td style="padding-top: 25px;"><%# GetShortenedName(Eval("Name")) %></td>
                                            <td style="padding-top: 25px;"><%# String.Format("{0:C}", Eval("LastPaymentAmount")).TrimEnd(')').TrimStart('(')  %></td>
                                            <td style="padding-top: 25px;"><%# Eval("account.meta_name") + " (" + Eval("account.meta_number") + ")"%></td>
                                            <td style="padding-top: 25px;">12/12/2016</td>
                                            <td style="padding-top: 25px;"><%# GetShortDate(Eval("PreviousBillingDate")) %></td>
                                            <td style="padding-top: 25px;"><%# GetPeriod(Eval("Period")) %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <asp:Panel CssClass="overlay" Visible="false" runat="server" ID="applicationsOverlay">
                    </asp:Panel>
                </div>
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Recurring payments</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                            <a class="dropdown-toggle" data-toggle="dropdown" href="http://webapplayers.com/inspinia_admin-v2.5/table_basic.html#">
                                <i class="fa fa-wrench"></i>
                            </a>
                            <ul class="dropdown-menu dropdown-user">
                                <li>
                                    <a href="http://webapplayers.com/inspinia_admin-v2.5/table_basic.html#">Config option 1</a>
                                </li>
                                <li>
                                    <a href="http://webapplayers.com/inspinia_admin-v2.5/table_basic.html#">Config option 2</a>
                                </li>
                            </ul>
                            <a class="close-link">
                                <i class="fa fa-times"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>Application</th>
                                    <th>Spend</th>
                                    <th>Users</th>
                                    <th>Account</th>
                                    <th>Renewal</th>
                                    <th>Since</th>
                                    <th>Period</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="otherRecurring" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="subscriptions brand-image" style='<%# String.Format("height:70px; width:108px; background-size:contains; background-image: url({0});", Eval("MotherSubscriptionLogoUrl")) %>'></td>
                                            <td><%# GetShortenedName(Eval("Name")) %></td>
                                            <td><%# String.Format("{0:C}", Eval("LastPaymentAmount")).TrimEnd(')').TrimStart('(')  %><span class="text-danger">&nbsp;&nbsp;<i class="fa fa-play fa-rotate-270"></i>&nbsp;+$19.99</span></td>
                                            <td>32</td>
                                            <td><%# Eval("account.meta_name") + " (" + Eval("account.meta_number") + ")"%></td>
                                            <td>12/12/2016</td>
                                            <td><%# GetShortDate(Eval("PreviousBillingDate")) %> (6 months)</td>
                                            <td><%# GetPeriod(Eval("Period")) %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <asp:Panel CssClass="overlay" Visible="false" runat="server" ID="recurringPaymentsOverlay">
                    </asp:Panel>
                </div>
            </div>
            <div class="col-md-4">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Spend Feed</h5>
                        <div class="ibox-tools">
                            <span class="label label-warning-light pull-right">10 Messages</span>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div>
                            <div class="feed-activity-list">
                                <div class="feed-element">
                                    <button type="button" class="btn btn-danger m-r-sm pull-left"><i class="fa fa-bell"></i></button>
                                    <div class="media-body ">
                                        <strong>Monica Smith</strong> has an active subscription to <u>JIRA</u> but has not signed in for 60 days (since 28/01/2016).
                                        <br>
                                        <small class="text-muted">Today 5:60 pm - 12.06.2014</small>
                                        <div class="actions">
                                            <a class="btn btn-xs btn-danger"><i class="fa fa-times-circle"></i>&nbsp;Cancel Subscription</a>
                                            <a class="btn btn-xs btn-white"><i class="fa fa-times"></i>&nbsp;Dismiss</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="feed-element">
                                    <button type="button" class="btn btn-danger m-r-sm pull-left"><i class="fa fa-bell"></i></button>
                                    <div class="media-body ">
                                        <strong>Mark Johnson</strong> has 5 active subscriptions worth $211/month but has not used them in 90 days
                                        <br>
                                        <small class="text-muted">Today 2:10 pm - 12.06.2014</small>
                                        <div class="actions">
                                            <a class="btn btn-xs btn-danger"><i class="fa fa-times-circle"></i>&nbsp;Cancel Subscriptions</a>
                                            <a class="btn btn-xs btn-white"><i class="fa fa-times"></i>&nbsp;Dismiss</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="feed-element">
                                    <button type="button" class="btn btn-primary m-r-sm pull-left"><i class="fa fa-check"></i></button>
                                    <div class="media-body ">
                                        <strong>Janet Rosowski</strong> signed up for a trial of Drip at $49/month<br>
                                        <small class="text-muted">2 days ago at 8:30am</small>
                                        <div class="actions">
                                            <a class="btn btn-xs btn-white"><i class="fa fa-times"></i>&nbsp;Dismiss</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="feed-element">
                                    <button type="button" class="btn btn-primary m-r-sm pull-left"><i class="fa fa-check"></i></button>
                                    <div class="media-body ">
                                        <strong>Janet Rosowski</strong> signed up for a trial of Drip at $49/month<br>
                                        <small class="text-muted">2 days ago at 8:30am</small>
                                        <div class="actions">
                                            <a class="btn btn-xs btn-white"><i class="fa fa-times"></i>&nbsp;Dismiss</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <button class="btn btn-primary btn-block m-t"><i class="fa fa-arrow-down"></i>Show More</button>
                        </div>
                    </div>
                    <asp:Panel CssClass="overlay" Visible="false" runat="server" ID="spendFeedOverlay">
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="footer">
            <div class="pull-right">
                10GB of <strong>250GB</strong> Free.
            </div>
            <div>
                <strong>Copyright</strong> Example Company © 2014-2015
            </div>
        </div>
        <asp:Panel CssClass="overlay" Visible="false" runat="server" ID="overallOverlay">
        </asp:Panel>

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
