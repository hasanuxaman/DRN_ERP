<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmSalesTCPend.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesTCPend" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        
    </script>
    <div align="center" style="background-color: #00FF99">
        Pending TC List
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <br />
                <asp:GridView ID="gvPendDoDet" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#E7E7FF" BorderStyle="None" CellPadding="3" Font-Size="8pt" PageSize="25">
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
                        <asp:BoundField HeaderText="D/O Date" DataField="DO_Hdr_Date" />
                        <asp:BoundField DataField="Par_Adr_Name" HeaderText="Dealer Name" />
                        <asp:BoundField DataField="Itm_Det_Desc" HeaderText="Item Desc." />
                        <asp:BoundField DataField="DO_Det_Itm_Uom" HeaderText="Unit" />
                        <asp:TemplateField HeaderText="D/O Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblDoQty" runat="server" Text='<%# Bind("DO_Det_Lin_Qty", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Free Bag">
                            <ItemTemplate>
                                <asp:Label ID="lblDoFreeQty" runat="server" Text='<%# Bind("DO_Det_Free_Qty", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total D/O Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblTotDoQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("DO_Det_Lin_Qty")) + Convert.ToDecimal(Eval("DO_Det_Free_Qty")))).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="90px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Balance Qty">
                            <ItemTemplate>
                                <%--<asp:Label ID="lblDoBalQty" runat="server" Text='<%# Bind("DO_Det_Del_Bal_Qty") %>'></asp:Label>--%>
                                <asp:Label ID="lblDoBalQty" runat="server" Text='<%# Bind("DO_Det_Unt_Wgt") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="90px" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="DSM Name">
                            <ItemTemplate>
                                <asp:Label ID="lblDsmName" runat="server" Text='<%# GetDsmName(Eval("Par_Adr_Ref_No").ToString())%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField HeaderText="SP Name">
                            <ItemTemplate>
                                <asp:Label ID="lblSpName" runat="server" Text='<%# GetSpName(Eval("SO_Hdr_Com4").ToString())%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnCreateTC" runat="server" OnClick="btnCreateTC_Click" OnClientClick="return confirm('Do you want to Proceed?')"
                                    Text="Create TC" />
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
