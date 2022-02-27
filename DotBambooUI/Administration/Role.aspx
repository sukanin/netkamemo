<%@ Page Title="" Language="C#" MasterPageFile="~/DotBambooEditPage.master" AutoEventWireup="true" CodeFile="Role.aspx.cs" Inherits="Administration_Role" %>

<%@ MasterType virtualPath="~/DotBambooEditPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
     <h1>Role</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">    
    <ContentTemplate>
    <table>
        <tr>
            <td><label>Role</label></td>
            <td><asp:TextBox ID="txtRoleName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2"><label>Authorization</label></td>            
        </tr>
        <tr>
            <td colspan="2">
                <asp:Table id="tblCapabilities" runat="server"></asp:Table>                
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <td colspan="3">
                            <label><asp:Label ID="lblUserHeader" runat="server" 
                                Text="Select username in this group"></asp:Label></label>
                        </td>
                    </tr>
                    <tr>
                        <td><label><asp:Label ID="lblUsers" runat="server" Text="Username"></asp:Label></label></td>
                        <td>&nbsp;</td>
                        <td>
                            <label><asp:Label ID="Label2" runat="server" Text="Username in this group"></asp:Label></label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="lstUnselectedUsers" runat="server" Rows="10" SelectionMode="Multiple" Width="300"></asp:ListBox>
                        </td>
                        <td>
                            
                            <asp:Button ID="btnMoveToSelected" runat="server" Text=">" onclick="btnMoveToSelected_Click" Width="40" /><br />
                            <asp:Button ID="btnMoveAllToSelected" runat="server" Text=">>" 
                                onclick="btnMoveAllToSelected_Click" Width="40" /><br />
                            <br />
                            <asp:Button ID="btnMoveToUnselected" runat="server" Text="<" onclick="btnMoveToUnselected_Click" Width="40" /><br />
                            <asp:Button ID="btnMoveAllToUnselected" runat="server" Text="<<" 
                                onclick="btnMoveAllToUnselected_Click" Width="40" /><br />
                        </td>
                        <td>
                            <asp:ListBox ID="lstSelectedUsers" runat="server" Rows="10" SelectionMode="Multiple" Width="300"></asp:ListBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

