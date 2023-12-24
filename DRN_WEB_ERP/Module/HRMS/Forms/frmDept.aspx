<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmDept.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmDept" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="border-style: groove; display: none; border-width: thin">
                Search Company:
                <asp:TextBox ID="txtSrchLoc" runat="server" CssClass="search" Width="350px"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderLoc" runat="server" BehaviorID="AutoCompleteSrchLoc"
                    CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                    DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchLoc" ServicePath="~/Module/HRMS/Forms/wsAutoComHrms.asmx"
                    ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSrchLoc">
                </cc1:AutoCompleteExtender>
                <asp:Button ID="btnSechLoc" runat="server" Text="Search" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" />
            </div>
            <div align="center" style="background-color: #00FF99">
                Create Department
            </div>
            <div align="center">
                <table style="width: 100%; background-color: #CCFFFF">
                    <tr align="left">
                        <td style="width: 147px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 86px" align="left">
                            Company
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 187px" align="left">
                            <asp:DropDownList ID="cboCompany" runat="server" Width="180px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboCompany_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select Company"
                                ForeColor="Red" InitialValue="0" ControlToValidate="cboCompany" ValidationGroup="SaveDept">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="left" style="width: 81px">
                            &nbsp;
                        </td>
                        <td style="width: 99px;" align="left">
                            Phone
                        </td>
                        <td style="width: 3px;" align="left">
                            :
                        </td>
                        <td style="width: 204px;" align="left">
                            <asp:TextBox runat="server" ID="txtDeptPhone" Width="180px"></asp:TextBox>
                        </td>
                        <td style="width: 122px;" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 147px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 86px" align="left">
                            Dept. Code
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 187px" align="left">
                            <asp:TextBox ID="txtDeptCode" runat="server" Width="180px" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDeptCode"
                                ErrorMessage="Enter Department Code" ForeColor="Red" ValidationGroup="SaveDept">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="left" style="width: 81px">
                            &nbsp;
                        </td>
                        <td style="width: 99px;" align="left">
                            Fax
                        </td>
                        <td style="width: 3px;" align="left">
                            :
                        </td>
                        <td style="width: 204px;" align="left">
                            <asp:TextBox runat="server" ID="txtDeptFax" Width="180px"></asp:TextBox>
                        </td>
                        <td style="width: 122px;" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 147px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 86px" align="left">
                            Name
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 187px" align="left">
                            <asp:TextBox ID="txtDeptName" runat="server" Width="180px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDeptName"
                                ErrorMessage="Enter Department Name" ForeColor="Red" ValidationGroup="SaveDept">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="left" style="width: 81px">
                            &nbsp;
                        </td>
                        <td style="width: 99px" align="left">
                            E-Mail
                        </td>
                        <td style="width: 3px" align="left">
                            :
                        </td>
                        <td style="width: 204px" align="left">
                            <asp:TextBox ID="txtDeptEmail" runat="server" Width="180px"></asp:TextBox>
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 147px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 86px" align="left">
                            Remarks
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 187px" align="left">
                            <asp:TextBox runat="server" ID="txtDeptRem" Width="180px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 81px">
                            &nbsp;
                        </td>
                        <td style="width: 99px" align="left">
                            Opening Date
                        </td>
                        <td style="width: 3px" align="left">
                            :
                        </td>
                        <td style="width: 204px" align="left">
                            <asp:TextBox runat="server" ID="txtDeptOpnDate" Width="180px"></asp:TextBox>
                            <asp:Image ID="imgDeptOpnDt" runat="server" ImageUrl="~/Image/calendar.png" />
                            <cc1:CalendarExtender ID="txtDeptCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                TargetControlID="txtDeptOpnDate" PopupButtonID="imgDeptOpnDt">
                            </cc1:CalendarExtender>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtDeptOpnDate"
                                ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck" Type="Date"
                                ValidationGroup="SaveDept">*</asp:CompareValidator>
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 147px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 86px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 187px" align="left">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 81px">
                            &nbsp;
                        </td>
                        <td style="width: 99px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 3px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 204px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 147px">
                            <asp:HiddenField ID="hfDeptRefNo" runat="server" Value="0" />
                        </td>
                        <td align="left" colspan="4">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="8pt" ForeColor="Red"
                                ValidationGroup="SaveDept" />
                        </td>
                        <td style="width: 99px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 3px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 204px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 147px">
                            &nbsp;
                        </td>
                        <td align="center" colspan="7">
                            &nbsp; &nbsp;<asp:Button ID="btnClearComp" runat="server" Text="Clear" Width="131px"
                                OnClick="btnClearComp_Click" />
                            &nbsp;<asp:Button ID="btnSaveCom" runat="server" Text="Save" ValidationGroup="SaveDept"
                                OnClick="btnSaveCom_Click" Width="131px" />
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="background-color: #CCFFFF">
                <br />
                <asp:GridView ID="gvDept" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvDept_RowDataBound" OnSelectedIndexChanged="gvDept_SelectedIndexChanged"
                    OnRowDeleting="gvDept_RowDeleting" AllowPaging="True" 
                    onpageindexchanging="gvDept_PageIndexChanging" PageSize="25">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="DeptRefNo" HeaderText="Ref. No" />
                        <asp:BoundField DataField="DeptCode" HeaderText="Code" />
                        <asp:BoundField DataField="DeptName" HeaderText="Name" />
                        <asp:TemplateField HeaderText="Company">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfCompRef" runat="server" Value='<%# Bind("LocRefNo") %>' />
                                <asp:Label ID="lblCompName" runat="server" Text='<%# GetCompanyName(Eval("LocRefNo").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DeptCPPhone" HeaderText="Phone" />
                        <asp:BoundField DataField="DeptCPFax" HeaderText="Fax" />
                        <asp:BoundField DataField="DeptCPEmail" HeaderText="E-Mail" />
                        <asp:BoundField DataField="DeptOpnDate" DataFormatString="{0:d}" HeaderText="Opening Date" />
                        <asp:BoundField DataField="DeptRem" HeaderText="Remarks" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    ImageUrl="~/Image/Delete.png" OnClientClick="return confirm('Do you want to delete?')"
                                    ToolTip="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="10pt" />
                    <PagerSettings Position="TopAndBottom" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboCompany" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvDept" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvDept" EventName="RowDeleting" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveCom" EventName="Click" />
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
