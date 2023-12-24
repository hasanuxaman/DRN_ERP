<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmGLMaster.aspx.cs" Inherits="DRN_WEB_ERP.Module.Accounts.Forms.frmGLMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        General Ledger Master</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="border: 1px solid #CCCCCC; background-color: #9E9AF5">
                <br />
                <table style="width: 100%; font-family: verdana; font-size: small;">
                    <tr>
                        <td style="width: 16px">
                            &nbsp;
                        </td>
                        <td style="width: 53px">
                            Group 1
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td style="width: 81px">
                            <asp:DropDownList ID="ddlFirstGrp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFirstGrp_SelectedIndexChanged"
                                Width="125px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 23px">
                            &nbsp;
                        </td>
                        <td style="width: 54px">
                            Group 2
                        </td>
                        <td>
                            :
                        </td>
                        <td style="width: 64px">
                            <asp:DropDownList ID="ddlSecondGrp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSecondGrp_SelectedIndexChanged"
                                Width="125px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 28px">
                            &nbsp;
                        </td>
                        <td style="width: 56px">
                            Group 3
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td colspan="4">
                            <asp:DropDownList ID="ddlThirdGrp" runat="server" Width="125px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 15px">
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
                        <td style="width: 16px">
                            &nbsp;
                        </td>
                        <td style="width: 53px">
                            Code
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td style="width: 81px">
                            <asp:TextBox ID="txtGLCode" runat="server" BackColor="White" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" Font-Bold="True" Width="125px"></asp:TextBox>
                        </td>
                        <td style="width: 23px">
                            &nbsp;
                        </td>
                        <td style="width: 54px">
                            Type
                        </td>
                        <td>
                            :
                        </td>
                        <td style="width: 64px">
                            <asp:DropDownList ID="ddlCoaType" runat="server" Width="125px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 28px">
                            &nbsp;
                        </td>
                        <td style="width: 56px">
                            Name
                        </td>
                        <td width="2px">
                            :
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtGLName" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Font-Bold="False" Width="370px"></asp:TextBox>
                        </td>
                        <td style="width: 15px">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" ValidationGroup="btnSave"
                                Width="60px" />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="60px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 16px">
                            &nbsp;
                        </td>
                        <td style="width: 53px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 81px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlFirstGrp"
                                ErrorMessage="*Select First Group" Font-Size="8pt" ForeColor="Red" InitialValue="0"
                                ValidationGroup="btnSave"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtGLCode"
                                ErrorMessage="*Enter GL Code" Font-Size="8pt" ForeColor="Red" ValidationGroup="btnSave"></asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 23px">
                            &nbsp;
                        </td>
                        <td style="width: 54px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="width: 64px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlCoaType"
                                ErrorMessage="*Select Type" Font-Size="8pt" ForeColor="Red" InitialValue="0"
                                ValidationGroup="btnSave"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlSecondGrp"
                                ErrorMessage="*Select Second Group" Font-Size="8pt" ForeColor="Red" InitialValue="0"
                                ValidationGroup="btnSave"></asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 28px">
                            &nbsp;
                        </td>
                        <td style="width: 56px">
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 163px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtGLName"
                                ErrorMessage="*Enter GL Name" Font-Size="8pt" ForeColor="Red" ValidationGroup="btnSave"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlThirdGrp"
                                ErrorMessage="*Select Third Group" Font-Size="8pt" ForeColor="Red" InitialValue="0"
                                ValidationGroup="btnSave"></asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 22px">
                            &nbsp;
                        </td>
                        <td style="width: 149px">
                            &nbsp;
                        </td>
                        <td width="2px">
                            &nbsp;
                        </td>
                        <td style="width: 15px">
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
            </div>
            <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnSearch">
                <div align="center" style="border: 1px solid #CCCCCC; background-color: #0099CC">
                    <asp:Panel ID="Panel1" runat="server" GroupingText="Filter">
                        <div align="center" style="background-color: #CCCC00">
                            <table style="width: 100%; font-size: small; font-family: verdana;">
                                <tr>
                                    <td style="width: 125px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 119px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 550px">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSearch"
                                            ErrorMessage="Enter Search Text" Font-Bold="False" Font-Size="10pt" ForeColor="Red"
                                            ValidationGroup="btnSearch"></asp:RequiredFieldValidator>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 125px">
                                        &nbsp;
                                    </td>
                                    <td align="left" style="width: 119px">
                                        Search Ledger:
                                    </td>
                                    <td align="left" style="width: 550px">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="search textAlignCenter" Width="550px"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteSrchAcc"
                                            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                            DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCoa" ServicePath="~/Module/Accounts/Forms/wsAutoComAcc.asmx"
                                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearch">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" onkeypress="return clickButton(event,'btnSearch')"
                                            Text="Search" ValidationGroup="btnSearch" Width="60px" />
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%; font-size: small; font-family: verdana;">
                                <tr>
                                    <td style="width: 38px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 50px">
                                        &nbsp;
                                    </td>
                                    <td width="2">
                                        &nbsp;
                                    </td>
                                    <td align="left" width="126">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="width: 55px">
                                        &nbsp;
                                    </td>
                                    <td width="2">
                                        &nbsp;
                                    </td>
                                    <td align="left" width="126">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="width: 55px">
                                        &nbsp;
                                    </td>
                                    <td width="2">
                                        &nbsp;
                                    </td>
                                    <td align="left" width="126">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="width: 50px">
                                        &nbsp;
                                    </td>
                                    <td width="2">
                                        &nbsp;
                                    </td>
                                    <td align="left" width="126">
                                        &nbsp;
                                    </td>
                                    <td align="center">
                                        &nbsp;
                                    </td>
                                    <td align="center">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 38px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 50px">
                                        Group1
                                    </td>
                                    <td width="2">
                                        :
                                    </td>
                                    <td align="left" width="126">
                                        <asp:DropDownList ID="ddlFirstGrpSrch" runat="server" AutoPostBack="True" Width="125px"
                                            OnSelectedIndexChanged="ddlFirstGrpSrch_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="width: 55px">
                                        Group 2
                                    </td>
                                    <td width="2">
                                        :
                                    </td>
                                    <td align="left" width="126">
                                        <asp:DropDownList ID="ddlSecondGrpSrch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSecondGrpSrch_SelectedIndexChanged"
                                            Width="125px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="width: 55px">
                                        Group 3
                                    </td>
                                    <td width="2">
                                        :
                                    </td>
                                    <td align="left" width="126">
                                        <asp:DropDownList ID="ddlThirdGrpSrch" runat="server" Width="125px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlThirdGrpSrch_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="width: 50px">
                                        Type
                                    </td>
                                    <td width="2">
                                        :
                                    </td>
                                    <td align="left" width="126">
                                        <asp:DropDownList ID="ddlCoaTypeSrch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCoaTypeSrch_SelectedIndexChanged"
                                            Width="125px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center">
                                        &nbsp;
                                    </td>
                                    <td align="center">
                                        &nbsp;<asp:Button ID="btnClearSrch" runat="server" Enabled="False" OnClick="btnClearSrch_Click"
                                            Text="Clear" Width="60px" />
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 38px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 50px">
                                        &nbsp;
                                    </td>
                                    <td width="2">
                                        &nbsp;
                                    </td>
                                    <td align="left" width="126">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="width: 55px">
                                        &nbsp;
                                    </td>
                                    <td width="2">
                                        &nbsp;
                                    </td>
                                    <td align="left" width="126">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="width: 55px">
                                        &nbsp;
                                    </td>
                                    <td width="2">
                                        &nbsp;
                                    </td>
                                    <td align="left" width="126">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="width: 50px">
                                        &nbsp;
                                    </td>
                                    <td width="2">
                                        &nbsp;
                                    </td>
                                    <td align="left" width="126">
                                        &nbsp;
                                    </td>
                                    <td align="center">
                                        &nbsp;
                                    </td>
                                    <td align="center">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <div>
                        <span>
                            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" onkeypress="return clickButton(event,'btnSearch')"
                                Text="Print Group List" Width="110px" />
                            <asp:Button ID="btnExport" runat="server" onkeypress="return clickButton(event,'btnSearch')"
                                Text="Export To Excel" Width="110px" onclick="btnExport_Click" />
                        </span>
                        <asp:Button ID="btnGlGrpTree" runat="server" OnClick="btnGlGrpTree_Click" onkeypress="return clickButton(event,'btnSearch')"
                            Text="Show Group Tree" Width="120px" />
                    </div>
                    <asp:GridView ID="gvGLMas" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" Font-Size="8pt" OnSelectedIndexChanged="gvGLMas_SelectedIndexChanged"
                        PageSize="50" AllowSorting="True" OnPageIndexChanging="gvGLMas_PageIndexChanging"
                        OnRowDataBound="gvGLMas_RowDataBound" OnSorting="gvGLMas_Sorting">
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="FirstGrp" HeaderText="First Group" />
                            <asp:BoundField DataField="SndGrp" HeaderText="Second Group" />
                            <asp:BoundField DataField="ThirdGrp" HeaderText="Third Group" />
                            <asp:TemplateField HeaderText="GL Code" SortExpression="Gl_Coa_Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblCoaCode" runat="server" Text='<%# Bind("Gl_Coa_Code") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Gl_Coa_Name" HeaderText="GL Name" SortExpression="Gl_Coa_Name">
                                <ItemStyle HorizontalAlign="Left" Width="350px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Type" SortExpression="Gl_Coa_Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblGlType" runat="server" Text='<%# GetGlType(Eval("Gl_Coa_Type").ToString())%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="60px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <SortedAscendingCellStyle BackColor="#F4F4FD" />
                        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                        <SortedDescendingCellStyle BackColor="#D8D8F0" />
                        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>
                    <span>
                        <asp:HiddenField ID="hfEditStatus" runat="server" Value="N" />
                        <asp:HiddenField ID="hfRefNo" runat="server" Value="0" />
                    </span>
                    <asp:Button ID="btnUpdateGl" runat="server" OnClick="btnUpdateGl_Click" Text="Update GL"
                        Visible="False" />
                    <br />
                    <asp:Button ID="btnUpdtTallyAllGlGrp" runat="server" OnClick="btnUpdtTallyAllGlGrp_Click"
                        Text="Update Tally GL Group" Visible="False" />
                    <br />
                    <asp:Button ID="btnUpdtGLCoaGrp" runat="server" OnClick="btnUpdtGLCoaGrp_Click" Text="Update GL COA Group"
                        Visible="False" />
                    <br />
                    <asp:Button ID="btnUpdtGLGrpCode" runat="server" OnClick="btnUpdtGLGrpCode_Click"
                        Text="Update GL Group Code" Visible="False" />
                    <br />
                    <asp:Button ID="btnUpdtGLOverheadGrpCode" runat="server" OnClick="btnUpdtGLOverheadGrpCode_Click"
                        Text="Update GL Overhead Group Code" Visible="False" />
                    <br />
                    <asp:Button ID="btnUpdtUnassignedGLGrpCode" runat="server" OnClick="btnUpdtUnassignedGLGrpCode_Click"
                        Text="Update Unassigned GL Group Code" Visible="False" />
                    <br />
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClearSrch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlCoaTypeSrch" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvGLMas" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvGLMas" EventName="RowDataBound" />
            <asp:AsyncPostBackTrigger ControlID="gvGLMas" EventName="PageIndexChanging" />
            <asp:AsyncPostBackTrigger ControlID="ddlFirstGrp" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSecondGrp" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlFirstGrpSrch" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSecondGrpSrch" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlThirdGrpSrch" EventName="SelectedIndexChanged" />
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
