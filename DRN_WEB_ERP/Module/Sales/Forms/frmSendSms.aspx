<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmSendSms.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSendSms" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Send SMS</div>
    <div align="center">
        <div>
        <table style="width: 100%;">
                <tr>
                    <td style="width: 230px">
                        &nbsp;
                    </td>
                    <td style="width: 107px" align="left">
                        &nbsp;
                    </td>
                    <td width="2">
                        &nbsp;
                    </td>
                    <td style="width: 232px" align="left">
                        &nbsp;
                    </td>
                    <td style="width: 94px">
                        &nbsp;</td>
                    <td style="width: 107px" align="left">
                        &nbsp;</td>
                    <td width="2">
                        &nbsp;</td>
                    <td style="width: 149px" align="left">
                        &nbsp;</td>
                    <td style="width: 149px">
                        &nbsp;
                    </td>
                    <td style="width: 156px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 230px">
                        &nbsp;
                    </td>
                    <td style="width: 107px" align="left">
                        DSM Name
                    </td>
                    <td width="2">
                        :
                    </td>
                    <td style="width: 232px" align="left">
                        <asp:DropDownList ID="cboDsm" runat="server" Width="230px">
                </asp:DropDownList>
                    </td>
                    <td align="center" style="width: 94px">
                        &nbsp;</td>
                    <td align="left" style="width: 107px">
                        As On Date</td>
                    <td align="center" width="2">
                        :</td>
                    <td align="left" style="width: 149px">
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDtFromCalender" runat="server" Enabled="True" 
                            Format="dd/MM/yyyy" PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                        </cc1:CalendarExtender>
                        <asp:Image ID="imgFromDate" runat="server" CssClass="inline" 
                            ImageUrl="~/Image/calendar.png" />
                    </td>
                    <td align="center" style="width: 149px">
                        <asp:Button ID="btnSendSmsDSM" runat="server" OnClick="btnSendSmsDSM_Click"
                    Text="Send SMS" />
                    </td>
                    <td style="width: 156px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 230px">
                        &nbsp;
                    </td>
                    <td style="width: 107px" align="left">
                        &nbsp;
                    </td>
                    <td width="2">
                        &nbsp;
                    </td>
                    <td style="width: 232px" align="left">
                        <asp:ListBox ID="ListBox1" runat="server" Height="120px" 
                            SelectionMode="Multiple">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                        </asp:ListBox>
                    </td>
                    <td style="width: 94px">
                        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                            Text="&gt;&gt;ADD" />
                            <br />
                            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
                            Text="&gt;&gt;Remove" />
                        </td>
                    <td style="width: 107px" align="left">
                        <asp:ListBox ID="ListBox2" runat="server"></asp:ListBox>
                    </td>
                    <td width="2">
                        &nbsp;</td>
                    <td style="width: 149px" align="left">
                        &nbsp;</td>
                    <td style="width: 149px">
                        &nbsp;
                    </td>
                    <td style="width: 156px">
                        <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
                            Text="SSL Bangla SMS" />
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table style="width: 100%;">
                <tr>
                    <td style="width: 284px">
                        &nbsp;
                    </td>
                    <td style="width: 73px">
                        &nbsp;
                    </td>
                    <td width="2">
                        &nbsp;
                    </td>
                    <td style="width: 232px">
                        &nbsp;
                    </td>
                    <td style="width: 149px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 284px">
                        &nbsp;
                    </td>
                    <td style="width: 73px">
                        SP Name
                    </td>
                    <td width="2">
                        :
                    </td>
                    <td style="width: 232px">
                        <asp:DropDownList ID="cboSp" runat="server" Width="230px">
                        </asp:DropDownList>
                    </td>
                    <td align="center" style="width: 149px">
                        <asp:Button ID="btnSendSmsSP" runat="server" OnClick="btnSendSmsSP_Click" Text="Send" />
                    </td>
                    <td>
                        &nbsp;
                        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
                            Text="GP SMS Test" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 284px">
                        &nbsp;
                    </td>
                    <td style="width: 73px">
                        &nbsp;
                    </td>
                    <td width="2">
                        &nbsp;
                    </td>
                    <td style="width: 232px">
                        &nbsp;
                    </td>
                    <td style="width: 149px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
