<%@ Page Title="" Language="C#" MasterPageFile="~/DotBambooEditPage.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="Administration_User" %>

<%@ MasterType VirtualPath="~/DotBambooEditPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <h1>User</h1>
    <table>
        <tr>
            <td>
                <label>Username</label></td>
            <td>
                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtUsername" ErrorMessage="กรุณาระบุรหัสผู้ใช้งาน"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <label>Password</label></td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtPassword" ErrorMessage="กรุณาระบุรหัสผ่าน"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <label>Name</label></td>
            <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <label>Position</label></td>
            <td>
                <asp:TextBox ID="txtPosition" runat="server"></asp:TextBox></td>
        </tr>
        
        <tr>
            <td>
                <label>Section</label></td>
            <td>
                <asp:TextBox ID="txtSection" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <label>Email</label></td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><label>Can Login</label></td>
            <td>
                <asp:CheckBox ID="chkActive" runat="server" Visible="true" />
            </td>
        </tr>
    </table>
</asp:Content>

