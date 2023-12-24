<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmQuotEnt_Old.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmQuotEnt_Old" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="UserControl/CtlQtnView.ascx" TagName="CtlQtnView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Quotation Entry</div>
    <table class="tblmas" style="width: 100%" id="tblmaster" runat="server">
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: left">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <asp:Button ID="btnQuotation0" runat="server" Text="QUOTATION ENTRY" Width="159px"
                    OnClick="btnQuotation_Click" />
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                Item Type:<asp:DropDownList ID="cboItemType" runat="server" AutoPostBack="True" Width="200px"
                    OnSelectedIndexChanged="cboItemType_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;Item Name:<asp:DropDownList ID="cboItem" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlOrdItem_SelectedIndexChanged" Width="230px">
                </asp:DropDownList>
                Year:<asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
                    Width="100px">
                </asp:DropDownList>
                &nbsp;Month:<asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"
                    Width="100px">
                </asp:DropDownList>
                &nbsp;<asp:Button ID="btnMprSort" runat="server" Text="Sort" OnClick="btnMprSort_Click"
                    Width="80px" />
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                <asp:UpdatePanel ID="updpnl" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gdItem" runat="server" BackColor="White" BorderColor="#6B7EBF"
                            BorderStyle="Solid" BorderWidth="1px" CellPadding="4" ForeColor="#333333" GridLines="None"
                            PageSize="100" SkinID="GridView" OnRowCommand="gdItem_RowCommand" OnSorting="gdItem_Sorting"
                            OnRowDataBound="gdItem_RowDataBound" Style="border-top-width: 1px; border-left-width: 1px;
                            border-left-color: #e6e6fa; border-bottom-width: 1px; border-bottom-color: #e6e6fa;
                            border-top-color: #e6e6fa; border-right-width: 1px; border-right-color: #e6e6fa;"
                            Width="100%" AllowSorting="True">
                            <FooterStyle BackColor="#6B7EBF" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#6B7EBF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle Font-Bold="True" Wrap="False" />
                            <HeaderStyle BackColor="#6B7EBF" Font-Bold="True" ForeColor="White" Font-Size="8" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" Font-Underline="False" />
                            <RowStyle Font-Size="8pt" Wrap="true" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sel">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qtn">
                                    <ItemTemplate>
                                        <uc1:CtlQtnView ID="ctl1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="Button1" runat="server" Text="Proceed" OnClick="Button1_Click" CssClass="btn2" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Panel ID="PNL" runat="server" CssClass="tbl" Style="border-right: black 2px solid;
                            padding-right: 20px; border-top: black 2px solid; padding-left: 20px; display: none;
                            padding-bottom: 20px; border-left: black 2px solid; width: 500px; padding-top: 20px;
                            border-bottom: black 2px solid; background-color: white" Height="224px" Width="500px">
                            &nbsp;<br />
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 66px; text-align: left;">
                                        MPR
                                    </td>
                                    <td style="width: 6px">
                                        :
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lblmpr" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 66px; text-align: left">
                                        Item
                                    </td>
                                    <td style="width: 6px">
                                        :
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lblitem" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hfPrDetLno" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 66px; text-align: left">
                                        &nbsp;
                                    </td>
                                    <td style="width: 6px">
                                        &nbsp;
                                    </td>
                                    <td style="text-align: left">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 66px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 6px">
                                        &nbsp;
                                    </td>
                                    <td style="text-align: left">
                                        <asp:CheckBox ID="chkurgent" runat="server" Font-Bold="True" ForeColor="#FF3300"
                                            Text="URGENT" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 66px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 6px">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 66px; text-align: left;">
                                        Comments
                                    </td>
                                    <td style="width: 6px">
                                        :
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtcomments" runat="server" CssClass="txtbox" Width="387px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 66px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 6px">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div style="text-align: right">
                                <asp:Button ID="ButtonOk" runat="server" CssClass="btn2" Text="OK" Width="80px" OnClick="ButtonOk_Click" />
                                &nbsp;<asp:Button ID="ButtonCancel" runat="server" CssClass="btn2" Text="Cancel"
                                    Width="82px" />
                            </div>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="ButtonCancel" PopupControlID="PNL" TargetControlID="Button2">
                        </ajaxToolkit:ModalPopupExtender>
                        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" DisplayModalPopupID="ModalPopupExtender1"
                            TargetControlID="Button2">
                        </ajaxToolkit:ConfirmButtonExtender>
                        <asp:Button ID="Button2" runat="server" Visible="false" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ButtonOk" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <asp:Button ID="btnQuotation" runat="server" Text="QUOTATION ENTRY" Width="159px"
                    OnClick="btnQuotation_Click" />
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <table id="tbltooltip" runat="server" class="tbl" style="border: thin solid #000000;
                    width: 836px">
                    <tr>
                        <td style="width: 21px" bgcolor="#ccccff">
                            SL
                        </td>
                        <td bgcolor="#ccccff">
                            Party
                        </td>
                        <td bgcolor="#ccccff">
                            Rate
                        </td>
                        <td bgcolor="#ccccff">
                            Specification
                        </td>
                        <td bgcolor="#ccccff">
                            Brand
                        </td>
                        <td bgcolor="#ccccff">
                            Origin
                        </td>
                        <td bgcolor="#ccccff">
                            Packing
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
                            &nbsp;
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 21px">
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
                        <td style="width: 21px">
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
                        <td style="width: 21px">
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
                        <td style="width: 21px">
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
            </td>
        </tr>
        <tr>
            <td>
                <table id="tbltooltip2" runat="server" class="style3" style="background-color: #FFFFFF;
                    background-position: center center; width: 772px;">
                    <tr>
                        <td class="style4" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style6" colspan="2">
                            TERMS AND CONDITIONS
                        </td>
                    </tr>
                    <tr>
                        <td class="style5" valign="top">
                            General Terms:
                        </td>
                        <td bgcolor="AliceBlue">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style5" valign="top">
                            Special Terms:
                        </td>
                        <td bgcolor="AliceBlue">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style5" valign="top">
                            Pay Terms:
                        </td>
                        <td bgcolor="AliceBlue">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style5" valign="top">
                            Valid Days:
                        </td>
                        <td bgcolor="AliceBlue">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="height: 57px">
            </td>
            <td style="height: 57px">
            </td>
            <td style="height: 57px">
            </td>
        </tr>
    </table>
</asp:Content>
