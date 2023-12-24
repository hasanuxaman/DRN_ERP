<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmSupplierBillEntry.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmSupplierBillEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="Javascript">
        function CalcBillAmt(billAmt, adjAmt, totBillAmt) {
            var BillAmt = 0;
            if (document.getElementById(billAmt).value != '') {
                BillAmt = parseFloat(document.getElementById(billAmt).value);
            }
            //alert("Bill Amount: " + BillAmt.toString());

            var AdjAmt = 0;
            if (document.getElementById(adjAmt).value != '') {
                AdjAmt = parseFloat(document.getElementById(adjAmt).value);
            }
            //alert("Adjustment Amount: " + AdjAmt.toString());            

            var TotBillAmt = document.getElementById(totBillAmt);
            if (AdjAmt > BillAmt) {
                document.getElementById(adjAmt).value = "0";
                var NetBillAmt = parseFloat(BillAmt - 0);
                TotBillAmt.value = NetBillAmt.toFixed(2);
                alert("You are not allowed to adjust more than bill amount: " + NetBillAmt.toString());
            }
            else {
                var NetBillAmt = parseFloat(BillAmt - AdjAmt);
                //alert("Net Bill Amount:" + NetBillAmt.toString());
                TotBillAmt.value = NetBillAmt.toFixed(2);
            }
        }

        function CalcBillAdjAmt(billAmt, adjAmt, totBillAmt) {
            var BillAmt = 0;
            if (document.getElementById(billAmt).value != '') {
                BillAmt = parseFloat(document.getElementById(billAmt).value);
            }
            //alert("Bill Amount: " + BillAmt.toString());            

            var TotBillAmt = 0;
            if (document.getElementById(totBillAmt).value != '') {
                TotBillAmt = parseFloat(document.getElementById(totBillAmt).value);
            }
            //alert("Total Bill Amount: " + TotBillAmt.toString()); 

            var AdjAmt = document.getElementById(adjAmt);
            var NetAdjAmt = parseFloat(BillAmt - TotBillAmt);
            //alert("Adjust Amount: " + NetAdjAmt.toString()); 

            if (TotBillAmt > BillAmt) {
                //document.getElementById(totBillAmt).value = BillAmt.toFixed(2);
                //AdjAmt.value = "0";
                //alert("You are not allowed to pay bill more than bill amount: " + BillAmt.toString());
                AdjAmt.value = parseFloat(TotBillAmt - BillAmt);
            }
            else {
                AdjAmt.value = NetAdjAmt.toFixed(2);
            }
        }                            
    </script>
    <div align="center" style="background-color: #00FF99">
        Supplier Bill Entry</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #CC66FF">
                <br />
                Supplier Name:<asp:TextBox ID="txtSearchSupplier" runat="server" Width="550px" CssClass="search textAlignCenter"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchSup" runat="server" BehaviorID="AutoCompleteSrchSup"
                    CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                    DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchSupplier"
                    ServicePath="~/Module/Procurement/Forms/wsAutoComProc.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                    TargetControlID="txtSearchSupplier">
                </cc1:AutoCompleteExtender>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                    Width="60px" ValidationGroup="btnSearch" />
                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="60px" />
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSearchSupplier"
                    ErrorMessage="Enter Supplier Name First" Font-Size="10pt" ForeColor="Red" ValidationGroup="btnSearch"></asp:RequiredFieldValidator>
                <br />
            </div>
            <div style="background-color: #9E9AF5">
                <table style="width: 100%; font-family: verdana; font-size: small;">
                    <tr>
                        <td style="width: 261px">
                            &nbsp;
                        </td>
                        <td style="width: 240px">
                            &nbsp;
                        </td>
                        <td style="width: 167px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 179px">
                            &nbsp;
                        </td>
                        <td style="width: 91px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 228px">
                            &nbsp;
                        </td>
                        <td style="width: 207px">
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
                        <td style="width: 261px">
                            &nbsp;
                        </td>
                        <td style="width: 240px">
                            &nbsp;
                        </td>
                        <td style="width: 167px">
                            Bill Ref No
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td style="width: 179px">
                            <asp:TextBox ID="txtBillRefNo" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" Font-Bold="True" Font-Size="10pt" ForeColor="Blue" Width="170px"></asp:TextBox>
                        </td>
                        <td style="width: 91px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                            Entry Date
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td style="width: 228px">
                            <asp:TextBox ID="txtBillEntDt" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" ForeColor="Blue" Width="170px"></asp:TextBox>
                        </td>
                        <td style="width: 207px">
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
                        <td style="width: 261px">
                            &nbsp;
                        </td>
                        <td style="width: 240px">
                            &nbsp;
                        </td>
                        <td style="width: 167px">
                            Supplier Bill No
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td style="width: 179px">
                            <asp:TextBox ID="txtSupBillNo" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Font-Bold="True" Font-Size="10pt" ForeColor="#FF33CC" Width="170px"></asp:TextBox>
                        </td>
                        <td style="width: 91px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSupBillNo"
                                ErrorMessage="Enter Supplier Bill No" ForeColor="Red" ValidationGroup="btnPost">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 136px">
                            Bill Date
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td style="width: 228px">
                            <asp:TextBox ID="txtBillDate" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" ForeColor="Blue" Width="170px"></asp:TextBox>
                            <asp:Image ID="imgReqDt" runat="server" ImageUrl="~/Image/calendar.png" />
                            <cc1:CalendarExtender ID="CalenderDelDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgReqDt" TargetControlID="txtBillDate">
                            </cc1:CalendarExtender>
                        </td>
                        <td style="width: 207px">
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
                        <td style="width: 261px">
                            &nbsp;
                        </td>
                        <td style="width: 240px">
                            &nbsp;
                        </td>
                        <td style="width: 167px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 179px">
                            &nbsp;
                        </td>
                        <td style="width: 91px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 228px">
                            &nbsp;
                        </td>
                        <td style="width: 207px">
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
                <br />
            </div>
            <div align="center" style="background-color: #00CCFF">
                <fieldset style="background-color: #00CCFF">
                    <legend>MRR / Challan List</legend>
                    <br />
                    <asp:GridView ID="gvMrrDet" runat="server" BackColor="White" BorderColor="#CC9966"
                        BorderWidth="1px" CellPadding="4" Font-Size="10pt" AutoGenerateColumns="False"
                        OnRowDataBound="gvMrrDet_RowDataBound" ShowFooter="True" BorderStyle="None" OnRowCommand="gvMrrDet_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MRR Ref No">
                                <ItemTemplate>
                                    <asp:Label ID="lblMrrRefNo" runat="server" Text='<%# Bind("Trn_Hdr_Ref_No") %>'></asp:Label>
                                    <asp:HiddenField ID="hfMrrTrnDetLno" runat="server" Value='<%# Bind("Trn_Det_Lno") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="120px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MRR Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblMrrDate" runat="server" Text='<%# Eval("Trn_Hdr_Date", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="55px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO Ref.">
                                <ItemTemplate>
                                    <asp:Label ID="lblMrrPoRef" runat="server" Text='<%# Bind("Trn_Hdr_Tran_Ref") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="90px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Challan No">
                                <ItemTemplate>
                                    <asp:Label ID="lblMrrChlnNo" runat="server" Text='<%# Bind("Trn_Hdr_Com1") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="90px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblMrrItemCode" runat="server" Text='<%# Bind("Trn_Det_Icode") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblMrrItemName" runat="server" Text='<%# Bind("Trn_Det_Itm_Desc") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblMrrItemUnit" runat="server" Text='<%# Bind("Trn_Det_Itm_Uom") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblMrrQty" runat="server" Text='<%# Bind("Trn_Det_Lin_Qty", "{0:0.00}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <FooterTemplate>
                                    Total:
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMrrRate" runat="server" Text='<%# Bind("Trn_Det_Lin_Rat") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <FooterTemplate>
                                    <asp:Label ID="lblTotMrrAmt" runat="server" Text="Label"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMrrAmount" runat="server" Text='<%# Bind("Trn_Det_Lin_Amt", "{0:0.00}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Add To Bill" Visible="False">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMrr" runat="server" AutoPostBack="true" OnCheckedChanged="chkMrr_CheckedChanged" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Add To Bill" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnAcceptData" runat="server" CausesValidation="False" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                        CommandName="cmdAddToBill" ImageUrl="~/Image/accept-icon.png" ToolTip="Visited" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                        <RowStyle Font-Size="8pt" BackColor="White" ForeColor="#330099" />
                        <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" Font-Bold="True" />
                        <SortedAscendingCellStyle BackColor="#FEFCEB" />
                        <SortedAscendingHeaderStyle BackColor="#AF0101" />
                        <SortedDescendingCellStyle BackColor="#F6F0C0" />
                        <SortedDescendingHeaderStyle BackColor="#7E0000" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                    <br />
                </fieldset>
                <br />
            </div>
            <div align="center" style="background-color: #FFCC66">
                <fieldset style="background-color: #FFCC66">
                    <legend>Bill Details</legend>
                    <br />
                    <asp:GridView ID="gvBillDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Size="10pt" ForeColor="#333333" OnRowDataBound="gvBillDet_RowDataBound"
                        ShowFooter="True" OnRowCommand="gvBillDet_RowCommand">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MRR Ref No">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillMrrRefNo" runat="server" Text='<%# Bind("Trn_Hdr_Ref_No") %>'></asp:Label>
                                    <asp:HiddenField ID="hfBillMrrTrnDetLno" runat="server" Value='<%# Bind("Trn_Det_Lno") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MRR Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillMrrDate" runat="server" Text='<%# Eval("Trn_Hdr_Date", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO Ref.">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillPoRef" runat="server" Text='<%# Bind("Trn_Hdr_Tran_Ref") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Challan No">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillChlnNo" runat="server" Text='<%# Bind("Trn_Hdr_Com1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillItemCode" runat="server" Text='<%# Bind("Trn_Det_Icode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillItemName" runat="server" Text='<%# Bind("Trn_Det_Itm_Desc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillItemUnit" runat="server" Text='<%# Bind("Trn_Det_Itm_Uom") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillQty" runat="server" Text='<%# Bind("Trn_Det_Lin_Qty", "{0:0.00}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <FooterTemplate>
                                    Total :
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBillRate" runat="server" Text='<%# Bind("Trn_Det_Lin_Rat") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <FooterTemplate>
                                    <asp:Label ID="lblTotAmt" runat="server"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBillAmount" runat="server" Text='<%# Bind("Trn_Det_Lin_Amt", "{0:N2}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remove" Visible="False">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkBill" runat="server" AutoPostBack="true" OnCheckedChanged="chkBll_CheckedChanged" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remove" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnDeleteData" runat="server" CausesValidation="False" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                        CommandName="Dismiss" ImageUrl="~/Image/Delete.png" ToolTip="Dismiss" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle Font-Size="8pt" BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                    <br />
                    <table style="width: 95%; font-size: small;" align="left">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td align="left" style="width: 91px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 531px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 36px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 107px">
                                Bill Amount
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" style="width: 117px">
                                <asp:TextBox ID="txtBillAmt" runat="server" CssClass="textAlignCenter" Enabled="False"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtBillAmt_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtBillAmt"
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
                            <td>
                                &nbsp;
                            </td>
                            <td align="left" style="width: 91px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 531px">
                                <asp:TextBox ID="txtSearchGLCoa" runat="server" CssClass="search textAlignCenter"
                                    Width="520px" Visible="False"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchAcc" runat="server" BehaviorID="AutoCompleteSrchAcc"
                                    CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                    ServiceMethod="GetSrchCoa" ServicePath="~/Module/Accounts/Forms/wsAutoComAcc.asmx"
                                    ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearchGLCoa">
                                </cc1:AutoCompleteExtender>
                            </td>
                            <td align="left" style="width: 36px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 107px">
                                Adjust Amount
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" style="width: 117px">
                                <asp:TextBox ID="txtBillAdjAmt" runat="server" CssClass="textAlignCenter" Enabled="False"></asp:TextBox>
                                <%--<cc1:FilteredTextBoxExtender ID="txtBillAdjAmt_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtBillAdjAmt"
                                    ValidChars=".">
                                </cc1:FilteredTextBoxExtender>--%>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td align="left" style="width: 91px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 531px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 36px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 107px">
                                Total Bill Amount
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" style="width: 117px">
                                <asp:TextBox ID="txtTotBillAmt" runat="server" CssClass="textAlignCenter"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                    FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtTotBillAmt"
                                    ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTotBillAmt"
                                    ErrorMessage="Enter Total Bill Amount" ForeColor="Red" ValidationGroup="btnPost">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td align="left" style="width: 91px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="center" style="width: 531px">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTotBillAmt"
                                    ErrorMessage="Enter Total Bill Amount" ForeColor="Red" ValidationGroup="btnPost"></asp:RequiredFieldValidator>
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSupBillNo"
                                    ErrorMessage="Enter Supplier Bill No" ForeColor="Red" ValidationGroup="btnPost"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" style="width: 36px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 107px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 117px">
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
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="btnHold" runat="server" Text="Hold" Width="120px" Visible="False" />
                    &nbsp;<asp:Button ID="btnPost" runat="server" Text="Post" Width="120px" OnClick="btnPost_Click"
                        Visible="False" ValidationGroup="btnPost" />
                    <br />
                    <br />
                </fieldset>
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
