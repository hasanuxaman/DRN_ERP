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
    public partial class frmIssueDirect : System.Web.UI.Page
    {
        double totIssQty = 0;

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

            Get_Isu_List();

            txtIssDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();

            var dtMaxHdrRef = taInvHdr.GetMaxHdrRefNo("ISU", DateTime.Now.Year);
            var nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;
            var nextHdrRefNo = "ECIL-ISU-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRef).ToString("000000");
            lblIssRefNo.Text = nextHdrRefNo;

            //ddlIssueList.DataSource = taInvHdr.GetInvTranDataList("IS", "ISU");
            //ddlIssueList.DataTextField = "Trn_Hdr_Ref_No";
            //ddlIssueList.DataValueField = "Trn_Hdr_Ref";
            //ddlIssueList.DataBind();
            //ddlIssueList.Items.Insert(0, "----------New----------");

            //var taItem = new tbl_InMa_Item_DetTableAdapter();
            //var dtItem = taItem.GetDataBySortAsc();
            //foreach (dsInvMas.tbl_InMa_Item_DetRow dr in dtItem.Rows)
            //{
            //    cboItem.Items.Add(new ListItem(dr.Itm_Det_Desc.ToString() + " [" + dr.Itm_Det_Ref.ToString() + "]", dr.Itm_Det_Ref.ToString()));
            //}            
            //cboItem.Items.Insert(0, new ListItem("----------Select----------", "0"));

            var taItemType = new tbl_InMa_TypeTableAdapter();
            var dtItemType = taItemType.GetDataByAsc();
            cboItemType.DataSource = dtItemType;
            cboItemType.DataTextField = "Item_Type_Name";
            cboItemType.DataValueField = "Item_Type_Code";
            cboItemType.DataBind();
            cboItemType.Items.Insert(0, new ListItem("-----All-----", "0"));
            cboItemType.SelectedIndex = 0;

            var taItemUom = new tbl_InMa_UomTableAdapter();
            var dtItemUom = taItemUom.GetData();
            cboItemUom.DataSource = dtItemUom;
            cboItemUom.DataTextField = "Uom_Name";
            cboItemUom.DataValueField = "Uom_Code";
            cboItemUom.DataBind();
            cboItemUom.Items.Insert(0, new ListItem("----", "0"));
            cboItemUom.SelectedIndex = 0;

            var taStore = new tbl_InMa_Str_LocTableAdapter();
            var dtStore = taStore.GetDataByGenStore();
            cboStore.DataSource = dtStore;
            cboStore.DataTextField = "Str_Loc_Name";
            cboStore.DataValueField = "Str_Loc_Ref";
            cboStore.DataBind();
            cboStore.Items.Insert(0, new ListItem("----", "0"));
            cboStore.SelectedIndex = 0;

            LoadInitIssDetGridData();
            SetIssDetGridData();

            //txtMrrQty.Attributes.Add("onkeyup", "CalcMrrAmount('" + txtMrrQty.ClientID + "', '" + txtMrrRate.ClientID + "', '" + txtMrrAmt.ClientID + "' )");
            //txtMrrRate.Attributes.Add("onkeyup", "CalcMrrAmount('" + txtMrrQty.ClientID + "', '" + txtMrrRate.ClientID + "', '" + txtMrrAmt.ClientID + "' )");            
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

        protected void btnAddIsuDet_Click(object sender, EventArgs e)
        {
            Page.Validate("btnSave");
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

                foreach (GridViewRow gr in gvIssDet.Rows)
                {
                    var lblItemCode = ((Label)(gr.FindControl("lblItemCode"))).Text.ToString();
                    if (itemRef.Trim().ToString() == lblItemCode.ToString())
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = itemName.Trim().ToString() + " already addred to Issue details.";
                        tblMsg.Rows[1].Cells[0].InnerText = "To add more you need to delete existing same item.";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                if (txtIssQty.Text.Trim() == "" || txtIssQty.Text.Trim().Length <= 0 || Convert.ToDouble(txtIssQty.Text.Trim()) <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter valid Quantity.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                var dt = new DataTable();
                dt = (DataTable)ViewState["dtTranDet"];

                var TRN_HDR_REF = "0";
                var TRN_DET_REF = "0";
                var TRN_DET_REF_NO = "0";
                var TRN_ITEM_REF = itemRef.Trim().ToString();
                var TRN_ITEM_NAME = itemName.Trim().ToString();
                var TRN_ITEM_UOM_REF = cboItemUom.SelectedValue.ToString();
                var TRN_ITEM_UOM = cboItemUom.SelectedItem.ToString();
                var TRN_STORE_REF = cboStore.SelectedValue.ToString();
                var TRN_STORE_NAME = cboStore.SelectedItem.ToString();
                var TRN_QTY = Convert.ToDouble(txtIssQty.Text.Trim().Length > 0 ? txtIssQty.Text.Trim() : "0");
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
                    btnPost.Visible = true;
                    btnHold.Visible = true;
                    btnPost.Enabled = true;
                    btnHold.Enabled = true;
                }
                else
                {
                    lblTotMrrItem.Text = "0";
                    lblTotIssQty.Text = "0.00";
                    lblTotMrrItem.Visible = false;
                    lblTotIssQty.Visible = false;
                    btnPost.Visible = false;
                    btnHold.Visible = false;
                    btnPost.Enabled = false;
                    btnHold.Enabled = false;
                }

                txtIssQty.Text = "";
                txtStock.Text = "";
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
                #region Get Location Details
                var locRef = "";
                var locName = "";
                var locAccCode = "";
                var srchWords = txtIssLoc.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    locRef = word;
                    break;
                }

                if (locRef.Length > 0)
                {
                    var taIssLoc = new View_Issue_HeadTableAdapter();
                    var dtIssLoc = taIssLoc.GetDataByIsuHeadRef(locRef);
                    locRef = dtIssLoc.Rows.Count > 0 ? dtIssLoc[0].IsuHeadRefNo.ToString() : "";
                    locName = dtIssLoc.Rows.Count > 0 ? dtIssLoc[0].IsuHeadName.ToString() : "";
                    //locAccCode = dtIssLoc.Rows.Count > 0 ? dtIssLoc[0].Str_Loc_Ref.ToString() : "";   
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Location.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

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

                    var dtMaxMrrRef = taInvHdr.GetMaxHdrRefNo("ISU", Convert.ToDateTime(txtIssDate.Text).Year);
                    var nextMrrRef = dtMaxMrrRef == null ? 1 : Convert.ToInt32(dtMaxMrrRef) + 1;
                    nextHdrRefNo = "ECIL-ISU-" + Convert.ToDateTime(txtIssDate.Text).ToString("00") + Convert.ToDateTime(txtIssDate.Text).ToString("yy") + "-" + Convert.ToInt32(nextMrrRef).ToString("000000");

                    taInvHdr.InsertInvHdr(nextHdrRef, "IS", "ISU", nextHdrRefNo, locRef.ToString(), locRef.ToString(), locRef.ToString(),
                        txtReqRef.Text.Trim(), Convert.ToDateTime(txtIssDate.Text), "", "", "", txtRem.Text.Trim(), "", "", "", "", "", "", Convert.ToDecimal(0), "H",
                        (Convert.ToDateTime(txtIssDate.Text).Month.ToString("00") + "/" + Convert.ToDateTime(txtIssDate.Text).Year.ToString()).ToString(), "ADM", "", "", "",
                        "", "", "", locName, "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

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
                            taInvDet.InsertInvDet(nextHdrRef.ToString(), "IS", "ISU", nextHdrRefNo.ToString(), Lno, "", 1, txtReqRef.Text.Trim(),
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                txtReqRef.Text.Trim(), txtReqRef.Text.Trim(), Lno, "", Convert.ToDateTime(txtIssDate.Text), DateTime.Now,
                                Convert.ToDouble(IssQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(IssQty) * Convert.ToDecimal(0),
                                Convert.ToDecimal(IssQty) * Convert.ToDecimal(0), "", "", "", 0, 0, "1", "");                            
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
                    nextHdrRef = Convert.ToInt32(ddlIssueList.SelectedValue.ToString());
                    nextHdrRefNo = ddlIssueList.SelectedItem.ToString();

                    taInvHdr.UpdateInvHdrByHdrRef(locRef.ToString(), locRef.ToString(), locRef.ToString(), txtReqRef.Text.Trim(), Convert.ToDateTime(txtIssDate.Text), "", "", "", txtRem.Text.Trim(), "", "", "", "", "",
                        "", Convert.ToDecimal(0), "H", (Convert.ToDateTime(txtIssDate.Text).Month.ToString("00") + "/" + Convert.ToDateTime(txtIssDate.Text).Year.ToString()).ToString(),
                        "ADM", "", "", "", "", "", "", locName, "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "",
                        Convert.ToInt32(nextHdrRef));

                    taInvDet.DeleteInvDetByHdrRef(ddlIssueList.SelectedValue.ToString());

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
                            taInvDet.InsertInvDet(nextHdrRef.ToString(), "IS", "ISU", nextHdrRefNo.ToString(), Lno, "", 1, txtReqRef.Text.Trim(),
                                Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                txtReqRef.Text.Trim(), txtReqRef.Text.Trim(), Lno, "", Convert.ToDateTime(txtIssDate.Text), DateTime.Now,
                                Convert.ToDouble(IssQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(IssQty) * Convert.ToDecimal(0),
                                Convert.ToDecimal(IssQty) * Convert.ToDecimal(0), "", "", "", 0, 0, "1", "");
                        }
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnPrint.Visible = true;
                }

                var curYear = DateTime.Now.Year;
                cboYear.SelectedValue = curYear.ToString();

                var curMonth = DateTime.Now.Month;
                cboMonth.SelectedValue = curMonth.ToString();

                ddlIssueList.DataSource = taInvHdr.GetDataByTrnRefListByYearMonth("IS", "ISU", Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                ddlIssueList.DataTextField = "Trn_Hdr_Ref_No";
                ddlIssueList.DataValueField = "Trn_Hdr_Ref";
                ddlIssueList.DataBind();
                ddlIssueList.Items.Insert(0, "----------New----------");
                ddlIssueList.SelectedIndex = ddlIssueList.Items.IndexOf(ddlIssueList.Items.FindByText(nextHdrRefNo));

                hfEditStatus.Value = "Y";
                hfRefNo.Value = "0";
            }
            catch (Exception ex)
            {
                hfEditStatus.Value = "N";
                hfRefNo.Value = "0";
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
                #region Get Location Details
                var locRef = "";
                var locName = "";
                var locAccCode = "";
                var srchWords = txtIssLoc.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    locRef = word;
                    break;
                }

                if (locRef.Length > 0)
                {
                    var taIssLoc = new View_Issue_HeadTableAdapter();
                    var dtIssLoc = taIssLoc.GetDataByIsuHeadRef(locRef);
                    locRef = dtIssLoc.Rows.Count > 0 ? dtIssLoc[0].IsuHeadRefNo.ToString() : "";
                    locName = dtIssLoc.Rows.Count > 0 ? dtIssLoc[0].IsuHeadName.ToString() : "";
                    //locAccCode = dtIssLoc.Rows.Count > 0 ? dtIssLoc[0].Str_Loc_Ref.ToString() : "";   
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Location.";
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

                    var dtMaxMrrRef = taInvHdr.GetMaxHdrRefNo("ISU", Convert.ToDateTime(txtIssDate.Text).Year);
                    var nextMrrRef = dtMaxMrrRef == null ? 1 : Convert.ToInt32(dtMaxMrrRef) + 1;
                    nextHdrRefNo = "ECIL-ISU-" + Convert.ToDateTime(txtIssDate.Text).Month.ToString("00") + Convert.ToDateTime(txtIssDate.Text).ToString("yy") + "-" + Convert.ToInt32(nextMrrRef).ToString("000000");

                    taInvHdr.InsertInvHdr(nextHdrRef, "IS", "ISU", nextHdrRefNo, locRef.ToString(), locRef.ToString(), locRef.ToString(),
                        txtReqRef.Text.Trim(), Convert.ToDateTime(txtIssDate.Text), "", "", "", txtRem.Text.Trim(), "", "", "", "", "", "", Convert.ToDecimal(0), "P",
                        (Convert.ToDateTime(txtIssDate.Text).Month.ToString("00") + "/" + Convert.ToDateTime(txtIssDate.Text).Year.ToString()).ToString(), "ADM", "", "", "",
                        "", "", "", locName, "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

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
                            var dtStkCtl = taStkCtl.GetDataByStoreItem(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                            if (dtStkCtl.Rows.Count > 0)
                            {
                                if (dtStkCtl[0].Stk_Ctl_Cur_Stk >= Convert.ToDouble(IssQty))
                                {
                                    taInvDet.InsertInvDet(nextHdrRef.ToString(), "IS", "ISU", nextHdrRefNo.ToString(), Lno, "", 1, txtReqRef.Text.Trim(),
                                        Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                        txtReqRef.Text.Trim(), txtReqRef.Text.Trim(), Lno, "", Convert.ToDateTime(txtIssDate.Text), DateTime.Now,
                                        Convert.ToDouble(IssQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(IssQty) * Convert.ToDecimal(0),
                                        Convert.ToDecimal(IssQty) * Convert.ToDecimal(0), "", "", "", 0, 0, "1", "");

                                    taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk - Convert.ToDouble(IssQty)), 4), hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                                }
                                else
                                {
                                    myTran.Rollback();
                                    tblMsg.Rows[0].Cells[0].InnerText = "There is not sufficient stock for the item [" + lblItemDesc.Text.Trim() + "]";
                                    tblMsg.Rows[1].Cells[0].InnerText = "Current Stock is: " + dtStkCtl[0].Stk_Ctl_Cur_Stk.ToString();
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }
                            }
                            else
                            {
                                myTran.Rollback();
                                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error";
                                tblMsg.Rows[1].Cells[0].InnerText = "Inventory data not found for the item " + lblItemDesc.Text.Trim();
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
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
                    nextHdrRef = Convert.ToInt32(ddlIssueList.SelectedValue.ToString());
                    nextHdrRefNo = ddlIssueList.SelectedItem.ToString();

                    taInvHdr.UpdateInvHdrByHdrRef(locRef.ToString(), locRef.ToString(), locRef.ToString(), txtReqRef.Text.Trim(), Convert.ToDateTime(txtIssDate.Text), "", "", "", txtRem.Text.Trim(), "", "", "", "", "",
                        "", Convert.ToDecimal(0), "P", (Convert.ToDateTime(txtIssDate.Text).Month.ToString("00") + "/" + Convert.ToDateTime(txtIssDate.Text).Year.ToString()).ToString(),
                        "ADM", "", "", "", "", "", "", locName, "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "",
                        Convert.ToInt32(nextHdrRef));

                    taInvDet.DeleteInvDetByHdrRef(ddlIssueList.SelectedValue.ToString());

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
                            var dtStkCtl = taStkCtl.GetDataByStoreItem(hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                            if (dtStkCtl.Rows.Count > 0)
                            {
                                if (dtStkCtl[0].Stk_Ctl_Cur_Stk >= Convert.ToDouble(IssQty))
                                {
                                    taInvDet.InsertInvDet(nextHdrRef.ToString(), "IS", "ISU", nextHdrRefNo.ToString(), Lno, "", 1, txtReqRef.Text.Trim(),
                                        Lno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), hfStoreRef.Value.ToString(), "",
                                        txtReqRef.Text.Trim(), txtReqRef.Text.Trim(), Lno, "", Convert.ToDateTime(txtIssDate.Text), DateTime.Now,
                                        Convert.ToDouble(IssQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(IssQty) * Convert.ToDecimal(0),
                                        Convert.ToDecimal(IssQty) * Convert.ToDecimal(0), "", "", "", 0, 0, "1", "");

                                    taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk - Convert.ToDouble(IssQty)), 4), hfStoreRef.Value.ToString(), lblItemCode.Text.Trim());
                                }
                                else
                                {
                                    myTran.Rollback();
                                    tblMsg.Rows[0].Cells[0].InnerText = "There is not sufficient stock for the item [" + lblItemDesc.Text.Trim() + "]";
                                    tblMsg.Rows[1].Cells[0].InnerText = "Current Stock is: " + dtStkCtl[0].Stk_Ctl_Cur_Stk.ToString();
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }
                            }
                            else
                            {
                                myTran.Rollback();
                                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error";
                                tblMsg.Rows[1].Cells[0].InnerText = "Inventory data not found for the item " + lblItemDesc.Text.Trim();
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
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

                ddlIssueList.DataSource = taInvHdr.GetDataByTrnRefListByYearMonth("IS", "ISU", Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                ddlIssueList.DataTextField = "Trn_Hdr_Ref_No";
                ddlIssueList.DataValueField = "Trn_Hdr_Ref";
                ddlIssueList.DataBind();
                ddlIssueList.Items.Insert(0, "----------New----------");
                ddlIssueList.SelectedIndex = ddlIssueList.Items.IndexOf(ddlIssueList.Items.FindByText(nextHdrRefNo));

                hfEditStatus.Value = "Y";
                hfRefNo.Value = "0";
            }
            catch (Exception ex)
            {
                hfEditStatus.Value = "Y";
                hfRefNo.Value = "0";
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }   

        protected void btnPrint_Click(object sender, EventArgs e)
        {

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
                btnPost.Visible = true;
                btnHold.Visible = true;
                btnPost.Enabled = true;
                btnHold.Enabled = true;
            }
            else
            {
                lblTotMrrItem.Text = "0";
                lblTotIssQty.Text = "0.00";
                lblTotMrrItem.Visible = false;
                lblTotIssQty.Visible = false;
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
                var locRef = "";
                var srchWords = txtIssLoc.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    locRef = word;
                    break;
                }

                if (locRef.Length > 0)
                {
                    var taSupAdr = new View_Issue_HeadTableAdapter();
                    var dtSupAdr = taSupAdr.GetDataByIsuHeadRef(locRef);
                    if (dtSupAdr.Rows.Count > 0)
                        args.IsValid = true;
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex) { args.IsValid = false; }
        }

        protected void ddlIssueList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            try
            {
                //reportInfo();

                if (ddlIssueList.SelectedIndex == 0)
                {
                    ClearData();
                }
                else
                {
                    var dtInvHdr = taInvHdr.GetDataByHdrRef(Convert.ToInt32(ddlIssueList.SelectedValue.ToString()));
                    if (dtInvHdr.Rows.Count > 0)
                    {
                        txtReqRef.Text = "";
       
                        txtIssLoc.Text = "";
                        txtIssQty.Text = "";

                        cboItemUom.SelectedIndex = 0;
                        cboStore.SelectedIndex = 0;

                        hfEditStatus.Value = "Y";

                        txtIssDate.Text = dtInvHdr[0].Trn_Hdr_Date.ToString("dd/MM/yyyy");
                        txtReqRef.Text = dtInvHdr[0].Trn_Hdr_Tran_Ref.ToString();

                        var taIssLoc = new View_Issue_HeadTableAdapter();
                        var dtIssLoc = taIssLoc.GetDataByIsuHeadRef(dtInvHdr[0].Trn_Hdr_Pcode.ToString());
                        if (dtIssLoc.Rows.Count > 0)
                            txtIssLoc.Text = dtIssLoc[0].IsuHeadRefNo.ToString() + "::" + dtIssLoc[0].IsuHeadCode.ToString() + "::" + dtIssLoc[0].IsuHeadName.ToString() + "::" + dtIssLoc[0].IsuHeadDet.ToString();
                        else
                            txtIssLoc.Text = "";

                        txtRem.Text = dtInvHdr[0].Trn_Hdr_Com4.ToString();

                        LoadInitIssDetGridData();
                        SetIssDetGridData();
                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtTranDet"];
                        var dtTranDet = taInvDet.GetDataByHdrRef(ddlIssueList.SelectedValue.ToString());
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
                            btnPost.Visible = true;
                            btnHold.Visible = true;
                            btnPost.Enabled = true;
                            btnHold.Enabled = true;
                        }
                        else
                        {
                            lblTotMrrItem.Text = "0";
                            lblTotIssQty.Text = "0.00";
                            lblTotMrrItem.Visible = false;
                            lblTotIssQty.Visible = false;
                            btnPost.Visible = false;
                            btnHold.Visible = false;
                            btnPost.Enabled = false;
                            btnHold.Enabled = false;
                        }

                        if (dtInvHdr[0].Trn_Hdr_HRPB_Flag == "P")
                        {
                            btnHold.Enabled = false;
                            btnPost.Enabled = false;
                            gvIssDet.Enabled = false;
                            btnAddIsuDet.Enabled = false;
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
                ddlIssueList.Items.Clear();

                var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();

                var dtMaxHdrRef = taInvHdr.GetMaxHdrRefNo("ISU", DateTime.Now.Year);
                var nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;
                var nextHdrRefNo = "ECIL-ISU-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRef).ToString("000000");
                lblIssRefNo.Text = nextHdrRefNo;

                ddlIssueList.DataSource = taInvHdr.GetDataByTrnRefListByYearMonth("IS", "ISU", Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                ddlIssueList.DataTextField = "Trn_Hdr_Ref_No";
                ddlIssueList.DataValueField = "Trn_Hdr_Ref";
                ddlIssueList.DataBind();
                ddlIssueList.Items.Insert(0, "----------New----------");

                txtIssDate.Text = DateTime.Now.ToString("dd/MM/yyy");
                txtReqRef.Text = "";
                txtIssLoc.Text = "";
                txtRem.Text = "";

                txtIssQty.Text = "";

                cboItemType.SelectedIndex = 0;
                txtItemName.Text = "";
                //cboItem.SelectedIndex = 0;
                cboItemUom.SelectedIndex = 0;
                cboStore.SelectedIndex = 0;

                lblTotMrrItem.Text = "0.00";
                lblTotIssQty.Text = "0.00";
                btnAddIsuDet.Enabled = true;
                btnPost.Visible = false;
                btnHold.Visible = false;
                btnPrint.Visible = false;

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

            //var taItem = new tbl_InMa_Item_DetTableAdapter();

            //if (cboItemType.SelectedIndex == 0)
            //{
            //    cboItem.Items.Clear();
            //    var dtItem = taItem.GetDataBySortAsc();
            //    foreach (dsInvMas.tbl_InMa_Item_DetRow dr in dtItem.Rows)
            //    {
            //        cboItem.Items.Add(new ListItem(dr.Itm_Det_Desc.ToString() + " [" + dr.Itm_Det_Ref.ToString() + "]", dr.Itm_Det_Ref.ToString()));
            //    }
            //    cboItem.Items.Insert(0, new ListItem("----------Select----------", "0"));                
            //    cboItem.SelectedIndex = 0;
            //    cboItemUom.SelectedIndex = 0;
            //    txtIssQty.Text = "";                
            //    return;
            //}

            //try
            //{                
            //    var dtItem = taItem.GetDataByItemType(cboItemType.SelectedValue.ToString());
            //    if (dtItem.Rows.Count > 0)
            //    {
            //        foreach (dsInvMas.tbl_InMa_Item_DetRow dr in dtItem.Rows)
            //        {
            //            cboItem.Items.Add(new ListItem(dr.Itm_Det_Desc.ToString() + " [" + dr.Itm_Det_Ref.ToString() + "]", dr.Itm_Det_Ref.ToString()));
            //        }
            //        cboItem.Items.Insert(0, new ListItem("----------Select----------", "0"));                    
            //    }
            //    else
            //    {
            //        cboItem.Items.Clear();
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
                            if (cboStore.SelectedIndex != 0)
                            {
                                var dtStkCtl = taStkCtl.GetDataByStoreItem(cboStore.SelectedValue.ToString(), itemRef.ToString());
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

        protected void cboStore_SelectedIndexChanged(object sender, EventArgs e)
        {
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

                var dtStkCtl = taStkCtl.GetDataByStoreItem(cboStore.SelectedValue.ToString(), itemRef.ToString());
                txtStock.Text = dtStkCtl.Rows.Count > 0 ? Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk), 4).ToString() : "0";
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

        private void Get_Isu_List()
        {
            ClearData();

            var taInvTrnHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var dtInvTrnHdr = new dsInvTran.tbl_InTr_Trn_HdrDataTable();

            if (cboMonth.SelectedIndex == 0)
                dtInvTrnHdr = taInvTrnHdr.GetDataByTranRefListByYear("IS", "ISU", Convert.ToDecimal(cboYear.SelectedValue.ToString()));
            else
                dtInvTrnHdr = taInvTrnHdr.GetDataByTrnRefListByYearMonth("IS", "ISU", Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));

            ddlIssueList.DataSource = dtInvTrnHdr;
            ddlIssueList.DataTextField = "Trn_Hdr_Ref_No";
            ddlIssueList.DataValueField = "Trn_Hdr_Ref";
            ddlIssueList.DataBind();
            ddlIssueList.Items.Insert(0, "----------New----------");
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_Isu_List();
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_Isu_List();
        }
    }
}