using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmMpr : System.Web.UI.Page
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

            var taPrHdr = new tbl_PuTr_Pr_HdrTableAdapter();
            var dtRefNo = taPrHdr.GetMaxPrRef(DateTime.Now.Year);
            var nextRefNo = (dtRefNo == null || Convert.ToInt32(dtRefNo) == 0) ? 1 : Convert.ToInt32(dtRefNo) + 1;
            var prefix = "MPR-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy");
            txtPoRefNo.Text = prefix + "-" + nextRefNo.ToString("000000");

            txtPoReqDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtPoExpDt.Text = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");

            var taEmpGenInfo = new View_Emp_BascTableAdapter();
            var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRefAct(Session["sessionEmpRef"] == null ? 0 : Convert.ToInt32(Session["sessionEmpRef"].ToString()));
            if (dtEmpGenInfo.Rows.Count > 0)
                txtPoReqBy.Text = dtEmpGenInfo[0].EmpRefNo.ToString() + ":" + dtEmpGenInfo[0].EmpId.ToString() + ":" + dtEmpGenInfo[0].EmpName.ToString();

            var taItemType = new tbl_InMa_TypeTableAdapter();
            var dtItemType = taItemType.GetDataByAsc();
            cboPoItmType.DataSource = dtItemType;
            cboPoItmType.DataTextField = "Item_Type_Name";
            cboPoItmType.DataValueField = "Item_Type_Code";
            cboPoItmType.DataBind();
            cboPoItmType.Items.Insert(0, new ListItem("-----ALL-----", "0"));
            cboPoItmType.SelectedIndex = 0;

            AutoCompleteExtenderSrchItem.ContextKey = "0";

            //cboPoItmSpec.Items.Insert(0, new ListItem("", "0"));

            var taItemUom = new tbl_InMa_UomTableAdapter();
            var dtItemUom = taItemUom.GetData();
            cboPoItmUom.DataSource = dtItemUom;
            cboPoItmUom.DataTextField = "Uom_Name";
            cboPoItmUom.DataValueField = "Uom_Code";
            cboPoItmUom.DataBind();
            cboPoItmUom.Items.Insert(0, new ListItem("----", "0"));

            var taStore = new tbl_InMa_Str_LocTableAdapter();
            var dtStore = taStore.GetDataByGenStore();
            ddlMprStore.DataSource = dtStore;
            ddlMprStore.DataTextField = "Str_Loc_Name";
            ddlMprStore.DataValueField = "Str_Loc_Ref";
            ddlMprStore.DataBind();
            ddlMprStore.Items.Insert(0, new ListItem("----", "0"));

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
            ddlMprList.Items.Insert(0, new ListItem("-----New-----", "0"));
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

        protected void btnAddPoDet_Click(object sender, EventArgs e)
        {
            Page.Validate("btnAdd");

            if (!Page.IsValid) return;

            try
            {
                #region Data Validation
                var itemRef = "";
                var itemName = "";
                var srchWords = txtPoItemName.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    itemRef = word;
                    break;
                }

                if (itemRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(itemRef, out result))
                    {
                        var taItem = new tbl_InMa_Item_DetTableAdapter();
                        var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(itemRef));
                        if (dtItem.Rows.Count > 0)
                        {
                            itemRef = dtItem[0].Itm_Det_Ref.ToString();
                            itemName = dtItem[0].IsItm_Det_DescNull() ? "0" : dtItem[0].Itm_Det_Desc.ToString();
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                if (hfItemEditStatus.Value == "N")
                {
                    foreach (GridViewRow gr in gvPoReqDet.Rows)
                    {
                        var lblItemCode = ((Label)(gr.FindControl("lblItemCode"))).Text.ToString();
                        if (itemRef.ToString().Trim() == lblItemCode.ToString().Trim())
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = txtPoItemName.Text.Trim() + " already addred to requisition details.";
                            tblMsg.Rows[1].Cells[0].InnerText = "To add more you need to delete existing same item.";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                }

                if (txtPoQty.Text.Trim() == "" || txtPoQty.Text.Trim().Length <= 0 || Convert.ToDouble(txtPoQty.Text.Trim()) <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter valid data.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion                

                var dt = new DataTable();
                dt = (DataTable)ViewState["dtPoReqDet"];

                var REQ_HDR_REF = "0";
                var REQ_DET_REF = "0";
                var REQ_DET_REF_NO = "0";
                var REQ_ITEM_REF = itemRef.ToString();
                var REQ_ITEM_NAME = itemName.ToString();
                var REQ_ITEM_UOM_REF = cboPoItmUom.SelectedValue.ToString();
                var REQ_ITEM_UOM = cboPoItmUom.SelectedItem.ToString();
                var REQ_STORE_REF = ddlMprStore.SelectedValue;
                var REQ_STORE = ddlMprStore.SelectedItem.ToString();
                var REQ_SPEC = txtPoSpec.Text.Trim();
                var REQ_BRAND = txtPoBrand.Text.Trim();
                var REQ_ORIGIN = txtPoOrigin.Text.Trim();
                var REQ_PACKING = txtPoPacking.Text.Trim();
                var REQ_EXP_DT = txtPoExpDt.Text.Trim();
                var REQ_LOC_REF = cboPoLoc.SelectedValue.ToString();
                var REQ_LOC = cboPoLoc.SelectedItem.ToString();
                var REQ_REM = txtPoRem.Text.Trim();
                var REQ_QTY = Convert.ToDouble(txtPoQty.Text.Trim().Length > 0 ? txtPoQty.Text.Trim() : "0");
                var REQ_REF = txtPoReqRef.Text.Trim();

                if (hfItemEditStatus.Value == "N")
                {
                    dt.Rows.Add(REQ_HDR_REF, REQ_DET_REF, REQ_DET_REF_NO, REQ_ITEM_REF, REQ_ITEM_NAME, REQ_ITEM_UOM_REF, REQ_ITEM_UOM, REQ_STORE_REF, REQ_STORE,
                        REQ_SPEC, REQ_BRAND, REQ_ORIGIN, REQ_PACKING, REQ_EXP_DT, REQ_LOC_REF, REQ_LOC, REQ_REM, REQ_QTY, REQ_REF);
                }
                else
                {
                    var rowNum = Convert.ToInt32(hfItemEditRefNo.Value);
                    if (rowNum == -1) return;

                    dt.Rows[rowNum]["REQ_HDR_REF"] = "0";
                    dt.Rows[rowNum]["REQ_DET_REF"] = "0";
                    dt.Rows[rowNum]["REQ_DET_REF_NO"] = "0";
                    dt.Rows[rowNum]["REQ_ITEM_REF"] = REQ_ITEM_REF;
                    dt.Rows[rowNum]["REQ_ITEM_NAME"] = REQ_ITEM_NAME;
                    dt.Rows[rowNum]["REQ_ITEM_UOM_REF"] = REQ_ITEM_UOM_REF;
                    dt.Rows[rowNum]["REQ_ITEM_UOM"] = REQ_ITEM_UOM;
                    dt.Rows[rowNum]["REQ_STORE_REF"] = REQ_STORE_REF;
                    dt.Rows[rowNum]["REQ_STORE"] = REQ_STORE;
                    dt.Rows[rowNum]["REQ_SPEC"] = REQ_SPEC;
                    dt.Rows[rowNum]["REQ_BRAND"] = REQ_BRAND;
                    dt.Rows[rowNum]["REQ_ORIGIN"] = REQ_ORIGIN;
                    dt.Rows[rowNum]["REQ_PACKING"] = REQ_PACKING;
                    dt.Rows[rowNum]["REQ_EXP_DT"] = REQ_EXP_DT;
                    dt.Rows[rowNum]["REQ_LOC_REF"] = REQ_LOC_REF;
                    dt.Rows[rowNum]["REQ_LOC"] = REQ_LOC;
                    dt.Rows[rowNum]["REQ_REM"] = REQ_REM;
                    dt.Rows[rowNum]["REQ_QTY"] = REQ_QTY;
                    dt.Rows[rowNum]["REQ_REF"] = REQ_REF;

                    dt.AcceptChanges();                    
                }

                gvPoReqDet.SelectedIndex = -1;
                ViewState["dtPoReqDet"] = dt;
                SetPoReqDetGridData();

                if (gvPoReqDet.Rows.Count > 0)
                {
                    txtPoReqRef.Enabled = false;
                    btnPost.Enabled = true;
                    btnHold.Enabled = true;
                }
                else
                {
                    txtPoReqRef.Enabled = true;
                    btnPost.Enabled = false;
                    btnHold.Enabled = false;
                }

                txtPoItemName.Text = "";
                txtPoQty.Text = "";
                txtPoSpec.Text = "";
                txtPoBrand.Text = "";
                txtPoOrigin.Text = "";
                txtPoPacking.Text = "";
                txtPoExpDt.Text = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                txtPoRem.Text = "";
                
                cboPoItmType.SelectedIndex = 0;
                //cboPoItmSpec.Items.Clear();
                //cboPoItmSpec.Items.Insert(0, new ListItem("", "0"));
                cboPoItmUom.SelectedIndex = 0;
                cboPoLoc.SelectedIndex = 0;

                hfItemEditStatus.Value = "N";
                hfItemEditRefNo.Value = "0";
                btnAddPoDet.Text = "Add";
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void gvPoReqDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var dt = new DataTable();
            dt = (DataTable)ViewState["dtPoReqDet"];

            dt.Rows[rowNum].Delete();
            dt.AcceptChanges();

            gvPoReqDet.EditIndex = -1;
            SetPoReqDetGridData();

            if (gvPoReqDet.Rows.Count > 0)
            {
                txtPoReqRef.Enabled = false;
                btnPost.Enabled = true;
                btnHold.Enabled = true;
            }
            else
            {
                txtPoReqRef.Enabled = true;
                btnPost.Enabled = false;
                btnHold.Enabled = false;
            }
        }

        protected void btnHold_Click(object sender, EventArgs e)
        {
            Page.Validate("btnSave");

            if (!Page.IsValid) return;

            var taPrHdr = new tbl_PuTr_Pr_HdrTableAdapter();
            var taPrDet = new tbl_PuTr_Pr_DetTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taPrHdr.Connection);

            try
            {
                if (gvPoReqDet.Rows.Count <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Add MPR Item first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                #region Get Employee Details
                var empRef = "";
                var empName = "";
                var srchWords = txtPoReqBy.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    empRef = word;
                    break;
                }

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
                            empName = dtEmpGenInfo[0].EmpName;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion
                    
                var nextMprRefNo = "";

                taPrHdr.AttachTransaction(myTran);
                taPrDet.AttachTransaction(myTran);

                if (hfEditStatus.Value == "N")
                {
                    var dtRefNo = taPrHdr.GetMaxPrRef(DateTime.Now.Year);
                    var nextRefNo = (dtRefNo == null || Convert.ToInt32(dtRefNo) == 0) ? 1 : Convert.ToInt32(dtRefNo) + 1;

                    nextMprRefNo = "MPR-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + nextRefNo.ToString("000000");

                    taPrHdr.InsertPrHdr("PR", "MPR", nextMprRefNo, empRef, empRef, empRef, Convert.ToDateTime(txtPoReqDt.Text.Trim()), empName,
                                        txtPoReqRef.Text.Trim(), "", "", "", "", "", "", "", "", 0, "H", DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00"),
                                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                        DateTime.Now, "", "", "", Convert.ToDateTime(txtPoReqDt.Text.Trim()),
                                        Convert.ToDateTime(txtPoReqDt.Text.Trim()).Year + "/" + Convert.ToDateTime(txtPoReqDt.Text.Trim()).Month.ToString("00"),
                                        "", "", "", 0, "BDT", 0);

                    #region Insert MPR Details
                    var dt = new DataTable();
                    dt = (DataTable)ViewState["dtPoReqDet"];

                    short lNo = 1;
                    foreach (GridViewRow row in gvPoReqDet.Rows)
                    {
                        var lblItemCode = ((Label)(row.FindControl("lblItemCode"))).Text.ToString();
                        var lblItemDesc = ((Label)(row.FindControl("lblItemDesc"))).Text.ToString();
                        var hfItemUnit = ((HiddenField)(row.FindControl("hfItemUnit"))).Value.ToString();
                        var lblItemUnit = ((Label)(row.FindControl("lblItemUnit"))).Text.ToString();
                        var hfStore = ((HiddenField)(row.FindControl("hfStore"))).Value.ToString();
                        var lblStore = ((Label)(row.FindControl("lblStore"))).Text.ToString();
                        var lblSpec = ((Label)(row.FindControl("lblSpec"))).Text.ToString();
                        var lblBrand = ((Label)(row.FindControl("lblBrand"))).Text.ToString();
                        var lblOrigin = ((Label)(row.FindControl("lblOrigin"))).Text.ToString();
                        var lblPacking = ((Label)(row.FindControl("lblPacking"))).Text.ToString();
                        var lblExpReqDt = ((Label)(row.FindControl("lblExpReqDt"))).Text.ToString();
                        var hfLocation = ((HiddenField)(row.FindControl("hfLocation"))).Value.ToString();
                        var lblLocation = ((Label)(row.FindControl("lblLocation"))).Text.ToString();
                        var lblRemarks = ((Label)(row.FindControl("lblRemarks"))).Text.ToString();
                        var lblPoReqQty = ((Label)(row.FindControl("lblPoReqQty"))).Text.ToString();
                        var hfReqRef = ((HiddenField)(row.FindControl("hfReqRef"))).Value.ToString();

                        taPrDet.InsertPrDet("PR", "MPR", nextMprRefNo, lNo, lblItemCode, lblItemDesc, hfItemUnit,
                                            hfStore, lblSpec, lblBrand, lblOrigin, lblPacking, hfLocation, lblLocation, "", hfReqRef, Convert.ToDateTime(lblExpReqDt),
                                            DateTime.Now, Convert.ToDouble(lblPoReqQty), 0, Convert.ToDouble(lblPoReqQty), Convert.ToDouble(lblPoReqQty), "O", "N", 0, 0, 0,
                                            "Req. By:- " + empName + " @" + DateTime.Now.ToString(), "", "H", 1, lblRemarks,
                                            "", "LPO", "", "", "TEN", "", "", "", "", "", "");
                        lNo++;
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnPrint.Enabled = true;
                }
                else
                {
                    nextMprRefNo = ddlMprList.SelectedValue.ToString();
                    taPrHdr.UpdatePrHdr(empRef, empRef, empRef, Convert.ToDateTime(txtPoReqDt.Text.Trim()), empName,
                                        txtPoReqRef.Text.Trim(), "", "", "", "", "", "", "", "", 0, "H", DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00"),
                                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                        DateTime.Now, "", "", "", Convert.ToDateTime(txtPoReqDt.Text.Trim()),
                                        Convert.ToDateTime(txtPoReqDt.Text.Trim()).Year + "/" + Convert.ToDateTime(txtPoReqDt.Text.Trim()).Month.ToString("00"),
                                        "", "", "", 0, "BDT", 0, nextMprRefNo);

                    taPrDet.DeletePrDet(ddlMprList.SelectedValue.ToString());

                    #region Insert MPR Details
                    var dt = new DataTable();
                    dt = (DataTable)ViewState["dtPoReqDet"];

                    short lNo = 1;
                    foreach (GridViewRow row in gvPoReqDet.Rows)
                    {
                        var lblItemCode = ((Label)(row.FindControl("lblItemCode"))).Text.ToString();
                        var lblItemDesc = ((Label)(row.FindControl("lblItemDesc"))).Text.ToString();
                        var hfItemUnit = ((HiddenField)(row.FindControl("hfItemUnit"))).Value.ToString();
                        var lblItemUnit = ((Label)(row.FindControl("lblItemUnit"))).Text.ToString();
                        var hfStore = ((HiddenField)(row.FindControl("hfStore"))).Value.ToString();
                        var lblStore = ((Label)(row.FindControl("lblStore"))).Text.ToString();
                        var lblSpec = ((Label)(row.FindControl("lblSpec"))).Text.ToString();
                        var lblBrand = ((Label)(row.FindControl("lblBrand"))).Text.ToString();
                        var lblOrigin = ((Label)(row.FindControl("lblOrigin"))).Text.ToString();
                        var lblPacking = ((Label)(row.FindControl("lblPacking"))).Text.ToString();
                        var lblExpReqDt = ((Label)(row.FindControl("lblExpReqDt"))).Text.ToString();
                        var hfLocation = ((HiddenField)(row.FindControl("hfLocation"))).Value.ToString();
                        var lblLocation = ((Label)(row.FindControl("lblLocation"))).Text.ToString();
                        var lblRemarks = ((Label)(row.FindControl("lblRemarks"))).Text.ToString();
                        var lblPoReqQty = ((Label)(row.FindControl("lblPoReqQty"))).Text.ToString();
                        var hfReqRef = ((HiddenField)(row.FindControl("hfReqRef"))).Value.ToString();
                        
                        taPrDet.InsertPrDet("PR", "MPR", nextMprRefNo, lNo, lblItemCode, lblItemDesc, hfItemUnit,
                                            hfStore, lblSpec, lblBrand, lblOrigin, lblPacking, hfLocation, lblLocation, "", hfReqRef, Convert.ToDateTime(lblExpReqDt),
                                            DateTime.Now, Convert.ToDouble(lblPoReqQty), 0, Convert.ToDouble(lblPoReqQty), Convert.ToDouble(lblPoReqQty), "O", "N", 0, 0, 0,
                                            "Req. By:- " + empName + " @" + DateTime.Now.ToString(), "", "H", 1, lblRemarks,
                                            "", "LPO", "", "", "TEN", "", "", "", "", "", "");
                        lNo++;
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnPrint.Enabled = true;
                }

                var curYear = DateTime.Now.Year;
                cboYear.SelectedValue = curYear.ToString();

                var curMonth = DateTime.Now.Month;
                cboMonth.SelectedValue = curMonth.ToString();

                ddlMprList.DataSource = taPrHdr.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue), Convert.ToDecimal(cboMonth.SelectedValue));
                ddlMprList.DataTextField = "Pr_Hdr_Ref";
                ddlMprList.DataValueField = "Pr_Hdr_Ref";
                ddlMprList.DataBind();
                ddlMprList.Items.Insert(0, "----------New----------");
                ddlMprList.SelectedIndex = ddlMprList.Items.IndexOf(ddlMprList.Items.FindByText(nextMprRefNo));

                txtPoReqRef.Enabled = false;

                hfEditStatus.Value = "Y";
                hfRefNo.Value = "0";
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
            Page.Validate("btnSave");

            if (!Page.IsValid) return;

            var taPrHdr = new tbl_PuTr_Pr_HdrTableAdapter();
            var taPrDet = new tbl_PuTr_Pr_DetTableAdapter();
            var comm = new tbl_Tran_ComTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taPrHdr.Connection);

            try
            {
                if (gvPoReqDet.Rows.Count <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Add MPR Item first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                #region Get Employee Details
                var empRef = "";
                var empId = "";
                var empName = "";
                var empDesig = "";
                var srchWords = txtPoReqBy.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    empRef = word;
                    break;
                }

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
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                var nextMprRefNo = "";

                taPrHdr.AttachTransaction(myTran);
                taPrDet.AttachTransaction(myTran);
                comm.AttachTransaction(myTran);

                if (hfEditStatus.Value == "N")
                {
                    var dtRefNo = taPrHdr.GetMaxPrRef(DateTime.Now.Year);
                    var nextRefNo = (dtRefNo == null || Convert.ToInt32(dtRefNo) == 0) ? 1 : Convert.ToInt32(dtRefNo) + 1;
                    nextMprRefNo = "MPR-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + nextRefNo.ToString("000000");

                    taPrHdr.InsertPrHdr("PR", "MPR", nextMprRefNo, empRef, empRef, empRef, Convert.ToDateTime(txtPoReqDt.Text.Trim()), empName,
                                        txtPoReqRef.Text.Trim(), "", "", "", "", "", "", "", "", 0, "P", DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00"),
                                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), DateTime.Now, "", "", "",
                                        Convert.ToDateTime(txtPoReqDt.Text.Trim()),
                                        Convert.ToDateTime(txtPoReqDt.Text.Trim()).Year + "/" + Convert.ToDateTime(txtPoReqDt.Text.Trim()).Month.ToString("00"),
                                        "", "", "", 1, "BDT", 0);

                    var comments_det = "(MPR Submited By:" + empName + ") " + txtPoRem.Text.Trim();
                    comm.InsertTranCom(nextMprRefNo, 1, DateTime.Now, empId, empName, empDesig, 1, "MPR", "OPN", comments_det, "", "1", "", "", "", "");

                    #region Insert MPR Details
                    var dt = new DataTable();
                    dt = (DataTable)ViewState["dtPoReqDet"];

                    short lNo = 1;
                    foreach (GridViewRow row in gvPoReqDet.Rows)
                    {
                        var lblItemCode = ((Label)(row.FindControl("lblItemCode"))).Text.ToString();
                        var lblItemDesc = ((Label)(row.FindControl("lblItemDesc"))).Text.ToString();
                        var hfItemUnit = ((HiddenField)(row.FindControl("hfItemUnit"))).Value.ToString();
                        var lblItemUnit = ((Label)(row.FindControl("lblItemUnit"))).Text.ToString();
                        var hfStore = ((HiddenField)(row.FindControl("hfStore"))).Value.ToString();
                        var lblStore = ((Label)(row.FindControl("lblStore"))).Text.ToString();
                        var lblSpec = ((Label)(row.FindControl("lblSpec"))).Text.ToString();
                        var lblBrand = ((Label)(row.FindControl("lblBrand"))).Text.ToString();
                        var lblOrigin = ((Label)(row.FindControl("lblOrigin"))).Text.ToString();
                        var lblPacking = ((Label)(row.FindControl("lblPacking"))).Text.ToString();
                        var lblExpReqDt = ((Label)(row.FindControl("lblExpReqDt"))).Text.ToString();
                        var hfLocation = ((HiddenField)(row.FindControl("hfLocation"))).Value.ToString();
                        var lblLocation = ((Label)(row.FindControl("lblLocation"))).Text.ToString();
                        var lblRemarks = ((Label)(row.FindControl("lblRemarks"))).Text.ToString();
                        var lblPoReqQty = ((Label)(row.FindControl("lblPoReqQty"))).Text.ToString();
                        var hfReqRef = ((HiddenField)(row.FindControl("hfReqRef"))).Value.ToString();

                        taPrDet.InsertPrDet("PR", "MPR", nextMprRefNo, lNo, lblItemCode, lblItemDesc, hfItemUnit,
                                            hfStore, lblSpec, lblBrand, lblOrigin, lblPacking, hfLocation, lblLocation, "", hfReqRef, Convert.ToDateTime(lblExpReqDt),
                                            Convert.ToDateTime(txtPoReqDt.Text.Trim()), Convert.ToDouble(lblPoReqQty), 0, Convert.ToDouble(lblPoReqQty),
                                            Convert.ToDouble(lblPoReqQty), "O", "N", 0, 0, 0, "Req. By:- " + empName + " @" + DateTime.Now.ToString(), "", "H", 1, lblRemarks,
                                            "", "LPO", "", "", "TEN", "", "", "", "", "", "");
                        lNo++;
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Purchase Requsition Posted Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    var curYear = DateTime.Now.Year;
                    cboYear.SelectedValue = curYear.ToString();

                    var curMonth = DateTime.Now.Month;
                    cboMonth.SelectedValue = curMonth.ToString();

                    ddlMprList.Items.Clear();

                    ListItem lst;

                    var dtPrHdrNew = taPrHdr.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue), Convert.ToDecimal(cboMonth.SelectedValue));
                    foreach (dsProcTran.tbl_PuTr_Pr_HdrRow dr in dtPrHdrNew.Rows)
                    {
                        lst = new ListItem();
                        lst.Text = dr.Pr_Hdr_Ref.ToString() + ":" + dr.Pr_Hdr_Com1.ToString() + ":[" + dr.Pr_Hdr_Date.ToString("dd/MM/yyyy") + "]";
                        lst.Value = dr.Pr_Hdr_Ref.ToString();
                        ddlMprList.Items.Add(lst);
                    }
                    ddlMprList.Items.Insert(0, new ListItem("-----New-----", "0"));
                    ddlMprList.SelectedIndex = ddlMprList.Items.IndexOf(ddlMprList.Items.FindByValue(nextMprRefNo.ToString()));

                    btnAddPoDet.Enabled = false;
                    btnHold.Enabled = false;
                    btnPost.Enabled = false;
                    btnPrint.Enabled = true;
                }
                else
                {
                    nextMprRefNo = ddlMprList.SelectedValue.ToString();

                    var taQtnDet = new tbl_Qtn_DetTableAdapter();
                    var dtQtnDet = taQtnDet.GetDataByMprRef(nextMprRefNo.ToString());
                    if (dtQtnDet.Rows.Count > 0)
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "MPR modification is not possible. Quotation already submited ";
                        tblMsg.Rows[1].Cells[0].InnerText = "against this MPR. Delete quotation first.";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }

                    taPrHdr.UpdatePrHdr(empRef, empRef, empRef, Convert.ToDateTime(txtPoReqDt.Text.Trim()), empName,
                                        txtPoReqRef.Text.Trim(), "", "", "", "", "", "", "", "", 0, "P", DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00"),
                                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                        DateTime.Now, "", "", "", Convert.ToDateTime(txtPoReqDt.Text.Trim()),
                                        Convert.ToDateTime(txtPoReqDt.Text.Trim()).Year + "/" + Convert.ToDateTime(txtPoReqDt.Text.Trim()).Month.ToString("00"),
                                        "", "", "", 1, "BDT", 0, nextMprRefNo);

                    taPrDet.DeletePrDet(ddlMprList.SelectedValue.ToString());

                    #region Insert MPR Details
                    var dt = new DataTable();
                    dt = (DataTable)ViewState["dtPoReqDet"];

                    short lNo = 1;
                    foreach (GridViewRow row in gvPoReqDet.Rows)
                    {
                        var lblItemCode = ((Label)(row.FindControl("lblItemCode"))).Text.ToString();
                        var lblItemDesc = ((Label)(row.FindControl("lblItemDesc"))).Text.ToString();
                        var hfItemUnit = ((HiddenField)(row.FindControl("hfItemUnit"))).Value.ToString();
                        var lblItemUnit = ((Label)(row.FindControl("lblItemUnit"))).Text.ToString();
                        var hfStore = ((HiddenField)(row.FindControl("hfStore"))).Value.ToString();
                        var lblStore = ((Label)(row.FindControl("lblStore"))).Text.ToString();
                        var lblSpec = ((Label)(row.FindControl("lblSpec"))).Text.ToString();
                        var lblBrand = ((Label)(row.FindControl("lblBrand"))).Text.ToString();
                        var lblOrigin = ((Label)(row.FindControl("lblOrigin"))).Text.ToString();
                        var lblPacking = ((Label)(row.FindControl("lblPacking"))).Text.ToString();
                        var lblExpReqDt = ((Label)(row.FindControl("lblExpReqDt"))).Text.ToString();
                        var hfLocation = ((HiddenField)(row.FindControl("hfLocation"))).Value.ToString();
                        var lblLocation = ((Label)(row.FindControl("lblLocation"))).Text.ToString();
                        var lblRemarks = ((Label)(row.FindControl("lblRemarks"))).Text.ToString();
                        var lblPoReqQty = ((Label)(row.FindControl("lblPoReqQty"))).Text.ToString();
                        var hfReqRef = ((HiddenField)(row.FindControl("hfReqRef"))).Value.ToString();

                        taPrDet.InsertPrDet("PR", "MPR", nextMprRefNo, lNo, lblItemCode, lblItemDesc, hfItemUnit,
                                            hfStore, lblSpec, lblBrand, lblOrigin, lblPacking, hfLocation, lblLocation, "", hfReqRef, Convert.ToDateTime(lblExpReqDt),
                                            Convert.ToDateTime(txtPoReqDt.Text.Trim()), Convert.ToDouble(lblPoReqQty), 0, Convert.ToDouble(lblPoReqQty),
                                            Convert.ToDouble(lblPoReqQty), "O", "N", 0, 0, 0, "Req. By:- " + empName + " @" + DateTime.Now.ToString(), "", "H", 1, lblRemarks,
                                            "", "LPO", "", "", "TEN", "", "", "", "", "", "");
                        lNo++;
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    //var curYear = DateTime.Now.Year;
                    //cboYear.SelectedValue = curYear.ToString();

                    //var curMonth = DateTime.Now.Month;
                    //cboMonth.SelectedValue = curMonth.ToString();

                    ddlMprList.Items.Clear();

                    ListItem lst;

                    var dtPrHdrNew = taPrHdr.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue), Convert.ToDecimal(cboMonth.SelectedValue));
                    foreach (dsProcTran.tbl_PuTr_Pr_HdrRow dr in dtPrHdrNew.Rows)
                    {
                        lst = new ListItem();
                        lst.Text = dr.Pr_Hdr_Ref.ToString() + ":" + dr.Pr_Hdr_Com1.ToString() + ":[" + dr.Pr_Hdr_Date.ToString("dd/MM/yyyy") + "]";
                        lst.Value = dr.Pr_Hdr_Ref.ToString();
                        ddlMprList.Items.Add(lst);
                    }
                    ddlMprList.Items.Insert(0, new ListItem("-----New-----", "0"));
                    ddlMprList.SelectedIndex = ddlMprList.Items.IndexOf(ddlMprList.Items.FindByValue(nextMprRefNo));

                    btnAddPoDet.Enabled = false;
                    btnHold.Enabled = false;
                    btnPost.Enabled = false;
                    btnPrint.Enabled = true;
                }                

                //ddlMprList.DataSource = taPrHdr.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue), Convert.ToDecimal(cboMonth.SelectedValue));
                //ddlMprList.DataTextField = "Pr_Hdr_Ref";
                //ddlMprList.DataValueField = "Pr_Hdr_Ref";
                //ddlMprList.DataBind();
                //ddlMprList.Items.Insert(0, "----------New----------");
                //ddlMprList.SelectedIndex = ddlMprList.Items.IndexOf(ddlMprList.Items.FindByText(nextMprRefNo));

                txtPoReqRef.Enabled = false;

                hfEditStatus.Value = "Y";
                hfRefNo.Value = "0";

                btnHold.Enabled = false;
                btnPost.Enabled = false;
                btnPrint.Enabled = true;
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //string FilePath = Server.MapPath("001.pdf");
            //WebClient User = new WebClient();
            //Byte[] FileBuffer = User.DownloadData(FilePath);
            //if (FileBuffer != null)
            //{
            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("content-length", FileBuffer.Length.ToString());
            //    Response.BinaryWrite(FileBuffer);
            //}  

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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void ClearData()
        {
            //var curYear = DateTime.Now.Year;
            //cboYear.SelectedValue = curYear.ToString();

            //var curMonth = DateTime.Now.Month;
            //cboMonth.SelectedValue = curMonth.ToString();

            ddlMprList.Items.Clear();

            var taPrHdrNew = new tbl_PuTr_Pr_HdrTableAdapter();
            ddlMprList.DataSource = taPrHdrNew.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue), Convert.ToDecimal(cboMonth.SelectedValue));
            ddlMprList.DataTextField = "Pr_Hdr_Ref";
            ddlMprList.DataValueField = "Pr_Hdr_Ref";
            ddlMprList.DataBind();
            ddlMprList.Items.Insert(0, "----------New----------");

            txtPoReqRef.Text = "";
            txtPoReqRef.Enabled = true;

            var dtRefNoNew = taPrHdrNew.GetMaxPrRef(DateTime.Now.Year);
            var nextRefNoNew = (dtRefNoNew == null || Convert.ToInt32(dtRefNoNew) == 0) ? 1 : Convert.ToInt32(dtRefNoNew) + 1;
            var prefixNew = "MPR-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy");
            txtPoRefNo.Text = prefixNew + "-" + nextRefNoNew.ToString("000000");            

            txtPoItemName.Text = "";
            txtPoQty.Text = "";
            txtPoSpec.Text = "";
            txtPoBrand.Text = "";
            txtPoOrigin.Text = "";
            txtPoExpDt.Text = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
            txtPoRem.Text = "";

            cboPoItmType.SelectedIndex = 0;
            //cboPoItmSpec.Items.Clear();
            //cboPoItmSpec.Items.Insert(0, new ListItem("", "0"));
            cboPoItmUom.SelectedIndex = 0;
            cboPoLoc.SelectedIndex = 0;
           
            txtPoReqDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtPoExpDt.Text = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");

            btnAddPoDet.Enabled = true;
            btnPost.Enabled = false;
            btnHold.Enabled = false;
            btnPrint.Enabled = false;

            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";
            hfItemEditStatus.Value = "N";
            hfItemEditRefNo.Value = "0";
            btnAddPoDet.Text = "Add";

            LoadInitPoReqDetGridData();
            SetPoReqDetGridData();

            gvPoReqDet.SelectedIndex = -1;
            gvPoReqDet.Enabled = true;
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var itemRef = "";
                var srchWords = txtPoItemName.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    itemRef = word;
                    break;
                }

                if (itemRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(itemRef, out result))
                    {
                        var taItem = new tbl_InMa_Item_DetTableAdapter();
                        var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(itemRef));
                        if (dtItem.Rows.Count > 0)
                            args.IsValid = true;
                        else
                            args.IsValid = false;
                    }
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex) { args.IsValid = false; }
        }

        protected void txtPoItemName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var itemRef = "";
                var srchWords = txtPoItemName.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    itemRef = word;
                    break;
                }

                if (itemRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(itemRef, out result))
                    {
                        var taItem = new tbl_InMa_Item_DetTableAdapter();
                        var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(itemRef));
                        if (dtItem.Rows.Count > 0)
                        {
                            cboPoItmUom.SelectedValue = dtItem[0].IsItm_Det_Stk_UnitNull() ? "0" : dtItem[0].Itm_Det_Stk_Unit.ToString();
                            ddlMprStore.SelectedIndex = ddlMprStore.Items.IndexOf(ddlMprStore.Items.FindByValue(dtItem[0].Itm_Det_Ext_Data2.ToString()));
                            this.txtPoQty.Focus();
                        }
                        else
                        {
                            cboPoItmUom.SelectedIndex = 0;
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                        }
                    }
                    else
                    {
                        cboPoItmUom.SelectedIndex = 0;
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                else
                {
                    cboPoItmUom.SelectedIndex = 0;
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
            catch (Exception ex) 
            {
                cboPoItmUom.SelectedIndex = 0;
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void ddlMprList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taPrHdr = new tbl_PuTr_Pr_HdrTableAdapter();
            var taPrDet = new tbl_PuTr_Pr_DetTableAdapter();
            var taStrLoc = new tbl_InMa_Str_LocTableAdapter();

            try
            {
                //reportInfo();

                if (ddlMprList.SelectedIndex == 0)
                {
                    ClearData();
                }
                else
                {
                    var dtPrHdr = taPrHdr.GetDataByPrRef(ddlMprList.SelectedValue.ToString());
                    if (dtPrHdr.Rows.Count > 0)
                    {                        
                        txtPoReqRef.Text = dtPrHdr[0].Pr_Hdr_Com2.ToString();
                        txtPoReqDt.Text = dtPrHdr[0].Pr_Hdr_Date.ToString("dd/MM/yyyy");
                        txtPoRefNo.Text = dtPrHdr[0].Pr_Hdr_Ref.ToString();

                        LoadInitPoReqDetGridData();
                        SetPoReqDetGridData();

                        var storeName = "";
                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtPoReqDet"];
                        var dtPrDet = taPrDet.GetDataByPrRef(ddlMprList.SelectedValue.ToString());
                        foreach (dsProcTran.tbl_PuTr_Pr_DetRow dr in dtPrDet.Rows)
                        {
                            var dtStrLoc = taStrLoc.GetDataByLocRef(Convert.ToInt32(dr.Pr_Det_Str_Code));
                            if (dtStrLoc.Rows.Count > 0) storeName = dtStrLoc[0].Str_Loc_Name.ToString();

                            dt.Rows.Add(dr.Pr_Det_Ref, dr.Pr_Det_Ref, dr.Pr_Det_Ref, dr.Pr_Det_Icode, dr.Pr_Det_Itm_Desc, dr.Pr_Det_Itm_Uom, dr.Pr_Det_Itm_Uom,
                                        dr.Pr_Det_Str_Code, storeName.ToString(), dr.Pr_Det_Spec, dr.Pr_Det_Brand, dr.Pr_Det_Origin, dr.Pr_Det_Packing,
                                        dr.Pr_Det_Exp_Dat.ToString("dd/MM/yyyy"), dr.Pr_Det_Use_Loc_Ref, dr.Pr_Det_Use_Loc_name, dr.Pr_Det_Rem,
                                        dr.Pr_Det_Lin_Qty, dr.Pr_Det_Bat_No);
                        }
                        ViewState["dtPoReqDet"] = dt;
                        SetPoReqDetGridData();
                        gvPoReqDet.SelectedIndex = -1;

                        var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                        if (dtPrHdr[0].Pr_Hdr_HPC_Flag == "P" && empRef != "000011")
                        {
                            txtPoReqRef.Enabled = false;

                            hfEditStatus.Value = "Y";

                            btnPost.Enabled = false;
                            btnHold.Enabled = false;
                            btnPrint.Enabled = true;
                            gvPoReqDet.Enabled = false;
                        }
                        else
                        {
                            txtPoReqRef.Enabled = true;

                            hfEditStatus.Value = "Y";

                            btnPost.Enabled = true;
                            btnHold.Enabled = true;
                            btnPrint.Enabled = true;
                            gvPoReqDet.Enabled = true;
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

        protected void cboPoItmType_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtenderSrchItem.ContextKey = cboPoItmType.SelectedValue.ToString();
            txtPoItemName.Text = "";
            cboPoItmUom.SelectedIndex = 0;
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_MPR_List();
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_MPR_List();
        }

        protected void gvPoReqDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvPoReqDet_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taItemDet = new tbl_InMa_Item_DetTableAdapter();

            int indx = gvPoReqDet.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    var itemName = "";
                    hfItemEditRefNo.Value = gvPoReqDet.SelectedIndex.ToString();
                    hfItemEditStatus.Value = "Y";
                    btnAddPoDet.Text = "Edit";

                    var itemCode = ((Label)gvPoReqDet.Rows[indx].FindControl("lblItemCode")).Text;
                    var dtItemDet = taItemDet.GetDataByItemRef(Convert.ToInt32(itemCode.ToString()));
                    if (dtItemDet.Rows.Count > 0) itemName = dtItemDet[0].Itm_Det_Ref + ":" + dtItemDet[0].Itm_Det_Code + ":" + dtItemDet[0].Itm_Det_Desc;

                    txtPoItemName.Text = itemName.ToString();
                    cboPoItmUom.SelectedValue = ((HiddenField)gvPoReqDet.Rows[indx].FindControl("hfItemUnit")).Value;
                    txtPoQty.Text = ((Label)gvPoReqDet.Rows[indx].FindControl("lblPoReqQty")).Text;
                    txtPoSpec.Text = ((Label)gvPoReqDet.Rows[indx].FindControl("lblSpec")).Text;
                    txtPoBrand.Text = ((Label)gvPoReqDet.Rows[indx].FindControl("lblBrand")).Text;
                    txtPoOrigin.Text = ((Label)gvPoReqDet.Rows[indx].FindControl("lblOrigin")).Text;
                    txtPoPacking.Text = ((Label)gvPoReqDet.Rows[indx].FindControl("lblPacking")).Text;
                    txtPoExpDt.Text = ((Label)gvPoReqDet.Rows[indx].FindControl("lblExpReqDt")).Text;
                    cboPoLoc.Text = ((HiddenField)gvPoReqDet.Rows[indx].FindControl("hfLocation")).Value;
                    ddlMprStore.Text = ((HiddenField)gvPoReqDet.Rows[indx].FindControl("hfStore")).Value;
                    txtPoRem.Text = ((Label)gvPoReqDet.Rows[indx].FindControl("lblRemarks")).Text;

                }
                catch (Exception ex)
                {
                    ClearData();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data processing Error.\n" + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }
    }
}