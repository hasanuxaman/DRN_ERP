<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmShift.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmShift" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Create Shift</div>
    <div align="center">
        <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%; background-color: #CCFFFF">
                    <tr align="left">
                        <td style="width: 147px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 86px" align="left">
                            Section</td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 187px" align="left">
                            <asp:DropDownList ID="cboSection" runat="server" Width="180px">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 81px">
                            &nbsp;</td>
                        <td style="width: 123px;" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 3px;" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 204px;" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 122px;" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 147px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 86px" align="left">
                            Code</td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 187px" align="left">
                            <asp:TextBox ID="txtShiftCode" runat="server" MaxLength="10" Width="180px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtShiftCode"
                                ErrorMessage="Enter Shift Code" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="left" style="width: 81px">
                            &nbsp;</td>
                        <td style="width: 123px;" align="left">
                            Start Time</td>
                        <td style="width: 3px;" align="left">
                            :&nbsp;
                        </td>
                        <td style="width: 204px;" align="left">
                            <asp:TextBox runat="server" ID="txtShiftStart" Width="180px" CssClass="textAlignCenter"
                                TextMode="Time" AutoPostBack="True" OnTextChanged="txtShiftStart_TextChanged"></asp:TextBox>
                            <%--<asp:RegularExpressionValidator ID="rev" runat="server" ErrorMessage="InvalidTime"
                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="txtShiftStart" ValidationExpression="^([ 01]?[0-9]2[0-3])(:([0-5][0-9]))?$"
                        Text="*">
                    </asp:RegularExpressionValidator>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtShiftStart"
                                ErrorMessage="Enter Start Time" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 122px;" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 147px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 86px" align="left">
                            Name</td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 187px" align="left">
                            <asp:TextBox ID="txtShiftName" runat="server" Width="180px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtShiftName"
                                ErrorMessage="Enter Shift Name" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="left" style="width: 81px">
                            &nbsp;</td>
                        <td style="width: 123px;" align="left">
                            End Time</td>
                        <td style="width: 3px;" align="left">
                            :
                        </td>
                        <td style="width: 204px;" align="left">
                            <asp:TextBox runat="server" ID="txtShiftEnd" Width="180px" CssClass="textAlignCenter"
                                TextMode="Time" AutoPostBack="True" OnTextChanged="txtShiftEnd_TextChanged"></asp:TextBox>
                            <%--<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptAMPM="true"
                        Mask="99:99" MaskType="Time" MessageValidatorTip="true" TargetControlID="txtShiftEnd"
                        AutoCompleteValue="00:00 AM">
                    </cc1:MaskedEditExtender>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtShiftEnd"
                                ErrorMessage="Enter End Time" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 122px;" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 147px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 86px" align="left">
                            Description</td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 187px" align="left">
                            <asp:TextBox ID="txtShiftDesc" runat="server" Width="180px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 81px">
                            &nbsp;</td>
                        <td style="width: 123px" align="left">
                            Total Time (hr)</td>
                        <td style="width: 3px" align="left">
                            :
                        </td>
                        <td style="width: 204px" align="left">
                            <asp:TextBox ID="txtShiftTotal" runat="server" Width="180px" CssClass="textAlignCenter"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 147px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 86px" align="left">
                            Remarks</td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 187px" align="left">
                            <asp:TextBox runat="server" ID="txtShiftRem" Width="180px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 81px">
                            &nbsp;</td>
                        <td style="width: 123px" align="left">
                            Grace Time (min)
                        </td>
                        <td style="width: 3px" align="left">
                            :
                        </td>
                        <td style="width: 204px" align="left">
                            <asp:TextBox runat="server" ID="txtShiftGrace" Width="180px" CssClass="textAlignCenter"
                                TextMode="Number"></asp:TextBox>
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 147px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 86px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 187px" align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 81px">
                            &nbsp;</td>
                        <td style="width: 123px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 3px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 204px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 147px">
                            <asp:HiddenField ID="hfShiftRefNo" runat="server" Value="0" />
                        </td>
                        <td align="left" colspan="4">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="8pt" ForeColor="Red"
                                ValidationGroup="Save" />
                        </td>
                        <td style="width: 123px" align="left">
                            &nbsp;</td>
                        <td style="width: 3px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 204px" align="left">
                            &nbsp;</td>
                        <td style="width: 122px" align="left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 147px">
                            &nbsp;
                        </td>
                        <td align="center" colspan="7">
                            &nbsp; &nbsp;<asp:Button ID="btnClearShift" runat="server" Text="Clear" Width="131px"
                                OnClick="btnClearShift_Click" />
                            &nbsp;<asp:Button ID="btnSaveShift" runat="server" Text="Save" ValidationGroup="Save"
                                OnClick="btnSaveShift_Click" Width="131px" />
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSaveShift" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="txtShiftStart" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtShiftEnd" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div align="center" style="background-color: #CCFFFF">
        <br />
        <asp:GridView ID="gvShift" runat="server" AutoGenerateColumns="False" CellPadding="4"
            Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvShift_RowDataBound" OnSelectedIndexChanged="gvShift_SelectedIndexChanged"
            OnRowDeleting="gvShift_RowDeleting">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ShiftRefNo" HeaderText="Ref. No" />
                <asp:BoundField DataField="SecRefNo" HeaderText="Section" />
                <asp:BoundField DataField="ShiftCode" HeaderText="Code" />
                <asp:BoundField DataField="ShiftName" HeaderText="Name" />
                <asp:BoundField DataField="ShiftDesc" HeaderText="Description" />
                <asp:BoundField DataField="ShiftRem" HeaderText="Remarks" />
                <asp:BoundField DataField="ShiftStart" HeaderText="Start Time" />
                <asp:BoundField DataField="ShiftEnd" HeaderText="End Time" />
                <asp:BoundField DataField="ShiftTotal" HeaderText="Total (hr)" />
                <asp:BoundField DataField="ShiftStartAdd" HeaderText="Grace Time (min)" />
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
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                Font-Size="10pt" />
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
