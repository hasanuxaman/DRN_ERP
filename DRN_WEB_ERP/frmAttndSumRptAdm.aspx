<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmAttndSumRptAdm.aspx.cs" Inherits="DRN_WEB_ERP.frmAttndSumRptAdm" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Attendance Report</div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%; font-size: 12px; background-color: #CCFFFF">
                <tr>
                    <td style="width: 53px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 146px">
                        &nbsp;
                    </td>
                    <td style="width: 74px" align="right">
                        Date From
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td style="width: 118px">
                        <asp:TextBox ID="txtFromDate" runat="server" Width="100px" CssClass="inline"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFromDateCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            TargetControlID="txtFromDate" PopupButtonID="imgFromDate">
                        </cc1:CalendarExtender>
                        <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Image/calendar.png" CssClass="inline" />
                    </td>
                    <td style="width: 61px" align="right">
                        Date To
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td style="width: 118px">
                        <asp:TextBox ID="txtToDate" runat="server" Width="100px" CssClass="inline"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtToDateCalendar" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            TargetControlID="txtToDate" PopupButtonID="imgToDate">
                        </cc1:CalendarExtender>
                        <asp:Image ID="imgToDate" runat="server" ImageUrl="~/Image/calendar.png" CssClass="inline"
                            Width="16px" />
                    </td>
                    <td style="width: 31px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
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
                    <td style="width: 53px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 146px">
                        &nbsp;
                    </td>
                    <td style="width: 74px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 118px">
                        &nbsp;
                    </td>
                    <td style="width: 61px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 9px">
                        &nbsp;
                    </td>
                    <td style="width: 31px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
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
                    <td style="width: 53px" align="right">
                        Company
                    </td>
                    <td>
                        :
                    </td>
                    <td style="width: 146px">
                        <asp:DropDownList ID="cboLoc" runat="server" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="cboLoc_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 74px" align="right">
                        Department
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td style="width: 118px">
                        <asp:DropDownList ID="cboDept" runat="server" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="cboDept_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 61px" align="right">
                        Section
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td style="width: 9px">
                        <asp:DropDownList ID="cboSec" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="cboSec_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 31px" align="right">
                        Shift
                    </td>
                    <td>
                        :&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="cboShift" runat="server" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnShowRpt" runat="server" Text="Preview" OnClick="btnShowRpt_Click"
                            Width="100px" />
                    </td>
                    <td align="center" style="">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 53px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 146px">
                        &nbsp;
                    </td>
                    <td style="width: 74px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 118px">
                        &nbsp;
                    </td>
                    <td style="width: 61px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 9px">
                        &nbsp;
                    </td>
                    <td style="width: 31px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboLoc" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboDept" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboSec" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
        Width="100%" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="14pt" Height="100%">
        <LocalReport ReportPath="AttndSumRpt.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DRNConStr %>"
        SelectCommand="SELECT * FROM [View_Emp_Attnd_Sum]"></asp:SqlDataSource>
</asp:Content>
