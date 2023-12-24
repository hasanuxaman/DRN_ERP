<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmLoadSlipUpdateOld.aspx.cs" Inherits="DRN_WEB_ERP.Module.Transport.Forms.frmLoadSlipUpdateOld" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Load Slip Update</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <div align="center">
                    <asp:Panel ID="pnlSrchHdr" runat="server" CssClass="cpHeader" Width="600px">
                        <asp:Label ID="lblSrchHdr" Text="Search Load Slip" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlSrchDet" runat="server" CssClass="cpBody" Width="600px" Height="34px">
                        <table>
                            <tr>
                                <td>
                                    Search Load Slip:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLoadSlipSearch" Width="300px" runat="server" autoComplete="off"
                                        CssClass="textAlignCenter search"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="txtLoadSlipSearch_AutoCompleteExtender" runat="server"
                                        BehaviorID="AutoCompleteExSrchLoadSlip" CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                        CompletionSetCount="20" DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3"
                                        ServiceMethod="GetInvPOListAll" ServicePath="~/ClientSide/Inventory/services/autocomplete.asmx"
                                        ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtLoadSlipSearch">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="60px" />
                                </td>
                                <td>
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="60px" Visible="False" />
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSrch" runat="server" TargetControlID="pnlSrchDet"
                        CollapseControlID="pnlSrchHdr" ExpandControlID="pnlSrchHdr" Collapsed="false"
                        TextLabelID="lblSrchHdr" CollapsedText="Show Search..." ExpandedText="Hide Search..."
                        CollapsedSize="0" ExpandedSize="34" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ImageControlID="Image1" ExpandedImage="~/images/collapse.jpg" CollapsedImage="~/images/expand.jpg"
                        ExpandDirection="Vertical">
                    </cc1:CollapsiblePanelExtender>
                </div>
                <div align="center">
                    <asp:Panel ID="pnlDoHdr" runat="server" CssClass="cpHeader" Width="600px">
                        <asp:Label ID="lblDoHdr" Text="D/O Section" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlDoDet" runat="server" CssClass="cpBody" Width="600px" Height="160px">
                        <table>
                            <tr>
                                <td>
                                    Dealer Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDealer" runat="server" autoComplete="off" Width="300px"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtendeDealer" runat="server" BehaviorID="AutoCompleteDealer"
                                        CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                        CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                        ServiceMethod="GetSrchDealer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                        ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtDealer">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Enter Valid Data"
                                        ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate" ControlToValidate="txtDealer"
                                        ValidationGroup="DoGrp"></asp:CustomValidator>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Item Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDoItemName" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtDoItemName"
                                        ErrorMessage="Enter Valid Data" ForeColor="Red" OnServerValidate="CustomValidator2_ServerValidate"
                                        ValidationGroup="DoGrp"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Bag Type:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDoBagType" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="txtDoBagType"
                                        ErrorMessage="Enter Valid Data" ForeColor="Red" OnServerValidate="CustomValidator4_ServerValidate"
                                        ValidationGroup="DoGrp"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Quantity:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDoQty" runat="server" Width="300px" CssClass="textAlignCenter"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                        FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDoQty"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDoQty"
                                        ErrorMessage="Enter Quantity" ForeColor="Red" ValidationGroup="DoGrp"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Challan No:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDoChallanNo" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtDoChallanNo"
                                        ErrorMessage="Enter Valid Data" ForeColor="Red" OnServerValidate="CustomValidator3_ServerValidate"
                                        ValidationGroup="DoGrp"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center">
                                    <asp:Button ID="btnUpdateDo" runat="server" Text="Update" ValidationGroup="DoGrp"
                                        Width="60px" Visible="true" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderHdr" runat="server" TargetControlID="pnlDoDet"
                        CollapseControlID="pnlDoHdr" ExpandControlID="pnlDoHdr" Collapsed="false" TextLabelID="lblDoHdr"
                        CollapsedText="D/O Section Show..." ExpandedText="D/O Section Hide..." CollapsedSize="0"
                        ExpandedSize="160" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ImageControlID="Image1" ExpandedImage="~/images/collapse.jpg" CollapsedImage="~/images/expand.jpg"
                        ExpandDirection="Vertical">
                    </cc1:CollapsiblePanelExtender>
                </div>
                <div align="center">
                    <asp:Panel ID="pnlWgtScaleHdr" runat="server" CssClass="cpHeader" Width="600px">
                        <asp:Label ID="lblWgtScaleHdr" Text="Weight Scale Section" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlWgtScaleDet" runat="server" CssClass="cpBody" Width="600px" Height="85px">
                        <table>
                            <tr>
                                <td>
                                    Empty Truck Wgt:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmptyWgt" runat="server" autoComplete="off" Width="300px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                        FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtEmptyWgt"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDoQty"
                                        ErrorMessage="Enter Empty Vassel Weight" ForeColor="Red" ValidationGroup="WgtScaleGrp"></asp:RequiredFieldValidator>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Load Truck Wgt:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLoadWgt" runat="server" Width="300px" ValidationGroup="WgtScaleGrp"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                        FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtLoadWgt"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDoQty"
                                        ErrorMessage="Enter Loaded Vassel Weight" ForeColor="Red" ValidationGroup="WgtScaleGrp"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center">
                                    <asp:Button ID="btnUpdateWgtScale" runat="server" Text="Update" ValidationGroup="WgtScaleGrp"
                                        Width="60px" Visible="true" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderWgtScale" runat="server"
                        TargetControlID="pnlWgtScaleDet" CollapseControlID="pnlWgtScaleHdr" ExpandControlID="pnlWgtScaleHdr"
                        Collapsed="false" TextLabelID="lblWgtScaleHdr" CollapsedText="Weight Scale Section Show..."
                        ExpandedText="Weight Scale Section Hide..." CollapsedSize="0" ExpandedSize="85"
                        AutoCollapse="False" AutoExpand="False" ScrollContents="false" ImageControlID="Image1"
                        ExpandedImage="~/images/collapse.jpg" CollapsedImage="~/images/expand.jpg" ExpandDirection="Vertical">
                    </cc1:CollapsiblePanelExtender>
                </div>
                <div align="center">
                    <asp:Panel ID="pnlTallyHdr" runat="server" CssClass="cpHeader" Width="600px">
                        <asp:Label ID="lblTallyHdr" Text="Tally Section" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlTallyDet" runat="server" CssClass="cpBody" Width="600px" Height="80px">
                        <table>
                            <tr>
                                <td>
                                    Item Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTallyItemName" runat="server" autoComplete="off" Width="300px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                        FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtTallyItemName"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:CustomValidator ID="CustomValidator5" runat="server" ControlToValidate="txtTallyItemName"
                                        ErrorMessage="Enter Valid Data" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"
                                        ValidationGroup="TallyGrp"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Quantity:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTallyQty" runat="server" Width="300px" ValidationGroup="WgtScaleGrp"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                        FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtLoadWgt"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtTallyQty"
                                        ErrorMessage="Enter Quantity" ForeColor="Red" ValidationGroup="TallyGrp"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center">
                                    <asp:Button ID="btnUpdateTally" runat="server" Text="Update" ValidationGroup="TallyGrp"
                                        Width="60px" Visible="true" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="pnlTallyDet"
                        CollapseControlID="pnlTallyHdr" ExpandControlID="pnlTallyHdr" Collapsed="false"
                        TextLabelID="lblTallyHdr" CollapsedText="Tally Section Show..." ExpandedText="Tally Section Hide..."
                        CollapsedSize="0" ExpandedSize="80" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ImageControlID="Image1" ExpandedImage="~/images/collapse.jpg" CollapsedImage="~/images/expand.jpg"
                        ExpandDirection="Vertical">
                    </cc1:CollapsiblePanelExtender>
                </div>
                <div align="center">
                    <asp:Panel ID="pnlVatHdr" runat="server" CssClass="cpHeader" Width="600px">
                        <asp:Label ID="lblVatHdr" Text="VAT Section" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlVatDet" runat="server" CssClass="cpBody" Width="600px" Height="70px">
                        <table>
                            <tr>
                                <td width="64">
                                    &nbsp;
                                </td>
                                <td width="300">
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                        FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtEmptyWgt"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td width="150">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="64">
                                    &nbsp;
                                </td>
                                <td align="center" width="300">
                                    <asp:Button ID="btnUpdateVat" runat="server" Text="Update VAT Challan" ValidationGroup="WgtScaleGrp"
                                        Visible="true" />
                                </td>
                                <td width="150">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="64">
                                    &nbsp;
                                </td>
                                <td align="center" width="300">
                                    &nbsp;
                                </td>
                                <td width="150">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" TargetControlID="pnlVatDet"
                        CollapseControlID="pnlVatHdr" ExpandControlID="pnlVatHdr" Collapsed="false" TextLabelID="lblVatHdr"
                        CollapsedText="VAT Section Show..." ExpandedText="VAT Section Hide..." CollapsedSize="0"
                        ExpandedSize="70" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ImageControlID="Image1" ExpandedImage="~/images/collapse.jpg" CollapsedImage="~/images/expand.jpg"
                        ExpandDirection="Vertical">
                    </cc1:CollapsiblePanelExtender>
                </div>
                <div align="center">
                    <asp:Panel ID="pnlDispatchHdr" runat="server" CssClass="cpHeader" Width="600px">
                        <asp:Label ID="lblDispatchHdr" Text="Dispatch Section" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlDispatchDet" runat="server" CssClass="cpBody" Width="600px" Height="110px">
                        <table>
                            <tr>
                                <td>
                                    Gift Item:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDispGiftItem" runat="server" autoComplete="off" Width="300px"
                                        ValidationGroup="DispatchGrp"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                        FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtEmptyWgt"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td width="156">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Advance Amount:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDispAdvAmt" runat="server" Width="300px" ValidationGroup="DispatchGrp"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                        FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDispAdvAmt"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td width="156">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Fuel Issue Qty:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDispFuelQty" runat="server" ValidationGroup="DispatchGrp" Width="300px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                        FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDispFuelQty"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td width="156">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center">
                                    <asp:Button ID="btnUpdateDispatch" runat="server" Text="Update" ValidationGroup="DispatchGrp"
                                        Width="60px" Visible="true" />
                                </td>
                                <td width="156">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" TargetControlID="pnlDispatchDet"
                        CollapseControlID="pnlDispatchHdr" ExpandControlID="pnlDispatchHdr" Collapsed="false"
                        TextLabelID="lblDispatchHdr" CollapsedText="Dispatch Section Show..." ExpandedText="Dispatch Section Hide..."
                        CollapsedSize="0" ExpandedSize="110" AutoCollapse="False" AutoExpand="False"
                        ScrollContents="false" ImageControlID="Image1" ExpandedImage="~/images/collapse.jpg"
                        CollapsedImage="~/images/expand.jpg" ExpandDirection="Vertical">
                    </cc1:CollapsiblePanelExtender>
                </div>
                <div align="center">
                    <asp:Panel ID="pnlAccHdr" runat="server" CssClass="cpHeader" Width="600px">
                        <asp:Label ID="lblAccHdr" Text="Accounts Section" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlAccDet" runat="server" CssClass="cpBody" Width="600px" Height="110px">
                        <table>
                            <tr>
                                <td>
                                    Gift Item:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAccGiftItem" runat="server" autoComplete="off" Width="300px"
                                        ValidationGroup="AccGrp"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                        FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtEmptyWgt"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td width="156">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Advance Amount:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAccAdvAmt" runat="server" Width="300px" ValidationGroup="AccGrp"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                        FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDispAdvAmt"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td width="156">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Fuel Issue Qty:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAccFuelQty" runat="server" ValidationGroup="AccGrp" Width="300px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                        FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDispFuelQty"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td width="156">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center">
                                    <asp:Button ID="btnUpdateAcc" runat="server" Text="Update" ValidationGroup="AccGrp"
                                        Width="60px" Visible="true" />
                                </td>
                                <td width="156">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server" TargetControlID="pnlAccDet"
                        CollapseControlID="pnlAccHdr" ExpandControlID="pnlAccHdr" Collapsed="false" TextLabelID="lblAccHdr"
                        CollapsedText="Accounts Section Show..." ExpandedText="Accounts Section Hide..."
                        CollapsedSize="0" ExpandedSize="110" AutoCollapse="False" AutoExpand="False"
                        ScrollContents="false" ImageControlID="Image1" ExpandedImage="~/images/collapse.jpg"
                        CollapsedImage="~/images/expand.jpg" ExpandDirection="Vertical">
                    </cc1:CollapsiblePanelExtender>
                </div>
                <br />
            </div>
        </ContentTemplate>
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
