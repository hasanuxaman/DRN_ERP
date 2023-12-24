<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmVehicleMaster.aspx.cs" Inherits="DRN_WEB_ERP.Module.Transport.Forms.frmVehicleMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" class="titleframe">
        Vehicle Details</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnSearch" BackColor="#CCFFFF">
                <div align="center">
                    <br />
                    <span>Search Vehicle:</span> <span>
                        <asp:TextBox ID="txtSearch" runat="server" Width="550px" CssClass="search textAlignCenter"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="VslSrchList" ServicePath="~/Module/Transport/Forms/wsAutoCompleteTransport.asmx"
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearch">
                        </cc1:AutoCompleteExtender>
                        <asp:Button ID="btnSearch" runat="server" onkeypress="return clickButton(event,'btnSearch')"
                            Text="Search" ValidationGroup="btnSearch" OnClick="btnSearch_Click" Width="90px" />
                        <asp:Button ID="btnClearSrch" runat="server" Text="Clear" Visible="False" Width="90px"
                            OnClick="btnClearSrch_Click" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSearch"
                            ErrorMessage="Enter Search Text" Font-Bold="False" ForeColor="Red" ValidationGroup="btnSearch"
                            Font-Size="10pt"></asp:RequiredFieldValidator>
                        <br />
                        <asp:HiddenField ID="hfVslRegNo" runat="server" Value="0" />
                    </span>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #CCFFFF">
                <br />
                <asp:Panel ID="PnlPicSig" runat="server">
                    <div>
                        <table style="font-weight: bold; font-size: 12px; background-color: #F3EBDA; border-collapse: collapse;">
                            <tr>
                                <td align="center" colspan="2">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" rowspan="2" style="width: 118px">
                                    <asp:HyperLink ID="hlVslPic" runat="server" Target="_blank" Height="100px" Width="115px">
                                        <asp:Image ID="imgVslPic" runat="server" Height="100px" Width="115px" ImageUrl="~/Image/NoImage.gif"
                                            BorderColor="#CCCCCC" BorderStyle="Solid" /></asp:HyperLink>
                                </td>
                                <td style="width: 40px;">
                                    <asp:Button ID="btnUpdatePic" runat="server" OnClick="btnUpdatePic_Click" Text="Update Pic"
                                        ValidationGroup="picUpdate" Width="96px" />
                                </td>
                                <td rowspan="2">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="8pt" ForeColor="Red"
                                        ValidationGroup="picUpdate" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 40px;">
                                    <asp:Button ID="btnDeletePic" runat="server" OnClick="btnDeletePic_Click" Text="Delete Pic"
                                        Width="96px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:FileUpload ID="picUpload" runat="server" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <table style="font-weight: bold; font-size: 12px; background-color: #F3EBDA; border-collapse: collapse;">
                    <tr>
                        <td width="10">
                            &nbsp;
                        </td>
                        <td width="100">
                            &nbsp;
                        </td>
                        <td width="5">
                            &nbsp;
                        </td>
                        <td align="left" width="150">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 141px">
                            &nbsp;
                        </td>
                        <td width="5">
                            &nbsp;
                        </td>
                        <td width="150">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            &nbsp;
                        </td>
                        <td width="5">
                            &nbsp;
                        </td>
                        <td width="150">
                            &nbsp;
                        </td>
                        <td width="10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="10">
                            &nbsp;
                        </td>
                        <td width="100">
                            REG. NO
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtVslRegNo"
                                ErrorMessage="RequiredFieldValidator" ForeColor="Red" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td align="left" width="150">
                            <asp:TextBox ID="txtVslRegNo" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 141px">
                            Brand Name
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtBrandName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Model No
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                        </td>
                        <td width="10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="10">
                            &nbsp;
                        </td>
                        <td width="100">
                            Grade
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td align="left" width="150">
                            <asp:TextBox ID="txtGrade" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 141px">
                            Shape
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtShape" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Vehicle Type
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:DropDownList ID="ddlVslType" runat="server" Width="174px">
                            </asp:DropDownList>
                        </td>
                        <td width="10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="10">
                            &nbsp;
                        </td>
                        <td width="100">
                            Engine No
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td align="left" width="150">
                            <asp:TextBox ID="txtEngNo" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 141px">
                            Chassis No
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtChasisNo" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Engine Capacity(CC)
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtEngCC" runat="server"></asp:TextBox>
                        </td>
                        <td width="10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="10">
                            &nbsp;
                        </td>
                        <td width="100">
                            Battery Size
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td align="left" width="150">
                            <asp:TextBox ID="txtBaterySize" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 141px">
                            Tyre Size
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtTyreSize" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Gear Type
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtGearType" runat="server"></asp:TextBox>
                        </td>
                        <td width="10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="10">
                            &nbsp;
                        </td>
                        <td width="100">
                            Fuel Type
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td align="left" width="150">
                            <asp:TextBox ID="txtFuelType" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 141px">
                            Made By
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtMadeBy" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Color
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtColor" runat="server"></asp:TextBox>
                        </td>
                        <td width="10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="10">
                            &nbsp;
                        </td>
                        <td width="100">
                            MF Year
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td align="left" width="150">
                            <asp:TextBox ID="txtMfgYear" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 141px">
                            Fuel Consumption (Ltr)
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtMilageUsed" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Reg. Date
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtRegDate" runat="server" Width="145px"></asp:TextBox>
                            <asp:Image ID="imgRegDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                            <cc1:CalendarExtender ID="txtRegDate_CalendarExtender" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" PopupButtonID="imgRegDate" TargetControlID="txtRegDate">
                            </cc1:CalendarExtender>
                        </td>
                        <td width="10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="10">
                            &nbsp;
                        </td>
                        <td width="100">
                            Supplier Name
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td align="left" width="150">
                            <asp:TextBox ID="txtSuppName" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 141px">
                            Supplier Address&nbsp;
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtSuppAddr" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Pur. Amount
                        </td>
                        <td width="5">
                            :&nbsp;
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtPurAmt" runat="server" Width="145px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtPurAmt" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtPurAmt"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td width="10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="10">
                            &nbsp;
                        </td>
                        <td width="100">
                            Insurance No
                        </td>
                        <td width="5">
                            :&nbsp;
                        </td>
                        <td align="left" width="150">
                            <asp:TextBox ID="txtInsNo" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 141px">
                            Insurance Comp.&nbsp;
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtInsComp" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Insurance Date&nbsp;
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtInsDate" runat="server" Width="145px"></asp:TextBox>
                            <asp:Image ID="imgInsDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                            <cc1:CalendarExtender ID="txtInsDate_CalendarExtender" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" PopupButtonID="imgInsDate" TargetControlID="txtInsDate">
                            </cc1:CalendarExtender>
                        </td>
                        <td width="10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="10">
                            &nbsp;
                        </td>
                        <td width="100">
                            Leasing Comp.
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td align="left" width="150">
                            <asp:TextBox ID="txtLeaseComp" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 141px">
                            Leasing Comp. Addr.
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtLeaseCompAdr" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Lease/Agreement Dt.&nbsp;
                        </td>
                        <td width="5">
                            :
                        </td>
                        <td width="150">
                            <asp:TextBox ID="txtLeaseDate" runat="server" Width="145px"></asp:TextBox>
                            <asp:Image ID="imgLeaseDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                            <cc1:CalendarExtender ID="txtLeaseDate_CalendarExtender" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" PopupButtonID="imgLeaseDate" TargetControlID="txtLeaseDate">
                            </cc1:CalendarExtender>
                        </td>
                        <td width="10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="10">
                            &nbsp;
                        </td>
                        <td width="100">
                            &nbsp;
                        </td>
                        <td width="5">
                            &nbsp;
                        </td>
                        <td align="left" width="150">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 141px">
                            &nbsp;
                        </td>
                        <td width="5">
                            &nbsp;
                        </td>
                        <td width="150">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            &nbsp;
                        </td>
                        <td width="5">
                            &nbsp;
                        </td>
                        <td width="150">
                            &nbsp;
                        </td>
                        <td width="10">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <div>
                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="120px" OnClick="btnClear_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="120px" OnClick="btnSave_Click"
                        ValidationGroup="btnSave" />
                </div>
                <br />
                <br />
                <asp:Panel ID="pnlList" runat="server" GroupingText="Vehicle List" BackColor="#CCFFFF">
                    <br />
                    <asp:GridView ID="gvVslDet" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="10pt"
                        OnRowDataBound="gvVslDet_RowDataBound" OnSelectedIndexChanged="gvVslDet_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vsl. Reg. No">
                                <ItemTemplate>
                                    <asp:Label ID="lblVslRegNo" runat="server" Text='<%# Bind("Vsl_Mas_No") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vsl. Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblVslType" runat="server" Text='<%# GetVslType(Eval("Vsl_Mas_Ext_Type").ToString())%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Vsl_Mas_Brand" HeaderText="Brand" />
                            <asp:BoundField DataField="Vsl_Mas_Model" HeaderText="Model" />
                            <asp:BoundField DataField="Vsl_Mas_Engine_No" HeaderText="Engine No" />
                            <asp:BoundField DataField="Vsl_Mas_Chasis_No" HeaderText="Chasis No" />
                            <asp:BoundField DataField="Vsl_Mas_Engine_CC" HeaderText="Engine CC" />
                            <asp:BoundField DataField="Vsl_Mas_Ins_No" HeaderText="Insurance No" />
                            <asp:BoundField DataField="Vsl_Mas_MF_Year" HeaderText="Mfg Year" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                    <br />
                </asp:Panel>
                <br />
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClearSrch" EventName="Click" />
            <asp:PostBackTrigger ControlID="btnUpdatePic" />
            <asp:PostBackTrigger ControlID="btnDeletePic" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvVslDet" EventName="SelectedIndexChanged" />
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
