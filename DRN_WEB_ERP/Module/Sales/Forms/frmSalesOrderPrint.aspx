<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="frmSalesOrderPrint.aspx.cs"
    Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesOrderPrint" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sales Order Entry</title>
    <style type="text/css">
        .style3
        {
        }
        .style6
        {
            width: 131px;
        }
        .search
        {
        }
        .style11
        {
            width: 282px;
        }
        .style12
        {
            width: 162px;
        }
        .style14
        {
            width: 129px;
        }
        .style15
        {
            width: 112px;
        }
        .style16
        {
            width: 102px;
        }
        .style18
        {
            width: 182px;
        }
        .style19
        {
            width: 436px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="background-color: #00FF99; font-size: x-large;">
        Sales Order</div>
    <div>
        <br />
        <asp:Panel ID="pnlOrdHdr" runat="server">
            <div align="center" style="height: 30px;">
                <table style="width: 100%;" border="1">
                    <tr>
                        <td align="left" class="style19">
                            <asp:Label ID="Label3" runat="server" ForeColor="Black" Text="Order Ref: " CssClass="style3"
                                Font-Bold="True"></asp:Label>
                            <asp:Label ID="lblOrdRefNo" runat="server" ForeColor="Black" Text="Label" Font-Bold="True"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label1" runat="server" BorderStyle="None" ForeColor="Black" Text="Order Quantity: "
                                Font-Bold="True"></asp:Label>
                            <asp:Label ID="lblTotOrdQty" runat="server" BorderStyle="None" ForeColor="Black"
                                Text="0.00" Font-Bold="True"></asp:Label>
                        </td>
                        <td align="right">
                            &nbsp;
                            <asp:Label ID="Label2" runat="server" BorderStyle="None" ForeColor="Black" Text="Order Value: "
                                Font-Bold="True"></asp:Label>
                            <asp:Label ID="lblTotOrdVal" runat="server" BorderStyle="None" ForeColor="Black"
                                Text="0.00" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <br />
                <table style="width: 100%; font-family: verdana; font-size: small;">
                    <tr>
                        <td class="style18">
                            &nbsp;
                        </td>
                        <td class="style14">
                            Date
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td class="style12">
                            <asp:Label ID="lblOrdDate" runat="server"></asp:Label>
                        </td>
                        <td style="width: 23px">
                            &nbsp;
                        </td>
                        <td class="style15">
                            Exp. Del. Date
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td class="style6">
                            <asp:Label ID="lblExpDelDate" runat="server"></asp:Label>
                        </td>
                        <td style="width: 22px">
                            &nbsp;
                        </td>
                        <td class="style16">
                            Customer
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td class="style11">
                            <asp:Label ID="lblCust" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style18">
                            &nbsp;
                        </td>
                        <td class="style14">
                            Prefix
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td class="style12">
                            <asp:Label ID="lblSalePrefix" runat="server"></asp:Label>
                        </td>
                        <td style="width: 23px">
                            &nbsp;
                        </td>
                        <td class="style15">
                            Valid Before
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td class="style6">
                            <asp:Label ID="lblOrdValidDate" runat="server"></asp:Label>
                        </td>
                        <td style="width: 22px">
                            &nbsp;
                        </td>
                        <td class="style16">
                            Location
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td class="style11">
                            <asp:Label ID="lblLoc" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style18">
                            &nbsp;
                        </td>
                        <td class="style14">
                            Transport Terms
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td class="style12">
                            <asp:Label ID="lblTransBy" runat="server"></asp:Label>
                        </td>
                        <td style="width: 23px">
                            &nbsp;
                        </td>
                        <td class="style15">
                            Transport Mode
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td class="style6">
                            <asp:Label ID="lblTransType" runat="server"></asp:Label>
                        </td>
                        <td style="width: 22px">
                            &nbsp;
                        </td>
                        <td class="style16">
                            Sales Person
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td class="style11">
                            <asp:Label ID="lblSp" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style18">
                            &nbsp;
                        </td>
                        <td class="style14">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td class="style12">
                            &nbsp;
                        </td>
                        <td style="width: 23px">
                            &nbsp;
                        </td>
                        <td class="style15">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td class="style6">
                            &nbsp;
                        </td>
                        <td style="width: 22px">
                            &nbsp;
                        </td>
                        <td class="style16">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td class="style11">
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
        </asp:Panel>
    </div>
    <div align="center">
        <asp:Panel ID="pnlOrdDet" runat="server" GroupingText="Order Details">
            <div align="center">
                &nbsp;<asp:GridView ID="gvOrdDet" runat="server" AutoGenerateColumns="False" Font-Size="10pt">
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdICode" runat="server" Text='<%# Bind("ORD_ITEM_REF") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ORD_ITEM_NAME" HeaderText="Item Desc." />
                        <asp:BoundField DataField="ORD_ITEM_UOM" HeaderText="Unit" />
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdQty" runat="server" Text='<%# Bind("ORD_QTY") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="True" HorizontalAlign="Center" Width="80px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="ORD_RATE" DataFormatString="{0:c}" HeaderText="Rate">
                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ORD_AMOUNT" DataFormatString="{0:c}" HeaderText="Amount">
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="90px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ORD_FREE_BAG" DataFormatString="{0:c}" HeaderText="Free Bag">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ORD_TRANS_RATE" DataFormatString="{0:c}" HeaderText="Transport Rate">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ORD_GRS_AMOUNT" HeaderText="Gross Amount">
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="90px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ORD_DISCOUNT" HeaderText="Discount">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Net Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdNetAmt" runat="server" Text='<%# Bind("ORD_NET_AMOUNT", "{0:c}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="100px" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle Wrap="False" />
                </asp:GridView>
                <br />
                <asp:HiddenField ID="hfRefNo" runat="server" Value="0" />
                <%--<asp:Label ID="lblOrdValText" runat="server" BackColor="#FFFF66" BorderStyle="Dashed"
                    BorderWidth="1px" Visible="False"></asp:Label>--%>
            </div>
        </asp:Panel>
    </div>
    <div align="center">
        <asp:Panel ID="pnlSoAppStat" runat="server" GroupingText="Approval Status">
            <br />
            <div>
                <asp:Label ID="lblTotSoQty" runat="server" BackColor="#00CC66" BorderStyle="Dashed"
                    BorderWidth="1px" Text="Total Order Qty: 0.00" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="lblTotAppQty" runat="server" BackColor="#CC00FF" BorderStyle="Dashed"
                    BorderWidth="1px" Text="Total Approved Qty: 0.00" Visible="False"></asp:Label>
            </div>
            <br />
            <asp:GridView ID="gvApprSoDet" runat="server" AutoGenerateColumns="False" Font-Size="9pt"
                EmptyDataText="No Data Approved for D/O.">
                <Columns>
                    <asp:TemplateField HeaderText="SL#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SO_Det_Icode" HeaderText="Item Code" />
                    <asp:BoundField DataField="SO_Det_Itm_Desc" HeaderText="Item Desc." />
                    <asp:BoundField DataField="SO_Det_Itm_Uom" HeaderText="Unit" />
                    <asp:TemplateField HeaderText="Order Qty" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblOrdLinQty" runat="server" Font-Bold="True" Text='<%# String.Format("{0:n}", (Convert.ToDecimal(Eval("SO_Det_T_C1")))) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Approved Qty">
                        <ItemTemplate>
                            <asp:Label ID="lblOrdApprQty" runat="server" Text='<%# Bind("SO_Det_Lin_Qty", "{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ord. Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblOrdRate" runat="server" Text='<%# Bind("SO_Det_Lin_Rat", "{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblOrdLinAmt" runat="server" Text='<%# String.Format( "{0:n}", (Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Lin_Rat")))) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Free Bag">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfOrgFreeQty" runat="server" Value='<%# String.Format("{0:n}", (Convert.ToDecimal(Eval("SO_Det_Free_Qty")))) %>' />
                            <asp:Label ID="lblOrdFreeQty" runat="server" Text='<%# Bind("SO_Det_Free_Qty", "{0:n}") %>'></asp:Label>
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
                            <%--<asp:TextBox ID="txtOrdLinNet" runat="server" BorderStyle="None" CssClass="textAlignRight transparent"
                                Enabled="false" Font-Bold="True" Font-Size="8" Text='<%# String.Format( "{0:n}", ((Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Lin_Rat"))) + (Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Trans_Rat"))))) %>'
                                Width="100px"></asp:TextBox>--%>
                            <asp:Label ID="lblOrdLinNet" runat="server" Text='<%# String.Format( "{0:n}", ((Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Lin_Rat"))) + (Convert.ToDecimal(Eval("SO_Det_Lin_Qty")) * Convert.ToDecimal(Eval("SO_Det_Trans_Rat"))))) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Font-Bold="True" Width="100px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle Font-Size="8pt" Wrap="false" />
            </asp:GridView>
            <br />
            <hr />
            <asp:PlaceHolder ID="phcomments" runat="server"></asp:PlaceHolder>
        </asp:Panel>
        <br />
    </div>
    </form>
</body>
</html>
