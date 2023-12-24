<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" CodeBehind="frmSupplierPay.aspx.cs"
    Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmSupplierPay" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Supplier Payment Entry</div>
    <script type="text/javascript" src="../../../jScript/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=txtPayAmt]").val("0");
        });

        $("[id*=txtPayAmt]").live("change", function () {
            if (isNaN(parseInt($(this).val()))) {
                $(this).val('0');
            } else {
                $(this).val(parseInt($(this).val()).toString());
            }
        });

        $("[id*=txtPayAmt]").live("keyup", function () {
            if (!jQuery.trim($(this).val()) == '') {
                if (!isNaN(parseFloat($(this).val()))) {
                    var row = $(this).closest("tr");
                    $("[id*=lblTotal]", row).html(parseFloat($(this).val()));

                    var paytk = parseFloat($(this).val());
                    //alert(paytk.toString());

                    var duetk = parseFloat($("[id*=lblDueAmt]", row).html());
                    //alert(duetk.toString());

                    if (paytk > duetk) {
                        alert("You are not allowed to pay more than" + duetk.toString());
                        $("[id*=lblTotal]", row).html('0');
                    }
                }
            }
            else {
                //               $(this).val('0');
                var row = $(this).closest("tr");
                $("[id*=lblTotal]", row).html('0');
            }

            var grandTotal = 0;
            $("[id*=lblTotal]").each(function () {
                grandTotal = grandTotal + parseFloat($(this).html());
            });

            if (grandTotal > 0) {
                $("[id*=txtvamnt]").val(grandTotal.toString());
            }
            else {
                $("[id*=txtvamnt]").val('0');
            }
        });
    </script>
    <asp:UpdatePanel ID="update" runat="server">
        <ContentTemplate>
            <div align="center" style="background-color: #CC66FF">
                <br />
                Search Supplier:
                <asp:TextBox ID="txtSearchSupplier" runat="server" CssClass="search textAlignCenter"
                    Width="550px"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderSrchSup" runat="server"
                    BehaviorID="AutoCompleteSrchSup" CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                    CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                    ServiceMethod="GetSrchSupplier" ServicePath="~/Module/Procurement/Forms/wsAutoComProc.asmx"
                    ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearchSupplier">
                </ajaxToolkit:AutoCompleteExtender>
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                    ValidationGroup="btnSearch" Width="60px" />
                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="60px" />
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSearchSupplier"
                    ErrorMessage="Enter Supplier Name First" Font-Size="10pt" ForeColor="Red" ValidationGroup="btnSearch"></asp:RequiredFieldValidator>
                <asp:Label ID="lblPartyBal" runat="server" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                <br />
            </div>
            <div align="center" style="background-color: #9E9AF5">
                <br />
                <asp:Label ID="lbltot" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
                <br />
                <asp:GridView ID="gdDueBillList" runat="server" BackColor="White" BorderColor="#6B7EBF"
                    BorderStyle="Solid" BorderWidth="10px" CellPadding="4" ForeColor="#333333" PageSize="100"
                    SkinID="GridView" Style="border-color: #e6e6fa; border-width: 1px; text-align: center;"
                    Width="99%" Font-Size="10pt" AutoGenerateColumns="False" OnRowDataBound="gdDueBillList_RowDataBound">
                    <FooterStyle BackColor="#6B7EBF" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#6B7EBF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle Font-Bold="True" />
                    <HeaderStyle BackColor="#6B7EBF" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfBillRef" runat="server" Value='<%# Bind("Bill_Hdr_Ref") %>' />
                                <asp:Label ID="lblBillNo" runat="server" Text='<%# Bind("Bill_Hdr_Ref_No") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="160px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Bill_Hdr_Date" DataFormatString="{0:d}" HeaderText="Bill Date" />
                        <asp:TemplateField HeaderText="PO Ref. No">
                            <ItemTemplate>
                                <asp:TreeView ID="tvPoList" runat="server" NodeIndent="15" ShowLines="True">
                                    <HoverNodeStyle Font-Underline="False" />
                                    <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                        NodeSpacing="0px" VerticalPadding="2px" />
                                    <ParentNodeStyle Font-Bold="False" />
                                    <SelectedNodeStyle Font-Underline="False" />
                                </asp:TreeView>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MRR No">
                            <ItemTemplate>
                                <%--<asp:Label ID="lblBillMrrNo" runat="server"></asp:Label>--%>
                                <asp:TreeView ID="tvMrrList" runat="server" NodeIndent="15" ShowLines="True">
                                    <HoverNodeStyle Font-Underline="False" />
                                    <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                        NodeSpacing="0px" VerticalPadding="2px" />
                                    <ParentNodeStyle Font-Bold="False" />
                                    <SelectedNodeStyle Font-Underline="False" />
                                </asp:TreeView>
                            </ItemTemplate>
                            <ItemStyle Width="140px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Bill_Hdr_Tot_Amount" HeaderText="Bill Amount" DataFormatString="{0:0.00}">
                            <ItemStyle HorizontalAlign="Right" Width="85px" BackColor="#00CC99" Font-Bold="True" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Bill_Hdr_Adj_Amount" HeaderText="Adj Amount" DataFormatString="{0:0.00}">
                            <ItemStyle HorizontalAlign="Right" Width="85px" BackColor="#00CC99" Font-Bold="True" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Bill_Hdr_Net_Amount" HeaderText="Net Amount" DataFormatString="{0:0.00}">
                            <ItemStyle HorizontalAlign="Right" Width="90px" BackColor="#BFFFEF" Font-Bold="True" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Bill_Hdr_Pay_Amount" HeaderText="Paid Amount" DataFormatString="{0:0.00}">
                            <ItemStyle HorizontalAlign="Right" Width="100px" BackColor="#CC66FF" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Due Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblDueAmt" runat="server" Text='<%# Bind("Bill_Hdr_Due_Amount", "{0:0.00}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#FF3399" Width="100px" HorizontalAlign="Right" Font-Bold="True" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pay Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text="0" Font-Size="1"></asp:Label>
                                <asp:TextBox ID="txtPayAmt" runat="server" CssClass="textAlignCenter" Width="90px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="txtPayAmt_FilteredTextBoxExtender" runat="server"
                                    FilterType="Custom, Numbers" TargetControlID="txtPayAmt" ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </ItemTemplate>
                            <ItemStyle Width="90px" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Button ID="btnUpdate" runat="server" Text="Submit Payment" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnHoldBill" runat="server" Text="Hold" OnClick="btnHoldBill_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="Lavender" Font-Size="8pt" Font-Strikeout="False" />
                    <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" Font-Underline="False" />
                    <RowStyle Font-Size="8pt" />
                </asp:GridView>
                <br />
            </div>
            <div style="background-color: #00CCFF">
                <table id="tbl_pay" runat="server" style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 174px">
                            &nbsp;
                        </td>
                        <td style="width: 185px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 363px">
                            &nbsp;
                        </td>
                        <td style="width: 309px">
                            &nbsp;
                        </td>
                        <td style="width: 138px">
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
                        <td style="width: 174px">
                            &nbsp;
                        </td>
                        <td style="width: 185px">
                            DATE
                        </td>
                        <td>
                            :
                        </td>
                        <td style="width: 363px">
                            <asp:TextBox ID="txtPayDate" runat="server" CssClass="textAlignCenter" Enabled="False"
                                Font-Bold="True" ForeColor="#0066FF" Width="182px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtPayDate_CalendarExtender" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" PopupButtonID="imgPayDt" TargetControlID="txtPayDate">
                            </ajaxToolkit:CalendarExtender>
                            <asp:Image ID="imgPayDt" runat="server" ImageUrl="~/Image/calendar.png" />
                        </td>
                        <td style="width: 309px">
                            &nbsp;
                        </td>
                        <td style="width: 138px">
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
                        <td style="width: 174px">
                            &nbsp;
                        </td>
                        <td style="width: 185px">
                            PAY TYPE
                        </td>
                        <td>
                            :</td>
                        <td style="width: 363px">
                            <asp:DropDownList ID="ddlPayMode" runat="server" Width="182px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged">
                                <asp:ListItem>Cheque</asp:ListItem>
                                <asp:ListItem>Cash</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 309px">
                            &nbsp;
                        </td>
                        <td style="width: 138px">
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
                        <td style="width: 174px">
                            &nbsp;
                        </td>
                        <td style="width: 185px">
                            <asp:Label ID="lblPayType" runat="server" Text="BANK"></asp:Label>
                        </td>
                        <td>
                            :</td>
                        <td style="width: 363px">
                            <asp:TextBox ID="txtCrAcc" runat="server" CssClass="search" Font-Bold="False" 
                                Width="364px"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteSrchAcc"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetSrchCoaByPayBank" ServicePath="~/Module/Accounts/Forms/wsAutoComAcc.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtCrAcc">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                        <td style="width: 309px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                ControlToValidate="txtCrAcc" ErrorMessage="RequiredFieldValidator" 
                                Font-Size="8pt" ForeColor="Red" ValidationGroup="btnUpdtPay">Enter Ledger Account First</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtCrAcc"
                                ErrorMessage="CustomValidator" Font-Size="8pt" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"
                                ValidationGroup="btnUpdtPay">Invalid Ledger Account</asp:CustomValidator>
                        </td>
                        <td style="width: 138px">
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
                        <td style="width: 174px">
                            &nbsp;
                        </td>
                        <td style="width: 185px">
                            CHQ NO / DOC. REF
                        </td>
                        <td>
                            :</td>
                        <td style="width: 363px">
                            <asp:TextBox ID="txtChqNo" runat="server" Width="182px"></asp:TextBox>
                        </td>
                        <td style="width: 309px">
                            &nbsp;
                        </td>
                        <td style="width: 138px">
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
                        <td style="width: 174px">
                            &nbsp;
                        </td>
                        <td style="width: 185px">
                            PAY AMOUNT
                        </td>
                        <td>
                            :</td>
                        <td style="width: 363px">
                            <asp:TextBox ID="txtvamnt" runat="server" CssClass="txtbox" Enabled="False" Width="182px">0</asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="txtvamnt_FilteredTextBoxExtender" runat="server"
                                FilterType="Custom, Numbers" TargetControlID="txtvamnt" ValidChars=".">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td style="width: 309px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtvamnt"
                                ErrorMessage="RequiredFieldValidator" Font-Size="8pt" ForeColor="Red" InitialValue="0"
                                ValidationGroup="btnUpdtPay">Enter Valid Amount</asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 138px">
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
                        <td style="width: 174px">
                            &nbsp;
                        </td>
                        <td style="width: 185px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 363px">
                            <asp:Button ID="btnUpdtPayment" runat="server" CssClass="btn2" OnClick="btnUpdtPayment_Click"
                                Text="Update Payment" Width="138px" ValidationGroup="btnUpdtPay" />
                        </td>
                        <td style="width: 309px">
                            &nbsp;
                        </td>
                        <td style="width: 138px">
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
                        <td style="width: 174px">
                            &nbsp;
                        </td>
                        <td style="width: 185px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 363px">
                            &nbsp;
                        </td>
                        <td style="width: 309px">
                            &nbsp;
                        </td>
                        <td style="width: 138px">
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
                        <td style="width: 174px">
                            &nbsp;
                        </td>
                        <td style="width: 185px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 363px">
                            &nbsp;
                        </td>
                        <td style="width: 309px">
                            &nbsp;
                        </td>
                        <td style="width: 138px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gdDueBillList" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="gdDueBillList" EventName="RowDataBound" />
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
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderMsg" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnMsgOk" OkControlID="btnMsgOk" PopupControlID="pnlMsg" TargetControlID="hfHidden"
        DropShadow="true">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
