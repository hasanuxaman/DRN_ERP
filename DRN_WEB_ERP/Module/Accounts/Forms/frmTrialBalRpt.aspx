<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmTrialBalRpt.aspx.cs" Inherits="DRN_WEB_ERP.Module.Accounts.Forms.frmTrialBalRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Trial Balance</div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center">
                <table style="width: 100%; font-size: 12px; background-color: #CCFFFF;">
                    <tr>
                        <td style="width: 78px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 138px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td style="width: 141px" align="left">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 154px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 146px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 105px" align="right">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left">
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
                        <td style="width: 78px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 138px">
                            View Option
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td style="width: 141px" align="left">
                            <asp:RadioButton ID="optGrpSum" runat="server" Checked="True" GroupName="ViewGrp"
                                Text="Group Summary" />
                        </td>
                        <td align="right" style="width: 154px">
                            <asp:RadioButton ID="optGrpDet" runat="server" GroupName="ViewGrp" Text="Group Details" />
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 146px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 105px" align="right">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left">
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
                        <td style="width: 78px">
                            &nbsp;</td>
                        <td align="right" style="width: 138px">
                            First Group</td>
                        <td width="2">
                            :</td>
                        <td align="left" style="width: 141px">
                            <asp:DropDownList ID="ddlFirstGrp" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlFirstGrp_SelectedIndexChanged" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width: 154px">
                            Second Group</td>
                        <td style="width: 3px">
                            :</td>
                        <td align="left" style="width: 146px">
                            <asp:DropDownList ID="ddlSecondGrp" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlSecondGrp_SelectedIndexChanged" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width: 105px">
                            Third Group</td>
                        <td width="2">
                            :</td>
                        <td align="left">
                            <asp:DropDownList ID="ddlThirdGrp" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 99px">
                            &nbsp;</td>
                        <td style="width: 98px">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 78px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 138px">
                            Date From
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td style="width: 141px" align="left">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtDtFromCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                            <asp:Image ID="imgFromDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                        </td>
                        <td align="right" style="width: 154px">
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
                        <td align="right" style="width: 105px">
                            &nbsp;
                        </td>
                        <td width="2">
                        </td>
                        <td align="left">
                            <asp:Button ID="btnShowRpt" runat="server" OnClick="btnShowRpt_Click" 
                                Text="Show" Width="100px" />
                        </td>
                        <td style="width: 99px">
                            &nbsp;</td>
                        <td style="width: 98px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 78px">
                            &nbsp;
                        </td>
                        <td style="width: 138px">
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td style="width: 141px" align="left">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFromDate"
                                Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFromDate"
                                Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                Type="Date" ValidationGroup="btnShow"></asp:CompareValidator>
                        </td>
                        <td style="width: 154px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 146px" align="left">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                                Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtToDate"
                                Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                Type="Date" ValidationGroup="btnShow"></asp:CompareValidator>
                        </td>
                        <td style="width: 105px" align="right">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left">
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
                        <td style="width: 78px">
                            &nbsp;
                        </td>
                        <td style="width: 138px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td style="width: 141px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 154px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 146px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 105px" align="right">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left">
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
