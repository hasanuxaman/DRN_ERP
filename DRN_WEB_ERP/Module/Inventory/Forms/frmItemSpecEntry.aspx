<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmItemSpecEntry.aspx.cs" Inherits="DRN_WEB_ERP.Module.Inventory.Forms.frmItemSpecEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Item Specification Entry</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #CCFFFF;">
                <div style="border: 1px solid #CCCCCC; width: 55%; background-color: #CCFFFF;">
                    <table style="width: 100%; background-color: #CCFFFF; font-size: small;">
                        <tr>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td style="width: 126px" align="left">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td style="width: 300px" align="left">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td style="width: 126px" align="left">
                                Spec. Ref No
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 300px" align="left">
                                <asp:TextBox ID="txtItemSpecRefNo" runat="server" BackColor="#CCCCFF" BorderStyle="Solid"
                                    CssClass="textAlignCenter" Enabled="False" Width="352px" Font-Bold="True"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td style="width: 126px" align="left">
                                Item Type
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 300px" align="left">
                                <asp:DropDownList ID="cboItemType" runat="server" ValidationGroup="ChkData" Width="353px"
                                    OnSelectedIndexChanged="cboItemType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cboItemType"
                                    ErrorMessage="Select Item Type" ForeColor="Red" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 126px">
                                Item Name
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td align="left" style="width: 300px">
                                <asp:DropDownList ID="cboItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrdItem_SelectedIndexChanged"
                                    Width="353px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="cboItemName"
                                    ErrorMessage="Select Item Name" ForeColor="Red" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 126px">
                                Spec. Name
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td align="left" style="width: 300px">
                                <asp:TextBox ID="txtISpecName" runat="server" CssClass="capitalize" Width="350px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtISpecName"
                                    ErrorMessage="Enter Spec. Name" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 126px">
                                Status
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td align="left" style="width: 300px">
                                <asp:RadioButtonList ID="optListItemSpecStatus" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td colspan="3" align="left">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div align="center">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Save"
                        Font-Size="8" ForeColor="Red" Width="350px" />
                </div>
                <div align="center" style="background-color: #66FFFF">
                    <br />
                    <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="Clear" Width="100px"
                        OnClick="btnClear_Click" />
                    <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" ValidationGroup="Save"
                        Width="100px" OnClick="btnSave_Click" />
                    <br />
                    <span>
                        <asp:HiddenField ID="hfEditStatus" runat="server" Value="N" />
                        <asp:HiddenField ID="hfRefNo" runat="server" Value="0" />
                    </span>
                    <br />
                </div>
            </div>
            <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnSearch">
                <div align="center" style="background-color: #86AEAE">
                    <span>
                        <br />
                        Search Specification:</span> <span>
                            <asp:TextBox ID="txtSearch" runat="server" Width="350px" CssClass="search textAlignCenter"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                                CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchItem" ServicePath="~/Module/Inventory/Forms/wsAutoCompInv.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearch">
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
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" 
                        Text="Update Mechanical" Visible="False" />
                    &nbsp;
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" 
                        Text="Update Batch Item" />
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
                                Item
                            </th>
                            <%--<th align="center" scope="col" width="80">
                                    &nbsp;
                                </th>--%>
                        </tr>
                        <tr class="gridFooterRow" style="background-color: #86AEAE;">
                            <td align="center" bgcolor="#009933" style="color: #F7F7F7; font-size: small; font-weight: bold">
                                &nbsp; Filter By:
                            </td>
                            <td>
                                <asp:DropDownList ID="cboItemTypeSrch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboItemTypeSrch_SelectedIndexChanged"
                                    Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboItem" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrdItem_SelectedIndexChanged"
                                    Width="230px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div align="center" style="background-color: #86AEAE">
                    <asp:GridView ID="gvItemSpec" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" EmptyDataText="No Data  Found."
                        Font-Size="8pt" AllowPaging="True" PageSize="50" AllowSorting="True" OnRowDataBound="gvItemSpec_RowDataBound"
                        OnPageIndexChanging="gvItemSpec_PageIndexChanging" OnSelectedIndexChanged="gvItemSpec_SelectedIndexChanged"
                        OnSorting="gvItemSpec_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Itm_Det_T_C1" HeaderText="Type" SortExpression="Itm_Det_T_C1">
                            </asp:BoundField>
                            <asp:BoundField DataField="Itm_Det_Desc" HeaderText="Item Name" SortExpression="Itm_Det_Desc">
                                <ItemStyle Width="250px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfItemSpecRef" runat="server" Value='<%# Bind("Spec_Ref") %>' />
                                    <asp:Label ID="lblItemSpecRefNo" runat="server" Text='<%# Bind("Spec_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Itm_Det_Stk_Unit" HeaderText="Unit">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Spec_Status" HeaderText="Status" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" Font-Size="8pt"
                            HorizontalAlign="Left" Wrap="False" />
                        <PagerSettings Position="TopAndBottom" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px"
                            HorizontalAlign="Left" Wrap="true" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#487575" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#275353" />
                    </asp:GridView>
                    <span>
                        <br />
                        <asp:HiddenField ID="HiddenField1" runat="server" Value="N" />
                        <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                    </span>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClearSrch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="cboItemTypeSrch" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboItem" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvItemSpec" EventName="RowDataBound" />
            <asp:AsyncPostBackTrigger ControlID="gvItemSpec" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvItemSpec" EventName="PageIndexChanging" />
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
