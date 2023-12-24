<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmStaffMas.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmStaffMas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .fancy-green .ajax__tab_header
        {
            background: url(green_bg_Tab.gif) repeat-x;
            cursor: pointer;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer
        {
            background: url(green_left_Tab.gif) no-repeat left top;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner
        {
            background: url(green_right_Tab.gif) no-repeat right top;
        }
        .fancy .ajax__tab_header
        {
            font-size: 13px;
            font-weight: bold;
            color: #000;
            font-family: sans-serif;
        }
        .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer
        {
            height: 56px;
        }
        .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner
        {
            height: 56px;
            margin-left: 16px; /* offset the width of the left image */
        }
        .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab
        {
            margin: 16px 16px 0px 0px;
        }
        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab
        {
            color: #fff;
        }
        .fancy .ajax__tab_body
        {
            font-family: Arial;
            font-size: 10pt;
            border-top: 0;
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
        }
        .style1
        {
            width: 91px;
        }
        .style2
        {
            height: 20px;
        }
        .style3
        {
            height: 20px;
            width: 89px;
        }
        .style5
        {
            width: 89px;
        }
    </style>
    <script type="text/javascript">
        ////Autocomplete Start
        //        function acePopulated(sender, e) {
        //            var behavior = $find('AutoCompleteSrchEmp');
        //            var target = behavior.get_completionList();
        //            target.style.width = 'auto';
        //            if (behavior._currentPrefix != null) {
        //                var prefix = behavior._currentPrefix.toLowerCase();
        //                var i;
        //                for (i = 0; i < target.childNodes.length; i++) {
        //                    var sValue = target.childNodes[i].innerHTML.toLowerCase();
        //                    if (sValue.indexOf(prefix) != -1) {
        //                        var fstr = target.childNodes[i].innerHTML.substring(0, sValue.indexOf(prefix));
        //                        var pstr = target.childNodes[i].innerHTML.substring(fstr.length, fstr.length + prefix.length);
        //                        var estr = target.childNodes[i].innerHTML.substring(fstr.length + prefix.length, target.childNodes[i].innerHTML.length);
        //                        target.childNodes[i].innerHTML = "<div class='autocomplete-item'>" + fstr + '<B><font color="#990000">' + pstr + '</font></B>' + estr + "</div>";
        //                    }
        //                }
        //            }
        //        }

        //        function aceSelected(sender, e) {
        //            var value = e.get_value();
        //            if (!value) {
        //                if (e._item.parentElement && e._item.parentElement.tagName == "LI")
        //                    value = e._item.parentElement.attributes["_value"].value;
        //                else if (e._item.parentElement && e._item.parentElement.parentElement.tagName == "LI")
        //                    value = e._item.parentElement.parentElement.attributes["_value"].value;
        //                else if (e._item.parentNode && e._item.parentNode.tagName == "LI")
        //                    value = e._item.parentNode._value;
        //                else if (e._item.parentNode && e._item.parentNode.parentNode.tagName == "LI")
        //                    value = e._item.parentNode.parentNode._value;
        //                else value = "";
        //            }
        //            var searchText = '';
        //            var searchText = $get('<%=txtSrchEmp.ClientID %>').value;
        //            searchText = searchText.replace('null', '');
        //            sender.get_element().value = searchText + value;
        //        }
        ////Autocomplete End    

        //Enable enter key for search textbox
        function clickButton(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {
                if (evt.keyCode == 13) {
                    bt.click();
                    return false;
                }
            }
        }

        function CheckPicFileSize(source, arguments) {
            arguments.IsValid = false;
            var size = document.getElementById("<%=picUpload.ClientID%>").files[0].size;
            if (size > 1048576) {
                arguments.IsValid = false;
                return false;
            }
            else {
                arguments.IsValid = true;
                return true;
            }
        }

        function CheckSigFileSize(source, arguments) {
            arguments.IsValid = false;
            var size = document.getElementById("<%=sigUpload.ClientID%>").files[0].size;
            if (size > 1048576) {
                arguments.IsValid = false;
                return false;
            }
            else {
                arguments.IsValid = true;
                return true;
            }
        }

        function CheckNidFileSize(source, arguments) {
            arguments.IsValid = false;
            var size = document.getElementById("<%=nidUpload.ClientID%>").files[0].size;
            if (size > 1048576) {
                arguments.IsValid = false;
                return false;
            }
            else {
                arguments.IsValid = true;
                return true;
            }
        }
                                              
    </script>
    <div align="center" style="font-weight: bold; font-style: italic; font-variant: normal;
        text-transform: capitalize;">
        Driver/Mechanic Information</div>
    <div align="center" style="background-color: #CC66FF">
        Search:<asp:TextBox ID="txtSrchEmp" runat="server" CssClass="search" Width="400"
            ForeColor="#0066FF"></asp:TextBox>
        <asp:AutoCompleteExtender ID="AutoCompleteExtenderEmp" runat="server" BehaviorID="AutoCompleteSrchEmp"
            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
            DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchEmp" ServicePath="~/Module/Transport/Forms/wsAutoCompleteTransport.asmx"
            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSrchEmp">
        </asp:AutoCompleteExtender>
        <asp:CheckBox ID="chkIncTermEmp" runat="server" AutoPostBack="True" OnCheckedChanged="chkIncTermEmp_CheckedChanged"
            Text="Include Settled Staff" />
        &nbsp;<asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" onkeypress="return clickButton(event,'btnSearch')"
            Text="Search" ValidationGroup="btnSearch" Width="60px" />
        &nbsp;<asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear"
            Visible="False" Width="60px" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSrchEmp"
            ErrorMessage="Enter Search Text" Font-Bold="False" ForeColor="#CC3300" ValidationGroup="btnSearch"
            Font-Size="12pt"></asp:RequiredFieldValidator>
        <asp:HiddenField ID="hfEmpRef" Value="0" runat="server" />
    </div>
    <div align="center" style="background-color: #CCFFFF">
        <table style="width: 100%;" align="center">
            <tr>
                <td valign="middle" align="left" style="width: 191px">
                    &nbsp;
                </td>
                <td valign="middle" align="left" style="width: 94px">
                    Staff Type
                </td>
                <td valign="middle" align="left" style="width: 2px">
                    :
                </td>
                <td valign="middle" align="left" style="width: 189px">
                    <asp:DropDownList ID="cboStaffType" runat="server">
                        <asp:ListItem Value="Driver">Driver</asp:ListItem>
                        <asp:ListItem Value="Mechanic">Helper</asp:ListItem>
                        <asp:ListItem Value="Mechanic">Mechanic</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td valign="middle" align="left" style="width: 2px">
                    &nbsp;
                </td>
                <td valign="middle" align="center" rowspan="4" style="width: 166px">
                    <asp:HyperLink ID="hlEmpPicPreview" Target="_blank" runat="server">
                        <asp:Image ID="imgEmpPicPreview" runat="server" Height="90px" Width="100px" ImageUrl="~/Image/NoImage.gif"
                            BorderStyle="Ridge" BorderColor="White" BorderWidth="5" />
                    </asp:HyperLink>
                </td>
                <td valign="middle" align="left">
                    &nbsp;
                </td>
                <td valign="middle" align="left" class="style1">
                    &nbsp;
                </td>
                <td valign="middle" align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td valign="middle" align="left" style="width: 191px">
                </td>
                <td valign="middle" align="left" style="width: 94px">
                    Office ID
                </td>
                <td valign="middle" align="left" style="width: 2px">
                    :
                </td>
                <td valign="middle" align="left" style="width: 189px">
                    <asp:TextBox ID="txtEmpId" runat="server" BorderStyle="Solid" Enabled="False" Font-Bold="True"
                        BorderWidth="1px" CssClass="textAlignCenter" Width="150px"></asp:TextBox>
                </td>
                <td valign="middle" align="left" style="width: 2px">
                </td>
                <td valign="middle" align="left">
                </td>
                <td valign="middle" align="left" class="style1">
                </td>
                <td valign="middle" align="left">
                </td>
            </tr>
            <tr>
                <td valign="middle" align="left" style="width: 191px">
                </td>
                <td valign="middle" align="left" style="width: 94px">
                    First Name
                </td>
                <td valign="middle" align="left" style="width: 2px">
                    :
                </td>
                <td valign="middle" align="left" style="width: 189px">
                    <asp:TextBox ID="txtEmpFirstName" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td valign="middle" align="left" style="width: 2px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmpFirstName"
                        ErrorMessage="Enter First Name" ForeColor="Red" ValidationGroup="SaveGenInfo">*</asp:RequiredFieldValidator>
                </td>
                <td valign="middle" align="left">
                </td>
                <td valign="middle" align="left" class="style1">
                    <asp:Button ID="btnSettle" runat="server" OnClick="btnSettle_Click" Text="Settlement"
                        Visible="False" />
                </td>
                <td valign="middle" align="left">
                </td>
            </tr>
            <tr>
                <td valign="middle" align="left" style="width: 191px">
                </td>
                <td valign="middle" align="left" style="width: 94px">
                    Last Name
                </td>
                <td valign="middle" align="left" style="width: 2px">
                    :
                </td>
                <td valign="middle" align="left" style="width: 189px">
                    <asp:TextBox ID="txtEmpLastName" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td valign="middle" align="left" style="width: 2px">
                </td>
                <td valign="middle" align="left">
                </td>
                <td valign="middle" align="left" class="style1">
                </td>
                <td valign="middle" align="left">
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="SaveGenInfo"
            Font-Size="8pt" />
        <asp:ValidationSummary ID="ValidationSummary5" runat="server" ForeColor="Red" ValidationGroup="SaveExtInfo"
            Font-Size="8pt" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" ValidationGroup="SaveOffcInfo"
            Font-Size="8pt" />
    </div>
    <div align="center" style="background-color: #99FF33">
        <span style="float: left; width: 50%; background-color: #99FF33;">
            <asp:LinkButton ID="lnkBtnPrev" runat="server" OnClick="btnPrev_Click">&lt;&lt;Prev Tab</asp:LinkButton></span>
        <span style="float: right; width: 50%; background-color: #99FF33;">
            <asp:LinkButton ID="lnkBtnNext" runat="server" OnClick="btnNext_Click">Next Tab&gt;&gt;</asp:LinkButton></span>
    </div>
    <div>
        <br />
    </div>
    <div style="width: 100%;">
        <asp:TabContainer ID="tabEmpInfo" runat="server" CssClass="fancy fancy-green" ActiveTabIndex="4">
            <asp:TabPanel runat="server" ID="TabPnlPersonal">
                <HeaderTemplate>
                    Personal
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:Panel ID="PnlPersonal" runat="server">
                        <table style="width: 100%; background-color: #CCFFFF">
                            <tr>
                                <td style="width: 23px">
                                </td>
                                <td align="center" colspan="3" style="text-decoration: underline; background-color: #9999FF;">
                                    Personal Info
                                </td>
                                <td style="width: 70px">
                                </td>
                                <td align="center" colspan="3" style="text-decoration: underline; background-color: #9999FF;">
                                    Present Address
                                </td>
                                <td style="width: 78px">
                                </td>
                                <td align="center" colspan="3" style="text-decoration: underline; background-color: #9999FF;">
                                    Permanent Address
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 23px">
                                </td>
                                <td style="width: 114px">
                                    Father&#39;s Name
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 206px">
                                    <asp:TextBox ID="txtEmpFathersName" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 70px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmpFathersName"
                                        ErrorMessage="Enter Father's Name" ForeColor="Red" ValidationGroup="SaveGenInfo">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 98px">
                                    House/Road
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 152px">
                                    <asp:TextBox ID="txtEmpPresAdrHouse" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 78px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEmpPresAdrHouse"
                                        ErrorMessage="Enter Present Address" ForeColor="Red" ValidationGroup="SaveGenInfo">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 104px">
                                    House/Road
                                </td>
                                <td style="width: 2px">
                                    :
                                </td>
                                <td style="width: 134px">
                                    <asp:TextBox ID="txtEmpPrmAdrHouse" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtEmpPrmAdrHouse"
                                        ErrorMessage="Enter Permanent Address" ForeColor="Red" ValidationGroup="SaveGenInfo">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 23px">
                                </td>
                                <td style="width: 114px">
                                    Mother&#39;s Name
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 206px">
                                    <asp:TextBox ID="txtEmpMothersName" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 70px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEmpMothersName"
                                        ErrorMessage="Enter Mother's Name" ForeColor="Red" ValidationGroup="SaveGenInfo">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 98px">
                                    Post Office
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 152px">
                                    <asp:TextBox ID="txtEmpPresAdrPO" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 78px">
                                </td>
                                <td style="width: 104px">
                                    Post Office
                                </td>
                                <td style="width: 2px">
                                    :
                                </td>
                                <td style="width: 134px">
                                    <asp:TextBox ID="txtEmpPrmAdrPO" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 23px">
                                </td>
                                <td style="width: 114px">
                                    Gender
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 206px">
                                    <asp:DropDownList ID="cboEmpGender" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 70px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cboEmpGender"
                                        ErrorMessage="Select Gender" ForeColor="Red" InitialValue="0" ValidationGroup="SaveGenInfo">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 98px">
                                    Post Code
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 152px">
                                    <asp:TextBox ID="txtEmpPresAdrPOCode" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 78px">
                                </td>
                                <td style="width: 104px">
                                    Post Code
                                </td>
                                <td style="width: 2px">
                                    :
                                </td>
                                <td style="width: 134px">
                                    <asp:TextBox ID="txtEmpPrmAdrPOCode" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 23px">
                                </td>
                                <td style="width: 114px">
                                    Marital Status
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 206px">
                                    <asp:DropDownList ID="cboEmpMaritalStat" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 70px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cboEmpMaritalStat"
                                        ErrorMessage="Select Marital Status" ForeColor="Red" ValidationGroup="SaveGenInfo"
                                        InitialValue="0">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 98px">
                                    Thana
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 152px">
                                    <asp:TextBox ID="txtEmpPresAdrThana" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 78px">
                                </td>
                                <td style="width: 104px">
                                    Thana
                                </td>
                                <td style="width: 2px">
                                    :
                                </td>
                                <td style="width: 134px">
                                    <asp:TextBox ID="txtEmpPrmAdrThana" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 23px">
                                </td>
                                <td style="width: 114px">
                                    Spouse Name
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 206px">
                                    <asp:TextBox ID="txtEmpSpouseName" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 70px">
                                </td>
                                <td style="width: 98px">
                                    District
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 152px">
                                    <asp:TextBox ID="txtEmpPresAdrDist" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 78px">
                                </td>
                                <td style="width: 104px">
                                    District
                                </td>
                                <td style="width: 2px">
                                    :
                                </td>
                                <td style="width: 134px">
                                    <asp:TextBox ID="txtEmpPrmAdrDist" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 23px">
                                </td>
                                <td style="width: 114px">
                                    Date Of Birth
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 206px">
                                    <asp:TextBox ID="txtEmpDOB" runat="server"></asp:TextBox>
                                    <asp:Image ID="imgDob" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <asp:CalendarExtender ID="txtDobCalender" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtEmpDOB" PopupButtonID="imgDob">
                                    </asp:CalendarExtender>
                                </td>
                                <td style="width: 70px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmpDOB"
                                        ErrorMessage="Enter Date of Birth" ForeColor="Red" ValidationGroup="SaveGenInfo">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtEmpDOB"
                                        ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck" Type="Date"
                                        ValidationGroup="SaveGenInfo">*</asp:CompareValidator>
                                </td>
                                <td style="width: 98px">
                                </td>
                                <td style="width: 1px">
                                </td>
                                <td style="width: 152px">
                                </td>
                                <td style="width: 78px">
                                </td>
                                <td style="width: 104px">
                                </td>
                                <td style="width: 2px">
                                </td>
                                <td style="width: 134px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 23px">
                                </td>
                                <td style="width: 114px">
                                    Place Of Birth
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 206px">
                                    <asp:TextBox ID="txtEmpPOB" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 70px">
                                </td>
                                <td style="width: 98px">
                                </td>
                                <td style="width: 1px">
                                </td>
                                <td style="width: 152px">
                                </td>
                                <td style="width: 78px">
                                </td>
                                <td style="width: 104px">
                                </td>
                                <td style="width: 2px">
                                </td>
                                <td style="width: 134px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 23px">
                                </td>
                                <td align="center" colspan="11" style="text-decoration: underline; background-color: #9999FF;">
                                    Emergency Contact
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 23px">
                                </td>
                                <td style="width: 114px">
                                    Name
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 206px">
                                    <asp:TextBox ID="txtEmpEmerAdrName" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 70px">
                                </td>
                                <td style="width: 98px">
                                    Relation
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 152px">
                                    <asp:TextBox ID="txtEmpEmerAdrRelation" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 78px">
                                </td>
                                <td style="width: 104px">
                                    House/Road
                                </td>
                                <td style="width: 2px">
                                    :
                                </td>
                                <td style="width: 134px">
                                    <asp:TextBox ID="txtEmpEmerAdrHouse" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 23px">
                                </td>
                                <td style="width: 114px">
                                    Post Office
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 206px">
                                    <asp:TextBox ID="txtEmpEmerAdrPO" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 70px">
                                </td>
                                <td style="width: 98px">
                                    Post Code
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 152px">
                                    <asp:TextBox ID="txtEmpEmerAdrPOCode" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 78px">
                                </td>
                                <td style="width: 104px">
                                    Thana
                                </td>
                                <td style="width: 2px">
                                    :
                                </td>
                                <td style="width: 134px">
                                    <asp:TextBox ID="txtEmpEmerAdrThana" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 23px">
                                </td>
                                <td style="width: 114px">
                                    District
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 206px">
                                    <asp:TextBox ID="txtEmpEmerAdrDist" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 70px">
                                </td>
                                <td style="width: 98px">
                                    Phone
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td style="width: 152px">
                                    <asp:TextBox ID="txtEmpEmerAdrPhone" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 78px">
                                </td>
                                <td style="width: 104px">
                                    Cell
                                </td>
                                <td style="width: 2px">
                                    :
                                </td>
                                <td style="width: 134px">
                                    <asp:TextBox ID="txtEmpEmerAdrCell" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <div align="center" style="background-color: #99FF33">
                            <asp:Button ID="btnSaveGenInfo" runat="server" Text="Update" Width="150px" OnClick="btnSaveGenInfo_Click"
                                ValidationGroup="SaveGenInfo" Visible="False" />
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPnlIdenti" runat="server">
                <HeaderTemplate>
                    Identification
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:Panel ID="PnlIdenti" runat="server">
                        <table style="width: 100%; background-color: #CCFFFF">
                            <tr>
                                <td style="width: 92px">
                                </td>
                                <td style="width: 136px">
                                    Blood Group
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 187px">
                                    <asp:DropDownList ID="cboBloodGrp" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 90px;">
                                </td>
                                <td style="width: 134px">
                                    National ID
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 207px">
                                    <asp:TextBox ID="txtNID" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 92px">
                                </td>
                                <td style="width: 136px">
                                    Religion
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 187px">
                                    <asp:DropDownList ID="cboReligion" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 90px">
                                </td>
                                <td style="width: 134px">
                                    TIN No
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 207px">
                                    <asp:TextBox ID="txtTIN" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 92px">
                                </td>
                                <td style="width: 136px">
                                    Nationality
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 187px">
                                    <asp:TextBox ID="txtNationality" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 90px">
                                </td>
                                <td style="width: 134px">
                                    Passport No
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 207px">
                                    <asp:TextBox ID="txtPPNo" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 92px">
                                </td>
                                <td style="width: 136px">
                                    Home Phone No
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 187px">
                                    <asp:TextBox ID="txtHomePhone" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 90px">
                                    &nbsp;
                                </td>
                                <td style="width: 134px">
                                    Issue Date
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 207px">
                                    <asp:TextBox ID="txtPPIsuDate" runat="server"></asp:TextBox>
                                    <asp:Image ID="imgPPIssuDate" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtPPIsuDate" PopupButtonID="imgPPIssuDate">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtPPIsuDate"
                                        ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck" Type="Date"
                                        ValidationGroup="SaveExtInfo">*</asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 92px">
                                </td>
                                <td style="width: 136px">
                                    Cell No
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 187px">
                                    <asp:TextBox ID="txtPersonalCell" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 90px">
                                </td>
                                <td style="width: 134px">
                                    Expire Date
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 207px">
                                    <asp:TextBox ID="txtPPExpDate" runat="server"></asp:TextBox>
                                    <asp:Image ID="imgPPExpDate" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtPPExpDate" PopupButtonID="imgPPExpDate">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtPPExpDate"
                                        ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck" Type="Date"
                                        ValidationGroup="SaveExtInfo">*</asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 92px">
                                </td>
                                <td style="width: 136px">
                                    E-Mail
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 187px">
                                    <asp:TextBox ID="txtPersonalEmail" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 90px">
                                </td>
                                <td style="width: 134px">
                                    Place Of Issue
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 207px">
                                    <asp:TextBox ID="txtPPIsuPlace" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 92px">
                                </td>
                                <td style="width: 136px">
                                    Identification Mark
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 187px">
                                    <asp:TextBox ID="txtIdentiMark" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 90px">
                                    &nbsp;
                                </td>
                                <td style="width: 134px">
                                    Driving License No
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 207px">
                                    <asp:TextBox ID="txtDLNo" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 92px">
                                </td>
                                <td style="width: 136px">
                                    Height (cm)
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 187px">
                                    <asp:TextBox ID="txtHeight" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 90px">
                                </td>
                                <td style="width: 134px">
                                    Expire Date
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 207px">
                                    <asp:TextBox ID="txtDLExprireDate" runat="server"></asp:TextBox>
                                    <asp:Image ID="imgDLExpDate" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtDLExprireDate" PopupButtonID="imgDLExpDate">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtDLExprireDate"
                                        ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck" Type="Date"
                                        ValidationGroup="SaveExtInfo">*</asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 92px">
                                </td>
                                <td style="width: 136px">
                                    Weight (kg)
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 187px">
                                    <asp:TextBox ID="txtWeight" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 90px">
                                    &nbsp;
                                </td>
                                <td style="width: 134px">
                                    Place Of Issue
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 207px">
                                    <asp:TextBox ID="txtDLIssuPlace" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <div align="center" style="background-color: #99FF33">
                            <asp:Button ID="btnSaveIndeni" runat="server" Text="Update" Width="150px" OnClick="btnSaveIndeni_Click"
                                ValidationGroup="SaveExtInfo" Visible="False" />
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPnlQual" runat="server">
                <HeaderTemplate>
                    Qualification
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:Panel ID="PnlQual" runat="server">
                        <table style="width: 100%; background-color: #CCFFFF">
                            <tr>
                                <td style="width: 145px">
                                </td>
                                <td style="width: 108px">
                                    Type
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 140px">
                                    <asp:DropDownList ID="cboQualType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboQualType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 92px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="cboQualType"
                                        ErrorMessage="Select Qualification Type" ForeColor="Red" InitialValue="0" ValidationGroup="SaveQualInfo">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 98px">
                                    Passing Year
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 148px">
                                    <asp:TextBox ID="txtQualPassYr" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 59px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtQualPassYr"
                                        ErrorMessage="Enter Passing Year" ForeColor="Red" ValidationGroup="SaveQualInfo">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="txtQualPassYr"
                                        ErrorMessage="Enter Valid Year" ForeColor="Red" Operator="DataTypeCheck" Type="Integer"
                                        ValidationGroup="SaveQualInfo">*</asp:CompareValidator>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 145px">
                                </td>
                                <td style="width: 108px">
                                    Exam Name
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 140px">
                                    <asp:DropDownList ID="cboQualExam" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 92px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="cboQualExam"
                                        ErrorMessage="Select Exam Name" ForeColor="Red" InitialValue="0" ValidationGroup="SaveQualInfo">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 98px">
                                    Result
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 148px">
                                    <asp:TextBox ID="txtQualResult" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 59px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 145px">
                                </td>
                                <td style="width: 108px">
                                    Group/Subject
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 140px">
                                    <asp:TextBox ID="txtQualGrpSub" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td style="width: 92px">
                                </td>
                                <td style="width: 98px">
                                    Exam Status
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 148px">
                                    <asp:DropDownList ID="cboQualExamStat" runat="server" DataSourceID="dsStat" DataTextField="StatusName"
                                        DataValueField="StatusRefNo">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 59px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 145px">
                                </td>
                                <td style="width: 108px">
                                    Institute Name
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 140px">
                                    <asp:TextBox ID="txtQualInst" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td style="width: 92px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtQualInst"
                                        ErrorMessage="Enter Institute Name" ForeColor="Red" ValidationGroup="SaveQualInfo">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 98px">
                                    Remarks
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 148px">
                                    <asp:TextBox ID="txtQualRem" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 59px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 145px">
                                </td>
                                <td style="width: 108px">
                                </td>
                                <td style="width: 6px">
                                </td>
                                <td style="width: 140px">
                                </td>
                                <td style="width: 92px">
                                </td>
                                <td style="width: 98px">
                                </td>
                                <td style="width: 4px">
                                </td>
                                <td style="width: 148px">
                                </td>
                                <td style="width: 59px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="10">
                                    <asp:HiddenField ID="hfEditQualFlag" runat="server" Value="N" />
                                    <asp:SqlDataSource ID="dsStat" runat="server" ConnectionString="<%$ ConnectionStrings:DRNConStr %>"
                                        SelectCommand="SELECT * FROM [tblStatusInfo] ORDER BY [StatusName]"></asp:SqlDataSource>
                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" Font-Size="8pt" ForeColor="Red"
                                        ValidationGroup="SaveQualInfo" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 145px">
                                </td>
                                <td style="width: 108px">
                                </td>
                                <td style="width: 6px">
                                </td>
                                <td style="width: 140px">
                                </td>
                                <td style="width: 92px">
                                </td>
                                <td style="width: 98px">
                                </td>
                                <td style="width: 4px">
                                </td>
                                <td style="width: 148px">
                                </td>
                                <td style="width: 59px">
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 145px">
                                </td>
                                <td style="width: 108px">
                                </td>
                                <td style="width: 6px">
                                </td>
                                <td align="center" colspan="3">
                                    <asp:Button ID="btnClearQual" runat="server" OnClick="btnClearQual_Click" Text="Clear"
                                        Width="148px" />
                                    &nbsp;<asp:Button ID="btnAddQual" runat="server" Text="Add Qualification" ValidationGroup="SaveQualInfo"
                                        OnClick="btnAddQual_Click" />
                                </td>
                                <td style="width: 4px">
                                </td>
                                <td style="width: 148px">
                                </td>
                                <td style="width: 59px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 145px">
                                </td>
                                <td style="width: 108px">
                                </td>
                                <td style="width: 6px">
                                </td>
                                <td align="center" colspan="3">
                                </td>
                                <td style="width: 4px">
                                </td>
                                <td style="width: 148px">
                                </td>
                                <td style="width: 59px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="10" align="center">
                                    <asp:GridView ID="gvEmpQual" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="10pt"
                                        OnRowDeleting="gvEmpQual_RowDeleting" OnRowDataBound="gvEmpQual_RowDataBound"
                                        OnSelectedIndexChanged="gvEmpQual_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Qual Type">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfQualTypeRef" runat="server" Value='<%# Bind("QUAL_TYPE_REF") %>' />
                                                    <asp:Label ID="lblQualType" runat="server" Text='<%# Bind("QUAL_TYPE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Exam Name">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfExamRef" runat="server" Value='<%# Bind("EXAM_REF") %>' />
                                                    <asp:Label ID="lblExamName" runat="server" Text='<%# Bind("EXAM_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Group/Subject">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrpSub" runat="server" Text='<%# Bind("GRP_SUB") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Institute Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInstName" runat="server" Text='<%#Bind("INST_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Passing Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPassYr" runat="server" Text='<%# Bind("PASS_YEAR") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Result">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResult" runat="server" Text='<%# Bind("RESULT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Exam Status">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfStatRef" runat="server" Value='<%# Bind("STAT_REF") %>' />
                                                    <asp:Label ID="lblExamStat" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("EXT_DATA1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgBtnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                        ImageUrl="~/Image/Delete.png" OnClientClick="return confirm('Do you want to delete?')"
                                                        ToolTip="Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table border="1" cellspacing="0" rules="all" style="border-collapse: collapse;">
                                                <tr style="background-color: #006699; font-weight: bold; color: #F7F7F7; font-size: smaller;
                                                    white-space: nowrap">
                                                    <th align="center" scope="col">
                                                        Qual Type
                                                    </th>
                                                    <th align="center" scope="col" width="150">
                                                        Exam Name
                                                    </th>
                                                    <th align="center" scope="col" width="70">
                                                        Group/Subject
                                                    </th>
                                                    <th align="center" scope="col" width="100">
                                                        Institute Name
                                                    </th>
                                                    <th align="center" scope="col" width="60">
                                                        Passing Year
                                                    </th>
                                                    <th align="center" scope="col" width="100">
                                                        Result
                                                    </th>
                                                    <th align="center" scope="col" width="100">
                                                        Exam Status
                                                    </th>
                                                    <th align="center" scope="col" width="100">
                                                        Remarks
                                                    </th>
                                                    <th align="center" scope="col" width="20">
                                                        &nbsp;
                                                    </th>
                                                </tr>
                                                <tr align="center" class="gridFooterRow" style="background-color: #FFFFFF; height: 30px;">
                                                    <td colspan="9">
                                                        Qualification Data Not Found.............
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <FooterStyle BackColor="#86AEAE" HorizontalAlign="Right" Wrap="False" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Size="Smaller" ForeColor="White"
                                            Wrap="True" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#CC99FF" Font-Bold="True" ForeColor="Black" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 145px">
                                </td>
                                <td style="width: 108px">
                                </td>
                                <td style="width: 6px">
                                </td>
                                <td align="center" colspan="3">
                                </td>
                                <td style="width: 4px">
                                </td>
                                <td style="width: 148px">
                                </td>
                                <td style="width: 59px">
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <div align="center" style="background-color: #99FF33">
                            <asp:Button ID="btnSaveQual" runat="server" Text="Update" Width="150px" OnClick="btnSaveQual_Click"
                                Visible="False" />
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPnlExp" runat="server">
                <HeaderTemplate>
                    Experience
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:Panel ID="PnlExp" runat="server">
                        <table style="width: 100%; background-color: #CCFFFF">
                            <tr>
                                <td style="width: 128px">
                                </td>
                                <td style="width: 103px">
                                    Organization
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 220px">
                                    <asp:TextBox ID="txtExpOrgName" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td style="width: 61px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtExpOrgName"
                                        ErrorMessage="Enter Organization Name" ForeColor="Red" ValidationGroup="SaveExpInfo">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 134px">
                                    From
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 167px">
                                    <asp:TextBox ID="txtExpFromDate" runat="server"></asp:TextBox>
                                    <asp:Image ID="imgExpFrom" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <asp:CalendarExtender ID="CalendarExtender7" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtExpFromDate" PopupButtonID="imgExpFrom">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txtExpFromDate"
                                        ErrorMessage="Enter From Date" ForeColor="Red" ValidationGroup="SaveExpInfo">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtExpFromDate"
                                        ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck" Type="Date"
                                        ValidationGroup="SaveExpInfo">*</asp:CompareValidator>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 128px">
                                </td>
                                <td style="width: 103px">
                                    Department
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 220px">
                                    <asp:TextBox ID="txtExpDept" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td style="width: 61px">
                                </td>
                                <td style="width: 134px">
                                    To
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 167px">
                                    <asp:TextBox ID="txtExpToDate" runat="server"></asp:TextBox>
                                    <asp:Image ID="imgExpTo" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <asp:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtExpToDate" PopupButtonID="imgExpTo">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtExpToDate"
                                        ErrorMessage="Enter To Date" ForeColor="Red" ValidationGroup="SaveExpInfo">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtExpToDate"
                                        ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck" Type="Date"
                                        ValidationGroup="SaveExpInfo">*</asp:CompareValidator>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 128px">
                                </td>
                                <td style="width: 103px">
                                    Designation
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 220px">
                                    <asp:TextBox ID="txtExpDesig" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td style="width: 61px">
                                    &nbsp;
                                </td>
                                <td style="width: 134px">
                                    Reference Name
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 167px">
                                    <asp:TextBox ID="txtExpRefName" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 128px">
                                </td>
                                <td style="width: 103px">
                                    Address
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 220px">
                                    <asp:TextBox ID="txtExpOrgAdr" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td style="width: 61px">
                                </td>
                                <td style="width: 134px">
                                    Reference Contact
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 167px">
                                    <asp:TextBox ID="txtExpRefContact" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 128px">
                                </td>
                                <td style="width: 103px">
                                    Remarks
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 220px">
                                    <asp:TextBox ID="txtExpRem" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td style="width: 61px">
                                    &nbsp;
                                </td>
                                <td style="width: 134px">
                                    Reference E-Mail
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 167px">
                                    <asp:TextBox ID="txtExpRefMail" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 128px">
                                </td>
                                <td style="width: 103px">
                                </td>
                                <td style="width: 6px">
                                </td>
                                <td style="width: 220px">
                                </td>
                                <td style="width: 61px">
                                </td>
                                <td style="width: 134px">
                                </td>
                                <td style="width: 4px">
                                </td>
                                <td style="width: 167px">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 128px">
                                </td>
                                <td align="center" colspan="7">
                                    <asp:Button ID="btnClearExp" runat="server" OnClick="btnClearExp_Click" Text="Clear"
                                        Width="148px" />
                                    &nbsp;<asp:Button ID="btnAddExp" runat="server" Text="Add Experience" ValidationGroup="SaveExpInfo"
                                        OnClick="btnAddExp_Click" />
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 128px">
                                </td>
                                <td align="center" colspan="7">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="10">
                                    <asp:HiddenField ID="hfEditExpFlag" runat="server" Value="N" />
                                    <asp:ValidationSummary ID="ValidationSummary4" runat="server" Font-Size="8pt" ForeColor="Red"
                                        ValidationGroup="SaveExpInfo" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 128px">
                                </td>
                                <td style="width: 103px">
                                </td>
                                <td style="width: 6px">
                                </td>
                                <td style="width: 220px">
                                    &nbsp;
                                </td>
                                <td style="width: 61px">
                                </td>
                                <td style="width: 134px">
                                </td>
                                <td style="width: 4px">
                                </td>
                                <td style="width: 167px">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="10">
                                    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>--%>
                                    <asp:GridView ID="gvEmpExp" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="10pt"
                                        OnRowDataBound="gvEmpExp_RowDataBound" OnRowDeleting="gvEmpExp_RowDeleting" OnSelectedIndexChanged="gvEmpExp_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Organization">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpOrg" runat="server" Text='<%# Bind("EXP_ORG") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpDept" runat="server" Text='<%# Bind("EXP_DEPT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpDesig" runat="server" Text='<%# Bind("EXP_DESIG") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpAdr" runat="server" Text='<%#Bind("EXP_ADR") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="From">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpFrom" runat="server" Text='<%# Bind("EXP_FROM") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpTo" runat="server" Text='<%# Bind("EXP_TO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpRem" runat="server" Text='<%# Bind("EXP_REM") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ref. Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpRefName" runat="server" Text='<%# Bind("EXP_REF_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ref. Contact">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpRefContact" runat="server" Text='<%# Bind("EXP_REF_CONT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ref. E-Mail">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpRefEmail" runat="server" Text='<%# Bind("EXP_REF_EMAIL") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgBtnDeleteExp" runat="server" CausesValidation="False" CommandName="Delete"
                                                        ImageUrl="~/Image/Delete.png" OnClientClick="return confirm('Do you want to delete?')"
                                                        ToolTip="Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table border="1" cellspacing="0" rules="all" style="border-collapse: collapse;">
                                                <tr style="background-color: #006699; font-weight: bold; color: #F7F7F7; font-size: smaller;
                                                    white-space: nowrap">
                                                    <th align="center" scope="col">
                                                        Organization
                                                    </th>
                                                    <th align="center" scope="col" width="150">
                                                        Department
                                                    </th>
                                                    <th align="center" scope="col" width="70">
                                                        Designation
                                                    </th>
                                                    <th align="center" scope="col" width="100">
                                                        Address
                                                    </th>
                                                    <th align="center" scope="col" width="60">
                                                        From
                                                    </th>
                                                    <th align="center" scope="col" width="100">
                                                        To
                                                    </th>
                                                    <th align="center" scope="col" width="100">
                                                        Remarks
                                                    </th>
                                                    <th align="center" scope="col" width="100">
                                                        Ref. Name
                                                    </th>
                                                    <th align="center" scope="col" width="100">
                                                        Ref. Contact
                                                    </th>
                                                    <th align="center" scope="col" width="100">
                                                        Ref. E-Mail
                                                    </th>
                                                    <th align="center" scope="col" width="20">
                                                        &nbsp;
                                                    </th>
                                                </tr>
                                                <tr align="center" class="gridFooterRow" style="background-color: #FFFFFF; height: 30px;">
                                                    <td colspan="11">
                                                        Experience Data Not Found.............
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <FooterStyle BackColor="#86AEAE" HorizontalAlign="Right" Wrap="False" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Size="Smaller" ForeColor="White"
                                            Wrap="True" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#CC99FF" Font-Bold="True" ForeColor="Black" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                    <%--</ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="gvEmpExp" EventName="DataBound" />
                                            </Triggers>
                                        </asp:UpdatePanel>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 128px">
                                </td>
                                <td style="width: 103px">
                                </td>
                                <td style="width: 6px">
                                </td>
                                <td style="width: 220px">
                                </td>
                                <td style="width: 61px">
                                    &nbsp;
                                </td>
                                <td style="width: 134px">
                                </td>
                                <td style="width: 4px">
                                </td>
                                <td style="width: 167px">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <div align="center" style="background-color: #99FF33">
                            <asp:Button ID="btnSaveExp" runat="server" Text="Update" Width="150px" OnClick="btnSaveExp_Click"
                                Visible="false" />
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>
            <%--<asp:TabPanel ID="TabPnlBnk" runat="server" HeaderText="Bank" Visible="false">
                <HeaderTemplate>
                    Bank Account
                </HeaderTemplate>
                <ContentTemplate>
                    <table style="width: 100%; background-color: #CCFFFF">
                        <tr>
                            <td style="width: 92px">
                            </td>
                            <td style="width: 136px">
                                Bank Name
                            </td>
                            <td style="width: 6px">
                                :
                            </td>
                            <td style="width: 330px">
                                <asp:DropDownList ID="cboBankName" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 134px">
                                Account No
                            </td>
                            <td style="width: 4px">
                                :
                            </td>
                            <td style="width: 162px">
                                <asp:TextBox ID="txtBankAccNo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 92px">
                            </td>
                            <td style="width: 136px">
                                Status
                            </td>
                            <td style="width: 6px">
                                :
                            </td>
                            <td style="width: 330px">
                                <asp:DropDownList ID="cboBankAccStat" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 134px">
                                Account Type
                            </td>
                            <td style="width: 4px">
                                :
                            </td>
                            <td style="width: 162px">
                                <asp:TextBox ID="txtBankAccType" runat="server"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 92px">
                            </td>
                            <td style="width: 136px">
                            </td>
                            <td style="width: 6px">
                            </td>
                            <td style="width: 330px">
                            </td>
                            <td style="width: 134px">
                            </td>
                            <td style="width: 4px">
                            </td>
                            <td style="width: 162px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 92px">
                            </td>
                            <td align="center" colspan="6">
                                <asp:Button ID="btnAddBnk" runat="server" Text="Add Bank Account" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 92px">
                            </td>
                            <td style="width: 136px">
                            </td>
                            <td style="width: 6px">
                            </td>
                            <td style="width: 330px">
                            </td>
                            <td style="width: 134px">
                            </td>
                            <td style="width: 4px">
                            </td>
                            <td style="width: 162px">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div align="center" style="background-color: #99FF33">
                        <asp:Button ID="btnSaveBankAcc" runat="server" Text="Save" Width="150px" />
                    </div>
                </ContentTemplate>
            </asp:TabPanel>--%>
            <asp:TabPanel ID="TabPnlOffice" runat="server">
                <HeaderTemplate>
                    Official
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:Panel ID="PnlOffice" runat="server" DefaultButton="btnSaveOffInfo">
                        <table style="width: 100%; background-color: #CCFFFF">
                            <tr>
                                <td style="width: 162px">
                                </td>
                                <td style="width: 136px">
                                    Office
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 164px">
                                    <asp:DropDownList ID="cboLoc" runat="server" OnSelectedIndexChanged="cboLoc_SelectedIndexChanged"
                                        AutoPostBack="True" Width="200px">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="imgBtnLocRef" runat="server" ImageUrl="~/Image/Refresh.png"
                                        Visible="False" />
                                </td>
                                <td style="width: 69px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="cboLoc"
                                        ErrorMessage="Select Office Name" ForeColor="Red" InitialValue="0" ValidationGroup="SaveOffcInfo">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 79px">
                                    Shift
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 159px">
                                    <asp:DropDownList ID="cboShift" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="cboShift"
                                        ErrorMessage="Select Shift" ForeColor="Red" InitialValue="0" ValidationGroup="SaveOffcInfo">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 162px">
                                </td>
                                <td style="width: 136px">
                                    Department
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 164px">
                                    <asp:DropDownList ID="cboDept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboDept_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 69px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="cboDept"
                                        ErrorMessage="Select Department" ForeColor="Red" InitialValue="0" ValidationGroup="SaveOffcInfo">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 79px">
                                    Grade
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 159px">
                                    <asp:DropDownList ID="cboGrade" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="cboGrade"
                                        ErrorMessage="Select Grade" ForeColor="Red" InitialValue="0" ValidationGroup="SaveOffcInfo">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 162px">
                                </td>
                                <td style="width: 136px">
                                    Section
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 164px">
                                    <asp:DropDownList ID="cboSec" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 69px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="cboSec"
                                        ErrorMessage="Select Section" ForeColor="Red" InitialValue="0" ValidationGroup="SaveOffcInfo">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 79px">
                                    Salary
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 159px">
                                    <asp:TextBox ID="txtSalary" runat="server"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="txtSalary_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtSalary" ValidChars=".">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtSalary"
                                        ErrorMessage="Enter Salary" ForeColor="Red" ValidationGroup="SaveOffcInfo">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtSalary"
                                        ErrorMessage="Enter Valid Salary" ForeColor="Red" Operator="DataTypeCheck" Type="Currency"
                                        ValidationGroup="SaveOffcInfo">*</asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 162px">
                                </td>
                                <td style="width: 136px">
                                    Designation
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 164px">
                                    <asp:DropDownList ID="cboDesig" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 69px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="cboDesig"
                                        ErrorMessage="Select Designation" ForeColor="Red" InitialValue="0" ValidationGroup="SaveOffcInfo">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 79px">
                                    Bank
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 159px">
                                    <asp:TextBox ID="txtSalBankAcc" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 162px">
                                </td>
                                <td style="width: 136px">
                                    Date of Joining
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 164px">
                                    <asp:TextBox ID="txtDOJ" runat="server"></asp:TextBox>
                                    <asp:Image ID="imgDOJ" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtDOJ" PopupButtonID="imgDOJ">
                                    </asp:CalendarExtender>
                                </td>
                                <td style="width: 69px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtDOJ"
                                        ErrorMessage="Enter Date of Joining" ForeColor="Red" ValidationGroup="SaveOffcInfo">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtDOJ"
                                        ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck" Type="Date"
                                        ValidationGroup="SaveOffcInfo">*</asp:CompareValidator>
                                </td>
                                <td style="width: 79px">
                                    E-Mail
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 159px">
                                    <asp:TextBox ID="txtOffEmail" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 162px">
                                </td>
                                <td style="width: 136px">
                                    Supervisor
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 164px">
                                    <asp:DropDownList ID="cboSupr" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 69px">
                                    &nbsp;
                                </td>
                                <td style="width: 79px">
                                    Phone
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 159px">
                                    <asp:TextBox ID="txtOffPhone" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 162px">
                                </td>
                                <td style="width: 136px">
                                    Job Status
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 164px">
                                    <asp:DropDownList ID="cboJobStat" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 69px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="cboJobStat"
                                        ErrorMessage="Select Job Status" ForeColor="Red" InitialValue="0" ValidationGroup="SaveOffcInfo">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 79px">
                                    PABX
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 159px">
                                    <asp:TextBox ID="txtOffPabx" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 162px">
                                </td>
                                <td style="width: 136px">
                                    Confirm Due Date
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 164px">
                                    <asp:TextBox ID="txtConfDueDate" runat="server"></asp:TextBox>
                                    <asp:Image ID="imgConfDueDate" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtConfDueDate" PopupButtonID="imgConfDueDate">
                                    </asp:CalendarExtender>
                                </td>
                                <td style="width: 69px">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtConfDueDate"
                                        ErrorMessage="Enter Confirm Due Date" ForeColor="Red" ValidationGroup="SaveOffcInfo">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtConfDueDate"
                                        ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck" Type="Date"
                                        ValidationGroup="SaveOffcInfo">*</asp:CompareValidator>
                                </td>
                                <td style="width: 79px">
                                    IP Phone
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 159px">
                                    <asp:TextBox ID="txtOffIpPhone" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 162px">
                                </td>
                                <td style="width: 136px">
                                    Confirm Date
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 164px">
                                    <asp:TextBox ID="txtConfDate" runat="server"></asp:TextBox>
                                    <asp:Image ID="imgConfDate" runat="server" ImageUrl="~/Image/calendar.png" />
                                    <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="txtConfDate" PopupButtonID="imgConfDate">
                                    </asp:CalendarExtender>
                                </td>
                                <td style="width: 69px">
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Enter Confirm Date"
                                        ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="SaveOffcInfo">*</asp:CustomValidator>
                                    <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="txtConfDate"
                                        ErrorMessage="Enter Valid Date" ForeColor="Red" Operator="DataTypeCheck" Type="Date"
                                        ValidationGroup="SaveOffcInfo">*</asp:CompareValidator>
                                </td>
                                <td style="width: 79px">
                                    Remarks
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td style="width: 159px">
                                    <asp:TextBox ID="txtEmpRemarks" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 162px">
                                </td>
                                <td style="width: 136px">
                                    Workstation
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 164px">
                                    <asp:DropDownList ID="cboWorkStation" runat="server">
                                        <asp:ListItem Value="ECILF">ECIL Factory</asp:ListItem>
                                        <asp:ListItem Value="WALSOW">Walsow Tower</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 69px">
                                    &nbsp;
                                </td>
                                <td style="width: 79px">
                                </td>
                                <td style="width: 4px">
                                </td>
                                <td style="width: 159px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 162px">
                                </td>
                                <td style="width: 136px">
                                    Card ID No
                                </td>
                                <td style="width: 6px">
                                    :
                                </td>
                                <td style="width: 164px">
                                    <asp:TextBox ID="txtIdCardNo" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 69px">
                                </td>
                                <td style="width: 79px">
                                </td>
                                <td style="width: 4px">
                                </td>
                                <td style="width: 159px">
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <div align="center" style="background-color: #99FF33">
                            <asp:Button ID="btnSaveOffInfo" runat="server" Text="Update" Width="150px" OnClick="btnSaveOffInfo_Click"
                                ValidationGroup="SaveOffcInfo" Visible="False" />
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabPnlPicSig" runat="server">
                <HeaderTemplate>
                    Attachment
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:Panel ID="PnlPicSig" runat="server">
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table style="width: 100%; background-color: #CCFFFF">
                                        <tr>
                                            <td style="width: 183px">
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 77px">
                                                Picture
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 2px">
                                                :
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 200px">
                                                <asp:FileUpload ID="picUpload" runat="server" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="picUpload"
                                                    ErrorMessage="Only .jpeg, .gif, .png, .jpg and .bitmap image formats are allowed."
                                                    Display="Dynamic" ForeColor="Red" ValidationExpression="^.+(.jpeg|.JPEG|.gif|.GIF|.png|.PNG|.jpg|.JPG|.bitmap|.BITMAP)$"
                                                    SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                <asp:CustomValidator ID="picSizeLimit" runat="server" ToolTip="FileSize should not exceed 1 MB"
                                                    ErrorMessage="FileSize Exceeds the Limits of 1 MB" ControlToValidate="picUpload"
                                                    Display="Dynamic" ClientValidationFunction="CheckPicFileSize" ForeColor="Red"></asp:CustomValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="You have not uploaded picture file"
                                                    ForeColor="Red" ValidationGroup="picUpdate" ControlToValidate="picUpload"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 118px" align="center">
                                                <asp:HyperLink ID="hlEmpPic" Target="_blank" runat="server">
                                                    <asp:Image ID="imgEmpPic" runat="server" Height="100px" Width="115px" ImageUrl="~/Image/NoImage.gif"
                                                        BorderColor="#CCCCCC" BorderStyle="Solid" />
                                                </asp:HyperLink>
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 64px;">
                                                <asp:Button ID="btnUpdatePic" runat="server" Text="Update Pic" OnClick="btnUpdatePic_Click"
                                                    ValidationGroup="picUpdate" Visible="false" />
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 40px;">
                                                <asp:Button ID="btnDeletePic" runat="server" Text="Delete Pic" OnClick="btnDeletePic_Click"
                                                    Visible="false" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 183px">
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 77px">
                                                Driving License
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 2px">
                                                :
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 200px">
                                                <asp:FileUpload ID="sigUpload" runat="server" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="sigUpload"
                                                    ErrorMessage="Only .jpeg, .gif, .png, .jpg and .bitmap image formats are allowed."
                                                    Display="Dynamic" ForeColor="Red" ValidationExpression="^.+(.jpeg|.JPEG|.gif|.GIF|.png|.PNG|.jpg|.JPG|.bitmap|.BITMAP)$"
                                                    SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                <asp:CustomValidator ID="CustomValidator2" runat="server" ToolTip="FileSize should not exceed 1 MB"
                                                    ErrorMessage="FileSize Exceeds the Limits of 1 MB" ControlToValidate="sigUpload"
                                                    Display="Dynamic" ClientValidationFunction="CheckSigFileSize" ForeColor="Red"></asp:CustomValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="You have not uploaded signature file"
                                                    ForeColor="Red" ValidationGroup="sigUpdate" ControlToValidate="sigUpload"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 118px" align="center">
                                                <asp:HyperLink ID="hlEmpSig" Target="_blank" runat="server">
                                                    <asp:Image ID="imgEmpSig" runat="server" Height="100px" Width="115px" ImageUrl="~/Image/NoImage.gif"
                                                        BorderColor="#CCCCCC" BorderStyle="Solid" />
                                                </asp:HyperLink>
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 64px;">
                                                <asp:Button ID="btnUpdateSig" runat="server" Text="Update D/L" OnClick="btnUpdateSig_Click"
                                                    ValidationGroup="sigUpdate" Visible="false" />
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 40px;">
                                                <asp:Button ID="btnDeleteSig" runat="server" Text="Delete D/L" OnClick="btnDeleteSig_Click"
                                                    Visible="false" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 183px">
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 77px">
                                                National ID
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 2px">
                                                :
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 200px">
                                                <asp:FileUpload ID="nidUpload" runat="server" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="nidUpload"
                                                    ErrorMessage="Only .jpeg, .gif, .png, .jpg and .bitmap image formats are allowed."
                                                    Display="Dynamic" ForeColor="Red" ValidationExpression="^.+(.jpeg|.JPEG|.gif|.GIF|.png|.PNG|.jpg|.JPG|.bitmap|.BITMAP)$"
                                                    SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                <asp:CustomValidator ID="CustomValidator3" runat="server" ToolTip="FileSize should not exceed 1 MB"
                                                    ErrorMessage="FileSize Exceeds the Limits of 1 MB" ControlToValidate="nidUpload"
                                                    Display="Dynamic" ClientValidationFunction="CheckNidFileSize" ForeColor="Red"></asp:CustomValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ErrorMessage="You have not uploaded NID file"
                                                    ForeColor="Red" ValidationGroup="nidUpdate" ControlToValidate="nidUpload"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 118px" align="center">
                                                <asp:HyperLink ID="hlEmpNid" Target="_blank" runat="server">
                                                    <asp:Image ID="imgEmpNId" runat="server" Height="100px" Width="115px" ImageUrl="~/Image/NoImage.gif"
                                                        BorderColor="#CCCCCC" BorderStyle="Solid" />
                                                </asp:HyperLink>
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 64px;">
                                                <asp:Button ID="btnUpdateNid" runat="server" Text="Update NID" OnClick="btnUpdateNid_Click"
                                                    ValidationGroup="nidUpdate" Visible="false" />
                                            </td>
                                            <td style="border: 1px solid #C0C0C0; width: 40px;">
                                                <asp:Button ID="btnDeleteNid" runat="server" Text="Delete NID" OnClick="btnDeleteNid_Click"
                                                    Visible="false" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                    <div align="center" style="background-color: #99FF33">
                                        <asp:Button ID="btnSaveEmp" runat="server" Text="Save Employee" Width="150px" ValidationGroup="SaveEmployee"
                                            OnClick="btnSaveEmp_Click" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUpdatePic" />
                                    <asp:PostBackTrigger ControlID="btnUpdateSig" />
                                    <asp:PostBackTrigger ControlID="btnUpdateNid" />
                                    <asp:PostBackTrigger ControlID="btnSaveEmp" />
                                    <asp:PostBackTrigger ControlID="btnDeletePic" />
                                    <asp:PostBackTrigger ControlID="btnDeleteSig" />
                                    <asp:PostBackTrigger ControlID="btnDeleteNid" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
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
    <asp:ModalPopupExtender ID="ModalPopupExtenderMsg" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnMsgOk" OkControlID="btnMsgOk" PopupControlID="pnlMsg" TargetControlID="hfHidden"
        DropShadow="true">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlSettle" runat="server" Style="border-right: black 2px solid; padding-right: 20px;
        display: none; border-top: black 2px solid; padding-left: 20px; padding-bottom: 20px;
        border-left: black 2px solid; padding-top: 20px; border-bottom: black 2px solid;
        background-color: #E1FBF1" ForeColor="Blue" Width="300px" Font-Size="Small">
        <table id="tblSettle" runat="server" style="width: 100%;">
            <tr>
                <td class="style3" align="right">
                    Type
                </td>
                <td class="style2">
                    <asp:DropDownList ID="ddlSettleType" runat="server">
                        <asp:ListItem Value="1">Resign</asp:ListItem>
                        <asp:ListItem Value="1">Terminate</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="style5">
                    Date
                </td>
                <td>
                    <asp:TextBox ID="txtSettleDate" runat="server"></asp:TextBox>
                    <asp:Image ID="imgSettleDate" runat="server" ImageUrl="~/Image/calendar.png" />
                    <asp:CalendarExtender ID="CalendarExtender9" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        TargetControlID="txtSettleDate" PopupButtonID="imgSettleDate">
                    </asp:CalendarExtender>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <div align="center">
            <asp:Button ID="btnOk" runat="server" Text="OK" Width="80px" OnClick="btnOk_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" />
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hfHidenBtn" runat="server" />
    <asp:ModalPopupExtender ID="ModalPopupExtenderSettle" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnCancel" OkControlID="hfHidenBtn" PopupControlID="pnlSettle"
        TargetControlID="hfHidenBtn" DropShadow="true">
    </asp:ModalPopupExtender>
</asp:Content>
