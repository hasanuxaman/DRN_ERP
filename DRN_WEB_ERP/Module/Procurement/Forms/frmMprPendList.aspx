<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmMprPendList.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmMprPendList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="UserControl/CtlQtnView.ascx" TagName="CtlQtnView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Pnding MPR List</div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tblmas" style="width: 100%" id="tblmaster" runat="server">
                <tr>
                    <td class="tbl" colspan="3" style="height: 8px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 3px">
                                    &nbsp;
                                </td>
                                <td align="left" style="width: 571px">
                                    MPR Ref. No:<asp:TextBox ID="txtMpr" runat="server" AutoPostBack="false" BorderStyle="Groove"
                                        CssClass="search textAlignCenter" OnTextChanged="txtMpr_TextChanged" Width="320px"></asp:TextBox>
                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                                        CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                        CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                        ServiceMethod="GetSrchMpr" ServicePath="~/Module/Procurement/Forms/wsAutoComProc.asmx"
                                        ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtMpr">
                                    </ajaxToolkit:AutoCompleteExtender>
                                </td>
                                <td align="right">
                                    <asp:RadioButtonList ID="optPendMprList" runat="server" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="optPendMprList_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="1" Selected="True">Show Filtered</asp:ListItem>
                                        <asp:ListItem Value="2">Show All</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                        Item Type:<asp:DropDownList ID="cboItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboItemType_SelectedIndexChanged"
                            Width="180px">
                        </asp:DropDownList>
                        &nbsp;Item Name:<asp:TextBox ID="txtItemName" runat="server" BorderStyle="Groove"
                            CssClass="search textAlignCenter" OnTextChanged="txtItemName_TextChanged" Width="320px"
                            AutoPostBack="false"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderSrchItem" runat="server"
                            BehaviorID="AutoCompleteSrchItem" CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                            ServiceMethod="GetSrchFilteredItem" ServicePath="~/Module/Inventory/Forms/wsAutoCompInv.asmx"
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtItemName">
                        </ajaxToolkit:AutoCompleteExtender>
                        Year:<asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
                            Width="90px">
                        </asp:DropDownList>
                        &nbsp;Month:<asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"
                            Width="90px">
                        </asp:DropDownList>
                        &nbsp;<asp:Button ID="btnMprSort" runat="server" OnClick="btnMprSort_Click" Text="Filter"
                            Width="80px" />
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                        <asp:UpdatePanel ID="updpnl" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gdItem" runat="server" BackColor="White" BorderColor="#6B7EBF"
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="4" ForeColor="#333333" PageSize="100"
                                    SkinID="GridView" OnRowCommand="gdItem_RowCommand" OnSorting="gdItem_Sorting"
                                    Style="border-top-width: 1px; border-left-width: 1px; border-left-color: #e6e6fa;
                                    border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa;
                                    border-right-width: 1px; border-right-color: #e6e6fa;" Width="100%" AllowSorting="True"
                                    AutoGenerateColumns="False">
                                    <FooterStyle BackColor="#6B7EBF" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#6B7EBF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle Font-Bold="True" Wrap="False" />
                                    <HeaderStyle BackColor="#6B7EBF" Font-Bold="True" ForeColor="White" Font-Size="8" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" Font-Underline="False" />
                                    <RowStyle Font-Size="8pt" Wrap="true" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="MPR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMpr" runat="server" Text='<%# Bind("MPR") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ICODE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIcode" runat="server" Text='<%# Bind("ICODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="IDET" HeaderText="IDET">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QTY" HeaderText="QTY">
                                            <ItemStyle Width="60px" Font-Bold="True" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UOM" HeaderText="UOM">
                                            <ItemStyle Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SPECIFICATION" HeaderText="SPECIFICATION">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BRAND" HeaderText="BRAND">
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ORIGIN" HeaderText="ORIGIN">
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PACKING" HeaderText="PACKING">
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ETR" HeaderText="ETR">
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="REMARKS" HeaderText="REMARKS">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="STAT" HeaderText="STAT">
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtMpr" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="optPendMprList" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboItemType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtItemName" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboYear" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboMonth" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnMprSort" EventName="Click" />
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
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderMsg" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnMsgOk" OkControlID="btnMsgOk" PopupControlID="pnlMsg" TargetControlID="hfHidden"
        DropShadow="true">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
