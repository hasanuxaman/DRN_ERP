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
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Inventory.Forms
{
    public partial class frmStockTransfer : System.Web.UI.Page
    {
        double totIssQty = 0;

        string rptFilePath;
        string rptFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            AutoCompleteExtenderSrchItem.ContextKey = "0";

            rptFormula = "";
            rptFilePath = "";
            Session["RptDateFrom"] = txtTranDate.Text.Trim();
            Session["RptDateTo"] = txtTranDate.Text.Trim();
            Session["RptFilePath"] = rptFilePath;
            Session["RptFormula"] = rptFormula;

            txtTranDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();

            var dtMaxHdrRef = taInvHdr.GetMaxHdrRefNo("STR", DateTime.Now.Year);
            var nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;
            var nextHdrRefNo = "ECIL-STR-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRef).ToString("000000");
            lblIssRefNo.Text = nextHdrRefNo;
            txtTranRef.Text = nextHdrRefNo.ToString();

            ddlTransferList.DataSource = taInvHdr.GetInvTranDataList("IT", "STR");
            ddlTransferList.DataTextField = "Trn_Hdr_Ref_No";
            ddlTransferList.DataValueField = "Trn_Hdr_Ref";
            ddlTransferList.DataBind();
            ddlTransferList.Items.Insert(0, "----------New----------");

            //var taSaleItem = new tbl_InMa_Item_DetTableAdapter();
            //var dtSaleItem = taSaleItem.GetRmItemData();
            //cboItem.DataSource = dtSaleItem;
            //cboItem.DataTextField = "Itm_Det_Desc";
            //cboItem.DataValueField = "Itm_Det_Ref";
            //cboItem.DataBind();
            //cboItem.Items.Insert(0, new ListItem("----------Select----------", "0"));

            var taItemType = new tbl_InMa_TypeTableAdapter();
            var dtItemType = taItemType.GetDataByAsc();
            cboItemType.DataSource = dtItemType;
            cboItemType.DataTextField = "Item_Type_Name";
            cboItemType.DataValueField = "Item_Type_Code";
            cboItemType.DataBind();
            cboItemType.Items.Insert(0, new ListItem("-----Select-----", "0"));

            var taItemUom = new tbl_InMa_UomTableAdapter();
            var dtItemUom = taItemUom.GetData();
            cboItemUom.DataSource = dtItemUom;
            cboItemUom.DataTextField = "Uom_Name";
            cboItemUom.DataValueField = "Uom_Code";
            cboItemUom.DataBind();
            cboItemUom.Items.Insert(0, new ListItem("----", "0"));

            var taStore = new tbl_InMa_Str_LocTableAdapter();
            var dtStore = taStore.GetDataBySortName();
            foreach(dsInvMas.tbl_InMa_Str_LocRow dr in dtStore.Rows)
            {
                ddlFromStore.Items.Add(new ListItem(dr.Str_Loc_Name, dr.Str_Loc_Ref.ToString()));
            }
            ddlFromStore.Items.Insert(0, new ListItem("---Select---", "0"));
            
            foreach (dsInvMas.tbl_InMa_Str_LocRow dr in dtStore.Rows)
            {
                ddlToStore.Items.Add(new ListItem(dr.Str_Loc_Name, dr.Str_Loc_Ref.ToString()));
            }
            ddlToStore.Items.Insert(0, new ListItem("---Select---", "0"));

            LoadInitIssDetGridData();
            SetIssDetGridData();
        }

        #region Issue Details Gridview
        protected void LoadInitIssDetGridData()
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
            dt.Columns.Add("TRN_QTY", typeof(string));
            dt.Columns.Add("TRN_RATE", typeof(string));
            dt.Columns.Add("TRN_AMOUNT", typeof(string));            
            ViewState["dtTranDet"] = dt;
        }

        protected void SetIssDetGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtTranDet"];

                gvIssDet.DataSource = dt;
                gvIssDet.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            Page.Validate("btnSave");

            if (!Page.IsValid) return;

            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();

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

                foreach (GridViewRow gr in gvIssDet.Rows)
                {
                    var lblItemCode = ((Label)(gr.FindControl("lblItemCode"))).Text.ToString();
                    if (itemRef.Trim().ToString() == lblItemCode.ToString())
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = itemName.ToString() + " already addred to transfer details.";
                        tblMsg.Rows[1].Cells[0].InnerText = "To add more you need to delete existing same item.";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                if (txtTranQty.Text.Trim() == "" || txtTranQty.Text.Trim().Length <= 0 || Convert.ToDouble(txtTranQty.Text.Trim()) <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter valid Quantity.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                var dtStkCtl = taStkCtl.GetDataByStoreItem(ddlFromStore.SelectedValue, itemRef.ToString());
                if (dtStkCtl.Rows.Count > 0)
                {
                    if (dtStkCtl[0].Stk_Ctl_Cur_Stk < Convert.ToDouble(txtTranQty.Text))
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to transfer more than stock quantity.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "There is no stock.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                var dt = new DataTable();
                dt = (DataTable)ViewState["dtTranDet"];

                var TRN_HDR_REF = "0";
                var TRN_DET_REF = "0";
                var TRN_DET_REF_NO = "0";
                var TRN_ITEM_REF = itemRef.ToString();
                var TRN_ITEM_NAME = itemName.ToString();
                var TRN_ITEM_UOM_REF = cboItemUom.SelectedValue.ToString();
                var TRN_ITEM_UOM = cboItemUom.SelectedItem.ToString();
                var TRN_STORE_REF = "";
                var TRN_STORE_NAME = "";
                var TRN_QTY = Convert.ToDouble(txtTranQty.Text.Trim().Length > 0 ? txtTranQty.Text.Trim() : "0");
                var TRN_RATE = Convert.ToDouble(0);
                var TRN_AMOUNT = (TRN_QTY * TRN_RATE).ToString("N2");

                dt.Rows.Add(TRN_HDR_REF, TRN_DET_REF, TRN_DET_REF_NO, TRN_ITEM_REF, TRN_ITEM_NAME, TRN_ITEM_UOM_REF, TRN_ITEM_UOM, TRN_STORE_REF, TRN_STORE_NAME, TRN_QTY, 
                    TRN_RATE.ToString("N2"), TRN_AMOUNT);

                ViewState["dtTranDet"] = dt;
                SetIssDetGridData();

                foreach (GridViewRow gr in gvIssDet.Rows)
                {
                    var lblIssQty = ((Label)(gr.FindControl("lblIssQty"))).Text.ToString();
                    totIssQty += Convert.ToDouble(lblIssQty.Trim());
                }

                if (gvIssDet.Rows.Count > 0)
                {
                    lblTotMrrItem.Text = gvIssDet.Rows.Count.ToString();
                    //lblTotMrrItem.Text = totMrrQty.ToString("N2");
                    lblTotIssQty.Text = totIssQty.ToString("N2");
                    lblTotMrrItem.Visible = true;
                    lblTotIssQty.Visible = true;                    
                    btnPost.Enabled = true;
                    btnHold.Enabled = true;
                }
                else
                {
                    lblTotMrrItem.Text = "0";
                    lblTotIssQty.Text = "0.00";
                    lblTotMrrItem.Visible = false;
                    lblTotIssQty.Visible = false;                    
                    btnPost.Enabled = false;
                    btnHold.Enabled = false;
                }

                txtItemName.Text = "";
                cboItemUom.SelectedIndex = 0;                
                txtTranQty.Text = "";                
                txtStock.Text = "0";                
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void btnHold_Click(object sender, EventArgs e)
        {
            Page.Validate("btnSave");

            if (!Page.IsValid) return;

            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taInvHdr.Connection);

            try
            {           
                var nextHdrRef = 1;
                var nextHdrRefNo = "";

                taInvHdr.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);

                foreach (GridViewRow gr in gvIssDet.Rows)
                {
                    var lblIssQty = ((Label)(gr.FindControl("lblIssQty"))).Text.ToString();
                    totIssQty += Convert.ToDouble(lblIssQty.Trim());
                }

                if (hfEditStatus.Value == "N")
                {
                    //Inventory Header Ref
                    var dtMaxHdrRef = taInvHdr.GetMaxHdrRef();
                    nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;

                    var dtMaxMrrRef = taInvHdr.GetMaxHdrRefNo("STR", DateTime.Now.Year);
                    var nextMrrRef = dtMaxMrrRef == null ? 1 : Convert.ToInt32(dtMaxMrrRef) + 1;
                    nextHdrRefNo = "ECIL-STR-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextMrrRef).ToString("000000");

                    taInvHdr.InsertInvHdr(nextHdrRef, "IT", "STR", nextHdrRefNo, ddlFromStore.SelectedValue.ToString(), ddlToStore.SelectedValue.ToString(), "",
                        "", DateTime.Now, txtRem.Text.Trim(), "", "", "", "", "", "", "", "", "", Convert.ToDecimal(0), "H",
                        (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), "ADM", "", "", "",
                        "", "", "", "STR to " + ddlToStore.SelectedItem.ToString(), "", "", 0, DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    //Inventory Header Ref
                    var dtMaxHdrRefNew = taInvHdr.GetMaxHdrRef();
                    var nextHdrRefNew = dtMaxHdrRefNew == null ? 1 : Convert.ToInt32(dtMaxHdrRefNew) + 1;

                    taInvHdr.InsertInvHdr(nextHdrRefNew, "RT", "STR", nextHdrRefNo, ddlFromStore.SelectedValue.ToString(), ddlToStore.SelectedValue.ToString(), "",
                        "", DateTime.Now, txtRem.Text.Trim(), "", "", "", "", "", "", "", "", "", Convert.ToDecimal(0), "H",
                        (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), "ADM", "", "", "",
                        "", "", "", "STR from " + ddlFromStore.SelectedItem.ToString(), "", "", 0, DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    #region Insert Inventory Details
                    short Lno = 0;
                    foreach (GridViewRow gr in gvIssDet.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblStore"));

                        var lblIssQty = (Label)(gr.FindControl("lblIssQty"));
                        var IssQty = Convert.ToDouble(lblIssQty.Text.Trim());

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

                        if (IssQty > 0)
                        {
                            taInvDet.InsertInvDet(nextHdrRef.ToString(), "IT", "STR", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(),ddlFromStore.SelectedValue.ToString(), "",
                                "", "", Lno, "", DateTime.Now, DateTime.Now,
                                Convert.ToDouble(IssQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(IssQty) * Convert.ToDecimal(0),
                                Convert.ToDecimal(IssQty) * Convert.ToDecimal(0), "", "", "", 0, 0, "1", "");

                            taInvDet.InsertInvDet(nextHdrRefNew.ToString(), "RT", "STR", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), ddlToStore.SelectedValue.ToString(), "",
                                "", "", Lno, "", DateTime.Now, DateTime.Now,
                                Convert.ToDouble(IssQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(IssQty) * Convert.ToDecimal(0),
                                Convert.ToDecimal(IssQty) * Convert.ToDecimal(0), "", "", "", 0, 0, "1", "");
                        }
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
                    nextHdrRef = Convert.ToInt32(ddlTransferList.SelectedValue.ToString());
                    nextHdrRefNo = ddlTransferList.SelectedItem.ToString();

                    taInvHdr.UpdateInvHdrByTypeCodeHdrRefNo(ddlFromStore.SelectedValue.ToString(), ddlToStore.SelectedValue.ToString(), "", "", DateTime.Now, txtRem.Text.Trim(),
                        "", "", "", "", "", "", "", "", "", Convert.ToDecimal(0), "H", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                        "ADM", "", "", "", "", "", "", "STR to " + ddlToStore.SelectedItem.ToString(), "", "", 0, DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "",
                        "IT", "STR", nextHdrRefNo);

                    taInvHdr.UpdateInvHdrByTypeCodeHdrRefNo(ddlFromStore.SelectedValue.ToString(), ddlToStore.SelectedValue.ToString(), "", "", DateTime.Now, txtRem.Text.Trim(),
                        "", "", "", "", "", "", "", "", "", Convert.ToDecimal(0), "H", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                        "ADM", "", "", "", "", "", "", "STR from " + ddlFromStore.SelectedItem.ToString(), "", "", 0, DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "",
                        "RT", "STR", nextHdrRefNo);

                    taInvDet.DeleteInvDetByHdrRefNo(ddlTransferList.SelectedItem.ToString());

                    #region Insert Inventory Details
                    short Lno = 0;
                    foreach (GridViewRow gr in gvIssDet.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblStore"));

                        var lblIssQty = (Label)(gr.FindControl("lblIssQty"));
                        var IssQty = Convert.ToDouble(lblIssQty.Text.Trim());

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

                        if (IssQty > 0)
                        {
                            taInvDet.InsertInvDet(nextHdrRef.ToString(), "IT", "STR", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), ddlFromStore.SelectedValue.ToString(), "",
                                "", "", Lno, "", DateTime.Now, DateTime.Now,
                                Convert.ToDouble(IssQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(IssQty) * Convert.ToDecimal(0),
                                Convert.ToDecimal(IssQty) * Convert.ToDecimal(0), "", "", "", 0, 0, "1", "");

                            taInvDet.InsertInvDet((nextHdrRef + 1).ToString(), "RT", "STR", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), ddlToStore.SelectedValue.ToString(), "",
                                "", "", Lno, "", DateTime.Now, DateTime.Now,
                                Convert.ToDouble(IssQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(IssQty) * Convert.ToDecimal(0),
                                Convert.ToDecimal(IssQty) * Convert.ToDecimal(0), "", "", "", 0, 0, "1", "");
                        }
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnPrint.Enabled = true;
                }

                ddlTransferList.DataSource = taInvHdr.GetInvTranDataList("IT", "STR");
                ddlTransferList.DataTextField = "Trn_Hdr_Ref_No";
                ddlTransferList.DataValueField = "Trn_Hdr_Ref";
                ddlTransferList.DataBind();
                ddlTransferList.Items.Insert(0, "----------New----------");
                ddlTransferList.SelectedIndex = ddlTransferList.Items.IndexOf(ddlTransferList.Items.FindByText(nextHdrRefNo));

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

            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taInvHdr.Connection);

            try
            {
                if (ddlFromStore.SelectedValue == ddlToStore.SelectedValue)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to transfer item between same store.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                var nextHdrRef = 1;
                var nextHdrRefNo = "";

                taInvHdr.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);
                taAcc.AttachTransaction(myTran);
                taStkCtl.AttachTransaction(myTran);

                foreach (GridViewRow gr in gvIssDet.Rows)
                {
                    var lblIssQty = ((Label)(gr.FindControl("lblIssQty"))).Text.ToString();
                    totIssQty += Convert.ToDouble(lblIssQty.Trim());
                }

                if (hfEditStatus.Value == "N")
                {
                    //Inventory Header Ref
                    var dtMaxHdrRef = taInvHdr.GetMaxHdrRef();
                    nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;

                    var dtMaxMrrRef = taInvHdr.GetMaxHdrRefNo("STR", DateTime.Now.Year);
                    var nextMrrRef = dtMaxMrrRef == null ? 1 : Convert.ToInt32(dtMaxMrrRef) + 1;
                    nextHdrRefNo = "ECIL-STR-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextMrrRef).ToString("000000");

                    taInvHdr.InsertInvHdr(nextHdrRef, "IT", "STR", nextHdrRefNo, ddlFromStore.SelectedValue.ToString(), ddlToStore.SelectedValue.ToString(), "",
                        "", DateTime.Now, txtRem.Text.Trim(), "", "", "", "", "", "", "", "", "", Convert.ToDecimal(0), "P",
                        (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), "ADM", "", "", "",
                        "", "", "", ddlToStore.SelectedItem.ToString(), "", "H", 0, DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    //Inventory Header Ref
                    var dtMaxHdrRefNew = taInvHdr.GetMaxHdrRef();
                    var nextHdrRefNew = dtMaxHdrRefNew == null ? 1 : Convert.ToInt32(dtMaxHdrRefNew) + 1;

                    taInvHdr.InsertInvHdr(nextHdrRefNew, "RT", "STR", nextHdrRefNo, ddlFromStore.SelectedValue.ToString(), ddlToStore.SelectedValue.ToString(), "",
                        "", DateTime.Now, txtRem.Text.Trim(), "", "", "", "", "", "", "", "", "", Convert.ToDecimal(0), "P",
                        (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), "ADM", "", "", "",
                        "", "", "", ddlFromStore.SelectedItem.ToString(), "", "H", 0, DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    //var dtMaxAccRef = taAcc.GetMaxRefNo("MJV");
                    //var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                    //var nextAccRefNo = "MJV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                    #region Insert Inventory Details
                    short Lno = 0;
                    foreach (GridViewRow gr in gvIssDet.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblStore"));

                        var lblIssQty = (Label)(gr.FindControl("lblIssQty"));
                        var IssQty = Convert.ToDouble(lblIssQty.Text.Trim());

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

                        if (IssQty > 0)
                        {
                            taInvDet.InsertInvDet(nextHdrRef.ToString(), "IT", "STR", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), ddlFromStore.SelectedValue.ToString(), "",
                                "", "", Lno, "", DateTime.Now, DateTime.Now,
                                Convert.ToDouble(IssQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(IssQty) * Convert.ToDecimal(0),
                                Convert.ToDecimal(IssQty) * Convert.ToDecimal(0), "", "", "H", 0, 0, "1", "");

                            var dtStkCtlIsu = taStkCtl.GetDataByStoreItem(ddlFromStore.SelectedValue.ToString(), lblItemCode.Text.Trim());
                            if (dtStkCtlIsu.Rows.Count > 0)
                                if (IssQty > dtStkCtlIsu[0].Stk_Ctl_Cur_Stk)
                                {
                                    myTran.Rollback();
                                    tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to transfer qty [" + IssQty.ToString() + "] more than stock qty [" + dtStkCtlIsu[0].Stk_Ctl_Cur_Stk.ToString() + "]";
                                    tblMsg.Rows[1].Cells[0].InnerText = "Store: " + ddlFromStore.SelectedItem.ToString();
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }
                                else
                                    taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtlIsu[0].Stk_Ctl_Cur_Stk - Convert.ToDouble(IssQty)), 4), ddlFromStore.SelectedValue.ToString(), lblItemCode.Text.Trim());
                            else
                                taStkCtl.InsertItemStore(ddlFromStore.SelectedValue.ToString(), lblItemCode.Text.Trim(), "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                    DateTime.Now, DateTime.Now, "", "", "", 0);

                            taInvDet.InsertInvDet(nextHdrRefNew.ToString(), "RT", "STR", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), ddlToStore.SelectedValue.ToString(), "",
                                "", "", Lno, "", DateTime.Now, DateTime.Now,
                                Convert.ToDouble(IssQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(IssQty) * Convert.ToDecimal(0),
                                Convert.ToDecimal(IssQty) * Convert.ToDecimal(0), "", "", "H", 0, 0, "1", "");

                            var dtStkCtlRcv = taStkCtl.GetDataByStoreItem(ddlToStore.SelectedValue.ToString(), lblItemCode.Text.Trim());
                            if (dtStkCtlRcv.Rows.Count > 0)
                                taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtlRcv[0].Stk_Ctl_Cur_Stk + Convert.ToDouble(IssQty)), 4), ddlToStore.SelectedValue.ToString(), lblItemCode.Text.Trim());
                            else
                                taStkCtl.InsertItemStore(ddlToStore.SelectedValue.ToString(), lblItemCode.Text.Trim(), "", Math.Round((Convert.ToDouble(IssQty)), 4), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                    DateTime.Now, DateTime.Now, "", "", "", 0);
                        }
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnHold.Enabled = false;
                    btnPost.Enabled = false;
                    btnPrint.Enabled = true;
                }
                else
                {
                    nextHdrRef = Convert.ToInt32(ddlTransferList.SelectedValue.ToString());
                    nextHdrRefNo = ddlTransferList.SelectedItem.ToString();

                    taInvHdr.UpdateInvHdrByTypeCodeHdrRefNo(ddlFromStore.SelectedValue.ToString(), ddlToStore.SelectedValue.ToString(), "", "", DateTime.Now, txtRem.Text.Trim(),
                        "", "", "", "", "", "", "", "", "", Convert.ToDecimal(0), "P", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                        "ADM", "", "", "", "", "", "", ddlToStore.SelectedItem.ToString(), "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "",
                        "IT", "STR", nextHdrRefNo);

                    taInvHdr.UpdateInvHdrByTypeCodeHdrRefNo(ddlFromStore.SelectedValue.ToString(), ddlToStore.SelectedValue.ToString(), "", "", DateTime.Now, txtRem.Text.Trim(),
                        "", "", "", "", "", "", "", "", "", Convert.ToDecimal(0), "P", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                        "ADM", "", "", "", "", "", "", ddlFromStore.SelectedItem.ToString(), "", "", 0, DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "",
                        "RT", "STR", nextHdrRefNo);

                    taInvDet.DeleteInvDetByHdrRefNo(ddlTransferList.SelectedItem.ToString());

                    //var dtMaxAccRef = taAcc.GetMaxRefNo("MJV");
                    //var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                    //var nextAccRefNo = "MJV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                    #region Insert Inventory Details
                    short Lno = 0;
                    foreach (GridViewRow gr in gvIssDet.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblStore"));

                        var lblIssQty = (Label)(gr.FindControl("lblIssQty"));
                        var IssQty = Convert.ToDouble(lblIssQty.Text.Trim());

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

                        if (IssQty > 0)
                        {
                            taInvDet.InsertInvDet(nextHdrRef.ToString(), "IT", "STR", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), ddlFromStore.SelectedValue.ToString(), "",
                                "", "", Lno, "", DateTime.Now, DateTime.Now,
                                Convert.ToDouble(IssQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(IssQty) * Convert.ToDecimal(0),
                                Convert.ToDecimal(IssQty) * Convert.ToDecimal(0), "", "", "", 0, 0, "1", "");

                            var dtStkCtlIsu = taStkCtl.GetDataByStoreItem(ddlFromStore.SelectedValue.ToString(), lblItemCode.Text.Trim());
                            if (dtStkCtlIsu.Rows.Count > 0)
                                if (IssQty > dtStkCtlIsu[0].Stk_Ctl_Cur_Stk)
                                {
                                    myTran.Rollback();
                                    tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to transfer qty [" + IssQty.ToString() + "] more than stock qty [" + dtStkCtlIsu[0].Stk_Ctl_Cur_Stk.ToString() + "]";
                                    tblMsg.Rows[1].Cells[0].InnerText = "Store: " + ddlFromStore.SelectedItem.ToString();
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }
                                else
                                taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtlIsu[0].Stk_Ctl_Cur_Stk - Convert.ToDouble(IssQty)), 4), ddlFromStore.SelectedValue.ToString(), lblItemCode.Text.Trim());
                            else
                                taStkCtl.InsertItemStore(ddlFromStore.SelectedValue.ToString(), lblItemCode.Text.Trim(), "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                    DateTime.Now, DateTime.Now, "", "", "", 0);

                            taInvDet.InsertInvDet((nextHdrRef + 1).ToString(), "RT", "STR", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), ddlToStore.SelectedValue.ToString(), "",
                                "", "", Lno, "", DateTime.Now, DateTime.Now,
                                Convert.ToDouble(IssQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(IssQty) * Convert.ToDecimal(0),
                                Convert.ToDecimal(IssQty) * Convert.ToDecimal(0), "", "", "", 0, 0, "1", "");

                            var dtStkCtlRcv = taStkCtl.GetDataByStoreItem(ddlToStore.SelectedValue.ToString(), lblItemCode.Text.Trim());
                            if (dtStkCtlRcv.Rows.Count > 0)
                                taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtlRcv[0].Stk_Ctl_Cur_Stk + Convert.ToDouble(IssQty)), 4), ddlToStore.SelectedValue.ToString(), lblItemCode.Text.Trim());
                            else
                                taStkCtl.InsertItemStore(ddlToStore.SelectedValue.ToString(), lblItemCode.Text.Trim(), "", Math.Round((Convert.ToDouble(IssQty)), 4), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                    DateTime.Now, DateTime.Now, "", "", "", 0);
                        }
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnHold.Enabled = false;
                    btnPost.Enabled = false;
                    btnPrint.Enabled = true;
                }

                ddlTransferList.DataSource = taInvHdr.GetInvTranDataList("IT", "STR");
                ddlTransferList.DataTextField = "Trn_Hdr_Ref_No";
                ddlTransferList.DataValueField = "Trn_Hdr_Ref";
                ddlTransferList.DataBind();
                ddlTransferList.Items.Insert(0, "----------New----------");
                ddlTransferList.SelectedIndex = ddlTransferList.Items.IndexOf(ddlTransferList.Items.FindByText(nextHdrRefNo));

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
            //reportInfo();
            //var url = "frmShowSalesReport.aspx";
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);            

            rptFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Type}='IT' AND {View_InTr_Trn_Hdr_Det.Trn_Hdr_Code}='STR' AND {View_InTr_Trn_Hdr_Det.Trn_Hdr_Ref_No}='" + ddlTransferList.SelectedItem.ToString() + "'";

            rptFilePath = "~/Module/Inventory/Reports/rptInvStkTran.rpt";

            Session["RptDateFrom"] = txtTranDate.Text.Trim();
            Session["RptDateTo"] = txtTranDate.Text.Trim();
            Session["RptFilePath"] = rptFilePath;
            Session["RptFormula"] = rptFormula;
        }

        protected void gvIssDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var dt = new DataTable();
            dt = (DataTable)ViewState["dtTranDet"];

            dt.Rows[rowNum].Delete();
            dt.AcceptChanges();

            gvIssDet.EditIndex = -1;
            SetIssDetGridData();

            foreach (GridViewRow gr in gvIssDet.Rows)
            {
                var lblIssQty = ((Label)(gr.FindControl("lblIssQty"))).Text.ToString();
                totIssQty += Convert.ToDouble(lblIssQty.Trim());
            }

            if (gvIssDet.Rows.Count > 0)
            {
                lblTotMrrItem.Text = gvIssDet.Rows.Count.ToString();
                //lblTotMrrItem.Text = totMrrQty.ToString("N2");
                lblTotIssQty.Text = totIssQty.ToString("N2");
                lblTotMrrItem.Visible = true;
                lblTotIssQty.Visible = true;                
                btnPost.Enabled = true;
                btnHold.Enabled = true;
            }
            else
            {
                lblTotMrrItem.Text = "0";
                lblTotIssQty.Text = "0.00";
                lblTotMrrItem.Visible = false;
                lblTotIssQty.Visible = false;                
                btnPost.Enabled = false;
                btnHold.Enabled = false;
            }
        }

        protected void ddlTransferList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            try
            {
                rptFormula = "";
                rptFilePath = "";
                Session["RptDateFrom"] = txtTranDate.Text.Trim();
                Session["RptDateTo"] = txtTranDate.Text.Trim();
                Session["RptFilePath"] = rptFilePath;
                Session["RptFormula"] = rptFormula;

                if (ddlTransferList.SelectedIndex == 0)
                {
                    ClearData();
                }
                else
                {
                    var dtInvHdr = taInvHdr.GetDataByHdrRefNo("IT", "STR", ddlTransferList.SelectedItem.ToString());
                    if (dtInvHdr.Rows.Count > 0)
                    {                                                     
                        txtTranQty.Text = "";

                        cboItemUom.SelectedIndex = 0;

                        hfEditStatus.Value = "Y";

                        lblIssRefNo.Text = dtInvHdr[0].Trn_Hdr_Ref_No.ToString();
                        txtTranRef.Text = dtInvHdr[0].Trn_Hdr_Ref_No.ToString();
                        txtTranDate.Text = dtInvHdr[0].Trn_Hdr_Date.ToString("dd/MM/yyyy");
                        //txtReqRef.Text = dtInvHdr[0].Trn_Hdr_Tran_Ref.ToString();
                        txtRem.Text = dtInvHdr[0].Trn_Hdr_Com1.ToString();

                        ddlFromStore.SelectedIndex = ddlFromStore.Items.IndexOf(ddlFromStore.Items.FindByValue(dtInvHdr[0].Trn_Hdr_Pcode.ToString()));
                        ddlToStore.SelectedIndex = ddlToStore.Items.IndexOf(ddlToStore.Items.FindByValue(dtInvHdr[0].Trn_Hdr_Dcode.ToString()));

                        LoadInitIssDetGridData();
                        SetIssDetGridData();
                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtTranDet"];
                        var dtTranDet = taInvDet.GetDataByHdrRefNo("IT", "STR", ddlTransferList.SelectedItem.ToString());
                        foreach (dsInvTran.tbl_InTr_Trn_DetRow dr in dtTranDet.Rows)
                        {
                            dt.Rows.Add(dr.Trn_Hdr_Ref, dr.Trn_Hdr_Ref, dr.Trn_Det_Ref, dr.Trn_Det_Icode, dr.Trn_Det_Itm_Desc, dr.Trn_Det_Itm_Uom, dr.Trn_Det_Itm_Uom,
                                dr.Trn_Det_Str_Code, dr.Trn_Det_Str_Code, dr.Trn_Det_Lin_Qty, dr.Trn_Det_Lin_Rat.ToString("N2"), dr.Trn_Det_Lin_Net);
                        }
                        ViewState["dtTranDet"] = dt;
                        SetIssDetGridData();

                        foreach (GridViewRow gr in gvIssDet.Rows)
                        {
                            var lblIssQty = ((Label)(gr.FindControl("lblIssQty"))).Text.ToString();
                            totIssQty += Convert.ToDouble(lblIssQty.Trim());
                        }

                        if (gvIssDet.Rows.Count > 0)
                        {
                            lblTotMrrItem.Text = gvIssDet.Rows.Count.ToString();
                            lblTotIssQty.Text = totIssQty.ToString("N2");
                            lblTotMrrItem.Visible = true;
                            lblTotIssQty.Visible = true;                            
                        }
                        else
                        {
                            lblTotMrrItem.Text = "0";
                            lblTotIssQty.Text = "0.00";
                            lblTotMrrItem.Visible = false;
                            lblTotIssQty.Visible = false;                            
                        }

                        if (dtInvHdr[0].Trn_Hdr_HRPB_Flag == "P")
                        {
                            btnHold.Enabled = false;
                            btnPost.Enabled = false;
                            gvIssDet.Enabled = false;
                            btnAddItem.Enabled = false;
                        }
                        else
                        {
                            btnHold.Enabled = true;
                            btnPost.Enabled = true;
                            gvIssDet.Enabled = true;
                            btnAddItem.Enabled = true;
                        }

                        btnPrint.Enabled = true;
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
                ddlTransferList.Items.Clear();

                var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();

                var dtMaxHdrRef = taInvHdr.GetMaxHdrRefNo("STR", DateTime.Now.Year);
                var nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;
                var nextHdrRefNo = "ECIL-STR-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRef).ToString("000000");
                lblIssRefNo.Text = nextHdrRefNo;
                txtTranRef.Text = nextHdrRefNo.ToString();

                ddlTransferList.DataSource = taInvHdr.GetInvTranDataList("IT", "STR");
                ddlTransferList.DataTextField = "Trn_Hdr_Ref_No";
                ddlTransferList.DataValueField = "Trn_Hdr_Ref";
                ddlTransferList.DataBind();
                ddlTransferList.Items.Insert(0, "----------New----------");

                txtTranDate.Text = DateTime.Now.ToString("dd/MM/yyy");                
                ddlFromStore.SelectedIndex = 0;
                ddlToStore.SelectedIndex = 0;
                txtRem.Text = "";

                txtTranQty.Text = "";

                cboItemType.SelectedIndex = 0;
                txtItemName.Text = "";
                //cboItem.SelectedIndex = 0;
                cboItemUom.SelectedIndex = 0;                

                lblTotMrrItem.Text = "0.00";
                lblTotIssQty.Text = "0.00";
                btnAddItem.Enabled = true;
                btnPost.Enabled = false;
                btnHold.Enabled = false;
                btnPrint.Enabled = false;

                hfEditStatus.Value = "N";
                hfRefNo.Value = "0";

                LoadInitIssDetGridData();
                SetIssDetGridData();
                gvIssDet.Enabled = true;
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
            cboItemUom.SelectedIndex = 0;
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
                            cboItemUom.SelectedValue = dtItem[0].IsItm_Det_Stk_UnitNull() ? "0" : dtItem[0].Itm_Det_Stk_Unit.ToString();
                            if (ddlFromStore.SelectedIndex != 0)
                            {
                                var dtStkCtl = taStkCtl.GetDataByStoreItem(ddlFromStore.SelectedValue.ToString(), itemRef.ToString());
                                txtStock.Text = dtStkCtl.Rows.Count > 0 ? Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk), 4).ToString() : "0";
                            }
                        }
                        else
                        {
                            cboItemUom.SelectedIndex = 0;
                            txtStock.Text = "0";
                        }
                    }
                    else
                    {
                        cboItemUom.SelectedIndex = 0;
                        txtStock.Text = "0";
                    }
                }
                else
                {
                    cboItemUom.SelectedIndex = 0;
                    txtStock.Text = "0";
                }
            }
            catch (Exception ex)
            {
                cboItemUom.SelectedIndex = 0;
                txtStock.Text = "0";
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();                
            }
        }

        protected void ddlFromStore_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                            cboItemUom.SelectedValue = dtItem[0].IsItm_Det_Stk_UnitNull() ? "0" : dtItem[0].Itm_Det_Stk_Unit.ToString();
                            if (ddlFromStore.SelectedIndex != 0)
                            {
                                var dtStkCtl = taStkCtl.GetDataByStoreItem(ddlFromStore.SelectedValue.ToString(), itemRef.ToString());
                                txtStock.Text = dtStkCtl.Rows.Count > 0 ? Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk), 4).ToString() : "0";
                            }
                        }
                        else
                        {
                            cboItemUom.SelectedIndex = 0;
                            txtStock.Text = "0";
                        }
                    }
                    else
                    {
                        cboItemUom.SelectedIndex = 0;
                        txtStock.Text = "0";
                    }
                }
                else
                {
                    cboItemUom.SelectedIndex = 0;
                    txtStock.Text = "0";
                }
            }
            catch (Exception ex)
            {
                cboItemUom.SelectedIndex = 0;
                txtStock.Text = "0";
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
    }
}