<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmSupplierBillView.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmSupplierBillView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Supplier Bill</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="background-color: #9E9AF5">
                <table style="width: 100%; font-family: verdana; font-size: small;">
                    <tr>
                        <td style="width: 261px">
                            &nbsp;
                        </td>
                        <td style="width: 240px">
                            &nbsp;
                        </td>
                        <td style="width: 167px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 63px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 261px">
                            &nbsp;
                        </td>
                        <td style="width: 240px">
                            &nbsp;
                        </td>
                        <td style="width: 167px">
                            Supplier Name
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td align="left" colspan="6">
                            <asp:TextBox ID="txtSuppName" runat="server" BorderStyle="None" Enabled="False" Font-Bold="True"
                                Font-Size="10pt" ForeColor="Blue" Width="510px"></asp:TextBox>
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 261px">
                            &nbsp;
                        </td>
                        <td style="width: 240px">
                            &nbsp;
                        </td>
                        <td style="width: 167px">
                            Bill Ref No
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td style="width: 168px">
                            <asp:TextBox ID="txtBillRefNo" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" Font-Bold="True" Font-Size="10pt" ForeColor="Blue" Width="170px"></asp:TextBox>
                        </td>
                        <td style="width: 63px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                            Entry Date
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td style="width: 168px">
                            <asp:TextBox ID="txtBillEntDt" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" ForeColor="Blue" Width="170px"></asp:TextBox>
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 261px">
                            &nbsp;
                        </td>
                        <td style="width: 240px">
                            &nbsp;
                        </td>
                        <td style="width: 167px">
                            Supplier Bill No
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td style="width: 168px">
                            <asp:TextBox ID="txtSupBillNo" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Font-Bold="True" Font-Size="10pt" ForeColor="#FF33CC" Width="170px" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="width: 63px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                            Bill Date
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td style="width: 168px">
                            <asp:TextBox ID="txtBillDate" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" ForeColor="Blue" Width="170px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalenderDelDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgReqDt" TargetControlID="txtBillDate">
                            </cc1:CalendarExtender>
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 261px">
                            &nbsp;
                        </td>
                        <td style="width: 240px">
                            &nbsp;
                        </td>
                        <td style="width: 167px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 63px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div style="background-color: #9E9AF5" align="center">
                <asp:GridView ID="gvBillDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="10pt" ForeColor="#333333" ShowFooter="True" OnRowDataBound="gvBillDet_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Bill_Det_Mrr_No" HeaderText="MRR Ref No" />
                        <asp:BoundField DataField="Bill_Det_Po_No" HeaderText="PO Ref No" />
                        <asp:BoundField DataField="Bill_Det_Chln_No" HeaderText="Challan No" />
                        <asp:BoundField DataField="Bill_Det_Item_Code" HeaderText="Item Code" />
                        <asp:BoundField DataField="Bill_Det_Item_Name" HeaderText="Item Name" />
                        <asp:BoundField DataField="Bill_Det_Item_Uom" HeaderText="Unit" />
                        <asp:BoundField DataField="Bill_Det_Item_Qty" HeaderText="Quantity">
                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Rate">
                            <FooterTemplate>
                                Total :
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Bill_Det_Item_Rate") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <FooterTemplate>
                                <asp:Label ID="lblTotAmt" runat="server" Font-Size="9pt"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBillAmount" runat="server" Text='<%# Bind("Bill_Det_Item_Amt") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="9pt" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" Font-Size="8pt" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
                <asp:Label ID="lbltot" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
                <br />
                <table style="width: 100%; font-family: verdana; font-size: small;">
                    <tr>
                        <td style="width: 261px">
                            &nbsp;
                        </td>
                        <td style="width: 240px">
                            &nbsp;
                        </td>
                        <td style="width: 167px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 63px">
                            &nbsp;
                        </td>
                        <td style="width: 170px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 261px">
                            &nbsp;
                        </td>
                        <td style="width: 240px">
                            &nbsp;
                        </td>
                        <td style="width: 167px">
                            &nbsp;
                        </td>
                        <td width="2px">
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 63px">
                            &nbsp;
                        </td>
                        <td style="width: 170px">
                            Bill Amount
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td style="width: 168px">
                            <asp:TextBox ID="txtBillAmt" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" Width="170px"></asp:TextBox>
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 261px">
                            &nbsp;
                        </td>
                        <td style="width: 240px">
                            &nbsp;
                        </td>
                        <td style="width: 167px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 63px">
                            &nbsp;
                        </td>
                        <td style="width: 170px">
                            Adjustment
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td style="width: 168px">
                            <asp:TextBox ID="txtBillAdjAmt" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" Width="170px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtBillAdjAmt_CalendarExtender" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" PopupButtonID="imgReqDt" TargetControlID="txtBillAdjAmt">
                            </cc1:CalendarExtender>
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 261px">
                            &nbsp;
                        </td>
                        <td style="width: 240px">
                            &nbsp;
                        </td>
                        <td style="width: 167px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 63px">
                            &nbsp;
                        </td>
                        <td style="width: 170px">
                            Net Bill amount
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td style="width: 168px">
                            <asp:TextBox ID="txtBillNetAmt" runat="server" BackColor="#CCCCCC" BorderStyle="None"
                                CssClass="textAlignCenter" Enabled="False" Font-Bold="True" Font-Size="11pt"
                                Width="170px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtBillNetAmt_CalendarExtender" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" PopupButtonID="imgReqDt" TargetControlID="txtBillNetAmt">
                            </cc1:CalendarExtender>
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 261px">
                            &nbsp;
                        </td>
                        <td style="width: 240px">
                            &nbsp;
                        </td>
                        <td style="width: 167px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 63px">
                            &nbsp;
                        </td>
                        <td style="width: 170px">
                            Pay Amount
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td style="width: 168px">
                            <asp:TextBox ID="txtBillPayAmt" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" Width="170px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtBillPayAmt_CalendarExtender" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" PopupButtonID="imgReqDt" TargetControlID="txtBillPayAmt">
                            </cc1:CalendarExtender>
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 261px">
                            &nbsp;
                        </td>
                        <td style="width: 240px">
                            &nbsp;
                        </td>
                        <td style="width: 167px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 63px">
                            &nbsp;
                        </td>
                        <td style="width: 170px">
                            Due Amount
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td style="width: 168px">
                            <asp:TextBox ID="txtBillDueAmt" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" Width="170px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtBillDueAmt_CalendarExtender" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" PopupButtonID="imgReqDt" TargetControlID="txtBillDueAmt">
                            </cc1:CalendarExtender>
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 261px">
                            &nbsp;
                        </td>
                        <td style="width: 240px">
                            &nbsp;
                        </td>
                        <td style="width: 167px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 63px">
                            &nbsp;
                        </td>
                        <td style="width: 170px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td style="width: 225px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
