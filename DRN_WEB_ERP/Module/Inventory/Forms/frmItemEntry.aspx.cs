using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;

namespace DRN_WEB_ERP.Module.Inventory.Forms
{
    public partial class frmItemEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var dtMaxItemRef = taItem.GetMaxtItemRef();
            var nextItemRef = dtMaxItemRef == null ? 100001 : Convert.ToInt32(dtMaxItemRef) + 1;
            txtItemRefNo.Text = "ITM-" + Convert.ToInt32(nextItemRef).ToString("000000");

            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var dtMaxCoaCode = taCoa.GetMaxCoaCode();
            var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
            var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");
            txtItemAccGlCode.Text = nextCoaCode.ToString();

            var taItemUom = new tbl_InMa_UomTableAdapter();
            var dtItemUom = taItemUom.GetDataBySortName();
            cboItemUom.DataSource = dtItemUom;
            cboItemUom.DataTextField = "Uom_Name";
            cboItemUom.DataValueField = "Uom_Code";
            cboItemUom.DataBind();
            cboItemUom.Items.Insert(0, new ListItem("-----Select-----", "0"));

            var taItemType = new tbl_InMa_TypeTableAdapter();
            var dtItemType = taItemType.GetDataByAsc();
            cboItemType.DataSource = dtItemType;
            cboItemType.DataTextField = "Item_Type_Name";
            cboItemType.DataValueField = "Item_Type_Code";
            cboItemType.DataBind();
            cboItemType.Items.Insert(0, new ListItem("-----Select-----", "0"));

            var taStore = new tbl_InMa_Str_LocTableAdapter();
            var dtStore = taStore.GetDataBySortName();
            cboItemStore.DataSource = dtStore;
            cboItemStore.DataTextField = "Str_Loc_Name";
            cboItemStore.DataValueField = "Str_Loc_Ref";
            cboItemStore.DataBind();
            cboItemStore.Items.Insert(0, new ListItem("-----Select-----", "0"));

            var dtItemTypeSrch = taItemType.GetDataByAsc();
            cboItemTypeSrch.DataSource = dtItemTypeSrch;
            cboItemTypeSrch.DataTextField = "Item_Type_Name";
            cboItemTypeSrch.DataValueField = "Item_Type_Code";
            cboItemTypeSrch.DataBind();
            cboItemTypeSrch.Items.Insert(0, new ListItem("-----All-----", "0"));
            
            //var dtSaleItem = taItem.GetDataByItemType(cboItemType.SelectedValue.ToString());
            //cboItem.DataSource = dtSaleItem;
            //cboItem.DataTextField = "Itm_Det_Desc";
            //cboItem.DataValueField = "Itm_Det_Ref";
            //cboItem.DataBind();
            cboItem.Items.Insert(0, new ListItem("----------All----------", "0"));

            btnSave.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";

