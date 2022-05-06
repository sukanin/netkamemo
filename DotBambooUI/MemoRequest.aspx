<%@ Page Title="" Language="C#" MasterPageFile="~/DotBambooEditPage.master" AutoEventWireup="true" CodeFile="MemoRequest.aspx.cs" Inherits="PrGeneralRequest" %>

<%@ MasterType VirtualPath="~/DotBambooEditPage.master" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <script type="text/javascript">
        function setCustomPosition(sender, args) {
            sender.moveTo(sender.getWindowBounds().x, 100);
        }

        function Approver1PopUp() {
            popUpWindow("select_approver_1.aspx?" + Math.random(), 'SH1', 600, 350, 'no');
        }

        function Approver2PopUp() {
            popUpWindow("select_approver_2.aspx?" + Math.random(), 'SH1', 600, 350, 'no');
        }

        function Approver3PopUp() {
            popUpWindow("select_approver_3.aspx?" + Math.random(), 'SH1', 600, 350, 'no');
        }

        function Approver4PopUp() {
            popUpWindow("select_approver_4.aspx?" + Math.random(), 'SH1', 600, 350, 'no');
        }

        function CusCode1PopUp() {
            popUpWindow("select_acc_code.aspx?" + Math.random(), 'SH1', 600, 350, 'no');
        }
        function CusCode2PopUp() {
            popUpWindow("select_customer2.aspx?" + Math.random(), 'SH1', 600, 350, 'no');
        }
        function DownloadPopUp() {
            popUpWindow("download_pr_attachment.aspx?" + Math.random(), 'SH1', 600, 350, 'no');
        }
        function CostCenterPopUp() {
            popUpWindow("select_costcenter.aspx?" + Math.random(), 'SH1', 600, 350, 'no');
        }
        function AccountCodePopUp() {
            popUpWindow("select_accountcode.aspx?" + Math.random(), 'SH1', 600, 350, 'no');
        }
        function VendorCodePopUp() {
            popUpWindow("select_vendor.aspx?" + Math.random(), 'SH1', 600, 350, 'no');
        }
        function UnitPopUp() {
            popUpWindow("select_unit.aspx?" + Math.random(), 'SH1', 600, 350, 'no');
        }

        function popUpWindow(URL, N, W, H, S) {
            var winleft = (screen.width - W) / 2;
            var winup = (screen.height - H) / 2;
            winProp = 'width=' + W + ',height=' + H + ',left=' + winleft + ',top=' + winup + ',scrollbars=' + S + ',resizable' + ',status=yes'
            Win = window.open(URL, N, winProp);

            if (parseInt(navigator.appVersion) >= 4) {
                Win.window.focus();
            }
            return false;
        }
    </script>
    <h1>Memo Request&nbsp;&nbsp;&nbsp;<asp:Label ID="MemoNumber" runat="server" ForeColor="Navy"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Label ID="Status" runat="server" ForeColor="Navy"></asp:Label></h1>
    <asp:Label ID="ErrorMsg" runat="server" ForeColor="Red"></asp:Label>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <label>Request Date</label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="MemoDate" runat="server" MinDate="2000-01-01" SharedCalendarID="sharedCalendar"
                            Width="120px">
                            <DateInput runat="server" ID="DateInput">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <telerik:RadCalendar ID="sharedCalendar" runat="server" EnableMultiSelect="false"
                            RangeMinDate="2000/01/01">
                        </telerik:RadCalendar>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label>Department</label>
                    </td>
                    <td>
                        <asp:TextBox ID="Department" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Applicant Name</label>
                    </td>
                    <td>
                        <asp:TextBox ID="ApplicantName" runat="server"  ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Subject</label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="Subject" runat="server" MaxLength="500" Width="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Email CC</label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="EmailCC" runat="server" MaxLength="500" Width="500"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <hr />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td colspan="3" rowspan="3">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label>Details</label><br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Detail" TextMode="MultiLine" runat="server" Width="600px" Rows="20"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td>
                        <label>Attachments</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="RadAsyncUpload1" MultipleFileSelection="Automatic" EnableInlineProgress="true"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="RadGrid3" runat="server" AllowPaging="True" AllowSorting="true" AutoGenerateColumns="false" OnNeedDataSource="RadGrid3_NeedDataSource" OnItemCommand="RadGrid3_ItemCommand">
                            <MasterTableView DataKeyNames="ID">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="ID" DataField="ID" UniqueName="ID" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Name" DataField="Filename" UniqueName="Filename">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn>
                                       <ItemTemplate>
                                           <%# "<a href='#' onclick=\"window.open('download_attachment.aspx?ID=" + Eval("ID").ToString() + "','popup','position=relative,width=960,height=500,toolbar=yes,scrollbars=yes,resizable=yes');return false;\">Download</a>" %>
                                       </ItemTemplate>
                                   </telerik:GridTemplateColumn>
                                   <telerik:GridTemplateColumn>
                                       <ItemTemplate>
                                           <%# "<a href='#' onclick=\"window.open('download_attachment2.aspx?ID=" + Eval("ID").ToString() + "','popup','position=relative,width=960,height=500,toolbar=yes,scrollbars=yes,resizable=yes');return false;\">Download Excel</a>" %>
                                       </ItemTemplate>
                                   </telerik:GridTemplateColumn>
                               </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <h4>Reviewer/Approver 1</h4>
            <table>
                <tr>
                    <td>
                        <asp:RadioButton ID="Approver1Confirm" runat="server" GroupName="Approver1Confirm" Text="Approved" />
                    </td>
                    <td style="width:200px;">
                        <asp:RadioButton ID="Approver1Reject" runat="server" GroupName="Approver1Confirm" Text="Rejected" />
                    </td>
                    <td>
                        <asp:Label ID="Approver1ConfirmDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>By: <asp:TextBox ID="Approver1ConfirmBy" runat="server" ></asp:TextBox></label> <asp:HiddenField ID="Approver1ConfirmByID" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="SelectApprover1" runat="server" OnClientClick="Approver1PopUp();return false;" Text="Select Reviewer"></asp:Button>
                    </td>
                    <td></td>
                </tr>
                
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="ApproveRemark1" Width="500" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <h4>Reviewer/Approver 2</h4>
            <table>
                <tr>
                    <td>
                        <asp:RadioButton ID="Approver2Confirm" runat="server" GroupName="ReqConfirm" Text="Approved" />
                    </td>
                    <td style="width:200px;">
                        <asp:RadioButton ID="Approver2Reject" runat="server" GroupName="ReqConfirm" Text="Rejected" />
                    </td>
                    <td>
                        <asp:Label ID="Approver2ConfirmDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>By: <asp:TextBox ID="Approver2ConfirmBy" runat="server" ></asp:TextBox></label> <asp:HiddenField ID="Approver2ConfirmByID" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="SelectApprover2" runat="server" OnClientClick="Approver2PopUp();return false;" Text="Select Reviewer"></asp:Button>
                    </td>
                    <td></td>
                </tr>
                
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="ApproveRemark2" Width="500" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <h4>Reviewer/Approver 3</h4>
            <table>
                <tr>
                    <td>
                        <asp:RadioButton ID="Approver3Confirm" runat="server" GroupName="ReviewConfirm" Text="Approved" />
                    </td>
                    <td style="width:200px;">
                        <asp:RadioButton ID="Approver3Reject" runat="server" GroupName="ReviewConfirm" Text="Rejected" />
                    </td>
                    <td>
                        <asp:Label ID="Approver3ConfirmDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    
                    <td>
                        <label>By: <asp:TextBox  ID="Approver3ConfirmBy" runat="server"></asp:TextBox></label> <asp:HiddenField ID="Approver3ConfirmByID" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="SelectApprover3" runat="server" OnClientClick="Approver3PopUp();return false;" Text="Select Reviewer"></asp:Button>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="ApproveRemark3" Width="500" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
            <h4>Approver 4</h4>
            <table>
                <tr>
                    <td>
                        <asp:RadioButton ID="Approver4Confirm" runat="server" GroupName="ApproveConfirm" Text="Approved" />
                    </td>
                    <td style="width:200px;">
                        <asp:RadioButton ID="Approver4Reject" runat="server" GroupName="ApproveConfirm" Text="Rejected" />
                    </td>
                    <td>
                        <asp:Label ID="Approver4ConfirmDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    
                    <td>
                        <label>By: <asp:TextBox  ID="Approver4ConfirmBy" runat="server"></asp:TextBox></label> <asp:HiddenField ID="Approver4ConfirmByID" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="SelectApprover4" runat="server" OnClientClick="Approver4PopUp();return false;" Text="Select Approver"></asp:Button>
                    </td>
                    <td></td>
                </tr>
                
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="ApproveRemark4" Width="500" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel10" runat="server" Visible="false">
        <ContentTemplate>
            <h4>Cancel</h4>
            <table>
                <tr>
                    <td>
                        <label>Date: <asp:Label ID="CancelDate" runat="server"></asp:Label></label> 
                    </td>
                    <td>
                        <label>By: <asp:Label ID="CancelBy" runat="server"></asp:Label></label> <asp:HiddenField ID="HiddenField1" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel7" runat="server"  Visible="false">
        <ContentTemplate>
            <h4>Debug</h4>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="MemoStatus" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
</asp:Content>

