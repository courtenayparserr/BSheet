<%@ Page Title="" Language="C#" MasterPageFile="~/Insignia.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="SaasTrack.Account.Employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row border-bottom">
        <nav class="navbar navbar-static-top white-bg" role="navigation" style="margin-bottom: 0">
            <ul class="nav navbar-top-links navbar-right">
                <li>
                    <a class="navbar-minimalize minimalize-styl-2 btn btn-white" style="border:none !important; position:absolute !important; padding:0px !important;margin-top:-25px !important;margin-left:-45px !important;margin-right:0 !important;margin-bottom:0 !important;" href="http://webapplayers.com/inspinia_admin-v2.5/dashboard_2.html#">
                        <img alt="image" class="img-circle" style="width: 40px; padding:0px !important; border:none !important;" src="/app/content/insignia/images/default-avatar-v9899025.png" />
                    </a>
                </li>
                <li>
                    <asp:Literal ID="employeeName" runat="server"></asp:Literal></li>
                <li>
                    <span class="m-r-sm text-muted welcome-message"></span>
                </li>
                <li class="dropdown">
                    <a class="dropdown-toggle count-info" data-toggle="dropdown" href="http://webapplayers.com/inspinia_admin-v2.5/dashboard_2.html#">
                        <i class="fa fa-envelope"></i><span class="label label-warning">16</span>
                    </a>
                </li>
                <li class="dropdown">
                    <a class="dropdown-toggle count-info" data-toggle="dropdown" href="http://webapplayers.com/inspinia_admin-v2.5/dashboard_2.html#">
                        <i class="fa fa-bell"></i><span class="label label-primary">8</span>
                    </a>
                    <ul class="dropdown-menu dropdown-alerts">
                        <li>
                            <a href="http://webapplayers.com/inspinia_admin-v2.5/mailbox.html">
                                <div>
                                    <i class="fa fa-envelope fa-fw"></i>You have 16 messages
                                            <span class="pucanll-right text-muted small">4 minutes ago</span>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="http://webapplayers.com/inspinia_admin-v2.5/profile.html">
                                <div>
                                    <i class="fa fa-twitter fa-fw"></i>3 New Followers
                                            <span class="pull-right text-muted small">12 minutes ago</span>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="http://webapplayers.com/inspinia_admin-v2.5/grid_options.html">
                                <div>
                                    <i class="fa fa-upload fa-fw"></i>Server Rebooted
                                            <span class="pull-right text-muted small">4 minutes ago</span>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <div class="text-center link-block">
                                <a href="http://webapplayers.com/inspinia_admin-v2.5/notifications.html">
                                    <strong>See All Alerts</strong>
                                    <i class="fa fa-angle-right"></i>
                                </a>
                            </div>
                        </li>
                    </ul>
                </li>

                <li>
                    <a href="http://webapplayers.com/inspinia_admin-v2.5/login.html">
                        <i class="fa fa-sign-out"></i>Log out
                    </a>
                </li>
                <li>
                    <a class="right-sidebar-toggle">
                        <i class="fa fa-tasks"></i>
                    </a>
                </li>
            </ul>
        </nav>
    </div>
    <div class="wrapper wrapper-content">
        <div class="row">
            <div class="col-lg-3">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <span class="label label-info pull-right">Monthly Visits</span>
                        <h5>Employee engagement</h5>
                    </div>
                    <div class="ibox-content">
                        <h1 class="no-margins">2,850</h1>
                        <div class="stat-percent font-bold text-danger">5%<i class="fa fa-level-down"></i> <small>lower than peers</small></div>
                        <small>Total minutes across all applications</small><span class="stat-percent font-bold text-danger pull-right">8%<i class="fa fa-level-down"></i> <small>monthly</small></span>
                    </div>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <span class="label label-success pull-right">Monthly</span>
                        <h5>Subscriptions</h5>
                    </div>
                    <div class="ibox-content">
                        <h1 class="no-margins">5</h1>
                        <div class="stat-percent font-bold text-success">9% <i class="fa fa-level-up"></i></div>
                        <small>Total subscriptions for <asp:Literal ID="employeeName0" runat="server"></asp:Literal></small>
                    </div>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <span class="label label-info pull-right">Monthly</span>
                        <h5>Spend</h5>
                    </div>
                    <div class="ibox-content">
                        <h1 class="no-margins">$322</h1>
                        <div class="stat-percent font-bold text-info">5% <i class="fa fa-level-up"></i></div>
                        <small>Total monthly spend for
                            <asp:Literal ID="employeeName1" runat="server"></asp:Literal></small>
                    </div>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <span class="label label-info pull-right">Monthly</span>
                        <h5>Employee Feedback</h5>
                    </div>
                    <div class="ibox-content">
                        <h1 class="no-margins">6.8/10</h1>
                        <div class="stat-percent font-bold text-danger">38% <i class="fa fa-level-down"></i></div>
                        <small>Average rating of software given by
                            <asp:Literal ID="employeeName2" runat="server"></asp:Literal></small>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>
                            <asp:Literal ID="employeeName3" runat="server"></asp:Literal>
                            uses the following software:</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <asp:Repeater ID="employeeRepeater" runat="server">
                            <HeaderTemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Application</th>
                                            <th>Avg Monthly Mins</th>
                                            <th>Feedback</th>
                                            <th>Last Login</th>
                                            <th>Since</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <img src='<%# Eval("service.MasterServiceImageUrl") %>' style="width: 32px;" /></td>
                                    <td><%# Eval("service.MasterServiceName") %></td>
                                    <td><%# Eval("TotalMinutesSpent") %>&nbsp;mins</td>
                                    <td class="text-navy">9.2/10<span class="text-navy">&nbsp;&nbsp;<i class="fa fa-play fa-rotate-270"></i>&nbsp;+20%</span></td>
                                    <td><%# Eval("LastLogin", "{0:d}") %></td>
                                    <td><%# Eval("DateAdded", "{0:d}") %></td>
                                    <td><a class="btn btn-xs btn-danger"><i class="fa fa-times-circle"></i>&nbsp;Cancel Subscription</a></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Feed</h5>
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
                                        <b>
                                            <asp:Literal ID="employeeName4" runat="server"></asp:Literal></b> has not logged into Drip since <u>28/01/2014</u> but has an active account<br>
                                        <small class="text-muted">Today 5:50 pm - 12.06.2016</small>
                                        <div class="actions">
                                            <a class="btn btn-xs btn-white"><i class="fa fa-times"></i>&nbsp;Dismiss</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="feed-element">
                                    <button type="button" class="btn btn-primary m-r-sm pull-left"><i class="fa fa-check"></i></button>
                                    <div class="media-body ">
                                        <b>
                                            <asp:Literal ID="employeeName5" runat="server"></asp:Literal></b> signed into Intercom<br />
                                        <small class="text-muted">2 days ago at 10;30am</small>
                                        <div class="actions">
                                            <a class="btn btn-xs btn-white"><i class="fa fa-times"></i>&nbsp;Dismiss</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="feed-element">
                                    <button type="button" class="btn btn-primary m-r-sm pull-left"><i class="fa fa-check"></i></button>
                                    <div class="media-body ">
                                        <b>
                                            <asp:Literal ID="employeeName6" runat="server"></asp:Literal></b> signed up for Slack at $49/month<br>
                                        <small class="text-muted">2 days ago at 8:30am</small>
                                        <div class="actions">
                                            <a class="btn btn-xs btn-white"><i class="fa fa-times"></i>&nbsp;Dismiss</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="feed-element">
                                    <button type="button" class="btn btn-warning m-r-sm pull-left"><i class="fa fa-bell"></i></button>
                                    <div class="media-body ">
                                        <b>
                                            <asp:Literal ID="employeeName7" runat="server"></asp:Literal>'s </b>overall spend has increased by $23/month in the past 60 days<br>
                                        <small class="text-muted">5 days ago at 8:30am</small>
                                        <div class="actions">
                                            <a class="btn btn-xs btn-white"><i class="fa fa-times"></i>&nbsp;Dismiss</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <button class="btn btn-primary btn-block m-t"><i class="fa fa-arrow-down"></i>Show More</button>
                        </div>
                    </div>
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
</asp:Content>
