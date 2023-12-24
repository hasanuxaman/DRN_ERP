<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmSalesReturnRpt.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesReturnRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Sales Retun Report
    </div>
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
                                        <asp:ListItem Value="1" Selected="True">All Return</asp:ListItem>
                                        <asp:ListItem Value="2">Pending</asp:ListItem>
                                        <asp:ListItem Value="3">Approved</asp:ListItem>
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
                    Visible="False" />
                <br />
                <asp:GridView ID="gvSalesRtn" runat="server" AutoGenerateColumns="False" CellPadding="4"
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
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBtnStatus" runat="server" Text='<%# GetRtnStat(Eval("Trn_Det_Ref").ToString()) %>'
                                    OnClick="lnkBtnStatus_Click"></asp:LinkButton>
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
