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
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmTranTripBill : System.Web.UI.Page
    {
        decimal totDrAmt = 0;
        decimal totCrAmt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            AutoCompleteExtender1.ContextKey = "T";

            var taAccJvHold = new tbl_Acc_Jv_HoldTableAdapter();
            var dtNewMaxAccRef = taAccJvHold.GetMaxRef("TTV");
            var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
            var nextNewAccRefNo = "TMP-TTV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
            txtTranRefNo.Text = nextNewAccRefNo.ToString();

            txtTranDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taVslMas = new tbl_TrTr_Vsl_MasTableAdapter();
            cboTruckNo.DataSource = taVslMas.GetDataByAsc();
            cboTruckNo.DataTextField = "Vsl_Mas_No";
            cboTruckNo.DataValueField = "Vsl_Mas_Ref";
            cboTruckNo.DataBind();
            cboTruckNo.Items.Insert(0, new ListItem("---Select---", "0"));

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
                        var Flag = "";

                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtVoucherDet"];

                        if (dtJvDet[0].Trn_Ref_No.ToString().Substring(0, 3) == "TMP")
                        {                            
                            var taJvHold = new tbl_Acc_Jv_HoldTableAdapter();
                            var dtJvHold = taJvHold.GetDataByJvRef(dtJvDet[0].Trn_Ref_No.ToString());
                            foreach (dsAccTran.tbl_Acc_Jv_HoldRow dr in dtJvHold.Rows)
                            {
                                var COA_CODE = dr.Tran_Acc_Code.ToString();
                                var COA_NAME = dr.Tran_Acc_Name.ToString();
                                var TRAN_DATE = dr.Trn_Date.ToString("dd/MM/yyyy");
                                var TRAN_NARRATION = dr.Tran_Narration.ToString();
                                var TRAN_DEBIT = dr.Trn_Type.ToString() == "D" ? dr.Trn_Amt : 0;
                                var TRAN_CREDIT = dr.Trn_Type.ToString() == "C" ? dr.Trn_Amt : 0;
                                var TRN_FLAG = dr.Trn_Flag.ToString();
                                Flag = dr.Trn_Flag.ToString();

                                dt.Rows.Add(COA_CODE, COA_NAME, TRAN_DATE, TRAN_NARRATION, TRAN_DEBIT.ToString("N2"), TRAN_CREDIT.ToString("N2"), TRN_FLAG);
                            }

                            #region Get Party Code
                            var taParty = new tblSalesPartyAdrTableAdapter();
                            var dtParty = taParty.GetDataByPartyAdrRef(Convert.ToInt32(dtJvHold[0].Trn_Adr_Code));
                            if (dtParty.Rows.Count > 0) txtGlParty.Text = dtParty[0].Par_Adr_Ref.ToString() + ":" + dtParty[0].Par_Adr_Ref_No.ToString() + ":" + dtParty[0].Par_Adr_Name.ToString();
                            #endregion

                            txtKm.Text = dtJvHold[0].Trn_Grn_No.ToString();
                            cboTruckNo.SelectedIndex = cboTruckNo.Items.IndexOf(cboTruckNo.Items.FindByValue(dtJvHold[0].IsTrn_Dc_NoNull() ? "0" : dtJvHold[0].Trn_Dc_No.ToString()));

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
                                var TRN_FLAG = dr.Trn_Flag.ToString();
                                Flag = dr.Trn_Flag.ToString();

                                dt.Rows.Add(COA_CODE, COA_NAME, TRAN_DATE, TRAN_NARRATION, TRAN_DEBIT.ToString("N2"), TRAN_CREDIT.ToString("N2"), TRN_FLAG);
                            }

                            #region Get Party Code
                            var taParty = new tblSalesPartyAdrTableAdapter();
                            var dtParty = taParty.GetDataByPartyAdrRef(Convert.ToInt32(dtFaTe[0].Trn_Adr_Code));
                            if (dtParty.Rows.Count > 0) txtGlParty.Text = dtParty[0].Par_Adr_Ref.ToString() + ":" + dtParty[0].Par_Adr_Ref_No.ToString() + ":" + dtParty[0].Par_Adr_Name.ToString();
                            #endregion

                            txtKm.Text = dtFaTe[0].Trn_GRN_No.ToString();
                            cboTruckNo.SelectedIndex = cboTruckNo.Items.IndexOf(cboTruckNo.Items.FindByValue(dtFaTe[0].IsTrn_Dc_NoNull() ? "0" : dtFaTe[0].Trn_Dc_No.ToString()));

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

                txtGlParty.Text = "";
                txtKm.Text = "";
                cboTruckNo.SelectedIndex = 0;

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

                //var taAcc = new tbl_Acc_Fa_TeTableAdapter();
                //var dtNewMaxAccRef = taAcc.GetMaxRefNo("TTV", DateTime.Now.Year);
                //var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
                //var nextNewAccRefNo = "TMP-TTV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
                //txtTranRefNo.Text = nextNewAccRefNo.ToString();

                var taAccJvHold = new tbl_Acc_Jv_HoldTableAdapter();
                var dtNewMaxAccRef = taAccJvHold.GetMaxRef("TTV");
                var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
                var nextNewAccRefNo = "TMP-TTV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
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
            dt.Columns.Add("HDR_FLAG", typeof(string));
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

                dt.Rows.Add(COA_CODE, COA_NAME, txtTranDate.Text.Trim(), TRAN_NARRATION, TRAN_DEBIT.ToString("N2"), TRAN_CREDIT.ToString("N2"), "TTV");

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
                #region Get Party Code
                var hdrPartyCode = "";
                var hdrPartyName = "";
                var srchPartyWords = txtGlParty.Text.Trim().Split(':');
                foreach (string word in srchPartyWords)
                {
                    hdrPartyCode = word;
                    break;
                }

                if (hdrPartyCode.Length > 0)
                {
                    var taParty = new tblSalesPartyAdrTableAdapter();
                    var dtParty = taParty.GetDataByPartyAdrRef(Convert.ToInt32(hdrPartyCode));
                    if (dtParty.Rows.Count > 0)
                    {
                        hdrPartyCode = dtParty[0].Par_Adr_Ref.ToString();
                        hdrPartyName = dtParty[0].Par_Adr_Name.ToString();
                    }
                }
                #endregion

                var nextJvRefNo = "";

                taAccJvHold.AttachTransaction(myTran);
               
                if (hfEditStat.Value == "Y")
                {
                    var dtJvHold = taAccJvHold.GetDataByJvRef(txtSearch.Text.ToString());
                    if (dtJvHold.Rows.Count > 0)
                        taAccJvHold.DeleteAccJvHold(dtJvHold[0].Tran_Ref_No.ToString());
                    nextJvRefNo = dtJvHold[0].Tran_Ref_No.ToString();
                }
                else
                {
                    var dtMaxJvRef = taAccJvHold.GetMaxRef("TTV");
                    var nextJvRef = dtMaxJvRef == null ? "000001" : (Convert.ToInt32(dtMaxJvRef) + 1).ToString();
                    nextJvRefNo = "TMP-TTV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextJvRef).ToString("000000");
                }
                
                #region Insert Accounts Voucher Details
                short jvLno = 1;
                foreach (GridViewRow row in gvVoucherDet.Rows)
                {                    
                    var COA_CODE = ((Label)(row.FindControl("lblCoaCode"))).Text.Trim();                    
                    var TRAN_DATE = ((Label)(row.FindControl("lblTrnDate"))).Text.Trim();
                    var TRAN_NARRATION = ((Label)(row.FindControl("lblNarration"))).Text.Trim();
                    var TRAN_DEBIT = ((Label)(row.FindControl("lblDr"))).Text.Trim();
                    var TRAN_CREDIT = ((Label)(row.FindControl("lblCr"))).Text.Trim();
                    var TRAN_TYPE = Convert.ToDecimal(TRAN_DEBIT) > 0 ? "D" : "C";
                    var TRAN_AMT = Convert.ToDecimal(TRAN_DEBIT) > 0 ? TRAN_DEBIT : TRAN_CREDIT;
                    //var HDR_FLAG = ((HiddenField)(row.FindControl("hfHdrFlag"))).Value.Trim();

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
                        TRAN_NARRATION.ToString(), TRAN_TYPE, Convert.ToDecimal(TRAN_AMT), hdrPartyCode, cboTruckNo.SelectedValue.ToString(), txtKm.Text.ToString(),
                        hdrPartyName.ToString(), cboTruckNo.SelectedItem.ToString(), "", "", "H", "1", "TTV");

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

                txtGlParty.Text = "";
                txtKm.Text = "";
                cboTruckNo.SelectedIndex = 0;

                hfEditStat.Value = "N";

                LoadInitVoucherDetGridData();
                SetVoucherDetGridData();

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

                var dtNewMaxJvRef = taAccJvHold.GetMaxRef("TTV");
                var nextNewJvRef = dtNewMaxJvRef == null ? "000001" : (Convert.ToInt32(dtNewMaxJvRef) + 1).ToString();
                txtTranRefNo.Text = "TMP-TTV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewJvRef).ToString("000000");
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

            var taAccJvHold = new tbl_Acc_Jv_HoldTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAccJvHold.Connection);

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

                #region Get Party Code
                var hdrPartyCode = "";
                var hdrPartyName = "";
                var srchPartyWords = txtGlParty.Text.Trim().Split(':');
                foreach (string word in srchPartyWords)
                {
                    hdrPartyCode = word;
                    break;
                }

                if (hdrPartyCode.Length > 0)
                {
                    var taParty = new tblSalesPartyAdrTableAdapter();
                    var dtParty = taParty.GetDataByPartyAdrRef(Convert.ToInt32(hdrPartyCode));
                    if (dtParty.Rows.Count > 0)
                    {
                        hdrPartyCode = dtParty[0].Par_Adr_Ref.ToString();
                        hdrPartyName = dtParty[0].Par_Adr_Name.ToString();
                    }
                }
                #endregion

                var nextJvRefNo = "";

                taAccJvHold.AttachTransaction(myTran);

                if (hfEditStat.Value == "Y")
                {
                    var dtJvHold = taAccJvHold.GetDataByJvRef(txtSearch.Text.ToString());
                    if (dtJvHold.Rows.Count > 0)
                        taAccJvHold.DeleteAccJvHold(dtJvHold[0].Tran_Ref_No.ToString());
                    nextJvRefNo = dtJvHold[0].Tran_Ref_No.ToString();
                }
                else
                {
                    var dtMaxJvRef = taAccJvHold.GetMaxRef("TTV");
                    var nextJvRef = dtMaxJvRef == null ? "000001" : (Convert.ToInt32(dtMaxJvRef) + 1).ToString();
                    nextJvRefNo = "TMP-TTV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextJvRef).ToString("000000");
                }

                #region Insert Accounts Voucher Details
                short jvLno = 1;
                foreach (GridViewRow row in gvVoucherDet.Rows)
                {
                    var COA_CODE = ((Label)(row.FindControl("lblCoaCode"))).Text.Trim();
                    var TRAN_DATE = ((Label)(row.FindControl("lblTrnDate"))).Text.Trim();
                    var TRAN_NARRATION = ((Label)(row.FindControl("lblNarration"))).Text.Trim();
                    var TRAN_DEBIT = ((Label)(row.FindControl("lblDr"))).Text.Trim();
                    var TRAN_CREDIT = ((Label)(row.FindControl("lblCr"))).Text.Trim();
                    var TRAN_TYPE = Convert.ToDecimal(TRAN_DEBIT) > 0 ? "D" : "C";
                    var TRAN_AMT = Convert.ToDecimal(TRAN_DEBIT) > 0 ? TRAN_DEBIT : TRAN_CREDIT;
                    var HDR_FLAG = ((HiddenField)(row.FindControl("hfHdrFlag"))).Value.Trim();

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
                        TRAN_NARRATION.ToString(), TRAN_TYPE, Convert.ToDecimal(TRAN_AMT), hdrPartyCode, cboTruckNo.SelectedValue.ToString(), txtKm.Text.ToString(),
                        hdrPartyName.ToString(), cboTruckNo.SelectedItem.ToString(), "", "", "P", "1", "TTV");

                    jvLno++;
                }
                #endregion

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Voucher Posted Successfully.";
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

                txtGlParty.Text = "";
                txtKm.Text = "";
                cboTruckNo.SelectedIndex = 0;

                hfEditStat.Value = "N";

                LoadInitVoucherDetGridData();
                SetVoucherDetGridData();

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

                var dtNewMaxJvRef = taAccJvHold.GetMaxRef("TTV");
                var nextNewJvRef = dtNewMaxJvRef == null ? "000001" : (Convert.ToInt32(dtNewMaxJvRef) + 1).ToString();
                txtTranRefNo.Text = "TMP-TTV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewJvRef).ToString("000000");
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearAll_Click(object sender, EventArgs e)
        {
            LoadInitVoucherDetGridData();
            SetVoucherDetGridData();

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

            txtGlParty.Text = "";
            txtKm.Text = "";
            txtGlAcc.Text = "";
            txtTranAmt.Text = "";
            txtNarration.Text = "";

            cboTruckNo.SelectedIndex = 0;

            var taAccJvHold = new tbl_Acc_Jv_HoldTableAdapter();
            var dtNewMaxJvRef = taAccJvHold.GetMaxRef("TTV");
            var nextNewJvRef = dtNewMaxJvRef == null ? "000001" : (Convert.ToInt32(dtNewMaxJvRef) + 1).ToString();
            txtTranRefNo.Text = "TMP-TTV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewJvRef).ToString("000000");
        }
    }
}