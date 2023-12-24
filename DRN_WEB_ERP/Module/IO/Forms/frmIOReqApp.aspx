<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmIOReqApp.aspx.cs" Inherits="DRN_WEB_ERP.Module.IO.Forms.frmIOReqApp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        I/O Requisition List
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <br />
                <asp:Panel ID="Panel1" runat="server" GroupingText="Pending I/O Requisition">
                    <br />
                    <asp:GridView ID="gvEmpPendIo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" Font-Size="8pt" OnRowCommand="gvEmpPendIo_RowCommand" OnRowDataBound="gvEmpPendIo_RowDataBound">
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
                            <asp:TemplateField HeaderText="I/O Ref No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfIoRef" runat="server" Value='<%# Bind("IoReqRef") %>' />
                                    <asp:Label ID="lblIoRefNo" runat="server" Text='<%# Bind("IoReqRefNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ReqDate" HeaderText="Date" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="ReqCmnt" HeaderText="Reason" />
                            <asp:TemplateField HeaderText="Business Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblBusUnit" runat="server" Text='<%# GetBusUnit(Eval("ReqBusUnit").ToString())%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ReqAmount" HeaderText="Req. Amount" DataFormatString="{0:n}">
                                <ItemStyle BackColor="#9966FF" Font-Bold="True" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Unadjusted">
                                <ItemTemplate>
                                    <asp:Label ID="lblIoUnAdj" runat="server" Text='<%# GetIoUnAdj(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#FFFF66" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("ReqStatDesc") %>'></asp:Label>--%>
                                    <asp:Label ID="lblIoStatus" runat="server" Text='<%# GetIoStatus(Eval("IoReqRefNo").ToString())%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="btnReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="btnForward" runat="server" Text="Approve" CommandName="Forward" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="btnApprove" runat="server" Text="Post" CommandName="Approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
                <asp:Button ID="btnShowRpt" runat="server" Text="Show Approved IO" OnClick="btnShowRpt_Click" />
                <br />
                <asp:GridView ID="gvShowApprIo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" Font-Size="8pt" OnRowDataBound="gvShowApprIo_RowDataBound"
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
                        <asp:TemplateField HeaderText="I/O Ref No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfIoRef" runat="server" Value='<%# Bind("IoReqRef") %>' />
                                <asp:Label ID="lblIoRefNo" runat="server" Text='<%# Bind("IoReqRefNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ReqDate" HeaderText="Date" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="ReqCmnt" HeaderText="Reason" />
                        <asp:TemplateField HeaderText="Business Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblBusUnit" runat="server" Text='<%# GetBusUnit(Eval("ReqBusUnit").ToString())%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ReqAmount" HeaderText="Amount" DataFormatString="{0:n}">
                            <ItemStyle BackColor="#9966FF" Font-Bold="True" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("ReqStatDesc") %>'></asp:Label>--%>
                                <asp:Label ID="lblIoStatus" runat="server" Text='<%# GetIoStatus(Eval("IoReqRefNo").ToString())%>'></asp:Label>
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
