<%@ Page Title="" Language="C#" MasterPageFile="~/Beam.Master" AutoEventWireup="true" CodeBehind="Dashboard - Copy.aspx.cs" Inherits="SaasTrack.Account.Dashboard_Copy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Start content -->
    <div class="content">
        <div class="container">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <ol class="breadcrumb pull-right">
                            <li><a href="#">Share Report</a></li>
                        </ol>
                        <h3 class="page-title">Overview</h3>
                    </div>
                </div>
            </div>

            <!-- start row -->
            <div class="row" style="margin-bottom: 20px">
                <div class="col-sm-4">
                    <div class="widget-simple text-center card-box">
                        <h2 class="counter">
                            <asp:Literal ID="monthlyAmount" runat="server"></asp:Literal></h2>
                        <p class="text-muted">Monthly</p>
                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="widget-simple-chart text-center card-box">
                        <h2 class="counter">
                            <asp:Literal ID="annualAmount" runat="server"></asp:Literal></h2>
                        <p class="text-muted">Annual</p>
                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="widget-simple-chart text-center card-box">
                        <h2 class="counter">
                            <asp:Literal ID="monthlySubs" runat="server"></asp:Literal></h2>
                        <p class="text-muted">Subs</p>
                    </div>
                </div>
            </div>
            <!-- end row -->
            <!-- start row -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <ol class="breadcrumb pull-right">
                            <li><a href="#">Missing subscriptions?</a></li>
                        </ol>
                        <h3 class="page-title">Subscriptions</h3>
                    </div>
                </div>
            </div>

            <asp:Repeater ID="subscriptions" runat="server">
                <HeaderTemplate>
                    <div class="panel panel-default m-t-20">
                        <div class="panel-body p-0">
                            <div class="table-responsive">
                                <table class="table table-hover mails m-0">
                                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="">
                            <img src='<%# Eval("MotherSubscriptionLogoUrl") %>' style=" max-width:140px"/>
                        </td>
                        <td>
                            <div><%# Eval("account.institution_type") + "(" + Eval("account.meta_number") + ")" %> </div>                            
                            <a href="mail-read.html"><%# Eval("Name") %></a>
                        </td>
                        <td>
                            <div>
                                Sep2 / Monthly
                            </div>
                            </div>$25</div>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                                            </table>
                                        </div>

                                    </div>
                    <!-- panel body -->
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <!-- end container -->
    </div>
    <!-- end content -->
</asp:Content>
