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
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmVoucherEntryContra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtRcvDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var dtNewMaxAccRef = taAcc.GetMaxRefNo("CNTV", Convert.ToDateTime(txtRcvDate.Text.Trim()).Year);
            var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
            var nextNewAccRefNo = "CNTV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
            txtRcvRefNo.Text = nextNewAccRefNo.ToString();

            var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
            var dtFaTeTrnRef = taFaTe.GetDataByTopTranByTranType("CNTV");
            gvLastPayDet.DataSource = dtFaTeTrnRef;
            gvLastPayDet.DataBind();

            var dtFaTeDate = taFaTe.GetDataByDateRangeTranTypeDrCr("CNTV", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"), "D");
            gvTodayPayDet.DataSource = dtFaTeDate;
            gvTodayPayDet.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGlCoaData();
        }

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                txtSearch.Enabled = true;
                btnSearch.Enabled = true;
                btnClearSrch.Visible = false;
                //lblCrLimit.Text = "0.00";
                //lblCrLimitOutStand.Text = "0.00";
                //txtCrLimitBal.Text = "0.00";
                txtRcvDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlRcvMode.SelectedIndex = 0;
                txtDocRef.Text = "";
                txtNarration.Text = "";
                txtCrAcc.Text = "";
                txtRcvAmt.Text = "";
                //hfEditStatus.Value = "N";
                //hfRefNo.Value = "0";
                pnlDet.Enabled = false;                

                var taAcc = new tbl_Acc_Fa_TeTableAdapter();
                var dtNewMaxAccRef = taAcc.GetMaxRefNo("CNTV", Convert.ToDateTime(txtRcvDate.Text.Trim()).Year);
                var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
                var nextNewAccRefNo = "CNTV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
                txtRcvRefNo.Text = nextNewAccRefNo.ToString();

                var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                var dtFaTeTrnRef = taFaTe.GetDataByTopTranByTranType("CNTV");
                gvLastPayDet.DataSource = dtFaTeTrnRef;
                gvLastPayDet.DataBind();

                var dtFaTeDate = taFaTe.GetDataByDateRangeTranTypeDrCr("CNTV", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"), "D");
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

        private void SearchGlCoaData()
        {
            var taSupAdr = new tbl_Acc_Fa_Gl_CoaTableAdapter();

            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                var supRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    supRef = word;
                    break;
                }

                if (supRef.Length > 0)
                {
                    var dtPartyAdr = taSupAdr.GetDataByCoaCode(supRef.ToString());
                    if (dtPartyAdr.Rows.Count > 0)
                    {
                        txtSearch.Enabled = false;
                        btnSearch.Enabled = false;
                        btnClearSrch.Visible = true;

                        //var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;
                        //var custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                        //var taSalesOrder = new View_Sales_Ord_NewTableAdapter();
                        //var dtSoBalAmtSum = taSalesOrder.GetSoBalAmt(custRef.ToString());
                        //var soBalAmtSum = dtSoBalAmtSum == null ? 0 : Convert.ToDouble(dtSoBalAmtSum);
                        //var taSalesOrderDo = new View_Delivery_Ord_NewTableAdapter();
                        //var dtDoBalAmtSum = taSalesOrderDo.GetDoBalAmt(custRef.ToString());
                        //var doBalAmtSum = dtDoBalAmtSum == null ? 0 : Convert.ToDouble(dtDoBalAmtSum);

                        //var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                        //var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                        //var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                        //var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                        //var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                        //lblCrLimit.Text = crLimit.ToString("N2");
                        //lblCrLimitOutStand.Text = (soBalAmtSum + doBalAmtSum + (drAmt - crAmt)).ToString("N2");
                        //txtCrLimitBal.Text = (Convert.ToDouble(crLimit) - (soBalAmtSum + doBalAmtSum + (drAmt - crAmt))).ToString("N2");

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
                        //lblCrLimit.Text = "0.00";
                        //lblCrLimitOutStand.Text = "0.00";
                        //txtCrLimitBal.Text = "0.00";
                        txtRcvDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        ddlRcvMode.SelectedIndex = 0;
                        txtNarration.Text = "";
                        txtCrAcc.Text = "";
                        txtRcvAmt.Text = "";
                        pnlDet.Enabled = false;

                        tblMsg.Rows[0].Cells[0].InnerText = "Supplier data not found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
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

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAcc.Connection);

            try
            {
                #region Validate and Get Credit Account
                var supRef = "";
                var supName = "";
                var supAcc = "";
                var supAccType = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    supRef = word;
                    break;
                }

                if (supRef.Length > 0)
                {
                    var taSupAdr = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtSupAdr = taSupAdr.GetDataByCoaCode(supRef.ToString());
                    if (dtSupAdr.Rows.Count > 0)
                    {
                        supRef = dtSupAdr[0].Gl_Coa_Code.ToString();
                        supName = dtSupAdr[0].Gl_Coa_Name.ToString();
                        supAcc = dtSupAdr[0].Gl_Coa_Code.ToString();
                        supAccType = dtSupAdr[0].Gl_Coa_Type.ToString();
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid GL.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid GL.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                #region Validate and Get Debit Account Code
                var drAcc = "";
                var drAccName = "";
                var drAccType = "";
                var srchAccWords = txtCrAcc.Text.Trim().Split(':');
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

                var nextAccRefNo = "";

                #region Insert Accounts Voucher Entry
                var dtMaxAccRef = taAcc.GetMaxRefNo("CNTV", Convert.ToDateTime(txtRcvDate.Text.Trim()).Year);
                var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                nextAccRefNo = "CNTV" + Convert.ToDateTime(txtRcvDate.Text.Trim()).Month.ToString("00") + Convert.ToDateTime(txtRcvDate.Text.Trim()).ToString("yy") + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                //Debit-Credit Account
                taAcc.InsertAccData(drAcc.ToString(), (Convert.ToDateTime(txtRcvDate.Text.Trim()).Month.ToString("00") + "/" + Convert.ToDateTime(txtRcvDate.Text.Trim()).Year.ToString()).ToString(),
                    nextAccRefNo.ToString(), 2, 1, supName.ToString(), "C", Convert.ToDecimal(txtRcvAmt.Text.Trim()),
                    ddlRcvMode.SelectedItem.ToString(), txtDocRef.Text.Trim(), "BDT", 1, Convert.ToDecimal(txtRcvAmt.Text.Trim()),
                    "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtRcvDate.Text.Trim()), drAccName.ToString(),
                    Convert.ToDateTime(txtRcvDate.Text.Trim()), "ADM", drAccType, "",
                    DateTime.Now, "CNTV", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(txtRcvAmt.Text.Trim()), "",
                    "", "", "N", 1, 0, "", "", "", "J", 0, "1", "CNTV");

                //Credit-Debit Account
                taAcc.InsertAccData(supAcc.ToString(), (Convert.ToDateTime(txtRcvDate.Text.Trim()).Month.ToString("00") + "/" + Convert.ToDateTime(txtRcvDate.Text.Trim()).Year.ToString()).ToString(),
                    nextAccRefNo.ToString(), 1, 1, drAccName.ToString() + "! " + txtNarration.Text.Trim(), "D", Convert.ToDecimal(txtRcvAmt.Text.Trim()),
                    ddlRcvMode.SelectedItem.ToString(), txtDocRef.Text.Trim(), "BDT", 1, Convert.ToDecimal(txtRcvAmt.Text.Trim()),
                    "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtRcvDate.Text.Trim()), supName.ToString(),
                    Convert.ToDateTime(txtRcvDate.Text.Trim()), "ADM", supAccType, "",
                    DateTime.Now, "CNTV", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(txtRcvAmt.Text.Trim()), "",
                    "", "", "N", 1, 0, "", "", "", "J", 0, "1", "CNTV");
                #endregion

                txtSearch.Text = "";
                txtSearch.Enabled = true;
                btnSearch.Enabled = true;
                btnClearSrch.Visible = false;
                //lblCrLimit.Text = "0.00";
                //lblCrLimitOutStand.Text = "0.00";
                //txtCrLimitBal.Text = "0.00";
                txtRcvDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlRcvMode.SelectedIndex = 0;
                txtDocRef.Text = "";
                txtNarration.Text = "";
                txtCrAcc.Text = "";
                txtRcvAmt.Text = "";
                //hfEditStatus.Value = "N";
                //hfRefNo.Value = "0";
                pnlDet.Enabled = false;

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Payment recorded successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "Ref. No: " + nextAccRefNo.ToString();
                ModalPopupExtenderMsg.Show();

                var dtNewMaxAccRef = taAcc.GetMaxRefNo("CNTV", Convert.ToDateTime(txtRcvDate.Text.Trim()).Year);
                var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
                var nextNewAccRefNo = "CNTV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
                txtRcvRefNo.Text = nextNewAccRefNo.ToString();

                var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                var dtFaTeTrnRef = taFaTe.GetDataByTopTranByTranType("CNTV");
                gvLastPayDet.DataSource = dtFaTeTrnRef;
                gvLastPayDet.DataBind();

                var dtFaTeDate = taFaTe.GetDataByDateRangeTranTypeDrCr("CNTV", DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"), "D");
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
                var srchAccWords = txtCrAcc.Text.Trim().Split(':');
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
            var dtNewMaxAccRef = taAcc.GetMaxRefNo("CNTV", Convert.ToDateTime(txtRcvDate.Text.Trim()).Year);
            var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
            var nextNewAccRefNo = "CNTV" + (Convert.ToDateTime(txtRcvDate.Text.Trim()).Month.ToString("00") + Convert.ToDateTime(txtRcvDate.Text.Trim()).ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
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