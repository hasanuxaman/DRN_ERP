<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmStoreReq.aspx.cs" Inherits="DRN_WEB_ERP.Module.Inventory.Forms.frmStoreReq" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .cpHeader
        {
            color: white;
            background-color: #719DDB;
            font: bold 11px auto "Trebuchet MS" , Verdana;
            font-size: 12px;
            cursor: pointer;
            width: 450px;
            height: 18px;
            padding: 4px;
        }
        .cpBody
        {
            background-color: #DCE4F9;
            font: normal 11px auto Verdana, Arial;
            border: 1px gray;
            width: 450px;
            padding: 4px;
            padding-top: 2px;
            height: 0px;
            overflow: hidden;
        }
        .style4
        {
            width: 312px;
        }
        .style5
        {
            width: 346px;
        }
    </style>
    <table style="width: 100%;">
        <tr>
            <td>
                &nbsp;
            </td>
            <td align="center">
                Store Requisition (SR)
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:UpdatePanel ID="updtPnl" runat="server">
                    <ContentTemplate>
                        <div align="center">
                            <asp:Panel ID="pnlSrchHdr" runat="server" CssClass="cpHeader" Width="600px">
                                <asp:Label ID="Label1" Text="Search SR" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnlSrchDet" runat="server" CssClass="cpBody" Width="600px" Height="34px">
                                <table>
                                    <tr>
                                        <td>
                                            Search SR:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSrSearch" Width="300px" runat="server" autoComplete="off" AutoPostBack="True"
                                                OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="txtSrSearch_AutoCompleteExtender" runat="server" BehaviorID="AutoCompleteEx1"
                                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="3"
                                                ServiceMethod="GetInvPOListAll" ServicePath="~/ClientSide/Inventory/services/autocomplete.asmx"
                                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSrSearch">
                                            </cc1:AutoCompleteExtender>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                                Width="60px" Height="18px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" Width="60px" Height="18px"
                                                OnClick="btnClear_Click" Visible="False" />
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSrch" runat="server" TargetControlID="pnlSrchDet"
                                CollapseControlID="pnlSrchHdr" ExpandControlID="pnlSrchHdr" Collapsed="true"
                                TextLabelID="Label1" CollapsedText="(Show Search..)" ExpandedText="(Hide Search..)"
                                CollapsedSize="0" ExpandedSize="34" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                                ImageControlID="Image1" ExpandedImage="~/images/collapse.jpg" CollapsedImage="~/images/expand.jpg"
                                ExpandDirection="Vertical">
                            </cc1:CollapsiblePanelExtender>
                            <br />
                        </div>
                        <div align="center">
                            <asp:Panel ID="pHeaderText" runat="server" CssClass="cpHeader" Width="600px">
                                <asp:Label ID="lblText" Text="Header" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pHdrBody" runat="server" CssClass="cpBody" Width="600px" Height="160px">
                                <table>
                                    <tr>
                                        <td>
                                            Requisition By:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFromDept" Width="300px" runat="server" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFromDept"
                                                runat="server" ErrorMessage="Enter Requisition By" ValidationGroup="HdrGrp" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Required For:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboReqFor" runat="server" Width="305px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboReqFor"
                                                runat="server" ErrorMessage="Select Reason" ValidationGroup="HdrGrp" ForeColor="Red"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Dealer/Retailer:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDealer" runat="server" autoComplete="off" Width="300px"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtendeDealer" runat="server" BehaviorID="AutoCompleteDealer"
                                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                                ServiceMethod="GetSrchDealerRetailer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtDealer">
                                            </cc1:AutoCompleteExtender>
                                        </td>
                                        <td>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Enter Valid Data"
                                                ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Entry Date:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEntryDate" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEntryDate"
                                                ErrorMessage="Enter Valid Entry Date" ForeColor="Red" ValidationGroup="HdrGrp"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Required Date:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRequiredDate" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtRequiredDate"
                                                runat="server" ErrorMessage="Enter Required Date" ValidationGroup="HdrGrp" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Remarks:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="300px"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="Next" ValidationGroup="HdrGrp"
                                                Width="60px" Visible="False" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderHdr" runat="server" TargetControlID="pHdrBody"
                                CollapseControlID="pHeaderText" ExpandControlID="pHeaderText" Collapsed="false"
                                TextLabelID="lblText" CollapsedText="Show Header" ExpandedText="Hide Header"
                                CollapsedSize="0" ExpandedSize="160" AutoCollapse="False" AutoExpand="False"
                                ScrollContents="false" ImageControlID="Image1" ExpandedImage="~/images/collapse.jpg"
                                CollapsedImage="~/images/expand.jpg" ExpandDirection="Vertical">
                            </cc1:CollapsiblePanelExtender>
                        </div>
                        <br />
                        <div align="center">
                            <asp:Panel ID="pDetailsText" runat="server" CssClass="cpHeader" Width="600px">
                                <asp:Label ID="lblDetText" Text="Details" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pDetBody" runat="server" CssClass="cpBody" Width="600px" Height="110px"
                                DefaultButton="btnAdd">
                                <table>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;
                                        </td>
                                        <td>
                                            Item:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtItem" runat="server" autoComplete="off" AutoPostBack="True" OnTextChanged="txtItem_TextChanged"
                                                CssClass="textAlignCenter" Width="300px"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" BehaviorID="AutoCompleteEx2"
                                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="false" MinimumPrefixLength="1"
                                                ServiceMethod="GetSrchProItem" ServicePath="~/Module/Inventory/Forms/wsAutoCompInv.asmx"
                                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtItem">
                                            </cc1:AutoCompleteExtender>
                                        </td>
                                        <td width="600">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtItem"
                                                ErrorMessage="Enter Valid Item" ForeColor="Red" ValidationGroup="DetGrp"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;
                                        </td>
                                        <td>
                                            UOM:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUom" runat="server" Enabled="false" BackColor="#CCCCCC" Width="300px"
                                                CssClass="textAlignCenter"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Stock:"></asp:Label>
                                            <asp:Label ID="lblStkQty" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;
                                        </td>
                                        <td>
                                            Quantity:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQuantity" runat="server" Width="300px" CssClass="textAlignCenter"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtQuantity_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtQuantity"
                                                ValidChars=".">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtQuantity"
                                                runat="server" ErrorMessage="Enter Item Quantity" ValidationGroup="DetGrp" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" Width="60px"
                                                ValidationGroup="DetGrp" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderDet" runat="server" TargetControlID="pDetBody"
                                CollapseControlID="pDetailsText" ExpandControlID="pDetailsText" Collapsed="false"
                                TextLabelID="lblDetText" CollapsedText="Show Details" ExpandedText="Hide Details"
                                CollapsedSize="0" ExpandedSize="110" AutoCollapse="False" AutoExpand="False"
                                ScrollContents="false" ImageControlID="Image1" ExpandedImage="~/images/collapse.jpg"
                                CollapsedImage="~/images/expand.jpg" ExpandDirection="Vertical">
                            </cc1:CollapsiblePanelExtender>
                        </div>
                        <br />
                        <div align="center">
                            <asp:GridView ID="gvSR" runat="server" CellPadding="4" ForeColor="#333333" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                Font-Names="Arial" Font-Size="10pt">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:CommandField SelectText="Remove" ShowSelectButton="True">
                                        <ItemStyle ForeColor="Red" />
                                    </asp:CommandField>
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                            <br />
                            <asp:Button ID="btnPost" runat="server" Text="Save" Width="90px" OnClick="btnPost_Click"
                                Visible="False" ValidationGroup="HdrGrp" />
                        </div>
                        <br />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtQuantity" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="txtItem" EventName="TextChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
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
