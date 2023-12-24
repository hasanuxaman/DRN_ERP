<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmModule.aspx.cs" Inherits="DRN_WEB_ERP.frmModule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Module Setup</div>
    <div align="center">
        <table style="border: 1px solid #800080; width: 30%; background-color: #FFCCFF;">
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
                    Company
                </td>
                <td style="width: 4px">
                    :
                </td>
                <td style="width: 108px" align="left">
                    <asp:DropDownList ID="cboCompany" runat="server" DataSourceID="dsCompany" DataTextField="Company_Name"
                        DataValueField="Company_Ref_No">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="dsCompany" runat="server" ConnectionString="<%$ ConnectionStrings:SYSConStr %>"
                        SelectCommand="SELECT * FROM [TBL_COMPANY]"></asp:SqlDataSource>
                </td>
                <td style="width: 12px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 139px" align="right">
                    Module Code
                </td>
                <td style="width: 4px">
                    :
                </td>
                <td style="width: 108px">
                    <asp:TextBox ID="txtModuleCode" runat="server"></asp:TextBox>
                </td>
                <td style="width: 12px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Module Code"
                        ForeColor="Red" ControlToValidate="txtModuleCode" Font-Bold="True">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 139px" align="right">
                    Module Name
                </td>
                <td style="width: 4px">
                    :
                </td>
                <td style="width: 108px">
                    <asp:TextBox ID="txtModuleName" runat="server"></asp:TextBox>
                </td>
                <td style="width: 12px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Module Name"
                        ForeColor="Red" ControlToValidate="txtModuleName" Font-Bold="True">*</asp:RequiredFieldValidator>
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
                    <asp:Button ID="Save" runat="server" Text="Save" Width="100px" OnClick="Save_Click" />
                </td>
                <td style="width: 12px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" colspan="4">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Enter Valid Data:"
                        ShowMessageBox="false" ShowSummary="true" Font-Size="10pt" ForeColor="Red" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div align="center">
        <asp:GridView ID="gvModule" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#336666" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" EmptyDataText="No Data  Found."
            Font-Size="10pt" OnRowDataBound="gvModule_RowDataBound" OnSelectedIndexChanged="gvModule_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField HeaderText="SL#">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Module_Code" HeaderText="Module Code" />
                <asp:BoundField DataField="Module_Name" HeaderText="Module Name" />
                <asp:TemplateField HeaderText="Company">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Company_Ref") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:HiddenField ID="hfModRef" runat="server" Value='<%# Bind("Module_Ref") %>' />
                        <asp:Label ID="Label4" runat="server" Text='<%# GetCompanyName(int.Parse(Eval("Company_Ref").ToString())) %>'></asp:Label>
                        <asp:HiddenField ID="hfCompany" runat="server" Value='<%# Bind("Company_Ref") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Entry Date" DataField="Module_Entry_Date" Visible="False" />
                <asp:TemplateField HeaderText="Entry User" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# GetUserName(int.Parse(Eval("Module_Entry_User").ToString())) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Update Date" DataField="Module_Update_Date" />
                <asp:TemplateField HeaderText="Update User">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# GetUserName(int.Parse(Eval("Module_Entry_User").ToString())) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Bind("Module_Status") %>' />
                        <asp:Label ID="Label3" runat="server" Text='<%# GetStatusName(int.Parse(Eval("Module_Status").ToString())) %>'></asp:Label>
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
    </div>
    <br />
</asp:Content>
