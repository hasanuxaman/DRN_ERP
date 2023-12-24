using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransDetTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;

namespace DRN_WEB_ERP.Module.Transport.Forms
{
    public partial class frmSalesTC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            AutoCompleteExtenderSrchRetailer.ContextKey = "0";

            //TC Header Ref
            var taTcHdr = new tbl_TrTr_TC_HdrTableAdapter();
            var dtGetMaxTcRef = taTcHdr.GetMaxTcRef(DateTime.Now.Year);
            var nextTcRef = dtGetMaxTcRef == null ? DateTime.Now.Year + "000001" : (Convert.ToInt32(dtGetMaxTcRef) + 1).ToString("000000");
            var nextTcRefNo = "ECIL-TC-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextTcRef).ToString("000000");
            txtTcRefNo.Text = nextTcRefNo;
            txtTcDate.Text = DateTime.Now.ToString();

            LoadInitTcDetGridData();
            SetTcDetGridData();
        }

        #region TC Details Gridview
        protected void LoadInitTcDetGridData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DO_REF", typeof(string));
            dt.Columns.Add("DO_REF_NO", typeof(string));
            dt.Columns.Add("DO_DET_LNO", typeof(string));
            dt.Columns.Add("DO_DATE", typeof(string));
            dt.Columns.Add("ITEM_REF", typeof(string));
            dt.Columns.Add("ITEM_NAME", typeof(string));
            dt.Columns.Add("ITEM_UOM", typeof(string));
            dt.Columns.Add("DO_QTY", typeof(string));
            dt.Columns.Add("FREE_BAG_QTY", typeof(string));
            dt.Columns.Add("TOT_DO_QTY", typeof(string));
            dt.Columns.Add("TC_BAL_QTY", typeof(string));
            dt.Columns.Add("TC_LIN_QTY", typeof(string));
            dt.Columns.Add("TC_LIN_RATE", typeof(string));
            dt.Columns.Add("TC_LIN_AMT", typeof(string));
            ViewState["dtTcDet"] = dt;
        }

        protected void SetTcDetGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtTcDet"];

                gvTcDetails.DataSource = dt;
                gvTcDetails.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion

        protected void btnSelectDo_Click(object sender, EventArgs e)
        {
            var taDelOrd = new VIEW_DELIVERY_ORDER_TCTableAdapter();
            var dtDelOrd = taDelOrd.GetDataByPendDoForTC();
            Session["data"] = dtDelOrd;
            SetDoGridData();
            gvPendDoDet.PageIndex = 1;
            ModalPopupExtenderDoList.Show();
        }

        protected void btnPopupDoOk_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = new DataTable();
                dt = (DataTable)ViewState["dtTcDet"];

                foreach (GridViewRow gr in gvPendDoDet.Rows)
                {
                    var chkDo = (CheckBox)gr.FindControl("chkDo");
                    if (chkDo.Checked)
                    {
                        var DO_REF = ((HiddenField)gr.FindControl("hfDoHdrRef")).Value;
                        var DO_REF_NO = ((Label)gr.FindControl("lblDoHdrRefNo")).Text;
                        var DO_DET_LNO = ((HiddenField)gr.FindControl("hfDoDetLno")).Value;
                        var DO_DATE = ((Label)gr.FindControl("lblDoDate")).Text;
                        var ITEM_REF = ((HiddenField)gr.FindControl("hfItemRef")).Value;
                        var ITEM_NAME = ((Label)gr.FindControl("lblItemDesc")).Text;
                        var ITEM_UOM = ((Label)gr.FindControl("lblItemUom")).Text;
                        var DO_QTY = ((Label)gr.FindControl("lblDoQty")).Text;
                        var FREE_BAG_QTY = ((Label)gr.FindControl("lblDoFreeQty")).Text;
                        var TOT_DO_QTY = ((Label)gr.FindControl("lblTotDoQty")).Text;
                        var TC_BAL_QTY = ((Label)gr.FindControl("lblDoBalQty")).Text;
                        var TC_LIN_QTY = ((Label)gr.FindControl("lblDoBalQty")).Text;
                        var TC_LIN_RATE = txtTcRate.Text.Trim().Length <= 0 ? "0" : txtTcRate.Text.Trim();
                        var TC_LIN_AMT = Convert.ToDecimal(TC_LIN_QTY.Length <= 0 ? "0" : TC_LIN_QTY) * Convert.ToDecimal(TC_LIN_RATE);

                        dt.Rows.Add(DO_REF, DO_REF_NO, DO_DET_LNO, DO_DATE, ITEM_REF, ITEM_NAME, ITEM_UOM, DO_QTY, FREE_BAG_QTY, TOT_DO_QTY, TC_BAL_QTY,
                            TC_LIN_QTY, TC_LIN_RATE, TC_LIN_AMT);
                    }
                }

                ViewState["dtTcDet"] = dt;
                SetTcDetGridData();
            }
            catch (Exception ex) { }
        }

        protected void btnSearchDo_Click(object sender, EventArgs e)
        {
            var taDelOrd = new VIEW_DELIVERY_ORDER_TCTableAdapter();
            var dtDelOrd = taDelOrd.GetDataByPendDoForTCSearch(txtSearchDo.Text.Trim());
            Session["data"] = dtDelOrd;
            SetDoGridData();
            ModalPopupExtenderDoList.Show();
        }

        protected void btnSearchDoClear_Click(object sender, EventArgs e)
        {
            txtSearchDo.Text = "";
            var taDelOrd = new VIEW_DELIVERY_ORDER_TCTableAdapter();
            var dtDelOrd = taDelOrd.GetDataByPendDoForTC();
            Session["data"] = dtDelOrd;
            SetDoGridData();
            ModalPopupExtenderDoList.Show();
        }

        protected void gvPendDoDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPendDoDet.PageIndex = e.NewPageIndex;
            SetDoGridData();
            ModalPopupExtenderDoList.Show();
        }

        protected void SetDoGridData()
        {
            var dtItem = Session["data"];
            gvPendDoDet.DataSource = dtItem;
            gvPendDoDet.DataBind();
            gvPendDoDet.SelectedIndex = -1;
        }

        protected void gvPendDoDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
            //    e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
            //}
        }

        protected void gvTcDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var rowNum = e.RowIndex;

                if (rowNum == -1) return;

                var dt = new DataTable();
                dt = (DataTable)ViewState["dtTcDet"];

                dt.Rows[rowNum].Delete();
                dt.AcceptChanges();

                gvTcDetails.EditIndex = -1;
                SetTcDetGridData();
            }
            catch (Exception ex) { }
        }

        protected void txtSearchDealer_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchDealer.Text.Trim().Length <= 0) return;

            try
            {
                var custRef = "";
                var srchWords = txtSearchDealer.Text.Trim().Split(':');
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
                        AutoCompleteExtenderSrchRetailer.ContextKey = custRef.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                AutoCompleteExtenderSrchRetailer.ContextKey = "";
            }
        }

        protected void txtSearchRetailer_TextChanged(object sender, EventArgs e)
        {
            var taRtlr = new tblSalesPartyRtlTableAdapter();
            var taTranLoc = new tbl_TrTr_Loc_MasTableAdapter();

            if (txtSearchRetailer.Text.Trim().Length <= 0) return;

            try
            {
                var rtlRef = "";
                var srchWords = txtSearchRetailer.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    rtlRef = word;
                    break;
                }

                if (rtlRef.Length > 0)
                {
                    var dtRtlr = taRtlr.GetDataByPartyRtlRef(rtlRef.ToString());
                    if (dtRtlr.Rows.Count > 0)
                    {
                        var dtTransLoc = taTranLoc.GetDataByLocRefNo(dtRtlr[0].IsPar_Rtl_Ext_Data4Null() ? "" : dtRtlr[0].Par_Rtl_Ext_Data4.ToString());
                        if (dtTransLoc.Rows.Count > 0)
                        {
                            txtTcLoc.Text = dtTransLoc[0].IsTrTr_Loc_NameNull() ? "" : dtTransLoc[0].TrTr_Loc_Name.ToString();
                            txtTcRate.Text = dtTransLoc[0].IsTrTr_Loc_Ext_Data1Null() ? "0" : dtTransLoc[0].TrTr_Loc_Ext_Data1.ToString();
                        }
                        else
                        {
                            txtTcLoc.Text = "";
                            txtTcRate.Text = "";
                        }

                    }
                    else
                    {
                        txtTcLoc.Text = "";
                        txtTcRate.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                txtTcLoc.Text = "";
                txtTcRate.Text = "";
            }
        }
    }
}