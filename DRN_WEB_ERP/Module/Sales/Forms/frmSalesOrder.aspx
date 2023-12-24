<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmSalesOrder.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CalcOrdAmount(OrdQty, OrdRate, OrdAmt, TranRate, GrsAmt, DisAmt, NetAmt, BnsPerc, FreeBag) {
            var OrderQty = 0;
            if (document.getElementById(OrdQty).value != '') {
                OrderQty = parseFloat(document.getElementById(OrdQty).value);
            }
            //alert(OrderQty.toString());

            var ItemRate = 0;
            if (document.getElementById(OrdRate).value != '') {
                ItemRate = parseFloat(document.getElementById(OrdRate).value);
            }
            //alert(ItemRate.toString());            

            var TransRate = 0;
            if (document.getElementById(TranRate).value != '') {
                TransRate = parseFloat(document.getElementById(TranRate).value);
            }
            //alert(TransRate.toString());

            var OrdDiscount = 0;
            if (document.getElementById(DisAmt).value != '') {
                OrdDiscount = parseFloat(document.getElementById(DisAmt).value);
            }
            //alert(OrdDiscount.toString());

            var OrderAmount = document.getElementById(OrdAmt);
            var OrderValue = parseFloat(OrderQty * ItemRate);
            OrderAmount.value = OrderValue.toFixed(2);
            //alert(OrderValue.toString());

            var GrsAmount = document.getElementById(GrsAmt);
            var GrsValue = parseFloat((OrderQty * ItemRate) + (OrderQty * TransRate));
            GrsAmount.value = GrsValue.toFixed(2);
            //alert(GrsValue.toString());

            var NetAmount = document.getElementById(NetAmt);
            var NetValue = parseFloat(((OrderQty * ItemRate) + (OrderQty * TransRate)) - OrdDiscount);
            NetAmount.value = NetValue.toFixed(2);
            //alert(NetValue.toString());

            var BonusPerc = 0;
            if (document.getElementById(BnsPerc).value != '') {
                BonusPerc = parseFloat(document.getElementById(BnsPerc).value);
            }
            //alert(BonusPerc.toString());

            var FreeQty = document.getElementById(FreeBag);
            var FreeBagQty = parseFloat((OrderQty * BonusPerc) / 100);
            FreeQty.value = FreeBagQty.toFixed(2);
            //alert(FreeBagQty.toString());
        }        
    </script>
    <style type="text/css">
        .style3
        {
            margin-top: 6px;
        }
    </style>
    <div align="center" style="background-color: #00FF99">
        Sales Order Entry</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <asp:Panel ID="pnlOrdHdr" runat="server" DefaultButton="btnProceed">
                    <div align="center" style="background-color: #CC66FF">
                        <br />
                        Year:<asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
                            Width="100px">
                        </asp:DropDownList>
                        &nbsp;Month:<asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"
                            Width="100px">
                        </asp:DropDownList>
                        &nbsp;Select Order:
                        <asp:DropDownList ID="ddlOrderList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderList_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;<asp:Button ID="btnViewOrder" runat="server" OnClick="btnViewOrder_Click" Text="View Order" />
                        <br />
                        <br />
                    </div>
                    <div align="center" style="background-color: #6600CC; height: 30px;">
                        <table style="width: 100%;">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label3" runat="server" ForeColor="White" Text="Order Ref: " CssClass="style3"></asp:Label>
                                    <asp:Label ID="lblOrdRefNo" runat="server" ForeColor="White" Text="Label"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label ID="Label1" runat="server" BorderStyle="None" ForeColor="White" Text="Order Quantity: "></asp:Label>
                                    <asp:Label ID="lblTotOrdQty" runat="server" BorderStyle="None" ForeColor="White"
                                        Text="0.00"></asp:Label>
                                </td>
                                <td align="right">
                                    &nbsp;
                                    <asp:Label ID="Label2" runat="server" BorderStyle="None" ForeColor="White" Text="Order Value: "></asp:Label>
                                    <asp:Label ID="lblTotOrdVal" runat="server" BorderStyle="None" ForeColor="White"
                                        Text="0.00"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="background-color: #9E9AF5">
                        <br />
                        <table style="width: 100%; font-family: verdana; font-size: small;">
                            <tr>
                                <td style="width: 16px">
                                    &nbsp;
                                </td>
                                <td style="width: 145px">
                                    Date
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 81px">
                                    <asp:TextBox ID="txtOrdDate" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        ForeColor="Blue" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td style="width: 23px">
                                    &nbsp;
                                </td>
                                <td style="width: 179px">
                                    Exp. Del. Date
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 163px">
                                    <asp:TextBox ID="txtOrdDelDate" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        ForeColor="Blue" ReadOnly="True" Width="120px"></asp:TextBox>
                                    <asp:Image ID="imgReqDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <cc1:CalendarExtender ID="CalenderDelDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgReqDt" TargetControlID="txtOrdDelDate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="width: 22px">
                                    &nbsp;
                                </td>
                                <td style="width: 149px">
                                    CREDIT LIMIT
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 66px">
                                    <asp:TextBox ID="txtOrdCrLimit" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        Enabled="False" Font-Bold="True" Font-Size="10pt" ForeColor="Blue" ReadOnly="True"
                                        Width="120px"></asp:TextBox>
                                </td>
                                <td align="center" rowspan="3" valign="middle">
                                    &nbsp;
                                </td>
                                <td align="center" rowspan="3" valign="middle">
                                    <asp:Button ID="btnProceed" runat="server" CssClass="inline" Height="65px" OnClick="btnProceed_Click"
                                        Text="Proceed Order" ValidationGroup="btnProcd" Width="100px" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="inline" Height="65px" OnClick="btnClear_Click"
                                        Text="Clear" Visible="False" Width="100px" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 16px">
                                    &nbsp;
                                </td>
                                <td style="width: 145px">
                                    Prefix
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 81px">
                                    <asp:DropDownList ID="ddlSalePrefix" runat="server" Width="170px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 23px">
                                    &nbsp;
                                </td>
                                <td style="width: 179px">
                                    Valid Before
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 163px">
                                    <asp:TextBox ID="txtOrdValidDate" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        ForeColor="Blue" ReadOnly="True" Width="120px"></asp:TextBox>
                                    <asp:Image ID="imgValDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <cc1:CalendarExtender ID="CalenderValDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtOrdValidDate" PopupButtonID="imgValDt">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="width: 22px">
                                    &nbsp;
                                </td>
                                <td style="width: 149px">
                                    OUTSTANDING
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 66px">
                                    <asp:TextBox ID="txtOrdCrOutsatnd" runat="server" CssClass="textAlignCenter" Enabled="False"
                                        Font-Bold="True" Font-Size="10pt" ForeColor="Blue" ReadOnly="True" BorderStyle="None"
                                        Width="120px"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 16px">
                                    &nbsp;
                                </td>
                                <td style="width: 145px">
                                    Transport Terms
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 81px">
                                    <asp:DropDownList ID="ddlOrdTransCostBy" runat="server" Width="170px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlOrdTransCostBy_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Customer Transport</asp:ListItem>
                                        <asp:ListItem Value="2">Company Transport</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 23px">
                                    &nbsp;
                                </td>
                                <td style="width: 179px">
                                    Transport Mode
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 163px">
                                    <asp:DropDownList ID="ddlOrdTransMode" runat="server" Width="120px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 22px">
                                    &nbsp;
                                </td>
                                <td style="width: 149px">
                                    AVAILABLE LIMIT
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 66px">
                                    <asp:TextBox ID="txtOrdCrLimitBal" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        Enabled="False" Font-Bold="True" Font-Size="10pt" ForeColor="Blue" ReadOnly="True"
                                        Width="120px"></asp:TextBox>
                                    <br />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <div>
                            <table style="width: 100%; font-family: verdana; font-size: small;">
                                <tr>
                                    <td style="width: 14px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 120px">
                                        Customer
                                    </td>
                                    <td width="2px">
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCust" runat="server" AutoPostBack="True" CssClass="inline search"
                                            OnTextChanged="txtCust_TextChanged" Width="792px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                                            CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                            ServiceMethod="GetSrchCustomer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtCust">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td width="2px" colspan="2" align="center">
                                        <asp:LinkButton ID="lnkBtnCrRpt" runat="server" ForeColor="#FFFF99" 
                                            Width="90px" onclick="lnkBtnCrRpt_Click" Visible="False">Aging Report</asp:LinkButton>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 14px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 120px">
                                        Location
                                    </td>
                                    <td width="2px">
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSalesLoc" runat="server" AutoPostBack="True" CssClass="inline search"
                                            Width="792px" OnTextChanged="txtSalesLoc_TextChanged" BackColor="#CCCCCC" Enabled="False"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="txtSalesLoc_AutoCompleteExtender" runat="server" BehaviorID="AutoCompleteSrchLoc"
                                            CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                            ServiceMethod="GetSrchSalesLoc" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSalesLoc">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td width="2px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 66px">
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 14px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 120px">
                                        Sales Person
                                    </td>
                                    <td width="2px">
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSalesPer" runat="server" CssClass="inline search" Width="792px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="txtSalesPer_AutoCompleteExtender" runat="server" BehaviorID="AutoCompleteSrchSalesPer"
                                            CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                            ServiceMethod="GetSrchSalesPer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSalesPer">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td width="2px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 66px">
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
                                    <td style="width: 14px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 120px" valign="top">
                                        Remarks
                                    </td>
                                    <td width="2px" valign="top">
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRemarks" runat="server" Height="50px" MaxLength="500" TextMode="MultiLine"
                                            Width="806px"></asp:TextBox>
                                    </td>
                                    <td width="2px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 66px">
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
                                    <td style="width: 14px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 120px">
                                        &nbsp;
                                    </td>
                                    <td width="2px">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCust0" runat="server" ControlToValidate="txtCust"
                                            ErrorMessage="Enter Customer First" ForeColor="Red" ValidationGroup="btnProcd"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Enter Valid Customer"
                                            ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="btnProcd"></asp:CustomValidator>
                                        <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtSalesLoc"
                                            ErrorMessage="Enter Valid Location" ForeColor="Red" OnServerValidate="CustomValidator3_ServerValidate"
                                            ValidationGroup="btnProcd"></asp:CustomValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCust1" runat="server" ControlToValidate="txtSalesPer"
                                            ErrorMessage="Enter Sales Person First" ForeColor="Red" ValidationGroup="btnProcd"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="txtSalesPer"
                                            ErrorMessage="Enter Valid Sales Person" ForeColor="Red" OnServerValidate="CustomValidator4_ServerValidate"
                                            ValidationGroup="btnProcd"></asp:CustomValidator>
                                    </td>
                                    <td width="2px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 66px">
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
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlOrdDet" runat="server" Visible="false" DefaultButton="btnAddOrdDet">
                    <div align="center" style="background-color: #9E9AF5">
                        <table cellspacing="0" rules="all" border="1" style="border-collapse: collapse;">
                            <tr style="background-color: #009933; font-weight: bold; color: #F7F7F7; font-size: small;
                                white-space: nowrap; height: 30px;">
                                <th align="center" scope="col" width="70" style="width: 210px;">
                                    Item
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemName" runat="server" ControlToValidate="ddlOrdItem"
                                        ErrorMessage="Select Order Item" ForeColor="Red" InitialValue="0" Text="*" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                                <th align="center" scope="col" width="80">
                                    Unit
                                </th>
                                <th align="center" scope="col" width="100">
                                    Quantity
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemQty" runat="server" ControlToValidate="txtOrdQty"
                                        ErrorMessage="Enter Order Quantity" ForeColor="Red" Text="*" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                                <th align="center" scope="col" width="50">
                                    Rate
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemRate" runat="server" ErrorMessage="Enter Item Rate"
                                        ForeColor="Red" Text="*" ControlToValidate="txtOrdRate" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                                <th align="center" scope="col" width="50">
                                    Amount
                                </th>
                                <th align="center" scope="col" width="60">
                                    Free Bag
                                </th>
                                <th align="center" scope="col" width="60">
                                    Trans. Rate
                                </th>
                                <th align="center" scope="col" width="60">
                                    Gross Amount
                                </th>
                                <th align="center" scope="col" width="60">
                                    Discount
                                </th>
                                <th align="center" scope="col" width="60">
                                    Net Amount
                                </th>
                                <%--<th align="center" scope="col" width="80">
                                    &nbsp;
                                </th>--%>
                            </tr>
                            <tr class="gridFooterRow" style="background-color: #86AEAE;">
                                <td>
                                    <asp:DropDownList ID="ddlOrdItem" runat="server" AutoPostBack="True" Width="230px"
                                        OnSelectedIndexChanged="ddlOrdItem_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlOrdItemUom" runat="server" Enabled="False" Width="80px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOrdQty" runat="server" CssClass="textAlignCenter" Width="100px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtOrdQty_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtOrdQty"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOrdRate" runat="server" CssClass="textAlignCenter" Width="80px"
                                        Enabled="False"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtOrdRate_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtOrdRate"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOrdAmt" runat="server" CssClass="textAlignCenter" Enabled="False"
                                        Font-Bold="True" Font-Size="10pt" ForeColor="Blue" Width="100px" BackColor="#6699FF"
                                        BorderStyle="Dashed" BorderWidth="1px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOrdFreeBag" runat="server" CssClass="textAlignCenter" Width="80px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                        FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtOrdFreeBag"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOrdTransRate" runat="server" CssClass="textAlignCenter" Width="80px"
                                        Enabled="False"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtOrdTransRate_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtOrdTransRate"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOrdGrsAmt" runat="server" CssClass="textAlignCenter" Width="100px"
                                        Enabled="False" Font-Bold="True" Font-Size="10pt" ForeColor="Blue" BackColor="#6699FF"
                                        BorderStyle="Dashed" BorderWidth="1px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOrdDiscount" runat="server" CssClass="textAlignCenter" Width="80px"
                                        Enabled="False"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtOrdDiscount_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtOrdDiscount"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOrdNetAmt" runat="server" BackColor="#6699FF" BorderStyle="Dashed"
                                        BorderWidth="1px" CssClass="textAlignCenter" Enabled="False" Font-Bold="True"
                                        Font-Size="10pt" ForeColor="Blue" Width="100px"></asp:TextBox>
                                </td>
                                <%--<td align="center">
                                    <asp:Button ID="btnAddOrdDet" runat="server" Text="Add" ValidationGroup="btnAdd"
                                        Width="75px" OnClick="btnAddOrdDet_Click" />
                                </td>--%>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnAddOrdDet" runat="server" Text="Add" ValidationGroup="btnAdd"
                            Width="75px" OnClick="btnAddOrdDet_Click" />
                        <br />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" Font-Size="10pt" ForeColor="Red"
                            ValidationGroup="btnAdd" DisplayMode="List" />
                    </div>
                    <div align="center" style="background-color: #9E9AF5">
                        <br />
                        <asp:GridView ID="gvOrdDet" runat="server" CellPadding="4" Font-Size="10pt" ForeColor="#333333"
                            AutoGenerateColumns="False" OnRowDeleting="gvOrdDet_RowDeleting">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrdICode" runat="server" Text='<%# Bind("ORD_ITEM_REF") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Item Desc." DataField="ORD_ITEM_NAME" />
                                <asp:BoundField DataField="ORD_ITEM_UOM" HeaderText="Unit" />
                                <asp:TemplateField HeaderText="Quantity">
                                    <%--<EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ORD_QTY") %>'></asp:TextBox>
                                    </EditItemTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrdQty" runat="server" Text='<%# Bind("ORD_QTY") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#00CC66" Font-Bold="True" HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Rate" DataField="ORD_RATE" DataFormatString="{0:c}">
                                    <ItemStyle HorizontalAlign="Right" Width="50px" BackColor="#FFFF99" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ORD_AMOUNT" HeaderText="Amount" DataFormatString="{0:c}">
                                    <ItemStyle HorizontalAlign="Right" BackColor="#0099FF" Font-Bold="True" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Free Bag" DataField="ORD_FREE_BAG" DataFormatString="{0:c}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Transport Rate" DataField="ORD_TRANS_RATE" DataFormatString="{0:c}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ORD_GRS_AMOUNT" HeaderText="Gross Amount">
                                    <ItemStyle HorizontalAlign="Right" BackColor="#CC9900" Font-Bold="True" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ORD_DISCOUNT" HeaderText="Discount">
                                    <ItemStyle BackColor="#99FFCC" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Net Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrdNetAmt" runat="server" Text='<%# Bind("ORD_NET_AMOUNT", "{0:c}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#CC33FF" Font-Bold="True" HorizontalAlign="Right" Width="100px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" DeleteText="Remove">
                                    <ItemStyle ForeColor="Red" />
                                </asp:CommandField>
                            </Columns>
                            <EditRowStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Wrap="False" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                        </asp:GridView>
                        <br />
                        <asp:Button ID="btnHold" runat="server" Text="Save" Width="120px" Visible="false"
                            OnClick="btnHold_Click" />
                        <asp:Button ID="btnPost" runat="server" OnClick="btnPost_Click" Text="Post" Visible="false"
                            Width="120px" />
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" Width="120px"
                            OnClick="btnPrint_Click" />
                        <asp:HiddenField ID="hfEditStatus" runat="server" Value="N" />
                        <asp:HiddenField ID="hfRefNo" runat="server" Value="0" />
                        <asp:HiddenField ID="hfBnsPerc" runat="server" Value="0" />
                        <asp:Label ID="lblOrdValText" runat="server" BackColor="#FFFF66" BorderStyle="Dashed"
                            BorderWidth="1px" Visible="False"></asp:Label>
                        <br />
                        <br />
                    </div>
                </asp:Panel>
            </div>
            <div align="center" style="background-color: #9E9AF5">
                <asp:Panel ID="pnlSoAppStat" runat="server" BackColor="#9E9AF5" GroupingText="Approval Status"
                    Visible="false">
                    <br />
                    <div>
                        <asp:Label ID="lblTotSoQty" runat="server" BackColor="#00CC66" BorderStyle="Dashed"
                            BorderWidth="1px" Text="Total Order Qty: 0.00" Visible="False"></asp:Label>
                        &nbsp;<asp:Label ID="lblTotAppQty" runat="server" BackColor="#CC00FF" BorderStyle="Dashed"
                            BorderWidth="1px" Text="Total Approved Qty: 0.00" Visible="False"></asp:Label>
                    </div>
                    <br />
                    <asp:GridView ID="gvApprSoDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Size="8pt" ForeColor="#333333" EmptyDataText="No Data Approved for D/O.">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SO_Det_Icode" HeaderText="Item Code" />
                            <asp:BoundField DataField="SO_Det_Itm_Desc" HeaderText="Item Desc." />
                            <asp:BoundField DataField="SO_Det_Itm_Uom" HeaderText="Unit" />
                            <asp:TemplateField HeaderText="Order Qty" ItemStyle-BackColor="#00CC99" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrdLinQty" runat="server" Font-Bold="True" Text='<%# String.Format("{0:n}", (Convert.ToDecimal(Eval("SO_Det_T_C1")))) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#99CCFF" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrdApprQty" runat="server" Text='<%# Bind("SO_Det_Lin_Qty", "{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BackColor="#00CC99" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ord. Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrdRate" runat="server" Text='<%# Bind("SO_Det_Lin_Rat", "{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="90px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOrdLinAmt" runat="server" BorderStyle="None" CssClass="textAlignRight transparent"
                                        Enabled="false" Font-Size="8" Text='<%# String.Format( "{0:n}", (Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Lin_Rat")))) %>'
                                        Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle BackColor="#CC99FF" HorizontalAlign="Center" Width="90px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Free Bag">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfOrgFreeQty" runat="server" Value='<%# String.Format("{0:n}", (Convert.ToDecimal(Eval("SO_Det_Free_Qty")))) %>' />
                                    <asp:TextBox ID="txtOrdFreeQty" runat="server" BorderStyle="None" CssClass="textAlignCenter transparent"
                                        Enabled="false" Font-Size="8" Text='<%# Bind("SO_Det_Free_Qty", "{0:n}") %>'
                                        Width="50px"></asp:TextBox>
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
                                    <asp:TextBox ID="txtOrdLinNet" runat="server" BorderStyle="None" CssClass="textAlignRight transparent"
                                        Enabled="false" Font-Bold="True" Font-Size="8" Text='<%# String.Format( "{0:n}", ((Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Lin_Rat"))) + (Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Trans_Rat"))))) %>'
                                        Width="100px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle BackColor="#66FF66" HorizontalAlign="Center" Width="100px" />
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
                    </asp:GridView>
                    <br />
                </asp:Panel>
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlOrderList" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnProceed" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlOrdItem" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtCust" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtSalesLoc" EventName="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="Panel4" runat="server" BorderStyle="Solid" BorderWidth="2px" CssClass="tbl"
        DefaultButton="btncancel" Height="200px" ScrollBars="Auto" Style="border-right: black 2px solid;
        display: none; padding-right: 20px; border-top: black 2px solid; padding-left: 20px;
        padding-bottom: 20px; border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
        background-color: white" Width="329px">
        <div style="border-color: #e6e6fa; border-width: 1px; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='#ffffff', startcolorstr='#e6e6fa', gradienttype='0');
            width: 94%; height: 177px; text-align: center;">
            &nbsp;&nbsp;<table id="Table1" runat="server" style="width: 286px">
                <tr>
                    <td colspan="1" style="width: 364px; height: 18px; text-align: center">
                        <span style="color: #ff0000"><strong>Credit Limit Exceeds</strong></span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 364px; height: 13px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 364px; text-align: center">
                        <asp:Button ID="Button1" runat="server" CssClass="btn2" Text="Button1" Width="0px"
                            Height="0px" />
                        Do You Want To Proceed Anyway ?
                    </td>
                </tr>
                <tr>
                    <td style="width: 364px; text-align: center">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="1" style="height: 19px; text-align: center; width: 364px;">
                    </td>
                </tr>
                <tr>
                    <td colspan="1" style="width: 364px; height: 29px; text-align: center">
                        <asp:Button ID="btncancel" runat="server" CssClass="btn2" Text="No" Width="102px" />
                        <asp:Button ID="btnProceedYes" runat="server" CssClass="btn2" Text="Yes" Width="102px"
                            OnClick="btnProceedYes_Click" Visible="false" />
                        <asp:Button ID="btnAddOrdDetYes" runat="server" CssClass="btn2" Text="Yes" Width="102px"
                            Visible="false" OnClick="btnAddOrdDetYes_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender5" runat="server" DisplayModalPopupID="ModalPopupExtender5"
        TargetControlID="Button1">
    </cc1:ConfirmButtonExtender>
    <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btncancel" PopupControlID="Panel4" TargetControlID="Button1">
    </cc1:ModalPopupExtender>
    <asp:Button ID="Button2" runat="server" Text="Button" Visible="False" />
    <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" BorderWidth="2px" CssClass="tbl"
        DefaultButton="btncancel" Height="200px" ScrollBars="Auto" Style="border-right: black 2px solid;
        display: none; padding-right: 20px; border-top: black 2px solid; padding-left: 20px;
        padding-bottom: 20px; border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
        background-color: white" Width="329px">
        <div style="border-color: #e6e6fa; border-width: 1px; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='#ffffff', startcolorstr='#e6e6fa', gradienttype='0');
            width: 94%; height: 177px; text-align: center;">
            &nbsp;&nbsp;<table id="Table2" runat="server" style="width: 286px">
                <tr>
                    <td colspan="1" style="width: 364px; height: 18px; text-align: center">
                        <span style="color: #ff0000"><strong>Credit Limit Exceeds</strong></span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 364px; height: 13px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 364px; text-align: center">
                        <asp:Button ID="Button3" runat="server" CssClass="btn2" Text="Button1" Width="0px"
                            Height="0px" />
                        Do You Want To Proceed Anyway ?
                    </td>
                </tr>
                <tr>
                    <td style="width: 364px; text-align: center">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="1" style="height: 19px; text-align: center; width: 364px;">
                    </td>
                </tr>
                <tr>
                    <td colspan="1" style="width: 364px; height: 29px; text-align: center">
                        <asp:Button ID="btnHoldPostOrdCalcel" runat="server" CssClass="btn2" Text="No" Width="102px" />
                        <asp:Button ID="btnHoldOrdYes" runat="server" CssClass="btn2" Text="Yes" Width="102px"
                            Visible="false" OnClick="btnHoldOrdYes_Click" />
                        <asp:Button ID="btnPostOrdYes" runat="server" CssClass="btn2" Text="Yes" Width="102px"
                            Visible="false" OnClick="btnPostOrdYes_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" DisplayModalPopupID="ModalPopupExtender1"
        TargetControlID="Button3">
    </cc1:ConfirmButtonExtender>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnHoldPostOrdCalcel" PopupControlID="Panel1" TargetControlID="Button3">
    </cc1:ModalPopupExtender>
    <asp:Button ID="Button7" runat="server" Text="Button" Visible="False" />
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
