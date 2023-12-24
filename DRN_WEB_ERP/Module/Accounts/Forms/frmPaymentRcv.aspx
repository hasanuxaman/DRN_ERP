<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmPaymentRcv.aspx.cs" Inherits="DRN_WEB_ERP.Module.Accounts.Forms.frmPaymentRcv" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Receive Entry</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnSearch">
                <div align="center" style="background-color: #86AEAE">
                    <br />
                    <span>Search Customer:</span> <span>
                        <asp:TextBox ID="txtSearch" runat="server" Width="550px" CssClass="search textAlignCenter"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCustomer"
                            ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
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
                        <asp:Label ID="Label1" runat="server" BorderStyle="None" ForeColor="White" Text="Credit Limit: "></asp:Label>
                        <asp:Label ID="lblCrLimit" runat="server" BorderStyle="None" ForeColor="White" Text="0.00"></asp:Label>
                    </td>
                    <td align="center" style="width: 33px">
                        <asp:Label ID="Label3" runat="server" BorderStyle="None" ForeColor="White" Text="Outstanding: "></asp:Label>
                        <asp:Label ID="lblCrLimitOutStand" runat="server" BorderStyle="None" ForeColor="White"
                            Text="0.00"></asp:Label>
                    </td>
                    <td align="right" style="width: 33px">
                        <asp:Label ID="Label2" runat="server" BorderStyle="None" ForeColor="White" Text="Available Credit Limit: "></asp:Label>
                        <asp:Label ID="txtCrLimitBal" runat="server" BorderStyle="None" ForeColor="White"
                            Text="0.00"></asp:Label>
                    </td>
                </tr>
            </table>
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
                        <asp:TextBox ID="txtRcvRefNo" runat="server" Enabled="False" Font-Bold="True" ForeColor="#FF9900"
                            Width="180px" CssClass="textAlignCenter"></asp:TextBox>
                    </td>
                    <td align="right" class="style2" style="font-size: 12px; width: 57px;">
                        Date
                    </td>
                    <td align="left" width="8">
                        :
                    </td>
                    <td align="left" class="style3" style="width: 165px">
                        <asp:TextBox ID="txtRcvDate" runat="server" CssClass="textAlignCenter" ForeColor="Blue"
                            Width="120px" AutoPostBack="True" OnTextChanged="txtRcvDate_TextChanged"></asp:TextBox>
                        <asp:Image ID="imgRcvDt" runat="server" ImageUrl="~/Image/calendar.png" />
                        <cc1:CalendarExtender ID="CalenderValDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            TargetControlID="txtRcvDate" PopupButtonID="imgRcvDt">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right" class="style4" style="font-size: 12px; width: 89px;">
                        Pay. Type
                    </td>
                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                        :
                    </td>
                    <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                        <asp:DropDownList ID="ddlRcvMode" runat="server" Width="130px">
                            <asp:ListItem>Cash</asp:ListItem>
                            <asp:ListItem>Cheque</asp:ListItem>
                            <asp:ListItem>DD</asp:ListItem>
                            <asp:ListItem>TT</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" class="style5" style="font-size: 12px; width: 74px;">
                        Ref. Doc
                    </td>
                    <td align="left" class="style5" style="width: 8px;">
                        :
                    </td>
                    <td align="left" class="style5" style="width: 8px;">
                        <asp:TextBox ID="txtDocRef" runat="server" Width="160px"></asp:TextBox>
                    </td>
                    <td align="left" class="style5" style="font-size: 14px;" valign="top" rowspan="4">
                        &nbsp;
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtRcvDate"
                            ErrorMessage="RequiredFieldValidator" Font-Size="8pt" ForeColor="Red" ValidationGroup="btnPost">Enter Date</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtRcvDate"
                            Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                            Type="Date" ValidationGroup="btnPost"></asp:CompareValidator>
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
                    <td align="left" class="style5" style="width: 8px;">
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
                        Debit Acc
                    </td>
                    <td align="left" width="8">
                        :
                    </td>
                    <td align="left" class="style1" colspan="4">
                        <asp:TextBox ID="txtDrAcc" runat="server" Font-Bold="False" Width="364px" CssClass="search"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteSrchAcc"
                            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCoaByPayRcv"
                            ServicePath="~/Module/Accounts/Forms/wsAutoComAcc.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                            TargetControlID="txtDrAcc">
                        </cc1:AutoCompleteExtender>
                    </td>
                    <td align="right" class="style4" style="font-size: 12px; width: 89px;">
                        Amount
                    </td>
                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                        :
                    </td>
                    <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                        <asp:TextBox ID="txtRcvAmt" runat="server" CssClass="textAlignCenter" Width="128px"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="txtRcvAmt_FilteredTextBoxExtender" runat="server"
                            Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtRcvAmt"
                            ValidChars=".">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                    <td align="right" class="style5" style="font-size: 12px; width: 74px;">
                        Narration
                    </td>
                    <td align="left" class="style5" style="width: 8px;">
                        :
                    </td>
                    <td align="left" class="style5" rowspan="2" style="width: 8px;">
                        <asp:TextBox ID="txtNarration" runat="server" Height="54px" TextMode="MultiLine"></asp:TextBox>
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDrAcc"
                            ErrorMessage="RequiredFieldValidator" Font-Size="8pt" ForeColor="Red" ValidationGroup="btnPost">Enter Debit Account</asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtDrAcc"
                            ErrorMessage="CustomValidator" Font-Size="8pt" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"
                            ValidationGroup="btnPost">Invalid Debit Account</asp:CustomValidator>
                    </td>
                    <td align="right" class="style4" style="font-size: 12px; width: 89px;">
                        &nbsp;
                    </td>
                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                        &nbsp;
                    </td>
                    <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtRcvAmt"
                            ErrorMessage="RequiredFieldValidator" Font-Size="8pt" ForeColor="Red" ValidationGroup="btnPost">Enter Amount</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtRcvAmt"
                            ErrorMessage="Enter Valid Amount" Font-Size="8pt" ForeColor="Red" Operator="DataTypeCheck"
                            Type="Currency" ValidationGroup="btnPost"></asp:CompareValidator>
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
            <br />
            <br />
            <asp:Panel ID="Panel1" runat="server">
                <span style="color: #FFFFFF"><strong><span style="background-color: #CC33FF">Last Payment
                    Details</span></strong></span><asp:GridView ID="gvLastPayDet" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvLastPayDet_RowDataBound"
                        OnSelectedIndexChanged="gvLastPayDet_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="Trn_Ac_Code" HeaderText="GL Code" Visible="False">
                                <ItemStyle Width="120px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Tran. Ref. No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfGlCode" runat="server" Value='<%# Bind("Trn_Ac_Code") %>' />
                                    <asp:Label ID="lblTrnRefNo" runat="server" Text='<%# Bind("Trn_Ref_No") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Trn_DATE" DataFormatString="{0:d}" HeaderText="Tran. Date">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Trn_Line_No" HeaderText="Ln#" />
                            <asp:BoundField DataField="Trn_Ac_Desc" HeaderText="GL Description">
                                <ItemStyle Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Trn_Entry_Date" DataFormatString="{0:d}" HeaderText="Entry Date">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Trn_Narration" HeaderText="Narration">
                                <ItemStyle Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Trn_Trn_type" HeaderText="Type">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Trn_Amount" HeaderText="Amount">
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                            </asp:BoundField>
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
            </asp:Panel>
            <br />
            <span style="color: #FFFFFF"><strong><span style="background-color: #CC6600">Today&#39;s
                Payment List</span></strong></span><asp:GridView ID="gvTodayPayDet" runat="server"
                    AutoGenerateColumns="False" CellPadding="4" Font-Size="8pt" ForeColor="#333333"
                    OnRowDataBound="gvTodayPayDet_RowDataBound" OnSelectedIndexChanged="gvTodayPayDet_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="Trn_Ac_Code" HeaderText="GL Code" Visible="False">
                            <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Tran. Ref. No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfGlCodeDay" runat="server" Value='<%# Bind("Trn_Ac_Code") %>' />
                                <asp:Label ID="lblTrnRefNoDay" runat="server" Text='<%# Bind("Trn_Ref_No") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Trn_DATE" DataFormatString="{0:d}" HeaderText="Tran. Date">
                            <ItemStyle Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Trn_Line_No" HeaderText="Ln#" />
                        <asp:BoundField DataField="Trn_Ac_Desc" HeaderText="GL Description">
                            <ItemStyle Width="300px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Trn_Entry_Date" DataFormatString="{0:d}" HeaderText="Entry Date">
                            <ItemStyle Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Trn_Narration" HeaderText="Narration">
                            <ItemStyle Width="300px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Trn_Trn_type" HeaderText="Type">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Trn_Amount" HeaderText="Amount">
                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                        </asp:BoundField>
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
            <asp:HiddenField ID="hfEditStatus111" runat="server" Value="N" />
            <asp:HiddenField ID="hfRefNo111" runat="server" Value="0" />
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
