<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmCustLedger.aspx.cs" Inherits="DRN_WEB_ERP.Module.Accounts.Forms.frmCustLedger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        GL Statement</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnSearch">
                <div align="center" style="background-color: #86AEAE">
                    <table style="width: 100%; font-size: 12px;">
                        <tr>
                            <td style="width: 169px">
                                &nbsp;
                            </td>
                            <td style="width: 185px">
                                &nbsp;
                            </td>
                            <td width="8">
                                &nbsp;
                            </td>
                            <td style="width: 144px">
                                &nbsp;
                            </td>
                            <td style="width: 64px">
                                &nbsp;
                            </td>
                            <td width="8">
                                &nbsp;
                            </td>
                            <td style="width: 131px">
                                &nbsp;
                            </td>
                            <td style="width: 273px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 169px">
                                <asp:Label ID="lblCloseBal1" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td align="right" style="width: 185px">
                                From date
                            </td>
                            <td width="8">
                                :
                            </td>
                            <td align="left" style="width: 144px">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="textAlignCenter" Width="100px"
                                    Enabled="false" BackColor="White" BorderStyle="None"></asp:TextBox>
                                <asp:Image ID="imgFromDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                <cc1:CalendarExtender ID="CalenderFromDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txtFromDate" PopupButtonID="imgFromDt">
                                </cc1:CalendarExtender>
                            </td>
                            <td align="right" style="width: 64px">
                                To Date
                            </td>
                            <td width="8">
                                :
                            </td>
                            <td align="left" style="width: 131px">
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="textAlignCenter" Width="100px"
                                    Enabled="false" BackColor="White" BorderStyle="None"></asp:TextBox>
                                <asp:Image ID="imgToDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txtToDate" PopupButtonID="imgToDt">
                                </cc1:CalendarExtender>
                            </td>
                            <td align="center" style="width: 273px">
                                <asp:Label ID="lblAegingBal" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 169px">
                                &nbsp;
                            </td>
                            <td style="width: 185px">
                                &nbsp;
                            </td>
                            <td width="8">
                                &nbsp;
                            </td>
                            <td style="width: 144px">
                                &nbsp;
                            </td>
                            <td style="width: 64px">
                                &nbsp;
                            </td>
                            <td width="8">
                                &nbsp;
                            </td>
                            <td style="width: 131px">
                                <asp:TextBox ID="txtDiff" runat="server" Visible="False"></asp:TextBox>
                                &nbsp;
                            </td>
                            <td style="width: 273px">
                                <asp:Button ID="btnUpdtAegingBal" runat="server" OnClick="btnUpdtAegingBal_Click"
                                    Text="Update Aeging Bal" Visible="False" />
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <span>Search GL Code:</span> <span>
                        <asp:TextBox ID="txtSearch" runat="server" Width="550px" CssClass="search textAlignCenter"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCoa" ServicePath="~/Module/Accounts/Forms/wsAutoComAcc.asmx"
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearch">
                        </cc1:AutoCompleteExtender>
                        <asp:Button ID="btnSearch" runat="server" onkeypress="return clickButton(event,'btnSearch')"
                            Text="View" ValidationGroup="btnSearch" OnClick="btnSearch_Click" 
                        Width="60px" />
                        <asp:Button ID="btnClearSrch" runat="server" Text="Clear" Width="60px" OnClick="btnClearSrch_Click"
                            Enabled="False" />
                        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print Preview"
                            ValidationGroup="btnSearch" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSearch"
                            ErrorMessage="Enter GL Code First." Font-Bold="False" ForeColor="Red" ValidationGroup="btnSearch"
                            Font-Size="10pt"></asp:RequiredFieldValidator>
                        <br />
                        <br />
                    </span>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlDet" runat="server">
        <div align="center" style="background-color: #9999FF">
            <%--<br />--%>
            <div style="background-color: #FF9999; width: 675px;" align="center">
                <asp:Label ID="lblOpenBal" runat="server" BorderStyle="None" ForeColor="White"></asp:Label>
            </div>
            <asp:GridView ID="gvCustLed" runat="server" AutoGenerateColumns="False" CellPadding="4"
                Font-Size="8pt" ForeColor="#333333" ShowFooter="True" OnRowDataBound="gvCustLed_RowDataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <%--<asp:TemplateField HeaderText="D/O No">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDoRef" runat="server" Text='<%# Bind("T_C1") %>' OnClick="lnkDoRef_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="Trn_DATE" DataFormatString="{0:d}" HeaderText="Date">
                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Voucher No">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfJrnType" runat="server" Value='<%# Bind("Trn_Jrn_Type") %>' />
                            <asp:Label ID="lblTrnRefNo" runat="server" Text='<%# Bind("Trn_Ref_No") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Narration">
                        <ItemTemplate>
                            <asp:Label ID="lblNarration" runat="server" Text='<%# Bind("Trn_Narration") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTot" runat="server" Text="Total "></asp:Label>
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="350px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Challan No" Visible="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkClnRef" runat="server" OnClick="lnkClnRef_Click" Text='<%# Bind("Trn_Dc_No") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ORD No" Visible="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkOrdRef" runat="server" Text='<%# Bind("Trn_GRN_No") %>' OnClick="lnkOrdRef_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Through">
                        <ItemTemplate>
                            <asp:Label ID="lblThrough" runat="server" Text='<%# Bind("Trn_Match") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Doc. Ref. No">
                        <ItemTemplate>
                            <asp:Label ID="lblDocRefNo" runat="server" Text='<%# Bind("Trn_Cheque_No") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Debit">
                        <ItemTemplate>
                            <asp:Label ID="lblDrAmt" runat="server" Text='<%# Bind("DebitAmount", "{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotDrAmt" runat="server"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit">
                        <ItemTemplate>
                            <asp:Label ID="lblCrAmt" runat="server" Text='<%# Bind("CreditAmount", "{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotCrAmt" runat="server"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkJvEdit" Text="Edit" runat="server" OnClick="lnkJvEdit_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="View">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkJvView" Text="Details" runat="server" OnClick="lnkJvView_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
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
            </asp:GridView>
            <div style="background-color: #FF9999; width: 675px;" align="center">
                <asp:Label ID="lblCloseBal" runat="server" BorderStyle="None" ForeColor="White" Font-Size="12pt"></asp:Label>
            </div>
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
