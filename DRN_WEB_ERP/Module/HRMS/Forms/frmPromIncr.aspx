<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmPromIncr.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmPromIncr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CalcIncrPrec(NewGrss, PresGrss, IncrPerc) {
            var NewGross = 0;
            if (document.getElementById(NewGrss).value != '') {
                NewGross = parseFloat(document.getElementById(NewGrss).value);
            }
            //alert(NewGross.toString());

            var PresGross = 0;
            if (document.getElementById(PresGrss).value != '') {
                PresGross = parseFloat(document.getElementById(PresGrss).value);
            }
            //alert(PresGross.toString());

            var IncrPrecent = document.getElementById(IncrPerc);
            var IncreamentPercentage = parseFloat(((NewGross - PresGross) / PresGross) * 100);
            IncrPrecent.value ="Increament: " + IncreamentPercentage.toFixed(2) + "%";            
            //alert(IncreamentPercentage.toString());
        }
    </script>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #00FF99">
                Promotion / Increament Entry</div>
            <div style="border: 1px solid #CCCCCC; width: 55%; height: 320px; background-color: #CCFFFF;">
                <table style="width: 100%; background-color: #CCFFFF; font-size: medium;">
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 300px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td style="width: 136px; font-size: 12px;">
                            Employee Name
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="txtEmpName" runat="server" CssClass="etc search" Width="350px" AutoPostBack="True"
                                OnTextChanged="txtEmpName_TextChanged"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderEmp" runat="server" BehaviorID="AutoCompleteSrchEmp"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetSrchEmp" ServicePath="~/Module/HRMS/Forms/wsAutoComHrms.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtEmpName">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEmpName"
                                ErrorMessage="Enter Employee Name" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtEmpName"
                                ErrorMessage="Enter Valid Employee" ForeColor="Red" OnServerValidate="CustomValidator2_ServerValidate"
                                ValidationGroup="Save">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td style="width: 136px; font-size: 12px;">
                            Type
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:DropDownList ID="cboType" runat="server" AutoPostBack="True" ValidationGroup="ChkData"
                                Width="185px" CssClass="etc" OnSelectedIndexChanged="cboType_SelectedIndexChanged">
                                <asp:ListItem Value="1">Increament</asp:ListItem>
                                <asp:ListItem Value="2">Promotion</asp:ListItem>
                                <%--<asp:ListItem Value="3">Increament with Promotion</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px;">
                            &nbsp;
                        </td>
                        <td style="width: 136px; font-size: 12px;">
                            <asp:Label ID="lblPresGross" runat="server" Text="Present Gross Salary"></asp:Label>
                            <br />
                            <asp:Label ID="lblPresDesig" runat="server" Text="Present Position" Visible="False"></asp:Label>
                        </td>
                        <td style="width: 3px;">
                            :
                        </td>
                        <td style="width: 300px;">
                            <asp:TextBox ID="lblPresGrossAmt" runat="server" Font-Size="9pt" Text="0.00" 
                                CssClass="etc" Enabled="False" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="lblPresDesignation" runat="server" Font-Size="9pt" Visible="False"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px; height: 28px;">
                            &nbsp;
                        </td>
                        <td style="width: 136px; height: 28px; font-size: 12px;">
                            <asp:Label ID="lblIncr" runat="server" Text="New Gross Amount"></asp:Label>
                            <br />
                            <asp:Label ID="lblProm" runat="server" Text="New Position" Visible="False"></asp:Label>
                        </td>
                        <td style="width: 3px; height: 28px;">
                            :
                        </td>
                        <td style="width: 300px; height: 28px;">
                            <asp:TextBox ID="txtIncrAmt" runat="server" CssClass="etc" TextMode="Number"></asp:TextBox>
                            <asp:DropDownList ID="cboDesig" runat="server" CssClass="etc" Visible="False" Width="360px">
                            </asp:DropDownList>
                            <asp:TextBox ID="lblIncrPrec" runat="server" Font-Size="9pt" 
                                Text="Increament: 0.00%" Enabled="False" ReadOnly="True" 
                                CssClass="textAlignCenter etc"></asp:TextBox>
                        </td>
                        <td style="height: 28px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td style="width: 136px; font-size: 12px;">
                            Effective Date
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="txtEffectDt" runat="server" CssClass="etc"></asp:TextBox>
                            <asp:Image ID="imgEffectDt" runat="server" ImageUrl="~/Image/calendar.png" />
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                TargetControlID="txtEffectDt" PopupButtonID="imgEffectDt">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEffectDt"
                                ErrorMessage="Enter Effective Date" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtEffectDt"
                                ErrorMessage="Enter Valid Effective Date" ForeColor="Red" Operator="DataTypeCheck"
                                Type="Date" ValidationGroup="Save">*</asp:CompareValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td style="width: 136px; font-size: 12px;">
                            Remarks
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" rowspan="3" valign="top">
                            <asp:TextBox ID="txtRem" runat="server" CssClass="etc" Height="60px" TextMode="MultiLine"
                                Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td colspan="3">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 300px">
                            <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="Clear" Width="100px"
                                OnClick="btnClear_Click" />
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" ValidationGroup="Save"
                                Width="100px" OnClick="btnSave_Click" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div style="border: 1px solid #CCCCCC; width: 527px; position: absolute; float: right;
                top: 88px; left: 820px; height: 320px; width: 487px; background-color: #CCFFFF;">
                <table style="width: 100%; background-color: #CCFFFF;">
                    <tr>
                        <td style="width: 55px">
                            &nbsp;
                        </td>
                        <td style="width: 64px">
                            &nbsp;
                        </td>
                        <td style="width: 22px">
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
                        <td align="center" colspan="5">
                            <asp:HyperLink ID="hlEmpPic" Target="_blank" runat="server">
                                <asp:Image ID="imgEmp" runat="server" Height="120px" Width="120px" ImageUrl="~/Image/NoImage.gif"
                                    BorderStyle="Ridge" BorderColor="White" BorderWidth="5" />
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 55px">
                            &nbsp;
                        </td>
                        <td style="width: 64px; font-size: 9px;">
                        </td>
                        <td style="width: 22px">
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
                        <td style="width: 55px">
                            &nbsp;
                        </td>
                        <td style="width: 64px; font-size: 9px;">
                            Designation
                        </td>
                        <td style="width: 22px">
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblDesig" runat="server" Font-Size="8pt"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 55px">
                            &nbsp;
                        </td>
                        <td style="width: 64px; font-size: 9px;">
                            Department
                        </td>
                        <td style="width: 22px">
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblDept" runat="server" Font-Size="8pt"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 55px">
                            &nbsp;
                        </td>
                        <td style="width: 64px; font-size: 9px;">
                            Section
                        </td>
                        <td style="width: 22px">
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblSec" runat="server" Font-Size="8pt"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 55px; height: 22px;">
                            &nbsp;
                        </td>
                        <td style="width: 64px; font-size: 9px;">
                            Shift
                        </td>
                        <td style="width: 22px; height: 22px;">
                            :
                        </td>
                        <td style="height: 22px">
                            <asp:Label ID="lblShift" runat="server" Font-Size="8pt"></asp:Label>
                        </td>
                        <td style="height: 22px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 55px">
                            &nbsp;
                        </td>
                        <td style="width: 64px; font-size: 9px;">
                            Supervisor
                        </td>
                        <td style="width: 22px">
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblSup" runat="server" Font-Size="8pt"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="10pt" ForeColor="Red"
                    ValidationGroup="Save" />
            </div>
            <div align="center" style="background-color: #CCFFFF">
                <br />
                <asp:GridView ID="gvEmpServiceLog" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333" AllowPaging="True" OnPageIndexChanging="gvDept_PageIndexChanging"
                    PageSize="25">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="EmpRefNo" HeaderText="Emp. ID" />
                        <asp:BoundField DataField="EmpRefNo" HeaderText="Name" />
                        <asp:BoundField DataField="EmpServLogType" HeaderText="Promotion / Increament" />
                        <asp:BoundField DataField="EmplSalary" HeaderText="Amount" />
                        <asp:BoundField DataField="EmpServLogExtData1" HeaderText="Designation" />
                        <asp:BoundField DataField="EmpServLogDate" DataFormatString="{0:d}" HeaderText="Effective Date" />
                        <asp:BoundField DataField="EmpServLogExtData1" HeaderText="Remarks" />
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
                    <PagerSettings Position="TopAndBottom" />
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
            <%--<asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />--%>
            <%--<asp:AsyncPostBackTrigger ControlID="gvEmpServiceLog" EventName="SelectedIndexChanged" />--%>
            <%--<asp:AsyncPostBackTrigger ControlID="gvEmpServiceLog" EventName="RowDeleting" />--%>
            <%--<asp:AsyncPostBackTrigger ControlID="btnSaveCom" EventName="Click" />--%>
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
