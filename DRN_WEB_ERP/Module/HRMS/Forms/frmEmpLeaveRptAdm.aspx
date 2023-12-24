<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmEmpLeaveRptAdm.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmEmpLeaveRptAdm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Leave Report
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
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
                    <td colspan="2" align="center">
                        <asp:RadioButton ID="optDetails" runat="server" Checked="True" GroupName="RptOpt"
                            Text="Details" />
                        <asp:RadioButton ID="optSummary" runat="server" GroupName="RptOpt" Text="Summary" />
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
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 146px">
                        &nbsp;
                    </td>
                    <td style="width: 74px" align="right">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 118px">
                        &nbsp;
                    </td>
                    <td style="width: 94px" align="right">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 130px">
                        &nbsp;
                    </td>
                    <td style="width: 68px" align="right">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 151px">
                        &nbsp;
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td style="" align="center">
                        &nbsp;
                    </td>
                    <td align="center" style="">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 53px">
                        Unit
                    </td>
                    <td>
                        :
                    </td>
                    <td style="width: 146px">
                        <asp:DropDownList ID="cboLoc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboLoc_SelectedIndexChanged"
                            Width="180px">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 74px">
                        Department
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td style="width: 118px">
                        <asp:DropDownList ID="cboDept" runat="server" Width="180px">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 94px">
                        From Date
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td style="width: 130px">
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="textAlignCenter" Width="90px"></asp:TextBox>
                        <asp:Image ID="imgFromDt" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                        <cc1:CalendarExtender ID="CalendarExtender_txtFromDt" runat="server" Enabled="True"
                            Format="dd/MM/yyyy" PopupButtonID="imgFromDt" TargetControlID="txtFromDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right" style="width: 68px">
                        To Date
                    </td>
                    <td>
                        :&nbsp;
                    </td>
                    <td style="width: 151px">
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="textAlignCenter" Width="90px"></asp:TextBox>
                        <asp:Image ID="imgToDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                        <cc1:CalendarExtender ID="CalendarExtender_txtToDate" runat="server" Enabled="True"
                            Format="dd/MM/yyyy" PopupButtonID="imgToDate" TargetControlID="txtToDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                    <td align="center">
                        <asp:Button ID="btnShowRpt" runat="server" OnClick="btnShowRpt_Click" Text="Show"
                            ValidationGroup="AtndDate" Width="100px" />
                    </td>
                    <td align="center" style="">
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
            <table style="width: 100%; font-size: 12px; background-color: #CCFFFF">
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
                            Visible="False">
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
                                    <ItemStyle Font-Bold="True" Width="140px" />
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
                            Visible="False">
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
                                <%--<asp:TemplateField HeaderText="Leave Type">
                                    <FooterTemplate>
                                        Total:
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfLvTypeSum" runat="server" Value='<%# Bind("LvDetLvType") %>' />
                                        <asp:Label ID="lblLvTypeSum" runat="server" Text='<%# GetLvType(int.Parse(Eval("LvDetLvType").ToString()))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
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
                                <%--<asp:TemplateField HeaderText="Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLvBalSum" runat="server" Text='<%# GetLvBal(Eval("EmpRefNo").ToString(),int.Parse(Eval("LvDetLvType").ToString()))%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle BackColor="Aqua" HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
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
