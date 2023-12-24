<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPoPrint.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmPoPrint" %>

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
                                <asp:Label ID="lblsub" runat="server" Font-Bold="True" Width="508px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                            </td>
                        </tr>
                        <tr>
                            <td class="style11">
                                <table id="tblhtml" runat="server" border="1" bordercolor="#6b7ebf" cellspacing="0"
                                    style="width: 99%">
                                    <tr>
                                        <td style="width: 27px">
                                            Sl
                                        </td>
                                        <td style="width: 209px">
                                            Item Name
                                        </td>
                                        <td style="width: 377px">
                                            Specification
                                        </td>
                                        <td>
                                            Brand
                                        </td>
                                        <td>
                                            Origin
                                        </td>
                                        <td>
                                            Packing
                                        </td>
                                        <td>
                                            Qty
                                        </td>
                                        <td>
                                            Rate
                                        </td>
                                        <td>
                                            Amount
                                        </td>
                                    </tr>
                                </table>
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
                            <td id="Td1" runat="server" class="style13">
                                <asp:Label ID="lblgen" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td id="genterms" runat="server" class="style13">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style13">
                                <asp:Label ID="lblspe" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td id="spterms" runat="server" class="style13">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style13">
                                <asp:Label ID="lblpay" runat="server" Font-Bold="True"></asp:Label>
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
                                Thanking you.
                            </td>
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
                                <asp:Label ID="lblfrom" runat="server" Font-Bold="True" Height="38px" Width="129px"></asp:Label><br />
                                <strong>Eastern Cement Industries Ltd.</strong>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
