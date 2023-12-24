<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlQtnEntry.ascx.cs"
    Inherits="DRN_WEB_ERP.Module.Procurement.Forms.UserControl.CtlQtnEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<style type="text/css">
    .style1
    {
        width: 60px;
    }
    .style2
    {
        width: 101px;
    }
</style>
<%--<asp:UpdatePanel ID="updentry" runat="server">
    <ContentTemplate>--%>
<table style="width: 90%">
    <tr>
        <td colspan="3" style="height: 10px">
        </td>
    </tr>
    <tr>
        <td class="tblsmall" colspan="3">
            <table style="border-color: #e6e6fa; border-width: 1px; width: 89%; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='#ffffff', startcolorstr='#e6e6fa', gradienttype='0');">
                <tr>
                    <td style="width: 29px">
                        <asp:Label ID="lblsl" runat="server" Text="sl" Font-Bold="True" ForeColor="Navy"></asp:Label>
                    </td>
                    <td colspan="2" style="text-align: left">
                        &nbsp;<asp:Label ID="lblproduct" runat="server" Text="Product" Width="405px" Font-Bold="True"
                            ForeColor="Navy"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 29px">
                    </td>
                    <td colspan="2" style="text-align: left">
                        <table id="tblproduct" runat="server" style="width: 701px">
                            <tr>
                                <td style="font-size: 12px;" class="style2">
                                    Ref No
                                </td>
                                <td style="font-size: 12px;">
                                    :
                                </td>
                                <td colspan="3" style="font-size: 12px">
                                    <asp:Label ID="lblref" runat="server" Text="Label" Width="300px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 12px;" class="style2">
                                    Requisition
                                    Type</td>
                                <td style="font-size: 12px;">
                                    :
                                </td>
                                <td colspan="3" style="font-size: 12px">
                                    <asp:Label ID="lblreqtype" runat="server" Text="Label" Width="300px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 12px;" class="style2">
                                    Item code
                                </td>
                                <td style="font-size: 12px;">
                                    :
                                </td>
                                <td colspan="3" style="font-size: 12px">
                                    <asp:Label ID="lblitmcode" runat="server" Font-Bold="False" Text="code" Width="155px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 12px;" class="style2">
                                    Qty
                                </td>
                                <td style="font-size: 12px;">
                                    :
                                </td>
                                <td colspan="3" style="font-size: 12px">
                                    <asp:Label ID="lblqty" runat="server" Font-Bold="False" Text="qty" Width="90px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 12px;" class="style2">
                                    Specification
                                </td>
                                <td style="font-size: 12px;">
                                    :
                                </td>
                                <td colspan="3" style="font-size: 12px">
                                    <asp:TextBox ID="txtspecification" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 12px;" class="style2">
                                    Brand
                                </td>
                                <td style="font-size: 12px;">
                                    :
                                </td>
                                <td colspan="3" style="font-size: 12px">
                                    <asp:TextBox ID="txtbrand" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 12px;" class="style2">
                                    Origin
                                </td>
                                <td style="font-size: 12px;">
                                    :
                                </td>
                                <td colspan="3" style="font-size: 12px">
                                    <asp:TextBox ID="txtorigin" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 12px;" class="style2">
                                    Packing
                                </td>
                                <td style="font-size: 12px;">
                                    :
                                </td>
                                <td colspan="3" style="font-size: 12px">
                                    <asp:TextBox ID="txtpacking" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 12px;" class="style2">
                                    Unit Price
                                    (Tk.)</td>
                                <td style="font-size: 12px;">
                                    :
                                </td>
                                <td style="font-size: 12px">
                                    <asp:TextBox ID="txtrate" runat="server" CssClass="txtbox" Width="137px"></asp:TextBox><span
                                        style="color: #ff0000">*</span>
                                    <span style="color: #ff0000"></span>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                        FilterType="Custom, Numbers" TargetControlID="txtrate" ValidChars=".">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td style="font-size: 12px" align="right" class="style1">
                                    <asp:Label ID="lbltk" runat="server"></asp:Label>
                                </td>
                                <td style="font-size: 12px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="font-size: 12px;" class="style2">
                                    <asp:Label ID="Label1" runat="server" Text="Recent Rate/Unit" Visible="False"></asp:Label>
                                </td>
                                <td style="font-size: 12px;">
                                    :
                                </td>
                                <td style="font-size: 12px" colspan="2">
                                    <%--<asp:DropDownList ID="DropDownList1" runat="server" Visible="False" Width="500px"
                                                AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            </asp:DropDownList>--%>
                                    <asp:DropDownList ID="DropDownList1" runat="server" Visible="False" Width="500px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" CssClass="btn2" runat="server" Text="View PO" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="tblbig" colspan="3" style="height: 1px">
        </td>
    </tr>
</table>
<%--    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>--%>
