<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmSalesDataBrowser.aspx.cs"
    Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesDataBrowser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function getVal() {
            var sel = document.getElementById("Select1");
            var index = sel.selectedIndex;
            var value = sel[index].value;
            return value;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="First Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Font-Size="14px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="Middle Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" Font-Size="14px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="Last Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" Font-Size="14px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" CssClass="lbl" Text="Gender"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server" Font-Size="14px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="Age"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox5" runat="server" Font-Size="14px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" CssClass="lbl" Text="City"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox6" runat="server" Font-Size="14px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" CssClass="lbl" Text="State"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox7" runat="server" Font-Size="14px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <select id="Select1" name="Select1">
                    <option value="1">one</option>
                    <option value="2">two</option>
                    <option value="3">three</option>
                </select>
                <input type="button" onclick="parent.setValue(getVal());" value="Go To Parent" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
