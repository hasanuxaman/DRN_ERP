<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmSec.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmSec" %>

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
                Create Section</div>
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
                            <asp:DropDownList ID="cboLoc" runat="server" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="cboLoc_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select Company"
                                ForeColor="Red" InitialValue="0" ControlToValidate="cboLoc" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="left" style="width: 81px">
                            &nbsp;
                        </td>
                        <td style="width: 99px;" align="left">
                            Phone
                        </td>
                        <td style="width: 3px;" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 204px;" align="left">
                            <asp:TextBox runat="server" ID="txtSectPhone" Width="180px"></asp:TextBox>
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
                            Department
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 187px" align="left">
                            <asp:DropDownList ID="cboDept" runat="server" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="cboDept_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select Department"
                                ForeColor="Red" InitialValue="0" ControlToValidate="cboDept" 
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="left" style="width: 81px">
                            &nbsp;
                        </td>
                        <td style="width: 99px;" align="left">
                            Fax
                        </td>
                        <td style="width: 3px;" align="left">
                            &nbsp;
                        </td>
                        <td style="width: 204px;" align="left">
                            <asp:TextBox runat="server" ID="txtSectFax" Width="180px"></asp:TextBox>
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
                            Sec. Code
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 187px" align="left">
                            <asp:TextBox ID="txtSectCode" runat="server" Width="180px" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSectCode"
                                ErrorMessage="Enter Section Code" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="left" style="width: 81px">
                            &nbsp;
                        </td>
                        <td style="width: 99px;" align="left">
                            E-Mail
                        </td>
                        <td style="width: 3px;" align="left">
                            :
                        </td>
                        <td style="width: 204px;" align="left">
                            <asp:TextBox ID="txtSectEmail" runat="server" Width="180px"></asp:TextBox>
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
                            <asp:TextBox ID="txtSectName" runat="server" Width="180px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSectName"
                                ErrorMessage="Enter Section Name" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
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
                            <asp:TextBox runat="server" ID="txtSectOpnDate" Width="180px"></asp:TextBox>
                            <asp:Image ID="imgSectOpnDt" runat="server" ImageUrl="~/Image/calendar.png" />
                            <cc1:CalendarExtender ID="txtDeptCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                TargetControlID="txtSectOpnDate" PopupButtonID="imgSectOpnDt">
                            </cc1:CalendarExtender>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtSectOpnDate"
                                ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck" Type="Date"
                                ValidationGroup="Save">*</asp:CompareValidator>
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
                            <asp:TextBox runat="server" ID="txtSectRem" Width="180px"></asp:TextBox>
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
                            <asp:HiddenField ID="hfSectRefNo" runat="server" Value="0" />
                        </td>
                        <td align="left" colspan="4">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="8pt" ForeColor="Red"
                                ValidationGroup="Save" />
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
                            &nbsp; &nbsp;<asp:Button ID="btnClearSect" runat="server" Text="Clear" Width="131px"
                                OnClick="btnClearSect_Click" />
                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save"
                                OnClick="btnSave_Click" Width="131px" />
                        </td>
                        <td style="width: 122px" align="left">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="background-color: #CCFFFF">
                <br />
                <asp:GridView ID="gvSec" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333" OnRowDataBound="gvSec_RowDataBound" OnSelectedIndexChanged="gvSec_SelectedIndexChanged"
                    OnRowDeleting="gvSec_RowDeleting" AllowPaging="True" 
                    onpageindexchanging="gvSec_PageIndexChanging" PageSize="25">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="SecRefNo" HeaderText="Ref. No" />
                        <asp:BoundField DataField="SecCode" HeaderText="Code" />
                        <asp:BoundField DataField="SecName" HeaderText="Name" />
                        <asp:TemplateField HeaderText="Company">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfCompRef" runat="server" Value='<%# Bind("LocRefNo") %>' />
                                <asp:Label ID="lblCompName" runat="server" Text='<%# GetCompanyName(Eval("LocRefNo").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfDeptRef" runat="server" Value='<%# Bind("DeptRefNo") %>' />
                                <asp:Label ID="lblDeptName" runat="server" Text='<%# GetDeptName(Eval("DeptRefNo").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SecCPPhone" HeaderText="Phone" />
                        <asp:BoundField DataField="SecCPFax" HeaderText="Fax" />
                        <asp:BoundField DataField="SecCPEmail" HeaderText="E-Mail" />
                        <asp:BoundField DataField="SecOpnDate" DataFormatString="{0:d}" HeaderText="Opening Date" />
                        <asp:BoundField DataField="SecRem" HeaderText="Remarks" />
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
            <asp:AsyncPostBackTrigger ControlID="cboLoc" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboDept" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvSec" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvSec" EventName="RowDeleting" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
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
