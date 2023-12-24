<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" MaintainScrollPositionOnPostback="true" CodeBehind="frmSupplierPayAdv.aspx.cs"
    Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmSupplierPayAdv" %>

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
                        Supplier Advance Payment
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 16px; text-align: center">
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 25px; text-align: left">
                        Pending List:<asp:Label ID="lblcount" runat="server" Text="(0)"></asp:Label>
                        <asp:DropDownList ID="ddllist" runat="server" Width="550px" OnSelectedIndexChanged="ddllist_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:Button ID="btnreload" runat="server" CssClass="btn2" OnClick="btnreload_Click"
                            Text="Reload" Width="77px" />
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 22px; text-align: center">
                        <table id="tbl_advance" width="99%" runat="server" style="text-align: left; border-top-width: 1px;
                            border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='#ffffff', startcolorstr='#e6e6fa', gradienttype='0');
                            border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa;
                            border-right-width: 1px; border-right-color: #e6e6fa;">
                            <tr>
                                <td style="text-align: center" colspan="5">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" colspan="5">
                                    <asp:GridView ID="gdItem" runat="server" BackColor="White" BorderColor="#6B7EBF"
                                        BorderStyle="Solid" BorderWidth="10px" CellPadding="4" ForeColor="#333333" GridLines="None"
                                        PageSize="100" SkinID="GridView" Style="border-color: #e6e6fa; border-width: 1px;
                                        text-align: left;" Width="98%" Font-Size="10pt">
                                        <FooterStyle BackColor="#6B7EBF" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#6B7EBF" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle Font-Bold="True" />
                                        <HeaderStyle BackColor="#6B7EBF" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="Lavender" Font-Size="8pt" Font-Strikeout="False" />
                                        <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" Font-Underline="False" />
                                        <RowStyle Font-Size="8pt" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left" colspan="5">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; font-weight: bold;" colspan="5">
                                    Total Amount:
                                    <asp:Label ID="lbltot" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" colspan="5">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" colspan="5">
                                    PAYMENT TERMS (<asp:Label ID="lblpaytype" runat="server" Font-Bold="True"></asp:Label>)
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" colspan="5">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td id="celtc" runat="server" style="text-align: left" colspan="5">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left" colspan="5">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 165px;">
                                    &nbsp;
                                </td>
                                <td style="text-align: left; width: 163px;">
                                    DATE
                                </td>
                                <td style="text-align: left">
                                    :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtPayDate" runat="server" Width="182px" CssClass="textAlignCenter"
                                        Enabled="False" Font-Bold="True" ForeColor="#0066FF"></asp:TextBox>
                                    <asp:Image ID="imgPayDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <ajaxToolkit:CalendarExtender ID="CalenderValDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtPayDate" PopupButtonID="imgPayDt">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td style="text-align: left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 165px;">
                                    &nbsp;
                                </td>
                                <td style="text-align: left; width: 163px;">
                                    PAY TYPE
                                </td>
                                <td style="text-align: left">
                                    :
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlPayMode" runat="server" Width="182px">
                                        <asp:ListItem>Cash</asp:ListItem>
                                        <asp:ListItem>Cheque</asp:ListItem>
                                        <%--<asp:ListItem>DD</asp:ListItem>--%>
                                        <%--<asp:ListItem>TT</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 165px;">
                                    &nbsp;&nbsp;
                                </td>
                                <td style="text-align: left; width: 163px;">
                                    BANK
                                </td>
                                <td style="text-align: left">
                                    :
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlbank" runat="server" CssClass="txtbox" Width="500px">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 165px;">
                                    &nbsp;
                                </td>
                                <td style="text-align: left; width: 163px;">
                                    CHQ NO / DOC. REF
                                </td>
                                <td style="text-align: left">
                                    :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtChqNo" runat="server" Width="182px"></asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 165px;">
                                    &nbsp;
                                </td>
                                <td style="text-align: left; width: 163px;">
                                    MONER RECT. NO
                                </td>
                                <td style="text-align: left">
                                    :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtMnyRctNo" runat="server" Width="182px"></asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 165px;">
                                    &nbsp;
                                </td>
                                <td style="text-align: left; width: 163px;">
                                    Amount
                                </td>
                                <td style="text-align: left">
                                    :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtvamnt" runat="server" Width="182px" CssClass="txtbox"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtqty_FilteredTextBoxExtender" runat="server"
                                        FilterType="Custom, Numbers" TargetControlID="txtvamnt" ValidChars=".">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" colspan="5">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" colspan="5">
                                    <asp:Button ID="btnreject" runat="server" CssClass="btn2" OnClick="btnreject_Click"
                                        Text="Reject" Width="143px" Visible="False" />
                                    &nbsp;<asp:Button ID="btnapprove" runat="server" CssClass="btn2" OnClick="btnapprove_Click"
                                        Text="Update Payment" Width="138px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" colspan="5">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 119px; text-align: center">
                        &nbsp;
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
            <asp:AsyncPostBackTrigger ControlID="ddllist" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnreload" EventName="Click" />
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
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderMsg" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnMsgOk" OkControlID="btnMsgOk" PopupControlID="pnlMsg" TargetControlID="hfHidden"
        DropShadow="true">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
