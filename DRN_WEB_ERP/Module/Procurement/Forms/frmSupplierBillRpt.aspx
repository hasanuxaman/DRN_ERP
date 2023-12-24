<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmSupplierBillRpt.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmSupplierBillRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Bill Report</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <table style="width: 100%; font-size: 12px; background-color: #CCFFFF;">
                    <tr>
                        <td style="width: 22px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td style="width: 231px">
                            &nbsp;
                        </td>
                        <td style="width: 47px">
                            &nbsp;
                        </td>
                        <td width="2">
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
                        <td style="width: 22px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td style="width: 231px">
                            &nbsp;
                        </td>
                        <td style="width: 47px">
                            Filter By
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td>
                            <asp:RadioButtonList ID="optRptFilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="optRptFilter_SelectedIndexChanged"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">None</asp:ListItem>
                                <asp:ListItem Value="1">Supplier Name</asp:ListItem>
                                <asp:ListItem Value="2">Item Name</asp:ListItem>
                                <asp:ListItem Value="3">Item Category</asp:ListItem>
                                <asp:ListItem Value="4">MRR Ref No</asp:ListItem>
                                <asp:ListItem Value="5">PO Ref No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 22px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td style="width: 231px">
                            &nbsp;
                        </td>
                        <td style="width: 47px">
                            &nbsp;
                        </td>
                        <td width="2">
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
                <table style="width: 100%; font-size: 12px; background-color: #CCFFFF;">
                    <tr>
                        <td style="width: 21px">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 138px">
                            Date From
                        </td>
                        <td>
                            :
                        </td>
                        <td style="width: 168px">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtDtFromCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                            <asp:Image ID="imgFromDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                        </td>
                        <td align="right" style="width: 97px">
                            Date To
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 146px">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgToDate" TargetControlID="txtToDate">
                            </cc1:CalendarExtender>
                            <asp:Image ID="imgToDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png"
                                Width="16px" />
                        </td>
                        <td align="right" style="width: 127px">
                            Search Filter
                        </td>
                        <td style="width: 6px">
                            :
                        </td>
                        <td style="width: 136px">
                            <asp:TextBox ID="txtSearch" runat="server" Width="320px" CssClass="search textAlignCenter"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                                CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchSupplier1"
                                ServicePath="~/Module/Procurement/Forms/wsAutoComProc.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                TargetControlID="txtSearch">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnShowRpt" runat="server" OnClick="btnShowRpt_Click" Text="Show"
                                Width="100px" />
                        </td>
                        <td style="width: 98px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
                            &nbsp;
                        </td>
                        <td style="width: 138px">
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFromDate"
                                Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFromDate"
                                Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                Type="Date" ValidationGroup="btnShow"></asp:CompareValidator>
                        </td>
                        <td style="width: 97px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 146px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                                Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtToDate"
                                Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                Type="Date" ValidationGroup="btnShow"></asp:CompareValidator>
                        </td>
                        <td style="width: 127px">
                            &nbsp;
                        </td>
                        <td style="width: 6px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 98px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
                            &nbsp;
                        </td>
                        <td style="width: 138px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 97px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 146px">
                            &nbsp;
                        </td>
                        <td style="width: 127px">
                            &nbsp;
                        </td>
                        <td style="width: 6px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 98px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
                            &nbsp;
                        </td>
                        <td style="width: 138px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 168px">
                            &nbsp;
                        </td>
                        <td style="width: 97px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 146px">
                            &nbsp;
                        </td>
                        <td style="width: 127px">
                            &nbsp;<asp:Button ID="btnExport" runat="server" Enabled="False" OnClick="btnExport_Click"
                                Text="Export to Excel" />
                        </td>
                        <td style="width: 6px">
                            &nbsp;
                        </td>
                        <td style="width: 136px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 98px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="background-color: #CCFFFF">
                <asp:GridView ID="gvPoBill" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="8pt"
                    GridLines="Vertical" OnRowCommand="gvPoBill_RowCommand">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle Width="20px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Bill_Hdr_Ext_Data1" HeaderText="Supplier Name">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Bill No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfBillRef" runat="server" Value='<%# Bind("Bill_Hdr_Ref") %>' />
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Bill_Hdr_Ref_No") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Bill_Hdr_Date" HeaderText="Bill Date" DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Bill_Hdr_Party_Bill_No" HeaderText="Supplier Bill#">
                            <ItemStyle Width="70px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="MRR Ref. No">
                            <ItemTemplate>
                                <asp:Label ID="lblMrrRefNo" runat="server" Text='<%# GetMrrRef(Eval("Bill_Hdr_Ref").ToString())%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="125px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Bill_Hdr_Tot_Amount" HeaderText="Bill Amount">
                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Bill_Hdr_Adj_Amount" HeaderText="Adjustment">
                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Bill_Hdr_Net_Amount" HeaderText="Net Amount">
                            <ItemStyle HorizontalAlign="Right" BackColor="#CCCCCC" Font-Bold="True" Width="90px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Bill_Hdr_Pay_Amount" HeaderText="Pay Amount">
                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Bill_Hdr_Due_Amount" HeaderText="Due Amount">
                            <ItemStyle HorizontalAlign="Right" Font-Bold="True" Width="90px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBillDetails" runat="server" Text="Details" CommandName="Details"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="optRptFilter" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnShowRpt" EventName="Click" />
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
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupExtenderMsg" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnMsgOk" OkControlID="btnMsgOk" PopupControlID="pnlMsg" TargetControlID="hfHidden"
        DropShadow="true">
    </cc1:ModalPopupExtender>
</asp:Content>
