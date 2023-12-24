<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmMprSplit.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmMprSplit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function PrintDiv() {
            var divContents = document.getElementById("dvContents").innerHTML;
            var printWindow = window.open('', '', 'height=400,width=600');
            printWindow.moveTo(0, 0);
            printWindow.resizeTo(screen.width, screen.height);
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
    </script>
    <div align="center" style="background-color: #00FF99">
        Purchase Requisition Inquiry</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #CC66FF">
                <br />
                Year:<asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
                    Width="70px">
                </asp:DropDownList>
                &nbsp;Month:<asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"
                    Width="100px">
                </asp:DropDownList>
                &nbsp;MPR. Ref. No:
                <asp:DropDownList ID="ddlMprList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMprList_SelectedIndexChanged"
                    Width="280px">
                </asp:DropDownList>
                &nbsp;Req. Ref. No:<asp:TextBox ID="txtReqRefNo" runat="server" CssClass="textAlignCenter"
                    Width="70px"></asp:TextBox>
                &nbsp;<asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                    Width="100px" />
                &nbsp;<asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print MPR"
                    Width="100px" />
                &nbsp;<br />
                <br />
            </div>
            <div align="center" style="background-color: #CCFFFF">
                <div id="dvContents" align="center" style="background-color: #CCFFFF">
                    <asp:Label ID="lblMsg" runat="server" Font-Size="10pt" ForeColor="Red" Visible="False"
                        Font-Italic="True"></asp:Label>
                    <asp:RadioButtonList ID="optMprList" runat="server" Font-Size="8pt" ForeColor="Red"
                        RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="optMprList_SelectedIndexChanged">
                    </asp:RadioButtonList>
                    <table style="width: 100%; background-color: #CCFFFF; font-size: small;">
                        <tr>
                            <td style="width: 254px">
                                &nbsp;
                            </td>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 91px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td align="center" style="width: 300px">
                                <asp:Label ID="lblMsgValidity" runat="server" Font-Bold="True" Font-Italic="True"
                                    Font-Size="10pt" ForeColor="Red" Visible="False"></asp:Label>
                            </td>
                            <td style="width: 94px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 254px">
                                &nbsp;
                            </td>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 91px">
                                Req. Ref. No
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td align="left" style="width: 300px">
                                <asp:TextBox ID="txtPoReqRef" runat="server" Width="350px" BorderStyle="Solid" CssClass="textAlignCenter"
                                    Enabled="False"></asp:TextBox>
                            </td>
                            <td style="width: 94px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 254px">
                                &nbsp;
                            </td>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 91px">
                                Date
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td align="left" style="width: 300px">
                                <asp:TextBox ID="txtPoReqDt" runat="server" Width="350px" Enabled="False" CssClass="textAlignCenter"
                                    Font-Bold="True" BorderStyle="Solid"></asp:TextBox>
                            </td>
                            <td style="width: 94px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 254px">
                                &nbsp;
                            </td>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 91px">
                                MPR Ref. No
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td align="left" style="width: 300px">
                                <asp:TextBox ID="txtPoRefNo" runat="server" BackColor="#CCCCFF" BorderStyle="Solid"
                                    CssClass="textAlignCenter" Enabled="False" Font-Bold="True" Width="350px"></asp:TextBox>
                            </td>
                            <td style="width: 94px">
                                <asp:LinkButton ID="lnkViewMpr" runat="server" OnClick="lnkViewMpr_Click" Visible="False">View</asp:LinkButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 254px">
                                &nbsp;
                            </td>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 91px">
                                Requisition By
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td align="left" style="width: 300px">
                                <asp:TextBox ID="txtPoReqBy" runat="server" CssClass="capitalize" Enabled="False"
                                    Width="350px" BorderStyle="Solid"></asp:TextBox>
                            </td>
                            <td style="width: 94px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 254px">
                                &nbsp;
                            </td>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 91px">
                                Remarks
                            </td>
                            <td style="width: 3px">
                                :
                            </td>
                            <td align="left" style="width: 300px">
                                <asp:TextBox ID="txtPoReqRem" runat="server" Font-Bold="True" Width="350px"></asp:TextBox>
                            </td>
                            <td style="width: 94px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 254px">
                                &nbsp;
                            </td>
                            <td style="width: 32px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 91px">
                                &nbsp;
                            </td>
                            <td style="width: 3px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 300px">
                                &nbsp;
                            </td>
                            <td style="width: 94px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvPoReqDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Size="7.5pt" ForeColor="#333333" OnRowDataBound="gvPoReqDet_RowDataBound">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="SL#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfPrDetLno" runat="server" Value='<%# Bind("REQ_DET_LNO") %>' />
                                    <asp:HiddenField ID="hfPrDetExpDt" runat="server" Value='<%# Bind("REQ_EXP_DT") %>' />
                                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("REQ_ITEM_REF") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("REQ_ITEM_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfItemUnit" runat="server" Value='<%# Bind("REQ_ITEM_UOM_REF") %>' />
                                    <asp:Label ID="lblItemUnit" runat="server" Text='<%# Bind("REQ_ITEM_UOM") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpec" runat="server" Text='<%# Bind("REQ_SPEC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Store">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfStore" runat="server" Value='<%# Bind("REQ_STORE_REF") %>' />
                                    <asp:Label ID="lblStore" runat="server" Text='<%# Bind("REQ_STORE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand">
                                <ItemTemplate>
                                    <asp:Label ID="lblBrand" runat="server" Text='<%# Bind("REQ_BRAND") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Origin">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrigin" runat="server" Text='<%# Bind("REQ_ORIGIN") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Packing">
                                <ItemTemplate>
                                    <asp:Label ID="lblPacking" runat="server" Text='<%# Bind("REQ_PACKING") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfLocation" runat="server" Value='<%# Bind("REQ_STORE_REF") %>' />
                                    <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("REQ_LOC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("REQ_REM") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CS Ref">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkCsRef" runat="server" Text='<%# Bind("CS_REF") %>' OnClick="lnkCsRef_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO Ref">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPoRef" runat="server" Text='<%# GetPoRef(Eval("REQ_HDR_REF").ToString(),Eval("REQ_ITEM_REF").ToString())%>'
                                        OnClick="lnkPoRef_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfReqRef" runat="server" Value='<%# Bind("REQ_REF") %>' />
                                    <asp:Label ID="lblPoReqQty" runat="server" Text='<%# Bind("REQ_QTY", "{0:c}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#FFFF66" HorizontalAlign="Center" Width="70px" Font-Bold="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Split Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSplitQty" runat="server" CssClass="textAlignCenter" Width="70px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSplit" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Wrap="False"
                            Font-Size="9pt" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
                </div>
                <br />
                <asp:Button ID="btnSplit" runat="server" OnClick="btnSplit_Click" Text="Split Selected MPR"
                    Enabled="False" />
                &nbsp;
                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                &nbsp;<input type="button" onclick="PrintDiv();" value="Print" style="width: 120px" />
                <br />
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboYear" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboMonth" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlMprList" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnSplit" EventName="Click" />
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
