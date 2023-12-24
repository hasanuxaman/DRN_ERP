<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmInvStockStat.aspx.cs" Inherits="DRN_WEB_ERP.Module.Inventory.Forms.frmInvStockStat" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
                        
    </script>
    <div align="center" style="background-color: #00FF99">
        Stock Status Inquery</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <br />
                <asp:Panel ID="Panel1" runat="server" GroupingText="Stock Status Inquery">
                    <br />
                    <table border="1" cellspacing="0" rules="all" style="border-collapse: collapse;">
                        <tr style="background-color: #009933; font-weight: bold; color: #F7F7F7; font-size: small;
                            white-space: nowrap; height: 30px;">
                            <th align="center" scope="col" style="width: 200px;" width="70">
                                &nbsp;
                            </th>
                            <th align="center" scope="col" style="width: 200px;" width="70">
                                Item Type
                            </th>
                            <th align="center" scope="col" style="width: 210px;" width="70">
                                Item Name
                                <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtItemName"
                                    ErrorMessage="Enter Valid Item" ForeColor="Red" OnServerValidate="CustomValidator3_ServerValidate"
                                    ValidationGroup="btnSearch">*</asp:CustomValidator>
                            </th>
                            <%--<th align="center" scope="col" width="80">
                                    &nbsp;
                                </th>--%>
                            <th align="center" scope="col" style="width: 210px;" width="70">
                                Store
                            </th>
                            <th align="center" scope="col" style="width: 210px;" width="80">
                                &nbsp;
                            </th>
                        </tr>
                        <tr class="gridFooterRow" style="background-color: #86AEAE;">
                            <td bgcolor="#009933" align="center" style="color: #F7F7F7; font-size: small; font-weight: bold">
                                Filter By:
                            </td>
                            <td>
                                <asp:DropDownList ID="cboItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboItemType_SelectedIndexChanged"
                                    Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <%--<asp:DropDownList ID="cboItem" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrdItem_SelectedIndexChanged"
                                    Width="230px">
                                </asp:DropDownList>--%>
                                <asp:TextBox ID="txtItemName" runat="server" CssClass="search textAlignCenter" Width="350px"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchItem" runat="server" BehaviorID="AutoCompleteSrchItem"
                                    CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                    ServiceMethod="GetSrchFilteredItem" ServicePath="~/Module/Inventory/Forms/wsAutoCompInv.asmx"
                                    ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtItemName">
                                </cc1:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboStore" runat="server" Width="230px">
                                </asp:DropDownList>
                            </td>
                            <td align="center">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                    ValidationGroup="btnSearch" />
                                &nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click"
                                    Width="61px" />
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export To Excel" />
                    <br />
                    <asp:GridView ID="gvStkStat" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" Font-Size="8pt" AllowPaging="True" PageSize="50" AllowSorting="True"
                        OnPageIndexChanging="gvStkStat_PageIndexChanging" OnSorting="gvStkStat_Sorting">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Str_Loc_Name" HeaderText="Store Name" SortExpression="Str_Loc_Name" />
                            <asp:BoundField DataField="Itm_Det_Ref" HeaderText="Item Ref.">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Itm_Det_Code" HeaderText="Item Code">
                                <ItemStyle Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Itm_Det_T_C1" HeaderText="Item Type" SortExpression="Itm_Det_T_C1">
                                <ItemStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Itm_Det_Desc" HeaderText="Item Name" SortExpression="Itm_Det_Desc">
                                <ItemStyle Font-Bold="True" Width="300px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Itm_Det_Stk_Unit" HeaderText="Unit">
                                <ItemStyle Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Stk_Ctl_Cur_Stk" HeaderText="Stock Qty">
                                <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Cur_Stk_Val" HeaderText="Stock Value">
                                <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="90px" />
                            </asp:BoundField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="10pt" />
                        <PagerSettings Position="TopAndBottom" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                    <br />
                </asp:Panel>
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboItemType" EventName="SelectedIndexChanged" />
            <%--<asp:AsyncPostBackTrigger ControlID="cboItem" EventName="SelectedIndexChanged" />--%>
            <asp:AsyncPostBackTrigger ControlID="cboStore" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
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
