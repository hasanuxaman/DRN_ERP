<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmEmpLeaveRpt.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmEmpLeaveRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Leave Report
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%; font-size: 12px; background-color: #CCFFFF">
                <tr>
                    <td style="width: 21px">
                        &nbsp;
                    </td>
                    <td style="width: 138px" align="right">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 141px">
                        &nbsp;
                    </td>
                    <td style="width: 97px" align="right">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 136px">
                        &nbsp;
                    </td>
                    <td style="width: 105px">
                        &nbsp;
                    </td>
                    <td style="width: 6px">
                        &nbsp;
                    </td>
                    <td style="width: 136px">
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
                    <td align="center" colspan="10">
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:RadioButton ID="optDetails" runat="server" Checked="True" GroupName="RptOpt"
                                Text="Details" />
                            <asp:RadioButton ID="optSummary" runat="server" GroupName="RptOpt" Text="Summary" />
                        </asp:Panel>
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
                    <td style="width: 141px">
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                        <asp:Image ID="imgFromDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                        <cc1:CalendarExtender ID="txtDtFromCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            TargetControlID="txtFromDate" PopupButtonID="imgFromDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right" style="width: 97px">
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
                    <td align="right" style="width: 105px">
                        Employee Id
                    </td>
                    <td style="width: 6px">
                        :
                    </td>
                    <td style="width: 136px">
                        <asp:DropDownList ID="cboEmp" runat="server" OnSelectedIndexChanged="cboEmp_SelectedIndexChanged"
                            Width="350px">
                        </asp:DropDownList>
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
                        &nbsp;
                    </td>
                    <td>
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
                    <td style="width: 97px">
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
                    <td style="width: 105px">
                        &nbsp;
                    </td>
                    <td style="width: 6px">
                        &nbsp;
                    </td>
                    <td style="width: 136px">
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
                    <td style="width: 141px">
                        &nbsp;
                    </td>
                    <td style="width: 97px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 136px">
                        &nbsp;
                    </td>
                    <td style="width: 105px">
                        &nbsp;
                    </td>
                    <td style="width: 6px">
                        &nbsp;
                    </td>
                    <td style="width: 136px">
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
                    <td align="center" colspan="12">
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export To Excel"
                            Visible="False" Width="120px" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="12">
                        <asp:GridView ID="gvEmpLvRptDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvEmpLvRptDet_RowDataBound"
                            Visible="False" ShowFooter="True">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" class="preview" NavigateUrl='<%# Eval("EmpRefNo", "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo={0}") %>'
                                            Target="_blank">
                                            <asp:Image ID="AttachImage" runat="server" Height="50px" Width="65px" ImageUrl="~/Image/NoImage.gif" />
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Details">
                                    <ItemTemplate>
                                        <asp:Label ID="lvEmpName" runat="server" Font-Bold="True" Text='<%# GetEmpName(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                        <br />
                                        <asp:Label ID="lvEmpId" runat="server" Font-Italic="True" ForeColor="#3399FF" Text='<%# GetEmpId(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                        <br />
                                        <asp:Label ID="lvEmpDes" runat="server" Font-Italic="True" Text='<%# GetEmpDesig(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                        <br />
                                        <asp:Label ID="lvEmpDep" runat="server" Font-Italic="True" Text='<%# GetEmpDept(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Leave Type">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfLvType" runat="server" Value='<%# Bind("LvDetLvType") %>' />
                                        <asp:Label ID="lblLvType" runat="server" Text='<%# GetLvType(int.Parse(Eval("LvDetLvType").ToString()))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LvDetCmnt" HeaderText="Reason">
                                    <ItemStyle Width="380px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Date">
                                    <FooterTemplate>
                                        Total:
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfLvRef" runat="server" Value='<%# Bind("LvDetRef") %>' />
                                        <asp:HiddenField ID="hfEmpRef" runat="server" Value='<%# Bind("EmpRefNo") %>' />
                                        <asp:Label ID="lblLvDt" runat="server" Text='<%# GetLvDt(Eval("LvDetRef").ToString())%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Leave Days">
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotDays" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLvDays" runat="server" Text='<%# Bind("LvDetDays") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <ItemStyle BackColor="#FFCCFF" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLvBal" runat="server" Text='<%# GetLvBal(Eval("LvDetStDate").ToString(),Eval("EmpRefNo").ToString(),int.Parse(Eval("LvDetLvType").ToString()))%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle BackColor="Aqua" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="10pt" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                        <br />
                        <asp:GridView ID="gvEmpLvRptSum" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvEmpLvRptSum_RowDataBound"
                            Visible="False" ShowFooter="True">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLinkLvSum" runat="server" class="preview" NavigateUrl='<%# Eval("EmpRefNo", "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo={0}") %>'
                                            Target="_blank">
                                            <asp:Image ID="AttachImageLvSum" runat="server" Height="50px" Width="65px" ImageUrl="~/Image/NoImage.gif" />
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Details">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfEmpRefLvSum" runat="server" Value='<%# Bind("EmpRefNo") %>' />
                                        <asp:Label ID="lvEmpNameLvSum" runat="server" Font-Bold="True" Text='<%# GetEmpName(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                        <br />
                                        <asp:Label ID="lvEmpIdLvSum" runat="server" Font-Italic="True" ForeColor="#3399FF"
                                            Text='<%# GetEmpId(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                        <br />
                                        <asp:Label ID="lvEmpDesLvSum" runat="server" Font-Italic="True" Text='<%# GetEmpDesig(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                        <br />
                                        <asp:Label ID="lvEmpDepLvSum" runat="server" Font-Italic="True" Text='<%# GetEmpDept(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Leave Type">
                                    <FooterTemplate>
                                        Total:
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfLvTypeSum" runat="server" Value='<%# Bind("LvDetLvType") %>' />
                                        <asp:Label ID="lblLvTypeSum" runat="server" Text='<%# GetLvType(int.Parse(Eval("LvDetLvType").ToString()))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Leave Days">
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotDays" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLvDaysSum" runat="server" Text='<%# Bind("LvDetDays") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <ItemStyle BackColor="#FFCCFF" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLvBalSum" runat="server" Text='<%# GetLvBal(Eval("LvDetStDate").ToString(),Eval("EmpRefNo").ToString(),int.Parse(Eval("LvDetLvType").ToString()))%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle BackColor="Aqua" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="10pt" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
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
                    <td style="width: 141px">
                        &nbsp;
                    </td>
                    <td style="width: 97px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 136px">
                        &nbsp;
                    </td>
                    <td style="width: 105px">
                        &nbsp;
                    </td>
                    <td style="width: 6px">
                        &nbsp;
                    </td>
                    <td style="width: 136px">
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
                    <td style="width: 141px">
                        &nbsp;
                    </td>
                    <td style="width: 97px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 136px">
                        &nbsp;
                    </td>
                    <td style="width: 105px">
                        &nbsp;
                    </td>
                    <td style="width: 6px">
                        &nbsp;
                    </td>
                    <td style="width: 136px">
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnShowRpt" EventName="click" />
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
