<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmGradeDet.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmGradeDet" %>

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
            var searchText = $get('<%=txtPayHead.ClientID %>').value;
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
        Grade Details Setup
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="border: 1px solid #CCCCCC; width: 100%; background-color: #CCFFFF;">
                <asp:Panel ID="pnlGrdHdr" runat="server" GroupingText="">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 323px">
                                &nbsp;
                            </td>
                            <td style="width: 52px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 269px">
                                &nbsp;
                            </td>
                            <td style="width: 24px">
                                &nbsp;
                            </td>
                            <td style="width: 225px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 323px">
                                &nbsp;
                            </td>
                            <td style="width: 52px">
                                Grade
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 269px">
                                <asp:DropDownList ID="cboGrade" runat="server" Width="250px" CssClass="etc" AutoPostBack="True"
                                    OnSelectedIndexChanged="cboGrade_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 24px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboGrade"
                                    ErrorMessage="Select Grade Code" ForeColor="Red" ValidationGroup="Save" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                            <td align="center" style="width: 225px">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 323px">
                                &nbsp;
                            </td>
                            <td style="width: 52px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 269px">
                                &nbsp;
                            </td>
                            <td style="width: 24px">
                                &nbsp;
                            </td>
                            <td align="center" style="width: 225px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div style="border: 1px solid #CCCCCC; width: 100%; background-color: #CCFFFF;">
                <asp:Panel ID="pnlGrdDet" runat="server" GroupingText="">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 23px">
                                &nbsp;
                            </td>
                            <td style="width: 88px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 262px">
                                &nbsp;
                            </td>
                            <td style="width: 16px">
                                &nbsp;
                            </td>
                            <td style="width: 45px">
                                &nbsp;
                            </td>
                            <td style="width: 4px">
                                &nbsp;
                            </td>
                            <td style="width: 159px">
                                &nbsp;
                            </td>
                            <td style="width: 137px">
                                &nbsp;
                            </td>
                            <td style="width: 68px">
                                &nbsp;
                            </td>
                            <td style="width: 225px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 23px">
                                &nbsp;
                            </td>
                            <td style="width: 88px">
                                Pay Head
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 262px">
                                <asp:TextBox ID="txtPayHead" runat="server" CssClass="etc" MaxLength="10" Width="240px"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderPayHead" runat="server" BehaviorID="AutoCompletePayHead"
                                    CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                    OnClientItemSelected="aceSelected" OnClientPopulated="acePopulated" ServiceMethod="GetSrchPayHead"
                                    ServicePath="~/Module/HRMS/Forms/wsAutoComHrms.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                    TargetControlID="txtPayHead">
                                </cc1:AutoCompleteExtender>
                            </td>
                            <td style="width: 16px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPayHead"
                                    ErrorMessage="Enter Pay Head" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtPayHead"
                                    ErrorMessage="Enter Valid Pay Head" ForeColor="Red" ValidationGroup="Save" OnServerValidate="CustomValidator1_ServerValidate">*</asp:CustomValidator>
                            </td>
                            <td style="width: 45px">
                                Value
                            </td>
                            <td style="width: 4px">
                                :
                            </td>
                            <td style="width: 159px">
                                <asp:RadioButtonList ID="optValType" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="True" OnSelectedIndexChanged="optValType_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="1">Default</asp:ListItem>
                                    <asp:ListItem Value="3">Manual</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 137px">
                                <asp:TextBox ID="txtManualVal" runat="server" CssClass="etc" Width="120px" Enabled="False"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtManualVal_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtManualVal" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                            </td>
                            <td style="width: 68px">
                                <asp:CheckBox ID="chkShow" runat="server" Text="Show" Checked="true" />
                            </td>
                            <td align="center" style="width: 225px">
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
                            <td style="width: 23px">
                                &nbsp;
                            </td>
                            <td colspan="7">
                                &nbsp;
                            </td>
                            <td style="width: 137px">
                                &nbsp;
                            </td>
                            <td style="width: 68px">
                                &nbsp;
                            </td>
                            <td style="width: 225px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div align="center" style="border: 1px solid #CCCCCC; width: 100%; background-color: #CCFFFF;
                overflow: auto;">
                <asp:Panel ID="pnlGrdDetList" runat="server" GroupingText="">
                    <asp:ValidationSummary ID="ValidationSummary5" runat="server" Font-Size="10pt" ForeColor="Red"
                        ValidationGroup="Save" DisplayMode="List" />
                    <br />
                    <asp:GridView ID="gvGrdDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvGrdDet_RowDataBound" OnRowDeleting="gvGrdDet_RowDeleting"
                        OnSelectedIndexChanged="gvGrdDet_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="GrdDetRefNo" HeaderText="Ref. No" />
                            <asp:BoundField DataField="GrdDetPayHead" HeaderText="Pay Head" />
                            <asp:TemplateField HeaderText="Value Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblValType" runat="server" Text='<%# Bind("GrdDetFlag") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="GrdDetBasePayHead" HeaderText="Base Value" />
                            <asp:TemplateField HeaderText="Show">
                                <ItemTemplate>
                                    <asp:Label ID="lblShowFlag" runat="server" Text='<%# Bind("GrdDetShowFlg") %>'></asp:Label>
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
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="10pt" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#CC99FF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                    <br />
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboGrade" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="optValType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvGrdDet" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvGrdDet" EventName="RowDeleting" />
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
    <asp:HiddenField ID="hfGrdDetRefNo" runat="server" Value="0" />
</asp:Content>
