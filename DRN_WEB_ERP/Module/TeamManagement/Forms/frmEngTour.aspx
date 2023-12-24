<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmEngTour.aspx.cs" Inherits="DRN_WEB_ERP.Module.TeamManagement.Forms.frmEngTour" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #00FF99">
                Monthly Site Tour</div>
            <asp:Panel ID="pnlSearchBox" runat="server">
                <div align="center" style="background-color: #86AEAE">
                    <br />
                    <table style="width: 100%; font-size: small;">
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
                                <asp:DropDownList ID="ddlEngName" runat="server" Width="550px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlEngName_SelectedIndexChanged">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddlEngName"
                                    PromptCssClass="ListSearchExtenderPrompt" QueryPattern="Contains" QueryTimeout="2000">
                                </cc1:ListSearchExtender>
                            </td>
                            <td align="left" style="width: 22px">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 219px">
                                &nbsp;
                                <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Refresh"
                                    Width="100px" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
                <div align="center" style="background-color: #86AEAE">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <fieldset style="background-color: #9966FF">
                                <legend>Pending List</legend>
                                <br />
                                <div align="center">
                                    <asp:GridView ID="gvPend" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="10pt"
                                        OnRowCommand="gvPend_RowCommand" OnRowDataBound="gvPend_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="##">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Organization Name" SortExpression="Tem_Plan_Com">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfPlanRef" runat="server" Value='<%# Bind("Tem_Plan_Ref") %>' />
                                                    <asp:Label ID="lblCompName" runat="server" Text='<%# Bind("Tem_Plan_Com") %>' Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="200px" />
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address" SortExpression="Tem_Plan_Com_Adr">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompAdr" runat="server" Text='<%# Bind("Tem_Plan_Com_Adr") %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="200px" />
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact No" SortExpression="Tem_Plan_Com_Cont_No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompContNo" runat="server" Text='<%# Bind("Tem_Plan_Com_Cont_No") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Name" SortExpression="Tem_Plan_Com_Cont_Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompContName" runat="server" Text='<%# Bind("Tem_Plan_Com_Cont_Name") %>'
                                                        Width="110px"></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="110px" />
                                                <ItemStyle Width="110px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" SortExpression="Tem_Plan_Com_Cont_Ref">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompContRef" runat="server" Text='<%# Bind("Tem_Plan_Com_Cont_Ref") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Plan Date" SortExpression="Tem_Plan_Tour_Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitDate" runat="server" Text='<%#  Bind ("Tem_Plan_Tour_Date", "{0:dd-MM-yyyy}") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Purpose" SortExpression="Tem_Plan_Tour_Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitType" runat="server" Text='<%# Bind("Tem_Plan_Tour_Type") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Visit Date" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtVisitDt" runat="server" CssClass="textAlignCenter" Enabled="false"
                                                        Width="70px"></asp:TextBox>
                                                    <asp:Image ID="imgVisitDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                                    <cc1:CalendarExtender ID="txtVisitDateTbl_CalendarExtender" runat="server" Enabled="True"
                                                        Format="dd/MM/yyyy" PopupButtonID="imgVisitDt" TargetControlID="txtVisitDt">
                                                    </cc1:CalendarExtender>
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Visit No" SortExpression="Tem_Plan_Ext_Data2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitNo" runat="server" Text='<%#  Bind ("Tem_Plan_Ext_Data2") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Accept" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgBtnAcceptData" runat="server" CausesValidation="False" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        CommandName="Accept" ImageUrl="~/Image/accept-icon.png" ToolTip="Visited" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dismiss" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgBtnDeleteData" runat="server" CausesValidation="False" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        CommandName="Dismiss" ImageUrl="~/Image/Delete.png" ToolTip="Dismiss" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#006699" HorizontalAlign="Right" Wrap="false" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Size="Smaller" ForeColor="White"
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
                                </div>
                            </fieldset>
                            <fieldset style="background-color: #99CCFF">
                                <legend>New Visit Entry</legend>
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
                                            <th align="center" scope="col" width="100">
                                                Contact Name
                                            </th>
                                            <th align="center" scope="col" width="100">
                                                Designation
                                            </th>
                                            <th align="center" scope="col" width="70">
                                                Visit Date
                                            </th>
                                            <th align="center" scope="col" width="95">
                                                Purpose
                                            </th>
                                            <th align="center" scope="col" width="95">
                                                Visit No
                                            </th>
                                        </tr>
                                        <tr class="gridFooterRow" style="background-color: #FF66FF;">
                                            <td>
                                                <asp:TextBox ID="txtCompNameTbl" runat="server" Width="210px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCompAdrTbl" runat="server" Width="220px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCompContNoTbl" runat="server" Width="120px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCompContNameTbl" runat="server" Width="110px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCompContDesigTbl" runat="server" Width="110px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtVisitDateTbl" runat="server" CssClass="textAlignCenter" Width="70px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtVisitDateTbl_CalendarExtender0" runat="server" Enabled="True"
                                                    Format="dd/MM/yyyy" TargetControlID="txtVisitDateTbl">
                                                </cc1:CalendarExtender>
                                                <%--<asp:Image ID="imgVisitDate" runat="server" ImageUrl="~/Image/calendar.png" />--%>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlVisitPurposeTbl" runat="server" Width="105px" DataSourceID="SqlDataSource1"
                                                    DataTextField="RefName" DataValueField="RefNo">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtVisitNoTbl" runat="server" Width="50px" CssClass="textAlignCenter"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="txtVisitNoTbl_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtVisitNoTbl"
                                                    ValidChars=".">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
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
                                            <td>
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
                                        <tr class="gridFooterRow" style="background-color: #00CC66;">
                                            <td align="center" style="background-color: #00CC66; font-weight: bold; color: #F7F7F7;
                                                font-size: smaller; white-space: nowrap">
                                                Remarks
                                            </td>
                                            <td align="left" colspan="5">
                                                <asp:TextBox ID="txtRemTbl" runat="server" Width="670px"></asp:TextBox>
                                            </td>
                                            <td align="center">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" OnClick="btnSave_Click" />
                                            </td>
                                            <td align="center">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DRNConStr %>"
                                        SelectCommand="SELECT * FROM [tbl_Team_Tour_Purpose] where [RefFlag]='ENG'">
                                    </asp:SqlDataSource>
                                    <br />
                                </div>
                            </fieldset>
                            <fieldset style="background-color: #FF9966">
                                <legend>Visit List</legend>
                                <div align="center">
                                    <br />
                                    <table style="width: 100%; font-size: small;">
                                        <tr>
                                            <td style="width: 118px">
                                                &nbsp;
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
                                                <asp:TextBox ID="txtTourFromDt" runat="server" CssClass="textAlignCenter" Enabled="false"
                                                    Width="100px"></asp:TextBox>
                                                <asp:Image ID="imgTourFromDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    PopupButtonID="imgTourFromDt" TargetControlID="txtTourFromDt">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td align="right" style="width: 87px">
                                                To Date
                                            </td>
                                            <td style="width: 2px">
                                                :
                                            </td>
                                            <td align="left" width="200">
                                                <asp:TextBox ID="txtTourToDt" runat="server" CssClass="textAlignCenter" Enabled="false"
                                                    Width="100px"></asp:TextBox>
                                                <asp:Image ID="imgTourToDt" runat="server" ImageUrl="~/Image/calendar.png" />
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    PopupButtonID="imgTourToDt" TargetControlID="txtTourToDt">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td align="left" style="width: 219px">
                                                &nbsp;
                                                <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" Width="100px" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:GridView ID="gvTour" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="8pt">
                                        <Columns>
                                            <asp:TemplateField HeaderText="##">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Organization Name" SortExpression="Tem_Tour_Com">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTourCompName" runat="server" Text='<%# Bind("Tem_Tour_Com") %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="180px" />
                                                <ItemStyle Width="180px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address" SortExpression="Tem_Tour_Com_Adr">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTourCompAdr" runat="server" Text='<%# Bind("Tem_Tour_Com_Adr") %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="200px" />
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact No" SortExpression="Tem_Tour_Com_Cont_No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTourCompContNo" runat="server" Text='<%# Bind("Tem_Tour_Com_Cont_No") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="90px" />
                                                <ItemStyle Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Name" SortExpression="Tem_Tour_Com_Cont_Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTourCompContName" runat="server" Text='<%# Bind("Tem_Tour_Com_Cont_Name") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" SortExpression="Tem_Tour_Com_Cont_Ref">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTourCompContRef" runat="server" Text='<%# Bind("Tem_Tour_Com_Cont_Ref") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Plan Date" SortExpression="Tem_Tour_Plan_Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTourPlanDate" runat="server" Text='<%#  Bind ("Tem_Tour_Plan_Date", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="65px" />
                                                <ItemStyle Width="65px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Tem_Tour_Ext_Data3" HeaderText="Remarks" />
                                            <asp:TemplateField HeaderText="Visit Date" SortExpression="Tem_Tour_Tour_Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTourVisitDate" runat="server" Text='<%#  Bind ("Tem_Tour_Tour_Date", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="65px" />
                                                <ItemStyle Width="65px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Purpose" SortExpression="Tem_Tour_Tour_Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTourVisitType" runat="server" Text='<%# Bind("Tem_Tour_Tour_Type") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Tem_Tour_Ext_Data2" HeaderText="Visit No">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" Visible="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgBtnDeleteTourData" runat="server" CausesValidation="False"
                                                        CommandName="Delete" ImageUrl="~/Image/Delete.png" ToolTip="Delete" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#006699" HorizontalAlign="Right" Wrap="false" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Size="Smaller" ForeColor="White"
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
                                </div>
                                <br />
                            </fieldset>
                            <br />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvTour" EventName="DataBound" />
                            <asp:AsyncPostBackTrigger ControlID="gvTour" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlEngName" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlRem" runat="server" Style="border-right: black 2px solid; padding-right: 20px;
        display: none; border-top: black 2px solid; padding-left: 20px; padding-bottom: 20px;
        border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
        background-color: #E1FBF1" ForeColor="Blue" Width="300px" Font-Size="Small">
        <table id="tblRem" runat="server" style="width: 100%;">
            <tr>
                <td>
                    Visit Date:<asp:TextBox ID="txtRemVisitDt" runat="server" CssClass="textAlignCenter"
                        Enabled="false" Width="120px"></asp:TextBox>
                    <asp:Image ID="imgRemVisitDt" runat="server" ImageUrl="~/Image/calendar.png" />
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        PopupButtonID="imgRemVisitDt" TargetControlID="txtRemVisitDt">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#66FF99" style="height: 20px">
                    Remarks
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:HiddenField ID="hfRemPlanRef" runat="server" />
                    <asp:TextBox ID="txtPendRem" runat="server" Height="80px" TextMode="MultiLine" Width="300px"
                        MaxLength="255"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <div align="center">
            <asp:Button ID="btnRemOk" runat="server" Text="OK" Width="80px" OnClick="btnRemOk_Click" />
            <asp:Button ID="btnRemCancel" runat="server" Text="Cancel" Width="80px" />
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hfRemOk" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupExtenderRem" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnRemCancel" OkControlID="btnRemCancel" PopupControlID="pnlRem"
        TargetControlID="hfRemOk" DropShadow="true">
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
