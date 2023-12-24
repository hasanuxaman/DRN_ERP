<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmSalaryCalc.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmSalaryCalc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Salary Calculation</div>
    <div align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%; font-size: 12px; background-color: #CCFFFF">
                    <tr>
                        <td style="width: 100px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
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
                        <td style="width: 47px">
                            &nbsp;
                        </td>
                        <td style="width: 1px">
                            &nbsp;
                        </td>
                        <td style="width: 176px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px" align="right">
                            Company
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 146px">
                            <asp:DropDownList ID="cboLoc" runat="server" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="cboLoc_SelectedIndexChanged"
                                CssClass="etc">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 74px" align="right">
                            Department
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 118px">
                            <asp:DropDownList ID="cboDept" runat="server" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="cboDept_SelectedIndexChanged"
                                CssClass="etc">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 61px" align="right">
                            Section
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 9px">
                            <asp:DropDownList ID="cboSec" runat="server" Width="150px" CssClass="etc">
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width: 47px">
                            Period
                        </td>
                        <td style="width: 1px">
                            :
                        </td>
                        <td style="width: 176px" align="center">
                            <asp:TextBox ID="txtSalDate" runat="server" Width="120px" CssClass="etc"></asp:TextBox>
                            <asp:Image ID="imgSalDate" runat="server" ImageUrl="~/Image/calendar.png" />
                            <cc1:CalendarExtender ID="txtSalDate_CalendarExtender" runat="server" Enabled="True"
                                Format="MMMM/yyyy" PopupButtonID="imgSalDate" TargetControlID="txtSalDate">
                            </cc1:CalendarExtender>
                        </td>
                        <td align="center" style="">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
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
                        <td style="width: 47px">
                            &nbsp;
                        </td>
                        <td style="width: 1px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 176px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSalDate"
                                Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red"></asp:RequiredFieldValidator>
                            <br />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div align="center" style="background-color: #0DFFFF">
                    <asp:Button ID="btnGetData" runat="server" Text="Get Data" Width="131px" OnClick="btnGetData_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="131px" OnClick="btnClear_Click" />
                    <asp:Button ID="btnCalculate" runat="server" Text="Trial Calculation" Width="131px"
                        OnClick="btnCalculate_Click" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCalculate" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnGetData" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="cboLoc" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="cboDept" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div align="center" style="background-color: #CCFFFF">
        <br />
        <br />
        <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvSalHdr" runat="server" CellPadding="4" Font-Size="10pt" ForeColor="#333333"
                    AutoGenerateColumns="False" OnRowDataBound="gvSalHdr_RowDataBound" OnRowDeleting="gvSalHdr_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="EmpId" HeaderText="Emp. ID">
                            <ItemStyle VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Employee Name">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfEmpRef" runat="server" Value='<%# Bind("EmpRefNo") %>' />
                                <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("EmpName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="SalPeriod" HeaderText="Period">
                            <ItemStyle VerticalAlign="Top" />
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Grade">
                            <ItemTemplate>
                                <asp:Label ID="lblGrade" runat="server" Text='<%# Bind("GrdDefName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Net Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblNetVal" runat="server" Text='<%# GetNetVal(Eval("EmpRefNo").ToString())%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle ForeColor="#339933" Font-Bold="True" />
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pay Details">
                            <ItemTemplate>
                                <asp:Panel ID="pnlSalHdr" runat="server" CssClass="cpHeader" Width="200px" HorizontalAlign="Center">
                                    <asp:Label ID="lblSalHdr" Text="Show Details" runat="server" />
                                </asp:Panel>
                                <asp:Panel ID="pnlSalDet" runat="server" CssClass="cpBody" Width="200px" HorizontalAlign="Center">
                                    <div align="center">
                                        <asp:GridView ID="gvSalDet" runat="server" CellPadding="2" Font-Size="8pt" ForeColor="Black"
                                            AutoGenerateColumns="False" EmptyDataText="Not Calculated" BackColor="LightGoldenrodYellow"
                                            BorderColor="Tan" BorderWidth="1px">
                                            <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                            <Columns>
                                                <asp:BoundField DataField="PayHeadName" HeaderText="Pay Head" />
                                                <asp:BoundField DataField="CalValue" HeaderText="Amount">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                            </Columns>
                                            <FooterStyle BackColor="Tan" />
                                            <HeaderStyle BackColor="Tan" Font-Bold="True" Font-Size="10pt" />
                                            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                            <SortedAscendingCellStyle BackColor="#FAFAE7" />
                                            <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                                            <SortedDescendingCellStyle BackColor="#E1DB9C" />
                                            <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender118" runat="server" TargetControlID="pnlSalDet"
                                    CollapseControlID="pnlSalHdr" ExpandControlID="pnlSalHdr" Collapsed="true" TextLabelID="lblSalHdr"
                                    CollapsedText="Show Details" ExpandedText="Hide Details" CollapsedSize="0" AutoCollapse="False"
                                    AutoExpand="False" ScrollContents="false" ImageControlID="Image1" ExpandedImage="~/Image/collapse.jpg"
                                    CollapsedImage="~/Image/expand.jpg" ExpandDirection="Vertical">
                                </cc1:CollapsiblePanelExtender>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnDelete" ImageUrl="~/Image/Delete.png" runat="server" CausesValidation="False"
                                    ToolTip="Delete" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Do you want to delete?')">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle BackColor="White" />
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
                </asp:GridView>
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gvSalHdr" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="gvSalHdr" EventName="RowDataBound" />
            </Triggers>
        </asp:UpdatePanel>
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
