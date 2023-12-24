<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmSalesReturnApp.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesReturnApp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Sales Retun Approval
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <br />
                <asp:Panel ID="Panel1" runat="server" GroupingText="Pending Sales Return">
                    <br />
                    <asp:GridView ID="gvPendRtn" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Size="8pt" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Return Ref. No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfChlnDetLno" runat="server" Value='<%# Bind("Trn_Det_Ord_Det_Lno") %>' />
                                    <asp:HiddenField ID="hfDoRef" runat="server" Value='<%# Bind("Trn_Det_Bin_Code") %>' />
                                    <asp:HiddenField ID="hfDoRefNo" runat="server" Value='<%# Bind("Trn_Det_Bat_No") %>' />
                                    <asp:HiddenField ID="hfDoDetLno" runat="server" Value='<%# Bind("Trn_Det_Exp_Lno") %>' />
                                    <asp:HiddenField ID="hfCustRef" runat="server" Value='<%# Bind("Trn_Hdr_Pcode") %>' />
                                    <asp:HiddenField ID="hfRtnRef" runat="server" Value='<%# Bind("Trn_Hdr_Ref") %>' />
                                    <asp:HiddenField ID="hfChlnStrCode" runat="server" Value='<%# Bind("Trn_Det_Str_Code") %>' />
                                    <asp:Label ID="lblRtnRefNo" runat="server" Text='<%# Bind("Trn_Hdr_Ref_No") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="120px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Trn_Hdr_T_C1" HeaderText="Customer Name">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Challan Ref No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfChlnRef" runat="server" Value='<%# Bind("Trn_Det_Ord_Ref") %>' />
                                    <asp:Label ID="lblChlnRefNo" runat="server" Text='<%# Bind("Trn_Det_Ord_Ref_No") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="120px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Trn_Hdr_T_C2" HeaderText="Date">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Item Desc">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfICode" runat="server" Value='<%# Bind("Trn_Det_Icode") %>' />
                                    <asp:Label ID="lblIDesc" runat="server" Text='<%# Bind("Trn_Det_Itm_Desc") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("Trn_Det_Itm_Uom") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rtn. Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblRtnQty" runat="server" Text='<%# (Convert.ToDecimal(Eval("Trn_Det_Lin_Qty"))).ToString("N2") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Free Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblRtnFreeQty" runat="server" Text='<%# Convert.ToDecimal(Eval("Trn_Det_Unt_Wgt")).ToString("N2") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Return Qty">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("Trn_Det_Lin_Qty")) + Convert.ToDecimal(Eval("Trn_Det_Unt_Wgt")))).ToString("N2") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50px" Font-Bold="True" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="30px"
                            Wrap="False" />
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
                <asp:Button ID="btnShowRpt" runat="server" Text="Show Approved Sales Return" OnClick="btnShowRpt_Click" />
                <asp:GridView ID="gvAppRtn" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333" Visible="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Return Ref. No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfOrdRef0" runat="server" Value='<%# Bind("Trn_Det_Ord_Ref") %>' />
                                <asp:HiddenField ID="hfOrdRefNo0" runat="server" Value='<%# Bind("Trn_Det_Ord_Ref_No") %>' />
                                <asp:HiddenField ID="hfOrdDetLno0" runat="server" Value='<%# Bind("Trn_Det_Ord_Det_Lno") %>' />
                                <%--<asp:HiddenField ID="hfChlnRef0" runat="server" Value='<%# Bind("Trn_Hdr_DC_No") %>' />--%>
                                <asp:Label ID="lblRtnRefNo0" runat="server" Text='<%# Bind("Trn_Det_Ref") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="130px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Trn_Hdr_T_C1" HeaderText="Customer Name">
                            <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Challan Ref No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfChlnRef0" runat="server" Value='<%# Bind("Trn_Det_Ord_Ref") %>' />
                                <asp:Label ID="lblChlnRefNo0" runat="server" Text='<%# Bind("Trn_Det_Ord_Ref_No") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="130px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Trn_Hdr_T_C2" HeaderText="Date">
                            <ItemStyle Width="50px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Item Desc">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfICode0" runat="server" Value='<%# Bind("Trn_Det_Icode") %>' />
                                <asp:Label ID="lblIDesc0" runat="server" Text='<%# Bind("Trn_Det_Itm_Desc") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="250px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblUnit0" runat="server" Text='<%# Bind("Trn_Det_Itm_Uom") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rtn. Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblChlnQty0" runat="server" Text='<%# (Convert.ToDecimal(Eval("Trn_Det_Lin_Qty"))).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rtn. Free Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblChlnFreeQty0" runat="server" Text='<%# Convert.ToDecimal(Eval("Trn_Det_Unt_Wgt")).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Rtn. Qty">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("Trn_Det_Lin_Qty")) + Convert.ToDecimal(Eval("Trn_Det_Unt_Wgt")))).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="30px"
                        Wrap="False" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <br />
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvPendRtn" EventName="RowDataBound" />
            <asp:AsyncPostBackTrigger ControlID="btnShowRpt" EventName="Click" />
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
