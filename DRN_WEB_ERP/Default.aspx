<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="DRN_WEB_ERP.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center" style="background-color: #00FF99">
        Pending Task List</div>
    <br />
    <table style="width: 100%;">
        <tr>
            <td>
            </td>
            <td align="center">
                <asp:LinkButton ID="lnkPendLv" runat="server" PostBackUrl="~/Module/HRMS/Forms/frmEmpLeaveAppr.aspx"></asp:LinkButton>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td align="center">
                <asp:LinkButton ID="lnkPendIoReq" runat="server" PostBackUrl="~/Module/IO/Forms/frmIOReqApp.aspx"></asp:LinkButton>
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
                <asp:LinkButton ID="lnkPendIoAdj" runat="server" PostBackUrl="~/Module/IO/Forms/frmIOAdjApp.aspx"></asp:LinkButton>
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
                <asp:LinkButton ID="lnkPendSo" runat="server" PostBackUrl="~/Module/Sales/Forms/frmSalesOrderApp.aspx"></asp:LinkButton>
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
                <asp:LinkButton ID="lnkPendDo" runat="server" PostBackUrl="~/Module/Sales/Forms/frmSalesDOPend.aspx"></asp:LinkButton>
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
                <asp:LinkButton ID="lnkSalesRet" runat="server" PostBackUrl="~/Module/Sales/Forms/frmSalesReturnApp.aspx"></asp:LinkButton>
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
                <asp:LinkButton ID="lnkStoreReq" runat="server" PostBackUrl="~/Module/Inventory/Forms/frmStoreReqAppr.aspx"></asp:LinkButton>
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
                <asp:LinkButton ID="lnkPendSrDDL" runat="server" PostBackUrl="~/Module/Inventory/Forms/frmStoreReqApprDDL.aspx"></asp:LinkButton>
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
                <asp:LinkButton ID="lnkSrIssue" runat="server" PostBackUrl="~/Module/Inventory/Forms/frmStoreReqIssu.aspx"></asp:LinkButton>
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
                <asp:LinkButton ID="lnkTrnJv" runat="server" PostBackUrl="~/Module/Accounts/Forms/frmTranTripBillAppr.aspx"></asp:LinkButton>
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
                <asp:LinkButton ID="lnkMpr" runat="server" PostBackUrl="~/Module/Procurement/Forms/frmMprAppr.aspx"></asp:LinkButton>
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
                <asp:LinkButton ID="lnkQtn" runat="server" PostBackUrl="~/Module/Procurement/Forms/frmQtnPendMpr.aspx"></asp:LinkButton>
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
                <asp:LinkButton ID="lnkCsApp" runat="server" PostBackUrl="~/Module/Procurement/Forms/frmCsAppr.aspx"></asp:LinkButton>
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
                <asp:LinkButton ID="lnkPoApp" runat="server" PostBackUrl="~/Module/Procurement/Forms/frmQuotPoAppr.aspx"></asp:LinkButton>
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
                <asp:LinkButton ID="lnkPo" runat="server" PostBackUrl="~/Module/Procurement/Forms/frmPoInit.aspx"></asp:LinkButton>
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
                <asp:LinkButton ID="lnkAdvPay" runat="server" PostBackUrl="~/Module/Procurement/Forms/frmSupplierPayAdv.aspx"></asp:LinkButton>
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
                <asp:LinkButton ID="lnkVatPend" runat="server" PostBackUrl="~/Module/Transport/Forms/frmLoadSlipVatPendList.aspx"></asp:LinkButton>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
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
