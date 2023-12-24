<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmProdItem.aspx.cs" Inherits="DRN_WEB_ERP.Module.Production.Forms.frmProdItem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Production Item Entry</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="border: 1px solid #CCCCCC; background-color: #CCFFFF;">
                <div align="center">
                    <table style="width: 100%; font-size: 12px;">
                        <tr>
                            <td style="width: 5px">
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 2px">
                                &nbsp;
                            </td>
                            <td>
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
                            <td width="2">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 67px">
                                &nbsp;
                            </td>
                            <td style="width: 2px">
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
                            <td align="left" style="width: 5px">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 42px">
                                Code
                            </td>
                            <td align="right" style="width: 2px">
                                :
                            </td>
                            <td align="right" style="width: 42px">
                                <asp:TextBox ID="txtProdItemRefNo" runat="server" CssClass="textAlignCenter" Enabled="False"
                                    Font-Bold="True" ForeColor="#3333CC" Width="50px"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 42px">
                                Name
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" style="width: 263px">
                                <asp:TextBox ID="txtProdItemName" runat="server" Width="250px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProdItemName"
                                    ErrorMessage="Enter Production Item Name First" Font-Size="8pt" ForeColor="Red"
                                    ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                            <td align="right" style="width: 30px">
                                Type
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" style="width: 100px">
                                <asp:DropDownList ID="ddlProdType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProdType_SelectedIndexChanged">
                                    <asp:ListItem Value="0">---Select---</asp:ListItem>
                                    <asp:ListItem Value="P">Bag</asp:ListItem>
                                    <asp:ListItem Value="B">Bulk</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlProdType"
                                    ErrorMessage="Select Production Item Type" Font-Size="8pt" ForeColor="Red" ValidationGroup="Save"
                                    InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                            <td align="left" style="width: 67px">
                                Match Item
                            </td>
                            <td align="left" style="width: 2px">
                                :
                            </td>
                            <td align="left" style="width: 250px">
                                <asp:DropDownList ID="cboItem" runat="server" Width="230px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cboItem"
                                    ErrorMessage="Select Match Item" Font-Size="8pt" ForeColor="Red" ValidationGroup="Save"
                                    InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:Button ID="btnClear" runat="server" CssClass="btn" OnClick="btnClear_Click"
                                    Text="Clear" Width="70px" />
                                <asp:Button ID="btnSave" runat="server" CssClass="btn" OnClick="btnSave_Click" Text="Save"
                                    ValidationGroup="Save" Width="70px" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 5px">
                            </td>
                            <td align="right">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 2px">
                                &nbsp;
                            </td>
                            <td align="right">
                                &nbsp;
                            </td>
                            <td align="right">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 263px">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="8pt" ForeColor="Red"
                                    ValidationGroup="Save" />
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
                            <td style="width: 67px">
                                &nbsp;
                            </td>
                            <td style="width: 2px">
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
                    <br />
                    <asp:GridView ID="gvProdItem" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" EmptyDataText="There is no Production item."
                        Font-Size="10pt" OnRowDataBound="gvProdItem_RowDataBound" OnSelectedIndexChanged="gvProdItem_SelectedIndexChanged">
                        <Columns>
                            <%--<asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Ref No">
                                <ItemTemplate>
                                    <asp:Label ID="lblItmRefNo" runat="server" Text='<%# Bind("Prod_Ref") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblItmCode" runat="server" Text='<%# Bind("Prod_Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblItmName" runat="server" Text='<%# Bind("Prod_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblItmType" runat="server" Text='<%# Bind("Prod_Type") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Match Item">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfMatchItem" runat="server" Value='<%# Bind("Prod_Ccode") %>' />
                                    <asp:Label ID="lblMatchItem" runat="server" Text='<%# Bind("Prod_Dcode") %>'></asp:Label>
                                </ItemTemplate>
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
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlProdType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvProdItem" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvProdItem" EventName="RowDataBound" />
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
