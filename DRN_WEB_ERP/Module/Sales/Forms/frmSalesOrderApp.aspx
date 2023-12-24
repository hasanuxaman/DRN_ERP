<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmSalesOrderApp.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesOrderApp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CalcOrdAmount(OrdQty, OrdRate, OrdAmt, TranRate, FreeBag, NetAmt, OrgOrdQty, OrgFreeQty) {
            var OrderQty = 0;
            if (document.getElementById(OrdQty).value != '') {
                OrderQty = parseFloat(document.getElementById(OrdQty).value);
            }
            if (OrderQty > OrgOrdQty) {
                document.getElementById(OrdQty).value = 0;
                OrderQty = 0;
                alert("You are not allowed to approve more than qty: " + OrgOrdQty.toString());
            }
            //else {
            //    document.getElementById(btnRej).disabled = true;
            //    document.getElementById(btnAppr).disabled = true;
            //}
            //alert("Ord Qty" + OrderQty.toString());

            var ItemRate = 0;
            ItemRate = parseFloat(OrdRate);
            //alert(ItemRate.toString());            

            //alert('Chk Org Ord Qty' + OrgOrdQty);
            var OrgQty = 0;
            OrgQty = parseInt(OrgOrdQty);
            //OrgQty = (OrgOrdQty);
            //alert('Org Ord Qty' + OrgQty);


            var OrgBnsQty = 0;
            OrgBnsQty = parseFloat(OrgFreeQty);
            //alert('Org Bns Qty' + OrgBnsQty.toString());

            var TransRate = 0;
            TransRate = parseFloat(TranRate);
            //alert(TransRate.toString());

            var OrderAmount = document.getElementById(OrdAmt);
            var OrderValue = parseFloat(OrderQty * ItemRate);
            //alert(OrderValue.toString());
            OrderAmount.value = OrderValue.toFixed(2);

            var FreeQty = document.getElementById(FreeBag);
            var FreeBagQty = parseFloat((OrderQty * OrgBnsQty) / OrgQty);
            //alert('Free Bag' + FreeBagQty.toString());
            FreeQty.value = FreeBagQty.toFixed(2);

            var NetAmount = document.getElementById(NetAmt);
            var NetValue = parseFloat((OrderQty * ItemRate) + (OrderQty * TransRate));
            //alert(NetValue.toString());
            NetAmount.value = NetValue.toFixed(2);
        }             
    </script>
    <div align="center" style="background-color: #00FF99">
        Sales Order Approval
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <br />
                <asp:GridView ID="gvPendSoHdr" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                    GridLines="Horizontal" OnRowDataBound="gvPendSoHdr_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Pending S/O List">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfCustRef" runat="server" Value='<%# Bind("Par_Adr_Ref") %>' />
                                <div align="center" style="background-color: #9999FF">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" bgcolor="#3366FF">
                                        <tr>
                                            <td align="left" style="width: 40%;">
                                                <asp:Label ID="Label4" runat="server" BorderStyle="None" ForeColor="White" Text="Customer Name: "
                                                    Font-Size="10pt"></asp:Label>
                                                <asp:Label ID="lblCustName" runat="server" BorderStyle="None" ForeColor="White" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 15%;">
                                                <asp:Label ID="Label1" runat="server" BorderStyle="None" ForeColor="White" Text="Credit Limit: "
                                                    Font-Size="10pt"></asp:Label>
                                                <asp:Label ID="lblCrLimit" runat="server" BorderStyle="None" ForeColor="White" Text="0.00"
                                                    Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td align="center" style="width: 15%">
                                                <asp:Label ID="Label3" runat="server" BorderStyle="None" ForeColor="White" Text="Outstanding: "
                                                    Font-Size="10pt"></asp:Label>
                                                <asp:Label ID="lblCrLimitOutStand" runat="server" BorderStyle="None" ForeColor="White"
                                                    Text="0.00" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 15%">
                                                <asp:Label ID="Label2" runat="server" BorderStyle="None" ForeColor="White" Text="Available Cr Limit: "
                                                    Font-Size="10pt"></asp:Label>
                                                <asp:Label ID="txtCrLimitBal" runat="server" BorderStyle="None" ForeColor="White"
                                                    Text="0.00" Font-Size="10pt"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 15%">
                                                <asp:Label ID="Label5" runat="server" BorderStyle="None" Font-Size="10pt" ForeColor="White"
                                                    Text="Security: "></asp:Label>
                                                <asp:LinkButton ID="lnkBtnCrSecurity" runat="server" Font-Size="9" ForeColor="#FFFF99"
                                                    OnClick="lnkBtnCrSecurity_Click">0.00</asp:LinkButton>
                                            </td>
                                            <td align="right" style="width: 20%">
                                                <asp:LinkButton ID="lnkBtnCrRpt" runat="server" ForeColor="#FFFF99" Width="90px"
                                                    OnClick="lnkBtnCrRpt_Click" Font-Size="9">Aging Report</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 40%;">
                                                <asp:Label ID="Label6" runat="server" BorderStyle="None" Font-Size="10pt" ForeColor="White"
                                                    Text="Todays's Collection:"></asp:Label>
                                                <asp:Label ID="lblTodaysCollection" runat="server" BorderStyle="None" Font-Size="10pt"
                                                    ForeColor="White"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 15%;">
                                                &nbsp;
                                            </td>
                                            <td align="center" style="width: 15%">
                                                &nbsp;
                                            </td>
                                            <td align="right" style="width: 15%">
                                                &nbsp;
                                            </td>
                                            <td align="right" style="width: 15%">
                                                &nbsp;
                                            </td>
                                            <td align="right" style="width: 20%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <asp:GridView ID="gvPendSoDet" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="8pt"
                                    PageSize="25" OnRowDataBound="gvPendSoDet_RowDataBound">
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgBtnDismiss" runat="server" CausesValidation="False" 
                                                    Width="20px" ImageUrl="~/Image/Delete.png" OnClick="imgBtnDismiss_Click" OnClientClick="return confirm('Do you want to dismiss this sales order?')"
                                                    ToolTip="Dismiss" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgBtnInfo" runat="server" CommandName="Info" Height="20px"
                                                    Width="20px" ImageUrl="~/Image/about-512.png" ToolTip='<%# Bind("SO_Hdr_Com8") %>'
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="imgBtnInfo_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="S/O Ref. No">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfParAdrRef" runat="server" Value='<%# Bind("Par_Adr_Ref") %>' />
                                                <asp:HiddenField ID="hfSoHdrRef" runat="server" Value='<%# Bind("SO_Hdr_Ref") %>' />
                                                <asp:HiddenField ID="hfSoDetLno" runat="server" Value='<%# Bind("SO_Det_Lno") %>' />
                                                <asp:Label ID="lblSoHdrRefNo" runat="server" Text='<%# Bind("SO_Hdr_Ref_No") %>'></asp:Label>
                                                <asp:HiddenField ID="hfSoDetAppStat" runat="server" Value='<%# Bind("SO_Det_T_Fl") %>' />
                                                <asp:HiddenField ID="hfSoDetRejStat" runat="server" Value='<%# Bind("SO_Det_T_In") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SO_Det_Itm_Desc" HeaderText="Item Desc." />
                                        <asp:BoundField DataField="SO_Det_Itm_Uom" HeaderText="Unit" />
                                        <asp:TemplateField HeaderText="Ord. Qty" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#00CC99">
                                            <ItemTemplate>
                                                <%--<asp:HiddenField ID="hfOrgOrdQty" runat="server" Value='<%# Eval("SO_Det_Lin_Qty") %>' />--%>
                                                <%--<asp:Label ID="lblOrgOrdQty" runat="server" Text='<%# String.Format("{0:n}", (Convert.ToDecimal(Eval("SO_Det_T_C1")))) %>'
                                                    Font-Bold="True"></asp:Label>--%>
                                                <asp:Label ID="lblOrgOrdQty" runat="server" Text='<%# Eval("SO_Det_Lin_Qty") %>'
                                                    Font-Bold="True"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle BackColor="#00CC99" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Appr. Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAppOrdQty" runat="server" Width="80px" CssClass="textAlignCenter"
                                                    Text='<%# Bind("SO_Det_Lin_Qty", "{0:n}") %>' Font-Size="8"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="txtAppOrdQty_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtAppOrdQty"
                                                    ValidChars=".">
                                                </cc1:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ord. Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrdRate" runat="server" Text='<%# Bind("SO_Det_Lin_Rat", "{0:n}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOrdAmt" runat="server" Enabled="false" Width="80px" CssClass="textAlignRight transparent"
                                                    Font-Size="8" Text='<%# String.Format( "{0:n}", (Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Lin_Rat"))) ) %>'
                                                    BorderStyle="None"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" BackColor="#CC99FF" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Free Bag">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfOrgFreeQty" runat="server" Value='<%# Bind("SO_Det_Free_Qty", "{0:n}") %>' />
                                                <asp:TextBox ID="txtAppFreeQty" runat="server" Enabled="false" Width="50px" CssClass="textAlignCenter transparent"
                                                    BorderStyle="None" Font-Size="8" Text='<%# Bind("SO_Det_Free_Qty", "{0:n}") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Trans. Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrdTransRat" runat="server" Text='<%# Bind("SO_Det_Trans_Rat", "{0:n}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOrdNetAmt" runat="server" Enabled="false" Width="90px" CssClass="textAlignRight transparent"
                                                    BorderStyle="None" Font-Size="8" Font-Bold="True" Text='<%# Bind("SO_Det_Lin_Net", "{0:n}") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="90px" BackColor="#66FF66" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="100"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnRejectDo" runat="server" OnClick="btnRejectDo_Click" OnClientClick="return confirm('Do you want to Reject?')"
                                                    Text="Reject" Width="50px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnApprDo" runat="server" OnClick="btnApprDo_Click" OnClientClick="return confirm('Do you want to Approve?')"
                                                    Text="Approve" Width="60px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
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
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                </asp:GridView>
                <br />
                <asp:Button ID="btnShowApprDo" runat="server" Text="Show Last 100 Approved S/O" OnClick="btnShowApprDo_Click" />
                <br />
                <asp:GridView ID="gvApprDoDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" Font-Size="8pt" Visible="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Par_Adr_Name" HeaderText="Customer Name" />
                        <asp:TemplateField HeaderText="S/O Ref. No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfSoHdrRef" runat="server" Value='<%# Bind("SO_Hdr_Ref") %>' />
                                <asp:HiddenField ID="hfSoDetLno" runat="server" Value='<%# Bind("SO_Det_Lno") %>' />
                                <asp:Label ID="lblSoHdrRefNo" runat="server" Text='<%# Bind("SO_Hdr_Ref_No") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SO_Hdr_Com9" DataFormatString="{0:d}" HeaderText="App. Date" />
                        <asp:BoundField DataField="SO_Det_Itm_Desc" HeaderText="Item Desc." />
                        <asp:BoundField DataField="SO_Det_Itm_Uom" HeaderText="Unit" />
                        <asp:TemplateField HeaderText="Order Qty" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#00CC99">
                            <ItemTemplate>
                                <asp:Label ID="lblOrgOrdQty" runat="server" Text='<%# String.Format("{0:n}", (Convert.ToDecimal(Eval("SO_Det_T_C1")))) %>'
                                    Font-Bold="True"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#00CC99" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="SO_Det_Lin_Qty" HeaderText="App. Qty" DataFormatString="{0:n}">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdRate" runat="server" Text='<%# Bind("SO_Det_Lin_Rat", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:TextBox ID="txtOrdAmt" runat="server" Enabled="false" Width="70px" CssClass="textAlignRight transparent"
                                    Font-Size="8" Text='<%# String.Format( "{0:n}", (Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Lin_Rat")))) %>'
                                    BorderStyle="None"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" BackColor="#CC99FF" Width="70px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Free Bag">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfOrgFreeQty" runat="server" Value='<%# String.Format("{0:n}", (Convert.ToDecimal(Eval("SO_Det_Free_Qty")))) %>' />
                                <asp:TextBox ID="txtOrdFreeQty" runat="server" Enabled="false" Width="50px" CssClass="textAlignCenter transparent"
                                    BorderStyle="None" Font-Size="8" Text='<%# Bind("SO_Det_Free_Qty", "{0:n}") %>'></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trans. Rate">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdTransRat" runat="server" Text='<%# Bind("SO_Det_Trans_Rat", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Net Amount">
                            <ItemTemplate>
                                <asp:TextBox ID="txtOrdNetAmt" runat="server" Enabled="false" Width="80px" CssClass="textAlignRight transparent"
                                    BorderStyle="None" Font-Size="8" Font-Bold="True" Text='<%# String.Format( "{0:n}", ((Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Lin_Rat"))) + (Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Trans_Rat"))))) %>'></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Width="80px" BackColor="#66FF66" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="8pt"
                        Wrap="false" />
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
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvPendSoHdr" EventName="RowDataBound" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlMsgInfo" runat="server" Style="border-right: black 2px solid; padding-right: 20px;
        display: none; border-top: black 2px solid; padding-left: 20px; padding-bottom: 20px;
        border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
        background-color: #E1FBF1" ForeColor="Blue" Width="510px" Font-Size="Small">
        <table id="tblMsgInfo" runat="server" style="width: 100%;">
            <tr>
                <td bgcolor="#33CCCC" align="center">
                    <asp:Label ID="lblMsgInfo" runat="server" Font-Bold="True">Approval Status Info</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtMsgInfo" runat="server" Enabled="false" Height="250px" TextMode="MultiLine"
                        Width="500px" MaxLength="255" BackColor="#99FFCC"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <div align="center">
            <asp:Button ID="btnMsgInfoOk" runat="server" Text="OK" Width="80px" />
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hfHiddenMsgInfo" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupExtenderMsgInfo" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnMsgInfoOk" OkControlID="btnMsgInfoOk" PopupControlID="pnlMsgInfo"
        TargetControlID="hfHiddenMsgInfo" DropShadow="true">
    </cc1:ModalPopupExtender>
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
