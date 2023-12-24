<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmItemReturn.aspx.cs" Inherits="DRN_WEB_ERP.Module.Inventory.Forms.frmItemReturn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Item Return
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnSearch">
                <div align="center" style="background-color: #86AEAE">
                    <table style="width: 100%; font-size: 14px;">
                        <tr>
                            <td style="width: 563px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 382px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="left" colspan="4">
                                &nbsp;
                            </td>
                            <td style="width: 290px" align="left">
                                &nbsp;
                            </td>
                            <td style="width: 235px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 563px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 382px">
                                Return From
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" colspan="4">
                                <asp:TextBox ID="txtSearchIssLoc" runat="server" CssClass="inline search" Width="600px"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchIssueLoc" runat="server" BehaviorID="AutoCompleteSrchIssuLoc"
                                    CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                    ServiceMethod="GetSrchIssueHead" ServicePath="~/Module/Inventory/Forms/wsAutoCompInv.asmx"
                                    ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearchIssLoc">
                                </cc1:AutoCompleteExtender>
                            </td>
                            <td style="width: 290px" align="left">
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtSearchIssLoc"
                                    ErrorMessage="Enter Valid Issue Location" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"
                                    ValidationGroup="btnSearch">*</asp:CustomValidator>
                            </td>
                            <td style="width: 235px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 563px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 382px">
                                Item Name
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" colspan="4">
                                <span>
                                    <asp:TextBox ID="txtSearchItem" runat="server" CssClass="search textAlignCenter"
                                        Width="600px"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchItem" runat="server" BehaviorID="AutoCompleteSrchItem"
                                        CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                        CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                        ServiceMethod="GetSrchItem" ServicePath="~/Module/Inventory/Forms/wsAutoCompInv.asmx"
                                        ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearchItem">
                                    </cc1:AutoCompleteExtender>
                                </span>
                            </td>
                            <td align="left" style="width: 290px">
                                <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtSearchItem"
                                    ErrorMessage="Enter Valid Item" ForeColor="Red" OnServerValidate="CustomValidator2_ServerValidate"
                                    ValidationGroup="btnSearch">*</asp:CustomValidator>
                            </td>
                            <td style="width: 235px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 563px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 382px">
                                Issued Date From
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" style="width: 549px">
                                <asp:TextBox ID="txtIssDateFrm" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                    ForeColor="Blue" Width="120px" Enabled="False"></asp:TextBox>
                                <asp:Image ID="imgIssDtFrm" runat="server" ImageUrl="~/Image/calendar.png" />
                                <cc1:CalendarExtender ID="CalenderIssDtFrm" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    PopupButtonID="imgIssDtFrm" TargetControlID="txtIssDateFrm">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCust0" runat="server" ControlToValidate="txtIssDateFrm"
                                    ErrorMessage="Enter Date From" ForeColor="Red" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>
                            </td>
                            <td align="left" style="width: 71px">
                                To Date
                            </td>
                            <td align="left">
                                :
                            </td>
                            <td align="left" style="width: 290px">
                                <asp:TextBox ID="txtIssDateTo" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                    ForeColor="Blue" Width="120px" Enabled="False"></asp:TextBox>
                                <asp:Image ID="imgIssDtTo" runat="server" ImageUrl="~/Image/calendar.png" />
                                <cc1:CalendarExtender ID="CalenderIssDtTo" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    PopupButtonID="imgIssDtTo" TargetControlID="txtIssDateTo">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCust1" runat="server" ControlToValidate="txtIssDateTo"
                                    ErrorMessage="Enter Date From To" ForeColor="Red" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 290px" align="left">
                                &nbsp;
                            </td>
                            <td style="width: 235px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 563px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 382px">
                                <asp:TextBox ID="TextBox1" runat="server" Enabled="False" Visible="False" Width="50px">chk</asp:TextBox>
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="left" colspan="4">
                                <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="TextBox1"
                                    ErrorMessage="Enter Valid Search Criteria" ForeColor="Red" OnServerValidate="CustomValidator3_ServerValidate"
                                    ValidationGroup="btnSearch">*</asp:CustomValidator>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="8pt" ForeColor="Red"
                                    ValidationGroup="btnSearch" />
                            </td>
                            <td align="left" style="width: 290px">
                                &nbsp;
                            </td>
                            <td style="width: 235px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 563px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 382px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" onkeypress="return clickButton(event,'btnSearch')"
                                    Text="Search" ValidationGroup="btnSearch" />
                                <asp:Button ID="btnClearSrch" runat="server" OnClick="btnClearSrch_Click" Text="Clear"
                                    Width="60px" />
                            </td>
                            <td align="center" colspan="2">
                                &nbsp;
                            </td>
                            <td style="width: 290px" align="left">
                                &nbsp;
                            </td>
                            <td style="width: 235px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </div>
                <div align="center" style="background-color: #86AEAE">
                    <asp:GridView ID="gvItemIssue" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Size="8pt" AllowPaging="True" PageSize="25" AllowSorting="True" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Trn_Hdr_Date" DataFormatString="{0:d}" HeaderText="Issue Date" />
                            <asp:TemplateField HeaderText="Issue Ref. No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfIsuHdrRef" runat="server" Value='<%# Bind("Trn_Hdr_Ref") %>' />
                                    <asp:Label ID="lblIsuHdrRefNo" runat="server" Text='<%# Bind("Trn_Det_Ref") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Line#">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsuDetLno" runat="server" Text='<%# Bind("Trn_Det_Lno") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issued To">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfIsuLocRef" runat="server" Value='<%# Bind("Trn_Hdr_Pcode") %>' />
                                    <asp:Label ID="lblIssuLoc" runat="server" Text='<%# GetIssueTo(Eval("Trn_Hdr_Pcode").ToString())%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfIsuDetIcode" runat="server" Value='<%# Bind("Trn_Det_Icode") %>' />
                                    <asp:Label ID="lblIsuDetItemName" runat="server" Text='<%# Bind("Trn_Det_Itm_Desc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsuDetItemUnit" runat="server" Text='<%# Bind("Trn_Det_Itm_Uom") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Store">
                                <ItemTemplate>
                                    <asp:HiddenField ID="lblIsuStoreRef" runat="server" Value='<%# Bind("Trn_Det_Str_Code") %>' />
                                    <asp:Label ID="lblIsuStoreName" runat="server" Text='<%# GetStore(Eval("Trn_Det_Str_Code").ToString())%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Trn_Det_Lin_Qty" HeaderText="Issue Qty" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Return Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotRtnQty" runat="server" Text='<%# GetTotRtnQty(Eval("Trn_Det_Ref").ToString(), Eval("Trn_Det_Icode").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRtnQty" runat="server" CssClass="textAlignCenter" Width="90px"
                                        Text='<%# GetTotRtnBalQty(Eval("Trn_Hdr_Ref").ToString(), Eval("Trn_Det_Icode").ToString(), Eval("Trn_Det_Ref").ToString()) %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnReturn" runat="server" Text="Return" OnClientClick="return confirm('Do you want to return?')"
                                        OnClick="btnReturn_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="8pt"
                            HorizontalAlign="Left" Wrap="False" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                            Wrap="true" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#487575" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#275353" />
                    </asp:GridView>
                    <br />
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtSearchIssLoc" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtSearchItem" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvItemIssue" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="gvItemIssue" EventName="RowDataBound" />
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
