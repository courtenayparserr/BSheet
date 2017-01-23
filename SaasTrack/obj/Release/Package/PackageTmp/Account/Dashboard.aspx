<%@ Page Title="" Language="C#" MasterPageFile="~/Beam.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SaasTrack.Account.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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
                    <div class="trend"><i class="fa fa-caret-up" aria-hidden="true"></i>$254</div>
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
                    <div class="trend"><i class="fa fa-caret-down" aria-hidden="true"></i>$254</div>
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
                    <div class="trend"><i class="fa fa-caret-up" aria-hidden="true"></i>25</div>
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
                <div class="item" style='<%# String.Format("background-image: url({0});", Eval("MotherSubscriptionLogoUrl")) %>'>
                    <span><a class="brand-name" href="#"><%# GetShortenedName(Eval("Name")) %></a> </span>
                    <span class="cycle"><%# GetPeriod(Eval("Period")) %> / <%# GetShortDate(Eval("PreviousBillingDate")) %></span>
                    <span class="settings"><i class="fa fa-cog cog" aria-hidden="true"></i></span>
                    <span class="amount"><%# String.Format("{0:C}", Eval("LastPaymentAmount")).TrimEnd(')').TrimStart('(')  %></span>
                </div>
            </ItemTemplate>

        </asp:Repeater>

    </div>
    </asp:Panel>
    

</asp:Content>
