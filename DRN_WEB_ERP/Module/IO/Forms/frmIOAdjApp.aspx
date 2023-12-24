<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmIOAdjApp.aspx.cs" Inherits="DRN_WEB_ERP.Module.IO.Forms.frmIOAdjApp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        I/O Adjustment List
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <br />
                <asp:Panel ID="Panel1" runat="server" GroupingText="Pending I/O Adjustment">
                    <br />
                    <asp:GridView ID="gvEmpPendIoAdj" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" Font-Size="8pt" OnRowCommand="gvEmpPendIoAdj_RowCommand"
                        OnRowDataBound="gvEmpPendIoAdj_RowDataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" class="preview" Target="_blank" NavigateUrl='<%# Eval("EmpRefNo", "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo={0}") %>'
                                        runat="server">
                                        <asp:Image ID="AttachImage" runat="server" Height="50px" Width="65px" ImageUrl="~/Image/NoImage.gif" />
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Details">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfEmpRef" runat="server" Value='<%# Bind("EmpRefNo") %>' />
                                    <asp:Label ID="IoEmpName" runat="server" Text='<%# GetEmpName(Eval("EmpRefNo").ToString())%>'
                                        Font-Bold="True"></asp:Label>
                                    <br />
                                    <asp:Label ID="IoEmpId" runat="server" Text='<%# GetEmpId(Eval("EmpRefNo").ToString())%>'
                                        Font-Italic="True" ForeColor="#3399FF"></asp:Label>
                                    <br />
                                    <asp:Label ID="IoEmpDes" runat="server" Text='<%# GetEmpDesig(Eval("EmpRefNo").ToString())%>'
                                        Font-Italic="True"></asp:Label>
                                    <br />
                                    <asp:Label ID="IoEmpDep" runat="server" Text='<%# GetEmpDept(Eval("EmpRefNo").ToString())%>'
                                        Font-Italic="True"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="I/O Adj. Ref. No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfIoAdjRef" runat="server" Value='<%# Bind("IoAdjRef") %>' />
                                    <asp:Label ID="lblIoAdjRefNo" runat="server" Text='<%# Bind("IoAdjRefNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AdjDate" HeaderText="Date" DataFormatString="{0:d}" />
                            <asp:TemplateField HeaderText="Business Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblBusUnit" runat="server" Text='<%# GetBusUnit(Eval("AdjBusUnit").ToString())%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AdjAmount" HeaderText="Amount" DataFormatString="{0:n}">
                                <ItemStyle BackColor="#9966FF" Font-Bold="True" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AdjMprNo" HeaderText="MPR No" />
                            <asp:BoundField DataField="AdjQuotNo" HeaderText="Quotation No" />
                            <asp:BoundField DataField="AdjWoNo" HeaderText="W/O No" />
                            <asp:BoundField DataField="AdjBillNo" HeaderText="Bill No" />
                            <asp:BoundField DataField="AdjSiteChlnNo" HeaderText="Site Rcv. Challan" />
                            <asp:BoundField DataField="AdjMrrNo" HeaderText="MRR No" />
                            <asp:BoundField DataField="AdjCmnt" HeaderText="Remarks" />
                            <%--<asp:BoundField DataField="AdjStatDesc" HeaderText="Status" />--%>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblIoAdjStatus" runat="server" Text='<%# GetAdjStatus(Eval("IoAdjRefNo").ToString())%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="btnReceive" runat="server" Text="Receive" CommandName="Receive" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="btnPost" runat="server" Text="Post" CommandName="Post" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="10pt" />
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
                </asp:Panel>
                <br />
                <asp:Button ID="btnShowRpt" runat="server" Text="Show Approved Adjustment" OnClick="btnShowRpt_Click" />
                <br />
                <asp:GridView ID="gvShowApprAdj" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" Font-Size="8pt" OnRowDataBound="gvShowApprAdj_RowDataBound"
                    Visible="false">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" class="preview" Target="_blank" NavigateUrl='<%# Eval("EmpRefNo", "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo={0}") %>'
                                    runat="server">
                                    <asp:Image ID="AttachImage" runat="server" Height="50px" Width="65px" ImageUrl="~/Image/NoImage.gif" />
                                </asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee Details">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfEmpRef" runat="server" Value='<%# Bind("EmpRefNo") %>' />
                                <asp:Label ID="IoEmpName" runat="server" Text='<%# GetEmpName(Eval("EmpRefNo").ToString())%>'
                                    Font-Bold="True"></asp:Label>
                                <br />
                                <asp:Label ID="IoEmpId" runat="server" Text='<%# GetEmpId(Eval("EmpRefNo").ToString())%>'
                                    Font-Italic="True" ForeColor="#3399FF"></asp:Label>
                                <br />
                                <asp:Label ID="IoEmpDes" runat="server" Text='<%# GetEmpDesig(Eval("EmpRefNo").ToString())%>'
                                    Font-Italic="True"></asp:Label>
                                <br />
                                <asp:Label ID="IoEmpDep" runat="server" Text='<%# GetEmpDept(Eval("EmpRefNo").ToString())%>'
                                    Font-Italic="True"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="I/O Adj. Ref. No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfIoAdjRef" runat="server" Value='<%# Bind("IoAdjRef") %>' />
                                <asp:Label ID="lblIoAdjRefNo" runat="server" Text='<%# Bind("IoAdjRefNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AdjDate" HeaderText="Date" DataFormatString="{0:d}" />
                        <asp:TemplateField HeaderText="Business Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblBusUnit" runat="server" Text='<%# GetBusUnit(Eval("AdjBusUnit").ToString())%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AdjAmount" HeaderText="Amount" DataFormatString="{0:n}">
                            <ItemStyle BackColor="#9966FF" Font-Bold="True" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AdjMprNo" HeaderText="MPR No" />
                        <asp:BoundField DataField="AdjQuotNo" HeaderText="Quotation No" />
                        <asp:BoundField DataField="AdjWoNo" HeaderText="W/O No" />
                        <asp:BoundField DataField="AdjBillNo" HeaderText="Bill No" />
                        <asp:BoundField DataField="AdjSiteChlnNo" HeaderText="Site Rcv. Challan" />
                        <asp:BoundField DataField="AdjMrrNo" HeaderText="MRR No" />
                        <asp:BoundField DataField="AdjCmnt" HeaderText="Remarks" />
                        <%--<asp:BoundField DataField="AdjStatDesc" HeaderText="Status" />--%>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblIoAdjStatus" runat="server" Text='<%# GetAdjStatus(Eval("IoAdjRefNo").ToString())%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="10pt" />
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
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="cboLeaveType" EventName="SelectedIndexChanged" />--%>
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
