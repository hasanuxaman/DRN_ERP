<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmPoInquery.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmPoInquery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Purchase Order Inquery</div>
    <div align="center" style="background-color: #f0f8ff">
        <br />
        Year:<asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
            Width="100px">
        </asp:DropDownList>
        &nbsp;Month:<asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"
            Width="100px">
        </asp:DropDownList>
        &nbsp;Total P/O Found:&nbsp;<asp:Label ID="lblcount" runat="server" Text="(0)"></asp:Label>
        &nbsp;
        <asp:DropDownList ID="ddllist" runat="server" Width="500px" OnSelectedIndexChanged="ddllist_SelectedIndexChanged"
            AutoPostBack="True" CssClass="txtbox">
        </asp:DropDownList>
        <asp:Button ID="btnPoPrint" runat="server" Text="Print" OnClick="btnPoPrint_Click"
            Width="80px" />
    </div>
    <table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="height: 22px">
                &nbsp;
            </td>
            <td style="height: 22px" colspan="2">
                &nbsp;
            </td>
            <td style="height: 22px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 22px">
            </td>
            <td style="height: 22px" colspan="2">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="tblbig" colspan="4" style="text-align: center; background-color: #FFCC99;">
                PURCHASE ORDER (P/O)
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="4" style="text-align: right">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="2" style="text-align: left">
                To: &nbsp; &nbsp;<asp:TextBox ID="txtparty" runat="server" autocomplete="off" CssClass="txtbox"
                    Width="515px" Enabled="false"></asp:TextBox>
                <%--<ajaxToolkit:AutoCompleteExtender runat="server" ID="autoComplete1" BehaviorID="AutoCompleteEx2"
                    TargetControlID="txtparty" ServicePath="services/autocomplete.asmx" ServiceMethod="GetPartyForPoSend"
                    MinimumPrefixLength="1" CompletionInterval="1000" EnableCaching="false" CompletionSetCount="20"
                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=","
                    ShowOnlyCurrentWordInCompletionListItem="true">
                </ajaxToolkit:AutoCompleteExtender>--%>
            </td>
            <td class="tbl" colspan="2" style="text-align: left">
                Date:
                <asp:TextBox ID="txtdate" runat="server" CssClass="txtbox" Width="105px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="4" style="vertical-align: middle; text-align: left; height: 17px;">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="4" style="height: 19px; text-align: left">
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
            <td class="tbl" colspan="4" style="height: 18px; text-align: left">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="4" style="height: 19px; text-align: left">
                General Terms:
            </td>
        </tr>
        <tr>
            <td id="Td1" class="tbl" colspan="4" style="height: 19px; text-align: left" runat="server">
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
            <td id="Td2222" runat="server" class="tbl" colspan="4" style="height: 19px; text-align: left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td id="Td2" runat="server" class="tbl" colspan="4" style="height: 19px; text-align: left">
                Special Terms:
            </td>
        </tr>
        <tr>
            <td id="Td3" runat="server" class="tbl" colspan="4" style="height: 19px; text-align: left">
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
            <td id="Td4444" runat="server" class="tbl" colspan="4" style="height: 19px; text-align: left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td id="Td4" runat="server" class="tbl" colspan="4" style="height: 19px; text-align: left">
                Payment Terms:
                <asp:Label ID="lblpaytype" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="Td5" runat="server" class="tbl" colspan="4" style="height: 19px; text-align: left">
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
            <td class="tbl" colspan="4" style="height: 22px; text-align: left">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="4" style="height: 19px; text-align: left">
                Thanking You.<br />
                <br />
                <asp:TextBox ID="txtfrom" runat="server" CssClass="txtbox" Height="65px" TextMode="MultiLine"
                    Width="237px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="4" style="height: 24px; text-align: left">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="4" style="height: 8px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="4" style="height: 25px; text-align: center">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
