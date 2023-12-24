<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmSalesDOPend.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmPendSo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        
    </script>
    <div align="center" style="background-color: #00FF99">
        Pending D/O List
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <br />
                <asp:GridView ID="gvPendDoDet" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#E7E7FF" BorderStyle="None" CellPadding="3" Font-Size="8pt" OnRowDataBound="gvPendDoDet_RowDataBound"
                    PageSize="25">
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="D/O Ref. No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfDoHdrRef" runat="server" Value='<%# Bind("DO_Hdr_Ref") %>' />
                                <asp:HiddenField ID="hfDoDetLno" runat="server" Value='<%# Bind("DO_Det_Lno") %>' />
                                <asp:HiddenField ID="hfHdrStat" runat="server" Value='<%# Bind("DO_Hdr_Status") %>' />
                                <asp:Label ID="lblDoHdrRefNo" runat="server" Text='<%# Bind("DO_Hdr_Ref_No") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Itm_Det_Desc" HeaderText="Item Desc." />
                        <asp:BoundField DataField="DO_Det_Itm_Uom" HeaderText="Unit" />
                        <asp:BoundField DataField="DO_Hdr_Date" DataFormatString="{0:d}" HeaderText="Del. Date" />
                        <asp:TemplateField HeaderText="D/O Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblPendDoQty" runat="server" Text='<%# Bind("DO_Det_Lin_Qty", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#00CC66" Font-Bold="True" HorizontalAlign="Center" Width="90px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Free Bag">
                            <ItemTemplate>
                                <asp:Label ID="lblPendDoFreeQty" runat="server" Text='<%# Bind("DO_Det_Free_Qty", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="SO_Det_Lin_Rat" DataFormatString="{0:n}" HeaderText="Ord. Rate">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblPendDoLinAmt" runat="server" Text='<%# String.Format( "{0:n}", (Convert.ToDecimal(Eval("DO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Lin_Rat"))) ) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#CC33FF" HorizontalAlign="Right" Width="90px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="DO_Det_Trans_Rat" DataFormatString="{0:n}" HeaderText="Trans. Rate">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Net Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblPendDoAmt" runat="server" Text='<%# Bind("DO_Det_Lin_Net", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#FF6699" Font-Bold="True" HorizontalAlign="Right" Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnCancelDo" runat="server" OnClick="btnCancelDo_Click" OnClientClick="return confirm('Do you want to Cancel?')"
                                    Text="Cancel" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnFwdDo" runat="server" OnClick="btnFwdDo_Click" OnClientClick="return confirm('Do you want to Forward?')"
                                    Text="Forward" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" Font-Size="8" ForeColor="#F7F7F7"
                        Wrap="false" />
                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E7E7FF" Font-Size="8" ForeColor="#4A3C8C" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                </asp:GridView>
                <br />
                <asp:GridView ID="gvPendOrdDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvPendOrdDet_RowDataBound"
                    OnSelectedIndexChanged="gvPendOrdDet_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="15px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Par_Adr_Name" HeaderText="Customer Name">
                            <ItemStyle Width="180px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="SP Name">
                            <ItemTemplate>
                                <asp:Label ID="lblSpName" runat="server" Text='<%# GetSpName(Eval("SO_Hdr_Com4").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ord. Ref. No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfOrdHdrRef" runat="server" Value='<%# Bind("SO_Hdr_Ref") %>' />
                                <asp:HiddenField ID="hfOrdDetLno" runat="server" Value='<%# Bind("SO_Det_Lno") %>' />
                                <asp:Label ID="lblOrdHdrRefNo" runat="server" Text='<%# Bind("SO_Hdr_Ref_No") %>'></asp:Label>
                                <asp:Label ID="lblOrdDate" runat="server" Text='<%# Bind("SO_Hdr_Date") %>' Font-Italic="True"
                                    ForeColor="#3399FF" Font-Size="8"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Desc.">
                            <ItemTemplate>
                                <asp:HiddenField ID="HfICode" runat="server" Value='<%# Bind("SO_Det_Icode") %>' />
                                <asp:Label ID="lblIDesc" runat="server" Text='<%# Bind("SO_Det_Itm_Desc") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="SO_Det_Itm_Uom" HeaderText="Unit">
                            <ItemStyle BackColor="#99FFCC" HorizontalAlign="Right" Width="20px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdQty" runat="server" Text='<%# Convert.ToDecimal(Eval("SO_Det_Lin_Qty")).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#00CC66" Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="SO_Det_Lin_Rat" DataFormatString="{0:F2}" HeaderText="Rate"
                            Visible="False">
                            <ItemStyle BackColor="#FFFF99" HorizontalAlign="Right" Width="40px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Amount" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblAmt" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Lin_Rat")))).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#0099FF" Font-Bold="True" HorizontalAlign="Right" Width="70px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Free Bag">
                            <ItemTemplate>
                                <asp:Label ID="lblAppFreeQty" runat="server" Text='<%# Bind("SO_Det_Free_Qty", "{0:F2}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="SO_Det_Trans_Rat" DataFormatString="{0:F2}" HeaderText="Trans. Rate"
                            Visible="False">
                            <ItemStyle HorizontalAlign="Right" Width="40px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SO_Det_Lin_Amt" DataFormatString="{0:F2}" HeaderText="Net Amount"
                            Visible="False">
                            <ItemStyle BackColor="#CC9900" Font-Bold="True" HorizontalAlign="Right" Width="70px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Tot. Qty (with free bag)">
                            <ItemTemplate>
                                <asp:Label ID="lblTotQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) + Convert.ToDecimal(Eval("SO_Det_Free_Qty")))).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#99FFCC" HorizontalAlign="Center" Width="30px" Font-Bold="True" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="D/O Qty (with free bag)">
                            <ItemTemplate>
                                <asp:Label ID="lblDoQty" runat="server" Text='<%# Bind("SO_Det_DO_Qty") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="D/O Balance (with free bag)">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfSoBalQty" runat="server" Value='<%# Bind("SO_Det_DO_Bal_Qty", "{0:F2}") %>' />
                                <asp:HiddenField ID="hfSoFreeBalQty" runat="server" Value='<%# Convert.ToDecimal(Eval("SO_Det_Ext_Data2")).ToString("N2") %>' />
                                <%--<asp:Label ID="lblDoBalQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("SO_Det_DO_Bal_Qty")) + Convert.ToDecimal(Eval("SO_Det_Ext_Data2")))).ToString("N2") %>'></asp:Label>--%>
                                <asp:Label ID="lblDoBalQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("SO_Det_DO_Bal_Qty")))).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#33CCCC" Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnProceed" runat="server" OnClick="btnProceed_Click" Text="Proceed" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Wrap="True" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
                <br />
                <br />
            </div>
        </ContentTemplate>
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
