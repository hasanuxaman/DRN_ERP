<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmSalesOrderRptMpo.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesOrderRptMpo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Sales Report</div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel3" runat="server">
                <div align="center">
                    <table style="width: 100%; font-size: 12px; background-color: #CCFFFF;">
                        <tr>
                            <td style="width: 21px">
                                &nbsp;
                            </td>
                            <td align="center" colspan="9">
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
                        <tr>
                            <td style="width: 21px">
                                &nbsp;
                            </td>
                            <td align="center" colspan="9">
                                <asp:RadioButtonList ID="optRpt" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">Sales Order (S/O)</asp:ListItem>
                                    <asp:ListItem Value="2">Pending D/O</asp:ListItem>
                                    <asp:ListItem Value="3">D/O Created</asp:ListItem>
                                    <asp:ListItem Value="4">Pending Delivery</asp:ListItem>
                                    <asp:ListItem Value="7">Delivery Challan</asp:ListItem>
                                    <asp:ListItem Value="5">Collection</asp:ListItem>
                                    <asp:ListItem Value="6">Customer Ledger</asp:ListItem>
                                    <asp:ListItem Value="8">Customer List</asp:ListItem>
                                </asp:RadioButtonList>
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
                            <td style="width: 157px">
                                &nbsp;
                            </td>
                            <td style="width: 201px">
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
                                <asp:TextBox ID="txtFromDateSo" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDtFromCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    PopupButtonID="imgFromDate" TargetControlID="txtFromDateSo">
                                </cc1:CalendarExtender>
                                <asp:Image ID="imgFromDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                            </td>
                            <td align="right" style="width: 97px">
                                Date To
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 157px">
                                <asp:TextBox ID="txtToDateSo" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    PopupButtonID="imgToDate" TargetControlID="txtToDateSo">
                                </cc1:CalendarExtender>
                                <asp:Image ID="imgToDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png"
                                    Width="16px" />
                            </td>
                            <td align="right" style="width: 201px">
                                Search Customer
                            </td>
                            <td style="width: 6px">
                                :
                            </td>
                            <td style="width: 136px">
                                <asp:TextBox ID="txtSearch" runat="server" Width="320px" CssClass="search textAlignCenter"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                                    CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                    DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCustomerBySalesTree"
                                    ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                    TargetControlID="txtSearch">
                                </cc1:AutoCompleteExtender>
                            </td>
                            <td style="width: 15px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
                                <asp:Button ID="btnShowSoRpt" runat="server" OnClick="btnShowSoRpt_Click" Text="Show"
                                    Width="100px" ValidationGroup="btnShowSo" />
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFromDateSo"
                                    Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShowSo"></asp:RequiredFieldValidator>
                                <br />
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFromDateSo"
                                    Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                    Type="Date" ValidationGroup="btnShowSo"></asp:CompareValidator>
                            </td>
                            <td style="width: 97px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 157px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDateSo"
                                    Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShowSo"></asp:RequiredFieldValidator>
                                <br />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtToDateSo"
                                    Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                    Type="Date" ValidationGroup="btnShowSo"></asp:CompareValidator>
                            </td>
                            <td style="width: 201px">
                                &nbsp;
                            </td>
                            <td style="width: 6px">
                                &nbsp;
                            </td>
                            <td style="width: 136px" align="left">
                                <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtSearch"
                                    ErrorMessage="Enter Valid Customer" ForeColor="Red" OnServerValidate="CustomValidator2_ServerValidate"
                                    ValidationGroup="btnShowSo"></asp:CustomValidator>
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
                            <td style="width: 157px">
                                &nbsp;
                            </td>
                            <td style="width: 201px">
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
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnShowSoRpt" EventName="Click" />
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
