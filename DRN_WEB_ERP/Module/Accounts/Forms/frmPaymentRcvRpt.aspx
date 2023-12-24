<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmPaymentRcvRpt.aspx.cs" Inherits="DRN_WEB_ERP.Module.Accounts.Forms.frmPaymentRcvRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Collection Report</div>
    <div align="center">
        <table style="width: 100%; font-size: 12px; background-color: #CCFFFF;">
            <tr>
                <td style="width: 21px">
                    &nbsp;
                </td>
                <td align="right" style="width: 138px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td style="width: 168px">
                    &nbsp;
                </td>
                <td align="right" style="width: 97px">
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
                <td style="width: 98px">
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
                <td align="right" style="width: 138px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td style="width: 168px">
                    &nbsp;
                </td>
                <td align="right" style="width: 97px">
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
                <td style="width: 98px">
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
                <td align="right" style="width: 138px">
                    Date From
                </td>
                <td>
                    :
                </td>
                <td style="width: 168px">
                    <asp:TextBox ID="txtFromDt" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        PopupButtonID="Image1" TargetControlID="txtFromDt">
                    </cc1:CalendarExtender>
                    <asp:Image ID="Image1" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                </td>
                <td align="right" style="width: 97px">
                    Date To
                </td>
                <td style="width: 3px">
                    :
                </td>
                <td style="width: 146px">
                    <asp:TextBox ID="txtToDt" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        PopupButtonID="Image2" TargetControlID="txtToDt">
                    </cc1:CalendarExtender>
                    <asp:Image ID="Image2" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png"
                        Width="16px" />
                </td>
                <td align="right" style="width: 127px">
                    Customer
                </td>
                <td style="width: 6px">
                    :
                </td>
                <td style="width: 136px">
                    <asp:TextBox ID="txtSearch" runat="server" Width="350px" CssClass="search textAlignCenter"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                        CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                        DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCustomer"
                        ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                        TargetControlID="txtSearch">
                    </cc1:AutoCompleteExtender>
                </td>
                <td style="width: 15px">
                    &nbsp;
                </td>
                <td style="width: 98px">
                    <asp:Button ID="btnShowCollectRpt" runat="server" OnClick="btnShowCollectRpt_Click"
                        Text="Collection" ValidationGroup="btnShow" Width="100px" />
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtFromDt"
                        Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" 
                        ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                    <br />
                    <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtFromDt"
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtToDt"
                        Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" 
                        ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                    <br />
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtToDt"
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
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtSearch"
                        ErrorMessage="Enter Valid Customer" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"
                        ValidationGroup="btnShow"></asp:CustomValidator>
                </td>
                <td style="width: 15px">
                    &nbsp;</td>
                <td style="width: 98px">
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
                <td style="width: 98px">
                    &nbsp;
                </td>
                <td style="width: 98px">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
