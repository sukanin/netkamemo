<%@ Page Title="" Language="C#" MasterPageFile="~/DotBambooEditGrid.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Administration_Users" %>

<%@ MasterType VirtualPath="~/DotBambooEditGrid.master" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <h1>Users</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h5>Filter</h5>
            <table>
                <tr>
                    <td><label>Name</label></td>
                    <td><asp:TextBox id="Name" runat="server"></asp:TextBox></td>
                    <td><label>Position</label></td>
                    <td><asp:TextBox id="Position" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><label>Username</label></td>
                    <td><asp:TextBox id="Username" runat="server"></asp:TextBox></td>
                    <td><label>Email</label></td>
                    <td><asp:TextBox id="Email" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><label>Show Disable</label></td>
                    <td><asp:CheckBox ID="showDisable" runat="server" /></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
            <asp:Button ID="Query" runat="server" OnClick="Query_Click" Text="Query" />
            <hr />
            <br />
            <telerik:RadGrid runat="server" ID="RadGrid1" MasterTableView-PageSize="20" AllowPaging="True" AllowSorting="true" 
                OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCommand="RadGrid1_ItemCommand"
                AllowFilteringByColumn="true" OnItemCreated="RadGrid1_ItemCreated" RenderMode="Lightweight" Width="900px">
                <MasterTableView AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="ID">
                    <HeaderStyle Width="120px" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="28px" Exportable="false">
                        </telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn ButtonType="ImageButton" Text="Delete" CommandName="Delete" HeaderStyle-Width="28px" ConfirmText="Do you want to disable login for this username?" Exportable="false">
                        </telerik:GridButtonColumn>
                        <telerik:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false" HeaderText="ID"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="UserName" HeaderText="UserName">
                            <HeaderStyle Width="120px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Name" HeaderText="Name">
                            <HeaderStyle Width="120px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Position" HeaderText="Position">
                            <HeaderStyle Width="120px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Section" HeaderText="Department">
                            <HeaderStyle Width="120px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Email" HeaderText="Email">
                            <HeaderStyle Width="120px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsActive" HeaderText="Can Login">
                            <HeaderStyle Width="120px" />
                        </telerik:GridBoundColumn>
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

