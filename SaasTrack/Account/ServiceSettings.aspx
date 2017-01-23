<%@ Page Title="" Language="C#" MasterPageFile="~/Insignia.Master" AutoEventWireup="true" CodeBehind="ServiceSettings.aspx.cs" Inherits="SaasTrack.Account.ServiceSettings" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/dropzone/4.3.0/basic.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/dropzone/4.3.0/dropzone.css" rel="stylesheet" />
    <style type="text/css">
        .dz-max-files-reached {
            background-color: red;
        }
    </style>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/dropzone/4.3.0/min/dropzone.min.js"></script>
    <script type="text/javascript">
        //File Upload response from the server
        Dropzone.options.dropzoneForm = {
            maxFiles: 2,
            url: "/ebFormDropzone.aspx",
            init: function () {
                this.on("maxfilesexceeded", function (data) {
                    var res = eval('(' + data.xhr.responseText + ')');
                });
                this.on("addedfile", function (file) {
                    // Create the remove button
                    var removeButton = Dropzone.createElement("<button>Remove file</button>");
                    // Capture the Dropzone instance as closure.
                    var _this = this;
                    // Listen to the click event
                    removeButton.addEventListener("click", function (e) {
                        // Make sure the button click doesn't submit the form:
                        e.preventDefault();
                        e.stopPropagation();
                        // Remove the file preview.
                        _this.removeFile(file);
                        // If you want to the delete the file on the server as well,
                        // you can do the AJAX request here.
                    });
                    // Add the button to the file preview element.
                    file.previewElement.appendChild(removeButton);
                });
            }
        };
    </script>
    <div class="wrapper wrapper-content">
        <div class="row">
            <div class="col-lg-6">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>
                            <asp:Literal ID="serviceName" runat="server"></asp:Literal>
                            Settings</h5>
                    </div>
                    <div class="ibox-content">
                        <asp:Panel CssClass="alert alert-danger" ID="errorPanel" runat="server" Visible="false">
                                <asp:Literal ID="errorMessage" runat="server"></asp:Literal>
                            </asp:Panel>
                        <div role="form">
                            <div class="form-group" id="serviceUrlsLabel" runat="server">
                                <label>Service Urls:</label>
                                <asp:TextBox ID="serviceUrls" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                <span class="help-block m-b-none">Add all the URLs that need to be included in the Analytics service. Each url on a new line</span>
                            </div>
                            <div class="form-group">
                                <label>Spend on <asp:Literal ID="serviceName2" runat="server"></asp:Literal> per month:</label> <br />
                                <telerik:RadNumericTextBox Skin="Bootstrap" runat="server" ID="RadNumericTextBox2" Width="190px" Value="10" EmptyMessage="Price per month" Type="Currency" MinValue="0"></telerik:RadNumericTextBox>
                                <span class="help-block m-b-none">Hey! Did you know that we can automatically track <asp:Literal ID="serviceName3" runat="server"></asp:Literal> costs so you don't have to do it manually? Add your bank details here, or give us access to <asp:Literal ID="serviceName4" runat="server"></asp:Literal> here</span>
                            </div>
                            <div class="form-group">
                                <label>Service considered inactive after days:</label> <br />
                                <telerik:RadNumericTextBox Skin="Bootstrap" runat="server" ID="RadNumericTextBox1" Value="30" EmptyMessage="Number of days" Type="Number" NumberFormat-DecimalDigits="0" MaxValue="1000" MinValue="0"></telerik:RadNumericTextBox>
                                <span class="help-block m-b-none">Your dashboard will present you an inactive seats for your organsiation or per service 
                                   which helps understand which services are being used are which are not. At times some services may not need to be used
                                   for months but no user actively uses the application. We allow you to adjust this number based on the experience:<br /><br />
                                eg. Salesforce inactivity after 60 days should be flagged - do you still need that account?<br />
                                    eg. Godaddy inactivity after 60 days may not need to be flagged.
                                </span>
                            </div>
                            <div class="form-group">
                                <label>Have any paperwork or contracts associated with <asp:Literal ID="serviceName5" runat="server"></asp:Literal>? We'll keep it safe for you:</label>
                                <div class="jumbotron">
                                    <div class="dropzone" id="dropzoneForm">
                                        <div class="fallback">
                                            <input name="file" type="file" multiple />
                                            <input type="submit" value="Upload" />
                                        </div>
                                    </div>
                                </div>
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
