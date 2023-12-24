<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmEmpAttnd.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmEmpAttnd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function TotAttndHr(inTime, outTime, totHr) {
            var inT = new Date();
            if (document.getElementById(inTime).value != '') {
                var inDateTimeString = new Date("January 01, 2014 " + document.getElementById(inTime).value);
                //var inT = new Date(inDateTimeString);
                inT = new Date(inDateTimeString);
            }
            var outT = new Date();
            if (document.getElementById(outTime).value != '') {
                var outDateTimeString = new Date("January 01, 2014 " + document.getElementById(outTime).value);
                //var outT = new Date(outDateTimeString);
                outT = new Date(outDateTimeString);
            }

            if (document.getElementById(inTime).value.toString() == "" || document.getElementById(outTime).value.toString() == "") {
                var diffHours1 = 0;
                var TotalHr = document.getElementById(totHr);
                TotalHr.value = diffHours1.toFixed(2);
            }
            else {
                var timeDiff = Math.abs(outT.getTime() - inT.getTime());
                var diffHours = parseFloat(timeDiff / 1000 / 60 / 60);
                var TotalHr = document.getElementById(totHr);
                TotalHr.value = diffHours.toFixed(2);
            }
        }
        function basicPopup() {
            popupWindow = window.open("frmQtnRmPrint.aspx", 'popUpWindow', 'height=600,width=600,left=100,top=30,resizable=No,scrollbars=No,toolbar=no,menubar=no,location=no,directories=no, status=No');
        }
    </script>
    <div align="center" style="background-color: #00FF99">
        Daily Attendance</div>
    <div align="center">
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
                        <td style="width: 61px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 9px">
                            &nbsp;
                        </td>
                        <td style="width: 31px">
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
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 53px" align="right">
                            Company
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
                            <asp:DropDownList ID="cboDept" runat="server" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="cboDept_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 61px" align="right">
                            Section
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 9px">
                            <asp:DropDownList ID="cboSec" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="cboSec_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 31px" align="right">
                            Shift
                        </td>
                        <td>
                            :&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="cboShift" runat="server" Width="120px">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            Date
                        </td>
                        <td>
                            :
                        </td>
                        <td style="" align="center">
                            <asp:TextBox ID="txtAttndDate" runat="server" Width="100px" CssClass="inline"></asp:TextBox>
                            <asp:Image ID="imgAttndDate" runat="server" ImageUrl="~/Image/calendar.png" CssClass="inline" />
                            <cc1:CalendarExtender ID="txtAttndDate_CalendarExtender" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" PopupButtonID="imgAttndDate" TargetControlID="txtAttndDate">
                            </cc1:CalendarExtender>
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
                        <td style="width: 61px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 9px">
                            &nbsp;
                        </td>
                        <td style="width: 31px">
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
                        <td align="left">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAttndDate"
                                Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="AtndDate"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtAttndDate"
                                Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                Type="Date" ValidationGroup="AtndDate"></asp:CompareValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div align="center" style="background-color: #0DFFFF">
                    <asp:Button ID="btnLoad" runat="server" OnClick="btnLoad_Click" Text="Load Data"
                        Width="131px" ValidationGroup="AtndDate" />
                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="131px" />
                    <asp:Button ID="btnSaveAll" runat="server" OnClick="btnSaveAll_Click" Text="Save All"
                        Width="131px" Visible="False" ValidationGroup="AtndDate" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnLoad" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSaveAll" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="cboLoc" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="cboDept" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="cboSec" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div align="center" style="background-color: #CCFFFF">
        <br />
        Color Indication:&nbsp;
        <asp:Label ID="Label1" runat="server" Text="[   ]"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="[   ]"></asp:Label>&nbsp;
        <asp:Label ID="Label3" runat="server" Text="[   ]"></asp:Label>
        <asp:Label ID="Label4" runat="server" Text="[   ]"></asp:Label>&nbsp;
        <asp:Label ID="Label5" runat="server" Text="[   ]"></asp:Label>
        <asp:Label ID="Label6" runat="server" Text="[   ]"></asp:Label>&nbsp;
        <asp:Label ID="Label7" runat="server" Text="[   ]"></asp:Label>
        <asp:Label ID="Label8" runat="server" Text="[   ]"></asp:Label>
        <%--<asp:Label ID="Label9" runat="server" Text="[   ]"></asp:Label>--%>
        <%--<asp:Label ID="Label10" runat="server" Text="[   ]"></asp:Label>--%>
        <br />
        <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <br />
                <asp:GridView ID="gvDayAttnd" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvDayAttnd_RowDataBound"
                    OnRowCommand="gvDayAttnd_RowCommand" OnRowDeleting="gvDayAttnd_RowDeleting">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Emp. ID">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfEmpRef" runat="server" Value='<%# Bind("EmpRefNo") %>' />
                                <%--<asp:Label ID="lblEmpId" runat="server" Text='<%# GetEmpId(int.Parse(Eval("EmpRefNo").ToString()))%>'></asp:Label>--%>
                                <asp:Label ID="lblEmpId" runat="server" Text='<%# Bind("EmpId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" ItemStyle-ForeColor="Blue">
                            <ItemTemplate>
                                <%--<asp:Label ID="lblEmpName" runat="server" Text='<%# GetEmpName(int.Parse(Eval("EmpRefNo").ToString()))%>'></asp:Label>--%>
                                <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle ForeColor="Blue" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company">
                            <ItemTemplate>
                                <%--<asp:Label ID="lblLocName" runat="server" Text='<%# GetLocName(int.Parse(Eval("EmpRefNo").ToString()))%>'></asp:Label>--%>
                                <asp:Label ID="lblLocName" runat="server" Text='<%# Bind("LocCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department">
                            <ItemTemplate>
                                <%-- <asp:Label ID="lblDeptName" runat="server" Text='<%# GetDeptName(int.Parse(Eval("EmpRefNo").ToString()))%>'></asp:Label>--%>
                                <asp:Label ID="lblDeptName" runat="server" Text='<%# Bind("DeptName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Section">
                            <ItemTemplate>
                                <%--<asp:Label ID="lblSecName" runat="server" Text='<%# GetSecName(int.Parse(Eval("EmpRefNo").ToString()))%>'></asp:Label>--%>
                                <asp:Label ID="lblSecName" runat="server" Text='<%# Bind("SecName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shift">
                            <ItemTemplate>
                                <%--<asp:HiddenField ID="hfShiftRef" runat="server" Value='<%# GetShiftRef(int.Parse(Eval("EmpRefNo").ToString()))%>' />--%>
                                <%--<asp:Label ID="lblShift" runat="server" Text='<%# GetShiftName(int.Parse(Eval("EmpRefNo").ToString()))%>'></asp:Label>--%>
                                <asp:HiddenField ID="hfShiftRef" runat="server" Value='<%# Bind("ShiftRefNo") %>' />
                                <asp:Label ID="lblShift" runat="server" Text='<%# Bind("ShiftName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="In Time">
                            <ItemTemplate>
                                <asp:TextBox ID="txtInTime" runat="server" Text='' TextMode="Time" CssClass="textAlignCenter"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Out Time">
                            <ItemTemplate>
                                <asp:TextBox ID="txtOutTime" runat="server" Text='' TextMode="Time" CssClass="textAlignCenter"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total (hr)" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtTotHr" runat="server" Text="0.00" Width="50px" CssClass="textAlignCenter transparent"
                                    BorderStyle="None" Enabled="False" Font-Size="8"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="lblRem" runat="server" ForeColor="Blue" Width="120px" BorderStyle="None"
                                    Font-Size="8"></asp:Label>
                                <asp:LinkButton ID="lnkYes" runat="server" Visible="false">Yes</asp:LinkButton>
                                <asp:LinkButton ID="lnkNo" runat="server" Visible="false">No</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:Button ID="btnLeave" runat="server" Text="Assign Leave" CommandName="Leave"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnSave" runat="server" CausesValidation="False" CommandName="Save"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ImageUrl="~/Image/Save.png"
                                    OnClientClick="return confirm('Do you want to save?')" ToolTip="Save" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    ImageUrl="~/Image/Delete.png" OnClientClick="return confirm('Do you want to delete?')"
                                    ToolTip="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="10pt" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" />
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
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gvDayAttnd" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="gvDayAttnd" EventName="RowDataBound" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
    </div>
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
