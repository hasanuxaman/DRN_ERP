<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmNode.aspx.cs" Inherits="DRN_WEB_ERP.frmNode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Node Setup</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="background-color: #CC66FF">
                <table style="border: 1px solid #800080; width: 55%; background-color: #FFCCFF;">
                    <tr>
                        <td style="width: 139px">
                            &nbsp;
                        </td>
                        <td style="width: 4px">
                            &nbsp;
                        </td>
                        <td style="width: 108px">
                            &nbsp;
                        </td>
                        <td style="width: 12px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 139px" align="right">
                            Module
                        </td>
                        <td style="width: 4px">
                            :
                        </td>
                        <td style="width: 108px" align="left">
                            <asp:DropDownList ID="cboModule" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 12px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cboModule"
                                ErrorMessage="Select Module" Font-Bold="True" ForeColor="Red" InitialValue="0"
                                ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 139px" align="right">
                            Node Code
                        </td>
                        <td style="width: 4px">
                            :
                        </td>
                        <td style="width: 108px">
                            <asp:TextBox ID="txtNodeCode" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 12px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Node Code"
                                ForeColor="Red" ControlToValidate="txtNodeCode" Font-Bold="True" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 139px" align="right">
                            Node Name
                        </td>
                        <td style="width: 4px">
                            :
                        </td>
                        <td style="width: 108px">
                            <asp:TextBox ID="txtNodeName" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 12px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Node Name"
                                ForeColor="Red" ControlToValidate="txtNodeName" Font-Bold="True" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 139px" align="right">
                            Node URL
                        </td>
                        <td style="width: 4px">
                            :
                        </td>
                        <td style="width: 108px" align="left">
                            <asp:TextBox ID="txtNodeUrl" runat="server" Width="300px"></asp:TextBox>
                        </td>
                        <td style="width: 12px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 139px" align="right">
                            Status
                        </td>
                        <td style="width: 4px">
                            :
                        </td>
                        <td style="width: 108px" align="left">
                            <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" />
                        </td>
                        <td style="width: 12px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 139px" align="right">
                            <asp:HiddenField ID="hfEditStatus" runat="server" Value="N" />
                            <asp:HiddenField ID="hfRefNo" runat="server" Value="0" />
                        </td>
                        <td style="width: 4px">
                            &nbsp;
                        </td>
                        <td style="width: 108px">
                            <asp:Button ID="btnClear" runat="server" Text="Clear" Width="100px" OnClick="btnClear_Click" />
                            &nbsp;<asp:Button ID="Save" runat="server" Text="Save" Width="100px" OnClick="Save_Click"
                                ValidationGroup="btnSave" />
                        </td>
                        <td style="width: 12px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Enter Valid Data:"
                                ShowMessageBox="false" ShowSummary="true" Font-Size="10pt" ForeColor="Red" ValidationGroup="btnSave" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="background-color: #6699FF">
                <br />
                Filter By:
                <asp:DropDownList ID="ddlNodeFilterByModule" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dlFilterByModule_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:GridView ID="gvNode" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#336666" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" EmptyDataText="No Data Found."
                    Font-Size="8pt" OnRowDataBound="gvNode_RowDataBound" OnSelectedIndexChanged="gvNode_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Node_Code" HeaderText="Node Code" />
                        <asp:BoundField DataField="Node_Name" HeaderText="Node Name" />
                        <asp:BoundField DataField="Node_Form_Url" HeaderText="Node URL" />
                        <asp:TemplateField HeaderText="Module">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Module_Ref") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HiddenField ID="hfNodRef" runat="server" Value='<%# Bind("Node_Ref") %>' />
                                <asp:Label ID="Label4" runat="server" Text='<%# GetModuleName(int.Parse(Eval("Module_Ref").ToString())) %>'></asp:Label>
                                <asp:HiddenField ID="hfModule" runat="server" Value='<%# Bind("Module_Ref") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Node_Entry_Date" HeaderText="Entry Date" Visible="False" />
                        <asp:TemplateField HeaderText="Entry User" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetUserName(int.Parse(Eval("Node_Entry_User").ToString())) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Node_Update_Date" HeaderText="Update Date" />
                        <asp:TemplateField HeaderText="Update User">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# GetUserName(int.Parse(Eval("Node_Update_User").ToString())) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Bind("Node_Status") %>' />
                                <asp:Label ID="Label3" runat="server" Text='<%# GetStatusName(int.Parse(Eval("Node_Status").ToString())) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" Font-Size="10pt"
                        HorizontalAlign="Left" Wrap="False" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px"
                        HorizontalAlign="Left" Wrap="False" Font-Size="8pt" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                </asp:GridView>
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlNodeFilterByModule" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