            var dtItem = taItem.GetDataBySortAsc();
            Session["data"] = dtItem;
            SetItemGridData();
        }

        protected void SetItemGridData()
        {
            //var taItem = new tbl_InMa_Item_DetTableAdapter();
            //var dtItem = taItem.GetData();
            var dtItem = Session["data"];
            gvItem.DataSource = dtItem;
            gvItem.DataBind();            
            gvItem.SelectedIndex = -1;
        }

        protected void gvItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItem.PageIndex = e.NewPageIndex;
            SetItemGridData();
        }

        protected void gvItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvItem.SelectedIndex;

            if (indx != -1)
            {
                var taItem = new tbl_InMa_Item_DetTableAdapter();

                try
                {
                    var userRef = Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString();
                    var userEmpRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                    HiddenField hfItemRef = (HiddenField)gvItem.Rows[indx].FindControl("hfItemRef");
                    hfRefNo.Value = hfItemRef.Value;
                    hfEditStatus.Value = "Y";
                    btnSave.Text = "Edit";

                    var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(hfRefNo.Value.ToString()));
                    if (dtItem.Rows.Count > 0)
                    {
                        txtItemRefNo.Text = dtItem[0].Itm_Det_Code.ToString();
                        txtItemName.Text = dtItem[0].Itm_Det_Desc.ToString();
                        txtItemDesc.Text = dtItem[0].Itm_Det_Add_Des1.ToString();
                        txtItemReOrderQty.Text = dtItem[0].Itm_Det_Exp_Level.ToString();
                        txtItemAccGlCode.Text = dtItem[0].Itm_Det_Acc_Code.ToString();

                        cboItemType.SelectedValue = dtItem[0].Itm_Det_Type.ToString();
                        cboItemUom.SelectedValue = dtItem[0].Itm_Det_Stk_Unit.ToString();

                        optListItemStatus.SelectedValue = dtItem[0].Itm_Det_Status.ToString();

                        if (userRef == "100001" || userEmpRef == "000011")
                            txtItemName.Enabled = true;
                        else
                            txtItemName.Enabled = false;

                        cboItemStore.SelectedValue = dtItem[0].Itm_Det_Ext_Data2.ToString();
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

        protected void gvItem_Sorting(object sender, GridViewSortEventArgs e)
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
                gvItem.DataSource = dataView;
                gvItem.DataBind();
            }
        }

        private void ClearData()
        {
            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var dtMaxItemRef = taItem.GetMaxtItemRef();
            var nextItemRef = dtMaxItemRef == null ? 100001 : Convert.ToInt32(dtMaxItemRef) + 1;
            txtItemRefNo.Text = "ITM-" + Convert.ToInt32(nextItemRef).ToString("000000");

            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var dtMaxCoaCode = taCoa.GetMaxCoaCode();
            var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
            var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");
            txtItemAccGlCode.Text = nextCoaCode.ToString();
            
            txtItemName.Text = "";
            txtItemDesc.Text = "";
            txtItemReOrderQty.Text = "";

            cboItemType.SelectedIndex = 0;
            cboItemUom.SelectedIndex = 0;

            optListItemStatus.SelectedValue = "1";

            btnSave.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";
            gvItem.SelectedIndex = -1;
        }

        protected void btnSave_Click(object sender, EventArgs e)
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

                var nextItemRef = 0;
                var NextItemRefNo = "";

                var itemName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtItemName.Text.Trim());

                if (hfEditStatus.Value == "N")
                {
                    #region Form Data Validation
                    var dtItemChk = taItem.GetDataByItemName(txtItemName.Text.Trim());
                    if (dtItemChk.Rows.Count > 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Item already exists with this name.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    #endregion

                    #region Insert Item
                    var dtMaxCoaRef = taCoa.GetMaxCoaRef();
                    var nextCoaRef = dtMaxCoaRef == null ? 1 : Convert.ToInt32(dtMaxCoaRef) + 1;

                    var dtMaxCoaCode = taCoa.GetMaxCoaCode();
                    var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
                    var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");

                    taCoa.InsertCoa(nextCoaRef, nextCoaCode, itemName.Trim(), nextCoaCode, "I", "B", "N", DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y",
                        "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    var dtMaxItemRef = taItem.GetMaxtItemRef();
                    nextItemRef = dtMaxItemRef == null ? 100001 : Convert.ToInt32(dtMaxItemRef) + 1;
                    NextItemRefNo = "ITM-" + Convert.ToInt32(nextItemRef).ToString("000000");

                    taItem.InsertItemDet(nextItemRef, NextItemRefNo, itemName.Trim(), txtItemDesc.Text.Trim(), "", "", cboItemUom.SelectedValue,
                                cboItemUom.SelectedValue, 0, "0", "0", "N", 0, "", "N", DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), DateTime.Now, "",
                                nextCoaCode, "", "", "", "", cboItemStore.SelectedValue.ToString(), "", "", "", "", "", cboItemType.SelectedValue.ToString(),
                                cboItemType.SelectedItem.ToString(), "", "", 0, optListItemStatus.SelectedValue, "");

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnSave.Text = "Save";
                    hfEditStatus.Value = "N";
                    hfRefNo.Value = "0";
                    SetItemGridData();
                    ClearData();
                    txtSearch.Text = "";
                    btnClearSrch.Visible = false;
                    #endregion
                }
                else
                {
                    #region Update Item
                    nextItemRef = Convert.ToInt32(hfRefNo.Value);

                    var dtItem = taItem.GetDataByItemRef(nextItemRef);
                    if (dtItem.Rows.Count > 0)
                    {
                        taItem.UpdateItemDet(dtItem[0].Itm_Det_Code, itemName.Trim(), txtItemDesc.Text.Trim(), "", "", cboItemUom.SelectedValue,
                                cboItemUom.SelectedValue, 0, "0", "0", "N", 0, "", "N", DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), DateTime.Now, "",
                                dtItem[0].Itm_Det_Acc_Code.ToString(), "", "", "", "", cboItemStore.SelectedValue.ToString(), "", "", "", "", "",
                                cboItemType.SelectedValue.ToString(), cboItemType.SelectedItem.ToString(), "", "", 0,
                                optListItemStatus.SelectedValue, "", nextItemRef);

                        var dtGlCoa = taCoa.GetDataByCoaCode(dtItem[0].Itm_Det_Acc_Code.ToString());
                        if (dtGlCoa.Rows.Count > 0)
                        {
                            taCoa.UpdateCoa(itemName.Trim(), "I", "B", "N", DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y",
                                "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "", dtItem[0].Itm_Det_Acc_Code.ToString());
                        }
                        else
                        {
                            myTran.Rollback();
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item GL Code.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item Code.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnSave.Text = "Save";
                    hfEditStatus.Value = "N";
                    hfRefNo.Value = "0";
                    SetItemGridData();
                    ClearData();
                    txtSearch.Text = "";
                    btnClearSrch.Visible = false;
                    #endregion
                }

                var dtStkCtl = taStkCtl.GetDataByStoreItem(cboItemStore.SelectedValue.ToString(), nextItemRef.ToString());
                if (dtStkCtl.Rows.Count <= 0)
                    taStkCtl.InsertItemStore(cboItemStore.SelectedValue.ToString(), nextItemRef.ToString(), "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                        DateTime.Now, DateTime.Now, "", "", "", 0);

                cboItem.Items.Clear();                
                cboItem.Items.Insert(0, new ListItem("----------All----------", "0"));

                cboItemTypeSrch.SelectedIndex = 0;
                
                var dtLoadItem = taItem.GetDataBySortAsc();
                Session["data"] = dtLoadItem;
                SetItemGridData();

                var dtMaxItemRefNew = taItem.GetMaxtItemRef();
                var nextItemRefNew = dtMaxItemRefNew == null ? 100001 : Convert.ToInt32(dtMaxItemRefNew) + 1;
                txtItemRefNo.Text = "ITM-" + Convert.ToInt32(nextItemRefNew).ToString("000000");
            }
            catch (Exception ex)
            {
                myTran.Rollback();
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
            var taItem = new tbl_InMa_Item_DetTableAdapter();

            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                var itemRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
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
                        var dtSupAdr = taItem.GetDataByItemRef(Convert.ToInt32(itemRef));
                        if (dtSupAdr.Rows.Count > 0)
                        {                            
                            Session["data"] = dtSupAdr;
                            SetItemGridData();
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
            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var dtItem = taItem.GetDataBySortAsc();
            Session["data"] = dtItem;
            SetItemGridData();
            btnClearSrch.Visible = false;
        }

        protected void cboItemTypeSrch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taItem = new tbl_InMa_Item_DetTableAdapter();

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
                    
                    var dtItem = taItem.GetDataBySortAsc();
                    Session["data"] = dtItem;
                    SetItemGridData();
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
                    
                    var dtItem = taItem.GetDataByItemType(cboItemTypeSrch.SelectedValue.ToString());
                    Session["data"] = dtItem;
                    SetItemGridData();
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

            try
            {
                if (cboItem.SelectedIndex == 0)
                {
                    var dtItem = taItem.GetDataByItemType(cboItemTypeSrch.SelectedValue.ToString());
                    Session["data"] = dtItem;
                    SetItemGridData();
                }
                else
                {
                    var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(cboItem.SelectedValue.ToString()));
                    Session["data"] = dtItem;
                    SetItemGridData();
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
            var taInvTrn = new tbl_InTr_Trn_Det1TableAdapter();
            var taInvTrnDet = new tbl_InTr_Trn_Det2TableAdapter();
            var taStat = new tbl_Test1111TableAdapter();

            try
            {
                var dtInvOpnTran = taInvTrn.GetData();
                foreach (dsInvTran.tbl_InTr_Trn_Det1Row dr in dtInvOpnTran.Rows)
                {
                    var dtInvMrrTran = taInvTrnDet.GetOpeningRate(dr.Trn_Det_Icode);
                    if (dtInvMrrTran.Rows.Count > 0)
                    {
                        taInvTrnDet.UpdateOpeningRate(dtInvMrrTran[0].Trn_Det_Lin_Rat, dr.Trn_Det_Icode);
                    }
                    else
                    {
                        taStat.InsertStatus(dr.Trn_Det_Icode, "No MRR found");
                    }
                }
            }
            catch(Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnExportExcl_Click(object sender, EventArgs e)
        {
            try
            {
                var qryStr = "";
                if (cboItemTypeSrch.SelectedIndex == 0)
                {
                    qryStr = "SELECT ROW_NUMBER() OVER(ORDER BY Itm_Det_Desc ASC) AS SL#, [Itm_Det_Ref] as [Item_Ref], [Itm_Det_Code] as [Item_Code], " +
                                "[Itm_Det_Desc] as [Item_Name], [Itm_Det_Stk_Unit] as [UOM], [Itm_Det_T_C1] as [Item_Type], [Itm_Det_Status] as [Status] " +
                                "FROM [DRN].[dbo].[View_InMa_Item_Det] order by [Itm_Det_Type],[Itm_Det_Desc]";
                }
                else
                {
                    if (cboItem.SelectedIndex == 0)
                    {
                        qryStr = "SELECT ROW_NUMBER() OVER(ORDER BY Itm_Det_Desc ASC) AS SL#, [Itm_Det_Ref] as [Item_Ref], [Itm_Det_Code] as [Item_Code], " +
                                "[Itm_Det_Desc] as [Item_Name], [Itm_Det_Stk_Unit] as [UOM], [Itm_Det_T_C1] as [Item_Type], [Itm_Det_Status] as [Status] " +
                                "FROM [DRN].[dbo].[View_InMa_Item_Det] where Itm_Det_Type='" + cboItemTypeSrch.SelectedValue.ToString() + "' " +
                                "order by [Itm_Det_Type],[Itm_Det_Desc]";
                    }
                    else
                    {
                        qryStr = "SELECT ROW_NUMBER() OVER(ORDER BY Itm_Det_Desc ASC) AS SL#, [Itm_Det_Ref] as [Item_Ref], [Itm_Det_Code] as [Item_Code], " +
                                "[Itm_Det_Desc] as [Item_Name], [Itm_Det_Stk_Unit] as [UOM], [Itm_Det_T_C1] as [Item_Type], [Itm_Det_Status] as [Status] " +
                                "FROM [DRN].[dbo].[View_InMa_Item_Det] where Itm_Det_Type='" + cboItemTypeSrch.SelectedValue.ToString() + "' and" +
                                "[Itm_Det_Ref]='" + cboItem.SelectedValue.ToString() + "' order by [Itm_Det_Type],[Itm_Det_Desc]";
                    }
                }
                
                SqlCommand cmd = new SqlCommand(qryStr);
                DataTable dt = GetData(cmd);

                if (dt.Rows.Count > 65535)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Export to Excel is not allowed due to excessive number of rows. (65535) ')", true);
                    return;
                }

                //First Method
                #region With Formating
                Response.Clear();
                Response.Buffer = true;
                string filename = String.Format("Item_List_as_on_{0}.xls", DateTime.Now.ToString("dd-MM-yy"));
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //Create a dummy GridView
                    GridView GridView1 = new GridView();
                    GridView1.AllowPaging = false;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    GridView1.HeaderRow.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in GridView1.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView1.HeaderStyle.BackColor;
                    }

                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        row.BackColor = System.Drawing.Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView1.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView1.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
                #endregion
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        private DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}