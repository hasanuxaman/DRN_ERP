using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesOrderStatusRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            var taOrd = new VIEW_SALES_ORDERTableAdapter();
            var dtOrd = new DataTable();

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
                dtOrd = taOrd.GetDataByOrdDateStatusAll(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
            }
            if (optRpt.SelectedValue == "2")
            {
                if (custRef.ToString().Length <= 0)
                    dtOrd = taOrd.GetDataByOrdDateStatusApp(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                else
                    dtOrd = taOrd.GetDataByOrdDateStatusAppCust(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), custRef.ToString());
            }
            if (optRpt.SelectedValue == "3")
            {
                if (custRef.ToString().Length <= 0)
                    dtOrd = taOrd.GetDataByOrdDateStatusPend(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                else
                    dtOrd = taOrd.GetDataByOrdDateStatusPendCust(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), custRef.ToString());
            }
            if (optRpt.SelectedValue == "4")
            {
                if (custRef.ToString().Length <= 0)
                    dtOrd = taOrd.GetDataByOrdDateStatusRej(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                else
                    dtOrd = taOrd.GetDataByOrdDateStatusRejCust(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), custRef.ToString());
            }
            gvApprDoDet.DataSource = dtOrd;
            gvApprDoDet.DataBind();

            btnExport.Enabled = dtOrd.Rows.Count > 0;
        }

        public string GetOrdCom(string ordRef)
        {
            //var taTranCom = new tbl_Tran_ComTableAdapter();

            //string ordCom = "";
            //try
            //{
            //    var dtTranCom = taTranCom.GetDataByRefNo(ordRef.ToString());
            //    foreach (dsProcTran.tbl_Tran_ComRow dr in dtTranCom.Rows)
            //    {
            //        ordCom = ordCom + "\n>>" + dr.Com_App_Date.ToString() + ", " + dr.Com_App_Name + " (" + dr.Com_App_Desig + "), " + dr.Com_Gen_Com;
            //    }
            //    return ordCom;
            //}
            //catch (Exception) { return ordCom; }

            var taOrdDet = new tblSalesOrderDetTableAdapter();

            string ordStat = "";
            try
            {
                var dtOrdDet = taOrdDet.GetDataByHdrRef(ordRef.ToString());
                if (dtOrdDet.Rows.Count > 0)
                {
                    if (dtOrdDet[0].SO_Det_Flag == "P" && dtOrdDet[0].SO_Det_T_Fl == "A")
                        ordStat = "Approved with Over Credit Limit."; //---
                    else if (dtOrdDet[0].SO_Det_Flag == "H" && dtOrdDet[0].SO_Det_Status == "1" && dtOrdDet[0].SO_Det_T_In == 0)
                        ordStat = "Rejected.";
                    else if (dtOrdDet[0].SO_Det_Flag == "H" && dtOrdDet[0].SO_Det_Status == "2")
                        ordStat = "Waiting for Credit Approval.";
                    else if (dtOrdDet[0].SO_Det_Flag == "H" && dtOrdDet[0].SO_Det_Status == "3")
                        ordStat = "Waiting for Head of F & A Approval.";
                    else if (dtOrdDet[0].SO_Det_Flag == "H" && dtOrdDet[0].SO_Det_Status == "4")
                        ordStat = "Waiting for Head of Sales Approval.";
                    else if (dtOrdDet[0].SO_Det_Flag == "P" && dtOrdDet[0].SO_Det_T_Fl != "A")
                        ordStat = ""; //"Order Within Credit Limit.";
                    else
                        ordStat = "";
                }
                else
                {
                    ordStat = "Order Data Not Found.";
                }

                return ordStat;
            }
            catch (Exception ex) { return ordStat; }
        }

        public string GetOrdAppBy(string ordRefNo)
        {
            var taTranCom = new tbl_Tran_ComTableAdapter();

            string ordCom = "";
            try
            {
                //var dtTranCom = taTranCom.GetDataByRefNo(ordRefNo.ToString());
                //foreach (dsProcTran.tbl_Tran_ComRow dr in dtTranCom.Rows)
                //{
                //    ordCom = ordCom + "\n>>" + dr.Com_App_Date.ToString() + ", " + dr.Com_App_Name + " (" + dr.Com_App_Desig + "), " + dr.Com_Gen_Com;
                //}

                var dtTranCom = taTranCom.GetDataByRefNoStatus(ordRefNo.ToString(), "APP");
                if (dtTranCom.Rows.Count > 0)
                    ordCom = dtTranCom[0].Com_App_Name + " (" + dtTranCom[0].Com_App_Desig + ")";

                return ordCom;
            }
            catch (Exception) { return ordCom; }
        }

        public string GetOrdComLog(string ordRefNo)
        {
            var taTranCom = new tbl_Tran_ComTableAdapter();

            string ordCom = "";
            try
            {
                var dtTranCom = taTranCom.GetDataByRefNo(ordRefNo.ToString());
                foreach (dsProcTran.tbl_Tran_ComRow dr in dtTranCom.Rows)
                {
                    ordCom = ordCom + ">>" + dr.Com_Gen_Com + "<br/><br/>";
                }
                return ordCom.Trim();
            }
            catch (Exception) { return ordCom; }
        }

        protected void lnkBtnStatus_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            var hfSoHdrRef = (HiddenField)(row.FindControl("hfSoHdrRef"));

            Session["OrderRefNo"] = hfSoHdrRef.Value.ToString();
            var url = "frmSalesOrderPrint.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var taOrd = new VIEW_SALES_ORDERTableAdapter();
                var dtOrd = new DataTable();

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
                    dtOrd = taOrd.GetDataByOrdDateStatusAll(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                }
                if (optRpt.SelectedValue == "2")
                {
                    if (custRef.ToString().Length <= 0)
                        dtOrd = taOrd.GetDataByOrdDateStatusApp(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                    else
                        dtOrd = taOrd.GetDataByOrdDateStatusAppCust(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), custRef.ToString());
                }
                if (optRpt.SelectedValue == "3")
                {
                    if (custRef.ToString().Length <= 0)
                        dtOrd = taOrd.GetDataByOrdDateStatusPend(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                    else
                        dtOrd = taOrd.GetDataByOrdDateStatusPendCust(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), custRef.ToString());
                }
                if (optRpt.SelectedValue == "4")
                {
                    if (custRef.ToString().Length <= 0)
                        dtOrd = taOrd.GetDataByOrdDateStatusRej(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                    else
                        dtOrd = taOrd.GetDataByOrdDateStatusRejCust(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), custRef.ToString());
                }

                if (dtOrd.Rows.Count > 65535)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Export to Excel is not allowed due to excessive number of rows. (65535) ')", true);
                    return;
                }

                //First Method
                #region With Formating
                Response.Clear();
                Response.Buffer = true;
                string filename = String.Format("Order_Status_Report_{0}_{1}_{2}.xls", txtFromDate.Text.Trim(), "to", txtToDate.Text.Trim());
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //Create a dummy GridView
                    gvApprDoDet.AllowPaging = false;
                    gvApprDoDet.DataSource = dtOrd;
                    gvApprDoDet.DataBind();

                    gvApprDoDet.HeaderRow.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in gvApprDoDet.HeaderRow.Cells)
                    {
                        cell.BackColor = gvApprDoDet.HeaderStyle.BackColor;
                    }

                    foreach (GridViewRow row in gvApprDoDet.Rows)
                    {
                        row.BackColor = System.Drawing.Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = gvApprDoDet.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = gvApprDoDet.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    gvApprDoDet.RenderControl(hw);

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
    }
}