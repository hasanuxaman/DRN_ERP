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
    public partial class frmInvRptIssue : System.Web.UI.Page
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
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Inventory/Forms/wsAutoCompInv.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchItem";
            }
            if (optSrchType.SelectedValue == "2")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Inventory/Forms/wsAutoCompInv.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchIsuHeadRpt";
            }
            if (optSrchType.SelectedValue == "3")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Inventory/Forms/wsAutoCompInv.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchItemCategory";
            }  
        }

        protected void reportInfo()
        {
            if (txtFromDate.Text.Trim().Length > 0 && txtToDate.Text.Trim().Length > 0)
            {
                var isuHeadRef = "";
                var itemRef = "";
                var itemTypeRef = "";

                if (optSrchType.SelectedValue == "1")
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

                    #region Set Selection Formula for Item
                    if (itemRef == "")
                    {
                        rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_InTr_Trn_Hdr_Det.Trn_Hdr_HRPB_Flag}='P' and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Type}='IS' and Not(Isnull({View_InTr_Trn_Hdr_Det.Trn_Det_Lin_Qty}))";
                    }
                    else
                    {
                        rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_InTr_Trn_Hdr_Det.Trn_Hdr_HRPB_Flag}='P' and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Type}='IS' and Not(Isnull({View_InTr_Trn_Hdr_Det.Trn_Det_Lin_Qty})) and {View_InTr_Trn_Hdr_Det.Trn_Det_Icode}='" + itemRef + "'";
                    }
                    #endregion

                    if (optRptVal.SelectedValue == "1")
                    {
                        if (optRptType.SelectedValue == "1")
                            rptFile = "~/Module/Inventory/Reports/rptInvIsuItemDet.rpt";
                        else
                            rptFile = "~/Module/Inventory/Reports/rptInvIsuItemSum.rpt";
                    }
                    else
                    {
                        if (optRptType.SelectedValue == "1")
                            rptFile = "~/Module/Inventory/Reports/rptInvIsuItemDetVal.rpt";                            
                        else
                            rptFile = "~/Module/Inventory/Reports/rptInvIsuItemSumVal.rpt";
                    }
                }
                else if (optSrchType.SelectedValue == "2")
                {
                    #region Get Issue Head Ref
                    if (txtSearch.Text.Trim().Length > 0)
                    {
                        var srchWords = txtSearch.Text.Trim().Split(':');
                        foreach (string word in srchWords)
                        {
                            isuHeadRef = word;
                            break;
                        }

                        if (isuHeadRef.Length > 0)
                        {
                            int result;
                            if (int.TryParse(isuHeadRef, out result))
                            {
                                var taIsuHead = new View_Issue_Head_ReportTableAdapter();
                                var dtIsuHead = taIsuHead.GetDataByIsuHeadRef(isuHeadRef.ToString());
                                if (dtIsuHead.Rows.Count > 0) isuHeadRef = dtIsuHead[0].IsuHeadRefNo.ToString();
                            }
                        }
                    }
                    #endregion

                    #region Set Selection Formula for Issue Head
                    if (isuHeadRef == "")
                    {
                        rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_InTr_Trn_Hdr_Det.Trn_Hdr_HRPB_Flag}='P' and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Type}='IS' and Not(Isnull({View_InTr_Trn_Hdr_Det.Trn_Det_Lin_Qty}))";
                    }
                    else
                    {
                        rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_InTr_Trn_Hdr_Det.Trn_Hdr_HRPB_Flag}='P' and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Type}='IS' and Not(Isnull({View_InTr_Trn_Hdr_Det.Trn_Det_Lin_Qty})) and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Pcode}='" + isuHeadRef + "'";
                    }
                    #endregion

                    if (optRptVal.SelectedValue == "1")
                    {
                        if (optRptType.SelectedValue == "1")
                            rptFile = "~/Module/Inventory/Reports/rptInvIsuHeadDet.rpt";
                        else
                            rptFile = "~/Module/Inventory/Reports/rptInvIsuHeadSum.rpt";
                    }
                    else
                    {
                        if (optRptType.SelectedValue == "1")
                            rptFile = "~/Module/Inventory/Reports/rptInvIsuHeadDetVal.rpt";
                        else
                            rptFile = "~/Module/Inventory/Reports/rptInvIsuHeadSumVal.rpt";
                    }                    
                }
                if (optSrchType.SelectedValue == "3")
                {
                    #region Get Item Category Ref
                    if (txtSearch.Text.Trim().Length > 0)
                    {
                        var srchWords = txtSearch.Text.Trim().Split(':');
                        foreach (string word in srchWords) 
                        {
                            itemTypeRef = word;
                            break;
                        }

                        if (itemTypeRef.Length > 0)
                        {
                            var taItem = new tbl_InMa_Item_DetTableAdapter();
                            var dtItem = taItem.GetDataByItemType(itemTypeRef);
                            if (dtItem.Rows.Count > 0) itemTypeRef = dtItem[0].Itm_Det_Type.ToString();
                        }
                    }
                    #endregion

                    #region Set Selection Formula for Item Category
                    if (itemTypeRef == "")
                    {
                        rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_InTr_Trn_Hdr_Det.Trn_Hdr_HRPB_Flag}='P' and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Type}='IS' and Not(Isnull({View_InTr_Trn_Hdr_Det.Trn_Det_Lin_Qty}))";
                    }
                    else
                    {
                        rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                        + "') and {View_InTr_Trn_Hdr_Det.Trn_Hdr_HRPB_Flag}='P' and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Type}='IS' and Not(Isnull({View_InTr_Trn_Hdr_Det.Trn_Det_Lin_Qty})) and {View_InTr_Trn_Hdr_Det.Itm_Det_Type}='" + itemTypeRef + "'";
                    }
                    #endregion

                    if (optRptVal.SelectedValue == "1")
                    {
                        if (optRptType.SelectedValue == "1")
                            rptFile = "~/Module/Inventory/Reports/rptInvIsuItemTypeDet.rpt";
                        else
                            rptFile = "~/Module/Inventory/Reports/rptInvIsuItemTypeSum.rpt";
                    }
                    else
                    {
                        if (optRptType.SelectedValue == "1")
                            rptFile = "~/Module/Inventory/Reports/rptInvIsuItemTypeDetVal.rpt";
                        else
                            rptFile = "~/Module/Inventory/Reports/rptInvIsuItemTypeSumVal.rpt";
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