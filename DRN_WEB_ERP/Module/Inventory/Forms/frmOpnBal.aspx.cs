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
    public partial class frmOpnBal : System.Web.UI.Page
    {
        double totOpnQty = 0;
        double totOpnAmt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            AutoCompleteExtenderSrchItem.ContextKey = "0";

            btnPrint.Attributes.Add("onclick", "javascript:w= window.open('frmShowSalesReport.aspx');");            

            txtTranDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();

            var dtMaxHdrRef = taInvHdr.GetMaxHdrRefNo("OPN", DateTime.Now.Year);
            var nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;
            var nextHdrRefNo = "ECIL-OPN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRef).ToString("000000");
            lblTranRefNo.Text = nextHdrRefNo;

            ddlTranList.DataSource = taInvHdr.GetInvTranDataList("RC", "OPN");
            ddlTranList.DataTextField = "Trn_Hdr_Ref_No";
            ddlTranList.DataValueField = "Trn_Hdr_Ref";
            ddlTranList.DataBind();
            ddlTranList.Items.Insert(0, "----------New----------");

            var taItemType = new tbl_InMa_TypeTableAdapter();
            var dtItemType = taItemType.GetDataByAsc();
            cboItemType.DataSource = dtItemType;
            cboItemType.DataTextField = "Item_Type_Name";
            cboItemType.DataValueField = "Item_Type_Code";
            cboItemType.DataBind();
            cboItemType.Items.Insert(0, new ListItem("-----Select-----", "0"));

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

            LoadInitOpnBalDetGridData();
            SetOpnBalDetGridData();

            txtTranQty.Attributes.Add("onkeyup", "CalcOpnAmount('" + txtTranQty.ClientID + "', '" + txtTranRate.ClientID + "', '" + txtOpnAmt.ClientID + "' )");
            txtTranRate.Attributes.Add("onkeyup", "CalcOpnAmount('" + txtTranQty.ClientID + "', '" + txtTranRate.ClientID + "', '" + txtOpnAmt.ClientID + "' )");
        }

        #region Opening Balance Details Gridview
        protected void LoadInitOpnBalDetGridData()
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

        protected void SetOpnBalDetGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtTranDet"];

                gvOpnBalDet.DataSource = dt;
                gvOpnBalDet.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion

        protected void btnAddOpnDet_Click(object sender, EventArgs e)
        {
            Page.Validate("btnAdd");

            if (!Page.IsValid) return;

            var taItmTranDet = new tbl_InTr_Trn_DetTableAdapter();

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

                var dtItmTranDet = taItmTranDet.GetDataByItemStore(itemRef.ToString(), ddlTranStore.SelectedValue.ToString());
                if (dtItmTranDet.Rows.Count > 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Opening balance entry is not possible.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Item Transaction already exists for this same store.";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                
                foreach (GridViewRow gr in gvOpnBalDet.Rows)
                {
                    var lblItemCode = ((Label)(gr.FindControl("lblItemCode"))).Text.ToString();
                    var hfStoreRef = ((HiddenField)(gr.FindControl("hfStoreRef"))).Value.ToString();
                    if (itemRef.ToString() == lblItemCode.ToString())
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = itemRef.ToString() + " already addred to Opening Balance details.";
                        tblMsg.Rows[1].Cells[0].InnerText = "To add more you need to delete existing same item.";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }                    
                }

                if (txtOpnAmt.Text.Trim() == "" || txtOpnAmt.Text.Trim().Length <= 0 || Convert.ToDouble(txtOpnAmt.Text.Trim()) <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter valid data.";
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
                var TRN_ITEM_UOM_REF = ddlTranItemUom.SelectedValue.ToString();
                var TRN_ITEM_UOM = ddlTranItemUom.SelectedItem.ToString();
                var TRN_STORE_REF = ddlTranStore.SelectedValue.ToString();
                var TRN_STORE_NAME = ddlTranStore.SelectedItem.ToString();
                var TRN_QTY = Convert.ToDouble(txtTranQty.Text.Trim().Length > 0 ? txtTranQty.Text.Trim() : "0");
                var TRN_RATE = Convert.ToDouble(txtTranRate.Text.Trim().Length > 0 ? txtTranRate.Text.Trim() : "0");
                var TRN_AMOUNT = (TRN_QTY * TRN_RATE).ToString("N2");

                dt.Rows.Add(TRN_HDR_REF, TRN_DET_REF, TRN_DET_REF_NO, TRN_ITEM_REF, TRN_ITEM_NAME, TRN_ITEM_UOM_REF, TRN_ITEM_UOM, TRN_STORE_REF, TRN_STORE_NAME, TRN_QTY,
                    TRN_RATE.ToString("N2"), TRN_AMOUNT);

                ViewState["dtTranDet"] = dt;
                SetOpnBalDetGridData();

                foreach (GridViewRow gr in gvOpnBalDet.Rows)
                {
                    var lblOpnQty = ((Label)(gr.FindControl("lblOpnQty"))).Text.ToString();
                    totOpnQty += Convert.ToDouble(lblOpnQty.Trim());

                    var lblOpnAmt = ((Label)(gr.FindControl("lblOpnAmt"))).Text.ToString();
                    totOpnAmt += Convert.ToDouble(lblOpnAmt.Trim());
                }

                if (gvOpnBalDet.Rows.Count > 0)
                {
                    lblTotTranItem.Text = gvOpnBalDet.Rows.Count.ToString();
                    //lblTotTranItem.Text = totOpnQty.ToString("N2");
                    lblTotTranVal.Text = totOpnAmt.ToString("N2");
                    lblTotTranItem.Visible = true;
                    lblTotTranVal.Visible = true;
                    btnPost.Visible = true;
                    btnHold.Visible = true;
                    btnPost.Enabled = true;
                    btnHold.Enabled = true;
                }
                else
                {
                    lblTotTranItem.Text = "0";
                    lblTotTranVal.Text = "0.00";
                    lblTotTranItem.Visible = false;
                    lblTotTranVal.Visible = false;
                    btnPost.Visible = false;
                    btnHold.Visible = false;
                    btnPost.Enabled = false;
                    btnHold.Enabled = false;
                }

                txtTranQty.Text = "";
                txtTranRate.Text = "";
                txtOpnAmt.Text = "";
                txtItemName.Text = "";
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
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taInvHdr.Connection);

            try
            {
                var nextHdrRef = 1;
                var nextHdrRefNo = "";
                var strCode = "";

                taInvHdr.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);

                foreach (GridViewRow gr in gvOpnBalDet.Rows)
                {
                    var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                    strCode = hfStoreRef.Value.ToString();

                    var lblOpnQty = ((Label)(gr.FindControl("lblOpnQty"))).Text.ToString();
                    totOpnQty += Convert.ToDouble(lblOpnQty.Trim());

                    var lblOpnAmt = ((Label)(gr.FindControl("lblOpnAmt"))).Text.ToString();
                    totOpnAmt += Convert.ToDouble(lblOpnAmt.Trim());
                }

                if (hfEditStatus.Value == "N")
                {
                    //Inventory Header Ref
                    var dtMaxHdrRef = taInvHdr.GetMaxHdrRef();
                    nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;

                    var dtMaxMrrRef = taInvHdr.GetMaxHdrRefNo("OPN", Convert.ToDateTime(txtTranDate.Text.Trim()).Year);
                    var nextMrrRef = dtMaxMrrRef == null ? 1 : Convert.ToInt32(dtMaxMrrRef) + 1;
                    nextHdrRefNo = "ECIL-OPN-" + Convert.ToDateTime(txtTranDate.Text.Trim()).Month.ToString("00") + Convert.ToDateTime(txtTranDate.Text.Trim()).ToString("yy") + "-" + Convert.ToInt32(nextMrrRef).ToString("000000");

                    taInvHdr.InsertInvHdr(nextHdrRef, "RC", "OPN", nextHdrRefNo, strCode, strCode, strCode,
                        "", Convert.ToDateTime(txtTranDate.Text.Trim()), txtRem.Text.Trim(), "", "", "", "", "", "", "", "", "", Convert.ToDecimal(totOpnAmt), "H",
                        (Convert.ToDateTime(txtTranDate.Text.Trim()).Month.ToString("00") + "/" + Convert.ToDateTime(txtTranDate.Text.Trim()).Year.ToString()).ToString(), "ADM", "", "", "",
                        "", "", "", "Opening Transaction", "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    #region Insert Inventory Details
                    short Lno = 0;
                    foreach (GridViewRow gr in gvOpnBalDet.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblStore"));

                        var lblOpnQty = (Label)(gr.FindControl("lblOpnQty"));
                        var lblOpnRate = (Label)(gr.FindControl("lblOpnRate"));

                        var OpnQty = Convert.ToDouble(lblOpnQty.Text.Trim());
                        var OpnRate = Convert.ToDouble(lblOpnRate.Text.Trim());
                        var OpnAmt = Convert.ToDecimal(OpnQty * OpnRate);

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

                        if (totOpnQty > 0)
                        {
                            taInvDet.InsertInvDet(nextHdrRef.ToString(), "RC", "OPN", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                "", "", Lno, "", Convert.ToDateTime(txtTranDate.Text.Trim()), Convert.ToDateTime(txtTranDate.Text.Trim()),
                                Convert.ToDouble(OpnQty), 0, Convert.ToDecimal(OpnRate), Convert.ToDecimal(OpnQty) * Convert.ToDecimal(OpnRate),
                                Convert.ToDecimal(OpnQty) * Convert.ToDecimal(OpnRate), "", "", "", 0, 0, "1", "");
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
                        "", Convert.ToDecimal(totOpnAmt), "H", (Convert.ToDateTime(txtTranDate.Text.Trim()).Month.ToString("00") + "/" + Convert.ToDateTime(txtTranDate.Text.Trim()).Year.ToString()).ToString(),
                        "ADM", "", "", "", "", "", "", "Opening Transaction", "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "",
                        Convert.ToInt32(nextHdrRef));

                    taInvDet.DeleteInvDetByHdrRef(ddlTranList.SelectedValue.ToString());

                    #region Insert Inventory Details
                    short Lno = 0;
                    foreach (GridViewRow gr in gvOpnBalDet.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblStore"));

                        var lblOpnQty = (Label)(gr.FindControl("lblOpnQty"));
                        var lblOpnRate = (Label)(gr.FindControl("lblOpnRate"));

                        var OpnQty = Convert.ToDouble(lblOpnQty.Text.Trim());
                        var OpnRate = Convert.ToDouble(lblOpnRate.Text.Trim());
                        var OpnAmt = Convert.ToDecimal(OpnQty * OpnRate);

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

                        if (totOpnQty > 0)
                        {
                            taInvDet.InsertInvDet(nextHdrRef.ToString(), "RC", "OPN", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                "", "", Lno, "", Convert.ToDateTime(txtTranDate.Text.Trim()), Convert.ToDateTime(txtTranDate.Text.Trim()),
                                Convert.ToDouble(OpnQty), 0, Convert.ToDecimal(OpnRate), Convert.ToDecimal(OpnQty) * Convert.ToDecimal(OpnRate),
                                Convert.ToDecimal(OpnQty) * Convert.ToDecimal(OpnRate), "", "", "", 0, 0, "1", "");
                        }
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnPrint.Visible = true;
                }

                ddlTranList.DataSource = taInvHdr.GetInvTranDataList("RC", "OPN");
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
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taInvHdr.Connection);

            try
            {
                var nextHdrRef = 1;
                var nextHdrRefNo = "";
                var strCode = "";

                taInvHdr.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);
                taAcc.AttachTransaction(myTran);
                taStkCtl.AttachTransaction(myTran);

                foreach (GridViewRow gr in gvOpnBalDet.Rows)
                {
                    var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                    strCode = hfStoreRef.Value.ToString();

                    var lblOpnQty = ((Label)(gr.FindControl("lblOpnQty"))).Text.ToString();
                    totOpnQty += Convert.ToDouble(lblOpnQty.Trim());

                    var lblOpnAmt = ((Label)(gr.FindControl("lblOpnAmt"))).Text.ToString();
                    totOpnAmt += Convert.ToDouble(lblOpnAmt.Trim());
                }

                if (hfEditStatus.Value == "N")
                {
                    //Inventory Header Ref
                    var dtMaxHdrRef = taInvHdr.GetMaxHdrRef();
                    nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;

                    var dtMaxMrrRef = taInvHdr.GetMaxHdrRefNo("OPN", Convert.ToDateTime(txtTranDate.Text.Trim()).Year);
                    var nextMrrRef = dtMaxMrrRef == null ? 1 : Convert.ToInt32(dtMaxMrrRef) + 1;
                    nextHdrRefNo = "ECIL-OPN-" + Convert.ToDateTime(txtTranDate.Text.Trim()).Month.ToString("00") + Convert.ToDateTime(txtTranDate.Text.Trim()).ToString("yy") + "-" + Convert.ToInt32(nextMrrRef).ToString("000000");

                    taInvHdr.InsertInvHdr(nextHdrRef, "RC", "OPN", nextHdrRefNo, strCode, strCode, strCode,
                        "", Convert.ToDateTime(txtTranDate.Text.Trim()), txtRem.Text.Trim(), "", "", "", "", "", "", "", "", "", Convert.ToDecimal(totOpnAmt), "P",
                        (Convert.ToDateTime(txtTranDate.Text.Trim()).Month.ToString("00") + "/" + Convert.ToDateTime(txtTranDate.Text.Trim()).Year.ToString()).ToString(), "ADM", "", "", "",
                        "", "", "", "Opening Transaction", "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    //var dtMaxAccRef = taAcc.GetMaxRefNo("MJV");
                    //var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                    //var nextAccRefNo = "MJV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                    #region Insert Inventory Details
                    var jvLNo = 0;
                    short Lno = 0;
                    foreach (GridViewRow gr in gvOpnBalDet.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblStore"));

                        var lblOpnQty = (Label)(gr.FindControl("lblOpnQty"));
                        var lblOpnRate = (Label)(gr.FindControl("lblOpnRate"));

                        var OpnQty = Convert.ToDouble(lblOpnQty.Text.Trim());
                        var OpnRate = Convert.ToDouble(lblOpnRate.Text.Trim());
                        var OpnAmt = Convert.ToDecimal(OpnQty * OpnRate);

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

                        if (totOpnQty > 0)
                        {
                            taInvDet.InsertInvDet(nextHdrRef.ToString(), "RC", "OPN", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                "", "", Lno, "", Convert.ToDateTime(txtTranDate.Text.Trim()), Convert.ToDateTime(txtTranDate.Text.Trim()),
                                Convert.ToDouble(OpnQty), 0, Convert.ToDecimal(OpnRate), Convert.ToDecimal(OpnQty) * Convert.ToDecimal(OpnRate),
                                Convert.ToDecimal(OpnQty) * Convert.ToDecimal(OpnRate), "", "", "", 0, 0, "1", "");

                            var dtStkCtl = taStkCtl.GetDataByStoreItem(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                            if (dtStkCtl.Rows.Count > 0)
                                taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk + Convert.ToDouble(OpnQty)), 4), hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                            else
                                taStkCtl.InsertItemStore(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim(), "", Math.Round((Convert.ToDouble(OpnQty)), 4), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                    DateTime.Now, DateTime.Now, "", "", "", 0);

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
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnHold.Enabled = false;
                    btnPost.Enabled = false;
                    btnPrint.Visible = true;
                }
                else
                {
                    nextHdrRef = Convert.ToInt32(ddlTranList.SelectedValue.ToString());
                    nextHdrRefNo = ddlTranList.SelectedItem.ToString();

                    taInvHdr.UpdateInvHdrByHdrRef("1001", "1001", "1001", "", Convert.ToDateTime(txtTranDate.Text.Trim()), txtRem.Text.Trim(), "", "", "", "", "", "", "", "",
                        "", Convert.ToDecimal(totOpnAmt), "P", (Convert.ToDateTime(txtTranDate.Text.Trim()).Month.ToString("00") + "/" + Convert.ToDateTime(txtTranDate.Text.Trim()).Year.ToString()).ToString(),
                        "ADM", "", "", "", "", "", "", "Opening Transaction", "", "", 0, DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "",
                        Convert.ToInt32(nextHdrRef));

                    taInvDet.DeleteInvDetByHdrRef(ddlTranList.SelectedValue.ToString());

                    //var dtMaxAccRef = taAcc.GetMaxRefNo("MJV");
                    //var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                    //var nextAccRefNo = "MJV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                    #region Insert Inventory Details
                    var jvLNo = 0;
                    short Lno = 0;
                    foreach (GridViewRow gr in gvOpnBalDet.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblStore"));

                        var lblOpnQty = (Label)(gr.FindControl("lblOpnQty"));
                        var lblOpnRate = (Label)(gr.FindControl("lblOpnRate"));

                        var OpnQty = Convert.ToDouble(lblOpnQty.Text.Trim());
                        var OpnRate = Convert.ToDouble(lblOpnRate.Text.Trim());
                        var OpnAmt = Convert.ToDecimal(OpnQty * OpnRate);

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

                        if (totOpnQty > 0)
                        {
                            taInvDet.InsertInvDet(nextHdrRef.ToString(), "RC", "OPN", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                "", "", Lno, "", Convert.ToDateTime(txtTranDate.Text.Trim()), Convert.ToDateTime(txtTranDate.Text.Trim()),
                                Convert.ToDouble(OpnQty), 0, Convert.ToDecimal(OpnRate), Convert.ToDecimal(OpnQty) * Convert.ToDecimal(OpnRate),
                                Convert.ToDecimal(OpnQty) * Convert.ToDecimal(OpnRate), "", "", "", 0, 0, "1", "");

                            var dtStkCtl = taStkCtl.GetDataByStoreItem(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                            if (dtStkCtl.Rows.Count > 0)
                                taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk + Convert.ToDouble(OpnQty)), 4), hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                            else
                                taStkCtl.InsertItemStore(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim(), "", Math.Round((Convert.ToDouble(OpnQty)), 4), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                    DateTime.Now, DateTime.Now, "", "", "", 0);

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
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnHold.Enabled = false;
                    btnPost.Enabled = false;
                    btnPrint.Visible = true;
                }

                ddlTranList.DataSource = taInvHdr.GetInvTranDataList("RC", "OPN");
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

        protected void ddlOrdItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var taItem = new tbl_InMa_Item_DetTableAdapter();

            //if (ddlTranItem.SelectedIndex == 0)
            //{
            //    ddlTranItemUom.SelectedIndex = 0;
            //    txtTranQty.Text = "";
            //    txtTranRate.Text = "";
            //    txtOpnAmt.Text = "";
            //    return;
            //}

            //try
            //{
            //    txtTranRate.Text = "";
            //    txtTranQty.Text = "";

            //    var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(ddlTranItem.SelectedValue.ToString()));
            //    if (dtItem.Rows.Count > 0)
            //        ddlTranItemUom.SelectedValue = dtItem[0].IsItm_Det_Stk_UnitNull() ? "0" : dtItem[0].Itm_Det_Stk_Unit.ToString();
            //    else
            //    {
            //        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
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

        protected void gvOpnBalDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var dt = new DataTable();
            dt = (DataTable)ViewState["dtTranDet"];

            dt.Rows[rowNum].Delete();
            dt.AcceptChanges();

            gvOpnBalDet.EditIndex = -1;
            SetOpnBalDetGridData();

            foreach (GridViewRow gr in gvOpnBalDet.Rows)
            {
                var lblOpnQty = ((Label)(gr.FindControl("lblOpnQty"))).Text.ToString();
                totOpnQty += Convert.ToDouble(lblOpnQty.Trim());

                var lblOpnAmt = ((Label)(gr.FindControl("lblOpnAmt"))).Text.ToString();
                totOpnAmt += Convert.ToDouble(lblOpnAmt.Trim());
            }

            if (gvOpnBalDet.Rows.Count > 0)
            {
                lblTotTranItem.Text = gvOpnBalDet.Rows.Count.ToString();
                //lblTotTranItem.Text = totOpnQty.ToString("N2");
                lblTotTranVal.Text = totOpnAmt.ToString("N2");
                lblTotTranItem.Visible = true;
                lblTotTranVal.Visible = true;
                btnPost.Visible = true;
                btnHold.Visible = true;
                btnPost.Enabled = true;
                btnHold.Enabled = true;
            }
            else
            {
                lblTotTranItem.Text = "0";
                lblTotTranVal.Text = "0.00";
                lblTotTranItem.Visible = false;
                lblTotTranVal.Visible = false;
                btnPost.Visible = false;
                btnHold.Visible = false;
                btnPost.Enabled = false;
                btnHold.Enabled = false;
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
                    var dtInvHdr = taInvHdr.GetDataByHdrRef(Convert.ToInt32(ddlTranList.SelectedValue.ToString()));
                    if (dtInvHdr.Rows.Count > 0)
                    {
                        txtRem.Text = "";
                        txtTranQty.Text = "";
                        txtTranRate.Text = "";
                        txtOpnAmt.Text = "";
                        ddlTranItemUom.SelectedIndex = 0;
                        ddlTranStore.SelectedIndex = 0;

                        hfEditStatus.Value = "Y";

                        txtTranDate.Text = dtInvHdr[0].Trn_Hdr_Date.ToString("dd/MM/yyyy");

                        txtRem.Text = dtInvHdr[0].Trn_Hdr_Com1.ToString();

                        LoadInitOpnBalDetGridData();
                        SetOpnBalDetGridData();
                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtTranDet"];
                        var dtMrrDet = taInvDet.GetDataByHdrRef(ddlTranList.SelectedValue.ToString());
                        foreach (dsInvTran.tbl_InTr_Trn_DetRow dr in dtMrrDet.Rows)
                        {
                            dt.Rows.Add(dr.Trn_Hdr_Ref, dr.Trn_Hdr_Ref, dr.Trn_Det_Ref, dr.Trn_Det_Icode, dr.Trn_Det_Itm_Desc, dr.Trn_Det_Itm_Uom, dr.Trn_Det_Itm_Uom,
                                dr.Trn_Det_Str_Code, dr.Trn_Det_Str_Code, dr.Trn_Det_Lin_Qty, dr.Trn_Det_Lin_Rat.ToString("N2"), dr.Trn_Det_Lin_Net);
                        }
                        ViewState["dtTranDet"] = dt;
                        SetOpnBalDetGridData();

                        foreach (GridViewRow gr in gvOpnBalDet.Rows)
                        {
                            var lblOpnQty = ((Label)(gr.FindControl("lblOpnQty"))).Text.ToString();
                            totOpnQty += Convert.ToDouble(lblOpnQty.Trim());

                            var lblOpnAmt = ((Label)(gr.FindControl("lblOpnAmt"))).Text.ToString();
                            totOpnAmt += Convert.ToDouble(lblOpnAmt.Trim());
                        }

                        if (gvOpnBalDet.Rows.Count > 0)
                        {
                            lblTotTranItem.Text = gvOpnBalDet.Rows.Count.ToString();
                            lblTotTranVal.Text = totOpnAmt.ToString("N2");
                            lblTotTranItem.Visible = true;
                            lblTotTranVal.Visible = true;
                            btnPost.Visible = true;
                            btnHold.Visible = true;
                            btnPost.Enabled = true;
                            btnHold.Enabled = true;
                        }
                        else
                        {
                            lblTotTranItem.Text = "0";
                            lblTotTranVal.Text = "0.00";
                            lblTotTranItem.Visible = false;
                            lblTotTranVal.Visible = false;
                            btnPost.Visible = false;
                            btnHold.Visible = false;
                            btnPost.Enabled = false;
                            btnHold.Enabled = false;
                        }

                        if (dtInvHdr[0].Trn_Hdr_HRPB_Flag == "P")
                        {
                            btnHold.Enabled = false;
                            btnPost.Enabled = false;
                            gvOpnBalDet.Enabled = false;
                            btnAddOpnDet.Enabled = false;
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

                var dtMaxHdrRef = taInvHdr.GetMaxHdrRefNo("OPN", DateTime.Now.Year);
                var nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;
                var nextHdrRefNo = "ECIL-OPN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRef).ToString("000000");
                lblTranRefNo.Text = nextHdrRefNo;

                ddlTranList.DataSource = taInvHdr.GetInvTranDataList("RC", "OPN");
                ddlTranList.DataTextField = "Trn_Hdr_Ref_No";
                ddlTranList.DataValueField = "Trn_Hdr_Ref";
                ddlTranList.DataBind();
                ddlTranList.Items.Insert(0, "----------New----------");

                txtTranDate.Text = DateTime.Now.ToString("dd/MM/yyy");
                txtRem.Text = "";

                txtTranQty.Text = "";
                txtTranRate.Text = "";
                txtOpnAmt.Text = "";
                txtItemName.Text = "";
                
                ddlTranItemUom.SelectedIndex = 0;
                ddlTranStore.SelectedIndex = 0;

                lblTotTranItem.Text = "0.00";
                lblTotTranVal.Text = "0.00";
                btnAddOpnDet.Enabled = true;
                btnPost.Visible = false;
                btnHold.Visible = false;
                btnPrint.Visible = false;

                hfEditStatus.Value = "N";
                hfRefNo.Value = "0";

                LoadInitOpnBalDetGridData();
                SetOpnBalDetGridData();
                gvOpnBalDet.Enabled = true;
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

            //var taItem = new tbl_InMa_Item_DetTableAdapter();

            //if (cboItemType.SelectedIndex == 0)
            //{
            //    ddlTranItem.Items.Clear();
            //    ddlTranItem.Items.Insert(0, new ListItem("----------Select----------", "0"));
            //    ddlTranItem.SelectedIndex = 0;
            //    ddlTranItem.SelectedIndex = 0;
            //    txtTranQty.Text = "";
            //    return;
            //}

            //try
            //{
            //    var dtItem = taItem.GetDataByItemType(cboItemType.SelectedValue.ToString());
            //    if (dtItem.Rows.Count > 0)
            //    {
            //        var dtSaleItem = taItem.GetDataByItemType(cboItemType.SelectedValue.ToString());
            //        ddlTranItem.DataSource = dtSaleItem;
            //        ddlTranItem.DataTextField = "Itm_Det_Desc";
            //        ddlTranItem.DataValueField = "Itm_Det_Ref";
            //        ddlTranItem.DataBind();
            //        ddlTranItem.Items.Insert(0, new ListItem("----------Select----------", "0"));
            //    }
            //    else
            //    {
            //        ddlTranItem.Items.Clear();
            //        tblMsg.Rows[0].Cells[0].InnerText = "There is no item in category :- " + cboItemType.SelectedItem.ToString();
            //        tblMsg.Rows[1].Cells[0].InnerText = "Please add item first.";
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taInvHdr.Connection);

            try
            {
                var nextHdrRef = 1;
                var nextHdrRefNo = "";

                taInvHdr.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);
                taStkCtl.AttachTransaction(myTran);

                var taItemOpnBal = new Item_Opening_BalanceTableAdapter();
                var dtItemOpnBal = taItemOpnBal.GetData();
                if (dtItemOpnBal.Rows.Count > 0)
                {
                    //Inventory Header Ref
                    var dtMaxHdrRef = taInvHdr.GetMaxHdrRef();
                    nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;

                    var dtMaxMrrRef = taInvHdr.GetMaxHdrRefNo("OPN", Convert.ToDateTime(txtTranDate.Text.Trim()).Year);
                    var nextMrrRef = dtMaxMrrRef == null ? 1 : Convert.ToInt32(dtMaxMrrRef) + 1;
                    nextHdrRefNo = "ECIL-OPN-" + Convert.ToDateTime(txtTranDate.Text.Trim()).Month.ToString("00") + Convert.ToDateTime(txtTranDate.Text.Trim()).ToString("yy") + "-" + Convert.ToInt32(nextMrrRef).ToString("000000");

                    taInvHdr.InsertInvHdr(nextHdrRef, "RC", "OPN", nextHdrRefNo, "1004", "1004", "1004",
                        "", Convert.ToDateTime(txtTranDate.Text.Trim()), "Opening Balance As On " + Convert.ToDateTime(txtTranDate.Text.Trim()).ToString("dd-MM-yyyy"), "", "", "", "", "", "", "", "", "", Convert.ToDecimal(1), "P",
                        (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), "ADM", "", "", "",
                        "", "", "", "Opening Transaction", "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    short Lno = 0;
                    var item = "";
                    foreach (dsInvTran.Item_Opening_BalanceRow dr in dtItemOpnBal.Rows)
                    {                        
                        var itemName = "";
                        var itemAcc = "";

                        var taItem = new tbl_InMa_Item_DetTableAdapter();
                        var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(dr.ItemRef.ToString()));
                        if (dtItem.Rows.Count > 0)
                        {
                            itemName = dtItem[0].Itm_Det_Desc.ToString();
                            itemAcc = dtItem[0].Itm_Det_Acc_Code.ToString();      

                            Lno++;

                            if (Convert.ToDouble(dr.ItemOpnBal) >= 0)
                            {
                                taInvDet.InsertInvDet(nextHdrRef.ToString(), "RC", "OPN", nextHdrRefNo.ToString(), Lno, "", 1, "",
                                    Lno, dr.ItemRef.ToString(), dtItem[0].Itm_Det_Desc, dtItem[0].Itm_Det_Stk_Unit, "1004", "",
                                    "", "", Lno, "", Convert.ToDateTime(txtTranDate.Text.Trim()), Convert.ToDateTime(txtTranDate.Text.Trim()),
                                    Convert.ToDouble(dr.ItemOpnBal), 0, Convert.ToDecimal(1), Convert.ToDecimal(dr.ItemOpnBal) * Convert.ToDecimal(1),
                                    Convert.ToDecimal(dr.ItemOpnBal) * Convert.ToDecimal(1), "", "", "", 0, 0, "1", "");

                                var dtStkCtl = taStkCtl.GetDataByStoreItem("1004", dtItem[0].Itm_Det_Ref.ToString());
                                if (dtStkCtl.Rows.Count > 0)
                                    taStkCtl.UpdateStkCtlCurStk(Convert.ToDouble(dr.ItemOpnBal), "1004", dtItem[0].Itm_Det_Ref.ToString());
                                else
                                    taStkCtl.InsertItemStore("1004", dtItem[0].Itm_Det_Ref.ToString(), "", Convert.ToDouble(dr.ItemOpnBal), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                        DateTime.Now, DateTime.Now, "", "", "", 0);
                            }
                            else
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item Qty: " + dr.ItemOpnBal.ToString();
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                            }
                        }
                        else
                        {
                            item = dr.ItemName.ToString() + " " + dr.ItemRef.ToString();
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item Ref: " + dr.ItemRef.ToString();
                            tblMsg.Rows[1].Cells[0].InnerText = "Item Name: " + dr.ItemName.ToString();
                            ModalPopupExtenderMsg.Show();                            
                        }
                    }

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully. Total (" + Lno.ToString() + ")";
                    tblMsg.Rows[1].Cells[0].InnerText = "Count" + dtItemOpnBal.Rows.Count.ToString() + " " + item;
                    ModalPopupExtenderMsg.Show();
                }
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
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
                            //if (cboStore.SelectedIndex != 0)
                            //{
                            //    var dtStkCtl = taStkCtl.GetDataByStoreItem(cboStore.SelectedValue.ToString(), itemRef.ToString());
                            //    txtStock.Text = dtStkCtl.Rows.Count > 0 ? dtStkCtl[0].Stk_Ctl_Cur_Stk.ToString() : "0";
                            //}
                        }
                        else
                        {
                            ddlTranItemUom.SelectedIndex = 0;
                            //txtStock.Text = "0";
                        }
                    }
                    else
                    {
                        ddlTranItemUom.SelectedIndex = 0;
                        //txtStock.Text = "0";
                    }
                }
                else
                {
                    ddlTranItemUom.SelectedIndex = 0;
                    //txtStock.Text = "0";
                }
            }
            catch (Exception ex)
            {
                ddlTranItemUom.SelectedIndex = 0;
                //txtStock.Text = "0";
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
    }
}