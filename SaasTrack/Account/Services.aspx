<%@ Page Title="" Language="C#" MasterPageFile="~/Insignia.Master" AutoEventWireup="true" CodeBehind="Services.aspx.cs" Inherits="SaasTrack.Account.Services" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .RadComboBoxDropDown .rcbImage
        { 
            vertical-align: middle; height:40px;
        }
    </style>
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

            function buttonHandler()
            {
                var userD = <% Response.Write("'" + GetLocalUser() + "'"); %>;
                var userEmail = <% Response.Write("'" + GetLocalUserEmail() + "'"); %>;
                var dataToSend = { user: userD, email: userEmail, service: 'services'
                };
                
                debugger;
                $.ajax({
                    url: "/api/Intros",
                    type: "POST",
                    data: { "": JSON.stringify(dataToSend) },
                    success: function (res) {
                        window.location.href = '/account/services';
                    },
                    error: function () {
                        console.log('error');
                    }
                });
            }
            $("#dontShowServicesButton").click(function () {
                buttonHandler();
            });
            $("#addServicesButton").click(function () 
            {
                //set cookie not to show again
                Cookies.set('showServices', 'false', { expires: 1 });   
                window.location.href = '/account/services';
            });
        });
    </script>
    <div class="mail-body text-right tooltip-demo">
        <a href="javascript:__doPostBack('ctl00$MainContent$gridServices$ctl00$ctl02$ctl00$InitInsertButton','')" class="btn btn-sm btn-primary" data-toggle="tooltip" data-placement="top" title="" data-original-title="Send"><i class="fa fa-plus-circle"></i>&nbsp;&nbsp;Add New Service</a>
        <a href="#" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" title="Share this report"><i class="fa fa-envelope-o"></i>&nbsp;&nbsp;Share report</a>
    </div>

    <div class="wrapper wrapper-content">
        <asp:Panel ID="addAccountsOrServices" Visible="false" runat="server" CssClass="dropdown-menu dropdown-messages asAModal">
            <div class="ibox ">
                <div class="ibox-title">
                    <h4>Adding Services</h4>
                </div>
                <div class="ibox-content" style="word-wrap: break-word;">
                    <p class="font-bold  alert alert-success m-b-sm" style="display: block;">
                        Add services/applications or any other tools you'd like gain insights into...
                    </p>
                    <p style="display: block;">
                        Beam automatically connects to your bank account and finds all the software/services you're paying for. You can also add those services manually by clicking on the "Add Service" button and adding those details.  
                        <br /><br />
                        Once you've added services you will need to add your employees so we can give you all the insight into usage and spend across all your services.   
                    </p>
                    <div>
                        <a href="#" id="addServicesButton" class="btn btn-sm btn-primary" data-toggle="tooltip" title="" data-original-title="Send"><i class="fa fa-plus-circle"></i>&nbsp;&nbsp;Add Services Now</a>
                        <a href="#" id="dontShowServicesButton" class="btn btn-white btn-sm" data-toggle="tooltip" title="Cancel">&nbsp;&nbsp;Don't show me this again</a>
                        <a href="#" id="sandboxLinkButton" style="display: none;">Sand <i class="fa fa-plus-circle" aria-hidden="true"></i></a>
                    </div>
                </div>

            </div>
        </asp:Panel>
        <telerik:RadGrid ID="gridServices" runat="server" AllowPaging="True"
            AllowSorting="True" OnItemDataBound="gridServices_ItemDataBound" OnItemCommand="gridServices_ItemCommand" OnNeedDataSource="gridServices_NeedDataSource" Skin="Bootstrap" OnInsertCommand="gridServices_InsertCommand" OnUpdateCommand="gridServices_UpdateCommand">
            <MasterTableView AutoGenerateColumns="false" AllowSorting="true" EditMode="InPlace" CommandItemDisplay="Top">
                <EditFormSettings InsertCaption="Add new item"></EditFormSettings>
                <Columns>
                    <telerik:GridEditCommandColumn />
                    <telerik:GridTemplateColumn ReadOnly="true" HeaderText="Settings" UniqueName="Settings">
                        <ItemTemplate>
                          <asp:HyperLink ID="settingsLink" runat="server" NavigateUrl='<%# "~/Account/ServiceSettings.aspx?Id=" + Eval("Id")%>' Text="Settings"></asp:HyperLink>
                        </ItemTemplate>                        
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="id" UniqueName="id" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="ServiceCol" HeaderText="Service">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "MasterServiceName")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadComboBox CssClass="ServicesDropDown" ID="RadCombobox1" runat="server" OnItemsRequested="RadCombobox1_ItemsRequested"
                                AllowCustomText="true" EnableLoadOnDemand="True" AutoPostBack="true"
                                >
                            </telerik:RadComboBox>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>                    
                    <telerik:GridBoundColumn ReadOnly="true" HeaderText="Name" DataField="MasterServiceName" UniqueName="MasterServiceName">
                    </telerik:GridBoundColumn>   
                    <telerik:GridBoundColumn ReadOnly="true" HeaderText="Url" DataField="MasterServiceUrl" UniqueName="MasterServiceUrl">
                    </telerik:GridBoundColumn> 
                    <telerik:GridTemplateColumn ReadOnly="true" HeaderTooltip="Indicates whether or not the service was auto-detected by our anayltics service for users across your organisation" HeaderText="Auto-detected by Analytics Service (?)" DataField="Autodetected" UniqueName="Autodetected">
                        <ItemTemplate>
                          <asp:Label ID="Label1" runat="server" Text='<%# Eval("Autodetected")%>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn> 
                    <telerik:GridTemplateColumn ReadOnly="true" HeaderTooltip="Indicates whether or not the service was found on your bank account feed and therefore added to your list of services" HeaderText="Added by Bank Feed (?)" DataField="AddedByBankFeed" UniqueName="AddedByBankFeed">
                        <ItemTemplate>
                          <asp:Label ID="Label2" runat="server" Text='<%# Eval("AddedByBankFeed")%>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>                    
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <telerik:RadToolTipManager RenderMode="Lightweight" ID="RadToolTipManager1" runat="server" RelativeTo="Element"
                               Position="MiddleRight" AutoTooltipify="true" ContentScrolling="Default" Width="150">
    </telerik:RadToolTipManager>
    <asp:Panel CssClass="overlay" Visible="false" runat="server" ID="overallOverlay">
    </asp:Panel>
</asp:Content>

