<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmPoInit.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmPoInit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Purchase Order</div>
    <table class="tblmas" style="width: 100%" id="tblmaster" runat="server">
        <tr>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 17px; text-align: right">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left" valign="top">
                Vendor Name:
                <asp:DropDownList ID="ddlparty" runat="server" AutoPostBack="True" CssClass="txtbox"
                    OnSelectedIndexChanged="ddlparty_SelectedIndexChanged" Width="518px">
                </asp:DropDownList>
                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddlparty"
                    PromptCssClass="ListSearchExtenderPrompt" QueryPattern="Contains" QueryTimeout="2000">
                </ajaxToolkit:ListSearchExtender>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 15px; text-align: left">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 19px; text-align: left">
                <asp:Label ID="lbltot" runat="server" Font-Bold="True" ForeColor="Red" Text="Label"
                    Visible="False" Width="265px"></asp:Label>
                <asp:GridView ID="gdItem" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#6B7EBF" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" ForeColor="#333333"
                    GridLines="None" PageSize="100" SkinID="GridView" Style="border-top-width: 1px;
                    border-left-width: 1px; border-left-color: #e6e6fa; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='#ffffff', startcolorstr='#e6e6fa', gradienttype='0');
                    border-bottom-width: 1px; border-bottom-color: #e6e6fa; border-top-color: #e6e6fa;
                    border-right-width: 1px; border-right-color: #e6e6fa;" Width="100%" AllowSorting="True"
                    Font-Size="8pt">
                    <FooterStyle BackColor="#6B7EBF" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#6B7EBF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle Font-Bold="True" Wrap="False" />
                    <HeaderStyle BackColor="#6B7EBF" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="Lavender" Font-Size="8pt" Font-Underline="False"
                        Wrap="False" />
                    <RowStyle Font-Size="8pt" Wrap="False" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sel">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ref">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Party">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qnty">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Avl Qty">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO Qty">
                            <ItemTemplate>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    FilterType="Custom, Numbers" TargetControlID="TextBox1" ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="txtbox" Width="50px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Specification">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Brand">
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Origin">
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Packing">
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="text-align: left">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
            </td>
        </tr>
        <tr>
            <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                <asp:Button ID="btncreate" runat="server" CssClass="btn2" OnClick="btncreate_Click"
                    Text="Proceed" Visible="False" Width="133px" />
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
