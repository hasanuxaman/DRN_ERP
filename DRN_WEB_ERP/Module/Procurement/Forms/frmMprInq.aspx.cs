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
    public partial class frmMprInq : System.Web.UI.Page
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

        public string GetMrrRef(string mprref, string icode)
        {
            string mrrrefno = "";
            try
            {
                var taPoDet = new tbl_PuTr_PO_DetTableAdapter();
                var dtPoDet = taPoDet.GetDataByMprItem(mprref.ToString(), icode.ToString());
                var porefno = dtPoDet.Rows.Count > 0 ? dtPoDet[0].PO_Det_Ref.ToString() : "";
                if (porefno != "")
                {
                    var taMrr = new tbl_InTr_Trn_DetTableAdapter();
                    var dtMrr = taMrr.GetDataByPoMprItemRef("PO", porefno, mprref.ToString(), icode);
                    mrrrefno = dtMrr.Rows.Count > 0 ? dtMrr[0].Trn_Det_Ref.ToString() : "";
                }
                return mrrrefno;
            }
            catch (Exception ex) { return mrrrefno; }
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
                        txtPoReqRef.Enabled = false;

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
                            dt.Rows.Add(dr.Pr_Det_Ref, dr.Pr_Det_Ref, dr.Pr_Det_Ref, dr.Pr_Det_Icode, dr.Pr_Det_Itm_Desc, dr.Pr_Det_Itm_Uom, dr.Pr_Det_Itm_Uom,
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

            //rptFile = "~/Module/Inventory/Reports/rptInvMrr.rpt";
            rptFile = "~/Module/Procurement/Reports/rptInvMrr.rpt";

            Session["RptDateFrom"] = DateTime.Now.ToString("dd/MM/yyyy");
            Session["RptDateTo"] = DateTime.Now.ToString("dd/MM/yyyy");
            Session["RptFilePath"] = rptFile;
            Session["RptFormula"] = rptSelcFormula;

            var url = "frmShowProcReport.aspx";
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
    }
}