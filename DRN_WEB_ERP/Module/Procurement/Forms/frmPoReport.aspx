<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmPoReport.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmPoReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        PO Report</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%; font-size: 12px; background-color: #CCFFFF;">
                <tr>
                    <td style="width: 22px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td width="2">
                        &nbsp;
                    </td>
                    <td style="width: 148px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td width="2">
                        &nbsp;
                    </td>
                    <td style="width: 195px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td width="2">
                        &nbsp;
                    </td>
                    <td>
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
                    <td style="width: 22px">
                        &nbsp;
                    </td>
                    <td>
                        PO Type
                    </td>
                    <td width="2">
                        :
                    </td>
                    <td style="width: 148px">
                        <asp:CheckBoxList ID="chkListRpt" runat="server" RepeatDirection="Horizontal">
                        </asp:CheckBoxList>
                    </td>
                    <td>
                        Option
                    </td>
                    <td width="2">
                        :
                    </td>
                    <td style="width: 195px">
                        <asp:RadioButtonList ID="optRptDetSum" runat="server" RepeatDirection="Horizontal"
                            Width="160px">
                            <asp:ListItem Selected="True" Value="1">Details</asp:ListItem>
                            <asp:ListItem Value="2">Summary</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        Filter By
                    </td>
                    <td width="2">
                        :
                    </td>
                    <td>
                        <asp:RadioButtonList ID="optRptFilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="optRptFilter_SelectedIndexChanged"
                            RepeatDirection="Horizontal" Width="300px">
                            <asp:ListItem Selected="True" Value="1">Supplier Name</asp:ListItem>
                            <asp:ListItem Value="2">Item Name</asp:ListItem>
                            <asp:ListItem Value="3">Item Category</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIncMrr" runat="server" Text="Include Receive" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 22px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td width="2">
                        &nbsp;
                    </td>
                    <td style="width: 148px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td width="2">
                        &nbsp;
                    </td>
                    <td style="width: 195px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td width="2">
                        &nbsp;
                    </td>
                    <td>
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
            <table style="width: 100%; font-size: 12px; background-color: #CCFFFF;">
                <tr>
                    <td style="width: 21px">
                        &nbsp;
                    </td>
                    <td align="right" style="width: 138px">
                        Date From
                    </td>
                    <td>
                        :
                    </td>
                    <td style="width: 168px">
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDtFromCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                        </cc1:CalendarExtender>
                        <asp:Image ID="imgFromDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                    </td>
                    <td align="right" style="width: 97px">
                        Date To
                    </td>
                    <td style="width: 3px">
                        :
                    </td>
                    <td style="width: 146px">
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="inline" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            PopupButtonID="imgToDate" TargetControlID="txtToDate">
                        </cc1:CalendarExtender>
                        <asp:Image ID="imgToDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png"
                            Width="16px" />
                    </td>
                    <td align="right" style="width: 127px">
                        Search Filter
                    </td>
                    <td style="width: 6px">
                        :
                    </td>
                    <td style="width: 136px">
                        <asp:TextBox ID="txtSearch" runat="server" Width="320px" CssClass="search textAlignCenter"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchSupplier"
                            ServicePath="~/Module/Procurement/Forms/wsAutoComProc.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                            TargetControlID="txtSearch">
                        </cc1:AutoCompleteExtender>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnShowRpt" runat="server" OnClick="btnShowRpt_Click" Text="Show"
                            Width="100px" />
                    </td>
                    <td style="width: 98px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 21px">
                        &nbsp;
                    </td>
                    <td style="width: 138px">
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 168px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFromDate"
                            Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                        <br />
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFromDate"
                            Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                            Type="Date" ValidationGroup="btnShow"></asp:CompareValidator>
                    </td>
                    <td style="width: 97px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 146px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                            Display="Dynamic" ErrorMessage="Enter Date First" ForeColor="Red" ValidationGroup="btnShow"></asp:RequiredFieldValidator>
                        <br />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtToDate"
                            Display="Dynamic" ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck"
                            Type="Date" ValidationGroup="btnShow"></asp:CompareValidator>
                    </td>
                    <td style="width: 127px">
                        &nbsp;
                    </td>
                    <td style="width: 6px">
                        &nbsp;
                    </td>
                    <td style="width: 136px">
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 98px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 21px">
                        &nbsp;
                    </td>
                    <td style="width: 138px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 168px">
                        &nbsp;
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DRNConStr %>"
                            SelectCommand="SELECT DISTINCT PO_Hdr_Code FROM tbl_PuTr_PO_Hdr ORDER BY PO_Hdr_Code">
                        </asp:SqlDataSource>
                    </td>
                    <td style="width: 97px">
                        &nbsp;
                    </td>
                    <td style="width: 3px">
                        &nbsp;
                    </td>
                    <td style="width: 146px">
                        &nbsp;
                    </td>
                    <td style="width: 127px">
                        &nbsp;
                    </td>
                    <td style="width: 6px">
                        &nbsp;
                    </td>
                    <td style="width: 136px">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 98px">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="optRptFilter" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnShowRpt" EventName="Click" />
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
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupExtenderMsg" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnMsgOk" OkControlID="btnMsgOk" PopupControlID="pnlMsg" TargetControlID="hfHidden"
        DropShadow="true">
    </cc1:ModalPopupExtender>
</asp:Content>
