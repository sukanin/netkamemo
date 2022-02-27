<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <title>Netka Memo System</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1, maximum-scale=1.0, user-scalable=no, height=device-height" />
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta name="author" content="Sukanin.M" />
    <meta name="description" content="" />
    <link type="image/x-icon" href="favicon.ico" rel="shortcut icon" />
</head>
<body class="qsf-body demo-page">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="DotbambooScriptManager" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Path="~/Common/Scripts/qsf-scripts.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadStyleSheetManager ID="DotbambooStyleSheetManager" runat="server">
            <StyleSheets>
                <telerik:StyleSheetReference Path="~/Common/Styles/qsf-styles.css" />
                <telerik:StyleSheetReference Path="~/Common/Styles/qsf-skin.css" />
            </StyleSheets>
        </telerik:RadStyleSheetManager>
        <div id="header">
            <div id="logo">
                <a href="<%= ResolveUrl( "~/" ) %>" title="Application" class="telerik-logo">
                    <asp:Image runat="server" ID="TelerikLogoImage" ImageUrl="~/Common/Images/telerik-logo2.png" AlternateText="Application" />
                </a>
            </div>
        </div>
        <div id="middle">
            <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="all" DecorationZoneID="main"></telerik:RadFormDecorator>

            <div id="main">
                <h5>Login to MEMO System</h5>
                <br />
                <div>
                    <table>
                        <tr>
                            <td><label>Username</label></td>
                            <td>
                                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><label>Password</label></td>
                            <td>
                                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnLogin" runat="server" Text="Login" CommandName="Login" OnCommand="Page_Command" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
