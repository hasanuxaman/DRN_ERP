<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmImportAtnd.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmImportAtnd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Walsow Tower Import Attendance</div>
    <div align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%; font-size: 12px; background-color: #CCFFFF">
                    <tr>
                        <td style="width: 173px">
                            &nbsp;
                        </td>
                        <td style="width: 5px">
                            &nbsp;
                        </td>
                        <td style="width: 207px">
                            &nbsp;
                        </td>
                        <td style="width: 66px">
                            &nbsp;
                        </td>
                        <td style="width: 5px">
                            &nbsp;
                        </td>
                        <td style="width: 218px">
                            &nbsp;
                        </td>
                        <td style="width: 275px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 173px" align="right">
                            From Date
                        </td>
                        <td style="width: 5px">
                            :
                        </td>
                        <td style="width: 207px" align="left">
                            <asp:TextBox ID="txtFromDt" runat="server" Width="150px" CssClass="inline"></asp:TextBox>
                            <asp:Image ID="imgFromDt" runat="server" ImageUrl="~/Image/calendar.png" CssClass="inline" />
                            <cc1:CalendarExtender ID="CalendarExtenderFromDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgFromDt" TargetControlID="txtFromDt">
                            </cc1:CalendarExtender>
                        </td>
                        <td align="right" style="width: 66px">
                            To Date
                        </td>
                        <td style="width: 5px">
                            :
                        </td>
                        <td style="width: 218px" align="left">
                            <asp:TextBox ID="txtToDate" runat="server" Width="150px" CssClass="inline"></asp:TextBox>
                            <asp:Image ID="imgToDate" runat="server" ImageUrl="~/Image/calendar.png" CssClass="inline" />
                            <cc1:CalendarExtender ID="txtAttndDate_CalendarExtender" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" PopupButtonID="imgToDate" TargetControlID="txtToDate">
                            </cc1:CalendarExtender>
                        </td>
                        <td align="center" style="width: 275px">
                            &nbsp;
                            <asp:Button ID="btnImport" runat="server" Text="Import" ValidationGroup="AtndDate"
                                Width="131px" OnClick="btnImport_Click" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="131px" OnClick="btnSave_Click"
                                ValidationGroup="AtndDate" />
                        </td>
                        <td align="center" style="">
                            <asp:Label ID="lblFrmDt" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 173px">
                            &nbsp;
                        </td>
                        <td style="width: 5px">
                            &nbsp;
                        </td>
                        <td style="width: 207px" align="left">
                            <asp:CheckBox ID="chkLocalData" runat="server" Text="Import from Local Data Source"
                                Checked="True" />
                            <br />
                        </td>
                        <td style="width: 66px">
                            &nbsp;
                        </td>
                        <td style="width: 5px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 218px">
                            <br />
                        </td>
                        <td style="width: 275px" align="center">
                            &nbsp;
                            <asp:Button ID="btnLoad" runat="server" OnClick="btnLoad_Click" Text="Load Data"
                                ValidationGroup="AtndDate" Width="131px" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" Width="131px" />
                        </td>
                        <td align="center">
                            &nbsp;<asp:Label ID="lblToDt" runat="server"></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 173px">
                            &nbsp;
                        </td>
                        <td style="width: 5px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 207px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFromDt"
                                Display="Dynamic" ErrorMessage="Enter From Date First" ForeColor="Red" ValidationGroup="AtndDate"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFromDt"
                                Display="Dynamic" ErrorMessage="Enter Valid From Date" ForeColor="Red" Operator="DataTypeCheck"
                                Type="Date" ValidationGroup="AtndDate"></asp:CompareValidator>
                        </td>
                        <td style="width: 66px">
                            &nbsp;
                        </td>
                        <td style="width: 5px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 218px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtToDate"
                                Display="Dynamic" ErrorMessage="Enter To Date First" ForeColor="Red" ValidationGroup="AtndDate"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtToDate"
                                Display="Dynamic" ErrorMessage="Enter Valid To Date" ForeColor="Red" Operator="DataTypeCheck"
                                Type="Date" ValidationGroup="AtndDate"></asp:CompareValidator>
                        </td>
                        <td align="center" style="width: 275px">
                            &nbsp;
                        </td>
                        <td align="center">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div align="center" style="background-color: #0DFFFF">
                    <br />
                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                    &nbsp;<asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export To Excel"
                        Visible="False" />
                    <asp:GridView ID="gvEmpAttnd" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowSorting="True"
                        OnSorting="gvEmpAttnd_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpId" HeaderText="Emp ID" NullDisplayText="" SortExpression="EmpId">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpName" HeaderText="Name" SortExpression="EmpName" />
                            <asp:BoundField DataField="LocCode" HeaderText="Unit" SortExpression="LocCode" />
                            <asp:BoundField DataField="DeptName" HeaderText="Department" SortExpression="DeptName" />
                            <asp:BoundField DataField="DesigName" HeaderText="Designation" SortExpression="DesigName" />
                            <asp:BoundField DataField="EventDate" DataFormatString="{0:d}" HeaderText="Date"
                                SortExpression="EventDate">
                                <ItemStyle Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="InTime" HeaderText="In Time" SortExpression="InTime">
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OutTime" HeaderText="Out Time" SortExpression="OutTime">
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LateMin" HeaderText="Late (Min)" SortExpression="LateMin">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ToAttndtHr" HeaderText="Total (hr)" SortExpression="ToAttndtHr">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
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
                    <br />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnImport" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnExport" />
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
