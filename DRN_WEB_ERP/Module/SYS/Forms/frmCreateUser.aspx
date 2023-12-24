<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmCreateUser.aspx.cs" Inherits="DRN_WEB_ERP.frmCreateUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        User Setup</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <style type="text/css">
                .ddlnomination
                {
                    text-align: center;
                }
                .upCase
                {
                    text-transform: uppercase;
                }
                .style1
                {
                    width: 70px;
                }
                .style2
                {
                    width: 7px;
                }
                .style3
                {
                    width: 61px;
                }
                .style5
                {
                    width: 102px;
                }
                .style7
                {
                    width: 30px;
                }
                .style9
                {
                    width: 181px;
                }
                .style11
                {
                }
                .style12
                {
                    width: 19px;
                }
                .style13
                {
                    width: 31px;
                }
                .style14
                {
                    width: 3px;
                }
            </style>
            <div align="center" style="background-color: #CC66FF">
            <br />
                <asp:Panel ID="pnlSearchBoxEmp" runat="server" DefaultButton="btnSearchEmp">
                    <div>
                        Search Employee:<asp:TextBox ID="txtSrchEmp" runat="server" CssClass="search" Width="400"
                            ForeColor="#0066FF"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteEmpSrch"
                            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchEmp" ServicePath="~/Module/SYS/Forms/wsAutoComSys.asmx"
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSrchEmp">
                        </cc1:AutoCompleteExtender>
                        &nbsp;<asp:Button ID="btnSearchEmp" runat="server" onkeypress="return clickButton(event,'txtSrchEmp')"
                            Text="Search" ValidationGroup="btnSearchEmp" Width="60px" OnClick="btnSearchEmp_Click" />
                        &nbsp;<asp:Button ID="btnClearEmp" runat="server" Text="Clear" Visible="False" Width="60px"
                            OnClick="btnClear_Click" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSrchEmp"
                            ErrorMessage="Enter Search Text" Font-Bold="False" ForeColor="#CC3300" ValidationGroup="btnSearchEmp"
                            Font-Size="12pt"></asp:RequiredFieldValidator>
                    </div>
                </asp:Panel>
                <br />
                <asp:Panel ID="pnlUserEntry" runat="server" DefaultButton="Save">
                    <div align="center">
                        <table id="tblUserInfo" runat="server" style="border: 2px solid #CCCCCC; width: 90%;
                            background-color: #FFCCFF; font-size: 10px;">
                            <tr>
                                <td align="left" class="style7">
                                    &nbsp;
                                </td>
                                <td align="left" class="style3">
                                    User Code
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td align="left" class="style9">
                                    <asp:TextBox ID="txtUserCode" runat="server" Width="200px" Style="text-transform: uppercase"
                                        MaxLength="10"></asp:TextBox>
                                </td>
                                <td align="left" class="style2">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter User Code"
                                        ValidationGroup="btnSave" ForeColor="Red" ControlToValidate="txtUserCode" Font-Bold="True">*</asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="User code already exists."
                                        ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="btnSave">*</asp:CustomValidator>
                                </td>
                                <td align="left" class="style12">
                                    &nbsp;
                                </td>
                                <td align="left" class="style1">
                                    Name
                                </td>
                                <td style="width: 2px" align="left">
                                    :
                                </td>
                                <td align="left" class="style5">
                                    <asp:TextBox ID="txtUserName" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td align="left" class="style13">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter User Name"
                                        ValidationGroup="btnSave" ForeColor="Red" ControlToValidate="txtUserName" Font-Bold="True">*</asp:RequiredFieldValidator>
                                </td>
                                <td align="left" class="style14">
                                    &nbsp;
                                </td>
                                <td align="center" class="style11" rowspan="7" valign="middle">
                                    &nbsp;
                                    <asp:HyperLink ID="hlEmpPic" Target="_blank" runat="server">
                                        <asp:Image ID="imgEmp" runat="server" Height="120px" Width="120px" ImageUrl="~/Image/NoImage.gif"
                                            BorderStyle="Ridge" BorderColor="White" BorderWidth="5" />
                                    </asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="style7">
                                    &nbsp;
                                </td>
                                <td align="left" class="style3">
                                    Company
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td align="left" class="style9">
                                    <asp:DropDownList ID="cboCompany" runat="server" DataSourceID="dsCompany" DataTextField="Company_Name"
                                        DataValueField="Company_Ref_No" Width="204px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" class="style2">
                                    &nbsp;
                                </td>
                                <td align="left" class="style12">
                                    &nbsp;
                                </td>
                                <td align="left" class="style1">
                                    Employee ID
                                </td>
                                <td style="width: 2px" align="left">
                                    :
                                </td>
                                <td align="left" class="style5">
                                    <asp:DropDownList ID="cboEmpId" runat="server" Width="304px" AutoPostBack="True"
                                        OnSelectedIndexChanged="cboEmpId_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" class="style13">
                                    &nbsp;
                                </td>
                                <td align="left" class="style14">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="style7">
                                    &nbsp;
                                </td>
                                <td align="left" class="style3">
                                    Designation
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td align="left" class="style9">
                                    <asp:TextBox ID="txtDesig" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td align="left" class="style2">
                                    &nbsp;
                                </td>
                                <td align="left" class="style12">
                                    &nbsp;
                                </td>
                                <td align="left" class="style1">
                                    Supervisor
                                </td>
                                <td style="width: 2px" align="left">
                                    :
                                </td>
                                <td align="left" class="style5">
                                    <asp:DropDownList ID="ddlSupervisor" runat="server" Width="304px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" class="style13">
                                    &nbsp;
                                </td>
                                <td align="left" class="style14">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="style7">
                                    &nbsp;
                                </td>
                                <td align="left" class="style3">
                                    Department
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td align="left" class="style9">
                                    <asp:TextBox ID="txtDept" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td align="left" class="style2">
                                    &nbsp;
                                </td>
                                <td align="left" class="style12">
                                    &nbsp;
                                </td>
                                <td align="left" class="style1">
                                    Contact No
                                </td>
                                <td style="width: 2px" align="left">
                                    :
                                </td>
                                <td align="left" class="style5">
                                    <asp:TextBox ID="txtContact" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td align="left" class="style13">
                                    &nbsp;
                                </td>
                                <td align="left" class="style14">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="style7">
                                    &nbsp;
                                </td>
                                <td align="left" class="style3">
                                    User Group
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td align="left" class="style9">
                                    <asp:DropDownList ID="ddlUserGroup" runat="server" DataSourceID="dsUserGroup" DataTextField="User_Grp_Name"
                                        DataValueField="User_Grp_Ref_No" Width="204px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" class="style2">
                                    &nbsp;
                                </td>
                                <td align="left" class="style12">
                                    &nbsp;
                                </td>
                                <td align="left" class="style1">
                                    Email
                                </td>
                                <td style="width: 2px" align="left">
                                    :
                                </td>
                                <td align="left" class="style5">
                                    <asp:TextBox ID="txtEmail" runat="server" Width="300px"></asp:TextBox>
                                </td>
                                <td align="left" class="style13">
                                    &nbsp;
                                </td>
                                <td align="left" class="style14">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="style7">
                                    &nbsp;
                                </td>
                                <td align="left" class="style3">
                                    User Level
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td align="left" class="style9">
                                    <asp:DropDownList ID="ddlUserLevel" runat="server" CssClass="ddlnomination" Width="204px">
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left" class="style2">
                                    &nbsp;
                                </td>
                                <td align="left" class="style12">
                                    &nbsp;
                                </td>
                                <td align="left" class="style1">
                                    Password
                                </td>
                                <td style="width: 2px" align="left">
                                    :
                                </td>
                                <td align="left" class="style5">
                                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                                </td>
                                <td align="left" class="style13">
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPass"
                                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                        ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                                </td>
                                <td align="left" class="style14">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="style7">
                                    &nbsp;
                                </td>
                                <td align="left" class="style3">
                                    Status
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td align="left" class="style9">
                                    <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" />
                                </td>
                                <td align="left" class="style2">
                                    &nbsp;
                                </td>
                                <td align="left" class="style12">
                                    &nbsp;
                                </td>
                                <td align="left" class="style1">
                                    Re-Enter Pass.
                                </td>
                                <td style="width: 2px" align="left">
                                    :
                                </td>
                                <td align="left" class="style5">
                                    <asp:TextBox ID="txtConfPass" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                                </td>
                                <td align="left" class="style13">
                                    <asp:RequiredFieldValidator ControlToValidate="txtConfPass" CssClass="failureNotification"
                                        Display="Dynamic" ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired"
                                        runat="server" ToolTip="Confirm Password is required." ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="txtPass"
                                        ControlToValidate="txtConfPass" CssClass="failureNotification" Display="Dynamic"
                                        ErrorMessage="Password and Confirmation Password does not match." ValidationGroup="btnSave">*</asp:CompareValidator>
                                    &nbsp;
                                </td>
                                <td align="left" class="style14">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btnClearEntry" runat="server" Text="Clear" Width="100px" OnClick="btnClearEntry_Click" />
                        &nbsp;<asp:Button ID="Save" runat="server" OnClick="Save_Click" Text="Save" ValidationGroup="btnSave"
                            Width="100px" />
                        <span>
                            <asp:HiddenField ID="hfEmpRef" Value="0" runat="server" />
                            <asp:SqlDataSource ID="dsCompany" runat="server" ConnectionString="<%$ ConnectionStrings:SYSConStr %>"
                                SelectCommand="SELECT * FROM [TBL_COMPANY]"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="dsUserGroup" runat="server" ConnectionString="<%$ ConnectionStrings:SYSConStr %>"
                                SelectCommand="SELECT * FROM [TBL_USER_GROUP]"></asp:SqlDataSource>
                            <asp:HiddenField ID="hfEditStatus" runat="server" Value="N" />
                            <asp:HiddenField ID="hfRefNo" runat="server" Value="0" />
                        </span>
                    </div>
                </asp:Panel>
                <br />
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Enter Valid Data:"
                ValidationGroup="btnSave" ShowMessageBox="false" ShowSummary="true" Font-Size="10pt"
                ForeColor="Red" />
            <asp:Panel ID="pnlSearchBoxUser" runat="server" DefaultButton="btnSearchUser">
                <div align="center" style="background-color: #86AEAE">
                    <span>Search User:</span> <span>
                        <asp:TextBox ID="txtSearchUser" runat="server" Width="350px" CssClass="search textAlignCenter"
                            autoComplete="off"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchUser" runat="server" BehaviorID="AutoCompleteUserSrch"
                            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchUser" ServicePath="~/Module/SYS/Forms/wsAutoComSys.asmx"
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearchUser">
                        </cc1:AutoCompleteExtender>
                        <asp:Button ID="btnSearchUser" runat="server" OnClick="btnSearchUser_Click" onkeypress="return clickButton(event,'btnSearchUser')"
                            Text="Search" ValidationGroup="btnSearchUser" />
                        <asp:Button ID="btnClearUser" runat="server" OnClick="btnClearUser_Click" Text="Clear"
                            Visible="False" Width="60px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSearchUser"
                            ErrorMessage="Enter Search Text" Font-Bold="False" ForeColor="Red" ValidationGroup="btnSearchUser"
                            Font-Size="10pt"></asp:RequiredFieldValidator>
                    </span>
                </div>
                <div align="center">
                    <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" EmptyDataText="No Data  Found."
                        Font-Size="8pt" OnRowDataBound="gvUser_RowDataBound" OnSelectedIndexChanged="gvUser_SelectedIndexChanged"
                        AllowPaging="True" PageSize="50" AllowSorting="True" OnPageIndexChanging="gvUser_PageIndexChanging"
                        OnSorting="gvUser_Sorting">
                        <RowStyle BackColor="White" ForeColor="#003399" Wrap="true" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="User_Code" HeaderText="User Code" SortExpression="User_Code" />
                            <asp:BoundField DataField="User_Name" HeaderText="User Name" SortExpression="User_Name" />
                            <asp:TemplateField HeaderText="Supervisor">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfEmpRef" runat="server" Value='<%# Bind("User_Ext_Data1") %>' />
                                    <asp:HiddenField ID="hfSupervisorRef" runat="server" Value='<%# Bind("User_Supervisor") %>' />
                                    <asp:Label ID="Label3" runat="server" Text='<%# GetSupervisorName(int.Parse(Eval("User_Supervisor").ToString())) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Group">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfUserGrp" runat="server" Value='<%# Bind("User_Group") %>' />
                                    <asp:HiddenField ID="hfDept" runat="server" Value='<%# Bind("Uer_Dept") %>' />
                                    <asp:HiddenField ID="hfDesig" runat="server" Value='<%# Bind("User_Desig") %>' />
                                    <asp:Label ID="Label4" runat="server" Text='<%# GetUserGroupName(int.Parse(Eval("User_Group").ToString())) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="User_Level" HeaderText="Level">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="User_Email" HeaderText="Email" />
                            <asp:BoundField DataField="User_Contact" HeaderText="Contact No" />
                            <asp:TemplateField HeaderText="Company">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfUserRef" runat="server" Value='<%# Bind("User_Ref_No") %>' />
                                    <asp:Label ID="Label1" runat="server" Text='<%# GetCompanyName(int.Parse(Eval("User_Comp_Ref").ToString())) %>'></asp:Label>
                                    <asp:HiddenField ID="hfCompany" runat="server" Value='<%# Bind("User_Comp_Ref") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="User_Entry_Date" HeaderText="Entry Date" />
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Bind("User_Status") %>' />
                                    <asp:Label ID="Label2" runat="server" Text='<%# GetStatusName(int.Parse(Eval("User_Status").ToString())) %>'></asp:Label>
                                    <asp:HiddenField ID="hfPass" runat="server" Value='<%# Bind("User_Pass") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" Font-Size="10pt"
                            HorizontalAlign="Left" Wrap="False" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px"
                            HorizontalAlign="Left" Wrap="False" Font-Size="8pt" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#487575" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#275353" />
                    </asp:GridView>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearchEmp" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClearEmp" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Save" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClearEntry" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSearchUser" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClearUser" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvUser" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvUser" EventName="PageIndexChanging" />
            <asp:AsyncPostBackTrigger ControlID="cboEmpId" EventName="SelectedIndexChanged" />
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
    <br />
    <asp:HiddenField ID="hfHidden" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupExtenderMsg" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnMsgOk" OkControlID="btnMsgOk" PopupControlID="pnlMsg" TargetControlID="hfHidden">
    </cc1:ModalPopupExtender>
</asp:Content>
