<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmSalesDeliveryChallanTruckEdit.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesDeliveryChallanTruckEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Delivery Challan Truck No Edit</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnSearch">
                <div align="center" style="background-color: #9999FF">
                    <span>
                        <br />
                    </span>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td align="left" style="width: 40px">
                                Year
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" width="100">
                                <asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
                                    Width="100px">
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 50px">
                                Month
                            </td>
                            <td align="left" width="2">
                                :
                            </td>
                            <td align="left" width="100">
                                <asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"
                                    Width="100px">
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left" width="90">
                                Challan No
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" style="width: 400px">
                                <asp:DropDownList ID="ddlChallanList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChallanList_SelectedIndexChanged"
                                    Width="400px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 33px">
                                <span>
                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" onkeypress="return clickButton(event,'btnSearch')"
                                        Text="Search" ValidationGroup="btnSearch" Width="60px" />
                                </span>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #9999FF">
                <br />
                <hr />
                <br />
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td align="left" style="font-size: 12px; width: 10px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 69px;">
                            Challan No
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style1" style="width: 182px">
                            <asp:TextBox ID="txtChallanNo" runat="server" Enabled="False" Font-Bold="True" ForeColor="#FF9900"
                                Width="180px" CssClass="textAlignCenter"></asp:TextBox>
                        </td>
                        <td align="left" class="style2" style="font-size: 12px; width: 72px;">
                            Date &amp; Time
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style3" style="width: 136px">
                            <asp:TextBox ID="txtChallanDate" runat="server" AutoPostBack="True" Enabled="False"
                                CssClass="textAlignCenter"></asp:TextBox>
                        </td>
                        <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                            Trans. Mode
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style5" style="font-size: 14px; width: 135px;">
                            <asp:DropDownList ID="ddlOrdTransMode" runat="server" Width="160px" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="style6" style="font-size: 12px; width: 75px;">
                            Del. Address
                        </td>
                        <td align="left" style="font-size: 12px; width: 8px;">
                            :
                        </td>
                        <td align="left" width="8" rowspan="4" valign="top">
                            <asp:TextBox ID="txtDelAddr" runat="server" ForeColor="#0066FF" Width="180px" TextMode="MultiLine"
                                Height="60px" MaxLength="100" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 12px; width: 10px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 69px;">
                            Trans. By
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style1" style="width: 182px">
                            <asp:RadioButtonList ID="optTranBy" runat="server" Font-Size="8pt" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="optTranBy_SelectedIndexChanged">
                                <asp:ListItem Value="1" Selected="True">Customer</asp:ListItem>
                                <asp:ListItem Value="2">Company</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left" class="style2" style="font-size: 12px; width: 72px;">
                            District
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style3" style="width: 136px">
                            <asp:DropDownList ID="cboCustDist" runat="server" ValidationGroup="ChkData" Width="172px"
                                AutoPostBack="True" OnSelectedIndexChanged="cboCustDist_SelectedIndexChanged"
                                Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                            Thana
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style5" style="font-size: 14px; width: 135px;">
                            <asp:DropDownList ID="cboCustThana" runat="server" Width="160px" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="style6" style="font-size: 12px; width: 75px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 8px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 12px; width: 10px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 69px;">
                            Vehicle No
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style1" style="width: 182px">
                            <asp:TextBox ID="txtTruckNo" runat="server" Width="180px" CssClass="uppercase"></asp:TextBox>
                            <asp:DropDownList ID="cboTruckNo" runat="server" Width="182px" Visible="False">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="style2" style="font-size: 12px; width: 72px;">
                            Driver Name
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style3" style="width: 136px">
                            <asp:TextBox ID="txtDriverName" runat="server" CssClass="capitalize"></asp:TextBox>
                        </td>
                        <td align="left" class="style4" style="font-size: 12px; width: 74px;">
                            Contact No
                        </td>
                        <td align="left" width="8">
                            :
                        </td>
                        <td align="left" class="style5" style="font-size: 14px; width: 135px;">
                            <asp:TextBox ID="txtDriverContact" runat="server" Width="156px"></asp:TextBox>
                        </td>
                        <td align="left" class="style6" style="font-size: 12px; width: 75px;">
                            &nbsp;
                        </td>
                        <td align="left" style="font-size: 12px; width: 8px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-size: 12px; width: 10px;">
                            &nbsp;</td>
                        <td align="left" style="font-size: 12px; width: 69px;">
                            &nbsp;</td>
                        <td align="left" width="8">
                            &nbsp;</td>
                        <td align="center" class="style1" colspan="7">
                            <br />
                            <asp:Button ID="btnUpdateVslNo" runat="server" onclick="btnUpdateVslNo_Click" 
                                Text="Update Vehicle Info" />
                        </td>
                        <td align="left" class="style6" style="font-size: 12px; width: 75px;">
                            &nbsp;</td>
                        <td align="left" style="font-size: 12px; width: 8px;">
                            &nbsp;</td>
                    </tr>
                </table>
                <br />
                <hr />
                <table style="width: 100%; font-size: 12px;">
                    <tr>
                        <td style="width: 37px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 59px">
                            Retailer 1
                        </td>
                        <td align="left">
                            :
                        </td>
                        <td align="left" style="width: 714px">
                            <asp:TextBox ID="txtRetailer1" runat="server" CssClass="inline search" Width="686px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtendertxtRetailer1" runat="server" BehaviorID="AutoCompleteSrchRTL1"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetSrchRetailer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtRetailer1">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 40px">
                            Quantity
                        </td>
                        <td align="left">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDelQty1" runat="server" CssClass="textAlignCenter" Width="100px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtDelQty1_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDelQty1"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 37px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 59px">
                            Retailer 2
                        </td>
                        <td align="left">
                            :
                        </td>
                        <td align="left" style="width: 714px">
                            <asp:TextBox ID="txtRetailer2" runat="server" CssClass="inline search" Width="686px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtendertxtRetailer2" runat="server" BehaviorID="AutoCompleteSrchRTL2"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetSrchRetailer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtRetailer2">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 40px">
                            Quantity
                        </td>
                        <td align="left">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDelQty2" runat="server" CssClass="textAlignCenter" Width="100px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtDelQty2_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDelQty2"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 37px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 59px">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 714px">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 40px">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left">
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
                <br />
                <asp:GridView ID="gvChlnDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Trn_Det_Ord_Ref_No" HeaderText="D/O Ref. No" />
                        <asp:BoundField DataField="Trn_Det_Itm_Desc" HeaderText="Item Desc">
                            <ItemStyle Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Trn_Det_Itm_Uom" HeaderText="Unit">
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Trn_Det_Code" HeaderText="Type" />
                        <asp:BoundField DataField="Trn_Det_Lin_Qty" HeaderText="Qty">
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Height="30px"
                        Wrap="False" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="optTranBy" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboCustDist" EventName="SelectedIndexChanged" />
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
