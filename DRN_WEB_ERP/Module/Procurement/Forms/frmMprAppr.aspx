<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmMprAppr.aspx.cs" Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmMprAppr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //Highlight Row when checkbox is checked
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua
                row.style.backgroundColor = "aqua";
            }
            else {
                //If not checked change back to original color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    //row.style.backgroundColor = "#C2D69B";
                    row.style.backgroundColor = "white";
                }
                else {
                    row.style.backgroundColor = "#EFF3FB";

                }
            }

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }

        //Check all checkboxes functionality
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            //row.style.backgroundColor = "#C2D69B";
                            row.style.backgroundColor = "white";

                        }
                        else {
                            row.style.backgroundColor = "#EFF3FB";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }

        //Highlight GridView row on mouseover event
        function MouseEvents(objRef, evt) {
            var checkbox = objRef.getElementsByTagName("input")[0];
            if (evt.type == "mouseover") {
                objRef.style.backgroundColor = "orange";
            }
            else {
                if (checkbox.checked) {
                    objRef.style.backgroundColor = "aqua";
                }
                else if (evt.type == "mouseout") {
                    if (objRef.rowIndex % 2 == 0) {
                        //Alternating Row Color
                        objRef.style.backgroundColor = "white";

                    }
                    else {
                        objRef.style.backgroundColor = "#EFF3FB";
                    }
                }
            }
        }
    </script>
    <div align="center" style="background-color: #00FF99">
        Pending Purchase Requisition
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <br />
                <asp:Button ID="btnAppAll" runat="server" Text="Proceed Selected" OnClick="btnAppAll_Click"
                    Visible="False" />
                <br />
                <br />
                <asp:GridView ID="gvPendPr" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" Font-Size="8pt" OnRowCommand="gvPendPr_RowCommand" OnRowDataBound="gvPendPr_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ShowHeader="True">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkMprAll" runat="server" onclick="checkAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkMpr" runat="server" onclick="Check_Click(this)" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MPR Ref. No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfPrRef" runat="server" Value='<%# Bind("PR_Hdr_Ref") %>' />
                                <asp:HiddenField ID="hfPrDetLno" runat="server" Value='<%# Bind("PR_Det_Lno") %>' />
                                <asp:Label ID="lblPrRefNo" runat="server" Text='<%# Bind("PR_Hdr_Ref") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="65px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MPR Date">
                            <ItemTemplate>
                                <asp:Label ID="lblPrDt" runat="server" Text='<%# Bind("PR_Hdr_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfPrItemCode" runat="server" Value='<%# Bind("PR_Det_Icode") %>' />
                                <asp:Label ID="lblPrItemName" runat="server" Text='<%# Bind("PR_Det_Itm_Desc") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#FFCCFF" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblItemUnit" runat="server" Text='<%# Bind("PR_Det_Itm_Uom") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Bold="True" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Pr_Det_Spec" HeaderText="Specification" />
                        <asp:BoundField DataField="Pr_Det_Brand" HeaderText="Brand" />
                        <asp:BoundField DataField="Pr_Det_Origin" HeaderText="Origin" />
                        <asp:TemplateField HeaderText="Last Pur. Dt">
                            <ItemTemplate>
                                <asp:Label ID="Label1" Font-Bold="True" runat="server" Text='<%# GetLastPurDt(Eval("PR_Det_Icode").ToString())%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#CCCCCC" HorizontalAlign="Right" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Pur. Qty">
                            <ItemTemplate>
                                <asp:Label ID="Label2" Font-Bold="True" runat="server" Text='<%# GetLastPurQty(Eval("PR_Det_Icode").ToString())%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#CCCCCC" HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Pur. Rate">
                            <ItemTemplate>
                                <asp:Label ID="Label3" Font-Bold="true" runat="server" Text='<%# GetLastPurRate(Eval("PR_Det_Icode").ToString())%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#CCCCCC" HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Last Pur. Party">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# GetLastPurSup(Eval("PR_Det_Icode").ToString())%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#CCCCCC" HorizontalAlign="Left" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Del. Due Date">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("PR_Det_Exp_Dat","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#CC99FF" HorizontalAlign="Left" Font-Bold="True" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Req. Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblPrReqQty" runat="server" Text='<%# Bind("PR_Det_Unt_Wgt") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="Aqua" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stock">
                            <ItemTemplate>
                                <asp:Label ID="lblStock" runat="server" Text='<%# GetCurStk(Eval("PR_Det_Icode").ToString())%>'
                                    Font-Bold="True"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle BackColor="#FF9900" Width="55px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPrQty" runat="server" Width="60px" CssClass="textAlignCenter"
                                    Text='<%# Bind("PR_Det_Lin_Qty") %>'></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtPrQty_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtPrQty"
                                    ValidChars=".">
                                </cc1:FilteredTextBoxExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField ShowHeader="True">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkMpr" runat="server"  />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField ShowHeader="True">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkMprAll" runat="server" onclick="checkAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkMpr" runat="server" onclick="Check_Click(this)" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField ShowHeader="False" Visible="False">
                            <ItemTemplate>
                                <asp:Button ID="btnReject" runat="server" Text="Reject" CommandName="Reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    OnClientClick="return confirm('Do you want to Reject?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" Visible="False">
                            <ItemTemplate>
                                <asp:Button ID="btnForward" runat="server" Text="Forward" CommandName="Forward" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    OnClientClick="return confirm('Do you want to Forward?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:Button ID="btnApprove" runat="server" Text="Proceed" CommandName="Approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    OnClientClick="return confirm('Do you want to Approve?')" />
                            </ItemTemplate>
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
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <br />
                <asp:Button ID="btnAppAll0" runat="server" OnClick="btnAppAll_Click" Text="Proceed Selected"
                    Visible="False" />
                <br />
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="cboLeaveType" EventName="SelectedIndexChanged" />--%>
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
