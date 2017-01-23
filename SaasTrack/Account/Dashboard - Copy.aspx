<%@ Page Title="" Language="C#" MasterPageFile="~/Beam.Master" AutoEventWireup="true" CodeBehind="Dashboard - Copy.aspx.cs" Inherits="SaasTrack.Account.DashboardCopy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function showhide(el)
        {
            if ($("#inactiveSubsDiv").is(":visible"))
            {
                $("#inactiveSubsDiv").hide();
                $(el).text("Show inactive subscriptions");
            }
            else {
                $("#inactiveSubsDiv").show();
                $(el).text("Hide inactive subscriptions");
            }
        }
        function showhideRecurring(el) {
            if ($("#inactiveRecurringDiv").is(":visible")) {
                $("#inactiveRecurringDiv").hide();
                $(el).text("Show inactive recurring payments");
            }
            else {
                $("#inactiveRecurringDiv").show();
                $(el).text("Hide inactive recurring payments");
            }
        }
    </script>
    <div class="alert alert-info" id="emptyUserDash" runat="server" visible="false">
        <strong>Hailing on all frequencies!</strong> We're just processing your data. As soon as your subscriptions are ready, youll see them here.
    </div>

    <asp:Panel ID="dashPanel" runat="server">
        <div class="heading">
            <a href="#">Share Report <i class="fa fa-envelope-o" aria-hidden="true"></i></a>
            <h1>Overview</h1>
        </div>

        <div class="kpi-big">
            <div class="kpi-data">
                <div class="kpi" style="text-align: left; width: 37%;">
                    <div class="stat">
                        <div class="number">
                            <asp:Literal ID="monthlyAmount" runat="server"></asp:Literal>
                            <div class="title">
                                <a href="#">Monthly</a>
                            </div>
                        </div>
                        <asp:Literal ID="monthlyIncreaseDecrease" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="kpi" style="text-align: center; width: 37%;">
                    <div class="stat">

                        <div class="number">
                            <asp:Literal ID="annualAmount" runat="server"></asp:Literal>
                            <div class="title">
                                <a href="#">Annual</a>
                            </div>
                        </div>
                        <asp:Literal ID="annualIncreaseDecrease" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="kpi" style="text-align: right; width: 26%;">
                    <div class="stat">
                        <div class="number">
                            <asp:Literal ID="monthlySubs" runat="server"></asp:Literal>
                            <div class="title">
                                <a href="#">Subs</a>
                            </div>
                        </div>
                        <asp:Literal ID="monthlySubsIncreaseDecrease" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>

        <div class="heading spacer">
            Subscriptions
        </div>

        <div class="subscriptions">
            <asp:Repeater ID="subscriptions" runat="server">
                <ItemTemplate>
                    <div class="item" style='display: flex'>
                        <span class="subscriptions brand-image" style='<%# String.Format("height:70px; width:108px; background-size:contains; background-image: url({0});", Eval("MotherSubscriptionLogoUrl")) %>'></span>
                        <span class="brand-item">
                            <a class="brand-name" href="#"><%# GetShortenedName(Eval("Name")) %></a>
                            <div class="cycle">
                                <%# GetPeriod(Eval("Period")) %> / <%# GetShortDate(Eval("PreviousBillingDate")) %> <br /> <%# Eval("account.meta_name") + " (" + Eval("account.meta_number") + ")"%>

                            </div>
                        </span>
                        <span class="amount"><%# String.Format("{0:C}", Eval("LastPaymentAmount")).TrimEnd(')').TrimStart('(')  %></span>
                        <span class="settings"><i class="fa fa-cog cog" aria-hidden="true"></i></span>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="show-inactive">
            <a onclick="showhide(this);" style="cursor:pointer">Hide inactive subscriptions</a>
        </div>
        <div class="subscriptions" id="inactiveSubsDiv">
            <asp:Repeater ID="inactiveSubs" runat="server">
                <ItemTemplate>
                    <div class="item" style='display: flex; filter: grayscale(100%);'>
                        <span class="subscriptions brand-image" style='<%# String.Format("height:70px; width:108px; background-size:contains; background-image: url({0});", Eval("MotherSubscriptionLogoUrl")) %>'></span>
                        <span class="brand-item">
                            <a class="brand-name" href="#"><%# GetShortenedName(Eval("Name")) %></a>
                            <div class="cycle">
                                <%# GetPeriod(Eval("Period")) %> / <%# GetShortDate(Eval("PreviousBillingDate")) %> / <%# Eval("account.meta_name") + " (" + Eval("account.meta_number") + ")"%>

                            </div>
                        </span>
                        <span class="amount"><%# String.Format("{0:C}", Eval("LastPaymentAmount")).TrimEnd(')').TrimStart('(')  %></span>
                        <span class="settings"><i class="fa fa-cog cog" aria-hidden="true"></i></span>
                    </div>
                </ItemTemplate>

            </asp:Repeater>
        </div>

        <div class="heading spacer">
            Recurring Payments
        </div>

        <div class="subscriptions">
            <asp:Repeater ID="otherRecurring" runat="server">
                <ItemTemplate>
                    <div class="item" style='display: flex'>
                        <span class="subscriptions brand-image" style='<%# String.Format("height:70px; width:108px; background-size:contains; background-image: url({0});", Eval("MotherSubscriptionLogoUrl")) %>'></span>
                        <span class="brand-item">
                            <a class="brand-name" href="#"><%# GetShortenedName(Eval("Name")) %></a>
                            <div class="cycle">
                                <%# GetPeriod(Eval("Period")) %> / <%# GetShortDate(Eval("PreviousBillingDate")) %> <br /> <%# Eval("account.meta_name") + " (" + Eval("account.meta_number") + ")"%>

                            </div>
                        </span>
                        <span class="amount"><%# String.Format("{0:C}", Eval("LastPaymentAmount")).TrimEnd(')').TrimStart('(')  %></span>
                        <span class="settings"><i class="fa fa-cog cog" aria-hidden="true"></i></span>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="show-inactive">
            <a onclick="showhideRecurring(this);" style="cursor:pointer">Hide inactive recurring payments</a>
        </div>
        <div class="subscriptions" id="inactiveRecurringDiv">
            <asp:Repeater ID="inactiveRecurring" runat="server">
                <ItemTemplate>
                    <div class="item" style='display: flex; filter: grayscale(100%);'>
                        <span class="subscriptions brand-image" style='<%# String.Format("height:70px; width:108px; background-size:contain; background-image: url({0});", Eval("MotherSubscriptionLogoUrl")) %>'></span>
                        <span class="brand-item">
                            <a class="brand-name" href="#"><%# GetShortenedName(Eval("Name")) %></a>
                            <div class="cycle">
                                <%# GetPeriod(Eval("Period")) %> / <%# GetShortDate(Eval("PreviousBillingDate")) %> / <%# Eval("account.meta_name") + " (" + Eval("account.meta_number") + ")"%>

                            </div>
                        </span>
                        <span class="amount"><%# String.Format("{0:C}", Eval("LastPaymentAmount")).TrimEnd(')').TrimStart('(')  %></span>
                        <span class="settings"><i class="fa fa-cog cog" aria-hidden="true"></i></span>
                    </div>
                </ItemTemplate>

            </asp:Repeater>
        </div>

    </asp:Panel>


</asp:Content>
