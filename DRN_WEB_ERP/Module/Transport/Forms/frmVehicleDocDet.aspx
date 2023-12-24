<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmVehicleDocDet.aspx.cs" Inherits="DRN_WEB_ERP.Module.Transport.Forms.frmVehicleDocDet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" class="titleframe">
        Vehicle Document Details</div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnSearch" BackColor="#CCFFFF">
                <div align="center">
                    <br />
                    <span>Search Vehicle:</span> <span>
                        <asp:TextBox ID="txtSearch" runat="server" Width="550px" CssClass="search textAlignCenter"></asp:TextBox>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                            DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="VslSrchList" ServicePath="~/Module/Transport/Forms/wsAutoCompleteTransport.asmx"
                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearch">
                        </cc1:AutoCompleteExtender>
                        <asp:Button ID="btnSearch" runat="server" onkeypress="return clickButton(event,'btnSearch')"
                            Text="Search" ValidationGroup="btnSearch" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnClearSrch" runat="server" Text="Clear" Width="60px" OnClick="btnClearSrch_Click"
                            Enabled="False" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSearch"
                            ErrorMessage="Enter Search Text" Font-Bold="False" ForeColor="Red" ValidationGroup="btnSearch"
                            Font-Size="10pt"></asp:RequiredFieldValidator>
                        <br />
                        <asp:HiddenField ID="hfVslRegNo" runat="server" Value="0" />
                    </span>
                    <br />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #CCFFFF">
                <asp:HyperLink ID="hlVslPic" runat="server" Target="_blank">
                    <asp:Image ID="imgVslPic" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                        Height="100px" ImageUrl="~/Image/NoImage.gif" Width="115px" /></asp:HyperLink>
                <br />
                <asp:GridView ID="gvDocDet" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" Font-Size="10pt"
                    CellSpacing="1" GridLines="None" OnRowDataBound="gvDocDet_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DOC. Ref.">
                            <ItemTemplate>
                                <asp:Label ID="lblDocRef" runat="server" Text='<%# Bind("Vsl_Doc_Ref") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DOC. Name">
                            <ItemTemplate>
                                <asp:Label ID="lblDocName" runat="server" Text='<%# Bind("Vsl_Doc_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Update Date">
                            <ItemTemplate>
                                <asp:Label ID="lblLastUpdt" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Updated As On">
                            <ItemTemplate>
                                <asp:TextBox ID="txtLastUpdt" runat="server" Width="90px" CssClass="textAlignCenter"></asp:TextBox>
                                <asp:Image ID="imgLastDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                                <cc1:CalendarExtender ID="txtLastUpdt_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd/MM/yyyy" PopupButtonID="imgLastDate" TargetControlID="txtLastUpdt">
                                </cc1:CalendarExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Next Update Date">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNextUpdt" runat="server" Width="90px" CssClass="textAlignCenter"></asp:TextBox>
                                <asp:Image ID="imgNextDate" runat="server" CssClass="inline" ImageUrl="~/Image/calendar.png" />
                                <cc1:CalendarExtender ID="txtNextUpdt_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd/MM/yyyy" PopupButtonID="imgNextDate" TargetControlID="txtNextUpdt">
                                </cc1:CalendarExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnSave" runat="server" Text="Update" OnClick="btnSave_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle ForeColor="Black" BackColor="#DEDFDE" />
                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#594B9C" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#33276A" />
                </asp:GridView>
                <br />
                <asp:GridView ID="gvVslDocDet" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="9.5pt"
                    OnRowDataBound="gvVslDocDet_RowDataBound" OnSelectedIndexChanged="gvVslDocDet_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vsl. Reg. No">
                            <ItemTemplate>
                                <asp:Label ID="lblVslRegNo" runat="server" Text='<%# Bind("Vsl_Mas_No") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vsl. Type">
                            <ItemTemplate>
                                <asp:Label ID="lblVslType" runat="server" Text='<%# GetVslType(Eval("Vsl_Mas_Ext_Type").ToString())%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Vsl_Mas_Brand" HeaderText="Brand" />
                        <asp:BoundField DataField="Vsl_Mas_Model" HeaderText="Model" />
                        <asp:TemplateField HeaderText="Registration">
                            <ItemTemplate>
                                <asp:Label ID="lblReg" runat="server" Text='<%# GetDocDate(Eval("Vsl_Mas_No").ToString(),("DOC-101").ToString())%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#33CC33" />
                            <ItemStyle HorizontalAlign="Center" BackColor="#33CC33" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Next Date">
                            <ItemTemplate>
                                <asp:Label ID="lblRegNext" runat="server" Text='<%# GetDocNextDate(Eval("Vsl_Mas_No").ToString(),("DOC-101").ToString())%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TAX Token">
                            <ItemTemplate>
                                <asp:Label ID="lblTax" runat="server" Text='<%# GetDocDate(Eval("Vsl_Mas_No").ToString(),("DOC-102").ToString())%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#33CC33" />
                            <ItemStyle BackColor="#33CC33" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Next Date">
                            <ItemTemplate>
                                <asp:Label ID="lblTaxNext" runat="server" Text='<%# GetDocNextDate(Eval("Vsl_Mas_No").ToString(),("DOC-102").ToString())%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Insurance">
                            <ItemTemplate>
                                <asp:Label ID="lblInsu" runat="server" Text='<%# GetDocDate(Eval("Vsl_Mas_No").ToString(),("DOC-103").ToString())%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#33CC33" />
                            <ItemStyle BackColor="#33CC33" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Next Date">
                            <ItemTemplate>
                                <asp:Label ID="lblInsuNext" runat="server" Text='<%# GetDocNextDate(Eval("Vsl_Mas_No").ToString(),("DOC-103").ToString())%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fitness">
                            <ItemTemplate>
                                <asp:Label ID="lblFit" runat="server" Text='<%# GetDocDate(Eval("Vsl_Mas_No").ToString(),("DOC-104").ToString())%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#33CC33" />
                            <ItemStyle BackColor="#33CC33" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Next Date">
                            <ItemTemplate>
                                <asp:Label ID="lblFitNext" runat="server" Text='<%# GetDocNextDate(Eval("Vsl_Mas_No").ToString(),("DOC-104").ToString())%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Road Permit">
                            <ItemTemplate>
                                <asp:Label ID="lblRoadPer" runat="server" Text='<%# GetDocDate(Eval("Vsl_Mas_No").ToString(),("DOC-105").ToString())%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle BackColor="#33CC33" />
                            <ItemStyle BackColor="#33CC33" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Next Date">
                            <ItemTemplate>
                                <asp:Label ID="lblRoadPerNext" runat="server" Text='<%# GetDocNextDate(Eval("Vsl_Mas_No").ToString(),("DOC-105").ToString())%>'></asp:Label>
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
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
