<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmTranTripBillShow.aspx.cs" Inherits="DRN_WEB_ERP.Module.Accounts.Forms.frmTranTripBillShow" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Transport Voucher Entry</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlSearchBox" runat="server">
                <div align="center" style="background-color: #86AEAE">
                    <br />
                    <asp:Panel ID="pnlDet" runat="server">
                        <div align="center" style="background-color: #9999FF">
                            <br />
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; font-size: 12px;">
                                <tr valign="top">
                                    <td align="right" style="font-size: 12px; width: 143px;">
                                        &nbsp;
                                    </td>
                                    <td align="right" style="font-size: 12px; width: 143px;">
                                        Ref. No
                                    </td>
                                    <td align="left" width="8">
                                        :
                                    </td>
                                    <td align="left" class="style1" style="width: 140px">
                                        <asp:TextBox ID="txtTranRefNo" runat="server" CssClass="textAlignCenter" Enabled="False"
                                            Font-Bold="True" ForeColor="#FF9900" Width="180px"></asp:TextBox>
                                    </td>
                                    <td align="right" class="style2" style="font-size: 12px; width: 57px;">
                                        Date
                                    </td>
                                    <td align="left" width="8">
                                        :
                                    </td>
                                    <td align="left" class="style3" style="width: 102px">
                                        <asp:TextBox ID="txtTranDate" runat="server" CssClass="textAlignCenter" ForeColor="Blue"
                                            Width="120px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td align="right" class="style4" style="font-size: 12px; width: 89px;">
                                        Truck No
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                                        :
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 171px;">
                                        <asp:DropDownList ID="cboTruckNo" runat="server" Width="130px" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" class="style5" style="width: 208px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style5" style="font-size: 14px; width: 20px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style5" style="font-size: 14px; width: 8px;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td align="right" style="font-size: 12px; width: 143px;">
                                        &nbsp;
                                    </td>
                                    <td align="right" style="font-size: 12px; width: 143px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" width="8">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style1" style="width: 140px">
                                        &nbsp;
                                    </td>
                                    <td align="right" class="style2" style="font-size: 12px; width: 57px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" width="8">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style3" style="width: 102px">
                                    </td>
                                    <td align="right" class="style4" style="font-size: 12px; width: 89px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 171px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style5" style="width: 208px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style5" style="font-size: 14px; width: 20px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style5" style="font-size: 14px; width: 8px;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td align="right" style="font-size: 12px; width: 143px;">
                                        &nbsp;
                                    </td>
                                    <td align="right" style="font-size: 12px; width: 143px;">
                                        Party Name
                                    </td>
                                    <td align="left" width="8">
                                        :
                                    </td>
                                    <td align="left" class="style1" colspan="4">
                                        <asp:TextBox ID="txtGlParty" runat="server" CssClass="search" Font-Bold="False" Width="364px"
                                            Enabled="False"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" BehaviorID="AutoCompleteSrchAccParty"
                                            CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                            ServiceMethod="GetSrchCustomer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtGlParty">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td align="right" class="style4" style="font-size: 12px; width: 89px;">
                                        Km
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                                        :
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 171px;">
                                        <asp:TextBox ID="txtKm" runat="server" CssClass="textAlignCenter" Font-Bold="True"
                                            ForeColor="#FF9900" Width="130px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td align="left" class="style5" style="width: 208px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style5" style="font-size: 14px; width: 20px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style5" style="font-size: 14px; width: 8px;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td align="right" style="font-size: 12px; width: 143px;">
                                        &nbsp;
                                    </td>
                                    <td align="right" style="font-size: 12px; width: 143px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" width="8">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style1" colspan="4">
                                    </td>
                                    <td align="right" class="style4" style="font-size: 12px; width: 89px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 8px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style4" style="font-size: 12px; width: 171px;">
                                    </td>
                                    <td align="left" class="style5" style="width: 208px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style5" style="font-size: 14px; width: 8px;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <div style="background-color: #808080">
                                <asp:Button ID="btnRej" runat="server" CssClass="btn" Height="40px" OnClick="btnRej_Click"
                                    Text="Reject" Width="100px" />
                                <asp:Button ID="BtnAppr" runat="server" CssClass="btn" Height="40px" OnClick="btnClearAll_Click"
                                    Text="Approve" Width="100px" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlVoucherDet" runat="server">
        <div align="center" style="border: 1px solid #CCCCCC; background-color: #009999">
            <br />
            <div>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" bgcolor="#3366FF">
                    <tr>
                        <td align="left" style="width: 33%;">
                            <asp:Label ID="Label4" runat="server" BorderStyle="None" ForeColor="White" Text="Debit Amount: "></asp:Label>
                            <asp:Label ID="lblTotDrAmt" runat="server" BorderStyle="None" ForeColor="White" Text="0.00"></asp:Label>
                        </td>
                        <td align="center" style="width: 33px">
                            <asp:Label ID="Label6" runat="server" BorderStyle="None" ForeColor="White" Text="Credit Amount: "></asp:Label>
                            <asp:Label ID="lblTotCrAmt" runat="server" BorderStyle="None" ForeColor="White" Text="0.00"></asp:Label>
                        </td>
                        <td align="right" style="width: 33px">
                            <asp:Label ID="Label8" runat="server" BorderStyle="None" ForeColor="White" Text="Balance: "></asp:Label>
                            <asp:Label ID="lblDrCrBal" runat="server" BorderStyle="None" ForeColor="White" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:GridView ID="gvVoucherDet" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="9pt"
                PageSize="25">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:TemplateField HeaderText="SL#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GL Code">
                        <ItemTemplate>
                            <asp:Label ID="lblCoaCode" runat="server" Text='<%# Bind("COA_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="COA_NAME" HeaderText="GL Name">
                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label ID="lblTrnDate" runat="server" Text='<%# Bind("TRAN_DATE", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Narration">
                        <ItemTemplate>
                            <asp:Label ID="lblNarration" runat="server" Text='<%# Bind("TRAN_NARRATION") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="450px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Debit">
                        <ItemTemplate>
                            <asp:Label ID="lblDr" runat="server" Text='<%# Bind("TRAN_DEBIT", "{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit">
                        <ItemTemplate>
                            <asp:Label ID="lblCr" runat="server" Text='<%# Bind("TRAN_CREDIT", "{0:n}") %>'></asp:Label>
                            <asp:HiddenField ID="hfHdrFlag" runat="server" Value='<%# Bind("HDR_FLAG") %>' />
                        </ItemTemplate>
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
