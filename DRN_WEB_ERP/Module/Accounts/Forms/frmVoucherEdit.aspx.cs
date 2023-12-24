using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmVoucherEdit : System.Web.UI.Page
    {
        decimal totDrAmt = 0;
        decimal totCrAmt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            if (Request.QueryString["JvRefNoEdit"] != null)
            {
                try
                {                    
                    LoadVoucherData(Request.QueryString["JvRefNoEdit"].ToString());

                }
                catch (Exception ex)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
            else
            {
                //txtTranDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                //var taAcc = new tbl_Acc_Fa_TeTableAdapter();
                //var dtNewMaxAccRef = taAcc.GetMaxRefNo("JV", Convert.ToDateTime(txtTranDate.Text.Trim()).Year);
                //var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
                //var nextNewAccRefNo = "JV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
                //txtTranRefNo.Text = nextNewAccRefNo.ToString();

                //SetVoucherDetGridData();
                //LoadInitVoucherDetGridData();
            }
        }

        private void LoadVoucherData(string JvRefNo)
        {
            LoadInitVoucherDetGridData();

            var taJvDet = new tbl_Acc_Fa_TeTableAdapter();
            var dtJvDet = taJvDet.GetDataByJvRef(JvRefNo);
            if (dtJvDet.Rows.Count > 0)
            {
                txtTranRefNo.Text = dtJvDet[0].Trn_Ref_No.ToString();
                lblJvCode.Text = dtJvDet[0].Trn_Jrn_Type.ToString();
                txtTranDate.Text = dtJvDet[0].Trn_DATE.ToString("dd/MM/yyyy");

                var dt = new DataTable();
                dt = (DataTable)ViewState["dtVoucherDet"];

                var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                var dtFaTe = taFaTe.GetDataByJvRef(dtJvDet[0].Trn_Ref_No.ToString());
                foreach (dsAccTran.tbl_Acc_Fa_TeRow dr in dtFaTe.Rows)
                {
                    var SL_NO = dr.Trn_Line_No.ToString();
                    var ORG_COA_CODE = dr.Trn_Ac_Code.ToString();
                    var COA_CODE = dr.Trn_Ac_Code.ToString();
                    var COA_NAME = dr.Trn_Ac_Desc.ToString();
                    var TRAN_DATE = dr.Trn_DATE.ToString("dd/MM/yyyy");
                    var TRAN_NARRATION = dr.Trn_Narration.ToString();
                    var TRAN_DEBIT = dr.Trn_Trn_type.ToString() == "D" ? dr.Trn_Amount : 0;
                    var TRAN_CREDIT = dr.Trn_Trn_type.ToString() == "C" ? dr.Trn_Amount : 0;
                    var TRAN_THROUGH = dr.Trn_Match.ToString();
                    var TRAN_CHEQUE = dr.Trn_Cheque_No.ToString();

                    dt.Rows.Add(SL_NO, ORG_COA_CODE, COA_CODE, COA_NAME, TRAN_DATE, TRAN_NARRATION, TRAN_DEBIT.ToString("N2"), TRAN_CREDIT.ToString("N2"), TRAN_THROUGH, TRAN_CHEQUE, "");
                }
                //btnAdd.Enabled = false;
                btnPost.Enabled = false;
                gvVoucherDet.Enabled = true;

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
                    lblTotDrAmt.Text = totDrAmt.ToString("N2");
                    lblTotCrAmt.Text = totCrAmt.ToString("N2");
                    lblDrCrBal.Text = (totDrAmt - totCrAmt).ToString("N2");
                }
                else
                {
                    lblTotDrAmt.Text = "0.00";
                    lblTotCrAmt.Text = "0.00";
                    lblDrCrBal.Text = "0.00";
                }

                //txtSearch.Enabled = false;
                //btnSearch.Enabled = false;
                //btnClearSrch.Visible = true;

                //hfEditStat.Value = "Y";

                Generate_Comments(JvRefNo);
            }
            else
            {
                tblMsg.Rows[0].Cells[0].InnerText = "JV data not found.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        private void Generate_Comments(string JvRefNo)
        {
            var com = new tbl_Tran_ComTableAdapter();
            var dt = new dsProcTran.tbl_Tran_ComDataTable();
            var taEmp = new View_Emp_BascTableAdapter();

            dt = com.GetDataByRefNo(JvRefNo);
            phcomments.Controls.Clear();

            var qtnSeqNo = 0;
            foreach (dsProcTran.tbl_Tran_ComRow dr in dt.Rows)
            {
                qtnSeqNo = dr.Com_Seq_no;

                Module.Procurement.Forms.UserControl.ctlQtnCom ctl = (Module.Procurement.Forms.UserControl.ctlQtnCom)LoadControl("~/Module/Procurement/Forms/UserControl/CtlQtnCom.ascx");

                Label lblname = (Label)ctl.FindControl("lblname");
                Label lbldate = (Label)ctl.FindControl("lbldate");
                HtmlTableCell celcomm = (HtmlTableCell)ctl.FindControl("celcomm");
                Image imgimage = (Image)ctl.FindControl("imgimage");

                var dtEmp = taEmp.GetDataByEmpId(dr.Com_App_Id.ToString());

                imgimage.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + (dtEmp.Rows.Count > 0 ? dtEmp[0].EmpRefNo : "") + "'";

                ctl.ID = "ctl_" + phcomments.Controls.Count.ToString();

                lblname.Text = dr.Com_App_Name.ToString() + " (" + dr.Com_App_Desig.ToString() + ")";
                lbldate.Text = dr.Com_App_Date.ToString();
                celcomm.InnerText = dr.Com_Gen_Com.ToString();

                phcomments.Controls.Add(ctl);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (txtSearch.Text.Trim().Length > 0)
            //    {
            //        var taJvDet = new tbl_Acc_Fa_TeTableAdapter();
            //        var dtJvDet = taJvDet.GetDataByJvRef(txtSearch.Text.Trim());
            //        if (dtJvDet.Rows.Count > 0)
            //        {
            //            var dt = new DataTable();
            //            dt = (DataTable)ViewState["dtVoucherDet"];

            //            var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
            //            var dtFaTe = taFaTe.GetDataByJvRef(dtJvDet[0].Trn_Ref_No.ToString());
            //            foreach (dsAccTran.tbl_Acc_Fa_TeRow dr in dtFaTe.Rows)
            //            {
            //                var SL_NO = dr.Trn_Line_No.ToString();
            //                var COA_CODE = dr.Trn_Ac_Code.ToString();
            //                var COA_NAME = dr.Trn_Ac_Desc.ToString();
            //                var TRAN_DATE = dr.Trn_DATE.ToString("dd/MM/yyyy");
            //                var TRAN_NARRATION = dr.Trn_Narration.ToString();
            //                var TRAN_DEBIT = dr.Trn_Trn_type.ToString() == "D" ? dr.Trn_Amount : 0;
            //                var TRAN_CREDIT = dr.Trn_Trn_type.ToString() == "C" ? dr.Trn_Amount : 0;
            //                var TRAN_THROUGH = dr.Trn_Match.ToString();
            //                var TRAN_CHEQUE = dr.Trn_Cheque_No.ToString();

            //                dt.Rows.Add(SL_NO, COA_CODE, COA_NAME, TRAN_DATE, TRAN_NARRATION, TRAN_DEBIT.ToString("N2"), TRAN_CREDIT.ToString("N2"), TRAN_THROUGH, TRAN_CHEQUE);
            //            }
            //            //btnAdd.Enabled = false;
            //            btnPost.Visible = false;
            //            gvVoucherDet.Enabled = true;

            //            ViewState["dtVoucherDet"] = dt;
            //            SetVoucherDetGridData();

            //            foreach (GridViewRow gr in gvVoucherDet.Rows)
            //            {
            //                var lblDrAmt = ((Label)(gr.FindControl("lblDr"))).Text.ToString();
            //                totDrAmt += Convert.ToDouble(lblDrAmt.Trim());

            //                var lblCrAmt = ((Label)(gr.FindControl("lblCr"))).Text.ToString();
            //                totCrAmt += Convert.ToDouble(lblCrAmt.Trim());
            //            }

            //            if (gvVoucherDet.Rows.Count > 0)
            //            {
            //                txtTranDate.Enabled = true;
            //                lblTotDrAmt.Text = totDrAmt.ToString("N2");
            //                lblTotCrAmt.Text = totCrAmt.ToString("N2");
            //                lblDrCrBal.Text = (totDrAmt - totCrAmt).ToString("N2");
            //            }
            //            else
            //            {
            //                txtTranDate.Enabled = false;
            //                lblTotDrAmt.Text = "0.00";
            //                lblTotCrAmt.Text = "0.00";
            //                lblDrCrBal.Text = "0.00";
            //            }

            //            //txtSearch.Enabled = false;
            //            //btnSearch.Enabled = false;
            //            //btnClearSrch.Visible = true;

            //            //hfEditStat.Value = "Y";
            //        }
            //        else
            //        {
            //            tblMsg.Rows[0].Cells[0].InnerText = "JV data not found.";
            //            tblMsg.Rows[1].Cells[0].InnerText = "";
            //            ModalPopupExtenderMsg.Show();
            //        }
            //    }
            //    else
            //    {
            //        tblMsg.Rows[0].Cells[0].InnerText = "JV data not found.";
            //        tblMsg.Rows[1].Cells[0].InnerText = "";
            //        ModalPopupExtenderMsg.Show();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
            //    tblMsg.Rows[1].Cells[0].InnerText = "";
            //    ModalPopupExtenderMsg.Show();
            //}
        }

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            try
            {
                //txtSearch.Text = "";
                //txtSearch.Enabled = true;
                //btnSearch.Enabled = true;
                //btnClearSrch.Visible = false;
                txtTranDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtNarration.Text = "";
                txtGlAcc.Text = "";
                txtTranAmt.Text = "";

                lblTotDrAmt.Text = "0.00";
                lblTotCrAmt.Text = "0.00";
                lblDrCrBal.Text = "0.00";

                LoadInitVoucherDetGridData();
                SetVoucherDetGridData();

                //btnAdd.Enabled = true;
                btnPost.Enabled = false;
                gvVoucherDet.Enabled = true;
                hfEditDateStat.Value = "N";

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
            dt.Columns.Add("SL_NO", typeof(string));
            dt.Columns.Add("ORG_COA_CODE", typeof(string));
            dt.Columns.Add("COA_CODE", typeof(string));
            dt.Columns.Add("COA_NAME", typeof(string));
            dt.Columns.Add("TRAN_DATE", typeof(string));
            dt.Columns.Add("TRAN_NARRATION", typeof(string));
            dt.Columns.Add("TRAN_DEBIT", typeof(string));
            dt.Columns.Add("TRAN_CREDIT", typeof(string));
            dt.Columns.Add("TRAN_THROUGH", typeof(string));
            dt.Columns.Add("TRAN_CHQ_NO", typeof(string));
            dt.Columns.Add("TRAN_EDIT_LOG", typeof(string));
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
            //Page.Validate("btnAdd");

            //if (!Page.IsValid) return;

            //try
            //{
            //    #region Get Tran GL Data
            //    var tranCoaCode = "";
            //    var tranCoaName = "";
            //    var srchWords = txtGlAcc.Text.Trim().Split(':');
            //    foreach (string word in srchWords)
            //    {
            //        tranCoaCode = word;
            //        break;
            //    }

            //    if (tranCoaCode.Length > 0)
            //    {
            //        var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            //        var dtCoa = taCoa.GetDataByCoaCode(tranCoaCode);
            //        if (dtCoa.Rows.Count > 0)
            //        {
            //            tranCoaCode = dtCoa[0].Gl_Coa_Code.ToString();
            //            tranCoaName = dtCoa[0].Gl_Coa_Name.ToString();
            //        }
            //        else
            //        {
            //            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Account Code.";
            //            tblMsg.Rows[1].Cells[0].InnerText = "";
            //            ModalPopupExtenderMsg.Show();
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Account Code.";
            //        tblMsg.Rows[1].Cells[0].InnerText = "";
            //        ModalPopupExtenderMsg.Show();
            //        return;
            //    }
            //    #endregion

            //    var dt = new DataTable();
            //    dt = (DataTable)ViewState["dtVoucherDet"];

            //    var COA_CODE = tranCoaCode.ToString();
            //    var COA_NAME = tranCoaName.ToString();
            //    var TRAN_NARRATION = txtNarration.Text.Trim();
            //    var TRAN_DEBIT = ddlTranType.SelectedValue.ToString() == "D" ? Convert.ToDouble(txtTranAmt.Text.Trim().Length > 0 ? txtTranAmt.Text.Trim() : "0") : 0;
            //    var TRAN_CREDIT = ddlTranType.SelectedValue.ToString() == "C" ? Convert.ToDouble(txtTranAmt.Text.Trim().Length > 0 ? txtTranAmt.Text.Trim() : "0") : 0;
            //    var TRAN_TROUGH = ddlRcvMode.SelectedItem.ToString();
            //    var TRAN_CHEQUE = txtDocRef.ToString();

            //    dt.Rows.Add(dt.Rows.Count + 1, COA_CODE, COA_NAME, txtTranDate.Text.Trim(), TRAN_NARRATION, TRAN_DEBIT.ToString("N2"), TRAN_CREDIT.ToString("N2"), TRAN_TROUGH, TRAN_CHEQUE);

            //    ViewState["dtVoucherDet"] = dt;
            //    SetVoucherDetGridData();

            //    foreach (GridViewRow gr in gvVoucherDet.Rows)
            //    {
            //        var lblDrAmt = ((Label)(gr.FindControl("lblDr"))).Text.ToString();
            //        totDrAmt += Convert.ToDouble(lblDrAmt.Trim());

            //        var lblCrAmt = ((Label)(gr.FindControl("lblCr"))).Text.ToString();
            //        totCrAmt += Convert.ToDouble(lblCrAmt.Trim());
            //    }

            //    if (gvVoucherDet.Rows.Count > 0)
            //    {
            //        txtTranDate.Enabled = false;
            //        lblTotDrAmt.Text = totDrAmt.ToString("N2");
            //        lblTotCrAmt.Text = totCrAmt.ToString("N2");
            //        lblDrCrBal.Text = (totDrAmt - totCrAmt).ToString("N2");
            //        btnPost.Visible = true;                    
            //    }
            //    else
            //    {
            //        txtTranDate.Enabled = true;
            //        lblTotDrAmt.Text = "0.00";
            //        lblTotCrAmt.Text = "0.00";
            //        lblDrCrBal.Text = "0.00";
            //        btnPost.Visible = false;
            //    }

            //    txtGlAcc.Text = "";
            //    txtTranAmt.Text = "";
            //    txtNarration.Text = "";
            //}
            //catch (Exception ex)
            //{
            //    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
            //    tblMsg.Rows[1].Cells[0].InnerText = "";
            //    ModalPopupExtenderMsg.Show();
            //    return;
            //}
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
                lblTotDrAmt.Text = totDrAmt.ToString("N2");
                lblTotCrAmt.Text = totCrAmt.ToString("N2");
                lblDrCrBal.Text = (totDrAmt - totCrAmt).ToString("N2");
                btnPost.Enabled = true;
            }
            else
            {
                lblTotDrAmt.Text = "0.00";
                lblTotCrAmt.Text = "0.00";
                lblDrCrBal.Text = "0.00";
                btnPost.Enabled = false;
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var taCrReal = new tblSalesCreditRealizeTableAdapter();
            var taComm = new tbl_Tran_ComTableAdapter();
            var taEmp = new View_Emp_BascTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAcc.Connection);

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

                foreach (GridViewRow gr in gvVoucherDet.Rows)
                {
                    var lblDrAmt = ((Label)(gr.FindControl("lblDr"))).Text.ToString();
                    totDrAmt += Convert.ToDecimal(lblDrAmt.Trim());

                    var lblCrAmt = ((Label)(gr.FindControl("lblCr"))).Text.ToString();
                    totCrAmt += Convert.ToDecimal(lblCrAmt.Trim());
                }

                if (lblJvCode.Text != "OPN" || lblJvCode.Text != "ADJ")
                {
                    if ((totDrAmt - totCrAmt) != 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to post Out of Balance Voucher.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                taAcc.AttachTransaction(myTran);
                taCrReal.AttachTransaction(myTran);
                taComm.AttachTransaction(myTran);

                if (hfEditDateStat.Value == "Y")
                {
                    #region Edit Voucher Date
                    DateTime? JvOrgDate = null;
                    var AccPeriod = DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.ToString("yyyy").ToString();
                    var dtJv = taAcc.GetDataByJvRef(txtTranRefNo.Text.ToString());
                    if (dtJv.Rows.Count > 0)
                    {
                        JvOrgDate = dtJv[0].Trn_DATE;
                        taAcc.UpdateVoucherDate(AccPeriod.ToString(), Convert.ToDateTime(txtTranDate.Text), "E", txtTranRefNo.Text);
                    }

                    var dtMaxComSeqNo = taComm.GetMaxComSeqNo(txtTranRefNo.Text);
                    var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                    taComm.InsertTranCom(txtTranRefNo.Text, nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "JV", "EDIT", "(Voucher Edited By: " + empName + ") [Date changed to " + Convert.ToDateTime(JvOrgDate).ToString("dd/MM/yyyy") + " to " + txtTranDate.Text, "", "1", "", "", "", "");

                    hfEditDateStat.Value = "N";

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Voucher Date Edited Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Voucher Ref No: " + txtTranRefNo.Text;
                    ModalPopupExtenderMsg.Show();
                    #endregion
                }
                else
                {
                    foreach (GridViewRow gr in gvVoucherDet.Rows)
                    {
                        var lblSlNo = ((Label)(gr.FindControl("lblSlNo"))).Text.ToString();
                        var lblOrgCoaCode = ((Label)(gr.FindControl("lblOrgCoaCode"))).Text.ToString();
                        var lblCoaCode = ((Label)(gr.FindControl("lblCoaCode"))).Text.ToString();
                        var lblNarration = ((Label)(gr.FindControl("lblNarration"))).Text.ToString();
                        var lblThrough = ((Label)(gr.FindControl("lblThrough"))).Text.ToString();
                        var lblCheque = ((Label)(gr.FindControl("lblCheque"))).Text.ToString();
                        var lblDr = ((Label)(gr.FindControl("lblDr"))).Text.ToString();
                        var lblCr = ((Label)(gr.FindControl("lblCr"))).Text.ToString();
                        var lblEditLog = ((Label)(gr.FindControl("lblEditLog"))).Text.ToString();

                        if (lblEditLog.Trim().Length > 0)
                        {
                            var trnType = "";
                            decimal tranAmt = 0;
                            if (Convert.ToDecimal(lblDr) > 0)
                            {
                                trnType = "D";
                                tranAmt = Convert.ToDecimal(lblDr);
                            }
                            else if (Convert.ToDecimal(lblCr) > 0)
                            {
                                trnType = "C";
                                tranAmt = Convert.ToDecimal(lblCr);
                            }

                            //Verify GL Code
                            var taGlCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                            var dtGlCoa = new dsAccMas.tbl_Acc_Fa_Gl_CoaDataTable();

                            //Verify Customer Code
                            var taCust = new tblSalesPartyAdrTableAdapter();
                            var dtCust = new dsSalesMas.tblSalesPartyAdrDataTable();

                            #region Get Org GL Code
                            var OrgGlCode = "";
                            var OrgGlName = "";
                            var OrgGlType = "";
                            var OrgCustRef = "";
                            dtGlCoa = taGlCoa.GetDataByCoaCode(lblOrgCoaCode.Trim());
                            if (dtGlCoa.Rows.Count > 0)
                            {
                                OrgGlCode = dtGlCoa[0].Gl_Coa_Code.ToString();
                                OrgGlName = dtGlCoa[0].Gl_Coa_Name.ToString();
                                OrgGlType = dtGlCoa[0].Gl_Coa_Type.ToString();
                                if (OrgGlType == "P")
                                {
                                    #region Get Org Cust Code
                                    dtCust = taCust.GetDataByPartyAccRef(lblOrgCoaCode);
                                    if (dtCust.Rows.Count > 0)
                                    {
                                        OrgCustRef = dtCust[0].Par_Adr_Ref.ToString();
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
                                }
                            }
                            else
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Original GL Code.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                            #endregion

                            #region Get New GL Code
                            var NewGlCode = "";
                            var NewGlName = "";
                            var NewGlType = "";
                            var NewCustRef = "";
                            dtGlCoa = taGlCoa.GetDataByCoaCode(lblCoaCode.Trim());
                            if (dtGlCoa.Rows.Count > 0)
                            {
                                NewGlCode = dtGlCoa[0].Gl_Coa_Code.ToString();
                                NewGlName = dtGlCoa[0].Gl_Coa_Name.ToString();
                                NewGlType = dtGlCoa[0].Gl_Coa_Type.ToString();
                                if (NewGlType == "P")
                                {
                                    #region Get New Cust Code
                                    dtCust = taCust.GetDataByPartyAccRef(lblCoaCode);
                                    if (dtCust.Rows.Count > 0)
                                    {
                                        NewCustRef = dtCust[0].Par_Adr_Ref.ToString();
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
                                }
                            }
                            else
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid GL Code.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                            #endregion                                                                                 

                            var dtFaTe = taAcc.GetDataByJvRefAccCodeLno(txtTranRefNo.Text.Trim(), OrgGlCode.Trim(), Convert.ToDouble(lblSlNo));
                            if (dtFaTe.Rows.Count < 0)
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Voucher Data. Ref. " + txtTranRefNo.Text.Trim();
                                tblMsg.Rows[1].Cells[0].InnerText = "GL Acc: " + OrgGlCode + ", LNo: " + lblSlNo;
                                ModalPopupExtenderMsg.Show();
                                return;
                            }

                            if (OrgGlCode.ToString() != NewGlCode.ToString())
                            {
                                if (OrgGlType == "P")
                                {
                                    if (dtFaTe[0].Trn_Trn_type == "C")
                                    {
                                        //---------Reverse Credit Amount
                                        #region Credit Realization Entry
                                        var TranDrAmt = Convert.ToDecimal(dtFaTe[0].Trn_Amount);
                                        var dtCrSum = taAcc.GetTotCrAmt(lblOrgCoaCode);
                                        var crAmt = dtCrSum == null ? 0 : Convert.ToDecimal(dtCrSum);
                                        var dtDrSum = taAcc.GetTotDrAmt(lblOrgCoaCode);
                                        var drAmt = dtDrSum == null ? 0 : Convert.ToDecimal(dtDrSum);
                                        var CrBal = (crAmt - drAmt);

                                        if (CrBal > 0)
                                        {
                                            if ((CrBal - TranDrAmt) < 0)
                                                taCrReal.InsertCreditRealize(txtTranRefNo.Text + "RC_1.1" + "_" + lblSlNo.ToString(), txtTranRefNo.Text, Convert.ToDateTime(txtTranDate.Text.Trim()),
                                                    Convert.ToDecimal(TranDrAmt - CrBal), Convert.ToDecimal((TranDrAmt - CrBal)), OrgCustRef.ToString(), "1", "", "", "", "", "");
                                        }
                                        else
                                        {
                                            taCrReal.InsertCreditRealize(txtTranRefNo.Text + "RC_1.2" + "_" + lblSlNo.ToString(), txtTranRefNo.Text, Convert.ToDateTime(txtTranDate.Text.Trim()),
                                                    Convert.ToDecimal(TranDrAmt), Convert.ToDecimal(TranDrAmt), OrgCustRef.ToString(), "1", "", "", "", "", "");
                                        }
                                        #endregion
                                    }

                                    if (dtFaTe[0].Trn_Trn_type == "D")
                                    {
                                        //---------Reverse Debit Amount
                                        #region Credit Realization Entry
                                        var TranCrAmt = Convert.ToDecimal(dtFaTe[0].Trn_Amount);
                                        var dtCrReal = taCrReal.GetPendChlnByCustRef(OrgCustRef.ToString());
                                        foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                                        {
                                            if (TranCrAmt > dr.Sales_Chln_Due_Amt)
                                            {
                                                TranCrAmt = TranCrAmt - dr.Sales_Chln_Due_Amt;
                                                taCrReal.UpdateCreditRealizeAmt(0, dr.Sales_Chln_Pay_Rcv_Ref + "Edt.Ref: " + txtTranRefNo.Text + " Amt: " + dr.Sales_Chln_Due_Amt.ToString("N2") + ", ", dr.Sales_Chln_Ref);                                                
                                            }
                                            else
                                            {
                                                taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - TranCrAmt, dr.Sales_Chln_Pay_Rcv_Ref + "Edt.Ref: " + txtTranRefNo.Text + " Amt: " + TranCrAmt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                                TranCrAmt = 0;
                                                break;
                                            }
                                        }
                                        #endregion
                                    }
                                }

                                if (NewGlType == "P")
                                {
                                    if (Convert.ToDecimal(lblCr) > 0)
                                    {
                                        //----------Add Credit
                                        #region Credit Realization Entry
                                        var TranCrAmt = Convert.ToDecimal(lblCr);
                                        var dtCrReal = taCrReal.GetPendChlnByCustRef(NewCustRef.ToString());
                                        foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                                        {
                                            if (TranCrAmt > dr.Sales_Chln_Due_Amt)
                                            {
                                                TranCrAmt = TranCrAmt - dr.Sales_Chln_Due_Amt;
                                                taCrReal.UpdateCreditRealizeAmt(0, dr.Sales_Chln_Pay_Rcv_Ref + "Edt.Ref: " + txtTranRefNo.Text + " Amt: " + dr.Sales_Chln_Due_Amt.ToString("N2") + ", ", dr.Sales_Chln_Ref);                                                
                                            }
                                            else
                                            {
                                                taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - TranCrAmt, dr.Sales_Chln_Pay_Rcv_Ref + "Edt.Ref: " + txtTranRefNo.Text + " Amt: " + TranCrAmt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                                TranCrAmt = 0;
                                                break;
                                            }
                                        }
                                        #endregion
                                    }

                                    if (Convert.ToDecimal(lblDr) > 0)
                                    {
                                        //----------Add debit
                                        #region Credit Realization Entry
                                        var TranDrAmt = Convert.ToDecimal(lblDr);
                                        var dtCrSum = taAcc.GetTotCrAmt(lblOrgCoaCode);
                                        var crAmt = dtCrSum == null ? 0 : Convert.ToDecimal(dtCrSum);
                                        var dtDrSum = taAcc.GetTotDrAmt(lblOrgCoaCode);
                                        var drAmt = dtDrSum == null ? 0 : Convert.ToDecimal(dtDrSum);
                                        var CrBal = (crAmt - drAmt);

                                        if (CrBal > 0)
                                        {
                                            if ((CrBal - TranDrAmt) < 0)
                                                taCrReal.InsertCreditRealize(txtTranRefNo.Text + "AD_1.1" + "_" + lblSlNo.ToString(), txtTranRefNo.Text, Convert.ToDateTime(txtTranDate.Text.Trim()),
                                                    Convert.ToDecimal(TranDrAmt - CrBal), Convert.ToDecimal((TranDrAmt - CrBal)), NewCustRef.ToString(), "1", "", "", "", "", "");
                                        }
                                        else
                                        {
                                            taCrReal.InsertCreditRealize(txtTranRefNo.Text + "AD_1.2" + "_" + lblSlNo.ToString(), txtTranRefNo.Text, Convert.ToDateTime(txtTranDate.Text.Trim()),
                                                    Convert.ToDecimal(TranDrAmt), Convert.ToDecimal(TranDrAmt), NewCustRef.ToString(), "1", "", "", "", "", "");
                                        }
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                if (OrgGlType == "P")
                                {
                                    if (dtFaTe[0].Trn_Trn_type == trnType)
                                    {
                                        if (dtFaTe[0].Trn_Trn_type == "C")
                                        {
                                            #region Modify Credit Amount
                                            //---------Revise Credit Amount                                               
                                            if (Convert.ToDecimal(dtFaTe[0].Trn_Amount.ToString("N2")) != Convert.ToDecimal(tranAmt))
                                            {
                                                if (Convert.ToDecimal(tranAmt) > Convert.ToDecimal(dtFaTe[0].Trn_Amount))
                                                {
                                                    #region Credit Realization Entry
                                                    var AddCrAmt = Convert.ToDecimal(tranAmt) - Convert.ToDecimal(dtFaTe[0].Trn_Amount);
                                                    var dtCrReal = taCrReal.GetPendChlnByCustRef(OrgCustRef.ToString());
                                                    foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                                                    {
                                                        if (AddCrAmt > dr.Sales_Chln_Due_Amt)
                                                        {
                                                            AddCrAmt = AddCrAmt - dr.Sales_Chln_Due_Amt;
                                                            taCrReal.UpdateCreditRealizeAmt(0, dr.Sales_Chln_Pay_Rcv_Ref + "Edt.Ref: " + txtTranRefNo.Text.Trim() + " Amt: " + dr.Sales_Chln_Due_Amt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                                        }
                                                        else
                                                        {
                                                            taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - AddCrAmt, dr.Sales_Chln_Pay_Rcv_Ref + "Edt. Ref: " + txtTranRefNo.Text.Trim() + " Amt: " + AddCrAmt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                                            AddCrAmt = 0;
                                                            break;
                                                        }
                                                    }
                                                    #endregion
                                                }

                                                if (Convert.ToDecimal(tranAmt) < Convert.ToDecimal(dtFaTe[0].Trn_Amount))
                                                {
                                                    #region Credit Realization Entry
                                                    var DedCrAmt = Convert.ToDecimal(dtFaTe[0].Trn_Amount) - Convert.ToDecimal(tranAmt);
                                                    var dtCrReal = taCrReal.GetRelizedChlnByCustRef(OrgCustRef.ToString());
                                                    foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                                                    {
                                                        if (DedCrAmt > (dr.Sales_Chln_Amt - dr.Sales_Chln_Due_Amt))
                                                        {
                                                            DedCrAmt = DedCrAmt - (dr.Sales_Chln_Amt - dr.Sales_Chln_Due_Amt);
                                                            taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt + (dr.Sales_Chln_Amt - dr.Sales_Chln_Due_Amt), dr.Sales_Chln_Pay_Rcv_Ref + "Rev.Ref: " + txtTranRefNo.Text.Trim() + " Amt: " + dr.Sales_Chln_Due_Amt + (dr.Sales_Chln_Amt - dr.Sales_Chln_Due_Amt).ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                                        }
                                                        else
                                                        {
                                                            taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt + DedCrAmt, dr.Sales_Chln_Pay_Rcv_Ref + "Rev.Ref: " + txtTranRefNo.Text.Trim() + " Amt: " + (dr.Sales_Chln_Due_Amt + DedCrAmt).ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                                            DedCrAmt = 0;
                                                            break;
                                                        }
                                                    }
                                                    #endregion
                                                }
                                            }
                                            #endregion
                                        }

                                        if (dtFaTe[0].Trn_Trn_type == "D")
                                        {
                                            #region Modify Debit Amount
                                            //---------Revise Debit Amount
                                            if (Convert.ToDecimal(dtFaTe[0].Trn_Amount.ToString("N2")) != Convert.ToDecimal(tranAmt))
                                            {
                                                if (Convert.ToDecimal(tranAmt) > Convert.ToDecimal(dtFaTe[0].Trn_Amount))
                                                {
                                                    #region Credit Realization Entry
                                                    var AddDrAmt = Convert.ToDecimal(tranAmt) - Convert.ToDecimal(dtFaTe[0].Trn_Amount);
                                                    var dtCrSum = taAcc.GetTotCrAmt(lblOrgCoaCode);
                                                    var crAmt = dtCrSum == null ? 0 : Convert.ToDecimal(dtCrSum);
                                                    var dtDrSum = taAcc.GetTotDrAmt(lblOrgCoaCode);
                                                    var drAmt = dtDrSum == null ? 0 : Convert.ToDecimal(dtDrSum);
                                                    var CrBal = (crAmt - drAmt);

                                                    if (CrBal > 0)
                                                    {
                                                        if ((CrBal - AddDrAmt) < 0)
                                                            taCrReal.InsertCreditRealize(txtTranRefNo.Text + "RD_2.1" + "_" + lblSlNo.ToString(), txtTranRefNo.Text, Convert.ToDateTime(txtTranDate.Text.Trim()),
                                                                Convert.ToDecimal(AddDrAmt - CrBal), Convert.ToDecimal((AddDrAmt - CrBal)), OrgCustRef.ToString(), "1", "", "", "", "", "");
                                                    }
                                                    else
                                                    {
                                                        taCrReal.InsertCreditRealize(txtTranRefNo.Text + "RD_2.2" + "_" + lblSlNo.ToString(), txtTranRefNo.Text, Convert.ToDateTime(txtTranDate.Text.Trim()),
                                                                Convert.ToDecimal(AddDrAmt), Convert.ToDecimal(AddDrAmt), OrgCustRef.ToString(), "1", "", "", "", "", "");
                                                    }
                                                    #endregion
                                                }
                                                if (Convert.ToDecimal(tranAmt) < Convert.ToDecimal(dtFaTe[0].Trn_Amount))
                                                {
                                                    #region Credit Realization Entry
                                                    var DedCrAmt = Convert.ToDecimal(tranAmt) - Convert.ToDecimal(dtFaTe[0].Trn_Amount);
                                                    var dtCrReal = taCrReal.GetPendChlnByCustRef(OrgCustRef.ToString());
                                                    foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                                                    {
                                                        if (DedCrAmt > dr.Sales_Chln_Due_Amt)
                                                        {
                                                            DedCrAmt = DedCrAmt - dr.Sales_Chln_Due_Amt;
                                                            taCrReal.UpdateCreditRealizeAmt(0, dr.Sales_Chln_Pay_Rcv_Ref + "Edt.Ref: " + txtTranRefNo.Text.Trim() + " Amt: " + dr.Sales_Chln_Due_Amt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                                        }
                                                        else
                                                        {
                                                            taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - DedCrAmt, dr.Sales_Chln_Pay_Rcv_Ref + "Edt. Ref: " + txtTranRefNo.Text.Trim() + " Amt: " + DedCrAmt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                                            DedCrAmt = 0;
                                                            break;
                                                        }
                                                    }
                                                    #endregion
                                                }
                                            }
                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        if (dtFaTe[0].Trn_Trn_type == "C")
                                        {
                                            //---------Reverse Credit Amount
                                            #region Credit Realization Entry
                                            var TranDrAmt = Convert.ToDecimal(dtFaTe[0].Trn_Amount);
                                            var dtCrSum = taAcc.GetTotCrAmt(lblOrgCoaCode);
                                            var crAmt = dtCrSum == null ? 0 : Convert.ToDecimal(dtCrSum);
                                            var dtDrSum = taAcc.GetTotDrAmt(lblOrgCoaCode);
                                            var drAmt = dtDrSum == null ? 0 : Convert.ToDecimal(dtDrSum);
                                            var CrBal = (crAmt - drAmt);

                                            if (CrBal > 0)
                                            {
                                                if ((CrBal - TranDrAmt) < 0)
                                                    taCrReal.InsertCreditRealize(txtTranRefNo.Text + "RC_3.1" + "_" + lblSlNo.ToString(), txtTranRefNo.Text, Convert.ToDateTime(txtTranDate.Text.Trim()),
                                                        Convert.ToDecimal(TranDrAmt - CrBal), Convert.ToDecimal((TranDrAmt - CrBal)), OrgCustRef.ToString(), "1", "", "", "", "", "");
                                            }
                                            else
                                            {
                                                taCrReal.InsertCreditRealize(txtTranRefNo.Text + "RD_3.2" + "_" + lblSlNo.ToString(), txtTranRefNo.Text, Convert.ToDateTime(txtTranDate.Text.Trim()),
                                                        Convert.ToDecimal(TranDrAmt), Convert.ToDecimal(TranDrAmt), OrgCustRef.ToString(), "1", "", "", "", "", "");
                                            }
                                            #endregion
                                        }

                                        if (dtFaTe[0].Trn_Trn_type == "D")
                                        {
                                            //---------Reverse Debit Amount
                                            #region Credit Realization Entry
                                            var TranCrAmt = Convert.ToDecimal(dtFaTe[0].Trn_Amount);
                                            var dtCrReal = taCrReal.GetPendChlnByCustRef(OrgCustRef.ToString());
                                            foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                                            {
                                                if (TranCrAmt > dr.Sales_Chln_Due_Amt)
                                                {
                                                    TranCrAmt = TranCrAmt - dr.Sales_Chln_Due_Amt;
                                                    taCrReal.UpdateCreditRealizeAmt(0, dr.Sales_Chln_Pay_Rcv_Ref + "Edt.Ref: " + txtTranRefNo.Text + " Amt: " + dr.Sales_Chln_Due_Amt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                                }
                                                else
                                                {
                                                    taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - TranCrAmt, dr.Sales_Chln_Pay_Rcv_Ref + "Edt.Ref: " + txtTranRefNo.Text + " Amt: " + TranCrAmt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                                    TranCrAmt = 0;
                                                    break;
                                                }
                                            }
                                            #endregion
                                        }

                                        if (Convert.ToDecimal(lblCr) > 0)
                                        {
                                            //----------Add Credit
                                            #region Credit Realization Entry
                                            var TranCrAmt = Convert.ToDecimal(lblCr);
                                            var dtCrReal = taCrReal.GetPendChlnByCustRef(OrgCustRef.ToString());
                                            foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                                            {
                                                if (TranCrAmt > dr.Sales_Chln_Due_Amt)
                                                {
                                                    TranCrAmt = TranCrAmt - dr.Sales_Chln_Due_Amt;
                                                    taCrReal.UpdateCreditRealizeAmt(0, dr.Sales_Chln_Pay_Rcv_Ref + "Edt.Ref: " + txtTranRefNo.Text + " Amt: " + dr.Sales_Chln_Due_Amt.ToString("N2") + ", ", dr.Sales_Chln_Ref);                                                    
                                                }
                                                else
                                                {
                                                    taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - TranCrAmt, dr.Sales_Chln_Pay_Rcv_Ref + "Edt.Ref: " + txtTranRefNo.Text + " Amt: " + TranCrAmt.ToString("N2") + ", ", dr.Sales_Chln_Ref);
                                                    TranCrAmt = 0;
                                                    break;
                                                }
                                            }
                                            #endregion
                                        }

                                        if (Convert.ToDecimal(lblDr) > 0)
                                        {
                                            //----------Add debit
                                            #region Credit Realization Entry
                                            var TranDrAmt = Convert.ToDecimal(lblDr);
                                            var dtCrSum = taAcc.GetTotCrAmt(lblOrgCoaCode);
                                            var crAmt = dtCrSum == null ? 0 : Convert.ToDecimal(dtCrSum);
                                            var dtDrSum = taAcc.GetTotDrAmt(lblOrgCoaCode);
                                            var drAmt = dtDrSum == null ? 0 : Convert.ToDecimal(dtDrSum);
                                            var CrBal = (crAmt - drAmt);

                                            if (CrBal > 0)
                                            {
                                                if ((CrBal - TranDrAmt) < 0)
                                                    taCrReal.InsertCreditRealize(txtTranRefNo.Text + "AD_3.1" + "_" + lblSlNo.ToString(), txtTranRefNo.Text, Convert.ToDateTime(txtTranDate.Text.Trim()),
                                                        Convert.ToDecimal(TranDrAmt - CrBal), Convert.ToDecimal((TranDrAmt - CrBal)), OrgCustRef.ToString(), "1", "", "", "", "", "");
                                            }
                                            else
                                            {
                                                taCrReal.InsertCreditRealize(txtTranRefNo.Text + "AD_3.2" + "_" + lblSlNo.ToString(), txtTranRefNo.Text, Convert.ToDateTime(txtTranDate.Text.Trim()),
                                                        Convert.ToDecimal(TranDrAmt), Convert.ToDecimal(TranDrAmt), OrgCustRef.ToString(), "1", "", "", "", "", "");
                                            }
                                            #endregion
                                        }
                                    }
                                }
                            }

                            taAcc.UpdateVoucher(lblCoaCode, lblNarration, trnType, tranAmt, lblThrough, lblCheque, tranAmt, NewGlName, NewGlType, "E", tranAmt, txtTranRefNo.Text, lblOrgCoaCode, Convert.ToDouble(lblSlNo));

                            var dtMaxComSeqNo = taComm.GetMaxComSeqNo(txtTranRefNo.Text);
                            var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                            taComm.InsertTranCom(txtTranRefNo.Text, nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "JV", "EDIT", lblEditLog, "", "1", "", "", "", "");
                        }
                    }
                    
                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Voucher Edited Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Voucher Ref No: " + txtTranRefNo.Text;
                    ModalPopupExtenderMsg.Show();
                }

                LoadVoucherData(txtTranRefNo.Text);
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
            //var taNewAcc = new tbl_Acc_Fa_TeTableAdapter();
            //var dtNewMaxAccRef = taNewAcc.GetMaxRefNo("JV", Convert.ToDateTime(txtTranDate.Text.Trim()).Year);
            //var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
            //var nextNewAccRefNo = "JV" + (Convert.ToDateTime(txtTranDate.Text.Trim()).Month.ToString("00") + Convert.ToDateTime(txtTranDate.Text.Trim()).ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
            //txtTranRefNo.Text = nextNewAccRefNo.ToString();
        }

        protected void gvVoucherDet_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvVoucherDet.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    lblLineNo.Text = ((Label)gvVoucherDet.Rows[indx].FindControl("lblSlNo")).Text;

                    var glCode = ((Label)gvVoucherDet.Rows[indx].FindControl("lblCoaCode")).Text;
                    var glName = ((Label)gvVoucherDet.Rows[indx].FindControl("lblCoaName")).Text;
                    txtGlAcc.Text = glCode + ":" + glName;

                    var TranType = "0";
                    decimal TranAmt = 0;
                    if (Convert.ToDecimal(((Label)gvVoucherDet.Rows[indx].FindControl("lblDr")).Text) > 0)
                    {
                        TranType = "D";
                        TranAmt = Convert.ToDecimal(((Label)gvVoucherDet.Rows[indx].FindControl("lblDr")).Text);
                    }
                    else if (Convert.ToDecimal(((Label)gvVoucherDet.Rows[indx].FindControl("lblCr")).Text) > 0)
                    {
                        TranType = "C";
                        TranAmt = Convert.ToDecimal(((Label)gvVoucherDet.Rows[indx].FindControl("lblCr")).Text);
                    }
                    //ddlTranType.SelectedValue = TranType.ToString();
                    ddlTranType.SelectedIndex = ddlTranType.Items.IndexOf(ddlTranType.Items.FindByValue(TranType.ToString()));

                    txtTranAmt.Text = TranAmt.ToString("N2");

                    txtNarration.Text = ((Label)gvVoucherDet.Rows[indx].FindControl("lblNarration")).Text;

                    txtDocRef.Text = ((Label)gvVoucherDet.Rows[indx].FindControl("lblCheque")).Text;

                    ddlRcvMode.SelectedIndex = ddlRcvMode.Items.IndexOf(ddlRcvMode.Items.FindByText(((Label)gvVoucherDet.Rows[indx].FindControl("lblThrough")).Text));

                    btnEdit.Enabled = true;

                    optEdit.Enabled = true;
                }
                catch (Exception ex)
                {
                    lblLineNo.Text = "";

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var rowNum = Convert.ToInt32(lblLineNo.Text) - 1;

            if (rowNum == -1) return;

            if (optEdit.SelectedValue == "0") return;

            var dt = new DataTable();
            dt = (DataTable)ViewState["dtVoucherDet"];

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

            var TranType = "0";
            decimal TranAmt = 0;
            switch (optEdit.SelectedValue.ToString())
            {
                case "0":
                    dt.Rows[rowNum]["TRAN_EDIT_LOG"] = "";
                    break;

                case "1":
                    foreach (DataRow dr in dt.Rows)
                    {                        
                        dr["TRAN_EDIT_LOG"] = dr["TRAN_EDIT_LOG"].ToString() + " *[Date changed " + dr["TRAN_DATE"].ToString() + " to " + txtTranDate.Text + "]";
                        dr["TRAN_DATE"] = txtTranDate.Text;
                    }
                    break;

                case "2":
                    dt.Rows[rowNum]["TRAN_EDIT_LOG"] = dt.Rows[rowNum]["TRAN_EDIT_LOG"].ToString() + " *[L#" + dt.Rows[rowNum]["SL_NO"].ToString() + "] [GL Acc changed " + dt.Rows[rowNum]["COA_CODE"].ToString() + ":" + dt.Rows[rowNum]["COA_NAME"].ToString() + " to " + tranCoaCode.ToString() + ":" + tranCoaName.ToString() + "]";
                    break;

                case "3":
                    if (Convert.ToDecimal(Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString())) > 0)
                        TranType = "D";
                    else if (Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString()) > 0)
                        TranType = "C";
                    dt.Rows[rowNum]["TRAN_EDIT_LOG"] = dt.Rows[rowNum]["TRAN_EDIT_LOG"].ToString() + " *[L#" + dt.Rows[rowNum]["SL_NO"].ToString() + "] [Type changed " + TranType.ToString() + " to " + ddlTranType.SelectedValue.ToString() + "]";
                    break;

                case "4":
                    if (Convert.ToDecimal(Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString())) > 0)
                        TranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString());
                    else if (Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString()) > 0)
                        TranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString());
                    dt.Rows[rowNum]["TRAN_EDIT_LOG"] = dt.Rows[rowNum]["TRAN_EDIT_LOG"].ToString() + " *[L#" + dt.Rows[rowNum]["SL_NO"].ToString() + "] [Amount changed " + TranAmt.ToString("N2") + " to " + txtTranAmt.Text + "]";
                    break;

                case "5":
                    dt.Rows[rowNum]["TRAN_EDIT_LOG"] = dt.Rows[rowNum]["TRAN_EDIT_LOG"].ToString() + " *[L#" + dt.Rows[rowNum]["SL_NO"].ToString() + "] [Narration changed " + dt.Rows[rowNum]["TRAN_NARRATION"].ToString() + " to " + txtNarration.Text + "]";
                    break;

                case "6":
                    dt.Rows[rowNum]["TRAN_EDIT_LOG"] = dt.Rows[rowNum]["TRAN_EDIT_LOG"].ToString() + " *[L#" + dt.Rows[rowNum]["SL_NO"].ToString() + "] [Through changed " + dt.Rows[rowNum]["TRAN_THROUGH"].ToString() + " to " + ddlRcvMode.SelectedItem.ToString() + "]";
                    break;

                case "7":
                    dt.Rows[rowNum]["TRAN_EDIT_LOG"] = dt.Rows[rowNum]["TRAN_EDIT_LOG"].ToString() + " *[L#" + dt.Rows[rowNum]["SL_NO"].ToString() + "] [Doc. Ref. changed " + dt.Rows[rowNum]["TRAN_CHQ_NO"].ToString() + " to " + txtDocRef.Text + "]";
                    break;

                default:
                    break;
            }

            dt.Rows[rowNum]["COA_CODE"] = tranCoaCode.ToString();
            dt.Rows[rowNum]["COA_NAME"] = tranCoaName.ToString();
            dt.Rows[rowNum]["TRAN_DATE"] = txtTranDate.Text;
            dt.Rows[rowNum]["TRAN_NARRATION"] = txtNarration.Text.Trim();
            dt.Rows[rowNum]["TRAN_DEBIT"] = ddlTranType.SelectedValue.ToString() == "D" ? Convert.ToDecimal(txtTranAmt.Text.Trim().Length > 0 ? txtTranAmt.Text.Trim() : "0") : 0;
            dt.Rows[rowNum]["TRAN_CREDIT"] = ddlTranType.SelectedValue.ToString() == "C" ? Convert.ToDecimal(txtTranAmt.Text.Trim().Length > 0 ? txtTranAmt.Text.Trim() : "0") : 0;
            dt.Rows[rowNum]["TRAN_THROUGH"] = ddlRcvMode.SelectedItem.ToString();
            dt.Rows[rowNum]["TRAN_CHQ_NO"] = txtDocRef.Text;

            dt.AcceptChanges();

            gvVoucherDet.EditIndex = -1;
            SetVoucherDetGridData();            

            txtGlAcc.Text = "";
            ddlTranType.SelectedIndex = 0;
            txtTranAmt.Text = "";
            txtNarration.Text = "";
            ddlRcvMode.SelectedIndex = 0;
            txtDocRef.Text = "";
            lblLineNo.Text = "";

            txtTranDate.Enabled = false;
            txtGlAcc.Enabled = false;
            ddlTranType.Enabled = false;
            txtTranAmt.Enabled = false;
            txtNarration.Enabled = false;
            ddlRcvMode.Enabled = false;
            txtDocRef.Enabled = false;

            btnEdit.Enabled = false;

            optEdit.SelectedValue = "0";
            optEdit.Enabled = false;

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
                btnPost.Enabled = true;
            }
            else
            {
                txtTranDate.Enabled = true;
                lblTotDrAmt.Text = "0.00";
                lblTotCrAmt.Text = "0.00";
                lblDrCrBal.Text = "0.00";
                btnPost.Enabled = false;
            }
        }

        protected void gvVoucherDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (optEdit.SelectedValue.ToString())
            {
                case "0":
                    {
                        var rowNum = Convert.ToInt32(lblLineNo.Text) - 1;

                        if (rowNum == -1) return;

                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtVoucherDet"];

                        txtTranDate.Text = Convert.ToDateTime(dt.Rows[rowNum]["TRAN_DATE"].ToString()).ToString("dd/MM/yyyy");
                        txtGlAcc.Text = dt.Rows[rowNum]["COA_CODE"].ToString() + ":" + dt.Rows[rowNum]["COA_NAME"].ToString();                        
                        txtNarration.Text = dt.Rows[rowNum]["TRAN_NARRATION"].ToString();
                        txtDocRef.Text = dt.Rows[rowNum]["TRAN_CHQ_NO"].ToString();                        

                        var trnType = "0";
                        decimal tranAmt = 0;
                        if (Convert.ToDecimal(Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString())) > 0)
                        {
                            trnType = "D";
                            tranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString());
                        }
                        else if (Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString()) > 0)
                        {
                            trnType = "C";
                            tranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString());
                        }
                        ddlTranType.SelectedIndex = ddlTranType.Items.IndexOf(ddlTranType.Items.FindByValue(trnType.ToString()));

                        ddlRcvMode.SelectedIndex = ddlRcvMode.Items.IndexOf(ddlRcvMode.Items.FindByValue(dt.Rows[rowNum]["TRAN_THROUGH"].ToString()));

                        txtTranAmt.Text = tranAmt.ToString("N2");

                        txtTranDate.Enabled = false;
                        txtGlAcc.Enabled = false;
                        ddlTranType.Enabled=false;
                        txtTranAmt.Enabled = false;
                        txtNarration.Enabled = false;
                        ddlRcvMode.Enabled = false;
                        txtDocRef.Enabled = false;
                        hfEditDateStat.Value = "N";
                        break;
                    }
                case "1":
                    {
                        var rowNum = Convert.ToInt32(lblLineNo.Text) - 1;

                        if (rowNum == -1) return;

                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtVoucherDet"];

                        txtTranDate.Text = Convert.ToDateTime(dt.Rows[rowNum]["TRAN_DATE"].ToString()).ToString("dd/MM/yyyy");
                        txtGlAcc.Text = dt.Rows[rowNum]["COA_CODE"].ToString() + ":" + dt.Rows[rowNum]["COA_NAME"].ToString();                        
                        txtNarration.Text = dt.Rows[rowNum]["TRAN_NARRATION"].ToString();
                        txtDocRef.Text = dt.Rows[rowNum]["TRAN_CHQ_NO"].ToString();                        

                        var trnType = "0";
                        decimal tranAmt = 0;
                        if (Convert.ToDecimal(Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString())) > 0)
                        {
                            trnType = "D";
                            tranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString());
                        }
                        else if (Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString()) > 0)
                        {
                            trnType = "C";
                            tranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString());
                        }
                        ddlTranType.SelectedIndex = ddlTranType.Items.IndexOf(ddlTranType.Items.FindByValue(trnType.ToString()));

                        ddlRcvMode.SelectedIndex = ddlRcvMode.Items.IndexOf(ddlRcvMode.Items.FindByValue(dt.Rows[rowNum]["TRAN_THROUGH"].ToString()));

                        txtTranAmt.Text = tranAmt.ToString("N2");

                        txtTranDate.Enabled = true;
                        txtGlAcc.Enabled = false;
                        ddlTranType.Enabled=false;
                        txtTranAmt.Enabled = false;
                        txtNarration.Enabled = false;
                        ddlRcvMode.Enabled = false;
                        txtDocRef.Enabled = false;
                        hfEditDateStat.Value = "Y";
                        break;
                    }
                case "2":
                    {
                        var rowNum = Convert.ToInt32(lblLineNo.Text) - 1;

                        if (rowNum == -1) return;

                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtVoucherDet"];

                        txtTranDate.Text = Convert.ToDateTime(dt.Rows[rowNum]["TRAN_DATE"].ToString()).ToString("dd/MM/yyyy");
                        txtGlAcc.Text = dt.Rows[rowNum]["COA_CODE"].ToString() + ":" + dt.Rows[rowNum]["COA_NAME"].ToString();
                        txtNarration.Text = dt.Rows[rowNum]["TRAN_NARRATION"].ToString();
                        txtDocRef.Text = dt.Rows[rowNum]["TRAN_CHQ_NO"].ToString();

                        var trnType = "0";
                        decimal tranAmt = 0;
                        if (Convert.ToDecimal(Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString())) > 0)
                        {
                            trnType = "D";
                            tranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString());
                        }
                        else if (Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString()) > 0)
                        {
                            trnType = "C";
                            tranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString());
                        }
                        ddlTranType.SelectedIndex = ddlTranType.Items.IndexOf(ddlTranType.Items.FindByValue(trnType.ToString()));

                        ddlRcvMode.SelectedIndex = ddlRcvMode.Items.IndexOf(ddlRcvMode.Items.FindByValue(dt.Rows[rowNum]["TRAN_THROUGH"].ToString()));

                        txtTranAmt.Text = tranAmt.ToString("N2");

                        txtTranDate.Enabled = false;
                        txtGlAcc.Enabled = true;
                        ddlTranType.Enabled = false;
                        txtTranAmt.Enabled = false;
                        txtNarration.Enabled = false;
                        ddlRcvMode.Enabled = false;
                        txtDocRef.Enabled = false;
                        hfEditDateStat.Value = "N";
                        break;
                    }
                case "3":
                    {
                        var rowNum = Convert.ToInt32(lblLineNo.Text) - 1;

                        if (rowNum == -1) return;

                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtVoucherDet"];

                        txtTranDate.Text = Convert.ToDateTime(dt.Rows[rowNum]["TRAN_DATE"].ToString()).ToString("dd/MM/yyyy");
                        txtGlAcc.Text = dt.Rows[rowNum]["COA_CODE"].ToString() + ":" + dt.Rows[rowNum]["COA_NAME"].ToString();
                        txtNarration.Text = dt.Rows[rowNum]["TRAN_NARRATION"].ToString();
                        txtDocRef.Text = dt.Rows[rowNum]["TRAN_CHQ_NO"].ToString();

                        var trnType = "0";
                        decimal tranAmt = 0;
                        if (Convert.ToDecimal(Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString())) > 0)
                        {
                            trnType = "D";
                            tranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString());
                        }
                        else if (Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString()) > 0)
                        {
                            trnType = "C";
                            tranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString());
                        }
                        ddlTranType.SelectedIndex = ddlTranType.Items.IndexOf(ddlTranType.Items.FindByValue(trnType.ToString()));

                        ddlRcvMode.SelectedIndex = ddlRcvMode.Items.IndexOf(ddlRcvMode.Items.FindByValue(dt.Rows[rowNum]["TRAN_THROUGH"].ToString()));

                        txtTranAmt.Text = tranAmt.ToString("N2");

                        txtTranDate.Enabled = false;
                        txtGlAcc.Enabled = false;
                        ddlTranType.Enabled = true;
                        txtTranAmt.Enabled = false;
                        txtNarration.Enabled = false;
                        ddlRcvMode.Enabled = false;
                        txtDocRef.Enabled = false;
                        hfEditDateStat.Value = "N";
                        break;
                    }
                case "4":
                    {
                        var rowNum = Convert.ToInt32(lblLineNo.Text) - 1;

                        if (rowNum == -1) return;

                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtVoucherDet"];

                        txtTranDate.Text = Convert.ToDateTime(dt.Rows[rowNum]["TRAN_DATE"].ToString()).ToString("dd/MM/yyyy");
                        txtGlAcc.Text = dt.Rows[rowNum]["COA_CODE"].ToString() + ":" + dt.Rows[rowNum]["COA_NAME"].ToString();
                        txtNarration.Text = dt.Rows[rowNum]["TRAN_NARRATION"].ToString();
                        txtDocRef.Text = dt.Rows[rowNum]["TRAN_CHQ_NO"].ToString();

                        var trnType = "0";
                        decimal tranAmt = 0;
                        if (Convert.ToDecimal(Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString())) > 0)
                        {
                            trnType = "D";
                            tranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString());
                        }
                        else if (Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString()) > 0)
                        {
                            trnType = "C";
                            tranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString());
                        }
                        ddlTranType.SelectedIndex = ddlTranType.Items.IndexOf(ddlTranType.Items.FindByValue(trnType.ToString()));

                        ddlRcvMode.SelectedIndex = ddlRcvMode.Items.IndexOf(ddlRcvMode.Items.FindByValue(dt.Rows[rowNum]["TRAN_THROUGH"].ToString()));

                        txtTranAmt.Text = tranAmt.ToString("N2");

                        txtTranDate.Enabled = false;
                        txtGlAcc.Enabled = false;
                        ddlTranType.Enabled = false;
                        txtTranAmt.Enabled = true;
                        txtNarration.Enabled = false;
                        ddlRcvMode.Enabled = false;
                        txtDocRef.Enabled = false;
                        hfEditDateStat.Value = "N";
                        break;
                    }
                case "5":
                    {
                        var rowNum = Convert.ToInt32(lblLineNo.Text) - 1;

                        if (rowNum == -1) return;

                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtVoucherDet"];

                        txtTranDate.Text = Convert.ToDateTime(dt.Rows[rowNum]["TRAN_DATE"].ToString()).ToString("dd/MM/yyyy");
                        txtGlAcc.Text = dt.Rows[rowNum]["COA_CODE"].ToString() + ":" + dt.Rows[rowNum]["COA_NAME"].ToString();
                        txtNarration.Text = dt.Rows[rowNum]["TRAN_NARRATION"].ToString();
                        txtDocRef.Text = dt.Rows[rowNum]["TRAN_CHQ_NO"].ToString();

                        var trnType = "0";
                        decimal tranAmt = 0;
                        if (Convert.ToDecimal(Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString())) > 0)
                        {
                            trnType = "D";
                            tranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString());
                        }
                        else if (Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString()) > 0)
                        {
                            trnType = "C";
                            tranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString());
                        }
                        ddlTranType.SelectedIndex = ddlTranType.Items.IndexOf(ddlTranType.Items.FindByValue(trnType.ToString()));

                        ddlRcvMode.SelectedIndex = ddlRcvMode.Items.IndexOf(ddlRcvMode.Items.FindByValue(dt.Rows[rowNum]["TRAN_THROUGH"].ToString()));

                        txtTranAmt.Text = tranAmt.ToString("N2");

                        txtTranDate.Enabled = false;
                        txtGlAcc.Enabled = false;
                        ddlTranType.Enabled = false;
                        txtTranAmt.Enabled = false;
                        txtNarration.Enabled = true;
                        ddlRcvMode.Enabled = false;
                        txtDocRef.Enabled = false;
                        hfEditDateStat.Value = "N";
                        break;
                    }
                case "6":
                    {
                        var rowNum = Convert.ToInt32(lblLineNo.Text) - 1;

                        if (rowNum == -1) return;

                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtVoucherDet"];

                        txtTranDate.Text = Convert.ToDateTime(dt.Rows[rowNum]["TRAN_DATE"].ToString()).ToString("dd/MM/yyyy");
                        txtGlAcc.Text = dt.Rows[rowNum]["COA_CODE"].ToString() + ":" + dt.Rows[rowNum]["COA_NAME"].ToString();
                        txtNarration.Text = dt.Rows[rowNum]["TRAN_NARRATION"].ToString();
                        txtDocRef.Text = dt.Rows[rowNum]["TRAN_CHQ_NO"].ToString();

                        var trnType = "0";
                        decimal tranAmt = 0;
                        if (Convert.ToDecimal(Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString())) > 0)
                        {
                            trnType = "D";
                            tranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString());
                        }
                        else if (Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString()) > 0)
                        {
                            trnType = "C";
                            tranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString());
                        }
                        ddlTranType.SelectedIndex = ddlTranType.Items.IndexOf(ddlTranType.Items.FindByValue(trnType.ToString()));

                        ddlRcvMode.SelectedIndex = ddlRcvMode.Items.IndexOf(ddlRcvMode.Items.FindByValue(dt.Rows[rowNum]["TRAN_THROUGH"].ToString()));

                        txtTranAmt.Text = tranAmt.ToString("N2");

                        txtTranDate.Enabled = false;
                        txtGlAcc.Enabled = false;
                        ddlTranType.Enabled = false;
                        txtTranAmt.Enabled = false;
                        txtNarration.Enabled = false;
                        ddlRcvMode.Enabled = true;
                        txtDocRef.Enabled = false;
                        hfEditDateStat.Value = "N";
                        break;
                    }
                case "7":
                    {
                        var rowNum = Convert.ToInt32(lblLineNo.Text) - 1;

                        if (rowNum == -1) return;

                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtVoucherDet"];

                        txtTranDate.Text = Convert.ToDateTime(dt.Rows[rowNum]["TRAN_DATE"].ToString()).ToString("dd/MM/yyyy");
                        txtGlAcc.Text = dt.Rows[rowNum]["COA_CODE"].ToString() + ":" + dt.Rows[rowNum]["COA_NAME"].ToString();
                        txtNarration.Text = dt.Rows[rowNum]["TRAN_NARRATION"].ToString();
                        txtDocRef.Text = dt.Rows[rowNum]["TRAN_CHQ_NO"].ToString();

                        var trnType = "0";
                        decimal tranAmt = 0;
                        if (Convert.ToDecimal(Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString())) > 0)
                        {
                            trnType = "D";
                            tranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_DEBIT"].ToString());
                        }
                        else if (Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString()) > 0)
                        {
                            trnType = "C";
                            tranAmt = Convert.ToDecimal(dt.Rows[rowNum]["TRAN_CREDIT"].ToString());
                        }
                        ddlTranType.SelectedIndex = ddlTranType.Items.IndexOf(ddlTranType.Items.FindByValue(trnType.ToString()));

                        ddlRcvMode.SelectedIndex = ddlRcvMode.Items.IndexOf(ddlRcvMode.Items.FindByValue(dt.Rows[rowNum]["TRAN_THROUGH"].ToString()));

                        txtTranAmt.Text = tranAmt.ToString("N2");

                        txtTranDate.Enabled = false;
                        txtGlAcc.Enabled = false;
                        ddlTranType.Enabled = false;
                        txtTranAmt.Enabled = false;
                        txtNarration.Enabled = false;
                        ddlRcvMode.Enabled = false;
                        txtDocRef.Enabled = true;
                        hfEditDateStat.Value = "N";
                        break;
                    }
                default:
                    break;
            }
        }
    }
}