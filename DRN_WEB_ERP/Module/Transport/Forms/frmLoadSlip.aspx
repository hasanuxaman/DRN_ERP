<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmLoadSlip.aspx.cs" Inherits="DRN_WEB_ERP.Module.Transport.Forms.frmLoadSlip" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Load Slip</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <br />
            <div align="center">
                <asp:Panel ID="pnlSlipHdr" runat="server" CssClass="cpHeader" Width="700px">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Label ID="lblSlipHdr" Text="Issue Load Slip" runat="server" />
                            </td>
                            <td align="right">
                                <asp:Image ID="imgSlip" CssClass="imgBtn" runat="server" ImageUrl="~/Image/collapse.jpg">
                                </asp:Image>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlSlipDet" runat="server" CssClass="cpBody" Width="700px" Height="400px"
                    DefaultButton="btnIsuLoadSlip">
                    <br />
                    <asp:Panel ID="pnlSearch" runat="server" CssClass="cpBody" DefaultButton="btnSearch">
                        <table>
                            <tr>
                                <td>
                                    Search:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLoadSlipSearch" Width="350px" runat="server" autoComplete="off"
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLoadSlipSearch"
                                        ErrorMessage="Enter Search Text" Font-Size="8pt" ForeColor="Red" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                        ValidationGroup="btnSearch" Width="60px" />
                                </td>
                                <td>
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="60px" Visible="False"
                                        OnClick="btnClear_Click" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Label ID="lblLsStatusMsg" runat="server" ForeColor="Red" Font-Bold="True" Font-Size="12pt"></asp:Label>
                    <br />
                    <br />
                    <div align="center">
                        <%--<asp:Image ID="Image1" runat="server" Height="141px" ImageUrl="~/Image/Load_Slip_Barcode.png"
                            Width="318px" />--%>
                        <asp:PlaceHolder ID="plBarCode" runat="server" />
                    </div>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 71px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 86px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 142px">
                                &nbsp;
                            </td>
                            <td style="width: 69px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 74px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td style="width: 44px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 71px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 86px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="right" colspan="3">
                                <asp:RadioButtonList ID="optTranBy" runat="server" AutoPostBack="True" Font-Size="8pt"
                                    OnSelectedIndexChanged="optTranBy_SelectedIndexChanged" RepeatDirection="Horizontal"
                                    BackColor="#FFCC00">
                                    <asp:ListItem Value="1" Selected="True">Company&#39;s Truck</asp:ListItem>
                                    <asp:ListItem Value="2">Customer&#39;s Truck</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td style="width: 44px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 71px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 86px">
                                Load Slip Ref.
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" style="width: 142px">
                                <asp:TextBox ID="txtLoadSlipRefNo" runat="server" CssClass="textAlignCenter" Enabled="False"
                                    Font-Bold="True"></asp:TextBox>
                            </td>
                            <td style="width: 69px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 74px">
                                Date &amp; Time
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLoadSlipDate" runat="server" CssClass="textAlignCenter" Enabled="False"></asp:TextBox>
                            </td>
                            <td style="width: 44px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 71px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 86px">
                                Truck No
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" style="width: 142px">
                                <asp:TextBox ID="txtTruckNo" runat="server" Width="167px" Visible="False"></asp:TextBox>
                                <%--<cc1:AutoCompleteExtender ID="AutoCompleteExtenderTruck" runat="server" BehaviorID="AutoCompleteSrchTruck"
                                    CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                    DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="TruckSrchList"
                                    ServicePath="~/Module/Transport/Forms/wsAutoCompleteTransport.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                    TargetControlID="txtTruckNo">
                                </cc1:AutoCompleteExtender>--%>
                                <asp:DropDownList ID="cboTruckNo" runat="server" Width="172px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 69px">
                            </td>
                            <td align="left" style="width: 74px">
                                SL No
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtTruckSlNo" runat="server" CssClass="textAlignCenter"></asp:TextBox>
                            </td>
                            <td style="width: 44px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 71px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 86px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 142px">
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="optTranBy"
                                    ErrorMessage="Enter Truck No" Font-Size="8pt" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"
                                    ValidationGroup="btnIsuLs"></asp:CustomValidator>
                            </td>
                            <td style="width: 69px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 74px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTruckSlNo"
                                    ErrorMessage="Enter SL No" Font-Size="8pt" ForeColor="Red" ValidationGroup="btnIsuLs"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 44px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 71px">
                                &nbsp;
                            </td>
                            <td align="center" colspan="7">
                                <asp:Button ID="btnGateOut" runat="server" OnClick="btnGateOut_Click" Text="Gate Pass"
                                    Width="95px" Visible="False" />
                                <asp:Button ID="btnRePrint" runat="server" Text="Print" Width="95px" Visible="False"
                                    OnClick="btnRePrint_Click" />
                                <asp:Button ID="btnIsuLoadSlip" runat="server" Text="Issue Load Slip" OnClick="btnIsuLoadSlip_Click"
                                    ValidationGroup="btnIsuLs" />
                                <asp:Button ID="btnIsuPrintLoadSlip" runat="server" Text="Issue &amp; Print Load Slip"
                                    OnClick="btnIsuPrintLoadSlip_Click" ValidationGroup="btnIsuLs" />
                            </td>
                            <td style="width: 44px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 71px">
                                &nbsp;
                            </td>
                            <td align="center" colspan="7">
                                <asp:Button ID="btnConnectPrinter" runat="server" OnClick="btnConnectPrinter_Click"
                                    Text="Printer Not Connected. Connect Printer ?" Visible="False" />
                                &nbsp;
                            </td>
                            <td style="width: 44px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="pnlSlipDet"
                    CollapseControlID="pnlSlipHdr" ExpandControlID="pnlSlipHdr" Collapsed="false"
                    TextLabelID="lblSlipHdr" CollapsedText="Issue Load Slip" ExpandedText="Issue Load Slip"
                    CollapsedSize="0" ExpandedSize="400" AutoCollapse="False" AutoExpand="False"
                    ScrollContents="false" ImageControlID="imgSlip" ExpandedImage="~/Image/collapse.jpg"
                    CollapsedImage="~/Image/expand.jpg" ExpandDirection="Vertical">
                </cc1:CollapsiblePanelExtender>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnGateOut" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnRePrint" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnIsuLoadSlip" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnIsuPrintLoadSlip" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="optTranBy" EventName="SelectedIndexChanged" />
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
