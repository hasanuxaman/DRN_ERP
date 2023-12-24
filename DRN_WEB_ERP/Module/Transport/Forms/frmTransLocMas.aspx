<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmTransLocMas.aspx.cs" Inherits="DRN_WEB_ERP.Module.Transport.Forms.frmTransLocMas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Delivery Location
    </div>
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
                            Location Ref. No
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:TextBox ID="txtLocRefNo" runat="server" BackColor="#CCCCFF" BorderStyle="Solid"
                                CssClass="textAlignCenter" Enabled="False" Width="352px" Font-Bold="True"></asp:TextBox>
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td rowspan="10">
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
                            Location Name
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:TextBox ID="txtLocName" runat="server" CssClass="capitalize" Width="350px"></asp:TextBox>
                        </td>
                        <td style="width: 3px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtLocName"
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
                        <td style="width: 136px" align="left">
                            Remarks
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" rowspan="3" valign="top" align="left">
                            <asp:TextBox ID="txtLocRem" runat="server" Height="60px" TextMode="MultiLine" Width="348px"></asp:TextBox>
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
                            District
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:DropDownList ID="cboLocDist" runat="server" ValidationGroup="ChkData" Width="353px"
                                AutoPostBack="True" OnSelectedIndexChanged="cboLocDist_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 3px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboLocDist"
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
                            <asp:DropDownList ID="cboLocThana" runat="server" ValidationGroup="ChkData" Width="353px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 3px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cboLocThana"
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
                        <td align="left" style="width: 136px">
                            Distance (km)
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td align="left" style="width: 300px">
                            <asp:TextBox ID="txtLocDistance" runat="server" Width="350px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtLocDistance_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtLocDistance"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
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
                            Rent
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td align="left" style="width: 300px">
                            <asp:TextBox ID="txtLocRent" runat="server" Width="350px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtLocRent_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtLocRent"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
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
                            Status
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" align="left">
                            <asp:RadioButtonList ID="optListLocStatus" runat="server" RepeatDirection="Horizontal">
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
                            <td>
                                &nbsp;
                            </td>
                            <td width="55">
                                &nbsp;
                            </td>
                            <td width="150">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="55">
                                &nbsp;
                            </td>
                            <td width="150">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="60">
                                &nbsp;
                            </td>
                            <td width="350">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td width="55">
                                District:
                            </td>
                            <td width="150">
                                <span>
                                    <asp:DropDownList ID="cboLocDistSrch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboLocDistSrch_SelectedIndexChanged"
                                        ValidationGroup="ChkData" Width="150px">
                                    </asp:DropDownList>
                                </span>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="55">
                                Thana:
                            </td>
                            <td width="150">
                                <asp:DropDownList ID="cboLocThanaSrch" runat="server" ValidationGroup="ChkData" Width="150px"
                                    OnSelectedIndexChanged="cboLocThanaSrch_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="60">
                                Location:
                            </td>
                            <td width="350">
                                <span>
                                    <asp:TextBox ID="txtSearchLoc" runat="server" CssClass="search textAlignCenter" Width="350px"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchLoc" runat="server" BehaviorID="AutoCompleteSrchLoc"
                                        CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                        CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                        ServiceMethod="DelLocSrchList" ServicePath="~/Module/Transport/Forms/wsAutoCompleteTransport.asmx"
                                        ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearchLoc">
                                    </cc1:AutoCompleteExtender>
                                </span>
                            </td>
                            <td>
                                <span>
                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" onkeypress="return clickButton(event,'btnSearch')"
                                        Text="Search" ValidationGroup="btnSearch" />
                                </span>&nbsp; <span>
                                    <asp:Button ID="btnClearSrch" runat="server" OnClick="btnClearSrch_Click" Text="Clear"
                                        Visible="False" Width="60px" />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td width="55">
                                &nbsp;
                            </td>
                            <td width="150">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="55">
                                &nbsp;
                            </td>
                            <td width="150">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="60">
                                &nbsp;
                            </td>
                            <td width="350">
                                <span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSearchLoc"
                                        ErrorMessage="Enter Search Text" Font-Bold="False" Font-Size="10pt" ForeColor="Red"
                                        ValidationGroup="btnSearch"></asp:RequiredFieldValidator>
                                </span>
                            </td>
                            <td>
                                <asp:Button ID="btnExport" runat="server" Enabled="False" OnClick="btnExport_Click"
                                    Text="Export to Excel" />
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div align="center" style="background-color: #86AEAE">
                    <asp:GridView ID="gvLocMas" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" EmptyDataText="No Data  Found."
                        Font-Size="8pt" AllowPaging="True" PageSize="50" AllowSorting="True" OnRowDataBound="gvLocMas_RowDataBound"
                        OnPageIndexChanging="gvLocMas_PageIndexChanging" OnSelectedIndexChanged="gvLocMas_SelectedIndexChanged"
                        OnSorting="gvLocMas_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="Loc Ref. No">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocRefNo" runat="server" Text='<%# Bind("TrTr_Loc_Ref_No") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="TrTr_Loc_Name" HeaderText="Location Name" SortExpression="TrTr_Loc_Name">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="District" SortExpression="TrTr_Loc_Dist">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocDist" runat="server" Text='<%# GetDistName(Eval("TrTr_Loc_Dist").ToString())%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Thana" SortExpression="TrTr_Loc_Thana">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocThana" runat="server" Text='<%# GetThanaName(Eval("TrTr_Loc_Thana").ToString())%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="TrTr_Loc_Distance_Km" HeaderText="Distance (km)" SortExpression="TrTr_Loc_Distance_Km">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TrTr_Loc_Ext_Data1" HeaderText="Rent" SortExpression="TrTr_Loc_Ext_Data1">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TrTr_Loc_Status" HeaderText="Status" SortExpression="TrTr_Loc_Status">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
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
                        <br />
                        <asp:HiddenField ID="hfEditStatus" runat="server" Value="N" />
                        <asp:HiddenField ID="hfRefNo" runat="server" Value="0" />
                    </span>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboLocDist" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClearSrch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvLocMas" EventName="PageIndexChanging" />
            <asp:AsyncPostBackTrigger ControlID="gvLocMas" EventName="RowDataBound" />
            <asp:AsyncPostBackTrigger ControlID="gvLocMas" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:AsyncPostBackTrigger ControlID="cboLocDistSrch" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboLocThanaSrch" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
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
