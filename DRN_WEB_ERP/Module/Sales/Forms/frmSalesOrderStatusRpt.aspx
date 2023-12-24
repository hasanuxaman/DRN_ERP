<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmSalesOrderStatusRpt.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesOrderStatusRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Sales Order Status Report</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <asp:Panel ID="Panel3" runat="server">
                    <div align="center">
                        <table style="width: 100%; font-size: 12px; background-color: #CCFFFF;">
                            <tr>
                                <td style="width: 21px">
                                    &nbsp;
                                </td>
                                <td align="right" style="width: 138px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 168px">
                                    &nbsp;
                                </td>
                                <td colspan="6" align="center" style="font-weight: bold">
                                    &nbsp;
                                </td>
                                <td style="width: 15px">
                                    &nbsp;
                                </td>
                                <td style="width: 99px">
                                    &nbsp;
                                </td>
                                <td style="width: 98px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                    &nbsp;
                                </td>
                                <td align="center" colspan="8">
                                    <asp:RadioButtonList ID="optRpt" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Selected="True">All Order</asp:ListItem>
                                        <asp:ListItem Value="2">Approved</asp:ListItem>
                                        <asp:ListItem Value="3">Pending</asp:ListItem>
                                        <asp:ListItem Value="4">Rejected</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                </td>
                                <td style="width: 15px">
                                    &nbsp;
                                </td>
                                <td style="width: 99px">
                                    &nbsp;
                                </td>
                                <td style="width: 98px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                    &nbsp;
                                </td>
                                <td align="right" style="width: 138px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 168px">
                                    &nbsp;
                                </td>
                                <td align="right" style="width: 97px">
                                    &nbsp;
                                </td>
                                <td style="width: 3px">
                                    &nbsp;
                                </td>
                                <td style="width: 146px">
                                    &nbsp;
                                </td>
                                <td style="width: 127px">
                                    &nbsp;
                                </td>
                                <td style="width: 6px">
                                    &nbsp;
                                </td>
                                <td style="width: 136px">
                                    &nbsp;
                                </td>
                                <td style="width: 15px">
                                    &nbsp;
                                </td>
                                <td style="width: 99px">
                                    &nbsp;
                                </td>
                                <td style="width: 98px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                    &nbsp;
                                </td>
                                <td align="right" style="width: 138px">
                                    Date From
                                </td>
                                <td>
                                    :
                                </td>
                                <td style="width: 168px">
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDtFromCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                                    </cc1:CalendarExtender>
                                    <asp:Image ID="imgFromDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                                </td>
                                <td align="right" style="width: 97px">
                                    Date To
                                </td>
                                <td style="width: 3px">
                                    :
                                </td>
                                <td style="width: 146px">
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgToDate" TargetControlID="txtToDate">
                                    </cc1:CalendarExtender>
                                    <asp:Image ID="imgToDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png"
                                        Width="16px" />
                                </td>
                                <td align="right" style="width: 127px">
                                    Filter By Customer
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 136px">
                                    <asp:TextBox ID="txtSearch" runat="server" Width="350px" CssClass="search textAlignCenter"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                                        CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                        DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCustomer"
                                        ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                        TargetControlID="txtSearch">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td style="width: 15px">
                                    &nbsp;
                                </td>
                                <td style="width: 99px">
                                    <asp:Button ID="btnShowRpt" runat="server" OnClick="btnShowRpt_Click" Text="Show"
                                        Width="100px" />
                                </td>
                                <td style="width: 98px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                    &nbsp;
                                </td>
                                <td style="width: 138px">
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 168px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFromDate"
                                        Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFromDate"
                                        Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                        Type="Date" ValidationGroup="btnShow"></asp:CompareValidator>
                                </td>
                                <td style="width: 97px">
                                    &nbsp;
                                </td>
                                <td style="width: 3px">
                                    &nbsp;
                                </td>
                                <td style="width: 146px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                                        Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtToDate"
                                        Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                        Type="Date" ValidationGroup="btnShow"></asp:CompareValidator>
                                </td>
                                <td style="width: 127px">
                                    &nbsp;
                                </td>
                                <td style="width: 6px">
                                    &nbsp;
                                </td>
                                <td style="width: 136px">
                                </td>
                                <td style="width: 15px">
                                    &nbsp;
                                </td>
                                <td style="width: 99px">
                                    &nbsp;
                                </td>
                                <td style="width: 98px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                    &nbsp;
                                </td>
                                <td style="width: 138px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 168px">
                                    &nbsp;
                                </td>
                                <td style="width: 97px">
                                    &nbsp;
                                </td>
                                <td style="width: 3px">
                                    &nbsp;
                                </td>
                                <td style="width: 146px">
                                    &nbsp;
                                </td>
                                <td style="width: 127px">
                                    &nbsp;
                                </td>
                                <td style="width: 6px">
                                    &nbsp;
                                </td>
                                <td style="width: 136px">
                                    &nbsp;
                                </td>
                                <td style="width: 15px">
                                    &nbsp;
                                </td>
                                <td style="width: 99px">
                                    &nbsp;
                                </td>
                                <td style="width: 98px">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </div>
            <div align="center" style="background-color: #CCFFFF">
                <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export To Excel"
                    Enabled="False" />
                <br />
                <asp:GridView ID="gvApprDoDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Par_Adr_Name" HeaderText="Customer Name" />
                        <asp:TemplateField HeaderText="S/O Ref. No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfSoHdrRef" runat="server" Value='<%# Bind("SO_Hdr_Ref") %>' />
                                <asp:HiddenField ID="hfSoDetLno" runat="server" Value='<%# Bind("SO_Det_Lno") %>' />
                                <asp:Label ID="lblSoHdrRefNo" runat="server" Text='<%# Bind("SO_Hdr_Ref_No") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SO_Hdr_Com9" DataFormatString="{0:d}" HeaderText="App. Date" />
                        <asp:BoundField DataField="SO_Det_Itm_Desc" HeaderText="Item Desc." />
                        <asp:BoundField DataField="SO_Det_Itm_Uom" HeaderText="Unit" />
                        <asp:TemplateField HeaderText="Order Qty" ItemStyle-BackColor="#00CC99" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblOrgOrdQty" runat="server" Font-Bold="True" Text='<%# String.Format("{0:n}", (Convert.ToDecimal(Eval("SO_Det_T_C1")))) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#00CC99" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="SO_Det_Lin_Qty" DataFormatString="{0:n}" HeaderText="App. Qty">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdRate" runat="server" Text='<%# Bind("SO_Det_Lin_Rat", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdAmt" runat="server" CssClass="textAlignRight transparent" Font-Size="8"
                                    Text='<%# String.Format( "{0:n}", (Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Lin_Rat")))) %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#CC99FF" HorizontalAlign="Center" Width="70px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Free Bag" Visible="False">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfOrgFreeQty" runat="server" Value='<%# String.Format("{0:n}", (Convert.ToDecimal(Eval("SO_Det_Free_Qty")))) %>' />
                                <asp:Label ID="lblOrdFreeQty" runat="server" CssClass="textAlignCenter transparent"
                                    Font-Size="8" Text='<%# Bind("SO_Det_Free_Qty", "{0:n}") %>' Width="50px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trans. Rate">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdTransRat" runat="server" Text='<%# Bind("SO_Det_Trans_Rat", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Net Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdNetAmt" runat="server" CssClass="textAlignRight transparent"
                                    Font-Bold="True" Font-Size="8" Text='<%# String.Format( "{0:n}", ((Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Lin_Rat"))) + (Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Trans_Rat"))))) %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#66FF66" HorizontalAlign="Center" Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBtnStatus" runat="server" Text='<%# GetOrdCom(Eval("SO_Hdr_Ref").ToString()) %>'
                                    OnClick="lnkBtnStatus_Click"></asp:LinkButton>
                                <%--<asp:Label ID="lblStatus" runat="server" Text='<%# GetOrdCom(Eval("SO_Hdr_Ref").ToString()) %>'></asp:Label>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="App/Agrd By">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetOrdAppBy(Eval("SO_Hdr_Ref_No").ToString()) %>'
                                    OnClick="lnkBtnStatus_Click"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comment Log">
                            <ItemTemplate>
                                <%# GetOrdComLog(Eval("SO_Hdr_Ref_No").ToString()).ToString().Replace(Environment.NewLine, "<br/>")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="8pt" ForeColor="White"
                        Wrap="false" />
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
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
