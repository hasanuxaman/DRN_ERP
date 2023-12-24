<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmCsAppr.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmCsAppr" %>

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
        C/S Approval</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #00FFFF">
                <br />
                <br />
                <div>
                    Pending List:&nbsp;<asp:Label ID="lblcount" runat="server" Text="(0)"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ddllist" runat="server" AutoPostBack="True" CssClass="txtbox"
                        OnSelectedIndexChanged="ddllist_SelectedIndexChanged" Width="500px">
                    </asp:DropDownList>
                    &nbsp;<asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh"
                        Width="80px" />
                    &nbsp;<asp:Button ID="btnQtnPrint" runat="server" OnClick="btnQtnPrint_Click" Text="Print"
                        Width="80px" />
                </div>
                <br />
                <asp:GridView ID="gvCS" runat="server" CellPadding="4" Font-Size="8pt" OnRowDataBound="gvCS_RowDataBound"
                    OnRowCreated="gvCS_RowCreated" ShowFooter="True" ForeColor="#333333">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <%--<HeaderTemplate>
                                <asp:Button ID="btnAppAll" runat="server" Text="All" OnClick="btnAppAll_Click" />
                            </HeaderTemplate>--%>
                            <ItemTemplate>
                                <%--<asp:Button ID="btnApp" runat="server" Text="Recomend" />--%>
                                <asp:LinkButton ID="lntQtnDet" runat="server" OnClick="lntQtnDet_Click">Details</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <%--<HeaderTemplate>
                                <asp:Button ID="btnRejAll" runat="server" Text="All" />
                            </HeaderTemplate>--%>
                            <ItemTemplate>
                                <%--<asp:Button ID="btnRej" runat="server" Text="Reject" />--%>
                                <asp:CheckBox ID="chkCsItem" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="true" Font-Size="8pt" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <br />
                <asp:Panel ID="PNL" runat="server" Style="border-right: black 2px solid; padding-right: 20px;
                    display: none; border-top: black 2px solid; padding-left: 20px; padding-bottom: 20px;
                    border-left: black 2px solid; width: 500px; padding-top: 20px; border-bottom: black 2px solid;
                    background-color: white" Height="224px" Width="500px" BorderStyle="Solid" BorderWidth="2px">
                    <br />
                    <table style="width: 100%; font-size: 14px; font-weight: bold; height: 80%;" bgcolor="#CC99FF">
                        <tr>
                            <td style="text-align: center; font-size: 14px; font-style: italic;" valign="top"
                                colspan="2">
                                Select Supplier
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; width: 132px;" valign="top">
                                &nbsp;
                            </td>
                            <td style="text-align: left" valign="top">
                                <asp:RadioButtonList ID="optListSupp" runat="server" Font-Bold="True" Font-Size="10pt"
                                    ForeColor="Blue">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                &nbsp;
                                <asp:Button ID="ButtonOk" runat="server" CssClass="btn2" OnClick="ButtonOk_Click"
                                    Text="OK" Width="80px" />
                                <asp:Button ID="ButtonCancel" runat="server" CssClass="btn2" Text="Cancel" Width="82px" />
                                <asp:HiddenField ID="hfPrDetLno" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="ButtonCancel" PopupControlID="PNL" TargetControlID="Button2">
                </cc1:ModalPopupExtender>
                <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" DisplayModalPopupID="ModalPopupExtender1"
                    TargetControlID="Button2">
                </cc1:ConfirmButtonExtender>
                <asp:Button ID="Button2" runat="server" Visible="false" />
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
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Font-Size="8pt" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
                <br />
                <asp:Panel ID="Panel1" runat="server" GroupingText="Comments">
                    <div align="left">
                        <asp:PlaceHolder ID="phcomments" runat="server"></asp:PlaceHolder>
                    </div>
                    <br />
                    <asp:TextBox ID="txtcomments" runat="server" Height="100px" TextMode="MultiLine"
                        Width="500px" Visible="False"></asp:TextBox>
                    <br />
                </asp:Panel>
                <br />
                <br />
                <asp:Button ID="btnRej" runat="server" CssClass="btn2" OnClick="btnRej_Click" Text="Reject"
                    Width="100px" Visible="false" />
                &nbsp;<asp:Button ID="btnApp" runat="server" CssClass="btn2" OnClick="btnApp_Click"
                    Text="Recommend" Width="100px" Visible="false" />
                <br />
                <br />
            </div>
            <asp:Panel ID="Panel2" runat="server" Style="border-right: black 2px solid; padding-right: 20px;
                display: none; border-top: black 2px solid; padding-left: 20px; padding-bottom: 20px;
                border-left: black 2px solid; width: 500px; padding-top: 20px; border-bottom: black 2px solid;
                background-color: white" Height="224px" Width="500px" BorderStyle="Solid" BorderWidth="2px">
                <br />
                <table style="width: 100%; font-size: 14px; font-weight: bold; height: 80%;" bgcolor="#CC99FF">
                    <tr>
                        <td style="text-align: center; font-size: 14px; font-style: italic;" valign="top">
                            Confirmation
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; width: 132px;" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" valign="top">
                            Are you sure want to reject ?
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; width: 132px;" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;
                            <asp:Button ID="btnRejOk" runat="server" CssClass="btn2" OnClick="btnRejOk_Click"
                                Text="OK" Width="80px" />
                            <asp:Button ID="btnRejCancel" runat="server" CssClass="btn2" Text="Cancel" Width="82px" />
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                        </td>
                    </tr>
                </table>
                <br />
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                CancelControlID="ButtonCancel" PopupControlID="Panel2" TargetControlID="Button4">
            </cc1:ModalPopupExtender>
            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender5" runat="server" DisplayModalPopupID="ModalPopupExtender2"
                TargetControlID="Button4">
            </cc1:ConfirmButtonExtender>
            <asp:Button ID="Button4" runat="server" Visible="false" />
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
