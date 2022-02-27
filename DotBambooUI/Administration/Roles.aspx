<%@ Page Title="" Language="C#" MasterPageFile="~/DotBambooEditGrid.master" AutoEventWireup="true" CodeFile="Roles.aspx.cs" Inherits="Administration_Roles" %>

<%@ MasterType VirtualPath="~/DotBambooEditGrid.master" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <h1>Roles</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h5>Filter</h5>
            <table>
                <tr>
                    <td><label>Role</label></td>
                    <td><asp:TextBox id="RoleName" runat="server"></asp:TextBox></td>
                </tr>
            </table>
            <asp:Button ID="Query" runat="server" OnClick="Query_Click" Text="Query" />
            <hr />
            <br />
            <telerik:RadGrid runat="server" ID="RadGrid1" MasterTableView-PageSize="20" AllowFilteringByColumn="false" 
                AllowPaging="True" AllowSorting="true" OnNeedDataSource="RadGrid1_NeedDataSource" 
                OnItemCommand="RadGrid1_ItemCommand" RenderMode="Lightweight" Width="900px">
                <MasterTableView Width="100%" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="ID" AllowFilteringByColumn="true">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="28px" Exportable="false"></telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn ButtonType="ImageButton" Text="Delete" CommandName="Delete" HeaderStyle-Width="28px" ConfirmText="Do you want to permanently delete this item?" Exportable="false"></telerik:GridButtonColumn>
                        <telerik:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false" HeaderText="ID"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RoleName" HeaderText="Role"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="" />
                </ClientSettings>
            </telerik:RadGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <h5>Save as Excel</h5>
    <br />
    <asp:Button ID="btnExport" runat="server" Text="Export excel" OnClick="btnExport_Click" />
    <hr />
</asp:Content>

