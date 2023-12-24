<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmAttndDayRpt.aspx.cs" Inherits="DRN_WEB_ERP.frmAttndDayRpt" %>

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
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 74px" align="right">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 141px">
                        &nbsp;
                    </td>
                    <td style="width: 61px" align="right">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 136px">
                        &nbsp;
                    </td>
                    <td style="width: 81px">
                    </td>
                    <td style="width: 5px">
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
                    <td>
                        &nbsp;
                    </td>
                    <td align="right" style="width: 74px">
                        Date From
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td style="width: 141px">
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                        <asp:Image ID="imgFromDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                        <cc1:CalendarExtender ID="txtDtFromCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            TargetControlID="txtFromDate" PopupButtonID="imgFromDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right" style="width: 61px">
                        Date To
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td style="width: 136px">
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                        <asp:Image ID="imgToDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png"
                            Width="16px" />
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            TargetControlID="txtToDate" PopupButtonID="imgToDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right" style="width: 81px">
                        Employee Id
                    </td>
                    <td style="width: 5px">
                        :&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="cboEmp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboEmp_SelectedIndexChanged"
                            Width="350px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnShowRpt" runat="server" OnClick="btnShowRpt_Click" Text="Preview"
                            Width="100px" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 74px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
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
                    <td style="width: 61px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 136px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                            Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                        <br />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtToDate"
                            Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                            Type="Date" ValidationGroup="btnShow"></asp:CompareValidator>
                    </td>
                    <td style="width: 81px">
                        &nbsp;</td>
                    <td style="width: 5px">
                        &nbsp;
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Employee Id"
                            ForeColor="Red" ValidationGroup="btnShow" ControlToValidate="txtEmpRef"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 74px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 141px">
                        &nbsp;
                    </td>
                    <td style="width: 61px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 136px">
                        &nbsp;
                    </td>
                    <td style="width: 81px">
                        &nbsp;
                    </td>
                    <td style="width: 5px">
                        &nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmpRef" runat="server" Width="350px" Visible="False"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderEmp" runat="server" BehaviorID="AutoCompleteSrchEmp"
                            CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                            ServiceMethod="GetSrchEmp" ServicePath="~/Module/HRMS/Forms/wsAutoComHrms.asmx"
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtEmpRef">
                        </cc1:AutoCompleteExtender>
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
            <asp:AsyncPostBackTrigger ControlID="btnShowRpt" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="cboEmp" EventName="SelectedIndexChanged" />
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
