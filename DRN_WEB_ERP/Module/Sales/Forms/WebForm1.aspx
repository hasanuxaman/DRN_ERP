<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" Width="100px">
            <Columns>
                <asp:BoundField DataField="DO_Hdr_Ref" HeaderText="D/O Ref No" />
                <asp:TemplateField HeaderText="Details">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDetail" Text="Show Detail" runat="server" OnClientClick="GetDetails(this)" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
        <script type="text/javascript">
            function GetDetails(lnk) {
                var row = $(lnk).closest('tr');
                var name = $('td', row).eq(0).html();
                $('#txtName', window.parent.document).val(name);
                alert(name.toString());
                alert($('#txtName', window.parent.document).val().toString());
                $('#pnlPopup', window.parent.document).hide();
                return false;
            }
        </script>
    </div>
    </form>
</body>
</html>
