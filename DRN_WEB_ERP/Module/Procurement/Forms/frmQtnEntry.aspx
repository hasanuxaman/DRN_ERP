﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    EnableEventValidation="false" ValidateRequest="false" CodeBehind="frmQtnEntry.aspx.cs"
    Inherits="DRN_WEB_ERP.Module.Procurement.Forms.frmQtnEntry" %>

<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Register Src="UserControl/CtlQtnEntry.ascx" TagName="CtlQtnEntry" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function SelectAll(id, indx) {
            //get reference of GridView control
            if (indx == '1')
                var grid = document.getElementById("<%= gdgen.ClientID %>");
            else if (indx == '2')
                var grid = document.getElementById("<%= gdspe.ClientID %>");
            else
                var grid = document.getElementById("<%= gdpay.ClientID %>");

            // var grid = document.getElementById(itm);
            //variable to contain the cell of the grid
            var cell;

            if (grid.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid.rows.length; i++) {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];

                    //loop according to the number of childNodes in the cell
                    for (j = 0; j < cell.childNodes.length; j++) {
                        //if childNode type is CheckBox


                        if (cell.childNodes[j].type == "checkbox") {

                            //assign the status of the Select All checkbox to the cell checkbox within the grid
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                            ColorRow(cell.childNodes[j]);
                        }
                    }
                }
            }
        }

        function ColorRow(CheckBoxObj) {
            if (CheckBoxObj.checked == true) {
                CheckBoxObj.parentElement.parentElement.style.backgroundColor = '#88AAFF';
            }
            else {
                CheckBoxObj.parentElement.parentElement.style.backgroundColor = '#f0f8ff';
            }
        }

        function OvrdSubmit() {
            var ftbSubmit = document.forms[0].onsubmit;
            if (typeof (ftbSubmit) == 'function') {
                document.forms[0].onsubmit = function () {
                    try { ftbSubmit(); }
                    catch (ex) { }
                }
            }

            // We are ok
            return true;
        }                            
    </script>
    <div align="center" style="background-color: #00FF99">
        Quotation Entry</div>
    <div style="background-color: #f0f8ff">
        <br />
        <div align="center" style="border: 1px dashed #006699">
            Vendor Name:<asp:TextBox ID="txtparty" runat="server" CssClass="txtbox" autocomplete="off"
                Width="450px"></asp:TextBox>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteSrch"
                CompletionInterval="100" EnableCaching="true" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                DelimiterCharacters="," MinimumPrefixLength="1" ServiceMethod="GetSrchSupplierQtn"
                ServicePath="~/Module/Procurement/Forms/wsAutoComProc.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                TargetControlID="txtparty">
            </ajaxToolkit:AutoCompleteExtender>
            <asp:Button ID="btnAddQtnSup" runat="server" OnClick="btnAddQtnSup_Click" Text="Add New..." />
            &nbsp; Submit Date:
            <asp:TextBox ID="txtSubmitDate" runat="server" CssClass="textAlignCenter" ForeColor="Blue"
                Width="170px" Enabled="False"></asp:TextBox>
            <asp:Image ID="imgReqDt" runat="server" ImageUrl="~/Image/calendar.png" />
            <ajaxToolkit:CalendarExtender ID="CalenderDelDt" runat="server" Enabled="True" Format="dd/MM/yyyy"
                PopupButtonID="imgReqDt" TargetControlID="txtSubmitDate">
            </ajaxToolkit:CalendarExtender>
        </div>
        <div>
            <asp:PlaceHolder ID="celquo" runat="server"></asp:PlaceHolder>
        </div>
        <div>
            <b>GENERAL TERMS AND CONDITIONS:</b>
            <asp:GridView ID="gdgen" runat="server" OnRowDataBound="gdgen_RowDataBound" Width="100%"
                AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField>
                        <AlternatingItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Text='<%# Bind("TaC_Seq_No") %>' />
                        </AlternatingItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="All" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Text='<%# Bind("TaC_Seq_No") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Terms and condition">
                        <ItemTemplate>
                            <FTB:FreeTextBox ID="TextBox1" runat="Server" Width="100%" EnableToolbars="False"
                                BreakMode="LineBreak" DownLevelCols="50" EnableHtmlMode="false" EditorBorderColorDark="AliceBlue"
                                EditorBorderColorLight="AliceBlue" FormatHtmlTagsToXhtml="True" Height="70px"
                                AllowHtmlMode="False" DesignModeBodyTagCssClass="" DesignModeCss="" BackColor="AliceBlue"
                                GutterBackColor="AliceBlue" GutterBorderColorDark="AliceBlue" GutterBorderColorLight="AliceBlue"
                                Text='<%# Bind("TaC_Det") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <div>
            <b>SPECIAL TERMS AND CONDITIONS:</b>
            <asp:GridView ID="gdspe" runat="server" Width="100%" AutoGenerateColumns="False"
                OnRowDataBound="gdspe_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <AlternatingItemTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" Text='<%# Bind("TaC_Seq_No") %>' />
                        </AlternatingItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="All" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" Text='<%# Bind("TaC_Seq_No") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Terms and condition">
                        <ItemTemplate>
                            <FTB:FreeTextBox ID="TextBox2" runat="Server" Width="100%" EnableToolbars="False"
                                BreakMode="LineBreak" DownLevelCols="50" EnableHtmlMode="false" EditorBorderColorDark="AliceBlue"
                                EditorBorderColorLight="AliceBlue" FormatHtmlTagsToXhtml="True" Height="70px"
                                AllowHtmlMode="False" DesignModeBodyTagCssClass="" DesignModeCss="" BackColor="AliceBlue"
                                GutterBackColor="AliceBlue" GutterBorderColorDark="AliceBlue" GutterBorderColorLight="AliceBlue"
                                Text='<%# Bind("TaC_Det") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <div>
            <b>PAYMENT TERMS:</b>
            <asp:DropDownList ID="ddlpayterms" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpayterms_SelectedIndexChanged"
                Width="200px">
                <asp:ListItem Value="No">No Advance</asp:ListItem>
                <asp:ListItem Value="Part">Part Advance</asp:ListItem>
                <asp:ListItem Value="Full">Full Advance</asp:ListItem>
            </asp:DropDownList>
            <asp:GridView ID="gdpay" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdpay_RowDataBound"
                Width="100%">
                <Columns>
                    <asp:TemplateField>
                        <AlternatingItemTemplate>
                            <asp:CheckBox ID="CheckBox3" runat="server" Text='<%# Bind("TaC_Seq_No") %>' />
                        </AlternatingItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="CheckBox3" runat="server" Text="All" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox3" runat="server" Text='<%# Bind("TaC_Seq_No") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Terms and condition">
                        <ItemTemplate>
                            <FTB:FreeTextBox ID="TextBox3" runat="Server" AllowHtmlMode="False" BackColor="AliceBlue"
                                BreakMode="LineBreak" DesignModeBodyTagCssClass="" DesignModeCss="" DownLevelCols="50"
                                EditorBorderColorDark="AliceBlue" EditorBorderColorLight="AliceBlue" EnableHtmlMode="false"
                                EnableToolbars="False" FormatHtmlTagsToXhtml="True" GutterBackColor="AliceBlue"
                                GutterBorderColorDark="AliceBlue" GutterBorderColorLight="AliceBlue" Height="70px"
                                Width="100%" Text='<%# Bind("TaC_Det") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <%--<asp:UpdatePanel ID="upd" runat="server">
                <ContentTemplate>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlpayterms" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>--%>
        </div>
        <br />
        <div>
            <b>Above quotetion valid upto
                <asp:TextBox ID="txtvaliddays" runat="server" Width="50px" CssClass="textAlignCenter"></asp:TextBox>
                &nbsp;days after purchase order.</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <ajaxToolkit:FilteredTextBoxExtender ID="txtvaliddays_FilteredTextBoxExtender" runat="server"
                FilterType="Numbers" TargetControlID="txtvaliddays">
            </ajaxToolkit:FilteredTextBoxExtender>
            <asp:Button ID="btnsave" runat="server" CssClass="btn2 inline" Text="SAVE" Width="105px"
                OnClick="btnsave_Click" />
        </div>
        <br />
        <br />
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
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderMsg" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnMsgOk" OkControlID="btnMsgOk" PopupControlID="pnlMsg" TargetControlID="hfHidden"
        DropShadow="true">
    </ajaxToolkit:ModalPopupExtender>
</asp:Content>
