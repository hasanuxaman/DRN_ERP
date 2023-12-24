<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmSalesDORpt.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesDORpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Delivery Order Report</div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
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
                        <td style="width: 141px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 97px">
                            &nbsp;
                        </td>
                        <td style="width: 3px" colspan="2">
                            &nbsp;
                        </td>
                        <td style="width: 146px">
                            &nbsp;
                        </td>
                        <td style="width: 105px" colspan="2">
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
                        <td align="center" colspan="5">
                            <asp:Label ID="Label1" runat="server" BackColor="#FF9999" Font-Bold="True" Text="---DO Status---"></asp:Label>
                            <asp:RadioButtonList ID="optRpt" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">Created</asp:ListItem>
                                <asp:ListItem Value="2">Pending</asp:ListItem>
                                <asp:ListItem Value="3">Executed (Challan)</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="center" colspan="3" style="width: 120px">
                            <asp:Label ID="Label2" runat="server" BackColor="#FF9999" Font-Bold="True" Text="---Option---"></asp:Label>
                            <asp:RadioButtonList ID="optRptOption" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="1">Details</asp:ListItem>
                                <asp:ListItem Value="2">Summary</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="center" colspan="3">
                            <asp:Label ID="Label3" runat="server" BackColor="#FF9999" Font-Bold="True" Text="---Filter By---"></asp:Label>
                            <asp:RadioButtonList ID="optRptFilter" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="optRptFilter_SelectedIndexChanged">
                                <asp:ListItem Value="1" Selected="True">Customer</asp:ListItem>
                                <asp:ListItem Value="2">DSM</asp:ListItem>
                                <asp:ListItem Value="3">SP</asp:ListItem>
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
                        <td style="width: 141px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 97px">
                            &nbsp;
                        </td>
                        <td style="width: 3px" colspan="2">
                            &nbsp;
                        </td>
                        <td style="width: 146px">
                            &nbsp;
                        </td>
                        <td style="width: 105px" colspan="2">
                        </td>
                        <td style="width: 6px">
                            :
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
                        <td style="width: 141px">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtDtFromCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                            <asp:Image ID="imgFromDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                        </td>
                        <td align="right" style="width: 97px">
                            Date To
                        </td>
                        <td style="width: 3px" colspan="2">
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
                        <td align="right" style="width: 105px" colspan="2">
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
                        <td style="width: 99px">
                            <asp:Button ID="btnShowRpt" runat="server" OnClick="btnShowRpt_Click" Text="Show"
                                Width="100px" />
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
                        <td style="width: 141px">
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
                        <td style="width: 3px" colspan="2">
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
                        <td style="width: 105px" colspan="2">
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
                        <td style="width: 141px">
                            &nbsp;
                        </td>
                        <td style="width: 97px">
                            &nbsp;
                        </td>
                        <td style="width: 3px" colspan="2">
                            &nbsp;
                        </td>
                        <td style="width: 146px">
                            &nbsp;
                        </td>
                        <td style="width: 105px" colspan="2">
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="optRptFilter" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnShowRpt" EventName="Click" />
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
