<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmAttndDayRptAdm.aspx.cs" Inherits="DRN_WEB_ERP.frmAttndDayRptAdm" %>

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
                    <td width="2">
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
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 3px">
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
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Work Station
                    </td>
                    <td align="right" width="2">
                        :
                    </td>
                    <td align="right">
                        <asp:DropDownList ID="cboWrkStn" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        Unit
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="cboLoc" runat="server" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="cboLoc_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        Department
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="cboDept" runat="server" Width="180px">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        From
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txtFromDate" runat="server" Width="70px" CssClass="textAlignCenter"></asp:TextBox>
                        <asp:Image ID="imgFromDt" runat="server" ImageUrl="~/Image/calendar.png" CssClass="inline" />
                        <cc1:CalendarExtender ID="CalendarExtender_txtFromDt" runat="server" Enabled="True"
                            Format="dd/MM/yyyy" PopupButtonID="imgFromDt" TargetControlID="txtFromDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right">
                        To
                    </td>
                    <td>
                        :&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtToDate" runat="server" Width="70px" CssClass="textAlignCenter"></asp:TextBox>
                        <asp:Image ID="imgToDate" runat="server" ImageUrl="~/Image/calendar.png" CssClass="inline" />
                        <cc1:CalendarExtender ID="CalendarExtender_txtToDate" runat="server" Enabled="True"
                            Format="dd/MM/yyyy" PopupButtonID="imgToDate" TargetControlID="txtToDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="center">
                        <asp:Button ID="btnShowRpt" runat="server" Text="Show" ValidationGroup="AtndDate"
                            OnClick="btnShowRpt_Click" Width="70px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td width="2">
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
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate"
                            Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="AtndDate"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtToDate"
                            Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                            Type="Date" ValidationGroup="AtndDate"></asp:CompareValidator>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtToDate"
                            Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="AtndDate"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtToDate"
                            Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                            Type="Date" ValidationGroup="AtndDate"></asp:CompareValidator>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <div align="center" style="width: 100%; font-size: 12px; background-color: #CCFFFF">
                <br />
                <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export To Excel" />
                <asp:GridView ID="gvEmpAttnd" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowSorting="True"
                    OnSorting="gvEmpAttnd_Sorting">
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Wrk_Station_Name" HeaderText="Location">
                            <ItemStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LocCode" HeaderText="Unit">
                            <ItemStyle Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EmpName" HeaderText="Name">
                            <ItemStyle Width="140px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EmpId" HeaderText="Emp ID" NullDisplayText="">
                            <ItemStyle Width="65px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DeptName" HeaderText="Department">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DesigName" HeaderText="Designation">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RptDate" DataFormatString="{0:d}" HeaderText="Date">
                            <ItemStyle Width="60px" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AttndInTime" HeaderText="In Time">
                            <ItemStyle Width="50px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AttndOutTime" HeaderText="Out Time">
                            <ItemStyle Width="50px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AttndLateMin" HeaderText="Late (Min)">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AttndTotHr" HeaderText="Total (hr)">
                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AttndExtData1" HeaderText="Remarks" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Size="10pt" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle Font-Size="8pt" ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnShowRpt" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="cboLoc" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboDept" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DRNConStr %>"
        SelectCommand="SELECT * FROM [View_Emp_Attnd]"></asp:SqlDataSource>
</asp:Content>
