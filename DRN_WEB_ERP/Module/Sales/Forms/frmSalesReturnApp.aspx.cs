using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesReturnApp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Auto  Page Refresh
            //Response.AppendHeader("Refresh", "60");

            if (Page.IsPostBack) return;

            try
            {
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var taInvDet = new View_InTr_Trn_Hdr_DetTableAdapter();
                var dtInvHdr = new DataTable();
                if (empRef == "000856" || empRef == "000011")//----------Ali Haider          
                    dtInvHdr = taInvDet.GetDataByPendRtn("1");
                else if (empRef == "000568" || empRef == "000275" || empRef == "000011")//----------Nazmul,Ismail
                    dtInvHdr = taInvDet.GetDataByPendRtn("2");
                gvPendRtn.DataSource = dtInvHdr;
                gvPendRtn.DataBind();
            }
            catch (Exception ex) { }
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            var taInvDet = new View_InTr_Trn_Hdr_DetTableAdapter();
            var dtInvHdr = taInvDet.GetDataByAppRtn();
            gvAppRtn.DataSource = dtInvHdr;
            gvAppRtn.DataBind();

            gvAppRtn.Visible = gvAppRtn.Visible ? false : true;

            if (btnShowRpt.Text == "Show Approved Sales Return")
                btnShowRpt.Text = "Hide Approved Sales Return";
            else
                btnShowRpt.Text = "Show Approved Sales Return";
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            var taSalesRtnHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taSalesRtnDet = new tbl_InTr_Trn_DetTableAdapter();
            var taComm = new tbl_Tran_ComTableAdapter();

            var taEmp = new View_Emp_BascTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesRtnHdr.Connection);

            try
            {
                #region Get Employee Details
                string empId = "", empName = "", empDesig = "";
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                {
                    empId = dtEmp[0].EmpId.ToString();
                    empName = dtEmp[0].EmpName.ToString();
                    empDesig = dtEmp[0].DesigName.ToString();
                }
                #endregion

                taSalesRtnHdr.AttachTransaction(myTran);
                taSalesRtnDet.AttachTransaction(myTran);
                taComm.AttachTransaction(myTran);

                GridViewRow gr = ((GridViewRow)((Button)sender).NamingContainer);
                var hfChlnRef = (HiddenField)(gr.FindControl("hfChlnRef"));
                var lblChlnRefNo = (Label)(gr.FindControl("lblChlnRefNo"));
                var hfChlnDetLno = (HiddenField)(gr.FindControl("hfChlnDetLno"));
                var hfCustRef = (HiddenField)(gr.FindControl("hfCustRef"));
                var hfDoRef = (HiddenField)(gr.FindControl("hfDoRef"));
                var hfDoRefNo = (HiddenField)(gr.FindControl("hfDoRefNo"));
                var hfDoDetLno = (HiddenField)(gr.FindControl("hfDoDetLno"));
                var hfRtnRef = (HiddenField)(gr.FindControl("hfRtnRef"));
                var lblRtnRefNo = (Label)(gr.FindControl("lblRtnRefNo"));
                var hfICode = (HiddenField)(gr.FindControl("hfICode"));
                var lblIDesc = (Label)(gr.FindControl("lblIDesc"));
                var lblUnit = (Label)(gr.FindControl("lblUnit"));
                var lblRtnQty = (Label)(gr.FindControl("lblRtnQty"));
                var lblRtnFreeQty = (Label)(gr.FindControl("lblRtnFreeQty"));

                var dtSalesOrdDet = taSalesRtnDet.GetDataByHdrRefItmRef(hfRtnRef.Value.ToString(), hfICode.Value.ToString());
                if (dtSalesOrdDet.Rows.Count > 0)
                {
                    taSalesRtnHdr.UpdateInvHdrStat("C", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                        DateTime.Now.ToString(), "0", "0", Convert.ToInt32(hfRtnRef.Value.ToString()));

                    var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblChlnRefNo.Text.Trim());
                    var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                    taComm.InsertTranCom(lblChlnRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "CLN-RTN", "APP", "(Sales Return Rejected By: " + empName + ") ", "", "1", "", "", "", "");

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Rejected Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    var taInvDet = new View_InTr_Trn_Hdr_DetTableAdapter();
                    var dtPendRtn = new DataTable();
                    if (empRef == "000856" || empRef == "000011")//----------Ali Haider            
                        dtPendRtn = taInvDet.GetDataByPendRtn("1");
                    else if (empRef == "000568" || empRef == "000275" || empRef == "000011")//----------Nazmul,Ismail
                        dtPendRtn = taInvDet.GetDataByPendRtn("2");
                    gvPendRtn.DataSource = dtPendRtn;
                    gvPendRtn.DataBind();
                }
                else
                {
                    myTran.Rollback();
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Return Details.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            var taSalesRtnHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taSalesRtnDet = new tbl_InTr_Trn_DetTableAdapter();
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var taComm = new tbl_Tran_ComTableAdapter();
            var taCrReal = new tblSalesCreditRealizeTableAdapter();
            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();

            var taEmp = new View_Emp_BascTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesRtnHdr.Connection);

            try
            {
                #region Get Employee Details
                string empId = "", empName = "", empDesig = "";
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                {
                    empId = dtEmp[0].EmpId.ToString();
                    empName = dtEmp[0].EmpName.ToString();
                    empDesig = dtEmp[0].DesigName.ToString();
                }
                #endregion

                taSalesRtnHdr.AttachTransaction(myTran);
                taSalesRtnDet.AttachTransaction(myTran);
                taComm.AttachTransaction(myTran);
                taCrReal.AttachTransaction(myTran);
                taStkCtl.AttachTransaction(myTran);

                GridViewRow gr = ((GridViewRow)((Button)sender).NamingContainer);
                var hfChlnRef = (HiddenField)(gr.FindControl("hfChlnRef"));
                var lblChlnRefNo = (Label)(gr.FindControl("lblChlnRefNo"));
                var hfChlnDetLno = (HiddenField)(gr.FindControl("hfChlnDetLno"));
                //var hfChlnStrCode = (HiddenField)(gr.FindControl("hfChlnStrCode"));
                var hfCustRef = (HiddenField)(gr.FindControl("hfCustRef"));                
                //var hfDoRef = (HiddenField)(gr.FindControl("hfDoRef"));
                //var hfDoRefNo = (HiddenField)(gr.FindControl("hfDoRefNo"));
                //var hfDoDetLno = (HiddenField)(gr.FindControl("hfDoDetLno"));
                var hfRtnRef = (HiddenField)(gr.FindControl("hfRtnRef"));
                var lblRtnRefNo = (Label)(gr.FindControl("lblRtnRefNo"));
                var hfICode = (HiddenField)(gr.FindControl("hfICode"));
                //var lblIDesc = (Label)(gr.FindControl("lblIDesc"));
                //var lblUnit = (Label)(gr.FindControl("lblUnit"));
                var lblRtnQty = (Label)(gr.FindControl("lblRtnQty"));
                //var lblRtnFreeQty = (Label)(gr.FindControl("lblRtnFreeQty"));                

                #region Get Cusromer Details
                var custName = "";
                var custAcc = "";
                var taCust = new tblSalesPartyAdrTableAdapter();
                var dtCust = taCust.GetDataByPartyAdrRef(Convert.ToInt32(hfCustRef.Value.Trim()));
                if (dtCust.Rows.Count > 0)
                {
                    custName = dtCust[0].Par_Adr_Name.ToString();
                    custAcc = dtCust[0].Par_Adr_Acc_Code.ToString();
                }
                else
                {
                    myTran.Rollback();
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                if (empRef == "000856" || empRef == "000011")//----------Ali Haider 
                {
                    if (lblRtnQty.Text.Trim() != "" || lblRtnQty.Text.Trim().Length > 0)
                    {
                        if (Convert.ToDouble(lblRtnQty.Text.Trim()) > 0)
                        {
                            var dtSalesRtnDet = taSalesRtnDet.GetDataByClnRefIcodeLno("SAL", hfChlnRef.Value.ToString(), hfICode.Value.ToString(), Convert.ToInt16(hfChlnDetLno.Value.ToString()));
                            if (dtSalesRtnDet.Rows.Count > 0)
                            {
                                taSalesRtnHdr.UpdateInvHdrStat("H", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "2", "", Convert.ToInt32(hfRtnRef.Value.ToString()));

                                var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblChlnRefNo.Text.Trim());
                                var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                                taComm.InsertTranCom(lblChlnRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "CLN-RTN", "APP", "(Sales Return Agreed By: " + empName + ") ", "", "1", "", "", "", "");

                                myTran.Commit();
                                tblMsg.Rows[0].Cells[0].InnerText = "Sales Return Forwarded Successfully.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                            }
                        }
                    }
                }

                var updtChk = false;
                if (empRef == "000568" || empRef == "000275" || empRef == "000011")//----------Nazmul,Ismail
                {
                    taSalesRtnHdr.UpdateInvHdrStat("P", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "1", "", Convert.ToInt32(hfRtnRef.Value.ToString()));

                    double totRtnQtySrt = 0;
                    var dtSalesRtnSRT = taSalesRtnDet.GetDataBySalesRtnRef(lblRtnRefNo.Text.ToString(),"SRT");
                    foreach (dsInvTran.tbl_InTr_Trn_DetRow drRtnSrt in dtSalesRtnSRT.Rows)
                    {
                        if (Convert.ToDouble(drRtnSrt.Trn_Det_Lin_Qty) > 0)
                        {
                            var dtSalesRtnDet = taSalesRtnDet.GetDataByClnRefIcodeLno("SAL", drRtnSrt.Trn_Det_Ord_Ref.ToString(), drRtnSrt.Trn_Det_Icode.ToString(), drRtnSrt.Trn_Det_Ord_Det_Lno);
                            if (dtSalesRtnDet.Rows.Count > 0)
                            {
                                if (dtSalesRtnDet[0].Trn_Det_Lin_Qty > 0)
                                {
                                    var dtSalesRtnDetBns = taSalesRtnDet.GetDataByClnRefIcodeLno("BNS", drRtnSrt.Trn_Det_Ord_Ref.ToString(), drRtnSrt.Trn_Det_Icode.ToString(), drRtnSrt.Trn_Det_Ord_Det_Lno);
                                    if (dtSalesRtnDetBns.Rows.Count > 0)
                                    {
                                        taSalesRtnDet.UpdateChallanQty(dtSalesRtnDet[0].Trn_Det_Lin_Qty - Convert.ToDouble(drRtnSrt.Trn_Det_Lin_Qty),
                                            dtSalesRtnDet[0].Trn_Det_Unt_Wgt - Convert.ToDouble(dtSalesRtnDetBns[0].Trn_Det_Lin_Qty),
                                            dtSalesRtnDet[0].Trn_Det_Bal_Qty - (Convert.ToDouble(drRtnSrt.Trn_Det_Lin_Qty) + Convert.ToDouble(dtSalesRtnDetBns[0].Trn_Det_Lin_Qty)),
                                            "SAL", drRtnSrt.Trn_Det_Ord_Ref.ToString(), drRtnSrt.Trn_Det_Icode.ToString(), drRtnSrt.Trn_Det_Ord_Det_Lno);
                                    }
                                    else
                                    {
                                        taSalesRtnDet.UpdateChallanQty(dtSalesRtnDet[0].Trn_Det_Lin_Qty - Convert.ToDouble(drRtnSrt.Trn_Det_Lin_Qty),
                                            dtSalesRtnDet[0].Trn_Det_Unt_Wgt - Convert.ToDouble(0),
                                            dtSalesRtnDet[0].Trn_Det_Bal_Qty - (Convert.ToDouble(drRtnSrt.Trn_Det_Lin_Qty) + Convert.ToDouble(0)),
                                            "SAL", drRtnSrt.Trn_Det_Ord_Ref.ToString(), drRtnSrt.Trn_Det_Icode.ToString(), drRtnSrt.Trn_Det_Ord_Det_Lno);
                                    }
                                    decimal NetRtnAmt = 0;

                                    #region Insert Accounts Voucher Entry
                                    var taDoDet = new tblSalesOrdDelDetTableAdapter();
                                    var dtDoDet = taDoDet.GetDataByDetLno(drRtnSrt.Trn_Det_Bin_Code.ToString(), drRtnSrt.Trn_Det_Exp_Lno);
                                    if (dtDoDet.Rows.Count > 0)
                                    {
                                        var dtMaxAccRef = taAcc.GetMaxRefNo("SRT", DateTime.Now.Year);
                                        var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                                        var nextAccRefNo = "SRT" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                                        var ReturnQty = Convert.ToDouble(drRtnSrt.Trn_Det_Lin_Qty);
                                        var ItemRate = Convert.ToDouble(dtDoDet[0].DO_Det_Lin_Rat);
                                        //var TransRate = Convert.ToDouble(dtSoDet[0].SO_Det_Trans_Rat);
                                        var TransRate = dtDoDet.Rows.Count > 0 ? Convert.ToDouble(dtDoDet[0].DO_Det_Trans_Rat) : 0;

                                        var OrdDiscount = (Convert.ToDouble(dtDoDet[0].DO_Det_Lin_Dis) * ReturnQty) / Convert.ToDouble(dtDoDet[0].DO_Det_Lin_Qty);
                                        var SalesAmt = Convert.ToDecimal((ReturnQty * ItemRate) - OrdDiscount);
                                        var TransAmt = Convert.ToDecimal(ReturnQty * TransRate);
                                        NetRtnAmt = SalesAmt + TransAmt;

                                        #region Get Item Details
                                        var itemName = "";
                                        var itemAcc = "";
                                        var taItemDet = new tbl_InMa_Item_DetTableAdapter();
                                        var dtItemDet = taItemDet.GetDataByItemRef(Convert.ToInt32(drRtnSrt.Trn_Det_Icode.ToString()));
                                        if (dtItemDet.Rows.Count > 0)
                                        {
                                            itemName = dtItemDet[0].Itm_Det_Desc.ToString();
                                            itemAcc = dtItemDet[0].Itm_Det_Acc_Code.ToString();
                                        }
                                        else
                                        {
                                            myTran.Rollback();
                                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                                            tblMsg.Rows[1].Cells[0].InnerText = "";
                                            ModalPopupExtenderMsg.Show();
                                            return;
                                        }
                                        #endregion

                                        #region Sales Return Journal
                                        //Credit-Customer Account
                                        taAcc.InsertAccData(custAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                            nextAccRefNo.ToString(), 1, 1, "Sales Return - " + drRtnSrt.Trn_Det_Ord_Ref_No.ToString()+ ", " + itemName.ToString(), "C", SalesAmt,
                                            lblRtnRefNo.Text.ToString(), "0", "BDT", 1, SalesAmt, "", "", "", "", "", "", "", "", "", "", "",
                                            (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, custName.ToString(), DateTime.Now,
                                            "ADM", "P", "", DateTime.Now, "SRT", "L", 0, "BDT", 1, "BDT", 1, SalesAmt, "",
                                            drRtnSrt.Trn_Det_Ord_Ref_No.ToString(), dtDoDet[0].DO_Det_SO_Ref_No, "N", 1, 0, "", "", "", "J", 0, "1", "SRT");

                                        //Debit-Item Account
                                        taAcc.InsertAccData(itemAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                            nextAccRefNo.ToString(), 2, 1, custName.ToString(), "D", SalesAmt, lblRtnRefNo.Text.ToString(), "0",
                                            "BDT", 1, SalesAmt, "", "", "", "", "", "", "", "", "", "", "",
                                            (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, itemName.ToString(), DateTime.Now,
                                            "ADM", "I", "", DateTime.Now, "SRT", "L", 0, "BDT", 1, "BDT", 1, SalesAmt, "",
                                            drRtnSrt.Trn_Det_Ord_Ref_No.ToString(), dtDoDet[0].DO_Det_SO_Ref_No, "N", 1, 0, "", "", "", "J", 0, "1", "SRT");
                                        #endregion

                                        if (TransRate > 0)
                                        {
                                            #region Carrying Charge Return Journal
                                            //Credit-Customer Account
                                            taAcc.InsertAccData(custAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                                nextAccRefNo.ToString(), 3, 1, "Sales Return Carrying Charge " + drRtnSrt.Trn_Det_Ord_Ref_No.ToString() + ", " + itemName.ToString(),
                                                "C", TransAmt, lblRtnRefNo.Text.ToString(), "0", "BDT", 1, TransAmt, "", "", "", "", "", "", "", "", "", "", "",
                                                (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, custName.ToString(), DateTime.Now,
                                                "ADM", "P", "", DateTime.Now, "SRT", "L", 0, "BDT", 1, "BDT", 1, TransAmt, "",
                                                drRtnSrt.Trn_Det_Ord_Ref_No.ToString(), dtDoDet[0].DO_Det_SO_Ref_No, "N", 1, 0, "", "", "", "J", 0, "1", "SRT");

                                            //Debit-Sales Carrying
                                            taAcc.InsertAccData("01.001.001.0246".ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                                nextAccRefNo.ToString(), 4, 1, custName.ToString(), "D", TransAmt, lblRtnRefNo.Text.ToString(), "0",
                                                "BDT", 1, TransAmt, "", "", "", "", "", "", "", "", "", "", "",
                                                (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, "Sales Carrying Charge", DateTime.Now,
                                                "ADM", "I", "", DateTime.Now, "SRT", "L", 0, "BDT", 1, "BDT", 1, TransAmt, "",
                                                drRtnSrt.Trn_Det_Ord_Ref_No.ToString(), dtDoDet[0].DO_Det_SO_Ref_No, "N", 1, 0, "", "", "", "J", 0, "1", "SRT");
                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        myTran.Rollback();
                                        tblMsg.Rows[0].Cells[0].InnerText = "Sales Order does not match.";
                                        tblMsg.Rows[1].Cells[0].InnerText = "";
                                        ModalPopupExtenderMsg.Show();
                                        return;
                                    }
                                    #endregion

                                    #region Credit Realization Entry Old
                                    ////var dtTotChlnAmt = taAcc.GetTotChlnAmt(lblChlnRefNo.Text.Trim());
                                    ////var rcvAmt = dtTotChlnAmt == null ? 0 : Convert.ToDecimal(dtTotChlnAmt);
                                    //var rcvAmt = NetRtnAmt;
                                    //var dtCrReal = taCrReal.GetRelizedChlnByCustRef(hfCustRef.Value.Trim());
                                    //foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                                    //{
                                    //    if (rcvAmt > dr.Sales_Chln_Due_Amt)
                                    //    {
                                    //        rcvAmt = rcvAmt - dr.Sales_Chln_Due_Amt;
                                    //        taCrReal.UpdateCreditRealizeAmt(0, dr.Sales_Chln_Pay_Rcv_Ref + "Chln. Rtn. Amt: " + (dr.Sales_Chln_Amt - dr.Sales_Chln_Due_Amt).ToString("N2") + ", ", dr.Sales_Chln_Ref);                                            
                                    //    }
                                    //    else
                                    //    {
                                    //        taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - rcvAmt, dr.Sales_Chln_Pay_Rcv_Ref + "Chln. Rtn. Amt: " + (dr.Sales_Chln_Due_Amt + rcvAmt).ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                    //        rcvAmt = 0;
                                    //        break;
                                    //    }
                                    //}
                                    #endregion

                                    #region Credit Realization Entry
                                    var rcvAmt = NetRtnAmt;
                                    var dtCrReal = taCrReal.GetPendChlnByCustRef(hfCustRef.Value.Trim());
                                    foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                                    {
                                        if (rcvAmt > dr.Sales_Chln_Due_Amt)
                                        {                                            
                                            taCrReal.UpdateCreditRealizeAmt(0, dr.Sales_Chln_Pay_Rcv_Ref + "RTN: " + lblChlnRefNo.Text + " Amt: " + dr.Sales_Chln_Due_Amt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                            rcvAmt = rcvAmt - dr.Sales_Chln_Due_Amt;
                                        }
                                        else
                                        {
                                            taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - rcvAmt, dr.Sales_Chln_Pay_Rcv_Ref + "RTN: " + lblChlnRefNo.Text + " Amt: " + rcvAmt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                            rcvAmt = 0;
                                            break;
                                        }
                                    }
                                    #endregion

                                    #region Update Stk_Ctl_Qty
                                    if (Convert.ToDouble(drRtnSrt.Trn_Det_Lin_Qty) > 0)
                                    {
                                        var dtStkCtl = taStkCtl.GetDataByStoreItem(drRtnSrt.Trn_Det_Str_Code.ToString(), drRtnSrt.Trn_Det_Icode.ToString());
                                        if (dtStkCtl.Rows.Count > 0)
                                        {
                                            taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk + Convert.ToDouble(drRtnSrt.Trn_Det_Lin_Qty)), 4),
                                                drRtnSrt.Trn_Det_Str_Code.ToString(), drRtnSrt.Trn_Det_Icode.ToString());
                                        }
                                        else
                                        {
                                            taStkCtl.InsertItemStore(drRtnSrt.Trn_Det_Str_Code.ToString(), drRtnSrt.Trn_Det_Icode.ToString(), "",
                                                Math.Round((Convert.ToDouble(drRtnSrt.Trn_Det_Lin_Qty)), 4), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, DateTime.Now, DateTime.Now, "", "", "", 0);
                                        }
                                    }
                                    #endregion

                                    updtChk = true;
                                }
                            }
                            else
                            {
                                myTran.Rollback();
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Sales Challan.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                        }

                        totRtnQtySrt = drRtnSrt.Trn_Det_Lin_Qty;
                    }

                    double totRtnQtyBrt = 0;
                    var dtSalesRtnBRT = taSalesRtnDet.GetDataBySalesRtnRef(lblRtnRefNo.Text.ToString(), "BRT");
                    foreach (dsInvTran.tbl_InTr_Trn_DetRow drRtnBrt in dtSalesRtnBRT.Rows)
                    {
                        if (Convert.ToDouble(drRtnBrt.Trn_Det_Lin_Qty) > 0)
                        {
                            var dtSalesRtnDet = taSalesRtnDet.GetDataByClnRefIcodeLno("BNS", drRtnBrt.Trn_Det_Ord_Ref.ToString(), drRtnBrt.Trn_Det_Icode.ToString(), drRtnBrt.Trn_Det_Ord_Det_Lno);
                            if (dtSalesRtnDet.Rows.Count > 0)
                            {
                                if (dtSalesRtnDet[0].Trn_Det_Lin_Qty > 0)
                                {
                                    taSalesRtnDet.UpdateChallanQty(dtSalesRtnDet[0].Trn_Det_Lin_Qty - Convert.ToDouble(drRtnBrt.Trn_Det_Lin_Qty), 0,
                                        0, "BNS", drRtnBrt.Trn_Det_Ord_Ref.ToString(), drRtnBrt.Trn_Det_Icode.ToString(), drRtnBrt.Trn_Det_Ord_Det_Lno);

                                    #region Update Stk_Ctl_Qty
                                    if (Convert.ToDouble(drRtnBrt.Trn_Det_Lin_Qty) > 0)
                                    {
                                        var dtStkCtl = taStkCtl.GetDataByStoreItem(drRtnBrt.Trn_Det_Str_Code.ToString(), drRtnBrt.Trn_Det_Icode.ToString());
                                        if (dtStkCtl.Rows.Count > 0)
                                        {
                                            taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk + Convert.ToDouble(drRtnBrt.Trn_Det_Lin_Qty)), 4), 
                                                drRtnBrt.Trn_Det_Str_Code.ToString(), drRtnBrt.Trn_Det_Icode.ToString());
                                        }
                                        else
                                        {
                                            taStkCtl.InsertItemStore(drRtnBrt.Trn_Det_Str_Code.ToString(), drRtnBrt.Trn_Det_Icode.ToString(), "", Math.Round((Convert.ToDouble(drRtnBrt.Trn_Det_Lin_Qty)), 4), 0,
                                                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, DateTime.Now, DateTime.Now, "", "", "", 0);
                                        }
                                    }
                                    #endregion

                                    updtChk = true;
                                }
                            }
                        }
                        totRtnQtyBrt = drRtnBrt.Trn_Det_Lin_Qty;
                    }

                    if (updtChk == true)
                    {
                        var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblChlnRefNo.Text.Trim());
                        var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                        taComm.InsertTranCom(lblChlnRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "CLN-RTN", "APP", "(Sales Return Approved By: " + empName + ") - Return Qty: " + (totRtnQtySrt + totRtnQtyBrt) + "(" + totRtnQtySrt.ToString() + "+" + totRtnQtyBrt.ToString() + ")", "", "1", "", "", "", "");

                        myTran.Commit();
                        tblMsg.Rows[0].Cells[0].InnerText = "Sales Return Approved Successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Sales Return Not Updated.";
                        tblMsg.Rows[1].Cells[0].InnerText = "There is not available challan qty to return.";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                
                var taInvDet = new View_InTr_Trn_Hdr_DetTableAdapter();
                var dtPendRtn = new DataTable();
                if (empRef == "000856" || empRef == "000011")//----------Ali Haider            
                    dtPendRtn = taInvDet.GetDataByPendRtn("1");
                else if (empRef == "000568" || empRef == "000275" || empRef == "000011")//----------Nazmul,Ismail
                    dtPendRtn = taInvDet.GetDataByPendRtn("2");
                gvPendRtn.DataSource = dtPendRtn;
                gvPendRtn.DataBind();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
    }
}