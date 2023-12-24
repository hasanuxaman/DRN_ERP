<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmSalesDsm.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSalesDsm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .cpHeader
        {
            color: white;
            background-color: #339966;
            font: bold 11px auto "Trebuchet MS" , Verdana;
            font-size: 12px;
            cursor: pointer;
            width: 450px;
            height: 18px;
            padding: 4px;
        }
        .cpBody
        {
            background-color: #DCE4F9;
            font: normal 12px auto Verdana, Arial;
            border: 1px gray;
            width: 450px;
            padding: 4px;
            padding-top: 2px;
            height: 0px;
            overflow: hidden;
        }
        .txtbox
        {
            padding: 0px;
            word-spacing: 0px;
            font-family: @Arial;
        }
        .style9
        {
            width: 161px;
        }
        .style13
        {
            width: 156px;
        }
        .style14
        {
            width: 93px;
        }
        .style15
        {
            width: 189px;
        }
    </style>
    <div align="center">
        <div align="center">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlSampleDetHdr" runat="server" CssClass="cpHeader" Width="1050px">
                        <asp:Label ID="lblSampleDetHdr" Text="DSM Setup" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlSampleDetDet" runat="server" CssClass="cpBody" Width="1050px">
                        <br />
                        <asp:Panel ID="pnlSearchBoxEmp" runat="server" DefaultButton="btnSearchEmp">
                            <div style="border: 1px solid #800080;">
                                Search Employee:<asp:TextBox ID="txtSrchEmp" runat="server" CssClass="search" Width="400"
                                    ForeColor="#0066FF"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteEmpSrch"
                                    CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                    DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchEmp" ServicePath="~/Module/SYS/Forms/wsAutoComSys.asmx"
                                    ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSrchEmp">
                                </cc1:AutoCompleteExtender>
                                &nbsp;<asp:Button ID="btnSearchEmp" runat="server" onkeypress="return clickButton(event,'txtSrchEmp')"
                                    Text="Search" ValidationGroup="btnSearchEmp" Width="60px" OnClick="btnSearchEmp_Click" />
                                &nbsp;<asp:Button ID="btnClearEmp" runat="server" Text="Clear" Visible="False" Width="60px"
                                    OnClick="btnClearEmp_Click" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSrchEmp"
                                    ErrorMessage="Enter Search Text" Font-Bold="False" ForeColor="#CC3300" ValidationGroup="btnSearchEmp"
                                    Font-Size="10pt"></asp:RequiredFieldValidator>
                            </div>
                        </asp:Panel>
                        <br />
                        <div id="Div1" style="overflow: auto;" runat="server">
                            <div align="center">
                                <table style="border: 1px solid #C0C0C0; width: 100%;">
                                    <tr>
                                        <td align="left" class="style9">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style15">
                                            Short Name
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDsmShortName" runat="server" CssClass="textAlignCenter" Font-Bold="True"
                                                ForeColor="#CC9900" MaxLength="10" Width="160px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDsmShortName"
                                                ErrorMessage="Enter DSM Name" ForeColor="Red" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                                            <%--<cc1:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender"
                                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                            </cc1:ValidatorCalloutExtender>--%>
                                        </td>
                                        <td align="left" class="style14">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style13">
                                            DSM ID
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left" width="180">
                                            <asp:TextBox ID="txtSpId" runat="server" CssClass="textAlignCenter" Enabled="False"
                                                Font-Bold="True" ForeColor="#CC9900" MaxLength="10" Width="160px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSpId"
                                                ErrorMessage="Enter DSM ID" ForeColor="Red" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator4_ValidatorCalloutExtender"
                                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                        <td align="left" width="180">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style9">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style15">
                                            Full Name
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtSpFullName" runat="server" Width="340px" MaxLength="150"></asp:TextBox>
                                        </td>
                                        <td align="left" class="style14">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSpFullName"
                                                ErrorMessage="Enter DSM Full Name" ForeColor="Red" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                                            <%--<cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                TargetControlID="RequiredFieldValidator1">
                                            </cc1:ValidatorCalloutExtender>--%>
                                        </td>
                                        <td align="left" class="style13">
                                            Employee ID
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left" width="180">
                                            <%--<asp:DropDownList ID="ddlUser" runat="server" OnSelectedIndexChanged="ddlSp_SelectedIndexChanged"
                                                    Width="165px">
                                                </asp:DropDownList>--%>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlUser"
                                                    Display="None" ErrorMessage="Select User Name" ForeColor="Red" InitialValue="0"
                                                    ValidationGroup="btnSave">*</asp:RequiredFieldValidator>--%>
                                            <%--<cc1:ValidatorCalloutExtender ID="RequiredFieldValidator5_ValidatorCalloutExtender"
                                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                                </cc1:ValidatorCalloutExtender>--%>
                                            <asp:DropDownList ID="cboEmpId" runat="server" Width="304px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" width="180">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator666" runat="server" ControlToValidate="cboEmpId"
                                                ErrorMessage="Select Empoyee Name" ForeColor="Red" InitialValue="0" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                                            <%--<cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender333" runat="server" Enabled="True"
                                                PopupPosition="Left" TargetControlID="RequiredFieldValidator666">
                                            </cc1:ValidatorCalloutExtender>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style9">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style15">
                                            Cell No
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtSpCell" runat="server" MaxLength="150" Width="340px"></asp:TextBox>
                                        </td>
                                        <td align="left" class="style14">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style13">
                                            Supervisor ID
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left" width="180">
                                            <asp:DropDownList ID="ddlSupervisor" runat="server" Width="304px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" width="180">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator555" runat="server" ControlToValidate="ddlSupervisor"
                                                ErrorMessage="Select Supervisor Name" ForeColor="Red" InitialValue="0" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                                            <%--<cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender555" runat="server" CssClass="CustomValidator"
                                                Enabled="True" PopupPosition="BottomRight" TargetControlID="RequiredFieldValidator555">
                                            </cc1:ValidatorCalloutExtender>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style9">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style15">
                                            Sales Zone
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlSalesZone" runat="server" Width="345px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" class="style14">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSalesZone"
                                                ErrorMessage="Select Sales Zone" ForeColor="Red" InitialValue="0" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                                            <%--<cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="CustomValidator"
                                                Enabled="True" TargetControlID="RequiredFieldValidator2">
                                            </cc1:ValidatorCalloutExtender>--%>
                                        </td>
                                        <td align="left" class="style13">
                                            Remarks
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left" rowspan="4" valign="top" width="180">
                                            <asp:TextBox ID="txtSpRemarks" runat="server" Height="70px" TextMode="MultiLine"
                                                MaxLength="250" Width="210px"></asp:TextBox>
                                        </td>
                                        <td align="left" rowspan="2" valign="top" width="180">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style9">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style15">
                                            Active Status
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left">
                                            <asp:RadioButtonList ID="optListStat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="optListStat_SelectedIndexChanged"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td align="left" class="style14">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style13">
                                        </td>
                                        <td align="center" width="9">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style9">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style15">
                                            Inactive Date
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtInactiveDate" runat="server" MaxLength="50" Width="160px" Enabled="False"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="txtInactiveDate_TextBoxWatermarkExtender" runat="server"
                                                Enabled="True" TargetControlID="txtInactiveDate" WatermarkCssClass="WaterMarkFont"
                                                WatermarkText="DD/MM/YYYY">
                                            </cc1:TextBoxWatermarkExtender>
                                            <cc1:CalendarExtender ID="txtInactiveDate_CalendarExtender" runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" TargetControlID="txtInactiveDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td align="left" class="style14">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style13">
                                            &nbsp;
                                        </td>
                                        <td align="center" width="9">
                                        </td>
                                        <td align="left" width="9">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style9">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style15">
                                            Next Resp. DSM
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlRespDsm" runat="server" Enabled="False" Width="345px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" class="style14">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style13">
                                            &nbsp;
                                        </td>
                                        <td align="center" width="9">
                                            &nbsp;
                                        </td>
                                        <td align="left" width="9">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                Font-Size="8pt" ForeColor="Red" ValidationGroup="btnSave" />
                            <br />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                            &nbsp;<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"
                                ValidationGroup="btnSave" Width="100px" />
                            <br />
                            <br />
                            <div align="center" class="AlphabetPager" style="font-size: 16px">
                                <span style="background-color: #00FFCC">Search By:</span> <span>
                                    <asp:Repeater ID="rptAlphabets" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("Value")%>' Visible='<%# !Convert.ToBoolean(Eval("Selected"))%>'
                                                OnClick="Alphabet_Click" />
                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("Value")%>' Visible='<%# Convert.ToBoolean(Eval("Selected"))%>' />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </span>
                            </div>
                            <br />
                            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" 
                                onclick="btnExport_Click" />
                            <br />
                            <asp:GridView ID="gvDsmList" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="#CCCCFF" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AllowPaging="True"
                                OnPageIndexChanging="gvDsmList_PageIndexChanging" PageSize="50" OnRowDataBound="gvDsmList_RowDataBound"
                                OnSelectedIndexChanged="gvDsmList_SelectedIndexChanged">
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL #">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Dsm_Full_Name" HeaderText="Full Name" />
                                    <asp:TemplateField HeaderText="Short Name">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfDsmRef" runat="server" Value='<%# Bind("Dsm_Ref") %>' />
                                            <asp:Label ID="lblDsmCode" runat="server" Text='<%# Bind("Dsm_Short_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Dsm_Cell_No" HeaderText="Cell No" />
                                    <asp:BoundField DataField="Dsm_Phone_No" HeaderText="Phone No" />
                                    <asp:BoundField DataField="Dsm_Emp_Ref" HeaderText="Emp. ID" />
                                    <asp:TemplateField HeaderText="Sales Zone">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSlsZone" runat="server" Text='<%# GetSalesZone(int.Parse(Eval("Dsm_Sls_Zone").ToString()))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Dsm_Remarks" HeaderText="Remarks" />
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# GetStatus(int.Parse(Eval("Dsm_Is_Active").ToString()))%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Dsm_Inactive_Date" DataFormatString="{0:d}" HeaderText="Inactive Date" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <table cellspacing="0" rules="all" border="0" style="border-collapse: collapse;">
                                        <tr style="color: White; background-color: #3AC0F2;">
                                            <th scope="col" style="width: 35px;">
                                                SL #
                                            </th>
                                            <th scope="col" style="width: 150px;">
                                                Short Name
                                            </th>
                                            <th scope="col" style="width: 150px;">
                                                Full Name
                                            </th>
                                            <th scope="col" style="width: 150px;">
                                                Cell No
                                            </th>
                                            <th scope="col" style="width: 150px;">
                                                Phone No
                                            </th>
                                            <th scope="col" style="width: 150px;">
                                                Emp. Id
                                            </th>
                                            <th scope="col" style="width: 150px;">
                                                Sales Zone
                                            </th>
                                            <th scope="col" style="width: 150px;">
                                                Remarks
                                            </th>
                                            <th scope="col" style="width: 150px;">
                                                Status
                                            </th>
                                            <th scope="col" style="width: 150px;">
                                                Inactive Date
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="99" align="center">
                                                No records found for the search criteria.
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                            </asp:GridView>
                            <br />
                        </div>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderSampleDet" runat="server"
                        TargetControlID="pnlSampleDetDet" CollapseControlID="pnlSampleDetHdr" ExpandControlID="pnlSampleDetHdr"
                        Collapsed="false" TextLabelID="lblSampleDetHdr" CollapsedText="DSM Setup"
                        ExpandedText="DSM Setup" CollapsedSize="0" AutoCollapse="False" AutoExpand="False"
                        ScrollContents="false" ImageControlID="Image1" ExpandedImage="~/Image/collapse.jpg"
                        CollapsedImage="~/Image/expand.jpg" ExpandDirection="Vertical">
                    </cc1:CollapsiblePanelExtender>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="optListStat" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="gvDsmList" EventName="DataBound" />
                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btnExport" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="hfDsmEditRef" runat="server" />
        <asp:HiddenField ID="hfHidden" runat="server" />
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
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupExtenderMsg" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnMsgOk" OkControlID="btnMsgOk" PopupControlID="pnlMsg" TargetControlID="hfHidden"
        DropShadow="true">
    </cc1:ModalPopupExtender>
</asp:Content>
