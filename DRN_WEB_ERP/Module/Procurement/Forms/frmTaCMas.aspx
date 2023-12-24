<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmTaCMas.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmTaCMas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="HTMLEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updateall" runat="server">
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
                        TERMS AND CONDITION MASTER SCREEN
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                        <table style="width: 61%;">
                            <tr>
                                <td style="width: 86px">
                                    &nbsp;
                                </td>
                                <td style="width: 15px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 86px; text-align: left;">
                                    TnC ID
                                </td>
                                <td style="width: 15px">
                                    :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtid" runat="server" CssClass="txtbox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 86px; text-align: left;">
                                    TYPE
                                </td>
                                <td style="width: 15px">
                                    :
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddltype" runat="server" CssClass="txtbox">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="GEN">Gen Terms</asp:ListItem>
                                        <asp:ListItem Value="SPE">Special Terms</asp:ListItem>
                                        <asp:ListItem Value="PAY">Pay Terms</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 86px; text-align: left;">
                                    SEQ NO
                                </td>
                                <td style="width: 15px">
                                    :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtseq" runat="server" CssClass="txtbox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 86px; text-align: left;">
                                    CATEGORY
                                </td>
                                <td style="width: 15px">
                                    :
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlcat" runat="server" Style="text-align: left">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="Com">Common</asp:ListItem>
                                        <asp:ListItem Value="Full">Full Advance</asp:ListItem>
                                        <asp:ListItem Value="Part">Part Advance</asp:ListItem>
                                        <asp:ListItem Value="No">No Advance</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                        DETAIL
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                        <HTMLEditor:Editor runat="server" ID="txteditor" Height="200px" AutoFocus="true"
                            Width="100%" />
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tbl" colspan="3" style="height: 24px; text-align: center">
                        <asp:Button ID="btndel" runat="server" CssClass="btn2" OnClick="btndel_Click" Text="Delete"
                            Width="98px" />
                        <asp:Button ID="btnadd" runat="server" CssClass="btn2" OnClick="btnadd_Click" Text="Add/Update"
                            Width="104px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" class="tdcell" align="center">
                        <asp:GridView ID="gdtac" runat="server" BackColor="White" BorderColor="#6B7EBF" BorderStyle="Solid"
                            BorderWidth="1px" CellPadding="4" ForeColor="#333333" OnRowDataBound="gdtac_RowDataBound"
                            OnSelectedIndexChanged="gdtac_SelectedIndexChanged" PageSize="100" SkinID="GridView"
                            Width="90%" Font-Size="8pt" AutoGenerateColumns="False">
                            <FooterStyle BackColor="#6B7EBF" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#6B7EBF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="LightBlue" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#6B7EBF" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="Lavender" />
                            <Columns>
                                <asp:BoundField DataField="TAC_ID" HeaderText="TAC_ID">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TYPE" HeaderText="TYPE">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SEQ" HeaderText="SEQ">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CAT" HeaderText="CAT">
                                    <ItemStyle Width="30px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="DETAILS">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("DETAILS") %>' Width="750px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="750px" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                        </asp:GridView>
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
    </asp:UpdatePanel>
</asp:Content>
