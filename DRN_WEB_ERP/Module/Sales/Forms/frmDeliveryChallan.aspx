<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmDeliveryChallan.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmDeliveryChallan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="Javascript">
        //        function setfocus(utxtbox, ptxtbox) {
        //            utxtbox.value = utxtbox.value.toUpperCase();
        //        }
        function CalcFreeQty(DoQty, DoFreeQty, OrgDoLinQty, OrgDoFreeLinQty, DoBalQty, DoFreeBalQty) {
            var DoDelQty = 0;
            if (document.getElementById(DoQty).value != '') {
                DoDelQty = parseFloat(document.getElementById(DoQty).value);
            }
            //alert("Do Qty: " + DoDelQty.toString());

            var DoDelFreeQty = 0;
            if (document.getElementById(DoFreeQty).value != '') {
                DoDelFreeQty = parseFloat(document.getElementById(DoFreeQty).value);
            }
            //alert("Do Free Qty: " + DoDelFreeQty.toString());

            var OrgDoQty = 0;
            OrgDoQty = parseFloat(OrgDoLinQty);
            //alert("Org Do Qty: " + OrgDoQty.toString());

            var OrgDoFreeQty = 0;
            OrgDoFreeQty = parseFloat(OrgDoFreeLinQty);
            //alert("Org Do Free Qty: " + OrgDoFreeQty.toString());

            var FreeQty = document.getElementById(DoFreeQty);
            var FreeBagQty = parseFloat((DoDelQty * OrgDoFreeQty) / OrgDoQty);
            //alert("Free Bag Qty:" + FreeBagQty.toString());
            FreeQty.value = FreeBagQty.toFixed(2);

            var BalDoQty = 0;
            BalDoQty = parseFloat(DoBalQty);
            //alert("Do Bal Qty: " + BalDoQty.toString());

            var BalDoFreeQty = 0;
            BalDoFreeQty = parseFloat(DoFreeBalQty);
            //alert("Do Free Bal Qty: " + BalDoFreeQty.toString());

            if (DoDelQty > BalDoQty) {
                document.getElementById(DoQty).value = BalDoQty;
                DoDelQty = BalDoQty;
                document.getElementById(DoFreeQty).value = BalDoFreeQty;
                DoDelFreeQty = BalDoFreeQty;
                alert("You are not allowed to deliver qty more than: " + BalDoQty.toString());
            }

            if (DoDelFreeQty > BalDoFreeQty) {
                document.getElementById(DoFreeQty).value = BalDoFreeQty;
                DoDelFreeQty = BalDoFreeQty;
                alert("You are not allowed to deliver free qty more than: " + BalDoFreeQty.toString());
            }
        }

        function CheckFreeQty(DoQty, DoFreeQty, OrgDoLinQty, OrgDoFreeLinQty, DoBalQty, DoFreeBalQty) {
            var DoDelQty = 0;
            if (document.getElementById(DoQty).value != '') {
                DoDelQty = parseFloat(document.getElementById(DoQty).value);
            }
            //alert("Do Qty: " + DoDelQty.toString());

            var DoDelFreeQty = 0;
            if (document.getElementById(DoFreeQty).value != '') {
                DoDelFreeQty = parseFloat(document.getElementById(DoFreeQty).value);
            }
            //alert("Do Free Qty: " + DoDelFreeQty.toString());

            var OrgDoQty = 0;
            OrgDoQty = parseFloat(OrgDoLinQty);
            //alert("Org Do Qty: " + OrgDoQty.toString());

            var OrgDoFreeQty = 0;
            OrgDoFreeQty = parseFloat(OrgDoFreeLinQty);
            //alert("Org Do Free Qty: " + OrgDoFreeQty.toString());

            var BalDoQty = 0;
            BalDoQty = parseFloat(DoBalQty);
            //alert("Do Bal Qty: " + BalDoQty.toString());

            var BalDoFreeQty = 0;
            BalDoFreeQty = parseFloat(DoFreeBalQty);
            //alert("Do Free Bal Qty: " + BalDoFreeQty.toString());

            if (DoDelQty > BalDoQty) {
                document.getElementById(DoQty).value = OrgDoQty;
                DoDelQty = OrgDoQty;
                document.getElementById(DoFreeQty).value = OrgDoFreeQty;
                DoDelFreeQty = OrgDoFreeQty;
                alert("You are not allowed to deliver qty more than: " + BalDoQty.toString());
            }

            if (DoDelFreeQty > BalDoFreeQty) {
                document.getElementById(DoFreeQty).value = BalDoFreeQty;
                DoDelFreeQty = BalDoFreeQty;
                alert("You are not allowed to deliver free qty more than: " + BalDoFreeQty.toString());
            }
        }                
    </script>
    <div align="center" style="background-color: #00FF99">
        Delivery Challan Entry</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnSearch">
                <div align="center" style="background-color: #86AEAE">
                    <asp:CheckBox ID="chkIncAll" runat="server" Font-Size="8pt" Text="Include delivered D/O"
                        OnCheckedChanged="chkIncAll_CheckedChanged" Visible="False" />
                    <br />
                    <span>Search Customer:</span> <span>
                        <asp:TextBox ID="txtSearch" runat="server" Width="550px" CssClass="search textAlignCenter"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCustomer"
                            ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                            TargetControlID="txtSearch">
                        </cc1:AutoCompleteExtender>
                        <asp:Button ID="btnSearch" runat="server" onkeypress="return clickButton(event,'btnSearch')"
                            Width="60px" Text="Search" ValidationGroup="btnSearch" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnClearSrch" runat="server" Text="Clear" Visible="False" Width="60px"
                            OnClick="btnClearSrch_Click" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSearch"
                            ErrorMessage="Enter Search Text" Font-Bold="False" ForeColor="Red" ValidationGroup="btnSearch"
                            Font-Size="10pt"></asp:RequiredFieldValidator>
                        <br />
                        <br />
                    </span>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClearSrch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #9999FF">
                <br />
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td align="left" style="font-size: 12px; width: 10px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 69px;">
                            Challan No
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style1" style="width: 182px">
                            <asp:TextBox ID="txtChallanNo" runat="server" Enabled="False" Font-Bold="True" ForeColor="#FF9900"
                                Width="180px" CssClass="textAlignCenter"></asp:TextBox>
                        </td>
                        <td align="left" class="style2" style="font-size: 12px; width: 72px;">
                            Date &amp; Time
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style3" style="width: 136px">
                            <asp:TextBox ID="txtChallanDate" runat="server" AutoPostBack="True" Enabled="False"
                                CssClass="textAlignCenter"></asp:TextBox>
                        </td>
                        <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                            Trans. Mode
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style5" style="font-size: 14px; width: 135px;">
                            <asp:DropDownList ID="ddlOrdTransMode" runat="server" Width="160px">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="style6" style="font-size: 12px; width: 75px;">
                            Del. Address
                        </td>
                        <td align="left" style="font-size: 12px; width: 8px;">
                            :
                        </td>
                        <td align="left" width="8" rowspan="3" valign="top">
                            <asp:TextBox ID="txtDelAddr" runat="server" ForeColor="#0066FF" Width="180px" TextMode="MultiLine"
                                Height="60px" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 12px; width: 10px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 69px;">
                            Trans. By
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style1" style="width: 182px">
                            <asp:RadioButtonList ID="optTranBy" runat="server" Font-Size="8pt" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="optTranBy_SelectedIndexChanged">
                                <asp:ListItem Value="1" Selected="True">Customer</asp:ListItem>
                                <asp:ListItem Value="2">Company</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left" class="style2" style="font-size: 12px; width: 72px;">
                            District
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style3" style="width: 136px">
                            <asp:DropDownList ID="cboCustDist" runat="server" ValidationGroup="ChkData" Width="172px"
                                AutoPostBack="True" OnSelectedIndexChanged="cboCustDist_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                            Thana
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style5" style="font-size: 14px; width: 135px;">
                            <asp:DropDownList ID="cboCustThana" runat="server" Width="160px">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="style6" style="font-size: 12px; width: 75px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 8px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 12px; width: 10px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 69px;">
                            Vehicle No
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style1" style="width: 182px">
                            <asp:TextBox ID="txtTruckNo" runat="server" Width="180px" CssClass="uppercase"></asp:TextBox>
                            <asp:DropDownList ID="cboTruckNo" runat="server" Width="182px" Visible="False">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="style2" style="font-size: 12px; width: 72px;">
                            Driver Name
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style3" style="width: 136px">
                            <asp:TextBox ID="txtDriverName" runat="server" CssClass="capitalize"></asp:TextBox>
                        </td>
                        <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                            Contact No
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style5" style="font-size: 14px; width: 135px;">
                            <asp:TextBox ID="txtDriverContact" runat="server" Width="156px"></asp:TextBox>
                        </td>
                        <td align="left" class="style6" style="font-size: 12px; width: 75px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 8px;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <hr />
                <table style="width: 100%; font-size: 12px;">
                    <tr>
                        <td style="width: 37px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 59px">
                            Retailer 1
                        </td>
                        <td align="left">
                            :
                        </td>
                        <td align="left" style="width: 714px">
                            <asp:TextBox ID="txtRetailer1" runat="server" CssClass="inline search" Width="686px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtendertxtRetailer1" runat="server" BehaviorID="AutoCompleteSrchRTL1"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetSrchRetailer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtRetailer1">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 40px">
                            Quantity
                        </td>
                        <td align="left">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDelQty1" runat="server" CssClass="textAlignCenter" 
                                Width="100px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtDelQty1_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDelQty1"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 37px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 59px">
                            Retailer 2
                        </td>
                        <td align="left">
                            :
                        </td>
                        <td align="left" style="width: 714px">
                            <asp:TextBox ID="txtRetailer2" runat="server" CssClass="inline search" Width="686px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtendertxtRetailer2" runat="server" BehaviorID="AutoCompleteSrchRTL2"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetSrchRetailer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtRetailer2">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 40px">
                            Quantity
                        </td>
                        <td align="left">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDelQty2" runat="server" CssClass="textAlignCenter" 
                                Width="100px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtDelQty2_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDelQty2"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 37px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 59px">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 714px">
                            &nbsp;</td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 40px">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="optTranBy" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboCustDist" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlOrdDet" runat="server">
                <div align="center" style="background-color: #9E9AF5">
                    <div style="background-color: #CCFF66">
                        <br />
                        Delivery Store:
                        <asp:DropDownList ID="ddlDelStore" runat="server" Width="192px">
                        </asp:DropDownList>
                        <br />
                        <br />
                    </div>
                    <asp:GridView ID="gvDoDet" runat="server" CellPadding="4" Font-Size="8pt" ForeColor="#333333"
                        AutoGenerateColumns="False" OnRowDataBound="gvDoDet_RowDataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="D/O Ref. No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfDoHdrRef" runat="server" Value='<%# Bind("DO_Hdr_Ref") %>' />
                                    <asp:Label ID="lblDoHdrRefNo" runat="server" Text='<%# Bind("DO_Hdr_Ref_No") %>'></asp:Label>
                                    <asp:HiddenField ID="hfDoDetLno" runat="server" Value='<%# Bind("DO_Det_Lno") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DO_Hdr_T_C1" HeaderText="Delivey Address">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DO_Hdr_Date" HeaderText="D/O Date" DataFormatString="{0:d}">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <%--<asp:TemplateField HeaderText="D/O Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDoDate" runat="server" Text='<%# Bind("DO_Hdr_Date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Item Desc.">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfICode" runat="server" Value='<%# Bind("DO_Det_Icode") %>' />
                                    <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("Itm_Det_Desc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblUom" runat="server" Text='<%# Bind("SO_Det_Itm_Uom") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="D/O Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrgDoQty" runat="server" Text='<%# Bind("DO_Det_Lin_Qty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#B9FFDC" HorizontalAlign="Center" Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="D/O Free Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrgDoFreeQty" runat="server" Text='<%# Bind("DO_Det_Free_Qty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#B9FFDC" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tot. D/O Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrgTotDoQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("DO_Det_Lin_Qty")) + Convert.ToDecimal(Eval("DO_Det_Free_Qty")))).ToString("N2") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#99CC00" HorizontalAlign="Center" Font-Bold="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tot. Del. Qty">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblTotDelQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("DO_Det_Org_QTY")) + Convert.ToDecimal(Eval("DO_Det_Ext_Data1")))).ToString("N2") %>'></asp:Label>--%>
                                    <asp:Label ID="lblTotDelQty" runat="server" Text=' <%# (Convert.ToDecimal(Eval("DO_Det_Del_Qty"))).ToString("N2") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#FFFF99" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="D/O Balance">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfDoBalQty" runat="server" Value='<%# Bind("DO_Det_Bal_Qty", "{0:F2}") %>' />
                                    <asp:HiddenField ID="hfDoFreeBalQty" runat="server" Value='<%# Convert.ToDecimal(Eval("DO_Det_Ext_Data2")).ToString() %>' />
                                    <%--<asp:Label ID="lblDoBalQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("DO_Det_Bal_Qty")) + Convert.ToDecimal(Eval("DO_Det_Ext_Data2")))).ToString("N2") %>'></asp:Label>--%>
                                    <asp:Label ID="lblDoBalQty" runat="server" Text='<%# Convert.ToDecimal(Eval("DO_Det_Del_Bal_Qty")).ToString("N2") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#33CCCC" Font-Bold="True" HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Challan Qty" ItemStyle-Width="70">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDoQty" runat="server" CssClass="textAlignCenter" Text='<%# Bind("DO_Det_Bal_Qty", "{0:F2}") %>'
                                        Width="70px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtDoQty_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDoQty"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </ItemTemplate>
                                <ItemStyle Width="70px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Free Bag" ItemStyle-Width="50">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDoFreeQty" runat="server" CssClass="textAlignCenter" Text='<%# Convert.ToDecimal(Eval("DO_Det_Ext_Data2")).ToString("N2") %>'
                                        Width="50px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtDoFreeQty_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDoFreeQty"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Button ID="btnCreateChln" runat="server" OnClientClick="return confirm('Do you want to Accept?')"
                                        Text="Delivery" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDoDelQty" runat="server" Checked="false" AutoPostBack="True"
                                        OnCheckedChanged="chkDoDelQty_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Wrap="False"
                            Height="30px" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
                    <span>
                        <br />
                        <asp:Button ID="btnSaveChln" runat="server" OnClick="btnSaveChln_Click" Text="Save"
                            Width="120px" Visible="False" />
                        <asp:HiddenField ID="hfEditStatus" runat="server" Value="N" />
                    </span>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSaveChln" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvDoDet" EventName="RowDataBound" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #CC66FF">
                <br />
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 131px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 222px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 48px">
                            Year
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left" style="width: 100px">
                            <asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
                                Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 87px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 62px">
                            Month</td>
                        <td align="left" style="width: 2px">
                            :</td>
                        <td align="left">
                            <asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" 
                                OnSelectedIndexChanged="cboMonth_SelectedIndexChanged" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 90px">
                            &nbsp;</td>
                        <td align="left" style="width: 125px">
                            Challan No
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlChallanList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChallanList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 33px">
                            &nbsp;
                        </td>
                        <td style="width: 121px">
                            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" Visible="false"
                                Width="120px" />
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="gvChlnDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Trn_Det_Ord_Ref_No" HeaderText="D/O Ref. No" />
                        <asp:BoundField DataField="Trn_Det_Itm_Desc" HeaderText="Item Desc">
                            <ItemStyle Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Trn_Det_Itm_Uom" HeaderText="Unit">
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Trn_Det_Code" HeaderText="Type" />
                        <asp:BoundField DataField="Trn_Det_Lin_Qty" HeaderText="Qty">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Height="30px"
                        Wrap="False" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboYear" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlChallanList" EventName="SelectedIndexChanged" />
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
