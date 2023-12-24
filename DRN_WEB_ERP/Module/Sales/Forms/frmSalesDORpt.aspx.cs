using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesDORpt : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            reportInfo();

            //btnShowRpt.Attributes.Add("onclick", "javascript:w= window.open('frmShowSalesReport.aspx');");

            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            reportInfo();
            var url = "frmShowSalesReport.aspx";
            //Response.Write("<script>var w=window.open('" + url + "'); w.focus();</script>");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (txtSearch.Text.Trim().Length <= 0) args.IsValid = true;

                var custRef = "";
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
                        if (dtPartyAdr.Rows.Count > 0)
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

        protected void reportInfo()
        {
            if (txtFromDate.Text.Trim().Length > 0 && txtToDate.Text.Trim().Length > 0)
            {
                #region Get Customer Ref
                var custRef = "";
                if (txtSearch.Text.Trim().Length > 0)
                {                    
                    if (txtSearch.Text.Trim().Length <= 0) return;

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

                #region Get DSM Ref
                var dsmRef = "";
                if (txtSearch.Text.Trim().Length > 0)
                {
                    if (txtSearch.Text.Trim().Length <= 0) return;

                    var srchWords = txtSearch.Text.Trim().Split(':');
                    foreach (string word in srchWords)
                    {
                        dsmRef = word;
                        break;
                    }

                    if (dsmRef.Length > 0)
                    {
                        int result;
                        if (int.TryParse(dsmRef, out result))
                        {
                            var taDsm = new tblSalesDsmMasTableAdapter();
                            var dtDsm = taDsm.GetDataByDsmRef(Convert.ToInt32(dsmRef));
                            if (dtDsm.Rows.Count > 0) dsmRef = dtDsm[0].Dsm_Ref.ToString();
                        }
                    }
                }
                #endregion

                #region Get SP Ref
                var spRef = "";
                if (txtSearch.Text.Trim().Length > 0)
                {
                    if (txtSearch.Text.Trim().Length <= 0) return;

                    var srchWords = txtSearch.Text.Trim().Split(':');
                    foreach (string word in srchWords)
                    {
                        spRef = word;
                        break;
                    }

                    if (spRef.Length > 0)
                    {
                        int result;
                        if (int.TryParse(spRef, out result))
                        {
                            var taSp = new tblSalesPersonMasTableAdapter();
                            var dtSp = taSp.GetDataBySpRef(Convert.ToInt32(spRef));
                            if (dtSp.Rows.Count > 0) spRef = dtSp[0].Sp_Ref.ToString();
                        }
                    }
                }
                #endregion


                #region DO Type Created
                if (optRpt.SelectedValue == "1")
                {
                    #region Customer Filter
                    if (optRptFilter.SelectedValue == "1")
                    {
                        if (custRef == "")
                        {
                            rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P'";
                        }
                        else
                        {
                            rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and {View_Sales_Do.SO_Hdr_Pcode}='" + custRef + "'";
                        }

                        if (optRptOption.SelectedValue == "1")
                            rptFile = "~/Module/Sales/Reports/rptDelOrdCustDet.rpt";
                        else
                            rptFile = "~/Module/Sales/Reports/rptDelOrdCustSum.rpt";
                    }
                    #endregion

                    #region DSM Filter
                    if (optRptFilter.SelectedValue == "2")
                    {
                        if (dsmRef == "")
                        {
                            rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P'";
                        }
                        else
                        {
                            rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and {View_Sales_Dsm.Dsm_Ref_Str}='" + dsmRef + "'";
                        }

                        if (optRptOption.SelectedValue == "1")
                            rptFile = "~/Module/Sales/Reports/rptDelOrdDsmDet.rpt";
                        else
                            rptFile = "~/Module/Sales/Reports/rptDelOrdDsmSum.rpt";
                    }
                    #endregion

                    #region SP Filter
                    if (optRptFilter.SelectedValue == "3")
                    {
                        if (spRef == "")
                        {
                            rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P'";
                        }
                        else
                        {
                            rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and {View_Sales_Do.SO_Hdr_Com4}='" + spRef + "'";
                        }

                        if (optRptOption.SelectedValue == "1")
                            rptFile = "~/Module/Sales/Reports/rptDelOrdSpDet.rpt";
                        else
                            rptFile = "~/Module/Sales/Reports/rptDelOrdSpSum.rpt";
                    }
                    #endregion
                }
                #endregion

                #region DO Type Pending
                if (optRpt.SelectedValue == "2")
                {
                    #region Customer Filter
                    if (optRptFilter.SelectedValue == "1")
                    {
                        if (custRef == "")
                        {
                            rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and ({View_Sales_Do.DO_Det_Bal_Qty}>0 or tonumber({View_Sales_Do.DO_Det_Ext_Data2})>0)";
                        }
                        else
                        {
                            rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and {View_Sales_Do.SO_Hdr_Pcode}='" + custRef
                            + "' and ({View_Sales_Do.DO_Det_Bal_Qty}>0 or tonumber({View_Sales_Do.DO_Det_Ext_Data2})>0)";
                        }
                        if (optRptOption.SelectedValue == "1")
                            rptFile = "~/Module/Sales/Reports/rptDelOrdPendCustDet.rpt";
                        else
                            rptFile = "~/Module/Sales/Reports/rptDelOrdPendCustSum.rpt";
                    }
                    #endregion

                    #region DSM Filter
                    if (optRptFilter.SelectedValue == "2")
                    {
                        if (dsmRef == "")
                        {
                            rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and ({View_Sales_Do.DO_Det_Bal_Qty}>0 or tonumber({View_Sales_Do.DO_Det_Ext_Data2})>0)";
                        }
                        else
                        {
                            rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and {View_Sales_Dsm.Dsm_Ref_Str}='" + dsmRef
                            + "' and ({View_Sales_Do.DO_Det_Bal_Qty}>0 or tonumber({View_Sales_Do.DO_Det_Ext_Data2})>0)";
                        }
                        if (optRptOption.SelectedValue == "1")
                            rptFile = "~/Module/Sales/Reports/rptDelOrdPendDsmDet.rpt";
                        else
                            rptFile = "~/Module/Sales/Reports/rptDelOrdPendDsmSum.rpt";
                    }
                    #endregion

                    #region SP Filter
                    if (optRptFilter.SelectedValue == "3")
                    {
                        if (spRef == "")
                        {
                            rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and ({View_Sales_Do.DO_Det_Bal_Qty}>0 or tonumber({View_Sales_Do.DO_Det_Ext_Data2})>0)";
                        }
                        else
                        {
                            rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and and {View_Sales_Do.SO_Hdr_Com4}='" + spRef
                            + "' and ({View_Sales_Do.DO_Det_Bal_Qty}>0 or tonumber({View_Sales_Do.DO_Det_Ext_Data2})>0)";
                        }
                        if (optRptOption.SelectedValue == "1")
                            rptFile = "~/Module/Sales/Reports/rptDelOrdPendSpDet.rpt";
                        else
                            rptFile = "~/Module/Sales/Reports/rptDelOrdPendSpSum.rpt";
                    }
                    #endregion
                }
                #endregion

                #region DO Type Executed
                if (optRpt.SelectedValue == "3")
                {
                    #region Customer Filter
                    if (optRptFilter.SelectedValue == "1")
                    {
                        if (custRef == "")
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P'";
                        }
                        else
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.Trn_Hdr_Pcode}='" + custRef + "'";
                        }
                        if (optRptOption.SelectedValue == "1")
                            rptFile = "~/Module/Sales/Reports/rptDelChlnList.rpt";
                        else
                            rptFile = "~/Module/Sales/Reports/rptDelChlnListCustSum.rpt";
                    }
                    #endregion

                    #region DSM Filter
                    if (optRptFilter.SelectedValue == "2")
                    {
                        if (dsmRef == "")
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P'";
                        }
                        else
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Dsm.Dsm_Ref_Str}='" + dsmRef + "'";
                        }
                        if (optRptOption.SelectedValue == "1")
                            rptFile = "~/Module/Sales/Reports/rptDelChlnListDsm.rpt";
                        else
                            rptFile = "~/Module/Sales/Reports/rptDelChlnListDsmSum.rpt";
                    }
                    #endregion

                    #region SP Filter
                    if (optRptFilter.SelectedValue == "3")
                    {
                        if (spRef == "")
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P'";
                        }
                        else
                        {
                            rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do.SO_Hdr_Com4}='" + spRef + "'";
                        }
                        if (optRptOption.SelectedValue == "1")
                            rptFile = "~/Module/Sales/Reports/rptDelChlnListSpDet.rpt";
                        else
                            rptFile = "~/Module/Sales/Reports/rptDelChlnListSpSum.rpt";
                    }
                    #endregion
                }
                #endregion

                Session["RptDateFrom"] = txtFromDate.Text.Trim();
                Session["RptDateTo"] = txtToDate.Text.Trim();
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
            }
        }

        protected void optRptFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optRptFilter.SelectedValue == "1")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Sales/Forms/wsAutoComSales.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchCustomer";
            }

            if (optRptFilter.SelectedValue == "2")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Sales/Forms/wsAutoComSales.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchDSM";
            }

            if (optRptFilter.SelectedValue == "3")
            {
                txtSearch.Text = "";
                AutoCompleteExtenderSrch.ServicePath = "~/Module/Sales/Forms/wsAutoComSales.asmx";
                AutoCompleteExtenderSrch.ServiceMethod = "GetSrchSalesPer";
            }
        }
    }
}