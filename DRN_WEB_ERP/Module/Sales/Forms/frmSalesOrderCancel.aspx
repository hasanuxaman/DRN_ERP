<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmSalesOrderCancel.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesOrderCancel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //Highlight GridView row on mouseover event
        function MouseEvents(objRef, evt) {
            var checkbox = objRef.getElementsByTagName("input")[0];
            if (evt.type == "mouseover") {
                objRef.style.backgroundColor = "orange";
            }
            else {
                if (checkbox.checked) {
                    objRef.style.backgroundColor = "aqua";
                }
                else if (evt.type == "mouseout") {
                    if (objRef.rowIndex % 2 == 0) {
                        //Alternating Row Color
                        objRef.style.backgroundColor = "white";

                    }
                    else {
                        objRef.style.backgroundColor = "#EFF3FB";
                    }
                }
            }
        }
    </script>
    <div align="center" style="background-color: #00FF99">
        Sales Order Cancel</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel3" runat="server" DefaultButton="btnSearchOrder">
                <div align="center">
                    <table style="width: 100%; font-size: 12px; background-color: #CCFFFF;">
                        <tr>
                            <td style="width: 21px">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 138px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 168px">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 97px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 146px">
                                &nbsp;
                            </td>
                            <td style="width: 127px">
                                &nbsp;
                            </td>
                            <td style="width: 6px">
                                &nbsp;
                            </td>
                            <td style="width: 136px">
                                &nbsp;
                            </td>
                            <td style="width: 15px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
                                &nbsp;
                            </td>
                            <td style="width: 98px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 21px">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 138px">
                                Date From
                            </td>
                            <td>
                                :
                            </td>
                            <td style="width: 168px">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDtFromCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                                </cc1:CalendarExtender>
                                <asp:Image ID="imgFromDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                            </td>
                            <td align="right" style="width: 97px">
                                Date To
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 146px">
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    PopupButtonID="imgToDate" TargetControlID="txtToDate">
                                </cc1:CalendarExtender>
                                <asp:Image ID="imgToDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png"
                                    Width="16px" />
                            </td>
                            <td align="right" style="width: 127px">
                                Customer
                            </td>
                            <td style="width: 6px">
                                :
                            </td>
                            <td style="width: 136px">
                                <asp:TextBox ID="txtSearch" runat="server" Width="350px" CssClass="search textAlignCenter"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                                    CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                    DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCustomer"
                                    ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                    TargetControlID="txtSearch">
                                </cc1:AutoCompleteExtender>
                            </td>
                            <td style="width: 15px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
                                <asp:Button ID="btnSearchOrder" runat="server" Text="Search" Width="100px" ValidationGroup="btnShow"
                                    OnClick="btnSearchOrder_Click" />
                            </td>
                            <td style="width: 98px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 21px">
                                &nbsp;
                            </td>
                            <td style="width: 138px">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 168px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFromDate"
                                    Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                                <br />
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFromDate"
                                    Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                    Type="Date" ValidationGroup="btnShow"></asp:CompareValidator>
                            </td>
                            <td style="width: 97px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 146px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                                    Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                                <br />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtToDate"
                                    Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                    Type="Date" ValidationGroup="btnShow"></asp:CompareValidator>
                            </td>
                            <td style="width: 127px">
                                &nbsp;
                            </td>
                            <td style="width: 6px">
                                &nbsp;
                            </td>
                            <td style="width: 136px" align="center">
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtSearch"
                                    ErrorMessage="Enter Valid Customer Name" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"
                                    ValidationGroup="btnShow"></asp:CustomValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSearch"
                                    ErrorMessage="Enter Curomer Name First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 15px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
                                &nbsp;
                            </td>
                            <td style="width: 98px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 21px">
                                &nbsp;
                            </td>
                            <td style="width: 138px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 168px">
                                &nbsp;
                            </td>
                            <td style="width: 97px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 146px">
                                &nbsp;
                            </td>
                            <td style="width: 127px">
                                &nbsp;
                            </td>
                            <td style="width: 6px">
                                &nbsp;
                            </td>
                            <td style="width: 136px">
                                &nbsp;
                            </td>
                            <td style="width: 15px">
                                &nbsp;
                            </td>
                            <td style="width: 99px">
                                &nbsp;
                            </td>
                            <td style="width: 98px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div align="center" style="background-color: #9E9AF5">
                <asp:Panel ID="pnlSoAppStat" runat="server" BackColor="#9E9AF5" GroupingText="Order Details">
                    <br />
                    <asp:GridView ID="gvOrdDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Size="7pt" ForeColor="#333333" OnSelectedIndexChanged="gvOrdDet_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="15px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfCustRef" runat="server" Value='<%# Bind("SO_Hdr_Pcode") %>' />
                                    <asp:Label ID="lblCustName" runat="server" Text='<%# Bind("Par_Adr_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SP Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpName" runat="server" Text='<%# GetSpName(Eval("SO_Hdr_Com4").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ord. Ref. No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfOrdHdrRef" runat="server" Value='<%# Bind("SO_Hdr_Ref") %>' />
                                    <asp:HiddenField ID="hfOrdDetLno" runat="server" Value='<%# Bind("SO_Det_Lno") %>' />
                                    <asp:Label ID="lblOrdHdrRefNo" runat="server" Text='<%# Bind("SO_Hdr_Ref_No") %>'></asp:Label>
                                    <asp:Label ID="lblOrdDate" runat="server" Font-Italic="True" Font-Size="7" ForeColor="#3399FF"
                                        Text='<%# Bind("SO_Hdr_Date") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="110px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Desc.">
                                <ItemTemplate>
                                    <asp:HiddenField ID="HfICode" runat="server" Value='<%# Bind("SO_Det_Icode") %>' />
                                    <asp:Label ID="lblIDesc" runat="server" Text='<%# Bind("SO_Det_Itm_Desc") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="120px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="SO_Det_Itm_Uom" HeaderText="Unit">
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Order Qty" ItemStyle-BackColor="#00CC99" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrdLinQty" runat="server" Font-Bold="True" Text='<%# String.Format("{0:n}", (Convert.ToDecimal(Eval("SO_Det_T_C1")))) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#99CCFF" Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrdQty" runat="server" Text='<%# Convert.ToDecimal(Eval("SO_Det_Lin_Qty")).ToString("N2") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#00CC66" HorizontalAlign="Center" Width="50px" Font-Bold="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Free Bag">
                                <ItemTemplate>
                                    <asp:Label ID="lblAppFreeQty" runat="server" Text='<%# Bind("SO_Det_Free_Qty", "{0:F2}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tot. Qty (with free bag)">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotSoQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) + Convert.ToDecimal(Eval("SO_Det_Free_Qty")))).ToString("N2") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BackColor="#99FFCC" Font-Bold="True" Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblLinRate" runat="server" Text='<%# Bind("SO_Det_Lin_Rat", "{0:F2}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblSoLinAmt" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Lin_Rat")))).ToString("N2") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="70px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trans. Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblTransRate" runat="server" Text='<%# Bind("SO_Det_Trans_Rat", "{0:F2}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblSoNetAmt" runat="server" Text='<%# Bind("SO_Det_Lin_Net", "{0:F2}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#CC9900" Font-Bold="True" HorizontalAlign="Right" Width="70px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="D/O Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblDoQty" runat="server" Text='<%# Bind("SO_Det_DO_Qty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="D/O Balance">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfSoBalQty" runat="server" Value='<%# Bind("SO_Det_DO_Bal_Qty", "{0:F2}") %>' />
                                    <asp:HiddenField ID="hfSoFreeBalQty" runat="server" Value='<%# Convert.ToDecimal(Eval("SO_Det_Ext_Data2")).ToString("N2") %>' />
                                    <%--<asp:Label ID="lblDoBalQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("SO_Det_DO_Bal_Qty")) + Convert.ToDecimal(Eval("SO_Det_Ext_Data2")))).ToString("N2") %>'></asp:Label>--%>
                                    <asp:Label ID="lblDoBalQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("SO_Det_DO_Bal_Qty")))).ToString("N2") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#33CCCC" Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnCancelOrd" runat="server" OnClick="btnCancelOrd_Click" Text="Cancel" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Wrap="True" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#FFFF66" Font-Bold="True" ForeColor="#333333" />
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
            <div align="center" style="background-color: #9E9AF5">
                <asp:Panel ID="Panel1" runat="server" BackColor="#9E9AF5" GroupingText="D/O Details">
                    <br />
                    <asp:GridView ID="gvDoDet" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" CellPadding="3" Font-Size="7pt" PageSize="25"
                        OnSelectedIndexChanged="gvDoDet_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="D/O Ref. No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfDoHdrRef" runat="server" Value='<%# Bind("DO_Hdr_Ref") %>' />
                                    <asp:HiddenField ID="hfDoDetLno" runat="server" Value='<%# Bind("DO_Det_Lno") %>' />
                                    <asp:HiddenField ID="hfHdrStat" runat="server" Value='<%# Bind("DO_Hdr_Status") %>' />
                                    <asp:Label ID="lblDoHdrRefNo" runat="server" Text='<%# Bind("DO_Hdr_Ref_No") %>'></asp:Label>
                                    <%--<asp:Label ID="lblOrdDate" runat="server" Font-Italic="True" Font-Size="7" ForeColor="#3399FF"
                                        Text='<%# Bind("DO_Hdr_Date") %>'></asp:Label>--%>
                                </ItemTemplate>
                                <ItemStyle Width="110px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="DO_Hdr_Date" HeaderText="D/O Date">
                                <ItemStyle Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Itm_Det_Desc" HeaderText="Item Desc.">
                                <ItemStyle Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DO_Det_Itm_Uom" HeaderText="Unit">
                                <ItemStyle Width="20px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="D/O Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblDoLinQty" runat="server" Text='<%# Bind("DO_Det_Lin_Qty", "{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#00CC66" Font-Bold="True" HorizontalAlign="Right" Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Free Bag">
                                <ItemTemplate>
                                    <asp:Label ID="lblDoLinFreeQty" runat="server" Text='<%# Bind("DO_Det_Free_Qty", "{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Qty (with free bag)">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotDoLinQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("DO_Det_Lin_Qty")) + Convert.ToDecimal(Eval("DO_Det_Free_Qty")))).ToString("N2") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="60px" BackColor="#CC33FF" HorizontalAlign="Right" Font-Bold="true" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="SO_Det_Lin_Rat" DataFormatString="{0:n}" HeaderText="D/O Rate">
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblDoLinAmt" runat="server" Text='<%# String.Format( "{0:n}", (Convert.ToDecimal(Eval("DO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Lin_Rat"))) ) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="DO_Det_Trans_Rat" DataFormatString="{0:n}" HeaderText="Trans. Rate">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Net Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblDoLinNetAmt" runat="server" Text='<%# Bind("DO_Det_Lin_Net", "{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#FF6699" Font-Bold="True" HorizontalAlign="Right" Width="70px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Challan Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblChlnQty" runat="server" Text='<%# Bind("DO_Det_Del_Qty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="D/O Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lblDoLinBalQty" runat="server" Text='<%# Bind("DO_Det_Del_Bal_Qty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#33CCCC" Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnCancelDo" runat="server" OnClick="btnCancelDo_Click" OnClientClick="return confirm('Do you want to Cancel?')"
                                        Text="Cancel" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" Font-Size="7pt" ForeColor="#F7F7F7"
                            Wrap="True" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E7E7FF" Font-Size="7.5" ForeColor="#4A3C8C" />
                        <SelectedRowStyle BackColor="#FFFF66" Font-Bold="True" ForeColor="Black" />
                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>
                    <br />
                </asp:Panel>
                <br />
            </div>
            <div align="center" style="background-color: #9E9AF5">
                <asp:Panel ID="Panel2" runat="server" BackColor="#9E9AF5" GroupingText="Challan Details">
                    <br />
                    <asp:GridView ID="gvChlnDet" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="7pt"
                        PageSize="25">
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Challan Ref. No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfClnHdrRef" runat="server" Value='<%# Bind("Trn_Hdr_DC_No") %>' />
                                    <asp:Label ID="lblClnHdrRefNo" runat="server" Text='<%# Bind("Trn_Hdr_Tran_Ref", "{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Trn_Hdr_Date" HeaderText="Challan Date" />
                            <asp:TemplateField HeaderText="Item Desc.">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("Trn_Det_Itm_Desc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Trn_Det_Itm_Uom" HeaderText="Unit" />
                            <asp:TemplateField HeaderText="Challan Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblClnLinQty" runat="server" Text='<%# Bind("DelQty", "{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#00CC66" Font-Bold="True" HorizontalAlign="Right" Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Free Bag">
                                <ItemTemplate>
                                    <asp:Label ID="lblClnLinFreeQty" runat="server" Text='<%# Bind("FreeQty", "{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Qty (with free bag)">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotClnLinQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("DelQty")) + Convert.ToDecimal(Eval("FreeQty")))).ToString("N2") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#CC33FF" Font-Bold="true" HorizontalAlign="Right" Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="D/O Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblClnDoRate" runat="server" Text='<%# GetDoRate(Eval("Trn_Det_Ord_Ref_No").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblClnLinAmt" runat="server" Text='<%# GetClnLinAmt( Eval("DelQty").ToString(), Eval("Trn_Det_Ord_Ref_No").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="70px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trans. Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lnlClnDoTransRate" runat="server" Text='<%# GetDoTransRate(Eval("Trn_Det_Ord_Ref_No").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblClnLinNetAmt" runat="server" Text='<%# GetClnNetAmt( Eval("DelQty").ToString(), Eval("Trn_Det_Ord_Ref_No").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#FF6699" Font-Bold="True" HorizontalAlign="Right" Width="70px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnCancelChallan" runat="server" OnClientClick="return confirm('Do you want to Cancel?')"
                                        Text="Cancel" OnClick="btnCancelChallan_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Size="7pt" ForeColor="White"
                            Wrap="True" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle Font-Size="7.5" ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#FFFF66" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>
                    <br />
                </asp:Panel>
                <br />
            </div>
        </ContentTemplate>
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
