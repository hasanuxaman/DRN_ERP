<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmTranPayRcv.aspx.cs" Inherits="DRN_WEB_ERP.Module.Accounts.Forms.frmTranPayRcv" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Transport Payment / Receive Entry</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnSearch">
                <div align="center" style="background-color: #86AEAE">
                    <br />
                    <span>Search Saved Reference No:</span>
                    <asp:TextBox ID="txtSearch" runat="server" Width="550px" CssClass="search textAlignCenter"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                        CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                        DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchJvList"
                        ServicePath="~/Module/Accounts/Forms/wsAutoComAcc.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                        TargetControlID="txtSearch">
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
                    <asp:Panel ID="pnlDet" runat="server">
                        <div>
                            <table style="width: 100%; background-color: #CC99FF;">
                                <tr>
                                    <td style="width: 352px">
                                        &nbsp;
                                    </td>
                                    <td align="center" style="width: 270px">
                                        <asp:RadioButtonList ID="optJvType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                            OnSelectedIndexChanged="optJvType_SelectedIndexChanged" Font-Bold="True" Font-Size="16pt">
                                            <asp:ListItem Value="1" Selected="True">Expences</asp:ListItem>
                                            <asp:ListItem Value="2">Income</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div align="center" style="background-color: #9999FF">
                            <br />
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; font-size: 12px;">
                                <tr valign="top">
                                    <td align="right" style="font-size: 12px; width: 143px;">
                                        Ref. No
                                    </td>
                                    <td align="left" width="8">
                                        :
                                    </td>
                                    <td align="left" class="style1" style="width: 140px">
                                        <asp:TextBox ID="txtTranRefNo" runat="server" CssClass="textAlignCenter" Enabled="False"
                                            Font-Bold="True" ForeColor="#FF9900" Width="180px"></asp:TextBox>
                                    </td>
                                    <td align="right" class="style2" style="font-size: 12px; width: 57px;">
                                        Date
                                    </td>
                                    <td align="left" width="8">
                                        :
                                    </td>
                                    <td align="left" class="style3" style="width: 165px">
                                        <asp:TextBox ID="txtTranDate" runat="server" CssClass="textAlignCenter" ForeColor="Blue"
                                            Width="120px"></asp:TextBox>
                                        <asp:Image ID="imgTranDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                        <cc1:CalendarExtender ID="CalenderTranDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            PopupButtonID="imgTranDt" TargetControlID="txtTranDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td align="right" class="style4" style="font-size: 12px; width: 89px;">
                                        Truck No
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                                        :
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 171px;">
                                        <asp:DropDownList ID="cboTruckNo" runat="server" Width="130px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="cboTruckNo"
                                            ErrorMessage="Select Truck No" Font-Size="8pt" ForeColor="Red" InitialValue="0"
                                            ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right" class="style5" style="font-size: 12px; width: 74px;">
                                        Narration
                                    </td>
                                    <td align="left" class="style5" style="width: 8px;">
                                        :
                                    </td>
                                    <td align="left" class="style5" rowspan="4" style="font-size: 14px;" valign="top">
                                        <asp:TextBox ID="txtNarration" runat="server" Height="54px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td align="left" class="style5" style="font-size: 14px; width: 20px;">
                                        &nbsp;
                                    </td>
                                    <td align="center" rowspan="6" style="font-size: 14px; width: 158px;">
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn" Height="40px" OnClick="btnAdd_Click"
                                            Text="Add" ValidationGroup="btnAdd" Width="100px" />
                                        <asp:Button ID="btnClearAll" runat="server" CssClass="btn" Height="40px" OnClick="btnClearAll_Click"
                                            Text="Clear All" Width="100px" />
                                    </td>
                                    <td align="left" class="style5" style="font-size: 14px; width: 8px;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td align="right" style="font-size: 12px; width: 143px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" width="8">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style1" style="width: 140px">
                                        &nbsp;
                                    </td>
                                    <td align="right" class="style2" style="font-size: 12px; width: 57px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" width="8">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style3" style="width: 165px">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtTranDate"
                                            ErrorMessage="Enter Date" Font-Size="8pt" ForeColor="Red" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtTranDate"
                                            ErrorMessage="Invalid Date" Font-Size="8pt" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"
                                            ValidationGroup="btnAdd">*</asp:CustomValidator>
                                    </td>
                                    <td align="right" class="style4" style="font-size: 12px; width: 89px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 171px;">
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
                                    <td align="right" style="font-size: 12px; width: 143px;">
                                        Party Name
                                    </td>
                                    <td align="left" width="8">
                                        :
                                    </td>
                                    <td align="left" class="style1" colspan="4">
                                        <asp:TextBox ID="txtGlParty" runat="server" CssClass="search" Font-Bold="False" Width="364px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" BehaviorID="AutoCompleteSrchAccParty"
                                            CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                            ServiceMethod="GetSrchCustomer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtGlParty">
                                        </cc1:AutoCompleteExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtGlParty"
                                            ErrorMessage="Enter Party Name" Font-Size="8pt" ForeColor="Red" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtGlParty"
                                            ErrorMessage="Invalid Party Name" Font-Size="8pt" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"
                                            ValidationGroup="btnAdd">*</asp:CustomValidator>
                                    </td>
                                    <td align="right" class="style4" style="font-size: 12px; width: 89px;">
                                        Km
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                                        :
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 171px;">
                                        <asp:TextBox ID="txtKm" runat="server" CssClass="textAlignCenter" Font-Bold="True"
                                            ForeColor="#FF9900" Width="130px"></asp:TextBox>
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
                                    <td align="right" style="font-size: 12px; width: 143px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" width="8">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style1" colspan="4">
                                    </td>
                                    <td align="right" class="style4" style="font-size: 12px; width: 89px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 171px;">
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
                                    <td align="right" style="font-size: 12px; width: 143px;">
                                        GL Code
                                    </td>
                                    <td align="left" width="8">
                                        :
                                    </td>
                                    <td align="left" class="style1" colspan="4">
                                        <asp:TextBox ID="txtGlAcc" runat="server" CssClass="search" Font-Bold="False" Width="364px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteSrchAcc"
                                            CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                            ServiceMethod="GetSrchCoa" ServicePath="~/Module/Accounts/Forms/wsAutoComAcc.asmx"
                                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtGlAcc">
                                        </cc1:AutoCompleteExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtGlAcc"
                                            ErrorMessage="Enter GL Account" Font-Size="8pt" ForeColor="Red" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtGlAcc"
                                            ErrorMessage="Invalid GL Account" Font-Size="8pt" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"
                                            ValidationGroup="btnAdd">*</asp:CustomValidator>
                                    </td>
                                    <td align="right" class="style4" style="font-size: 12px; width: 89px;">
                                        Type
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                                        :
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 171px;">
                                        <asp:DropDownList ID="ddlTranType" runat="server" Width="130px">
                                            <asp:ListItem Value="D">Debit</asp:ListItem>
                                            <asp:ListItem Value="C">Credit</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" class="style5" style="font-size: 12px; width: 74px;">
                                        Amount
                                    </td>
                                    <td align="left" class="style5" style="width: 8px;">
                                        :
                                    </td>
                                    <td align="left" class="style5" style="font-size: 14px; width: 20px;">
                                        <asp:TextBox ID="txtTranAmt" runat="server" CssClass="textAlignCenter" Width="150px"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtTranAmt_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtTranAmt"
                                            ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtTranAmt"
                                            ErrorMessage="Enter Amount" Font-Size="8pt" ForeColor="Red" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtTranAmt"
                                            ErrorMessage="Enter Valid Amount" Font-Size="8pt" ForeColor="Red" Operator="DataTypeCheck"
                                            Type="Currency" ValidationGroup="btnAdd">*</asp:CompareValidator>
                                    </td>
                                    <td align="left" class="style5" style="font-size: 14px; width: 8px;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td align="right" style="font-size: 12px; width: 143px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" width="8">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style1" colspan="4">
                                    </td>
                                    <td align="right" class="style4" style="font-size: 12px; width: 89px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 171px;">
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
                            <div style="background-color: #808080">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="9pt" ForeColor="Red"
                                    ValidationGroup="btnAdd" Width="250px" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlVoucherDet" runat="server">
        <div align="center" style="border: 1px solid #CCCCCC; background-color: #009999">
            <br />
            <div>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" bgcolor="#3366FF">
                    <tr>
                        <td align="left" style="width: 33%;">
                            <asp:Label ID="Label4" runat="server" BorderStyle="None" ForeColor="White" Text="Debit Amount: "></asp:Label>
                            <asp:Label ID="lblTotDrAmt" runat="server" BorderStyle="None" ForeColor="White" Text="0.00"></asp:Label>
                        </td>
                        <td align="center" style="width: 33px">
                            <asp:Label ID="Label6" runat="server" BorderStyle="None" ForeColor="White" Text="Credit Amount: "></asp:Label>
                            <asp:Label ID="lblTotCrAmt" runat="server" BorderStyle="None" ForeColor="White" Text="0.00"></asp:Label>
                        </td>
                        <td align="right" style="width: 33px">
                            <asp:Label ID="Label8" runat="server" BorderStyle="None" ForeColor="White" Text="Balance: "></asp:Label>
                            <asp:Label ID="lblDrCrBal" runat="server" BorderStyle="None" ForeColor="White" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:GridView ID="gvVoucherDet" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="9pt"
                PageSize="25" OnRowDeleting="gvVoucherDet_RowDeleting">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:TemplateField HeaderText="SL#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GL Code">
                        <ItemTemplate>
                            <asp:Label ID="lblCoaCode" runat="server" Text='<%# Bind("COA_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="COA_NAME" HeaderText="GL Name">
                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label ID="lblTrnDate" runat="server" Text='<%# Bind("TRAN_DATE", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Narration">
                        <ItemTemplate>
                            <asp:Label ID="lblNarration" runat="server" Text='<%# Bind("TRAN_NARRATION") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="450px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Debit">
                        <ItemTemplate>
                            <asp:Label ID="lblDr" runat="server" Text='<%# Bind("TRAN_DEBIT", "{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit">
                        <ItemTemplate>
                            <asp:Label ID="lblCr" runat="server" Text='<%# Bind("TRAN_CREDIT", "{0:n}") %>'></asp:Label>
                            <asp:HiddenField ID="hfHdrFlag" runat="server" Value='<%# Bind("HDR_FLAG") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" DeleteText="Remove">
                        <ItemStyle ForeColor="Red" />
                    </asp:CommandField>
                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                <SortedDescendingHeaderStyle BackColor="#3E3277" />
            </asp:GridView>
            <br />
            <asp:Button ID="btnHold" runat="server" CssClass="btn" Height="60px" OnClick="btnHold_Click"
                Text="Hold" Width="100px" Visible="False" />
            <asp:Button ID="btnPost" runat="server" CssClass="btn" Height="60px" OnClick="btnPost_Click"
                Text="Post" Width="100px" Visible="False" />
            <br />
            <asp:HiddenField ID="hfEditStat" runat="server" Value="N" />
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
        CancelControlID="btnMsgOk" DropShadow="true" OkControlID="btnMsgOk" PopupControlID="pnlMsg"
        TargetControlID="hfHidden">
    </cc1:ModalPopupExtender>
</asp:Content>
