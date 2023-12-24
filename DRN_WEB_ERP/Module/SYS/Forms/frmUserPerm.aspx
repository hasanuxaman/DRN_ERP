<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmUserPerm.aspx.cs" Inherits="DRN_WEB_ERP.frmUserPerm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        User Permission Setup</div>
    <div align="center">
        <div align="center" style="background-color: #CC66FF">
            <br />
            Search User:<asp:TextBox ID="txtSrchUser" runat="server" CssClass="search" Width="400"
                autoComplete="off" ForeColor="#0066FF"></asp:TextBox>
            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderUser" runat="server" BehaviorID="AutoCompleteSrchUser"
                CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchUser" ServicePath="~/Module/SYS/Forms/wsAutoComSys.asmx"
                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSrchUser">
            </cc1:AutoCompleteExtender>
            &nbsp;<asp:Button ID="btnSearch" runat="server" onkeypress="return clickButton(event,'btnSearch')"
                Text="Search" ValidationGroup="btnSearch" Width="60px" OnClick="btnSearch_Click" />
            &nbsp;<asp:Button ID="btnClear" runat="server" Text="Clear" Visible="False" Width="60px"
                OnClick="btnClear_Click" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSrchUser"
                ErrorMessage="Enter Search Text" Font-Bold="False" ForeColor="#CC3300" ValidationGroup="btnSearch"
                Font-Size="12pt"></asp:RequiredFieldValidator>
            <br />
            <br />
            <div align="center">
                <table id="tblUserInfo" runat="server" style="border: 2px solid #CCCCCC; width: 70%;
                    background-color: #FFCCFF; font-size: 10px;">
                    <tr>
                        <td style="width: 54px" align="left">
                            Code
                        </td>
                        <td style="width: 4px">
                            :
                        </td>
                        <td style="width: 199px" align="left">
                            <asp:Label ID="lblCode" runat="server"></asp:Label>
                        </td>
                        <td style="width: 51px" align="left">
                            Name
                        </td>
                        <td style="width: 2px" align="left">
                            :
                        </td>
                        <td style="width: 133px" align="left">
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                        </td>
                        <td style="width: 60px" align="center" rowspan="4" valign="middle">
                            <asp:HyperLink ID="hlEmpPic" Target="_blank" runat="server">
                                <asp:Image ID="imgEmp" runat="server" Height="60px" Width="60px" ImageUrl="~/Image/NoImage.gif"
                                    BorderStyle="Ridge" BorderColor="White" BorderWidth="5" />
                            </asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 54px" align="left">
                            Designation
                        </td>
                        <td style="width: 4px">
                            :
                        </td>
                        <td style="width: 199px" align="left">
                            <asp:Label ID="lblDesig" runat="server"></asp:Label>
                        </td>
                        <td style="width: 51px" align="left">
                            Department
                        </td>
                        <td style="width: 2px" align="left">
                            :
                        </td>
                        <td style="width: 133px" align="left">
                            <asp:Label ID="lblDept" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 54px" align="left">
                            Supervisor
                        </td>
                        <td style="width: 4px">
                            :
                        </td>
                        <td style="width: 199px" align="left">
                            <asp:Label ID="lblSuprv" runat="server"></asp:Label>
                        </td>
                        <td style="width: 51px" align="left">
                            Email
                        </td>
                        <td style="width: 2px" align="left">
                            :
                        </td>
                        <td style="width: 133px" align="left">
                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 54px" align="left">
                            Status
                        </td>
                        <td style="width: 4px">
                            :
                        </td>
                        <td style="width: 199px" align="left">
                            <asp:CheckBox ID="chkStatus" runat="server" Text="Active" />
                        </td>
                        <td style="width: 51px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 2px" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 133px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <span>
                    <asp:HiddenField ID="hfUserRef" Value="0" runat="server" />
                    <asp:Button ID="Save" runat="server" Text="Save User Permission" OnClick="Save_Click" />
                </span>
                <br />
            </div>
        </div>
        <div align="center" style="border: 1px solid #800080; width: 100%; background-color: #FFCCFF;">
            <table>
                <tr>
                    <td>
                        <asp:TreeView ID="tvMenu" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                            ShowLines="True" OnSelectedNodeChanged="tvMenu_SelectedNodeChanged" OnTreeNodeCheckChanged="tvMenu_TreeNodeCheckChanged">
                            <HoverNodeStyle Font-Underline="False" />
                            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                NodeSpacing="0px" VerticalPadding="2px" />
                            <ParentNodeStyle Font-Bold="False" />
                            <SelectedNodeStyle Font-Underline="False" />
                        </asp:TreeView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
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
