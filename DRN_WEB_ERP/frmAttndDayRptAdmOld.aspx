<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmAttndDayRptAdmOld.aspx.cs" Inherits="DRN_WEB_ERP.frmAttndDayRptAdmOld" %>

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
                    <td style="width: 74px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 118px">
                        &nbsp;
                    </td>
                    <td style="width: 94px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 130px">
                        &nbsp;
                    </td>
                    <td style="width: 68px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 151px">
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
                        Unit
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
                        <asp:DropDownList ID="cboDept" runat="server" Width="180px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 94px" align="right">
                        From Date
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td style="width: 130px">
                        <asp:TextBox ID="txtFromDate" runat="server" Width="90px" CssClass="textAlignCenter"></asp:TextBox>
                        <asp:Image ID="imgFromDt" runat="server" ImageUrl="~/Image/calendar.png" CssClass="inline" />
                        <cc1:CalendarExtender ID="CalendarExtender_txtFromDt" runat="server" Enabled="True"
                            Format="dd/MM/yyyy" PopupButtonID="imgFromDt" TargetControlID="txtFromDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td style="width: 68px" align="right">
                        To Date
                    </td>
                    <td>
                        :&nbsp;
                    </td>
                    <td style="width: 151px">
                        <asp:TextBox ID="txtToDate" runat="server" Width="90px" CssClass="textAlignCenter"></asp:TextBox>
                        <asp:Image ID="imgToDate" runat="server" ImageUrl="~/Image/calendar.png" CssClass="inline" />
                        <cc1:CalendarExtender ID="CalendarExtender_txtToDate" runat="server" Enabled="True"
                            Format="dd/MM/yyyy" PopupButtonID="imgToDate" TargetControlID="txtToDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                    <td align="center">
                        <asp:Button ID="btnShowRpt" runat="server" Text="Show" ValidationGroup="AtndDate"
                            OnClick="btnShowRpt_Click" Width="100px" />
                    </td>
                    <td style="" align="center">
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
                    <td style="width: 94px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 130px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate"
                            Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="AtndDate"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtToDate"
                            Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                            Type="Date" ValidationGroup="AtndDate"></asp:CompareValidator>
                    </td>
                    <td style="width: 68px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 151px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtToDate"
                            Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="AtndDate"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtToDate"
                            Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                            Type="Date" ValidationGroup="AtndDate"></asp:CompareValidator>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="left">
                        <br />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnShowRpt" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="cboDept" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
        Width="100%" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="14pt" Height="100%">
        <LocalReport ReportPath="AttndDayRpt.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DRNConStr %>"
        SelectCommand="SELECT * FROM [View_Emp_Attnd]"></asp:SqlDataSource>
</asp:Content>
