using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmDeliveryChallanPrintStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void optRpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optRpt.SelectedValue == "1")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchCustomer";
            }

            if (optRpt.SelectedValue == "2")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchUndeliverChlnListAll";
            }
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            var taChlnSum = new View_Trn_Hdr_Chln_SumTableAdapter();
            var dtChlnSum = new dsInvTran.View_Trn_Hdr_Chln_SumDataTable();

            if (txtFromDate.Text.Trim().Length > 0 && txtToDate.Text.Trim().Length > 0)
            {
                if (optRpt.SelectedValue == "0")
                {
                    dtChlnSum = taChlnSum.GetDataByDateRange(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                }

                if (optRpt.SelectedValue == "1")
                {
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

                    if (custRef == "")
                        dtChlnSum = taChlnSum.GetDataByDateRange(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                    else
                        dtChlnSum = taChlnSum.GetDataByDateRangeCustomer(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), custRef.ToString());
                }

                if (optRpt.SelectedValue == "2")
                {
                    #region Get Chln Ref
                    var chlnRef = "";
                    if (txtSearch.Text.Trim().Length > 0)
                    {
                        var srchWords = txtSearch.Text.Trim().Split(':');
                        foreach (string word in srchWords)
                        {
                            chlnRef = word;
                            break;
                        }

                        if (chlnRef.Length > 0)
                        {
                            var taChln = new tbl_InTr_Trn_HdrTableAdapter();
                            var dtChln = taChln.GetDataByChlnRef(chlnRef);
                            if (dtChln.Rows.Count > 0) chlnRef = dtChln[0].Trn_Hdr_DC_No.ToString();
                        }
                    }
                    #endregion

                    if (chlnRef == "")
                        dtChlnSum = taChlnSum.GetDataByDateRange(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                    else
                        dtChlnSum = taChlnSum.GetDataByDateRangeChlnNo(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), chlnRef.ToString());
                }

                gvChlnList.DataSource = dtChlnSum;
                gvChlnList.DataBind();
            }
        }

        protected void gvChlnList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var chlnQty=Convert.ToInt32(e.Row.Cells[5].Text);
                var freeQty = Convert.ToInt32(e.Row.Cells[6].Text);
                Label lblTotQty = (Label)e.Row.FindControl("lblTotQty");
                lblTotQty.Text = (chlnQty + freeQty).ToString();

                var hfStatus = ((HiddenField)e.Row.FindControl("hfStatus")).Value.ToString();
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                Button btnStatus = (Button)e.Row.FindControl("btnStatus");
                if (hfStatus == "")
                {
                    btnStatus.Visible = false;
                    lblStatus.Text = "Pending";
                }
                else
                {
                    btnStatus.Visible = true;
                    lblStatus.Text = "Printed";
                }
            }
        }

        protected void btnStatus_Click(object sender, EventArgs e)
        {
            var taInTrTrnHdr = new tbl_InTr_Trn_HdrTableAdapter();            
            try
            {
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfChlnRef = (HiddenField)(row.FindControl("hfChlnRef"));

                taInTrTrnHdr.UpdateChlnPrintFlag("", hfChlnRef.Value.ToString());

                var lblStatus = (Label)(row.FindControl("lblStatus"));
                var btnStatus = (Button)(row.FindControl("btnStatus"));
                lblStatus.Text = "Pending";
                btnStatus.Visible = false;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }         
        }
    }
}