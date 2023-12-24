<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmProdRecipe.aspx.cs" Inherits="DRN_WEB_ERP.Module.Production.Forms.frmProdRecipe" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Production Recipe Setup</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="border: 1px solid #CCCCCC; background-color: #CCFFFF;">
                <div align="center">
                    <br />
                    Production Item:<asp:DropDownList ID="ddlProdItem" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlProdItem_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:GridView ID="gvRecipe" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="10pt"
                        OnRowDataBound="gvRecipe_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblItmCode" runat="server" Text='<%# Bind("Itm_Det_Ref") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblItmName" runat="server" Text='<%# Bind("Itm_Det_Desc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate>
                                    <asp:Label ID="lblItmUom" runat="server" Text='<%# Bind("Itm_Det_Stk_Unit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblItmType" runat="server" Text='<%# Bind("Itm_Det_Type") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Type Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblItmTypeName" runat="server" Text='<%# Bind("Itm_Det_T_C1") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RM">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRm" runat="server" AutoPostBack="true" OnCheckedChanged="chkRm_CheckedChanged" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FG">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkFg" runat="server" AutoPostBack="true" OnCheckedChanged="chkFg_CheckedChanged" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RM Ratio (%)">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRmRatio" runat="server" CssClass="textAlignCenter" Width="80px">
                                    </asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtRmRatio_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtRmRatio"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Wastage (%)">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRmWastage" runat="server" CssClass="textAlignCenter" Width="80px">
                                    </asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtRmWastage_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtRmWastage"
                                        ValidChars=".">
                                    </cc1:FilteredTextBoxExtender>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Store">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlStore" runat="server" DataSourceID="SqlDataSource3" DataTextField="Str_Loc_Name"
                                        DataValueField="Str_Loc_Ref">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DRNConStr %>"
                                        SelectCommand="SELECT [Str_Loc_Ref], [Str_Loc_Name] FROM [tbl_InMa_Str_Loc] ORDER BY Str_Loc_Code">
                                    </asp:SqlDataSource>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="Clear" Width="100px"
                        OnClick="btnClear_Click" />
                    <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" ValidationGroup="Save"
                        Width="100px" OnClick="btnSave_Click" Visible="False" />
                    <br />
                    <br />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlProdItem" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:PostBackTrigger ControlID="gvRecipe" />
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
