<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmInvRptMrr.aspx.cs" Inherits="DRN_WEB_ERP.Module.Inventory.Forms.frmInvRptMrr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        MRR Report</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center">
                <div>
                    <table style="width: 100%; font-size: 12px; background-color: #CCFFFF;">
                        <tr>
                            <td style="width: 175px">
                                &nbsp;
                            </td>
                            <td style="width: 175px">
                                &nbsp;
                            </td>
                            <td style="width: 116px" width="220">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 138px" width="280">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 138px" width="200">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 235px">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 138px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 175px">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 175px">
                            </td>
                            <td align="right" style="width: 116px; background-color: #CCCCCC;" width="220">
                                <asp:RadioButtonList ID="optSrchType" runat="server" RepeatDirection="Horizontal"
                                    Width="201px" Style="margin-right: 0px" AutoPostBack="True" OnSelectedIndexChanged="optSrchType_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Selected="True">Party Wise</asp:ListItem>
                                    <asp:ListItem Value="2">Item Wise</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="left" style="width: 138px; background-color: #CCCCCC;" width="280">
                                <asp:RadioButtonList ID="optRptVal" runat="server" RepeatDirection="Horizontal" Width="201px">
                                    <asp:ListItem Value="1" Selected="True">Without Value</asp:ListItem>
                                    <asp:ListItem Value="2">With Value</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="left" style="width: 138px; background-color: #CCCCCC;" width="200">
                                <asp:RadioButtonList ID="optRptType" runat="server" RepeatDirection="Horizontal"
                                    Width="201px">
                                    <asp:ListItem Selected="True" Value="1">Details</asp:ListItem>
                                    <asp:ListItem Value="2">Summary</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="left" style="width: 235px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 138px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <table style="width: 100%; font-size: 12px; background-color: #CCFFFF;">
                    <tr>
                        <td style="width: 21px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 138px">
                            Date From
                        </td>
                        <td>
                            :
                        </td>
                        <td style="width: 168px">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtDtFromCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                            <asp:Image ID="imgFromDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                        </td>
                        <td align="right" style="width: 97px">
                            Date To
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 146px">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgToDate" TargetControlID="txtToDate">
                            </cc1:CalendarExtender>
                            <asp:Image ID="imgToDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png"
                                Width="16px" />
                        </td>
                        <td align="right" style="width: 127px">
                            Search Filter
                        </td>
                        <td style="width: 6px">
                            :
                        </td>
                        <td style="width: 136px">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="search textAlignCenter" Width="350px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetSrchSupplier" ServicePath="~/Module/Procurement/Forms/wsAutoComProc.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearch">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td style="width: 15px">
                            &nbsp;
                        </td>
                        <td style="width: 99px">
                            <asp:Button ID="btnShowRpt" runat="server" Text="Show" Width="100px" OnClick="btnShowRpt_Click" />
                        </td>
                        <td style="width: 98px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
                            &nbsp;
                        </td>
                        <td style="width: 138px">
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFromDate"
                                Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFromDate"
                                Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                Type="Date" ValidationGroup="btnShow"></asp:CompareValidator>
                        </td>
                        <td style="width: 97px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 146px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                                Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtToDate"
                                Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                Type="Date" ValidationGroup="btnShow"></asp:CompareValidator>
                        </td>
                        <td style="width: 127px">
                            &nbsp;
                        </td>
                        <td style="width: 6px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                        </td>
                        <td style="width: 15px">
                            &nbsp;
                        </td>
                        <td style="width: 99px">
                            &nbsp;
                        </td>
                        <td style="width: 98px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
                            &nbsp;
                        </td>
                        <td style="width: 138px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 97px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 146px">
                            &nbsp;
                        </td>
                        <td style="width: 127px">
                            &nbsp;
                        </td>
                        <td style="width: 6px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                            &nbsp;
                        </td>
                        <td style="width: 15px">
                            &nbsp;
                        </td>
                        <td style="width: 99px">
                            &nbsp;
                        </td>
                        <td style="width: 98px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
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
