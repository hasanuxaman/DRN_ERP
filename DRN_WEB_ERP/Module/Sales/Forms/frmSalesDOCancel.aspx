<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmSalesDOCancel.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesDOCancel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Delivery Order Cancel</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #86AEAE">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 118px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 89px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 138px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 124px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 222px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td style="width: 135px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 118px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 89px">
                            Date From
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left" style="width: 138px">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="inline" Width="100px" Enabled="False"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtDtFromCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                            <asp:Image ID="imgFromDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                        </td>
                        <td align="right" style="width: 124px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 222px">
                            Date To
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td style="width: 135px" align="left">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="inline" Width="100px" Enabled="False"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgToDate" TargetControlID="txtToDate">
                            </cc1:CalendarExtender>
                            <asp:Image ID="imgToDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png"
                                Width="16px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 118px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 89px">
                            &nbsp;Customer
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="search textAlignCenter" Width="480px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetSrchCustomer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearch">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 135px">
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" onkeypress="return clickButton(event,'btnSearch')"
                                Text="Search" />
                            <asp:Button ID="btnClearSrch" runat="server" OnClick="btnClearSrch_Click" Text="Clear"
                                Visible="False" Width="60px" />
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 118px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 89px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 138px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 124px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 222px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td style="width: 135px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <asp:GridView ID="gvDoDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="7.5pt" ForeColor="#333333">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Par_Adr_Name" HeaderText="Customer Name">
                            <ItemStyle Width="170px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="D/O Ref. No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfAppDoHdrRef" runat="server" Value='<%# Bind("DO_Hdr_Ref") %>' />
                                <asp:Label ID="lblAppDoHdrRefNo" runat="server" Text='<%# Bind("DO_Hdr_Ref_No") %>'></asp:Label>
                                <asp:HiddenField ID="hfAppDoDetLno" runat="server" Value='<%# Bind("DO_Det_Lno") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="105px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="DO_Hdr_Date" DataFormatString="{0:d}" HeaderText="D/O Date">
                            <ItemStyle Width="40px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SO_Hdr_Ref_No" HeaderText="Ord. Ref. No">
                            <ItemStyle Width="115px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SO_Hdr_Date" DataFormatString="{0:d}" HeaderText="Ord. Date">
                            <ItemStyle Width="40px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Item Code" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblAppDoICode" runat="server" Text='<%# Bind("DO_Det_Icode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Desc.">
                            <ItemTemplate>
                                <asp:Label ID="lblAppDoItemDesc" runat="server" Text='<%# Bind("Itm_Det_Desc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblAppDoUom" runat="server" Text='<%# Bind("SO_Det_Itm_Uom") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="D/O Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblAppDoQty" runat="server" Text='<%# Bind("DO_Det_Lin_Qty") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#B9FFDC" HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="D/O Free Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblAppDoFreeQty" runat="server" Text='<%# Bind("DO_Det_Free_Qty") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#B9FFDC" HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tot. D/O Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblTotAppDoQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("DO_Det_Lin_Qty")) + Convert.ToDecimal(Eval("DO_Det_Free_Qty")))).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#99CC00" Font-Bold="True" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tot. Del. Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblTotDoDelQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("DO_Det_Org_QTY")) + Convert.ToDecimal(Eval("DO_Det_Ext_Data1")))).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#FFFF99" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="D/O Balance">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfDoBalQty" runat="server" Value='<%# Bind("DO_Det_Bal_Qty", "{0:F2}") %>' />
                                <asp:HiddenField ID="hfDoFreeBalQty" runat="server" Value='<%# Convert.ToDecimal(Eval("DO_Det_Ext_Data2")).ToString("N2") %>' />
                                <asp:Label ID="lblAppDoBalQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("DO_Det_Bal_Qty")) + Convert.ToDecimal(Eval("DO_Det_Ext_Data2")))).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#33CCCC" Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnCancelDo" runat="server" OnClick="btnCancelDo_Click" OnClientClick="return confirm('Do you want to Cancel?')"
                                    Text="Cancel" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Height="30px"
                        Wrap="True" />
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
