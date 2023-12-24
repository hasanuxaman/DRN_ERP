<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="frmCsPrint.aspx.cs"
    Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmCsPrint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Comparative Statement (C/S) Print</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="border: 1px dotted #000000; width: 100%; font-size: medium;
        font-weight: bold; background-color: #E6E6E6;">
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
        .style38
        {
            width: 245px;
        }
        .style39
        {
            width: 382px;
        }
        .textAlignCenter
        {
            text-align: center;
            margin-top: 0px;
        }
        .style40
        {
            width: 722px;
        }
        .style41
        {
            width: 455px;
        }
        .style42
        {
            width: 288px;
        }
        .style43
        {
            width: 10px;
        }
        .style44
        {
            width: 332px;
        }
        .style45
        {
            width: 250px;
        }
        .style46
        {
            width: 257px;
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
                <table style="font-size: small; width: 100%;">
                    <tr>
                        <td class="style38">
                            &nbsp;
                        </td>
                        <td class="style45">
                            CS Ref
                        </td>
                        <td class="style43">
                            :
                        </td>
                        <td class="style39" width="982">
                            <asp:Label ID="lblcsref" runat="server" Width="200px"></asp:Label>
                        </td>
                        <td class="style40">
                            &nbsp;
                        </td>
                        <td class="style41">
                            &nbsp;
                        </td>
                        <td class="style44">
                            &nbsp;
                        </td>
                        <td class="style42">
                            MPR Ref
                        </td>
                        <td>
                            :
                        </td>
                        <td class="style46">
                            <asp:Label ID="lblmpr" runat="server" Width="145px"></asp:Label>
                        </td>
                        <td width="380">
                            &nbsp;
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
                                        <span style="font-size: 10pt; color: #000099"><strong>CS Details</strong></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" align="center">
                                        <asp:GridView ID="gvCS" runat="server" Font-Size="8pt" OnRowDataBound="gvCS_RowDataBound"
                                            OnRowCreated="gvCS_RowCreated" ShowFooter="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Audit Check">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAuditChk" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Font-Size="7pt" Font-Bold="False" />
                                        </asp:GridView>
                                        <br />
                                        <br />
                                        <asp:GridView ID="gvCsSum" runat="server" Font-Size="8pt" AutoGenerateColumns="False"
                                            OnRowDataBound="gvCsSum_RowDataBound">
                                            <Columns>
                                                <asp:BoundField HeaderText="Supplier Name" DataField="Par_Adr_Qtn_Name" />
                                                <asp:TemplateField HeaderText="QTN Value">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfSuppRef" runat="server" Value='<%#  Bind("Qtn_Par_Code") %>' />
                                                        <asp:Label ID="lblQtnVal" runat="server" Text='<%#  Bind("Qtn_Val", "{0:F2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Carrying Charge">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaryCrg" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Loading Charge">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLoadCrg" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount Amt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDisAmt" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grand Total">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGrandTot" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" BackColor="#D6D6D6" Font-Bold="True" Width="100px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Font-Size="8pt" />
                                        </asp:GridView>
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
                    ADO</td>
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
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
