<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmIOSetup.aspx.cs" Inherits="DRN_WEB_ERP.Module.IO.Forms.frmIOSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        I/O Approval Flow Setup</div>
    <div align="center" style="border: 1px solid #CCCCCC; height: 320px; background-color: #CCFFFF;">
        <table style="background-color: #CCFFFF;">
            <tr>
                <td style="width: 10px">
                    &nbsp;
                </td>
                <td style="width: 136px" align="right">
                    &nbsp;
                </td>
                <td style="width: 3px">
                    &nbsp;
                </td>
                <td style="width: 300px" align="left">
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                    &nbsp;
                </td>
                <td align="right" style="width: 136px">
                    <asp:DropDownList ID="cboStepNo1" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="width: 3px">
                    :
                </td>
                <td align="left" style="width: 300px">
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                    &nbsp;
                </td>
                <td style="width: 136px" align="right">
                    <asp:DropDownList ID="cboStepNo2" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="width: 3px">
                    :
                </td>
                <td style="width: 300px" align="left">
                    <asp:DropDownList ID="DropDownList2" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 136px" align="right">
                    <asp:DropDownList ID="cboStepNo3" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="width: 3px">
                    :
                </td>
                <td style="width: 300px" align="left">
                    <asp:DropDownList ID="DropDownList3" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                    &nbsp;
                </td>
                <td style="width: 136px" align="right">
                    <asp:DropDownList ID="cboStepNo4" runat="server">
                    </asp:DropDownList>
                    &nbsp;</td>
                <td style="width: 3px">
                    :
                </td>
                <td style="width: 300px" align="left">
                    <asp:DropDownList ID="DropDownList4" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                    &nbsp;
                </td>
                <td align="right" style="width: 136px">
                    <asp:DropDownList ID="cboStepNo5" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="width: 3px">
                    :
                </td>
                <td align="left" style="width: 300px">
                    <asp:DropDownList ID="DropDownList5" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                    &nbsp;
                </td>
                <td style="width: 136px" align="right">
                    <asp:DropDownList ID="cboStepNo6" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="width: 3px">
                    &nbsp;:
                </td>
                <td style="width: 300px" valign="top" align="left">
                    <asp:DropDownList ID="DropDownList6" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                    &nbsp;
                </td>
                <td style="width: 136px" align="right">
                    <asp:DropDownList ID="cboStepNo7" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="width: 3px">
                    :
                </td>
                <td style="width: 300px" valign="top" align="left">
                    <asp:DropDownList ID="DropDownList7" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                    &nbsp;
                </td>
                <td style="width: 136px" align="right">
                    <asp:DropDownList ID="cboStepNo8" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="width: 3px">
                    :
                </td>
                <td style="width: 300px" valign="top" align="left">
                    <asp:DropDownList ID="DropDownList8" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                    &nbsp;
                </td>
                <td style="width: 136px" align="right">
                    <asp:DropDownList ID="cboStepNo9" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="width: 3px">
                    :
                </td>
                <td style="width: 300px" valign="top" align="left">
                    <asp:DropDownList ID="DropDownList9" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                    &nbsp;
                </td>
                <td style="width: 136px" align="right">
                    <asp:DropDownList ID="cboStepNo10" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="width: 3px">
                    :
                </td>
                <td style="width: 300px" valign="top" align="left">
                    <asp:DropDownList ID="DropDownList10" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                    &nbsp;
                </td>
                <td colspan="3">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 136px">
                    &nbsp;
                </td>
                <td style="width: 3px">
                    &nbsp;
                </td>
                <td style="width: 300px">
                    <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="Clear" Width="100px"
                        OnClick="btnClear_Click" />
                    <asp:Button ID="btnSave" runat="server" CssClass="btn" OnClick="btnSave_Click" Text="Save"
                        ValidationGroup="Save" Width="100px" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="10pt" ForeColor="Red"
                ValidationGroup="Save" />
        </div>
    </div>
</asp:Content>
