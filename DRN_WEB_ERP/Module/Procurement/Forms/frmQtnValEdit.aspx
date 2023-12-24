<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" ValidateRequest="false" CodeBehind="frmQtnValEdit.aspx.cs"
    Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmQtnValEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="tblbig" colspan="3" style="text-align: center">
                QUOTATION UPDATE
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 19px; text-align: left">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 46px">
                            &nbsp;
                        </td>
                        <td style="width: 107px">
                            MPR REF. NO</td>
                        <td style="width: 16px">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtid" runat="server" CssClass="btn2" ReadOnly="True" Width="166px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 46px">
                            &nbsp;
                        </td>
                        <td style="width: 107px">
                            ITEM DET
                        </td>
                        <td style="width: 16px">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtitem" runat="server" CssClass="btn2" ReadOnly="True" Width="389px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 46px">
                            &nbsp;
                        </td>
                        <td style="width: 107px">
                            PARTY DETAILS
                        </td>
                        <td style="width: 16px">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtparty" runat="server" CssClass="btn2" ReadOnly="True" Width="389px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 46px">
                            &nbsp;
                        </td>
                        <td style="width: 107px">
                            QUANTITY
                        </td>
                        <td style="width: 16px">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtqty" runat="server" CssClass="btn2" ReadOnly="True" Width="166px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 46px; height: 34px">
                        </td>
                        <td style="width: 107px; height: 34px">
                        </td>
                        <td style="width: 16px; height: 34px">
                        </td>
                        <td style="height: 34px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 37px">
                            &nbsp;
                        </td>
                        <td style="width: 123px">
                            SPECIFICATION
                        </td>
                        <td style="width: 6px">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtspecification" runat="server" Width="377px" CssClass="txtbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 37px">
                            &nbsp;
                        </td>
                        <td style="width: 123px">
                            BRAND
                        </td>
                        <td style="width: 6px">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtbrand" runat="server" Width="377px" CssClass="txtbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 37px">
                            &nbsp;
                        </td>
                        <td style="width: 123px">
                            ORIGIN
                        </td>
                        <td style="width: 6px">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtorigin" runat="server" Width="377px" CssClass="txtbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 37px">
                            &nbsp;
                        </td>
                        <td style="width: 123px">
                            PACKING
                        </td>
                        <td style="width: 6px">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtpacking" runat="server" Width="377px" CssClass="txtbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 37px">
                            &nbsp;
                        </td>
                        <td style="width: 123px">
                            UNIT PRICE
                        </td>
                        <td style="width: 6px">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtrate" runat="server" CssClass="txtbox" Width="137px"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                FilterType="Custom, Numbers" TargetControlID="txtrate" ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <asp:Label ID="lbltk" runat="server" Text="tk" Width="140px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 28px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 16px; text-align: center">
                &nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnupdate" runat="server" OnClick="btnupdate_Click" Text="Update"
                    CssClass="btn2" Width="109px" />
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 25px;" align="center">
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="Data Processing Error."
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: left">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: center">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
