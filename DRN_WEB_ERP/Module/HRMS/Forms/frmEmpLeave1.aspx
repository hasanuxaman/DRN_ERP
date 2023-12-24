<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="frmEmpLeave1.aspx.cs" Inherits="DRN_WEB_ERP.Module.HRMS.Forms.frmEmpLeave1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //Autocomplete Start
        function acePopulated(sender, e) {
            var behavior = $find('AutoCompleteSrchLvEmp');
            var target = behavior.get_completionList();
            //        target.style.width = 'auto';
            if (behavior._currentPrefix != null) {
                var prefix = behavior._currentPrefix.toLowerCase();
                var i;
                for (i = 0; i < target.childNodes.length; i++) {
                    var sValue = target.childNodes[i].innerHTML.toLowerCase();
                    if (sValue.indexOf(prefix) != -1) {
                        var fstr = target.childNodes[i].innerHTML.substring(0, sValue.indexOf(prefix));
                        var pstr = target.childNodes[i].innerHTML.substring(fstr.length, fstr.length + prefix.length);
                        var estr = target.childNodes[i].innerHTML.substring(fstr.length + prefix.length, target.childNodes[i].innerHTML.length);
                        target.childNodes[i].innerHTML = "<div class='autocomplete-item'>" + fstr + '<B><font color="#990000">' + pstr + '</font></B>' + estr + "</div>";
                    }
                }
            }
        }

        function aceSelected(sender, e) {
            var value = e.get_value();
            if (!value) {
                if (e._item.parentElement && e._item.parentElement.tagName == "LI")
                    value = e._item.parentElement.attributes["_value"].value;
                else if (e._item.parentElement && e._item.parentElement.parentElement.tagName == "LI")
                    value = e._item.parentElement.parentElement.attributes["_value"].value;
                else if (e._item.parentNode && e._item.parentNode.tagName == "LI")
                    value = e._item.parentNode._value;
                else if (e._item.parentNode && e._item.parentNode.parentNode.tagName == "LI")
                    value = e._item.parentNode.parentNode._value;
                else value = "";
            }
            var searchText = '';
            var searchText = $get('<%=txtEmpName.ClientID %>').value;
            searchText = searchText.replace('null', '');
            sender.get_element().value = searchText + value;
        }
        //Autocomplete End    

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
    </script>
    <div align="center" style="background-color: #00FF99">
        Allocate Employee Leave
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="wrapper" style="margin: 0px; padding: 0px; border: 0px; font-family: Arial, Helvetica, sans-serif;
                font-size: 13px; font-style: normal; font-variant: normal; font-weight: normal;
                line-height: 13px; vertical-align: baseline; min-width: 940px; overflow: hidden;
                color: rgb(93, 93, 93); letter-spacing: normal; orphans: auto; text-align: start;
                text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px;
                -webkit-text-stroke-width: 0px;">
                <div id="content2" style="margin: 0px; padding: 0px 0px 0px; border: 0px; font-family: inherit;
                    font-size: inherit; font-style: inherit; font-variant: inherit; font-weight: inherit;
                    line-height: inherit; vertical-align: baseline;">
                    <div id="assign-leave" class="box" style="margin: 20px; padding: 0px; border: 0px;
                        font-family: inherit; font-size: inherit; font-style: inherit; font-variant: inherit;
                        font-weight: inherit; line-height: inherit; vertical-align: baseline; position: relative;">
                        <div class="head" style="margin: 0px; padding: 0px; border: 0px; font-family: inherit;
                            font-size: inherit; font-style: inherit; font-variant: inherit; font-weight: inherit;
                            line-height: inherit; vertical-align: baseline; position: relative;">
                            <h1 style="margin: 0px; padding: 9px 15px; border: 1px solid rgb(222, 222, 222);
                                font-family: inherit; font-size: 18px; font-style: inherit; font-variant: inherit;
                                font-weight: inherit; line-height: 20px; vertical-align: baseline; color: rgb(93, 93, 93);
                                border-top-left-radius: 3px; border-top-right-radius: 3px; background: url(http://enterprise.demo.orangehrmlive.com/symfony/web/webres_54059700537954.43518716/themes/enterprise/images/h1-bg.png) 0% 100% repeat-x rgb(243, 243, 243);">
                                Assign Leave</h1>
                        </div>
                        <div class="inner" style="margin: 0px 0px 19px; padding: 15px; border-right-width: 1px;
                            border-bottom-width: 1px; border-left-width: 1px; border-style: none solid solid;
                            border-right-color: rgb(222, 222, 222); border-bottom-color: rgb(222, 222, 222);
                            border-left-color: rgb(222, 222, 222); font-family: inherit; font-size: inherit;
                            font-style: inherit; font-variant: inherit; font-weight: inherit; line-height: inherit;
                            vertical-align: baseline; border-bottom-right-radius: 3px; border-bottom-left-radius: 3px;
                            overflow: hidden; background: rgb(247, 246, 246);">
                            <fieldset style="margin: 0px; padding: 0px; border: 0px; font-family: inherit; font-size: inherit;
                                font-style: inherit; font-variant: inherit; font-weight: inherit; line-height: inherit;
                                vertical-align: baseline;">
                                <ol style="border-bottom: 1px solid rgb(222, 222, 222); margin: 0px 0px 10px; padding: 0px 0px 5px;
                                    font-family: inherit; font-size: inherit; font-style: inherit; font-variant: inherit;
                                    font-weight: inherit; line-height: inherit; vertical-align: baseline; list-style: none;
                                    overflow: hidden; width: 608px; border-left-width: 0px; border-right-width: 0px;
                                    border-top-width: 0px;">
                                    <li style="margin: 3px 0px 12px; padding: 0px; border: 0px; font-family: inherit;
                                        font-size: inherit; font-style: inherit; font-variant: inherit; font-weight: inherit;
                                        line-height: inherit; vertical-align: baseline; float: left; position: relative;
                                        width: 1149.296875px;">
                                        <label for="assignleave_txtEmployee" style="margin: 0px; padding: 0px; border: 0px;
                                            font-family: inherit; font-size: inherit; font-style: inherit; font-variant: inherit;
                                            font-weight: inherit; line-height: inherit; vertical-align: baseline; display: block;
                                            color: rgb(93, 93, 93); width: 150px; float: left;">
                                            Employee Name<span class="Apple-converted-space">&nbsp;</span><em style="margin: 0px;
                                                padding: 0px; border: 0px; font-family: inherit; font-size: inherit; font-style: inherit;
                                                font-variant: inherit; font-weight: inherit; line-height: inherit; vertical-align: baseline;
                                                color: rgb(170, 73, 53);">*</em></label>
                                        <asp:TextBox ID="txtEmpName" runat="server" CssClass="etc search"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtenderLvEmp" runat="server" BehaviorID="AutoCompleteSrchLvEmp"
                                            CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                            DelimiterCharacters="," MinimumPrefixLength="1" OnClientPopulated="acePopulated"
                                            OnClientItemSelected="aceSelected" ServiceMethod="GetSrchEmp" ServicePath="~/Module/HRMS/Forms/wsAutoComHrms.asmx"
                                            ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtEmpName">
                                        </cc1:AutoCompleteExtender>
                                    </li>
                                    <li style="margin: 3px 0px 12px; padding: 0px; border: 0px; font-family: inherit;
                                        font-size: inherit; font-style: inherit; font-variant: inherit; font-weight: inherit;
                                        line-height: inherit; vertical-align: baseline; float: left; position: relative;
                                        width: 1149.296875px;">
                                        <label for="assignleave_txtLeaveType" style="margin: 0px; padding: 0px; border: 0px;
                                            font-family: inherit; font-size: inherit; font-style: inherit; font-variant: inherit;
                                            font-weight: inherit; line-height: inherit; vertical-align: baseline; display: block;
                                            color: rgb(93, 93, 93); width: 150px; float: left;">
                                            Leave Type<span class="Apple-converted-space">&nbsp;</span><em style="margin: 0px;
                                                padding: 0px; border: 0px; font-family: inherit; font-size: inherit; font-style: inherit;
                                                font-variant: inherit; font-weight: inherit; line-height: inherit; vertical-align: baseline;
                                                color: rgb(170, 73, 53);">*</em></label>
                                        <asp:DropDownList ID="cboLeaveType" runat="server" Height="25px" Width="240px" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblLvBal" runat="server" Text=""></asp:Label>
                                    </li>
                                    <li style="margin: 3px 0px 12px; padding: 0px; border: 0px; font-family: inherit;
                                        font-size: inherit; font-style: inherit; font-variant: inherit; font-weight: inherit;
                                        line-height: inherit; vertical-align: baseline; float: left; position: relative;
                                        width: 1149.296875px;">
                                        <label for="assignleave_txtFromDate" style="margin: 0px; padding: 0px; border: 0px;
                                            font-family: inherit; font-size: inherit; font-style: inherit; font-variant: inherit;
                                            font-weight: inherit; line-height: inherit; vertical-align: baseline; display: block;
                                            color: rgb(93, 93, 93); width: 150px; float: left;">
                                            From Date<span class="Apple-converted-space">&nbsp;</span><em style="margin: 0px;
                                                padding: 0px; border: 0px; font-family: inherit; font-size: inherit; font-style: inherit;
                                                font-variant: inherit; font-weight: inherit; line-height: inherit; vertical-align: baseline;
                                                color: rgb(170, 73, 53);">*</em></label>
                                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="etc"></asp:TextBox>
                                        <img alt="" class="ui-datepicker-trigger" src="../../../Image/calendar.png" style="margin: 0px 0px 0px 5px;
                                            padding: 0px; border: 0px; font-family: inherit; font-size: inherit; font-style: inherit;
                                            font-variant: inherit; font-weight: inherit; line-height: inherit; vertical-align: baseline;
                                            display: block; float: left;" title="" /></li>
                                    <li style="border-style: none; border-color: inherit; border-width: 0px; margin: 3px 0px 12px; padding: 0px; font-family: inherit;
                                        font-size: inherit; font-style: inherit; font-variant: inherit; font-weight: inherit;
                                        line-height: inherit; vertical-align: baseline; float: left; position: relative;
                                        width: 1151px; top: 0px; left: 0px; height: 26px;">
                                        <label for="assignleave_txtToDate" style="margin: 0px; padding: 0px; border: 0px;
                                            font-family: inherit; font-size: inherit; font-style: inherit; font-variant: inherit;
                                            font-weight: inherit; line-height: inherit; vertical-align: baseline; display: block;
                                            color: rgb(93, 93, 93); width: 150px; float: left;">
                                            To Date<span class="Apple-converted-space">&nbsp;</span><em style="margin: 0px; padding: 0px;
                                                border: 0px; font-family: inherit; font-size: inherit; font-style: inherit; font-variant: inherit;
                                                font-weight: inherit; line-height: inherit; vertical-align: baseline; color: rgb(170, 73, 53);">*</em></label>
                                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="etc"></asp:TextBox>
                                        <img alt="" class="ui-datepicker-trigger" src="../../../Image/calendar.png" style="margin: 0px 0px 0px 5px;
                                            padding: 0px; border: 0px; font-family: inherit; font-size: inherit; font-style: inherit;
                                            font-variant: inherit; font-weight: inherit; line-height: inherit; vertical-align: baseline;
                                            display: block; float: left;" title="" /></li>
                                    <li class="largeTextBox" style="margin: 3px 0px 12px; padding: 0px; border: 0px;
                                        font-family: inherit; font-size: inherit; font-style: inherit; font-variant: inherit;
                                        font-weight: inherit; line-height: inherit; vertical-align: baseline; float: left;
                                        position: relative; width: 1149.296875px;">
                                        <label for="assignleave_txtComment" style="margin: 0px; padding: 0px; border: 0px;
                                            font-family: inherit; font-size: inherit; font-style: inherit; font-variant: inherit;
                                            font-weight: inherit; line-height: inherit; vertical-align: baseline; display: block;
                                            color: rgb(93, 93, 93); width: 150px; float: left;">
                                            Comment</label><textarea id="assignleave_txtComment" cols="30" name="assignleave[txtComment]"
                                                rows="3" style="font-family: Arial, Helvetica, sans-serif; font-size: 13px; border: 1px solid rgb(210, 209, 209);
                                                color: rgb(93, 93, 93); border-top-left-radius: 3px; border-top-right-radius: 3px;
                                                border-bottom-right-radius: 3px; border-bottom-left-radius: 3px; outline: none;
                                                padding: 4px 6px; float: left; width: 250px; height: 80px; background-color: rgb(255, 255, 255);"></textarea></li>
                                    <li id="required" class="required new" style="margin: 9px 0px 12px; padding: 0px;
                                        border: 0px; font-family: inherit; font-size: 11px; font-style: inherit; font-variant: inherit;
                                        font-weight: inherit; line-height: inherit; vertical-align: baseline; float: left;
                                        position: relative; width: 425px; color: rgb(169, 169, 169); clear: left;"><em style="margin: 0px;
                                            padding: 0px; border: 0px; font-family: inherit; font-size: inherit; font-style: inherit;
                                            font-variant: inherit; font-weight: inherit; line-height: inherit; vertical-align: baseline;
                                            color: rgb(170, 73, 53);">*</em><span class="Apple-converted-space">&nbsp;</span>Required
                                        field</li>
                                </ol>
                                <ol style="border-bottom: 1px solid rgb(222, 222, 222); margin: 0px 0px 10px; padding: 0px 0px 5px;
                                    font-family: inherit; font-size: inherit; font-style: inherit; font-variant: inherit;
                                    font-weight: inherit; line-height: inherit; vertical-align: top; list-style: none;
                                    overflow: hidden; width: 441px; border-left-width: 0px; border-right-width: 0px;
                                    border-top-width: 0px; float: right; clip: rect(20px, auto, auto, auto); position: absolute;
                                    z-index: inherit; top: 55px; left: 635px; height: 307px;">
                                    <li></li>
                                </ol>
                                <p style="border-style: none; border-color: inherit; border-width: 0px; margin: 0px;
                                    padding: 5px 0px 0px; font-family: inherit; font-size: inherit; font-style: inherit;
                                    font-variant: inherit; font-weight: inherit; line-height: inherit; vertical-align: baseline;
                                    text-align: left; width: 607px;">
                                    &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Save"
                                        CssClass="btn" />
                                </p>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
            <br class="Apple-interchange-newline" />
        </ContentTemplate>
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
