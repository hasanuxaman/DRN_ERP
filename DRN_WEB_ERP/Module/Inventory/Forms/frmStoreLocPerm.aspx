<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmStoreLocPerm.aspx.cs" Inherits="DRN_WEB_ERP.Module.Inventory.Forms.frmStoreLocPerm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Store Location Permission</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="Div1" align="center" style="overflow: auto; background-color: #66CCFF;"
                runat="server">
                <table style="border: 1px solid #C0C0C0; width: 100%; background-color: #66CCFF;
                    font-size: small;">
                    <tr>
                        <td align="left" class="style16" style="width: 79px">
                            &nbsp;
                        </td>
                        <td align="left" class="style10" style="width: 335px">
                            &nbsp;
                        </td>
                        <td align="left" class="style10" style="width: 130px">
                            Employee Name
                        </td>
                        <td align="center" width="9">
                            :
                        </td>
                        <td align="left" class="style15" style="width: 339px">
                            <asp:TextBox ID="txtEmpName" runat="server" MaxLength="150" Width="400px" 
                                AutoPostBack="True" ontextchanged="txtEmpName_TextChanged"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderEmp" runat="server" BehaviorID="AutoCompleteSrchEmp"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," Enabled="True" MinimumPrefixLength="1"
                                UseContextKey="true" ServiceMethod="GetSrchEmp" ServicePath="~/Module/HRMS/Forms/wsAutoComHrms.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="True" TargetControlID="txtEmpName">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td align="left">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmpName"
                                ErrorMessage="Enter Employee Name First." ForeColor="Red" ValidationGroup="btnSave"></asp:RequiredFieldValidator>
                        </td>
                        <td align="left" class="style14">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style16" style="width: 79px">
                            &nbsp;
                        </td>
                        <td align="left" class="style10" style="width: 335px">
                            &nbsp;
                        </td>
                        <td align="left" class="style10" style="width: 130px">
                            Designation
                        </td>
                        <td align="center" width="9">
                            :
                        </td>
                        <td align="left" class="style15" style="width: 339px">
                            <asp:TextBox ID="txtEmpDesig" runat="server" Width="400px" Enabled="False"></asp:TextBox>
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
                        <td align="left" class="style10" style="width: 335px">
                            &nbsp;
                        </td>
                        <td align="left" class="style10" style="width: 130px">
                            Store Location
                        </td>
                        <td align="center" width="9">
                            :
                        </td>
                        <td align="left" class="style15" style="width: 339px">
                            <asp:CheckBoxList ID="chkListStrLoc" runat="server">
                            </asp:CheckBoxList>
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
                        <td align="left" class="style10" style="width: 335px">
                            &nbsp;
                        </td>
                        <td align="left" class="style10" style="width: 130px">
                            Status
                        </td>
                        <td align="center" width="9">
                            :
                        </td>
                        <td align="left" class="style15" style="width: 339px">
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
                </table>
                <br />
                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" ValidationGroup="btnSave"
                    Width="100px" />
                <br />
            </div>
            <div align="center" style="background-color: #99CCFF">
                <br />
                <br />
                <asp:GridView ID="gvStrLocPer" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="10pt"
                    OnRowDataBound="gvStrLocPer_RowDataBound" OnSelectedIndexChanged="gvStrLocPer_SelectedIndexChanged">
                    <Columns>
                        <%--<asp:BoundField DataField="Str_Loc_Perm_Store_Code" HeaderText="Store Ref" />--%>
                        <%--<asp:BoundField DataField="Str_Loc_Perm_Store_Name" HeaderText="Store Name" />--%>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Emp_Ref">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpRef" runat="server" Text='<%# Bind("Str_Loc_Perm_Emp_Ref") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Str_Loc_Perm_Emp_Name" HeaderText="Emp_Name" />
                        <asp:BoundField DataField="Str_Loc_Perm_Emp_Desig" HeaderText="Designation" />
                        <asp:TemplateField HeaderText="Store Permission">
                            <ItemTemplate>
                                <asp:TreeView ID="tvStrLocPerm" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                                    ShowLines="True">
                                    <HoverNodeStyle Font-Underline="False" />
                                    <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                        NodeSpacing="0px" VerticalPadding="2px" />
                                    <ParentNodeStyle Font-Bold="False" />
                                    <SelectedNodeStyle Font-Underline="False" />
                                </asp:TreeView>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# GetStatus(int.Parse(Eval("Str_Loc_Perm_Status").ToString()))%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
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
                <br />
            </div>
        </ContentTemplate>
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
