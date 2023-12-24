<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmVoucherRpt.aspx.cs" Inherits="DRN_WEB_ERP.Module.Accounts.Forms.frmVoucherRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Voucher Statement</div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center">
                <table style="width: 100%; font-size: 12px; background-color: #CCFFFF;">
                    <tr>
                        <td align="right" style="width: 50px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left" width="130">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 30px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td align="left" width="130">
                            &nbsp;
                        </td>
                        <td style="width: 50px">
                            &nbsp;
                        </td>
                        <td style="width: 6px">
                            &nbsp;
                        </td>
                        <td width="205">
                            &nbsp;
                        </td>
                        <td style="width: 80px" align="right">
                            &nbsp;
                        </td>
                        <td style="width: 2px">
                            &nbsp;
                        </td>
                        <td style="width: 15px">
                            &nbsp;
                        </td>
                        <td style="width: 99px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 50px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left" width="130">
                            &nbsp;
                        </td>
                        <td align="right" style="width: 30px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td align="left" width="130">
                            &nbsp;
                        </td>
                        <td style="width: 50px">
                            &nbsp;
                        </td>
                        <td style="width: 6px">
                            &nbsp;
                        </td>
                        <td width="205">
                            &nbsp;
                        </td>
                        <td style="width: 80px" align="right">
                            &nbsp;
                        </td>
                        <td style="width: 2px">
                            &nbsp;
                        </td>
                        <td style="width: 15px">
                            &nbsp;
                        </td>
                        <td style="width: 99px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 50px">
                            &nbsp;From
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left" width="130">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="inline" Width="80px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtDtFromCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                            <asp:Image ID="imgFromDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                        </td>
                        <td align="right" style="width: 30px">
                            To
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td align="left" width="130">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="inline" Width="80px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgToDate" TargetControlID="txtToDate">
                            </cc1:CalendarExtender>
                            <asp:Image ID="imgToDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png"
                                Width="16px" />
                        </td>
                        <td align="right" style="width: 50px">
                            &nbsp;Type
                        </td>
                        <td style="width: 6px">
                            :
                        </td>
                        <td width="205">
                            <asp:DropDownList ID="cboJvType" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 80px" align="right">
                            GL Code
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td style="width: 15px">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="search textAlignCenter" Width="300px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteSrchAcc"
                                CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCoa" ServicePath="~/Module/Accounts/Forms/wsAutoComAcc.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearch">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td style="width: 99px">
                            <asp:Button ID="btnShowRpt" runat="server" OnClick="btnShowRpt_Click" Text="Show"
                                Width="70px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50px">
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left" width="130">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFromDate"
                                Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFromDate"
                                Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                Type="Date" ValidationGroup="btnShow"></asp:CompareValidator>
                        </td>
                        <td style="width: 30px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td align="left" width="130">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                                Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtToDate"
                                Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                                Type="Date" ValidationGroup="btnShow"></asp:CompareValidator>
                        </td>
                        <td style="width: 50px">
                            &nbsp;
                        </td>
                        <td style="width: 6px">
                            &nbsp;
                        </td>
                        <td width="205">
                            &nbsp;
                        </td>
                        <td style="width: 80px" align="right">
                            &nbsp;
                        </td>
                        <td style="width: 2px">
                            &nbsp;
                        </td>
                        <td style="width: 15px">
                            &nbsp;
                        </td>
                        <td style="width: 99px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left" width="130">
                            &nbsp;
                        </td>
                        <td style="width: 30px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td align="left" width="130">
                            &nbsp;
                        </td>
                        <td style="width: 50px">
                            &nbsp;
                        </td>
                        <td style="width: 6px">
                            &nbsp;
                        </td>
                        <td width="205">
                            <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" 
                                Text="Export To Excel" Enabled="False" />
                        </td>
                        <td style="width: 80px" align="right">
                            &nbsp;
                        </td>
                        <td style="width: 2px">
                            &nbsp;
                        </td>
                        <td style="width: 15px">
                            &nbsp;
                        </td>
                        <td style="width: 99px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center">
                <asp:GridView ID="gvPayDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvPayDet_RowDataBound" AllowPaging="True"
                    OnPageIndexChanging="gvPayDet_PageIndexChanging" PageSize="50">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="Trn_Ac_Code" HeaderText="GL Code" Visible="False">
                            <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Tran. Ref. No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfJrnType" runat="server" Value='<%# Bind("Trn_Jrn_Type") %>' />
                                <asp:HiddenField ID="hfGlCode" runat="server" Value='<%# Bind("Trn_Ac_Code") %>' />
                                <asp:Label ID="lblTrnRefNo" runat="server" Text='<%# Bind("Trn_Ref_No") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Trn_DATE" DataFormatString="{0:d}" HeaderText="Tran. Date">
                            <ItemStyle Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Trn_Line_No" HeaderText="Ln#"></asp:BoundField>
                        <asp:BoundField DataField="Trn_Ac_Desc" HeaderText="GL Description">
                            <ItemStyle Width="300px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Trn_Entry_Date" DataFormatString="{0:d}" HeaderText="Entry Date">
                            <ItemStyle Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Trn_Narration" HeaderText="Narration">
                            <ItemStyle Width="300px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Type">
                            <ItemTemplate>
                                <asp:Label ID="lblTrnDrCrType" runat="server" Text='<%# Bind("Trn_Trn_type") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Trn_Amount" HeaderText="Amount" DataFormatString="{0:n}">
                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                        </asp:BoundField>
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
                        <asp:TemplateField HeaderText="Print">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkJvPrint" Text="Print" runat="server" OnClick="lnkJvPrint_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerSettings Position="TopAndBottom" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
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
