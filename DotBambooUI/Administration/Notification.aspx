<%@ Page Title="" Language="C#" MasterPageFile="~/DotBambooEditPage.master"  AutoEventWireup="true" CodeFile="Notification.aspx.cs" Inherits="Notification" %>

<%@ MasterType virtualPath="~/DotBambooEditPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <h1>Email Notification Setup</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">    
    <ContentTemplate>
	<table>
		<tr>
			<td><label>Description</label></td>
			<td><asp:TextBox ID="Description" runat="server" Width="500"></asp:TextBox></td>
		</tr>
		<tr>
			<td><label>FromEmailAddress</label></td>
			<td><asp:TextBox ID="FromEmailAddress" runat="server" Width="500"></asp:TextBox></td>
		</tr>
		<tr>
			<td><label>Subject</label></td>
			<td><asp:TextBox ID="Subject" runat="server" Width="500"></asp:TextBox></td>
		</tr>
		<tr>
			<td><label>Body</label></td>
			<td><asp:TextBox ID="Body" runat="server" TextMode="MultiLine" Rows="10" Width="500"></asp:TextBox></td>
		</tr>
	</table>

    <hr />
    <table>
        <tr>
            <td>
                Suppport Tag:
            </td>
        </tr>
        <tr>
            <td>
                <label>For PR: &lt;PRNUMBER&gt;, &lt;REQUESTOR&gt;, &lt;SECTION&gt;, &lt;LINK&gt;</label>
            </td>
        </tr>
        <tr>
            <td>
                <label>For PO: &lt;PONUMBER&gt;, &lt;PRNUMBER&gt;, &lt;LINK&gt;, &lt;VENDOR&gt;</label>
            </td>
        </tr>
    </table>

	</ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>