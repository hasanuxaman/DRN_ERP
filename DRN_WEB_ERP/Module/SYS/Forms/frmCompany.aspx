<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmCompany.aspx.cs" Inherits="DRN_WEB_ERP.frmCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Company Setup</div>
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
                    Company Code
                </td>
                <td style="width: 4px">
                    :
                </td>
                <td style="width: 108px">
                    <asp:TextBox ID="txtCompCode" runat="server"></asp:TextBox>
                </td>
                <td style="width: 12px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Company Code"
                        ForeColor="Red" ControlToValidate="txtCompCode" Font-Bold="True">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 139px" align="right">
                    Company Name
                </td>
                <td style="width: 4px">
                    :
                </td>
                <td style="width: 108px">
                    <asp:TextBox ID="txtCompName" runat="server"></asp:TextBox>
                </td>
                <td style="width: 12px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Company Name"
                        ForeColor="Red" ControlToValidate="txtCompName" Font-Bold="True">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 139px" align="right">
                    Remarks
                </td>
                <td style="width: 4px">
                    :
                </td>
                <td style="width: 108px">
                    <asp:TextBox ID="txtCompRemarks" runat="server"></asp:TextBox>
                </td>
                <td style="width: 12px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 139px" align="right">
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
        <asp:GridView ID="gvCompany" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#336666" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" EmptyDataText="No Data are found"
            Font-Size="10pt">
            <Columns>
                <asp:TemplateField HeaderText="SL#">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Company_Code" HeaderText="Code" />
                <asp:BoundField DataField="Company_Name" HeaderText="Company Name" />
                <asp:BoundField DataField="Company_Remarks" HeaderText="Remarks" />
            </Columns>
            <EmptyDataTemplate>
                <div style="border: 1px solid #800080;">
                    No Data Found.</div>
            </EmptyDataTemplate>
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
