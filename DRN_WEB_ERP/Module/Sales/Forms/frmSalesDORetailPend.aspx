<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmSalesDORetailPend.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesDORetailPend" %>

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
                <asp:GridView ID="gvPendDoDetRtl" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333" OnSelectedIndexChanged="gvPendDoDetRtl_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="15px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Par_Adr_Name" HeaderText="Customer Name">
                            <ItemStyle Width="170px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="DO Ref. No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfDoHdrRef" runat="server" Value='<%# Bind("DO_Hdr_Ref") %>' />
                                <asp:Label ID="lblDoHdrRefNo" runat="server" Text='<%# Bind("DO_Det_Ref_No") %>'></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lblDoDate" runat="server" Text='<%# Bind("DO_Hdr_Date")%>' Font-Italic="True"
                                    ForeColor="#3399FF" Font-Size="8"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="115px" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Delivery Address" DataField="DO_Hdr_T_C1">
                            <ItemStyle Width="190px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Ord. Ref. No">
                            <ItemTemplate>
                                <%--<asp:HiddenField ID="hfOrdDetRef" runat="server" Value='<%# Bind("DO_Det_SO_Ref") %>' />--%>
                                <%--<asp:HiddenField ID="hfOrdDetLno" runat="server" Value='<%# Bind("DO_Det_SO_Lno") %>' />--%>
                                <asp:Label ID="lblOrdHdrRefNo" runat="server" Text='<%# Bind("DO_Det_SO_Ref_No") %>'></asp:Label>
                                <asp:Label ID="lblOrdDate" runat="server" Text='<%# Bind("SO_Hdr_Date") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Desc.">
                            <ItemTemplate>
                                <%--<asp:HiddenField ID="HfICode" runat="server" Value='<%# Bind("DO_Det_Icode") %>' />--%>
                                <asp:Label ID="lblIDesc" runat="server" Text='<%# Bind("DO_Det_Itm_Desc") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="DO_Det_Itm_Uom" HeaderText="Unit">
                            <ItemStyle BackColor="#99FFCC" HorizontalAlign="Right" Width="20px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdQty" runat="server" Text='<%# Bind("DO_Det_Lin_Qty") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#00CC66" Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Free Bag">
                            <ItemTemplate>
                                <asp:Label ID="lblAppFreeQty" runat="server" Text='<%# Bind("DO_Det_Free_Qty", "{0:F2}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tot. Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblDoQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("DO_Det_Lin_Qty")) + Convert.ToDecimal(Eval("DO_Det_Free_Qty")))).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEditDo" runat="server" OnClick="btnEditDo_Click" Text="Edit..." />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="20px" />
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvPendDoDetRtl" EventName="SelectedIndexChanged" />
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
