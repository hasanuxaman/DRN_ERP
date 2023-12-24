<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmSalesDOEdit.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.frmSalesDOEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Delivery Order Retailer Entry</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnSearch">
                <div align="center" style="background-color: #86AEAE">
                    <span>
                        <br />
                        Search Order:</span> <span>
                            <asp:TextBox ID="txtSearch" runat="server" Width="550px" 
                        CssClass="search textAlignCenter" Enabled="False"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrchHdr"
                                CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchDoList"
                                ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                TargetControlID="txtSearch">
                            </cc1:AutoCompleteExtender>
                            <asp:Button ID="btnSearch" runat="server" onkeypress="return clickButton(event,'btnSearch')"
                                Text="Search" ValidationGroup="btnSearch" 
                        OnClick="btnSearch_Click" Visible="False" />
                            <asp:Button ID="btnClearSrch" runat="server" Text="Clear" Visible="False" Width="60px"
                                OnClick="btnClearSrch_Click" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator110" runat="server" ControlToValidate="txtSearch"
                                ErrorMessage="Enter Search Text" Font-Bold="False" ForeColor="Red" ValidationGroup="btnSearch"
                                Font-Size="10pt"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                        </span>
                </div>
            </asp:Panel>
            <div align="center" style="background-color: #9999FF">
                <br />
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; font-size: 12px;">
                    <tr>
                        <td align="left" style="font-size: 12px; width: 10px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 69px;">
                            D/O Ref No
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style1" style="width: 182px">
                            <asp:TextBox ID="txtDoRefNo" runat="server" Enabled="False" Font-Bold="True" ForeColor="#FF9900"
                                Width="180px" CssClass="textAlignCenter"></asp:TextBox>
                        </td>
                        <td align="left" class="style2" style="font-size: 12px; width: 72px;">
                            Date &amp; Time
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style3" style="width: 136px">
                            <asp:TextBox ID="txtDoDate" runat="server" AutoPostBack="True" Enabled="False" CssClass="textAlignCenter"></asp:TextBox>
                        </td>
                        <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                            Trans. Mode
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style5" style="font-size: 14px; width: 135px;">
                            <asp:DropDownList ID="ddlOrdTransMode" runat="server" Width="160px" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="style6" style="font-size: 12px; width: 75px;">
                            Del. Address
                        </td>
                        <td align="left" style="font-size: 12px; width: 8px;">
                            :
                        </td>
                        <td align="left" width="8" rowspan="4" valign="top">
                            <asp:TextBox ID="txtDelAddr" runat="server" ForeColor="#0066FF" Width="180px" TextMode="MultiLine"
                                Height="60px" MaxLength="200" ReadOnly="True"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtDelAddr"
                                ErrorMessage="Enter Delivery Address" Font-Size="8pt" ForeColor="Red" ValidationGroup="btnCreate"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 12px; width: 10px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 69px;">
                            Trans. By
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style1" style="width: 182px">
                            <asp:RadioButtonList ID="optTranBy" runat="server" Font-Size="8pt" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="optTranBy_SelectedIndexChanged" Enabled="False">
                                <asp:ListItem Value="1" Selected="True">Customer</asp:ListItem>
                                <asp:ListItem Value="2">Company</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left" class="style2" style="font-size: 12px; width: 72px;">
                            District
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style3" style="width: 136px">
                            <asp:DropDownList ID="cboCustDist" runat="server" ValidationGroup="ChkData" Width="172px"
                                AutoPostBack="True" OnSelectedIndexChanged="cboCustDist_SelectedIndexChanged"
                                Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                            Thana
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style5" style="font-size: 14px; width: 135px;">
                            <asp:DropDownList ID="cboCustThana" runat="server" Width="160px" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="style6" style="font-size: 12px; width: 75px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 8px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 12px; width: 10px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 69px;">
                            Vehicle No
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style1" style="width: 182px">
                            <asp:TextBox ID="txtTruckNo" runat="server" Width="180px" CssClass="uppercase" Enabled="False"></asp:TextBox>
                            <asp:DropDownList ID="cboTruckNo" runat="server" Width="182px" Visible="False" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="style2" style="font-size: 12px; width: 72px;">
                            Driver Name
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style3" style="width: 136px">
                            <asp:TextBox ID="txtDriverName" runat="server" CssClass="capitalize" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                            Driver Cell
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style5" style="font-size: 14px; width: 135px;">
                            <asp:TextBox ID="txtDriverContact" runat="server" Width="156px" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="left" class="style6" style="font-size: 12px; width: 75px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 8px;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%; font-size: 12px;">
                    <tr>
                        <td align="left" style="font-size: 12px; width: 11px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 71px;">
                            Location
                        </td>
                        <td align="left" width="8">
                            :&nbsp;
                        </td>
                        <td align="left" class="style1">
                            <asp:TextBox ID="txtSalesLoc" runat="server" AutoPostBack="True" Width="686px" OnTextChanged="txtSalesLoc_TextChanged"
                                CssClass="search" Enabled="False"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="txtSalesLoc_AutoCompleteExtender" runat="server" BehaviorID="AutoCompleteSrchLoc"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetSrchSalesLoc" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSalesLoc">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td align="left" class="style6" style="font-size: 12px; width: 75px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 8px;">
                            &nbsp;
                        </td>
                        <td align="left" width="8" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 12px; width: 11px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 69px;">
                            &nbsp;
                        </td>
                        <td align="left" width="8">
                            &nbsp;
                        </td>
                        <td align="center" class="style1">
                            <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtSalesLoc"
                                ErrorMessage="Enter Valid Location" ForeColor="Red" OnServerValidate="CustomValidator3_ServerValidate"
                                ValidationGroup="btnCreate"></asp:CustomValidator>
                            <asp:Label ID="lblTransRate" runat="server" ForeColor="#FFCC99" Text="Transport Rate Changed"
                                Visible="False"></asp:Label>
                        </td>
                        <td align="left" class="style6" style="font-size: 12px; width: 75px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 8px;">
                            &nbsp;
                        </td>
                        <td align="left" width="8" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <hr />
                <br />
                <div align="center">
                    <asp:Label ID="lblDoDet" runat="server" Text="D/O Details" Font-Size="10pt" Font-Bold="True"></asp:Label>
                </div>
                <asp:GridView ID="gvDoDet" runat="server" AutoGenerateColumns="False" CellPadding="3"
                    Font-Size="8pt" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" PageSize="25">
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
                        <asp:BoundField DataField="DO_Det_Itm_Uom" HeaderText="Unit"></asp:BoundField>
                        <asp:BoundField DataField="DO_Hdr_Date" DataFormatString="{0:d}" HeaderText="D/O Date">
                        </asp:BoundField>
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
                        <asp:TemplateField HeaderText="Tot. D/O Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblTotDoQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("DO_Det_Lin_Qty")) + Convert.ToDecimal(Eval("DO_Det_Free_Qty")))).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#33CCCC" Font-Bold="True" HorizontalAlign="Center" Width="100px" />
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
                    </Columns>
                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="false"
                        Font-Size="8" />
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
                <hr />
                <table style="width: 100%; font-size: 12px;">
                    <tr>
                        <td style="width: 37px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 59px">
                            Retailer 1
                        </td>
                        <td align="left">
                            :
                        </td>
                        <td align="left" style="width: 714px">
                            <asp:TextBox ID="txtRetailer1" runat="server" CssClass="inline search" Width="686px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtendertxtRetailer1" runat="server" BehaviorID="AutoCompleteSrchRTL1"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetSrchRetailer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtRetailer1">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 46px">
                            Quantity
                        </td>
                        <td align="left">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDoQty1" runat="server" CssClass="textAlignCenter" Width="100px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtDoQty1_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDoQty1"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 37px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 59px">
                            Retailer 2
                        </td>
                        <td align="left">
                            :
                        </td>
                        <td align="left" style="width: 714px">
                            <asp:TextBox ID="txtRetailer2" runat="server" CssClass="inline search" Width="686px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtendertxtRetailer2" runat="server" BehaviorID="AutoCompleteSrchRTL2"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetSrchRetailer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtRetailer2">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 46px">
                            Quantity
                        </td>
                        <td align="left">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDoQty2" runat="server" CssClass="textAlignCenter" Width="100px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtDoQty2_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDoQty2"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 37px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 59px">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 714px">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 46px">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left">
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
                <asp:HiddenField ID="hfOrdTransRate" runat="server" Value="0" />
                <br />
                <asp:Button ID="btnEditDo" runat="server" Text="Update" Width="90px" OnClick="btnEditDo_Click" />
                <br />
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClearSrch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnEditDo" EventName="Click" />
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
