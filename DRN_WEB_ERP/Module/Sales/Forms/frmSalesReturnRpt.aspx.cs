using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesReturnRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            var taInvHdrDet = new View_InTr_Trn_Hdr_DetTableAdapter();
            var dtInvHdrDet = new DataTable();

            #region Get Customer Ref
            var custRef = "";
            if (txtSearch.Text.Trim().Length > 0)
            {
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custRef = word;
                    break;
                }

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0) custRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                    }
                }
            }
            #endregion

            if (optRpt.SelectedValue == "1")
            {
                if (custRef.ToString().Length <= 0)
                    dtInvHdrDet = taInvHdrDet.GetDataByDateRangeAll(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                else
                    dtInvHdrDet = taInvHdrDet.GetDataByDateRangeAllCust(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), custRef.ToString());
            }
            if (optRpt.SelectedValue == "2")
            {
                if (custRef.ToString().Length <= 0)
                    dtInvHdrDet = taInvHdrDet.GetDataByDateRangePend(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                else
                    dtInvHdrDet = taInvHdrDet.GetDataByDateRangePendCust(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), custRef.ToString());
            }
            if (optRpt.SelectedValue == "3")
            {
                if (custRef.ToString().Length <= 0)
                    dtInvHdrDet = taInvHdrDet.GetDataByDateRangeApp(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                else
                    dtInvHdrDet = taInvHdrDet.GetDataByDateRangeAppCust(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), custRef.ToString());
            }
            gvSalesRtn.DataSource = dtInvHdrDet;
            gvSalesRtn.DataBind();
        }

        public string GetRtnStat(string rtnRefNo)
        {
            var taInvHdrDet = new View_InTr_Trn_Hdr_DetTableAdapter();

            string rtnStat = "";
            try
            {
                var dtInvHdrDet = taInvHdrDet.GetDataByRtnRefNo(rtnRefNo.ToString());
                if (dtInvHdrDet.Rows.Count > 0)
                {
                    if (dtInvHdrDet[0].Trn_Hdr_HRPB_Flag == "P")
                        rtnStat = "Approved.";
                    else if (dtInvHdrDet[0].Trn_Hdr_HRPB_Flag == "H" && dtInvHdrDet[0].Trn_Hdr_Status == "1")
                        rtnStat = "Waiting for HOD Sales Approval.";
                    else if (dtInvHdrDet[0].Trn_Hdr_HRPB_Flag == "H" && dtInvHdrDet[0].Trn_Hdr_Status == "2")
                        rtnStat = "Waiting for Accounts Approval.";
                    else if (dtInvHdrDet[0].Trn_Hdr_HRPB_Flag == "C")
                        rtnStat = "Rejected.";
                    else
                        rtnStat = "";
                }
                else
                {
                    rtnStat = "Return Data Not Found.";
                }

                return rtnStat;
            }
            catch (Exception) { return rtnStat; }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var taInvHdrDet = new View_InTr_Trn_Hdr_DetTableAdapter();
                var dtInvHdrDet = new DataTable();

                #region Get Customer Ref
                var custRef = "";
                if (txtSearch.Text.Trim().Length > 0)
                {
                    var srchWords = txtSearch.Text.Trim().Split(':');
                    foreach (string word in srchWords)
                    {
                        custRef = word;
                        break;
                    }

                    if (custRef.Length > 0)
                    {
                        int result;
                        if (int.TryParse(custRef, out result))
                        {
                            var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                            var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                            if (dtPartyAdr.Rows.Count > 0) custRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                        }
                    }
                }
                #endregion

                if (optRpt.SelectedValue == "1")
                {
                    if (custRef.ToString().Length <= 0)
                        dtInvHdrDet = taInvHdrDet.GetDataByDateRangeAll(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                    else
                        dtInvHdrDet = taInvHdrDet.GetDataByDateRangeAllCust(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), custRef.ToString());
                }
                if (optRpt.SelectedValue == "2")
                {
                    if (custRef.ToString().Length <= 0)
                        dtInvHdrDet = taInvHdrDet.GetDataByDateRangePend(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                    else
                        dtInvHdrDet = taInvHdrDet.GetDataByDateRangePendCust(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), custRef.ToString());
                }
                if (optRpt.SelectedValue == "3")
                {
                    if (custRef.ToString().Length <= 0)
                        dtInvHdrDet = taInvHdrDet.GetDataByDateRangeApp(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                    else
                        dtInvHdrDet = taInvHdrDet.GetDataByDateRangeAppCust(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), custRef.ToString());
                }

                if (dtInvHdrDet.Rows.Count > 65535)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Export to Excel is not allowed due to excessive number of rows. (65535) ')", true);
                    return;
                }

                //First Method
                #region With Formating
                Response.Clear();
                Response.Buffer = true;
                string filename = String.Format("Sales_Return_Report_{0}_{1}_{2}.xls", txtFromDate.Text.Trim(), "to", txtToDate.Text.Trim());
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //Create a dummy GridView
                    GridView GridView1 = new GridView();
                    GridView1.AllowPaging = false;
                    GridView1.DataSource = dtInvHdrDet;
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void lnkBtnStatus_Click(object sender, EventArgs e)
        {

        }
    }
}