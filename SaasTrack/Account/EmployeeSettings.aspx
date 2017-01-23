<%@ Page Title="" Language="C#" MasterPageFile="~/Insignia.Master" AutoEventWireup="true" CodeBehind="EmployeeSettings.aspx.cs" Inherits="SaasTrack.Account.EmployeeSettings" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
    <div class="wrapper wrapper-content">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>
                            <asp:Literal ID="employeeName" runat="server"></asp:Literal>'s
                            Settings</h5>
                    </div>
                    <div class="ibox-content">
                        <asp:Panel CssClass="alert alert-danger" ID="errorPanel" runat="server" Visible="false">
                            <asp:Literal ID="errorMessage" runat="server"></asp:Literal>
                        </asp:Panel>
                        <div role="form">
                            <div class="form-group">
                                <label>Employee Services</label><br />
                                <telerik:RadListBox ID="servicesList" runat="server" AllowTransfer="true" 
                                    TransferToID="servicesUsed" ButtonSettings-AreaWidth="35px" Height="300px" Width="200px" AllowTransferDuplicates="false" AllowTransferOnDoubleClick="true"></telerik:RadListBox>                                
                                <telerik:RadListBox ID="servicesUsed" runat="server" Height="300px" Width="200px"></telerik:RadListBox>
                                <span class="help-block m-b-none">Add all the URLs that need to be included in the Analytics service. Items on the right hand side that are disabled have already had minutes spent</span>
                            </div>                            
                            <div>
                                <asp:Button ID="saveSettings" OnClick="saveSettings_Click" runat="server" CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" Text="Save Settings" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
