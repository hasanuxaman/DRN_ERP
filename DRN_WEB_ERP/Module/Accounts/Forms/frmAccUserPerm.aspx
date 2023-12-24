<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmAccUserPerm.aspx.cs" Inherits="DRN_WEB_ERP.Module.Accounts.Forms.frmAccUserPerm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Accounts User Permission</div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlSampleDetDet" runat="server" CssClass="cpBody" Width="1050px">
                <div align="center">
                    <table style="border: 1px solid #C0C0C0; width: 100%;">
                        <tr>
                            <td align="left" class="style16" style="width: 79px">
                                &nbsp;
                            </td>
                            <td align="left" class="style10" style="width: 167px">
                                &nbsp;
                            </td>
                            <td align="left" class="style10" style="width: 130px">
                                Employee Name
                            </td>
                            <td align="center" width="9">
                                :
                            </td>
                            <td align="left" class="style15">
                                <asp:TextBox ID="txtEmpName" runat="server" MaxLength="150" Width="340px" AutoPostBack="True"
                                    OnTextChanged="txtEmpName_TextChanged"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderEmp" runat="server" BehaviorID="AutoCompleteSrchEmp"
                                    CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionSetCount="20" DelimiterCharacters="," Enabled="True" MinimumPrefixLength="1"
                                    UseContextKey="true" ServiceMethod="GetSrchEmp" ServicePath="~/Module/HRMS/Forms/wsAutoComHrms.asmx"
                                    ShowOnlyCurrentWordInCompletionListItem="True" TargetControlID="txtEmpName">
                                </cc1:AutoCompleteExtender>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left" class="style14">
                                <%--<cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="CustomValidator"
                                                Enabled="True" TargetControlID="RequiredFieldValidator2">
                                            </cc1:ValidatorCalloutExtender>--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style16" style="width: 79px">
                                &nbsp;
                            </td>
                            <td align="left" class="style10" style="width: 167px">
                                &nbsp;
                            </td>
                            <td align="left" class="style10" style="width: 130px">
                                Designation
                            </td>
                            <td align="center" width="9">
                                :
                            </td>
                            <td align="left" class="style15">
                                <asp:TextBox ID="txtDesignation" runat="server" MaxLength="150" Width="340px" Enabled="False"></asp:TextBox>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left" class="style14">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style16" style="width: 79px">
                                &nbsp;
                            </td>
                            <td align="left" class="style10" style="width: 167px">
                                &nbsp;
                            </td>
                            <td align="left" class="style10" style="width: 130px">
                                Department
                            </td>
                            <td align="center" width="9">
                                :
                            </td>
                            <td align="left" class="style15">
                                <asp:TextBox ID="txtDept" runat="server" MaxLength="150" Width="340px" Enabled="False"></asp:TextBox>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left" class="style14">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style16" style="width: 79px">
                                &nbsp;
                            </td>
                            <td align="left" class="style10" style="width: 167px">
                                &nbsp;
                            </td>
                            <td align="left" class="style10" style="width: 130px">
                                Can Edit Voucher ?
                            </td>
                            <td align="center" width="9">
                                :
                            </td>
                            <td align="left" class="style15">
                                <asp:RadioButton ID="optEditYes" runat="server" Checked="True" GroupName="optEditPerm"
                                    Text="Yes" />
                                <asp:RadioButton ID="optEditNo" runat="server" GroupName="optEditPerm" Text="No" />
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left" class="style14">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style16" style="width: 79px">
                                &nbsp;
                            </td>
                            <td align="left" class="style10" style="width: 167px">
                                &nbsp;
                            </td>
                            <td align="left" class="style10" style="width: 130px">
                                Status
                            </td>
                            <td align="center" width="9">
                                :
                            </td>
                            <td align="left" class="style15">
                                <asp:RadioButton ID="optActive" runat="server" Checked="True" GroupName="optSpStat"
                                    Text="Active" />
                                &nbsp;<asp:RadioButton ID="optInactive" runat="server" GroupName="optSpStat" Text="Inactive" />
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left" class="style14">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style16" style="width: 79px">
                                &nbsp;
                            </td>
                            <td align="left" class="style10" style="width: 167px">
                                &nbsp;
                            </td>
                            <td align="left" class="style10" style="width: 130px">
                                &nbsp;
                            </td>
                            <td align="center" width="9">
                                &nbsp;
                            </td>
                            <td align="left" class="style15">
                                &nbsp;
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left" class="style14">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style16" style="width: 79px">
                                &nbsp;
                            </td>
                            <td align="left" class="style10" style="width: 167px">
                                &nbsp;
                            </td>
                            <td align="left" class="style10" style="width: 130px">
                                &nbsp;
                            </td>
                            <td align="center" width="9">
                                &nbsp;
                            </td>
                            <td align="left" class="style15">
                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" ValidationGroup="btnSave"
                                    Width="100px" />
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left" class="style14">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div align="center">
                    <asp:GridView ID="gvAccUserPermList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#CCCCFF" BorderStyle="Solid" BorderWidth="1px"
                        CellPadding="3" Font-Size="8pt" OnPageIndexChanging="gvAccUserPermList_PageIndexChanging"
                        OnRowDataBound="gvAccUserPermList_RowDataBound" OnSelectedIndexChanged="gvAccUserPermList_SelectedIndexChanged"
                        PageSize="25">
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL #">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpRef" runat="server" Text='<%# Bind("Acc_Perm_Emp_Ref") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Acc_Perm_Emp_Name" HeaderText="Employee Name" />
                            <asp:BoundField DataField="Acc_Perm_Emp_Desig" HeaderText="Designaton" />
                            <asp:BoundField DataField="Acc_Perm_Emp_Dept" HeaderText="Department" />
                            <asp:BoundField DataField="Acc_Perm_Edit_Perm" HeaderText="Edit_Perm">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# GetStatus(int.Parse(Eval("Acc_Perm_Status").ToString()))%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>
                    <asp:HiddenField ID="hfUserPermEditRef" runat="server" Value="0" />
                </div>
                <br />
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtEmpName" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvAccUserPermList" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvAccUserPermList" EventName="PageIndexChanging" />
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
