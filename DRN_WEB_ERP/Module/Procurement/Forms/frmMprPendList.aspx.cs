using System;
using System.IO;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmMprPendList : System.Web.UI.Page
    {
        tbl_Qtn_DetTableAdapter quo = new tbl_Qtn_DetTableAdapter();
        dsProcTran.tbl_Qtn_DetDataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            tblmaster.BgColor = "#f0f8ff";
            //tbltooltip2.Visible = false;

            if (Page.IsPostBack) return;

            AutoCompleteExtenderSrchItem.ContextKey = "0";

            var taItemType = new tbl_InMa_TypeTableAdapter();
            var dtItemType = taItemType.GetDataByAsc();
            cboItemType.DataSource = dtItemType;
            cboItemType.DataTextField = "Item_Type_Name";
            cboItemType.DataValueField = "Item_Type_Code";
            cboItemType.DataBind();
            cboItemType.Items.Insert(0, new ListItem("-----Select-----", "0"));  

            var curYear = DateTime.Now.Year;
            for (Int64 year = 2014; year <= (curYear); year++)
            {
                cboYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            cboYear.Items.Insert(0, new ListItem("---Select---", "0"));

            cboMonth.Items.Insert(0, new ListItem("---Select---", "0"));

            GetPendMprList();
            get_pending();
        }

        private void GetPendMprList()
        {
            View_PuTr_Pr_Hdr_DetTableAdapter srdet = new View_PuTr_Pr_Hdr_DetTableAdapter();
            dsProcTran.View_PuTr_Pr_Hdr_DetDataTable dtdet = new dsProcTran.View_PuTr_Pr_Hdr_DetDataTable();

            int cnt;

            dtdet = srdet.GetDataByQtnStatus("TEN", "P");

            cnt = dtdet.Rows.Count;

            GetMprDetailsData(dtdet);
        }

        private void GetMprDetailsData(dsProcTran.View_PuTr_Pr_Hdr_DetDataTable dtdet)
        {
            DataTable dt = new DataTable();

            //int qcnt;

            dt.Columns.Add("MPR", typeof(string));            
            dt.Columns.Add("ICODE", typeof(string));
            dt.Columns.Add("IDET", typeof(string));
            dt.Columns.Add("QTY", typeof(double));
            dt.Columns.Add("UOM", typeof(string));
            dt.Columns.Add("SPECIFICATION", typeof(string));
            dt.Columns.Add("BRAND", typeof(string));
            dt.Columns.Add("ORIGIN", typeof(string));
            dt.Columns.Add("PACKING", typeof(string));
            dt.Columns.Add("ETR", typeof(string));
            dt.Columns.Add("REMARKS", typeof(string));
            dt.Columns.Add("STAT", typeof(string));

            foreach (dsProcTran.View_PuTr_Pr_Hdr_DetRow dr in dtdet.Rows)
            {
                //qcnt = quo.GetActiveQuot("", dr.Pr_Det_Icode.ToString(), dr.Pr_Det_Ref.ToString()).Rows.Count;

                dt.Rows.Add(dr.Pr_Det_Ref, dr.Pr_Det_Icode, dr.Pr_Det_Itm_Desc, dr.Pr_Det_Lin_Qty, dr.Pr_Det_Itm_Uom, dr.Pr_Det_Spec, dr.Pr_Det_Brand, dr.Pr_Det_Origin, dr.Pr_Det_Packing, Convert.ToDateTime(dr.Pr_Det_Exp_Dat).ToString("dd/MM/yyyy"), dr.Pr_Det_Rem, dr.Pr_Det_Scm_Com);
            }

            gdItem.DataSource = dt;
            gdItem.DataBind();

            ViewState["ViewStateDataTable"] = dt;
        }
        
        protected void gdItem_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["ViewStateSortExpression"] = e.SortExpression;
            AddSortImage(gdItem.HeaderRow);
        }

        protected void gdItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Sort"))
            {
                DataTable dttemp = new DataTable();
                dttemp = (DataTable)ViewState["ViewStateDataTable"];


                if (ViewState["ViewStateSortDirection"] != null)
                    if ((SortDirection)ViewState["ViewStateSortDirection"] == SortDirection.Descending)
                    {

                        dttemp.DefaultView.Sort = e.CommandArgument + "  ASC";
                        ViewState["ViewStateSortDirection"] = SortDirection.Ascending;
                    }
                    else
                    {
                        dttemp.DefaultView.Sort = e.CommandArgument + "  DESC";
                        ViewState["ViewStateSortDirection"] = SortDirection.Descending;
                    }
                else
                {
                    dttemp.DefaultView.Sort = e.CommandArgument + "  ASC";
                    ViewState["ViewStateSortDirection"] = SortDirection.Ascending;
                }

                gdItem.DataSource = dttemp;
                gdItem.DataBind();
            }
        }

        private void AddSortImage(GridViewRow headerRow)
        {
            if (ViewState["ViewStateSortExpression"] == null) return;

            int columnIndex = 1;
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["ViewStateDataTable"];
            if (dt == null) return;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].Caption == ViewState["ViewStateSortExpression"].ToString())
                {
                    columnIndex = i;
                }
            }

            Image sortImage = new Image();

            if (ViewState["ViewStateSortDirection"] == null) return;

            if ((SortDirection)ViewState["ViewStateSortDirection"] == SortDirection.Ascending)
                sortImage.ImageUrl = "~/Image/group_arrow_top.gif";
            else
                sortImage.ImageUrl = "~/Image/group_arrow_bottom.gif";

            headerRow.Cells[columnIndex + 2].Controls.Add(sortImage);
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboMonth.Items.Clear();
            if (cboYear.SelectedIndex != 0)
            {
                var curMonth = DateTime.Now.Month;
                for (int month = 1; month <= 12; month++)
                {
                    var monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                    cboMonth.Items.Add(new ListItem(monthName.ToString(), month.ToString()));
                }                
            }
            cboMonth.Items.Insert(0, new ListItem("---Select---", "0"));            

            get_pending();
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_pending();
        }

        private void get_pending()
        {
            if (optPendMprList.SelectedValue != "1") return;

            View_PuTr_Pr_Hdr_DetTableAdapter srdet = new View_PuTr_Pr_Hdr_DetTableAdapter();
            dsProcTran.View_PuTr_Pr_Hdr_DetDataTable dtdet = new dsProcTran.View_PuTr_Pr_Hdr_DetDataTable();

            int cnt;

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
                }
            }

            dtdet = srdet.GetDataByPendMprByMprRef("TEN", "P", txtMpr.Text.Trim());
            if (dtdet.Rows.Count <= 0)
            {
                if (cboItemType.SelectedIndex != 0)
                {
                    if (itemRef.Trim().ToString() != "")
                        if (cboYear.SelectedIndex != 0)
                            if (cboMonth.SelectedIndex != 0)
                                dtdet = srdet.GetDataByPendMprByItemYearMonth("TEN", "P", itemRef.Trim().ToString(), Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                            else
                                dtdet = srdet.GetDataByPendMprByItemYear("TEN", "P", itemRef.Trim().ToString(), Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                        else
                            dtdet = srdet.GetDataByPendMprByItemRef("TEN", "P", itemRef.Trim().ToString());
                    else
                        if (cboYear.SelectedIndex != 0)
                            if (cboMonth.SelectedIndex != 0)
                                dtdet = srdet.GetDataByPendMprByItemTypeYearMonth("TEN", "P", cboItemType.SelectedValue.ToString(), Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                            else
                                dtdet = srdet.GetDataByPendMprByItemTypeYear("TEN", "P", cboItemType.SelectedValue.ToString(), Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                        else
                            dtdet = srdet.GetDataByPendMprByItemType("TEN", "P", cboItemType.SelectedValue.ToString());
                }
                else
                {
                    if (itemRef.Trim().ToString() != "")
                        if (cboYear.SelectedIndex != 0)
                            if (cboMonth.SelectedIndex != 0)
                                dtdet = srdet.GetDataByPendMprByItemYearMonth("TEN", "P", itemRef.Trim().ToString(), Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                            else
                                dtdet = srdet.GetDataByPendMprByItemYear("TEN", "P", itemRef.Trim().ToString(), Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                        else
                            dtdet = srdet.GetDataByPendMprByItemRef("TEN", "P", itemRef.Trim().ToString());
                    else
                        if (cboYear.SelectedIndex != 0)
                            if (cboMonth.SelectedIndex != 0)
                                dtdet = srdet.GetDataByPendMprByYearMonth("TEN", "P", Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                            else
                                dtdet = srdet.GetDataByPendMprByYear("TEN", "P", Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                        //else
                        //    dtdet = srdet.GetDataByQtnStatus("TEN", "P");
                }
            }
            cnt = dtdet.Rows.Count;

            GetMprDetailsData(dtdet);
        }

        protected void cboItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var dtItem = new dsInvMas.tbl_InMa_Item_DetDataTable();

            try
            {
                AutoCompleteExtenderSrchItem.ContextKey = cboItemType.SelectedValue.ToString();
                txtItemName.Text = "";

                get_pending();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnMprSort_Click(object sender, EventArgs e)
        {
            get_pending();
        }

        protected void txtItemName_TextChanged(object sender, EventArgs e)
        {
            if (optPendMprList.SelectedValue != "1") return;
            get_pending();
        }

        protected void txtMpr_TextChanged(object sender, EventArgs e)
        {
            if (optPendMprList.SelectedValue != "1") return;
            get_pending();
        }

        protected void optPendMprList_SelectedIndexChanged(object sender, EventArgs e)
        {
            View_PuTr_Pr_Hdr_DetTableAdapter srdet = new View_PuTr_Pr_Hdr_DetTableAdapter();
            dsProcTran.View_PuTr_Pr_Hdr_DetDataTable dtdet = new dsProcTran.View_PuTr_Pr_Hdr_DetDataTable();

            if (optPendMprList.SelectedValue == "1")
            {
                get_pending();
                btnMprSort.Enabled = true;
            }
            if (optPendMprList.SelectedValue == "2")
            {
                btnMprSort.Enabled = false;
                dtdet = srdet.GetDataByQtnStatus("TEN", "P");
                GetMprDetailsData(dtdet);
            }
        }
    }
}