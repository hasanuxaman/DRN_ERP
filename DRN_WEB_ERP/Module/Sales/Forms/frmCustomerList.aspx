<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmCustomerList.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmCustomerList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Customer List</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #86AEAE">
                <br />
                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                <asp:GridView ID="gvCust" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#336666" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" EmptyDataText="No Data  Found."
                    Font-Size="8pt" AllowPaging="True" PageSize="50" AllowSorting="True" OnPageIndexChanging="gvCust_PageIndexChanging"
                    OnSorting="gvCust_Sorting">
                    <Columns>
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
                        <asp:BoundField DataField="Par_Adr_Cr_Limit" HeaderText="Cr Limit" SortExpression="Par_Adr_Cr_Limit"
                            DataFormatString="{0:N2}">
                            <ItemStyle Width="80px" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Par_Adr_Cr_Days" HeaderText="Cr Days" SortExpression="Par_Adr_Cr_Days">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Par_Adr_Status" HeaderText="Status" />
                        <asp:BoundField DataField="Par_Adr_Ent_date" HeaderText="Entry Date" DataFormatString="{0:dd/MM/yyyy}" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" Font-Size="8pt"
                        HorizontalAlign="Left" Wrap="False" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px"
                        HorizontalAlign="Left" Wrap="true" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
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
</asp:Content>
