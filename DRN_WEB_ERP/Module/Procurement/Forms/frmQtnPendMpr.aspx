<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmQtnPendMpr.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmQtnPendMpr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="UserControl/CtlQtnView.ascx" TagName="CtlQtnView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Quotation Entry</div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tblmas" style="width: 100%" id="tblmaster" runat="server">
                <tr>
                    <td class="tbl" colspan="3" style="height: 8px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                        <asp:Button ID="btnQuotation0" runat="server" Text="QUOTATION ENTRY" Width="159px"
                            OnClick="btnQuotation_Click" Visible="False" />
                        &nbsp;<asp:Button ID="btnCSEntry" runat="server" Text="C/S ENTRY" Width="159px" OnClick="btnCSEntry_Click"
                            Visible="False" />
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
                                        <asp:ListItem Value="3">Show Only Quoted</asp:ListItem>
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
                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="4" ForeColor="#333333" GridLines="None"
                                    PageSize="100" SkinID="GridView" OnRowCommand="gdItem_RowCommand" OnSorting="gdItem_Sorting"
                                    OnRowDataBound="gdItem_RowDataBound" Style="border-top-width: 1px; border-left-width: 1px;
                                    border-left-color: #e6e6fa; border-bottom-width: 1px; border-bottom-color: #e6e6fa;
                                    border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;"
                                    Width="100%" AllowSorting="True" AutoGenerateColumns="False">
                                    <FooterStyle BackColor="#6B7EBF" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#6B7EBF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle Font-Bold="True" Wrap="False" />
                                    <HeaderStyle BackColor="#6B7EBF" Font-Bold="True" ForeColor="White" Font-Size="8" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" Font-Underline="False" />
                                    <RowStyle Font-Size="8pt" Wrap="true" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sel">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qtn">
                                            <ItemTemplate>
                                                <uc1:CtlQtnView ID="ctl1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <%--<asp:Button ID="Button1" runat="server" Text="Proceed" OnClick="Button1_Click" CssClass="btn2"
                                                    Visible="false" />--%>
                                                <asp:Button ID="btnEditQtn" runat="server" Text="Edit" CssClass="btn2" OnClick="btnEditQtn_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MPR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMpr" runat="server" Text='<%# Bind("MPR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ICODE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIcode" runat="server" Text='<%# Bind("ICODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="IDET" HeaderText="IDET" />
                                        <asp:BoundField DataField="QTY" HeaderText="QTY" />
                                        <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                        <asp:BoundField DataField="SPECIFICATION" HeaderText="SPECIFICATION" />
                                        <asp:BoundField DataField="BRAND" HeaderText="BRAND" />
                                        <asp:BoundField DataField="ORIGIN" HeaderText="ORIGIN" />
                                        <asp:BoundField DataField="PACKING" HeaderText="PACKING" />
                                        <asp:BoundField DataField="ETR" HeaderText="ETR" />
                                        <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" />
                                        <asp:BoundField DataField="STAT" HeaderText="STAT" />
                                    </Columns>
                                </asp:GridView>
                                <asp:Panel ID="PNL" runat="server" CssClass="tbl" Style="border-right: black 2px solid;
                                    padding-right: 20px; border-top: black 2px solid; padding-left: 20px; display: none;
                                    padding-bottom: 20px; border-left: black 2px solid; width: 500px; padding-top: 20px;
                                    border-bottom: black 2px solid; background-color: white" Height="224px" Width="500px">
                                    &nbsp;<br />
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 66px; text-align: left;">
                                                MPR
                                            </td>
                                            <td style="width: 6px">
                                                :
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblmpr" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 66px; text-align: left">
                                                Item
                                            </td>
                                            <td style="width: 6px">
                                                :
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblitem" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfPrDetLno" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 66px; text-align: left">
                                                &nbsp;
                                            </td>
                                            <td style="width: 6px">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 66px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 6px">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: left">
                                                <asp:CheckBox ID="chkurgent" runat="server" Font-Bold="True" ForeColor="#FF3300"
                                                    Text="URGENT" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 66px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 6px">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 66px; text-align: left;">
                                                Comments
                                            </td>
                                            <td style="width: 6px">
                                                :
                                            </td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtcomments" runat="server" CssClass="txtbox" Width="387px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 66px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 6px">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div style="text-align: right">
                                        <asp:Button ID="ButtonOk" runat="server" CssClass="btn2" Text="OK" Width="80px" OnClick="ButtonOk_Click" />
                                        &nbsp;<asp:Button ID="ButtonCancel" runat="server" CssClass="btn2" Text="Cancel"
                                            Width="82px" />
                                    </div>
                                </asp:Panel>
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="ButtonCancel" PopupControlID="PNL" TargetControlID="Button2">
                                </ajaxToolkit:ModalPopupExtender>
                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" DisplayModalPopupID="ModalPopupExtender1"
                                    TargetControlID="Button2">
                                </ajaxToolkit:ConfirmButtonExtender>
                                <asp:Button ID="Button2" runat="server" Visible="false" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="ButtonOk" />
                            </Triggers>
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
                        <asp:Button ID="btnQuotation" runat="server" Text="QUOTATION ENTRY" Width="159px"
                            OnClick="btnQuotation_Click" Visible="False" />
                        &nbsp;<asp:Button ID="btnCSEntry0" runat="server" Text="C/S ENTRY" Width="159px"
                            OnClick="btnCSEntry_Click" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                        <table id="tbltooltip" runat="server" class="tbl" style="border: thin solid #000000;
                            width: 836px">
                            <tr>
                                <td style="width: 21px" bgcolor="#ccccff">
                                    SL
                                </td>
                                <td bgcolor="#ccccff">
                                    Party
                                </td>
                                <td bgcolor="#ccccff">
                                    Rate
                                </td>
                                <td bgcolor="#ccccff">
                                    Specification
                                </td>
                                <td bgcolor="#ccccff">
                                    Brand
                                </td>
                                <td bgcolor="#ccccff">
                                    Origin
                                </td>
                                <td bgcolor="#ccccff">
                                    Packing
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 21px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
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
                                <td style="width: 21px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
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
                                <td style="width: 21px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
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
                                <td style="width: 21px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
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
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tbltooltip2" runat="server" class="style3" style="background-color: #FFFFFF;
                            background-position: center center; width: 772px;">
                            <tr>
                                <td class="style4" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style6" colspan="2">
                                    TERMS AND CONDITIONS
                                </td>
                            </tr>
                            <tr>
                                <td class="style5" valign="top">
                                    General Terms:
                                </td>
                                <td bgcolor="AliceBlue">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style5" valign="top">
                                    Special Terms:
                                </td>
                                <td bgcolor="AliceBlue">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style5" valign="top">
                                    Pay Terms:
                                </td>
                                <td bgcolor="AliceBlue">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style5" valign="top">
                                    Valid Days:
                                </td>
                                <td bgcolor="AliceBlue">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style5">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="height: 57px">
                    </td>
                    <td style="height: 57px">
                    </td>
                    <td style="height: 57px">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnQuotation" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnQuotation0" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCSEntry" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCSEntry0" EventName="Click" />
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
