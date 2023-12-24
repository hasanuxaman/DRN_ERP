<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmGlGrpTree.aspx.cs" Inherits="DRN_WEB_ERP.Module.Accounts.Forms.frmGlGrpTree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        GL Group Tree</div>
    <div align="center" style="border: 1px solid #800080; width: 100%; background-color: #FFCCFF;">
        <table>
            <tr>
                <td>
                    <asp:TreeView ID="tvGlGrp" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                        ShowLines="True">
                        <HoverNodeStyle Font-Underline="False" />
                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                            NodeSpacing="0px" VerticalPadding="2px" />
                        <ParentNodeStyle Font-Bold="False" />
                        <SelectedNodeStyle Font-Underline="False" />
                    </asp:TreeView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
