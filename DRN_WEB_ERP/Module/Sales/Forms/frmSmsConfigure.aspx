<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmSmsConfigure.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmSmsConfigure" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center">
        <div align="center">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlSampleDetHdr" runat="server" CssClass="cpHeader" Width="1050px">
                        <asp:Label ID="lblSampleDetHdr" Text="SMS Configure" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="pnlSampleDetDet" runat="server" CssClass="cpBody" Width="1050px">
                        <br />
                        <div id="Div1" style="overflow: auto;" runat="server">
                            <div align="center">
                                <table style="border: 1px solid #C0C0C0; width: 100%;">
                                    <tr>
                                        <td align="left" class="style16" style="width: 79px">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style10" style="width: 167px">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style10" style="width: 130px">
                                            TRAN. Type
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left" class="style15">
                                            <asp:DropDownList ID="cboTranType" runat="server" Width="345px">
                                                <asp:ListItem Value="0">-----Select-----</asp:ListItem>
                                                <asp:ListItem Value="PAY-RCV">Payment Receive</asp:ListItem>
                                                <asp:ListItem Value="DO-ISU">D/O Issue</asp:ListItem>
                                                <asp:ListItem Value="VSL-IN">Vehicle In</asp:ListItem>
                                                <asp:ListItem Value="VSL-OUT">Vehicle Out</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cboTranType"
                                                ErrorMessage="Select Tran Type First" ForeColor="Red" InitialValue="0" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left" class="style14">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style16" style="width: 79px">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style10" style="width: 167px">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style10" style="width: 130px">
                                            Receiver Type
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left" class="style15">
                                            <asp:DropDownList ID="cboReceiverType" runat="server" Width="345px" AutoPostBack="True"
                                                OnSelectedIndexChanged="cboReceiverType_SelectedIndexChanged">
                                                <asp:ListItem Value="0">-----Select-----</asp:ListItem>
                                                <asp:ListItem>SINGLE</asp:ListItem>
                                                <asp:ListItem>MULTIPLE</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboReceiverType"
                                                ErrorMessage="Select Receiver Type First" ForeColor="Red" InitialValue="0" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left" class="style14">
                                            <%--<cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                                TargetControlID="RequiredFieldValidator1">
                                            </cc1:ValidatorCalloutExtender>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style16" style="width: 79px">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style10" style="width: 167px">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style10" style="width: 130px">
                                            Receiver Group Name
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left" class="style15">
                                            <asp:DropDownList ID="cboReceiverGroup" runat="server" Width="345px" Enabled="False">
                                                <asp:ListItem Value="0">-----Select-----</asp:ListItem>
                                                <asp:ListItem Value="INDV">Individual</asp:ListItem>
                                                <asp:ListItem Value="CUST">Customer</asp:ListItem>
                                                <asp:ListItem Value="DSM">Divisional Sales Manager</asp:ListItem>
                                                <asp:ListItem Value="MPO">Market Promotional Officer</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style14">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style16" style="width: 79px">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style10" style="width: 167px">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style10" style="width: 130px">
                                            Employee Name
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left" class="style15">
                                            <asp:TextBox ID="txtEmpName" runat="server" MaxLength="150" Width="340px" 
                                                Enabled="False" AutoPostBack="True" ontextchanged="txtEmpName_TextChanged"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderEmp" runat="server" BehaviorID="AutoCompleteSrchEmp"
                                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                                CompletionSetCount="20" DelimiterCharacters="," Enabled="True" MinimumPrefixLength="1"
                                                UseContextKey="true" ServiceMethod="GetSrchEmp" ServicePath="~/Module/HRMS/Forms/wsAutoComHrms.asmx"
                                                ShowOnlyCurrentWordInCompletionListItem="True" TargetControlID="txtEmpName">
                                            </cc1:AutoCompleteExtender>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style14">
                                            <%--<cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="CustomValidator"
                                                Enabled="True" TargetControlID="RequiredFieldValidator2">
                                            </cc1:ValidatorCalloutExtender>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style16" style="width: 79px">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style10" style="width: 167px">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style10" style="width: 130px">
                                            Designation
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left" class="style15">
                                            <asp:TextBox ID="txtDesignation" runat="server" MaxLength="150" Width="340px" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style14">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style16" style="width: 79px">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style10" style="width: 167px">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style10" style="width: 130px">
                                            Cell No
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left" class="style15">
                                            <asp:TextBox ID="txtCellNo" runat="server" MaxLength="150" Width="340px" Enabled="False"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style14">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style16" style="width: 79px">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style10" style="width: 167px">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style10" style="width: 130px">
                                            Status
                                        </td>
                                        <td align="center" width="9">
                                            :
                                        </td>
                                        <td align="left" class="style15">
                                            <asp:RadioButton ID="optActive" runat="server" Checked="True" GroupName="optSpStat"
                                                Text="Active" />
                                            &nbsp;<asp:RadioButton ID="optInactive" runat="server" GroupName="optSpStat" Text="Inactive" />
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                        <td align="left" class="style14">
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
                            <br />
                            <asp:GridView ID="gvSmsConfigList" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="#CCCCFF" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AllowPaging="True"
                                OnPageIndexChanging="gvSmsConfigList_PageIndexChanging" PageSize="50" OnRowDataBound="gvSmsConfigList_RowDataBound"
                                OnSelectedIndexChanged="gvSmsConfigList_SelectedIndexChanged" Font-Size="8pt">
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL #">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tran Type">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfConfigRefNo" runat="server" Value='<%# Bind("Config_Ref_No") %>' />
                                            <asp:Label ID="lblConfigTranType" runat="server" Text='<%# Bind("Config_Tran_Type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Config_Receiver_Type" HeaderText="Receiver Type" />
                                    <asp:BoundField DataField="Config_Receiver_Grp_Name" HeaderText="Receiver Group" />
                                    <asp:BoundField DataField="Config_Receiver_Emp_Ref" HeaderText="Employee ID" />
                                    <asp:BoundField DataField="Config_Receiver_Name" HeaderText="Employee Name" />
                                    <asp:BoundField DataField="Config_Receiver_Desig" HeaderText="Designaton" />
                                    <asp:BoundField DataField="Config_Receiver_Cell_No" HeaderText="Cell_No" />
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# GetStatus(int.Parse(Eval("Config_Status").ToString()))%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
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
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvSmsConfigList" EventName="DataBound" />
                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="hfConfigEditRef" runat="server" Value="0" />
        <%--<asp:HiddenField ID="hfHidden" runat="server" />--%>
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
