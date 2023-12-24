<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmPoView.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmPoView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        
        .inline
        {}
        
        .style1
        {
            height: 18px;
            width: 130px;
        }
        .style2
        {
            width: 130px;
        }
        .style3
        {
            height: 39px;
            width: 130px;
        }
        
    </style>
    <div align="center" style="background-color: #00FF99">
        Purchase Order View</div>
    <table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="height: 22px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tblbig" style="text-align: center">
                PURCHASE ORDER
            </td>
        </tr>
        <tr>
            <td class="tbl" style="height: 24px; text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tbl" style="text-align: right">
                <table style="width: 547px">
                    <tr>
                        <td style="text-align: right">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/Print.gif" /><asp:LinkButton
                                ID="lnkView" runat="server" Width="93px" OnClick="lnkView_Click" 
                                CssClass="inline">Print Version</asp:LinkButton>
                        </td>
                        <td style="width: 11px">
                        </td>
                        <td style="text-align: right; width: 141px;">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Image/Mail.gif" />
                            <asp:LinkButton
                                ID="lnkMail" runat="server" OnClick="lnkMail_Click" Width="75px" Height="16px"
                                CssClass="inline">Send Mail </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 800px; text-align: left">
                    <tr>
                        <td class="style1">
                        </td>
                        <td style="width: 629px; height: 18px">
                            Date:
                            <asp:Label ID="lbldate" runat="server" Font-Bold="False" Width="138px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td style="width: 629px; height: 18px">
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td style="width: 629px; height: 18px">
                            PO Ref:
                            <asp:Label ID="lblporef" runat="server" Font-Bold="False" Width="315px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td style="width: 629px; height: 18px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                        </td>
                        <td style="vertical-align: top; width: 629px; text-align: left">
                            To: &nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblto" runat="server" Font-Bold="True" Width="508px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td style="width: 629px; height: 18px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lbladd" runat="server" Font-Bold="False" Width="508px" Height="41px"></asp:Label>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td style="width: 629px; height: 18px">
                            Sub:&nbsp;
                            <asp:Label ID="lblsub" runat="server" Font-Bold="True" Width="508px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td style="width: 629px; height: 18px">
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td style="width: 629px; height: 18px">
                            <table id="tblhtml" runat="server" border="1" bordercolor="#6b7ebf" cellspacing="0"
                                style="width: 99%">
                                <tr>
                                    <td style="width: 27px">
                                        Sl
                                    </td>
                                    <td style="width: 309px">
                                        Item Name
                                    </td>
                                    <td style="width: 77px">
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
                        <td class="style1">
                        </td>
                        <td style="width: 629px; height: 18px">
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;
                        </td>
                        <td>
                            <b>Total Amount Tk:</b>
                            <asp:Label ID="lbltot" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td style="width: 629px; height: 18px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td id="Td1" runat="server" style="width: 629px; height: 18px">
                            <asp:Label ID="lblgen" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td id="genterms" runat="server" style="width: 629px; height: 18px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td style="width: 629px; height: 18px">
                            <asp:Label ID="lblspe" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td id="spterms" runat="server" style="width: 629px; height: 18px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td style="width: 629px; height: 18px">
                            <asp:Label ID="lblpay" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td id="payterms" runat="server" style="width: 629px; height: 18px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td style="width: 629px; height: 18px">
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td id="daycount" runat="server" style="width: 629px; height: 18px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td style="width: 629px; height: 18px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td style="width: 629px; height: 18px">
                            Thanking you.
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td style="width: 629px; height: 18px">
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td style="width: 629px; height: 18px">
                            <asp:Label ID="lblfrom" runat="server" Font-Bold="True" Height="38px" Width="129px"></asp:Label><br />
                            <strong>Eastern Cement Industries Ltd.</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                        </td>
                        <td style="width: 629px; height: 39px">
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td style="width: 629px; height: 18px">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 92px">
            </td>
        </tr>
    </table>
</asp:Content>
