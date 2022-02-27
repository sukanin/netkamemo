<%@ Page Title="" Language="C#" MasterPageFile="~/DotBambooEditGrid.master" AutoEventWireup="true" CodeFile="Notifications.aspx.cs" Inherits="Notifications" %>

<%@ MasterType virtualPath="~/DotBambooEditGrid.master"%>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <h1>Email Notification Setup</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <telerik:RadGrid runat="server" ID="RadGrid1" AllowPaging="True" AllowSorting="true" 
                OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCommand="RadGrid1_ItemCommand" RenderMode="Lightweight" Width="900px">
                <MasterTableView Width="100%" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="ID" AllowFilteringByColumn="true">
                    <Columns>
				        <telerik:GridEditCommandColumn ButtonType="ImageButton" Exportable="false" HeaderStyle-Width="28px"></telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn Visible="false"  ButtonType="ImageButton" Exportable="false" Text="Delete" CommandName="Delete" HeaderStyle-Width="28px" ConfirmText="Do you want to permanently delete this item?"></telerik:GridButtonColumn>
				        <telerik:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false" HeaderText="ID"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Description" HeaderText="Email Description"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="" />
                </ClientSettings>
            </telerik:RadGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="btnExport" runat="server" Text="Export excel" OnClick="btnExport_Click" />
</asp:Content>