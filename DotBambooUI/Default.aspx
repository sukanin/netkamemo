<%@ Page Title="" Language="C#" MasterPageFile="~/DotBamboo.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Netka - Home</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <h5>Memo Requests</h5>
            <br />
            <table>
            <tr>
                <td style="width:400px;text-align:center;">
                    <telerik:RadButton RenderMode="Lightweight" ID="Total" runat="server" Text="Total: " Width="230px" Height="65px" Font-Size="18px" Skin="Metro" BackColor="Wheat" AutoPostBack="true" OnClick="Total_Click"></telerik:RadButton>
                </td>
                <td style="width:400px;text-align:center;">
                    <telerik:RadButton RenderMode="Lightweight" ID="Todo" runat="server" Text="Todo: " Width="230px" Height="65px" Font-Size="18px" Skin="Metro" BackColor="Yellow" AutoPostBack="true" OnClick="Todo_Click"></telerik:RadButton>
                </td>
                <td style="width:400px;text-align:center;">
                    <telerik:RadButton RenderMode="Lightweight" ID="Pending" runat="server" Text="Pending: " Width="230px" Height="65px" Font-Size="18px" Skin="Metro" BackColor="Yellow" AutoPostBack="true" OnClick="Pending_Click"></telerik:RadButton>
                </td>
                <td style="width:400px;text-align:center;">
                    <telerik:RadButton RenderMode="Lightweight" ID="Completed" runat="server" Text="Completed: " Width="230px" Height="65px" Font-Size="18px" Skin="Metro" BackColor="Green" AutoPostBack="true" OnClick="Completed_Click"></telerik:RadButton>
                </td>
            </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        $(document).ready(function () {
            // your code

            $("#nav-switch").click();
            console.log("activate");
        });
    </script>
</asp:Content>

