using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
//using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
//using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmPoDirect : System.Web.UI.Page
    {
        double totPoQty = 0;
        double totPoAmt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            AutoCompleteExtenderSrchItem.ContextKey = "0";

            //btnPrint.Attributes.Add("onclick", "javascript:w= window.open('frmShowSalesReport.aspx');");

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

            txtPoDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taPoHdr = new tbl_PuTr_PO_HdrTableAdapter();

            var dtMaxHdrRef = taPoHdr.GetMaxPoRef("SPO", DateTime.Now.Year);
            var nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;
            var nextHdrRefNo = "SPO-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRef).ToString("000000");
            lblPoRefNo.Text = nextHdrRefNo;            

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            if (empRef == "000011" || empRef == "000442")//--------Tarikul-SCM
            {
                txtSup.Text = "";
                txtSup.Enabled = true;
            }
            else
            {
                txtSup.Text = "10.100281:SUP-10.100281:Cash Party - Factory";
                txtSup.Enabled = false;
            }

            LoadPoList();

            var taItemType = new tbl_InMa_TypeTableAdapter();
            var dtItemType = taItemType.GetDataByAsc();
            ddlItemType.DataSource = dtItemType;
            ddlItemType.DataTextField = "Item_Type_Name";
            ddlItemType.DataValueField = "Item_Type_Code";
            ddlItemType.DataBind();
            ddlItemType.Items.Insert(0, new ListItem("-----Select-----", "0"));

            //var taSaleItem = new tbl_InMa_Item_DetTableAdapter();
            //var dtSaleItem = taSaleItem.GetRmItemData();
            //ddlItem.DataSource = dtSaleItem;
            //ddlItem.DataTextField = "Itm_Det_Desc";
            //ddlItem.DataValueField = "Itm_Det_Ref";
            //ddlItem.DataBind();
            //ddlItem.Items.Insert(0, new ListItem("----------Select----------", "0"));

            var taItemUom = new tbl_InMa_UomTableAdapter();
            var dtItemUom = taItemUom.GetData();
            ddlItemUom.DataSource = dtItemUom;
            ddlItemUom.DataTextField = "Uom_Name";
            ddlItemUom.DataValueField = "Uom_Code";
            ddlItemUom.DataBind();
            ddlItemUom.Items.Insert(0, new ListItem("----", "0"));
            ddlItemUom.SelectedIndex = 0;

            var taStore = new tbl_InMa_Str_LocTableAdapter();
            var dtStore = taStore.GetDataBySortName();
            ddlStore.DataSource = dtStore;
            ddlStore.DataTextField = "Str_Loc_Name";
            ddlStore.DataValueField = "Str_Loc_Ref";
            ddlStore.DataBind();
            ddlStore.Items.Insert(0, new ListItem("----", "0"));
            ddlStore.SelectedIndex = 0;

            LoadInitPoDetGridData();
            SetPoDetGridData();

            txtPoQty.Attributes.Add("onkeyup", "CalcPoAmount('" + txtPoQty.ClientID + "', '" + txtPoRate.ClientID + "', '" + txtPoAmt.ClientID + "' )");
            txtPoRate.Attributes.Add("onkeyup", "CalcPoAmount('" + txtPoQty.ClientID + "', '" + txtPoRate.ClientID + "', '" + txtPoAmt.ClientID + "' )");
        }

        #region PO Details Gridview
        protected void LoadInitPoDetGridData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PO_HDR_REF", typeof(string));
            dt.Columns.Add("PO_DET_REF", typeof(string));
            dt.Columns.Add("PO_DET_REF_NO", typeof(string));
            dt.Columns.Add("PO_ITEM_REF", typeof(string));
            dt.Columns.Add("PO_ITEM_NAME", typeof(string));
            dt.Columns.Add("PO_ITEM_UOM_REF", typeof(string));
            dt.Columns.Add("PO_ITEM_UOM", typeof(string));
            dt.Columns.Add("PO_STORE_REF", typeof(string));
            dt.Columns.Add("PO_STORE_NAME", typeof(string));
            dt.Columns.Add("PO_QTY", typeof(string));
            dt.Columns.Add("PO_RATE", typeof(string));
            dt.Columns.Add("PO_AMOUNT", typeof(string));            
            ViewState["dtPoDet"] = dt;
        }

        protected void SetPoDetGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtPoDet"];

                gvPoDet.DataSource = dt;
                gvPoDet.DataBind();
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

                foreach (GridViewRow gr in gvPoDet.Rows)
                {
                    var lblItemCode = ((Label)(gr.FindControl("lblItemCode"))).Text.ToString();
                    if (itemRef.ToString() == lblItemCode.ToString())
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = itemName.ToString() + " already addred to PO details.";
                        tblMsg.Rows[1].Cells[0].InnerText = "To add more you need to delete existing same item.";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                if (txtPoAmt.Text.Trim() == "" || txtPoAmt.Text.Trim().Length <= 0 || Convert.ToDouble(txtPoAmt.Text.Trim()) <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter valid data.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                var dt = new DataTable();
                dt = (DataTable)ViewState["dtPoDet"];

                var PO_HDR_REF = "0";
                var PO_DET_REF = "0";
                var PO_DET_REF_NO = "0";
                var PO_ITEM_REF = itemRef.ToString();
                var PO_ITEM_NAME = itemName.ToString();
                var PO_ITEM_UOM_REF = ddlItemUom.SelectedValue.ToString();
                var PO_ITEM_UOM = ddlItemUom.SelectedItem.ToString();
                var PO_STORE_REF = ddlStore.SelectedValue.ToString();
                var PO_STORE_NAME = ddlStore.SelectedItem.ToString();
                var PO_QTY = Convert.ToDouble(txtPoQty.Text.Trim().Length > 0 ? txtPoQty.Text.Trim() : "0");
                var PO_RATE = Convert.ToDouble(txtPoRate.Text.Trim().Length > 0 ? txtPoRate.Text.Trim() : "0");
                var PO_AMOUNT = (PO_QTY * PO_RATE).ToString("N2");

                dt.Rows.Add(PO_HDR_REF, PO_DET_REF, PO_DET_REF_NO, PO_ITEM_REF, PO_ITEM_NAME, PO_ITEM_UOM_REF, PO_ITEM_UOM, PO_STORE_REF, PO_STORE_NAME, PO_QTY, 
                    PO_RATE.ToString("N2"), PO_AMOUNT);

                ViewState["dtPoDet"] = dt;
                SetPoDetGridData();

                foreach (GridViewRow gr in gvPoDet.Rows)
                {
                    var lblPoQty = ((Label)(gr.FindControl("lblPoQty"))).Text.ToString();
                    totPoQty += Convert.ToDouble(lblPoQty.Trim());

                    var lblMrrAmt = ((Label)(gr.FindControl("lblPoAmt"))).Text.ToString();
                    totPoAmt += Convert.ToDouble(lblMrrAmt.Trim());
                }

                if (gvPoDet.Rows.Count > 0)
                {
                    lblTotPoItem.Text = gvPoDet.Rows.Count.ToString();
                    //lblTotPoItem.Text = totPoQty.ToString("N2");
                    lblTotPoVal.Text = totPoAmt.ToString("N2");
                    lblTotPoItem.Visible = true;
                    lblTotPoVal.Visible = true;
                    btnPost.Enabled = true;
                    btnHold.Enabled = true;
                }
                else
                {
                    lblTotPoItem.Text = "0";
                    lblTotPoVal.Text = "0.00";
                    lblTotPoItem.Visible = false;
                    lblTotPoVal.Visible = false;
                    btnPost.Enabled = false;
                    btnHold.Enabled = false;
                }

                txtPoQty.Text = "";
                txtPoRate.Text = "";
                txtPoAmt.Text = "";
                txtItemName.Text = "";
                ddlItemUom.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
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
            Page.Validate("btnAdd");

            if (!Page.IsValid) return;

            var taPoHdr = new tbl_PuTr_PO_HdrTableAdapter();
            var taPoDet = new tbl_PuTr_PO_DetTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taPoHdr.Connection);

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

                var nextHdrRef = "";
                var nextHdrRefNo = "";

                taPoHdr.AttachTransaction(myTran);
                taPoDet.AttachTransaction(myTran);

                foreach (GridViewRow gr in gvPoDet.Rows)
                {
                    var lblPoQty = ((Label)(gr.FindControl("lblPoQty"))).Text.ToString();
                    totPoQty += Convert.ToDouble(lblPoQty.Trim());

                    var lblMrrAmt = ((Label)(gr.FindControl("lblPoAmt"))).Text.ToString();
                    totPoAmt += Convert.ToDouble(lblMrrAmt.Trim());
                }

                var empId = "";
                var empName = "";
                var empDesig = "";
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt32(empRef));
                if (dtEmp.Rows.Count > 0)
                {
                    empId = dtEmp[0].EmpId.ToString();
                    empName = dtEmp[0].EmpName.ToString();
                    empDesig = dtEmp[0].DesigName.ToString();
                }

                if (hfEditStatus.Value == "N")
                {
                    //Inventory Header Ref
                    //PO Header Ref                   
                    var dtMaxHdrRef = taPoHdr.GetMaxPoRef("SPO", DateTime.Now.Year);
                    nextHdrRef = dtMaxHdrRef == null ? "1" : (Convert.ToInt32(dtMaxHdrRef) + 1).ToString();
                    nextHdrRefNo = "SPO-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRef).ToString("000000");

                    taPoHdr.InsertPoHdr("PO", "SPO", nextHdrRefNo, supRef.ToString(), supRef.ToString(), supAccCode.ToString(), DateTime.Now, "", "",
                                    "", "", "", "", "", "", "", supName.ToString(), Convert.ToDecimal(totPoAmt), "H", DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00"),
                                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), DateTime.Now, "", "", "", DateTime.Now,
                                    DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00"), "", "", "", 1, "BDT", 0);

                    #region Insert Inventory Details
                    short Lno = 0;
                    foreach (GridViewRow gr in gvPoDet.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblStore"));

                        var lblPoQty = (Label)(gr.FindControl("lblPoQty"));
                        var lblPoRate = (Label)(gr.FindControl("lblPoRate"));

                        var PoQty = Convert.ToDouble(lblPoQty.Text.Trim());
                        var PoRate = Convert.ToDouble(lblPoRate.Text.Trim());
                        var PoAmt = Convert.ToDecimal(PoQty * PoRate);

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

                        if (totPoQty > 0)
                        {
                            taPoDet.InsertPoDet("PO", "SPO", nextHdrRefNo, (short)Lno, "", 0, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(),
                                        hfStoreRef.Value.ToString(), "", txtMprRef.Text.Trim(), 1, "", DateTime.Now, DateTime.Now,
                                        PoQty, 0, PoQty, 0, "O", "N", Convert.ToDecimal(PoRate), PoAmt, PoAmt,
                                        "Posted. By:- " + empName + " @" + DateTime.Now.ToString(), "", "H", 1);
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
                    nextHdrRef = ddlPoList.SelectedValue.ToString();
                    nextHdrRefNo = ddlPoList.SelectedValue.ToString();

                    taPoHdr.UpdatePoHdr(supRef.ToString(), supRef.ToString(), supAccCode.ToString(), DateTime.Now, "", "",
                                                     "", "", "", "", "", "", "", supName.ToString(), Convert.ToDecimal(totPoAmt), "H", DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00"),
                                                     Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), DateTime.Now, "", "", "", DateTime.Now,
                                                     DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00"), "", "", "", 1, "BDT", 0, "PO", "SPO", nextHdrRefNo);

                    taPoDet.DeletePoDet(ddlPoList.SelectedValue.ToString());

                    #region Insert Inventory Details
                    short Lno = 0;
                    foreach (GridViewRow gr in gvPoDet.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblStore"));

                        var lblPoQty = (Label)(gr.FindControl("lblPoQty"));
                        var lblPoRate = (Label)(gr.FindControl("lblPoRate"));

                        var PoQty = Convert.ToDouble(lblPoQty.Text.Trim());
                        var PoRate = Convert.ToDouble(lblPoRate.Text.Trim());
                        var PoAmt = Convert.ToDecimal(PoQty * PoRate);

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

                        if (totPoQty > 0)
                        {
                            taPoDet.InsertPoDet("PO", "SPO", nextHdrRefNo, (short)Lno, "", 0, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(),
                                        hfStoreRef.Value.ToString(), "", txtMprRef.Text.Trim(), 1, "", DateTime.Now, DateTime.Now,
                                        PoQty, 0, PoQty, 0, "O", "N", Convert.ToDecimal(PoRate), PoAmt, PoAmt,
                                        "Posted. By:- " + empName + " @" + DateTime.Now.ToString(), "", "H", 1);
                        }
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnPrint.Enabled = true;
                }

                var curYear = DateTime.Now.Year;
                cboYear.SelectedValue = curYear.ToString();

                var curMonth = DateTime.Now.Month;
                cboMonth.SelectedValue = curMonth.ToString();

                LoadPoList();
                ddlPoList.SelectedIndex = ddlPoList.Items.IndexOf(ddlPoList.Items.FindByValue(nextHdrRefNo));

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

            var taPoHdr = new tbl_PuTr_PO_HdrTableAdapter();
            var taPoDet = new tbl_PuTr_PO_DetTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taPoHdr.Connection);

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

                var nextHdrRef = "";
                var nextHdrRefNo = "";

                taPoHdr.AttachTransaction(myTran);
                taPoDet.AttachTransaction(myTran);

                foreach (GridViewRow gr in gvPoDet.Rows)
                {
                    var lblPoQty = ((Label)(gr.FindControl("lblPoQty"))).Text.ToString();
                    totPoQty += Convert.ToDouble(lblPoQty.Trim());

                    var lblMrrAmt = ((Label)(gr.FindControl("lblPoAmt"))).Text.ToString();
                    totPoAmt += Convert.ToDouble(lblMrrAmt.Trim());
                }

                
                var empId = "";
                var empName = "";
                var empDesig = "";
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt32(empRef));
                if (dtEmp.Rows.Count > 0)
                {
                    empId = dtEmp[0].EmpId.ToString();
                    empName = dtEmp[0].EmpName.ToString();
                    empDesig = dtEmp[0].DesigName.ToString();
                }

                if (hfEditStatus.Value == "N")
                {
                    //PO Header Ref                   
                    var dtMaxHdrRef = taPoHdr.GetMaxPoRef("SPO", DateTime.Now.Year);
                    nextHdrRef = dtMaxHdrRef == null ? "1" : (Convert.ToInt32(dtMaxHdrRef) + 1).ToString();
                    nextHdrRefNo = "SPO-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRef).ToString("000000");

                    taPoHdr.InsertPoHdr("PO", "SPO", nextHdrRefNo, supRef.ToString(), supRef.ToString(), supAccCode.ToString(), DateTime.Now, "", "",
                                    "", "", "", "", "", "", "", supName.ToString(), Convert.ToDecimal(totPoAmt), "P", DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00"),
                                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), DateTime.Now, "", "", "", DateTime.Now,
                                    DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00"), "", "", "", 1, "BDT", 0);

                    #region Insert Inventory Details
                    short Lno = 0;
                    foreach (GridViewRow gr in gvPoDet.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblStore"));

                        var lblPoQty = (Label)(gr.FindControl("lblPoQty"));
                        var lblPoRate = (Label)(gr.FindControl("lblPoRate"));

                        var PoQty = Convert.ToDouble(lblPoQty.Text.Trim());
                        var PoRate = Convert.ToDouble(lblPoRate.Text.Trim());
                        var PoAmt = Convert.ToDecimal(PoQty * PoRate);

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

                        if (totPoQty > 0)
                        {
                            taPoDet.InsertPoDet("PO", "SPO", nextHdrRefNo, (short)Lno, "", 0, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(),
                                        hfStoreRef.Value.ToString(), "", txtMprRef.Text.Trim(), 1, "", DateTime.Now, DateTime.Now,
                                        PoQty, 0, PoQty, 0, "O", "N", Convert.ToDecimal(PoRate), PoAmt, PoAmt,
                                        "Posted. By:- " + empName + " @" + DateTime.Now.ToString(), "", "P", 1);
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
                    nextHdrRef = ddlPoList.SelectedValue.ToString();
                    nextHdrRefNo = ddlPoList.SelectedValue.ToString();

                    taPoHdr.UpdatePoHdr(supRef.ToString(), supRef.ToString(), supAccCode.ToString(), DateTime.Now, "", "",
                                    "", "", "", "", "", "", "", supName.ToString(), Convert.ToDecimal(totPoAmt), "P", DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00"),
                                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), DateTime.Now, "", "", "", DateTime.Now,
                                    DateTime.Now.Year + "/" + DateTime.Now.Month.ToString("00"), "", "", "", 1, "BDT", 0, "PO", "SPO", nextHdrRefNo);

                    taPoDet.DeletePoDet(ddlPoList.SelectedValue.ToString());

                    #region Insert Inventory Details
                    short Lno = 0;
                    foreach (GridViewRow gr in gvPoDet.Rows)
                    {
                        var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                        var hfStoreRef = (HiddenField)(gr.FindControl("hfStoreRef"));
                        var lblStoreName = (Label)(gr.FindControl("lblStore"));

                        var lblPoQty = (Label)(gr.FindControl("lblPoQty"));
                        var lblPoRate = (Label)(gr.FindControl("lblPoRate"));

                        var PoQty = Convert.ToDouble(lblPoQty.Text.Trim());
                        var PoRate = Convert.ToDouble(lblPoRate.Text.Trim());
                        var PoAmt = Convert.ToDecimal(PoQty * PoRate);

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

                        if (totPoQty > 0)
                        {
                            taPoDet.InsertPoDet("PO", "SPO", nextHdrRefNo, (short)Lno, "", 0, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(),
                                        hfStoreRef.Value.ToString(), "", txtMprRef.Text.Trim(), 1, "", DateTime.Now, DateTime.Now,
                                        PoQty, 0, PoQty, 0, "O", "N", Convert.ToDecimal(PoRate), PoAmt, PoAmt,
                                        "Posted. By:- " + empName + " @" + DateTime.Now.ToString(), "", "P", 1);
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

                var curYear = DateTime.Now.Year;
                cboYear.SelectedValue = curYear.ToString();

                var curMonth = DateTime.Now.Month;
                cboMonth.SelectedValue = curMonth.ToString();

                LoadPoList();
                ddlPoList.SelectedIndex = ddlPoList.Items.IndexOf(ddlPoList.Items.FindByValue(nextHdrRefNo));

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
            var url = "frmPoPrintSPO.aspx?PO_REF=" + ddlPoList.SelectedValue.ToString();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }        

        protected void gvPoDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var dt = new DataTable();
            dt = (DataTable)ViewState["dtPoDet"];

            dt.Rows[rowNum].Delete();
            dt.AcceptChanges();

            gvPoDet.EditIndex = -1;
            SetPoDetGridData();

            foreach (GridViewRow gr in gvPoDet.Rows)
            {
                var lblPoQty = ((Label)(gr.FindControl("lblPoQty"))).Text.ToString();
                totPoQty += Convert.ToDouble(lblPoQty.Trim());

                var lblMrrAmt = ((Label)(gr.FindControl("lblPoAmt"))).Text.ToString();
                totPoAmt += Convert.ToDouble(lblMrrAmt.Trim());
            }

            if (gvPoDet.Rows.Count > 0)
            {
                lblTotPoItem.Text = gvPoDet.Rows.Count.ToString();
                //lblTotPoItem.Text = totPoQty.ToString("N2");
                lblTotPoVal.Text = totPoAmt.ToString("N2");
                lblTotPoItem.Visible = true;
                lblTotPoVal.Visible = true;
                btnPost.Enabled = true;
                btnHold.Enabled = true;
            }
            else
            {
                lblTotPoItem.Text = "0";
                lblTotPoVal.Text = "0.00";
                lblTotPoItem.Visible = false;
                lblTotPoVal.Visible = false;
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

        protected void ddlPoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taPoHdr = new tbl_PuTr_PO_HdrTableAdapter();
            var taPoDet = new tbl_PuTr_PO_DetTableAdapter();
            try
            {
                //reportInfo();

                if (ddlPoList.SelectedIndex == 0)
                {
                    ClearData();
                }
                else
                {
                    var dtPoHdr = taPoHdr.GetDataByHdrRef(ddlPoList.SelectedValue.ToString());
                    if (dtPoHdr.Rows.Count > 0)
                    {
                        btnPrint.Enabled = true;

                        txtMprRef.Text = "";
                        txtCarryCharge.Text = "";
                        txtSup.Text = "";
                        txtPoQty.Text = "";
                        txtPoRate.Text = "";
                        txtPoAmt.Text = "";
                        ddlItemUom.SelectedIndex = 0;
                        ddlStore.SelectedIndex = 0;

                        hfEditStatus.Value = "Y";

                        txtPoDate.Text = dtPoHdr[0].PO_Hdr_DATE.ToString("dd/MM/yyyy");
                        //txtMprRef.Text = dtPoHdr[0].Trn_Hdr_Tran_Ref.ToString();
                        //txtCarryCharge.Text = dtPoHdr[0].PO_Hdr_Com10.ToString();

                        var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();
                        var dtSupAdr = taSupAdr.GetDataBySupAdrRef(dtPoHdr[0].PO_Hdr_Pcode.ToString());
                        if (dtSupAdr.Rows.Count > 0)
                            txtSup.Text = dtSupAdr[0].Par_Adr_Ref.ToString() + ":" + dtSupAdr[0].Par_Adr_Ref_No.ToString() + ":" + dtSupAdr[0].Par_Adr_Name.ToString();
                        else
                            txtSup.Text = "";

                        LoadInitPoDetGridData();
                        SetPoDetGridData();
                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtPoDet"];
                        var dtPoDet = taPoDet.GetDataByPoDetRef(ddlPoList.SelectedValue.ToString());
                        foreach (dsProcTran.tbl_PuTr_PO_DetRow dr in dtPoDet.Rows)
                        {
                            dt.Rows.Add(dr.PO_Det_Ref, dr.PO_Det_Ref, dr.PO_Det_Ref, dr.PO_Det_Icode, dr.PO_Det_Itm_Desc, dr.PO_Det_Itm_Uom, dr.PO_Det_Itm_Uom,
                                dr.PO_Det_Str_Code, dr.PO_Det_Str_Code, dr.PO_Det_Lin_Qty, dr.PO_Det_Lin_Rat.ToString("N2"), dr.PO_Det_Lin_Net);
                        }
                        ViewState["dtPoDet"] = dt;
                        SetPoDetGridData();

                        foreach (GridViewRow gr in gvPoDet.Rows)
                        {
                            var lblPoQty = ((Label)(gr.FindControl("lblPoQty"))).Text.ToString();
                            totPoQty += Convert.ToDouble(lblPoQty.Trim());

                            var lblMrrAmt = ((Label)(gr.FindControl("lblPoAmt"))).Text.ToString();
                            totPoAmt += Convert.ToDouble(lblMrrAmt.Trim());
                        }

                        if (gvPoDet.Rows.Count > 0)
                        {
                            lblTotPoItem.Text = gvPoDet.Rows.Count.ToString();
                            lblTotPoVal.Text = totPoAmt.ToString("N2");
                            lblTotPoItem.Visible = true;
                            lblTotPoVal.Visible = true;
                            btnPost.Enabled = true;
                            btnHold.Enabled = true;
                        }
                        else
                        {
                            lblTotPoItem.Text = "0";
                            lblTotPoVal.Text = "0.00";
                            lblTotPoItem.Visible = false;
                            lblTotPoVal.Visible = false;
                            btnPost.Enabled = false;
                            btnHold.Enabled = false;
                        }

                        if (dtPoHdr[0].PO_Hdr_HPC_Flag == "P")
                        {
                            btnHold.Enabled = false;
                            btnPost.Enabled = false;
                            gvPoDet.Enabled = false;
                            btnAddPoDet.Enabled = false;
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
                tbl_PuTr_PO_HdrTableAdapter det = new tbl_PuTr_PO_HdrTableAdapter();
                dsProcTran.tbl_PuTr_PO_HdrDataTable dt = new dsProcTran.tbl_PuTr_PO_HdrDataTable();

                ListItem lst;

                if (cboMonth.SelectedIndex == 0)
                    dt = det.GetDataByPoListByYear("PO", "SPO", Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                else
                    dt = det.GetDataByPoListByYearMonth("PO", "SPO", Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));

                ddlPoList.Items.Clear();
                foreach (dsProcTran.tbl_PuTr_PO_HdrRow dr in dt.Rows)
                {
                    lst = new ListItem();
                    lst.Text = dr.PO_Hdr_Ref.ToString() + ":" + dr.PO_Hdr_Com10.ToString() + ":   [" + dr.PO_Hdr_DATE.ToString("dd/MM/yyyy") + "]";
                    lst.Value = dr.PO_Hdr_Ref.ToString();
                    ddlPoList.Items.Add(lst);
                }
                ddlPoList.Items.Insert(0, new ListItem("-----New-----", "0"));


                var taPoHdr = new tbl_PuTr_PO_HdrTableAdapter();
                var dtMaxHdrRef = taPoHdr.GetMaxPoRef("SPO", DateTime.Now.Year);
                var nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;
                var nextHdrRefNo = "SPO-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRef).ToString("000000");
                lblPoRefNo.Text = nextHdrRefNo;                

                txtPoDate.Text = DateTime.Now.ToString("dd/MM/yyy");
                txtMprRef.Text = "";
                txtCarryCharge.Text = "";
                //txtSup.Text = "";

                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                if (empRef == "000011" || empRef == "000442")//--------Tarikul-SCM
                {
                    txtSup.Text = "";
                    txtSup.Enabled = true;
                }
                else
                {
                    txtSup.Text = "10.100281:SUP-10.100281:Cash Party - Factory";
                    txtSup.Enabled = false;
                }

                txtPoQty.Text = "";
                txtPoRate.Text = "";
                txtPoAmt.Text = "";
                txtItemName.Text = "";
                
                ddlItemUom.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;

                lblTotPoItem.Text = "0.00";
                lblTotPoVal.Text = "0.00";
                btnAddPoDet.Enabled = true;
                btnPost.Enabled = false;
                btnHold.Enabled = false;
                btnPrint.Enabled = false;

                hfEditStatus.Value = "N";
                hfRefNo.Value = "0";

                LoadInitPoDetGridData();
                SetPoDetGridData();
                gvPoDet.Enabled = true;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtenderSrchItem.ContextKey = ddlItemType.SelectedValue.ToString();
            txtItemName.Text = "";
            ddlItemUom.SelectedIndex = 0;
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPoList();
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPoList();
        }

        private void LoadPoList()
        {
            ClearData();

            tbl_PuTr_PO_HdrTableAdapter det = new tbl_PuTr_PO_HdrTableAdapter();
            dsProcTran.tbl_PuTr_PO_HdrDataTable dt = new dsProcTran.tbl_PuTr_PO_HdrDataTable();
            
            ListItem lst;

            if (cboMonth.SelectedIndex == 0)
                dt = det.GetDataByPoListByYear("PO", "SPO", Convert.ToDecimal(cboYear.SelectedValue.ToString()));
            else
                dt = det.GetDataByPoListByYearMonth("PO", "SPO", Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));

            ddlPoList.Items.Clear();
            foreach (dsProcTran.tbl_PuTr_PO_HdrRow dr in dt.Rows)
            {
                lst = new ListItem();
                lst.Text = dr.PO_Hdr_Ref.ToString() + ":" + dr.PO_Hdr_Com10.ToString() + ":   [" + dr.PO_Hdr_DATE.ToString("dd/MM/yyyy") + "]";
                lst.Value = dr.PO_Hdr_Ref.ToString();
                ddlPoList.Items.Add(lst);
            }
            ddlPoList.Items.Insert(0, new ListItem("-----New-----", "0"));
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
                        {
                            ddlItemUom.SelectedValue = dtItem[0].IsItm_Det_Stk_UnitNull() ? "0" : dtItem[0].Itm_Det_Stk_Unit.ToString();
                            ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dtItem[0].Itm_Det_Ext_Data2.ToString()));
                        }
                        else
                        {
                            ddlItemUom.SelectedIndex = 0;
                            ddlStore.SelectedIndex = 0;
                        }
                    }
                    else                    
                        ddlItemUom.SelectedIndex = 0;                    
                }
                else                
                    ddlItemUom.SelectedIndex = 0;                                
            }
            catch (Exception ex)
            {
                ddlItemUom.SelectedIndex = 0;                
            }
        }
    }
}