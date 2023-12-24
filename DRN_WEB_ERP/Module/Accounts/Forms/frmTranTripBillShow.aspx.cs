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
    public partial class frmTranTripBillShow : System.Web.UI.Page
    {
        decimal totDrAmt = 0;
        decimal totCrAmt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taVslMas = new tbl_TrTr_Vsl_MasTableAdapter();
            cboTruckNo.DataSource = taVslMas.GetDataByAsc();
            cboTruckNo.DataTextField = "Vsl_Mas_No";
            cboTruckNo.DataValueField = "Vsl_Mas_Ref";
            cboTruckNo.DataBind();
            cboTruckNo.Items.Insert(0, new ListItem("---Select---", "0"));

            if (Request.QueryString["TranJvRef"] != null)
            {
                var JvRef = Request.QueryString["TranJvRef"].ToString();

                try
                {
                    LoadInitVoucherDetGridData();
                    SetVoucherDetGridData();

                    var dt = new DataTable();
                    dt = (DataTable)ViewState["dtVoucherDet"];

                    var taJvHold = new tbl_Acc_Jv_HoldTableAdapter();
                    var dtJvHold = taJvHold.GetDataByJvRef(JvRef.ToString());
                    if (dtJvHold.Rows.Count > 0)
                    {
                        txtTranRefNo.Text = dtJvHold[0].Tran_Ref_No.ToString();
                        txtTranDate.Text = dtJvHold[0].Trn_Date.ToString("dd/MM/yyyy");
                        txtKm.Text = dtJvHold[0].Trn_Grn_No.ToString();

                        #region Get Party Code
                        var taParty = new tblSalesPartyAdrTableAdapter();
                        var dtParty = taParty.GetDataByPartyAdrRef(Convert.ToInt32(dtJvHold[0].Trn_Adr_Code));
                        if (dtParty.Rows.Count > 0) txtGlParty.Text = dtParty[0].Par_Adr_Ref.ToString() + ":" + dtParty[0].Par_Adr_Ref_No.ToString() + ":" + dtParty[0].Par_Adr_Name.ToString();
                        #endregion

                        cboTruckNo.SelectedIndex = cboTruckNo.Items.IndexOf(cboTruckNo.Items.FindByValue(dtJvHold[0].IsTrn_Dc_NoNull() ? "0" : dtJvHold[0].Trn_Dc_No.ToString()));

                        foreach (dsAccTran.tbl_Acc_Jv_HoldRow dr in dtJvHold.Rows)
                        {
                            var COA_CODE = dr.Tran_Acc_Code.ToString();
                            var COA_NAME = dr.Tran_Acc_Name.ToString();
                            var TRAN_DATE = dr.Trn_Date.ToString("dd/MM/yyyy");
                            var TRAN_NARRATION = dr.Tran_Narration.ToString();
                            var TRAN_DEBIT = dr.Trn_Type.ToString() == "D" ? dr.Trn_Amt : 0;
                            var TRAN_CREDIT = dr.Trn_Type.ToString() == "C" ? dr.Trn_Amt : 0;
                            var TRN_FLAG = dr.Trn_Ext_Data4.ToString();

                            dt.Rows.Add(COA_CODE, COA_NAME, TRAN_DATE, TRAN_NARRATION, TRAN_DEBIT.ToString("N2"), TRAN_CREDIT.ToString("N2"), TRN_FLAG);
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
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Vouvher Data Not Found.";
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

        protected void btnRej_Click(object sender, EventArgs e)
        {
            var taHold = new tbl_Acc_Jv_HoldTableAdapter();

            taHold.UpdateTranJvStat("H", txtTranRefNo.Text.ToString());

            tblMsg.Rows[0].Cells[0].InnerText = "Data Rejected successfully.";
            tblMsg.Rows[1].Cells[0].InnerText = "";
            ModalPopupExtenderMsg.Show();

            btnRej.Visible = false;
            BtnAppr.Visible = false;
        }

        protected void btnClearAll_Click(object sender, EventArgs e)
        {
            var taHold = new tbl_Acc_Jv_HoldTableAdapter();
            var taPost = new tbl_Acc_Fa_TeTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taHold.Connection);

            try
            {
                taHold.AttachTransaction(myTran);
                taPost.AttachTransaction(myTran);

                var dtHoldJv = taHold.GetDataByJvRef(txtTranRefNo.Text.ToString());
                if (dtHoldJv.Rows.Count > 0)
                {
                    var dtMaxAccRef = taPost.GetMaxRefNo(dtHoldJv[0].Trn_Flag, Convert.ToDateTime(dtHoldJv[0].Trn_Date).Year);
                    var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                    var nextAccRefNo = dtHoldJv[0].Trn_Flag + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                    var jvLno = 1;
                    foreach (dsAccTran.tbl_Acc_Jv_HoldRow dr in dtHoldJv.Rows)
                    {
                        taPost.InsertAccData(dr.Tran_Acc_Code.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                            nextAccRefNo.ToString(), jvLno, 1, dr.Tran_Narration.ToString(), dr.Trn_Type.ToString(), Convert.ToDecimal(dr.Trn_Amt),
                            dr.Trn_Ext_Data2, "0", "BDT", 1, Convert.ToDecimal(dr.Trn_Amt), "", "", "", "", "", "", "", "", "", "", "",
                            (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "",
                            Convert.ToDateTime(dr.Trn_Date), dr.Tran_Acc_Name, DateTime.Now, "ADM", "T", "",
                            Convert.ToDateTime(dr.Trn_Date), dtHoldJv[0].Trn_Flag, "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(dr.Trn_Amt), dr.Trn_Adr_Code,
                            dr.Trn_Dc_No, dr.Trn_Grn_No, "N", 1, 0, "", "", "", "J", 0, "1", dtHoldJv[0].Trn_Flag);

                        jvLno++;
                    }

                    taHold.DeleteAccJvHold(txtTranRefNo.Text.ToString());

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Approved successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnRej.Visible = false;
                    BtnAppr.Visible = false;
                }
                else
                {
                    myTran.Rollback();
                    tblMsg.Rows[0].Cells[0].InnerText = "Voucher Data Not Found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
    }
}