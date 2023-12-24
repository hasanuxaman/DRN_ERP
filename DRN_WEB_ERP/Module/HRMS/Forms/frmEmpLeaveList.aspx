<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmEmpLeaveList.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmEmpLeaveList" %>

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
        Employee Leave List
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="border: 1px solid #CCCCCC; background-color: #CCFFFF;">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 200px">
                            &nbsp;
                        </td>
                        <td style="width: 181px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 250px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px">
                            &nbsp;
                        </td>
                        <td style="width: 181px">
                            Date From
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 250px">
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="etc"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px">
                            &nbsp;
                        </td>
                        <td style="width: 181px">
                            Date To
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 250px">
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="etc"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px">
                            &nbsp;
                        </td>
                        <td style="width: 181px">
                            Show Leave with Status
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td colspan="2">
                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">All</asp:ListItem>
                                <asp:ListItem Value="2">Rejected</asp:ListItem>
                                <asp:ListItem Value="3">Canceled</asp:ListItem>
                                <asp:ListItem Selected="True" Value="4">Pending Approval</asp:ListItem>
                                <asp:ListItem Value="5">Scheduled</asp:ListItem>
                                <asp:ListItem Value="6">Taken</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px">
                            &nbsp;
                        </td>
                        <td style="width: 181px">
                            Employee Name
                        </td>
                        <td style="width: 3px">
                            :&nbsp;
                        </td>
                        <td style="width: 250px">
                            <asp:TextBox ID="txtEmpName" runat="server" CssClass="etc search" AutoPostBack="True"
                                OnTextChanged="txtEmpName_TextChanged" Width="350px"></asp:TextBox>
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
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px">
                        </td>
                        <td style="width: 181px">
                            Leave Type
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 250px">
                            <asp:DropDownList ID="cboLeaveType" runat="server" AutoPostBack="True" CssClass="etc"
                                OnSelectedIndexChanged="cboLeaveType_SelectedIndexChanged" ValidationGroup="ChkData"
                                Width="185px">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px">
                            &nbsp;&nbsp;
                        </td>
                        <td style="width: 181px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                        </td>
                        <td style="width: 250px" valign="top">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" Width="100px"
                                OnClick="btnSearch_Click" />
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="background-color: #CCFFFF">
                <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" 
                    Font-Size="8pt">
                    <AlternatingRowStyle BackColor="White" />
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="cboLeaveType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtEmpName" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="CheckBoxList1" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
