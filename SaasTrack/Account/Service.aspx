<%@ Page Title="" Language="C#" MasterPageFile="~/Insignia.Master" AutoEventWireup="true" CodeBehind="Service.aspx.cs" Inherits="SaasTrack.Account.ServiceApp" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
    <div class="row border-bottom">
        <nav class="navbar navbar-static-top white-bg" role="navigation" style="margin-bottom: 0">
            <div class="navbar-header">
                <a class="navbar-minimalize minimalize-styl-2 btn btn-white" href="#">
                    <img src="https://zapier.cachefly.net/storage/developer/57b336375384ab62cc06e7e83d5c3622_2.32x32.png" style="width:32px" runat="server" id="appImage"/>
                </a>
                <div class="navbar-form-custom" action="http://webapplayers.com/inspinia_admin-v2.5/search_results.html">
                    <div class="form-group">
                        <span><asp:Literal ID="serviceName1" runat="server"></asp:Literal></span>                     
                    </div>
                </div>
            </div>
            <ul class="nav navbar-top-links navbar-right">
                <li>
                    <span class="m-r-sm text-muted welcome-message"></span>
                </li>
                <li class="dropdown">
                    <a class="dropdown-toggle count-info" data-toggle="dropdown" href="http://webapplayers.com/inspinia_admin-v2.5/dashboard_2.html#">
                        <i class="fa fa-envelope"></i><span class="label label-warning">16</span>
                    </a>
                    <ul class="dropdown-menu dropdown-messages">
                        <li>
                            <div class="dropdown-messages-box">
                                <a href="http://webapplayers.com/inspinia_admin-v2.5/profile.html" class="pull-left">
                                    <img alt="image" class="img-circle" src="./INSPINIA _ Dashboard v.2_files/a7.jpg">
                                </a>
                                <div>
                                    <small class="pull-right">46h ago</small>
                                    <strong>Mike Loreipsum</strong> started following <strong>Monica Smith</strong>.
                                    <br>
                                    <small class="text-muted">3 days ago at 7:58 pm - 10.06.2014</small>
                                </div>
                            </div>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <div class="dropdown-messages-box">
                                <a href="http://webapplayers.com/inspinia_admin-v2.5/profile.html" class="pull-left">
                                    <img alt="image" class="img-circle" src="./INSPINIA _ Dashboard v.2_files/a4.jpg">
                                </a>
                                <div>
                                    <small class="pull-right text-navy">5h ago</small>
                                    <strong>Chris Johnatan Overtunk</strong> started following <strong>Monica Smith</strong>.
                                    <br>
                                    <small class="text-muted">Yesterday 1:21 pm - 11.06.2014</small>
                                </div>
                            </div>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <div class="dropdown-messages-box">
                                <a href="http://webapplayers.com/inspinia_admin-v2.5/profile.html" class="pull-left">
                                    <img alt="image" class="img-circle" src="./INSPINIA _ Dashboard v.2_files/profile.jpg">
                                </a>
                                <div>
                                    <small class="pull-right">23h ago</small>
                                    <strong>Monica Smith</strong> love <strong>Kim Smith</strong>.
                                    <br>
                                    <small class="text-muted">2 days ago at 2:30 am - 11.06.2014</small>
                                </div>
                            </div>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <div class="text-center link-block">
                                <a href="http://webapplayers.com/inspinia_admin-v2.5/mailbox.html">
                                    <i class="fa fa-envelope"></i><strong>Read All Messages</strong>
                                </a>
                            </div>
                        </li>
                    </ul>
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
                        <span class="label label-success pull-right">Monthly</span>
                        <h5>Subscriptions</h5>
                    </div>
                    <div class="ibox-content">
                        <h1 class="no-margins">
                            <asp:Literal ID="serviceSubscriptions" runat="server"></asp:Literal></h1>
                        <div class="stat-percent font-bold text-success">9% <i class="fa fa-level-up"></i></div>
                        <small>Total <asp:Literal ID="Literal1" runat="server"></asp:Literal> subscriptions for all users</small>
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
                        <h1 class="no-margins">
                            <asp:Literal ID="serviceSubscriptionSpend" runat="server"></asp:Literal></h1>
                        <div class="stat-percent font-bold text-info">5% <i class="fa fa-level-up"></i></div>
                        <small>Total monthly spend on <asp:Literal ID="Literal2" runat="server"></asp:Literal></small>
                    </div>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <span class="label label-info pull-right">Monthly minutes</span>
                        <h5>Employee engagement</h5>
                    </div>
                    <div class="ibox-content">
                        <h1 class="no-margins">
                            <asp:Literal ID="serviceMonthlyMinutes" runat="server"></asp:Literal></h1>
                        <div class="stat-percent font-bold text-danger">5% <i class="fa fa-level-down"></i></div>
                        <small>Total minutes spent on
                            <asp:Literal ID="serviceName" runat="server"></asp:Literal></small>
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
                        <h1 class="no-margins">
                            <asp:Literal ID="serviceRatingOfTen" runat="server"></asp:Literal></h1>
                        <div class="stat-percent font-bold text-danger">38% <i class="fa fa-level-down"></i></div>
                        <small>Average rating by employees</small>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-8">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Employees using <asp:Literal ID="Literal3" runat="server"></asp:Literal></h5>
                    </div>
                    <div class="ibox-content">
                        <asp:Repeater ID="employeeRepeater" runat="server">
                            <HeaderTemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>First Name</th>
                                            <th>Last Name</th>
                                            <th>Last Login</th> 
                                            <th>Avg Monthly Mins</th>                                           
                                            <th>Member Since</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <img class="img-circle" src='content/insignia/images/default-avatar-v9899025.png' style="width: 32px;" /></td>
                                    <td><%# Eval("companyuser.FirstName") %></td>
                                    <td><%# Eval("companyuser.LastName") %></td>
                                    <td><%# Eval("LastLogin", "{0:d}") %></td>
                                    <td><%# Eval("TotalMinutesSpent") %>&nbsp;mins</td>
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
                        <h5><asp:Literal ID="Literal4" runat="server"></asp:Literal> Feed</h5>
                        <div class="ibox-tools">
                            <span class="label label-warning-light pull-right">10 Messages</span>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div>
                            <div class="feed-activity-list">
                                <div class="feed-element">
                                    <button type="button" class="btn btn-primary m-r-sm pull-left"><i class="fa fa-check"></i></button>
                                    <div class="media-body ">
                                        Overall engagement level with Slack has increased by <u>13%</u> since 28/01/2016<br>
                                        <small class="text-muted">Today 5:50 pm - 12.06.2014</small>
                                        <div class="actions">
                                            <a class="btn btn-xs btn-white"><i class="fa fa-times"></i>&nbsp;Dismiss</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="feed-element">
                                    <button type="button" class="btn btn-primary m-r-sm pull-left"><i class="fa fa-check"></i></button>
                                    <div class="media-body ">
                                        Overall rating by employees for Slack has increased by <u>44%</u> since 28/01/2016<br>
                                        <small class="text-muted">2 days ago at 10;30am</small>
                                        <div class="actions">
                                            <a class="btn btn-xs btn-white"><i class="fa fa-times"></i>&nbsp;Dismiss</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="feed-element">
                                    <button type="button" class="btn btn-primary m-r-sm pull-left"><i class="fa fa-check"></i></button>
                                    <div class="media-body ">
                                        <strong>Janet Rosowski</strong> signed up for Slack at $49/month<br>
                                        <small class="text-muted">2 days ago at 8:30am</small>
                                        <div class="actions">
                                            <a class="btn btn-xs btn-white"><i class="fa fa-times"></i>&nbsp;Dismiss</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="feed-element">
                                    <button type="button" class="btn btn-warning m-r-sm pull-left"><i class="fa fa-bell"></i></button>
                                    <div class="media-body ">
                                        Overall slack spend has increased by $233/month in the past 60 days<br>
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
    </div>
</asp:Content>
