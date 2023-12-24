using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmOpnBalEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtEntDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var dtNewMaxAccRef = taAcc.GetMaxRefNo("OPN", Convert.ToDateTime(txtEntDate.Text.Trim()).Year);
            var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
            var nextNewAccRefNo = "OPN" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
            txtTranRefNo.Text = nextNewAccRefNo.ToString();
            
            txtNarration.Text = "Opening Balance Entry";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                var coaCode = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    coaCode = word;
                    break;
                }

                if (coaCode.Length > 0)
                {
                    var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtCoa = taCoa.GetDataByCoaCode(coaCode.ToString());
                    if (dtCoa.Rows.Count > 0)
                    {
                        var taFaTe = new View_Acc_Tran_DetTableAdapter();

                        var dtTotCr = taFaTe.GetAccTotalCr(coaCode.ToString());
                        var totCr = dtTotCr == null ? 0 : Convert.ToDecimal(dtTotCr);
                        var dtTotDr = taFaTe.GetAccTotalDr(coaCode.ToString());
                        var totDr = dtTotDr == null ? 0 : Convert.ToDecimal(dtTotDr);
                        var closingBalance = (totDr - totCr);
                        lblTotDr.Text = totDr.ToString("N2");
                        lblTotCr.Text = totCr.ToString("N2");
                        lblCloseBal.Text = closingBalance.ToString("N2");

                        txtSearch.Enabled = false;
                        btnSearch.Enabled = false;
                        btnClearSrch.Visible = true;
                        pnlDet.Enabled = true;
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "GL data not found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "GL data not found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                txtSearch.Enabled = true;
                btnSearch.Enabled = true;
                btnClearSrch.Visible = false;
                lblTotDr.Text = "0.00";
                lblTotCr.Text = "0.00";
                lblCloseBal.Text = "0.00";
                txtEntDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtNarration.Text = "Opening Balance Entry";
                txtTranAmt.Text = "";
                pnlDet.Enabled = false;

                var taAcc = new tbl_Acc_Fa_TeTableAdapter();
                var dtNewMaxAccRef = taAcc.GetMaxRefNo("OPN", Convert.ToDateTime(txtEntDate.Text.Trim()).Year);
                var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
                var nextNewAccRefNo = "OPN" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
                txtTranRefNo.Text = nextNewAccRefNo.ToString();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var taCrReal = new tblSalesCreditRealizeTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAcc.Connection);

            try
            {
                if (txtNarration.Text.Trim().Length <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter narration first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                #region Validate and Get Coa Data
                var coaCode = "";
                var coaName = "";
                var coaType = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    coaCode = word;
                    break;
                }

                if (coaCode.Length > 0)
                {
                    var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtCoa = taCoa.GetDataByCoaCode(coaCode);
                    if (dtCoa.Rows.Count > 0)
                    {
                        coaCode = dtCoa[0].Gl_Coa_Code.ToString();
                        coaName = dtCoa[0].Gl_Coa_Name.ToString();
                        coaType = dtCoa[0].Gl_Coa_Type.ToString();
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Account Code.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Account Code.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                taAcc.AttachTransaction(myTran);
                taCrReal.AttachTransaction(myTran);

                #region Opening Banalce Entry
                var dtMaxAccRef = taAcc.GetMaxRefNo("OPN", Convert.ToDateTime(txtEntDate.Text.Trim()).Year);
                var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                var nextAccRefNo = "OPN" + (Convert.ToDateTime(txtEntDate.Text).Month.ToString("00") + Convert.ToDateTime(txtEntDate.Text).ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                taAcc.InsertAccData(coaCode.ToString(), (Convert.ToDateTime(txtEntDate.Text).Month.ToString("00") + "/" + Convert.ToDateTime(txtEntDate.Text).Year.ToString()).ToString(),
                    nextAccRefNo.ToString(), 1, 1, txtNarration.Text.Trim(), ddlTrnType.SelectedValue.ToString(), Convert.ToDecimal(txtTranAmt.Text.Trim()),
                    "", "0", "BDT", 1, Convert.ToDecimal(txtTranAmt.Text.Trim()),
                    "", "", "", "", "", "", "", "", "", "", "", (Convert.ToDateTime(txtEntDate.Text).Month.ToString("00") + "/" + Convert.ToDateTime(txtEntDate.Text).Year.ToString()), DateTime.Now,
                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtEntDate.Text.Trim()), coaName.ToString(),
                    Convert.ToDateTime(txtEntDate.Text.Trim()), "ADM", coaType, "",
                    DateTime.Now, "OPN", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(txtTranAmt.Text.Trim()), "",
                    "", "", "N", 1, 0, "", "", "", "J", 0, "1", "OPN");
                #endregion

                if (coaType == "P")
                {
                    #region Validate and Get Coa Data
                    var custRef = "";
                    var custName = "";
                    var taCust = new tblSalesPartyAdrTableAdapter();
                    var dtCust = taCust.GetDataByPartyAccRef(coaCode.ToString());
                    if (dtCust.Rows.Count > 0)
                    {
                        custRef = dtCust[0].Par_Adr_Ref.ToString();
                        custName = dtCust[0].Par_Adr_Name.ToString();
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer Code.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    #endregion

                    if (ddlTrnType.SelectedValue=="C")
                    {
                        #region Credit Realization Entry
                        var rcvAmt = Convert.ToDecimal(txtTranAmt.Text.Trim());
                        var dtCrReal = taCrReal.GetPendChlnByCustRef(custRef.ToString());
                        foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                        {
                            if (rcvAmt > dr.Sales_Chln_Due_Amt)
                            {
                                rcvAmt = rcvAmt - dr.Sales_Chln_Due_Amt;
                                taCrReal.UpdateCreditRealizeAmt(0, dr.Sales_Chln_Pay_Rcv_Ref + "OPN.Ref: " + nextAccRefNo.ToString() + " Amt: " + dr.Sales_Chln_Due_Amt.ToString("N2") + ", ", dr.Sales_Chln_Ref);                                
                            }
                            else
                            {
                                taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - rcvAmt, dr.Sales_Chln_Pay_Rcv_Ref + "OPN.Ref: " + nextAccRefNo.ToString() + " Amt: " + rcvAmt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                rcvAmt = 0;
                                break;
                            }
                        }
                        #endregion
                    }

                    if (ddlTrnType.SelectedValue == "D")
                    {
                        #region Credit Realization Entry
                        var TranDrAmt = Convert.ToDecimal(txtTranAmt.Text.Trim());

                        var dtCrSum = taAcc.GetTotCrAmt(coaCode.ToString());
                        var crAmt = dtCrSum == null ? 0 : Convert.ToDecimal(dtCrSum);
                        var dtDrSum = taAcc.GetTotDrAmt(coaCode.ToString());
                        var drAmt = dtDrSum == null ? 0 : Convert.ToDecimal(dtDrSum);
                        var CrBal = (crAmt - drAmt);

                        if (CrBal > 0)
                        {
                            if ((CrBal - TranDrAmt) < 0)
                                taCrReal.InsertCreditRealize(nextAccRefNo.ToString(), nextAccRefNo.ToString(), Convert.ToDateTime(txtEntDate.Text.Trim()),
                                    Convert.ToDecimal(TranDrAmt - CrBal), Convert.ToDecimal((TranDrAmt - CrBal)), custRef.ToString(), "1", "", "", "", "", "");
                        }
                        else
                        {
                            taCrReal.InsertCreditRealize(nextAccRefNo.ToString(), nextAccRefNo.ToString(), Convert.ToDateTime(txtEntDate.Text.Trim()),
                                    Convert.ToDecimal(TranDrAmt), Convert.ToDecimal(TranDrAmt), custRef.ToString(), "1", "", "", "", "", "");
                        }
                        #endregion
                    }
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Opening balance entry successful.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                txtSearch.Text = "";
                txtSearch.Enabled = true;
                btnSearch.Enabled = true;
                btnClearSrch.Visible = false;
                lblTotDr.Text = "0.00";
                lblTotCr.Text = "0.00";
                lblCloseBal.Text = "0.00";
                txtEntDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtNarration.Text = "Opening Balance Entry";
                txtTranAmt.Text = "";
                pnlDet.Enabled = false;

                var dtNewMaxAccRef = taAcc.GetMaxRefNo("OPN", Convert.ToDateTime(txtEntDate.Text.Trim()).Year);
                var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
                var nextNewAccRefNo = "OPN" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
                txtTranRefNo.Text = nextNewAccRefNo.ToString();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnUpdtCrRealize_Click(object sender, EventArgs e)
        {
            var taCrReal = new tblSalesCreditRealizeTableAdapter();
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taCrReal.Connection);
            try
            {
                taCrReal.AttachTransaction(myTran);
                taAcc.AttachTransaction(myTran);  

                var view1 = new DataTable1TableAdapter();
                var dtCrReal = view1.GetData();
                foreach (dsSalesTran.DataTable1Row dr in dtCrReal.Rows)
                {
                    taCrReal.InsertCreditRealize(dr.chlnRef.ToString(), dr.Trn_Dc_No.ToString(), Convert.ToDateTime(dr.Trn_Entry_Date), Convert.ToDecimal(dr.Sales_Chln_Amt),
                        Convert.ToDecimal(dr.Sales_Chln_Amt), dr.Par_Adr_Acc_Code.ToString(), dr.Sales_Chln_Status.ToString(), dr.Sales_Chln_Flag.ToString(), "", "", "", "");
                }

                var dtCustPay=taAcc.ExistingCustPaymentRealized();
                foreach (dsAccTran.tbl_Acc_Fa_TeRow drAcc in dtCustPay.Rows)
                {
                    var rcvAmt = drAcc.Trn_Amount;
                    var dtCrRealize = taCrReal.GetPendChlnByCustRef(drAcc.Trn_Ac_Code.ToString());
                    foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrRealize.Rows)
                    {
                        if (rcvAmt > dr.Sales_Chln_Due_Amt)
                        {
                            rcvAmt = rcvAmt - dr.Sales_Chln_Due_Amt;
                            taCrReal.UpdateCreditRealizeAmt(0, dr.Sales_Chln_Pay_Rcv_Ref + "Opening Balance Update Amt: " + dr.Sales_Chln_Due_Amt.ToString("N2") + ", ", dr.Sales_Chln_Ref);                            
                        }
                        else
                        {
                            taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - rcvAmt, dr.Sales_Chln_Pay_Rcv_Ref + "Opening Balance Update Amt: " + (dr.Sales_Chln_Due_Amt - rcvAmt).ToString("N2") + ", ", dr.Sales_Chln_Ref);
                            rcvAmt = 0;
                        }
                    }
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Updated.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnUpdateExisting_Click(object sender, EventArgs e)
        {
            var taPartyAdr = new tblSalesPartyAdrTableAdapter();
            var taCrLimit = new tblSalesPartyCrLimitTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taPartyAdr.Connection);

            try
            {
                taPartyAdr.AttachTransaction(myTran);
                taCrLimit.AttachTransaction(myTran);

                var dtPartyAdr = taPartyAdr.GetData();
                foreach (dsSalesMas.tblSalesPartyAdrRow dr in dtPartyAdr.Rows)
                {
                    taCrLimit.InsertCreditLimit(dr.Par_Adr_Ref.ToString(), 1, 0, dr.Par_Adr_Cr_Limit,
                    DateTime.Now, Session["sessionUserName"] == null ? "0" : Session["sessionUserName"].ToString(), "", "", "", "", "",
                    DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Credit Limit Updated Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnUpdtOpnBal_Click(object sender, EventArgs e)
        {
            var taCloseBal = new Customer_Opening_BalanceTableAdapter();
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAcc.Connection);

            try
            {                
                taAcc.AttachTransaction(myTran);
                taCloseBal.AttachTransaction(myTran);

                var iCount = 1;
                var dtCloseBal = taCloseBal.GetData();
                foreach (dsAccTran.Customer_Opening_BalanceRow dr in dtCloseBal.Rows)
                {
                    var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtCoa = taCoa.GetDataByCoaCode(dr.cust_Acc_code);
                    if (dtCoa.Rows.Count > 0)
                    {
                        var dtMaxAccRef = taAcc.GetMaxRefNo("OPN", Convert.ToDateTime(txtEntDate.Text.Trim()).Year);
                        var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                        var nextAccRefNo = "OPN" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                        taAcc.InsertAccData(dr.cust_Acc_code.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                            nextAccRefNo.ToString(), 1, 1, "Opening Balance As On " + DateTime.Now.ToString("dd/MM/yyyy"),
                            dr.Debit_Credit_Type, Convert.ToDecimal(dr._Closing_Balance_As_on_31_05_15.ToString()),
                            "", "0", "BDT", 1, Convert.ToDecimal(dr._Closing_Balance_As_on_31_05_15.ToString()),
                            "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, dtCoa[0].Gl_Coa_Name.ToString(), DateTime.Now,
                            "ADM", dtCoa[0].Gl_Coa_Type.ToString(), "", DateTime.Now, "OPN", "L", 0, "BDT", 1, "BDT", 1, 
                            Convert.ToDecimal(dr._Closing_Balance_As_on_31_05_15.ToString()), "",
                            "", "", "N", 1, 0, "", "", "", "J", 0, "1", "OPN");
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Account Code.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    iCount++;
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Opening balance entry successful. total (" + iCount.ToString() + ")";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void txtEntDate_TextChanged(object sender, EventArgs e)
        {
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var dtNewMaxAccRef = taAcc.GetMaxRefNo("OPN", Convert.ToDateTime(txtEntDate.Text.Trim()).Year);
            var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
            var nextNewAccRefNo = "OPN" + (Convert.ToDateTime(txtEntDate.Text.Trim()).Month.ToString("00") + Convert.ToDateTime(txtEntDate.Text.Trim()).ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
            txtTranRefNo.Text = nextNewAccRefNo.ToString();
        }
    }
}