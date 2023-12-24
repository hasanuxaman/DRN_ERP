<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmEmpLeaveAppr.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmEmpLeaveAppr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
                        
    </script>
    <div align="center" style="background-color: #00FF99">
        Allocate Employee Leave
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <br />
                <asp:Panel ID="Panel1" runat="server" GroupingText="Pending Leave List">
                    <br />
                    <asp:GridView ID="gvEmpPendLv" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" Font-Size="8pt" OnRowCommand="gvEmpPendLv_RowCommand" OnRowDataBound="gvEmpPendLv_RowDataBound">
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
                                    <asp:Label ID="lvEmpName" runat="server" Text='<%# GetEmpName(Eval("EmpRefNo").ToString())%>'
                                        Font-Bold="True"></asp:Label>
                                    <br />
                                    <asp:Label ID="lvEmpId" runat="server" Text='<%# GetEmpId(Eval("EmpRefNo").ToString())%>'
                                        Font-Italic="True" ForeColor="#3399FF"></asp:Label>
                                    <br />
                                    <asp:Label ID="lvEmpDes" runat="server" Text='<%# GetEmpDesig(Eval("EmpRefNo").ToString())%>'
                                        Font-Italic="True"></asp:Label>
                                    <br />
                                    <asp:Label ID="lvEmpDep" runat="server" Text='<%# GetEmpDept(Eval("EmpRefNo").ToString())%>'
                                        Font-Italic="True"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="120px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="LvDetEntDt" DataFormatString="{0:d}" HeaderText="Apply Date">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Leave Type">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfLvType" runat="server" Value='<%# Bind("LvDetLvType") %>' />
                                    <asp:Label ID="lblLvType" runat="server" Text='<%# GetLvType(int.Parse(Eval("LvDetLvType").ToString()))%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="70px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Leave Date">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfLvRef" runat="server" Value='<%# Bind("LvDetRef") %>' />
                                    <asp:HiddenField ID="hfEmpRef" runat="server" Value='<%# Bind("EmpRefNo") %>' />
                                    <asp:HiddenField ID="hfEmpSupRef" runat="server" Value='<%# Bind("EmpSuprId") %>' />
                                    <asp:Label ID="lblLvDt" runat="server" Text='<%# GetPendLvDt(int.Parse(Eval("LvDetRef").ToString()))%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="140px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tot. Days">
                                <ItemTemplate>
                                    <asp:Label ID="lblLvDays" runat="server" Text='<%# Bind("LvDetDays") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#FFCCFF" HorizontalAlign="Center" Width="50px" Font-Bold="True" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="LvDetCmnt" HeaderText="Reason">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lblLvBal" runat="server" Text='<%# GetLvBal(Eval("LvDetStDate").ToString(),Eval("EmpRefNo").ToString(),int.Parse(Eval("LvDetLvType").ToString()))%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="Aqua" HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("LvDetAppStat") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="btnReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                        OnClientClick="return confirm('Do you want to Reject?')" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="btnForward" runat="server" Text="Fwd. to HRD" CommandName="Forward"
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Do you want to Forward?')" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CommandName="Approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                        OnClientClick="return confirm('Do you want to Approve?')" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="8pt" />
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
