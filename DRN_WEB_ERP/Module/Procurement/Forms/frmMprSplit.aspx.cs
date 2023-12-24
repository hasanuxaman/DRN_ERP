using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmMprSplit : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var curYear = DateTime.Now.Year;
            for (Int64 year = 2014; year <= (curYear); year++)
            {
                cboYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            cboYear.SelectedValue = curYear.ToString();

            var curMonth = DateTime.Now.Month;
            for (int month = 1; month <= 12; month++)
            {
                var monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                cboMonth.Items.Add(new ListItem(monthName.ToString(), month.ToString()));
            }
            cboMonth.Items.Insert(0, new ListItem("-----Select-----", "0"));
            cboMonth.SelectedValue = curMonth.ToString();

            Load_MPR_List();

            LoadInitPoReqDetGridData();
            SetPoReqDetGridData();
        }

        private void Load_MPR_List()
        {
            ClearData();

            var taPrHdr = new tbl_PuTr_Pr_HdrTableAdapter();
            var dtPrHdr = new dsProcTran.tbl_PuTr_Pr_HdrDataTable();

            ListItem lst;

            if (cboMonth.SelectedIndex == 0)
                dtPrHdr = taPrHdr.GetDataByYear(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
            else
                dtPrHdr = taPrHdr.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));

            ddlMprList.Items.Clear();
            foreach (dsProcTran.tbl_PuTr_Pr_HdrRow dr in dtPrHdr.Rows)
            {
                lst = new ListItem();
                lst.Text = dr.Pr_Hdr_Ref.ToString() + ":" + dr.Pr_Hdr_Com1.ToString() + ":[" + dr.Pr_Hdr_Date.ToString("dd/MM/yyyy") + "]";
                lst.Value = dr.Pr_Hdr_Ref.ToString();
                ddlMprList.Items.Add(lst);
            }
            ddlMprList.Items.Insert(0, new ListItem("-----Select-----", "0"));


        }

        #region MPR Details Gridview
        protected void LoadInitPoReqDetGridData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("REQ_HDR_REF", typeof(string));
            dt.Columns.Add("REQ_DET_REF", typeof(string));
            dt.Columns.Add("REQ_DET_REF_NO", typeof(string));
            dt.Columns.Add("REQ_DET_LNO", typeof(string));
            dt.Columns.Add("REQ_ITEM_REF", typeof(string));
            dt.Columns.Add("REQ_ITEM_NAME", typeof(string));
            dt.Columns.Add("REQ_ITEM_UOM_REF", typeof(string));
            dt.Columns.Add("REQ_ITEM_UOM", typeof(string));
            dt.Columns.Add("REQ_STORE_REF", typeof(string));
            dt.Columns.Add("REQ_STORE", typeof(string));
            dt.Columns.Add("REQ_SPEC", typeof(string));           
            dt.Columns.Add("REQ_BRAND", typeof(string));
            dt.Columns.Add("REQ_ORIGIN", typeof(string));
            dt.Columns.Add("REQ_PACKING", typeof(string));
            dt.Columns.Add("REQ_EXP_DT", typeof(string));
            dt.Columns.Add("REQ_LOC_REF", typeof(string));
            dt.Columns.Add("REQ_LOC", typeof(string));
            dt.Columns.Add("REQ_REM", typeof(string));
            dt.Columns.Add("REQ_QTY", typeof(string));
            dt.Columns.Add("REQ_REF", typeof(string));
            dt.Columns.Add("CS_REF", typeof(string));
            ViewState["dtPoReqDet"] = dt;
        }

        protected void SetPoReqDetGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtPoReqDet"];

                gvPoReqDet.DataSource = dt;
                gvPoReqDet.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion

        #region GridData
        public string GetPoRef(string mprref, string icode)
        {
            string porefno = "";
            try
            {
                var taPoDet = new tbl_PuTr_PO_DetTableAdapter();
                var dtPoDet = taPoDet.GetDataByMprItem(mprref.ToString(), icode.ToString());
                porefno = dtPoDet.Rows.Count > 0 ? dtPoDet[0].PO_Det_Ref.ToString() : "";

                return porefno;
            }
            catch (Exception ex) { return porefno; }
        }
        #endregion

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            reportInfo();
            var url = "frmShowProcReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportInfo()
        {
            rptSelcFormula = "{View_PuTr_Pr_Hdr_Det.Pr_Hdr_Ref}='" + ddlMprList.SelectedValue.ToString() + "'";

            rptFile = "~/Module/Procurement/Reports/rptProcMpr.rpt";

            Session["RptDateFrom"] = DateTime.Now.ToString("dd/MM/yyyy");
            Session["RptDateTo"] = DateTime.Now.ToString("dd/MM/yyyy");
            Session["RptFilePath"] = rptFile;
            Session["RptFormula"] = rptSelcFormula;
        }

        private void ClearData()
        {
            //var curYear = DateTime.Now.Year;
            //for (Int64 year = 2014; year <= (curYear); year++)
            //{
            //    cboYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            //}
            //cboYear.SelectedValue = curYear.ToString();

            //var curMonth = DateTime.Now.Month;
            //for (int month = 1; month <= 12; month++)
            //{
            //    var monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            //    cboMonth.Items.Add(new ListItem(monthName.ToString(), month.ToString()));
            //}
            //cboMonth.Items.Insert(0, new ListItem("-----Select-----", "0"));
            //cboMonth.SelectedValue = curMonth.ToString();

            //Load_MPR_List();

            txtPoReqRef.Text = "";            
            txtPoReqDt.Text = "";
            txtPoRefNo.Text = "";
            txtPoReqBy.Text = "";
            lblMsg.Text = "";
            lblMsg.Visible = false;
            lblMsgValidity.Text = "";
            lblMsgValidity.Visible = false;
            optMprList.Items.Clear();
            btnPrint.Enabled = false;
            btnSplit.Enabled = false;

            LoadInitPoReqDetGridData();
            SetPoReqDetGridData();

            //gvPoReqDet.Enabled = true;
        }

        protected void ddlMprList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taPrHdr = new tbl_PuTr_Pr_HdrTableAdapter();
            var taPrDet = new tbl_PuTr_Pr_DetTableAdapter();

            try
            {
                //reportInfo();

                if (ddlMprList.SelectedIndex == 0)
                {
                    ClearData();
                }
                else
                {
                    lblMsg.Text = "";
                    lblMsg.Visible = false;
                    lblMsgValidity.Text = "";
                    lblMsgValidity.Visible = false;
                    optMprList.Items.Clear();
                    var dtPrHdr = taPrHdr.GetDataByPrRef(ddlMprList.SelectedValue.ToString());
                    if (dtPrHdr.Rows.Count > 0)
                    {
                        btnSplit.Enabled = true;

                        txtPoReqRef.Text = dtPrHdr[0].Pr_Hdr_Com2.ToString();
                        txtPoReqDt.Text = dtPrHdr[0].Pr_Hdr_Date.ToString("dd/MM/yyyy");
                        txtPoRefNo.Text = dtPrHdr[0].Pr_Hdr_Ref.ToString();
                        txtPoReqBy.Text = dtPrHdr[0].Pr_Hdr_Acode.ToString() + "::" + dtPrHdr[0].Pr_Hdr_Com1.ToString();

                        if ((DateTime.Now - dtPrHdr[0].Pr_Hdr_Date).Days > 90)
                        {
                            lblMsgValidity.Text = "MPR exceeds 90 days validity.";
                            lblMsgValidity.Visible = true;
                        }

                        LoadInitPoReqDetGridData();
                        SetPoReqDetGridData();

                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtPoReqDet"];
                        var dtPrDet = taPrDet.GetDataByPrRef(ddlMprList.SelectedValue.ToString());
                        foreach (dsProcTran.tbl_PuTr_Pr_DetRow dr in dtPrDet.Rows)
                        {
                            dt.Rows.Add(dr.Pr_Det_Ref, dr.Pr_Det_Ref, dr.Pr_Det_Ref, dr.Pr_Det_Lno, dr.Pr_Det_Icode, dr.Pr_Det_Itm_Desc, dr.Pr_Det_Itm_Uom, dr.Pr_Det_Itm_Uom,
                                        dr.Pr_Det_Str_Code, dr.Pr_Det_Str_Code, dr.Pr_Det_Spec, dr.Pr_Det_Brand, dr.Pr_Det_Origin, dr.Pr_Det_Packing,
                                        dr.Pr_Det_Exp_Dat.ToString("dd/MM/yyyy"), dr.Pr_Det_Use_Loc_Ref, dr.Pr_Det_Use_Loc_name, dr.Pr_Det_Rem,
                                        dr.Pr_Det_Lin_Qty, dr.Pr_Det_Bat_No, dr.Pr_Det_Quot_Ref);
                        }
                        ViewState["dtPoReqDet"] = dt;
                        SetPoReqDetGridData();

                        if (dtPrHdr[0].Pr_Hdr_HPC_Flag == "P")
                        {
                            btnPrint.Enabled = true;
                            //gvPoReqDet.Enabled = false;
                        }
                        else
                        {
                            btnPrint.Enabled = true;
                            //gvPoReqDet.Enabled = true;
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

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_MPR_List();
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_MPR_List();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var taMprHdr = new tbl_PuTr_Pr_HdrTableAdapter();

            try
            {
                if (txtReqRefNo.Text.Trim() == "")
                {
                    ClearData(); 
                    return;
                }

                var dtMprHdr = taMprHdr.GetDataByReqRef(txtReqRefNo.Text.Trim());
                if (dtMprHdr.Rows.Count > 1)
                {
                    optMprList.Items.Clear();
                    //var mprRef = "";
                    foreach (dsProcTran.tbl_PuTr_Pr_HdrRow dr in dtMprHdr.Rows)
                    {
                        //mprRef = mprRef + ", " + dr.Pr_Hdr_Ref.ToString();

                        optMprList.Items.Add(new ListItem(dr.Pr_Hdr_Ref.ToString(), dr.Pr_Hdr_Ref.ToString()));
                    }
                    //mprRef = mprRef.Substring(2, mprRef.Length - 2);
                    lblMsg.Text = "[There are multiple MPR against Req. Ref. No:" + txtReqRefNo.Text.Trim() + ".]"; // {MPR Ref No:- " + mprRef + "}";
                    lblMsg.Visible = true;

                    if ((DateTime.Now - dtMprHdr[0].Pr_Hdr_Date).Days > 90)
                    {
                        lblMsgValidity.Text = "MPR exceeds 90 days validity.";
                        lblMsgValidity.Visible = true;
                    }
                    return;
                }
                
                if (dtMprHdr.Rows.Count > 0)
                {
                    btnSplit.Enabled = true;

                    cboYear.SelectedValue = dtMprHdr[0].Pr_Hdr_Date.Year.ToString();
                    cboMonth.SelectedValue = dtMprHdr[0].Pr_Hdr_Date.Month.ToString();
                    Load_MPR_List();
                    ddlMprList.SelectedValue = dtMprHdr[0].Pr_Hdr_Ref.ToString();
                    ddlMprList_SelectedIndexChanged(sender, e);
                }                
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
            txtReqRefNo.Text = "";

            var curYear = DateTime.Now.Year;
            cboYear.SelectedValue = curYear.ToString();

            var curMonth = DateTime.Now.Month;
            cboMonth.SelectedValue = curMonth.ToString();

            Load_MPR_List();
        }

        protected void lnkCsRef_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            var CsRefNo = ((LinkButton)row.FindControl("lnkCsRef")).Text.Trim();
            Session["CsRefNoPrint"] = CsRefNo.ToString();
            var url = "frmCsInq.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void lnkPoRef_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            var PoRefNo = ((LinkButton)row.FindControl("lnkPoRef")).Text.Trim();
            Session["PoRefNoPrint"] = PoRefNo.ToString();
            var url = "frmPoInquery.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void lnkMrrRef_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            var MrrRefNo = ((LinkButton)row.FindControl("lnkMrrRef")).Text.Trim();

            rptSelcFormula = "{View_InTr_Trn_Hdr_Det_Tran.Trn_Hdr_Ref_No}='" + MrrRefNo.ToString() + "'";

            rptFile = "~/Module/Inventory/Reports/rptInvMrr.rpt";

            Session["RptDateFrom"] = DateTime.Now.ToString("dd/MM/yyyy");
            Session["RptDateTo"] = DateTime.Now.ToString("dd/MM/yyyy");
            Session["RptFilePath"] = rptFile;
            Session["RptFormula"] = rptSelcFormula;

            var url = @"/Module/Inventory/Forms/frmShowInvReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void optMprList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taMprHdr = new tbl_PuTr_Pr_HdrTableAdapter();
            var dtMprHdr = taMprHdr.GetDataByPrRef(optMprList.SelectedValue.ToString());
            if (dtMprHdr.Rows.Count > 0)
            {
                cboYear.SelectedValue = dtMprHdr[0].Pr_Hdr_Date.Year.ToString();
                cboMonth.SelectedValue = dtMprHdr[0].Pr_Hdr_Date.Month.ToString();
                Load_MPR_List();
                ddlMprList.SelectedValue = dtMprHdr[0].Pr_Hdr_Ref.ToString();
                ddlMprList_SelectedIndexChanged(sender, e);
            }                
        }

        protected void lnkViewMpr_Click(object sender, EventArgs e)
        {
            Session["MprRefNoView"] = txtPoRefNo.Text.Trim();
            var url = "frmMpr.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void gvPoReqDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lnkCsRef = ((LinkButton)e.Row.FindControl("lnkCsRef"));
                var txtSplitQty = ((TextBox)e.Row.FindControl("txtSplitQty"));
                var chkSplit = ((CheckBox)e.Row.FindControl("chkSplit"));

                if (lnkCsRef.Text.Trim().Length > 0)
                {
                    txtSplitQty.Enabled = false;
                    chkSplit.Enabled = false;
                }
            }
        }

        protected void btnSplit_Click(object sender, EventArgs e)
        {
            var taPrHdr = new tbl_PuTr_Pr_HdrTableAdapter();
            var taPrDet = new tbl_PuTr_Pr_DetTableAdapter();
            var taComm = new tbl_Tran_ComTableAdapter();
            var taQtn = new tbl_Qtn_DetTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taPrHdr.Connection);

            try
            {
                #region Data Validation
                int chkStat = 0;
                foreach(GridViewRow gr in gvPoReqDet.Rows)
                {
                    var lblPoReqQty = ((Label)gr.FindControl("lblPoReqQty"));
                    var txtSplitQty = ((TextBox)gr.FindControl("txtSplitQty"));
                    var chkSplit = ((CheckBox)gr.FindControl("chkSplit"));
                    if (chkSplit.Checked)
                    {
                        chkStat = 1;
                        if (Convert.ToDouble(txtSplitQty.Text.Trim()) >= Convert.ToDouble(lblPoReqQty.Text.Trim()))
                        {
                            gr.BackColor = System.Drawing.Color.Red;
                            tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to split more than or equel to original MPR qty.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                }

                if (chkStat == 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Select MPR item first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                #region Get MPR Employee Details
                var mprEmpRef = "";
                var mprEmpName = "";
                var srchWords = txtPoReqBy.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    mprEmpRef = word;
                    break;
                }
                if (mprEmpRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(mprEmpRef, out result))
                    {
                        var taEmpGenInfo = new View_Emp_BascTableAdapter();
                        var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRef(Convert.ToInt32(mprEmpRef.ToString()));
                        if (dtEmpGenInfo.Rows.Count > 0)
                        {
                            mprEmpRef = dtEmpGenInfo[0].EmpRefNo;
                            mprEmpName = dtEmpGenInfo[0].EmpName;
                        }
                    }
                }
                #endregion

                var nextMprRefNo = "";

                taPrHdr.AttachTransaction(myTran);
                taPrDet.AttachTransaction(myTran);
                taComm.AttachTransaction(myTran);
                taQtn.AttachTransaction(myTran);

                var dtRefNo = taPrHdr.GetMaxPrRef(Convert.ToDateTime(txtPoReqDt.Text).Year);
                var nextRefNo = (dtRefNo == null || Convert.ToInt32(dtRefNo) == 0) ? 1 : Convert.ToInt32(dtRefNo) + 1;
                nextMprRefNo = "MPR-" + Convert.ToDateTime(txtPoReqDt.Text).Month.ToString("00") + Convert.ToDateTime(txtPoReqDt.Text).ToString("yy") + "-" + nextRefNo.ToString("000000");

                taPrHdr.InsertPrHdr("PR", "MPR", nextMprRefNo, mprEmpRef, mprEmpRef, mprEmpRef, Convert.ToDateTime(txtPoReqDt.Text.Trim()), mprEmpName,
                                    txtPoReqRef.Text.Trim(), "", "", "", "", "", "", "", "", 0, "P", 
                                    Convert.ToDateTime(txtPoReqDt.Text.Trim()).Year + "/" + Convert.ToDateTime(txtPoReqDt.Text.Trim()).Month.ToString("00"),
                                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), DateTime.Now, "", "", "",
                                    Convert.ToDateTime(txtPoReqDt.Text.Trim()),
                                    Convert.ToDateTime(txtPoReqDt.Text.Trim()).Year + "/" + Convert.ToDateTime(txtPoReqDt.Text.Trim()).Month.ToString("00"),
                                    "", "", "", 1, "BDT", 0);

                #region Get Login Employee Details
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var empId = "";
                var empName = "";
                var empDesig = "";
                if (empRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(empRef, out result))
                    {
                        var taEmpGenInfo = new View_Emp_BascTableAdapter();
                        var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                        if (dtEmpGenInfo.Rows.Count > 0)
                        {
                            empRef = dtEmpGenInfo[0].EmpRefNo;
                            empId = dtEmpGenInfo[0].EmpId;
                            empName = dtEmpGenInfo[0].EmpName;
                            empDesig = dtEmpGenInfo[0].DesigName;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Login.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Login.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                var comments_det = "(MPR Splited By:" + empName + ") " + txtPoReqRem.Text.Trim();
                taComm.InsertTranCom(txtPoRefNo.Text.Trim(), 3, DateTime.Now, empId, empName, empDesig, 1, "MPR", "SPL", comments_det, "", "1", "", "", "", "");

                #region Insert MPR Details
                var dt = new DataTable();
                dt = (DataTable)ViewState["dtPoReqDet"];

                short lNo = 1;
                foreach (GridViewRow row in gvPoReqDet.Rows)
                {
                    var lblItemCode = ((Label)(row.FindControl("lblItemCode"))).Text.ToString();
                    var lblItemDesc = ((Label)(row.FindControl("lblItemDesc"))).Text.ToString();
                    var hfPrDetLno = ((HiddenField)(row.FindControl("hfPrDetLno"))).Value.ToString();
                    var hfItemUnit = ((HiddenField)(row.FindControl("hfItemUnit"))).Value.ToString();
                    var lblItemUnit = ((Label)(row.FindControl("lblItemUnit"))).Text.ToString();
                    var hfStore = ((HiddenField)(row.FindControl("hfStore"))).Value.ToString();
                    var lblStore = ((Label)(row.FindControl("lblStore"))).Text.ToString();
                    var lblSpec = ((Label)(row.FindControl("lblSpec"))).Text.ToString();
                    var lblBrand = ((Label)(row.FindControl("lblBrand"))).Text.ToString();
                    var lblOrigin = ((Label)(row.FindControl("lblOrigin"))).Text.ToString();
                    var lblPacking = ((Label)(row.FindControl("lblPacking"))).Text.ToString();
                    var lblExpReqDt = ((HiddenField)(row.FindControl("hfPrDetExpDt"))).Value.ToString();
                    var hfLocation = ((HiddenField)(row.FindControl("hfLocation"))).Value.ToString();
                    var lblLocation = ((Label)(row.FindControl("lblLocation"))).Text.ToString();
                    var lblRemarks = ((Label)(row.FindControl("lblRemarks"))).Text.ToString();
                    var lblPoReqQty = ((Label)(row.FindControl("lblPoReqQty"))).Text.ToString();
                    var hfReqRef = ((HiddenField)(row.FindControl("hfReqRef"))).Value.ToString();
                    var txtSplitQty = (((TextBox)row.FindControl("txtSplitQty"))).Text.Trim();
                    var chkSplit = (((CheckBox)row.FindControl("chkSplit")));

                    if (chkSplit.Checked && txtSplitQty.Length > 0)
                    {
                        taPrDet.InsertPrDet("PR", "MPR", nextMprRefNo, lNo, lblItemCode, lblItemDesc, hfItemUnit,
                                            hfStore, lblSpec, lblBrand, lblOrigin, lblPacking, hfLocation, lblLocation, "", hfReqRef, Convert.ToDateTime(lblExpReqDt),
                                            Convert.ToDateTime(txtPoReqDt.Text.Trim()), Convert.ToDouble(txtSplitQty), 0, Convert.ToDouble(txtSplitQty),
                                            Convert.ToDouble(txtSplitQty), "O", "N", 0, 0, 0, "Req. By:- " + empName + " @" + DateTime.Now.ToString(), "", "P", 1, lblRemarks,
                                            "", "LPO", "", "", "TEN", "", "", "", "", "", "");

                        taPrDet.UpdatePrDetSplitQty(Convert.ToDouble(lblPoReqQty) - Convert.ToDouble(txtSplitQty), Convert.ToDouble(lblPoReqQty) - Convert.ToDouble(txtSplitQty),
                                        ddlMprList.SelectedValue.ToString(), lblItemCode.ToString(), Convert.ToInt16(hfPrDetLno.ToString()));

                        var dtQtn = taQtn.GetDataByMprRefItemCode(ddlMprList.SelectedValue.ToString(), lblItemCode);
                        if (dtQtn.Rows.Count > 0)
                        {
                            if (dtQtn[0].Qtn_Ref_No == "")
                                taQtn.UpdateQtnQty(Convert.ToDouble(lblPoReqQty) - Convert.ToDouble(txtSplitQty), (Convert.ToDecimal(lblPoReqQty) - Convert.ToDecimal(txtSplitQty)) * Convert.ToDecimal(dtQtn[0].Qtn_Itm_Rate), ddlMprList.SelectedValue.ToString(), lblItemCode);
                        }

                        lNo++;
                    }
                }
                #endregion                

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Purchase Requsition Quantity Splited Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "New MPR Ref: " + nextMprRefNo.ToString();
                ModalPopupExtenderMsg.Show();

                //var curYear = DateTime.Now.Year;
                //cboYear.SelectedValue = curYear.ToString();

                //var curMonth = DateTime.Now.Month;
                //cboMonth.SelectedValue = curMonth.ToString();

                //ddlMprList.Items.Clear();

                //ListItem lst;

                //var dtPrHdrNew = taPrHdr.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue), Convert.ToDecimal(cboMonth.SelectedValue));
                //foreach (dsProcTran.tbl_PuTr_Pr_HdrRow dr in dtPrHdrNew.Rows)
                //{
                //    lst = new ListItem();
                //    lst.Text = dr.Pr_Hdr_Ref.ToString() + ":" + dr.Pr_Hdr_Com1.ToString() + ":[" + dr.Pr_Hdr_Date.ToString("dd/MM/yyyy") + "]";
                //    lst.Value = dr.Pr_Hdr_Ref.ToString();
                //    ddlMprList.Items.Add(lst);
                //}
                //ddlMprList.Items.Insert(0, new ListItem("-----New-----", "0"));
                //ddlMprList.SelectedIndex = ddlMprList.Items.IndexOf(ddlMprList.Items.FindByValue(nextMprRefNo.ToString()));

                btnPrint.Enabled = true;

                btnPrint.Enabled = true;
                btnSplit.Enabled = false;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }
    }
}