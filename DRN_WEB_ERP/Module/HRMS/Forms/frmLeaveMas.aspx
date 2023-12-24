<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="frmLeaveMas.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmLeaveMas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Create Leave Type
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <div>
                    <table style="width: 100%; background-color: #CCFFFF">
                        <tr>
                            <td style="width: 28px">
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
                            <td style="width: 28px">
                                &nbsp;
                            </td>
                            <td>
                                Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtLvMasCode" runat="server" MaxLength="10" Width="180px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLvMasCode"
                                    ErrorMessage="Enter Leave Code" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtLvMasName" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtLvMasName"
                                    ErrorMessage="Enter Leave Name" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Desctiption
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtLvMasDesc" runat="server" MaxLength="50" Width="280px"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <table style="width: 100%; background-color: #CCFFFF">
                    <tr align="left">
                        <td align="left" style="width: 107px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 33px">
                            &nbsp;
                        </td>
                        <td style="width: 219px;" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 3px;" align="left">
                            &nbsp;
                        </td>
                        <td align="left" colspan="3">
                            &nbsp;
                        </td>
                        <td align="left" width="2">
                            &nbsp;
                        </td>
                        <td style="width: 79px;" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 122px;" align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 122px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" style="width: 107px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 33px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 219px;">
                            Pay Type
                        </td>
                        <td align="left" style="width: 3px;">
                            :
                        </td>
                        <td align="left" colspan="3">
                            <asp:RadioButtonList ID="optListPayType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="1">Full</asp:ListItem>
                                <asp:ListItem Value="2">Half</asp:ListItem>
                                <asp:ListItem Value="3">No Pay</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left" width="2">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px;">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 122px;">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 122px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" style="width: 107px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 33px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 219px;">
                            Max Days Per Year
                        </td>
                        <td align="left" style="width: 3px;">
                            :
                        </td>
                        <td align="left" style="width: 53px;">
                            <asp:TextBox ID="txtLvMasMaxDays" runat="server" CssClass="textAlignCenter" Width="90px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                FilterType="Custom, Numbers" TargetControlID="txtLvMasMaxDays" ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLvMasMaxDays"
                                ErrorMessage="Enter Max Days Allowed Per Anumn." ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtLvMasMaxDays"
                                ErrorMessage="Enter Valid Number" ForeColor="Red" Operator="DataTypeCheck" Type="Currency"
                                ValidationGroup="Save">*</asp:CompareValidator>
                        </td>
                        <td align="left" style="width: 158px;">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 87px;">
                            Day Value
                        </td>
                        <td align="left" width="2">
                            :
                        </td>
                        <td align="left" style="width: 79px;">
                            <asp:TextBox ID="txtLvDayVal" runat="server" CssClass="textAlignCenter" ToolTip="for full day 1, half day 0.5 etc"
                                Width="90px">1.00</asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtLvDayVal_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtLvDayVal" ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td align="left" style="width: 122px;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLvDayVal"
                                ErrorMessage="Enter Day Value. (e.g. 1 for full day, 0.5 for half day)" ForeColor="Red"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtLvDayVal"
                                ErrorMessage="Enter Valid Number" ForeColor="Red" Operator="DataTypeCheck" Type="Currency"
                                ValidationGroup="Save">*</asp:CompareValidator>
                        </td>
                        <td align="left" style="width: 122px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" style="width: 107px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 33px">
                            &nbsp;
                        </td>
                        <td style="width: 219px;" align="left">
                            Holiday Consideration
                        </td>
                        <td style="width: 3px;" align="left">
                            :
                        </td>
                        <td style="width: 53px;" align="left">
                            <asp:CheckBox ID="chkHolDayConsider" runat="server" AutoPostBack="True" OnCheckedChanged="chkHolDayConsider_CheckedChanged"
                                Text="No" />
                        </td>
                        <td style="width: 158px;" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 87px;" align="left">
                            Max Days
                        </td>
                        <td align="left" width="2">
                            :
                        </td>
                        <td style="width: 79px;" align="left">
                            <asp:TextBox ID="txtHolDayCons" runat="server" CssClass="textAlignCenter" Enabled="False"
                                Width="90px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                FilterType="Custom, Numbers" TargetControlID="txtHolDayCons" ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="width: 122px;" align="left">
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Enter Max. Holiday Consideration Days"
                                ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="Save">*</asp:CustomValidator>
                        </td>
                        <td align="left" style="width: 122px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" style="width: 107px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 33px">
                            &nbsp;
                        </td>
                        <td style="width: 219px" align="left">
                            Carry Forward To Next Year
                        </td>
                        <td style="width: 3px" align="left">
                            :
                        </td>
                        <td style="width: 53px" align="left">
                            <asp:CheckBox ID="chkCaryFwd" runat="server" AutoPostBack="True" OnCheckedChanged="chkCaryFwd_CheckedChanged"
                                Text="No" />
                        </td>
                        <td style="width: 158px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 87px" align="left">
                            Max. Days
                        </td>
                        <td align="left" width="2">
                            :
                        </td>
                        <td style="width: 79px" align="left">
                            <asp:TextBox ID="txtMaxCaryFwdDays" runat="server" CssClass="textAlignCenter" Enabled="False"
                                Width="90px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                FilterType="Custom, Numbers" TargetControlID="txtMaxCaryFwdDays" ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="width: 122px" align="left">
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Enter Max. Carry Forward Days"
                                ForeColor="Red" OnServerValidate="CustomValidator2_ServerValidate" ValidationGroup="Save">*</asp:CustomValidator>
                        </td>
                        <td align="left" style="width: 122px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" style="width: 107px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 33px">
                            &nbsp;
                        </td>
                        <td style="width: 219px" align="left">
                            Maternity Type Leave
                        </td>
                        <td style="width: 3px" align="left">
                            :
                        </td>
                        <td style="width: 53px" align="left">
                            <asp:CheckBox ID="chkMatrnType" runat="server" AutoPostBack="True" OnCheckedChanged="chkMatrnType_CheckedChanged"
                                Text="No" />
                        </td>
                        <td style="width: 158px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 87px" align="left">
                            Max. Times
                        </td>
                        <td align="left" width="2">
                            :
                        </td>
                        <td style="width: 79px" align="left">
                            <asp:TextBox ID="txtMaxMtrn" runat="server" CssClass="textAlignCenter" Enabled="False"
                                Width="90px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                FilterType="Custom, Numbers" TargetControlID="txtMaxMtrn" ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="width: 122px" align="left">
                            <asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage="Enter Max. Times of Maternity Allowed"
                                ForeColor="Red" OnServerValidate="CustomValidator3_ServerValidate" ValidationGroup="Save">*</asp:CustomValidator>
                        </td>
                        <td align="left" style="width: 122px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" style="width: 107px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 33px">
                            &nbsp;
                        </td>
                        <td style="width: 219px" align="left">
                            Eligible for All Job Status
                        </td>
                        <td style="width: 3px" align="left">
                            :
                        </td>
                        <td style="width: 53px" align="left">
                            <asp:CheckBox ID="chkEligibility" runat="server" AutoPostBack="True" OnCheckedChanged="chkEligibility_CheckedChanged"
                                Text="Yes" Checked="True" />
                        </td>
                        <td style="width: 158px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 87px" align="left">
                            Job Status
                        </td>
                        <td align="left" width="2">
                            :
                        </td>
                        <td style="width: 79px" align="left">
                            <asp:DropDownList ID="cboJobStat" runat="server" Width="95px" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 122px" align="left">
                            <asp:CustomValidator ID="CustomValidator4" runat="server" ErrorMessage="Select Job Status"
                                ForeColor="Red" OnServerValidate="CustomValidator4_ServerValidate" ValidationGroup="Save">*</asp:CustomValidator>
                        </td>
                        <td align="left" style="width: 122px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" style="width: 107px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 33px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 219px">
                            Eligible after Service Period
                        </td>
                        <td align="left" style="width: 3px">
                            :
                        </td>
                        <td align="left" style="width: 53px">
                            <asp:TextBox ID="txtMinSrvcDays" runat="server" CssClass="textAlignCenter" Width="90px">1</asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtMinSrvcDays_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtMinSrvcDays"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMinSrvcDays"
                                ErrorMessage="Enter Eligible Servise Days" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="left" style="width: 158px">
                            (Days)
                        </td>
                        <td align="left" style="width: 87px">
                            Emp. Type
                        </td>
                        <td align="left" width="2">
                            :
                        </td>
                        <td align="left" style="width: 79px">
                            <asp:DropDownList ID="cboEmpType" runat="server" Width="95px">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 122px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 122px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left" style="width: 107px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 33px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 219px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 3px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 53px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 158px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 87px">
                            &nbsp;
                        </td>
                        <td align="left" width="2">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 122px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 122px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 107px">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td style="width: 219px" align="left">
                            <asp:HiddenField ID="hfLvMasRefNo" runat="server" Value="0" />
                        </td>
                        <td style="width: 3px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 53px" align="left">
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="131px" />
                        </td>
                        <td style="width: 158px" align="left">
                            &nbsp;
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" ValidationGroup="Save"
                                Width="131px" />
                        </td>
                        <td style="width: 87px" align="left">
                            &nbsp;
                        </td>
                        <td align="left" width="2">
                            &nbsp;
                        </td>
                        <td style="width: 79px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 122px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 107px">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left" colspan="7">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="8pt" ForeColor="Red"
                                ValidationGroup="Save" />
                        </td>
                        <td align="left" style="width: 122px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 122px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 107px">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 219px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 3px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 53px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 158px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 87px">
                            &nbsp;
                        </td>
                        <td align="left" width="2">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 122px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 122px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="chkCaryFwd" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="chkHolDayConsider" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="chkMatrnType" EventName="CheckedChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <div align="center" style="background-color: #CCFFFF">
        <br />
        <asp:GridView ID="gvLeaveMas" runat="server" AutoGenerateColumns="False" CellPadding="4"
            Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvLeaveMas_RowDataBound"
            OnSelectedIndexChanged="gvLeaveMas_SelectedIndexChanged" OnRowDeleting="gvLeaveMas_RowDeleting">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="LvMasRefNo" HeaderText="Ref. No">
                    <ItemStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="LvMasCode" HeaderText="Code">
                    <ItemStyle Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="LvMasName" HeaderText="Name">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="LvMasMaxDays" HeaderText="Max. Days">
                    <ItemStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="LvMasDayVal" HeaderText="Day Value">
                    <ItemStyle Width="50px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Pay Type">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfPayType" runat="server" Value='<%# Bind("LvMasPayTypeRef") %>' />
                        <asp:Label ID="lblPayType" runat="server" Text='<%# Bind("LvMasPayType") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Emp. Type">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfEmpType" runat="server" Value='<%# Bind("LvMasExtData1") %>' />
                        <asp:Label ID="lblEmpType" runat="server" Text='<%# Bind("LvMasExtData1") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Holiday Consider">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfHolDayCons" runat="server" Value='<%# Bind("LvMasHolDayCons") %>' />
                        <asp:Label ID="lblHolDayCons" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>
                <asp:BoundField DataField="LvMasMaxHolDayCons" HeaderText="Max. Cons. Days">
                    <ItemStyle Width="50px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Carry Forward">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfCryFwd" runat="server" Value='<%# Bind("LvMasCryFwd") %>' />
                        <asp:Label ID="lblCryFwd" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>
                <asp:BoundField DataField="LvMasMaxCryFwdDays" HeaderText="Max. Carry Days">
                    <ItemStyle Width="50px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Maternity Type">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfMtrnType" runat="server" Value='<%# Bind("LvMasMtrnType") %>' />
                        <asp:Label ID="lblMtrnType" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>
                <asp:BoundField DataField="LvMasMaxMtrnTimes" HeaderText="Max. Times">
                    <ItemStyle Width="50px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Eligible Job Status">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfLvJobStat" runat="server" Value='<%# Bind("LvMasExtData2") %>' />
                        <asp:Label ID="lblLvJobStat" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Min. Service Days">
                    <ItemTemplate>
                        <asp:Label ID="lblMinSerDays" runat="server" Text='<%# Bind("LvMasExtData3") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgBtnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                            ImageUrl="~/Image/Delete.png" OnClientClick="return confirm('Do you want to delete?')"
                            ToolTip="Delete" />
                    </ItemTemplate>
                    <ItemStyle Width="20px" />
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
