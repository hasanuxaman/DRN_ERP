<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmStoreReqApprDDL.aspx.cs" Inherits="DRN_WEB_ERP.Module.Inventory.Forms.frmStoreReqApprDDL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
                        
    </script>
    <div align="center" style="background-color: #00FF99">
        Pending Store Requisition
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <br />
                <asp:Panel ID="Panel1" runat="server" GroupingText="Pending Store Requisition">
                    <br />
                    <asp:GridView ID="gvPendPo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" Font-Size="8pt" OnRowCommand="gvPendPo_RowCommand" OnRowDataBound="gvPendPo_RowDataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" class="preview" Target="_blank" NavigateUrl='<%# Eval("EmpRefNo", "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo={0}") %>'
                                        runat="server">
                                        <asp:Image ID="AttachImage" runat="server" Height="50px" Width="65px" ImageUrl="~/Image/NoImage.gif" /></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Details">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpName" runat="server" Text='<%# GetEmpName(Eval("EmpRefNo").ToString())%>'
                                        Font-Bold="True"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblEmpId" runat="server" Text='<%# GetEmpId(Eval("EmpRefNo").ToString())%>'
                                        Font-Italic="True" ForeColor="#3399FF"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblEmpDes" runat="server" Text='<%# GetEmpDesig(Eval("EmpRefNo").ToString())%>'
                                        Font-Italic="True"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblEmpDep" runat="server" Text='<%# GetEmpDept(Eval("EmpRefNo").ToString())%>'
                                        Font-Italic="True"></asp:Label>
                                    <asp:HiddenField ID="hfEmpRef" runat="server" Value='<%# Bind("EmpRefNo") %>' />
                                    <asp:HiddenField ID="hfEmpSupRef" runat="server" Value='<%# Bind("EmpSuprId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ref. No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfPoRef" runat="server" Value='<%# Bind("PO_Hdr_Ref") %>' />
                                    <asp:HiddenField ID="hfPoDetLno" runat="server" Value='<%# Bind("PO_Det_Lno") %>' />
                                    <asp:Label ID="lblPoRefNo" runat="server" Text='<%# Bind("PO_Hdr_Ref") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblPoDt" runat="server" Text='<%# Bind("PO_Hdr_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PO_Hdr_Com3" HeaderText="Required For" />
                            <asp:BoundField DataField="PO_Hdr_Com1" HeaderText="Remarks" />
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblPoItemCode" runat="server" Text='<%# Bind("PO_Det_Icode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblPoItemName" runat="server" Text='<%# Bind("PO_Det_Itm_Desc") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#FFCCFF" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemUnit" runat="server" Text='<%# Bind("PO_Det_Itm_Uom") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Req. Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblPoReqQty" runat="server" Text='<%# Bind("PO_Det_Unt_Wgt") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="Aqua" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblPoQty" runat="server" Text='<%# Bind("PO_Det_Lin_Qty") %>'></asp:Label>--%>
                                    <asp:TextBox ID="txtPoQty" runat="server" Width="60px" CssClass="textAlignCenter"
                                        Text='<%# Bind("PO_Det_Lin_Qty") %>'></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtPoQty_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtPoQty"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </ItemTemplate>
                                <ItemStyle BackColor="Aqua" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stock">
                                <ItemTemplate>
                                    <asp:Label ID="lblStock" runat="server" Text='<%# GetCurStk(Eval("PO_Det_Str_Code").ToString(),Eval("PO_Det_Icode").ToString())%>'
                                        Font-Bold="True"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnInfo" runat="server" CommandName="Info" Height="20px"
                                        Width="20px" ImageUrl="~/Image/about-512.png" ToolTip='<%# Bind("Det_T_C1") %>'
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="btnReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                        OnClientClick="return confirm('Do you want to Reject?')" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="btnForward" runat="server" Text="Forward" CommandName="Forward" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                        OnClientClick="return confirm('Do you want to Forward?')" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CommandName="Approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                        OnClientClick="return confirm('Do you want to Approve?')" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="10pt" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                    <br />
                </asp:Panel>
                <br />
            </div>
            <br />
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="cboLeaveType" EventName="SelectedIndexChanged" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlMsgInfo" runat="server" Style="border-right: black 2px solid; padding-right: 20px;
        display: none; border-top: black 2px solid; padding-left: 20px; padding-bottom: 20px;
        border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
        background-color: #E1FBF1" ForeColor="Blue" Width="510px" Font-Size="Small">
        <table id="tblMsgInfo" runat="server" style="width: 100%;">
            <tr>
                <td bgcolor="#33CCCC" align="center">
                    <asp:Label ID="lblMsgInfo" runat="server" Font-Bold="True">Approval Status Info</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtMsgInfo" runat="server" Enabled="false" Height="110px" TextMode="MultiLine"
                        Width="500px" MaxLength="255" BackColor="#99FFCC"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <div align="center">
            <asp:Button ID="btnMsgInfoOk" runat="server" Text="OK" Width="80px" />
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hfHiddenMsgInfo" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupExtenderMsgInfo" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnMsgInfoOk" OkControlID="btnMsgInfoOk" PopupControlID="pnlMsgInfo"
        TargetControlID="hfHiddenMsgInfo" DropShadow="true">
    </cc1:ModalPopupExtender>
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
