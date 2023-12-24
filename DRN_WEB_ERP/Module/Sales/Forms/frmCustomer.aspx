<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmCustomer.aspx.cs" Inherits="DRN_WEB_ERP.Module.Sales.Forms.frmCustomer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function CalcTotSecAmount(BGamt, FDamt, SDamt, LNDamt, CHQamt, NOAamt, TOTamt) {
            var BGAmt = 0;
            if (document.getElementById(BGamt).value != '') {
                BGAmt = parseFloat(document.getElementById(BGamt).value);
            }
            //alert("BG-" + BGAmt.toString());

            var FDAmt = 0;
            if (document.getElementById(FDamt).value != '') {
                FDAmt = parseFloat(document.getElementById(FDamt).value);
            }
            //alert("FD-" + FDAmt.toString());            

            var SDAmt = 0;
            if (document.getElementById(SDamt).value != '') {
                SDAmt = parseFloat(document.getElementById(SDamt).value);
            }
            //alert("SD-" + SDAmt.toString());

            var LNDAmt = 0;
            if (document.getElementById(LNDamt).value != '') {
                LNDAmt = parseFloat(document.getElementById(LNDamt).value);
            }
            //alert("LAND-" + LNDAmt.toString());

            var CHQAmt = 0;
            if (document.getElementById(CHQamt).value != '') {
                CHQAmt = parseFloat(document.getElementById(CHQamt).value);
            }
            //alert("CHQ-" + CHQAmt.toString());

            var NOAAmt = 0;
            if (document.getElementById(NOAamt).value != '') {
                NOAAmt = parseFloat(document.getElementById(NOAamt).value);
            }
            //alert("NOA-" + NOAAmt.toString());

            var TOTAmt = document.getElementById(TOTamt);
            var TotSecAmt = parseFloat(BGAmt + FDAmt + SDAmt + LNDAmt + CHQAmt + NOAAmt);
            TOTAmt.value = TotSecAmt.toFixed(2);
            //alert("Tot-" + TotSecAmt.toString());
        }
    </script>
    <div align="center" style="background-color: #00FF99">
        Customer Information</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="border: 1px solid #CCCCCC; width: 55%; height: 435px; background-color: #CCFFFF;">
                <table style="width: 100%; background-color: #CCFFFF; font-size: small; height: 436px;">
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td style="width: 300px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Customer Ref
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="txtCustRefNo" runat="server" BackColor="#CCCCFF" BorderStyle="Solid"
                                CssClass="textAlignCenter" Enabled="False" Width="352px" Font-Bold="True"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Customer Name
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="txtCustName" runat="server" CssClass="capitalize" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCustName"
                                ErrorMessage="Enter Customer Name" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Customer Type
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:DropDownList ID="cboCustType" runat="server" ValidationGroup="ChkData" Width="353px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="cboCustType"
                                ErrorMessage="Select Customer Type" ForeColor="Red" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Address
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px" rowspan="3" valign="top">
                            <asp:TextBox ID="txtCustAdr" runat="server" Height="60px" TextMode="MultiLine" Width="348px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            &nbsp;
                        </td>
                        <td style="width: 3px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Contact Person (CP)
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="txtCustCp" runat="server" CssClass="capitalize" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            CP Designation
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="txtCustCpDesig" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Cell No
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="txtCustCell" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Phone No
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="txtCustPhone" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Fax No
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="txtCustFax" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            E-Mail
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="txtCustEmail" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            District
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:DropDownList ID="cboCustDist" runat="server" ValidationGroup="ChkData" Width="353px"
                                AutoPostBack="True" OnSelectedIndexChanged="cboCustDist_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboCustDist"
                                ErrorMessage="Select District" ForeColor="Red" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Thana
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:DropDownList ID="cboCustThana" runat="server" ValidationGroup="ChkData" Width="353px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cboCustThana"
                                ErrorMessage="Select Thana" ForeColor="Red" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td style="width: 126px">
                            Sales Zone
                        </td>
                        <td style="width: 3px">
                            :
                        </td>
                        <td style="width: 300px">
                            <asp:DropDownList ID="cboCustZone" runat="server" ValidationGroup="ChkData" Width="353px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cboCustZone"
                                ErrorMessage="Select Sales Zone" ForeColor="Red" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 32px">
                            &nbsp;
                        </td>
                        <td colspan="3">
                            &nbsp;
                            <asp:Button ID="btnCrSecurity" runat="server" OnClick="btnCrSecurity_Click" Text="CreditSecurity" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div style="border: 1px solid #CCCCCC; width: 527px; height: 400px; position: absolute;
                float: right; top: 88px; left: 820px; width: 487px; background-color: #CCFFFF;">
                <table style="width: 100%; background-color: #CCFFFF; font-size: small; height: 436px;">
                    <tr>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 143px">
                            &nbsp;
                        </td>
                        <td style="width: 2px">
                            &nbsp;
                        </td>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 143px;">
                            Credit Limit
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td style="width: 258px">
                            <asp:TextBox ID="txtCustCrLimit" runat="server" Width="100px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCustCrLimit_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCustCrLimit"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                            <asp:Button ID="Button1" runat="server" Height="20px" Text="New Limit" Visible="False" />
                            <asp:Button ID="Button2" runat="server" Height="20px" Text="Update" Width="68px"
                                Visible="False" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtCustCrLimit"
                                ErrorMessage="Enter Credit Limit Amount" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtCustCrLimit"
                                ErrorMessage="Enter Valid Amount" ForeColor="Red" Operator="DataTypeCheck" Type="Currency"
                                ValidationGroup="Save">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 143px;">
                            Credit Period (days)
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td style="width: 258px">
                            <asp:TextBox ID="txtCustCrPeriod" runat="server" Width="250px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCustCrPeriod"
                                ErrorMessage="Enter Credit Limit Period" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtCustCrPeriod"
                                ErrorMessage="Enter Valid Period" ForeColor="Red" Operator="DataTypeCheck" Type="Integer"
                                ValidationGroup="Save">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 143px;">
                            Pay Terms
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td style="width: 258px">
                            <asp:TextBox ID="txtCustCrPayTerm" runat="server" Width="250px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 143px; text-decoration: underline;">
                            Credit Limit Support Doc
                        </td>
                        <td style="width: 2px">
                            &nbsp;
                        </td>
                        <td style="width: 258px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 143px;">
                            Bank Gurantee
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td style="width: 258px">
                            <asp:TextBox ID="txtCrSecBG" runat="server" CssClass="textAlignCenter" Font-Bold="True"
                                Width="250px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCrSecBG_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCrSecBG"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 143px;">
                            ITR
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td style="width: 258px">
                            <asp:TextBox ID="txtCrSecFDR" runat="server" CssClass="textAlignCenter" Font-Bold="True"
                                Width="250px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCrSecFDR_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCrSecFDR"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 143px;">
                            Deposit
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td style="width: 258px">
                            <asp:TextBox ID="txtCrSecSDR" runat="server" CssClass="textAlignCenter" Font-Bold="True"
                                Width="250px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCrSecSDR_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCrSecSDR"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 143px;">
                            Post Dated Cheque
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td style="width: 258px">
                            <asp:TextBox ID="txtCrSecLAND" runat="server" CssClass="textAlignCenter" Font-Bold="True"
                                Width="250px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCrSecLAND_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCrSecLAND"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 143px;">
                            Undated Cheque
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td style="width: 258px">
                            <asp:TextBox ID="txtCrSecCHQ" runat="server" CssClass="textAlignCenter" Font-Bold="True"
                                Width="250px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCrSecCHQ_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCrSecCHQ"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 143px;">
                            Notification of Award
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td style="width: 258px">
                            <asp:TextBox ID="txtCrSecNOA" runat="server" CssClass="textAlignCenter" Font-Bold="True"
                                Width="250px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCrSecNOA_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCrSecNOA"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px; height: 22px;">
                            &nbsp;
                        </td>
                        <td style="width: 143px; font-weight: bold;">
                            Total Security
                        </td>
                        <td style="width: 2px; height: 22px;">
                            :
                        </td>
                        <td style="height: 22px; width: 258px;" valign="top">
                            <asp:TextBox ID="txtCrSecTotAmt" runat="server" Enabled="False" Font-Bold="True"
                                ForeColor="#3399FF" CssClass="textAlignCenter" Width="250px"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="txtCrSecTotAmt_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtCrSecTotAmt"
                                ValidChars=".">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="height: 22px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px; height: 22px;">
                            &nbsp;
                        </td>
                        <td style="width: 143px;">
                            Account Code
                        </td>
                        <td style="width: 2px; height: 22px;">
                            :
                        </td>
                        <td style="height: 22px; width: 258px;" valign="top">
                            <asp:TextBox ID="txtCustAccCode" runat="server" Enabled="False" Width="250px"></asp:TextBox>
                        </td>
                        <td style="height: 22px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 143px;">
                            DSM Name
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td style="width: 258px">
                            <asp:DropDownList ID="cboDsm" runat="server" Width="255px" AutoPostBack="True" OnSelectedIndexChanged="cboDsm_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator244" runat="server" ControlToValidate="cboDsm"
                                ErrorMessage="Select DSM" ForeColor="Red" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 143px;">
                            SPO Name
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td style="width: 258px">
                            <asp:DropDownList ID="cboSpo" runat="server" Width="255px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 143px;">
                            Sales Wing
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td style="width: 258px">
                            <asp:DropDownList ID="cboSalesWing" runat="server" Width="255px">
                                <%--<asp:ListItem Value="0">----Select---</asp:ListItem>
                                <asp:ListItem Value="1">Wing 1 (Sayem) </asp:ListItem>
                                <asp:ListItem Value="2">Wing 2 (Others) </asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator245" runat="server" ControlToValidate="cboSalesWing"
                                ErrorMessage="Select Sales Wing" ForeColor="Red" InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 34px">
                            &nbsp;
                        </td>
                        <td style="width: 143px;">
                            Status
                        </td>
                        <td style="width: 2px">
                            :
                        </td>
                        <td style="width: 258px">
                            <asp:RadioButtonList ID="optListCustStatus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Save"
                    Font-Size="8" ForeColor="Red" Width="350px" />
            </div>
            <div align="center" style="background-color: #66FFFF">
                <br />
                <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="Clear" Width="100px"
                    OnClick="btnClear_Click" />
                <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" ValidationGroup="Save"
                    Width="100px" OnClick="btnSave_Click" />
                <br />
                <br />
            </div>
            <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="btnSearch">
                <div align="center" style="background-color: #86AEAE">
                    <span>
                        <br />
                        Sales Zone:
                        <asp:DropDownList ID="cboCustZoneSrch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboCustZoneSrch_SelectedIndexChanged"
                            ValidationGroup="ChkData" Width="353px">
                        </asp:DropDownList>
                        &nbsp; Customer:</span> <span>
                            <asp:TextBox ID="txtSearch" runat="server" Width="350px" CssClass="search textAlignCenter"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrch" runat="server" BehaviorID="AutoCompleteSrch"
                                CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchCustomerByZone"
                                ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                                TargetControlID="txtSearch">
                            </cc1:AutoCompleteExtender>
                            <asp:Button ID="btnSearch" runat="server" onkeypress="return clickButton(event,'btnSearch')"
                                Text="Search" ValidationGroup="btnSearch" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnClearSrch" runat="server" Text="Clear" Visible="False" Width="60px"
                                OnClick="btnClearSrch_Click" />
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSearch"
                                ErrorMessage="Enter Search Text" Font-Bold="False" ForeColor="Red" ValidationGroup="btnSearch"
                                Font-Size="10pt"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                        </span>
                    <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click"
                        Enabled="False" />
                </div>
                <div align="center" style="background-color: #86AEAE">
                    <asp:GridView ID="gvCust" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" EmptyDataText="No Data  Found."
                        Font-Size="8pt" AllowPaging="True" PageSize="50" AllowSorting="True" OnRowDataBound="gvCust_RowDataBound"
                        OnPageIndexChanging="gvCust_PageIndexChanging" OnSelectedIndexChanged="gvCust_SelectedIndexChanged"
                        OnSorting="gvCust_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="Cust. Ref.">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfCustRef" runat="server" Value='<%# Bind("Par_Adr_Ref") %>' />
                                    <asp:Label ID="lblCustRefNo" runat="server" Text='<%# Bind("Par_Adr_Ref_No") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="60px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Par_Adr_Name" HeaderText="Name" SortExpression="Par_Adr_Name">
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Adr_Addr" HeaderText="Address">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Adr_Cont_Per" HeaderText="Contact Person">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Adr_Cell_No" HeaderText="Cell No">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Adr_Tel_No" HeaderText="Phone No">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Adr_Email_Id" HeaderText="Email">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Adr_Cr_Limit" HeaderText="Cr Limit" SortExpression="Par_Adr_Cr_Limit"
                                DataFormatString="{0:N2}">
                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Par_Adr_Cr_Days" HeaderText="Cr Days" SortExpression="Par_Adr_Cr_Days">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Sales Zone">
                                <ItemTemplate>
                                    <asp:Label ID="lblSalesZone" runat="server" Text='<%# GetSalesZone(Eval("Par_Adr_Sale_Zone").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Par_Adr_Ext_Data2" HeaderText="MPO Ref" />
                            <asp:TemplateField HeaderText="MPO Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblMpoName" runat="server" Text='<%# GetMpoName(Eval("Par_Adr_Ext_Data2").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Par_Adr_Status" HeaderText="Status" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" Font-Size="8pt"
                            HorizontalAlign="Left" Wrap="False" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px"
                            HorizontalAlign="Left" Wrap="true" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#487575" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#275353" />
                    </asp:GridView>
                    <span>
                        <asp:HiddenField ID="hfEditStatus" runat="server" Value="N" />
                        <asp:HiddenField ID="hfRefNo" runat="server" Value="0" />
                    </span>
                    <br />
                    <asp:Button ID="btnUpdtCustGl" runat="server" Text="Update Customeer GL Account"
                        OnClick="btnUpdtCustGl_Click" Visible="False" />
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboCustDist" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClearSrch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvCust" EventName="RowDataBound" />
            <asp:AsyncPostBackTrigger ControlID="gvCust" EventName="PageIndexChanging" />
            <asp:AsyncPostBackTrigger ControlID="gvCust" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btnExport" />
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
