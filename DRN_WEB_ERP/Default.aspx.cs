using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsTranTableAdapters;
using DRN_WEB_ERP.Module.IO.DataSet.dsIOTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.Transport.DataSet;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;

namespace DRN_WEB_ERP
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Auto  Page Refresh
            Response.AppendHeader("Refresh", "60");

            if (Page.IsPostBack) return;

            try
            {
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var userRef = Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString();
                empRef = "100001";
                #region Pending Leave
                var taPndLvDet = new ViewHrmsEmpLeaveAppTableAdapter();
                var dtPndLvDet = new DataTable();
                if (empRef == "000634" || empRef == "000884" || empRef == "000011")//HRD,Nahamad
                    dtPndLvDet = taPndLvDet.GetDataByHrdRef(empRef);
                else if (empRef == "100001") //Admin User
                    dtPndLvDet = taPndLvDet.GetDataByAdminUser();
                else if (userRef == "100290" || userRef == "100291") //VC,SMD Sir
                    dtPndLvDet = taPndLvDet.GetDataBySupRef("000586");
                else
                    dtPndLvDet = taPndLvDet.GetDataBySupRef(empRef);
                lnkPendLv.Text = "Leave Apply : (" + dtPndLvDet.Rows.Count.ToString() + ")";
                lnkPendLv.Visible = dtPndLvDet.Rows.Count > 0;
                #endregion

                #region Pending I/O Requisition
                var taPendIoApp = new ViewIOReqAppTableAdapter();
                var dtPndLIoApp = new DataTable();
                if (empRef == "000568" || empRef == "000011")//Head of F&A -----2
                {
                    dtPndLIoApp = taPendIoApp.GetDataByHOFnA();
                }
                else if (empRef == "000555" || empRef == "000011")//CEO---------3
                {
                    dtPndLIoApp = taPendIoApp.GetDataByCEO();
                }
                else if (empRef == "000002" || empRef == "000011")//Mamun Sir for DECL---------2
                {
                    dtPndLIoApp = taPendIoApp.GetDataByCEODecl();
                }
                //else if (empRef == "000071" || empRef == "000069" || empRef == "000011")//Cashier GROUP & ECIL-----4
                else if (empRef == "000069" || empRef == "000011")//Cashier GROUP & ECIL-----4
                {
                    dtPndLIoApp = taPendIoApp.GetDataByCashGroupEcil();
                }
                else if (empRef == "000080" || empRef == "000224" || empRef == "000011")//Cashier DDL & DECL-----4
                {
                    dtPndLIoApp = taPendIoApp.GetDataByCashDdlDecl();
                }
                else
                    dtPndLIoApp = taPendIoApp.GetDataBySupId(empRef);
                lnkPendIoReq.Text = "I/O Requisition : (" + dtPndLIoApp.Rows.Count.ToString() + ")";
                lnkPendIoReq.Visible = dtPndLIoApp.Rows.Count > 0;
                #endregion

                #region Pending I/O Adjustment
                var taPendIoAdjApp = new ViewIoAdjAppTableAdapter();
                var dtPndLIoAdjApp = new DataTable();
                //if (empRef == "000071" || empRef == "000069" || empRef == "000011")//Cashier GROUP & ECIL-----0
                if (empRef == "000069" || empRef == "000011")//Cashier GROUP & ECIL-----0
                {
                    dtPndLIoAdjApp = taPendIoAdjApp.GetDataByCashGroupEcil();
                }
                else if (empRef == "000080" || empRef == "000224" || empRef == "000011")//Cashier DDL & DECL-----0
                {
                    dtPndLIoAdjApp = taPendIoAdjApp.GetDataByCashDdlDecl();
                }
                lnkPendIoAdj.Text = "I/O Adjustment : (" + dtPndLIoAdjApp.Rows.Count.ToString() + ")";
                lnkPendIoAdj.Visible = dtPndLIoAdjApp.Rows.Count > 0;
                #endregion

                #region Pending S/O
                var taOrd = new VIEW_SALES_ORDERTableAdapter();
                var dtOrd = new DataTable();
                //if (empRef == "000568" || empRef == "000018" || empRef == "000070" || empRef == "000071" || empRef == "000275" || empRef == "000011")//----------Arman Sir,Saiful,Lutfor,Atiqur,Ismail
                if (empRef == "000072" || empRef == "000275" || empRef == "000568" || empRef == "000011")//----------Azizul,Ismail, Nazmul Sir
                //if (empRef == "000316" || empRef == "000072" || empRef == "000011")//----------Masum Billah, Azizul Islam
                {
                    dtOrd = taOrd.GetDataByPendOrdAcc();
                }
                //if (empRef == "000568" || empRef == "000011")//---------Nazmul Sir
                if (empRef == "000856" || empRef == "000011")//---------Ali Haider Sir
                {
                    dtOrd = taOrd.GetDataByPendOrdAccHead();
                }
                if (empRef == "000410" || empRef == "000011")//---------Zakir Sir
                {
                    dtOrd = taOrd.GetDataByPendOrdAccHeadSalesWing1();
                }
                if (empRef == "000643" || empRef == "000011")//---------Rabbi Sir
                {
                    dtOrd = taOrd.GetDataByPendOrdAccHeadSalesWing2();
                } 
                lnkPendSo.Text = "Sales Order Approval : (" + dtOrd.Rows.Count.ToString() + ")";
                lnkPendSo.Visible = dtOrd.Rows.Count > 0;
                #endregion

                #region Pending D/O
                var taSalesOrd = new VIEW_SALES_ORDERTableAdapter();
                var dtSalesOrd = new DataTable();
                //if (empRef == "000773" || empRef == "000416" || empRef == "000011")//----------Fazlul Karim, Tushar
                if (empRef == "000416" || empRef == "000011")//---------Tushar
                {
                    dtSalesOrd = taSalesOrd.GetPendSoListAll();
                }
                if (empRef == "000773")//---------Fazlul Karim
                {
                    dtSalesOrd = taSalesOrd.GetPendSoListAllExcept();
                }
                lnkPendDo.Text = "Delivery Order : (" + dtSalesOrd.Rows.Count.ToString() + ")";
                lnkPendDo.Visible = dtSalesOrd.Rows.Count > 0;
                #endregion

                #region Pending Sales Return
                var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
                var dtInvHdr = new DataTable();
                if (empRef == "000856" || empRef == "000011")//----------Ali Haider           
                    dtInvHdr = taInvHdr.GetDataByRtnStatHold("1");
                else if (empRef == "000568" || empRef == "000275" || empRef == "000011")//----------Nazmul,Ismail
                    dtInvHdr = taInvHdr.GetDataByRtnStatHold("2");
                lnkSalesRet.Text = "Sales Return : (" + dtInvHdr.Rows.Count.ToString() + ")";
                lnkSalesRet.Visible = dtInvHdr.Rows.Count > 0;
                #endregion

                #region Pending Store Requisition
                var taPoHdr = new View_Pro_Sr_DetTableAdapter();
                var dtPoHdr = new DataTable();
                //if (empRef == "000515" || empRef == "000776" || empRef == "000011")//Abdullah,Bharat
                if (empRef == "000856" || empRef == "000011")//Ali Haider
                    dtPoHdr = taPoHdr.GetDataByAppr(empRef);
                //else if (empRef == "000148" || empRef == "000011")
                //    dtPoHdr = taPoHdr.GetDataByFwd(empRef);
                else
                    dtPoHdr = taPoHdr.GetDataBySupRef(empRef);
                lnkStoreReq.Text = "Srore Requsition : (" + dtPoHdr.Rows.Count.ToString() + ")";
                lnkStoreReq.Visible = dtPoHdr.Rows.Count > 0;
                #endregion

                #region Pending Store Requisition Issue
                var taPoHdrIssu = new View_Pro_Sr_DetTableAdapter();
                var dtPoHdrIssu = new DataTable();
                if (empRef == "000778" || empRef == "000011")//-----Mozumder
                    dtPoHdrIssu = taPoHdrIssu.GetDataByPendIssue();
                lnkSrIssue.Text = "Srore Requsition Issue : (" + dtPoHdrIssu.Rows.Count.ToString() + ")";
                lnkSrIssue.Visible = dtPoHdrIssu.Rows.Count > 0;
                #endregion

                #region Pending Transport Voucher
                var totHoldJv = 0;
                var taJvHold = new tbl_Acc_Jv_HoldTableAdapter();
                if (empRef == "000568" || empRef == "000070" || empRef == "000011")
                {
                    var dtJvHold = taJvHold.GetPendTranJvRef();
                    totHoldJv = dtJvHold == null ? 0 : Convert.ToInt32(dtJvHold);
                }
                lnkTrnJv.Text = "Transport Voucher : (" + totHoldJv.ToString() + ")";
                lnkTrnJv.Visible = totHoldJv > 0;
                #endregion

                #region Pending Purchase Requisition
                var taPrHdr = new View_PuTr_Pr_Hdr_DetTableAdapter();
                var dtPrHdr = new DataTable();
                if (empRef == "000002" || empRef == "000480" || empRef == "000134" || empRef == "000442" || empRef == "000561" || empRef == "000011" || empRef == "000683")//-----Mamun,Habib,Habib,Tarikul,Rahik,Emran
                    dtPrHdr = taPrHdr.GetDataByAppr();
                lnkMpr.Text = "Purchase Requsition : (" + dtPrHdr.Rows.Count.ToString() + ")";
                lnkMpr.Visible = dtPrHdr.Rows.Count > 0;
                #endregion

                #region Pending Quotation Entry
                var taPrHdrPend = new tbl_PuTr_Pr_DetTableAdapter();
                var dtPrHdrPend = new DataTable();
                if (empRef == "000002" || empRef == "000480" || empRef == "000134" || empRef == "000442" || empRef == "000561" || empRef == "000011" || empRef == "000683")//----mamun,hahib,habib,tariqul,rahik,emran
                    dtPrHdrPend = taPrHdrPend.GetDataByStatusWithinNinetyDays("TEN", "P");
                lnkQtn.Text = "Quotation Entry : (" + dtPrHdrPend.Rows.Count.ToString() + ")";
                lnkQtn.Visible = dtPrHdrPend.Rows.Count > 0;
                #endregion

                #region Pending C/S Approval
                View_CS_ListTableAdapter indet = new View_CS_ListTableAdapter();
                dsProcTran.View_CS_ListDataTable dtdet = new dsProcTran.View_CS_ListDataTable();
                if (empRef == "000914" || empRef == "000509" || empRef == "000510" || empRef == "000535" || empRef == "000549" || empRef == "000732" || empRef == "000011") //sohag,alwashib,riaz,Alif,Saroar,kawshik-----Audit
                    dtdet = indet.GetPendCsList();
                lnkCsApp.Text = "C/S Approval : (" + dtdet.Rows.Count.ToString() + ")";
                lnkCsApp.Visible = dtdet.Rows.Count > 0;
                #endregion

                #region Pending P/O Approval
                tbl_PuTr_Pr_DetTableAdapter indetPo = new tbl_PuTr_Pr_DetTableAdapter();
                dsProcTran.tbl_PuTr_Pr_DetDataTable dtdetPo = new dsProcTran.tbl_PuTr_Pr_DetDataTable();
                //if (empRef == "000002" || empRef == "000011")
                if (empRef == "000002" || empRef == "000480" || empRef == "000134" || empRef == "000442" || empRef == "000561" || empRef == "000011" || empRef == "000683")//mamun,habib,hahib,tariqul,rahik,emran
                    dtdetPo = indetPo.GetDataByStatus("APR", "P");
                lnkPoApp.Text = "P/O Approval : (" + dtdetPo.Rows.Count.ToString() + ")";
                lnkPoApp.Visible = dtdetPo.Rows.Count > 0;
                #endregion

                #region Pending PO
                tbl_PuTr_Pr_DetTableAdapter det = new tbl_PuTr_Pr_DetTableAdapter();
                dsProcTran.tbl_PuTr_Pr_DetDataTable dt = new dsProcTran.tbl_PuTr_Pr_DetDataTable();
                if (empRef == "000002" || empRef == "000480" || empRef == "000134" || empRef == "000442" || empRef == "000561" || empRef == "000011" || empRef == "000683")//mamun,hahib,habib,tariqul,rahik,emran
                    dt = det.GetDataByStatus("APP", "P");
                lnkPo.Text = "Purchase Order (P/O) : (" + dt.Rows.Count.ToString() + ")";
                lnkPo.Visible = dt.Rows.Count > 0;
                #endregion

                #region Pending Advance Payment
                tbl_Acc_Chq_VoucherTableAdapter taChqVoucher = new tbl_Acc_Chq_VoucherTableAdapter();
                dsAccTran.tbl_Acc_Chq_VoucherDataTable dtChqVoucher = new dsAccTran.tbl_Acc_Chq_VoucherDataTable();
                
                if (empRef == "000568" || empRef == "000070" || empRef == "000068" || empRef == "000011")//Arman,lutfar,tapash
                    dtChqVoucher = taChqVoucher.GetDataByStatus("INI");

                lnkAdvPay.Text = "Advance Payment : (" + dtChqVoucher.Rows.Count.ToString() + ")";
                lnkAdvPay.Visible = dtChqVoucher.Rows.Count > 0;
                #endregion

                #region Pending Store Requisition DDL
                var taPoHdrNew = new View_Pro_Sr_DetTableAdapter();
                var dtPoHdrNew = new DataTable();
                if (empRef == "000498" || empRef == "000011")//Nayan Zahid DDL
                    dtPoHdrNew = taPoHdrNew.GetDataByApprDDL();
                lnkPendSrDDL.Text = "Srore Requsition - [DDL] : (" + dtPoHdrNew.Rows.Count.ToString() + ")";
                lnkPendSrDDL.Visible = dtPoHdrNew.Rows.Count > 0;
                #endregion

                #region Pending Vat Challan
                var taLs = new tbl_TrTr_Load_SlipTableAdapter();
                var dtLS = new DataTable();
                if (empRef == "000044" || empRef == "000045" || empRef == "000139" || empRef == "000142" || empRef == "000011")//VAT-Rasheduzzaman,Sirajul Islam,Monirul Islam,Rokon Uddin
                    dtLS = taLs.GetDataByVatPend();
                lnkVatPend.Text = "VAT Challan Pending : (" + dtLS.Rows.Count.ToString() + ")";
                lnkVatPend.Visible = dtLS.Rows.Count > 0;
                #endregion
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
    }
}