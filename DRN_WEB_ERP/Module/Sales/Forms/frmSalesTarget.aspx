﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmSalesTarget.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesTarget" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #00FF99">
                Sales Target Setup - DSM</div>
            <asp:Panel ID="pnlSearchBox" runat="server">
                <div align="center" style="background-color: #86AEAE">
                    <br />
                    <asp:TextBox ID="txtTrgtDate" runat="server" Width="120px" Enabled="false" AutoPostBack="true"
                        CssClass="textAlignCenter" OnTextChanged="txtTrgtDate_TextChanged"></asp:TextBox>
                    <asp:Image ID="imgTrgtDate" runat="server" ImageUrl="~/Image/calendar.png" />
                    <cc1:CalendarExtender ID="txtTrgtDate_CalendarExtender" runat="server" Enabled="True"
                        Format="MMMM/yyyy" PopupButtonID="imgTrgtDate" TargetControlID="txtTrgtDate">
                    </cc1:CalendarExtender>
                    <br />
                    <br />
                    <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export To Excel" />
                    <br />
                </div>
                <div align="center" style="background-color: #86AEAE">
                    <asp:GridView ID="gvSalesTarget" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Size="10pt" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DSM Ref">
                                <ItemTemplate>
                                    <asp:Label ID="lblDsmRef" runat="server" Text='<%# Bind("Dsm_Ref") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DSM Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblDsmName" runat="server" Text='<%# Bind("Dsm_Full_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sales Zone">
                                <ItemTemplate>
                                    <asp:Label ID="lblSalesZone" runat="server" Text='<%# Bind("SalesZoneName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Regular">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfItemCodeRegular" runat="server" Value="100002" />
                                    <asp:HiddenField ID="hfItemNameRegular" runat="server" Value="Bag Cement - Regular" />
                                    <asp:TextBox ID="txtTrgReg" runat="server" Width="100px" CssClass="textAlignRight"
                                        Text='<%# GetSlsTrgt(Eval("Dsm_Ref").ToString(),("100002").ToString())%>'></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtTrgReg"
                                        ErrorMessage="Enter Valid Quantity" ForeColor="Red" Operator="DataTypeCheck"
                                        Type="Currency" ValidationGroup="Save">*</asp:CompareValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supreme">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfItemCodeSupreme" runat="server" Value="100001" />
                                    <asp:HiddenField ID="hfItemNameSupreme" runat="server" Value="Bag Cement - Supreme" />
                                    <asp:TextBox ID="txtTrgtSup" runat="server" Width="100px" CssClass="textAlignRight"
                                        Text='<%# GetSlsTrgt(Eval("Dsm_Ref").ToString(),("100001").ToString())%>'></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtTrgtSup"
                                        ErrorMessage="Enter Valid Quantity" ForeColor="Red" Operator="DataTypeCheck"
                                        Type="Currency" ValidationGroup="Save">*</asp:CompareValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eastern (PCC)">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfItemCodeEasternP" runat="server" Value="100003" />
                                    <asp:HiddenField ID="hfItemNameEasternP" runat="server" Value="Bag Cement - Eastern (PCC)" />
                                    <asp:TextBox ID="txtTrgtEstPcc" runat="server" Width="100px" CssClass="textAlignRight"
                                        Text='<%# GetSlsTrgt(Eval("Dsm_Ref").ToString(),("100003").ToString())%>'></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtTrgtEstPcc"
                                        ErrorMessage="Enter Valid Quantity" ForeColor="Red" Operator="DataTypeCheck"
                                        Type="Currency" ValidationGroup="Save">*</asp:CompareValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eastern (OPC)">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfItemCodeEasternO" runat="server" Value="100004" />
                                    <asp:HiddenField ID="hfItemNameEasternO" runat="server" Value="Bag Cement - Eastern (OPC)" />
                                    <asp:TextBox ID="txtTrgtEstOpc" runat="server" Width="100px" CssClass="textAlignRight"
                                        Text='<%# GetSlsTrgt(Eval("Dsm_Ref").ToString(),("100004").ToString())%>'></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtTrgtEstOpc"
                                        ErrorMessage="Enter Valid Quantity" ForeColor="Red" Operator="DataTypeCheck"
                                        Type="Currency" ValidationGroup="Save">*</asp:CompareValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Collection">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfItemCodeCollection" runat="server" Value="100000" />
                                    <asp:HiddenField ID="hfItemNameCollection" runat="server" Value="Collection" />
                                    <asp:TextBox ID="txtTrgtCollect" runat="server" Width="120px" CssClass="textAlignRight"
                                        Text='<%# GetSlsTrgt(Eval("Dsm_Ref").ToString(),("100000").ToString())%>'></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtTrgtCollect"
                                        ErrorMessage="Enter Valid Amount" ForeColor="Red" Operator="DataTypeCheck" Type="Currency"
                                        ValidationGroup="Save">*</asp:CompareValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="8pt" ForeColor="Red"
                        ValidationGroup="Save" />
                    <asp:Button ID="btnSaveAll" runat="server" OnClick="btnSaveAll_Click" onkeypress="return clickButton(event,'btnSearch')"
                        Text="Save All" Width="100px" Visible="False" ValidationGroup="Save" />
                    <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh"
                        Width="100px" />
                    <br />
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSaveAll" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="txtTrgtDate" EventName="TextChanged" />
            <asp:PostBackTrigger ControlID="btnExport" />
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
