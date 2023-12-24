<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmStoreReqInq.aspx.cs" Inherits="DRN_WEB_ERP.Module.Inventory.Forms.frmStoreReqInq" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
                        
    </script>
    <div align="center" style="background-color: #00FF99">
        Store Requisition Inquiry</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <br />
                <asp:Panel ID="Panel1" runat="server" GroupingText="Store Requisition Inquiry">
                    <br />
                    Year:<asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
                        Width="100px">
                    </asp:DropDownList>
                    &nbsp;Requisition By:<asp:DropDownList ID="cboReqBy" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="cboReqBy_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;Required For:<asp:DropDownList ID="cboReqFor" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="cboReqFor_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:GridView ID="gvPendPo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" Font-Size="7pt" OnRowDataBound="gvPendPo_RowDataBound" AllowPaging="True"
                        PageSize="25" OnPageIndexChanging="gvPendPo_PageIndexChanging">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnInfo" runat="server" Height="20px" Width="20px" ImageUrl="~/Image/about-512.png"
                                        ToolTip='<%# Bind("Det_T_C1") %>' OnClick="imgBtnInfo_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" class="preview" Target="_blank" NavigateUrl='<%# Eval("EmpRefNo", "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo={0}") %>'
                                        runat="server">
                                        <asp:Image ID="AttachImage" runat="server" Height="50px" Width="65px" ImageUrl="~/Image/NoImage.gif" /></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Requsition By">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpName" runat="server" Text='<%# GetEmpName(Eval("EmpRefNo").ToString())%>'
                                        Font-Bold="True"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblEmpId" runat="server" Text='<%# GetEmpId(Eval("EmpRefNo").ToString())%>'
                                        Font-Italic="True" ForeColor="#3399FF"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblEmpDes" runat="server" Text='<%# GetEmpDesig(Eval("EmpRefNo").ToString())%>'
                                        Font-Italic="True"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblEmpDep" runat="server" Text='<%# GetEmpDept(Eval("EmpRefNo").ToString())%>'
                                        Font-Italic="True"></asp:Label>
                                    <asp:HiddenField ID="hfEmpRef" runat="server" Value='<%# Bind("EmpRefNo") %>' />
                                    <asp:HiddenField ID="hfEmpSupRef" runat="server" Value='<%# Bind("EmpSuprId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ref. No">
                                <ItemTemplate>
                                    <%--<asp:HiddenField ID="hfPoRef" runat="server" Value='<%# Bind("PO_Hdr_Ref") %>' />--%>
                                    <asp:Label ID="lblPoRefNo" runat="server" Text='<%# Bind("PO_Hdr_Ref") %>'></asp:Label>
                                    <asp:HiddenField ID="hfPoDetLno" runat="server" Value='<%# Bind("PO_Det_Lno") %>' />
                                    <asp:HiddenField ID="hfReqBy" runat="server" Value='<%# Bind("PO_Hdr_Pcode") %>' />
                                    <asp:HiddenField ID="hfDcode" runat="server" Value='<%# Bind("PO_Hdr_Dcode") %>' />
                                    <asp:HiddenField ID="hfAcode" runat="server" Value='<%# Bind("PO_Hdr_Acode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblPoDt" runat="server" Text='<%# Bind("PO_Hdr_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PO_Hdr_Com3" HeaderText="Required For" />
                            <asp:BoundField DataField="PO_Hdr_Com9" HeaderText="Dealer/Retailer Name" />
                            <asp:BoundField DataField="PO_Hdr_Com1" HeaderText="Remarks" />
                            <asp:TemplateField HeaderText="Item Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("PO_Det_Icode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("PO_Det_Itm_Desc") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="#FFCCFF" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemUnit" runat="server" Text='<%# Bind("PO_Det_Itm_Uom") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfStoreRef" runat="server" Value='<%# Bind("PO_Det_Str_Code") %>' />
                                    <asp:Label ID="lblIssQty" runat="server" Text='<%# Bind("PO_Det_Lin_Qty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle BackColor="Aqua" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Store">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlStore" runat="server" Width="120px" DataSourceID="SqlDataSource1"
                                        DataTextField="Str_Loc_Name" DataValueField="Str_Loc_Ref" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlStore_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DRNConStr %>"
                                        SelectCommand="SELECT * FROM [tbl_InMa_Str_Loc] WHERE (Str_Loc_Type IN ('H', 'F')) ORDER BY Str_Loc_Name">
                                    </asp:SqlDataSource>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stock">
                                <ItemTemplate>
                                    <asp:Label ID="lblStock" runat="server" Text='<%# GetCurStk(Eval("PO_Det_Str_Code").ToString(),Eval("PO_Det_Icode").ToString())%>'
                                        Font-Bold="True"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="10pt" />
                        <PagerSettings Position="TopAndBottom" />
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
                    </asp:GridView>
                    <br />
                    <br />
                </asp:Panel>
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="cboLeaveType" EventName="SelectedIndexChanged" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlMsgInfo" runat="server" Style="border-right: black 2px solid; padding-right: 20px;
        display: none; border-top: black 2px solid; padding-left: 20px; padding-bottom: 20px;
        border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
        background-color: #E1FBF1" ForeColor="Blue" Width="510px" Font-Size="Small">
        <table id="tblMsgInfo" runat="server" style="width: 100%;">
            <tr>
                <td bgcolor="#33CCCC" align="center">
                    <asp:Label ID="lblMsgInfo" runat="server" Font-Bold="True">Approval Status Info</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtMsgInfo" runat="server" Enabled="false" Height="110px" TextMode="MultiLine"
                        Width="500px" MaxLength="255" BackColor="#99FFCC"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <div align="center">
            <asp:Button ID="btnMsgInfoOk" runat="server" Text="OK" Width="80px" />
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hfHiddenMsgInfo" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupExtenderMsgInfo" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnMsgInfoOk" OkControlID="btnMsgInfoOk" PopupControlID="pnlMsgInfo"
        TargetControlID="hfHiddenMsgInfo" DropShadow="true">
    </cc1:ModalPopupExtender>
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
