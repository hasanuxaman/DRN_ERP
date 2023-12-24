<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmSupplier.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmSupplier" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Supplier Entry</div>
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
                                Supplier Ref No
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 300px" align="left">
                                <asp:TextBox ID="txtSupRefNo" runat="server" BackColor="#CCCCFF" BorderStyle="Solid"
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
                                Supplier Name
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 300px" align="left">
                                <asp:TextBox ID="txtSupName" runat="server" CssClass="capitalize" Width="350px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtSupName"
                                    ErrorMessage="Enter Supplier Name" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td style="width: 126px" align="left">
                                Type
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 300px" align="left">
                                <asp:DropDownList ID="cboSupType" runat="server" ValidationGroup="ChkData" Width="353px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cboSupType"
                                    ErrorMessage="Select Supplier Type" ForeColor="Red" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td style="width: 126px" align="left">
                                Address
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 300px" rowspan="3" valign="top" align="left">
                                <asp:TextBox ID="txtSupAdr" runat="server" Height="60px" TextMode="MultiLine" Width="348px"></asp:TextBox>
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
                                &nbsp;
                            </td>
                            <td style="width: 3px">
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
                                &nbsp;
                            </td>
                            <td style="width: 3px">
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
                                Contact Person (CP)
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 300px" align="left">
                                <asp:TextBox ID="txtSupCp" runat="server" CssClass="capitalize" Width="350px"></asp:TextBox>
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
                                CP Designation
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 300px" align="left">
                                <asp:TextBox ID="txtSupCpDesig" runat="server" Width="350px"></asp:TextBox>
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
                                Cell No
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 300px" align="left">
                                <asp:TextBox ID="txtSupCell" runat="server" Width="350px"></asp:TextBox>
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
                                Phone No
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 300px" align="left">
                                <asp:TextBox ID="txtSupPhone" runat="server" Width="350px"></asp:TextBox>
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
                                Fax No
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 300px" align="left">
                                <asp:TextBox ID="txtSupFax" runat="server" Width="350px"></asp:TextBox>
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
                                E-Mail
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 300px" align="left">
                                <asp:TextBox ID="txtSupEmail" runat="server" Width="350px"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 126px">
                                Acc GL Code
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td style="width: 300px" align="left">
                                <asp:TextBox ID="txtSupAccGlCode" runat="server" Enabled="False" Width="350px"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
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
                                <asp:RadioButtonList ID="optListSupStatus" runat="server" RepeatDirection="Horizontal">
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
                        Search Supplier:</span> <span>
                            <asp:TextBox ID="txtSearch" runat="server" Width="350px" CssClass="search textAlignCenter"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                                CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchSupplier"
                                ServicePath="~/Module/Procurement/Forms/wsAutoComProc.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
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
                            <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export To Excel" />
                        </span>
                </div>
                <div align="center" style="background-color: #86AEAE">
                    <asp:GridView ID="gvSup" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" EmptyDataText="No Data  Found."
                        Font-Size="8pt" AllowPaging="True" PageSize="50" AllowSorting="True" OnRowDataBound="gvSup_RowDataBound"
                        OnPageIndexChanging="gvSup_PageIndexChanging" OnSelectedIndexChanged="gvSup_SelectedIndexChanged"
                        OnSorting="gvSup_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="Sup. Ref.">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfCustRef" runat="server" Value='<%# Bind("Par_Adr_Ref") %>' />
                                    <asp:Label ID="lblCustRefNo" runat="server" Text='<%# Bind("Par_Adr_Ref_No") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Par_Adr_Name" HeaderText="Name" SortExpression="Par_Adr_Name">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Adr_Addr" HeaderText="Address">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Adr_Cont_Per" HeaderText="Contact Person">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Adr_Cell_No" HeaderText="Cell No">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Adr_Tel_No" HeaderText="Phone No">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Adr_Fax_No" HeaderText="Fax No">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Adr_Email_Id" HeaderText="Email">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Adr_Status" HeaderText="Status" />
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
                        <asp:Button ID="btnDeleteDupplicate" runat="server" Text="Delete Dupplicate" OnClick="btnDeleteDupplicate_Click"
                            Visible="False" />
                        <asp:Button ID="btnImpSup" runat="server" OnClick="btnImpSup_Click" Text="Import Supplier"
                            Visible="False" />
                        <asp:HiddenField ID="HiddenField1" runat="server" Value="N" />
                        <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                    </span>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClearSrch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvSup" EventName="RowDataBound" />
            <asp:AsyncPostBackTrigger ControlID="gvSup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvSup" EventName="PageIndexChanging" />
            <asp:AsyncPostBackTrigger ControlID="gvSup" EventName="Sorting" />
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
