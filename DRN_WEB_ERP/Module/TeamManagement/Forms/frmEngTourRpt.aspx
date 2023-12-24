<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmEngTourRpt.aspx.cs" Inherits="DRN_WEB_ERP.Module.TeamManagement.Forms.frmEngTourRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Tour Report</div>
    <div align="center" style="background-color: #86AEAE">
        <br />
        <table style="width: 100%;">
            <tr>
                <td style="width: 435px">
                    &nbsp;
                </td>
                <td align="left" style="width: 166px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="left" colspan="7">
                    &nbsp;
                </td>
                <td style="width: 14px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 435px">
                    &nbsp;
                </td>
                <td align="left" style="width: 166px">
                    Eng. Name
                </td>
                <td>
                    :
                </td>
                <td align="left" colspan="7">
                    <asp:DropDownList ID="ddlEngName" runat="server" Width="550px">
                    </asp:DropDownList>
                    <cc1:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="ddlEngName"
                        PromptCssClass="ListSearchExtenderPrompt" QueryPattern="Contains" QueryTimeout="2000">
                    </cc1:ListSearchExtender>
                </td>
                <td style="width: 14px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 435px">
                    &nbsp;
                </td>
                <td align="left" style="width: 166px">
                    Date From
                </td>
                <td>
                    :
                </td>
                <td align="left" style="width: 246px">
                    <asp:TextBox ID="txtRptFrmDt" runat="server" CssClass="textAlignCenter" Enabled="false"
                        Width="120px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtRptFrmDt_CalendarExtender" runat="server" Enabled="True"
                        Format="dd/MM/yyyy" PopupButtonID="imgTourFrmDt" TargetControlID="txtRptFrmDt">
                    </cc1:CalendarExtender>
                    <asp:Image ID="imgTourFrmDt" runat="server" ImageUrl="~/Image/calendar.png" />
                </td>
                <td align="left" style="width: 90px">
                    Date To
                </td>
                <td align="left" style="width: 1px">
                    :
                </td>
                <td align="left" style="width: 194px">
                    <asp:TextBox ID="txtRptToDt" runat="server" CssClass="textAlignCenter" Enabled="false"
                        Width="120px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtRptToDt_CalendarExtender" runat="server" Enabled="True"
                        Format="dd/MM/yyyy" PopupButtonID="imgRptToDt" TargetControlID="txtRptToDt">
                    </cc1:CalendarExtender>
                    <asp:Image ID="imgRptToDt" runat="server" ImageUrl="~/Image/calendar.png" />
                </td>
                <td align="left" style="width: 30px">
                    &nbsp;
                </td>
                <td align="left" style="width: 306px">
                    <asp:Button ID="btnShow" runat="server" Text="Show" Width="120px" OnClick="btnShow_Click" />
                </td>
                <td align="left" style="width: 306px">
                    &nbsp;
                </td>
                <td style="width: 14px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 435px">
                    &nbsp;
                </td>
                <td align="left" style="width: 166px">
                    Purpose
                </td>
                <td>
                    :
                </td>
                <td align="left" colspan="7">
                    <asp:DropDownList ID="ddlVisitPurpose" runat="server" Width="205px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlVisitPurpose_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 14px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 435px">
                    &nbsp;
                </td>
                <td align="left" style="width: 166px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="left" colspan="7">
                    &nbsp;
                </td>
                <td style="width: 14px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
        <asp:Panel ID="pnlPlan" runat="server" GroupingText="Plan Details">
            <br />
            <asp:GridView ID="gvTourPlan" runat="server" AutoGenerateColumns="False" Font-Size="8pt"
                CellPadding="3" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                GridLines="Vertical">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="##">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Emp. Name">
                        <ItemTemplate>
                            <asp:Label ID="lblPlanEmpName" runat="server" Text='<%# GetEmpName(Eval("Tem_Plan_Emp_Ref").ToString())%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Plan Date" SortExpression="Tem_Plan_Tour_Date">
                        <ItemTemplate>
                            <asp:Label ID="lblPlanDate" runat="server" Text='<%# Eval("Tem_Plan_Tour_Date", "{0:dd, MMM yyyy}") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Company Name" SortExpression="Tem_Plan_Com">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfPlanId" runat="server" Value='<%# Bind("Tem_Plan_Ref") %>' />
                            <asp:Label ID="lblCompName" Width="150px" runat="server" Text='<%# Bind("Tem_Plan_Com") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="150px" />
                        <ItemStyle Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address" SortExpression="Tem_Plan_Com_Adr">
                        <ItemTemplate>
                            <asp:Label ID="lblCompAdr" Width="180px" runat="server" Text='<%# Bind("Tem_Plan_Com_Adr") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="180px" />
                        <ItemStyle Width="180px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contact No" SortExpression="Tem_Plan_Com_Cont_No">
                        <ItemTemplate>
                            <asp:Label ID="lblCompContNo" Width="110px" runat="server" Text='<%# Bind("Tem_Plan_Com_Cont_No") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="110px" />
                        <ItemStyle Width="110px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contact Name" SortExpression="Tem_Plan_Com_Cont_Name">
                        <ItemTemplate>
                            <asp:Label ID="lblCompContName" Width="90px" runat="server" Text='<%# Bind("Tem_Plan_Com_Cont_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="90px" />
                        <ItemStyle Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Designation" SortExpression="Tem_Plan_Com_Cont_Ref">
                        <ItemTemplate>
                            <asp:Label ID="lblCompContRef" Width="100px" runat="server" Text='<%# Bind("Tem_Plan_Com_Cont_Ref") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purpose" SortExpression="Tem_Plan_Tour_Type">
                        <ItemTemplate>
                            <asp:Label ID="lblVisitType" Width="110px" runat="server" Text='<%# Bind("Tem_Plan_Tour_Type") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Visit No" SortExpression="Tem_Plan_Ext_Data2">
                        <ItemTemplate>
                            <asp:Label ID="lblVisitNo" Width="40px" runat="server" Text='<%# Bind("Tem_Plan_Ext_Data2") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="40px" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" HorizontalAlign="Right" Wrap="false" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Size="Smaller"
                    Wrap="False" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
            <br />
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlVisit" runat="server" GroupingText="Visit Details">
            <br />
            <asp:GridView ID="gvTour" runat="server" AutoGenerateColumns="False" CellPadding="4"
                Font-Size="8pt" ForeColor="#333333">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="##">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Emp. Name">
                        <ItemTemplate>
                            <asp:Label ID="lblEmpName" runat="server" Text='<%# GetEmpName(Eval("Tem_Tour_Emp_Ref").ToString())%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Visit Date" SortExpression="Tem_Tour_Tour_Date">
                        <ItemTemplate>
                            <asp:Label ID="lblTourVisitDate" runat="server" Text='<%#  Bind ("Tem_Tour_Tour_Date", "{0:dd-MM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="60px" />
                        <ItemStyle Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Organization Name" SortExpression="Tem_Tour_Com">
                        <ItemTemplate>
                            <asp:Label ID="lblTourCompName" runat="server" Text='<%# Bind("Tem_Tour_Com") %>'
                                Width="150px"></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="150px" />
                        <ItemStyle Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address" SortExpression="Tem_Tour_Com_Adr">
                        <ItemTemplate>
                            <asp:Label ID="lblTourCompAdr" runat="server" Text='<%# Bind("Tem_Tour_Com_Adr") %>'
                                Width="180px"></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="180px" />
                        <ItemStyle Width="180px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contact No" SortExpression="Tem_Tour_Com_Cont_No">
                        <ItemTemplate>
                            <asp:Label ID="lblTourCompContNo" runat="server" Text='<%# Bind("Tem_Tour_Com_Cont_No") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="70px" />
                        <ItemStyle Width="70px" />
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
                        <ControlStyle Width="60px" />
                        <ItemStyle Width="60px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Tem_Tour_Ext_Data3" HeaderText="Remarks" />
                    <asp:TemplateField HeaderText="Purpose" SortExpression="Tem_Tour_Tour_Type">
                        <ItemTemplate>
                            <asp:Label ID="lblTourVisitType" runat="server" Text='<%# Bind("Tem_Tour_Tour_Type") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Tem_Tour_Ext_Data2" HeaderText="Visit No">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" HorizontalAlign="Right" Wrap="false" ForeColor="White"
                    Font-Bold="True" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="Smaller" ForeColor="White"
                    Wrap="False" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <br />
        </asp:Panel>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="10pt">
            <Columns>
                <asp:BoundField DataField="EmpName" HeaderText="Employee Name">
                    <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="Tem_Plan_Ext_Data1" HeaderText="Period">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="TotPlan" HeaderText="Total Plan">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="TotVisit" HeaderText="Total Visit" NullDisplayText="0">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="TotPlanDone" HeaderText="Plan Wise Visit">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div align="center" class="box" style="display: none">
                    <h3>
                        Monhly Site Visit Status
                    </h3>
                    <asp:Chart ID="Chart4" runat="server" Width="510">
                        <Series>
                            <asp:Series Name="Plan">
                            </asp:Series>
                            <asp:Series Name="Visit">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="cboSalesDate" EventName="SelectedIndexChanged" />--%>
            </Triggers>
        </asp:UpdatePanel>
        <br />
    </div>
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
