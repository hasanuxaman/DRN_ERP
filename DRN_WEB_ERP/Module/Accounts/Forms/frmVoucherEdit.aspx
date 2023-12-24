<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmVoucherEdit.aspx.cs" Inherits="DRN_WEB_ERP.Module.Accounts.Forms.frmVoucherEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Voucher Edit</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnEdit">
                <div align="center" style="background-color: #86AEAE;">
                    <span>
                        <br />
                        <asp:Panel ID="pnlDet" runat="server">
                            <div align="center" style="background-color: #9999FF">
                                <span>
                                    <table style="width: 100%; font-size: 12px;">
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td align="right" width="90">
                                                Edit Option
                                            </td>
                                            <td width="2">
                                                :
                                            </td>
                                            <td align="left" colspan="5">
                                                <asp:RadioButtonList ID="optEdit" runat="server" AutoPostBack="True" Enabled="False"
                                                    OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Value="0">None</asp:ListItem>
                                                    <asp:ListItem Value="1">Date</asp:ListItem>
                                                    <asp:ListItem Value="2">GL Acc Code</asp:ListItem>
                                                    <asp:ListItem Value="3">Type</asp:ListItem>
                                                    <asp:ListItem Value="4">Amount</asp:ListItem>
                                                    <asp:ListItem Value="5">Narration</asp:ListItem>
                                                    <asp:ListItem Value="6">Through</asp:ListItem>
                                                    <asp:ListItem Value="7">Doc. Ref</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td width="10">
                                                &nbsp;
                                            </td>
                                            <td align="right" width="70">
                                                &nbsp;
                                            </td>
                                            <td width="2">
                                                &nbsp;
                                            </td>
                                            <td align="left" width="130">
                                                &nbsp;
                                            </td>
                                            <td align="left" width="15">
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
                                            <td align="right" width="90">
                                                &nbsp;
                                            </td>
                                            <td width="2">
                                                &nbsp;
                                            </td>
                                            <td align="left" width="520">
                                                &nbsp;
                                            </td>
                                            <td width="10">
                                            </td>
                                            <td align="right" width="40">
                                                &nbsp;
                                            </td>
                                            <td width="2">
                                                &nbsp;
                                            </td>
                                            <td align="left" width="70">
                                                &nbsp;
                                            </td>
                                            <td width="10">
                                                &nbsp;
                                            </td>
                                            <td align="right" width="70">
                                                &nbsp;
                                            </td>
                                            <td width="2">
                                                &nbsp;
                                            </td>
                                            <td align="left" width="130">
                                                &nbsp;
                                            </td>
                                            <td align="left" width="15">
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
                                            <td align="right" width="90">
                                                Voucher Ref. No
                                            </td>
                                            <td width="2">
                                                :
                                            </td>
                                            <td align="left" width="520">
                                                <span>
                                                    <asp:TextBox ID="txtTranRefNo" runat="server" CssClass="textAlignCenter" Enabled="False"
                                                        Font-Bold="True" ForeColor="#FF9900" Width="180px"></asp:TextBox>
                                                </span>
                                            </td>
                                            <td width="10">
                                                &nbsp;
                                            </td>
                                            <td align="right" width="40">
                                                CODE</td>
                                            <td width="2">
                                                :
                                            </td>
                                            <td align="left" width="70">
                                                <asp:Label ID="lblJvCode" runat="server"></asp:Label>
                                            </td>
                                            <td width="10">
                                                &nbsp;
                                            </td>
                                            <td align="right" width="70">
                                                Date
                                            </td>
                                            <td width="2">
                                                :
                                            </td>
                                            <td align="left" width="130">
                                                <span>
                                                    <asp:TextBox ID="txtTranDate" runat="server" CssClass="textAlignCenter" ForeColor="Blue"
                                                        OnTextChanged="txtTranDate_TextChanged" Width="110px" Enabled="False"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtTranDate_CalendarExtender" runat="server" Enabled="True"
                                                        Format="dd/MM/yyyy" PopupButtonID="imgRcvDt" TargetControlID="txtTranDate">
                                                    </cc1:CalendarExtender>
                                                    <asp:Image ID="imgRcvDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                                </span>
                                            </td>
                                            <td align="left" width="15">
                                                <span>
                                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtTranDate"
                                                        ErrorMessage="Invalid Date" Font-Size="8pt" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"
                                                        ValidationGroup="btnAdd">*</asp:CustomValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtTranDate"
                                                        ErrorMessage="Enter Date" Font-Size="8pt" ForeColor="Red" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>
                                                </span>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td align="right" width="90">
                                                GL Acc Code
                                            </td>
                                            <td width="2">
                                                :
                                            </td>
                                            <td align="left" width="520">
                                                <span>
                                                    <asp:TextBox ID="txtGlAcc" runat="server" CssClass="search" Font-Bold="False" Width="500px"
                                                        Enabled="False"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteSrchAcc"
                                                        CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                                        CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                                        ServiceMethod="GetSrchCoa" ServicePath="~/Module/Accounts/Forms/wsAutoComAcc.asmx"
                                                        ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtGlAcc">
                                                    </cc1:AutoCompleteExtender>
                                                </span>
                                            </td>
                                            <td width="10">
                                                <span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtGlAcc"
                                                        ErrorMessage="Enter GL Account" Font-Size="8pt" ForeColor="Red" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtGlAcc"
                                                        ErrorMessage="Invalid GL Account" Font-Size="8pt" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"
                                                        ValidationGroup="btnAdd">*</asp:CustomValidator>
                                                </span>
                                            </td>
                                            <td align="right" width="40">
                                                Type
                                            </td>
                                            <td width="2">
                                                :
                                            </td>
                                            <td align="left" width="70">
                                                <span>
                                                    <asp:DropDownList ID="ddlTranType" runat="server" Width="70px" Enabled="False">
                                                        <asp:ListItem Value="0">-----</asp:ListItem>
                                                        <asp:ListItem Value="D">Debit</asp:ListItem>
                                                        <asp:ListItem Value="C">Credit</asp:ListItem>
                                                    </asp:DropDownList>
                                                </span>
                                            </td>
                                            <td width="10">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlTranType"
                                                    ErrorMessage="Select Type" ForeColor="Red" InitialValue="0" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td align="right" width="70">
                                                Amount
                                            </td>
                                            <td width="2">
                                                :
                                            </td>
                                            <td align="left" width="130">
                                                <span>
                                                    <asp:TextBox ID="txtTranAmt" runat="server" CssClass="textAlignCenter" Width="130px"
                                                        Enabled="False"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="txtTranAmt_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtTranAmt"
                                                        ValidChars=".">
                                                    </cc1:FilteredTextBoxExtender>
                                                </span>
                                            </td>
                                            <td align="left" width="15">
                                                <span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtTranAmt"
                                                        ErrorMessage="Enter Amount" Font-Size="8pt" ForeColor="Red" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtTranAmt"
                                                        ErrorMessage="Invalid Amount" Font-Size="8pt" ForeColor="Red" Operator="DataTypeCheck"
                                                        Type="Currency" ValidationGroup="btnAdd">*</asp:CompareValidator>
                                                </span>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                &nbsp;
                                            </td>
                                            <td align="right" valign="top" width="90">
                                                Narration
                                            </td>
                                            <td valign="top" width="2">
                                                :
                                            </td>
                                            <td align="left" valign="top" width="520">
                                                <span>
                                                    <asp:TextBox ID="txtNarration" runat="server" Height="54px" MaxLength="250" TextMode="MultiLine"
                                                        Width="515px" Enabled="False"></asp:TextBox>
                                                </span>
                                            </td>
                                            <td valign="top" width="10">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtNarration"
                                                    ErrorMessage="Enter Narration" ForeColor="Red" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td align="right" valign="top" width="40">
                                                Through
                                            </td>
                                            <td valign="top" width="2">
                                                :
                                            </td>
                                            <td align="left" valign="top" width="70">
                                                <asp:DropDownList ID="ddlRcvMode" runat="server" AutoPostBack="True" Width="70px"
                                                    Enabled="False">
                                                    <asp:ListItem Value="1">Cash</asp:ListItem>
                                                    <asp:ListItem Value="2">Cheque</asp:ListItem>
                                                    <asp:ListItem Value="3">DD</asp:ListItem>
                                                    <asp:ListItem Value="4">TT</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td valign="top" width="10">
                                            </td>
                                            <td align="right" valign="top" width="70">
                                                Doc. Ref
                                            </td>
                                            <td valign="top" width="2">
                                                :
                                            </td>
                                            <td align="left" valign="top" width="130">
                                                <asp:TextBox ID="txtDocRef" runat="server" Width="130px" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td align="left" valign="top" width="15">
                                            </td>
                                            <td valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td align="right" width="90">
                                                &nbsp;<asp:Label ID="lblLineNo" runat="server"></asp:Label>
                                            </td>
                                            <td width="2">
                                                &nbsp;
                                            </td>
                                            <td align="left" width="520">
                                                &nbsp;
                                            </td>
                                            <td width="10">
                                                &nbsp;
                                            </td>
                                            <td align="right" width="40">
                                                &nbsp;
                                            </td>
                                            <td width="2">
                                                &nbsp;
                                            </td>
                                            <td align="left" width="70">
                                                &nbsp;
                                            </td>
                                            <td width="10">
                                                &nbsp;
                                            </td>
                                            <td align="right" width="70">
                                                &nbsp;
                                            </td>
                                            <td width="2">
                                                &nbsp;
                                            </td>
                                            <td align="left" width="130">
                                                &nbsp;
                                            </td>
                                            <td align="left" width="15">
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
                                            <td align="right" width="90">
                                                &nbsp;
                                            </td>
                                            <td width="2">
                                                &nbsp;
                                            </td>
                                            <td align="center" colspan="7">
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="8pt" ForeColor="Red"
                                                    ValidationGroup="btnAdd" Width="734px" />
                                            </td>
                                            <td width="2">
                                                &nbsp;
                                            </td>
                                            <td align="left" width="130">
                                                &nbsp;
                                            </td>
                                            <td align="left" width="15">
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
                                            <td align="right" width="90">
                                                &nbsp;
                                            </td>
                                            <td width="2">
                                                &nbsp;
                                            </td>
                                            <td align="left" width="520">
                                                &nbsp;
                                            </td>
                                            <td width="10">
                                                &nbsp;
                                            </td>
                                            <td align="right" width="40">
                                                &nbsp;
                                            </td>
                                            <td width="2">
                                                &nbsp;
                                            </td>
                                            <td align="left" width="70">
                                                &nbsp;
                                            </td>
                                            <td width="10">
                                                &nbsp;
                                            </td>
                                            <td align="right" width="70">
                                                &nbsp;
                                            </td>
                                            <td width="2">
                                                &nbsp;
                                            </td>
                                            <td align="left" width="130">
                                                &nbsp;
                                            </td>
                                            <td align="left" width="15">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </span>
                                <br />
                                <span>
                                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" ValidationGroup="btnAdd"
                                        Visible="False" Width="100px" />
                                    <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit" ValidationGroup="btnAdd"
                                        Width="100px" Enabled="False" />
                                </span>
                                <br />
                                <br />
                            </div>
                        </asp:Panel>
                    </span>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlVoucherDet" runat="server">
        <div align="center" style="border: 1px solid #CCCCCC; background-color: #009999">
            <br />
            <div>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" bgcolor="#3366FF">
                    <tr>
                        <td align="left" style="width: 33%;">
                            <asp:Label ID="Label4" runat="server" BorderStyle="None" ForeColor="White" Text="Debit Amount: "></asp:Label>
                            <asp:Label ID="lblTotDrAmt" runat="server" BorderStyle="None" ForeColor="White" Text="0.00"></asp:Label>
                        </td>
                        <td align="center" style="width: 33px">
                            <asp:Label ID="Label6" runat="server" BorderStyle="None" ForeColor="White" Text="Credit Amount: "></asp:Label>
                            <asp:Label ID="lblTotCrAmt" runat="server" BorderStyle="None" ForeColor="White" Text="0.00"></asp:Label>
                        </td>
                        <td align="right" style="width: 33px">
                            <asp:Label ID="Label8" runat="server" BorderStyle="None" ForeColor="White" Text="Balance: "></asp:Label>
                            <asp:Label ID="lblDrCrBal" runat="server" BorderStyle="None" ForeColor="White" Text="0.00"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:GridView ID="gvVoucherDet" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="8pt"
                PageSize="25" OnRowDeleting="gvVoucherDet_RowDeleting" OnSelectedIndexChanged="gvVoucherDet_SelectedIndexChanged"
                OnRowDataBound="gvVoucherDet_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="L#">
                        <ItemTemplate>
                            <asp:Label ID="lblSlNo" runat="server" Text='<%# Bind("SL_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="40px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GL Org. Code">
                        <ItemTemplate>
                            <asp:Label ID="lblOrgCoaCode" runat="server" Text='<%# Bind("ORG_COA_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GL Code">
                        <ItemTemplate>
                            <asp:Label ID="lblCoaCode" runat="server" Text='<%# Bind("COA_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GL Name">
                        <ItemTemplate>
                            <asp:Label ID="lblCoaName" runat="server" Text='<%# Bind("COA_NAME") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="250px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label ID="lblTrnDate" runat="server" Text='<%# Bind("TRAN_DATE", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Narration">
                        <ItemTemplate>
                            <asp:Label ID="lblNarration" runat="server" Text='<%# Bind("TRAN_NARRATION") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="450px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Through">
                        <ItemTemplate>
                            <asp:Label ID="lblThrough" runat="server" Text='<%# Bind("TRAN_THROUGH") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Doc. Ref.">
                        <ItemTemplate>
                            <asp:Label ID="lblCheque" runat="server" Text='<%# Bind("TRAN_CHQ_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Debit">
                        <ItemTemplate>
                            <asp:Label ID="lblDr" runat="server" Text='<%# Bind("TRAN_DEBIT", "{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit">
                        <ItemTemplate>
                            <asp:Label ID="lblCr" runat="server" Text='<%# Bind("TRAN_CREDIT", "{0:n}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150px" />
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" DeleteText="Remove" Visible="False">
                        <ItemStyle ForeColor="Red" />
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="Edit_Log">
                        <ItemTemplate>
                            <asp:Label ID="lblEditLog" runat="server" Text='<%# Bind("TRAN_EDIT_LOG") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#CCFF99" Font-Bold="True" ForeColor="Black" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
            <br />
            <asp:Button ID="btnPost" runat="server" OnClick="btnPost_Click" Text="Update Voucher"
                Enabled="False" />
            <br />
            <asp:HiddenField ID="hfEditDateStat" runat="server" Value="N" />
            <br />
            <div align="left" style="background-color: #FFCCFF">
                <asp:Panel ID="panel1" runat="server" GroupingText="Edit Log">
                    <asp:PlaceHolder ID="phcomments" runat="server"></asp:PlaceHolder>
                </asp:Panel>
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
        CancelControlID="btnMsgOk" DropShadow="true" OkControlID="btnMsgOk" PopupControlID="pnlMsg"
        TargetControlID="hfHidden">
    </cc1:ModalPopupExtender>
</asp:Content>
