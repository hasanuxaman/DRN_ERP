<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmMrr.aspx.cs" Inherits="DRN_WEB_ERP.Module.Inventory.Forms.frmMrr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="Javascript">
        function MrrBalQty(MrrQty, MrrBalQty) {
            var mrrQty = 0;
            if (document.getElementById(MrrQty).value != '') {
                mrrQty = parseFloat(document.getElementById(MrrQty).value);
            }
            //alert("Mrr Qty: " + mrrQty.toString());

            var mrrBalQty = 0;
            mrrBalQty = parseFloat(MrrBalQty);
            //alert("Mrr Bal Qty: " + mrrBalQty.toString());

            if (mrrQty > mrrBalQty) {
                document.getElementById(MrrQty).value = MrrBalQty;
                alert("You are not allowed to receive qty more than: " + mrrBalQty.toString());
            }
        }
    </script>
    <div align="center" style="background-color: #00FF99">
        Material Receiving Receipt (MRR)</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <asp:Panel ID="pnlMrrHdr" runat="server">
                    <div align="center" style="background-color: #CC66FF">
                        <br />
                        Search Pending PO:
                        <asp:TextBox ID="txtSrcPendPo" runat="server" CssClass="inline search" Width="580px"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchPo" runat="server" BehaviorID="AutoCompleteSrchPo"
                            CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                            ServiceMethod="GetSrchPendMrr" ServicePath="~/Module/Inventory/Forms/wsAutoCompInv.asmx"
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSrcPendPo">
                        </cc1:AutoCompleteExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCust0" runat="server" ControlToValidate="txtSrcPendPo"
                            ErrorMessage="Select PO First" ForeColor="Red" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtSrcPendPo"
                            ErrorMessage="Enter Valid PO" ForeColor="Red" OnServerValidate="CustomValidator2_ServerValidate"
                            ValidationGroup="btnSearch">*</asp:CustomValidator>
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" onkeypress="return clickButton(event,'btnSearch')"
                            Text="Search" ValidationGroup="btnSearch" />
                        <asp:Button ID="btnClearSrch" runat="server" OnClick="btnClearSrch_Click" Text="Clear"
                            Width="60px" />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" Font-Size="8pt" ForeColor="Red"
                            ValidationGroup="btnSearch" />
                        <br />
                        <br />
                    </div>
                    <div align="center" style="background-color: #6600CC; height: 30px;">
                        <table style="width: 100%;">
                            <tr>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="center" style="color: #FFFFFF" valign="middle">
                                    Supplier Name:
                                    <asp:Label ID="lblSup" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="background-color: #9E9AF5">
                        <table style="width: 100%; font-family: verdana; font-size: small;">
                            <tr>
                                <td style="width: 261px">
                                    &nbsp;
                                </td>
                                <td style="width: 240px">
                                    &nbsp;
                                </td>
                                <td style="width: 91px">
                                    &nbsp;
                                </td>
                                <td width="2px">
                                    &nbsp;
                                </td>
                                <td style="width: 179px">
                                    &nbsp;
                                </td>
                                <td style="width: 91px">
                                    &nbsp;
                                </td>
                                <td style="width: 110px">
                                    &nbsp;
                                </td>
                                <td width="2px">
                                    &nbsp;
                                </td>
                                <td style="width: 228px">
                                    &nbsp;
                                </td>
                                <td style="width: 207px">
                                    &nbsp;
                                </td>
                                <td style="width: 225px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 261px">
                                    &nbsp;
                                </td>
                                <td style="width: 240px">
                                    &nbsp;
                                </td>
                                <td style="width: 91px">
                                    MRR No
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 179px">
                                    <asp:TextBox ID="txtMrrNo" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        Enabled="False" Font-Bold="True" Font-Size="10pt" ForeColor="Blue" Width="170px"></asp:TextBox>
                                </td>
                                <td style="width: 91px">
                                    &nbsp;
                                </td>
                                <td style="width: 110px">
                                    MRR Date
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 228px">
                                    <asp:TextBox ID="txtMrrDate" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        Enabled="False" ForeColor="Blue" Width="170px"></asp:TextBox>
                                    <asp:Image ID="imgReqDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <cc1:CalendarExtender ID="CalenderDelDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgReqDt" TargetControlID="txtMrrDate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="width: 207px">
                                    &nbsp;
                                </td>
                                <td style="width: 225px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 261px">
                                    &nbsp;
                                </td>
                                <td style="width: 240px">
                                    &nbsp;
                                </td>
                                <td style="width: 91px">
                                    PO Ref
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 179px">
                                    <asp:TextBox ID="txtPoRef" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        Enabled="False" Font-Bold="True" Font-Size="10pt" ForeColor="#FF33CC" Width="170px"></asp:TextBox>
                                </td>
                                <td style="width: 91px">
                                    &nbsp;
                                </td>
                                <td style="width: 110px">
                                    Challan No
                                </td>
                                <td width="2px">
                                    :
                                </td>
                                <td style="width: 228px">
                                    <asp:TextBox ID="txtChlnNo" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                        Font-Bold="False" Font-Size="10pt" Width="170px"></asp:TextBox>
                                </td>
                                <td style="width: 207px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Challan No"
                                        Font-Size="8pt" ForeColor="Red" ValidationGroup="btnSave" ControlToValidate="txtChlnNo">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 225px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <div align="center">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="10pt" ForeColor="Red"
                                ValidationGroup="btnSave" DisplayMode="List" />
                            <br />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlMrrDet" runat="server">
                    <div align="center" style="background-color: #9E9AF5">
                        <br />
                        <asp:GridView ID="gvPoDet" runat="server" CellPadding="4" Font-Size="10pt" ForeColor="#333333"
                            AutoGenerateColumns="False" OnRowDataBound="gvPoDet_RowDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <%--<asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMrrRate" runat="server" Text='<%# Bind("PO_Det_Lin_Rat","{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#CC9900" Font-Bold="False" HorizontalAlign="Center" Width="90px" />
                                </asp:TemplateField>--%>
                                <%--<asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMrrAmt" runat="server" Text='<%# Bind("PO_Det_Lin_Amt","{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#99FFCC" HorizontalAlign="Right" Font-Bold="True" Width="100px" />
                                </asp:TemplateField>--%>
                                <%--<asp:CommandField ShowDeleteButton="True" DeleteText="Remove">
                                    <ItemStyle ForeColor="Red" />
                                </asp:CommandField>--%>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MPR Ref. No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMprRefNo" runat="server" Text='<%# Bind("PO_Det_Pr_Ref") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("PO_Det_Icode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Desc.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("PO_Det_Itm_Desc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemUnit" runat="server" Text='<%# Bind("PO_Det_Itm_Uom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Store">
                                    <ItemTemplate>
                                        <%--<asp:HiddenField ID="hfMrrStoreRef" runat="server" Value='<%# Bind("PO_Det_Str_Code") %>' />--%>
                                        <%--<asp:Label ID="lblMrrStore" runat="server" Text='<%# Bind("PO_Det_Str_Code") %>'></asp:Label>--%>
                                        <asp:DropDownList ID="ddlMrrStore" runat="server" Width="180px" DataSourceID="SqlDataSource1"
                                            SelectedValue='<%# Bind("PO_Det_Str_Code") %>' DataTextField="Str_Loc_Name" DataValueField="Str_Loc_Ref">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DRNConStr %>"
                                            SelectCommand="SELECT * FROM [tbl_InMa_Str_Loc]"></asp:SqlDataSource>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblMrrQty" runat="server" Text='<%# Bind("PO_Det_Lin_Qty", "{0:n}") %>'></asp:Label>--%>
                                        <asp:HiddenField ID="hfMrrBalQty" runat="server" Value='<%# Bind("PO_Det_Bal_Qty") %>' />
                                        <asp:TextBox ID="txtMrrQty" runat="server" CssClass="textAlignCenter" Width="120px"
                                            Text='<%# Bind("PO_Det_Bal_Qty") %>'></asp:TextBox>
                                        <asp:HiddenField ID="hfPoRate" runat="server" Value='<%# Bind("PO_Det_Lin_Rat") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" BackColor="#CC3399" Width="90px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkMrrItem" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Wrap="False" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                        </asp:GridView>
                        <br />
                        <asp:Button ID="btnHold" runat="server" Text="Save" Width="120px" ValidationGroup="btnSave"
                            Visible="False" />
                        <asp:Button ID="btnPost" runat="server" OnClick="btnPost_Click" Text="Post" Width="120px"
                            ValidationGroup="btnSave" Visible="False" />
                        <asp:HiddenField ID="hfEditStatus" runat="server" Value="N" />
                        <asp:HiddenField ID="hfRefNo" runat="server" Value="0" />
                        <br />
                        <br />
                    </div>
                </asp:Panel>
                <div align="center" style="background-color: #FFFF66">
                    <asp:Panel ID="Panel1" runat="server" Style="background-color: #FFFF66" GroupingText="Existing MRR">
                        <br />
                        Year:<asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
                            Width="100px">
                        </asp:DropDownList>
                        &nbsp;Month:<asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"
                            Width="100px">
                        </asp:DropDownList>
                        &nbsp;MRR Ref. No:<asp:Label ID="lblcount" runat="server" Text="(0)"></asp:Label>
                        &nbsp;<asp:DropDownList ID="ddlMrrList" runat="server" Width="400px" OnSelectedIndexChanged="ddlMrrList_SelectedIndexChanged"
                            AutoPostBack="True" CssClass="txtbox">
                        </asp:DropDownList>
                        &nbsp;<asp:Button ID="btnPrint" runat="server" Text="Print" Width="100px" OnClick="btnPrint_Click" />
                        <br />
                        <br />
                        <asp:GridView ID="gvMrrDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            Font-Size="10pt" ForeColor="#333333">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="MRR Ref. No" DataField="Trn_Hdr_Ref_No" />
                                <asp:BoundField HeaderText="MRR Date" DataField="Trn_Hdr_Date" DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="PO Ref. No" DataField="Trn_Hdr_Tran_Ref" />
                                <asp:BoundField HeaderText="Challan No" DataField="Trn_Hdr_Com1" />
                                <asp:BoundField HeaderText="MPR Ref No" DataField="Trn_Det_Bat_No" />
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCodeMrr" runat="server" Text='<%# Bind("Trn_Det_Icode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Desc.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemDescMrr" runat="server" Text='<%# Bind("Trn_Det_Itm_Desc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemUnitMrr" runat="server" Text='<%# Bind("Trn_Det_Itm_Uom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Store">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfMrrStoreRefMrr" runat="server" Value='<%# Bind("Trn_Det_Str_Code") %>' />
                                        <asp:Label ID="lblMrrStoreMrr" runat="server" Text='<%# Bind("Trn_Det_Str_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMrrQty" runat="server" Text='<%# Bind("Trn_Det_Lin_Qty", "{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#CC3399" HorizontalAlign="Center" Width="90px" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Wrap="False" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                        </asp:GridView>
                        <br />
                    </asp:Panel>
                    <br />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClearSrch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnHold" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnPost" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="cboYear" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboMonth" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlMrrList" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnPrint" EventName="Click" />
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
