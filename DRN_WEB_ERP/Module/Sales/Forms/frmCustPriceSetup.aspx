<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmCustPriceSetup.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmCustPriceSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Item Price Setup</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnSearch">
                <div align="center" style="background-color: #86AEAE">
                    <span>
                        <br />
                        Search Customer:</span> <span>
                            <asp:TextBox ID="txtSearch" runat="server" Width="550px" CssClass="search textAlignCenter"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                                CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCustomer"
                                ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                TargetControlID="txtSearch">
                            </cc1:AutoCompleteExtender>
                            <asp:Button ID="btnSearch" runat="server" onkeypress="return clickButton(event,'btnSearch')"
                                Text="Search" ValidationGroup="btnSearch" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnClearSrch" runat="server" Text="Clear" Visible="False" Width="60px"
                                OnClick="btnClearSrch_Click" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSearch"
                                ErrorMessage="Enter Search Text" Font-Bold="False" ForeColor="Red" ValidationGroup="btnSearch"
                                Font-Size="10pt"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                        </span>
                    <asp:Button ID="btnExport" runat="server" Text="Export All To Excel" OnClick="btnExport_Click" />
                    <br />
                    <br />
                </div>
                <div align="center" style="background-color: #86AEAE">
                    <asp:GridView ID="gvItemDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Size="10pt" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblIcode" runat="server" Text='<%# Bind("Itm_Det_Ref") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Itm_Det_Desc" HeaderText="Item Name" />
                            <asp:BoundField DataField="Itm_Det_Stk_Unit" HeaderText="Unit" />
                            <asp:BoundField DataField="Itm_Det_Ext_Data1" DataFormatString="{0:n}" HeaderText="Item MRP">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="D/O Price">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNewPrice" runat="server" Width="80px" CssClass="textAlignCenter"
                                        Text='<%# GetPrice(int.Parse(Eval("Itm_Det_Ref").ToString()))%>'></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtNewPrice"
                                        ErrorMessage="Enter Valid Price" ForeColor="Red" Operator="DataTypeCheck" Type="Currency"
                                        ValidationGroup="Save">*</asp:CompareValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valid Till">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPriceValidDate" runat="server" Width="100px"></asp:TextBox>
                                    <asp:Image ID="imgPriceValidDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <cc1:CalendarExtender ID="CalenderPriceValidDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgPriceValidDt" TargetControlID="txtPriceValidDate">
                                    </cc1:CalendarExtender>
                                    <cc1:TextBoxWatermarkExtender ID="txtPriceValidDate_TextBoxWatermarkExtender" runat="server"
                                        Enabled="True" TargetControlID="txtPriceValidDate" WatermarkCssClass="WaterMarkFont"
                                        WatermarkText="Price Valid Date">
                                    </cc1:TextBoxWatermarkExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bonus %">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBnsPerc" runat="server" Width="80px" CssClass="textAlignCenter"
                                        Text='<%# GetBns(int.Parse(Eval("Itm_Det_Ref").ToString()))%>'></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtBnsPerc"
                                        ErrorMessage="Enter Valid Bonus Percentage" ForeColor="Red" Operator="DataTypeCheck"
                                        Type="Currency" ValidationGroup="Save">*</asp:CompareValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valid Till">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBonusValidDate" runat="server" Width="105px"></asp:TextBox>
                                    <asp:Image ID="imgBonusValidDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <cc1:CalendarExtender ID="CalenderBonusValidDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgBonusValidDt" TargetControlID="txtBonusValidDate">
                                    </cc1:CalendarExtender>
                                    <cc1:TextBoxWatermarkExtender ID="txtBonusValidDate_TextBoxWatermarkExtender" runat="server"
                                        Enabled="True" TargetControlID="txtBonusValidDate" WatermarkCssClass="WaterMarkFont"
                                        WatermarkText="Bonus Valid Date">
                                    </cc1:TextBoxWatermarkExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="60px" ValidationGroup="Save" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <br />
                    <span>
                        <asp:Button ID="btnSaveAll" runat="server" OnClick="btnSaveAll_Click" onkeypress="return clickButton(event,'btnSearch')"
                            Text="Save All" Width="100px" Visible="False" />
                        <br />
                        <br />
                    </span>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClearSrch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveAll" EventName="Click" />
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
