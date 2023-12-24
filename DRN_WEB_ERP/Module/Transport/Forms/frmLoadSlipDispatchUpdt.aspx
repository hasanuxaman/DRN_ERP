<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmLoadSlipDispatchUpdt.aspx.cs" Inherits="DRN_WEB_ERP.Module.Transport.Forms.frmLoadSlipDispatchUpdt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Load Slip Update</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <br />
            <div>
                <div align="center">
                    <asp:Panel ID="pnlSrchHdr" runat="server" CssClass="cpHeader" Width="600px">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblSrchHdr" Text="Search Load Slip" runat="server" />
                                </td>
                                <td align="right">
                                    <asp:Image ID="imgSrch" CssClass="imgBtn" runat="server" ImageUrl="~/Image/collapse.jpg">
                                    </asp:Image>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlSrchDet" runat="server" CssClass="cpBody" Width="600px" Height="34px">
                        <table>
                            <tr>
                                <td>
                                    Search:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLoadSlipSearch" Width="300px" runat="server" autoComplete="off"
                                        CssClass="textAlignCenter search"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrchLoadSlip"
                                        CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                        DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="LoadSlipSrchList"
                                        ServicePath="~/Module/Transport/Forms/wsAutoCompleteTransport.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                        TargetControlID="txtLoadSlipSearch">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="60px" OnClick="btnSearch_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="60px" Visible="False"
                                        OnClick="btnClear_Click" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSrch" runat="server" TargetControlID="pnlSrchDet"
                        CollapseControlID="pnlSrchHdr" ExpandControlID="pnlSrchHdr" Collapsed="false"
                        TextLabelID="lblSrchHdr" CollapsedText="Search Load Slip" ExpandedText="Search Load Slip"
                        CollapsedSize="0" ExpandedSize="34" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ImageControlID="imgSrch" ExpandedImage="~/Image/collapse.jpg" CollapsedImage="~/Image/expand.jpg"
                        ExpandDirection="Vertical">
                    </cc1:CollapsiblePanelExtender>
                </div>
                <div align="center">
                    <asp:Label ID="lblLsStatusMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                    <br />
                </div>
                <div align="center">
                    <asp:Panel ID="pnlLsHdr" runat="server" CssClass="cpHeader" Width="600px">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblLsHdr" Text="Load Slip Information..." runat="server" />
                                </td>
                                <td align="right">
                                    <asp:Image ID="Image1" CssClass="imgBtn" runat="server" ImageUrl="~/Image/collapse.jpg">
                                    </asp:Image>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlLsDet" runat="server" CssClass="cpBody" Width="600px" Height="350px">
                        <table>
                            <tr>
                                <td align="left">
                                    Truck No
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtLsTruckNo" runat="server" autoComplete="off" Width="300px" BackColor="#33CCCC"
                                        BorderStyle="None" CssClass="textAlignCenter" Enabled="False" Font-Bold="True"
                                        ForeColor="Blue"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Load Slip Time
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left" style="width: 111px">
                                    <asp:TextBox ID="txtLsDateTime" runat="server" autoComplete="off" BackColor="#9999FF"
                                        BorderStyle="None" Enabled="False" Font-Bold="True" ForeColor="Black" Width="140px"
                                        Font-Size="9pt" CssClass="textAlignCenter"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    SL No
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLsTruckSlNo" runat="server" autoComplete="off" BackColor="#9999FF"
                                        BorderStyle="None" CssClass="textAlignCenter" Enabled="False" Font-Bold="True"
                                        ForeColor="Black" Width="78px" Font-Size="9pt"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Dealer Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5" align="left">
                                    <asp:TextBox ID="txtLsDoDealerName" runat="server" autoComplete="off" Width="300px"
                                        CssClass="textAlignCenter" BorderStyle="None" Enabled="False" Font-Size="9pt"
                                        BackColor="#FFFFCC"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Item Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5" align="left">
                                    <asp:TextBox ID="txtLsDoItemName" runat="server" Width="300px" CssClass="textAlignCenter"
                                        BorderStyle="None" Enabled="False" Font-Size="9pt" BackColor="#FFFFCC"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Bag Type
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5" align="left">
                                    <asp:TextBox ID="txtLsDoBagType" runat="server" Width="300px" CssClass="textAlignCenter"
                                        BorderStyle="None" Enabled="False" Font-Size="9pt" BackColor="#FFFFCC"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Quantity
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5" align="left">
                                    <asp:TextBox ID="txtLsDoQty" runat="server" Width="300px" CssClass="textAlignCenter"
                                        BorderStyle="None" Enabled="False" Font-Size="9pt" BackColor="#FFFFCC"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Challan No
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left" style="width: 111px">
                                    <asp:TextBox ID="txtLsDoChlnNo" runat="server" autoComplete="off" BorderStyle="None"
                                        Enabled="False" Font-Bold="False" ForeColor="Black" Width="120px" Font-Size="9pt"
                                        CssClass="textAlignCenter" BackColor="#FFFFCC"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    Updt. Time
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLsDoChallanUpdtTime" runat="server" autoComplete="off" BorderStyle="None"
                                        CssClass="textAlignCenter" Enabled="False" Font-Bold="False" ForeColor="Black"
                                        Width="140px" Font-Size="9pt" BackColor="#FFFFCC"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Empty Truck Wgt.(Kg)
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left" style="width: 111px">
                                    <asp:TextBox ID="txtLsWsEmptyWgt" runat="server" autoComplete="off" BorderStyle="None"
                                        Enabled="False" Font-Bold="False" ForeColor="Black" Width="120px" Font-Size="9pt"
                                        CssClass="textAlignCenter" BackColor="#CC99FF"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    Updt. Time
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLsWsEmptyWgtUpdtTime" runat="server" autoComplete="off" BorderStyle="None"
                                        CssClass="textAlignCenter" Enabled="False" Font-Bold="False" ForeColor="Black"
                                        Width="140px" Font-Size="9pt" BackColor="#CC99FF"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Load Truck Wgt.(Kg)
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left" style="width: 111px">
                                    <asp:TextBox ID="txtLsWsLoadedWgt" runat="server" autoComplete="off" BorderStyle="None"
                                        Enabled="False" Font-Bold="False" ForeColor="Black" Width="120px" Font-Size="9pt"
                                        CssClass="textAlignCenter" BackColor="#CC99FF"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    Updt. Time
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLsWsLoadedWgtUpdtTime" runat="server" autoComplete="off" BorderStyle="None"
                                        CssClass="textAlignCenter" Enabled="False" Font-Bold="False" ForeColor="Black"
                                        Width="140px" Font-Size="9pt" BackColor="#CC99FF"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Tally Item Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5" align="left">
                                    <asp:TextBox ID="txtLsTallyItemName" runat="server" Width="362px" CssClass="textAlignCenter"
                                        BorderStyle="None" Enabled="False" Font-Size="9pt" BackColor="Yellow"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Tally Quantity
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left" style="width: 111px">
                                    <asp:TextBox ID="txtLsTallyQty" runat="server" autoComplete="off" BorderStyle="None"
                                        Enabled="False" Font-Bold="False" ForeColor="Black" Width="120px" Font-Size="9pt"
                                        CssClass="textAlignCenter" BackColor="Yellow"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    Updt. Time
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLsTallyUpdtTime" runat="server" autoComplete="off" BorderStyle="None"
                                        CssClass="textAlignCenter" Enabled="False" Font-Bold="False" ForeColor="Black"
                                        Width="140px" Font-Size="9pt" BackColor="Yellow"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    VAT Challan Status
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left" style="width: 111px">
                                    <asp:TextBox ID="txtLsVatChlnStat" runat="server" autoComplete="off" BorderStyle="None"
                                        Enabled="False" Font-Bold="False" ForeColor="Black" Width="120px" Font-Size="9pt"
                                        CssClass="textAlignCenter" BackColor="#33CC33"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    Updt. Time
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLsVatChlnUpdtTime" runat="server" autoComplete="off" BorderStyle="None"
                                        CssClass="textAlignCenter" Enabled="False" Font-Bold="False" ForeColor="Black"
                                        Width="140px" Font-Size="9pt" BackColor="#33CC33"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Dispatch Gift Item
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5" align="left">
                                    <asp:TextBox ID="txtLsDispGiftItemName" runat="server" Width="362px" CssClass="textAlignCenter"
                                        BorderStyle="None" Enabled="False" Font-Size="9pt" BackColor="#FF99FF"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Advance Amount
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left" style="width: 111px">
                                    <asp:TextBox ID="txtLsDispAdvAmt" runat="server" autoComplete="off" BorderStyle="None"
                                        Enabled="False" Font-Bold="False" ForeColor="Black" Width="120px" Font-Size="9pt"
                                        CssClass="textAlignCenter" BackColor="#FF99FF"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    Fuel Qty
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLsDispFuelQty" runat="server" autoComplete="off" BorderStyle="None"
                                        CssClass="textAlignCenter" Enabled="False" Font-Bold="False" ForeColor="Black"
                                        Width="140px" Font-Size="9pt" BackColor="#FF99FF"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Update Time
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5" align="left">
                                    <asp:TextBox ID="txtLsDispUpdtTime" runat="server" autoComplete="off" BackColor="#FF99FF"
                                        BorderStyle="None" CssClass="textAlignCenter" Enabled="False" Font-Bold="False"
                                        ForeColor="Black" Width="362px" Font-Size="9pt"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 21px">
                                    Acc. Verify Status
                                </td>
                                <td style="height: 21px">
                                    :
                                </td>
                                <td align="left" style="width: 111px; height: 21px;">
                                    <asp:TextBox ID="txtLsAccVerityStatus" runat="server" autoComplete="off" BorderStyle="None"
                                        Enabled="False" Font-Bold="False" ForeColor="Black" Width="120px" Font-Size="9pt"
                                        CssClass="textAlignCenter" BackColor="#3366FF"></asp:TextBox>
                                </td>
                                <td style="height: 21px">
                                    &nbsp;
                                </td>
                                <td align="right" style="height: 21px">
                                    Updt. Time
                                </td>
                                <td style="height: 21px">
                                    :
                                </td>
                                <td align="left" style="height: 21px">
                                    <asp:TextBox ID="txtLsAccVerifyUpdtTime" runat="server" autoComplete="off" BorderStyle="None"
                                        CssClass="textAlignCenter" Enabled="False" Font-Bold="False" ForeColor="Black"
                                        Width="140px" Font-Size="9pt" BackColor="#3366FF"></asp:TextBox>
                                </td>
                                <td style="height: 21px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Gate Pass Time
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5" align="left">
                                    <asp:TextBox ID="txtLsGatePassTime" runat="server" Width="362px" CssClass="textAlignCenter"
                                        BorderStyle="None" Enabled="False" Font-Size="9pt" BackColor="#FF3399" Font-Bold="True"
                                        ForeColor="Black"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="pnlLsDet"
                        CollapseControlID="pnlLsHdr" ExpandControlID="pnlLsHdr" Collapsed="false" TextLabelID="lblLsHdr"
                        CollapsedText="Load Slip Information..." ExpandedText="Load Slip Information..."
                        CollapsedSize="0" ExpandedSize="350" AutoCollapse="False" AutoExpand="False"
                        ScrollContents="false" ImageControlID="imgDo" ExpandedImage="~/Image/collapse.jpg"
                        CollapsedImage="~/Image/expand.jpg" ExpandDirection="Vertical">
                    </cc1:CollapsiblePanelExtender>
                </div>
                <br />
                <div align="center">
                    <asp:Panel ID="pnlDispHdr" runat="server" CssClass="cpHeader" Width="600px">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblDispHdr" Text="Dispatch Section" runat="server" />
                                </td>
                                <td align="right">
                                    <asp:Image ID="imgDisp" CssClass="imgBtn" runat="server" ImageUrl="~/Image/collapse.jpg">
                                    </asp:Image>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlDispDet" runat="server" CssClass="cpBody" Width="600px" Height="65px">
                        <table>
                            <tr>
                                <td style="width: 157px">
                                    &nbsp;
                                </td>
                                <td align="left" style="width: 234px">
                                    Gift Item:
                                </td>
                                <td colspan="5" align="left">
                                    <asp:TextBox ID="txtDispGiftItem" runat="server" autoComplete="off" Width="296px"
                                        CssClass="search textAlignCenter" ValidationGroup="DispatchGrp" Enabled="False"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderGiftItemName" runat="server" BehaviorID="AutoCompleteGiftItemName"
                                        CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                        CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                        ServiceMethod="GetSrchActiveProItem" ServicePath="~/Module/Inventory/Forms/wsAutoCompInv.asmx"
                                        ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtDispGiftItem">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td style="width: 112px">
                                    Qty:
                                </td>
                                <td width="156" align="left">
                                    &nbsp;
                                    <asp:TextBox ID="txtDispGiftItemQty" runat="server" CssClass="textAlignCenter" ValidationGroup="DispatchGrp"
                                        Width="84px" Enabled="False"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtDispGiftItemQty_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDispGiftItemQty"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td width="156">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 157px">
                                    &nbsp;
                                </td>
                                <td align="left" style="width: 234px">
                                    Advance Amount:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDispAdvAmt" runat="server" CssClass="textAlignCenter" ValidationGroup="DispatchGrp"
                                        Width="90px" Enabled="False"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                        FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDispAdvAmt"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 97px">
                                    Fuel Issue Qty
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDispFuelQty" runat="server" CssClass="textAlignCenter" ValidationGroup="DispatchGrp"
                                        Width="95px" Enabled="False"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                        FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDispFuelQty"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="width: 112px">
                                    &nbsp;
                                </td>
                                <td align="center" width="156">
                                    &nbsp;
                                    <asp:Button ID="btnUpdateDispatch" runat="server" Text="Update" ValidationGroup="DispatchGrp"
                                        Visible="true" Width="60px" Enabled="False" OnClick="btnUpdateDispatch_Click" />
                                </td>
                                <td width="156">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderWgtScale" runat="server"
                        TargetControlID="pnlDispDet" CollapseControlID="pnlDispHdr" ExpandControlID="pnlDispHdr"
                        Collapsed="false" TextLabelID="lblDispHdr" CollapsedText="Dispatch Section" ExpandedText="Dispatch Section"
                        CollapsedSize="0" ExpandedSize="65" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                        ImageControlID="imgDisp" ExpandedImage="~/Image/collapse.jpg" CollapsedImage="~/Image/expand.jpg"
                        ExpandDirection="Vertical">
                    </cc1:CollapsiblePanelExtender>
                </div>
                <asp:HiddenField ID="hfLsRefNo" runat="server" Value="0" />
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
