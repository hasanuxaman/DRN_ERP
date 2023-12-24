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
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmVoucherEntry : System.Web.UI.Page
    {
        decimal totDrAmt = 0;
        decimal totCrAmt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtTranDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var dtNewMaxAccRef = taAcc.GetMaxRefNo("JV", Convert.ToDateTime(txtTranDate.Text.Trim()).Year);
            var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
            var nextNewAccRefNo = "JV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
            txtTranRefNo.Text = nextNewAccRefNo.ToString();            

            LoadInitVoucherDetGridData();
            SetVoucherDetGridData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                if (txtSearch.Text.Trim().Length > 0)
                {
                    var taJvDet = new View_Jv_DetTableAdapter();
                    var dtJvDet = taJvDet.GetDataByJvRef(txtSearch.Text.Trim());
                    if (dtJvDet.Rows.Count > 0)
                    {
                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtVoucherDet"];

                        if (dtJvDet[0].Trn_Ref_No.ToString().Substring(0, 3) == "TMP")
                        {
                            var taJvHold = new tbl_Acc_Jv_HoldTableAdapter();
                            var dtJvHold = taJvHold.GetDataByJvRef(dtJvDet[0].Trn_Ref_No.ToString());
                            foreach(dsAccTran.tbl_Acc_Jv_HoldRow dr in dtJvHold.Rows)
                            {
                                var COA_CODE = dr.Tran_Acc_Code.ToString();
                                var COA_NAME = dr.Tran_Acc_Name.ToString();
                                var TRAN_DATE = dr.Trn_Date.ToString("dd/MM/yyyy");
                                var TRAN_NARRATION = dr.Tran_Narration.ToString();
                                var TRAN_DEBIT = dr.Trn_Type.ToString() == "D" ? dr.Trn_Amt : 0;
                                var TRAN_CREDIT = dr.Trn_Type.ToString() == "C" ? dr.Trn_Amt : 0;

                                dt.Rows.Add(COA_CODE, COA_NAME, TRAN_DATE, TRAN_NARRATION, TRAN_DEBIT.ToString("N2"), TRAN_CREDIT.ToString("N2"));
                            }
                            btnAdd.Enabled = true;
                            btnHold.Visible = true;
                            btnPost.Visible = true;
                            gvVoucherDet.Enabled = true;
                        }
                        else
                        {
                            var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                            var dtFaTe = taFaTe.GetDataByJvRef(dtJvDet[0].Trn_Ref_No.ToString());
                            foreach (dsAccTran.tbl_Acc_Fa_TeRow dr in dtFaTe.Rows)
                            {                                
                                var COA_CODE = dr.Trn_Ac_Code.ToString();
                                var COA_NAME = dr.Trn_Ac_Desc.ToString();
                                var TRAN_DATE = dr.Trn_DATE.ToString("dd/MM/yyyy");
                                var TRAN_NARRATION = dr.Trn_Narration.ToString();
                                var TRAN_DEBIT = dr.Trn_Trn_type.ToString() == "D" ? dr.Trn_Amount : 0;
                                var TRAN_CREDIT = dr.Trn_Trn_type.ToString() == "C" ? dr.Trn_Amount : 0;

                                dt.Rows.Add(COA_CODE, COA_NAME, TRAN_DATE, TRAN_NARRATION, TRAN_DEBIT.ToString("N2"), TRAN_CREDIT.ToString("N2"));                    
                            }
                            btnAdd.Enabled = false;
                            btnHold.Visible = false;
                            btnPost.Visible = false;
                            gvVoucherDet.Enabled = false;
                        }

                        ViewState["dtVoucherDet"] = dt;
                        SetVoucherDetGridData();

                        foreach (GridViewRow gr in gvVoucherDet.Rows)
                        {
                            var lblDrAmt = ((Label)(gr.FindControl("lblDr"))).Text.ToString();
                            totDrAmt += Convert.ToDecimal(lblDrAmt.Trim());

                            var lblCrAmt = ((Label)(gr.FindControl("lblCr"))).Text.ToString();
                            totCrAmt += Convert.ToDecimal(lblCrAmt.Trim());
                        }

                        if (gvVoucherDet.Rows.Count > 0)
                        {
                            txtTranDate.Enabled = false;
                            lblTotDrAmt.Text = totDrAmt.ToString("N2");
                            lblTotCrAmt.Text = totCrAmt.ToString("N2");
                            lblDrCrBal.Text = (totDrAmt - totCrAmt).ToString("N2");
                        }
                        else
                        {
                            txtTranDate.Enabled = true;
                            lblTotDrAmt.Text = "0.00";
                            lblTotCrAmt.Text = "0.00";
                            lblDrCrBal.Text = "0.00";
                        }

                        txtSearch.Enabled = false;
                        btnSearch.Enabled = false;
                        btnClearSrch.Visible = true;

                        hfEditStat.Value = "Y";
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "JV data not found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "JV data not found.";
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
                txtTranDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTranDate.Enabled = true;
                txtNarration.Text = "";
                txtGlAcc.Text = "";
                txtTranAmt.Text = "";

                lblTotDrAmt.Text = "0.00";
                lblTotCrAmt.Text = "0.00";
                lblDrCrBal.Text = "0.00";

                LoadInitVoucherDetGridData();
                SetVoucherDetGridData();

                btnAdd.Enabled = true;
                btnHold.Visible = false;
                btnPost.Visible = false;
                gvVoucherDet.Enabled = true;
                hfEditStat.Value = "N";

                var taAcc = new tbl_Acc_Fa_TeTableAdapter();
                var dtNewMaxAccRef = taAcc.GetMaxRefNo("JV", Convert.ToDateTime(txtTranDate.Text.Trim()).Year);
                var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
                var nextNewAccRefNo = "JV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
                txtTranRefNo.Text = nextNewAccRefNo.ToString();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        #region Voucher Details Gridview
        protected void LoadInitVoucherDetGridData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("COA_CODE", typeof(string));
            dt.Columns.Add("COA_NAME", typeof(string));
            dt.Columns.Add("TRAN_DATE", typeof(string));
            dt.Columns.Add("TRAN_NARRATION", typeof(string));
            dt.Columns.Add("TRAN_DEBIT", typeof(string));
            dt.Columns.Add("TRAN_CREDIT", typeof(string));
            ViewState["dtVoucherDet"] = dt;
        }

        protected void SetVoucherDetGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtVoucherDet"];

                gvVoucherDet.DataSource = dt;
                gvVoucherDet.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion
        
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var accRef = "";
                var srchAccWords = txtGlAcc.Text.Trim().Split(':');
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Page.Validate("btnAdd");

            if (!Page.IsValid) return;

            try
            {
                #region Get Tran GL Data
                var tranCoaCode = "";
                var tranCoaName = "";
                var srchWords = txtGlAcc.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    tranCoaCode = word;
                    break;
                }

                if (tranCoaCode.Length > 0)
                {
                    var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtCoa = taCoa.GetDataByCoaCode(tranCoaCode);
                    if (dtCoa.Rows.Count > 0)
                    {
                        tranCoaCode = dtCoa[0].Gl_Coa_Code.ToString();
                        tranCoaName = dtCoa[0].Gl_Coa_Name.ToString();
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

                var dt = new DataTable();
                dt = (DataTable)ViewState["dtVoucherDet"];

                var COA_CODE = tranCoaCode.ToString();
                var COA_NAME=tranCoaName.ToString();
                var TRAN_NARRATION = txtNarration.Text.Trim();
                var TRAN_DEBIT = ddlTranType.SelectedValue.ToString() == "D" ? Convert.ToDecimal(txtTranAmt.Text.Trim().Length > 0 ? txtTranAmt.Text.Trim() : "0") : 0;
                var TRAN_CREDIT = ddlTranType.SelectedValue.ToString() == "C" ? Convert.ToDecimal(txtTranAmt.Text.Trim().Length > 0 ? txtTranAmt.Text.Trim() : "0") : 0;

                dt.Rows.Add(COA_CODE, COA_NAME, txtTranDate.Text.Trim(), TRAN_NARRATION, TRAN_DEBIT.ToString("N2"), TRAN_CREDIT.ToString("N2"));

                ViewState["dtVoucherDet"] = dt;
                SetVoucherDetGridData();

                foreach (GridViewRow gr in gvVoucherDet.Rows)
                {
                    var lblDrAmt = ((Label)(gr.FindControl("lblDr"))).Text.ToString();
                    totDrAmt += Convert.ToDecimal(lblDrAmt.Trim());

                    var lblCrAmt = ((Label)(gr.FindControl("lblCr"))).Text.ToString();
                    totCrAmt += Convert.ToDecimal(lblCrAmt.Trim());
                }

                if (gvVoucherDet.Rows.Count > 0)
                {
                    txtTranDate.Enabled = false;
                    lblTotDrAmt.Text = totDrAmt.ToString("N2");
                    lblTotCrAmt.Text = totCrAmt.ToString("N2");
                    lblDrCrBal.Text = (totDrAmt - totCrAmt).ToString("N2");
                    btnHold.Visible = true;
                    btnPost.Visible = true;                    
                }
                else
                {
                    txtTranDate.Enabled = true;
                    lblTotDrAmt.Text = "0.00";
                    lblTotCrAmt.Text = "0.00";
                    lblDrCrBal.Text = "0.00";
                    btnHold.Visible = false;
                    btnPost.Visible = false;
                }

                txtGlAcc.Text = "";
                txtTranAmt.Text = "";
                txtNarration.Text = "";
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void gvVoucherDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var dt = new DataTable();
            dt = (DataTable)ViewState["dtVoucherDet"];

            dt.Rows[rowNum].Delete();
            dt.AcceptChanges();

            gvVoucherDet.EditIndex = -1;
            SetVoucherDetGridData();

            foreach (GridViewRow gr in gvVoucherDet.Rows)
            {
                var lblDrAmt = ((Label)(gr.FindControl("lblDr"))).Text.ToString();
                totDrAmt += Convert.ToDecimal(lblDrAmt.Trim());

                var lblCrAmt = ((Label)(gr.FindControl("lblCr"))).Text.ToString();
                totCrAmt += Convert.ToDecimal(lblCrAmt.Trim());
            }

            if (gvVoucherDet.Rows.Count > 0)
            {
                txtTranDate.Enabled = false;
                lblTotDrAmt.Text = totDrAmt.ToString("N2");
                lblTotCrAmt.Text = totCrAmt.ToString("N2");
                lblDrCrBal.Text = (totDrAmt - totCrAmt).ToString("N2");
                btnHold.Visible = true;
                btnPost.Visible = true;
            }
            else
            {
                txtTranDate.Enabled = true;
                lblTotDrAmt.Text = "0.00";
                lblTotCrAmt.Text = "0.00";
                lblDrCrBal.Text = "0.00";
                btnHold.Visible = false;
                btnPost.Visible = false;
            }
        }

        protected void btnHold_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var taAccJvHold = new tbl_Acc_Jv_HoldTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAccJvHold.Connection);

            try
            {
                taAccJvHold.AttachTransaction(myTran);

                if (hfEditStat.Value == "Y")
                {
                    var dtJvHold = taAccJvHold.GetDataByJvRef(txtSearch.Text.ToString());
                    if (dtJvHold.Rows.Count > 0)
                    {
                        taAccJvHold.DeleteAccJvHold(dtJvHold[0].Tran_Ref_No.ToString());
                    }
                }

                var dtMaxJvRef = taAccJvHold.GetMaxRef("JV");
                var nextJvRef = dtMaxJvRef == null ? "000001" : (Convert.ToInt32(dtMaxJvRef) + 1).ToString();
                var nextJvRefNo = "TMP-JV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextJvRef).ToString("000000");

                #region Insert Accounts Voucher Details
                short jvLno = 1;
                foreach (GridViewRow row in gvVoucherDet.Rows)
                {
                    var COA_CODE = ((Label)(row.FindControl("lblCoaCode"))).Text.Trim();
                    //var COA_NAME = ((Label)(row.FindControl("COA_CODE"))).Text.Trim();
                    var TRAN_DATE = ((Label)(row.FindControl("lblTrnDate"))).Text.Trim();
                    var TRAN_NARRATION = ((Label)(row.FindControl("lblNarration"))).Text.Trim();
                    var TRAN_DEBIT = ((Label)(row.FindControl("lblDr"))).Text.Trim();
                    var TRAN_CREDIT = ((Label)(row.FindControl("lblCr"))).Text.Trim();
                    var TRAN_TYPE = Convert.ToDecimal(TRAN_DEBIT) > 0 ? "D" : "C";
                    var TRAN_AMT = Convert.ToDecimal(TRAN_DEBIT) > 0 ? TRAN_DEBIT : TRAN_CREDIT;

                    #region Validate and Get Coa Data
                    var coaCode = "";
                    var coaName = "";
                    var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtCoa = taCoa.GetDataByCoaCode(COA_CODE);
                    if (dtCoa.Rows.Count > 0)
                    {
                        coaCode = dtCoa[0].Gl_Coa_Code.ToString();
                        coaName = dtCoa[0].Gl_Coa_Name.ToString();
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Account Code.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    #endregion

                    taAccJvHold.InsertAccJvHold(coaCode.ToString(), coaName.ToString(), nextJvRefNo.ToString(), Convert.ToDateTime(txtTranDate.Text.Trim()), jvLno,
                        TRAN_NARRATION.ToString(), TRAN_TYPE, Convert.ToDecimal(TRAN_AMT), "", "", "", "", "", "", "", "", "1", "JV");

                    jvLno++;
                }
                #endregion

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Voucher Held Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "Held JV Ref No: " + nextJvRefNo.ToString();
                ModalPopupExtenderMsg.Show();

                txtSearch.Text = "";
                txtSearch.Enabled = true;
                btnSearch.Enabled = true;
                btnClearSrch.Visible = false;
                txtTranDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtNarration.Text = "";
                txtGlAcc.Text = "";
                txtTranAmt.Text = "";

                hfEditStat.Value = "N";

                LoadInitVoucherDetGridData();
                SetVoucherDetGridData();

                foreach (GridViewRow gr in gvVoucherDet.Rows)
                {
                    var lblDrAmt = ((Label)(gr.FindControl("lblDr"))).Text.ToString();
                    totDrAmt += Convert.ToDecimal(lblDrAmt.Trim());

                    var lblCrAmt = ((Label)(gr.FindControl("lblCr"))).Text.ToString();
                    totCrAmt += Convert.ToDecimal(lblCrAmt.Trim());
                }

                if (gvVoucherDet.Rows.Count > 0)
                {
                    txtTranDate.Enabled = false;
                    lblTotDrAmt.Text = totDrAmt.ToString("N2");
                    lblTotCrAmt.Text = totCrAmt.ToString("N2");
                    lblDrCrBal.Text = (totDrAmt - totCrAmt).ToString("N2");
                    btnHold.Visible = true;
                    btnPost.Visible = true;
                }
                else
                {
                    txtTranDate.Enabled = true;
                    lblTotDrAmt.Text = "0.00";
                    lblTotCrAmt.Text = "0.00";
                    lblDrCrBal.Text = "0.00";
                    btnHold.Visible = false;
                    btnPost.Visible = false;
                }

                var taNewAcc = new tbl_Acc_Fa_TeTableAdapter();
                var dtNewMaxAccRef = taNewAcc.GetMaxRefNo("JV", Convert.ToDateTime(txtTranDate.Text.Trim()).Year);
                var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
                var nextNewAccRefNo = "JV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
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

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var taAccJvHold = new tbl_Acc_Jv_HoldTableAdapter();
            var taCrReal = new tblSalesCreditRealizeTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAcc.Connection);

            try
            {
                foreach (GridViewRow gr in gvVoucherDet.Rows)
                {
                    var lblDrAmt = ((Label)(gr.FindControl("lblDr"))).Text.ToString();
                    totDrAmt += Convert.ToDecimal(lblDrAmt.Trim());

                    var lblCrAmt = ((Label)(gr.FindControl("lblCr"))).Text.ToString();
                    totCrAmt += Convert.ToDecimal(lblCrAmt.Trim());
                }

                if ((totDrAmt - totCrAmt) != 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to post Out of Balance Voucher.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                taAcc.AttachTransaction(myTran);
                taAccJvHold.AttachTransaction(myTran);
                taCrReal.AttachTransaction(myTran);

                if (hfEditStat.Value == "Y")
                {
                    var dtJvHold = taAccJvHold.GetDataByJvRef(txtSearch.Text.ToString());
                    if (dtJvHold.Rows.Count > 0)
                    {
                        taAccJvHold.DeleteAccJvHold(dtJvHold[0].Tran_Ref_No.ToString());
                    }
                }

                var dtMaxAccRef = taAcc.GetMaxRefNo("JV", Convert.ToDateTime(txtTranDate.Text.Trim()).Year);
                var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                var nextAccRefNo = "JV" + (Convert.ToDateTime(txtTranDate.Text.Trim()).Month.ToString("00") + Convert.ToDateTime(txtTranDate.Text.Trim()).ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                #region Insert Accounts Voucher Details
                short jvLno = 1;
                foreach (GridViewRow row in gvVoucherDet.Rows)
                {
                    var COA_CODE = ((Label)(row.FindControl("lblCoaCode"))).Text.Trim();
                    //var COA_NAME = ((Label)(row.FindControl("COA_CODE"))).Text.Trim();
                    var TRAN_DATE = ((Label)(row.FindControl("lblTrnDate"))).Text.Trim();
                    var TRAN_NARRATION = ((Label)(row.FindControl("lblNarration"))).Text.Trim();
                    var TRAN_DEBIT = ((Label)(row.FindControl("lblDr"))).Text.Trim();
                    var TRAN_CREDIT = ((Label)(row.FindControl("lblCr"))).Text.Trim();
                    var TRAN_TYPE = Convert.ToDecimal(TRAN_DEBIT) > 0 ? "D" : "C";
                    var TRAN_AMT = Convert.ToDecimal(TRAN_DEBIT) > 0 ? TRAN_DEBIT : TRAN_CREDIT;

                    #region Validate and Get Coa Data
                    var coaCode = "";
                    var coaName = "";
                    var coaType="";
                    var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtCoa = taCoa.GetDataByCoaCode(COA_CODE);
                    if (dtCoa.Rows.Count > 0)
                    {
                        coaCode = dtCoa[0].Gl_Coa_Code.ToString();
                        coaName = dtCoa[0].Gl_Coa_Name.ToString();
                        coaType = dtCoa[0].Gl_Coa_Type.ToString();
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Account Code.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    #endregion

                    taAcc.InsertAccData(coaCode.ToString(), (Convert.ToDateTime(txtTranDate.Text.Trim()).Month.ToString("00") + "/" + Convert.ToDateTime(txtTranDate.Text.Trim()).Year.ToString()).ToString(),
                        nextAccRefNo.ToString(), jvLno, 1, TRAN_NARRATION.ToString(), TRAN_TYPE.ToString(), Convert.ToDecimal(TRAN_AMT),
                        "", "0", "BDT", 1, Convert.ToDecimal(TRAN_AMT), "", "", "", "", "", "", "", "", "", "", "",
                        (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "",
                        Convert.ToDateTime(txtTranDate.Text.Trim()), coaName.ToString(), Convert.ToDateTime(txtTranDate.Text.Trim()), "ADM", coaType, "",
                        DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(TRAN_AMT), "",
                        "", "", "N", 1, 0, "", "", "", "J", 0, "1", "JV");

                    if (coaType == "P")
                    {
                        #region Validate and Get Coa Data
                        var custRef = "";
                        var custName = "";
                        var taCust = new tblSalesPartyAdrTableAdapter();
                        var dtCust = taCust.GetDataByPartyAccRef(COA_CODE);
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

                        if (Convert.ToDecimal(TRAN_CREDIT) > 0)
                        {
                            #region Credit Realization Entry
                            var rcvAmt = Convert.ToDecimal(TRAN_CREDIT);
                            var dtCrReal = taCrReal.GetPendChlnByCustRef(custRef.ToString());
                            foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                            {
                                if (rcvAmt > dr.Sales_Chln_Due_Amt)
                                {
                                    rcvAmt = rcvAmt - dr.Sales_Chln_Due_Amt;
                                    taCrReal.UpdateCreditRealizeAmt(0, dr.Sales_Chln_Pay_Rcv_Ref + "Ref:" + nextAccRefNo.ToString() + " Amt: " + dr.Sales_Chln_Due_Amt.ToString("N2") + ", ", dr.Sales_Chln_Ref);                                    
                                }
                                else
                                {
                                    taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - rcvAmt, dr.Sales_Chln_Pay_Rcv_Ref + "Ref:" + nextAccRefNo.ToString() + " Amt: " + rcvAmt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                    rcvAmt = 0;
                                    break;
                                }
                            }
                            #endregion
                        }

                        if (Convert.ToDecimal(TRAN_DEBIT) > 0)
                        {
                            #region Credit Realization Entry
                            var TranDrAmt = Convert.ToDecimal(TRAN_DEBIT);

                            var dtCrSum = taAcc.GetTotCrAmt(COA_CODE.ToString());
                            var crAmt = dtCrSum == null ? 0 : Convert.ToDecimal(dtCrSum);
                            var dtDrSum = taAcc.GetTotDrAmt(COA_CODE.ToString());
                            var drAmt = dtDrSum == null ? 0 : Convert.ToDecimal(dtDrSum);
                            var CrBal = (crAmt - drAmt);

                            if (CrBal > 0)
                            {
                                if ((CrBal - TranDrAmt) < 0)
                                    taCrReal.InsertCreditRealize(nextAccRef.ToString() + "_" + jvLno.ToString(), nextAccRefNo.ToString(), Convert.ToDateTime(txtTranDate.Text.Trim()),
                                        Convert.ToDecimal(TranDrAmt - CrBal), Convert.ToDecimal((TranDrAmt - CrBal)), custRef.ToString(), "1", "", "", "", "", "");
                            }
                            else
                            {
                                taCrReal.InsertCreditRealize(nextAccRef.ToString() + "_" + jvLno.ToString(), nextAccRefNo.ToString(), Convert.ToDateTime(txtTranDate.Text.Trim()),
                                        Convert.ToDecimal(TranDrAmt), Convert.ToDecimal(TranDrAmt), custRef.ToString(), "1", "", "", "", "", "");
                            }
                            #endregion
                        }
                    }
                    jvLno++;
                }
                #endregion

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Voucher Posted Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "Posted JV Ref No: " + nextAccRefNo.ToString();
                ModalPopupExtenderMsg.Show();

                txtSearch.Text = "";
                txtSearch.Enabled = true;
                btnSearch.Enabled = true;
                btnClearSrch.Visible = false;
                txtTranDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtNarration.Text = "";
                txtGlAcc.Text = "";
                txtTranAmt.Text = "";

                hfEditStat.Value = "N";

                LoadInitVoucherDetGridData();
                SetVoucherDetGridData();

                foreach (GridViewRow gr in gvVoucherDet.Rows)
                {
                    var lblDrAmt = ((Label)(gr.FindControl("lblDr"))).Text.ToString();
                    totDrAmt += Convert.ToDecimal(lblDrAmt.Trim());

                    var lblCrAmt = ((Label)(gr.FindControl("lblCr"))).Text.ToString();
                    totCrAmt += Convert.ToDecimal(lblCrAmt.Trim());
                }

                if (gvVoucherDet.Rows.Count > 0)
                {
                    txtTranDate.Enabled = false;
                    lblTotDrAmt.Text = totDrAmt.ToString("N2");
                    lblTotCrAmt.Text = totCrAmt.ToString("N2");
                    lblDrCrBal.Text = (totDrAmt - totCrAmt).ToString("N2");
                    btnHold.Visible = true;
                    btnPost.Visible = true;
                }
                else
                {
                    txtTranDate.Enabled = true;
                    lblTotDrAmt.Text = "0.00";
                    lblTotCrAmt.Text = "0.00";
                    lblDrCrBal.Text = "0.00";
                    btnHold.Visible = false;
                    btnPost.Visible = false;
                }

                var taNewAcc = new tbl_Acc_Fa_TeTableAdapter();
                var dtNewMaxAccRef = taNewAcc.GetMaxRefNo("JV", Convert.ToDateTime(txtTranDate.Text.Trim()).Year);
                var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
                var nextNewAccRefNo = "JV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
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

        protected void txtTranDate_TextChanged(object sender, EventArgs e)
        {
            var taNewAcc = new tbl_Acc_Fa_TeTableAdapter();
            var dtNewMaxAccRef = taNewAcc.GetMaxRefNo("JV", Convert.ToDateTime(txtTranDate.Text.Trim()).Year);
            var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
            var nextNewAccRefNo = "JV" + (Convert.ToDateTime(txtTranDate.Text.Trim()).Month.ToString("00") + Convert.ToDateTime(txtTranDate.Text.Trim()).ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
            txtTranRefNo.Text = nextNewAccRefNo.ToString();
        }
    }
}