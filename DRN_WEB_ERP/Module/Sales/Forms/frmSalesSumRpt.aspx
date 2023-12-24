<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmSalesSumRpt.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesSumRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Target Vs Sales Summary Report</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center">
                <table style="width: 100%; font-size: 12px; background-color: #CCFFFF;">
                    <tr>
                        <td style="width: 21px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 193px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 145px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 88px">
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
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 127px">
                            &nbsp;
                        </td>
                        <td style="width: 127px">
                            &nbsp;
                        </td>
                        <td style="width: 6px">
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
                        <td align="right" style="width: 193px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 145px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 88px">
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
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 127px">
                            &nbsp;
                        </td>
                        <td style="width: 127px">
                            &nbsp;
                        </td>
                        <td style="width: 6px">
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
                        <td align="right" style="width: 193px">
                            Date From
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 145px" align="left">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtDtFromCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                            <asp:Image ID="imgFromDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                        </td>
                        <td align="right" style="width: 88px">
                            Date To
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 146px" align="left">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgToDate" TargetControlID="txtToDate">
                            </cc1:CalendarExtender>
                            <asp:Image ID="imgToDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png"
                                Width="16px" />
                        </td>
                        <td align="right" style="width: 127px">
                            Sales Zone
                        </td>
                        <td align="right">
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="cboDsm" runat="server" Width="230px">
                            </asp:DropDownList>
                        </td>
                        <td align="center" style="width: 127px">
                            <asp:Button ID="btnShowRpt" runat="server" OnClick="btnShowRpt_Click" Text="DSM Wise"
                                ValidationGroup="btnShow" Width="100px" />
                        </td>
                        <td style="width: 6px">
                            &nbsp;
                        </td>
                        <td align="center">
                            <asp:Button ID="btnShowRptSp" runat="server" OnClick="btnShowRptSp_Click" Text="SP Wise"
                                ValidationGroup="btnShow" Width="100px" />
                        </td>
                        <td style="width: 98px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
                            &nbsp;
                        </td>
                        <td style="width: 193px">
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 145px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFromDate"
                                Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFromDate"
                                Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                Type="Date" ValidationGroup="btnShow"></asp:CompareValidator>
                        </td>
                        <td style="width: 88px">
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
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 127px">
                            &nbsp;
                        </td>
                        <td style="width: 127px">
                            &nbsp;
                        </td>
                        <td style="width: 6px">
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
                        <td style="width: 193px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 145px">
                            &nbsp;
                        </td>
                        <td style="width: 88px">
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
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 127px">
                            &nbsp;
                        </td>
                        <td style="width: 127px">
                            &nbsp;
                        </td>
                        <td style="width: 6px">
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
