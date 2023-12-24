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



using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
//using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;


namespace DRN_WEB_ERP.Module.Inventory.Forms
{
    public partial class frmItemSpecEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taItemSpec = new tbl_InMa_Item_SpecTableAdapter();
            var dtMaxItemSpecRef = taItemSpec.GetMaxItemSpecRef();
            var nextItemRef = dtMaxItemSpecRef == null ? 100001 : Convert.ToInt32(dtMaxItemSpecRef) + 1;
            txtItemSpecRefNo.Text = "77." + Convert.ToInt32(nextItemRef).ToString("000000");

            var taItemType = new tbl_InMa_TypeTableAdapter();
            var dtItemType = taItemType.GetDataByAsc();
            cboItemType.DataSource = dtItemType;
            cboItemType.DataTextField = "Item_Type_Name";
            cboItemType.DataValueField = "Item_Type_Code";
            cboItemType.DataBind();
            cboItemType.Items.Insert(0, new ListItem("-----Select-----", "0"));

            cboItemName.Items.Insert(0, new ListItem("----------Select----------", "0"));

            var dtItemTypeSrch = taItemType.GetDataByAsc();
            cboItemTypeSrch.DataSource = dtItemTypeSrch;
            cboItemTypeSrch.DataTextField = "Item_Type_Name";
            cboItemTypeSrch.DataValueField = "Item_Type_Code";
            cboItemTypeSrch.DataBind();
            cboItemTypeSrch.Items.Insert(0, new ListItem("-----All-----", "0"));                       
            
            cboItem.Items.Insert(0, new ListItem("----------All----------", "0"));

            btnSave.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";

