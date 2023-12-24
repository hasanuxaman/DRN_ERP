<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmEngTourPlan.aspx.cs" Inherits="DRN_WEB_ERP.Module.TeamManagement.Forms.frmEngTourPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #86AEAE">
                <div align="center" style="background-color: #00FF99">
                    Monthly Site Tour Plan</div>
                <asp:Panel ID="pnlSearchBox" runat="server">
                    <div align="center" style="background-color: #86AEAE">
                        <br />
                        <table style="width: 100%; font-size: small;">
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 87px">
                                    &nbsp;
                                </td>
                                <td style="width: 119px">
                                    &nbsp;
                                </td>
                                <td style="width: 3px">
                                    &nbsp;
                                </td>
                                <td style="width: 228px">
                                    &nbsp;
                                </td>
                                <td style="width: 56px">
                                    &nbsp;
                                </td>
                                <td style="width: 87px">
                                    &nbsp;
                                </td>
                                <td width="2">
                                    &nbsp;
                                </td>
                                <td width="200">
                                    &nbsp;
                                </td>
                                <td style="width: 87px">
                                    &nbsp;
                                </td>
                                <td style="width: 2px">
                                    &nbsp;
                                </td>
                                <td width="200">
                                    &nbsp;
                                </td>
                                <td style="width: 219px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="left" style="width: 87px">
                                    &nbsp;
                                </td>
                                <td align="right" style="width: 119px">
                                    Engineer Name
                                </td>
                                <td style="width: 3px">
                                    :
                                </td>
                                <td align="left" style="width: 228px">
                                    <asp:DropDownList ID="ddlEngName" runat="server" Width="230px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlEngName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <cc1:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddlEngName"
                                        PromptCssClass="ListSearchExtenderPrompt" QueryPattern="Contains" QueryTimeout="2000">
                                    </cc1:ListSearchExtender>
                                </td>
                                <td align="left" style="width: 56px">
                                    &nbsp;
                                </td>
                                <td align="right" style="width: 87px">
                                    From Date
                                </td>
                                <td align="right" width="2">
                                    :
                                </td>
                                <td align="left" width="200">
                                    <asp:TextBox ID="txtPlanFromDt" runat="server" Width="100px" Enabled="false" CssClass="textAlignCenter"></asp:TextBox>
                                    <asp:Image ID="imgPlanFromDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgPlanFromDt" TargetControlID="txtPlanFromDt">
                                    </cc1:CalendarExtender>
                                </td>
                                <td align="right" style="width: 87px">
                                    To Date
                                </td>
                                <td style="width: 2px">
                                    :
                                </td>
                                <td align="left" width="200">
                                    <asp:TextBox ID="txtPlanToDt" runat="server" Width="100px" Enabled="false" CssClass="textAlignCenter"></asp:TextBox>
                                    <asp:Image ID="imgPlanToDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgPlanToDt" TargetControlID="txtPlanToDt">
                                    </cc1:CalendarExtender>
                                </td>
                                <td align="left" style="width: 219px">
                                    &nbsp;
                                    <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Show"
                                        Width="100px" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 87px">
                                    &nbsp;
                                </td>
                                <td style="width: 119px">
                                    &nbsp;
                                </td>
                                <td style="width: 3px">
                                    &nbsp;
                                </td>
                                <td style="width: 228px">
                                    &nbsp;
                                </td>
                                <td style="width: 56px">
                                    &nbsp;
                                </td>
                                <td style="width: 87px">
                                    &nbsp;
                                </td>
                                <td width="2">
                                    &nbsp;
                                </td>
                                <td width="200">
                                    &nbsp;
                                </td>
                                <td style="width: 87px">
                                    &nbsp;
                                </td>
                                <td style="width: 2px">
                                    &nbsp;
                                </td>
                                <td width="200">
                                    &nbsp;
                                </td>
                                <td style="width: 219px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div>
                        <fieldset style="background-color: #99CCFF">
                            <br />
                            <div align="center">
                                <table border="1" cellspacing="0" rules="all" style="border-collapse: collapse;">
                                    <tr style="background-color: #00CC66; font-weight: bold; color: #F7F7F7; font-size: smaller;
                                        white-space: nowrap">
                                        <th align="center" scope="col" width="210">
                                            Organization Name
                                        </th>
                                        <th align="center" scope="col" width="210">
                                            Address
                                        </th>
                                        <th align="center" scope="col" width="110">
                                            Contact No
                                        </th>
                                        <th align="center" scope="col" width="110">
                                            Contact Name
                                        </th>
                                        <th align="center" scope="col" width="100">
                                            Designation
                                        </th>
                                        <th align="center" scope="col" width="120">
                                            Plan Date
                                        </th>
                                        <th align="center" scope="col" width="95">
                                            Purpose
                                        </th>
                                        <th align="center" scope="col" width="75">
                                            Visit No
                                        </th>
                                        <th align="center" scope="col" width="20">
                                            &nbsp;
                                        </th>
                                    </tr>
                                    <tr class="gridFooterRow" style="background-color: #FF66FF;">
                                        <td>
                                            <asp:TextBox ID="txtCompNameTbl" runat="server" Width="210px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCompAdrTbl" runat="server" Width="210px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCompContNoTbl" runat="server" Width="110px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCompContNameTbl" runat="server" Width="110px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCompContDesigTbl" runat="server" Width="110px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPlanDateTbl" runat="server" CssClass="textAlignCenter" Width="70px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtPlanDateTbl_CalendarExtender" runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtPlanDateTbl">
                                            </cc1:CalendarExtender>
                                            <%--<asp:Image ID="imgPlanDate" runat="server" ImageUrl="~/Image/calendar.png" />--%>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlVisitPurposeTbl" runat="server" Width="105px" DataSourceID="SqlDataSource1"
                                                DataTextField="RefName" DataValueField="RefNo">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtVisitNo" runat="server" Width="50px" CssClass="textAlignCenter"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtVisitNo_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtVisitNo"
                                                ValidChars=".">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td align="center">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DRNConStr %>"
                                    SelectCommand="SELECT * FROM [tbl_Team_Tour_Purpose] where [RefFlag]='ENG'">
                                </asp:SqlDataSource>
                                <br />
                            </div>
                        </fieldset>
                    </div>
                    <br />
                    <div align="center" style="background-color: #86AEAE">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="gvTourPlan" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    Font-Size="8pt" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    OnRowEditing="gvTourPlan_RowEditing" OnRowCancelingEdit="gvTourPlan_RowCancelingEdit"
                                    OnRowUpdating="gvTourPlan_RowUpdating" OnRowDeleting="gvTourPlan_RowDeleting"
                                    OnRowDataBound="gvTourPlan_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="##">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company Name" SortExpression="COMP_NAME">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfPlanId" runat="server" Value='<%# Bind("PLAN_REF") %>' />
                                                <asp:Label ID="lblCompName" Width="210px" runat="server" Text='<%# Bind("COMP_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:HiddenField ID="hfPlanIdEdit" runat="server" Value='<%# Bind("PLAN_REF") %>' />
                                                <asp:TextBox ID="txtCompNameEdit" Width="210px" runat="server" Text='<%# Bind("COMP_NAME") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ControlStyle Width="210px" />
                                            <ItemStyle Width="210px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address" SortExpression="COMP_ADR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompAdr" Width="210px" runat="server" Text='<%# Bind("COMP_ADR") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCompAdrEdit" Width="210px" runat="server" Text='<%# Bind("COMP_ADR") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ControlStyle Width="210px" />
                                            <ItemStyle Width="210px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact No" SortExpression="CONT_NO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompContNo" Width="110px" runat="server" Text='<%# Bind("CONT_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCompContNoEdit" Width="110px" runat="server" Text='<%# Bind("CONT_NO") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ControlStyle Width="110px" />
                                            <ItemStyle Width="110px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact Name" SortExpression="CONT_NAME">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompContName" Width="90px" runat="server" Text='<%# Bind("CONT_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCompContNameEdit" Width="90px" runat="server" Text='<%# Bind("CONT_NAME") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ControlStyle Width="90px" />
                                            <ItemStyle Width="90px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" SortExpression="CONT_DESIG">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompContRef" Width="100px" runat="server" Text='<%# Bind("CONT_DESIG") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCompContDesigEdit" runat="server" Width="110px" Text='<%# Bind("CONT_DESIG") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ControlStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plan Date" SortExpression="PLAN_DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPlanDate" runat="server" Text='<%# Eval("PLAN_DATE", "{0:dd, MMM yyyy}") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPlanDateEdit" Width="70px" runat="server" Text='<%# Bind("PLAN_DATE") %>'></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtPlanDateEdit_CalendarExtender" runat="server" Enabled="True"
                                                    Format="dd/MM/yyyy" TargetControlID="txtPlanDateEdit">
                                                </cc1:CalendarExtender>
                                            </EditItemTemplate>
                                            <ItemStyle Width="70px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purpose" SortExpression="VISIT_PURPOSE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVisitType" Width="110px" runat="server" Text='<%# Bind("VISIT_PURPOSE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlVisitPurposeEdit" runat="server" Width="105px" DataSourceID="SqlDataSource2"
                                                    DataTextField="RefName" DataValueField="RefNo">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DRNConStr %>"
                                                    SelectCommand="SELECT * FROM [tbl_Team_Tour_Purpose] where [RefFlag]='ENG'">
                                                </asp:SqlDataSource>
                                            </EditItemTemplate>
                                            <ControlStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Visit No" SortExpression="VISIT_NO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVisitNo" Width="40px" runat="server" Text='<%# Bind("VISIT_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtVisitNoEdit" runat="server" Width="40px" Text='<%# Bind("VISIT_NO") %>'></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="txtVisitNoEdit_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtVisitNoEdit"
                                                    ValidChars=".">
                                                </cc1:FilteredTextBoxExtender>
                                            </EditItemTemplate>
                                            <ControlStyle Width="40px" />
                                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgBtnDelete" ImageUrl="~/Image/Delete.png" runat="server" CausesValidation="False"
                                                    ToolTip="Delete" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Do you want to delete?')">
                                                </asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgBtnEditData" runat="server" CausesValidation="False" CommandName="Edit"
                                                    ToolTip="Edit" ImageUrl="~/Image/Edit.png" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="imgBtnUpdateData" runat="server" CausesValidation="True" OnClientClick="return confirm('Do you want to update?')"
                                                    CommandName="Update" ImageUrl="~/Image/Save.png" ToolTip="Update" />
                                                <asp:ImageButton ID="imgBtnCancelData" runat="server" CausesValidation="False" ToolTip="Cancel"
                                                    OnClientClick="return confirm('Do you want to cancel?')" CommandName="Cancel"
                                                    ImageUrl="~/Image/Back.png" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnPost" runat="server" Text="Post" Style="background-image: url('~/Image/Add3.png');
                                                    background-repeat: no-repeat" OnClick="btnPost_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#FF66FF" HorizontalAlign="Right" Wrap="false" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" Font-Size="Smaller"
                                        Wrap="False" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#CC99FF" Font-Bold="True" ForeColor="Black" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                </asp:GridView>
                                <br />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvTourPlan" EventName="DataBound" />
                                <asp:AsyncPostBackTrigger ControlID="gvTourPlan" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlEngName" EventName="SelectedIndexChanged" />
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
