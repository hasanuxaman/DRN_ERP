using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.IO.DataSet.dsIOTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesOrderApp : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Auto  Page Refresh
            //Response.AppendHeader("Refresh", "60");

            if (Page.IsPostBack) return;

            try
            {
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var taOrd = new VIEW_PEND_SALES_ORDTableAdapter();
                var dtOrd = new DataTable();
                //if (empRef == "000568" || empRef == "000018" || empRef == "000070" || empRef == "000011")//----------Nazmul Sir,Saiful,Lutfor                    
                if (empRef == "000072" || empRef == "000275" || empRef == "000568" || empRef == "000011")//----------Azizul,Ismail, Nazmul Sir
                //if (empRef == "000316" || empRef == "000072" || empRef == "000011")//----------Masum Billah, Azizul Islam
                    dtOrd = taOrd.GetPendSoAcc();
                //if (empRef == "000568" || empRef == "000011")//--------Nazmul Sir
                if (empRef == "000856" || empRef == "000011")//---------Ali Haider Sir
                    dtOrd = taOrd.GetPendSoAccHead();
                if (empRef == "000410" || empRef == "000011")//---------Zakir Sir
                    dtOrd = taOrd.GetPendSoAccHeadSalesWing1();
                if (empRef == "000643" || empRef == "000011")//---------Rabbi Sir
                    dtOrd = taOrd.GetPendSoAccHeadSalesWing2();
                gvPendSoHdr.DataSource = dtOrd;
                gvPendSoHdr.DataBind();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvPendSoHdr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hfCustRef = (HiddenField)(e.Row.FindControl("hfCustRef"));
                var lblCustName = (Label)(e.Row.FindControl("lblCustName"));
                var lblCrLimit = (Label)(e.Row.FindControl("lblCrLimit"));
                var lblCrLimitOutStand = (Label)(e.Row.FindControl("lblCrLimitOutStand"));
                var txtCrLimitBal = (Label)(e.Row.FindControl("txtCrLimitBal"));
                var lnkBtnCrSecurity = (LinkButton)(e.Row.FindControl("lnkBtnCrSecurity"));

                var lblTodaysCollection = (Label)(e.Row.FindControl("lblTodaysCollection"));

                var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(hfCustRef.Value));
                if (dtPartyAdr.Rows.Count > 0)
                {
                    lblCustName.Text = dtPartyAdr[0].Par_Adr_Name;

                    var custAccCode = dtPartyAdr[0].IsPar_Adr_Acc_CodeNull() ? "" : dtPartyAdr[0].Par_Adr_Acc_Code;
                    var crLimit = dtPartyAdr[0].IsPar_Adr_Cr_LimitNull() ? 0 : dtPartyAdr[0].Par_Adr_Cr_Limit;

                    var taSalesOrder = new View_Sales_Ord_NewTableAdapter();
                    var dtSoBalAmtSum = taSalesOrder.GetSoBalAmt(hfCustRef.Value.ToString());
                    var soBalAmtSum = dtSoBalAmtSum == null ? 0 : Convert.ToDouble(dtSoBalAmtSum);
                    var taSalesOrderDo = new View_Delivery_Ord_NewTableAdapter();
                    var dtDoBalAmtSum = taSalesOrderDo.GetDoBalAmt(hfCustRef.Value.ToString());
                    var doBalAmtSum = dtDoBalAmtSum == null ? 0 : Convert.ToDouble(dtDoBalAmtSum);

                    var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                    var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                    var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                    var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                    var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                    //lblCrLimit.Text = crLimit.ToString("N2");
                    //lblCrLimitOutStand.Text = (drAmt - crAmt).ToString("N2");
                    //txtCrLimitBal.Text = (Convert.ToDouble(crLimit) - (drAmt - crAmt)).ToString("N2");

                    lblCrLimit.Text = crLimit.ToString("N2");
                    lblCrLimitOutStand.Text = (soBalAmtSum + doBalAmtSum + (drAmt - crAmt)).ToString("N2");
                    txtCrLimitBal.Text = (Convert.ToDouble(crLimit) - (soBalAmtSum + doBalAmtSum + (drAmt - crAmt))).ToString("N2");

                    var dtTodaysCollction = taFaTe.GetTotDrCrByAccCodeJvType(custAccCode.ToString(), "RJV", DateTime.Now.ToString(), DateTime.Now.ToString());
                    var TodaysCollction = dtTodaysCollction == null ? 0 : Convert.ToDouble(dtTodaysCollction);
                    lblTodaysCollection.Text = TodaysCollction.ToString("N2");

                    var taCrSecurity=new tblSalesCrLimitSecurityTableAdapter();
                    var dtCrSecurity=taCrSecurity.GetDataByPartyRef(Convert.ToInt32(hfCustRef.Value.ToString()));
                    if (dtCrSecurity.Rows.Count > 0)
                        lnkBtnCrSecurity.Text = dtCrSecurity[0].Sec_Tot_Amt.ToString("N2");                   
                }
                else
                {
                    lblCustName.Text = "";
                    lblCrLimit.Text = "0.00";
                    lblCrLimitOutStand.Text = "0.00";
                    txtCrLimitBal.Text = "0.00";
                    lnkBtnCrSecurity.Text = "0.00";
                }

                var gvPendSoDet = (GridView)(e.Row.FindControl("gvPendSoDet"));
                var taOrd = new VIEW_SALES_ORDERTableAdapter();
                var dtOrd = new DataTable();
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                if (empRef == "000072" || empRef == "000275" || empRef == "000568" || empRef == "000011")//----------Azizul,Ismail, Nazmul Sir
                //if (empRef == "000316" || empRef == "000072" || empRef == "000011")//----------Masum Billah, Azizul Islam
                    dtOrd = taOrd.GetDataByPendOrdCustAcc(hfCustRef.Value);
                //if (empRef == "000568" || empRef == "000011")//---------Nazmul Sir
                if (empRef == "000856" || empRef == "000011")//---------Ali Haider Sir
                    dtOrd = taOrd.GetDataByPendOrdCustAccHead(hfCustRef.Value);
                if (empRef == "000410" || empRef == "000011")//---------Zakir Sir
                    dtOrd = taOrd.GetDataByPendOrdCustAccHeadSalesWing1(hfCustRef.Value);
                if (empRef == "000643" || empRef == "000011")//---------Rabbi Sir
                    dtOrd = taOrd.GetDataByPendOrdCustAccHeadSalesWing2(hfCustRef.Value);
                gvPendSoDet.DataSource = dtOrd;
                gvPendSoDet.DataBind();
            }
        }

        protected void gvPendSoDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfOrgFreeQty = (HiddenField)e.Row.FindControl("hfOrgFreeQty");
                TextBox txtAppOrdQty = (TextBox)e.Row.FindControl("txtAppOrdQty");
                Label lblOrdRate = (Label)e.Row.FindControl("lblOrdRate");
                Label lblOrdTransRat = (Label)e.Row.FindControl("lblOrdTransRat");
                TextBox txtOrdAmt = (TextBox)e.Row.FindControl("txtOrdAmt");
                TextBox txtAppFreeQty = (TextBox)e.Row.FindControl("txtAppFreeQty");
                TextBox txtOrdNetAmt = (TextBox)e.Row.FindControl("txtOrdNetAmt");
                Label lblOrgOrdQty = (Label)e.Row.FindControl("lblOrgOrdQty");
                //HiddenField hfOrgOrdQty = (HiddenField)e.Row.FindControl("hfOrgOrdQty");
                HiddenField hfSoDetAppStat = (HiddenField)e.Row.FindControl("hfSoDetAppStat");
                HiddenField hfSoDetRejStat = (HiddenField)e.Row.FindControl("hfSoDetRejStat");

                Button btnRejectDo = (Button)e.Row.FindControl("btnRejectDo");
                Button btnApprDo = (Button)e.Row.FindControl("btnApprDo");
                ImageButton imgBtnDismiss = (ImageButton)e.Row.FindControl("imgBtnDismiss");

                txtAppOrdQty.Attributes.Add("onkeyup", "CalcOrdAmount('" + txtAppOrdQty.ClientID + "', '" + lblOrdRate.Text.Trim() + "', '"
                    + txtOrdAmt.ClientID + "', '" + lblOrdTransRat.Text.Trim() + "', '" + txtAppFreeQty.ClientID + "', '"
                    + txtOrdNetAmt.ClientID + "', '" + lblOrgOrdQty.Text.Trim() + "', '" + hfOrgFreeQty.Value + "' )");

                if (hfSoDetAppStat.Value == "A")
                {
                    e.Row.Cells[13].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[14].BackColor = System.Drawing.Color.Red;
                }

                if (empRef == "000275" || empRef == "000011")//---------Ismail
                    imgBtnDismiss.Visible = true;
                else
                    imgBtnDismiss.Visible = false;

                //if (empRef == "000410" || empRef=="000643")//---------Zakir Sir, Rabbi Sir
                //{
                //    if (hfSoDetRejStat.Value.ToString() == "0")
                //        btnApprDo.Text = "Forward";
                //    else
                //        btnApprDo.Text = "Accept";
                //}

                if (empRef == "000856" || empRef == "000011")//---------Ali Haider Sir
                {
                    if (hfSoDetRejStat.Value.ToString() == "0")
                        btnApprDo.Text = "Approve";
                    else
                        btnApprDo.Text = "Accept";
                }
            }
        }         

        protected void btnShowApprDo_Click(object sender, EventArgs e)
        {            
            var taOrd = new VIEW_SALES_ORDERTableAdapter();
            var dtOrd = new DataTable();
            dtOrd = taOrd.GetDataByApprOrd();
            gvApprDoDet.DataSource = dtOrd;
            gvApprDoDet.DataBind();

            gvApprDoDet.Visible = gvApprDoDet.Visible ? false : true;

            if (btnShowApprDo.Text == "Show Last 100 Approved S/O")
                btnShowApprDo.Text = "Hide Last 100 Approved S/O";
            else
                btnShowApprDo.Text = "Show Last 100 Approved S/O";
        }

        protected void btnRejectDo_Click_Old(object sender, EventArgs e)
        {
            var taSalesOrdHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesOrdDet = new tblSalesOrderDetTableAdapter();
            var taComm = new tbl_Tran_ComTableAdapter();
            var taEmp = new View_Emp_BascTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesOrdHdr.Connection);

            try
            {
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfParAdrRef = (HiddenField)(row.FindControl("hfParAdrRef"));
                var hfSoHdrRef = (HiddenField)(row.FindControl("hfSoHdrRef"));
                var lblSoHdrRefNo = (Label)(row.FindControl("lblSoHdrRefNo"));
                var hfSoDetLno = (HiddenField)(row.FindControl("hfSoDetLno"));
                var txtAppOrdQty = (TextBox)(row.FindControl("txtAppOrdQty"));
                var lblOrdRate = (Label)(row.FindControl("lblOrdRate"));
                var txtAppFreeQty = (TextBox)(row.FindControl("txtAppFreeQty"));
                var lblOrdTransRat = (Label)(row.FindControl("lblOrdTransRat"));
                var txtOrdNetAmt = (TextBox)(row.FindControl("txtOrdNetAmt"));

                #region Get Employee Details
                string empId = "", empName = "", empDesig = "", empDept = "";
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                {
                    empId = dtEmp[0].EmpId.ToString();
                    empName = dtEmp[0].EmpName.ToString();
                    empDesig = dtEmp[0].DesigName.ToString();
                    empDept = dtEmp[0].DeptName.ToString();
                }
                #endregion

                #region Get Credit Balance
                var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(hfParAdrRef.Value));

                var custRef = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Ref.ToString() : "";
                var custName = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Name.ToString() : "";
                var custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;

                var taSalesOrder = new View_Sales_Ord_NewTableAdapter();
                var dtSoBalAmtSum = taSalesOrder.GetSoBalAmt(custRef.ToString());
                var soBalAmtSum = dtSoBalAmtSum == null ? 0 : Convert.ToDouble(dtSoBalAmtSum);
                var taSalesOrderDo = new View_Delivery_Ord_NewTableAdapter();
                var dtDoBalAmtSum = taSalesOrderDo.GetDoBalAmt(custRef.ToString());
                var doBalAmtSum = dtDoBalAmtSum == null ? 0 : Convert.ToDouble(dtDoBalAmtSum);

                var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                var custCrOutsatnd = soBalAmtSum + doBalAmtSum + (drAmt - crAmt);
                var custCrLimitBal = (Convert.ToDouble(crLimit) - (soBalAmtSum + doBalAmtSum + (drAmt - crAmt)));
                #endregion

                var strMailHdr = "";
                var strmailTo = "";
                var strMailCc = "";

                var salesWing = "0";
                var taParAdr = new tblSalesPartyAdrTableAdapter();
                var dtParAdr = taParAdr.GetDataByPartyAdrRef(Convert.ToInt32(hfParAdrRef.Value.ToString()));
                salesWing = dtParAdr.Rows.Count > 0 ? (dtParAdr[0].IsPar_Adr_Ext_Data3Null() ? "0" : dtParAdr[0].Par_Adr_Ext_Data3.ToString()) : "0";

                var mailTo = "";
                if (salesWing == "1")
                    mailTo = "hossain.jakir@doreen.com";
                else if (salesWing == "2")
                    mailTo = "fazle.rabbi@doreen.com";
                else
                    mailTo = "imran.ahsan@doreen.com";

                taSalesOrdHdr.AttachTransaction(myTran);
                taSalesOrdDet.AttachTransaction(myTran);
                taComm.AttachTransaction(myTran);               

                var dtSalesOrdDet = taSalesOrdDet.GetDataByDetLno(hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value.ToString()));
                if (dtSalesOrdDet.Rows.Count > 0)
                {
                    //taSalesOrdHdr.UpdateSalesHdrStat("H", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                    //        DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                    //        (Convert.ToInt32(dtSalesOrdDet[0].SO_Det_Status) - 1).ToString(), hfSoHdrRef.Value.ToString());

                    //taSalesOrdDet.UpdateSalesDetStat((Convert.ToInt32(dtSalesOrdDet[0].SO_Det_Status) - 1).ToString(), "H", hfSoHdrRef.Value.ToString(),
                    //    Convert.ToInt16(hfSoDetLno.Value));

                    //var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblSoHdrRefNo.Text.Trim());
                    //var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                    //taComm.InsertTranCom(lblSoHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "INIT",
                    //    "Credit Approval Rejected [Qty:" + txtAppOrdQty.Text.Trim() + ", Amount: " + txtOrdNetAmt.Text.Trim() + "]", "", "1", "", "", "", "");

                    if (dtSalesOrdDet[0].SO_Det_Flag == "H")
                    {
                        if (dtSalesOrdDet[0].SO_Det_Status == "2")
                        {
                            taSalesOrdHdr.UpdateSalesHdrStat("H", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                0, "4", hfSoHdrRef.Value.ToString());

                            taSalesOrdDet.UpdateSalesDetStat(0, "4", "H", hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value));

                            var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblSoHdrRefNo.Text.Trim());
                            var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                            taComm.InsertTranCom(lblSoHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "INIT",
                                "(Credit Approval Rejected [Qty:" + txtAppOrdQty.Text.Trim() + ", Amount: " + txtOrdNetAmt.Text.Trim() + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "])", "", "1", "", "", "", "");

                            strMailCc = "rahman.tushar@doreen.com";
                        }
                        else
                        {
                            taSalesOrdHdr.UpdateSalesHdrStat("H", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                0, "1", hfSoHdrRef.Value.ToString());

                            taSalesOrdDet.UpdateSalesDetStat(0, "1", "H", hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value));

                            var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblSoHdrRefNo.Text.Trim());
                            var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                            taComm.InsertTranCom(lblSoHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "INIT",
                                "(Credit Approval Rejected [Qty:" + txtAppOrdQty.Text.Trim() + ", Amount: " + txtOrdNetAmt.Text.Trim() + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "])", "", "1", "", "", "", "");

                            strMailCc = "rahman.tushar@doreen.com";
                        }
                    }

                    myTran.Commit();
                    //tblMsg.Rows[0].Cells[0].InnerText = "Rejected Successfully.";
                    //tblMsg.Rows[1].Cells[0].InnerText = "";
                    //ModalPopupExtenderMsg.Show();

                    try
                    {
                        //Send Mail
                        var mBody = MsgBody(hfParAdrRef.Value.ToString(), "Credit Approval Rejected by " + empName + " : " + empDesig + " , " + empDept, txtAppOrdQty.Text.Trim(), txtOrdNetAmt.Text.Trim(), custCrLimitBal.ToString("N2"));

                        DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail(mailTo, strMailCc, "", "Credit Approval Rejected", mBody);
                    }
                    catch (Exception ex) { }   

                    var taOrd = new VIEW_PEND_SALES_ORDTableAdapter();
                    var dtOrd = new DataTable();
                    //if (empRef == "000568" || empRef == "000018" || empRef == "000070" || empRef == "000011")//----------Nazmul Sir,Saiful,Lutfor                    
                    if (empRef == "000072" || empRef == "000275" || empRef == "000568" || empRef == "000011")//----------Azizul,Ismail, Nazmul Sir
                    //if (empRef == "000316" || empRef == "000072" || empRef == "000011")//----------Masum Billah, Azizul Islam
                        dtOrd = taOrd.GetPendSoAcc();
                    //if (empRef == "000568" || empRef == "000011")//--------Nazmul Sir
                    if (empRef == "000856" || empRef == "000011")//---------Ali Haider Sir
                        dtOrd = taOrd.GetPendSoAccHead();
                    if (empRef == "000410" || empRef == "000011")//---------Zakir Sir
                        dtOrd = taOrd.GetPendSoAccHeadSalesWing1();
                    if (empRef == "000643" || empRef == "000011")//---------Rabbi Sir
                        dtOrd = taOrd.GetPendSoAccHeadSalesWing2();
                    gvPendSoHdr.DataSource = dtOrd;
                    gvPendSoHdr.DataBind();

                    var taApprOrd = new VIEW_SALES_ORDERTableAdapter();
                    var dtApprOrd = new DataTable();
                    dtApprOrd = taApprOrd.GetDataByApprOrd();
                    gvApprDoDet.DataSource = dtApprOrd;
                    gvApprDoDet.DataBind();
                }
                else
                {
                    myTran.Rollback();
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Order Details.";
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

        protected void btnRejectDo_Click(object sender, EventArgs e)
        {
            var taSalesOrdHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesOrdDet = new tblSalesOrderDetTableAdapter();
            var taComm = new tbl_Tran_ComTableAdapter();
            var taEmp = new View_Emp_BascTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesOrdHdr.Connection);

            try
            {
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfParAdrRef = (HiddenField)(row.FindControl("hfParAdrRef"));
                var hfSoHdrRef = (HiddenField)(row.FindControl("hfSoHdrRef"));
                var lblSoHdrRefNo = (Label)(row.FindControl("lblSoHdrRefNo"));
                var hfSoDetLno = (HiddenField)(row.FindControl("hfSoDetLno"));
                var txtAppOrdQty = (TextBox)(row.FindControl("txtAppOrdQty"));
                var lblOrdRate = (Label)(row.FindControl("lblOrdRate"));
                var txtAppFreeQty = (TextBox)(row.FindControl("txtAppFreeQty"));
                var lblOrdTransRat = (Label)(row.FindControl("lblOrdTransRat"));
                var txtOrdNetAmt = (TextBox)(row.FindControl("txtOrdNetAmt"));

                #region Get Employee Details
                string empId = "", empName = "", empDesig = "", empDept = "";
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                {
                    empId = dtEmp[0].EmpId.ToString();
                    empName = dtEmp[0].EmpName.ToString();
                    empDesig = dtEmp[0].DesigName.ToString();
                    empDept = dtEmp[0].DeptName.ToString();
                }
                #endregion

                #region Get Credit Balance
                var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(hfParAdrRef.Value));

                var custRef = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Ref.ToString() : "";
                var custName = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Name.ToString() : "";
                var custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;

                var taSalesOrder = new View_Sales_Ord_NewTableAdapter();
                var dtSoBalAmtSum = taSalesOrder.GetSoBalAmt(custRef.ToString());
                var soBalAmtSum = dtSoBalAmtSum == null ? 0 : Convert.ToDouble(dtSoBalAmtSum);
                var taSalesOrderDo = new View_Delivery_Ord_NewTableAdapter();
                var dtDoBalAmtSum = taSalesOrderDo.GetDoBalAmt(custRef.ToString());
                var doBalAmtSum = dtDoBalAmtSum == null ? 0 : Convert.ToDouble(dtDoBalAmtSum);

                var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                var custCrOutsatnd = soBalAmtSum + doBalAmtSum + (drAmt - crAmt);
                var custCrLimitBal = (Convert.ToDouble(crLimit) - (soBalAmtSum + doBalAmtSum + (drAmt - crAmt)));
                #endregion

                var strmailTo = "ali.haider@doreen.com";
                var strMailCc = "";

                taSalesOrdHdr.AttachTransaction(myTran);
                taSalesOrdDet.AttachTransaction(myTran);
                taComm.AttachTransaction(myTran);

                var dtSalesOrdDet = taSalesOrdDet.GetDataByDetLno(hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value.ToString()));
                if (dtSalesOrdDet.Rows.Count > 0)
                {
                    if (dtSalesOrdDet[0].SO_Det_Flag == "H")
                    {
                        if (dtSalesOrdDet[0].SO_Det_Status == "2")
                        {
                            taSalesOrdHdr.UpdateSalesHdrStat("H", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                0, "3", hfSoHdrRef.Value.ToString());

                            taSalesOrdDet.UpdateSalesDetStat(0, "3", "H", hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value));

                            var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblSoHdrRefNo.Text.Trim());
                            var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                            taComm.InsertTranCom(lblSoHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "INIT",
                                "(Credit Approval Rejected [Qty:" + txtAppOrdQty.Text.Trim() + ", Amount: " + txtOrdNetAmt.Text.Trim() + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "])", "", "1", "", "", "", "");

                            strMailCc = "rahman.tushar@doreen.com";
                        }
                        else
                        {
                            taSalesOrdHdr.UpdateSalesHdrStat("H", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                0, "1", hfSoHdrRef.Value.ToString());

                            taSalesOrdDet.UpdateSalesDetStat(0, "1", "H", hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value));

                            var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblSoHdrRefNo.Text.Trim());
                            var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                            taComm.InsertTranCom(lblSoHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "INIT",
                                "(Credit Approval Rejected [Qty:" + txtAppOrdQty.Text.Trim() + ", Amount: " + txtOrdNetAmt.Text.Trim() + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "])", "", "1", "", "", "", "");

                            strMailCc = "rahman.tushar@doreen.com";
                        }
                    }

                    myTran.Commit();
                    //tblMsg.Rows[0].Cells[0].InnerText = "Rejected Successfully.";
                    //tblMsg.Rows[1].Cells[0].InnerText = "";
                    //ModalPopupExtenderMsg.Show();

                    try
                    {
                        //Send Mail
                        var mBody = MsgBody(hfParAdrRef.Value.ToString(), "Credit Approval Rejected by " + empName + " : " + empDesig + " , " + empDept, txtAppOrdQty.Text.Trim(), txtOrdNetAmt.Text.Trim(), custCrLimitBal.ToString("N2"));

                        DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail(strmailTo, strMailCc, "", "Credit Approval Rejected", mBody);
                    }
                    catch (Exception ex) { }

                    var taOrd = new VIEW_PEND_SALES_ORDTableAdapter();
                    var dtOrd = new DataTable();
                    //if (empRef == "000568" || empRef == "000018" || empRef == "000070" || empRef == "000011")//----------Nazmul Sir,Saiful,Lutfor                    
                    if (empRef == "000072" || empRef == "000275" || empRef == "000568" || empRef == "000011")//----------Azizul,Ismail, Nazmul Sir
                        //if (empRef == "000316" || empRef == "000072" || empRef == "000011")//----------Masum Billah, Azizul Islam
                        dtOrd = taOrd.GetPendSoAcc();
                    //if (empRef == "000568" || empRef == "000011")//--------Nazmul Sir
                    if (empRef == "000856" || empRef == "000011")//---------Ali Haider Sir
                        dtOrd = taOrd.GetPendSoAccHead();
                    if (empRef == "000410" || empRef == "000011")//---------Zakir Sir
                        dtOrd = taOrd.GetPendSoAccHeadSalesWing1();
                    if (empRef == "000643" || empRef == "000011")//---------Rabbi Sir
                        dtOrd = taOrd.GetPendSoAccHeadSalesWing2();
                    gvPendSoHdr.DataSource = dtOrd;
                    gvPendSoHdr.DataBind();

                    var taApprOrd = new VIEW_SALES_ORDERTableAdapter();
                    var dtApprOrd = new DataTable();
                    dtApprOrd = taApprOrd.GetDataByApprOrd();
                    gvApprDoDet.DataSource = dtApprOrd;
                    gvApprDoDet.DataBind();
                }
                else
                {
                    myTran.Rollback();
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Order Details.";
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

        protected void btnApprDo_Click_Old(object sender, EventArgs e)
        {
            var taSalesOrdHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesOrdDet = new tblSalesOrderDetTableAdapter();
            var taComm = new tbl_Tran_ComTableAdapter();
            var taEmp = new View_Emp_BascTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesOrdHdr.Connection);

            try
            {
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfParAdrRef = (HiddenField)(row.FindControl("hfParAdrRef"));
                var hfSoHdrRef = (HiddenField)(row.FindControl("hfSoHdrRef"));
                var lblSoHdrRefNo = (Label)(row.FindControl("lblSoHdrRefNo"));
                var hfSoDetLno = (HiddenField)(row.FindControl("hfSoDetLno"));
                var txtAppOrdQty = (TextBox)(row.FindControl("txtAppOrdQty"));
                var lblOrdRate = (Label)(row.FindControl("lblOrdRate"));
                var txtAppFreeQty = (TextBox)(row.FindControl("txtAppFreeQty"));
                var lblOrdTransRat = (Label)(row.FindControl("lblOrdTransRat"));
                var txtRemarks = (TextBox)(row.FindControl("txtRemarks"));

                #region Get Employee Details
                string empId = "", empName = "", empDesig = "", empDept = "";
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                {
                    empId = dtEmp[0].EmpId.ToString();
                    empName = dtEmp[0].EmpName.ToString();
                    empDesig = dtEmp[0].DesigName.ToString();
                    empDept = dtEmp[0].DeptName.ToString();
                }
                #endregion

                #region Get Credit Balance
                var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(hfParAdrRef.Value));

                var custRef = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Ref.ToString() : "";
                var custName = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Name.ToString() : "";
                var custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;

                var taSalesOrder = new View_Sales_Ord_NewTableAdapter();
                var dtSoBalAmtSum = taSalesOrder.GetSoBalAmt(custRef.ToString());
                var soBalAmtSum = dtSoBalAmtSum == null ? 0 : Convert.ToDouble(dtSoBalAmtSum);
                var taSalesOrderDo = new View_Delivery_Ord_NewTableAdapter();
                var dtDoBalAmtSum = taSalesOrderDo.GetDoBalAmt(custRef.ToString());
                var doBalAmtSum = dtDoBalAmtSum == null ? 0 : Convert.ToDouble(dtDoBalAmtSum);

                var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                var custCrOutsatnd = soBalAmtSum + doBalAmtSum + (drAmt - crAmt);
                var custCrLimitBal = (Convert.ToDouble(crLimit) - (soBalAmtSum + doBalAmtSum + (drAmt - crAmt)));
                #endregion

                var strMailHdr = "";
                var strmailTo = "";
                var strMailCc = "";

                var salesWing = "0";
                var taParAdr = new tblSalesPartyAdrTableAdapter();
                var dtParAdr = taParAdr.GetDataByPartyAdrRef(Convert.ToInt32(hfParAdrRef.Value.ToString()));
                salesWing = dtParAdr.Rows.Count > 0 ? (dtParAdr[0].IsPar_Adr_Ext_Data3Null() ? "0" : dtParAdr[0].Par_Adr_Ext_Data3.ToString()) : "0";

                if (salesWing == "1")
                    strmailTo = "hossain.jakir@doreen.com";
                else if (salesWing == "2")
                    strmailTo = "fazle.rabbi@doreen.com";
                else
                    strmailTo = "imran.ahsan@doreen.com";                

                taSalesOrdHdr.AttachTransaction(myTran);
                taSalesOrdDet.AttachTransaction(myTran);
                taComm.AttachTransaction(myTran);

                var ORD_QTY = Convert.ToDouble(txtAppOrdQty.Text.Trim());
                var ORD_RATE = Convert.ToDecimal(lblOrdRate.Text.Trim());
                var ORD_AMOUNT = Convert.ToDecimal(Convert.ToDecimal(ORD_QTY) * ORD_RATE);
                var ORD_FREE_BAG = Convert.ToDouble(txtAppFreeQty.Text.Trim());
                var ORD_TRANS_RATE = Convert.ToDecimal(lblOrdTransRat.Text.Trim());
                var ORD_NET_AMOUNT = Convert.ToDecimal(Convert.ToDecimal(ORD_QTY) * ORD_RATE) + Convert.ToDecimal(Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE);

                var dtSalesOrdDet = taSalesOrdDet.GetDataByDetLno(hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value.ToString()));
                if (dtSalesOrdDet.Rows.Count > 0)
                {
                    if (dtSalesOrdDet[0].SO_Det_Flag == "H" && dtSalesOrdDet[0].SO_Det_Status == "2")
                    {
                        if (dtSalesOrdDet[0].SO_Det_T_Fl == "A")
                        {
                            taSalesOrdHdr.UpdateSalesHdrStat("H", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), 1, "4", hfSoHdrRef.Value.ToString());

                            taSalesOrdDet.UpdateSalesDetApp(ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, ORD_QTY + ORD_FREE_BAG, "0", ORD_FREE_BAG.ToString(),
                                (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)), (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)),
                                1, "4", "H", hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value));

                            var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblSoHdrRefNo.Text.Trim());
                            var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                            taComm.InsertTranCom(lblSoHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "RUN",
                                "(Credit Approved [Qty:" + ORD_QTY.ToString("N2") + ", Amount: " + ORD_NET_AMOUNT.ToString("N2") + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "]) " + txtRemarks.Text.Trim(),
                                "", "1", "", "", "", "");

                            strMailHdr = "Credit Approved";
                            //strMailCc = "masum.billah@doreen.com";
                            strMailCc = "rahman.tushar@doreen.com;ismail.saber@doreen.com";
                        }
                        else
                        {
                            taSalesOrdHdr.UpdateSalesHdrStat("P", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), 1, "1", hfSoHdrRef.Value.ToString());

                            taSalesOrdDet.UpdateSalesDetApp(ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, ORD_QTY + ORD_FREE_BAG, "0", ORD_FREE_BAG.ToString(),
                                (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)), (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)),
                                1, "1", "P", hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value));

                            var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblSoHdrRefNo.Text.Trim());
                            var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                            taComm.InsertTranCom(lblSoHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "APP",
                                "(Credit Verified [Qty:" + ORD_QTY.ToString("N2") + ", Amount: " + ORD_NET_AMOUNT.ToString("N2") + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "]) " + txtRemarks.Text.Trim(),
                                "", "1", "", "", "", "");

                            strMailHdr = "Credit Verified";
                            //strMailCc = "masum.billah@doreen.com";
                            strMailCc = "rahman.tushar@doreen.com;ismail.saber@doreen.com";
                        }
                    }

                    if (dtSalesOrdDet[0].SO_Det_Flag == "H" && dtSalesOrdDet[0].SO_Det_Status == "3")
                    {
                        taSalesOrdHdr.UpdateSalesHdrStat("P", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), 1, "1", hfSoHdrRef.Value.ToString());

                        taSalesOrdDet.UpdateSalesDetApp(ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, ORD_QTY + ORD_FREE_BAG, "0", ORD_FREE_BAG.ToString(),
                            (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)), (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)),
                            1, "1", "P", hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value));

                        var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblSoHdrRefNo.Text.Trim());
                        var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                        taComm.InsertTranCom(lblSoHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "APP",
                            "(Credit Approved [Qty:" + ORD_QTY.ToString("N2") + ", Amount: " + ORD_NET_AMOUNT.ToString("N2") + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "]) " + txtRemarks.Text.Trim(),
                            "", "1", "", "", "", "");

                        strMailHdr = "Credit Approved";
                        //strMailCc = "masum.billah@doreen.com";
                        strMailCc = "rahman.tushar@doreen.com;ismail.saber@doreen.com";
                    }

                    if (dtSalesOrdDet[0].SO_Det_Flag == "H" && dtSalesOrdDet[0].SO_Det_Status == "4")
                    {
                        if (dtSalesOrdDet[0].SO_Det_T_In == 0)
                        {
                            taSalesOrdHdr.UpdateSalesHdrStat("H", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), 1, "3", hfSoHdrRef.Value.ToString());

                            taSalesOrdDet.UpdateSalesDetApp(ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, ORD_QTY + ORD_FREE_BAG, "0", ORD_FREE_BAG.ToString(),
                                (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)), (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)),
                                1, "3", "H", hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value));

                            var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblSoHdrRefNo.Text.Trim());
                            var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                            taComm.InsertTranCom(lblSoHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "RUN",
                                "(Forwarded to Director (Operation) [Qty:" + ORD_QTY.ToString("N2") + ", Amount: " + ORD_NET_AMOUNT.ToString("N2") + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "]) " + txtRemarks.Text.Trim(),
                                "", "1", "", "", "", "");

                            strMailHdr = "Forwarded to Director (Operation)";
                            //strMailCc = "ali.haider@doreen.com,masum.billah@doreen.com";
                            strMailCc = "ali.haider@doreen.com,rahman.tushar@doreen.com;ismail.saber@doreen.com";
                        }
                        else
                        {
                            taSalesOrdHdr.UpdateSalesHdrStat("P", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), 1, "1", hfSoHdrRef.Value.ToString());

                            taSalesOrdDet.UpdateSalesDetApp(ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, ORD_QTY + ORD_FREE_BAG, "0", ORD_FREE_BAG.ToString(),
                                (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)), (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)),
                                1, "1", "P", hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value));

                            var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblSoHdrRefNo.Text.Trim());
                            var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                            taComm.InsertTranCom(lblSoHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "APP",
                                "(Credit Agreed [Qty:" + ORD_QTY.ToString("N2") + ", Amount: " + ORD_NET_AMOUNT.ToString("N2") + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "]) " + txtRemarks.Text.Trim(),
                                "", "1", "", "", "", "");

                            strMailHdr = "Credit Agreed";
                            //strMailCc = "masum.billah@doreen.com";
                            strMailCc = "rahman.tushar@doreen.com;ismail.saber@doreen.com";
                        }
                    }

                    myTran.Commit();
                    //tblMsg.Rows[0].Cells[0].InnerText = "Approved Successfully.";
                    //tblMsg.Rows[1].Cells[0].InnerText = "";
                    //ModalPopupExtenderMsg.Show();

                    try
                    {
                        //Send Mail
                        var mBody = MsgBody(hfParAdrRef.Value.ToString(), strMailHdr + " by " + empName + " : " + empDesig + " , " + empDept, txtAppOrdQty.Text.Trim(), ORD_NET_AMOUNT.ToString("N2"), custCrLimitBal.ToString("N2"));

                        DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail(strmailTo, strMailCc, "", strMailHdr, mBody);
                    }
                    catch (Exception ex) { } 

                    var taOrd = new VIEW_PEND_SALES_ORDTableAdapter();
                    var dtOrd = new DataTable();
                    //if (empRef == "000568" || empRef == "000018" || empRef == "000070" || empRef == "000011")//----------Nazmul Sir,Saiful,Lutfor                    
                    if (empRef == "000072" || empRef == "000275" || empRef == "000568" || empRef == "000011")//----------Azizul,Ismail, Nazmul Sir
                    //if (empRef == "000316" || empRef == "000072" || empRef == "000011")//----------Masum Billah, Azizul Islam
                        dtOrd = taOrd.GetPendSoAcc();
                    //if (empRef == "000568" || empRef == "000011")//--------Nazmul Sir
                    if (empRef == "000856" || empRef == "000011")//---------Ali Haider Sir
                        dtOrd = taOrd.GetPendSoAccHead();
                    if (empRef == "000410" || empRef == "000011")//---------Zakir Sir
                        dtOrd = taOrd.GetPendSoAccHeadSalesWing1();
                    if (empRef == "000643" || empRef == "000011")//---------Rabbi Sir
                        dtOrd = taOrd.GetPendSoAccHeadSalesWing2();
                    gvPendSoHdr.DataSource = dtOrd;
                    gvPendSoHdr.DataBind();

                    var taApprOrd = new VIEW_SALES_ORDERTableAdapter();
                    var dtApprOrd = new DataTable();
                    dtApprOrd = taApprOrd.GetDataByApprOrd();
                    gvApprDoDet.DataSource = dtApprOrd;
                    gvApprDoDet.DataBind();
                }
                else
                {
                    myTran.Rollback();
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Order Details.";
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

        protected void btnApprDo_Click(object sender, EventArgs e)
        {
            var taSalesOrdHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesOrdDet = new tblSalesOrderDetTableAdapter();
            var taComm = new tbl_Tran_ComTableAdapter();
            var taEmp = new View_Emp_BascTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesOrdHdr.Connection);

            try
            {
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfParAdrRef = (HiddenField)(row.FindControl("hfParAdrRef"));
                var hfSoHdrRef = (HiddenField)(row.FindControl("hfSoHdrRef"));
                var lblSoHdrRefNo = (Label)(row.FindControl("lblSoHdrRefNo"));
                var hfSoDetLno = (HiddenField)(row.FindControl("hfSoDetLno"));
                var txtAppOrdQty = (TextBox)(row.FindControl("txtAppOrdQty"));
                var lblOrdRate = (Label)(row.FindControl("lblOrdRate"));
                var txtAppFreeQty = (TextBox)(row.FindControl("txtAppFreeQty"));
                var lblOrdTransRat = (Label)(row.FindControl("lblOrdTransRat"));
                var txtRemarks = (TextBox)(row.FindControl("txtRemarks"));

                #region Get Employee Details
                string empId = "", empName = "", empDesig = "", empDept = "";
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                {
                    empId = dtEmp[0].EmpId.ToString();
                    empName = dtEmp[0].EmpName.ToString();
                    empDesig = dtEmp[0].DesigName.ToString();
                    empDept = dtEmp[0].DeptName.ToString();
                }
                #endregion

                #region Get Credit Balance
                var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(hfParAdrRef.Value));

                var custRef = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Ref.ToString() : "";
                var custName = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Name.ToString() : "";
                var custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;

                var taSalesOrder = new View_Sales_Ord_NewTableAdapter();
                var dtSoBalAmtSum = taSalesOrder.GetSoBalAmt(custRef.ToString());
                var soBalAmtSum = dtSoBalAmtSum == null ? 0 : Convert.ToDouble(dtSoBalAmtSum);
                var taSalesOrderDo = new View_Delivery_Ord_NewTableAdapter();
                var dtDoBalAmtSum = taSalesOrderDo.GetDoBalAmt(custRef.ToString());
                var doBalAmtSum = dtDoBalAmtSum == null ? 0 : Convert.ToDouble(dtDoBalAmtSum);

                var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                var custCrOutsatnd = soBalAmtSum + doBalAmtSum + (drAmt - crAmt);
                var custCrLimitBal = (Convert.ToDouble(crLimit) - (soBalAmtSum + doBalAmtSum + (drAmt - crAmt)));
                #endregion

                var strMailHdr = "";
                var strmailTo = "";
                var strMailCc = "";

                taSalesOrdHdr.AttachTransaction(myTran);
                taSalesOrdDet.AttachTransaction(myTran);
                taComm.AttachTransaction(myTran);

                var ORD_QTY = Convert.ToDouble(txtAppOrdQty.Text.Trim());
                var ORD_RATE = Convert.ToDecimal(lblOrdRate.Text.Trim());
                var ORD_AMOUNT = Convert.ToDecimal(Convert.ToDecimal(ORD_QTY) * ORD_RATE);
                var ORD_FREE_BAG = Convert.ToDouble(txtAppFreeQty.Text.Trim());
                var ORD_TRANS_RATE = Convert.ToDecimal(lblOrdTransRat.Text.Trim());
                var ORD_NET_AMOUNT = Convert.ToDecimal(Convert.ToDecimal(ORD_QTY) * ORD_RATE) + Convert.ToDecimal(Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE);

                var dtSalesOrdDet = taSalesOrdDet.GetDataByDetLno(hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value.ToString()));
                if (dtSalesOrdDet.Rows.Count > 0)
                {
                    if (dtSalesOrdDet[0].SO_Det_Flag == "H" && dtSalesOrdDet[0].SO_Det_Status == "2")
                    {
                        if (dtSalesOrdDet[0].SO_Det_T_Fl == "A")
                        {
                            taSalesOrdHdr.UpdateSalesHdrStat("H", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), 1, "3", hfSoHdrRef.Value.ToString());

                            taSalesOrdDet.UpdateSalesDetApp(ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, ORD_QTY + ORD_FREE_BAG, "0", ORD_FREE_BAG.ToString(),
                                (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)), (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)),
                                1, "3", "H", hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value));

                            var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblSoHdrRefNo.Text.Trim());
                            var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                            taComm.InsertTranCom(lblSoHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "RUN",
                                "(Credit Approved [Qty:" + ORD_QTY.ToString("N2") + ", Amount: " + ORD_NET_AMOUNT.ToString("N2") + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "]) " + txtRemarks.Text.Trim(),
                                "", "1", "", "", "", "");

                            strMailHdr = "Credit Approved";
                            //strMailCc = "masum.billah@doreen.com";
                            strMailCc = "rahman.tushar@doreen.com;ismail.saber@doreen.com";
                        }
                        else
                        {
                            taSalesOrdHdr.UpdateSalesHdrStat("P", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), 1, "1", hfSoHdrRef.Value.ToString());

                            taSalesOrdDet.UpdateSalesDetApp(ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, ORD_QTY + ORD_FREE_BAG, "0", ORD_FREE_BAG.ToString(),
                                (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)), (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)),
                                1, "1", "P", hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value));

                            var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblSoHdrRefNo.Text.Trim());
                            var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                            taComm.InsertTranCom(lblSoHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "APP",
                                "(Credit Verified [Qty:" + ORD_QTY.ToString("N2") + ", Amount: " + ORD_NET_AMOUNT.ToString("N2") + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "]) " + txtRemarks.Text.Trim(),
                                "", "1", "", "", "", "");

                            strMailHdr = "Credit Verified";
                            //strMailCc = "masum.billah@doreen.com";
                            strMailCc = "rahman.tushar@doreen.com;ismail.saber@doreen.com";
                        }
                    }

                    if (dtSalesOrdDet[0].SO_Det_Flag == "H" && dtSalesOrdDet[0].SO_Det_Status == "3")
                    {
                        if (dtSalesOrdDet[0].SO_Det_T_In == 0)
                        {
                            taSalesOrdHdr.UpdateSalesHdrStat("P", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), 1, "1", hfSoHdrRef.Value.ToString());

                            taSalesOrdDet.UpdateSalesDetApp(ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, ORD_QTY + ORD_FREE_BAG, "0", ORD_FREE_BAG.ToString(),
                                (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)), (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)),
                                1, "1", "P", hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value));

                            var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblSoHdrRefNo.Text.Trim());
                            var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                            taComm.InsertTranCom(lblSoHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "APP",
                                "(Credit Approved [Qty:" + ORD_QTY.ToString("N2") + ", Amount: " + ORD_NET_AMOUNT.ToString("N2") + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "]) " + txtRemarks.Text.Trim(),
                                "", "1", "", "", "", "");

                            strMailHdr = "Credit Approved";
                            //strMailCc = "masum.billah@doreen.com";
                            strMailCc = "rahman.tushar@doreen.com;ismail.saber@doreen.com";
                        }
                        else
                        {
                            taSalesOrdHdr.UpdateSalesHdrStat("P", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), 1, "1", hfSoHdrRef.Value.ToString());

                            taSalesOrdDet.UpdateSalesDetApp(ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, ORD_QTY + ORD_FREE_BAG, "0", ORD_FREE_BAG.ToString(),
                                (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)), (ORD_AMOUNT + (Convert.ToDecimal(ORD_QTY) * ORD_TRANS_RATE)),
                                1, "1", "P", hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value));

                            var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblSoHdrRefNo.Text.Trim());
                            var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                            taComm.InsertTranCom(lblSoHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "APP",
                                "(Credit Agreed [Qty:" + ORD_QTY.ToString("N2") + ", Amount: " + ORD_NET_AMOUNT.ToString("N2") + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "]) " + txtRemarks.Text.Trim(),
                                "", "1", "", "", "", "");

                            strMailHdr = "Credit Agreed";
                            //strMailCc = "masum.billah@doreen.com";
                            strMailCc = "rahman.tushar@doreen.com;ismail.saber@doreen.com";
                        }
                    }

                    myTran.Commit();
                    //tblMsg.Rows[0].Cells[0].InnerText = "Approved Successfully.";
                    //tblMsg.Rows[1].Cells[0].InnerText = "";
                    //ModalPopupExtenderMsg.Show();

                    try
                    {
                        //Send Mail
                        var mBody = MsgBody(hfParAdrRef.Value.ToString(), strMailHdr + " by " + empName + " : " + empDesig + " , " + empDept, txtAppOrdQty.Text.Trim(), ORD_NET_AMOUNT.ToString("N2"), custCrLimitBal.ToString("N2"));

                        DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail(strmailTo, strMailCc, "", strMailHdr, mBody);
                    }
                    catch (Exception ex) { }

                    var taOrd = new VIEW_PEND_SALES_ORDTableAdapter();
                    var dtOrd = new DataTable();
                    //if (empRef == "000568" || empRef == "000018" || empRef == "000070" || empRef == "000011")//----------Nazmul Sir,Saiful,Lutfor                    
                    if (empRef == "000072" || empRef == "000275" || empRef == "000568" || empRef == "000011")//----------Azizul,Ismail, Nazmul Sir
                        //if (empRef == "000316" || empRef == "000072" || empRef == "000011")//----------Masum Billah, Azizul Islam
                        dtOrd = taOrd.GetPendSoAcc();
                    //if (empRef == "000568" || empRef == "000011")//--------Nazmul Sir
                    if (empRef == "000856" || empRef == "000011")//---------Ali Haider Sir
                        dtOrd = taOrd.GetPendSoAccHead();
                    if (empRef == "000410" || empRef == "000011")//---------Zakir Sir
                        dtOrd = taOrd.GetPendSoAccHeadSalesWing1();
                    if (empRef == "000643" || empRef == "000011")//---------Rabbi Sir
                        dtOrd = taOrd.GetPendSoAccHeadSalesWing2();
                    gvPendSoHdr.DataSource = dtOrd;
                    gvPendSoHdr.DataBind();

                    var taApprOrd = new VIEW_SALES_ORDERTableAdapter();
                    var dtApprOrd = new DataTable();
                    dtApprOrd = taApprOrd.GetDataByApprOrd();
                    gvApprDoDet.DataSource = dtApprOrd;
                    gvApprDoDet.DataBind();
                }
                else
                {
                    myTran.Rollback();
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Order Details.";
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

        protected void imgBtnInfo_Click(object sender, ImageClickEventArgs e)
        {
            var taTranCom = new tbl_Tran_ComTableAdapter();

            GridViewRow row = ((GridViewRow)((ImageButton)sender).NamingContainer);
            var lblSoHdrRefNo = (Label)(row.FindControl("lblSoHdrRefNo"));

            var msg = "";

            var dtTranCom = taTranCom.GetDataByRefNo(lblSoHdrRefNo.Text.Trim());
            foreach (dsProcTran.tbl_Tran_ComRow dr in dtTranCom.Rows)
            {
                msg = msg + "\n>>" + dr.Com_App_Date.ToString() + ", " + dr.Com_App_Name + " (" + dr.Com_App_Desig + "), " + dr.Com_Gen_Com ;
            }
            txtMsgInfo.Text = msg.ToString();
            ModalPopupExtenderMsgInfo.Show();
        }

        protected void lnkBtnCrRpt_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            var hfCustRef = (HiddenField)(row.FindControl("hfCustRef"));

            agingReportInfo(hfCustRef.Value.ToString());
            //var url = @"~/Module/Accounts/Forms/frmShowAccReport.aspx";
            var url = "frmShowSalesReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void agingReportInfo(string custRefNo)
        {
            var taTranCom = new tbl_Tran_ComTableAdapter();

            if (custRefNo.Trim().Length <= 0) return;

            if (custRefNo.Length > 0)
            {
                int result;
                if (int.TryParse(custRefNo, out result))
                {
                    var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                    var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRefNo));
                    if (dtPartyAdr.Rows.Count > 0) custRefNo = dtPartyAdr[0].Par_Adr_Ref.ToString();
                }
                else
                    return;
            }
            else
                return;

            if (custRefNo == "")
            {
                rptSelcFormula = "{View_Credit_Report.Sales_Chln_Date} in Date('01/01/2014') to Date ('" + DateTime.Now.ToString("dd/MM/yyyy") + "')";
            }
            else
            {
                rptSelcFormula = "{View_Credit_Report.Sales_Chln_Date} in Date('01/01/2014') to Date ('" + DateTime.Now.ToString("dd/MM/yyyy") + "') " +
                    "and {View_Credit_Report.Sales_Par_Adr_Ref}='" + custRefNo + "'";
            }

            rptFile = "~/Module/Accounts/Reports/rptCreditSum.rpt";

            Session["RptDateFrom"] = "01/01/2014";
            Session["RptDateTo"] = DateTime.Now.ToString("dd/MM/yyyy");
            Session["RptFilePath"] = rptFile;
            Session["RptFormula"] = rptSelcFormula;
        }

        protected void lnkBtnCrSecurity_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            var hfCustRef = (HiddenField)(row.FindControl("hfCustRef"));

            var url = "frmSalesCrSecurity.aspx?DLR=" + hfCustRef.Value.ToString();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        private string MsgBody(string partyRef, string mailHdr, string ordQty, string ordAmt, string crLimitBal)
        {
            string str = "";
            try
            {
                var empref = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                var partyName = ""; double crLimit = 0;
                var taParAdr = new tblSalesPartyAdrTableAdapter();
                var dtParAdr = taParAdr.GetDataByPartyAdrRef(Convert.ToInt32(partyRef));
                if (dtParAdr.Rows.Count > 0)
                {
                    partyName = dtParAdr[0].Par_Adr_Name.ToString();
                    crLimit = Convert.ToDouble(dtParAdr[0].Par_Adr_Cr_Limit);
                }

                str = "--------------------------(" + mailHdr.ToString() + ")-------------------------";
                str += "\n";
                str += "\n";
                str += "\n\n------------------------ DETAIL INFORMATION -------------------------------";
                str += "\nParty Name                 : " + partyName.ToString();
                str += "\nCredit Limit               : " + crLimit.ToString("N2");
                str += "\nOrder Quantity             : " + ordQty.ToString();
                str += "\nOrder Amount               : " + ordAmt.ToString();                
                str += "\nAvailable Limit            : " + crLimitBal.ToString();
                str += "\n";
                str += "\n\n\n\nTo view detail just login the link bellow:\n\n";
                str += "http://192.168.0.10/DRNERP/";
                str += "\nor\n";
                str += "http://182.160.110.139/DRNERP/";
                str += "\n\n\n\n";
                str += "This is auto generated mail from DRN-ERP.";
                return str;
            }
            catch (Exception ex) { return str; }
        }

        protected void imgBtnDismiss_Click(object sender, ImageClickEventArgs e)
        {
            var taSalesOrdHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesOrdDet = new tblSalesOrderDetTableAdapter();
            var taComm = new tbl_Tran_ComTableAdapter();
            var taEmp = new View_Emp_BascTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesOrdHdr.Connection);

            try
            {
                GridViewRow row = ((GridViewRow)((ImageButton)sender).NamingContainer);
                var hfParAdrRef = (HiddenField)(row.FindControl("hfParAdrRef"));
                var hfSoHdrRef = (HiddenField)(row.FindControl("hfSoHdrRef"));
                var lblSoHdrRefNo = (Label)(row.FindControl("lblSoHdrRefNo"));
                var hfSoDetLno = (HiddenField)(row.FindControl("hfSoDetLno"));
                var txtAppOrdQty = (TextBox)(row.FindControl("txtAppOrdQty"));
                var lblOrdRate = (Label)(row.FindControl("lblOrdRate"));
                var txtAppFreeQty = (TextBox)(row.FindControl("txtAppFreeQty"));
                var lblOrdTransRat = (Label)(row.FindControl("lblOrdTransRat"));
                var txtOrdNetAmt = (TextBox)(row.FindControl("txtOrdNetAmt"));

                #region Get Employee Details
                string empId = "", empName = "", empDesig = "", empDept = "";
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                {
                    empId = dtEmp[0].EmpId.ToString();
                    empName = dtEmp[0].EmpName.ToString();
                    empDesig = dtEmp[0].DesigName.ToString();
                    empDept = dtEmp[0].DeptName.ToString();
                }
                #endregion

                #region Get Credit Balance
                var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(hfParAdrRef.Value));

                var custRef = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Ref.ToString() : "";
                var custName = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Name.ToString() : "";
                var custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;

                var taSalesOrder = new View_Sales_Ord_NewTableAdapter();
                var dtSoBalAmtSum = taSalesOrder.GetSoBalAmt(custRef.ToString());
                var soBalAmtSum = dtSoBalAmtSum == null ? 0 : Convert.ToDouble(dtSoBalAmtSum);
                var taSalesOrderDo = new View_Delivery_Ord_NewTableAdapter();
                var dtDoBalAmtSum = taSalesOrderDo.GetDoBalAmt(custRef.ToString());
                var doBalAmtSum = dtDoBalAmtSum == null ? 0 : Convert.ToDouble(dtDoBalAmtSum);

                var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                var custCrOutsatnd = soBalAmtSum + doBalAmtSum + (drAmt - crAmt);
                var custCrLimitBal = (Convert.ToDouble(crLimit) - (soBalAmtSum + doBalAmtSum + (drAmt - crAmt)));
                #endregion

                var strmailTo = "ali.haider@doreen.com";
                var strMailCc = "";

                taSalesOrdHdr.AttachTransaction(myTran);
                taSalesOrdDet.AttachTransaction(myTran);
                taComm.AttachTransaction(myTran);

                var dtSalesOrdDet = taSalesOrdDet.GetDataByDetLno(hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value.ToString()));
                if (dtSalesOrdDet.Rows.Count > 0)
                {
                    if (dtSalesOrdDet[0].SO_Det_Flag == "H")
                    {
                        //if (dtSalesOrdDet[0].SO_Det_Status == "2")
                        //{
                        //    taSalesOrdHdr.UpdateSalesHdrStat("H", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                        //        DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                        //        0, "3", hfSoHdrRef.Value.ToString());

                        //    taSalesOrdDet.UpdateSalesDetStat(0, "3", "H", hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value));

                        //    var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblSoHdrRefNo.Text.Trim());
                        //    var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                        //    taComm.InsertTranCom(lblSoHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "INIT",
                        //        "(Credit Approval Rejected [Qty:" + txtAppOrdQty.Text.Trim() + ", Amount: " + txtOrdNetAmt.Text.Trim() + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "])", "", "1", "", "", "", "");

                        //    strMailCc = "rahman.tushar@doreen.com";
                        //}
                        //else
                        //{
                            taSalesOrdHdr.UpdateSalesHdrStat("H", DateTime.Now.ToString(), Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                0, "1", hfSoHdrRef.Value.ToString());

                            taSalesOrdDet.UpdateSalesDetStat(0, "1", "H", hfSoHdrRef.Value.ToString(), Convert.ToInt16(hfSoDetLno.Value));

                            var dtMaxComSeqNo = taComm.GetMaxComSeqNo(lblSoHdrRefNo.Text.Trim());
                            var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                            taComm.InsertTranCom(lblSoHdrRefNo.Text.Trim(), nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "INIT",
                                "(Credit Approval Dismissed [Qty:" + txtAppOrdQty.Text.Trim() + ", Amount: " + txtOrdNetAmt.Text.Trim() + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "])", "", "1", "", "", "", "");

                            strMailCc = "rahman.tushar@doreen.com";
                        //}
                    }

                    myTran.Commit();
                    //tblMsg.Rows[0].Cells[0].InnerText = "Rejected Successfully.";
                    //tblMsg.Rows[1].Cells[0].InnerText = "";
                    //ModalPopupExtenderMsg.Show();

                    try
                    {
                        //Send Mail
                        //var mBody = MsgBody(hfParAdrRef.Value.ToString(), "Credit Approval Rejected by " + empName + " : " + empDesig + " , " + empDept, txtAppOrdQty.Text.Trim(), txtOrdNetAmt.Text.Trim(), custCrLimitBal.ToString("N2"));

                        //DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail(strmailTo, strMailCc, "", "Credit Approval Rejected", mBody);
                    }
                    catch (Exception ex) { }

                    var taOrd = new VIEW_PEND_SALES_ORDTableAdapter();
                    var dtOrd = new DataTable();
                    //if (empRef == "000568" || empRef == "000018" || empRef == "000070" || empRef == "000011")//----------Nazmul Sir,Saiful,Lutfor                    
                    if (empRef == "000072" || empRef == "000275" || empRef == "000568" || empRef == "000011")//----------Azizul,Ismail, Nazmul Sir
                        //if (empRef == "000316" || empRef == "000072" || empRef == "000011")//----------Masum Billah, Azizul Islam
                        dtOrd = taOrd.GetPendSoAcc();
                    //if (empRef == "000568" || empRef == "000011")//--------Nazmul Sir
                    if (empRef == "000856" || empRef == "000011")//---------Ali Haider Sir
                        dtOrd = taOrd.GetPendSoAccHead();
                    if (empRef == "000410" || empRef == "000011")//---------Zakir Sir
                        dtOrd = taOrd.GetPendSoAccHeadSalesWing1();
                    if (empRef == "000643" || empRef == "000011")//---------Rabbi Sir
                        dtOrd = taOrd.GetPendSoAccHeadSalesWing2();
                    gvPendSoHdr.DataSource = dtOrd;
                    gvPendSoHdr.DataBind();

                    var taApprOrd = new VIEW_SALES_ORDERTableAdapter();
                    var dtApprOrd = new DataTable();
                    dtApprOrd = taApprOrd.GetDataByApprOrd();
                    gvApprDoDet.DataSource = dtApprOrd;
                    gvApprDoDet.DataBind();
                }
                else
                {
                    myTran.Rollback();
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Order Details.";
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
    }
}