<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmSalesCrSecurity.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesCrSecurity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Credit Limit Security
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <br />
                <table style="width: 63%; background-color: #CCFFFF; font-size: small;">
                    <tr>
                        <td style="width: 171px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 212px">
                            &nbsp;
                        </td>
                        <td style="width: 2px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 212px;">
                            Credit Limit
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td align="left" style="width: 258px">
                            <asp:TextBox ID="txtCustCrLimit" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" Font-Bold="True" Width="250px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCustCrLimit_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCustCrLimit"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 212px;">
                            Credit Period (days)
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td align="left" style="width: 258px">
                            <asp:TextBox ID="txtCustCrPeriod" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" Font-Bold="True" Width="250px"></asp:TextBox>
                        </td>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 212px;">
                            &nbsp;
                        </td>
                        <td style="width: 2px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 212px; text-decoration: underline;">
                            Credit Limit Support Doc
                        </td>
                        <td style="width: 2px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 258px">
                            &nbsp;
                        </td>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 212px;">
                            Bank Gurantee
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td align="left" style="width: 258px">
                            <asp:TextBox ID="txtCrSecBG" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" Font-Bold="False" Width="250px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCrSecBG_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCrSecBG"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 212px;">
                            ITR
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td align="left" style="width: 258px">
                            <asp:TextBox ID="txtCrSecFDR" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" Font-Bold="False" Width="250px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCrSecFDR_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCrSecFDR"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 212px;">
                            Deposit
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td align="left" style="width: 258px">
                            <asp:TextBox ID="txtCrSecSDR" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" Font-Bold="False" Width="250px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCrSecSDR_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCrSecSDR"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 212px;">
                            Post Dated Cheque
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td align="left" style="width: 258px">
                            <asp:TextBox ID="txtCrSecLAND" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" Font-Bold="False" Width="250px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCrSecLAND_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCrSecLAND"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 212px;">
                            Undated Cheque
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td align="left" style="width: 258px">
                            <asp:TextBox ID="txtCrSecCHQ" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" Font-Bold="False" Width="250px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCrSecCHQ_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCrSecCHQ"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 212px;">
                            Notification of Award
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td align="left" style="width: 258px">
                            <asp:TextBox ID="txtCrSecNOA" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" Font-Bold="False" Width="250px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCrSecNOA_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCrSecNOA"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px; height: 22px;">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 212px; font-weight: bold;">
                            Total Security
                        </td>
                        <td style="width: 2px; height: 22px;">
                            :
                        </td>
                        <td align="left" style="height: 22px; width: 258px;" valign="top">
                            <asp:TextBox ID="txtCrSecTotAmt" runat="server" BorderStyle="None" CssClass="textAlignCenter"
                                Enabled="False" Font-Bold="True" Width="250px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCrSecTotAmt_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCrSecTotAmt"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="height: 22px; width: 258px;" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 171px; height: 22px;">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 212px; font-weight: bold;">
                            &nbsp;
                        </td>
                        <td style="width: 2px; height: 22px;">
                            &nbsp;
                        </td>
                        <td align="left" style="height: 22px; width: 258px;" valign="top">
                            &nbsp;
                            <asp:Button ID="btnUploadCreditAeging" runat="server" OnClick="btnUploadCreditAeging_Click"
                                Text="Upload Credit Aeging" />
                        </td>
                        <td style="height: 22px; width: 258px;" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                </table>
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
