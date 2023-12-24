<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCustomerListByMpo.aspx.cs"
    Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmCustomerListByMpo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer List By MPO</title>
    <style type="text/css">
        .search
        {
            background: #FFFFFF url('../../../Image/find.png') no-repeat;
            padding-left: 18px;
            border: 1px solid #ccc;
            text-align: center;
        }
        .textAlignCenter
        {
            text-align: center;
            margin-top: 0px;
        }
        .style1
        {
            width: 104px;
        }
        .style2
        {
            width: 83px;
        }
        .style3
        {
            width: 14px;
        }
        .style4
        {
            width: 123px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div align="center" style="border: 1px dotted #000000; width: 100%; font-size: medium;
        font-weight: bold; background-color: #CC99FF;">
        Customer List By MPO</div>
    <div align="center" style="background-color: #33CCCC">
        <table style="width: 100%;">
            <tr>
                <td class="style4">
                    &nbsp;
                </td>
                <td align="right" class="style1">
                    &nbsp;
                </td>
                <td align="left" width="200">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                </td>
                <td align="right" width="120">
                    &nbsp;
                </td>
                <td align="left" width="320">
                    &nbsp;
                </td>
                <td width="210">
                    &nbsp;
                </td>
                <td class="style2">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;
                </td>
                <td align="right" class="style1">
                    Filter By MPO:
                </td>
                <td align="left" width="200">
                    <asp:DropDownList ID="ddlMpoList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMpoList_SelectedIndexChanged"
                        Width="200px">
                    </asp:DropDownList>
                </td>
                <td class="style3">
                    &nbsp;
                </td>
                <td align="right" width="120">
                    Search Customer:
                </td>
                <td align="left" width="320">
                    <asp:TextBox ID="txtSearch" runat="server" Width="320px" CssClass="search textAlignCenter"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                        CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" DelimiterCharacters=","
                        MinimumPrefixLength="1" ServiceMethod="GetSrchCustomerBySalesTree" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                        ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearch">
                    </cc1:AutoCompleteExtender>
                </td>
                <td width="210">
                    <asp:Button ID="btnShowSoRpt" runat="server" Text="Show" Width="100px" ValidationGroup="btnSearch"
                        OnClick="btnShowSoRpt_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="100px" OnClick="btnClear_Click" />
                </td>
                <td class="style2">
                    <asp:Button ID="btnExport" runat="server" Enabled="False" OnClick="btnExport_Click"
                        Text="Export" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;
                </td>
                <td align="right" class="style1">
                    &nbsp;
                </td>
                <td align="left" width="200">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                </td>
                <td align="right" width="120">
                    &nbsp;
                </td>
                <td align="left" width="320">
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtSearch"
                        ErrorMessage="Enter Valid Customer" ForeColor="Red" OnServerValidate="CustomValidator2_ServerValidate"
                        ValidationGroup="btnSearch"></asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearch"
                        ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="btnSearch">Enter Customer Name</asp:RequiredFieldValidator>
                </td>
                <td width="210">
                    &nbsp;
                </td>
                <td class="style2">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvCust" runat="server" AutoGenerateColumns="False" CellPadding="4"
            EmptyDataText="No Data  Found." Font-Size="8pt" AllowPaging="True" PageSize="50"
            AllowSorting="True" OnPageIndexChanging="gvCust_PageIndexChanging" OnSorting="gvCust_Sorting"
            ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="SL#">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cust. Ref.">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfCustRef" runat="server" Value='<%# Bind("Par_Adr_Ref") %>' />
                        <asp:Label ID="lblCustRefNo" runat="server" Text='<%# Bind("Par_Adr_Ref_No") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="80px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Par_Adr_Name" HeaderText="Name" SortExpression="Par_Adr_Name">
                    <ItemStyle Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="Par_Adr_Addr" HeaderText="Address">
                    <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="Par_Adr_Cont_Per" HeaderText="Contact Person">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Par_Adr_Cell_No" HeaderText="Cell No">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Par_Adr_Tel_No" HeaderText="Phone No">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Par_Adr_Fax_No" HeaderText="Fax No">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Par_Adr_Email_Id" HeaderText="Email">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Sales Zone" SortExpression="Par_Adr_Sale_Zone">
                    <ItemTemplate>
                        <asp:Label ID="lblSalesZone" runat="server" Text='<%# GetSalesZone(Eval("Par_Adr_Sale_Zone").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Sp_Ref" HeaderText="MPO Ref" SortExpression="Sp_Ref">
                    <ItemStyle HorizontalAlign="Right" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="Sp_Full_Name" HeaderText="MPO Name" SortExpression="Sp_Full_Name">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Supervisor">
                    <ItemTemplate>
                        <asp:Label ID="lblSupervisor" runat="server" Text='<%# GetSupervisor(Eval("Sp_Supr_Ref").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Par_Adr_Status" HeaderText="Status" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="8pt"
                HorizontalAlign="Left" Wrap="False" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                Wrap="true" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
            <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>
            <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>
            <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>
            <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
        </asp:GridView>
        <br />
        <br />
    </div>
    <asp:Panel ID="pnlMsg" runat="server" Style="border-right: black 2px solid; padding-right: 20px;
        display: none; border-top: black 2px solid; padding-left: 20px; padding-bottom: 20px;
        border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
        background-color: #E1FBF1" ForeColor="Blue" Width="300px" Font-Size="Small">
        <table id="tblMsg" runat="server" style="width: 100%;">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <div align="center">
            <asp:Button ID="btnMsgOk" runat="server" Text="OK" Width="80px" />
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hfHidden" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupExtenderMsg" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnMsgOk" OkControlID="btnMsgOk" PopupControlID="pnlMsg" TargetControlID="hfHidden"
        DropShadow="true">
    </cc1:ModalPopupExtender>
    </form>
</body>
</html>
