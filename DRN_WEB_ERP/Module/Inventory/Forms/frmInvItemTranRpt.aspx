<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmInvItemTranRpt.aspx.cs" Inherits="DRN_WEB_ERP.Module.Inventory.Forms.frmInvItemTranRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Item Transaction Report</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #CCFFFF;">
                <br />
                <div>
                    <table border="1" cellspacing="0" rules="all" style="border-collapse: collapse;">
                        <tr style="background-color: #009933; font-weight: bold; color: #F7F7F7; font-size: small;
                            white-space: nowrap; height: 30px;">
                            <th align="center" scope="col" style="width: 106px;">
                                &nbsp;
                            </th>
                            <th align="center" scope="col">
                                Item Type
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cboItemType"
                                    Display="Dynamic" ErrorMessage="Select Item Type First" ForeColor="Red" InitialValue="0"
                                    ValidationGroup="btnShow">*</asp:RequiredFieldValidator>
                            </th>
                            <th align="center" scope="col">
                                Item Name
                            </th>
                            <th align="center" scope="col">
                                Store
                            </th>
                            <th align="center" scope="col" style="width: 150px;">
                                From Date
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFromDate"
                                    Display="Dynamic" ErrorMessage="Enter From Date First" ForeColor="Red" ValidationGroup="btnShow">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFromDate"
                                    Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                    Type="Date" ValidationGroup="btnShow">*</asp:CompareValidator>
                            </th>
                            <th align="center" scope="col" style="width: 150px;">
                                To Date
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                                    Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtToDate"
                                    Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                    Type="Date" ValidationGroup="btnShow">*</asp:CompareValidator>
                            </th>
                            <th align="center" scope="col" style="width: 150px;" width="70">
                                <asp:RadioButtonList ID="optRptType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">Details</asp:ListItem>
                                    <asp:ListItem Value="2">Summary</asp:ListItem>
                                </asp:RadioButtonList>
                            </th>
                        </tr>
                        <tr class="gridFooterRow" style="background-color: #86AEAE;">
                            <td align="center" bgcolor="#009933" style="color: #F7F7F7; font-size: small; font-weight: bold;
                                width: 106px;">
                                Filter By:
                            </td>
                            <td>
                                <asp:DropDownList ID="cboItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboItemType_SelectedIndexChanged"
                                    Width="180px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboItem" runat="server" Width="230px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboStore" runat="server" Width="230px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 149px">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="inline" Width="80px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDtFromCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                                </cc1:CalendarExtender>
                                <asp:Image ID="imgFromDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="inline" Width="80px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    PopupButtonID="imgToDate" TargetControlID="txtToDate">
                                </cc1:CalendarExtender>
                                <asp:Image ID="imgToDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png"
                                    Width="16px" />
                            </td>
                            <td align="center">
                                <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" Width="100px"
                                    ValidationGroup="btnShow" />
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="10pt" ForeColor="Red"
                        ValidationGroup="btnShow" />
                    <br />
                </div>
                <div>
                    <iframe id="acrobatPanel" src="frmShowIFrameRpt.aspx" width="98%" height="700px"
                        frameborder="0" style="border: 2px inset #C0C0C0"></iframe>
                </div>
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboItemType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnShow" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
