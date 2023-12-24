<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmPoSend.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmPoSend" %>

<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="update" runat="server">
        <ContentTemplate>
            <table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
                <tr>
                    <td style="height: 22px">
                    </td>
                    <td style="height: 22px">
                    </td>
                    <td style="height: 22px">
                    </td>
                </tr>
                <tr>
                    <td class="tblbig" colspan="3" style="text-align: center">
                        PURCHASE ORDER</td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="text-align: right">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="text-align: left">
                        Date:
                        <asp:TextBox ID="txtdate" runat="server" CssClass="txtbox" Width="105px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="text-align: left; height: 16px;">
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="vertical-align: top; text-align: left">
                        To: &nbsp; &nbsp;<asp:TextBox ID="txtparty" runat="server" AutoPostBack="True" autocomplete="off"
                            CssClass="txtbox" OnTextChanged="txtparty_TextChanged" Width="515px"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender runat="server" ID="autoComplete1" BehaviorID="AutoCompleteEx2"
                            TargetControlID="txtparty" ServicePath="services/autocomplete.asmx" ServiceMethod="GetPartyForPoSend"
                            MinimumPrefixLength="1" CompletionInterval="1000" EnableCaching="false" CompletionSetCount="20"
                            CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=","
                            ShowOnlyCurrentWordInCompletionListItem="true">
                        </ajaxToolkit:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="vertical-align: middle; text-align: left; height: 17px;">
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="vertical-align: middle; text-align: left">
                        Sub:&nbsp;
                        <asp:TextBox ID="txtsub" runat="server" CssClass="txtbox" Width="515px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="vertical-align: middle; height: 17px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 19px; text-align: left">
                        <%--<table id="tblhtml" runat="server" style="width: 99%" border="1" bordercolor="#6b7ebf"
                            cellspacing="0">
                            <tr>
                                <td style="width: 27px">
                                    Sl
                                </td>
                                <td style="width: 27px">
                                    Code
                                </td>
                                <td style="width: 209px">
                                    Item Name
                                </td>
                                <td style="width: 377px">
                                    Specification
                                </td>
                                <td>
                                    Brand
                                </td>
                                <td>
                                    Origin
                                </td>
                                <td>
                                    Packing
                                </td>
                                <td>
                                    Qty
                                </td>
                                <td>
                                    Rate
                                </td>
                                <td>
                                    Amount
                                </td>
                            </tr>
                        </table>--%>
                        <table id="tblhtml" runat="server" style="width: 99%" border="1" bordercolor="#6b7ebf"
                            cellspacing="0">
                            <tr>
                                <td style="width: 27px">
                                    Sl
                                </td>
                                <td style="width: 27px">
                                    Code
                                </td>
                                <td style="width: 209px">
                                    Item Name
                                </td>
                                <td style="width: 377px">
                                    Specification
                                </td>
                                <td>
                                    Brand
                                </td>
                                <td>
                                    Origin
                                </td>
                                <td>
                                    Packing
                                </td>
                                <td>
                                    Qty
                                </td>
                                <td>
                                    Rate
                                </td>
                                <td>
                                    Amount
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 18px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 19px; text-align: left">
                        General Terms:
                    </td>
                </tr>
                <tr>
                    <td id="Td1" class="tbl" colspan="3" style="height: 19px; text-align: left" runat="server">
                        <table id="tblgen" runat="server" border="1" bordercolor="#CC99FF" cellspacing="0"
                            style="width: 99%">
                            <tr>
                                <td style="width: 27px">
                                    Sl
                                </td>
                                <td style="width: 27px">
                                    Select
                                </td>
                                <td>
                                    Terms Detail
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="Td2" runat="server" class="tbl" colspan="3" style="height: 19px; text-align: left">
                        Special Terms:
                    </td>
                </tr>
                <tr>
                    <td id="Td3" runat="server" class="tbl" colspan="3" style="height: 19px; text-align: left">
                        <table id="tblspe" runat="server" border="1" bordercolor="#CC99FF" cellspacing="0"
                            style="width: 99%">
                            <tr>
                                <td style="width: 27px">
                                    Sl
                                </td>
                                <td style="width: 27px">
                                    Select
                                </td>
                                <td>
                                    Terms Detail
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="Td4" runat="server" class="tbl" colspan="3" style="height: 19px; text-align: left">
                        Payment Terms:
                        <asp:Label ID="lblpaytype" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td id="Td5" runat="server" class="tbl" colspan="3" style="height: 19px; text-align: left">
                        <table id="tblpay" runat="server" border="1" bordercolor="#CC99FF" cellspacing="0"
                            style="width: 99%">
                            <tr>
                                <td style="width: 27px">
                                    Sl
                                </td>
                                <td style="width: 27px">
                                    Select
                                </td>
                                <td>
                                    Terms Detail
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 22px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 19px; text-align: left">
                        Thanking You.<br />
                        <br />
                        <asp:TextBox ID="txtfrom" runat="server" CssClass="txtbox" Height="65px" TextMode="MultiLine"
                            Width="237px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 16px; text-align: center">
                        &nbsp;<asp:Button ID="btnproceed" runat="server" CssClass="btn2" OnClick="btnproceed_Click"
                            Text="Proceed" Width="136px" Visible="False" />
                        &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 8px; text-align: center">
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 25px; text-align: center">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtparty" EventName="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
