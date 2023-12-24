<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmSalesOrderRpt.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesOrderRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Sales Report</div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <hr />
            <asp:Panel ID="Panel3" runat="server">
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
                            <td colspan="6" align="center" style="font-weight: bold">
                                Sales Order Report
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
                            <td align="center" colspan="8">
                                <asp:RadioButtonList ID="optRpt" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                    OnSelectedIndexChanged="optRpt_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Selected="True">Customer</asp:ListItem>
                                    <asp:ListItem Value="2">Item Name</asp:ListItem>
                                    <asp:ListItem Value="5">DSM Wise</asp:ListItem>
                                    <asp:ListItem Value="3">Sales Person</asp:ListItem>
                                    <asp:ListItem Value="4">Pending D/O</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="optRptTypeSo" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">Details</asp:ListItem>
                                    <asp:ListItem Value="2">Summary</asp:ListItem>
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
            </asp:Panel>
            <hr />
            <asp:Panel ID="pnlDsmRpt" runat="server">
                <div align="center">
                    <table style="width: 100%; font-size: 12px; background-color: #CCFFFF;">
                        <tr>
                            <td style="width: 21px">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 169px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 167px">
                                &nbsp;
                            </td>
                            <td style="width: 166px">
                                &nbsp;
                            </td>
                            <td style="width: 6px">
                                &nbsp;
                            </td>
                            <td style="width: 136px; font-weight: bold;">
                                DSM Wise Delivery and Collection
                            </td>
                            <td style="width: 15px">
                                &nbsp;
                            </td>
                            <td style="width: 258px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
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
                            <td align="right" style="width: 169px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 168px">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 122px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 167px">
                                &nbsp;
                            </td>
                            <td style="width: 166px">
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
                            <td style="width: 258px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
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
                            <td align="right" style="width: 169px">
                                Date From
                            </td>
                            <td>
                                :
                            </td>
                            <td style="width: 168px">
                                <asp:TextBox ID="txtDsmFromDt" runat="server" CssClass="inline" Width="90px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    PopupButtonID="Image1" TargetControlID="txtDsmFromDt">
                                </cc1:CalendarExtender>
                                <asp:Image ID="Image1" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                            </td>
                            <td align="right" style="width: 122px">
                                Date To
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 167px">
                                <asp:TextBox ID="txtDsmToDt" runat="server" CssClass="inline" Width="90px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    PopupButtonID="Image2" TargetControlID="txtDsmToDt">
                                </cc1:CalendarExtender>
                                <asp:Image ID="Image2" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png"
                                    Width="16px" />
                            </td>
                            <td align="right" style="width: 166px">
                                DSM Name
                            </td>
                            <td style="width: 6px">
                                :
                            </td>
                            <td style="width: 136px">
                                <asp:DropDownList ID="cboDsm" runat="server" Width="230px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 15px">
                                &nbsp;
                            </td>
                            <td style="width: 258px">
                                <asp:RadioButtonList ID="optRptType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">Details</asp:ListItem>
                                    <asp:ListItem Value="2">Summary</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 99px">
                                <asp:Button ID="btnShowChlnRpt" runat="server" OnClick="btnShowChlnRpt_Click" Text="Delivery"
                                    ValidationGroup="ShowChln" Width="100px" />
                            </td>
                            <td style="width: 98px">
                                <asp:Button ID="btnShowCollectRpt" runat="server" OnClick="btnShowCollectRpt_Click"
                                    Text="Collection" ValidationGroup="ShowChln" Width="100px" />
                            </td>
                            <td style="width: 98px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 21px">
                                &nbsp;
                            </td>
                            <td style="width: 169px">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 168px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDsmFromDt"
                                    Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="ShowChln"></asp:RequiredFieldValidator>
                                <br />
                                <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtDsmFromDt"
                                    Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                    Type="Date" ValidationGroup="ShowChln"></asp:CompareValidator>
                            </td>
                            <td style="width: 122px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 167px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDsmToDt"
                                    Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="ShowChln"></asp:RequiredFieldValidator>
                                <br />
                                <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtDsmToDt"
                                    Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                    Type="Date" ValidationGroup="ShowChln"></asp:CompareValidator>
                            </td>
                            <td style="width: 166px">
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
                            <td style="width: 258px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
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
                            <td style="width: 169px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 168px">
                                &nbsp;
                            </td>
                            <td style="width: 122px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 167px">
                                &nbsp;
                            </td>
                            <td style="width: 166px">
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
                            <td style="width: 258px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
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
            </asp:Panel>
            <hr />
            <asp:Panel ID="Panel1" runat="server">
                <div align="center">
                    <table style="width: 100%; font-size: 12px; background-color: #CCFFFF;">
                        <tr>
                            <td style="width: 21px">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 169px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 167px">
                                &nbsp;
                            </td>
                            <td style="width: 166px">
                                &nbsp;
                            </td>
                            <td style="width: 6px">
                                &nbsp;
                            </td>
                            <td style="width: 136px; font-weight: bold;">
                                Sales Person Wise Delivery
                            </td>
                            <td style="width: 15px">
                                &nbsp;
                            </td>
                            <td style="width: 258px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
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
                            <td align="right" style="width: 169px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 168px">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 122px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 167px">
                                &nbsp;
                            </td>
                            <td style="width: 166px">
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
                            <td style="width: 258px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
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
                            <td align="right" style="width: 169px">
                                Date From
                            </td>
                            <td>
                                :
                            </td>
                            <td style="width: 168px">
                                <asp:TextBox ID="txtSpDtFrom" runat="server" CssClass="inline" Width="90px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender444" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    PopupButtonID="Image111" TargetControlID="txtSpDtFrom">
                                </cc1:CalendarExtender>
                                <asp:Image ID="Image111" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                            </td>
                            <td align="right" style="width: 122px">
                                Date To
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 167px">
                                <asp:TextBox ID="txtSpDtTo" runat="server" CssClass="inline" Width="90px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender555" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    PopupButtonID="Image222" TargetControlID="txtSpDtTo">
                                </cc1:CalendarExtender>
                                <asp:Image ID="Image222" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png"
                                    Width="16px" />
                            </td>
                            <td align="right" style="width: 166px">
                                SP Name
                            </td>
                            <td style="width: 6px">
                                :
                            </td>
                            <td style="width: 136px">
                                <asp:DropDownList ID="cboSp" runat="server" Width="230px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 15px">
                                &nbsp;
                            </td>
                            <td style="width: 258px">
                                <asp:RadioButtonList ID="optSpSumDet" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">Details</asp:ListItem>
                                    <asp:ListItem Value="2">Summary</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 99px">
                                <asp:Button ID="btnSp" runat="server" Text="Delivery" ValidationGroup="ShowSpChln"
                                    Width="100px" OnClick="btnSp_Click" />
                            </td>
                            <td style="width: 98px">
                                <asp:Button ID="Button2" runat="server" Text="Collection" ValidationGroup="ShowSpChln"
                                    Width="100px" Visible="False" />
                            </td>
                            <td style="width: 98px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 21px">
                                &nbsp;
                            </td>
                            <td style="width: 169px">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 168px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator111" runat="server" ControlToValidate="txtSpDtFrom"
                                    Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="ShowSpChln"></asp:RequiredFieldValidator>
                                <br />
                                <asp:CompareValidator ID="CompareValidator333" runat="server" ControlToValidate="txtSpDtFrom"
                                    Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                    Type="Date" ValidationGroup="ShowSpChln"></asp:CompareValidator>
                            </td>
                            <td style="width: 122px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 167px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator444" runat="server" ControlToValidate="txtSpDtTo"
                                    Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="ShowSpChln"></asp:RequiredFieldValidator>
                                <br />
                                <asp:CompareValidator ID="CompareValidator444" runat="server" ControlToValidate="txtSpDtTo"
                                    Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                    Type="Date" ValidationGroup="ShowSpChln"></asp:CompareValidator>
                            </td>
                            <td style="width: 166px">
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
                            <td style="width: 258px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
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
                            <td style="width: 169px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 168px">
                                &nbsp;
                            </td>
                            <td style="width: 122px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 167px">
                                &nbsp;
                            </td>
                            <td style="width: 166px">
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
                            <td style="width: 258px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
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
            </asp:Panel>
            <hr />
            <asp:Panel ID="Panel2" runat="server">
                <div align="center">
                    <table style="width: 100%; font-size: 12px; background-color: #CCFFFF;">
                        <tr>
                            <td style="width: 21px">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 169px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 189px">
                                &nbsp;
                            </td>
                            <td style="width: 113px">
                                &nbsp;
                            </td>
                            <td style="width: 6px">
                                &nbsp;
                            </td>
                            <td style="width: 136px; font-weight: bold;">
                                District Thana Wise Delivery
                            </td>
                            <td style="width: 15px">
                                &nbsp;
                            </td>
                            <td style="width: 318px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 21px">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 169px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 168px">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 122px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 189px">
                                &nbsp;
                            </td>
                            <td style="width: 113px">
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
                            <td style="width: 318px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 21px">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 169px">
                                Date From
                            </td>
                            <td>
                                :
                            </td>
                            <td style="width: 168px">
                                <asp:TextBox ID="txtDistDateFrm" runat="server" CssClass="inline" Width="90px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    PopupButtonID="Image1111" TargetControlID="txtDistDateFrm">
                                </cc1:CalendarExtender>
                                <asp:Image ID="Image1111" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                            </td>
                            <td align="right" style="width: 122px">
                                Date To
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 189px">
                                <asp:TextBox ID="txtDistDateTo" runat="server" CssClass="inline" Width="90px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    PopupButtonID="Image2222" TargetControlID="txtDistDateTo">
                                </cc1:CalendarExtender>
                                <asp:Image ID="Image2222" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png"
                                    Width="16px" />
                            </td>
                            <td align="right" style="width: 113px">
                                District
                            </td>
                            <td style="width: 6px">
                                :
                            </td>
                            <td style="width: 136px" align="left">
                                <asp:DropDownList ID="cboCustDist" runat="server" Width="230px" AutoPostBack="True"
                                    OnSelectedIndexChanged="cboCustDist_SelectedIndexChanged" ValidationGroup="ChkData">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 15px">
                                &nbsp;
                            </td>
                            <td style="width: 318px">
                                <asp:RadioButtonList ID="optDistThana" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="True" OnSelectedIndexChanged="optDistThana_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Selected="True">Details</asp:ListItem>
                                    <asp:ListItem Value="2">Summary</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 99px">
                                <asp:Button ID="btnShowDistRpt" runat="server" Text="Delivery" ValidationGroup="ShowSpChln"
                                    Width="100px" OnClick="btnShowDistRpt_Click" />
                            </td>
                            <td style="width: 99px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 21px">
                                &nbsp;
                            </td>
                            <td style="width: 169px">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 168px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDistDateFrm"
                                    Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="ShowSpChln"></asp:RequiredFieldValidator>
                                <br />
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtDistDateFrm"
                                    Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                    Type="Date" ValidationGroup="ShowSpChln"></asp:CompareValidator>
                            </td>
                            <td style="width: 122px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 189px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDistDateTo"
                                    Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="ShowSpChln"></asp:RequiredFieldValidator>
                                <br />
                                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtDistDateTo"
                                    Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                    Type="Date" ValidationGroup="ShowSpChln"></asp:CompareValidator>
                            </td>
                            <td style="width: 113px" align="right">
                                Thana
                            </td>
                            <td style="width: 6px">
                                :
                            </td>
                            <td style="width: 136px" align="left">
                                <asp:DropDownList ID="cboCustThana" runat="server" Width="230px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 15px">
                                &nbsp;
                            </td>
                            <td style="width: 318px" align="center">
                                By:<asp:RadioButtonList ID="optDistThanaSum" runat="server" RepeatDirection="Horizontal"
                                    Visible="False">
                                    <asp:ListItem Selected="True" Value="1">District</asp:ListItem>
                                    <asp:ListItem Value="2">Thana</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 99px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 21px">
                                &nbsp;
                            </td>
                            <td style="width: 169px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 168px">
                                &nbsp;
                            </td>
                            <td style="width: 122px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 189px">
                                &nbsp;
                            </td>
                            <td style="width: 113px">
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
                            <td style="width: 318px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
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
