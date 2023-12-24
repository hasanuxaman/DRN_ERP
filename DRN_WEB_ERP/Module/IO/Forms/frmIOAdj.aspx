<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmIOAdj.aspx.cs" Inherits="DRN_WEB_ERP.Module.IO.frmIOAdj" %>

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
        I/O Adjustment</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center">
                <br />
                <asp:Label ID="lblIoLimit" runat="server" BackColor="#9966FF"></asp:Label>
                &nbsp;&nbsp;<asp:Label ID="lblIoUnAdj" runat="server" BackColor="#FF66CC"></asp:Label>
                &nbsp;&nbsp;<asp:Label ID="lblIoBal" runat="server" BackColor="#33CCFF"></asp:Label>
                <br />
            </div>
            <div align="center" style="border: 1px solid #CCCCCC; height: 550px; background-color: #CCFFFF;">
                <table style="background-color: #CCFFFF;">
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="right">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 300px" align="left">
                            &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 136px">
                            I/O Adjust Ref No
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td align="left" style="width: 300px">
                            <asp:TextBox ID="txtIoAdjRefNo" runat="server" Width="370px" CssClass="textAlignCenter"
                                BackColor="#9999FF" BorderStyle="None" Enabled="False" Font-Bold="True" ForeColor="Black"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="right">
                            Employee Name
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="txtEmpName" runat="server" CssClass="etc search" Width="350px" Enabled="False"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderEmp" runat="server" BehaviorID="AutoCompleteSrchEmp"
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
                        </td>
                        <td style="width: 136px" align="right">
                            Business Unit
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:DropDownList ID="cboBusUnit" runat="server" ValidationGroup="ChkData" Width="185px"
                                CssClass="etc">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cboBusUnit"
                                ErrorMessage="Select Business Unit" ForeColor="Red" ValidationGroup="Save" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="right">
                            Date
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:TextBox ID="txtAdjDt" runat="server" CssClass="etc"></asp:TextBox>
                            <asp:Image ID="imgAdjDt" runat="server" ImageUrl="~/Image/calendar.png" />
                            <cc1:CalendarExtender ID="CalenderReqDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                TargetControlID="txtAdjDt" PopupButtonID="imgAdjDt">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAdjDt"
                                ErrorMessage="Enter Adjustment Date" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtAdjDt"
                                ErrorMessage="Enter Valid Adjustment Date" ForeColor="Red" Operator="DataTypeCheck"
                                Type="Date" ValidationGroup="Save">*</asp:CompareValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 136px">
                            Amount
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td align="left" style="width: 300px">
                            <asp:TextBox ID="txtIoReqAmnt" runat="server"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtIoReqAmnt_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtIoReqAmnt" ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtIoReqAmnt"
                                ErrorMessage="Enter Required Amount" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtIoReqAmnt"
                                ErrorMessage="Enter Valid Amount" ForeColor="Red" Operator="DataTypeCheck" Type="Currency"
                                ValidationGroup="Save">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 136px">
                            I/O Req. No
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td align="left" style="width: 300px">
                            <asp:TextBox ID="txtIoReqRefNo" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 136px">
                            MPR No
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td align="left" style="width: 300px">
                            <asp:TextBox ID="txtMpr" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 136px">
                            Quotation
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td align="left" style="width: 300px">
                            <asp:TextBox ID="txtQuotation" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 136px">
                            W/O
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td align="left" style="width: 300px">
                            <asp:TextBox ID="txtWO" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 136px">
                            Bill No
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td align="left" style="width: 300px">
                            <asp:TextBox ID="txtBill" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 136px">
                            Site Rcv. Challan
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 300px">
                            <asp:TextBox ID="txtSiteChallan" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 136px">
                            MRR
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td align="left" style="width: 300px">
                            <asp:TextBox ID="txtMrr" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                        </td>
                        <td align="right" style="width: 136px">
                            Remarks
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" rowspan="3" valign="top">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="etc" Height="76px" TextMode="MultiLine"
                                Width="365px"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="right">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td rowspan="3">
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
                    </tr>
                    <tr>
                        <td style="width: 29px">
                            &nbsp;
                        </td>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 29px">
                        </td>
                        <td style="width: 136px">
                            <asp:Label ID="lblIoWithd" runat="server" BackColor="#FF9966" Visible="False"></asp:Label>
                            <asp:Label ID="lblIoAdj" runat="server" BackColor="#FFFF66" Visible="False"></asp:Label>
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 300px">
                            <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="Clear" Width="100px"
                                OnClick="btnClear_Click" />
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" OnClick="btnSave_Click" Text="Submit"
                                ValidationGroup="Save" Width="100px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="10pt" ForeColor="Red"
                        ValidationGroup="Save" />
                </div>
            </div>
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <br />
                <asp:Panel ID="Panel2" runat="server" GroupingText="Pending I/O Adjustment">
                    <br />
                    <asp:GridView ID="gvPendIoAdj" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Size="8pt" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="I/O Adj. Ref. No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfIoAdjRef" runat="server" Value='<%# Bind("IoAdjRef") %>' />
                                    <asp:Label ID="lblIoAdjRefNo" runat="server" Text='<%# Bind("IoAdjRefNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AdjDate" HeaderText="Date" DataFormatString="{0:d}" />
                            <asp:TemplateField HeaderText="Business Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblBusUnit" runat="server" Text='<%# GetBusUnit(Eval("AdjBusUnit").ToString())%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AdjAmount" HeaderText="Amount" DataFormatString="{0:n}">
                                <ItemStyle BackColor="#9966FF" Font-Bold="True" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AdjStatDesc" HeaderText="Status" />
                            <asp:BoundField DataField="AdjMprNo" HeaderText="MPR No" />
                            <asp:BoundField DataField="AdjQuotNo" HeaderText="Quotation No" />
                            <asp:BoundField DataField="AdjWoNo" HeaderText="W/O No" />
                            <asp:BoundField DataField="AdjBillNo" HeaderText="Bill No" />
                            <asp:BoundField DataField="AdjSiteChlnNo" HeaderText="Site Rcv. Challan" />
                            <asp:BoundField DataField="AdjMrrNo" HeaderText="MRR No" />
                            <asp:BoundField DataField="AdjCmnt" HeaderText="Remarks" />
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="10pt" ForeColor="White" />
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
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <br />
                <asp:Panel ID="Panel1" runat="server" GroupingText="I/O Adjustment">
                    <br />
                    <asp:GridView ID="gvIoAdj" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Size="8pt" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="I/O Adj. Ref. No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfIoAdjRef" runat="server" Value='<%# Bind("IoAdjRef") %>' />
                                    <asp:Label ID="lblIoAdjRefNo" runat="server" Text='<%# Bind("IoAdjRefNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AdjDate" DataFormatString="{0:d}" HeaderText="Date" />
                            <asp:TemplateField HeaderText="Business Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblBusUnit" runat="server" Text='<%# GetBusUnit(Eval("AdjBusUnit").ToString())%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AdjAmount" HeaderText="Amount" DataFormatString="{0:n}">
                                <ItemStyle BackColor="#9966FF" Font-Bold="True" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AdjStatDesc" HeaderText="Status" />
                            <asp:BoundField DataField="AdjMprNo" HeaderText="MPR No" />
                            <asp:BoundField DataField="AdjQuotNo" HeaderText="Quotation No" />
                            <asp:BoundField DataField="AdjWoNo" HeaderText="W/O No" />
                            <asp:BoundField DataField="AdjBillNo" HeaderText="Bill No" />
                            <asp:BoundField DataField="AdjSiteChlnNo" HeaderText="Site Rcv. Challan" />
                            <asp:BoundField DataField="AdjMrrNo" HeaderText="MRR No" />
                            <asp:BoundField DataField="AdjCmnt" HeaderText="Remarks" />
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="10pt" ForeColor="White" />
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
