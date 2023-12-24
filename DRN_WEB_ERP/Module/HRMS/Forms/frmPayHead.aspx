<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmPayHead.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmPayHead" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //Autocomplete Start
        function acePopulated(sender, e) {
            var behavior = $find('AutoCompletePayHead');
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

        function acePopulatedDpnd(sender, e) {
            var behavior = $find('AutoCompletePayHeadDpnd');
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
            var searchText = $get('<%=txtBaseValPayHead.ClientID %>').value;
            searchText = searchText.replace('null', '');
            sender.get_element().value = searchText + value;
        }

        function aceSelectedDpnd(sender, e) {
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
            var searchText = $get('<%=txtDpnd.ClientID %>').value;
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
        Pay Head Setup
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="border: 1px solid #CCCCCC; background-color: #CCFFFF; width: 40%;">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 42px">
                            &nbsp;
                        </td>
                        <td style="width: 143px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 216px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 42px">
                            &nbsp;
                        </td>
                        <td style="width: 143px">
                            Code
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 216px">
                            <asp:TextBox ID="txtPayCode" runat="server" CssClass="etc" MaxLength="10" Width="180px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPayCode"
                                ErrorMessage="Enter Pay Head Code First" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 42px">
                            &nbsp;
                        </td>
                        <td style="width: 143px">
                            Name
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 216px">
                            <asp:TextBox ID="txtPayName" runat="server" CssClass="etc" Width="180px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 42px">
                            &nbsp;
                        </td>
                        <td style="width: 143px">
                            Fixed Value
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 216px">
                            <asp:TextBox ID="txtBaseValFixedValue" runat="server" CssClass="etc" Width="180px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtBaseValFixedValue_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtBaseValFixedValue"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 42px">
                            &nbsp;
                        </td>
                        <td style="width: 143px" align="center">
                            Or
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 216px">
                            <asp:CheckBox ID="chkDepndVal" runat="server" Text="Dependent Value" AutoPostBack="True"
                                OnCheckedChanged="chkDepndVal_CheckedChanged" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 42px">
                            &nbsp;
                        </td>
                        <td style="width: 143px">
                            Dependent
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 216px">
                            <asp:TextBox ID="txtBaseValPayHead" runat="server" CssClass="etc" Width="180px" Enabled="False"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderPayHead" runat="server" BehaviorID="AutoCompletePayHead"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                OnClientItemSelected="aceSelected" OnClientPopulated="acePopulated" ServiceMethod="GetSrchPayHead"
                                ServicePath="~/Module/HRMS/Forms/wsAutoComHrms.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                TargetControlID="txtBaseValPayHead">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBaseValPayHead"
                                ErrorMessage="Enter Dependent Pay Head First" ForeColor="Red" ValidationGroup="chkDpnd">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtBaseValPayHead"
                                ErrorMessage="Enter Valid Pay Head" ForeColor="Red" ValidationGroup="chkDpnd"
                                OnServerValidate="CustomValidator1_ServerValidate">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 42px">
                            &nbsp;
                        </td>
                        <td style="width: 143px">
                            Operator
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 216px">
                            <asp:DropDownList ID="cboOperator" runat="server" Enabled="False" Width="50px" Font-Size="Medium">
                                <asp:ListItem Value="*">*</asp:ListItem>
                                <asp:ListItem Value="/">/</asp:ListItem>
                                <asp:ListItem Value="%">%</asp:ListItem>
                                <asp:ListItem Value="-">-</asp:ListItem>
                                <asp:ListItem Value="+">+</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 42px; height: 30px;">
                            &nbsp;
                        </td>
                        <td style="width: 143px; height: 30px;">
                            Value
                        </td>
                        <td style="width: 3px; height: 30px;">
                            :
                        </td>
                        <td style="width: 216px; height: 30px;">
                            <asp:TextBox ID="txtDpnd" runat="server" CssClass="etc" Width="180px" Enabled="False"></asp:TextBox>
                            <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                FilterType="Custom, Numbers" TargetControlID="txtDpnd" ValidChars=".">
                            </cc1:FilteredTextBoxExtender>--%>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderDpnd" runat="server" BehaviorID="AutoCompletePayHeadDpnd"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                OnClientItemSelected="aceSelectedDpnd" OnClientPopulated="acePopulatedDpnd" ServiceMethod="GetSrchPayHead"
                                ServicePath="~/Module/HRMS/Forms/wsAutoComHrms.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                TargetControlID="txtDpnd">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td style="height: 30px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDpnd"
                                ErrorMessage="Enter Value First" ForeColor="Red" ValidationGroup="chkDpnd">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtDpnd"
                                ErrorMessage="Enter Valid Pay Head" ForeColor="Red" ValidationGroup="chkDpnd"
                                OnServerValidate="CustomValidator3_ServerValidate">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 42px">
                            &nbsp;
                        </td>
                        <td style="width: 143px">
                            Accumulate
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 216px">
                            <asp:CheckBox ID="chkAccu" runat="server" AutoPostBack="True" OnCheckedChanged="chkAccu_CheckedChanged"
                                Text="No" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 42px">
                            &nbsp;
                        </td>
                        <td style="width: 143px">
                            Operator
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 216px">
                            <asp:RadioButtonList ID="optAccOpr" runat="server" Enabled="False" RepeatDirection="Horizontal"
                                Font-Size="Medium">
                                <asp:ListItem Value="+" Selected="True">+</asp:ListItem>
                                <asp:ListItem Value="-">-</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 42px">
                            &nbsp;
                        </td>
                        <td style="width: 143px">
                            Accumulator
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 216px">
                            <asp:TextBox ID="txtAccPayHead" runat="server" CssClass="etc" Width="180px" Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAccPayHead"
                                ErrorMessage="Enter Accumulator Pay Head First" ForeColor="Red" ValidationGroup="chkAcc">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtAccPayHead"
                                ErrorMessage="Enter Valid Pay Head" ForeColor="Red" OnServerValidate="CustomValidator2_ServerValidate"
                                ValidationGroup="chkAcc">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 42px">
                            &nbsp;
                        </td>
                        <td style="width: 143px">
                            Decimal Place
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 216px">
                            <asp:DropDownList ID="cboDesiPlace" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 42px">
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
                        <td style="width: 42px">
                            &nbsp;
                        </td>
                        <td align="center" colspan="3">
                            &nbsp; &nbsp;
                            <asp:Button ID="btnClear" runat="server" CssClass="btn" OnClick="btnClear_Click"
                                Text="Clear" Width="100px" />
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" OnClick="btnSave_Click" Text="Save"
                                ValidationGroup="Save" Width="100px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 42px">
                            &nbsp;
                        </td>
                        <td colspan="3">
                            &nbsp; &nbsp; &nbsp;
                            <asp:HiddenField ID="hfPayHeadRefNo" runat="server" Value="0" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="border: 1px solid #CCCCCC; background-color: #CCFFFF;
                width: 527px; position: absolute; float: right; top: 88px; left: 660px; height: 457px;
                width: 647px; overflow: auto;">
                <asp:GridView ID="gvPayHead" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvPayHead_RowDataBound" OnRowDeleting="gvPayHead_RowDeleting"
                    OnSelectedIndexChanged="gvPayHead_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="PayHeadRef" HeaderText="Ref. No" />
                        <asp:BoundField DataField="PayHeadCode" HeaderText="Code" />
                        <asp:BoundField DataField="PayHeadName" HeaderText="Name" />
                        <asp:BoundField DataField="PayHeadBaseVal" HeaderText="Base Value" />
                        <asp:BoundField DataField="PayHeadOpr" HeaderText="Opr." />
                        <asp:BoundField DataField="PayHeadDepnd" HeaderText="Dependent" />
                        <asp:BoundField DataField="PayHeadAccOpr" HeaderText="Accum." />
                        <asp:BoundField DataField="PayHeadAcc" HeaderText="Pay Head" />
                        <asp:BoundField DataField="PayHeadDecPlace" HeaderText="Decimal" />
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
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="10pt" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#CC99FF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </div>
            <div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="10pt" ForeColor="Red"
                    ValidationGroup="Save" />
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" Font-Size="10pt" ForeColor="Red"
                    ValidationGroup="FixedVal" />
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" Font-Size="10pt" ForeColor="Red"
                    ValidationGroup="chkDpnd" />
                <asp:ValidationSummary ID="ValidationSummary4" runat="server" Font-Size="10pt" ForeColor="Red"
                    ValidationGroup="chkAcc" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="chkDepndVal" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="chkAccu" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvPayHead" EventName="RowDataBound" />
            <asp:AsyncPostBackTrigger ControlID="gvPayHead" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvPayHead" EventName="RowDeleting" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
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
