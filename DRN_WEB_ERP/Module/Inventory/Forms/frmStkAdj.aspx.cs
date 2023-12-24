using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Inventory.Forms
{
    public partial class frmStkAdj : System.Web.UI.Page
    {
        double totAdjQty = 0;
        double totAdjAmt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            AutoCompleteExtenderSrchItem.ContextKey = "0";

            btnPrint.Attributes.Add("onclick", "javascript:w= window.open('frmShowSalesReport.aspx');");

            txtTranDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();

            var dtMaxHdrRef = taInvHdr.GetMaxHdrRefNo("ADJ", DateTime.Now.Year);
            var nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;
            var nextHdrRefNo = "ECIL-ADJ-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRef).ToString("000000");
            lblTranRefNo.Text = nextHdrRefNo;

            ddlTranList.DataSource = taInvHdr.GetInvTranDataByTrnCode("ADJ");
            ddlTranList.DataTextField = "Trn_Hdr_Ref_No";
            ddlTranList.DataValueField = "Trn_Hdr_Ref";
            ddlTranList.DataBind();
            ddlTranList.Items.Insert(0, new ListItem("----------New----------", "0"));

            var taItemType = new tbl_InMa_TypeTableAdapter();
            var dtItemType = taItemType.GetDataByAsc();
            cboItemType.DataSource = dtItemType;
            cboItemType.DataTextField = "Item_Type_Name";
            cboItemType.DataValueField = "Item_Type_Code";
            cboItemType.DataBind();
            cboItemType.Items.Insert(0, new ListItem("-----All-----", "0"));

            var taItemUom = new tbl_InMa_UomTableAdapter();
            var dtItemUom = taItemUom.GetData();
            ddlTranItemUom.DataSource = dtItemUom;
            ddlTranItemUom.DataTextField = "Uom_Name";
            ddlTranItemUom.DataValueField = "Uom_Code";
            ddlTranItemUom.DataBind();
            ddlTranItemUom.Items.Insert(0, "----");

            var taStore = new tbl_InMa_Str_LocTableAdapter();
            var dtStore = taStore.GetDataBySortName();
            ddlTranStore.DataSource = dtStore;
            ddlTranStore.DataTextField = "Str_Loc_Name";
            ddlTranStore.DataValueField = "Str_Loc_Ref";
            ddlTranStore.DataBind();
            ddlTranStore.Items.Insert(0, new ListItem("----", "0"));

            LoadInitAdjDedGridData();
            SetAdjDedGridData();

            LoadInitAdjAddGridData();
            SetAdjAddGridData(); 
        }

        #region Adjustment Details Gridview
        protected void LoadInitAdjDedGridData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TRN_HDR_REF", typeof(string));
            dt.Columns.Add("TRN_DET_REF", typeof(string));
            dt.Columns.Add("TRN_DET_REF_NO", typeof(string));
            dt.Columns.Add("TRN_ITEM_REF", typeof(string));
            dt.Columns.Add("TRN_ITEM_NAME", typeof(string));
            dt.Columns.Add("TRN_ITEM_UOM_REF", typeof(string));
            dt.Columns.Add("TRN_ITEM_UOM", typeof(string));
            dt.Columns.Add("TRN_STORE_REF", typeof(string));
            dt.Columns.Add("TRN_STORE_NAME", typeof(string));
            dt.Columns.Add("TRN_STK", typeof(string));
            dt.Columns.Add("TRN_QTY", typeof(string));                                   
            ViewState["dtTranDet"] = dt;
        }

        protected void SetAdjDedGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtTranDet"];

                gvItmStkAdjDed.DataSource = dt;
                gvItmStkAdjDed.DataBind();
            }
            catch (Exception ex) { }
        }

        protected void LoadInitAdjAddGridData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TRN_HDR_REF", typeof(string));
            dt.Columns.Add("TRN_DET_REF", typeof(string));
            dt.Columns.Add("TRN_DET_REF_NO", typeof(string));
            dt.Columns.Add("TRN_ITEM_REF", typeof(string));
            dt.Columns.Add("TRN_ITEM_NAME", typeof(string));
            dt.Columns.Add("TRN_ITEM_UOM_REF", typeof(string));
            dt.Columns.Add("TRN_ITEM_UOM", typeof(string));
            dt.Columns.Add("TRN_STORE_REF", typeof(string));
            dt.Columns.Add("TRN_STORE_NAME", typeof(string));
            dt.Columns.Add("TRN_STK", typeof(string));
            dt.Columns.Add("TRN_QTY", typeof(string));
            ViewState["dtTranDetAdd"] = dt;
        }

        protected void SetAdjAddGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtTranDetAdd"];

                gvItmStkAdjAdd.DataSource = dt;
                gvItmStkAdjAdd.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion

        protected void btnAddAdjDet_Click(object sender, EventArgs e)
        {
            Page.Validate("btnAdd");

            if (!Page.IsValid) return;

            try
            {
                var itemRef = "";
                var itemName = "";
                var srchWords = txtItemName.Text.Trim().Split(':');
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

                foreach (GridViewRow gr in gvItmStkAdjDed.Rows)
                {
                    var lblItemCode = ((Label)(gr.FindControl("lblItemCode"))).Text.ToString();
                    if (itemRef.Trim().ToString() == lblItemCode.ToString())
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = itemName.ToString() + " already addred to Item Deduction List.";
                        tblMsg.Rows[1].Cells[0].InnerText = "To add more you need to delete existing same item.";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                foreach (GridViewRow gr in gvItmStkAdjAdd.Rows)
                {
                    var lblItemCode = ((Label)(gr.FindControl("lblItemCodeAdd"))).Text.ToString();
                    if (itemRef.Trim().ToString() == lblItemCode.ToString())
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = itemName.ToString() + " already addred to Item Addition List.";
                        tblMsg.Rows[1].Cells[0].InnerText = "To add more you need to delete existing same item.";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                if (txtTranQty.Text.Trim() == "" || txtTranQty.Text.Trim().Length <= 0 || Convert.ToDouble(txtTranQty.Text.Trim()) < 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter valid quantity.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                if (txtCurStock.Text.Trim() == txtTranQty.Text.Trim())
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to enter Adjust Quantity same as Current Stock Quantity.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                if (Convert.ToDouble(txtTranQty.Text.Trim()) < Convert.ToDouble(txtCurStock.Text.Trim()))
                {
                    #region Deduction
                    var dt = new DataTable();
                    dt = (DataTable)ViewState["dtTranDet"];

                    var TRN_HDR_REF = "0";
                    var TRN_DET_REF = "0";
                    var TRN_DET_REF_NO = "0";
                    var TRN_ITEM_REF = itemRef.ToString();
                    var TRN_ITEM_NAME = itemName.ToString();
                    var TRN_ITEM_UOM_REF = ddlTranItemUom.SelectedValue.ToString();
                    var TRN_ITEM_UOM = ddlTranItemUom.SelectedItem.ToString();
                    var TRN_STORE_REF = ddlTranStore.SelectedValue.ToString();
                    var TRN_STORE_NAME = ddlTranStore.SelectedItem.ToString();
                    var TRN_STK = Convert.ToDouble(txtCurStock.Text.Trim().Length > 0 ? txtCurStock.Text.Trim() : "0");
                    var TRN_QTY = Convert.ToDouble(txtTranQty.Text.Trim().Length > 0 ? txtTranQty.Text.Trim() : "0");

                    dt.Rows.Add(TRN_HDR_REF, TRN_DET_REF, TRN_DET_REF_NO, TRN_ITEM_REF, TRN_ITEM_NAME, TRN_ITEM_UOM_REF, TRN_ITEM_UOM, TRN_STORE_REF, TRN_STORE_NAME, TRN_STK, TRN_QTY);

                    ViewState["dtTranDet"] = dt;
                    SetAdjDedGridData();
                    #endregion
                }
                else
                {
                    #region Addition
                    var dt = new DataTable();
                    dt = (DataTable)ViewState["dtTranDetAdd"];

                    var TRN_HDR_REF = "0";
                    var TRN_DET_REF = "0";
                    var TRN_DET_REF_NO = "0";
                    var TRN_ITEM_REF = itemRef.ToString();
                    var TRN_ITEM_NAME = itemName.ToString();
                    var TRN_ITEM_UOM_REF = ddlTranItemUom.SelectedValue.ToString();
                    var TRN_ITEM_UOM = ddlTranItemUom.SelectedItem.ToString();
                    var TRN_STORE_REF = ddlTranStore.SelectedValue.ToString();
                    var TRN_STORE_NAME = ddlTranStore.SelectedItem.ToString();
                    var TRN_STK = Convert.ToDouble(txtCurStock.Text.Trim().Length > 0 ? txtCurStock.Text.Trim() : "0");
                    var TRN_QTY = Convert.ToDouble(txtTranQty.Text.Trim().Length > 0 ? txtTranQty.Text.Trim() : "0");

                    dt.Rows.Add(TRN_HDR_REF, TRN_DET_REF, TRN_DET_REF_NO, TRN_ITEM_REF, TRN_ITEM_NAME, TRN_ITEM_UOM_REF, TRN_ITEM_UOM, TRN_STORE_REF, TRN_STORE_NAME, TRN_STK, TRN_QTY);

                    ViewState["dtTranDetAdd"] = dt;
                    SetAdjAddGridData();
                    #endregion
                }

                int adjDedCnt = 0;
                foreach (GridViewRow gr in gvItmStkAdjDed.Rows)
                {
                    adjDedCnt++;
                    var lblTranQty = ((Label)(gr.FindControl("lblTranQty"))).Text.ToString();
                    totAdjQty += Convert.ToDouble(lblTranQty.Trim());
                }
                lblItmStkAdjDed.Visible = adjDedCnt > 0;

                int adjAddCnt = 0;
                foreach (GridViewRow gr in gvItmStkAdjAdd.Rows)
                {
                    adjAddCnt++;
                    var lblTranQty = ((Label)(gr.FindControl("lblTranQtyAdd"))).Text.ToString();
                    totAdjQty += Convert.ToDouble(lblTranQty.Trim());
                }
                lblItmStkAdjAdd.Visible = adjAddCnt > 0;

                if (gvItmStkAdjDed.Rows.Count > 0 || gvItmStkAdjAdd.Rows.Count > 0)
                {
                    txtTranDate.ReadOnly = true;
                    txtTranDate.Enabled = false;
                    lblTotTranItem.Text = (gvItmStkAdjDed.Rows.Count + gvItmStkAdjAdd.Rows.Count).ToString();
                    //lblTotTranItem.Text = totAdjQty.ToString("N2");
                    lblTotTranVal.Text = totAdjAmt.ToString("N2");
                    lblTotTranItem.Visible = true;
                    lblTotTranVal.Visible = true;
                    btnPost.Visible = true;
                    //btnHold.Visible = true;
                    btnPost.Enabled = true;
                    //btnHold.Enabled = true;
                }
                else
                {
                    txtTranDate.ReadOnly = false;
                    txtTranDate.Enabled = true;
                    lblTotTranItem.Text = "0";
                    lblTotTranVal.Text = "0.00";
                    lblTotTranItem.Visible = false;
                    lblTotTranVal.Visible = false;
                    btnPost.Visible = false;
                    //btnHold.Visible = false;
                    btnPost.Enabled = false;
                    //btnHold.Enabled = false;
                }

                txtItemName.Text = "";
                ddlTranItemUom.SelectedIndex = 0;
                ddlTranStore.SelectedIndex = 0;
                txtCurStock.Text = "";
                txtTranQty.Text = "";
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnHold_Click(object sender, EventArgs e)
        {
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taInvHdr.Connection);

            try
            {
                var nextHdrRef = 1;
                var nextHdrRefNo = "";
                var trnType = "";
                var strCode = "";

                taInvHdr.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);
                taStkCtl.AttachTransaction(myTran);

                foreach (GridViewRow gr in gvItmStkAdjDed.Rows)
                {
                    var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                    var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                    var lblStoreName = (Label)(gr.FindControl("lblStore"));
                    strCode = hfStoreRef.Value.ToString();

                    var lblTranQty = ((Label)(gr.FindControl("lblTranQty"))).Text.ToString();
                    totAdjQty += Convert.ToDouble(lblTranQty.Trim());

                    var TranQty = Convert.ToDouble(lblTranQty);
                    if (TranQty > 0)
                    {
                        var dtStkCtl = taStkCtl.GetDataByStoreItem(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                        {
                            if (dtStkCtl.Rows.Count > 0)
                                trnType = Math.Round((TranQty), 4) < Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk), 4) ? "IA" : "RA";
                        }
                    }
                }

                if (hfEditStatus.Value == "N")
                {
                    //Inventory Header Ref
                    var dtMaxHdrRef = taInvHdr.GetMaxHdrRef();
                    nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;

                    var dtMaxMrrRef = taInvHdr.GetMaxHdrRefNo("ADJ", DateTime.Now.Year);
                    var nextMrrRef = dtMaxMrrRef == null ? 1 : Convert.ToInt32(dtMaxMrrRef) + 1;
                    nextHdrRefNo = "ECIL-ADJ-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextMrrRef).ToString("000000");

                    taInvHdr.InsertInvHdr(nextHdrRef, trnType, "ADJ", nextHdrRefNo, strCode, strCode, strCode,
                        "", Convert.ToDateTime(txtTranDate.Text.Trim()), txtRem.Text.Trim(), "", "", "", "", "", "", "", "", "", Convert.ToDecimal(totAdjAmt), "H",
                        (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), "ADM", "", "", "",
                        "", "", "", "Adjustment Transaction", "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    #region Insert Inventory Details
                    short Lno = 0;
                    foreach (GridViewRow gr in gvItmStkAdjDed.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblStore"));
                        var lblTranQty = (Label)(gr.FindControl("lblTranQty"));                        

                        var TranQty = Convert.ToDouble(lblTranQty.Text.Trim());
                        var TranRate = Convert.ToDouble(0);
                        var TranAmt = Convert.ToDecimal(TranQty * TranRate);

                        #region Get Item Details
                        var itemName = "";
                        var itemAcc = "";
                        var taItemDet = new tbl_InMa_Item_DetTableAdapter();
                        var dtItemDet = taItemDet.GetDataByItemRef(Convert.ToInt32(lblItemCode.Text.Trim()));
                        if (dtItemDet.Rows.Count > 0)
                        {
                            itemName = dtItemDet[0].Itm_Det_Desc.ToString();
                            itemAcc = dtItemDet[0].Itm_Det_Acc_Code.ToString();
                        }
                        else
                        {
                            myTran.Rollback();
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                        #endregion

                        Lno++;

                        if (TranQty > 0)
                        {
                            var dtStkCtl = taStkCtl.GetDataByStoreItem(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                            if (dtStkCtl.Rows.Count > 0)
                            {
                                TranQty = Math.Round((TranQty), 4) < Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk), 4) ? Math.Round(((dtStkCtl[0].Stk_Ctl_Cur_Stk - TranQty)), 4) : Math.Round(((TranQty - dtStkCtl[0].Stk_Ctl_Cur_Stk)), 4);

                                taInvDet.InsertInvDet(nextHdrRef.ToString(), trnType, "ADJ", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                    Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                    "", "", Lno, "", Convert.ToDateTime(txtTranDate.Text.Trim()), Convert.ToDateTime(txtTranDate.Text.Trim()),
                                    Convert.ToDouble(TranQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(TranQty) * Convert.ToDecimal(0),
                                    Convert.ToDecimal(TranQty) * Convert.ToDecimal(0), "", "", "", 0, 0, "1", "");
                            }
                        }
                    }
                    #endregion
                    
                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();                    

                    btnPrint.Visible = true;
                }
                else
                {
                    nextHdrRef = Convert.ToInt32(ddlTranList.SelectedValue.ToString());
                    nextHdrRefNo = ddlTranList.SelectedItem.ToString();

                    taInvHdr.UpdateInvHdrByHdrRef(strCode, strCode, strCode, "", Convert.ToDateTime(txtTranDate.Text.Trim()), txtRem.Text.Trim(), "", "", "", "", "", "", "", "",
                        "", Convert.ToDecimal(totAdjAmt), "H", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                        "ADM", "", "", "", "", "", "", "Adjustment Transaction", "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "",
                        Convert.ToInt32(nextHdrRef));

                    taInvDet.DeleteInvDetByHdrRef(ddlTranList.SelectedValue.ToString());

                    #region Insert Inventory Details
                    short Lno = 0;
                    foreach (GridViewRow gr in gvItmStkAdjDed.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblStore"));
                        var lblTranQty = (Label)(gr.FindControl("lblTranQty"));

                        var TranQty = Convert.ToDouble(lblTranQty.Text.Trim());
                        var TranRate = Convert.ToDouble(0);
                        var TranAmt = Convert.ToDecimal(TranQty * TranRate);

                        #region Get Item Details
                        var itemName = "";
                        var itemAcc = "";
                        var taItemDet = new tbl_InMa_Item_DetTableAdapter();
                        var dtItemDet = taItemDet.GetDataByItemRef(Convert.ToInt32(lblItemCode.Text.Trim()));
                        if (dtItemDet.Rows.Count > 0)
                        {
                            itemName = dtItemDet[0].Itm_Det_Desc.ToString();
                            itemAcc = dtItemDet[0].Itm_Det_Acc_Code.ToString();
                        }
                        else
                        {
                            myTran.Rollback();
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                        #endregion

                        Lno++;

                        if (totAdjQty > 0)
                        {
                            var dtStkCtl = taStkCtl.GetDataByStoreItem(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                            if (dtStkCtl.Rows.Count > 0)
                            {
                                TranQty = Math.Round((TranQty), 4) < Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk), 4) ? Math.Round(((dtStkCtl[0].Stk_Ctl_Cur_Stk - TranQty)), 4) : Math.Round(((TranQty - dtStkCtl[0].Stk_Ctl_Cur_Stk)), 4);

                                taInvDet.InsertInvDet(nextHdrRef.ToString(), trnType, "ADJ", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                    Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                    "", "", Lno, "", Convert.ToDateTime(txtTranDate.Text.Trim()), Convert.ToDateTime(txtTranDate.Text.Trim()),
                                    Convert.ToDouble(TranQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(TranQty) * Convert.ToDecimal(0),
                                    Convert.ToDecimal(TranQty) * Convert.ToDecimal(0), "", "", "", 0, 0, "1", "");
                            }
                        }
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnPrint.Visible = true;
                }

                ddlTranList.DataSource = taInvHdr.GetInvTranDataByTrnCode("ADJ");
                ddlTranList.DataTextField = "Trn_Hdr_Ref_No";
                ddlTranList.DataValueField = "Trn_Hdr_Ref";
                ddlTranList.DataBind();
                ddlTranList.Items.Insert(0, "----------New----------");
                ddlTranList.SelectedIndex = ddlTranList.Items.IndexOf(ddlTranList.Items.FindByText(nextHdrRefNo));

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
            GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();
            var taStkBal = new View_InTr_Trn_Det_Stock_BalTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taInvHdr.Connection);

            try
            {                
                var nextHdrRef = 1;
                var nextHdrRefNo = "";
                var trnType = "";
                var strCode = "";

                taInvHdr.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);
                taAcc.AttachTransaction(myTran);
                taStkCtl.AttachTransaction(myTran);
                taStkBal.AttachTransaction(myTran);

                if (hfEditStatus.Value == "N")
                {
                    if (gvItmStkAdjDed.Rows.Count > 0)
                    {
                        trnType = "IA";
                        //Inventory Header Ref
                        var dtMaxHdrRef = taInvHdr.GetMaxHdrRef();
                        nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;

                        var dtMaxMrrRef = taInvHdr.GetMaxHdrRefNo("ADJ", DateTime.Now.Year);
                        var nextMrrRef = dtMaxMrrRef == null ? 1 : Convert.ToInt32(dtMaxMrrRef) + 1;
                        nextHdrRefNo = "ECIL-ADJ-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextMrrRef).ToString("000000");

                        taInvHdr.InsertInvHdr(nextHdrRef, trnType, "ADJ", nextHdrRefNo, strCode, strCode, strCode,
                            "", Convert.ToDateTime(txtTranDate.Text.Trim()), txtRem.Text.Trim(), "", "", "", "", "", "", "", "", "", Convert.ToDecimal(totAdjAmt), "P",
                            (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), "ADM", "", "", "",
                            "", "", "", "Stock Adjustment", "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        //var dtMaxAccRef = taAcc.GetMaxRefNo("MJV");
                        //var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                        //var nextAccRefNo = "MJV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                        #region Insert Inventory Deduction Details
                        var jvLNo = 0;
                        short Lno = 0;
                        foreach (GridViewRow gr in gvItmStkAdjDed.Rows)
                        {
                            var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                            var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                            var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                            var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                            var lblStoreName = (Label)(gr.FindControl("lblStore"));
                            var lblTranQty = (Label)(gr.FindControl("lblTranQty"));

                            var TranQty = Convert.ToDouble(lblTranQty.Text.Trim());
                            var TranRate = Convert.ToDouble(1);
                            var TranAmt = Convert.ToDecimal(TranQty * TranRate);

                            #region Get Item Details
                            var itemName = "";
                            var itemAcc = "";
                            var taItemDet = new tbl_InMa_Item_DetTableAdapter();
                            var dtItemDet = taItemDet.GetDataByItemRef(Convert.ToInt32(lblItemCode.Text.Trim()));
                            if (dtItemDet.Rows.Count > 0)
                            {
                                itemName = dtItemDet[0].Itm_Det_Desc.ToString();
                                itemAcc = dtItemDet[0].Itm_Det_Acc_Code.ToString();
                            }
                            else
                            {
                                myTran.Rollback();
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                            #endregion

                            Lno++;

                            var qrySqlStr = "";
                            qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_InTr_Trn_Det_Stock_Bal]')) DROP VIEW [dbo].[View_InTr_Trn_Det_Stock_Bal]";
                            dbCon.ExecuteSQLStmt(qrySqlStr);

                            qrySqlStr = "Create view View_InTr_Trn_Det_Stock_Bal as SELECT Trn_Det_Icode, Trn_Det_Str_Code, " +
                                "SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'R' THEN Trn_Det_Lin_Qty ELSE 0 END) AS Rcv, " +
                                "SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'I' THEN Trn_Det_Lin_Qty ELSE 0 END) AS Isu, " +
                                "CAST(SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'R' THEN Trn_Det_Lin_Qty ELSE 0 END) " +
                                "  - SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'I' THEN Trn_Det_Lin_Qty ELSE 0 END) AS decimal(18, 4)) AS BalQty " +
                                "FROM tbl_InTr_Trn_Det  where CONVERT(date,Trn_Det_Book_Dat,103)<=CONVERT(date,'" + txtTranDate.Text + "',103) " +
                                "GROUP BY Trn_Det_Icode, Trn_Det_Str_Code";
                            dbCon.ExecuteSQLStmt(qrySqlStr);

                            double stkBal = 0;
                            var dtStkBal = taStkBal.GetDataByItemStore(lblItemCode.Text.Trim(), hfStoreRef.Value.ToString());
                            if (dtStkBal.Rows.Count > 0) stkBal = dtStkBal[0].BalQty;

                            TranQty = Convert.ToDouble(decimal.Round((Convert.ToDecimal(stkBal) - Convert.ToDecimal(TranQty)), 4));

                            if (TranQty > 0)
                            {
                                if (Convert.ToDecimal(stkBal) == Convert.ToDecimal(TranQty))
                                {
                                    myTran.Rollback();
                                    tblMsg.Rows[1].Cells[0].InnerText = "Adjstment Qty: [" + TranQty + "] is same as Stock Qty: [" + stkBal.ToString() + "]";
                                    tblMsg.Rows[1].Cells[0].InnerText = "Date: " + txtTranDate.Text + ", Item: " + lblItemDesc.Text + " , Store: " + lblStoreName.Text;
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }

                                if (TranQty > stkBal)
                                {
                                    myTran.Rollback();
                                    tblMsg.Rows[1].Cells[0].InnerText = "Adjstment Qty: [" + TranQty + "] is greater than Stock Qty: [" + stkBal.ToString() + "]";
                                    tblMsg.Rows[1].Cells[0].InnerText = "Date: " + txtTranDate.Text + ", Item: " + lblItemDesc.Text + " , Store: " + lblStoreName.Text;
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }
                                else
                                {
                                    var dtStkCtl = taStkCtl.GetDataByStoreItem(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                                    if (dtStkCtl.Rows.Count > 0)
                                    {
                                        if (Convert.ToDecimal(TranQty) > Convert.ToDecimal(dtStkCtl[0].Stk_Ctl_Cur_Stk))
                                        {
                                            myTran.Rollback();
                                            tblMsg.Rows[1].Cells[0].InnerText = "Adjstment Qty: [" + TranQty + "] is greater than Current Stock Qty: [" + dtStkCtl[0].Stk_Ctl_Cur_Stk.ToString() + "]";
                                            tblMsg.Rows[1].Cells[0].InnerText = "Date: " + DateTime.Now.ToString("dd/MM/yyyy") + ", Item: " + lblItemDesc.Text + " , Store: " + lblStoreName.Text;
                                            ModalPopupExtenderMsg.Show();
                                            return;
                                        }
                                        else
                                        {
                                            taInvDet.InsertInvDet(nextHdrRef.ToString(), trnType, "ADJ", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                                "", "", Lno, "", Convert.ToDateTime(txtTranDate.Text.Trim()), Convert.ToDateTime(txtTranDate.Text.Trim()),
                                                Convert.ToDouble(TranQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(TranQty) * Convert.ToDecimal(0),
                                                Convert.ToDecimal(TranQty) * Convert.ToDecimal(0), "", TranQty.ToString(), "", 0, stkBal, "1", "");

                                            if (trnType == "IA")
                                                taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk - Convert.ToDouble(TranQty)), 4), hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                                            else
                                                taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk + Convert.ToDouble(TranQty)), 4), hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                                        }
                                    }
                                    else
                                    {
                                        myTran.Rollback();
                                        tblMsg.Rows[0].Cells[0].InnerText = "Stock Control Ledger not found.";
                                        tblMsg.Rows[1].Cells[0].InnerText = "Item : " + lblItemDesc.Text + " , Store : " + lblStoreName.Text;
                                        ModalPopupExtenderMsg.Show();
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                myTran.Rollback();
                                tblMsg.Rows[0].Cells[0].InnerText = "Adjstment Qty: [" + TranQty + "] is Zero/Negative.";
                                tblMsg.Rows[1].Cells[0].InnerText = "Item : " + lblItemDesc.Text + " , Store : " + lblStoreName.Text;
                                ModalPopupExtenderMsg.Show();
                                return;
                            }

                            //#region Insert Accounts Voucher Entry
                            //jvLNo++;
                            ////Debit-Customer Account
                            //taAcc.InsertAccData(supAccCode.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                            //    nextAccRefNo.ToString(), jvLNo, 1, itemName.ToString(), "D", MrrAmt, nextHdrRefNo.ToString(), "0",
                            //    "BDT", 1, MrrAmt, "", "", "", "", "", "", "", "", "", "", "",
                            //    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                            //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, itemName.ToString(), DateTime.Now,
                            //    "ADM", "P", "", DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, MrrAmt, "",
                            //    "", "", "N", 1, 0, "", "", "", "J", 0, "1", "MJV");

                            //jvLNo++;
                            ////Credit-Item Account
                            //taAcc.InsertAccData(itemAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                            //    nextAccRefNo.ToString(), jvLNo, 1, itemName.ToString(), "C", MrrAmt, nextHdrRefNo.ToString(), "0",
                            //    "BDT", 1, MrrAmt, "", "", "", "", "", "", "", "", "", "", "",
                            //    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                            //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, supName.ToString(), DateTime.Now,
                            //    "ADM", "P", "", DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, MrrAmt, "",
                            //    "", "", "N", 1, 0, "", "", "", "J", 0, "1", "MJV");
                            //#endregion
                        }
                        #endregion
                    }

                    if (gvItmStkAdjAdd.Rows.Count > 0)
                    {
                        trnType = "RA";
                        //Inventory Header Ref
                        var dtMaxHdrRefAdd = taInvHdr.GetMaxHdrRef();
                        nextHdrRef = dtMaxHdrRefAdd == null ? 1 : Convert.ToInt32(dtMaxHdrRefAdd) + 1;

                        var dtMaxAdjAddRef = taInvHdr.GetMaxHdrRefNo("ADJ", DateTime.Now.Year);
                        var nextAdjAddRef = dtMaxAdjAddRef == null ? 1 : Convert.ToInt32(dtMaxAdjAddRef) + 1;
                        nextHdrRefNo = "ECIL-ADJ-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextAdjAddRef).ToString("000000");

                        taInvHdr.InsertInvHdr(nextHdrRef, trnType, "ADJ", nextHdrRefNo, strCode, strCode, strCode,
                            "", Convert.ToDateTime(txtTranDate.Text.Trim()), txtRem.Text.Trim(), "", "", "", "", "", "", "", "", "", Convert.ToDecimal(totAdjAmt), "P",
                            (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), "ADM", "", "", "",
                            "", "", "", "Stock Adjustment", "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        //var dtMaxAccRef = taAcc.GetMaxRefNo("MJV");
                        //var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                        //var nextAccRefNo = "MJV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                        #region Insert Inventory Addition Details
                        var jvLNoAdd = 0;
                        short LnoAdd = 0;
                        foreach (GridViewRow gr in gvItmStkAdjAdd.Rows)
                        {
                            var lblItemCode = (Label)(gr.FindControl("lblItemCodeAdd"));
                            var lblItemDesc = (Label)(gr.FindControl("lblItemDescAdd"));
                            var lblItemUnit = (Label)(gr.FindControl("lblItemUnitAdd"));
                            var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRefAdd"));
                            var lblStoreName = (Label)(gr.FindControl("lblStoreAdd"));
                            var lblTranQty = (Label)(gr.FindControl("lblTranQtyAdd"));

                            var TranQty = Convert.ToDouble(lblTranQty.Text.Trim());
                            var TranRate = Convert.ToDouble(0);
                            var TranAmt = Convert.ToDecimal(TranQty * TranRate);

                            #region Get Item Details
                            var itemName = "";
                            var itemAcc = "";
                            var taItemDet = new tbl_InMa_Item_DetTableAdapter();
                            var dtItemDet = taItemDet.GetDataByItemRef(Convert.ToInt32(lblItemCode.Text.Trim()));
                            if (dtItemDet.Rows.Count > 0)
                            {
                                itemName = dtItemDet[0].Itm_Det_Desc.ToString();
                                itemAcc = dtItemDet[0].Itm_Det_Acc_Code.ToString();
                            }
                            else
                            {
                                myTran.Rollback();
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                            #endregion

                            LnoAdd++;

                            var qrySqlStr = "";
                            qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_InTr_Trn_Det_Stock_Bal]')) DROP VIEW [dbo].[View_InTr_Trn_Det_Stock_Bal]";
                            dbCon.ExecuteSQLStmt(qrySqlStr);

                            qrySqlStr = "Create view View_InTr_Trn_Det_Stock_Bal as SELECT Trn_Det_Icode, Trn_Det_Str_Code, " +
                                "SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'R' THEN Trn_Det_Lin_Qty ELSE 0 END) AS Rcv, " +
                                "SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'I' THEN Trn_Det_Lin_Qty ELSE 0 END) AS Isu, " +
                                "CAST(SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'R' THEN Trn_Det_Lin_Qty ELSE 0 END) " +
                                "  - SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'I' THEN Trn_Det_Lin_Qty ELSE 0 END) AS decimal(18, 4)) AS BalQty " +
                                "FROM tbl_InTr_Trn_Det  where CONVERT(date,Trn_Det_Book_Dat,103)<=CONVERT(date,'" + txtTranDate.Text + "',103) " +
                                "GROUP BY Trn_Det_Icode, Trn_Det_Str_Code";
                            dbCon.ExecuteSQLStmt(qrySqlStr);

                            double stkBal = 0;
                            var dtStkBal = taStkBal.GetDataByItemStore(lblItemCode.Text.Trim(), hfStoreRef.Value.ToString());
                            if (dtStkBal.Rows.Count > 0) stkBal = dtStkBal[0].BalQty;

                            TranQty = TranQty - stkBal;

                            if (TranQty > 0)
                            {
                                if (Convert.ToDecimal(stkBal) == Convert.ToDecimal(TranQty))
                                {
                                    myTran.Rollback();
                                    tblMsg.Rows[1].Cells[0].InnerText = "Adjstment Qty: [" + TranQty + "] is same as Stock Qty: [" + stkBal.ToString() + "]";
                                    tblMsg.Rows[1].Cells[0].InnerText = "Date: " + txtTranDate.Text + ", Item: " + lblItemDesc.Text + " , Store: " + lblStoreName.Text;
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }

                                taInvDet.InsertInvDet(nextHdrRef.ToString(), trnType, "ADJ", nextHdrRefNo.ToString(), LnoAdd, "", 1, "",
                                    LnoAdd, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                    "", "", LnoAdd, "", Convert.ToDateTime(txtTranDate.Text.Trim()), Convert.ToDateTime(txtTranDate.Text.Trim()),
                                    Convert.ToDouble(TranQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(TranQty) * Convert.ToDecimal(0),
                                    Convert.ToDecimal(TranQty) * Convert.ToDecimal(0), "", TranQty.ToString(), "", 0, stkBal, "1", "");

                                var dtStkCtl = taStkCtl.GetDataByStoreItem(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                                if (dtStkCtl.Rows.Count > 0)
                                {
                                    if (trnType == "IA")
                                        taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk - Convert.ToDouble(TranQty)), 4), hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                                    else
                                        taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk + Convert.ToDouble(TranQty)), 4), hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                                }
                                else
                                {
                                    taStkCtl.InsertItemStore(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim(), "", Math.Round((Convert.ToDouble(TranQty)), 4), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                        DateTime.Now, DateTime.Now, "", "", "", 0);
                                }
                            }
                            else
                            {
                                myTran.Rollback();
                                tblMsg.Rows[0].Cells[0].InnerText = "Adjstment Qty: [" + TranQty + "] is Zero/Negative.";
                                tblMsg.Rows[1].Cells[0].InnerText = "Item : " + lblItemDesc.Text + " , Store : " + lblStoreName.Text;
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                            
                            //#region Insert Accounts Voucher Entry
                            //jvLNoAdd++;
                            ////Debit-Customer Account
                            //taAcc.InsertAccData(supAccCode.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                            //    nextAccRefNo.ToString(), jvLNo, 1, itemName.ToString(), "D", MrrAmt, nextHdrRefNo.ToString(), "0",
                            //    "BDT", 1, MrrAmt, "", "", "", "", "", "", "", "", "", "", "",
                            //    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                            //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, itemName.ToString(), DateTime.Now,
                            //    "ADM", "P", "", DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, MrrAmt, "",
                            //    "", "", "N", 1, 0, "", "", "", "J", 0, "1", "MJV");

                            //jvLNo++;
                            ////Credit-Item Account
                            //taAcc.InsertAccData(itemAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                            //    nextAccRefNo.ToString(), jvLNo, 1, itemName.ToString(), "C", MrrAmt, nextHdrRefNo.ToString(), "0",
                            //    "BDT", 1, MrrAmt, "", "", "", "", "", "", "", "", "", "", "",
                            //    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                            //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, supName.ToString(), DateTime.Now,
                            //    "ADM", "P", "", DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, MrrAmt, "",
                            //    "", "", "N", 1, 0, "", "", "", "J", 0, "1", "MJV");
                            //#endregion                            
                        }
                        #endregion
                    }

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    //btnHold.Enabled = false;
                    btnPost.Enabled = false;
                    btnPrint.Visible = true;
                }
                else
                {
                    nextHdrRef = Convert.ToInt32(ddlTranList.SelectedValue.ToString());
                    nextHdrRefNo = ddlTranList.SelectedItem.ToString();

                    var dtTrnHdr = taInvHdr.GetDataByHdrRef(nextHdrRef);
                    if (dtTrnHdr.Rows.Count > 0)
                    {
                        taInvHdr.UpdateInvHdrByHdrRef(strCode, strCode, strCode, "", Convert.ToDateTime(txtTranDate.Text.Trim()), txtRem.Text.Trim(), "", "", "", "", "", "", "", "",
                            "", Convert.ToDecimal(totAdjAmt), "P", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                            "ADM", "", "", "", "", "", "", "Stock Adjustment", "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                            "1", "", Convert.ToInt32(nextHdrRef));

                        taInvDet.DeleteInvDetByHdrRef(ddlTranList.SelectedValue.ToString());

                        //var dtMaxAccRef = taAcc.GetMaxRefNo("MJV");
                        //var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                        //var nextAccRefNo = "MJV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                        if (dtTrnHdr[0].Trn_Hdr_Type == "IA")
                        {
                            #region Insert Inventory Deduction Details
                            var jvLNo = 0;
                            short Lno = 0;
                            foreach (GridViewRow gr in gvItmStkAdjDed.Rows)
                            {
                                var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                                var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                                var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                                var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                                var lblStoreName = (Label)(gr.FindControl("lblStore"));
                                var lblTranQty = (Label)(gr.FindControl("lblTranQty"));

                                var TranQty = Convert.ToDouble(lblTranQty.Text.Trim());
                                var TranRate = Convert.ToDouble(1);
                                var TranAmt = Convert.ToDecimal(TranQty * TranRate);

                                #region Get Item Details
                                var itemName = "";
                                var itemAcc = "";
                                var taItemDet = new tbl_InMa_Item_DetTableAdapter();
                                var dtItemDet = taItemDet.GetDataByItemRef(Convert.ToInt32(lblItemCode.Text.Trim()));
                                if (dtItemDet.Rows.Count > 0)
                                {
                                    itemName = dtItemDet[0].Itm_Det_Desc.ToString();
                                    itemAcc = dtItemDet[0].Itm_Det_Acc_Code.ToString();
                                }
                                else
                                {
                                    myTran.Rollback();
                                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                                    tblMsg.Rows[1].Cells[0].InnerText = "";
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }
                                #endregion

                                Lno++;

                                var qrySqlStr = "";
                                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_InTr_Trn_Det_Stock_Bal]')) DROP VIEW [dbo].[View_InTr_Trn_Det_Stock_Bal]";
                                dbCon.ExecuteSQLStmt(qrySqlStr);

                                qrySqlStr = "Create view View_InTr_Trn_Det_Stock_Bal as SELECT Trn_Det_Icode, Trn_Det_Str_Code, " +
                                            "SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'R' THEN Trn_Det_Lin_Qty ELSE 0 END) AS Rcv, " +
                                            "SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'I' THEN Trn_Det_Lin_Qty ELSE 0 END) AS Isu, " +
                                            "CAST(SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'R' THEN Trn_Det_Lin_Qty ELSE 0 END) " +
                                            "  - SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'I' THEN Trn_Det_Lin_Qty ELSE 0 END) AS decimal(18, 4)) AS BalQty " +
                                            "FROM tbl_InTr_Trn_Det  where CONVERT(date,Trn_Det_Book_Dat,103)<=CONVERT(date,'" + txtTranDate.Text + "',103) " +
                                            "GROUP BY Trn_Det_Icode, Trn_Det_Str_Code";
                                dbCon.ExecuteSQLStmt(qrySqlStr);

                                double stkBal = 0;
                                var dtStkBal = taStkBal.GetDataByItemStore(lblItemCode.Text.Trim(), hfStoreRef.Value.ToString());
                                if (dtStkBal.Rows.Count > 0) stkBal = dtStkBal[0].BalQty;

                                TranQty = stkBal - TranQty;

                                if (TranQty > 0)
                                {
                                    if (Convert.ToDecimal(stkBal) == Convert.ToDecimal(TranQty))
                                    {
                                        myTran.Rollback();
                                        tblMsg.Rows[1].Cells[0].InnerText = "Adjstment Qty: [" + TranQty + "] is same as Stock Qty: [" + stkBal.ToString() + "]";
                                        tblMsg.Rows[1].Cells[0].InnerText = "Date: " + txtTranDate.Text + ", Item: " + lblItemDesc.Text + " , Store: " + lblStoreName.Text;
                                        ModalPopupExtenderMsg.Show();
                                        return;
                                    }

                                    if (TranQty > stkBal)
                                    {
                                        myTran.Rollback();
                                        tblMsg.Rows[1].Cells[0].InnerText = "Adjstment Qty: [" + TranQty + "] is greater than Stock Qty: [" + stkBal.ToString() + "]";
                                        tblMsg.Rows[1].Cells[0].InnerText = "Date: " + txtTranDate.Text + ", Item: " + lblItemDesc.Text + " , Store: " + lblStoreName.Text;
                                        ModalPopupExtenderMsg.Show();
                                        return;
                                    }
                                    else
                                    {
                                        var dtStkCtl = taStkCtl.GetDataByStoreItem(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                                        if (dtStkCtl.Rows.Count > 0)
                                        {
                                            if (Convert.ToDecimal(TranQty) > Convert.ToDecimal(dtStkCtl[0].Stk_Ctl_Cur_Stk))
                                            {
                                                myTran.Rollback();
                                                tblMsg.Rows[1].Cells[0].InnerText = "Adjstment Qty: [" + TranQty + "] is greater than Current Stock Qty: [" + dtStkCtl[0].Stk_Ctl_Cur_Stk.ToString() + "]";
                                                tblMsg.Rows[1].Cells[0].InnerText = "Date: " + DateTime.Now.ToString("dd/MM/yyyy") + ", Item: " + lblItemDesc.Text + " , Store: " + lblStoreName.Text;
                                                ModalPopupExtenderMsg.Show();
                                                return;
                                            }
                                            else
                                            {
                                                taInvDet.InsertInvDet(nextHdrRef.ToString(), "IA", "ADJ", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                                    Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                                    "", "", Lno, "", Convert.ToDateTime(txtTranDate.Text.Trim()), Convert.ToDateTime(txtTranDate.Text.Trim()),
                                                    Convert.ToDouble(TranQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(TranQty) * Convert.ToDecimal(0),
                                                    Convert.ToDecimal(TranQty) * Convert.ToDecimal(0), "", TranQty.ToString(), "", 0, stkBal, "1", "");

                                                if (trnType == "IA")
                                                    taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk - Convert.ToDouble(TranQty)), 4), hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                                                else
                                                    taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk + Convert.ToDouble(TranQty)), 4), hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                                            }
                                        }
                                        else
                                        {
                                            myTran.Rollback();
                                            tblMsg.Rows[0].Cells[0].InnerText = "Stock Control Ledger not found.";
                                            tblMsg.Rows[1].Cells[0].InnerText = "Item : " + lblItemDesc.Text + " , Store : " + lblStoreName.Text;
                                            ModalPopupExtenderMsg.Show();
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    myTran.Rollback();
                                    tblMsg.Rows[0].Cells[0].InnerText = "Adjstment Qty: [" + TranQty + "] is Zero/Negative.";
                                    tblMsg.Rows[1].Cells[0].InnerText = "Item : " + lblItemDesc.Text + " , Store : " + lblStoreName.Text;
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }

                                //#region Insert Accounts Voucher Entry
                                //jvLNo++;
                                ////Debit-Customer Account
                                //taAcc.InsertAccData(supAccCode.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                //    nextAccRefNo.ToString(), jvLNo, 1, itemName.ToString(), "D", MrrAmt, nextHdrRefNo.ToString(), "0",
                                //    "BDT", 1, MrrAmt, "", "", "", "", "", "", "", "", "", "", "",
                                //    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, itemName.ToString(), DateTime.Now,
                                //    "ADM", "P", "", DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, MrrAmt, "",
                                //    "", "", "N", 1, 0, "", "", "", "J", 0, "1", "MJV");

                                //jvLNo++;
                                ////Credit-Item Account
                                //taAcc.InsertAccData(itemAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                //    nextAccRefNo.ToString(), jvLNo, 1, itemName.ToString(), "C", MrrAmt, nextHdrRefNo.ToString(), "0",
                                //    "BDT", 1, MrrAmt, "", "", "", "", "", "", "", "", "", "", "",
                                //    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, supName.ToString(), DateTime.Now,
                                //    "ADM", "P", "", DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, MrrAmt, "",
                                //    "", "", "N", 1, 0, "", "", "", "J", 0, "1", "MJV");
                                //#endregion
                            }
                            #endregion
                        }
                        else
                        {
                            #region Insert Inventory Addition Details
                            var jvLNoAdd = 0;
                            short LnoAdd = 0;
                            foreach (GridViewRow gr in gvItmStkAdjAdd.Rows)
                            {
                                var lblItemCode = (Label)(gr.FindControl("lblItemCodeAdd"));
                                var lblItemDesc = (Label)(gr.FindControl("lblItemDescAdd"));
                                var lblItemUnit = (Label)(gr.FindControl("lblItemUnitAdd"));
                                var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRefAdd"));
                                var lblStoreName = (Label)(gr.FindControl("lblStoreAdd"));
                                var lblTranQty = (Label)(gr.FindControl("lblTranQtyAdd"));

                                var TranQty = Convert.ToDouble(lblTranQty.Text.Trim());
                                var TranRate = Convert.ToDouble(0);
                                var TranAmt = Convert.ToDecimal(TranQty * TranRate);

                                #region Get Item Details
                                var itemName = "";
                                var itemAcc = "";
                                var taItemDet = new tbl_InMa_Item_DetTableAdapter();
                                var dtItemDet = taItemDet.GetDataByItemRef(Convert.ToInt32(lblItemCode.Text.Trim()));
                                if (dtItemDet.Rows.Count > 0)
                                {
                                    itemName = dtItemDet[0].Itm_Det_Desc.ToString();
                                    itemAcc = dtItemDet[0].Itm_Det_Acc_Code.ToString();
                                }
                                else
                                {
                                    myTran.Rollback();
                                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                                    tblMsg.Rows[1].Cells[0].InnerText = "";
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }
                                #endregion

                                LnoAdd++;

                                var qrySqlStr = "";
                                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_InTr_Trn_Det_Stock_Bal]')) DROP VIEW [dbo].[View_InTr_Trn_Det_Stock_Bal]";
                                dbCon.ExecuteSQLStmt(qrySqlStr);

                                qrySqlStr = "Create view View_InTr_Trn_Det_Stock_Bal as SELECT Trn_Det_Icode, Trn_Det_Str_Code, " +
                                            "SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'R' THEN Trn_Det_Lin_Qty ELSE 0 END) AS Rcv, " +
                                            "SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'I' THEN Trn_Det_Lin_Qty ELSE 0 END) AS Isu, " +
                                            "CAST(SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'R' THEN Trn_Det_Lin_Qty ELSE 0 END) " +
                                            "  - SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'I' THEN Trn_Det_Lin_Qty ELSE 0 END) AS decimal(18, 4)) AS BalQty " +
                                            "FROM tbl_InTr_Trn_Det  where CONVERT(date,Trn_Det_Book_Dat,103)<=CONVERT(date,'" + txtTranDate.Text + "',103) " +
                                            "GROUP BY Trn_Det_Icode, Trn_Det_Str_Code";
                                dbCon.ExecuteSQLStmt(qrySqlStr);

                                double stkBal = 0;
                                var dtStkBal = taStkBal.GetDataByItemStore(lblItemCode.Text.Trim(), hfStoreRef.Value.ToString());
                                if (dtStkBal.Rows.Count > 0) stkBal = dtStkBal[0].BalQty;

                                TranQty = TranQty - stkBal;

                                if (TranQty > 0)
                                {
                                    if (Convert.ToDecimal(stkBal) == Convert.ToDecimal(TranQty))
                                    {
                                        myTran.Rollback();
                                        tblMsg.Rows[1].Cells[0].InnerText = "Adjstment Qty: [" + TranQty + "] is same as Stock Qty: [" + stkBal.ToString() + "]";
                                        tblMsg.Rows[1].Cells[0].InnerText = "Date: " + txtTranDate.Text + ", Item: " + lblItemDesc.Text + " , Store: " + lblStoreName.Text;
                                        ModalPopupExtenderMsg.Show();
                                        return;
                                    }

                                    taInvDet.InsertInvDet(nextHdrRef.ToString(), "RA", "ADJ", nextHdrRefNo.ToString(), LnoAdd, "", 1, "",
                                        LnoAdd, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                        "", "", LnoAdd, "", Convert.ToDateTime(txtTranDate.Text.Trim()), Convert.ToDateTime(txtTranDate.Text.Trim()),
                                        Convert.ToDouble(TranQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(TranQty) * Convert.ToDecimal(0),
                                        Convert.ToDecimal(TranQty) * Convert.ToDecimal(0), "", TranQty.ToString(), "", 0, stkBal, "1", "");

                                    var dtStkCtl = taStkCtl.GetDataByStoreItem(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                                    if (dtStkCtl.Rows.Count > 0)
                                    {
                                        if (trnType == "IA")
                                            taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk - Convert.ToDouble(TranQty)), 4), hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                                        else
                                            taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk + Convert.ToDouble(TranQty)), 4), hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                                    }
                                    else
                                    {
                                        taStkCtl.InsertItemStore(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim(), "", Math.Round((Convert.ToDouble(TranQty)), 4), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                            DateTime.Now, DateTime.Now, "", "", "", 0);
                                    }
                                }
                                else
                                {
                                    myTran.Rollback();
                                    tblMsg.Rows[0].Cells[0].InnerText = "Adjstment Qty: [" + TranQty + "] is Zero/Negative.";
                                    tblMsg.Rows[1].Cells[0].InnerText = "Item : " + lblItemDesc.Text + " , Store : " + lblStoreName.Text;
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }

                                //#region Insert Accounts Voucher Entry
                                //jvLNoAdd++;
                                ////Debit-Customer Account
                                //taAcc.InsertAccData(supAccCode.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                //    nextAccRefNo.ToString(), jvLNo, 1, itemName.ToString(), "D", MrrAmt, nextHdrRefNo.ToString(), "0",
                                //    "BDT", 1, MrrAmt, "", "", "", "", "", "", "", "", "", "", "",
                                //    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, itemName.ToString(), DateTime.Now,
                                //    "ADM", "P", "", DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, MrrAmt, "",
                                //    "", "", "N", 1, 0, "", "", "", "J", 0, "1", "MJV");

                                //jvLNo++;
                                ////Credit-Item Account
                                //taAcc.InsertAccData(itemAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                //    nextAccRefNo.ToString(), jvLNo, 1, itemName.ToString(), "C", MrrAmt, nextHdrRefNo.ToString(), "0",
                                //    "BDT", 1, MrrAmt, "", "", "", "", "", "", "", "", "", "", "",
                                //    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, supName.ToString(), DateTime.Now,
                                //    "ADM", "P", "", DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, MrrAmt, "",
                                //    "", "", "N", 1, 0, "", "", "", "J", 0, "1", "MJV");
                                //#endregion                            
                            }
                            #endregion
                        }

                        myTran.Commit();
                        tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();

                        //btnHold.Enabled = false;
                        btnPost.Enabled = false;                        
                        gvItmStkAdjDed.Enabled = false;
                        gvItmStkAdjAdd.Enabled = false;
                        btnPrint.Visible = true;
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Transaction data not found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }

                ddlTranList.DataSource = taInvHdr.GetInvTranDataByTrnCode("ADJ");
                ddlTranList.DataTextField = "Trn_Hdr_Ref_No";
                ddlTranList.DataValueField = "Trn_Hdr_Ref";
                ddlTranList.DataBind();
                ddlTranList.Items.Insert(0, "----------New----------");
                ddlTranList.SelectedIndex = ddlTranList.Items.IndexOf(ddlTranList.Items.FindByText(nextHdrRefNo));

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

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void gvItmStkAdjDed_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var dt = new DataTable();
            dt = (DataTable)ViewState["dtTranDet"];

            dt.Rows[rowNum].Delete();
            dt.AcceptChanges();

            gvItmStkAdjDed.EditIndex = -1;
            SetAdjDedGridData();

            int adjDedCnt = 0;
            foreach (GridViewRow gr in gvItmStkAdjDed.Rows)
            {
                adjDedCnt++;
                var lblTranQty = ((Label)(gr.FindControl("lblTranQty"))).Text.ToString();
                totAdjQty += Convert.ToDouble(lblTranQty.Trim());
            }
            lblItmStkAdjDed.Visible = adjDedCnt > 0;

            int adjAddCnt = 0;
            foreach (GridViewRow gr in gvItmStkAdjAdd.Rows)
            {
                adjAddCnt++;
                var lblTranQty = ((Label)(gr.FindControl("lblTranQtyAdd"))).Text.ToString();
                totAdjQty += Convert.ToDouble(lblTranQty.Trim());
            }
            lblItmStkAdjAdd.Visible = adjAddCnt > 0;

            if (gvItmStkAdjDed.Rows.Count > 0 || gvItmStkAdjAdd.Rows.Count > 0)
            {
                txtTranDate.ReadOnly = true;
                txtTranDate.Enabled = false;
                lblTotTranItem.Text = (gvItmStkAdjDed.Rows.Count + gvItmStkAdjAdd.Rows.Count).ToString();
                //lblTotTranItem.Text = totAdjQty.ToString("N2");
                lblTotTranVal.Text = totAdjAmt.ToString("N2");
                lblTotTranItem.Visible = true;
                lblTotTranVal.Visible = true;
                btnPost.Visible = true;
                //btnHold.Visible = true;
                btnPost.Enabled = true;
                //btnHold.Enabled = true;
            }
            else
            {
                txtTranDate.ReadOnly = false;
                txtTranDate.Enabled = true;
                lblTotTranItem.Text = "0";
                lblTotTranVal.Text = "0.00";
                lblTotTranItem.Visible = false;
                lblTotTranVal.Visible = false;
                btnPost.Visible = false;
                //btnHold.Visible = false;
                btnPost.Enabled = false;
                //btnHold.Enabled = false;
            }
        }

        protected void ddlTranList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            try
            {
                //reportInfo();

                if (ddlTranList.SelectedIndex == 0)
                {
                    ClearData();
                }
                else
                {
                    LoadInitAdjDedGridData();
                    SetAdjDedGridData();

                    LoadInitAdjAddGridData();
                    SetAdjAddGridData();

                    var dtInvHdr = taInvHdr.GetDataByHdrRef(Convert.ToInt32(ddlTranList.SelectedValue.ToString()));
                    if (dtInvHdr.Rows.Count > 0)
                    {                        
                        txtRem.Text = "";
                        txtItemName.Text = "";
                        ddlTranItemUom.SelectedIndex = 0;
                        ddlTranStore.SelectedIndex = 0;
                        txtCurStock.Text = "";
                        txtTranQty.Text = "";

                        hfEditStatus.Value = "Y";

                        txtTranDate.Text = dtInvHdr[0].Trn_Hdr_Date.ToString("dd/MM/yyyy");

                        txtRem.Text = dtInvHdr[0].Trn_Hdr_Com1.ToString();

                        if (dtInvHdr[0].Trn_Hdr_Type == "IA")
                        {                            
                            var dt = new DataTable();
                            dt = (DataTable)ViewState["dtTranDet"];
                            var dtAdjDed = taInvDet.GetDataByHdrRef(ddlTranList.SelectedValue.ToString());
                            foreach (dsInvTran.tbl_InTr_Trn_DetRow dr in dtAdjDed.Rows)
                            {
                                dt.Rows.Add(dr.Trn_Hdr_Ref, dr.Trn_Hdr_Ref, dr.Trn_Det_Ref, dr.Trn_Det_Icode, dr.Trn_Det_Itm_Desc, dr.Trn_Det_Itm_Uom, dr.Trn_Det_Itm_Uom,
                                    dr.Trn_Det_Str_Code, dr.Trn_Det_Str_Code, dr.Trn_Det_Bal_Qty, dr.T_C2);
                            }
                            ViewState["dtTranDet"] = dt;
                            SetAdjDedGridData();
                        }
                        else
                        {                            
                            var dt = new DataTable();
                            dt = (DataTable)ViewState["dtTranDetAdd"];
                            var dtAdjAdd = taInvDet.GetDataByHdrRef(ddlTranList.SelectedValue.ToString());
                            foreach (dsInvTran.tbl_InTr_Trn_DetRow dr in dtAdjAdd.Rows)
                            {
                                dt.Rows.Add(dr.Trn_Hdr_Ref, dr.Trn_Hdr_Ref, dr.Trn_Det_Ref, dr.Trn_Det_Icode, dr.Trn_Det_Itm_Desc, dr.Trn_Det_Itm_Uom, dr.Trn_Det_Itm_Uom,
                                    dr.Trn_Det_Str_Code, dr.Trn_Det_Str_Code, dr.Trn_Det_Bal_Qty, dr.T_C2);
                            }
                            ViewState["dtTranDetAdd"] = dt;
                            SetAdjAddGridData();
                        }

                        int adjDedCnt = 0;
                        foreach (GridViewRow gr in gvItmStkAdjDed.Rows)
                        {
                            adjDedCnt++;
                            var lblTranQty = ((Label)(gr.FindControl("lblTranQty"))).Text.ToString();
                            totAdjQty += Convert.ToDouble(lblTranQty.Trim());
                        }
                        lblItmStkAdjDed.Visible = adjDedCnt > 0;

                        int adjAddCnt = 0;
                        foreach (GridViewRow gr in gvItmStkAdjAdd.Rows)
                        {
                            adjAddCnt++;
                            var lblTranQty = ((Label)(gr.FindControl("lblTranQtyAdd"))).Text.ToString();
                            totAdjQty += Convert.ToDouble(lblTranQty.Trim());
                        }
                        lblItmStkAdjAdd.Visible = adjAddCnt > 0;

                        if (gvItmStkAdjDed.Rows.Count > 0 || gvItmStkAdjAdd.Rows.Count > 0)
                        {
                            lblTotTranItem.Text = (gvItmStkAdjDed.Rows.Count + gvItmStkAdjAdd.Rows.Count).ToString();
                            lblTotTranVal.Text = totAdjAmt.ToString("N2");
                            lblTotTranItem.Visible = true;
                            lblTotTranVal.Visible = true;
                            btnPost.Visible = true;
                            //btnHold.Visible = true;
                            btnPost.Enabled = true;
                            //btnHold.Enabled = true;
                        }
                        else
                        {
                            lblTotTranItem.Text = "0";
                            lblTotTranVal.Text = "0.00";
                            lblTotTranItem.Visible = false;
                            lblTotTranVal.Visible = false;
                            btnPost.Visible = false;
                            //btnHold.Visible = false;
                            btnPost.Enabled = false;
                            //btnHold.Enabled = false;
                        }

                        if (dtInvHdr[0].Trn_Hdr_HRPB_Flag == "P")
                        {
                            //btnHold.Enabled = false;
                            btnPost.Enabled = false;
                            gvItmStkAdjDed.Enabled = false;
                            gvItmStkAdjAdd.Enabled = false;
                            btnAddAdjDet.Enabled = false;
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

        private void ClearData()
        {
            try
            {
                ddlTranList.Items.Clear();

                var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();

                var dtMaxHdrRef = taInvHdr.GetMaxHdrRefNo("ADJ", DateTime.Now.Year);
                var nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;
                var nextHdrRefNo = "ECIL-ADJ-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRef).ToString("000000");
                lblTranRefNo.Text = nextHdrRefNo;

                ddlTranList.DataSource = taInvHdr.GetInvTranDataByTrnCode("ADJ");
                ddlTranList.DataTextField = "Trn_Hdr_Ref_No";
                ddlTranList.DataValueField = "Trn_Hdr_Ref";
                ddlTranList.DataBind();
                ddlTranList.Items.Insert(0, new ListItem("----------New----------", "0"));

                txtTranDate.Text = DateTime.Now.ToString("dd/MM/yyy");
                txtRem.Text = "";                

                txtTranQty.Text = "";
                txtCurStock.Text = "";

                txtItemName.Text = "";
                ddlTranItemUom.SelectedIndex = 0;
                ddlTranStore.SelectedIndex = 0;

                lblTotTranItem.Text = "0.00";
                lblTotTranVal.Text = "0.00";
                btnAddAdjDet.Enabled = true;
                btnPost.Visible = false;
                //btnHold.Visible = false;
                btnPrint.Visible = false;

                hfEditStatus.Value = "N";
                hfRefNo.Value = "0";

                lblItmStkAdjDed.Visible = false;
                LoadInitAdjDedGridData();
                SetAdjDedGridData();
                gvItmStkAdjDed.Enabled = true;

                lblItmStkAdjAdd.Visible = false;
                LoadInitAdjAddGridData();
                SetAdjAddGridData();
                gvItmStkAdjAdd.Enabled = true;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void cboItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtenderSrchItem.ContextKey = cboItemType.SelectedValue.ToString();
            txtItemName.Text = "";
            ddlTranItemUom.SelectedIndex = 0;            
        }

        protected void ddlTranStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Old_code
            //var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();

            //try
            //{
            //    var itemRef = "";
            //    var itemName = "";
            //    var srchWords = txtItemName.Text.Trim().Split(':');
            //    foreach (string word in srchWords)
            //    {
            //        itemRef = word;
            //        break;
            //    }

            //    if (itemRef.Length > 0)
            //    {
            //        int result;
            //        if (int.TryParse(itemRef, out result))
            //        {
            //            var taItem = new tbl_InMa_Item_DetTableAdapter();
            //            var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(itemRef));
            //            if (dtItem.Rows.Count > 0)
            //            {
            //                itemRef = dtItem[0].Itm_Det_Ref.ToString();
            //                itemName = dtItem[0].IsItm_Det_DescNull() ? "0" : dtItem[0].Itm_Det_Desc.ToString();
            //            }
            //            else
            //            {
            //                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
            //                tblMsg.Rows[1].Cells[0].InnerText = "";
            //                ModalPopupExtenderMsg.Show();
            //                return;
            //            }
            //        }
            //        else
            //        {
            //            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
            //            tblMsg.Rows[1].Cells[0].InnerText = "";
            //            ModalPopupExtenderMsg.Show();
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
            //        tblMsg.Rows[1].Cells[0].InnerText = "";
            //        ModalPopupExtenderMsg.Show();
            //        return;
            //    }

            //    if (ddlTranStore.SelectedIndex == 0)
            //    {
            //        txtCurStock.Text = "";
            //        txtTranQty.Text = "";
            //    }
            //    else
            //    {
            //        var dtStkCtl = taStkCtl.GetDataByStoreItem(ddlTranStore.SelectedValue, itemRef.ToString());
            //        if (dtStkCtl.Rows.Count > 0)
            //        {
            //            txtCurStock.Text = dtStkCtl[0].Stk_Ctl_Cur_Stk.ToString();
            //            txtTranQty.Text = dtStkCtl[0].Stk_Ctl_Cur_Stk.ToString();
            //        }
            //        else
            //        {
            //            txtCurStock.Text = "0";
            //            txtTranQty.Text = "0";
            //        }
            //    }
            //}
            //catch (Exception ex) 
            //{
            //    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
            //    tblMsg.Rows[1].Cells[0].InnerText = "";
            //    ModalPopupExtenderMsg.Show();
            //}
            #endregion

            GetStockBalance();
        }

        private void GetStockBalance()
        {
            GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

            try
            {
                #region Get Item Ref
                var itemRef = "";
                var itemName = "";
                var srchWords = txtItemName.Text.Trim().Split(':');
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
                            txtCurStock.Text = "0";
                            return;
                        }
                    }
                    else
                    {
                        txtCurStock.Text = "0";
                        return;
                    }
                }
                else
                {
                    txtCurStock.Text = "0";
                    return;
                }
                #endregion

                var qrySqlStr = "";
                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_InTr_Trn_Det_Stock_Bal]')) DROP VIEW [dbo].[View_InTr_Trn_Det_Stock_Bal]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                //qrySqlStr = "Create view View_InTr_Trn_Det_Stock_Bal as SELECT Trn_Det_Icode, Trn_Det_Str_Code, " +
                //    "SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'R' THEN Trn_Det_Lin_Qty ELSE 0 END) AS Rcv, " +
                //    "SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'I' THEN Trn_Det_Lin_Qty ELSE 0 END) AS Isu, " +
                //    "SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'R' THEN Trn_Det_Lin_Qty ELSE 0 END) " +
                //    "- SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'I' THEN Trn_Det_Lin_Qty ELSE 0 END) AS BalQty " +
                //    "FROM dbo.tbl_InTr_Trn_Det  left outer join tbl_InTr_Trn_Hdr on tbl_InTr_Trn_Det.Trn_Hdr_Ref=tbl_InTr_Trn_Hdr.Trn_Hdr_Ref " +
                //    "and Trn_Det_Type=Trn_Hdr_Type and Trn_Det_Code=Trn_Hdr_Code where Trn_Det_Icode='" + itemRef.ToString() + "' " +
                //    "and Trn_Det_Str_Code='" + ddlTranStore.SelectedValue.ToString() + "' " +
                //    "and CONVERT(date,Trn_Hdr_Date,103)<=CONVERT(date,'" + txtTranDate.Text + "',103) " +
                //    "GROUP BY Trn_Det_Icode, Trn_Det_Str_Code";

                qrySqlStr = "Create view View_InTr_Trn_Det_Stock_Bal as SELECT Trn_Det_Icode, Trn_Det_Str_Code, " +
                    "SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'R' THEN Trn_Det_Lin_Qty ELSE 0 END) AS Rcv, " +
                    "SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'I' THEN Trn_Det_Lin_Qty ELSE 0 END) AS Isu, " +
                    "CAST(SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'R' THEN Trn_Det_Lin_Qty ELSE 0 END) " +
                    "  - SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'I' THEN Trn_Det_Lin_Qty ELSE 0 END) AS decimal(18, 4)) AS BalQty " +
                    "FROM tbl_InTr_Trn_Det  where CONVERT(date,Trn_Det_Book_Dat,103)<=CONVERT(date,'" + txtTranDate.Text + "',103) " +
                    "GROUP BY Trn_Det_Icode, Trn_Det_Str_Code";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                var taStkBal = new View_InTr_Trn_Det_Stock_BalTableAdapter();
                var dtStkBal = taStkBal.GetDataByItemStore(itemRef.ToString(), ddlTranStore.SelectedValue.ToString());
                if (dtStkBal.Rows.Count > 0)
                    txtCurStock.Text = dtStkBal[0].BalQty.ToString("N4");
                else
                    txtCurStock.Text = "0";
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var itemRef = "";
                var srchWords = txtItemName.Text.Trim().Split(':');
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

        protected void txtItemName_TextChanged(object sender, EventArgs e)
        {
            #region Old_Code
            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();

            try
            {
                var itemRef = "";
                var srchWords = txtItemName.Text.Trim().Split(':');
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

                        var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(itemRef));
                        if (dtItem.Rows.Count > 0)
                        {
                            ddlTranItemUom.SelectedValue = dtItem[0].IsItm_Det_Stk_UnitNull() ? "0" : dtItem[0].Itm_Det_Stk_Unit.ToString();
                            //if (ddlTranStore.SelectedIndex != 0)
                            //{
                            //    var dtStkCtl = taStkCtl.GetDataByStoreItem(ddlTranStore.SelectedValue.ToString(), itemRef.ToString());
                            //    txtCurStock.Text = dtStkCtl.Rows.Count > 0 ? dtStkCtl[0].Stk_Ctl_Cur_Stk.ToString() : "0";
                            //}

                            GetStockBalance();
                        }
                        else
                        {
                            ddlTranItemUom.SelectedIndex = 0;
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        ddlTranItemUom.SelectedIndex = 0;
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    ddlTranItemUom.SelectedIndex = 0;
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                ddlTranItemUom.SelectedIndex = 0;
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
            #endregion            
        }

        protected void gvItmStkAdjAdd_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var dt = new DataTable();
            dt = (DataTable)ViewState["dtTranDetAdd"];

            dt.Rows[rowNum].Delete();
            dt.AcceptChanges();

            gvItmStkAdjAdd.EditIndex = -1;
            SetAdjAddGridData();

            int adjDedCnt = 0;
            foreach (GridViewRow gr in gvItmStkAdjDed.Rows)
            {
                adjDedCnt++;
                var lblTranQty = ((Label)(gr.FindControl("lblTranQty"))).Text.ToString();
                totAdjQty += Convert.ToDouble(lblTranQty.Trim());
            }
            lblItmStkAdjDed.Visible = adjDedCnt > 0;

            int adjAddCnt = 0;
            foreach (GridViewRow gr in gvItmStkAdjAdd.Rows)
            {
                adjAddCnt++;
                var lblTranQty = ((Label)(gr.FindControl("lblTranQtyAdd"))).Text.ToString();
                totAdjQty += Convert.ToDouble(lblTranQty.Trim());
            }
            lblItmStkAdjAdd.Visible = adjAddCnt > 0;

            if (gvItmStkAdjDed.Rows.Count > 0 || gvItmStkAdjAdd.Rows.Count > 0)
            {
                txtTranDate.ReadOnly = true;
                txtTranDate.Enabled = false;
                lblTotTranItem.Text = (gvItmStkAdjDed.Rows.Count + gvItmStkAdjAdd.Rows.Count).ToString();
                //lblTotTranItem.Text = totAdjQty.ToString("N2");
                lblTotTranVal.Text = totAdjAmt.ToString("N2");
                lblTotTranItem.Visible = true;
                lblTotTranVal.Visible = true;
                btnPost.Visible = true;
                //btnHold.Visible = true;
                btnPost.Enabled = true;
                //btnHold.Enabled = true;
            }
            else
            {
                txtTranDate.ReadOnly = false;
                txtTranDate.Enabled = true;
                lblTotTranItem.Text = "0";
                lblTotTranVal.Text = "0.00";
                lblTotTranItem.Visible = false;
                lblTotTranVal.Visible = false;
                btnPost.Visible = false;
                //btnHold.Visible = false;
                btnPost.Enabled = false;
                //btnHold.Enabled = false;
            }
        }

        protected void txtTranDate_TextChanged(object sender, EventArgs e)
        {
            GetStockBalance();
        }
    }
}