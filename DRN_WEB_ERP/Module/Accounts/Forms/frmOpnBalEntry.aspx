<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmOpnBalEntry.aspx.cs" Inherits="DRN_WEB_ERP.Module.Accounts.Forms.frmOpnBalEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Opening Balance Entry</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnSearch">
                <div align="center" style="background-color: #86AEAE">
                    <br />
                    <span>Search GL Code:</span> <span>
                        <asp:TextBox ID="txtSearch" runat="server" Width="550px" CssClass="search textAlignCenter"></asp:TextBox>
                        <%--<cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCustomer"
                            ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                            TargetControlID="txtSearch">
                        </cc1:AutoCompleteExtender>--%>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCoa" ServicePath="~/Module/Accounts/Forms/wsAutoComAcc.asmx"
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearch">
                        </cc1:AutoCompleteExtender>
                        <asp:Button ID="btnSearch" runat="server" onkeypress="return clickButton(event,'btnSearch')"
                            Text="Search" ValidationGroup="btnSearch" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnClearSrch" runat="server" Text="Clear" Visible="False" Width="60px"
                            OnClick="btnClearSrch_Click" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSearch"
                            ErrorMessage="Enter Search Text" Font-Bold="False" ForeColor="Red" ValidationGroup="btnSearch"
                            Font-Size="10pt"></asp:RequiredFieldValidator>
                        <br />
                        <br />
                    </span>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlDet" runat="server" Enabled="false">
        <div align="center" style="background-color: #9999FF">
            <%--<br />--%>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" bgcolor="#3366FF">
                <tr>
                    <td align="left" style="width: 33%;">
                        <asp:Label ID="Label1" runat="server" BorderStyle="None" ForeColor="White" Text="Debit Amount: "></asp:Label>
                        <asp:Label ID="lblTotDr" runat="server" BorderStyle="None" ForeColor="White" Text="0.00"></asp:Label>
                    </td>
                    <td align="center" style="width: 33px">
                        <asp:Label ID="Label3" runat="server" BorderStyle="None" ForeColor="White" Text="Credit Amount: "></asp:Label>
                        <asp:Label ID="lblTotCr" runat="server" BorderStyle="None" ForeColor="White" Text="0.00"></asp:Label>
                    </td>
                    <td align="right" style="width: 33px">
                        <asp:Label ID="Label2" runat="server" BorderStyle="None" ForeColor="White" Text="Closing Balance: "></asp:Label>
                        <asp:Label ID="lblCloseBal" runat="server" BorderStyle="None" ForeColor="White" Text="0.00"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; font-size: 12px;">
                <tr valign="top">
                    <td align="right" style="font-size: 12px; width: 113px;">
                        Ref. No
                    </td>
                    <td align="left" width="8">
                        :
                    </td>
                    <td align="left" class="style1" style="width: 140px">
                        <asp:TextBox ID="txtTranRefNo" runat="server" Enabled="False" Font-Bold="True" ForeColor="#FF9900"
                            Width="180px" CssClass="textAlignCenter"></asp:TextBox>
                    </td>
                    <td align="right" class="style4" style="font-size: 12px; width: 89px;">
                        Tran. Type
                    </td>
                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                        :
                    </td>
                    <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                        <asp:DropDownList ID="ddlTrnType" runat="server" Width="130px">
                            <asp:ListItem Value="C">Credit</asp:ListItem>
                            <asp:ListItem Value="D">Debit</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" class="style5" style="font-size: 12px; width: 74px;">
                        Narration
                    </td>
                    <td align="left" class="style5" style="width: 8px;">
                        :
                    </td>
                    <td align="left" style="font-size: 14px; width: 2px;" valign="top" rowspan="4">
                        <asp:TextBox ID="txtNarration" runat="server" TextMode="MultiLine" Height="54px"></asp:TextBox>
                    </td>
                    <td align="left" class="style5" style="font-size: 14px; width: 20px;">
                        &nbsp;
                    </td>
                    <td align="center" class="style5" style="font-size: 14px; width: 158px;" rowspan="4">
                        <asp:Button ID="btnPost" runat="server" Text="Post" Width="120px" Height="60px" CssClass="btn"
                            OnClick="btnPost_Click" ValidationGroup="btnPost" />
                    </td>
                    <td align="left" class="style5" style="font-size: 14px; width: 8px;">
                        &nbsp;
                    </td>
                </tr>
                <tr valign="top">
                    <td align="right" style="font-size: 12px; width: 113px;">
                        &nbsp;
                    </td>
                    <td align="left" width="8">
                        &nbsp;
                    </td>
                    <td align="left" class="style1" style="width: 140px">
                        &nbsp;
                    </td>
                    <td align="right" class="style4" style="font-size: 12px; width: 89px;">
                        &nbsp;
                    </td>
                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                        &nbsp;
                    </td>
                    <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                        &nbsp;
                    </td>
                    <td align="right" class="style5" style="font-size: 12px; width: 74px;">
                        &nbsp;
                    </td>
                    <td align="left" class="style5" style="width: 8px;">
                        &nbsp;
                    </td>
                    <td align="left" class="style5" style="font-size: 14px; width: 20px;">
                        &nbsp;
                    </td>
                    <td align="left" class="style5" style="font-size: 14px; width: 8px;">
                        &nbsp;
                    </td>
                </tr>
                <tr valign="top">
                    <td align="right" style="font-size: 12px; width: 113px;" valign="middle">
                        Date
                    </td>
                    <td align="left" width="8">
                        :
                    </td>
                    <td align="left" class="style1">
                        <asp:TextBox ID="txtEntDate" runat="server" CssClass="textAlignCenter" ForeColor="Blue"
                            Width="160px" Enabled="False" AutoPostBack="True" 
                            ontextchanged="txtEntDate_TextChanged"></asp:TextBox>
                        <asp:Image ID="imgRcvDt" runat="server" ImageUrl="~/Image/calendar.png" />
                        <cc1:CalendarExtender ID="CalenderValDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            TargetControlID="txtEntDate" PopupButtonID="imgRcvDt">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right" class="style4" style="font-size: 12px; width: 89px;" valign="middle">
                        Amount
                    </td>
                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                        :
                    </td>
                    <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                        <asp:TextBox ID="txtTranAmt" runat="server" CssClass="textAlignCenter" Width="128px"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="txtTranAmt_FilteredTextBoxExtender" runat="server"
                            Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtTranAmt"
                            ValidChars=".">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                    <td align="right" class="style5" style="font-size: 12px; width: 74px;">
                        &nbsp;
                    </td>
                    <td align="left" class="style5" style="width: 8px;">
                        &nbsp;
                    </td>
                    <td align="left" class="style5" style="font-size: 14px; width: 20px;">
                        &nbsp;
                    </td>
                    <td align="left" class="style5" style="font-size: 14px; width: 8px;">
                        &nbsp;
                    </td>
                </tr>
                <tr valign="top">
                    <td align="right" style="font-size: 12px; width: 113px;">
                        &nbsp;
                    </td>
                    <td align="left" width="8">
                        &nbsp;
                    </td>
                    <td align="left" class="style1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtEntDate"
                            ErrorMessage="RequiredFieldValidator" Font-Size="8pt" ForeColor="Red" ValidationGroup="btnPost">Enter Date</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtEntDate"
                            ErrorMessage="Enter Valid Date" Font-Size="8pt" ForeColor="Red" Operator="DataTypeCheck"
                            Type="Date" ValidationGroup="btnPost"></asp:CompareValidator>
                    </td>
                    <td align="right" class="style4" style="font-size: 12px; width: 89px;">
                        &nbsp;
                    </td>
                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                        &nbsp;
                    </td>
                    <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="RequiredFieldValidator"
                            Font-Size="8pt" ForeColor="Red" ControlToValidate="txtTranAmt" ValidationGroup="btnPost">Enter Amount</asp:RequiredFieldValidator>
                        <br />
                        <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtTranAmt"
                            ErrorMessage="Enter Valid Amount" ForeColor="Red" Operator="DataTypeCheck" Type="Currency"
                            ValidationGroup="btnPost" Font-Size="8pt"></asp:CompareValidator>
                    </td>
                    <td align="right" class="style5" style="font-size: 12px; width: 74px;">
                        &nbsp;
                    </td>
                    <td align="left" class="style5" style="width: 8px;">
                        &nbsp;
                    </td>
                    <td align="left" class="style5" style="font-size: 14px; width: 20px;">
                        &nbsp;
                    </td>
                    <td align="left" class="style5" style="font-size: 14px; width: 8px;">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <span>
                <asp:Button ID="btnUpdateExisting" runat="server" OnClick="btnUpdateExisting_Click"
                    onkeypress="return clickButton(event,'btnSearch')" Text="Update Existing Credit Limit"
                    ValidationGroup="Save" Visible="False" />
            </span>
            <asp:Button ID="btnUpdtCrRealize" runat="server" OnClick="btnUpdtCrRealize_Click"
                Text="Update Credit Realization" Visible="False" />
            <asp:Button ID="btnUpdtOpnBal" runat="server" OnClick="btnUpdtOpnBal_Click" Text="Update Opening Balance"
                Visible="False" />
            <br />
        </div>
    </asp:Panel>
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
