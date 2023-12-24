using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
//using DRN_WEB_ERP.bd.com.mobireach.user;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmPaymentRcv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtRcvDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var dtNewMaxAccRef = taAcc.GetMaxRefNo("RJV", Convert.ToDateTime(txtRcvDate.Text.Trim()).Year);
            var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
            var nextNewAccRefNo = "RJV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
            txtRcvRefNo.Text = nextNewAccRefNo.ToString();

            var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
            var dtFaTeTrnRef = taFaTe.GetDataByTopTranByTranType("RJV");
            gvLastPayDet.DataSource = dtFaTeTrnRef;
            gvLastPayDet.DataBind();

            var dtFaTeDate = taFaTe.GetDataByDateRangeTranTypeDrCr("RJV", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"), "C");
            gvTodayPayDet.DataSource = dtFaTeDate;
            gvTodayPayDet.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchCustomerData();
        }

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                txtSearch.Enabled = true;
                btnSearch.Enabled = true;
                btnClearSrch.Visible = false;
                lblCrLimit.Text = "0.00";
                lblCrLimitOutStand.Text = "0.00";
                txtCrLimitBal.Text = "0.00";
                txtRcvDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlRcvMode.SelectedIndex = 0;
                txtDocRef.Text = "";
                txtNarration.Text = "";
                txtDrAcc.Text = "";
                txtRcvAmt.Text = "";
                //hfEditStatus.Value = "N";
                //hfRefNo.Value = "0";
                pnlDet.Enabled = false;                

                var taAcc = new tbl_Acc_Fa_TeTableAdapter();
                var dtNewMaxAccRef = taAcc.GetMaxRefNo("RJV", Convert.ToDateTime(txtRcvDate.Text.Trim()).Year);
                var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
                var nextNewAccRefNo = "RJV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
                txtRcvRefNo.Text = nextNewAccRefNo.ToString();

                var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                var dtFaTeTrnRef = taFaTe.GetDataByTopTranByTranType("RJV");
                gvLastPayDet.DataSource = dtFaTeTrnRef;
                gvLastPayDet.DataBind();

                var dtFaTeDate = taFaTe.GetDataByDateRangeTranTypeDrCr("RJV", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"), "C");
                gvTodayPayDet.DataSource = dtFaTeDate;
                gvTodayPayDet.DataBind();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        private void SearchCustomerData()
        {
            var taPartyAdr = new tblSalesPartyAdrTableAdapter();

            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                var custRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custRef = word;
                    break;
                }

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
                        {
                            txtSearch.Enabled = false;
                            btnSearch.Enabled = false;
                            btnClearSrch.Visible = true;

                            var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;
                            var custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                            var taSalesOrder = new View_Sales_Ord_NewTableAdapter();
                            var dtSoBalAmtSum = taSalesOrder.GetSoBalAmt(custRef.ToString());
                            var soBalAmtSum = dtSoBalAmtSum == null ? 0 : Convert.ToDecimal(dtSoBalAmtSum);
                            var taSalesOrderDo = new View_Delivery_Ord_NewTableAdapter();
                            var dtDoBalAmtSum = taSalesOrderDo.GetDoBalAmt(custRef.ToString());
                            var doBalAmtSum = dtDoBalAmtSum == null ? 0 : Convert.ToDecimal(dtDoBalAmtSum);

                            var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                            var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                            var crAmt = dtCrSum == null ? 0 : Convert.ToDecimal(dtCrSum);
                            var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                            var drAmt = dtDrSum == null ? 0 : Convert.ToDecimal(dtDrSum);

                            lblCrLimit.Text = crLimit.ToString("N2");
                            lblCrLimitOutStand.Text = (soBalAmtSum + doBalAmtSum + (drAmt - crAmt)).ToString("N2");
                            txtCrLimitBal.Text = (Convert.ToDecimal(crLimit) - (soBalAmtSum + doBalAmtSum + (drAmt - crAmt))).ToString("N2");          

                            pnlDet.Enabled = true;
                        }
                        else
                        {
                            ////Challan Header Ref
                            //var taInvHdrData = new tbl_InTr_Trn_HdrTableAdapter();
                            //var dtGetMaxChlnRef = taInvHdrData.GetMaxChlnRef();
                            //var nextChlnRef = dtGetMaxChlnRef == null ? "000001" : (Convert.ToInt32(dtGetMaxChlnRef) + 1).ToString("000000");
                            //var nextChlnRefNo = "ECIL-CLN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextChlnRef).ToString("000000");
                            //txtChallanNo.Text = nextChlnRefNo;
                            //txtChallanDate.Text = DateTime.Now.ToString();

                            txtSearch.Enabled = true;
                            btnSearch.Enabled = true;
                            btnClearSrch.Visible = false;
                            lblCrLimit.Text = "0.00";
                            lblCrLimitOutStand.Text = "0.00";
                            txtCrLimitBal.Text = "0.00";
                            txtRcvDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            ddlRcvMode.SelectedIndex = 0;
                            txtNarration.Text = "";
                            txtDrAcc.Text = "";
                            txtRcvAmt.Text = "";
                            pnlDet.Enabled = false;

                            tblMsg.Rows[0].Cells[0].InnerText = "Customer data not found.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                        }
                    }
                }
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
                #region Validate and Get Customer Data
                var custRef = "";
                var custName = "";
                var custAcc = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custRef = word;
                    break;
                }

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
                        {
                            custRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                            custName = dtPartyAdr[0].Par_Adr_Name.ToString();
                            custAcc = dtPartyAdr[0].Par_Adr_Acc_Code.ToString();
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                #region Validate and Get Debit Account Code
                var drAcc = "";
                var drAccName = "";
                var drAccType = "";
                var srchAccWords = txtDrAcc.Text.Trim().Split(':');
                foreach (string word in srchAccWords)
                {
                    drAcc = word;
                    break;
                }

                if (drAcc.Length > 0)
                {
                    var taGl = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtGl = taGl.GetDataByCoaCode(drAcc);
                    if (dtGl.Rows.Count > 0)
                    {
                        drAcc = dtGl[0].Gl_Coa_Code.ToString();
                        drAccName = dtGl[0].Gl_Coa_Name.ToString();
                        drAccType = dtGl[0].Gl_Coa_Type.ToString();
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Debit Account.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Debit Account.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                taAcc.AttachTransaction(myTran);
                taCrReal.AttachTransaction(myTran);

                var jvType = "";
                if (ddlRcvMode.SelectedItem.ToString() == "Cash")
                    jvType = "RJV";
                else
                    jvType = "RJV";

                var nextAccRefNo = "";
                //if (hfEditStatus.Value == "N")
                //{
                #region Insert Accounts Voucher Entry

                var dtMaxAccRef = taAcc.GetMaxRefNo(jvType, Convert.ToDateTime(txtRcvDate.Text.Trim()).Year);
                var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                nextAccRefNo = jvType + Convert.ToDateTime(txtRcvDate.Text.Trim()).Month.ToString("00") + Convert.ToDateTime(txtRcvDate.Text.Trim()).ToString("yy") + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                //Credit-Customer Account
                taAcc.InsertAccData(custAcc.ToString(), (Convert.ToDateTime(txtRcvDate.Text.Trim()).Month.ToString("00") + "/" + Convert.ToDateTime(txtRcvDate.Text.Trim()).Year.ToString()).ToString(),
                    nextAccRefNo.ToString(), 1, 1, drAccName.ToString() + "! " + txtNarration.Text.Trim(), "C", Convert.ToDecimal(txtRcvAmt.Text.Trim()),
                    ddlRcvMode.SelectedItem.ToString(), txtDocRef.Text.Trim(), "BDT", 1, Convert.ToDecimal(txtRcvAmt.Text.Trim()),
                    "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtRcvDate.Text.Trim()), custName.ToString(),
                    Convert.ToDateTime(txtRcvDate.Text.Trim()), "ADM", "P", "",
                    DateTime.Now, jvType, "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(txtRcvAmt.Text.Trim()), "",
                    "", "", "N", 1, 0, "", "", "", "J", 0, "1", jvType);

                //Debit-Given Account
                taAcc.InsertAccData(drAcc.ToString(), (Convert.ToDateTime(txtRcvDate.Text.Trim()).Month.ToString("00") + "/" + Convert.ToDateTime(txtRcvDate.Text.Trim()).Year.ToString()).ToString(),
                    nextAccRefNo.ToString(), 2, 1, custName.ToString(), "D", Convert.ToDecimal(txtRcvAmt.Text.Trim()),
                    ddlRcvMode.SelectedItem.ToString(), txtDocRef.Text.Trim(), "BDT", 1, Convert.ToDecimal(txtRcvAmt.Text.Trim()),
                    "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtRcvDate.Text.Trim()), drAccName.ToString(),
                    Convert.ToDateTime(txtRcvDate.Text.Trim()), "ADM", drAccType, "",
                    DateTime.Now, jvType, "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(txtRcvAmt.Text.Trim()), "",
                    "", "", "N", 1, 0, "", "", "", "J", 0, "1", jvType);

                #endregion

                #region Credit Realization Entry
                var rcvAmt = Convert.ToDecimal(txtRcvAmt.Text.Trim());
                var dtCrReal = taCrReal.GetPendChlnByCustRef(custRef.ToString());
                foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                {
                    //if (rcvAmt > 0)
                    //{
                    if (rcvAmt > dr.Sales_Chln_Due_Amt)
                    {
                        rcvAmt = rcvAmt - dr.Sales_Chln_Due_Amt;
                        taCrReal.UpdateCreditRealizeAmt(0, dr.Sales_Chln_Pay_Rcv_Ref + "RCV.Ref: " + nextAccRefNo + " Amt: " + dr.Sales_Chln_Due_Amt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                    }
                    else
                    {
                        taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - rcvAmt, dr.Sales_Chln_Pay_Rcv_Ref + "RCV.Ref: " + nextAccRefNo + " Amt: " + rcvAmt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                        rcvAmt = 0;
                        break;
                    }
                    //}
                    //else
                    //    break;
                }
                #endregion
                //}
                //else
                //{
                //    nextAccRefNo = hfRefNo.Value.ToString();

                #region Insert Accounts Voucher Entry
                //Credit-Customer Account
                //taAcc.UpdateAccData((Convert.ToDateTime(txtRcvDate.Text.Trim()).Month.ToString("00") + "/" + Convert.ToDateTime(txtRcvDate.Text.Trim()).Year.ToString()).ToString(),
                //    1, 1, drAccName.ToString() + "! " + txtNarration.Text.Trim(), "C", Convert.ToDecimal(txtRcvAmt.Text.Trim()),
                //    ddlRcvMode.SelectedItem.ToString(), txtDocRef.Text.Trim(), "BDT", 1, Convert.ToDecimal(txtRcvAmt.Text.Trim()),
                //    "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtRcvDate.Text.Trim()), custName.ToString(),
                //    Convert.ToDateTime(txtRcvDate.Text.Trim()), "ADM", "P", "",
                //    DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(txtRcvAmt.Text.Trim()), "",
                //    "", "", "N", 1, 0, "", "", "", "J", 0, "1", jvType, custAcc.ToString(), nextAccRefNo.ToString(),check);

                //Debit-Given Account
                //taAcc.UpdateAccData((Convert.ToDateTime(txtRcvDate.Text.Trim()).Month.ToString("00") + "/" + Convert.ToDateTime(txtRcvDate.Text.Trim()).Year.ToString()).ToString(),
                //    2, 1, custName.ToString(), "D", Convert.ToDecimal(txtRcvAmt.Text.Trim()),
                //    ddlRcvMode.SelectedItem.ToString(), txtDocRef.Text.Trim(), "BDT", 1, Convert.ToDecimal(txtRcvAmt.Text.Trim()),
                //    "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtRcvDate.Text.Trim()), drAccName.ToString(),
                //    Convert.ToDateTime(txtRcvDate.Text.Trim()), "ADM", "P", "",
                //    DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(txtRcvAmt.Text.Trim()), "",
                //    "", "", "N", 1, 0, "", "", "", "J", 0, "1", jvType, drAcc.ToString(), nextAccRefNo.ToString(), check);

                #endregion

                #region Credit Realization Entry
                //var rcvAmt = Convert.ToDecimal(txtRcvAmt.Text.Trim());
                //var dtCrReal = taCrReal.GetPendChlnByCustRef(custRef.ToString());
                //foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                //{
                //    if (rcvAmt > dr.Sales_Chln_Due_Amt)
                //    {
                //        //need updated logic
                //        taCrReal.UpdateCreditRealizeAmt(0, dr.Sales_Chln_Pay_Rcv_Ref + "Receive Ref. No " + nextAccRefNo + " Amt: " + dr.Sales_Chln_Due_Amt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                //        rcvAmt = rcvAmt - dr.Sales_Chln_Due_Amt;
                //    }
                //    else
                //    {
                //        taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - rcvAmt, dr.Sales_Chln_Pay_Rcv_Ref + "Receive Ref. No " + nextAccRefNo + " Amt: " + rcvAmt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                //        rcvAmt = 0;
                //        break;
                //    }
                //}
                #endregion
                //}

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Payment received successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "Ref. No: " + nextAccRefNo.ToString();
                ModalPopupExtenderMsg.Show();

                try
                {
                    //Procedure one             
                    String sid = "7HorseSuprmBng";
                    String user = "sevenhorse";
                    String pass = "123456";
                    String URI = "http://sms.sslwireless.com/pushapi/dynamic/server.php";

                    var cellNo = "";
                    var taSmsConfig = new tbl_Sms_ConfigTableAdapter();
                    var dtSmsConfig = taSmsConfig.GetDataByTranTypeStatus("PAY-RCV", "1");
                    foreach (dsSalesMas.tbl_Sms_ConfigRow dr in dtSmsConfig.Rows)
                    {
                        var taParAdr = new tblSalesPartyAdrTableAdapter();
                        var dtParAdr = taParAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef.ToString()));

                        if (dr.Config_Receiver_Grp_Name.ToString() == "INDV")
                            cellNo = dr.Config_Receiver_Cell_No.ToString();

                        if (dr.Config_Receiver_Grp_Name.ToString() == "CUST")
                        {
                            if (dtParAdr.Rows.Count > 0)
                                cellNo = dtParAdr[0].Par_Adr_Cell_No.ToString();
                        }

                        if (dr.Config_Receiver_Grp_Name.ToString() == "DSM")
                        {
                            var dsmRef = "";
                            if (dtParAdr.Rows.Count > 0)
                                dsmRef = dtParAdr[0].Par_Adr_Sls_Per.ToString();

                            var taDsm = new tblSalesDsmMasTableAdapter();
                            var dtDsm = taDsm.GetDataByDsmRef(Convert.ToInt32(dsmRef.ToString()));
                            if (dtDsm.Rows.Count > 0)
                            {
                                if (dtDsm[0].Dsm_Status.ToString() == "1")
                                    cellNo = dtDsm[0].Dsm_Cell_No.ToString();
                            }
                        }

                        if (dr.Config_Receiver_Grp_Name.ToString() == "MPO")
                        {
                            var mpoRef = "";
                            if (dtParAdr.Rows.Count > 0)
                                mpoRef = dtParAdr[0].Par_Adr_Ext_Data2.ToString();

                            var taMpo = new tblSalesPersonMasTableAdapter();
                            var dtMpo = taMpo.GetDataBySpRef(Convert.ToInt32(mpoRef.ToString()));
                            if (dtMpo.Rows.Count > 0)
                            {
                                if (dtMpo[0].Sp_Status.ToString() == "1")
                                    cellNo = dtMpo[0].Sp_Cell_No.ToString();
                            }
                        }

                        if (cellNo.Length > 0)
                        {
                            if (cellNo.Substring(0, 2) == "01" && cellNo.ToString().Length == 11)
                            {
                                var taSmsBody = new tbl_Sms_BodyTableAdapter();
                                var dtSmsBody = taSmsBody.GetDataBySmsTranTypeLanguage("PAY-RCV", "Bangla");
                                if (dtSmsBody.Rows.Count > 0)
                                {
                                    //String myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=88" + cellNo.ToString() + "&sms[0][1]="
                                    //                       + System.Web.HttpUtility.UrlEncode(DRN_WEB_ERP.GlobalClass.clsSmsHelper.convertBanglatoUnicode(dtSmsBody[0].SMS_Body_1.ToString()
                                    //                       + txtRcvAmt.Text.Trim() + dtSmsBody[0].SMS_Body_2.ToString() + " (Dealer Name: " + custName + ")"))
                                    //                       + "&sms[0][2]=" + "1234567890" + "&sid=" + sid;

                                    String myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=88" + cellNo.ToString() + "&sms[0][1]="
                                                           + System.Web.HttpUtility.UrlEncode(DRN_WEB_ERP.GlobalClass.clsSmsHelper.convertBanglatoUnicode(dtSmsBody[0].SMS_Body_1.ToString()
                                                           + txtRcvAmt.Text.Trim() + dtSmsBody[0].SMS_Body_2.ToString()))
                                                           + "&sms[0][2]=" + "1234567890" + "&sid=" + sid;

                                    using (WebClient wc = new WebClient())
                                    {
                                        wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                                        string HtmlResult = wc.UploadString(URI, myParameters);

                                        Console.Write(HtmlResult);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex) { }

                txtSearch.Text = "";
                txtSearch.Enabled = true;
                btnSearch.Enabled = true;
                btnClearSrch.Visible = false;
                lblCrLimit.Text = "0.00";
                lblCrLimitOutStand.Text = "0.00";
                txtCrLimitBal.Text = "0.00";
                txtRcvDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlRcvMode.SelectedIndex = 0;
                txtDocRef.Text = "";
                txtNarration.Text = "";
                txtDrAcc.Text = "";
                txtRcvAmt.Text = "";
                //hfEditStatus.Value = "N";
                //hfRefNo.Value = "0";
                pnlDet.Enabled = false;

                var dtNewMaxAccRef = taAcc.GetMaxRefNo(jvType, Convert.ToDateTime(txtRcvDate.Text.Trim()).Year);
                var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
                var nextNewAccRefNo = jvType + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
                txtRcvRefNo.Text = nextNewAccRefNo.ToString();

                var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                var dtFaTeTrnRef = taFaTe.GetDataByTopTranByTranType(jvType);
                gvLastPayDet.DataSource = dtFaTeTrnRef;
                gvLastPayDet.DataBind();

                var dtFaTeDate = taFaTe.GetDataByDateRangeTranTypeDrCr(jvType, DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"), "C");
                gvTodayPayDet.DataSource = dtFaTeDate;
                gvTodayPayDet.DataBind();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var accRef = "";
                var srchAccWords = txtDrAcc.Text.Trim().Split(':');
                foreach (string word in srchAccWords)
                {
                    accRef = word;
                    break;
                }

                if (accRef.Length > 0)
                {
                    var taGl = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtGl = taGl.GetDataByCoaCode(accRef);
                    if (dtGl.Rows.Count > 0)
                        args.IsValid = true;
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex)
            {
                args.IsValid = false;
            }
        }

        protected void txtRcvDate_TextChanged(object sender, EventArgs e)
        {
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var dtNewMaxAccRef = taAcc.GetMaxRefNo("RJV", Convert.ToDateTime(txtRcvDate.Text.Trim()).Year);
            var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
            var nextNewAccRefNo = "RJV" + (Convert.ToDateTime(txtRcvDate.Text.Trim()).Month.ToString("00") + Convert.ToDateTime(txtRcvDate.Text.Trim()).ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
            txtRcvRefNo.Text = nextNewAccRefNo.ToString();
        }

        protected void gvLastPayDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
        //        e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
        //        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
        //    }
        }

        protected void gvLastPayDet_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    int indx = gvLastPayDet.SelectedIndex;

        //    if (indx != -1)
        //    {
        //        try
        //        {
        //            Label lblTrnRefNo = (Label)gvLastPayDet.Rows[indx].FindControl("lblTrnRefNo");
        //            HiddenField hfGlCode = (HiddenField)gvLastPayDet.Rows[indx].FindControl("hfGlCode");
        //            hfEditStatus.Value = "Y";
        //            hfRefNo.Value = lblTrnRefNo.Text.Trim();

        //            var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
        //            var dtFaTe = taFaTe.GetDataByJvRef(lblTrnRefNo.Text.ToString());
        //            foreach(dsAccTran.tbl_Acc_Fa_TeRow dr in dtFaTe.Rows)
        //            {
        //                if (dr.Trn_Trn_type == "C")
        //                {
        //                    var taCust = new tblSalesPartyAdrTableAdapter();
        //                    var dtCust = taCust.GetDataByPartyAccRef(hfGlCode.Value.ToString());
        //                    if (dtCust.Rows.Count > 0)
        //                    {
        //                        txtSearch.Text = (dtCust[0].Par_Adr_Ref.ToString() + ":" + dtCust[0].Par_Adr_Ref_No.ToString() + ":" + dtCust[0].Par_Adr_Name.ToString());
        //                        SearchCustomerData();

        //                        var trnNarration = "";
        //                        var srchWords = dr.Trn_Narration.Trim().Split('!');
        //                        if (srchWords.Length >= 1) trnNarration = srchWords[1].ToString().Trim();
        //                        txtNarration.Text = trnNarration.ToString();
        //                    }
        //                }
        //                if (dr.Trn_Trn_type == "D")
        //                {
        //                    txtRcvRefNo.Text = dr.Trn_Ref_No;
        //                    txtRcvDate.Text = dr.Trn_DATE.ToString("dd/MM/yyyy");
        //                    txtDocRef.Text = dr.Trn_Cheque_No;

        //                    var taGlCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
        //                    var dtGlCoa = taGlCoa.GetDataByCoaCode(dr.Trn_Ac_Code);
        //                    txtDrAcc.Text = dtGlCoa.Rows.Count > 0 ? (dtGlCoa[0].Gl_Coa_Code.ToString() + ":" + dtGlCoa[0].Gl_Coa_Name.ToString()) : "";
                            
        //                    txtRcvAmt.Text = dr.Trn_Amount.ToString();                            

        //                    ddlRcvMode.SelectedItem.Text = dtFaTe[0].Trn_Match.ToString();
        //                }                        
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
        //            tblMsg.Rows[1].Cells[0].InnerText = "";
        //            ModalPopupExtenderMsg.Show();
        //        }
        //    }
        }

        protected void gvTodayPayDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
        //        e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
        //        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
        //    }
        }

        protected void gvTodayPayDet_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    int indx = gvTodayPayDet.SelectedIndex;

        //    if (indx != -1)
        //    {
        //        try
        //        {
        //            Label lblTrnRefNo = (Label)gvTodayPayDet.Rows[indx].FindControl("lblTrnRefNoDay");
        //            HiddenField hfGlCode = (HiddenField)gvTodayPayDet.Rows[indx].FindControl("hfGlCodeDay");
        //            hfEditStatus.Value = "Y";
        //            hfRefNo.Value = lblTrnRefNo.Text.Trim();

        //            var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
        //            var dtFaTe = taFaTe.GetDataByJvRef(lblTrnRefNo.Text.ToString());
        //            foreach (dsAccTran.tbl_Acc_Fa_TeRow dr in dtFaTe.Rows)
        //            {
        //                if (dr.Trn_Trn_type == "C")
        //                {
        //                    var taCust = new tblSalesPartyAdrTableAdapter();
        //                    var dtCust = taCust.GetDataByPartyAccRef(hfGlCode.Value.ToString());
        //                    if (dtCust.Rows.Count > 0)
        //                    {
        //                        txtSearch.Text = (dtCust[0].Par_Adr_Ref.ToString() + ":" + dtCust[0].Par_Adr_Ref_No.ToString() + ":" + dtCust[0].Par_Adr_Name.ToString());
        //                        SearchCustomerData();

        //                        var trnNarration = "";
        //                        var srchWords = dr.Trn_Narration.Trim().Split('!');
        //                        if (srchWords.Length >= 1) trnNarration = srchWords[1].ToString().Trim();
        //                        txtNarration.Text = trnNarration.ToString();
        //                    }
        //                }
        //                if (dr.Trn_Trn_type == "D")
        //                {
        //                    txtRcvRefNo.Text = dr.Trn_Ref_No;
        //                    txtRcvDate.Text = dr.Trn_DATE.ToString("dd/MM/yyyy");
        //                    txtDocRef.Text = dr.Trn_Cheque_No;

        //                    var taGlCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
        //                    var dtGlCoa = taGlCoa.GetDataByCoaCode(dr.Trn_Ac_Code);
        //                    txtDrAcc.Text = dtGlCoa.Rows.Count > 0 ? (dtGlCoa[0].Gl_Coa_Code.ToString() + ":" + dtGlCoa[0].Gl_Coa_Name.ToString()) : "";

        //                    txtRcvAmt.Text = dr.Trn_Amount.ToString();

        //                    ddlRcvMode.SelectedItem.Text = dtFaTe[0].Trn_Match.ToString();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
        //            tblMsg.Rows[1].Cells[0].InnerText = "";
        //            ModalPopupExtenderMsg.Show();
        //        }
        //    }
        }
    }
}