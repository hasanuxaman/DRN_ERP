<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmProdEntry.aspx.cs" Inherits="DRN_WEB_ERP.Module.Production.Forms.frmProdEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CalcWstPerc(wstQty, rmQty, wstPerc, totQty) {
            var WastageQty = 0;
            if (document.getElementById(wstQty).value != '') {
                WastageQty = parseFloat(document.getElementById(wstQty).value);
            }
            //alert(WastageQty.toString());

            var RmIssueQty = 0;
            if (document.getElementById(rmQty).value != '') {
                RmIssueQty = parseFloat(document.getElementById(rmQty).value);
            }
            //alert(RmIssueQty.toString());

            var LblWastagePerc = document.getElementById(wstPerc);
            var WastagePercent = parseFloat((WastageQty * 100) / RmIssueQty);
            //alert(WastagePercent.toString());
            LblWastagePerc.innerHTML = WastagePercent.toFixed(2);

            var LblTotalQty = document.getElementById(totQty);
            var TotalQty = parseFloat(RmIssueQty + WastageQty);
            //alert(TotalQty.toString());
            LblTotalQty.value = TotalQty.toFixed(2);
        }                     
    </script>
    <div align="center" style="background-color: #00FF99">
        Production Entry</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="border: 1px solid #CCCCCC; background-color: #CCFFFF;">
                <div align="center">
                    <table style="width: 100%; font-size: 12px;">
                        <tr>
                            <td style="width: 99px">
                                &nbsp;
                            </td>
                            <td style="width: 194px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td style="width: 197px">
                                &nbsp;
                            </td>
                            <td style="width: 98px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td style="width: 351px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 99px">
                                &nbsp;
                            </td>
                            <td align="right" style="width: 194px">
                                Production Date
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" style="width: 197px">
                                <asp:TextBox ID="txtProdDate" runat="server" CssClass="textAlignCenter" ForeColor="Blue"
                                    Enabled="false" Width="150px"></asp:TextBox>
                                <asp:Image ID="imgReqDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                <cc1:CalendarExtender ID="CalenderDelDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    PopupButtonID="imgReqDt" TargetControlID="txtProdDate">
                                </cc1:CalendarExtender>
                            </td>
                            <td align="right" style="width: 98px">
                                &nbsp;Item Name
                            </td>
                            <td width="2">
                                :
                            </td>
                            <td align="left" style="width: 351px">
                                <asp:DropDownList ID="ddlProdItem" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProdItem_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 99px">
                                &nbsp;
                            </td>
                            <td style="width: 194px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td style="width: 197px">
                                &nbsp;
                            </td>
                            <td style="width: 98px">
                                &nbsp;
                            </td>
                            <td width="2">
                                &nbsp;
                            </td>
                            <td style="width: 351px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlFg0" runat="server" GroupingText="Finish Goods">
                        <br />
                        <asp:GridView ID="gvProdFg" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="8pt"
                            EmptyDataText="There is no Finish Goods found for this production item." OnRowDataBound="gvProdFg_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFgItmCode" runat="server" Text='<%# Bind("Itm_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFgItmName" runat="server" Text='<%# Bind("Itm_Desc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFgItmUom" runat="server" Text='<%# Bind("Itm_Uom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Store">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlFgStore" runat="server" DataSourceID="SqlDataSource3" DataTextField="Str_Loc_Name"
                                            DataValueField="Str_Loc_Ref">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DRNConStr %>"
                                            SelectCommand="SELECT [Str_Loc_Ref], [Str_Loc_Name] FROM [tbl_InMa_Str_Loc] ORDER BY Str_Loc_Code">
                                        </asp:SqlDataSource>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Avl. Qty" Visible="False" />
                                <asp:TemplateField HeaderText="Production Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFgQty" runat="server" CssClass="textAlignCenter" Width="100px">
                                        </asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtFgQty_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtFgQty"
                                            ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
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
                    </asp:Panel>
                    <asp:Button ID="btnProcess" runat="server" CssClass="btn" Text="Process" ValidationGroup="Save"
                        Width="100px" OnClick="btnProcess_Click" Visible="False" />
                    <br />
                    <asp:Panel ID="pnlRm" runat="server" GroupingText="Raw material">
                        <br />
                        <asp:GridView ID="gvProdRm" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="8pt"
                            OnRowDataBound="gvProdRm_RowDataBound" Enabled="False">
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfItmType" runat="server" Value='<%# Bind("Itm_Type_Code") %>' />
                                        <asp:Label ID="lblItmCode" runat="server" Text='<%# Bind("Itm_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItmName" runat="server" Text='<%# Bind("Itm_Desc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItmUom" runat="server" Text='<%# Bind("Itm_Uom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Store">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlRmStore" runat="server" DataSourceID="SqlDataSource1" DataTextField="Str_Loc_Name"
                                            DataValueField="Str_Loc_Ref" Enabled="false">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DRNConStr %>"
                                            SelectCommand="SELECT [Str_Loc_Ref], [Str_Loc_Name] FROM [tbl_InMa_Str_Loc] ORDER BY Str_Loc_Code">
                                        </asp:SqlDataSource>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Avl. Qty" Visible="False" />
                                <asp:TemplateField HeaderText="STD Ratio (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStdRatio" runat="server" Text='<%# Bind("Prod_Std_Ratio") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="STD RM Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRmIssue" runat="server" CssClass="textAlignCenter" Width="100px"
                                            Enabled="false">
                                        </asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtRmIssue_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtRmIssue"
                                            ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Wastage (%)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWstPerc" runat="server" Text='<%# Bind("Prod_Wast_Ratio") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Wastage Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWastageQty" runat="server" CssClass="textAlignCenter" Width="100px"
                                            Enabled="false">
                                        </asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtWastageQty_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtWastageQty"
                                            ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total RM Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTotRmQty" runat="server" CssClass="textAlignCenter" Width="100px"
                                            Enabled="false">
                                        </asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtTotRmQty_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtTotRmQty"
                                            ValidChars=".">
                                        </cc1:FilteredTextBoxExtender>
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
                    </asp:Panel>
                    <br />
                    <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="Clear" Width="100px"
                        OnClick="btnClear_Click" />
                    <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" ValidationGroup="Save"
                        Width="100px" Visible="False" OnClick="btnSave_Click" />
                    <br />
                    <br />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlProdItem" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnProcess" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
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
