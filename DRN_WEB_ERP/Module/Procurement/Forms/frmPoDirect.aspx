<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmPoDirect.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmPoDirect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CalcPoAmount(PoQty, PoRate, PoAmt) {
            var PoQuantity = 0;
            if (document.getElementById(PoQty).value != '') {
                PoQuantity = parseFloat(document.getElementById(PoQty).value);
            }
            //alert(PoQuantity.toString());

            var PoItemRate = 0;
            if (document.getElementById(PoRate).value != '') {
                PoItemRate = parseFloat(document.getElementById(PoRate).value);
            }
            //alert(PoItemRate.toString());

            var PoAmount = document.getElementById(PoAmt);
            var PoValue = parseFloat(PoQuantity * PoItemRate);
            PoAmount.value = PoValue.toFixed(2);
            //alert(PoValue.toString());
        }
    </script>
    <div align="center" style="background-color: #00FF99">
        Purchase Order (Direct)</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <asp:Panel ID="pnlOrdHdr" runat="server">
                    <div align="center" style="background-color: #CC66FF">
                        <br />
                        <br />
                        Year:<asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
                            Width="100px">
                        </asp:DropDownList>
                        &nbsp;Month:<asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"
                            Width="100px">
                        </asp:DropDownList>
                        PO Ref. No:
                        <asp:DropDownList ID="ddlPoList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPoList_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                        <br />
                    </div>
                    <div align="center" style="background-color: #6600CC; height: 30px;">
                        <table style="width: 100%;">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label3" runat="server" ForeColor="White" Text="PO Ref No: " CssClass="style3"></asp:Label>
                                    <asp:Label ID="lblPoRefNo" runat="server" ForeColor="White"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" BorderStyle="None" ForeColor="White" Text="Total Item: "></asp:Label>
                                    <asp:Label ID="lblTotPoItem" runat="server" BorderStyle="None" ForeColor="White"
                                        Text="0.00"></asp:Label>
                                </td>
                                <td align="right">
                                    &nbsp;
                                    <asp:Label ID="Label2" runat="server" BorderStyle="None" ForeColor="White" Text="Total Value: "></asp:Label>
                                    <asp:Label ID="lblTotPoVal" runat="server" BorderStyle="None" ForeColor="White" Text="0.00"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="background-color: #9E9AF5">
                        <br />
                        <table style="width: 100%; font-family: verdana; font-size: small;">
                            <tr>
                                <td style="width: 129px">
                                    &nbsp;
                                </td>
                                <td style="width: 48px">
                                    Date
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 148px">
                                    <asp:TextBox ID="txtPoDate" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        ForeColor="Blue" Width="120px" Enabled="False"></asp:TextBox>
                                    <asp:Image ID="imgReqDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <cc1:CalendarExtender ID="CalenderDelDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgReqDt" TargetControlID="txtPoDate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="width: 33px">
                                    &nbsp;
                                </td>
                                <td style="width: 76px">
                                    MPR Ref.
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 138px">
                                    <asp:TextBox ID="txtMprRef" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        Font-Bold="True" Font-Size="10pt" ForeColor="Blue" Width="190px"></asp:TextBox>
                                </td>
                                <td style="width: 22px">
                                    &nbsp;
                                </td>
                                <td style="width: 87px">
                                    Carring Cost
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 66px">
                                    <asp:TextBox ID="txtCarryCharge" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        Font-Bold="True" Font-Size="10pt" ForeColor="Blue" Width="120px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtCustCrLimit_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCarryCharge"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td align="center" valign="middle">
                                    <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtCarryCharge"
                                        ErrorMessage="Enter Valid Amount" ForeColor="Red" Operator="DataTypeCheck" Type="Currency"
                                        ValidationGroup="Save">*</asp:CompareValidator>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <div>
                            <table style="width: 100%; font-family: verdana; font-size: small;">
                                <tr>
                                    <td style="width: 128px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 49px">
                                        Supplier
                                    </td>
                                    <td width="2px">
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSup" runat="server" CssClass="inline search" Width="700px" Enabled="False"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                                            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                            DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchSupplier"
                                            ServicePath="~/Module/Procurement/Forms/wsAutoComProc.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                            TargetControlID="txtSup">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td width="2px">
                                    </td>
                                    <td style="width: 66px">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorSup" runat="server" ControlToValidate="txtSup"
                                            ErrorMessage="Enter Supplier First" ForeColor="Red" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtSup"
                                            ErrorMessage="Enter Valid Supplier" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"
                                            ValidationGroup="btnSave">*</asp:CustomValidator>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 128px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 49px">
                                        &nbsp;
                                    </td>
                                    <td width="2px">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td width="2px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 66px">
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div align="center">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="10pt" ForeColor="Red"
                                ValidationGroup="btnSave" DisplayMode="List" />
                            <br />
                            <br />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlOrdDet" runat="server" DefaultButton="btnAddPoDet">
                    <div align="center" style="background-color: #9E9AF5">
                        <table cellspacing="0" rules="all" border="1" style="border-collapse: collapse;">
                            <tr style="background-color: #009933; font-weight: bold; color: #F7F7F7; font-size: small;
                                white-space: nowrap; height: 30px;">
                                <th align="center" scope="col" width="70" style="width: 200px;">
                                    Item Type</th>
                                <th align="center" scope="col" style="width: 210px;" width="70">
                                    Item Name
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemName1" runat="server" ControlToValidate="txtItemName"
                                        ErrorMessage="Enter Item Name" ForeColor="Red" Text="*" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtItemName"
                                        ErrorMessage="Enter Valid Item" ForeColor="Red" OnServerValidate="CustomValidator3_ServerValidate"
                                        ValidationGroup="btnAdd">*</asp:CustomValidator>
                                </th>
                                <th align="center" scope="col" width="80">
                                    Unit
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemQty1" runat="server" 
                                        ControlToValidate="ddlItemUom" ErrorMessage="Select Unit" ForeColor="Red" 
                                        InitialValue="0" Text="*" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                                <th align="center" scope="col" width="80">
                                    Store
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemQty0" runat="server" ControlToValidate="ddlStore"
                                        ErrorMessage="Select Store" ForeColor="Red" InitialValue="0" Text="*" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                                <th align="center" scope="col" width="100">
                                    Quantity
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemQty" runat="server" ControlToValidate="txtPoQty"
                                        ErrorMessage="Enter PO Quantity" ForeColor="Red" Text="*" 
                                        ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                                <th align="center" scope="col" width="50">
                                    Rate
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemRate" runat="server" ErrorMessage="Enter Item Rate"
                                        ForeColor="Red" Text="*" ControlToValidate="txtPoRate" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                                <th align="center" scope="col" width="50">
                                    Amount
                                </th>
                                <%--<th align="center" scope="col" width="80">
                                    &nbsp;
                                </th>--%>
                            </tr>
                            <tr class="gridFooterRow" style="background-color: #86AEAE;">
                                <td>
                                    <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" Width="200px"
                                        OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtItemName" runat="server" AutoPostBack="True" CssClass="search textAlignCenter"
                                        OnTextChanged="txtItemName_TextChanged" Width="340px"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchItem" runat="server" BehaviorID="AutoCompleteSrchItem"
                                        CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                        CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                        ServiceMethod="GetSrchFilteredItem" ServicePath="~/Module/Inventory/Forms/wsAutoCompInv.asmx"
                                        ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtItemName">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlItemUom" runat="server" Enabled="False" Width="80px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStore" runat="server" Width="90px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPoQty" runat="server" CssClass="textAlignCenter" Width="100px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtPoQty_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtPoQty"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPoRate" runat="server" CssClass="textAlignCenter" Width="80px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtPoRate_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtPoRate"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPoAmt" runat="server" CssClass="textAlignCenter" Enabled="False"
                                        Font-Bold="True" Font-Size="10pt" ForeColor="Blue" Width="100px" BackColor="#6699FF"
                                        BorderStyle="Dashed" BorderWidth="1px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnAddPoDet" runat="server" Text="Add" ValidationGroup="btnAdd" Width="75px"
                            OnClick="btnAddPoDet_Click" />
                        <br />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" Font-Size="10pt" ForeColor="Red"
                            ValidationGroup="btnAdd" DisplayMode="List" />
                    </div>
                    <div align="center" style="background-color: #9E9AF5">
                        <br />
                        <asp:GridView ID="gvPoDet" runat="server" CellPadding="4" Font-Size="10pt" ForeColor="#333333"
                            AutoGenerateColumns="False" OnRowDeleting="gvPoDet_RowDeleting">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("PO_ITEM_REF") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Desc.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("PO_ITEM_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemUnit" runat="server" Text='<%# Bind("PO_ITEM_UOM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Store">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfStoreRef" runat="server" Value='<%# Bind("PO_STORE_REF") %>' />
                                        <asp:Label ID="lblStore" runat="server" Text='<%# Bind("PO_STORE_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPoQty" runat="server" Text='<%# Bind("PO_QTY", "{0:c}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" BackColor="#CC3399" Width="90px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPoRate" runat="server" Text='<%# Bind("PO_RATE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#CC9900" Font-Bold="False" HorizontalAlign="Center" Width="90px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPoAmt" runat="server" Text='<%# Bind("PO_AMOUNT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#99FFCC" HorizontalAlign="Right" Font-Bold="True" Width="100px" />
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
                        <asp:Button ID="btnHold" runat="server" Text="Save" Width="120px"
                            OnClick="btnHold_Click" ValidationGroup="btnSave" />
                        <asp:Button ID="btnPost" runat="server" OnClick="btnPost_Click" Text="Post"
                            Width="120px" ValidationGroup="btnSave" />
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Width="120px"
                            OnClick="btnPrint_Click" Enabled="False" />
                        <asp:HiddenField ID="hfEditStatus" runat="server" Value="N" />
                        <asp:HiddenField ID="hfRefNo" runat="server" Value="0" />
                        <br />
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>            
            <asp:AsyncPostBackTrigger ControlID="ddlItemType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtItemName" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnAddPoDet" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnHold" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnPost" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvPoDet" EventName="RowDeleting" />
            <asp:AsyncPostBackTrigger ControlID="gvPoDet" EventName="RowDataBound" />
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
