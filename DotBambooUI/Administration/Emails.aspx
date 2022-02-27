<%@ Page Title="" Language="C#" MasterPageFile="~/DotBambooEditGrid.master" AutoEventWireup="true" CodeFile="Emails.aspx.cs" Inherits="Emails" %>

<%@ MasterType VirtualPath="~/DotBambooEditGrid.master" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <h1>Email Log</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h5>Filter</h5>
            <table>
                <tr>
                    <td>
                        <label>Search</label></td>
                    <td>
                        <asp:TextBox ID="Search" runat="server"></asp:TextBox></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
            <asp:Button ID="Query" runat="server" OnClick="Query_Click" Text="Query" />
            <hr />
            <br />
            <telerik:RadGrid runat="server" ID="RadGrid1" AllowPaging="True" AllowSorting="true" OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCommand="RadGrid1_ItemCommand">
                <MasterTableView Width="100%" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridEditCommandColumn Visible="false" ButtonType="ImageButton" HeaderStyle-Width="28px" Exportable="false"></telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn ButtonType="ImageButton" Text="Delete" CommandName="Delete" HeaderStyle-Width="28px" Exportable="false" ConfirmText="Do you want to permanently delete this item?"></telerik:GridButtonColumn>
                        <telerik:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false" HeaderText="ID"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="InsertDate" HeaderText="DateTime"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ToEmailAddress" HeaderText="ToEmailAddress"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Subject" HeaderText="Subject"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EmailStatusFlag" HeaderText="Status"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>

        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <h5>Export Excel</h5>
    <br />
    <asp:Button ID="btnExport" runat="server" Text="Export excel" OnClick="btnExport_Click" />
    <hr />
</asp:Content>
