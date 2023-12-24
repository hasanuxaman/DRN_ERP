<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmSalesReturn.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesReturn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="Javascript">
        function CalcFreeQty(RtnQty, RtnFreeQty, ChlnQty, ChlnFreeQty, TotRtnQty) {
            var ReturnQty = 0;
            if (document.getElementById(RtnQty).value != '') {
                ReturnQty = parseFloat(document.getElementById(RtnQty).value);
            }
            //alert("Return Qty: " + ReturnQty.toString());

            var ReturnFreeQty = 0;
            if (document.getElementById(RtnFreeQty).value != '') {
                ReturnFreeQty = parseFloat(document.getElementById(RtnFreeQty).value);
            }
            //alert("Return Free Qty: " + ReturnFreeQty.toString());

            var ChallanQty = 0;
            ChallanQty = parseFloat(ChlnQty);
            //alert("Challan Qty: " + ChallanQty.toString());

            var ChallanFreeQty = 0;
            ChallanFreeQty = parseFloat(ChlnFreeQty);
            //alert("Challan Free Qty: " + ChallanFreeQty.toString());

            var TotalRtnQty = document.getElementById(TotRtnQty);
            var TotalReturnQty = parseFloat(ReturnQty + ReturnFreeQty);
            //alert("Total Return Qty:" + TotalReturnQty.toString());
            TotalRtnQty.value = TotalReturnQty.toFixed(2);

            if (ReturnQty > ChallanQty) {
                document.getElementById(RtnQty).value = ChallanQty;
                //document.getElementById(RtnFreeQty).value = ChallanFreeQty;
                document.getElementById(TotRtnQty).value = ChallanQty + ChallanFreeQty;
                alert("You are not allowed to return qty more than: " + ChallanQty.toString());
            }

            if (ReturnFreeQty > ChallanFreeQty) {
                document.getElementById(RtnFreeQty).value = ChallanFreeQty;
                alert("You are not allowed to return free qty more than: " + ChallanFreeQty.toString());
            }
        }

        function CheckFreeQty(RtnQty, RtnFreeQty, ChlnQty, ChlnFreeQty, TotRtnQty) {
            var ReturnQty = 0;
            if (document.getElementById(RtnQty).value != '') {
                ReturnQty = parseFloat(document.getElementById(RtnQty).value);
            }
            //alert("Return Qty: " + ReturnQty.toString());

            var ReturnFreeQty = 0;
            if (document.getElementById(RtnFreeQty).value != '') {
                ReturnFreeQty = parseFloat(document.getElementById(RtnFreeQty).value);
            }
            //alert("Return Free Qty: " + ReturnFreeQty.toString());

            var ChallanQty = 0;
            ChallanQty = parseFloat(ChlnQty);
            //alert("Challan Qty: " + ChallanQty.toString());

            var ChallanFreeQty = 0;
            ChallanFreeQty = parseFloat(ChlnFreeQty);
            //alert("Challan Free Qty: " + ChallanFreeQty.toString());

            var TotalRtnQty = document.getElementById(TotRtnQty);
            var TotalReturnQty = parseFloat(ReturnQty + ReturnFreeQty);
            //alert("Total Return Qty:" + TotalReturnQty.toString());
            TotalRtnQty.value = TotalReturnQty.toFixed(2);

            if (ReturnQty > ChallanQty) {
                document.getElementById(RtnQty).value = ChallanQty;
                //document.getElementById(RtnFreeQty).value = ChallanFreeQty;
                document.getElementById(TotRtnQty).value = ChallanQty + ChallanFreeQty;
                alert("You are not allowed to return qty more than: " + ChallanQty.toString());
            }

            if (ReturnFreeQty > ChallanFreeQty) {
                document.getElementById(RtnFreeQty).value = ChallanFreeQty;
                document.getElementById(TotRtnQty).value = ChallanQty + ChallanFreeQty;
                alert("You are not allowed to return free qty more than: " + ChallanFreeQty.toString());
            }
        }            
    </script>
    <div align="center" style="background-color: #00FF99">
        Sales Return Entry</div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" BackColor="#86AEAE">
                <div align="center" style="background-color: #E1F0FF">
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="Search Challan" Font-Bold="True" Font-Italic="False"
                        ForeColor="#0000CC"></asp:Label>
                    <hr />
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 131px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 31px">
                                Year
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left" style="width: 100px">
                                <asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
                                    Width="80px">
                                </asp:DropDownList>
                            </td>
                            <td align="left" style="width: 44px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 44px">
                                Month
                            </td>
                            <td align="left">
                                :
                            </td>
                            <td align="left" style="width: 105px">
                                <asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"
                                    Width="100px">
                                </asp:DropDownList>
                            </td>
                            <td align="left" style="width: 88px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 147px">
                                Challan No
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left">
                                <span>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="search textAlignCenter" Width="400px"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchCln" runat="server" BehaviorID="AutoCompleteSrchCln"
                                        CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                        CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                        ServiceMethod="GetSrchChlnListByYearMonth" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                        ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearch">
                                    </cc1:AutoCompleteExtender>
                                </span>
                            </td>
                            <td style="width: 33px">
                                <span>
                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" onkeypress="return clickButton(event,'btnSearch')"
                                        Text="Search" ValidationGroup="btnSearch" Width="60px" />
                                </span>
                            </td>
                            <td style="width: 48px">
                                <span>
                                    <asp:Button ID="btnClearSrch" runat="server" OnClick="btnClearSrch_Click" Text="Clear"
                                        Width="60px" />
                                </span>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 131px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 31px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="left" style="width: 100px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 44px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 44px">
                                &nbsp;
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 105px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 88px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 147px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="left">
                                <span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSearch"
                                        ErrorMessage="Enter Search Text" Font-Bold="False" Font-Size="10pt" ForeColor="Red"
                                        ValidationGroup="btnSearch"></asp:RequiredFieldValidator>
                                </span>
                            </td>
                            <td style="width: 33px">
                                &nbsp;
                            </td>
                            <td style="width: 48px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div align="center" style="background-color: #CCCCFF">
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Challan Details" Font-Bold="True" ForeColor="#0000CC"></asp:Label>
                    <hr />
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
                                <asp:TextBox ID="txtChallanNo" runat="server" CssClass="textAlignCenter" Enabled="False"
                                    Font-Bold="True" ForeColor="#FF9900" Width="180px"></asp:TextBox>
                            </td>
                            <td align="left" class="style2" style="font-size: 12px; width: 72px;">
                                Date &amp; Time
                            </td>
                            <td align="left" width="8">
                                :
                            </td>
                            <td align="left" class="style3" style="width: 136px">
                                <asp:TextBox ID="txtChallanDate" runat="server" AutoPostBack="True" CssClass="textAlignCenter"
                                    Enabled="False"></asp:TextBox>
                            </td>
                            <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                                Trans. Mode
                            </td>
                            <td align="left" width="8">
                                :
                            </td>
                            <td align="left" class="style5" style="font-size: 14px; width: 135px;">
                                <asp:DropDownList ID="ddlOrdTransMode" runat="server" Width="172px" Enabled="False">
                                </asp:DropDownList>
                            </td>
                            <td align="left" class="style6" style="font-size: 12px; width: 75px;">
                                Del. Address
                            </td>
                            <td align="left" style="font-size: 12px; width: 8px;">
                                :
                            </td>
                            <td align="left" rowspan="2" valign="top" width="8">
                                <asp:TextBox ID="txtDelAddr" runat="server" ForeColor="#0066FF" Height="40px" MaxLength="100"
                                    TextMode="MultiLine" Width="180px" Enabled="False"></asp:TextBox>
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
                                <asp:TextBox ID="txtTruckNo" runat="server" CssClass="uppercase" Width="180px" Enabled="False"></asp:TextBox>
                            </td>
                            <td align="left" class="style2" style="font-size: 12px; width: 72px;">
                                Driver Name
                            </td>
                            <td align="left" width="8">
                                :
                            </td>
                            <td align="left" class="style3" style="width: 136px">
                                <asp:TextBox ID="txtDriverName" runat="server" CssClass="capitalize" Enabled="False"></asp:TextBox>
                            </td>
                            <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                                Contact No
                            </td>
                            <td align="left" width="8">
                                :
                            </td>
                            <td align="left" class="style5" style="font-size: 14px; width: 135px;">
                                <asp:TextBox ID="txtDriverContact" runat="server" Enabled="False"></asp:TextBox>
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
                    <asp:GridView ID="gvChlnDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvChlnDet_RowDataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Challan Ref. No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfOrdRef" runat="server" Value='<%# Bind("Trn_Det_Ord_Ref") %>' />
                                    <asp:HiddenField ID="hfOrdRefNo" runat="server" Value='<%# Bind("Trn_Det_Ord_Ref_No") %>' />
                                    <asp:HiddenField ID="hfOrdDetLno" runat="server" Value='<%# Bind("Trn_Det_Ord_Det_Lno") %>' />
                                    <asp:HiddenField ID="hfChlnRef" runat="server" Value='<%# Bind("Trn_Hdr_DC_No") %>' />
                                    <asp:Label ID="lblChlnRefNo" runat="server" Text='<%# Bind("Trn_Hdr_Cno") %>'></asp:Label>
                                    <asp:HiddenField ID="hfChlnStrCode" runat="server" Value='<%# Bind("Trn_Det_Str_Code") %>' />
                                    <asp:HiddenField ID="hfChlnDetLno" runat="server" Value='<%# Bind("Trn_Det_Lno") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Desc">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfICode" runat="server" Value='<%# Bind("Trn_Det_Icode") %>' />
                                    <asp:Label ID="lblIDesc" runat="server" Text='<%# Bind("Trn_Det_Itm_Desc") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="250px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("Trn_Det_Itm_Uom") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chln. Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblChlnQty" runat="server" Text='<%# (Convert.ToDecimal(Eval("Trn_Det_Del_Qty"))).ToString("N2") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Free Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblChlnFreeQty" runat="server" Text='<%# Convert.ToDecimal(Eval("Trn_Det_Free_Qty")).ToString("N2") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Qty">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("Trn_Det_Del_Qty")) + Convert.ToDecimal(Eval("Trn_Det_Free_Qty")))).ToString("N2") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Prev. Rtn. Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrevRtnQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("Trn_Det_Del_Qty")) + Convert.ToDecimal(Eval("Trn_Det_Free_Qty"))) - Convert.ToDecimal(Eval("Rtn_Bal_Qty")) ).ToString("N2") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Rtn. Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRtnQty" runat="server" Width="70px" CssClass="textAlignCenter"
                                        Text='<%# (Convert.ToDecimal(Eval("Trn_Det_Del_Qty"))).ToString("N2") %>'></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtRtnQty_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtRtnQty"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rtn. Free Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRtnFreeQty" runat="server" Width="70px" CssClass="textAlignCenter"
                                        Text='<%# (Convert.ToDecimal(Eval("Trn_Det_Free_Qty"))).ToString("N2") %>'></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtRtnFreeQty_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtRtnFreeQty"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Rtn. Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTotRtnQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("Trn_Det_Del_Qty")) + Convert.ToDecimal(Eval("Trn_Det_Free_Qty")))).ToString("N2") %>'
                                        BackColor="#9999FF" BorderStyle="None" CssClass="textAlignCenter" Enabled="False"
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkChln" runat="server" Checked="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
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
            </asp:Panel>
            <div align="center" style="background-color: #99CCFF">
                <br />
                <asp:Label ID="Label4" runat="server" Text="Return Info" Font-Bold="True" ForeColor="#0000CC"></asp:Label>
                <hr />
                <table style="width: 100%;">
                    <tr>
                        <td align="left" style="font-size: 12px; width: 93px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 62px;">
                            Return Ref
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style1" style="width: 196px">
                            <asp:TextBox ID="txtRtnRef" runat="server" CssClass="textAlignCenter" Enabled="False"
                                Font-Bold="True" ForeColor="#FF9900" Width="180px"></asp:TextBox>
                        </td>
                        <td align="left" class="style2" style="font-size: 12px; width: 72px;">
                            Date &amp; Time
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style3" style="width: 154px">
                            <asp:TextBox ID="txtRtnDate" runat="server" AutoPostBack="True" CssClass="textAlignCenter"
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                            Trans. Mode
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style5" style="font-size: 14px; width: 116px;">
                            <asp:DropDownList ID="ddlRtnTranMode" runat="server" Width="172px">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="style5" style="font-size: 14px; width: 116px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 8px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 12px; width: 93px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 62px;">
                            Trans. By
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style1" style="width: 196px">
                            <asp:RadioButtonList ID="optTranBy" runat="server" AutoPostBack="True" Font-Size="8pt"
                                OnSelectedIndexChanged="optTranBy_SelectedIndexChanged" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="1">Customer</asp:ListItem>
                                <asp:ListItem Value="2">Company</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left" class="style2" style="font-size: 12px; width: 72px;">
                            &nbsp;
                        </td>
                        <td align="left" width="8">
                            &nbsp;
                        </td>
                        <td align="left" class="style3" style="width: 154px">
                            &nbsp;
                        </td>
                        <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                            &nbsp;
                        </td>
                        <td align="left" width="8">
                            &nbsp;
                        </td>
                        <td align="left" class="style5" style="font-size: 14px; width: 116px;">
                            &nbsp;
                        </td>
                        <td align="left" class="style5" style="font-size: 14px; width: 116px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 8px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 12px; width: 93px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 62px;">
                            Vehicle No
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style1" style="width: 196px">
                            <asp:TextBox ID="txtRtnTruckNo" runat="server" CssClass="uppercase" Width="180px"></asp:TextBox>
                            <asp:DropDownList ID="cboRtnTruckNo" runat="server" Visible="False" Width="182px">
                                <asp:ListItem Value="0">---Select---</asp:ListItem>
                                <asp:ListItem>DMT-09-4644</asp:ListItem>
                                <asp:ListItem Value="DMT-09-4688">DMT-09-4688</asp:ListItem>
                                <asp:ListItem>DMT-09-5000</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="style2" style="font-size: 12px; width: 72px;">
                            Driver Name
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style3" style="width: 154px">
                            <asp:TextBox ID="txtRtnDriverName" runat="server" CssClass="capitalize"></asp:TextBox>
                        </td>
                        <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                            Driver Cell No
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style5" style="font-size: 14px; width: 116px;">
                            <asp:TextBox ID="txtRtnDriverContact" runat="server"></asp:TextBox>
                        </td>
                        <td align="left" class="style5" style="font-size: 14px; width: 116px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 8px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 12px; width: 93px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 62px;">
                            Remarks
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style1" colspan="8">
                            <asp:TextBox ID="txtRemarks" runat="server" Width="750px"></asp:TextBox>
                        </td>
                        <td align="left" style="font-size: 12px; width: 8px;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="gvPendRtn" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Return Ref. No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfOrdRef0" runat="server" Value='<%# Bind("Trn_Det_Ord_Ref") %>' />
                                <asp:HiddenField ID="hfOrdRefNo0" runat="server" Value='<%# Bind("Trn_Det_Ord_Ref_No") %>' />
                                <asp:HiddenField ID="hfOrdDetLno0" runat="server" Value='<%# Bind("Trn_Det_Ord_Det_Lno") %>' />
                                <%--<asp:HiddenField ID="hfChlnRef0" runat="server" Value='<%# Bind("Trn_Hdr_DC_No") %>' />--%>
                                <asp:Label ID="lblRtnRefNo0" runat="server" Text='<%# Bind("Trn_Det_Ref") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="130px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Trn_Hdr_T_C1" HeaderText="Customer Name">
                            <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Challan Ref No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfChlnRef0" runat="server" Value='<%# Bind("Trn_Det_Ord_Ref") %>' />
                                <asp:Label ID="lblChlnRefNo0" runat="server" Text='<%# Bind("Trn_Det_Ord_Ref_No") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="130px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Trn_Hdr_T_C2" HeaderText="Date">
                            <ItemStyle Width="50px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Item Desc">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfICode0" runat="server" Value='<%# Bind("Trn_Det_Icode") %>' />
                                <asp:Label ID="lblIDesc0" runat="server" Text='<%# Bind("Trn_Det_Itm_Desc") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="250px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblUnit0" runat="server" Text='<%# Bind("Trn_Det_Itm_Uom") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rtn. Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblChlnQty0" runat="server" Text='<%# (Convert.ToDecimal(Eval("Trn_Det_Lin_Qty"))).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rtn. Free Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblChlnFreeQty0" runat="server" Text='<%# Convert.ToDecimal(Eval("Trn_Det_Unt_Wgt")).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Rtn. Qty">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("Trn_Det_Lin_Qty")) + Convert.ToDecimal(Eval("Trn_Det_Unt_Wgt")))).ToString("N2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="30px"
                        Wrap="False" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <br />
                <asp:Button ID="btnSaveChln" runat="server" OnClick="btnSaveChln_Click" Text="Save"
                    Width="120px" Enabled="False" />
                <asp:HiddenField ID="hfEditStatus" runat="server" Value="N" />
                <br />
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
