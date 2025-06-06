﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmQuotCsInq.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmQuotCsInq" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Comparative Statement (C/S) Inquery</div>
    <style type="text/css">
        .style2
        {
            height: 34px;
        }
        
        
        
        .tbl
        {
            font: 10pt verdana;
            font-weight: 300;
            color: #330099;
        }
        
        .style3
        {
            width: 646px;
            font-size: 1em;
            text-align: left;
            border: thin solid #000080;
        }
        .style33
        {
            width: 676px;
            font-size: 1em;
            text-align: left;
            border: thin solid #000080;
        }
        .style4
        {
            text-align: center;
        }
        .style6
        {
            text-align: center;
            font-size: x-small;
        }
        .style5
        {
            width: 123px;
            font-weight: bold;
        }
        .heading
        {
            text-align: left;
        }
        .style36
        {
            width: 235px;
        }
    </style>
    <table class="tblmas" style="width: 100%" id="tblmaster" runat="server">
        <tr>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                Year:<asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
                    Width="100px">
                </asp:DropDownList>
                &nbsp;Month:<asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"
                    Width="100px">
                </asp:DropDownList>
                &nbsp;Total C/S Found:&nbsp;<asp:Label ID="lblcount" runat="server" Text="(0)"></asp:Label>
                &nbsp;
                <asp:DropDownList ID="ddllist" runat="server" Width="500px" OnSelectedIndexChanged="ddllist_SelectedIndexChanged"
                    AutoPostBack="True" CssClass="txtbox">
                </asp:DropDownList>
                <asp:Button ID="btnQtnPrint" runat="server" Text="Print" OnClick="btnQtnPrint_Click"
                    Width="80px" />
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <asp:Label ID="lblQtnStat" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#FF3399"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                <table id="tbltooltip" runat="server" class="style3" style="background-color: #FFFFFF;
                    background-position: center center">
                    <tr>
                        <td class="style4" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style6" colspan="2">
                            TERMS AND CONDITIONS
                        </td>
                    </tr>
                    <tr>
                        <td class="style5" valign="top">
                            General Terms:
                        </td>
                        <td bgcolor="AliceBlue">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style5" valign="top">
                            Special Terms:
                        </td>
                        <td bgcolor="AliceBlue">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style5" valign="top">
                            Pay Terms:
                        </td>
                        <td bgcolor="AliceBlue">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style5" valign="top">
                            Valid Days:
                        </td>
                        <td bgcolor="AliceBlue">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
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
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                <table id="tblquotation" runat="server" style="width: 100%">
                    <tr>
                        <td style="height: 10px">
                            <asp:Panel ID="Panel4" runat="server" BorderStyle="Solid" BorderWidth="2px" CssClass="tbl"
                                DefaultButton="btncancel" Height="500px" ScrollBars="Auto" Style="border-right: black 2px solid;
                                padding-right: 20px; border-top: black 2px solid; padding-left: 20px; display: none;
                                padding-bottom: 20px; border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
                                background-color: white" Width="700px">
                                <table style="border-color: #e6e6fa; border-width: 1px; text-align: left; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='#ffffff', startcolorstr='#e6e6fa', gradienttype='0');
                                    width: 688px;">
                                    <tr>
                                        <td>
                                            <table id="tbltooltippnl" runat="server" class="style33" style="background-color: #FFFFFF;
                                                background-position: center center">
                                                <tr>
                                                    <td class="style4" colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style6" colspan="2">
                                                        TERMS AND CONDITIONS
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5" valign="top">
                                                        General Terms:
                                                    </td>
                                                    <td bgcolor="AliceBlue">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5" valign="top">
                                                        Special Terms:
                                                    </td>
                                                    <td bgcolor="AliceBlue">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5" valign="top">
                                                        Payment Terms:
                                                    </td>
                                                    <td bgcolor="AliceBlue">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5" valign="top">
                                                        Valid Days:
                                                    </td>
                                                    <td bgcolor="AliceBlue">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        &nbsp;
                                                    </td>
                                                    <td align="center">
                                                        <asp:Button ID="btncancel" runat="server" CssClass="btn2" Text="Close" Width="80px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                    </td>
                                                    <td>
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender5" runat="server" DisplayModalPopupID="ModalPopupExtender5"
                                TargetControlID="Button1">
                            </ajaxToolkit:ConfirmButtonExtender>
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
                                CancelControlID="btncancel" PopupControlID="Panel4" TargetControlID="Button1">
                            </ajaxToolkit:ModalPopupExtender>
                            <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tbl">
                            <table id="tbl_product" width="100%" runat="server" style="border-top-width: 1px;
                                border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='#ffffff', startcolorstr='#e6e6fa', gradienttype='0');
                                border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa;
                                border-right-width: 1px; border-right-color: #e6e6fa;">
                                <tr>
                                    <td style="width: 58px">
                                    </td>
                                    <td style="width: 9px">
                                    </td>
                                    <td style="width: 22px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <table style="width: 551px">
                                            <tr>
                                                <td class="style36">
                                                    MPR Ref
                                                </td>
                                                <td style="width: 20px">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblmpr" runat="server" Width="200px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style36">
                                                    QTN Ref
                                                </td>
                                                <td style="width: 20px">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblqref" runat="server" Width="200px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style36">
                                                    Product
                                                </td>
                                                <td style="width: 20px">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblproduct" runat="server" Width="438px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style36">
                                                    Quantity
                                                </td>
                                                <td style="width: 20px">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblqty" runat="server" Width="200px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style36">
                                                    Currnet Stock
                                                </td>
                                                <td style="width: 20px">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblcurstk" runat="server" Width="200px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style36">
                                                    Requisition
                                                </td>
                                                <td style="width: 20px">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblreq" runat="server" Width="200px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style36">
                                                    Remarks
                                                </td>
                                                <td style="width: 20px">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblremarks" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        <span style="font-size: 10pt; color: #000099"><strong>QUOTATION</strong></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="celquotation" runat="server" colspan="3" style="text-align: left">
                                        <table id="tbl_party" runat="server" style="width: 98%">
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
                                                <td bgcolor="#ccccff">
                                                    T&amp;C
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
                                                <td>
                                                    <asp:LinkButton ID="lnktc1" runat="server" OnClick="lnktc1_Click">T&amp;C</asp:LinkButton>
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
                                                <td>
                                                    <asp:LinkButton ID="lnktc2" runat="server" OnClick="lnktc2_Click">T&amp;C</asp:LinkButton>
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
                                                <td>
                                                    <asp:LinkButton ID="lnktc3" runat="server" OnClick="lnktc3_Click">T&amp;C</asp:LinkButton>
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
                                                <td>
                                                    <asp:LinkButton ID="lnktc4" runat="server" OnClick="lnktc4_Click">T&amp;C</asp:LinkButton>
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
                                                <td>
                                                    <asp:LinkButton ID="lnktc5" runat="server" OnClick="lnktc5_Click">T&amp;C</asp:LinkButton>
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
                                                <td>
                                                    <asp:LinkButton ID="lnktc6" runat="server" OnClick="lnktc6_Click">T&amp;C</asp:LinkButton>
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
                                                <td>
                                                    <asp:LinkButton ID="lnktc7" runat="server" OnClick="lnktc7_Click">T&amp;C</asp:LinkButton>
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
                                                <td>
                                                    <asp:LinkButton ID="lnktc8" runat="server" OnClick="lnktc8_Click">T&amp;C</asp:LinkButton>
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
                                                <td>
                                                    <asp:LinkButton ID="lnktc9" runat="server" OnClick="lnktc9_Click">T&amp;C</asp:LinkButton>
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
                                                <td>
                                                    <asp:LinkButton ID="lnktc10" runat="server" OnClick="lnktc10_Click">T&amp;C</asp:LinkButton>
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
                                                <td>
                                                    <asp:LinkButton ID="lnktc11" runat="server" OnClick="lnktc11_Click">T&amp;C</asp:LinkButton>
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
                                                <td>
                                                    <asp:LinkButton ID="lnktc12" runat="server" OnClick="lnktc12_Click">T&amp;C</asp:LinkButton>
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
                                                <td>
                                                    <asp:LinkButton ID="lnktc13" runat="server" OnClick="lnktc13_Click">T&amp;C</asp:LinkButton>
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
                                                <td>
                                                    <asp:LinkButton ID="lnktc14" runat="server" OnClick="lnktc14_Click">T&amp;C</asp:LinkButton>
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
                                                <td>
                                                    <asp:LinkButton ID="lnktc15" runat="server" OnClick="lnktc15_Click">T&amp;C</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: left" class="style2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td id="Td1" runat="server" colspan="3" style="text-align: center" class="tbl" bgcolor="#9999FF">
                                        COMMENTS
                                    </td>
                                </tr>
                                <tr>
                                    <td id="Td2" runat="server" colspan="3" style="text-align: left">
                                        <asp:PlaceHolder ID="phcomments" runat="server"></asp:PlaceHolder>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
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
</asp:Content>
