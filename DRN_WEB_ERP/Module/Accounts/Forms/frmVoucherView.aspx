<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmVoucherView.aspx.cs" Inherits="DRN_WEB_ERP.Module.Accounts.Forms.frmVoucherView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Voucher Details</div>
    <asp:Panel ID="pnlVoucherDet" runat="server">
        <div align="center" style="border: 1px solid #CCCCCC; background-color: #009999">
            <table style="width: 100%;">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label10" runat="server" Text="Voucher No:" Font-Bold="True"></asp:Label>
                        <asp:Label ID="lblJvNo" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblJrnType" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label12" runat="server" Text="Date:" Font-Bold="True"></asp:Label>
                        <asp:Label ID="lblJvDate" runat="server" Font-Bold="True"></asp:Label>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvVoucherDet" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="8pt"
                PageSize="25" OnRowDataBound="gvVoucherDet_RowDataBound" ShowFooter="True">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <%--<asp:TemplateField HeaderText="SL#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="TRN_REF_NO" HeaderText="Trn. Ref. No." Visible="False">
                        <ItemStyle Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TRN_LNO" HeaderText="L-No">
                        <ItemStyle Width="30px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="GL Code">
                        <ItemTemplate>
                            <asp:Label ID="lblCoaCode" runat="server" Text='<%# Bind("COA_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="COA_NAME" HeaderText="GL Name">
                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Date" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblTrnDate" runat="server" Text='<%# Bind("TRAN_DATE", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Narration">
                        <ItemTemplate>
                            <asp:Label ID="lblNarration" runat="server" Text='<%# Bind("TRAN_NARRATION") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="Label9" runat="server" Text="Total :" Font-Bold="True"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Left" Width="450px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Debit">
                        <ItemTemplate>
                            <asp:Label ID="lblDr" runat="server" Text='<%# Bind("TRAN_DEBIT", "{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotDr" runat="server" Font-Bold="True"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit">
                        <ItemTemplate>
                            <asp:Label ID="lblCr" runat="server" Text='<%# Bind("TRAN_CREDIT", "{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotCr" runat="server" Font-Bold="True"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                <SortedDescendingHeaderStyle BackColor="#3E3277" />
            </asp:GridView>
            <br />
            <asp:Button ID="btnPrintJv" runat="server" CssClass="btn" Height="60px" Text="Print"
                Width="100px" OnClick="btnPrintJv_Click" />
            <br />
            <br />
            <div align="left" style="background-color: #FFCCFF">
                <asp:Panel ID="panel1" runat="server" GroupingText="Edit Log">
                    <asp:PlaceHolder ID="phcomments" runat="server"></asp:PlaceHolder>
                </asp:Panel>
            </div>
            <br />
        </div>
    </asp:Panel>
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
        CancelControlID="btnMsgOk" DropShadow="true" OkControlID="btnMsgOk" PopupControlID="pnlMsg"
        TargetControlID="hfHidden">
    </cc1:ModalPopupExtender>
</asp:Content>
