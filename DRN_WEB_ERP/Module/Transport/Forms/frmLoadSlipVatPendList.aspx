<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmLoadSlipVatPendList.aspx.cs" Inherits="DRN_WEB_ERP.Module.Transport.Forms.frmLoadSlipVatPendList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" class="titleframe">
        VAT Challan Pending List</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #CCFFFF">
                <br />
                <br />
                <asp:Panel ID="pnlList" runat="server" GroupingText="Load Slip List" BackColor="#CCFFFF">
                    <br />
                    <asp:GridView ID="gvLsVatPend" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="8pt"
                        OnRowDataBound="gvLsVatPend_RowDataBound" AllowPaging="True" OnPageIndexChanging="gvLsVatPend_PageIndexChanging"
                        PageSize="50">
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LS Ref No">
                                <ItemTemplate>
                                    <asp:Label ID="lblLsRefNo" runat="server" Text='<%# Bind("LS_Ref_No") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Truck No">
                                <ItemTemplate>
                                    <asp:Label ID="lblLsTruckNo" runat="server" Text='<%# Bind("LS_Truck_No") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="110px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="LS_Date_Time" HeaderText="LS Date Time">
                                <ItemStyle Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LS_DO_Dealer" HeaderText="Dealer Name">
                                <ItemStyle Width="220px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LS_DO_Item" HeaderText="Item Name">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="LS_DO_Bag_Type" HeaderText="Bag Type" />--%>
                            <asp:BoundField DataField="LS_DO_Qty" HeaderText="D/O Qty">
                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LS_DO_Chln" HeaderText="Challan No">
                                <ItemStyle Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LS_WS_Empty_Wgt" HeaderText="Empty Weight" Visible="False">
                                <ItemStyle Width="70px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LS_WS_Load_Wgt" HeaderText="Load Weight" Visible="False">
                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LS_TLY_Item_Name" HeaderText="Tally Item Name" Visible="False">
                                <ItemStyle Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LS_TLY_Item_Qty" HeaderText="Tally Qty" Visible="False">
                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnReject" runat="server" OnClick="btnReject_Click" Text="Return" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnVatChlnOk" runat="server" Text="VAT Ok" OnClick="btnVatChlnOk_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" Wrap="True" />
                        <PagerSettings Position="TopAndBottom" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                    <br />
                </asp:Panel>
                <br />
                <br />
            </div>
        </ContentTemplate>
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
