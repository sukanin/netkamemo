<%@ Page Title="" Language="C#" MasterPageFile="~/DotBambooEditPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Administration_ChangePassword" %>

<%@ MasterType virtualPath="~/DotBambooEditPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <h1>Change Password</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <label>Old Password</label>
                    </td>
                    <td>
                        <asp:TextBox ID="PasswordOld" runat="server" Width="100" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>New Password</label>
                    </td>
                    <td>
                        <asp:TextBox ID="PasswordNew" runat="server" Width="100" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

