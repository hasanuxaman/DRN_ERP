using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmSupplierBillRpt : System.Web.UI.Page
    {
        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void optRptFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optRptFilter.SelectedValue == "1")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Procurement/Forms/wsAutoComProc.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchSupplier";
            }
            else if (optRptFilter.SelectedValue == "2")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Inventory/Forms/wsAutoCompInv.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchItem";
            }
            else if (optRptFilter.SelectedValue == "3")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Inventory/Forms/wsAutoCompInv.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchItemCategory";
            }
            else
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Procurement/Forms/wsAutoComProc.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchSupplier1";
            }
        }

        #region GridData
        public string GetMrrRef(string billRef)
        {
            string mrrRef = "";
            try
            {
                var taBillDet = new tbl_PuTr_Bill_DetTableAdapter();
                var dtBillDet = taBillDet.GetDataByBillRef(billRef.ToString());
                foreach (dsProcTran.tbl_PuTr_Bill_DetRow dr in dtBillDet.Rows)
                {
                    mrrRef = dr.Bill_Det_Mrr_No.ToString() + ", " + mrrRef;
                }
                return mrrRef;
            }
            catch (Exception) { return mrrRef; }
        }       
        #endregion

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            #region Get Supplier Ref
            var supRef = "";
            if (txtSearch.Text.Trim().Length > 0)
            {
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    supRef = word;
                    break;
                }

                if (supRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(supRef, out result))
                    {
                        var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();
                        var dtPartyAdr = taSupAdr.GetDataBySupAdrRef(supRef);
                        if (dtPartyAdr.Rows.Count > 0) supRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                    }
                }
            }
            #endregion

            #region Get Item Ref
            var itemRef = "";
            if (txtSearch.Text.Trim().Length > 0)
            {
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
                        var taItem = new tbl_InMa_Item_DetTableAdapter();
                        var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(itemRef));
                        if (dtItem.Rows.Count > 0) itemRef = dtItem[0].Itm_Det_Ref.ToString();
                    }
                }
            }
            #endregion

            #region Get Item Type
            var itemType = "";
            if (txtSearch.Text.Trim().Length > 0)
            {
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    itemType = word;
                    break;
                }

                if (itemType.Length > 0)
                {
                    var taItemType = new tbl_InMa_TypeTableAdapter();
                    var dtItemType = taItemType.GetDataByItemTypeCode(itemType);
                    if (dtItemType.Rows.Count > 0) itemType = dtItemType[0].Item_Type_Code.ToString();
                }
            }
            #endregion

            var taViewBillDet = new View_PuTr_Po_Bill_Det_ListTableAdapter();
            var dtViewBillDet = new dsProcTran.View_PuTr_Po_Bill_Det_ListDataTable();

            if (optRptFilter.SelectedValue == "0")
                dtViewBillDet = taViewBillDet.GetDataByDate(txtFromDate.Text.Trim(), txtToDate.Text.Trim());

            if (optRptFilter.SelectedValue == "1")
                dtViewBillDet = taViewBillDet.GetDataBySupRef(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), supRef.ToString());

            if (optRptFilter.SelectedValue == "2")
                dtViewBillDet = taViewBillDet.GetDataByItemRef(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), supRef.ToString());

            if (optRptFilter.SelectedValue == "3")
                dtViewBillDet = taViewBillDet.GetDataByItemType(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), supRef.ToString());

            if (optRptFilter.SelectedValue == "4")
                dtViewBillDet = taViewBillDet.GetDataByMrrRefNo(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), txtSearch.Text.Trim());

            if (optRptFilter.SelectedValue == "5")
                dtViewBillDet = taViewBillDet.GetDataByPoRefNo(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), txtSearch.Text.Trim());

            gvPoBill.DataSource = dtViewBillDet;
            gvPoBill.DataBind();

            btnExport.Enabled = dtViewBillDet.Rows.Count > 0;
        }

        protected void gvPoBill_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Convert the row index stored in the CommandArgument
            // property to an Integer.
            int index = Convert.ToInt32(e.CommandArgument);

            // Retrieve the row that contains the button clicked 
            // by the user from the Rows collection.
            GridViewRow row = gvPoBill.Rows[index];

            var hfBillRef = (HiddenField)(row.Cells[2].FindControl("hfBillRef"));

            #region Details
            if (e.CommandName == "Details")
            {
                var url = "frmSupplierBillView.aspx?BillRef=" + hfBillRef.Value.ToString();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
            }
            #endregion
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                #region Get Supplier Ref
                var supRef = "";
                if (txtSearch.Text.Trim().Length > 0)
                {
                    var srchWords = txtSearch.Text.Trim().Split(':');
                    foreach (string word in srchWords)
                    {
                        supRef = word;
                        break;
                    }

                    if (supRef.Length > 0)
                    {
                        int result;
                        if (int.TryParse(supRef, out result))
                        {
                            var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();
                            var dtPartyAdr = taSupAdr.GetDataBySupAdrRef(supRef);
                            if (dtPartyAdr.Rows.Count > 0) supRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                        }
                    }
                }
                #endregion

                #region Get Item Ref
                var itemRef = "";
                if (txtSearch.Text.Trim().Length > 0)
                {
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
                            var taItem = new tbl_InMa_Item_DetTableAdapter();
                            var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(itemRef));
                            if (dtItem.Rows.Count > 0) itemRef = dtItem[0].Itm_Det_Ref.ToString();
                        }
                    }
                }
                #endregion

                #region Get Item Type
                var itemType = "";
                if (txtSearch.Text.Trim().Length > 0)
                {
                    var srchWords = txtSearch.Text.Trim().Split(':');
                    foreach (string word in srchWords)
                    {
                        itemType = word;
                        break;
                    }

                    if (itemType.Length > 0)
                    {
                        var taItemType = new tbl_InMa_TypeTableAdapter();
                        var dtItemType = taItemType.GetDataByItemTypeCode(itemType);
                        if (dtItemType.Rows.Count > 0) itemType = dtItemType[0].Item_Type_Code.ToString();
                    }
                }
                #endregion

                var qryStr = "";

                if (optRptFilter.SelectedValue == "0")
                    qryStr = "SELECT Distinct Par_Adr_Name as Supplier_Name,Bill_Hdr_Ref_No as Bill_Ref_No,CONVERT(date,Bill_Hdr_Date,103) AS Bill_Date,Bill_Hdr_Party_Bill_No AS Party_Bill_No, " +
                             "Bill_Hdr_Tot_Amount as Bill_Amount,Bill_Hdr_Adj_Amount as Adjust_Amount, " +
                             "Bill_Hdr_Net_Amount as Net_Bill_Amount,Bill_Hdr_Pay_Amount as Payment_Amount,Bill_Hdr_Due_Amount AS Due_Amount " +
                             "FROM [DRN].[dbo].[View_PuTr_Po_Bill_Det] WHERE (CONVERT(date, Bill_Hdr_Date, 103) >= CONVERT(date, '" + txtFromDate.Text.Trim() + "', 103)) " +
                             "AND (CONVERT(date, Bill_Hdr_Date, 103) <= CONVERT(date, '" + txtToDate.Text.Trim() + "', 103)) ORDER BY CONVERT(date,Bill_Hdr_Date,103)";

                if (optRptFilter.SelectedValue == "1")
                    qryStr = "SELECT Distinct Par_Adr_Name as Supplier_Name,Bill_Hdr_Ref_No as Bill_Ref_No,CONVERT(date,Bill_Hdr_Date,103) AS Bill_Date,Bill_Hdr_Party_Bill_No AS Party_Bill_No, " +
                             "Bill_Hdr_Tot_Amount as Bill_Amount,Bill_Hdr_Adj_Amount as Adjust_Amount, " +
                             "Bill_Hdr_Net_Amount as Net_Bill_Amount,Bill_Hdr_Pay_Amount as Payment_Amount,Bill_Hdr_Due_Amount AS Due_Amount " +
                             "FROM [DRN].[dbo].[View_PuTr_Po_Bill_Det] WHERE (CONVERT(date, Bill_Hdr_Date, 103) >= CONVERT(date, '" + txtFromDate.Text.Trim() + "', 103)) " +
                             "AND (CONVERT(date, Bill_Hdr_Date, 103) <= CONVERT(date, '" + txtToDate.Text.Trim() + "', 103)) AND (Par_Adr_Ref ='" + supRef.ToString() + "') ORDER BY CONVERT(date,Bill_Hdr_Date,103)";

                if (optRptFilter.SelectedValue == "2")
                    qryStr = "SELECT Distinct Par_Adr_Name as Supplier_Name,Bill_Hdr_Ref_No as Bill_Ref_No,CONVERT(date,Bill_Hdr_Date,103) AS Bill_Date,Bill_Hdr_Party_Bill_No AS Party_Bill_No, " +
                             "Bill_Hdr_Tot_Amount as Bill_Amount,Bill_Hdr_Adj_Amount as Adjust_Amount, " +
                             "Bill_Hdr_Net_Amount as Net_Bill_Amount,Bill_Hdr_Pay_Amount as Payment_Amount,Bill_Hdr_Due_Amount AS Due_Amount " +
                             "FROM [DRN].[dbo].[View_PuTr_Po_Bill_Det] WHERE (CONVERT(date, Bill_Hdr_Date, 103) >= CONVERT(date, '" + txtFromDate.Text.Trim() + "', 103)) " +
                             "AND (CONVERT(date, Bill_Hdr_Date, 103) <= CONVERT(date, '" + txtToDate.Text.Trim() + "', 103)) AND (Bill_Det_Item_Code ='" + itemRef.ToString() + "') ORDER BY CONVERT(date,Bill_Hdr_Date,103)";

                if (optRptFilter.SelectedValue == "3")
                    qryStr = "SELECT Distinct Par_Adr_Name as Supplier_Name,Bill_Hdr_Ref_No as Bill_Ref_No,CONVERT(date,Bill_Hdr_Date,103) AS Bill_Date,Bill_Hdr_Party_Bill_No AS Party_Bill_No, " +
                             "Bill_Hdr_Tot_Amount as Bill_Amount,Bill_Hdr_Adj_Amount as Adjust_Amount, " +
                             "Bill_Hdr_Net_Amount as Net_Bill_Amount,Bill_Hdr_Pay_Amount as Payment_Amount,Bill_Hdr_Due_Amount AS Due_Amount " +
                             "FROM [DRN].[dbo].[View_PuTr_Po_Bill_Det] WHERE (CONVERT(date, Bill_Hdr_Date, 103) >= CONVERT(date, '" + txtFromDate.Text.Trim() + "', 103)) " +
                             "AND (CONVERT(date, Bill_Hdr_Date, 103) <= CONVERT(date, '" + txtToDate.Text.Trim() + "', 103)) AND (Itm_Det_Type ='" + itemType.ToString() + "') ORDER BY CONVERT(date,Bill_Hdr_Date,103)";

                if (optRptFilter.SelectedValue == "4")
                    qryStr = "SELECT Distinct Par_Adr_Name as Supplier_Name,Bill_Hdr_Ref_No as Bill_Ref_No,CONVERT(date,Bill_Hdr_Date,103) AS Bill_Date,Bill_Hdr_Party_Bill_No AS Party_Bill_No, " +
                             "Bill_Hdr_Tot_Amount as Bill_Amount,Bill_Hdr_Adj_Amount as Adjust_Amount, " +
                             "Bill_Hdr_Net_Amount as Net_Bill_Amount,Bill_Hdr_Pay_Amount as Payment_Amount,Bill_Hdr_Due_Amount AS Due_Amount " +
                             "FROM [DRN].[dbo].[View_PuTr_Po_Bill_Det] WHERE (CONVERT(date, Bill_Hdr_Date, 103) >= CONVERT(date, '" + txtFromDate.Text.Trim() + "', 103)) " +
                             "AND (CONVERT(date, Bill_Hdr_Date, 103) <= CONVERT(date, '" + txtToDate.Text.Trim() + "', 103)) AND (Bill_Det_Mrr_No LIKE '%" + txtSearch.Text.Trim() + "%') ORDER BY CONVERT(date,Bill_Hdr_Date,103)";

                if (optRptFilter.SelectedValue == "5")
                    qryStr = "SELECT Distinct Par_Adr_Name as Supplier_Name,Bill_Hdr_Ref_No as Bill_Ref_No,CONVERT(date,Bill_Hdr_Date,103) AS Bill_Date,Bill_Hdr_Party_Bill_No AS Party_Bill_No, " +
                             "Bill_Hdr_Tot_Amount as Bill_Amount,Bill_Hdr_Adj_Amount as Adjust_Amount, " +
                             "Bill_Hdr_Net_Amount as Net_Bill_Amount,Bill_Hdr_Pay_Amount as Payment_Amount,Bill_Hdr_Due_Amount AS Due_Amount " +
                             "FROM [DRN].[dbo].[View_PuTr_Po_Bill_Det] WHERE (CONVERT(date, Bill_Hdr_Date, 103) >= CONVERT(date, '" + txtFromDate.Text.Trim() + "', 103)) " +
                             "AND (CONVERT(date, Bill_Hdr_Date, 103) <= CONVERT(date, '" + txtToDate.Text.Trim() + "', 103)) AND (Bill_Det_Po_No LIKE '%" + txtSearch.Text.Trim() + "%') ORDER BY CONVERT(date,Bill_Hdr_Date,103)";


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
                string filename = String.Format("Bill_Report_{0}_to_{1}.xls", txtFromDate.Text, txtToDate.Text);
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