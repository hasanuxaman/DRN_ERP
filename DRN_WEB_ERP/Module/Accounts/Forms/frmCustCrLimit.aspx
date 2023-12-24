<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmCustCrLimit.aspx.cs" Inherits="DRN_WEB_ERP.Module.Accounts.Forms.frmCustCrLimit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Credit Limit Setup</div>
    <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnSearch">
        <div align="center" style="background-color: #86AEAE">
            <span>
                <br />
                Search Customer:</span> <span>
                    <asp:TextBox ID="txtSearch" runat="server" Width="550px" CssClass="search textAlignCenter"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                        CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                        DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCustomer"
                        ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
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
                </span>
            <div align="center" style="background-color: #86AEAE">
                <table style="width: 100%; font-size: small;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 55px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 101px">
                            Existing Cr. Limit
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td style="width: 92px">
                            <asp:TextBox ID="txtCrLimit" runat="server" Enabled="False" ForeColor="Blue" BorderStyle="Dashed"
                                BorderWidth="1px" CssClass="textAlignCenter"></asp:TextBox>
                        </td>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 88px">
                            New Cr. Limir
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left" style="width: 127px">
                            <asp:TextBox ID="txtNewCrLimit" runat="server" CssClass="textAlignCenter" Enabled="False"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtNewCrLimit_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtNewCrLimit"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td align="center" style="width: 147px">
                            <span>
                                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" onkeypress="return clickButton(event,'btnSearch')"
                                    Text="Update" ValidationGroup="Save" Width="90px" Enabled="False" />
                            </span>
                        </td>
                        <td style="width: 141px">
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
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 55px">
                            &nbsp;
                        </td>
                        <td style="width: 101px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td style="width: 92px">
                            &nbsp;
                        </td>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 88px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td style="width: 127px">
                            <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtNewCrLimit"
                                ErrorMessage="Enter Valid Amount" Font-Size="8pt" ForeColor="Red" Operator="DataTypeCheck"
                                Type="Currency" ValidationGroup="Save"></asp:CompareValidator>
                        </td>
                        <td style="width: 147px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtNewCrLimit"
                                ErrorMessage="Enter New Credit Limit" Font-Size="8pt" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 141px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:GridView ID="gvCustCrLimit" runat="server" AutoGenerateColumns="False" CellPadding="4"
                Font-Size="8pt" ForeColor="#333333">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="SL#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Par_Adr_Prev_Cr_Limit" DataFormatString="{0:F2}" HeaderText="Existing Limit" />
                    <asp:BoundField DataField="Par_Adr_Cr_Limit" DataFormatString="{0:F2}" HeaderText="New Cr. Limit" />
                    <asp:BoundField DataField="Par_Adr_Cr_Limit_Ent_Date" HeaderText="Approve Date" 
                        DataFormatString="{0:d}" />
                    <asp:BoundField DataField="Par_Adr_Cr_Limit_App_By" HeaderText="Approved By" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <br />
        </div>
    </asp:Panel>
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
