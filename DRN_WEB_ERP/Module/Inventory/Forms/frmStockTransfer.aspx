<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmStockTransfer.aspx.cs" Inherits="DRN_WEB_ERP.Module.Inventory.Forms.frmStockTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        
    </script>
    <div align="center" style="background-color: #00FF99">
        Stock Transfer</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <asp:Panel ID="pnlOrdHdr" runat="server">
                    <div align="center" style="background-color: #CC66FF">
                        <br />
                        Transfer Ref. No:
                        <asp:DropDownList ID="ddlTransferList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTransferList_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                        <br />
                    </div>
                    <div align="center" style="background-color: #6600CC; height: 30px;">
                        <table style="width: 100%;">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label3" runat="server" ForeColor="White" Text="Transfer Ref No: "
                                        CssClass="style3"></asp:Label>
                                    <asp:Label ID="lblIssRefNo" runat="server" ForeColor="White"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" BorderStyle="None" ForeColor="White" Text="Total Item No: "></asp:Label>
                                    <asp:Label ID="lblTotMrrItem" runat="server" BorderStyle="None" ForeColor="White"
                                        Text="0.00"></asp:Label>
                                </td>
                                <td align="right">
                                    &nbsp;
                                    <asp:Label ID="Label2" runat="server" BorderStyle="None" ForeColor="White" Text="Total Quantity: "></asp:Label>
                                    <asp:Label ID="lblTotIssQty" runat="server" BorderStyle="None" ForeColor="White"
                                        Text="0.00"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="background-color: #9E9AF5">
                        <br />
                        <table style="width: 100%; font-family: verdana; font-size: small;">
                            <tr>
                                <td style="width: 203px">
                                    &nbsp;
                                </td>
                                <td style="width: 97px">
                                    Tran. Ref. No
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 148px">
                                    <asp:TextBox ID="txtTranRef" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        Font-Bold="True" Font-Size="10pt" ForeColor="Blue" Width="190px" Enabled="False"></asp:TextBox>
                                </td>
                                <td style="width: 33px">
                                    &nbsp;
                                </td>
                                <td style="width: 33px">
                                    &nbsp;
                                </td>
                                <td style="width: 64px">
                                    Date
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 138px">
                                    <asp:TextBox ID="txtTranDate" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        ForeColor="Blue" Width="165px" Enabled="False"></asp:TextBox>
                                    <asp:Image ID="imgIssDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <cc1:CalendarExtender ID="CalenderDelDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgIssDt" TargetControlID="txtTranDate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="width: 56px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 203px">
                                    &nbsp;
                                </td>
                                <td style="width: 97px">
                                    From Store
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 148px">
                                    <asp:DropDownList ID="ddlFromStore" runat="server" Width="190px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlFromStore_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 33px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlFromStore"
                                        ErrorMessage="Select From Store" ForeColor="Red" InitialValue="0" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 33px">
                                    &nbsp;
                                </td>
                                <td style="width: 64px">
                                    To Store
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 138px">
                                    <asp:DropDownList ID="ddlToStore" runat="server" Width="192px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 56px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlToStore"
                                        ErrorMessage="Select To Store" ForeColor="Red" InitialValue="0" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 203px">
                                    &nbsp;
                                </td>
                                <td style="width: 97px" valign="top">
                                    Remarks
                                </td>
                                <td width="2px" valign="top">
                                    :
                                </td>
                                <td colspan="7" valign="top">
                                    <asp:TextBox ID="txtRem" runat="server" Width="532px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 203px">
                                    &nbsp;
                                </td>
                                <td style="width: 97px">
                                    &nbsp;
                                </td>
                                <td width="2px">
                                    &nbsp;
                                </td>
                                <td style="width: 148px">
                                    &nbsp;
                                </td>
                                <td style="width: 33px">
                                    &nbsp;
                                </td>
                                <td style="width: 33px">
                                    &nbsp;
                                </td>
                                <td style="width: 64px">
                                    &nbsp;
                                </td>
                                <td width="2px">
                                    &nbsp;
                                </td>
                                <td style="width: 138px">
                                    &nbsp;
                                </td>
                                <td style="width: 56px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <div align="center">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="10pt" ForeColor="Red"
                                ValidationGroup="btnSave" DisplayMode="List" />
                            <br />
                            <br />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlTranDet" runat="server" DefaultButton="btnAddItem">
                    <div align="center" style="background-color: #9E9AF5">
                        <table border="1" cellspacing="0" rules="all" style="border-collapse: collapse;">
                            <tr style="background-color: #009933; font-weight: bold; color: #F7F7F7; font-size: small;
                                white-space: nowrap; height: 30px;">
                                <th align="center" scope="col" style="width: 200px;" width="70">
                                    Item Type
                                </th>
                                <th align="center" scope="col" style="width: 210px;" width="70">
                                    Item
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemName1" runat="server" ControlToValidate="txtItemName"
                                        ErrorMessage="Select Item Name" ForeColor="Red" Text="*" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtItemName"
                                        ErrorMessage="Enter Valid Item" ForeColor="Red" OnServerValidate="CustomValidator3_ServerValidate"
                                        ValidationGroup="btnAdd">*</asp:CustomValidator>
                                </th>
                                <th align="center" scope="col" width="80">
                                    Unit
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemQty3" runat="server" ControlToValidate="cboItemUom"
                                        ErrorMessage="Unit" ForeColor="Red" InitialValue="0" Text="*" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                                <th align="center" scope="col" width="80">
                                    Cur. Stock
                                </th>
                                <th align="center" scope="col" width="100">
                                    Tran. Quantity
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemQty2" runat="server" ControlToValidate="txtTranQty"
                                        ErrorMessage="Enter Issue Quantity" ForeColor="Red" Text="*" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                                <%--<th align="center" scope="col" width="80">
                                    &nbsp;
                                </th>--%>
                            </tr>
                            <tr class="gridFooterRow" style="background-color: #86AEAE;">
                                <td>
                                    <asp:DropDownList ID="cboItemType" runat="server" AutoPostBack="True" Width="200px"
                                        OnSelectedIndexChanged="cboItemType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtItemName" runat="server" AutoPostBack="True" CssClass="search textAlignCenter"
                                        OnTextChanged="txtItemName_TextChanged" Width="350px"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchItem" runat="server" BehaviorID="AutoCompleteSrchItem"
                                        CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                        CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                        ServiceMethod="GetSrchFilteredItem" ServicePath="~/Module/Inventory/Forms/wsAutoCompInv.asmx"
                                        ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtItemName">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboItemUom" runat="server" Enabled="False" Width="80px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStock" runat="server" BackColor="#CCFFCC" BorderStyle="None"
                                        CssClass="textAlignCenter" Enabled="false" Width="100px"></asp:TextBox>
                                    <%--<cc1:FilteredTextBoxExtender ID="txtStock_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtStock"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>--%>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTranQty" runat="server" CssClass="textAlignCenter" Width="100px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtTranQty_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtTranQty"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnAddItem" runat="server" Text="Add" ValidationGroup="btnAdd" Width="75px"
                            OnClick="btnAddItem_Click" />
                        <br />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" Font-Size="10pt" ForeColor="Red"
                            ValidationGroup="btnAdd" DisplayMode="List" />
                    </div>
                    <div align="center" style="background-color: #9E9AF5">
                        <br />
                        <asp:GridView ID="gvIssDet" runat="server" CellPadding="4" Font-Size="10pt" ForeColor="#333333"
                            AutoGenerateColumns="False" OnRowDeleting="gvIssDet_RowDeleting">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("TRN_ITEM_REF") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Desc.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("TRN_ITEM_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemUnit" runat="server" Text='<%# Bind("TRN_ITEM_UOM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssQty" runat="server" Text='<%# Bind("TRN_QTY", "{0:c}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" BackColor="#CC3399" Width="90px" />
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
                        <asp:Button ID="btnHold" runat="server" Text="Save" Width="120px" OnClick="btnHold_Click"
                            ValidationGroup="btnSave" Enabled="False" Visible="False" />
                        <asp:Button ID="btnPost" runat="server" OnClick="btnPost_Click" Text="Post" Width="120px"
                            ValidationGroup="btnSave" Enabled="False" />
                        <asp:Button ID="btnPrint" runat="server" Text="Print" Width="120px" OnClick="btnPrint_Click"
                            Enabled="False" />
                        <asp:HiddenField ID="hfEditStatus" runat="server" Value="N" />
                        <asp:HiddenField ID="hfRefNo" runat="server" Value="0" />
                        <br />
                        <br />
                        <div>
                            <iframe id="acrobatPanel" frameborder="0" height="700px" name="I1" src="frmShowIFrameRpt.aspx"
                                style="border: 2px inset #C0C0C0" width="98%"></iframe>
                        </div>
                        <br />
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlTransferList" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlFromStore" 
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboItemType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtItemName" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnAddItem" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnHold" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnPost" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvIssDet" EventName="RowDataBound" />
            <asp:AsyncPostBackTrigger ControlID="gvIssDet" EventName="RowDeleting" />
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
