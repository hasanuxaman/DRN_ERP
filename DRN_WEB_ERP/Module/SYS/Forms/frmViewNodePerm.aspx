<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmViewNodePerm.aspx.cs" Inherits="DRN_WEB_ERP.frmViewNodePerm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Node Setup</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="border: 1px solid #800080; width: 100%; background-color: #FFCCFF;">
                <table>
                    <tr>
                        <td>
                            <asp:TreeView ID="tvMenu" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                                ShowLines="True" OnSelectedNodeChanged="tvMenu_SelectedNodeChanged">
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
            <div align="center" style="background-color: #6699FF">
                <br />
                <asp:GridView ID="gvNode" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#336666" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" EmptyDataText="No Data Found."
                    Font-Size="8pt" OnRowDeleting="gvNode_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="User_Ref_No" HeaderText="User Code" />
                        <asp:BoundField DataField="User_Name" HeaderText="User Name" />
                        <asp:BoundField DataField="Module_Name" HeaderText="Module Name" />
                        <asp:BoundField DataField="Node_Ref" HeaderText="Node Ref" />
                        <asp:BoundField DataField="Node_Code" HeaderText="Node Code" />
                        <asp:BoundField DataField="Node_Name" HeaderText="Node Name" />
                        <asp:BoundField DataField="Perm_Entry_Date" HeaderText="Entry Date" Visible="False" />
                        <asp:TemplateField HeaderText="Entry User" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetUserName(int.Parse(Eval("Perm_Entry_User").ToString())) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Perm_Update_Date" HeaderText="Update Date" />
                        <asp:TemplateField HeaderText="Update User">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# GetUserName(int.Parse(Eval("Perm_Update_User").ToString())) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    ImageUrl="~/Image/Delete.png" OnClientClick="return confirm('Do you want to delete?')"
                                    ToolTip="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" Font-Size="9pt"
                        HorizontalAlign="Left" Wrap="False" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px"
                        HorizontalAlign="Left" Wrap="False" Font-Size="8pt" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                </asp:GridView>
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlMsg" runat="server" Style="border-right: black 2px solid; padding-right: 20px;
        display: none; border-top: black 2px solid; padding-left: 20px; padding-bottom: 20px;
        border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
        background-color: #E1FBF1" ForeColor="Blue" Width="300px" Font-Size="Small">
        <table id="tblMsg" runat="server" style="width: 100%;">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <div align="center">
            <asp:Button ID="btnMsgOk" runat="server" Text="OK" Width="80px" />
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hfHidden" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupExtenderMsg" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnMsgOk" OkControlID="btnMsgOk" PopupControlID="pnlMsg" TargetControlID="hfHidden"
        DropShadow="true">
    </cc1:ModalPopupExtender>
</asp:Content>
