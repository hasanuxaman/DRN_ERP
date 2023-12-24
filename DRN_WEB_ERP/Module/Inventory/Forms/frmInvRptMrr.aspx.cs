using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;

namespace DRN_WEB_ERP.Module.Inventory.Forms
{
    public partial class frmInvRptMrr : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            reportInfo();
            var url = "frmShowInvReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void optSrchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optSrchType.SelectedValue == "1")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Procurement/Forms/wsAutoComProc.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchSupplier";
            }
            if (optSrchType.SelectedValue == "2")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Inventory/Forms/wsAutoCompInv.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchItem";
            }            
        }

        protected void reportInfo()
        {
            if (txtFromDate.Text.Trim().Length > 0 && txtToDate.Text.Trim().Length > 0)
            {
                var supRef = "";
                var itemRef = "";

                if (optSrchType.SelectedValue == "1")
                {
                    #region Get Supplier Ref
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
                                var dtSupAdr = taSupAdr.GetDataBySupAdrRef(supRef.ToString());
                                if (dtSupAdr.Rows.Count > 0) supRef = dtSupAdr[0].Par_Adr_Ref.ToString();
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region Get Item Ref
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
                }

                if (optSrchType.SelectedValue == "1")
                {
                    #region Set Selection Formula for Supplier
                    if (supRef == "")
                    {
                        rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_InTr_Trn_Hdr_Det.Trn_Hdr_HRPB_Flag}='P' and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Code}='PO'";
                    }
                    else
                    {
                        rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_InTr_Trn_Hdr_Det.Trn_Hdr_HRPB_Flag}='P' and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Code}='PO' and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Pcode}='" + supRef + "'";
                    }
                    #endregion

                    if (optRptVal.SelectedValue == "1")
                    {
                        if (optRptType.SelectedValue == "1")
                            rptFile = "~/Module/Inventory/Reports/rptInvMrrSupDet.rpt";
                        else
                            rptFile = "~/Module/Inventory/Reports/rptInvMrrSupSum.rpt";
                    }
                    else
                    {
                        if (optRptType.SelectedValue == "1")
                            rptFile = "~/Module/Inventory/Reports/rptInvMrrSupDetVal.rpt";
                        else
                            rptFile = "~/Module/Inventory/Reports/rptInvMrrSupSumVal.rpt";
                    }                    
                }
                else
                {
                    #region Set Selection Formula for Item
                    if (itemRef == "")
                    {
                        rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_InTr_Trn_Hdr_Det.Trn_Hdr_HRPB_Flag}='P' and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Code}='PO'";
                    }
                    else
                    {
                        rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_InTr_Trn_Hdr_Det.Trn_Hdr_HRPB_Flag}='P' and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Code}='PO' and {View_InTr_Trn_Hdr_Det.Trn_Det_Icode}='" + itemRef + "'";
                    }
                    #endregion

                    if (optRptVal.SelectedValue == "1")
                    {
                        if (optRptType.SelectedValue == "1")
                            rptFile = "~/Module/Inventory/Reports/rptInvMrrItemDet.rpt";
                        else
                            rptFile = "~/Module/Inventory/Reports/rptInvMrrItemSum.rpt";                        
                    }
                    else
                    {
                        if (optRptType.SelectedValue == "1")
                            rptFile = "~/Module/Inventory/Reports/rptInvMrrItemSumVal.rpt";
                        else
                            rptFile = "~/Module/Inventory/Reports/rptInvMrrItemDetVal.rpt";
                    }
                }                               

                Session["RptDateFrom"] = txtFromDate.Text.Trim();
                Session["RptDateTo"] = txtToDate.Text.Trim();
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
            }
        }
    }
}