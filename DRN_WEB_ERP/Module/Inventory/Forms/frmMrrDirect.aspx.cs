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
    public partial class frmMrrDirect : System.Web.UI.Page
    {
        double totMrrQty = 0;
        double totMrrAmt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            AutoCompleteExtenderSrchItem.ContextKey = "0";

            btnPrint.Attributes.Add("onclick", "javascript:w= window.open('frmShowSalesReport.aspx');");

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

            txtMrrDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var dtMaxHdrRef = taInvHdr.GetMaxHdrRefNo("PO", Convert.ToDateTime(txtMrrDate.Text).Year);
            var nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;
            var nextHdrRefNo = "ECIL-MRR-" + Convert.ToDateTime(txtMrrDate.Text).Month.ToString("00") + Convert.ToDateTime(txtMrrDate.Text).ToString("yy") + "-" + Convert.ToInt32(nextHdrRef).ToString("000000");
            lblMrrRefNo.Text = nextHdrRefNo;

            //ddlMrrList.DataSource = taInvHdr.GetInvTranDataList("RC", "PO");
            //ddlMrrList.DataTextField = "Trn_Hdr_Ref_No";
            //ddlMrrList.DataValueField = "Trn_Hdr_Ref";
            //ddlMrrList.DataBind();
            //ddlMrrList.Items.Insert(0, "----------New----------");

            var taMrrHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var dtMrrHdr = new dsInvTran.tbl_InTr_Trn_HdrDataTable();
            if (cboMonth.SelectedIndex == 0)//taInvHdr.GetInvTranDataList("RC", "PO");
                dtMrrHdr = taMrrHdr.GetInvTranDataListByYear("RC", "PO", Convert.ToDecimal(cboYear.SelectedValue.ToString()));
            else
                dtMrrHdr = taMrrHdr.GetInvTranDataListByYearMonth("RC", "PO", Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
            ListItem lst;
            foreach (dsInvTran.tbl_InTr_Trn_HdrRow dr in dtMrrHdr.Rows)
            {
                lst = new ListItem();
                lst.Text = dr.Trn_Hdr_Ref_No.ToString() + "    [" + dr.T_C1.ToString() + ", Date:" + dr.Trn_Hdr_Date.ToString("dd-MM-yyyy") + "]";
                lst.Value = dr.Trn_Hdr_Ref.ToString();

                ddlMrrList.Items.Add(lst);
            }
            ddlMrrList.Items.Insert(0, "----------New----------");

            var taItemType = new tbl_InMa_TypeTableAdapter();
            var dtItemType = taItemType.GetDataByAsc();
            ddlMrrItemType.DataSource = dtItemType;
            ddlMrrItemType.DataTextField = "Item_Type_Name";
            ddlMrrItemType.DataValueField = "Item_Type_Code";
            ddlMrrItemType.DataBind();
            ddlMrrItemType.Items.Insert(0, new ListItem("-----Select-----", "0"));

            //var taSaleItem = new tbl_InMa_Item_DetTableAdapter();
            //var dtSaleItem = taSaleItem.GetRmItemData();
            //ddlMrrItem.DataSource = dtSaleItem;
            //ddlMrrItem.DataTextField = "Itm_Det_Desc";
            //ddlMrrItem.DataValueField = "Itm_Det_Ref";
            //ddlMrrItem.DataBind();
            //ddlMrrItem.Items.Insert(0, new ListItem("----------Select----------", "0"));

            //var taItemUom = new tbl_InMa_UomTableAdapter();
            //var dtItemUom = taItemUom.GetData();
            //ddlMrrItemUom.DataSource = dtItemUom;
            //ddlMrrItemUom.DataTextField = "Uom_Name";
            //ddlMrrItemUom.DataValueField = "Uom_Code";
            //ddlMrrItemUom.DataBind();
            //ddlMrrItemUom.Items.Insert(0, new ListItem("----", "0"));

            var taStore = new tbl_InMa_Str_LocTableAdapter();
            var dtStore = taStore.GetDataByGenStore();
            ddlMrrStore.DataSource = dtStore;
            ddlMrrStore.DataTextField = "Str_Loc_Name";
            ddlMrrStore.DataValueField = "Str_Loc_Ref";
            ddlMrrStore.DataBind();
            ddlMrrStore.Items.Insert(0, new ListItem("----", "0"));

            LoadInitMrrDetGridData();
            SetMrrDetGridData();

            txtMrrQty.Attributes.Add("onkeyup", "CalcMrrAmount('" + txtMrrQty.ClientID + "', '" + txtMrrRate.ClientID + "', '" + txtMrrAmt.ClientID + "' )");
            txtMrrRate.Attributes.Add("onkeyup", "CalcMrrAmount('" + txtMrrQty.ClientID + "', '" + txtMrrRate.ClientID + "', '" + txtMrrAmt.ClientID + "' )");            
        }

        #region MRR Details Gridview
        protected void LoadInitMrrDetGridData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MRR_HDR_REF", typeof(string));
            dt.Columns.Add("MRR_DET_REF", typeof(string));
            dt.Columns.Add("MRR_DET_REF_NO", typeof(string));
            dt.Columns.Add("MRR_ITEM_REF", typeof(string));
            dt.Columns.Add("MRR_ITEM_NAME", typeof(string));
            dt.Columns.Add("MRR_ITEM_UOM_REF", typeof(string));
            dt.Columns.Add("MRR_ITEM_UOM", typeof(string));
            dt.Columns.Add("MRR_STORE_REF", typeof(string));
            dt.Columns.Add("MRR_STORE_NAME", typeof(string));
            dt.Columns.Add("MRR_QTY", typeof(string));
            dt.Columns.Add("MRR_RATE", typeof(string));
            dt.Columns.Add("MRR_AMOUNT", typeof(string));            
            ViewState["dtMrrDet"] = dt;
        }

        protected void SetMrrDetGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtMrrDet"];

                gvMrrDet.DataSource = dt;
                gvMrrDet.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion

        protected void btnAddMrrDet_Click(object sender, EventArgs e)
        {
            Page.Validate("btnAdd");
            Page.Validate("btnSave");

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

                foreach (GridViewRow gr in gvMrrDet.Rows)
                {
                    var lblItemCode = ((Label)(gr.FindControl("lblItemCode"))).Text.ToString();
                    if (itemRef.ToString() == lblItemCode.ToString())
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = itemName.ToString() + " already addred to MRR details.";
                        tblMsg.Rows[1].Cells[0].InnerText = "To add more you need to delete existing same item.";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                if (txtMrrAmt.Text.Trim() == "" || txtMrrAmt.Text.Trim().Length <= 0 || Convert.ToDouble(txtMrrAmt.Text.Trim()) <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter valid data.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                var dt = new DataTable();
                dt = (DataTable)ViewState["dtMrrDet"];

                var MRR_HDR_REF = "0";
                var MRR_DET_REF = "0";
                var MRR_DET_REF_NO = "0";
                var MRR_ITEM_REF = itemRef.ToString();
                var MRR_ITEM_NAME = itemName.ToString();
                //var MRR_ITEM_UOM_REF = ddlMrrItemUom.SelectedValue.ToString();
                //var MRR_ITEM_UOM = ddlMrrItemUom.SelectedItem.ToString();
                var MRR_ITEM_UOM_REF = txtMrrItemUom.Text.Trim();
                var MRR_ITEM_UOM = txtMrrItemUom.Text.Trim();
                var MRR_STORE_REF = ddlMrrStore.SelectedValue.ToString();
                var MRR_STORE_NAME = ddlMrrStore.SelectedItem.ToString();
                var MRR_QTY = Convert.ToDouble(txtMrrQty.Text.Trim().Length > 0 ? txtMrrQty.Text.Trim() : "0");
                var MRR_RATE = Convert.ToDouble(txtMrrRate.Text.Trim().Length > 0 ? txtMrrRate.Text.Trim() : "0");
                var MRR_AMOUNT = (MRR_QTY * MRR_RATE).ToString("N2");

                dt.Rows.Add(MRR_HDR_REF, MRR_DET_REF, MRR_DET_REF_NO, MRR_ITEM_REF, MRR_ITEM_NAME, MRR_ITEM_UOM_REF, MRR_ITEM_UOM, MRR_STORE_REF, MRR_STORE_NAME, MRR_QTY, 
                    MRR_RATE.ToString("N2"), MRR_AMOUNT);

                ViewState["dtMrrDet"] = dt;
                SetMrrDetGridData();

                foreach (GridViewRow gr in gvMrrDet.Rows)
                {
                    var lblMrrQty = ((Label)(gr.FindControl("lblMrrQty"))).Text.ToString();
                    totMrrQty += Convert.ToDouble(lblMrrQty.Trim());

                    var lblMrrAmt = ((Label)(gr.FindControl("lblMrrAmt"))).Text.ToString();
                    totMrrAmt += Convert.ToDouble(lblMrrAmt.Trim());
                }

                if (gvMrrDet.Rows.Count > 0)
                {
                    lblTotMrrItem.Text = gvMrrDet.Rows.Count.ToString();
                    //lblTotMrrItem.Text = totMrrQty.ToString("N2");
                    lblTotMrrVal.Text = totMrrAmt.ToString("N2");
                    lblTotMrrItem.Visible = true;
                    lblTotMrrVal.Visible = true;
                    btnPost.Visible = true;
                    btnHold.Visible = true;
                    btnPost.Enabled = true;
                    btnHold.Enabled = true;
                }
                else
                {
                    lblTotMrrItem.Text = "0";                    
                    lblTotMrrVal.Text = "0.00";
                    lblTotMrrItem.Visible = false;
                    lblTotMrrVal.Visible = false;
                    btnPost.Visible = false;
                    btnHold.Visible = false;
                    btnPost.Enabled = false;
                    btnHold.Enabled = false;
                }

                txtMrrItemUom.Text = "";
                txtMrrQty.Text = "";
                txtMrrRate.Text = "";
                txtMrrAmt.Text = "";
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
            Page.Validate("btnSave");

            if (!Page.IsValid) return;

            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taInvHdr.Connection);

            try
            {
                #region Get Supplier Details
                var supRef = "";
                var supName = "";
                var supAccCode = "";
                var srchWords = txtSup.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    supRef = word;
                    break;
                }

                if (supRef.Length > 0)
                {
                    var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();
                    var dtSupAdr = taSupAdr.GetDataBySupAdrRef(supRef);
                    supRef = dtSupAdr.Rows.Count > 0 ? dtSupAdr[0].Par_Adr_Ref.ToString() : "";
                    supName = dtSupAdr.Rows.Count > 0 ? dtSupAdr[0].Par_Adr_Name.ToString() : "";
                    supAccCode = dtSupAdr.Rows.Count > 0 ? dtSupAdr[0].Par_Adr_Acc_Code.ToString() : "";
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Supplier.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                var nextHdrRef = 1;
                var nextHdrRefNo = "";

                taInvHdr.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);

                foreach (GridViewRow gr in gvMrrDet.Rows)
                {
                    var lblMrrQty = ((Label)(gr.FindControl("lblMrrQty"))).Text.ToString();
                    totMrrQty += Convert.ToDouble(lblMrrQty.Trim());

                    var lblMrrAmt = ((Label)(gr.FindControl("lblMrrAmt"))).Text.ToString();
                    totMrrAmt += Convert.ToDouble(lblMrrAmt.Trim());
                }

                if (hfEditStatus.Value == "N")
                {
                    //Inventory Header Ref
                    var dtMaxHdrRef = taInvHdr.GetMaxHdrRef();
                    nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;

                    var dtMaxMrrRef = taInvHdr.GetMaxHdrRefNo("PO", Convert.ToDateTime(txtMrrDate.Text).Year);
                    var nextMrrRef = dtMaxMrrRef == null ? 1 : Convert.ToInt32(dtMaxMrrRef) + 1;
                    nextHdrRefNo = "ECIL-MRR-" + Convert.ToDateTime(txtMrrDate.Text).Month.ToString("00") + Convert.ToDateTime(txtMrrDate.Text).ToString("yy") + "-" + Convert.ToInt32(nextMrrRef).ToString("000000");

                    taInvHdr.InsertInvHdr(nextHdrRef, "RC", "PO", nextHdrRefNo, supRef.ToString(), supRef.ToString(), supRef.ToString(),
                        txtPoRef.Text.Trim(), Convert.ToDateTime(txtMrrDate.Text), "", "", "", "", "", "", "", "", "", txtCarryCharge.Text, Convert.ToDecimal(totMrrAmt), "H",
                        (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), "ADM", "", "", "",
                        "", "", "", supName, "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    #region Insert Inventory Details
                    short Lno = 0;
                    foreach (GridViewRow gr in gvMrrDet.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfMrrStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblMrrStore"));

                        var lblMrrQty = (Label)(gr.FindControl("lblMrrQty"));
                        var lblMrrRate = (Label)(gr.FindControl("lblMrrRate"));

                        var MrrQty = Convert.ToDouble(lblMrrQty.Text.Trim());
                        var MrrRate = Convert.ToDouble(lblMrrRate.Text.Trim());
                        var MrrAmt = Convert.ToDecimal(MrrQty * MrrRate);

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

                        if (totMrrQty > 0)
                        {
                            taInvDet.InsertInvDet(nextHdrRef.ToString(), "RC", "PO", nextHdrRefNo.ToString(), Lno, "", 1, txtPoRef.Text.Trim(),
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                txtPoRef.Text.Trim(), txtPoRef.Text.Trim(), Lno, "", DateTime.Now, Convert.ToDateTime(txtMrrDate.Text),
                                Convert.ToDouble(MrrQty), 0, Convert.ToDecimal(MrrRate), Convert.ToDecimal(MrrQty) * Convert.ToDecimal(MrrRate),
                                Convert.ToDecimal(MrrQty) * Convert.ToDecimal(MrrRate), "", "", "", 0, 0, "1", "");                            
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
                    nextHdrRef = Convert.ToInt32(ddlMrrList.SelectedValue.ToString());
                    nextHdrRefNo = ddlMrrList.SelectedItem.ToString();

                    taInvHdr.UpdateInvHdrByHdrRef(supRef.ToString(), supRef.ToString(), supRef.ToString(), txtPoRef.Text.Trim(), Convert.ToDateTime(txtMrrDate.Text), "", "", "", "", "", "", "", "", "",
                        txtCarryCharge.Text, Convert.ToDecimal(totMrrAmt), "H", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                        "ADM", "", "", "", "", "", "", supName, "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "",
                        Convert.ToInt32(nextHdrRef));

                    taInvDet.DeleteInvDetByHdrRef(ddlMrrList.SelectedValue.ToString());
        
                    #region Insert Inventory Details
                    short Lno = 0;
                    foreach (GridViewRow gr in gvMrrDet.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfMrrStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblMrrStore"));

                        var lblMrrQty = (Label)(gr.FindControl("lblMrrQty"));
                        var lblMrrRate = (Label)(gr.FindControl("lblMrrRate"));

                        var MrrQty = Convert.ToDouble(lblMrrQty.Text.Trim());
                        var MrrRate = Convert.ToDouble(lblMrrRate.Text.Trim());
                        var MrrAmt = Convert.ToDecimal(MrrQty * MrrRate);

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

                        if (totMrrQty > 0)
                        {
                            taInvDet.InsertInvDet(nextHdrRef.ToString(), "RC", "PO", nextHdrRefNo.ToString(), Lno, "", 1, txtPoRef.Text.Trim(),
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                txtPoRef.Text.Trim(), txtPoRef.Text.Trim(), Lno, "", DateTime.Now, Convert.ToDateTime(txtMrrDate.Text),
                                Convert.ToDouble(MrrQty), 0, Convert.ToDecimal(MrrRate), Convert.ToDecimal(MrrQty) * Convert.ToDecimal(MrrRate),
                                Convert.ToDecimal(MrrQty) * Convert.ToDecimal(MrrRate), "", "", "", 0, 0, "1", "");
                        }
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnPrint.Visible = true;
                }

                //ddlMrrList.DataSource = taInvHdr.GetInvTranDataList("RC", "PO");
                //ddlMrrList.DataTextField = "Trn_Hdr_Ref_No";
                //ddlMrrList.DataValueField = "Trn_Hdr_Ref";
                //ddlMrrList.DataBind();
                //ddlMrrList.Items.Insert(0, "----------New----------");
                //ddlMrrList.SelectedIndex = ddlMrrList.Items.IndexOf(ddlMrrList.Items.FindByText(nextHdrRefNo));

                var taMrrHdr = new tbl_InTr_Trn_HdrTableAdapter();
                var dtMrrHdr = new dsInvTran.tbl_InTr_Trn_HdrDataTable();
                if (cboMonth.SelectedIndex == 0)//taInvHdr.GetInvTranDataList("RC", "PO");
                    dtMrrHdr = taMrrHdr.GetInvTranDataListByYear("RC", "PO", Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                else
                    dtMrrHdr = taMrrHdr.GetInvTranDataListByYearMonth("RC", "PO", Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                ListItem lst;
                foreach (dsInvTran.tbl_InTr_Trn_HdrRow dr in dtMrrHdr.Rows)
                {
                    lst = new ListItem();
                    lst.Text = dr.Trn_Hdr_Ref_No.ToString() + "    [" + dr.T_C1.ToString() + ", Date:" + dr.Trn_Hdr_Date.ToString("dd-MM-yyyy") + "]";
                    lst.Value = dr.Trn_Hdr_Ref.ToString();

                    ddlMrrList.Items.Add(lst);
                }
                ddlMrrList.Items.Insert(0, "----------New----------");

                ddlMrrList.SelectedIndex = ddlMrrList.Items.IndexOf(ddlMrrList.Items.FindByText(nextHdrRefNo));

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
                #region Get Supplier Details
                var supRef = "";
                var supName = "";
                var supAccCode = "";
                var srchWords = txtSup.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    supRef = word;
                    break;
                }

                if (supRef.Length > 0)
                {
                    var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();
                    var dtSupAdr = taSupAdr.GetDataBySupAdrRef(supRef);
                    supRef = dtSupAdr.Rows.Count > 0 ? dtSupAdr[0].Par_Adr_Ref.ToString() : "";
                    supName = dtSupAdr.Rows.Count > 0 ? dtSupAdr[0].Par_Adr_Name.ToString() : "";
                    supAccCode = dtSupAdr.Rows.Count > 0 ? dtSupAdr[0].Par_Adr_Acc_Code.ToString() : "";
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Supplier.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                var nextHdrRef = 1;
                var nextHdrRefNo = "";

                taInvHdr.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);
                taAcc.AttachTransaction(myTran);
                taStkCtl.AttachTransaction(myTran);

                foreach (GridViewRow gr in gvMrrDet.Rows)
                {
                    var lblMrrQty = ((Label)(gr.FindControl("lblMrrQty"))).Text.ToString();
                    totMrrQty += Convert.ToDouble(lblMrrQty.Trim());

                    var lblMrrAmt = ((Label)(gr.FindControl("lblMrrAmt"))).Text.ToString();
                    totMrrAmt += Convert.ToDouble(lblMrrAmt.Trim());
                }

                if (hfEditStatus.Value == "N")
                {
                    //Inventory Header Ref
                    var dtMaxHdrRef = taInvHdr.GetMaxHdrRef();
                    nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;

                    var dtMaxMrrRef = taInvHdr.GetMaxHdrRefNo("PO", Convert.ToDateTime(txtMrrDate.Text).Year);
                    var nextMrrRef = dtMaxMrrRef == null ? 1 : Convert.ToInt32(dtMaxMrrRef) + 1;
                    nextHdrRefNo = "ECIL-MRR-" + Convert.ToDateTime(txtMrrDate.Text).Month.ToString("00") + Convert.ToDateTime(txtMrrDate.Text).ToString("yy") + "-" + Convert.ToInt32(nextMrrRef).ToString("000000");

                    taInvHdr.InsertInvHdr(nextHdrRef, "RC", "PO", nextHdrRefNo, supRef.ToString(), supRef.ToString(), supAccCode.ToString(),
                        txtPoRef.Text.Trim(), Convert.ToDateTime(txtMrrDate.Text), "", "", "", "", "", "", "", "", "", txtCarryCharge.Text, Convert.ToDecimal(totMrrAmt), "P",
                        (Convert.ToDateTime(txtMrrDate.Text).Month.ToString("00") + "/" + Convert.ToDateTime(txtMrrDate.Text).Year.ToString()).ToString(), "ADM", "", "", "",
                        "", "", "", supName, "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    var dtMaxAccRef = taAcc.GetMaxRefNo("MJV", Convert.ToDateTime(txtMrrDate.Text).Year);
                    var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                    var nextAccRefNo = "MJV" + (Convert.ToDateTime(txtMrrDate.Text).Month.ToString("00") + Convert.ToDateTime(txtMrrDate.Text).ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                    #region Insert Inventory Details
                    var jvLNo = 0;
                    short Lno = 0;
                    foreach (GridViewRow gr in gvMrrDet.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfMrrStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblMrrStore"));

                        var lblMrrQty = (Label)(gr.FindControl("lblMrrQty"));
                        var lblMrrRate = (Label)(gr.FindControl("lblMrrRate"));

                        var MrrQty = Convert.ToDouble(lblMrrQty.Text.Trim());
                        var MrrRate = Convert.ToDouble(lblMrrRate.Text.Trim());
                        var MrrAmt = Convert.ToDecimal(MrrQty * MrrRate);

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

                        if (totMrrQty > 0)
                        {
                            taInvDet.InsertInvDet(nextHdrRef.ToString(), "RC", "PO", nextHdrRefNo.ToString(), Lno, "", 1, txtPoRef.Text.Trim(),
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                txtPoRef.Text.Trim(), txtPoRef.Text.Trim(), Lno, "", DateTime.Now, Convert.ToDateTime(txtMrrDate.Text),
                                Convert.ToDouble(MrrQty), 0, Convert.ToDecimal(MrrRate), Convert.ToDecimal(MrrQty) * Convert.ToDecimal(MrrRate),
                                Convert.ToDecimal(MrrQty) * Convert.ToDecimal(MrrRate), "", "", "", 0, 0, "1", "");

                            var dtStkCtl = taStkCtl.GetDataByStoreItem(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                            if (dtStkCtl.Rows.Count > 0)
                                taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk + Convert.ToDouble(MrrQty)), 4), hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                            else
                                taStkCtl.InsertItemStore(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim(), "", Math.Round((Convert.ToDouble(MrrQty)), 4), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
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
                            //    "", txtPoRef.Text.Trim(), "N", 1, 0, "", "", "", "J", 0, "1", "MJV");

                            //jvLNo++;
                            ////Credit-Item Account
                            //taAcc.InsertAccData(itemAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                            //    nextAccRefNo.ToString(), jvLNo, 1, itemName.ToString(), "C", MrrAmt, nextHdrRefNo.ToString(), "0",
                            //    "BDT", 1, MrrAmt, "", "", "", "", "", "", "", "", "", "", "",
                            //    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                            //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, supName.ToString(), DateTime.Now,
                            //    "ADM", "P", "", DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, MrrAmt, "",
                            //    "", txtPoRef.Text.Trim(), "N", 1, 0, "", "", "", "J", 0, "1", "MJV");
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
                    nextHdrRef = Convert.ToInt32(ddlMrrList.SelectedValue.ToString());
                    nextHdrRefNo = ddlMrrList.SelectedItem.ToString();

                    taInvHdr.UpdateInvHdrByHdrRef(supRef.ToString(), supRef.ToString(), supRef.ToString(), txtPoRef.Text.Trim(), Convert.ToDateTime(txtMrrDate.Text), "", "", "", "", "", "", "", "", "",
                        txtCarryCharge.Text, Convert.ToDecimal(totMrrAmt), "P", (Convert.ToDateTime(txtMrrDate.Text).Month.ToString("00") + "/" + Convert.ToDateTime(txtMrrDate.Text).Year.ToString()).ToString(),
                        "ADM", "", "", "", "", "", "", supName, "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "",
                        Convert.ToInt32(nextHdrRef));

                    taInvDet.DeleteInvDetByHdrRef(ddlMrrList.SelectedValue.ToString());

                    var dtMaxAccRef = taAcc.GetMaxRefNo("MJV", Convert.ToDateTime(txtMrrDate.Text).Year);
                    var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                    var nextAccRefNo = "MJV" + (Convert.ToDateTime(txtMrrDate.Text).Month.ToString("00") + Convert.ToDateTime(txtMrrDate.Text).ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                    #region Insert Inventory Details
                    var jvLNo = 0;
                    short Lno = 0;
                    foreach (GridViewRow gr in gvMrrDet.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfMrrStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblMrrStore"));

                        var lblMrrQty = (Label)(gr.FindControl("lblMrrQty"));
                        var lblMrrRate = (Label)(gr.FindControl("lblMrrRate"));

                        var MrrQty = Convert.ToDouble(lblMrrQty.Text.Trim());
                        var MrrRate = Convert.ToDouble(lblMrrRate.Text.Trim());
                        var MrrAmt = Convert.ToDecimal(MrrQty * MrrRate);

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

                        if (totMrrQty > 0)
                        {
                            taInvDet.InsertInvDet(nextHdrRef.ToString(), "RC", "PO", nextHdrRefNo.ToString(), Lno, "", 1, txtPoRef.Text.Trim(),
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                txtPoRef.Text.Trim(), txtPoRef.Text.Trim(), Lno, "", DateTime.Now, Convert.ToDateTime(txtMrrDate.Text),
                                Convert.ToDouble(MrrQty), 0, Convert.ToDecimal(MrrRate), Convert.ToDecimal(MrrQty) * Convert.ToDecimal(MrrRate),
                                Convert.ToDecimal(MrrQty) * Convert.ToDecimal(MrrRate), "", "", "", 0, 0, "1", "");

                            var dtStkCtl = taStkCtl.GetDataByStoreItem(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                            if (dtStkCtl.Rows.Count > 0)
                                taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk + Convert.ToDouble(MrrQty)), 4), hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                            else
                                taStkCtl.InsertItemStore(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim(), "", Math.Round((Convert.ToDouble(MrrQty)), 4), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
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
                            //    "", txtPoRef.Text.Trim(), "N", 1, 0, "", "", "", "J", 0, "1", "MJV");

                            //jvLNo++;
                            ////Credit-Item Account
                            //taAcc.InsertAccData(itemAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                            //    nextAccRefNo.ToString(), jvLNo, 1, itemName.ToString(), "C", MrrAmt, nextHdrRefNo.ToString(), "0",
                            //    "BDT", 1, MrrAmt, "", "", "", "", "", "", "", "", "", "", "",
                            //    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                            //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, supName.ToString(), DateTime.Now,
                            //    "ADM", "P", "", DateTime.Now, "JV", "L", 0, "BDT", 1, "BDT", 1, MrrAmt, "",
                            //    "", txtPoRef.Text.Trim(), "N", 1, 0, "", "", "", "J", 0, "1", "MJV");
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

                var curYear = DateTime.Now.Year;
                cboYear.SelectedValue = curYear.ToString();

                var curMonth = DateTime.Now.Month;
                cboMonth.SelectedValue = curMonth.ToString();

                //ddlMrrList.DataSource = taInvHdr.GetInvTranDataList("RC", "PO");
                //ddlMrrList.DataTextField = "Trn_Hdr_Ref_No";
                //ddlMrrList.DataValueField = "Trn_Hdr_Ref";
                //ddlMrrList.DataBind();
                //ddlMrrList.Items.Insert(0, "----------New----------");
                //ddlMrrList.SelectedIndex = ddlMrrList.Items.IndexOf(ddlMrrList.Items.FindByText(nextHdrRefNo));

                ddlMrrList.Items.Clear();
                var taMrrHdr = new tbl_InTr_Trn_HdrTableAdapter();
                var dtMrrHdr = new dsInvTran.tbl_InTr_Trn_HdrDataTable();
                if (cboMonth.SelectedIndex == 0)//taInvHdr.GetInvTranDataList("RC", "PO");
                    dtMrrHdr = taMrrHdr.GetInvTranDataListByYear("RC", "PO", Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                else
                    dtMrrHdr = taMrrHdr.GetInvTranDataListByYearMonth("RC", "PO", Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                ListItem lst;
                foreach (dsInvTran.tbl_InTr_Trn_HdrRow dr in dtMrrHdr.Rows)
                {
                    lst = new ListItem();
                    lst.Text = dr.Trn_Hdr_Ref_No.ToString() + "    [" + dr.T_C1.ToString() + ", Date:" + dr.Trn_Hdr_Date.ToString("dd-MM-yyyy") + "]";
                    lst.Value = dr.Trn_Hdr_Ref.ToString();

                    ddlMrrList.Items.Add(lst);
                }
                ddlMrrList.Items.Insert(0, "----------New----------");

                ddlMrrList.SelectedIndex = ddlMrrList.Items.IndexOf(ddlMrrList.Items.FindByText(nextHdrRefNo));

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

            //if (ddlMrrItem.SelectedIndex == 0)
            //{
            //    ddlMrrItemUom.SelectedIndex = 0;
            //    txtMrrQty.Text = "";
            //    txtMrrRate.Text = "";
            //    txtMrrAmt.Text = "";
            //    return;
            //}

            //try
            //{
            //    txtMrrRate.Text = "";
            //    txtMrrQty.Text = "";

            //    var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(ddlMrrItem.SelectedValue.ToString()));
            //    if (dtItem.Rows.Count > 0)
            //        ddlMrrItemUom.SelectedValue = dtItem[0].IsItm_Det_Stk_UnitNull() ? "0" : dtItem[0].Itm_Det_Stk_Unit.ToString();
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

        protected void gvMrrDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var dt = new DataTable();
            dt = (DataTable)ViewState["dtMrrDet"];

            dt.Rows[rowNum].Delete();
            dt.AcceptChanges();

            gvMrrDet.EditIndex = -1;
            SetMrrDetGridData();

            foreach (GridViewRow gr in gvMrrDet.Rows)
            {
                var lblMrrQty = ((Label)(gr.FindControl("lblMrrQty"))).Text.ToString();
                totMrrQty += Convert.ToDouble(lblMrrQty.Trim());

                var lblMrrAmt = ((Label)(gr.FindControl("lblMrrAmt"))).Text.ToString();
                totMrrAmt += Convert.ToDouble(lblMrrAmt.Trim());
            }

            if (gvMrrDet.Rows.Count > 0)
            {
                lblTotMrrItem.Text = gvMrrDet.Rows.Count.ToString();
                //lblTotMrrItem.Text = totMrrQty.ToString("N2");
                lblTotMrrVal.Text = totMrrAmt.ToString("N2");
                lblTotMrrItem.Visible = true;
                lblTotMrrVal.Visible = true;
                btnPost.Visible = true;
                btnHold.Visible = true;
                btnPost.Enabled = true;
                btnHold.Enabled = true;
            }
            else
            {
                lblTotMrrItem.Text = "0";
                lblTotMrrVal.Text = "0.00";
                lblTotMrrItem.Visible = false;
                lblTotMrrVal.Visible = false;
                btnPost.Visible = false;
                btnHold.Visible = false;
                btnPost.Enabled = false;
                btnHold.Enabled = false;
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var supRef = "";
                var srchWords = txtSup.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    supRef = word;
                    break;
                }

                if (supRef.Length > 0)
                {
                    //int result;
                    //if (int.TryParse(supRef, out result))
                    //{
                        var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();
                        var dtSupAdr = taSupAdr.GetDataBySupAdrRef(supRef);
                        if (dtSupAdr.Rows.Count > 0)
                            args.IsValid = true;
                        else
                            args.IsValid = false;
                    //}
                    //else
                    //    args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex) { args.IsValid = false; }
        }

        protected void ddlMrrList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            try
            {
                //reportInfo();

                if (ddlMrrList.SelectedIndex == 0)
                {
                    ClearData();
                }
                else
                {
                    var dtInvHdr = taInvHdr.GetDataByHdrRef(Convert.ToInt32(ddlMrrList.SelectedValue.ToString()));
                    if (dtInvHdr.Rows.Count > 0)
                    {
                        txtPoRef.Text = "";
                        txtCarryCharge.Text = "";
                        txtSup.Text = "";
                        txtMrrQty.Text = "";
                        txtMrrRate.Text = "";
                        txtMrrAmt.Text = "";
                        //ddlMrrItemUom.SelectedIndex = 0;
                        txtMrrItemUom.Text = "";
                        ddlMrrStore.SelectedIndex = 0;

                        hfEditStatus.Value = "Y";

                        txtMrrDate.Text = dtInvHdr[0].Trn_Hdr_Date.ToString("dd/MM/yyyy");
                        txtPoRef.Text = dtInvHdr[0].Trn_Hdr_Tran_Ref.ToString();
                        txtCarryCharge.Text = dtInvHdr[0].Trn_Hdr_Com10.ToString();

                        var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();
                        var dtSupAdr = taSupAdr.GetDataBySupAdrRef(dtInvHdr[0].Trn_Hdr_Pcode.ToString());
                        if (dtSupAdr.Rows.Count > 0)
                            txtSup.Text = dtSupAdr[0].Par_Adr_Ref.ToString() + ":" + dtSupAdr[0].Par_Adr_Ref_No.ToString() + ":" + dtSupAdr[0].Par_Adr_Name.ToString();
                        else
                            txtSup.Text = "";

                        LoadInitMrrDetGridData();
                        SetMrrDetGridData();
                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtMrrDet"];
                        var dtMrrDet = taInvDet.GetDataByHdrRef(ddlMrrList.SelectedValue.ToString());
                        foreach (dsInvTran.tbl_InTr_Trn_DetRow dr in dtMrrDet.Rows)
                        {
                            dt.Rows.Add(dr.Trn_Hdr_Ref, dr.Trn_Hdr_Ref, dr.Trn_Det_Ref, dr.Trn_Det_Icode, dr.Trn_Det_Itm_Desc, dr.Trn_Det_Itm_Uom, dr.Trn_Det_Itm_Uom,
                                dr.Trn_Det_Str_Code, dr.Trn_Det_Str_Code, dr.Trn_Det_Lin_Qty, dr.Trn_Det_Lin_Rat.ToString("N2"), dr.Trn_Det_Lin_Net);
                        }
                        ViewState["dtMrrDet"] = dt;
                        SetMrrDetGridData();

                        foreach (GridViewRow gr in gvMrrDet.Rows)
                        {
                            var lblMrrQty = ((Label)(gr.FindControl("lblMrrQty"))).Text.ToString();
                            totMrrQty += Convert.ToDouble(lblMrrQty.Trim());

                            var lblMrrAmt = ((Label)(gr.FindControl("lblMrrAmt"))).Text.ToString();
                            totMrrAmt += Convert.ToDouble(lblMrrAmt.Trim());
                        }

                        if (gvMrrDet.Rows.Count > 0)
                        {
                            lblTotMrrItem.Text = gvMrrDet.Rows.Count.ToString();
                            lblTotMrrVal.Text = totMrrAmt.ToString("N2");
                            lblTotMrrItem.Visible = true;
                            lblTotMrrVal.Visible = true;
                            btnPost.Visible = true;
                            btnHold.Visible = true;
                            btnPost.Enabled = true;
                            btnHold.Enabled = true;
                        }
                        else
                        {
                            lblTotMrrItem.Text = "0";
                            lblTotMrrVal.Text = "0.00";
                            lblTotMrrItem.Visible = false;
                            lblTotMrrVal.Visible = false;
                            btnPost.Visible = false;
                            btnHold.Visible = false;
                            btnPost.Enabled = false;
                            btnHold.Enabled = false;
                        }

                        if (dtInvHdr[0].Trn_Hdr_HRPB_Flag == "P")
                        {
                            btnHold.Enabled = false;
                            btnPost.Enabled = false;
                            gvMrrDet.Enabled = false;
                            btnAddMrrDet.Enabled = false;
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
                ddlMrrList.Items.Clear();

                var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
                var dtMaxHdrRef = taInvHdr.GetMaxHdrRefNo("PO", Convert.ToDateTime(txtMrrDate.Text).Year);
                var nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;
                var nextHdrRefNo = "ECIL-MRR-" + Convert.ToDateTime(txtMrrDate.Text).Month.ToString("00") + Convert.ToDateTime(txtMrrDate.Text).ToString("yy") + "-" + Convert.ToInt32(nextHdrRef).ToString("000000");
                lblMrrRefNo.Text = nextHdrRefNo;

                //ddlMrrList.DataSource = taInvHdr.GetInvTranDataList("RC", "PO");
                //ddlMrrList.DataTextField = "Trn_Hdr_Ref_No";
                //ddlMrrList.DataValueField = "Trn_Hdr_Ref";
                //ddlMrrList.DataBind();
                //ddlMrrList.Items.Insert(0, "----------New----------");

                var taMrrHdr = new tbl_InTr_Trn_HdrTableAdapter();
                var dtMrrHdr = new dsInvTran.tbl_InTr_Trn_HdrDataTable();
                if (cboMonth.SelectedIndex == 0)//taInvHdr.GetInvTranDataList("RC", "PO");
                    dtMrrHdr = taMrrHdr.GetInvTranDataListByYear("RC", "PO", Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                else
                    dtMrrHdr = taMrrHdr.GetInvTranDataListByYearMonth("RC", "PO", Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                ListItem lst;
                foreach (dsInvTran.tbl_InTr_Trn_HdrRow dr in dtMrrHdr.Rows)
                {
                    lst = new ListItem();
                    lst.Text = dr.Trn_Hdr_Ref_No.ToString() + "    [" + dr.T_C1.ToString() + ", Date:" + dr.Trn_Hdr_Date.ToString("dd-MM-yyyy") + "]";
                    lst.Value = dr.Trn_Hdr_Ref.ToString();

                    ddlMrrList.Items.Add(lst);
                }
                ddlMrrList.Items.Insert(0, "----------New----------");

                txtMrrDate.Text = DateTime.Now.ToString("dd/MM/yyy");
                txtPoRef.Text = "";
                txtCarryCharge.Text = "";
                txtSup.Text = "";                

                txtMrrQty.Text = "";
                txtMrrRate.Text = "";
                txtMrrAmt.Text = "";
                txtItemName.Text = "";
                txtMrrItemUom.Text = "";

                //ddlMrrItemUom.SelectedIndex = 0;
                ddlMrrStore.SelectedIndex = 0;

                lblTotMrrItem.Text = "0.00";
                lblTotMrrVal.Text = "0.00";
                btnAddMrrDet.Enabled = true;
                btnPost.Visible = false;
                btnHold.Visible = false;
                btnPrint.Visible = false;

                hfEditStatus.Value = "N";
                hfRefNo.Value = "0";

                LoadInitMrrDetGridData();
                SetMrrDetGridData();
                gvMrrDet.Enabled = true;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void ddlMrrItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtenderSrchItem.ContextKey = ddlMrrItemType.SelectedValue.ToString();
            txtItemName.Text = "";
            txtMrrItemUom.Text = "";

            //ddlMrrItemUom.SelectedIndex = 0;

            //var taItem = new tbl_InMa_Item_DetTableAdapter();

            //if (ddlMrrItemType.SelectedIndex == 0)
            //{
            //    ddlMrrItem.Items.Clear();
            //    ddlMrrItem.Items.Insert(0, new ListItem("----------Select----------", "0"));
            //    ddlMrrItem.SelectedIndex = 0;
            //    ddlMrrItemUom.SelectedIndex = 0;
            //    txtMrrQty.Text = "";
            //    txtMrrRate.Text = "";
            //    txtMrrAmt.Text = "";
            //    return;
            //}

            //try
            //{
            //    txtMrrRate.Text = "";
            //    txtMrrQty.Text = "";

            //    var dtItem = taItem.GetDataByItemType(ddlMrrItemType.SelectedValue.ToString());
            //    if (dtItem.Rows.Count > 0)
            //    {
            //        var dtSaleItem = taItem.GetDataByItemType(ddlMrrItemType.SelectedValue.ToString());
            //        ddlMrrItem.DataSource = dtSaleItem;
            //        ddlMrrItem.DataTextField = "Itm_Det_Desc";
            //        ddlMrrItem.DataValueField = "Itm_Det_Ref";
            //        ddlMrrItem.DataBind();
            //        ddlMrrItem.Items.Insert(0, new ListItem("----------Select----------", "0"));
            //    }                 
            //    else
            //    {
            //        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item Type.";
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
                            //ddlMrrItemUom.SelectedValue = dtItem[0].IsItm_Det_Stk_UnitNull() ? "0" : dtItem[0].Itm_Det_Stk_Unit.ToString();
                            txtMrrItemUom.Text = dtItem[0].IsItm_Det_Stk_UnitNull() ? "" : dtItem[0].Itm_Det_Stk_Unit.ToString();

                            ddlMrrStore.Focus();
                            //if (cboStore.SelectedIndex != 0)
                            //{
                            //    var dtStkCtl = taStkCtl.GetDataByStoreItem(cboStore.SelectedValue.ToString(), itemRef.ToString());
                            //    txtStock.Text = dtStkCtl.Rows.Count > 0 ? dtStkCtl[0].Stk_Ctl_Cur_Stk.ToString() : "0";
                            //}
                        }
                        else
                        {
                            //ddlMrrItemUom.SelectedIndex = 0;
                            txtMrrItemUom.Text = "";
                            //txtStock.Text = "0";
                        }
                    }
                    else
                    {
                        //ddlMrrItemUom.SelectedIndex = 0;
                        txtMrrItemUom.Text = "";
                        //txtStock.Text = "0";
                    }
                }
                else
                {
                    //ddlMrrItemUom.SelectedIndex = 0;
                    txtMrrItemUom.Text = "";
                    //txtStock.Text = "0";
                }
            }
            catch (Exception ex)
            {
                //ddlMrrItemUom.SelectedIndex = 0;
                txtMrrItemUom.Text = "";
                //txtStock.Text = "0";
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_MRR_List();
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_MRR_List();
        }

        private void Get_MRR_List()
        {
            ClearData();

            ddlMrrList.Items.Clear();

            var taMrrHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var dtMrrHdr = new dsInvTran.tbl_InTr_Trn_HdrDataTable();
            if (cboMonth.SelectedIndex == 0)//taInvHdr.GetInvTranDataList("RC", "PO");
                dtMrrHdr = taMrrHdr.GetInvTranDataListByYear("RC", "PO", Convert.ToDecimal(cboYear.SelectedValue.ToString()));
            else
                dtMrrHdr = taMrrHdr.GetInvTranDataListByYearMonth("RC", "PO", Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
            ListItem lst;
            foreach (dsInvTran.tbl_InTr_Trn_HdrRow dr in dtMrrHdr.Rows)
            {
                lst = new ListItem();
                lst.Text = dr.Trn_Hdr_Ref_No.ToString() + "    [" + dr.T_C1.ToString() + ", Date:" + dr.Trn_Hdr_Date.ToString("dd-MM-yyyy") + "]";
                lst.Value = dr.Trn_Hdr_Ref.ToString();

                ddlMrrList.Items.Add(lst);
            }
            ddlMrrList.Items.Insert(0, "----------New----------");
        }
    }
}