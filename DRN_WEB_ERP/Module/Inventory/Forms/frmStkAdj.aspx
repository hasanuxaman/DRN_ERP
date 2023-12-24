<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmStkAdj.aspx.cs" Inherits="DRN_WEB_ERP.Module.Inventory.Forms.frmStkAdj" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
    </script>
    <div align="center" style="background-color: #00FF99">
        Stock Adjustment</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <asp:Panel ID="pnlOrdHdr" runat="server">
                    <div align="center" style="background-color: #CC66FF">
                        <br />
                        Tran. Ref. No:
                        <asp:DropDownList ID="ddlTranList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTranList_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                        <br />
                    </div>
                    <div align="center" style="background-color: #6600CC; height: 30px;">
                        <table style="width: 100%;">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label3" runat="server" ForeColor="White" Text="Tran. Ref. No: " CssClass="style3"></asp:Label>
                                    <asp:Label ID="lblTranRefNo" runat="server" ForeColor="White"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" BorderStyle="None" ForeColor="White" Text="Total Item: "></asp:Label>
                                    <asp:Label ID="lblTotTranItem" runat="server" BorderStyle="None" ForeColor="White"
                                        Text="0.00"></asp:Label>
                                </td>
                                <td align="right">
                                    &nbsp;
                                    <asp:Label ID="Label2" runat="server" BorderStyle="None" ForeColor="White" Text="Total Qty: "></asp:Label>
                                    <asp:Label ID="lblTotTranVal" runat="server" BorderStyle="None" ForeColor="White"
                                        Text="0.00"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="background-color: #9E9AF5">
                        <br />
                        <table style="width: 100%; font-family: verdana; font-size: small;">
                            <tr>
                                <td style="width: 25px">
                                    &nbsp;
                                </td>
                                <td style="width: 41px">
                                    Date
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 161px">
                                    <asp:TextBox ID="txtTranDate" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        ForeColor="Blue" Width="120px" Enabled="False" AutoPostBack="True" OnTextChanged="txtTranDate_TextChanged"></asp:TextBox>
                                    <asp:Image ID="imgReqDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <cc1:CalendarExtender ID="CalenderDelDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgReqDt" TargetControlID="txtTranDate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="width: 14px">
                                    &nbsp;
                                </td>
                                <td style="width: 76px">
                                    Remarks
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRem" runat="server" CssClass="inline search" Width="680px"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25px">
                                    &nbsp;
                                </td>
                                <td style="width: 41px">
                                    &nbsp;
                                </td>
                                <td width="2px">
                                    &nbsp;
                                </td>
                                <td style="width: 161px">
                                    &nbsp;
                                </td>
                                <td style="width: 14px">
                                    &nbsp;
                                </td>
                                <td style="width: 76px">
                                    &nbsp;
                                </td>
                                <td width="2px">
                                    &nbsp;
                                </td>
                                <td>
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
                </asp:Panel>
                <asp:Panel ID="pnlOrdDet" runat="server" DefaultButton="btnAddAdjDet">
                    <div align="center" style="background-color: #9E9AF5">
                        <table cellspacing="0" rules="all" border="1" style="border-collapse: collapse;">
                            <tr style="background-color: #009933; font-weight: bold; color: #F7F7F7; font-size: small;
                                white-space: nowrap; height: 30px;">
                                <th align="center" scope="col" style="width: 160px;" width="70">
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
                                </th>
                                <th align="center" scope="col" width="80">
                                    Store
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemQty0" runat="server" ControlToValidate="ddlTranStore"
                                        ErrorMessage="Select Store" ForeColor="Red" InitialValue="0" Text="*" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                                <th align="center" scope="col" width="100">
                                    Stock Qty</th>
                                <th align="center" scope="col" width="100">
                                    New Stock
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemQty" runat="server" ControlToValidate="txtTranQty"
                                        ErrorMessage="Enter New Stock Quantity" ForeColor="Red" Text="*" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                            </tr>
                            <tr class="gridFooterRow" style="background-color: #86AEAE;">
                                <td>
                                    <asp:DropDownList ID="cboItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboItemType_SelectedIndexChanged"
                                        Width="160px">
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
                                    <asp:DropDownList ID="ddlTranItemUom" runat="server" Enabled="False" Width="80px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTranStore" runat="server" Width="190px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlTranStore_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCurStock" runat="server" CssClass="textAlignCenter" Enabled="False"
                                        Width="100px" Font-Bold="True" ForeColor="#0099FF"></asp:TextBox>
                                    <%--<cc1:FilteredTextBoxExtender ID="txtCurStock_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCurStock"
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
                        <asp:Button ID="btnAddAdjDet" runat="server" Text="Add" ValidationGroup="btnAdd"
                            Width="75px" OnClick="btnAddAdjDet_Click" />
                        <br />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" Font-Size="10pt" ForeColor="Red"
                            ValidationGroup="btnAdd" DisplayMode="List" />
                    </div>
                    <div align="center" style="background-color: #9E9AF5">
                        <br />
                        <asp:Label ID="lblItmStkAdjDed" runat="server" BackColor="Red" BorderColor="#FF99FF"
                            Text="== Item Deduction List ==" Visible="False"></asp:Label>
                        <asp:GridView ID="gvItmStkAdjDed" runat="server" CellPadding="4" Font-Size="10pt"
                            ForeColor="#333333" AutoGenerateColumns="False" OnRowDeleting="gvItmStkAdjDed_RowDeleting">
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
                                <asp:TemplateField HeaderText="Store">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfStoreRef" runat="server" Value='<%# Bind("TRN_STORE_REF") %>' />
                                        <asp:Label ID="lblStore" runat="server" Text='<%# Bind("TRN_STORE_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cur. Stock">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurStk" runat="server" Text='<%# Bind("TRN_STK") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" BackColor="#CC9900" Width="90px" Font-Bold="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="New Stock">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTranQty" runat="server" Text='<%# Bind("TRN_QTY", "{0:c}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#CC3399" HorizontalAlign="Center" Width="90px" />
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
                        <asp:Label ID="lblItmStkAdjAdd" runat="server" BackColor="#009933" BorderColor="#FF99FF"
                            Text="== Item Addition List ==" Visible="False"></asp:Label>
                        <asp:GridView ID="gvItmStkAdjAdd" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            Font-Size="10pt" ForeColor="#333333" OnRowDeleting="gvItmStkAdjAdd_RowDeleting">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCodeAdd" runat="server" Text='<%# Bind("TRN_ITEM_REF") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Desc.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemDescAdd" runat="server" Text='<%# Bind("TRN_ITEM_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemUnitAdd" runat="server" Text='<%# Bind("TRN_ITEM_UOM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Store">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfStoreRefAdd" runat="server" Value='<%# Bind("TRN_STORE_REF") %>' />
                                        <asp:Label ID="lblStoreAdd" runat="server" Text='<%# Bind("TRN_STORE_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cur. Stock">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurStkAdd" runat="server" Text='<%# Bind("TRN_STK") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#CC9900" Font-Bold="False" HorizontalAlign="Center" Width="90px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="New Stock">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTranQtyAdd" runat="server" Text='<%# Bind("TRN_QTY", "{0:c}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#CC3399" HorizontalAlign="Center" Width="90px" />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="Remove" ShowDeleteButton="True">
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
                        <br />
                        <br />
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlTranList" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboItemType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtItemName" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlTranStore" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnAddAdjDet" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnHold" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnPost" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvItmStkAdjDed" EventName="RowDataBound" />
            <asp:AsyncPostBackTrigger ControlID="gvItmStkAdjDed" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="gvItmStkAdjAdd" EventName="RowDataBound" />
            <asp:AsyncPostBackTrigger ControlID="gvItmStkAdjAdd" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="txtTranDate" EventName="TextChanged" />
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
