<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmIssueDirect.aspx.cs" Inherits="DRN_WEB_ERP.Module.Inventory.Forms.frmIssueDirect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        
    </script>
    <div align="center" style="background-color: #00FF99">
        Material Issue (Direct)</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <asp:Panel ID="pnlOrdHdr" runat="server">
                    <%--<div align="center" style="background-color: #CC66FF">
                        <br />
                        Issue Ref. No:
                        <asp:DropDownList ID="ddlIssueList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIssueList_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                        <br />
                    </div>--%>
                    <div align="center" style="background-color: #CC66FF">
                        <br />
                        Year:<asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
                            Width="100px">
                        </asp:DropDownList>
                        &nbsp;Month:<asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"
                            Width="100px">
                        </asp:DropDownList>
                        &nbsp;Select Order:
                        <asp:DropDownList ID="ddlIssueList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIssueList_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                        <br />
                    </div>
                    <div align="center" style="background-color: #6600CC; height: 30px;">
                        <table style="width: 100%;">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label3" runat="server" ForeColor="White" Text="Issue Ref No: " CssClass="style3"></asp:Label>
                                    <asp:Label ID="lblIssRefNo" runat="server" ForeColor="White"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" BorderStyle="None" ForeColor="White" Text="Total Item: "></asp:Label>
                                    &nbsp;<asp:Label ID="lblTotMrrItem" runat="server" BorderStyle="None" ForeColor="White"
                                        Text="0.00"></asp:Label>
                                </td>
                                <td align="right">
                                    &nbsp;
                                    <asp:Label ID="Label2" runat="server" BorderStyle="None" ForeColor="White" Text="Total Quantity: "></asp:Label>
                                    &nbsp;<asp:Label ID="lblTotIssQty" runat="server" BorderStyle="None" ForeColor="White"
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
                                    <asp:TextBox ID="txtIssDate" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        ForeColor="Blue" Width="120px" Enabled="False"></asp:TextBox>
                                    <asp:Image ID="imgIssDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <cc1:CalendarExtender ID="CalenderDelDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgIssDt" TargetControlID="txtIssDate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="width: 33px">
                                    &nbsp;
                                </td>
                                <td style="width: 33px">
                                    &nbsp;
                                </td>
                                <td style="width: 33px">
                                    &nbsp;
                                </td>
                                <td style="width: 33px">
                                    &nbsp;
                                </td>
                                <td style="width: 33px">
                                    &nbsp;
                                </td>
                                <td style="width: 33px">
                                    &nbsp;
                                </td>
                                <td style="width: 33px">
                                    &nbsp;
                                </td>
                                <td style="width: 85px">
                                    Req. Ref No
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 138px">
                                    <asp:TextBox ID="txtReqRef" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        Font-Bold="True" Font-Size="10pt" ForeColor="Blue" Width="190px"></asp:TextBox>
                                </td>
                                <td style="width: 22px">
                                    &nbsp;
                                </td>
                                <td style="width: 87px">
                                </td>
                                <td align="center" valign="middle">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <div>
                            <table style="width: 100%; font-family: verdana; font-size: small;">
                                <tr>
                                    <td style="width: 118px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 60px">
                                        Issue To
                                    </td>
                                    <td width="2px">
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIssLoc" runat="server" CssClass="inline search" Width="680px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                                            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                            DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchIssueHead"
                                            ServicePath="~/Module/Inventory/Forms/wsAutoCompInv.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                            TargetControlID="txtIssLoc">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td width="2px">
                                    </td>
                                    <td style="width: 66px">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCust0" runat="server" ControlToValidate="txtIssLoc"
                                            ErrorMessage="Enter Issue Head" ForeColor="Red" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtIssLoc"
                                            ErrorMessage="Enter Valid Location" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"
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
                                    <td style="width: 118px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 60px">
                                        Rem.
                                    </td>
                                    <td width="2px">
                                        :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtRem" runat="server" Width="695px"></asp:TextBox>
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
                                <tr>
                                    <td style="width: 118px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 60px">
                                        &nbsp;
                                    </td>
                                    <td width="2px">
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        &nbsp;
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
                        <div align="center">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="10pt" ForeColor="Red"
                                ValidationGroup="btnSave" DisplayMode="List" />
                            <br />
                            <br />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlOrdDet" runat="server" DefaultButton="btnAddIsuDet">
                    <div align="center" style="background-color: #9E9AF5">
                        <table border="1" cellspacing="0" rules="all" style="border-collapse: collapse;">
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemQty3" runat="server" ControlToValidate="cboItemUom"
                                        ErrorMessage="Select Item Unit" ForeColor="Red" InitialValue="0" Text="*" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                                <th align="center" scope="col" width="80">
                                    Store
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemQty1" runat="server" ControlToValidate="cboStore"
                                        ErrorMessage="Select Store" ForeColor="Red" InitialValue="0" Text="*" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                                <th align="center" scope="col" width="100">
                                    Quantity
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorItemQty2" runat="server" ControlToValidate="txtIssQty"
                                        ErrorMessage="Enter Issue Quantity" ForeColor="Red" Text="*" ValidationGroup="btnAdd"></asp:RequiredFieldValidator>
                                </th>
                                <%--<th align="center" scope="col" width="80">
                                    &nbsp;
                                </th>--%>
                                <th align="center" scope="col" width="100">
                                    Stock
                                </th>
                            </tr>
                            <tr class="gridFooterRow" style="background-color: #86AEAE;">
                                <td>
                                    <asp:DropDownList ID="cboItemType" runat="server" AutoPostBack="True" Width="160px"
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
                                    <asp:DropDownList ID="cboStore" runat="server" Width="190px" AutoPostBack="True"
                                        OnSelectedIndexChanged="cboStore_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIssQty" runat="server" CssClass="textAlignCenter" Width="100px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtIssQty_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtIssQty"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStock" runat="server" BackColor="#CCFFCC" BorderStyle="None"
                                        CssClass="textAlignCenter" Enabled="false" Width="100px"></asp:TextBox>
                                    <%--<cc1:FilteredTextBoxExtender ID="txtStock_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtStock"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>--%>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnAddIsuDet" runat="server" Text="Add" ValidationGroup="btnAdd"
                            Width="75px" OnClick="btnAddIsuDet_Click" />
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
                                <asp:TemplateField HeaderText="Store">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfStoreRef" runat="server" Value='<%# Bind("TRN_STORE_REF") %>' />
                                        <asp:Label ID="lblStore" runat="server" Text='<%# Bind("TRN_STORE_NAME") %>'></asp:Label>
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
            <asp:AsyncPostBackTrigger ControlID="ddlIssueList" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboItemType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtItemName" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboStore" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnAddIsuDet" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnHold" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnPost" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvIssDet" EventName="RowDeleting" />
            <asp:AsyncPostBackTrigger ControlID="gvIssDet" EventName="RowDataBound" />
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
