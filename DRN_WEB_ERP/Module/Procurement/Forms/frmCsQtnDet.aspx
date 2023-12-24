<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmCsQtnDet.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmCsQtnDet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Quotation Details</div>
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
            width: 250px;
        }
    </style>
    <div style="background-color: #B9DCFF">
        <table style="width: 100%;">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 500px; font-size: 12px;">
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
                                C/S Ref
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
                                <asp:Label ID="lblproduct" runat="server" Width="350px"></asp:Label>
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
                <td>
                    Last Five(5) Purchase History<asp:GridView ID="gvRecentRate" runat="server" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" Font-Size="8pt">
                        <Columns>
                            <asp:BoundField DataField="PO_Hdr_Com10" HeaderText="Supplier Name">
                                <ItemStyle Width="250px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="P.O Ref. No">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPoRef" runat="server" Text='<%# Bind("PO_Det_Ref") %>' OnClick="lnkPoRef_Click"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CS Ref. No">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkCsRef" runat="server" Text='<%# Bind("PO_Det_Bat_No") %>'
                                        OnClick="lnkCsRef_Click"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="90px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="PO_Hdr_DATE" DataFormatString="{0:dd-MMM-yy}" HeaderText="Date">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="70px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PO_Det_Lin_Rat" HeaderText="Rate" DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <table class="tblmas" style="width: 100%" id="tblmaster" runat="server">
        <tr>
            <td class="tbl" style="height: 24px; text-align: center">
                <asp:Button ID="btnQtnPrint" runat="server" Text="Print" OnClick="btnQtnPrint_Click"
                    Width="80px" Visible="False" />
            </td>
        </tr>
        <tr>
            <td class="tbl" style="height: 24px; text-align: left">
                <table id="tblquotation" runat="server" style="width: 100%">
                    <tr>
                        <td class="tbl">
                            <table id="tbl_product" width="100%" runat="server" style="border-top-width: 1px;
                                border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='#ffffff', startcolorstr='#e6e6fa', gradienttype='0');
                                border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa;
                                border-right-width: 1px; border-right-color: #e6e6fa;">
                                <tr>
                                    <td>
                                        <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" />
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
                                                                <td>
                                                                    <asp:Button ID="btncancel" runat="server" CssClass="btn2" Text="Close" Width="80px" />
                                                                    &nbsp;
                                                                    <asp:Button ID="btnedit" runat="server" CssClass="btn2" Text="Edit Terms &amp; Condition"
                                                                        Width="207px" OnClick="btnedit_Click" />
                                                                    &nbsp;
                                                                    <asp:Button ID="btneditval" runat="server" CssClass="btn2" OnClick="btneditval_Click"
                                                                        Text="Edit Rate &amp; Others" Width="155px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style5">
                                                                </td>
                                                                <td align="center">
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
                                        <asp:Button ID="Button2" runat="server" Text="Button" Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <span style="font-size: 10pt; color: #000099"><strong>QUOTATION DETAILS</strong></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="celquotation" runat="server" style="text-align: left">
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
                                                    <%--<input id="Radio1" runat="server" type="radio" name="MaritalStatus" value="1">--%>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
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
                                                    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox2_CheckedChanged" />
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
                                                    <asp:CheckBox ID="CheckBox3" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox3_CheckedChanged" />
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
                                                    <asp:CheckBox ID="CheckBox4" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox4_CheckedChanged" />
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
                                                    <asp:CheckBox ID="CheckBox5" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox5_CheckedChanged" />
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
                                                    <asp:CheckBox ID="CheckBox6" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox6_CheckedChanged" />
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
                                                    <asp:CheckBox ID="CheckBox7" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox7_CheckedChanged" />
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
                                                    <asp:CheckBox ID="CheckBox8" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox8_CheckedChanged" />
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
                                                    <asp:CheckBox ID="CheckBox9" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox9_CheckedChanged" />
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
                                                    <asp:CheckBox ID="CheckBox10" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox10_CheckedChanged" />
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
                                                    <asp:CheckBox ID="CheckBox11" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox11_CheckedChanged" />
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
                                                    <asp:CheckBox ID="CheckBox12" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox12_CheckedChanged" />
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
                                                    <asp:CheckBox ID="CheckBox13" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox13_CheckedChanged" />
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
                                                    <asp:CheckBox ID="CheckBox14" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox14_CheckedChanged" />
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
                                                    <asp:CheckBox ID="CheckBox15" runat="server" AutoPostBack="True" 
                                                        oncheckedchanged="CheckBox15_CheckedChanged" />
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
                                    <td style="text-align: left" class="style2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td id="Td2" runat="server" align="center">
                                        <asp:Button ID="btnDeleteQtn" runat="server" OnClick="btnDeleteQtn_Click" Text="DELETE QUOTATION"
                                            Enabled="False" />
                                        &nbsp;<asp:Button ID="btnAddQtn" runat="server" OnClick="Button2_Click" Text="ADD QUOTATION" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
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
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
