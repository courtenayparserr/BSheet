<%@ Page Title="" Language="C#" MasterPageFile="~/Insignia.Master" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="SaasTrack.Account.Employees" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .RadComboBoxDropDown .rcbImage {
            vertical-align: middle;
            height: 40px;
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
                var dataToSend = { user: userD, email: userEmail, service: 'employee'
                };
                
                $.ajax({
                    url: "/api/Intros",
                    type: "POST",
                    data: { "": JSON.stringify(dataToSend) },
                    success: function (res) {
                        window.location.href = '/account/employees';
                    },
                    error: function () {
                        console.log('error');
                    }
                });
            }
            $("#dontShowServicesButton").click(function () {
                buttonHandler();
            });
            $("#addEmployeesButton").click(function () 
            {
                //set cookie not to show again
                Cookies.set('showEmployee', 'false', { expires: 1 });   
                window.location.href = '/account/employees';
            });
        });
    </script>
    <div class="mail-body text-right tooltip-demo">
        <a href="javascript:__doPostBack('ctl00$MainContent$gridEmployees$ctl00$ctl02$ctl00$InitInsertButton','')" class="btn btn-sm btn-primary" data-toggle="tooltip" data-placement="top" title="" data-original-title="Send"><i class="fa fa-plus-circle"></i>&nbsp;&nbsp;Add New Employee</a>
        <a href="#" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" title="Share this report"><i class="fa fa-envelope-o"></i>&nbsp;&nbsp;Share report</a>
    </div>

    <div class="wrapper wrapper-content">
        <asp:Panel ID="addAccountsOrServices" Visible="false" runat="server" CssClass="dropdown-menu dropdown-messages asAModal">
            <div class="ibox ">
                <div class="ibox-title">
                    <h4>Adding Employees</h4>
                </div>
                <div class="ibox-content" style="word-wrap: break-word;">
                    <p class="font-bold  alert alert-success m-b-sm" style="display: block;">
                        Add employees you'd like to run the analytics service...
                    </p>
                    <p style="display: block;">
                        Add your employees so we can start giving you great insights into their SAAS usage. Simply add them below and we'll send them an email on how to get started.
                        <br />
                        <br />
                        Once you've added services you will need to add your employees so we can give you all the insight into usage and spend across all your services.   
                    </p>
                    <div>
                        <a href="#" id="addEmployeesButton" class="btn btn-sm btn-primary" data-toggle="tooltip" title="" data-original-title="Send"><i class="fa fa-plus-circle"></i>&nbsp;&nbsp;Add Employees Now</a>
                        <a href="#" id="dontShowServicesButton" class="btn btn-white btn-sm" data-toggle="tooltip" title="Cancel">&nbsp;&nbsp;Don't show me this again</a>
                        <a href="#" id="sandboxLinkButton" style="display: none;">Sand <i class="fa fa-plus-circle" aria-hidden="true"></i></a>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <telerik:RadGrid ID="gridEmployees" runat="server" AllowPaging="True"
            AllowSorting="True" OnNeedDataSource="gridEmployees_NeedDataSource" OnDeleteCommand="gridEmployees_DeleteCommand" Skin="Bootstrap" OnInsertCommand="gridEmployees_InsertCommand" OnUpdateCommand="gridEmployees_UpdateCommand">
            <MasterTableView AutoGenerateColumns="false" AllowSorting="true" EditMode="InPlace" CommandItemDisplay="Top" DataKeyNames="id">
                <EditFormSettings InsertCaption="Add new item"></EditFormSettings>
                <Columns>
                    <telerik:GridEditCommandColumn />
                    <telerik:GridButtonColumn ButtonType="LinkButton" CommandName="Delete"
                        FilterControlAltText="Filter DeleteColumn column" Text="Delete"
                        UniqueName="DeleteColumn" Resizable="false" ConfirmText="Remove employee?">
                        <HeaderStyle CssClass="rgHeader ButtonColumnHeader"></HeaderStyle>
                        <ItemStyle CssClass="ButtonColumn" />
                    </telerik:GridButtonColumn>
                    <telerik:GridBoundColumn DataField="id" UniqueName="id" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="firstName" HeaderText="First Name" UniqueName="firstName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="lastName" HeaderText="Last Name" UniqueName="lastName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="email" HeaderText="Email" UniqueName="email">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="ServiceCol" HeaderText="Registered Services">
                        <ItemTemplate>
                            <%# GetServicesForEmployee(Eval("services"))%>
                            <a href='EmployeeSettings.aspx?id=<%#Eval("id")%>' id="buttonAddServiceToEmployee" runat="server" class="btn btn-sm btn-primary" >Manage Services</a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <asp:Panel CssClass="overlay" Visible="false" runat="server" ID="overallOverlay">
    </asp:Panel>
</asp:Content>

