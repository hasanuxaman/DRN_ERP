<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmMrrDirect.aspx.cs" Inherits="DRN_WEB_ERP.Module.Inventory.Forms.frmMrrDirect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CalcMrrAmount(MrrQty, MrrRate, MrrAmt) {
            var MrrQuantity = 0;
            if (document.getElementById(MrrQty).value != '') {
                MrrQuantity = parseFloat(document.getElementById(MrrQty).value);
            }
            //alert(MrrQuantity.toString());

            var MrrItemRate = 0;
            if (document.getElementById(MrrRate).value != '') {
                MrrItemRate = parseFloat(document.getElementById(MrrRate).value);
            }
            //alert(MrrItemRate.toString());

            var MrrAmount = document.getElementById(MrrAmt);
            var MrrValue = parseFloat(MrrQuantity * MrrItemRate);
            MrrAmount.value = MrrValue.toFixed(2);
            //alert(MrrValue.toString());
        }
    </script>
    <div align="center" style="background-color: #00FF99">
        Material Receive (Direct)</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <asp:Panel ID="pnlOrdHdr" runat="server">
                    <div align="center" style="background-color: #CC66FF">
                        <br />
                        Year:<asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" 
                            OnSelectedIndexChanged="cboYear_SelectedIndexChanged" Width="100px">
                        </asp:DropDownList>
                        &nbsp;Month:<asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" 
                            OnSelectedIndexChanged="cboMonth_SelectedIndexChanged" Width="100px">
                        </asp:DropDownList>
                        &nbsp;MRR Ref. No:
                        <asp:DropDownList ID="ddlMrrList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMrrList_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                        <br />
                    </div>
                    <div align="center" style="background-color: #6600CC; height: 30px;">
                        <table style="width: 100%;">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label3" runat="server" ForeColor="White" Text="MRR Ref No: " CssClass="style3"></asp:Label>
                                    <asp:Label ID="lblMrrRefNo" runat="server" ForeColor="White"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" BorderStyle="None" ForeColor="White" Text="Total Item: "></asp:Label>
                                    <asp:Label ID="lblTotMrrItem" runat="server" BorderStyle="None" ForeColor="White"
                                        Text="0.00"></asp:Label>
                                </td>
                                <td align="right">
                                    &nbsp;
                                    <asp:Label ID="Label2" runat="server" BorderStyle="None" ForeColor="White" Text="Total Value: "></asp:Label>
                                    <asp:Label ID="lblTotMrrVal" runat="server" BorderStyle="None" ForeColor="White"
                                        Text="0.00"></asp:Label>
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
                                    <asp:TextBox ID="txtMrrDate" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        ForeColor="Blue" Width="120px" Enabled="False"></asp:TextBox>
                                    <asp:Image ID="imgReqDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <cc1:CalendarExtender ID="CalenderDelDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgReqDt" TargetControlID="txtMrrDate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="width: 33px">
                                    &nbsp;
                                </td>
                                <td style="width: 76px">
                                    P.O Ref No
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 138px">
                                    <asp:TextBox ID="txtPoRef" runat="server" BorderStyle="None" CssClass="textAlignCenter"
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
                                        <asp:TextBox ID="txtSup" runat="server" CssClass="inline search" Width="700px"></asp:TextBox>
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
                <asp:Panel ID="pnlOrdDet" runat="server" DefaultButton="btnAddMrrDet">
                    <div align="center" style="background-color: #9E9AF5">
                        <table cellspacing="0" rules="all" border="1" style="border-collapse: collapse;">
                            <tr style="background-color: #009933; font-weight: bold; color: #F7F7F7; font-size: small;
                                white-space: nowrap; height: 30px;">
                                <th align="center" scope="col" width="70" style="width: 150px;">
                                    Item Type
                                </th>
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemQty1" runat="server" ControlToValidate="txtMrrItemUom"
                                        ErrorMessage="Invalid MRR Item Unit" ForeColor="Red" Text="*" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                                <th align="center" scope="col" width="150">
                                    Store
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemQty0" runat="server" ControlToValidate="ddlMrrStore"
                                        ErrorMessage="Select Store" ForeColor="Red" InitialValue="0" Text="*" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                                <th align="center" scope="col" width="100">
                                    Quantity
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemQty" runat="server" ControlToValidate="txtMrrQty"
                                        ErrorMessage="Enter MRR Quantity" ForeColor="Red" Text="*" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                                <th align="center" scope="col" width="50">
                                    Rate
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemRate" runat="server" ErrorMessage="Enter Item Rate"
                                        ForeColor="Red" Text="*" ControlToValidate="txtMrrRate" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
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
                                    <asp:DropDownList ID="ddlMrrItemType" runat="server" AutoPostBack="True" Width="150px"
                                        OnSelectedIndexChanged="ddlMrrItemType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <%--<asp:DropDownList ID="ddlMrrItem" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrdItem_SelectedIndexChanged"
                                        Width="230px">
                                    </asp:DropDownList>--%>
                                    <asp:TextBox ID="txtItemName" runat="server" AutoPostBack="True" CssClass="search textAlignCenter"
                                        Width="350px" OnTextChanged="txtItemName_TextChanged"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchItem" runat="server" BehaviorID="AutoCompleteSrchItem"
                                        CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                        CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                        ServiceMethod="GetSrchFilteredItem" ServicePath="~/Module/Inventory/Forms/wsAutoCompInv.asmx"
                                        ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtItemName">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td>
                                    <%--<asp:DropDownList ID="ddlMrrItemUom" runat="server" Enabled="False" Width="80px">
                                    </asp:DropDownList>--%>
                                    <asp:TextBox ID="txtMrrItemUom" runat="server" Width="80px" Enabled="False" CssClass="textAlignCenter"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMrrStore" runat="server" Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMrrQty" runat="server" CssClass="textAlignCenter" Width="100px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtMrrQty_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtMrrQty"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMrrRate" runat="server" CssClass="textAlignCenter" Width="80px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtMrrRate_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtMrrRate"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMrrAmt" runat="server" CssClass="textAlignCenter" Enabled="False"
                                        Font-Bold="True" Font-Size="10pt" ForeColor="Blue" Width="100px" BackColor="#FFFF99"
                                        BorderStyle="Dashed" BorderWidth="1px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnAddMrrDet" runat="server" Text="Add" ValidationGroup="btnAdd"
                            Width="75px" OnClick="btnAddMrrDet_Click" />
                        <br />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" Font-Size="10pt" ForeColor="Red"
                            ValidationGroup="btnAdd" DisplayMode="List" />
                    </div>
                    <div align="center" style="background-color: #9E9AF5">
                        <br />
                        <asp:GridView ID="gvMrrDet" runat="server" CellPadding="4" Font-Size="10pt" ForeColor="#333333"
                            AutoGenerateColumns="False" OnRowDeleting="gvMrrDet_RowDeleting">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("MRR_ITEM_REF") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Desc.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("MRR_ITEM_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemUnit" runat="server" Text='<%# Bind("MRR_ITEM_UOM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Store">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfMrrStoreRef" runat="server" Value='<%# Bind("MRR_STORE_REF") %>' />
                                        <asp:Label ID="lblMrrStore" runat="server" Text='<%# Bind("MRR_STORE_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMrrQty" runat="server" Text='<%# Bind("MRR_QTY", "{0:c}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" BackColor="#CC3399" Width="90px" Font-Bold="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMrrRate" runat="server" Text='<%# Bind("MRR_RATE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#CC9900" Font-Bold="False" HorizontalAlign="Center" Width="90px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMrrAmt" runat="server" Text='<%# Bind("MRR_AMOUNT") %>'></asp:Label>
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
                        <asp:Button ID="btnHold" runat="server" Text="Save" Width="120px" Visible="false"
                            OnClick="btnHold_Click" ValidationGroup="btnSave" />
                        <asp:Button ID="btnPost" runat="server" OnClick="btnPost_Click" Text="Post" Visible="false"
                            Width="120px" ValidationGroup="btnSave" />
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" Width="120px"
                            OnClick="btnPrint_Click" />
                        <asp:HiddenField ID="hfEditStatus" runat="server" Value="N" />
                        <asp:HiddenField ID="hfRefNo" runat="server" Value="0" />
                        <br />
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtSup" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlMrrItemType" EventName="SelectedIndexChanged" />
            <%--<asp:AsyncPostBackTrigger ControlID="ddlMrrItem" EventName="SelectedIndexChanged" />--%>
            <asp:AsyncPostBackTrigger ControlID="txtItemName" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnAddMrrDet" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnHold" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnPost" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvMrrDet" EventName="RowDeleting" />
            <asp:AsyncPostBackTrigger ControlID="gvMrrDet" EventName="RowDataBound" />
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
