<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmIOLimit.aspx.cs" Inherits="DRN_WEB_ERP.Module.IO.Forms.frmIOLimit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //Autocomplete Start
        function acePopulated(sender, e) {
            var behavior = $find('AutoCompleteSrchEmp');
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
            var searchText = $get('<%=txtSrchEmp.ClientID %>').value;
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
        I/O Limit</div>
    <div style="border: 1px solid #CCCCCC; width: 100%; background-color: #CCFFFF;">
        <asp:Panel ID="pnlEmpHdr" runat="server" GroupingText="">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 31px">
                        &nbsp;
                    </td>
                    <td style="width: 52px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 283px">
                        <asp:CheckBox ID="chkIncTermEmp" runat="server" AutoPostBack="True" OnCheckedChanged="chkIncTermEmp_CheckedChanged"
                            Text="Include Settled Employee" />
                    </td>
                    <td style="width: 23px">
                        &nbsp;
                    </td>
                    <td style="width: 95px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 122px">
                        <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtLimitAmt"
                            ErrorMessage="Enter Valid Amount" Font-Size="10pt" ForeColor="Red" Operator="DataTypeCheck"
                            Type="Currency" ValidationGroup="Save"></asp:CompareValidator>
                    </td>
                    <td style="width: 214px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 31px">
                        &nbsp;
                    </td>
                    <td style="width: 52px">
                        Employee
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td style="width: 283px">
                        <asp:TextBox ID="txtSrchEmp" runat="server" CssClass="etc search" Width="400" ForeColor="#0066FF"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderEmp" runat="server" BehaviorID="AutoCompleteSrchEmp"
                            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            DelimiterCharacters="," MinimumPrefixLength="1" OnClientPopulated="acePopulated"
                            OnClientItemSelected="aceSelected" ServiceMethod="GetSrchEmp" ServicePath="~/Module/HRMS/Forms/wsAutoComHrms.asmx"
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSrchEmp">
                        </cc1:AutoCompleteExtender>
                    </td>
                    <td style="width: 23px">
                        &nbsp;
                    </td>
                    <td style="width: 95px">
                        Limit Amount
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td style="width: 122px">
                        <asp:TextBox ID="txtLimitAmt" runat="server" CssClass="etc"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="txtLimitAmt_FilteredTextBoxExtender" runat="server"
                            Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtLimitAmt" ValidChars=".">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                    <td align="center" style="width: 214px">
                        <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="Clear" Width="100px"
                            OnClick="btnClear_Click" />
                        <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" Width="100px"
                            ValidationGroup="Save" OnClick="btnSave_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 31px">
                        &nbsp;
                    </td>
                    <td style="width: 52px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 283px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSrchEmp"
                            ErrorMessage="* Enter Employee Name" Font-Size="10pt" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        &nbsp;<asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtSrchEmp"
                            ErrorMessage="* Enter Valid Employee" Font-Size="10pt" ForeColor="Red" OnServerValidate="CustomValidator3_ServerValidate"
                            ValidationGroup="Save"></asp:CustomValidator>
                    </td>
                    <td style="width: 23px">
                        &nbsp;
                    </td>
                    <td style="width: 95px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 122px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtLimitAmt"
                            ErrorMessage="Enter Limit Amount" ForeColor="Red" ValidationGroup="Save" Font-Size="10pt"></asp:RequiredFieldValidator>
                    </td>
                    <td align="center" style="width: 214px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div align="center" style="border: 1px solid #CCCCCC; width: 100%; overflow: auto;
        background-color: #CCFFFF;">
        <asp:Panel ID="pnlIOLimitList" runat="server" GroupingText="">
            <br />
            <asp:GridView ID="gvIoLimit" runat="server" AutoGenerateColumns="False" CellPadding="4"
                Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvIoLimit_RowDataBound" OnSelectedIndexChanged="gvIoLimit_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" class="preview" Target="_blank" NavigateUrl='<%# Eval("IoAccEmpRef", "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo={0}") %>'
                                runat="server">
                                <asp:Image ID="AttachImage" runat="server" Height="50px" Width="65px" ImageUrl="~/Image/NoImage.gif" />
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee Details">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfEmpRef" runat="server" Value='<%# Bind("IoAccEmpRef") %>' />
                            <asp:Label ID="IoEmpName" runat="server" Text='<%# GetEmpName(Eval("IoAccEmpRef").ToString())%>'
                                Font-Bold="True"></asp:Label>
                            <br />
                            <asp:Label ID="IoEmpId" runat="server" Text='<%# GetEmpId(Eval("IoAccEmpRef").ToString())%>'
                                Font-Italic="True" ForeColor="#3399FF"></asp:Label>
                            <br />
                            <asp:Label ID="IoEmpDes" runat="server" Text='<%# GetEmpDesig(Eval("IoAccEmpRef").ToString())%>'
                                Font-Italic="True"></asp:Label>
                            <br />
                            <asp:Label ID="IoEmpDep" runat="server" Text='<%# GetEmpDept(Eval("IoAccEmpRef").ToString())%>'
                                Font-Italic="True"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Limit Amount" DataField="IoAccLimit" DataFormatString="{0:n}" />
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
