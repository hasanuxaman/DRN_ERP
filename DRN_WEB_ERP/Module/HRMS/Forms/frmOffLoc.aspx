<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmOffLoc.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmOffLoc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Create Company
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center">
                <table style="width: 100%; background-color: #CCFFFF">
                    <tr align="left">
                        <td style="width: 147px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 53px" align="left">
                            Code
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 187px" align="left">
                            <asp:TextBox ID="txtLocCode" runat="server" Width="180px" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLocCode"
                                ErrorMessage="Enter Company Code" ForeColor="Red" ValidationGroup="SaveLoc">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="left" style="width: 28px">
                            &nbsp;
                        </td>
                        <td style="width: 99px;" align="left">
                            Phone
                        </td>
                        <td style="width: 3px;" align="left">
                            :
                        </td>
                        <td style="width: 185px;" align="left">
                            <asp:TextBox runat="server" ID="txtLocPhone" Width="180px"></asp:TextBox>
                        </td>
                        <td style="width: 122px;" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 147px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 53px" align="left">
                            Name
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 187px" align="left">
                            <asp:TextBox ID="txtLocName" runat="server" Width="180px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLocName"
                                ErrorMessage="Enter Company Name" ForeColor="Red" ValidationGroup="SaveLoc">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="left" style="width: 28px">
                            &nbsp;
                        </td>
                        <td style="width: 99px" align="left">
                            Fax
                        </td>
                        <td style="width: 3px" align="left">
                            :
                        </td>
                        <td style="width: 185px" align="left">
                            <asp:TextBox runat="server" ID="txtLocFax" Width="180px"></asp:TextBox>
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 147px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 53px" align="left">
                            Address
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 187px" align="left" rowspan="3" valign="top">
                            <asp:TextBox runat="server" ID="txtLocAddr" Height="79px" TextMode="MultiLine" Width="180px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 28px">
                            &nbsp;
                        </td>
                        <td style="width: 99px" align="left">
                            E-Mail
                        </td>
                        <td style="width: 3px" align="left">
                            :
                        </td>
                        <td style="width: 185px" align="left">
                            <asp:TextBox runat="server" ID="txtLocEmail" Width="180px"></asp:TextBox>
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 147px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 53px" align="left">
                            &nbsp;
                        </td>
                        <td style="3px: ;">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 28px">
                            &nbsp;
                        </td>
                        <td style="width: 99px" align="left">
                            Opening Date
                        </td>
                        <td style="width: 3px" align="left">
                            :
                        </td>
                        <td style="width: 185px" align="left">
                            <asp:TextBox runat="server" ID="txtOpnDate" Width="180px"></asp:TextBox>
                            <asp:Image ID="imgLocOpnDt" runat="server" ImageUrl="~/Image/calendar.png" />
                            <cc1:CalendarExtender ID="txtLocCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                TargetControlID="txtOpnDate" PopupButtonID="imgLocOpnDt">
                            </cc1:CalendarExtender>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtOpnDate"
                                ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck" Type="Date"
                                ValidationGroup="SaveLoc">*</asp:CompareValidator>
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 147px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 53px" align="left">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left" style="width: 28px">
                            &nbsp;
                        </td>
                        <td style="width: 99px" align="left">
                            Remarks
                        </td>
                        <td style="width: 3px" align="left">
                            :
                        </td>
                        <td style="width: 185px" align="left">
                            <asp:TextBox runat="server" ID="txtLocRem" Width="180px"></asp:TextBox>
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 147px">
                            <asp:HiddenField ID="hfLocRefNo" runat="server" Value="0" />
                        </td>
                        <td align="left" colspan="4">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="8pt" ForeColor="Red"
                                ValidationGroup="SaveLoc" />
                        </td>
                        <td style="width: 99px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 3px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 185px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 147px">
                            &nbsp;
                        </td>
                        <td align="center" colspan="7">
                            &nbsp; &nbsp;<asp:Button ID="btnClearComp" runat="server" Text="Clear" Width="131px"
                                OnClick="btnClearComp_Click" />
                            &nbsp;<asp:Button ID="btnSaveCom" runat="server" Text="Save" ValidationGroup="SaveLoc"
                                OnClick="btnSaveCom_Click" Width="131px" />
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="background-color: #CCFFFF">
                <br />
                <asp:GridView ID="gvLoc" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvLoc_RowDataBound" OnSelectedIndexChanged="gvLoc_SelectedIndexChanged"
                    OnRowDeleting="gvLoc_RowDeleting">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="LocRefNo" HeaderText="Ref. No" />
                        <asp:BoundField DataField="LocCode" HeaderText="Code" />
                        <asp:BoundField DataField="LocName" HeaderText="Name" />
                        <asp:BoundField DataField="LocAddr" HeaderText="Address" />
                        <asp:BoundField DataField="LocPhone" HeaderText="Phone" />
                        <asp:BoundField DataField="LocFax" HeaderText="Fax" />
                        <asp:BoundField DataField="LocEmail" HeaderText="E-Mail" />
                        <asp:BoundField DataField="LocOpnDate" DataFormatString="{0:d}" HeaderText="Opening Date" />
                        <asp:BoundField DataField="LocRem" HeaderText="Remarks" />
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
            <asp:AsyncPostBackTrigger ControlID="gvLoc" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvLoc" EventName="RowDeleting" />
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
