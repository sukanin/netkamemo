<%@ Page Language="C#" AutoEventWireup="true" CodeFile="download_attachment.aspx.cs" Inherits="download_attachment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <title>Netka Memo System</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1, maximum-scale=1.0, user-scalable=no, height=device-height" />
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta name="author" content="Sukanin.M" />
    <meta name="description" content="" />
</head>
<body class="qsf-body demo-page">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="DotbambooScriptManager" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Path="../Common/Scripts/qsf-scripts.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadStyleSheetManager ID="DotbambooStyleSheetManager" runat="server">
            <StyleSheets>
                <telerik:StyleSheetReference Path="~/Common/Styles/qsf-styles.css" />
                <telerik:StyleSheetReference Path="~/Common/Styles/qsf-skin.css" />
            </StyleSheets>
        </telerik:RadStyleSheetManager>
        <div id="main">
            <h2>Download File</h2>
        </div>
    </form>
</body>
</html>
