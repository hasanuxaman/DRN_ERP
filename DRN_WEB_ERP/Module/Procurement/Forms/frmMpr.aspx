<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmMpr.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmMpr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Purchase Requisition</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #CC66FF">
                <br />
                Year:<asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
                    Width="100px">
                </asp:DropDownList>
                &nbsp;Month:<asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"
                    Width="100px">
                </asp:DropDownList>
                &nbsp; Req. Ref. No:
                <asp:DropDownList ID="ddlMprList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMprList_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                <br />
            </div>
            <table style="width: 100%; background-color: #CCFFFF; font-size: small;">
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 300px">
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
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px">
                        Req. Ref. No
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td align="left" style="width: 300px">
                        <asp:TextBox ID="txtPoReqRef" runat="server" Width="350px" CssClass="textAlignCenter"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtPoReqRef"
                            ErrorMessage="* Enter Requisition Ref. No" ForeColor="Red" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px">
                        Date
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td align="left" style="width: 300px">
                        <asp:TextBox ID="txtPoReqDt" runat="server" Width="350px" Enabled="false" CssClass="textAlignCenter"
                            Font-Bold="True"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px">
                        MPR Ref. No
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td align="left" style="width: 300px">
                        <asp:TextBox ID="txtPoRefNo" runat="server" BackColor="#CCCCFF" BorderStyle="Solid"
                            CssClass="textAlignCenter" Enabled="False" Font-Bold="True" Width="352px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px">
                        Item Type
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td align="left" style="width: 300px">
                        <asp:DropDownList ID="cboPoItmType" runat="server" Width="353px" OnSelectedIndexChanged="cboPoItmType_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
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
                        <asp:TextBox ID="txtPoItemName" runat="server" AutoPostBack="True" CssClass="search textAlignCenter"
                            OnTextChanged="txtPoItemName_TextChanged" Width="335px"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchItem" runat="server" BehaviorID="AutoCompleteSrch"
                            CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                            ServiceMethod="GetSrchFilteredItem" ServicePath="~/Module/Inventory/Forms/wsAutoCompInv.asmx"
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtPoItemName">
                        </cc1:AutoCompleteExtender>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPoItemName"
                            ErrorMessage="* Enter Item Name" ForeColor="Red" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtPoItemName"
                            ErrorMessage="* Enter Valid Item" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"
                            ValidationGroup="btnAdd">*</asp:CustomValidator>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px">
                        Unit
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td align="left" style="width: 300px">
                        <asp:DropDownList ID="cboPoItmUom" runat="server" Width="353px" Enabled="False">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="cboPoItmUom"
                            ErrorMessage="* Enter Valid Unit" ForeColor="Red" InitialValue="0" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px">
                        Quantity
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td align="left" style="width: 300px">
                        <asp:TextBox ID="txtPoQty" runat="server" Width="350px" CssClass="textAlignCenter"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="txtPoQty_FilteredTextBoxExtender" runat="server"
                            Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtPoQty"
                            ValidChars=".">
                        </cc1:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPoQty"
                            ErrorMessage="* Enter Quantity" ForeColor="Red" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px">
                        Specification
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td align="left" style="width: 300px">
                        <asp:TextBox ID="txtPoSpec" runat="server" Width="350px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px">
                        Brand
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td align="left" style="width: 300px">
                        <asp:TextBox ID="txtPoBrand" runat="server" Width="350px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px">
                        Origin
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td align="left" style="width: 300px">
                        <asp:TextBox ID="txtPoOrigin" runat="server" Width="350px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px">
                        Packing
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td align="left" style="width: 300px">
                        <asp:TextBox ID="txtPoPacking" runat="server" Width="350px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px">
                        Exp. Require Date
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td align="left" style="width: 300px">
                        <asp:TextBox ID="txtPoExpDt" runat="server" Width="350px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPoExpDt"
                            ErrorMessage="* Enter Valid Date" ForeColor="Red" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>
                        (dd/mm/yyyy)
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px">
                        Location of Use
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td align="left" style="width: 300px">
                        <asp:DropDownList ID="cboPoLoc" runat="server" ValidationGroup="btnAdd" Width="353px">
                            <asp:ListItem Value="1">Factory</asp:ListItem>
                            <asp:ListItem Value="2">Walsow Tower</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px">
                        Store
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td align="left" style="width: 300px">
                        <asp:DropDownList ID="ddlMprStore" runat="server" ValidationGroup="ChkData" Width="353px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlMprStore"
                            ErrorMessage="* Select Store" ForeColor="Red" ValidationGroup="btnAdd" InitialValue="0">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px" valign="top">
                        Remarks
                    </td>
                    <td style="width: 3px" valign="top">
                        :
                    </td>
                    <td align="left" style="width: 300px">
                        <asp:TextBox ID="txtPoRem" runat="server" Height="60px" TextMode="MultiLine" Width="348px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px">
                        Requisition By
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td align="left" style="width: 300px">
                        <asp:TextBox ID="txtPoReqBy" runat="server" CssClass="capitalize" Enabled="False"
                            Width="350px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 206px">
                        &nbsp;
                    </td>
                    <td style="width: 32px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 126px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 300px">
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
            <div align="center" style="background-color: #CCFFFF">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                    Font-Size="8pt" ForeColor="Red" ValidationGroup="btnAdd" />
                <br />
                <asp:Button ID="btnClear" runat="server" Text="Clear All" Width="75px" OnClick="btnClear_Click" />
                <asp:Button ID="btnAddPoDet" runat="server" OnClick="btnAddPoDet_Click" Text="Add"
                    ValidationGroup="btnAdd" Width="75px" />
                <br />
                <br />
            </div>
            <div align="center" style="background-color: #CCFFFF">
                <asp:GridView ID="gvPoReqDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333" OnRowDeleting="gvPoReqDet_RowDeleting" OnRowDataBound="gvPoReqDet_RowDataBound"
                    OnSelectedIndexChanged="gvPoReqDet_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("REQ_ITEM_REF") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate>
                                <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("REQ_ITEM_NAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Specification">
                            <ItemTemplate>
                                <asp:Label ID="lblSpec" runat="server" Text='<%# Bind("REQ_SPEC") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfItemUnit" runat="server" Value='<%# Bind("REQ_ITEM_UOM_REF") %>' />
                                <asp:Label ID="lblItemUnit" runat="server" Text='<%# Bind("REQ_ITEM_UOM") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Store">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfStore" runat="server" Value='<%# Bind("REQ_STORE_REF") %>' />
                                <asp:Label ID="lblStore" runat="server" Text='<%# Bind("REQ_STORE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Brand">
                            <ItemTemplate>
                                <asp:Label ID="lblBrand" runat="server" Text='<%# Bind("REQ_BRAND") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Origin">
                            <ItemTemplate>
                                <asp:Label ID="lblOrigin" runat="server" Text='<%# Bind("REQ_ORIGIN") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Packing">
                            <ItemTemplate>
                                <asp:Label ID="lblPacking" runat="server" Text='<%# Bind("REQ_PACKING") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exp.Req. Date">
                            <ItemTemplate>
                                <asp:Label ID="lblExpReqDt" runat="server" Text='<%# Bind("REQ_EXP_DT","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfLocation" runat="server" Value='<%# Bind("REQ_STORE_REF") %>' />
                                <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("REQ_LOC") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("REQ_REM") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfReqRef" runat="server" Value='<%# Bind("REQ_REF") %>' />
                                <asp:Label ID="lblPoReqQty" runat="server" Text='<%# Bind("REQ_QTY", "{0:c}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#FFFF66" HorizontalAlign="Center" Width="90px" />
                        </asp:TemplateField>
                        <asp:CommandField DeleteText="Remove" ShowDeleteButton="True">
                            <ItemStyle ForeColor="Red" />
                        </asp:CommandField>
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Wrap="False"
                        Font-Size="9pt" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
                <br />
                <br />
                <asp:Button ID="btnHold" runat="server" OnClick="btnHold_Click" Text="Save" ValidationGroup="btnSave"
                    Width="120px" Enabled="False" />
                <asp:Button ID="btnPost" runat="server" OnClick="btnPost_Click" Text="Post" ValidationGroup="btnSave"
                    Width="120px" Enabled="False" />
                <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" Width="120px"
                    Enabled="False" />
                <asp:HiddenField ID="hfEditStatus" runat="server" Value="N" />
                <asp:HiddenField ID="hfRefNo" runat="server" Value="0" />
                <asp:HiddenField ID="hfItemEditStatus" runat="server" Value="N" />
                <asp:HiddenField ID="hfItemEditRefNo" runat="server" Value="0" />
                <br />
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboPoItmType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtPoItemName" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnAddPoDet" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnHold" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnPost" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvPoReqDet" EventName="RowDeleting" />
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
