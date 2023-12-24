<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPoPrintSPO.aspx.cs"
    Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmPoPrintSPO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Purchase Order Print</title>
    <style type="text/css">
        p.MsoNormal
        {
            margin-bottom: .0001pt;
            font-size: 10.0pt;
            font-family: "Times New Roman" , "serif";
            margin-left: 0in;
            margin-right: 0in;
            margin-top: 0in;
        }
        .style4
        {
            font-size: 20pt;
            font-family: "Times New Roman" , Times, serif;
        }
        .style5
        {
            height: 23px;
        }
        .style6
        {
            font-weight: bold;
            color: white;
            background-color: #6B7EBF;
            font-style: normal;
            font-variant: normal;
            font-size: 11pt;
            line-height: normal;
            font-family: verdana;
        }
        .style11
        {
            height: 18px;
            width: 677px;
        }
        .style12
        {
            width: 677px;
        }
        .style13
        {
            height: 0px;
            width: 677px;
        }
        .style14
        {
            height: 10px;
            width: 677px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
            <tr>
                <td style="text-align: center">
                    <table style="width: 100%;">
                        <tr>
                            <td class="style4" style="vertical-align: top; text-align: center;">
                                <asp:Image ID="Image1" runat="server" Height="36px" Width="48px" />
                                Eastern Cement Industries Ltd.
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                Walso Tower, Level-1&amp;3, 21 &amp; 23 Kazi Nazrul Islam Avenue, Dhaka-1000, Bangladesh,
                                Telephone: +88 02 966 5338-40, 09613787878, Fax: +88 0258614645
                            </td>
                        </tr>
                        <tr>
                            <td class="style5">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style6" style="text-align: center">
                    PURCHASE ORDER
                </td>
            </tr>
            <tr>
                <td>
                    <table id="tbldet" runat="server" style="width: 865px; background-color: #f0f8ff;
                        text-align: left">
                        <tr>
                            <td class="style11">
                                Date:
                                <asp:Label ID="lbldate" runat="server" Font-Bold="False" Width="138px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                PO Ref:
                                <asp:Label ID="lblporef" runat="server" Font-Bold="False" Width="333px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; text-align: left" class="style12">
                                To: &nbsp;&nbsp;
                                <asp:Label ID="lblto" runat="server" Font-Bold="True" Width="508px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbladd" runat="server" Font-Bold="False" Width="508px" Height="41px"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                Sub:&nbsp;
                                <asp:Label ID="lblsub" runat="server" Font-Bold="True" Width="508px">Purchase Order</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                <asp:GridView ID="gvSPODet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    Font-Size="10pt" ForeColor="#333333" ShowFooter="True" OnRowDataBound="gvSPODet_RowDataBound">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PO_Det_Itm_Desc" HeaderText="Item Name">
                                            <ItemStyle Width="280px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PO_Det_Itm_Uom" HeaderText="Unit">
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PO_Det_Lin_Qty" HeaderText="Quantity">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("PO_Det_Lin_Rat") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Total:
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("PO_Det_Lin_Amt","{0:n2}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblGrandTotal" runat="server" Text=""></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="120px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="style13">
                            </td>
                        </tr>
                        <tr>
                            <td class="style13">
                                <b>Total Amount TK:</b>
                                <asp:Label ID="lbltot" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style13">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td id="payterms" runat="server" class="style13">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td id="daycount" runat="server" class="style14">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                Approved By,<br />
                                ------------------</td>
                        </tr>
                        <tr>
                            <td class="style11">
                                <br />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
