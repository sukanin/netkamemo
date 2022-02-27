<%@ Page Language="C#" AutoEventWireup="true" CodeFile="select_approver_4.aspx.cs" Inherits="select_approver_4" %>

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
            <h2>Vendor</h2>
            <table>
                <tr>
                    <td><label>Search</label></td>
                    <td><asp:TextBox id="Search" runat="server" Width="200"></asp:TextBox></td>
                </tr>
            </table>
            <asp:Button ID="Query" runat="server" OnClick="Query_Click" Text="Query" />
            <hr />
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <telerik:RadGrid runat="server" ID="RadGrid1" EnableTheming="true" AllowFilteringByColumn="false" AllowPaging="True" AllowSorting="true" OnNeedDataSource="RadGrid1_NeedDataSource">
                <MasterTableView Width="100%" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="ID">
                    <Columns>
                        
                        <telerik:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false" HeaderText="ID"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn DataField="ID" HeaderText="ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# "<a href='#' onclick=\"window.opener.document.forms[0].ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder3_Approver4ConfirmBy.value='" + Eval("Username").ToString() + "';window.close();\">select</a>"%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        
                        <telerik:GridBoundColumn DataField="Username" HeaderText="Username"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Name" HeaderText="Name"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Section" HeaderText="Department"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Position" HeaderText="Position"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
                </ContentTemplate>
            </asp:UpdatePanel>
            
        </div>
    </form>
</body>
</html>
