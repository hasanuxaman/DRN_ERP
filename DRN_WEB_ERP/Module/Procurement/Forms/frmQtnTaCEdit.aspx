<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" ValidateRequest="false" CodeBehind="frmQtnTaCEdit.aspx.cs"
    Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmQtnTaCEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>--%>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function ColorRow(CheckBoxObj) {
            if (CheckBoxObj.checked == true) {
                CheckBoxObj.parentElement.parentElement.style.backgroundColor = '#88AAFF';
            }
            else {
                CheckBoxObj.parentElement.parentElement.style.backgroundColor = '#f0f8ff';
            }
        }                             
    </script>
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
                TERMS &amp; CONDITION UPDATE&nbsp;
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="Red"
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left">
                MPR REF. NO:
                            <asp:TextBox ID="txtMprRef" runat="server" CssClass="btn2" 
                    ReadOnly="True" Width="166px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left">
                ITEM DET:
                            <asp:TextBox ID="txtitem" runat="server" CssClass="btn2" ReadOnly="True" Width="389px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left">
                TERMS &amp; CONDITION ID:
                <asp:TextBox ID="txtid" runat="server" CssClass="btn2" ReadOnly="True" Width="166px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left; height: 16px;">
                PARTY DETAILS:&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtparty" runat="server" CssClass="btn2" ReadOnly="True" Width="493px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 19px; text-align: left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 19px; text-align: center">
                <asp:Button ID="btnshow" runat="server" CssClass="btn2" OnClick="btnshow_Click" Text=" Show T&amp;C Detail"
                    Width="131px" />
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 19px; text-align: left">
                <asp:UpdatePanel ID="upd" runat="server">
                    <ContentTemplate>
                        <table id="tbltac" runat="server" style="width: 100%;">
                            <tr>
                                <td colspan="2">
                                    <b>GENERAL TERMS AND CONDITIONS:</b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gdgen" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdgen_RowDataBound"
                                        Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Terms and condition">
                                                <ItemTemplate>
                                                    <FTB:FreeTextBox ID="TextBox1" runat="Server" AllowHtmlMode="False" BackColor="AliceBlue"
                                                        BreakMode="LineBreak" DesignModeBodyTagCssClass="" DesignModeCss="" DownLevelCols="50"
                                                        EditorBorderColorDark="AliceBlue" EditorBorderColorLight="AliceBlue" EnableHtmlMode="false"
                                                        EnableToolbars="False" FormatHtmlTagsToXhtml="True" GutterBackColor="AliceBlue"
                                                        GutterBorderColorDark="AliceBlue" GutterBorderColorLight="AliceBlue" Height="70px"
                                                        Text="" Width="100%" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 22px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <b>SPECIAL TERMS AND CONDITIONS:</b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gdspe" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdspe_RowDataBound"
                                        Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Terms and condition">
                                                <ItemTemplate>
                                                    <FTB:FreeTextBox ID="TextBox2" runat="Server" AllowHtmlMode="False" BackColor="AliceBlue"
                                                        BreakMode="LineBreak" DesignModeBodyTagCssClass="" DesignModeCss="" DownLevelCols="50"
                                                        EditorBorderColorDark="AliceBlue" EditorBorderColorLight="AliceBlue" EnableHtmlMode="false"
                                                        EnableToolbars="False" FormatHtmlTagsToXhtml="True" GutterBackColor="AliceBlue"
                                                        GutterBorderColorDark="AliceBlue" GutterBorderColorLight="AliceBlue" Height="70px"
                                                        Text="" Width="100%" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 25px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>PAYMENT TERMS:</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlpayterms" runat="server" Width="200px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlpayterms_SelectedIndexChanged">
                                        <asp:ListItem Value="No">No Advance</asp:ListItem>
                                        <asp:ListItem Value="Part">Part Advance</asp:ListItem>
                                        <asp:ListItem Value="Full">Full Advance</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gdpay" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdpay_RowDataBound"
                                        Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox3" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Terms and condition">
                                                <ItemTemplate>
                                                    <FTB:FreeTextBox ID="TextBox3" runat="Server" AllowHtmlMode="False" BackColor="AliceBlue"
                                                        BreakMode="LineBreak" DesignModeBodyTagCssClass="" DesignModeCss="" DownLevelCols="50"
                                                        EditorBorderColorDark="AliceBlue" EditorBorderColorLight="AliceBlue" EnableHtmlMode="false"
                                                        EnableToolbars="False" FormatHtmlTagsToXhtml="True" GutterBackColor="AliceBlue"
                                                        GutterBorderColorDark="AliceBlue" GutterBorderColorLight="AliceBlue" Height="70px"
                                                        Text="" Width="100%" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 37px">
                                    <b><span style="font-size: medium">Above quotetion valid upto </span>
                                        <asp:TextBox ID="txtvaliddays" runat="server" Style="font-size: medium" Width="50px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtqty_FilteredTextBoxExtender" runat="server"
                                            FilterType="Numbers" TargetControlID="txtvaliddays">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <span style="font-size: medium">&nbsp;days after purchase order.</span></b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 37px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="btnupdate" runat="server" CssClass="btn2" OnClick="btnupdate_Click"
                                        Text="Update" Width="109px" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlpayterms" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnshow" EventName="Click" />
                        <asp:PostBackTrigger ControlID="btnupdate" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 16px; text-align: center">
                &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 25px; text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 22px; text-align: left">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 8px; text-align: center">
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
</asp:Content>
