<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmAttndSumRpt.aspx.cs" Inherits="DRN_WEB_ERP.frmAttndSumRpt" %>

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
                    <td style="width: 25px">
                    </td>
                    <td>
                    </td>
                    <td style="width: 74px" align="right">
                    </td>
                    <td style="width: 3px">
                    </td>
                    <td style="width: 137px">
                    </td>
                    <td style="width: 61px" align="right">
                    </td>
                    <td style="width: 3px">
                    </td>
                    <td style="width: 134px">
                    </td>
                    <td style="width: 38px">
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25px; height: 33px;">
                        &nbsp;
                    </td>
                    <td style="height: 33px">
                        &nbsp;
                    </td>
                    <td align="right" style="width: 74px; height: 33px;">
                        Date From
                    </td>
                    <td style="width: 3px; height: 33px;">
                        :
                    </td>
                    <td style="width: 137px; height: 33px;">
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                        <asp:Image ID="imgFromDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                        <cc1:CalendarExtender ID="txtDtFromCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            TargetControlID="txtFromDate" PopupButtonID="imgFromDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right" style="width: 61px; height: 33px;">
                        Date To
                    </td>
                    <td style="width: 3px; height: 33px;">
                        :
                    </td>
                    <td style="width: 134px; height: 33px;">
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                        <asp:Image ID="imgToDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png"
                            Width="16px" />
                        <cc1:CalendarExtender ID="txtDtToCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            TargetControlID="txtToDate" PopupButtonID="imgToDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td style="width: 38px; height: 33px;">
                        Employee
                    </td>
                    <td style="height: 33px">
                        :
                    </td>
                    <td style="height: 33px">
                        <asp:DropDownList ID="cboEmp" runat="server" OnSelectedIndexChanged="cboEmp_SelectedIndexChanged"
                            Width="350px">
                        </asp:DropDownList>
                    </td>
                    <td style="height: 33px">
                        <asp:Button ID="btnShowRpt" runat="server" OnClick="btnShowRpt_Click" Text="Preview"
                            Width="100px" />
                    </td>
                    <td style="height: 33px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 25px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 74px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 137px">
                        &nbsp;
                    </td>
                    <td style="width: 61px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 134px">
                        &nbsp;
                    </td>
                    <td style="width: 38px">
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
            <asp:AsyncPostBackTrigger ControlID="cboEmp" EventName="SelectedIndexChanged" />
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