            var taLoadItemSpec = new View_InMa_Item_SpecTableAdapter();
            var dtItemSpec = taLoadItemSpec.GetDataBySortAsc();
            Session["data"] = dtItemSpec;
            SetItemSpecGridData();
        }

        protected void SetItemSpecGridData()
        {
            //var taItem = new tbl_InMa_Item_DetTableAdapter();
            //var dtItem = taItem.GetData();
            var dtItem = Session["data"];
            gvItemSpec.DataSource = dtItem;
            gvItemSpec.DataBind();
            gvItemSpec.SelectedIndex = -1;
        }

        protected void gvItemSpec_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItemSpec.PageIndex = e.NewPageIndex;
            SetItemSpecGridData();
        }

        protected void gvItemSpec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvItemSpec_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvItemSpec.SelectedIndex;

            if (indx != -1)
            {
                var taItemSpec = new tbl_InMa_Item_SpecTableAdapter();

                try
                {
                    HiddenField hfItemSpecRef = (HiddenField)gvItemSpec.Rows[indx].FindControl("hfItemSpecRef");
                    hfRefNo.Value = hfItemSpecRef.Value.ToString();
                    hfEditStatus.Value = "Y";
                    btnSave.Text = "Edit";

                    var dtItemSpec = taItemSpec.GetDataBySpecRef(hfRefNo.Value.ToString());
                    if (dtItemSpec.Rows.Count > 0)
                    {
                        txtItemSpecRefNo.Text = dtItemSpec[0].Spec_Ref.ToString();
                        txtISpecName.Text = dtItemSpec[0].Spec_Name.ToString();

                        var taItem = new tbl_InMa_Item_DetTableAdapter();
                        var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(dtItemSpec[0].Spec_Item_Ref.ToString()));
                        cboItemType.SelectedValue = dtItem[0].Itm_Det_Type.ToString();

                        cboItemName.Items.Clear();

                        var dtSaleItem = taItem.GetDataByItemType(dtItem[0].Itm_Det_Type.ToString());
                        cboItemName.DataSource = dtSaleItem;
                        cboItemName.DataTextField = "Itm_Det_Desc";
                        cboItemName.DataValueField = "Itm_Det_Ref";
                        cboItemName.DataBind();
                        cboItemName.Items.Insert(0, new ListItem("----------Select----------", "0"));
                        cboItemName.SelectedValue = dtItemSpec[0].Spec_Item_Ref.ToString();

                        optListItemSpecStatus.SelectedValue = dtItemSpec[0].Spec_Status.ToString();
                    }
                }
                catch (Exception ex)
                {
                    ClearData();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void gvItemSpec_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortexp = e.SortExpression;
            Session["sortexp"] = sortexp;
            if (Session["sortDirection"] != null && Session["sortDirection"].ToString() == SortDirection.Descending.ToString())
            {
                Session["sortDirection"] = SortDirection.Ascending;
                ConvertSortDirection(sortexp, "ASC");
            }
            else
            {
                Session["sortDirection"] = SortDirection.Descending;
                ConvertSortDirection(sortexp, "DESC");
            }
        }

        private void ConvertSortDirection(string soreExpression, string p)
        {
            DataTable dataTable = Session["data"] as DataTable;
            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = soreExpression + " " + p;
                gvItemSpec.DataSource = dataView;
                gvItemSpec.DataBind();
            }
        }

        private void ClearData()
        {
            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var dtMaxItemRef = taItem.GetMaxtItemRef();
            var nextItemRef = dtMaxItemRef == null ? 100001 : Convert.ToInt32(dtMaxItemRef) + 1;
            txtItemSpecRefNo.Text = "77." + Convert.ToInt32(nextItemRef).ToString("000000");

            txtItemSpecRefNo.Text = "";
            txtISpecName.Text = "";

            cboItemType.SelectedIndex = 0;
            cboItemName.Items.Clear();
            cboItemName.Items.Insert(0, new ListItem("----------Select----------", "0"));

            optListItemSpecStatus.SelectedValue = "1";

            btnSave.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";
            gvItemSpec.SelectedIndex = -1;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var taItemSpec = new tbl_InMa_Item_SpecTableAdapter();

            try
            {
                var nextItemSpecRef = 0;
                var nextItemSpecRefNo = "";

                if (hfEditStatus.Value == "N")
                {
                    var dtMaxItemRef = taItemSpec.GetMaxItemSpecRef();
                    nextItemSpecRef = dtMaxItemRef == null ? 100001 : Convert.ToInt32(dtMaxItemRef) + 1;
                    nextItemSpecRefNo = "77." + Convert.ToInt32(nextItemSpecRef).ToString("000000");

                    taItemSpec.InsertItemSpec(nextItemSpecRefNo, "SPEC-" + nextItemSpecRefNo, txtISpecName.Text.Trim(), cboItemName.SelectedValue,
                                DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), optListItemSpecStatus.SelectedValue.ToString(), "");

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnSave.Text = "Save";
                    hfEditStatus.Value = "N";
                    hfRefNo.Value = "0";
                    SetItemSpecGridData();
                    ClearData();
                    txtSearch.Text = "";
                    btnClearSrch.Visible = false;
                }
                else
                {
                    nextItemSpecRefNo = hfRefNo.Value;

                    var dttItemSpec = taItemSpec.GetDataBySpecRef(nextItemSpecRefNo);
                    if (dttItemSpec.Rows.Count > 0)
                    {
                        taItemSpec.UpdateItemSpec("SPEC-" + nextItemSpecRefNo, txtISpecName.Text.Trim(), cboItemName.SelectedValue,
                                DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                optListItemSpecStatus.SelectedValue.ToString(), "", nextItemSpecRefNo);
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item Specification.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnSave.Text = "Save";
                    hfEditStatus.Value = "N";
                    hfRefNo.Value = "0";
                    SetItemSpecGridData();
                    ClearData();
                    txtSearch.Text = "";
                    btnClearSrch.Visible = false;
                }

                cboItem.Items.Clear();
                cboItem.Items.Insert(0, new ListItem("----------All----------", "0"));

                cboItemTypeSrch.SelectedIndex = 0;

                var taLoadItemSpec = new View_InMa_Item_SpecTableAdapter();
                var dtLoadItem = taLoadItemSpec.GetDataBySortAsc();
                Session["data"] = dtLoadItem;
                SetItemSpecGridData();
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
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {            
            var taItemSpec = new View_InMa_Item_SpecTableAdapter();

            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                var itemSpecRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    itemSpecRef = word;
                    break;
                }

                if (itemSpecRef.Length > 0)
                {
                    var dtItemSpec = taItemSpec.GetDataByItemSpecRef(itemSpecRef);
                    if (dtItemSpec.Rows.Count > 0)
                    {
                        Session["data"] = dtItemSpec;
                        SetItemSpecGridData();
                        btnClearSrch.Visible = true;
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "No Data Found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
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

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            var taLoadItemSpec = new View_InMa_Item_SpecTableAdapter();
            var dtItemSpec = taLoadItemSpec.GetDataBySortAsc();
            Session["data"] = dtItemSpec;
            SetItemSpecGridData();
            btnClearSrch.Visible = false;
        }

        protected void cboItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taItem = new tbl_InMa_Item_DetTableAdapter();

            try
            {
                cboItemName.Items.Clear();

                if (cboItemType.SelectedIndex == 0)
                {                    
                    var dtSaleItem = taItem.GetDataBySortAsc();
                    cboItemName.DataSource = dtSaleItem;
                    cboItemName.DataTextField = "Itm_Det_Desc";
                    cboItemName.DataValueField = "Itm_Det_Ref";
                    cboItemName.DataBind();
                    cboItemName.Items.Insert(0, new ListItem("----------Select----------", "0"));
                }
                else
                {
                    var dtSaleItem = taItem.GetDataByItemType(cboItemType.SelectedValue.ToString());
                    cboItemName.DataSource = dtSaleItem;
                    cboItemName.DataTextField = "Itm_Det_Desc";
                    cboItemName.DataValueField = "Itm_Det_Ref";
                    cboItemName.DataBind();
                    cboItemName.Items.Insert(0, new ListItem("----------Select----------", "0"));
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void cboItemTypeSrch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var taItemSpec = new View_InMa_Item_SpecTableAdapter();

            try
            {
                if (cboItemTypeSrch.SelectedIndex == 0)
                {
                    cboItem.Items.Clear();

                    var dtSaleItem = taItem.GetDataBySortAsc();
                    cboItem.DataSource = dtSaleItem;
                    cboItem.DataTextField = "Itm_Det_Desc";
                    cboItem.DataValueField = "Itm_Det_Ref";
                    cboItem.DataBind();
                    cboItem.Items.Insert(0, new ListItem("----------All----------", "0"));

                    var dtItemSpec = taItemSpec.GetDataBySortAsc();
                    Session["data"] = dtItemSpec;
                    SetItemSpecGridData();
                }
                else
                {
                    cboItem.Items.Clear();

                    var dtSaleItem = taItem.GetDataByItemType(cboItemTypeSrch.SelectedValue.ToString());
                    cboItem.DataSource = dtSaleItem;
                    cboItem.DataTextField = "Itm_Det_Desc";
                    cboItem.DataValueField = "Itm_Det_Ref";
                    cboItem.DataBind();
                    cboItem.Items.Insert(0, new ListItem("----------All----------", "0"));

                    var dtItemSpec = taItemSpec.GetDataByItemType(cboItemTypeSrch.SelectedValue.ToString());
                    Session["data"] = dtItemSpec;
                    SetItemSpecGridData();
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void ddlOrdItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var taItemSpec = new View_InMa_Item_SpecTableAdapter();

            try
            {
                if (cboItem.SelectedIndex == 0)
                {
                    var dtItemSpec = taItemSpec.GetDataByItemType(cboItemTypeSrch.SelectedValue.ToString());
                    Session["data"] = dtItemSpec;
                    SetItemSpecGridData();
                }
                else
                {
                    var dtItemSpec = taItemSpec.GetDataByItemRef(cboItem.SelectedValue.ToString());
                    Session["data"] = dtItemSpec;
                    SetItemSpecGridData();
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();
            var taItemSpec = new tbl_InMa_Item_SpecTableAdapter();
            var taMecDet = new mechanicalTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taItem.Connection);

            try
            {
                taItem.AttachTransaction(myTran);
                taCoa.AttachTransaction(myTran);
                taStkCtl.AttachTransaction(myTran);
                taItemSpec.AttachTransaction(myTran);
                taMecDet.AttachTransaction(myTran);

                var taMecView = new mechanical1TableAdapter();
                var dtMecView = taMecView.GetData();
                foreach (dsInvTran.mechanical1Row dr in dtMecView.Rows)
                {
                    var nextItemRef = 0;
                    var NextItemRefNo = "";

                    var dtItem = taItem.GetDataByItemName(dr.ItemGeneric);
                    if (dtItem.Rows.Count > 0)
                    {                        
                        taMecDet.UpdateSpecItemCode(dtItem[0].Itm_Det_Ref.ToString(), dr.ItemGeneric);
                    }
                    else
                    {
                        #region Insert Item
                        var dtMaxCoaRef = taCoa.GetMaxCoaRef();
                        var nextCoaRef = dtMaxCoaRef == null ? 1 : Convert.ToInt32(dtMaxCoaRef) + 1;

                        var dtMaxCoaCode = taCoa.GetMaxCoaCode();
                        var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
                        var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");

                        taCoa.InsertCoa(nextCoaRef, nextCoaCode, dr.ItemGeneric, nextCoaCode, "I", "B", "N", DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y",
                            "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        var dtMaxItemRef = taItem.GetMaxtItemRef();
                        nextItemRef = dtMaxItemRef == null ? 100001 : Convert.ToInt32(dtMaxItemRef) + 1;
                        NextItemRefNo = "ITM-" + Convert.ToInt32(nextItemRef).ToString("000000");

                        taItem.InsertItemDet(nextItemRef, NextItemRefNo, dr.ItemGeneric, "", "", "", "PCS",
                                    "PCS", 0, "0", "0", "N", 0, "", "N", DateTime.Now,
                                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), DateTime.Now, "",
                                    nextCoaCode, "", "", "", "", "1004", "", "", "", "", "", "MEC",
                                    "Mechanical", "", "", 0, "1", "");

                        taMecDet.UpdateSpecItemCode(nextItemRef.ToString(), dr.ItemGeneric);
                        #endregion
                    }
                }
                
                var dtMecDet = taMecDet.GetData();
                foreach (dsInvTran.mechanicalRow dr in dtMecDet.Rows)
                {
                    var dtSpec=taItemSpec.GetDataBySpecName(dr.ItemSpec);
                    if (dtSpec.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        var dtMaxItemRef = taItemSpec.GetMaxItemSpecRef();
                        var nextItemSpecRef = dtMaxItemRef == null ? 100001 : Convert.ToInt32(dtMaxItemRef) + 1;
                        var nextItemSpecRefNo = "77." + Convert.ToInt32(nextItemSpecRef).ToString("000000");

                        taItemSpec.InsertItemSpec(nextItemSpecRefNo, "SPEC-" + nextItemSpecRefNo, dr.ItemSpec, dr.ItemCode,
                                    DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");
                    }
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Item Data Saved Successfully. Item Count (" + dtMecView.Rows.Count + ")";
                tblMsg.Rows[1].Cells[0].InnerText = "Spec Data Saved Successfully. Item Count (" + dtMecDet.Rows.Count + ")";
                ModalPopupExtenderMsg.Show();

                var taLoadItemSpec = new View_InMa_Item_SpecTableAdapter();
                var dtLoadItem = taLoadItemSpec.GetDataBySortAsc();
                Session["data"] = dtLoadItem;
                SetItemSpecGridData();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taItem.Connection);

            try
            {
                taItem.AttachTransaction(myTran);
                taCoa.AttachTransaction(myTran);
                taStkCtl.AttachTransaction(myTran);                

                var taItemView = new Table_Item_ImportTableAdapter();
                var dtItemView = taItemView.GetData();
                foreach (dsInvTran.Table_Item_ImportRow dr in dtItemView.Rows)
                {
                    var nextItemRef = 0;
                    var NextItemRefNo = "";

                    var dtItem = taItem.GetDataByItemName(dr.NewItemName);
                    if (dtItem.Rows.Count > 0)
                    {
                        //taMecDet.UpdateSpecItemCode(dtItem[0].Itm_Det_Ref.ToString(), dr.ItemGeneric);
                    }
                    else
                    {
                        #region Insert Item
                        var dtMaxCoaRef = taCoa.GetMaxCoaRef();
                        var nextCoaRef = dtMaxCoaRef == null ? 1 : Convert.ToInt32(dtMaxCoaRef) + 1;

                        var dtMaxCoaCode = taCoa.GetMaxCoaCode();
                        var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
                        var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");

                        taCoa.InsertCoa(nextCoaRef, nextCoaCode, dr.NewItemName, nextCoaCode, "I", "B", "N", DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y",
                            "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        var dtMaxItemRef = taItem.GetMaxtItemRef();
                        nextItemRef = dtMaxItemRef == null ? 100001 : Convert.ToInt32(dtMaxItemRef) + 1;
                        NextItemRefNo = "ITM-" + Convert.ToInt32(nextItemRef).ToString("000000");

                        taItem.InsertItemDet(nextItemRef, NextItemRefNo, dr.NewItemName, "", "", "", dr.ItemUnitCode.Trim(),
                                    dr.ItemUnitCode.Trim(), 0, "0", "0", "N", 0, "", "N", DateTime.Now,
                                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), DateTime.Now, "",
                                    nextCoaCode, "", "", "", "", "1004", "", "", "", "", "", dr.ItemType,
                                    "", "", "", 0, "1", "");

                        var dtStkCtl = taStkCtl.GetDataByStoreItem("1004", nextItemRef.ToString());
                        if (dtStkCtl.Rows.Count <= 0)
                            taStkCtl.InsertItemStore("1004", nextItemRef.ToString(), "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                DateTime.Now, DateTime.Now, "", "", "", 0);

                        //taMecDet.UpdateSpecItemCode(nextItemRef.ToString(), dr.ItemGeneric);
                        #endregion
                    }
                }                

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Item Data Saved Successfully. Item Count (" + dtItemView.Rows.Count + ")";                
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
    }
}