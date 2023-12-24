<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmIORpt.aspx.cs" Inherits="DRN_WEB_ERP.Module.IO.Forms.frmIORpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=500,height=500,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
    </script>
    <div align="center" style="background-color: #00FF99">
        I/O Status</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center">
                <table style="width: 100%; font-size: 12px; background-color: #CCFFFF">
                    <tr>
                        <td style="width: 25px">
                        </td>
                        <td style="width: 246px">
                        </td>
                        <td style="width: 1px">
                        </td>
                        <td style="width: 422px">
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
                        <td align="right" style="width: 246px; height: 33px;">
                            Employee Name
                        </td>
                        <td style="height: 33px; width: 1px;">
                            :
                        </td>
                        <td align="left" style="height: 33px; width: 422px;">
                            <asp:DropDownList ID="cboEmp" runat="server" AutoPostBack="True" Width="450px" OnSelectedIndexChanged="cboEmp_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="height: 33px">
                            <asp:Button ID="btnShowRpt" runat="server" Text="Preview" Width="100px" OnClick="btnShowRpt_Click" />
                        </td>
                        <td style="height: 33px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25px">
                            &nbsp;
                        </td>
                        <td style="width: 246px">
                            &nbsp;
                        </td>
                        <td style="width: 1px">
                            &nbsp;
                        </td>
                        <td style="width: 422px" align="center">
                            <%--<asp:Button ID="btnPrint" runat="server" Text="Print" Width="100px" OnClientClick="javascript:CallPrint('divPrint')" />--%>
                            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" Width="100px" OnClick="btnExport_Click" />
                            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" Width="100px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div align="center" id="divPrint" runat="server">
                    <asp:GridView ID="gvIoRpt" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvIoRpt_RowDataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
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
                                    <asp:HiddenField ID="hfEmpRef" runat="server" Value='<%# Bind("EmpRefNo") %>' />
                                    <asp:Label ID="IoEmpName" runat="server" Font-Bold="True" Text='<%# GetEmpName(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                    <br />
                                    <asp:Label ID="IoEmpId" runat="server" Font-Italic="True" ForeColor="#3399FF" Text='<%# GetEmpId(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                    <br />
                                    <asp:Label ID="IoEmpDes" runat="server" Font-Italic="True" Text='<%# GetEmpDesig(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                    <br />
                                    <asp:Label ID="IoEmpDep" runat="server" Font-Italic="True" Text='<%# GetEmpDept(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Limit Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblIoLimit" runat="server" Text='<%# GetIoLimit(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unadjusted">
                                <ItemTemplate>
                                    <asp:Label ID="lblIoUnAdj" runat="server" Text='<%# GetIoUnAdj(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#FFFF66" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Available Limit">
                                <ItemTemplate>
                                    <asp:Label ID="lblIoAvailLimit" runat="server" Text='<%# GetIoAvailLimit(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
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
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:AsyncPostBackTrigger ControlID="btnPrint" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
