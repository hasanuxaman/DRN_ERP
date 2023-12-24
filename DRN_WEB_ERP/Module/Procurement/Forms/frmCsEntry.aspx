<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmCsEntry.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmCsEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="Javascript">

        function CalGrandTot(CryCrg, LodCrg, DisAmt, QtnAmt, GndTot) {
            var cryCrg = 0;
            if (document.getElementById(CryCrg).value != '') {
                cryCrg = parseFloat(document.getElementById(CryCrg).value);
            }
            //alert("Carry Charge: " + cryCrg.toString());

            var lodCrg = 0;
            if (document.getElementById(LodCrg).value != '') {
                lodCrg = parseFloat(document.getElementById(LodCrg).value);
            }
            //alert("Load Charge: " + lodCrg.toString());

            var disAmt = 0;
            if (document.getElementById(DisAmt).value != '') {
                disAmt = parseFloat(document.getElementById(DisAmt).value);
            }
            //alert("Discount: " + disAmt.toString());

            var qtnAmt = 0;
            qtnAmt = parseFloat(QtnAmt);
            //alert("QTN Amount: " + qtnAmt.toString());

            var gndTot = document.getElementById(GndTot);
            var GrandTotal = (qtnAmt + cryCrg + lodCrg) - disAmt;
            //alert("Grand Total:" + GrandTotal.toString());
            gndTot.innerHTML = GrandTotal.toFixed(2);
        }

    </script>
    <div align="center" style="background-color: #00FF99">
        C/S Entry</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #CC66FF">
                <br />
                Priority:
                <asp:CheckBox ID="chkurgent" runat="server" ForeColor="#CC0000" Text="Urgent" Font-Bold="True" />
                <br />
                <br />
                <asp:GridView ID="gvCS" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge"
                    BorderWidth="2px" CellPadding="3" CellSpacing="1" Font-Size="8pt" GridLines="None"
                    OnRowDataBound="gvCS_RowDataBound" OnRowCreated="gvCS_RowCreated" ShowFooter="True">
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" Font-Bold="True" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="true" Font-Size="8pt" ForeColor="#E7E7FF" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#594B9C" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#33276A" />
                </asp:GridView>
                <br />
                <asp:GridView ID="gvCsSum" runat="server" CellPadding="4" ForeColor="#333333" Font-Size="8pt"
                    AutoGenerateColumns="False" OnRowDataBound="gvCsSum_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Supplier Ref.">
                            <ItemTemplate>
                                <asp:Label ID="lblSupRef" runat="server" Text='<%# Bind("Qtn_Par_Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Supplier Name" DataField="Par_Adr_Qtn_Name" />
                        <asp:TemplateField HeaderText="QTN Value">
                            <ItemTemplate>
                                <asp:Label ID="lblQtnVal" runat="server" Text='<%#  Bind("Qtn_Val", "{0:F2}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Carrying Charge">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCaryCrg" runat="server" Width="100px" CssClass="textAlignCenter"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtCaryCrg_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCaryCrg"
                                    ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Loading Charge">
                            <ItemTemplate>
                                <asp:TextBox ID="txtLoadCrg" runat="server" Width="100px" CssClass="textAlignCenter"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtLoadCrg_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtLoadCrg"
                                    ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Discount Amt">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDisAmt" runat="server" Width="100px" CssClass="textAlignCenter"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtDisAmt_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtDisAmt"
                                    ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grand Total">
                            <ItemTemplate>
                                <asp:Label ID="lblGrandTot" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" BackColor="#00CC66" Font-Bold="True" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="8pt" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <br />
                <asp:TextBox ID="txtComments" runat="server" Height="100px" TextMode="MultiLine"
                    Width="500px"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnCsPost" runat="server" OnClick="btnCsPost_Click" Text="Post" Width="120px" />
                <br />
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCsPost" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="Panel4" runat="server" BorderStyle="Solid" BorderWidth="2px" CssClass="tbl"
        DefaultButton="btncancel" Height="200px" ScrollBars="Auto" Style="border-right: black 2px solid;
        display: none; padding-right: 20px; border-top: black 2px solid; padding-left: 20px;
        padding-bottom: 20px; border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
        background-color: white" Width="329px">
        <div style="border-color: #e6e6fa; border-width: 1px; filter: progid:dximagetransform.microsoft.gradient(endcolorstr='#ffffff', startcolorstr='#e6e6fa', gradienttype='0');
            width: 94%; height: 177px; text-align: center;">
            &nbsp;&nbsp;<table id="Table1" runat="server" style="width: 286px">
                <tr>
                    <td colspan="1" style="width: 364px; height: 18px; text-align: center">
                        <span style="color: #ff0000"><strong>CS CREATED SUCCESSFULLY</strong></span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 364px; height: 13px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 364px; text-align: center">
                        CS REF NO:
                    </td>
                </tr>
                <tr>
                    <td style="width: 364px; text-align: center">
                        &nbsp;
                        <asp:Label ID="lblcsref" runat="server" Font-Bold="True" Width="162px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="1" style="height: 19px; text-align: center; width: 364px;">
                    </td>
                </tr>
                <tr>
                    <td colspan="1" style="width: 364px; height: 29px; text-align: center">
                        <asp:Button ID="btncancel" runat="server" CssClass="btn2" Width="0px" Height="0px" />
                        <asp:Button ID="btnok" runat="server" CssClass="btn2" OnClick="btnok_Click" Text="OK"
                            Width="102px" />
                        <asp:Button ID="btnprint" runat="server" CssClass="btn2" OnClick="btnprint_Click"
                            Text="PRINT" Width="102px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender5" runat="server" DisplayModalPopupID="ModalPopupExtender5"
        TargetControlID="Button1">
    </cc1:ConfirmButtonExtender>
    <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btncancel" PopupControlID="Panel4" TargetControlID="Button1">
    </cc1:ModalPopupExtender>
    <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" />
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
