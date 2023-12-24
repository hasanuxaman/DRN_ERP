<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmPaymentRcvEdit.aspx.cs" Inherits="DRN_WEB_ERP.Module.Accounts.Forms.frmPaymentRcvEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Receive Edit</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #86AEAE">
                <div align="center">
                    <br />
                    From Date:
                    <asp:TextBox ID="txtFrmDate" runat="server" CssClass="textAlignCenter" ForeColor="Blue"
                        Width="120px"></asp:TextBox>
                    <asp:Image ID="imgFrmDt" runat="server" ImageUrl="~/Image/calendar.png" />
                    <cc1:CalendarExtender ID="CalenderTranDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        PopupButtonID="imgFrmDt" TargetControlID="txtFrmDate">
                    </cc1:CalendarExtender>
                    To Date:
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textAlignCenter" ForeColor="Blue"
                        Width="120px"></asp:TextBox>
                    <asp:Image ID="imgToDt" runat="server" ImageUrl="~/Image/calendar.png" />
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        PopupButtonID="imgToDt" TargetControlID="txtToDate">
                    </cc1:CalendarExtender>
                    <br />
                    <span>Search Customer:</span> <span>
                        <asp:TextBox ID="txtSearch" runat="server" Width="550px" CssClass="search textAlignCenter"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteSrchAcc"
                            CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                            ServiceMethod="GetSrchCoaByType" ServicePath="~/Module/Accounts/Forms/wsAutoComAcc.asmx"
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearch">
                        </cc1:AutoCompleteExtender>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="btnSearch"
                            OnClick="btnSearch_Click" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearch"
                            ErrorMessage="Enter Search Text" Font-Bold="False" ForeColor="Red" ValidationGroup="btnSearch"
                            Font-Size="10pt"></asp:RequiredFieldValidator>
                        <br />
                    </span>
                    <br />
                </div>
                <div style="background-color: #FFCCFF; width: 50%;" align="center">
                    <div align="center" style="background-color: #FF6600">
                        <table style="width: 100%;">
                            <tr>
                                <td align="right" style="width: 156px">
                                    Revice Type
                                </td>
                                <td width="2">
                                    :
                                </td>
                                <td align="left">
                                    <asp:RadioButtonList ID="optUpdtType" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="optUpdtType_SelectedIndexChanged" Enabled="False">
                                        <asp:ListItem Selected="True" Value="0">None</asp:ListItem>
                                        <asp:ListItem Value="1">Amount</asp:ListItem>
                                        <asp:ListItem Value="2">Date</asp:ListItem>
                                        <asp:ListItem Value="3">GL Code</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <table style="width: 100%;">
                        <tr>
                            <td align="left" style="width: 189px; font-size: 12px;">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 369px; font-size: 12px;">
                                GL Code
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" style="width: 214px">
                                <asp:TextBox ID="txtGlCode" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 214px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 189px; font-size: 12px;">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 369px; font-size: 12px;">
                                Tran. Ref. No
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" style="width: 214px">
                                <asp:TextBox ID="txtTranRefNo" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 214px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 189px; font-size: 12px;">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 369px; font-size: 12px;">
                                Amount
                            </td>
                            <td align="left" width="2">
                                :
                            </td>
                            <td align="left" style="width: 214px">
                                <asp:TextBox ID="txtNewAmt" runat="server" Enabled="false"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtNewAmt_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtNewAmt"
                                    ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewAmt"
                                    ErrorMessage="Enter Amount" Font-Bold="False" ForeColor="Red" ValidationGroup="btnUpdate"
                                    Font-Size="8pt"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" style="width: 214px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 189px; font-size: 12px;">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 369px; font-size: 12px;">
                                Type
                            </td>
                            <td align="left" width="2">
                            </td>
                            <td align="left" style="width: 214px">
                                <asp:TextBox ID="txtTrnDrCrType" runat="server" CssClass="textAlignCenter" Enabled="False"
                                    Font-Bold="True"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 214px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 189px; font-size: 12px;">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 369px; font-size: 12px;">
                                Date
                            </td>
                            <td align="left" width="2">
                                :
                            </td>
                            <td align="left" style="width: 214px">
                                <asp:TextBox ID="txtNewDate" runat="server" CssClass="textAlignCenter" ForeColor="Blue"
                                    Width="120px" Enabled="false"></asp:TextBox>
                                <asp:Image ID="imgNewDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                <cc1:CalendarExtender ID="txtNewDate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd/MM/yyyy" PopupButtonID="imgNewDt" TargetControlID="txtNewDate">
                                </cc1:CalendarExtender>
                                <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtNewDate"
                                    ErrorMessage="Enter Valid Date" Font-Size="8pt" ForeColor="Red" Operator="DataTypeCheck"
                                    Type="Date" ValidationGroup="btnUpdate"></asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNewDate"
                                    ErrorMessage="Enter Date" Font-Bold="False" ForeColor="Red" ValidationGroup="btnUpdate"
                                    Font-Size="8pt"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" style="width: 214px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 189px; font-size: 12px;">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 369px; font-size: 12px;">
                                GL Code
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" style="width: 214px">
                                <asp:TextBox ID="txtNewCust" runat="server" CssClass="search textAlignCenter" Width="350px"
                                    Enabled="false"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" BehaviorID="AutoCompleteSrchGlAcc"
                                    CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                    ServiceMethod="GetSrchCoa" ServicePath="~/Module/Accounts/Forms/wsAutoComAcc.asmx"
                                    ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtNewCust">
                                </cc1:AutoCompleteExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNewCust"
                                    ErrorMessage="Enter GL Code" Font-Bold="False" ForeColor="Red" ValidationGroup="btnUpdate"
                                    Font-Size="8pt"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" style="width: 214px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 189px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 369px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 214px">
                                <asp:Button ID="btnUpdt" runat="server" Enabled="False" OnClick="btnUpdt_Click" Text="Update"
                                    Width="120px" ValidationGroup="btnUpdate" />
                            </td>
                            <td align="left" style="width: 214px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 189px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 369px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 214px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 214px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
                <div>
                    <br />
                    <asp:GridView ID="gvPayDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvPayDet_RowDataBound" OnSelectedIndexChanged="gvPayDet_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                            <asp:BoundField DataField="Trn_Line_No" HeaderText="Ln#"></asp:BoundField>
                            <asp:BoundField DataField="Trn_Ac_Desc" HeaderText="GL Description">
                                <ItemStyle Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Trn_Entry_Date" DataFormatString="{0:d}" HeaderText="Entry Date">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Trn_Narration" HeaderText="Narration">
                                <ItemStyle Width="300px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrnDrCrType" runat="server" Text='<%# Bind("Trn_Trn_type") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Trn_Amount" HeaderText="Amount" DataFormatString="{0:n}">
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <br />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="optUpdtType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvPayDet" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnUpdt" EventName="Click" />
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
