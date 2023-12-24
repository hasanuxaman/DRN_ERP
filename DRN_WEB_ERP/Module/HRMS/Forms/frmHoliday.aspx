<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmHoliday.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmHoliday" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Allocate Holiday</div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center">
                <table style="width: 100%; background-color: #CCFFFF">
                    <tr align="left">
                        <td style="width: 203px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 53px" align="left">
                            Company
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 187px" align="left">
                            <asp:DropDownList ID="cboLoc" runat="server" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="cboLoc_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 45px">
                            &nbsp;
                        </td>
                        <td style="width: 99px;" align="left">
                            Description
                        </td>
                        <td style="width: 3px;" align="left">
                            :
                        </td>
                        <td style="width: 196px;" align="left">
                            <asp:TextBox ID="txtHolDesc" runat="server" Width="180px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHolDesc"
                                ErrorMessage="Enter Holiday Description" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 122px;" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 203px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 53px" align="left">
                            Department
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 187px" align="left">
                            <asp:DropDownList ID="cboDept" runat="server" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="cboDept_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 45px">
                            &nbsp;
                        </td>
                        <td style="width: 99px" align="left">
                            Remarks
                        </td>
                        <td style="width: 3px" align="left">
                            :
                        </td>
                        <td style="width: 196px" align="left" rowspan="2" valign="top">
                            <asp:TextBox ID="txtHolRem" runat="server" Width="180px" Height="40px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 203px; height: 28px;" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 53px; height: 28px;" align="left">
                            Section
                        </td>
                        <td style="width: 3px; height: 28px;">
                            :
                        </td>
                        <td style="width: 187px; height: 28px;" align="left">
                            <asp:DropDownList ID="cboSec" runat="server" Width="180px">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 45px; height: 28px;">
                            &nbsp;
                        </td>
                        <td style="width: 99px; height: 28px;" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 3px; height: 28px;" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 122px; height: 28px;" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 203px; height: 28px;" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 53px; height: 28px;" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 3px; height: 28px;">
                            &nbsp;
                        </td>
                        <td style="width: 187px; height: 28px;" align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 45px; height: 28px;">
                            &nbsp;
                        </td>
                        <td style="width: 99px; height: 28px;" align="left">
                            <asp:Label ID="lblChngDt" runat="server" Visible="False">Change Date</asp:Label>
                        </td>
                        <td style="width: 3px; height: 28px;" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 196px; height: 28px;" align="left">
                            <asp:TextBox ID="txtHolDate" runat="server" Visible="False"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtHolDtCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                TargetControlID="txtHolDate">
                            </cc1:CalendarExtender>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Enter valid date"
                                ControlToValidate="txtHolDate" ForeColor="Red" Operator="DataTypeCheck" Type="Date">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 203px">
                            &nbsp;<asp:HiddenField ID="hfHolidayRefNo" runat="server" Value="0" />
                            &nbsp;
                        </td>
                        <td colspan="7">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="8pt" ForeColor="Red"
                                ValidationGroup="Save" />
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboLoc" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboDept" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboSec" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <div align="center" style="background-color: #CCFFFF">
        <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#CC66FF"
                    BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px"
                    NextPrevFormat="FullMonth" OnPreRender="Calendar1_PreRender" Width="350px" OnSelectionChanged="Calendar1_SelectionChanged"
                    OnDayRender="Calendar1_DayRender" BorderStyle="Solid">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" BackColor="#00FFCC" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                    <TitleStyle BackColor="#CC99FF" Height="35" Font-Size="Large" Font-Names="Courier New Baltic" />
                </asp:Calendar>
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <br />
                <asp:Button ID="btnClearHoliday" runat="server" OnClick="btnClearHoliday_Click" Text="Clear"
                    Width="131px" />
                <asp:Button ID="btnSaveHoliday" runat="server" OnClick="btnSaveHoliday_Click" Text="Save"
                    ValidationGroup="Save" Width="131px" />
                <br />
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Calendar1" EventName="SelectionChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #CCFFFF">
                <br />
                Select Year:
                <asp:DropDownList ID="cboYear" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                <asp:GridView ID="gvHoliday" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvHoliday_RowDataBound" OnSelectedIndexChanged="gvHoliday_SelectedIndexChanged"
                    OnRowDeleting="gvHoliday_RowDeleting">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="HolRefNo" HeaderText="Ref. No" />
                        <asp:BoundField DataField="HolDate" DataFormatString="{0:d}" HeaderText="Date" />
                        <asp:BoundField DataField="HolDesc" HeaderText="Description" />
                        <asp:BoundField DataField="HolComRefNo" HeaderText="Company" />
                        <asp:BoundField DataField="HolDeptRefNo" HeaderText="Department" />
                        <asp:BoundField DataField="HolSecRefNo" HeaderText="Section" />
                        <asp:BoundField HeaderText="Remarks" DataField="HolRem" />
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
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboYear" EventName="SelectedIndexChanged" />
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
