<%@ Page Title="" Language="C#" MasterPageFile="~/DotBambooEditGrid.master" AutoEventWireup="true" CodeFile="OverallMemoRequests.aspx.cs" Inherits="OverallPurchaseRequests" %>

<%@ MasterType VirtualPath="~/DotBambooEditGrid.master" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <script type="text/javascript">
        function OpenPrPage(id, prtype) {
            window.open('MemoRequest.aspx?ID=' + id, '_blank');
            
            return false;
        }
    </script>
    <h1>All Memo Request</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h5>Filter</h5>
            <table>
                <tr>
                    <td>
                        <telerik:RadDatePicker ID="StartDate" MinDate="2000/1/1" runat="server" DateInput-Label="StartDate"></telerik:RadDatePicker>
                        
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="EndDate" MinDate="2000/1/1" runat="server" DateInput-Label="EndDate"></telerik:RadDatePicker>
                        
                        <asp:CompareValidator ID="dateCompareValidator" runat="server" ControlToValidate="EndDate" ControlToCompare="StartDate" Operator="GreaterThanEqual" Type="Date" ErrorMessage="Enddate should more than Startdate"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <label>Search</label>&nbsp;<asp:TextBox ID="Search" runat="server" Width="300"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Current State</label>&nbsp;<asp:DropDownList ID="State" runat="server" Width="200">
                            <asp:ListItem Text="All" Value="9999" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Waiting Approver 1" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Waiting Approver 2" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Waiting Approver 3" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Waiting Approver 4" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Approved" Value="7"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <label>Status</label>&nbsp;<asp:DropDownList ID="Status" runat="server" Width="100">
                            <asp:ListItem Text="All" Value="9999" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Cancel" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <telerik:RadButton runat="server" Text="Query" ID="Query" OnClick="Query_Click"></telerik:RadButton>
            <hr />
            <br />
            <telerik:RadGrid RenderMode="Lightweight" Width="900px" runat="server" ID="RadGrid1" MasterTableView-PageSize="20" AllowPaging="True" AllowSorting="true" OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCommand="RadGrid1_ItemCommand" Culture="th-TH" Height="600px">
                <MasterTableView Width="900px" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="ID">
                    <HeaderStyle Width="120px" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="28px"></telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn ButtonType="ImageButton" Text="Delete" UniqueName="Delete" CommandName="Delete" Visible="false" HeaderStyle-Width="28px" ConfirmText="Do you want to cancel this PR request?" Exportable="false"></telerik:GridButtonColumn>
                        <telerik:GridBoundColumn DataField="ID" UniqueName="ID" Visible="false" HeaderText="ID"></telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn DataField="MemoDate" HeaderText="Date" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100"></telerik:GridDateTimeColumn>
                        <telerik:GridTemplateColumn DataField="MemoNumber" UniqueName="MemoNumberText" HeaderText="MemoNumber" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150" Exportable="false">
                            <ItemTemplate>
                                <%# "<a href='#' onclick=\"OpenPrPage("+ Eval("ID").ToString() +",1);\">" + Eval("MemoNumber").ToString() + "</a>"%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="MemoNumber" Visible="false" HeaderText="MemoNumber" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Subject" Visible="true" HeaderText="Subject" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="250"></telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn DataField="ApplicantName" HeaderText="ApplicantName" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="100"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Department" HeaderText="Department" ></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MemoStatusText" HeaderText="Status" ></telerik:GridBoundColumn>
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
