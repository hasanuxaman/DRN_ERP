<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmSalesTC.aspx.cs" Inherits="DRN_WEB_ERP.Module.Transport.Forms.frmSalesTC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //        //Highlight Row when checkbox is checked
        //        function Check_Click(objRef) {
        //            //Get the Row based on checkbox
        //            var row = objRef.parentNode.parentNode;
        //            if (objRef.checked) {
        //                //If checked change color to Aqua
        //                row.style.backgroundColor = "aqua";
        //            }
        //            else {
        //                //If not checked change back to original color
        //                if (row.rowIndex % 2 == 0) {
        //                    //Alternating Row Color
        //                    //row.style.backgroundColor = "#C2D69B";
        //                    row.style.backgroundColor = "white";
        //                }
        //                else {
        //                    row.style.backgroundColor = "#EFF3FB";
        //                }
        //            }

        //            //Get the reference of GridView
        //            var GridView = row.parentNode;

        //            //Get all input elements in Gridview
        //            var inputList = GridView.getElementsByTagName("input");

        //            for (var i = 0; i < inputList.length; i++) {
        //                //The First element is the Header Checkbox
        //                var headerCheckBox = inputList[0];

        //                //Based on all or none checkboxes
        //                //are checked check/uncheck Header Checkbox
        //                var checked = true;
        //                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
        //                    if (!inputList[i].checked) {
        //                        checked = false;
        //                        break;
        //                    }
        //                }
        //            }
        //            headerCheckBox.checked = checked;
        //        }

        //        //Check all checkboxes functionality
        //        function checkAll(objRef) {
        //            var GridView = objRef.parentNode.parentNode.parentNode;
        //            var inputList = GridView.getElementsByTagName("input");
        //            for (var i = 0; i < inputList.length; i++) {
        //                //Get the Cell To find out ColumnIndex
        //                var row = inputList[i].parentNode.parentNode;
        //                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
        //                    if (objRef.checked) {
        //                        //If the header checkbox is checked
        //                        //check all checkboxes
        //                        //and highlight all rows
        //                        row.style.backgroundColor = "aqua";
        //                        inputList[i].checked = true;
        //                    }
        //                    else {
        //                        //If the header checkbox is checked
        //                        //uncheck all checkboxes
        //                        //and change rowcolor back to original
        //                        if (row.rowIndex % 2 == 0) {
        //                            //Alternating Row Color
        //                            //row.style.backgroundColor = "#C2D69B";
        //                            row.style.backgroundColor = "white";

        //                        }
        //                        else {
        //                            row.style.backgroundColor = "#EFF3FB";
        //                        }
        //                        inputList[i].checked = false;
        //                    }
        //                }
        //            }
        //        }

        //        //Highlight GridView row on mouseover event
        //        function MouseEvents(objRef, evt) {
        //            var checkbox = objRef.getElementsByTagName("input")[0];
        //            if (evt.type == "mouseover") {
        //                objRef.style.backgroundColor = "orange";
        //            }
        //            else {
        //                if (checkbox.checked) {
        //                    objRef.style.backgroundColor = "aqua";
        //                }
        //                else if (evt.type == "mouseout") {
        //                    if (objRef.rowIndex % 2 == 0) {
        //                        //Alternating Row Color
        //                        objRef.style.backgroundColor = "white";

        //                    }
        //                    else {
        //                        objRef.style.backgroundColor = "#EFF3FB";
        //                    }
        //                }
        //            }
        //        }
    </script>
    <style type="text/css">
        .Background
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        
        .Popup
        {
            border: 3px solid black;
            background-color: #CCCCCC;
            padding-top: 10px;
            padding-left: 10px;
            padding-right: 10px;
        }
        
        .lbl
        {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }
    </style>
    <div align="center" style="background-color: #00FF99">
        Transport Contact</div>
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="center" style="width: 100%; background-color: #CCFFFF;">
                <table style="width: 100%; font-size: small;">
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 318px">
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            TC No
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left" style="width: 318px">
                            <asp:TextBox ID="txtTcRefNo" runat="server" CssClass="textAlignCenter" Enabled="False"
                                Font-Bold="True" ForeColor="#FF9900" Width="180px"></asp:TextBox>
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            Date &amp; Time
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTcDate" runat="server" AutoPostBack="True" CssClass="textAlignCenter"
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 318px">
                            &nbsp;
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            Pay Mode
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left" style="width: 318px">
                            <asp:DropDownList ID="ddlTcPayMode" runat="server" Width="180px">
                                <asp:ListItem>CNF</asp:ListItem>
                                <asp:ListItem>FOB</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            Status
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlTcStatus" runat="server" Width="180px">
                                <asp:ListItem Value="P">Open</asp:ListItem>
                                <asp:ListItem Value="C">Cancel</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 318px">
                            &nbsp;
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            Dealer Name
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left" style="width: 318px">
                            <span>
                                <asp:TextBox ID="txtSearchDealer" runat="server" CssClass="search textAlignCenter"
                                    Width="300px" AutoPostBack="True" OnTextChanged="txtSearchDealer_TextChanged"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchDealer" runat="server" BehaviorID="AutoCompleteSrchDealer"
                                    CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                    ServiceMethod="GetSrchCustomer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                    ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearchDealer">
                                </cc1:AutoCompleteExtender>
                            </span>
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            Retailer Name
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSearchRetailer" runat="server" CssClass="inline search" Width="300px"
                                AutoPostBack="True" OnTextChanged="txtSearchRetailer_TextChanged"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderSrchRetailer" runat="server" BehaviorID="AutoCompleteSrchRetailer"
                                CompletionInterval="100" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem"
                                CompletionSetCount="20" DelimiterCharacters="," EnableCaching="true" MinimumPrefixLength="1"
                                ServiceMethod="GetSrchRetailer" ServicePath="~/Module/Sales/Forms/wsAutoComSales.asmx"
                                ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txtSearchRetailer">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 318px">
                            &nbsp;
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            Location
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left" style="width: 318px">
                            <asp:TextBox ID="txtTcLoc" runat="server" Enabled="False" Font-Bold="True" ForeColor="#FF9900"
                                Width="315px"></asp:TextBox>
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            Rate
                        </td>
                        <td width="2">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTcRate" runat="server" CssClass="textAlignCenter" Font-Bold="True"
                                ForeColor="#FF9900" Width="315px" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 79px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 318px">
                            &nbsp;
                        </td>
                        <td style="width: 48px">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 88px">
                            &nbsp;
                        </td>
                        <td width="2">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td style="width: 38px">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnSelectDo" runat="server" Text="Select D/O" OnClick="btnSelectDo_Click" />
                <br />
                <br />
                <asp:GridView ID="gvTcDetails" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Size="8pt" ForeColor="#333333" GridLines="None" PageSize="7" ShowFooter="True"
                    OnRowDeleting="gvTcDetails_RowDeleting">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="SL#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="D/O Ref. No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfTcDoHdrRef" runat="server" Value='<%# Bind("DO_REF") %>' />
                                <asp:HiddenField ID="hfTcDoDetLno" runat="server" Value='<%# Bind("DO_DET_LNO") %>' />
                                <asp:Label ID="lblTcDoHdrRefNo" runat="server" Text='<%# Bind("DO_REF_NO") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="110px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="DO_DATE" HeaderText="D/O Date">
                            <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Desc." />
                        <asp:BoundField DataField="ITEM_UOM" HeaderText="Unit" />
                        <asp:TemplateField HeaderText="D/O Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblTcDoQty" runat="server" Text='<%# Bind("DO_QTY", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Free Bag">
                            <ItemTemplate>
                                <asp:Label ID="lblTcDoFreeQty" runat="server" Text='<%# Bind("FREE_BAG_QTY", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Qty" HeaderStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblTcTotDoQty" runat="server" Text='<%# Bind("TOT_DO_QTY", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bal. Qty" HeaderStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblTcBalQty" runat="server" Text='<%# Bind("TC_BAL_QTY", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TC Qty">
                            <ItemTemplate>
                                <asp:TextBox ID="txtTcLinQty" runat="server" Width="70px" Font-Size="8" CssClass="textAlignCenter"
                                    Text='<%# Bind("TC_LIN_QTY", "{0:n}") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rent">
                            <ItemTemplate>
                                <%--<asp:TextBox ID="txtTcLinRate" runat="server" Text='<%# Bind("TC_LIN_RATE") %>'></asp:TextBox>--%>
                                <asp:Label ID="lblTcLinRate" runat="server" Text='<%# Bind("TC_LIN_RATE", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="txtTcLinAmt" runat="server" Text='<%# Bind("TC_LIN_AMT", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" Width="70px" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgBtnDelete" ImageUrl="~/Image/Delete.png" runat="server" CausesValidation="False"
                                    ToolTip="Delete" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Do you want to delete?')">
                                </asp:ImageButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="8" ForeColor="White"
                        Wrap="false" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" Font-Size="8" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                </asp:GridView>
                <br />
                <br />
            </div>
            <asp:Panel ID="PNL" runat="server" CssClass="Popup" align="center" Width="910px"
                Height="330px" Style="display: none;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div>
                            <asp:TextBox ID="txtSearchDo" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearchDo" runat="server" Width="70px" Text="Search" OnClick="btnSearchDo_Click" />
                            <asp:Button ID="btnSearchDoClear" runat="server" Width="70px" Text="Clear" OnClick="btnSearchDoClear_Click" />
                        </div>
                        <br />
                        <div style="height: 235px">
                            <asp:GridView ID="gvPendDoDet" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                Font-Size="8pt" ForeColor="#333333" GridLines="None" PageSize="7" AllowPaging="True"
                                OnPageIndexChanging="gvPendDoDet_PageIndexChanging" OnRowDataBound="gvPendDoDet_RowDataBound"
                                EmptyDataText="No Data Found........">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="D/O Ref. No">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfDoHdrRef" runat="server" Value='<%# Bind("DO_Hdr_Ref") %>' />
                                            <asp:HiddenField ID="hfDoDetLno" runat="server" Value='<%# Bind("DO_Det_Lno") %>' />
                                            <asp:HiddenField ID="hfHdrStat" runat="server" Value='<%# Bind("DO_Hdr_Status") %>' />
                                            <asp:Label ID="lblDoHdrRefNo" runat="server" Text='<%# Bind("DO_Hdr_Ref_No") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="110px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="D/O Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDoDate" runat="server" Text='<%# Bind("DO_Hdr_Date") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="120px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Par_Adr_Name" HeaderText="Dealer Name">
                                        <ItemStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Item Desc.">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfItemRef" runat="server" Value='<%# Bind("DO_Det_Icode") %>' />
                                            <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("Itm_Det_Desc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemUom" runat="server" Text='<%# Bind("DO_Det_Itm_Uom") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="15px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="D/O Qty" HeaderStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDoQty" runat="server" Text='<%# Bind("DO_Det_Lin_Qty", "{0:n}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Free Bag" HeaderStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDoFreeQty" runat="server" Text='<%# Bind("DO_Det_Free_Qty", "{0:n}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="40px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Qty" HeaderStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotDoQty" runat="server" Text=' <%# ((Convert.ToDecimal(Eval("DO_Det_Lin_Qty")) + Convert.ToDecimal(Eval("DO_Det_Free_Qty")))).ToString("N2") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bal. Qty" HeaderStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblDoBalQty" runat="server" Text='<%# Bind("DO_Det_Del_Bal_Qty") %>'></asp:Label>--%>
                                            <asp:Label ID="lblDoBalQty" runat="server" Text='<%# Bind("DO_Det_Unt_Wgt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--<HeaderTemplate>
                                            <asp:CheckBox ID="chkDoAll" runat="server" onclick="checkAll(this);" />
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDo" runat="server" onclick="Check_Click(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="8" ForeColor="White"
                                    Wrap="false" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" Font-Size="8" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                                <SortedDescendingHeaderStyle BackColor="#3E3277" />
                            </asp:GridView>
                        </div>
                        <br />
                        <div align="center">
                            <asp:Button ID="btnPopupDoOk" runat="server" OnClick="btnPopupDoOk_Click" Text="OK"
                                Width="80px" />
                            <asp:Button ID="btnPopupDoCancel" runat="server" Text="Cancel" Width="82px" />
                        </div>
                        <%--<table style="width: 100%; font-size: 14px;" bgcolor="#CC99FF">
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center">
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>--%>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearchDo" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="txtSearchDealer" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="txtSearchRetailer" EventName="TextChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:HiddenField ID="hfModalBtn" runat="server" />
                <br />
            </asp:Panel>
            <cc1:ModalPopupExtender ID="ModalPopupExtenderDoList" runat="server" BackgroundCssClass="modalBackground"
                CancelControlID="btnPopupDoCancel" PopupControlID="PNL" TargetControlID="hfModalBtn">
            </cc1:ModalPopupExtender>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSelectDo" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
