<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="frmQuotCsPrint.aspx.cs"
    Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmQuotCsPrint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Comparative Statement (C/S) Print</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="border: 1px dotted #000000; width: 98%; font-size: medium;
        font-weight: bold;">
        Comparative Statement (C/S)</div>
    <style type="text/css">
        .style2
        {
            height: 34px;
        }
        
        .tbl
        {
            font: 8pt verdana;
            font-weight: 300;
            color: #330099;
        }
        
        .heading
        {
            text-align: left;
        }
        .style37
        {
            width: 96px;
        }
        .style38
        {
            width: 77px;
        }
        .style39
        {
            width: 382px;
        }
    </style>
    <table class="tblmas" style="width: 100%;" id="tblmaster" runat="server">
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <table style="font-size: small">
                    <tr>
                        <td class="style38">
                            QTN Ref
                        </td>
                        <td style="width: 20px">
                            :
                        </td>
                        <td class="style39" width="982">
                            <asp:Label ID="lblqref" runat="server" Width="200px"></asp:Label>
                        </td>
                        <td class="style37">
                            MPR Ref
                        </td>
                        <td>
                            :
                        </td>
                        <td width="380">
                            <asp:Label ID="lblmpr" runat="server" Width="200px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style38">
                            Product
                        </td>
                        <td style="width: 20px">
                            :
                        </td>
                        <td class="style39" width="982">
                            <asp:Label ID="lblproduct" runat="server" Width="438px"></asp:Label>
                        </td>
                        <td class="style37">
                            Currnet Stock
                        </td>
                        <td>
                            :
                        </td>
                        <td width="380">
                            <asp:Label ID="lblcurstk" runat="server" Width="200px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style38">
                            Quantity
                        </td>
                        <td style="width: 20px">
                            :
                        </td>
                        <td class="style39" width="982">
                            <asp:Label ID="lblqty" runat="server" Width="200px"></asp:Label>
                        </td>
                        <td class="style37">
                            Remarks
                        </td>
                        <td>
                            :&nbsp;
                        </td>
                        <td width="380">
                            <asp:Label ID="lblremarks" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                <table id="tblquotation" runat="server" style="width: 100%">
                    <tr>
                        <td class="tbl">
                            <table id="tbl_product" width="100%" runat="server" style="border-top-width: 1px;
                                border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='#ffffff', startcolorstr='#e6e6fa', gradienttype='0');
                                border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa;
                                border-right-width: 1px; border-right-color: #e6e6fa;">
                                <tr>
                                    <td style="text-align: center">
                                        <span style="font-size: 10pt; color: #000099"><strong>QUOTATION</strong></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="celquotation" runat="server" style="text-align: left">
                                        <table id="tbl_party" runat="server" style="border: 1px dotted #000000; width: 98%;
                                            font-size: small;" border="1">
                                            <tr>
                                                <td bgcolor="#ccccff" style="width: 59px">
                                                    Sl
                                                </td>
                                                <td bgcolor="#ccccff" style="width: 6px">
                                                    Code
                                                </td>
                                                <td bgcolor="#ccccff">
                                                    Vendor Details
                                                </td>
                                                <td bgcolor="#ccccff">
                                                    Rate
                                                </td>
                                                <td bgcolor="#ccccff">
                                                    Amount
                                                </td>
                                                <td bgcolor="#ccccff">
                                                    Specificaton
                                                </td>
                                                <td bgcolor="#ccccff">
                                                    Brand
                                                </td>
                                                <td bgcolor="#ccccff">
                                                    Origin
                                                </td>
                                                <td bgcolor="#ccccff">
                                                    Packing
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 59px">
                                                    <input id="Radio1" runat="server" type="radio" name="MaritalStatus" value="1">
                                                </td>
                                                <td style="width: 6px">
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 59px">
                                                    <input id="Radio2" runat="server" type="radio" name="MaritalStatus" value="2">
                                                </td>
                                                <td style="width: 6px">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 59px">
                                                    <input id="Radio3" runat="server" type="radio" name="MaritalStatus" value="3">
                                                </td>
                                                <td style="width: 6px">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 59px">
                                                    <input id="Radio4" runat="server" type="radio" name="MaritalStatus" value="4">
                                                </td>
                                                <td style="width: 6px">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 59px">
                                                    <input id="Radio5" runat="server" type="radio" name="MaritalStatus" value="5">
                                                </td>
                                                <td style="width: 6px">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 59px">
                                                    <input id="Radio6" runat="server" type="radio" name="MaritalStatus" value="6">
                                                </td>
                                                <td style="width: 6px">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 59px">
                                                    <input id="Radio7" runat="server" type="radio" name="MaritalStatus" value="7">
                                                </td>
                                                <td style="width: 6px">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 59px">
                                                    <input id="Radio8" runat="server" type="radio" name="MaritalStatus" value="8">
                                                </td>
                                                <td style="width: 6px">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 59px">
                                                    <input id="Radio9" runat="server" type="radio" name="MaritalStatus" value="9">
                                                </td>
                                                <td style="width: 6px">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 59px">
                                                    <input id="Radio10" runat="server" type="radio" name="MaritalStatus" value="10">
                                                </td>
                                                <td style="width: 6px">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 59px">
                                                    <input id="Radio11" runat="server" type="radio" name="MaritalStatus" value="11">
                                                </td>
                                                <td style="width: 6px">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 59px">
                                                    <input id="Radio12" runat="server" type="radio" name="MaritalStatus" value="12">
                                                </td>
                                                <td style="width: 6px">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 59px">
                                                    <input id="Radio13" runat="server" type="radio" name="MaritalStatus" value="13">
                                                </td>
                                                <td style="width: 6px">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 59px">
                                                    <input id="Radio14" runat="server" type="radio" name="MaritalStatus" value="14">
                                                </td>
                                                <td style="width: 6px">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 59px">
                                                    <input id="Radio15" runat="server" type="radio" name="MaritalStatus" value="15">
                                                </td>
                                                <td style="width: 6px">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
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
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" class="style2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td id="Td1" runat="server" style="text-align: center" class="tbl" bgcolor="#9999FF">
                                        COMMENTS
                                    </td>
                                </tr>
                                <tr style="font-size: small">
                                    <td id="Td2" runat="server" style="text-align: left; font-size: small;">
                                        <asp:PlaceHolder ID="phcomments" runat="server"></asp:PlaceHolder>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div align="center">
        <table style="width: 100%;">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblPepBy" runat="server"></asp:Label>
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center">
                    <asp:Label ID="lblChkBy" runat="server"></asp:Label>
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center">
                    <asp:Label ID="lblFwdBy" runat="server"></asp:Label>
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center">
                    <asp:Label ID="lblVerBy" runat="server"></asp:Label>
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center">
                    <asp:Label ID="lblAppBy" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" style="border-style: dotted none none none; border-width: 2px;
                    border-color: #000000">
                    Prepared By
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center" style="border-style: dotted none none none; border-width: 2px;
                    border-color: #000000">
                    Checked By
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="border-style: dotted none none none; border-width: 2px;
                    border-color: #000000">
                    Forwarded By
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="border-style: dotted none none none; border-width: 2px;
                    border-color: #000000">
                    Verified By
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="border-style: dotted none none none; border-width: 2px;
                    border-color: #000000">
                    Approved By
                </td>
            </tr>
            <tr>
                <td align="center">
                    (SCM)
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center">
                    (SCM)
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center">
                    HOD - SCM
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center">
                    (Audit)
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td align="center">
                    CEO
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
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
    </form>
</body>
</html>
