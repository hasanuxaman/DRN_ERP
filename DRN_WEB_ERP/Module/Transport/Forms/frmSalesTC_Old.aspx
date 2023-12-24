<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmSalesTC_Old.aspx.cs" Inherits="DRN_WEB_ERP.Module.Transport.Forms.frmSalesTC_Old" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .Background
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        
        .Popup
        {
            border: 3px solid black;
            background-color: #FFFFFF;
            padding-top: 10px;
            padding-left: 10px;
        }
        
        .lbl
        {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }
    </style>
    <div align="center" style="background-color: #00FF99">
        Transport Contact</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                Value:
                <input type="text" id="Target"><br />
                <asp:Button ID="Button1" runat="server" Text="Fill Form in Popup" />
                <cc1:ModalPopupExtender ID="mp11" runat="server" PopupControlID="Panl1" TargetControlID="Button1"
                    CancelControlID="Button22" BackgroundCssClass="Background">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
                    <iframe style="width: 350px; height: 300px;" id="irm11" src="frmSalesDataBrowser.aspx"
                        runat="server"></iframe>
                    <br />
                    <asp:Button ID="Button22" runat="server" Text="Close" />
                </asp:Panel>
                <script type="text/javascript">
                    function setValue(val) {
                        var value = val;
                        document.getElementById('Target').value = value;
                    }
                </script>
            </div>
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <table style="width: 100%; font-size: small;">
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 318px">
                            <asp:Button ID="btnSelectDo" runat="server" Text="Select D/O" OnClick="btnSelectDo_Click" />
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            TC No
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left" style="width: 318px">
                            <asp:TextBox ID="txtChallanNo" runat="server" CssClass="textAlignCenter" Enabled="False"
                                Font-Bold="True" ForeColor="#FF9900" Width="180px"></asp:TextBox>
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            Date &amp; Time
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtChallanDate" runat="server" AutoPostBack="True" CssClass="textAlignCenter"
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 318px">
                            &nbsp;
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            Pay Mode
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left" style="width: 318px">
                            <asp:DropDownList ID="ddlOrdTransMode0" runat="server" Width="180px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            Status
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlOrdTransMode" runat="server" Width="180px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 318px">
                            &nbsp;
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            Dealer Name
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left" style="width: 318px">
                            <span>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="search textAlignCenter" Width="300px"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                                    CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                    ServiceMethod="GetSrchCustomer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                    ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearch">
                                </cc1:AutoCompleteExtender>
                            </span>
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            Retailer Name
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRetailer" runat="server" CssClass="inline search" Width="300px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtendertxtRetailer" runat="server" BehaviorID="AutoCompleteSrchRTL"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetSrchRetailer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtRetailer">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 318px">
                            &nbsp;
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            Location
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left" style="width: 318px">
                            <asp:TextBox ID="txtChallanNo0" runat="server" CssClass="textAlignCenter" Enabled="False"
                                Font-Bold="True" ForeColor="#FF9900" Width="315px"></asp:TextBox>
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            Rate
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtChallanNo1" runat="server" CssClass="textAlignCenter" Enabled="False"
                                Font-Bold="True" ForeColor="#FF9900" Width="315px"></asp:TextBox>
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 318px">
                            &nbsp;
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="gvTcDoDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333" GridLines="None" PageSize="25" Font-Bold="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="D/O Ref. No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfDoHdrRef0" runat="server" Value='<%# Bind("DO_Hdr_Ref") %>' />
                                <asp:HiddenField ID="hfDoDetLno0" runat="server" Value='<%# Bind("DO_Det_Lno") %>' />
                                <asp:HiddenField ID="hfHdrStat0" runat="server" Value='<%# Bind("DO_Hdr_Status") %>' />
                                <asp:Label ID="lblDoHdrRefNo0" runat="server" Text='<%# Bind("DO_Hdr_Ref_No") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DO_Hdr_Date" HeaderText="D/O Date" />
                        <asp:BoundField DataField="Par_Adr_Name" HeaderText="Dealer Name" />
                        <asp:BoundField DataField="Itm_Det_Desc" HeaderText="Item Desc." />
                        <asp:BoundField DataField="DO_Det_Itm_Uom" HeaderText="Unit" />
                        <asp:TemplateField HeaderText="D/O Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblDoQty0" runat="server" Text='<%# Bind("DO_Det_Lin_Qty", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Free Bag">
                            <ItemTemplate>
                                <asp:Label ID="lblDoFreeQty0" runat="server" Text='<%# Bind("DO_Det_Free_Qty", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total D/O Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblTotDoQty0" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("DO_Det_Lin_Qty")) + Convert.ToDecimal(Eval("DO_Det_Free_Qty")))).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="90px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Balance Qty">
                            <ItemTemplate>
                                <%--<asp:Label ID="lblDoBalQty" runat="server" Text='<%# Bind("DO_Det_Del_Bal_Qty") %>'></asp:Label>--%>
                                <asp:Label ID="lblDoBalQty0" runat="server" Text='<%# Bind("DO_Det_Unt_Wgt") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="90px" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="8" ForeColor="White"
                        Wrap="false" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" Font-Size="8" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                </asp:GridView>
                <br />
                <br />
            </div>
            <asp:Panel ID="PNL" runat="server" CssClass="Popup" align="center" Width="800px"
                Height="330px" Style="display: none">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 100%; font-size: 14px; font-weight: bold;" bgcolor="#CC99FF">
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearchDo" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnSearchDo" runat="server" Text="Search" OnClick="btnSearchDo_Click" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:GridView ID="gvPendDoDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        Font-Size="8pt" ForeColor="#333333" GridLines="None" PageSize="7" AllowPaging="True">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
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
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DO_Hdr_Date" HeaderText="D/O Date" />
                                            <asp:BoundField DataField="Par_Adr_Name" HeaderText="Dealer Name" />
                                            <asp:BoundField DataField="Itm_Det_Desc" HeaderText="Item Desc." />
                                            <asp:BoundField DataField="DO_Det_Itm_Uom" HeaderText="Unit" />
                                            <asp:TemplateField HeaderText="D/O Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDoQty" runat="server" Text='<%# Bind("DO_Det_Lin_Qty", "{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Free Bag">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDoFreeQty" runat="server" Text='<%# Bind("DO_Det_Free_Qty", "{0:n}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total D/O Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotDoQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("DO_Det_Lin_Qty")) + Convert.ToDecimal(Eval("DO_Det_Free_Qty")))).ToString("N2") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance Qty">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblDoBalQty" runat="server" Text='<%# Bind("DO_Det_Del_Bal_Qty") %>'></asp:Label>--%>
                                                    <asp:Label ID="lblDoBalQty" runat="server" Text='<%# Bind("DO_Det_Unt_Wgt") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDo" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="8" ForeColor="White"
                                            Wrap="false" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" Font-Size="8" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                                    </asp:GridView>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearchDo" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:Button ID="ButtonOk" runat="server" CssClass="btn2" OnClick="ButtonOk_Click"
                    Text="OK" Width="80px" />
                <asp:Button ID="ButtonCancel" runat="server" CssClass="btn2" Text="Cancel" Width="82px" />
                <br />
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalPopupExtenderDoList" runat="server" BackgroundCssClass="modalBackground"
                CancelControlID="ButtonCancel" PopupControlID="PNL" TargetControlID="btnSelectDo">
            </cc1:ModalPopupExtender>
            <%--<asp:Button ID="Button2" runat="server" />--%>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSelectDo" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div style="width: 60%">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <asp:Button ID="btnPopup" runat="server" Text="Show Popup" OnClientClick="ShowPopup()" /><br />
                </td>
            </tr>
        </table>
        <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlPopup" TargetControlID="btnPopup"
            BehaviorID="mp1" CancelControlID="btnClose">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="Popup" align="center" Style="display: none">
            <iframe style="width: 300px; height: 300px;" id="irm1" src="WebForm1.aspx" runat="server">
            </iframe>
            <br />
            <asp:Button ID="btnClose" runat="server" Text="Close" />
        </asp:Panel>
        <script type="text/javascript">
                    function ShowPopup() {
                $find('mp1').show();
                return false;
            }
        </script>
    </div>
</asp:Content>
