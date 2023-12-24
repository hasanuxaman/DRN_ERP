<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmEmpLeave_Old.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmEmpLeave_Old" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //Autocomplete Start
        function acePopulated(sender, e) {
            var behavior = $find('AutoCompleteSrchLvEmp');
            var target = behavior.get_completionList();
            //        target.style.width = 'auto';
            if (behavior._currentPrefix != null) {
                var prefix = behavior._currentPrefix.toLowerCase();
                var i;
                for (i = 0; i < target.childNodes.length; i++) {
                    var sValue = target.childNodes[i].innerHTML.toLowerCase();
                    if (sValue.indexOf(prefix) != -1) {
                        var fstr = target.childNodes[i].innerHTML.substring(0, sValue.indexOf(prefix));
                        var pstr = target.childNodes[i].innerHTML.substring(fstr.length, fstr.length + prefix.length);
                        var estr = target.childNodes[i].innerHTML.substring(fstr.length + prefix.length, target.childNodes[i].innerHTML.length);
                        target.childNodes[i].innerHTML = "<div class='autocomplete-item'>" + fstr + '<B><font color="#990000">' + pstr + '</font></B>' + estr + "</div>";
                    }
                }
            }
        }

        function aceSelected(sender, e) {
            var value = e.get_value();
            if (!value) {
                if (e._item.parentElement && e._item.parentElement.tagName == "LI")
                    value = e._item.parentElement.attributes["_value"].value;
                else if (e._item.parentElement && e._item.parentElement.parentElement.tagName == "LI")
                    value = e._item.parentElement.parentElement.attributes["_value"].value;
                else if (e._item.parentNode && e._item.parentNode.tagName == "LI")
                    value = e._item.parentNode._value;
                else if (e._item.parentNode && e._item.parentNode.parentNode.tagName == "LI")
                    value = e._item.parentNode.parentNode._value;
                else value = "";
            }
            var searchText = '';
            var searchText = $get('<%=txtEmpName.ClientID %>').value;
            searchText = searchText.replace('null', '');
            sender.get_element().value = searchText + value;
        }
        //Autocomplete End    

        //Enable enter key for search textbox
        function clickButton(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {
                if (evt.keyCode == 13) {
                    bt.click();
                    return false;
                }
            }
        }                                              
    </script>
    <div align="center" style="background-color: #00FF99">
        Allocate Employee Leave
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="border: 1px solid #CCCCCC; width: 55%; height: 320px; background-color: #CCFFFF;">
                <table style="width: 100%; background-color: #CCFFFF;">
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
                        <td style="width: 136px">
                            Employee Name
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="txtEmpName" runat="server" CssClass="etc search" Width="350px" AutoPostBack="True"
                                OnTextChanged="txtEmpName_TextChanged"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderLvEmp" runat="server" BehaviorID="AutoCompleteSrchLvEmp"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                OnClientItemSelected="aceSelected" OnClientPopulated="acePopulated" ServiceMethod="GetSrchEmp"
                                ServicePath="~/Module/HRMS/Forms/wsAutoComHrms.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                TargetControlID="txtEmpName">
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
                        <td style="width: 136px">
                            Leave Type
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:DropDownList ID="cboLeaveType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboLeaveType_SelectedIndexChanged"
                                ValidationGroup="ChkData" Width="185px" CssClass="etc">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cboLeaveType"
                                ErrorMessage="Select Leave Type" ForeColor="Red" ValidationGroup="Save" InitialValue="0">*</asp:RequiredFieldValidator>
                            <asp:Label ID="lblLvBal" runat="server" Font-Size="8pt" ForeColor="Blue"></asp:Label>
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
                            Date From
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="txtLvFrmDt" runat="server" CssClass="etc"></asp:TextBox>
                            <asp:Image ID="imgStDt" runat="server" ImageUrl="~/Image/calendar.png" />
                            <cc1:CalendarExtender ID="txtDobCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                TargetControlID="txtLvFrmDt" PopupButtonID="imgStDt">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLvFrmDt"
                                ErrorMessage="Enter Date From" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtLvFrmDt"
                                ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck" Type="Date"
                                ValidationGroup="Save">*</asp:CompareValidator>
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
                            Date To
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="txtLvToDt" runat="server" CssClass="etc"></asp:TextBox>
                            <asp:Image ID="imgEndDt" runat="server" ImageUrl="~/Image/calendar.png" />
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                TargetControlID="txtLvToDt" PopupButtonID="imgEndDt">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLvToDt"
                                ErrorMessage="Enter Date To" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtLvToDt"
                                ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck" Type="Date"
                                ValidationGroup="Save">*</asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtLvToDt"
                                ErrorMessage="Date To should be after Date From" ForeColor="Red" Operator="GreaterThanEqual"
                                Type="Date" ValidationGroup="Save" ControlToCompare="txtLvFrmDt">*</asp:CompareValidator>
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
                            Resp. hand over to
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="txtRespHandedTo" runat="server" CssClass="etc search" Width="350px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteSrchLvPespHandedToEmp"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetSrchEmp" ServicePath="~/Module/HRMS/Forms/wsAutoComHrms.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtRespHandedTo">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRespHandedTo"
                                ErrorMessage="Enter Responsible Employee Name" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtRespHandedTo"
                                ErrorMessage="Enter Valid Responsible Employee" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"
                                ValidationGroup="Save">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                            Reason
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" rowspan="3" valign="top">
                            <asp:TextBox ID="txtLvCmnt" runat="server" CssClass="etc" Height="60px" TextMode="MultiLine"
                                Width="168px"></asp:TextBox>
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
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" OnClick="btnSave_Click" Text="Save"
                                ValidationGroup="Save" Width="100px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div style="border: 1px solid #CCCCCC; width: 527px; position: fixed; float: right;
                top: 88px; left: 820px; height: 320px; width: 487px; background-color: #CCFFFF;">
                <table style="width: 100%; background-color: #CCFFFF;">
                    <tr>
                        <td style="width: 55px">
                            &nbsp;
                        </td>
                        <td style="width: 148px">
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
                        <td style="width: 148px">
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
                        <td style="width: 55px">
                            &nbsp;
                        </td>
                        <td style="width: 148px">
                            Designation
                        </td>
                        <td style="width: 22px">
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblDesig" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 55px">
                            &nbsp;
                        </td>
                        <td style="width: 148px">
                            Department
                        </td>
                        <td style="width: 22px">
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblDept" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 55px">
                            &nbsp;
                        </td>
                        <td style="width: 148px">
                            Section
                        </td>
                        <td style="width: 22px">
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblSec" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 55px">
                            &nbsp;
                        </td>
                        <td style="width: 148px">
                            Shift
                        </td>
                        <td style="width: 22px">
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblShift" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 55px">
                            &nbsp;
                        </td>
                        <td style="width: 148px">
                            Supervisor
                        </td>
                        <td style="width: 22px">
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblSup" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <%--<div align="center">
                <asp:Panel ID="pnlPendLv" runat="server" GroupingText="Pending Leave List">
                    <asp:GridView ID="gvEmpPendLv" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" Font-Size="8pt">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField HeaderText="Date" />
                            <asp:BoundField HeaderText="Employee Name" />
                            <asp:BoundField HeaderText="Leave Type" />
                            <asp:BoundField HeaderText="Number of Days" />
                            <asp:BoundField HeaderText="Status" />
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
                </asp:Panel>
            </div>--%>
            <div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="10pt" ForeColor="Red"
                    ValidationGroup="Save" />
            </div>
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <br />
                <asp:Panel ID="pnlEmpAppLv" runat="server" GroupingText="Leave List">
                    <br />
                    <asp:GridView ID="gvEmpApprLv" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" Font-Size="8pt" OnRowDeleting="gvEmpApprLv_RowDeleting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfLvRef" runat="server" Value='<%# Bind("LvDetRef") %>' />
                                    <asp:HiddenField ID="hfEmpRef" runat="server" Value='<%# Bind("EmpRefNo") %>' />
                                    <asp:Label ID="lblLvDt" runat="server" Text='<%# GetLvDt(Eval("LvDetRef").ToString())%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:Label ID="lvEmpName" runat="server" Text='<%# GetEmpName(Eval("EmpRefNo").ToString())%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Leave Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblLvType" runat="server" Text='<%# GetLvType(int.Parse(Eval("LvDetLvType").ToString()))%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Leave Days">
                                <ItemTemplate>
                                    <asp:Label ID="lblLvDays" runat="server" Text='<%# Bind("LvDetDays") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("LvDetStatus") %>'></asp:Label>
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
            <asp:AsyncPostBackTrigger ControlID="cboLeaveType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="txtEmpName" EventName="TextChanged" />
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
