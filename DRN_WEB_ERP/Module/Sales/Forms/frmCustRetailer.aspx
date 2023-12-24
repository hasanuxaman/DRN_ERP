<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmCustRetailer.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmCustRetailer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Retailer Information</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="border: 1px solid #CCCCCC; background-color: #CCFFFF;">
                <table style="width: 100%; background-color: #CCFFFF; font-size: small;">
                    <tr>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 300px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
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
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            Retailer Ref
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:TextBox ID="txtRtlrRefNo" runat="server" BackColor="#CCCCFF" BorderStyle="Solid"
                                CssClass="textAlignCenter" Enabled="False" Width="352px" Font-Bold="True"></asp:TextBox>
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td rowspan="21">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="8" ForeColor="Red"
                                ValidationGroup="Save" Width="281px" DisplayMode="List" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            Retailer Name
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:TextBox ID="txtRtlrName" runat="server" CssClass="capitalize" Width="350px"></asp:TextBox>
                        </td>
                        <td style="width: 3px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtRtlrName"
                                ErrorMessage="Enter Retailer Name" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 136px">
                            Type
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td align="left" style="width: 300px">
                            <asp:DropDownList ID="cboRtlrType" runat="server" ValidationGroup="ChkData" Width="353px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 3px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cboRtlrType"
                                ErrorMessage="Select Retailer Type" ForeColor="Red" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            Address
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" rowspan="3" valign="top" align="left">
                            <asp:TextBox ID="txtRtlrAdr" runat="server" Height="60px" TextMode="MultiLine" Width="348px"></asp:TextBox>
                        </td>
                        <td style="width: 3px">
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
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
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
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
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
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            Location
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:DropDownList ID="cboRtlrLoc" runat="server" AutoPostBack="True" ValidationGroup="ChkData"
                                Width="353px" OnSelectedIndexChanged="cboRtlrLoc_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 3px">
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
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 136px">
                            Distance (km)
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td align="left" style="width: 300px">
                            <asp:TextBox ID="txtDistance" runat="server" Width="350px" Enabled="False"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtDistance_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDistance"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="width: 3px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtDistance"
                                ErrorMessage="Enter Distance (km)" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtDistance"
                                ErrorMessage="Enter Valid Distance (km)" ForeColor="Red" Operator="DataTypeCheck"
                                Type="Currency" ValidationGroup="Save">*</asp:CompareValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 136px">
                            Contact Person (CP)
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td align="left" style="width: 300px">
                            <asp:TextBox ID="txtRtlrCp" runat="server" CssClass="capitalize" Width="350px"></asp:TextBox>
                        </td>
                        <td style="width: 3px">
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
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            CP Designation
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:TextBox ID="txtRtlrCpDesig" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td style="width: 3px">
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
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            Cell No
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:TextBox ID="txtRtlrCell" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td style="width: 3px">
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
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            Phone No
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:TextBox ID="txtRtlrPhone" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td style="width: 3px">
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
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            Fax No
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:TextBox ID="txtRtlrFax" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td style="width: 3px">
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
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            E-Mail
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:TextBox ID="txtRtlrEmail" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td style="width: 3px">
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
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            District
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:DropDownList ID="cboRtlrDist" runat="server" ValidationGroup="ChkData" Width="353px"
                                AutoPostBack="True" OnSelectedIndexChanged="cboCustDist_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 3px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboRtlrDist"
                                ErrorMessage="Select District" ForeColor="Red" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            Thana
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:DropDownList ID="cboRtlrThana" runat="server" ValidationGroup="ChkData" Width="353px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 3px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cboRtlrThana"
                                ErrorMessage="Select Thana" ForeColor="Red" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            Sales Zone
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:DropDownList ID="cboRtlrZone" runat="server" ValidationGroup="ChkData" Width="353px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 3px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cboRtlrZone"
                                ErrorMessage="Select Sales Zone" ForeColor="Red" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            Dealer Name
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:TextBox ID="txtCustAdrCode" runat="server" Width="333px" CssClass="inline search"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderDealer" runat="server" BehaviorID="AutoCompleteSrchDealer"
                                CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCustomer"
                                ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                TargetControlID="txtCustAdrCode">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td style="width: 3px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtCustAdrCode"
                                ErrorMessage="Enter Dealer Name" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtCustAdrCode"
                                ErrorMessage="Enter Valid Dealer" ForeColor="Red" OnServerValidate="CustomValidator2_ServerValidate"
                                ValidationGroup="Save">*</asp:CustomValidator>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            DSM Name
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:DropDownList ID="cboDsm" runat="server" Width="353px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 3px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="cboDsm"
                                ErrorMessage="Select DSM Name" ForeColor="Red" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 136px">
                            SP Name
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td align="left" style="width: 300px">
                            <asp:TextBox ID="txtSalesPer" runat="server" CssClass="inline search" Width="333px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="txtSalesPer_AutoCompleteExtender" runat="server" BehaviorID="AutoCompleteSrchSalesPer"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetSrchSalesPer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSalesPer">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td style="width: 3px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtSalesPer"
                                ErrorMessage="Enter SP Name" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="txtSalesPer"
                                ErrorMessage="Enter Valid SP Name" ForeColor="Red" OnServerValidate="CustomValidator4_ServerValidate"
                                ValidationGroup="Save">*</asp:CustomValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 136px" align="left">
                            Status
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:RadioButtonList ID="optListRtlrStatus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="width: 3px">
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
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td colspan="3" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
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
            </div>
            <div align="center" style="background-color: #66FFFF">
                <br />
                <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="Clear" Width="100px"
                    OnClick="btnClear_Click" />
                <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" ValidationGroup="Save"
                    Width="100px" OnClick="btnSave_Click" />
                <br />
                <br />
            </div>
            <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnSearch">
                <div align="center" style="background-color: #86AEAE">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 13px">
                                &nbsp;
                            </td>
                            <td style="width: 230px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="60">
                                &nbsp;
                            </td>
                            <td width="370" align="center">
                                Search Filter
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 13px">
                                &nbsp;
                            </td>
                            <td style="width: 230px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="60">
                                Dealer:
                            </td>
                            <td width="370" align="left">
                                <asp:TextBox ID="txtSearchDlr" runat="server" Width="350px" CssClass="search textAlignCenter"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchDlr" runat="server" BehaviorID="AutoCompleteSrchDlr"
                                    CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                    DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCustomer"
                                    ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                    TargetControlID="txtSearchDlr">
                                </cc1:AutoCompleteExtender>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 13px">
                                &nbsp;
                            </td>
                            <td style="width: 230px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="60">
                                Retailer:
                            </td>
                            <td width="370" align="left">
                                <asp:TextBox ID="txtSearchRtl" runat="server" CssClass="search textAlignCenter" Width="350px"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchRtlr" runat="server" BehaviorID="AutoCompleteSrchRtlr"
                                    CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                    ServiceMethod="GetSrchRetailer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                    ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearchRtl">
                                </cc1:AutoCompleteExtender>
                            </td>
                            <td align="left">
                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" onkeypress="return clickButton(event,'btnSearch')"
                                    Text="Search" ValidationGroup="btnSearch" />
                                <asp:Button ID="btnClearSrch" runat="server" OnClick="btnClearSrch_Click" Text="Clear"
                                    Visible="False" Width="60px" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 13px">
                                &nbsp;
                            </td>
                            <td style="width: 230px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="60">
                                &nbsp;
                            </td>
                            <td width="370" align="center">
                                <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export To Excel" />
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div align="center" style="background-color: #86AEAE">
                    <asp:GridView ID="gvRtl" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" EmptyDataText="No Data  Found."
                        Font-Size="8pt" AllowPaging="True" PageSize="50" AllowSorting="True" OnRowDataBound="gvRtl_RowDataBound"
                        OnPageIndexChanging="gvRtl_PageIndexChanging" OnSelectedIndexChanged="gvRtl_SelectedIndexChanged"
                        OnSorting="gvRtl_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Par_Adr_Name" HeaderText="Dealer Name" SortExpression="Par_Adr_Name" />
                            <asp:TemplateField HeaderText="RTL. Ref.">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfCustRef" runat="server" Value='<%# Bind("Par_Rtl_Ref") %>' />
                                    <asp:Label ID="lblCustRefNo" runat="server" Text='<%# Bind("Par_Rtl_Ref_No") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Par_Rtl_Name" HeaderText="Name" SortExpression="Par_Rtl_Name">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Rtl_Addr" HeaderText="Address">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Rtl_Cont_Per" HeaderText="Contact Person">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Rtl_Cell_No" HeaderText="Cell No">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Rtl_Tel_No" HeaderText="Phone No">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Rtl_Email_Id" HeaderText="Email">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TrTr_Loc_Name" HeaderText="Location" SortExpression="TrTr_Loc_Name" />
                            <asp:BoundField DataField="TrTr_Loc_Distance_Km" HeaderText="Distance" SortExpression="TrTr_Loc_Distance_Km" />
                            <asp:BoundField DataField="Par_Rtl_Status" HeaderText="Status" SortExpression="Par_Rtl_Status" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" Font-Size="8pt"
                            HorizontalAlign="Left" Wrap="False" />
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
                        <asp:HiddenField ID="hfEditStatus" runat="server" Value="N" />
                        <asp:HiddenField ID="hfRefNo" runat="server" Value="0" />
                    </span>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboRtlrDist" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClearSrch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvRtl" EventName="PageIndexChanging" />
            <asp:AsyncPostBackTrigger ControlID="gvRtl" EventName="RowDataBound" />
            <asp:AsyncPostBackTrigger ControlID="gvRtl" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:AsyncPostBackTrigger ControlID="cboRtlrLoc" EventName="SelectedIndexChanged" />
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
