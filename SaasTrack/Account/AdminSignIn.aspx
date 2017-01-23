<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="AdminSignIn.aspx.cs" Inherits="SaasTrack.Account.AdminSignIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
    <asp:Button ID="signIn" runat="server" Text="Button" OnClick="signIn_Click" />
</asp:Content>

