using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesOrderRptDsm : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        string empRef = "";
        string dsmRef = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            if (Session["sessionEmpRef"] != null)
                empRef = Session["sessionEmpRef"].ToString();

            var taDsm = new tblSalesDsmMasTableAdapter();
            var dtDsm = taDsm.GetDataByEmpRef(empRef);
            if (dtDsm.Rows.Count > 0)
            {
                dsmRef = dtDsm[0].Dsm_Ref.ToString();
                AutoCompleteExtenderSrch.ContextKey = dtDsm[0].Dsm_Ref.ToString();
            }
            else
            {
                AutoCompleteExtenderSrch.ContextKey = "0";
            }

            txtFromDateSo.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            txtToDateSo.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void btnShowSoRpt_Click(object sender, EventArgs e)
        {            
            reportInfo();
            var url = "frmShowSalesReport.aspx";
            //Response.Write("<script>var w=window.open('" + url + "'); w.focus();</script>");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportInfo()
        {
            if (txtFromDateSo.Text.Trim().Length > 0 && txtToDateSo.Text.Trim().Length > 0)
            {
                #region DSM Ref
                if (Session["sessionEmpRef"] != null)
                    empRef = Session["sessionEmpRef"].ToString();

                var taDsm = new tblSalesDsmMasTableAdapter();
                var dtDsm = taDsm.GetDataByEmpRef(empRef);
                if (dtDsm.Rows.Count > 0)                
                    dsmRef = dtDsm[0].Dsm_Ref.ToString();                
                #endregion

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
                    if (custRef == "")
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P' and {View_Sales_Details.Par_Adr_Sls_Per}='" + dsmRef + "'";
                    }
                    else
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P' and {View_Sales_Details.SO_Hdr_Pcode}='" + custRef + "'";
                    }
                    rptFile = "~/Module/Sales/Reports/rptSalesOrdByCust.rpt";
                }

                if (optRpt.SelectedValue == "2")
                {
                    if (custRef == "")
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P' and {View_Sales_Details.SO_Det_DO_Bal_Qty}>0 and {View_Sales_Details.Par_Adr_Sls_Per}='" + dsmRef + "'";
                    }
                    else
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P' and {View_Sales_Details.SO_Det_DO_Bal_Qty}>0 and {View_Sales_Details.SO_Hdr_Pcode}='" + custRef + "'";
                    }
                    rptFile = "~/Module/Sales/Reports/rptSalesOrdByPendDo.rpt";
                }

                if (optRpt.SelectedValue == "3")
                {
                    if (custRef == "")
                    {
                        rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and {tblSalesPartyAdr.Par_Adr_Sls_Per}='" + dsmRef + "'";
                    }
                    else
                    {
                        rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and {View_Sales_Do.SO_Hdr_Pcode}='" + custRef + "'";
                    }
                    rptFile = "~/Module/Sales/Reports/rptDelOrd.rpt";
                }

                if (optRpt.SelectedValue == "4")
                {
                    if (custRef == "")
                    {
                        rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and ({View_Sales_Do.DO_Det_Bal_Qty}>0 or tonumber({View_Sales_Do.DO_Det_Ext_Data2})>0) "
                        + "and {tblSalesPartyAdr.Par_Adr_Sls_Per}='" + dsmRef + "'";
                    }
                    else
                    {
                        rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and {View_Sales_Do.SO_Hdr_Pcode}='" + custRef
                        + "' and ({View_Sales_Do.DO_Det_Bal_Qty}>0 or tonumber({View_Sales_Do.DO_Det_Ext_Data2})>0)";
                    }
                    rptFile = "~/Module/Sales/Reports/rptDelOrdPend.rpt";
                }

                if (optRpt.SelectedValue == "5")
                {                    
                    var qrySqlStr = "";

                    qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Collection_Det]')) DROP VIEW [dbo].[View_Collection_Det]";
                    dbCon.ExecuteSQLStmt(qrySqlStr);

                    qrySqlStr = "CREATE view [dbo].[View_Collection_Det] as SELECT * FROM dbo.tbl_Acc_Fa_Te left outer join tblSalesPartyAdr " +
                                "on Trn_Ac_Code=Par_Adr_Acc_Code left outer join tblSalesDsmMas on Par_Adr_Sls_Per=Dsm_Ref WHERE (dbo.tbl_Acc_Fa_Te.Trn_Flag = 'RJV') " +
                                "and Trn_Trn_type='C' AND (dbo.tbl_Acc_Fa_Te.Trn_Narration <> 'Dummy Payment Receive') AND (CONVERT(date, dbo.tbl_Acc_Fa_Te.Trn_DATE, 103) " +
                                "BETWEEN CONVERT(date, '" + txtFromDateSo.Text.Trim() + "', 103) AND CONVERT(date, '" + txtToDateSo.Text.Trim() + "', 103))";
                    dbCon.ExecuteSQLStmt(qrySqlStr);

                    if (custRef == "")
                    {
                        rptSelcFormula = "{View_Collection_Det.Par_Adr_Sls_Per}='" + dsmRef + "'";
                    }
                    else
                    {
                        rptSelcFormula = "{View_Collection_Det.Trn_DATE} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim() + "') " +
                            "and {View_Collection_Det.Par_Adr_Ref}=" + custRef + "";
                    }

                    rptFile = "~/Module/Accounts/Reports/rptCollectionDet.rpt";
                }

                //if (optRpt.SelectedValue == "5")
                //{
                //    if (custRef == "")
                //    {
                //        rptSelcFormula = "{View_Sales_Do.DO_Hdr_HPC_Flag}='P' and ({View_Sales_Do.DO_Det_Bal_Qty}>0 or tonumber({View_Sales_Do.DO_Det_Ext_Data2})>0) "
                //        + "and {tblSalesPartyAdr.Par_Adr_Sls_Per}='" + dsmRef + "'";
                //    }
                //    else
                //    {
                //        rptSelcFormula = "{View_Sales_Do.DO_Hdr_HPC_Flag}='P' and {View_Sales_Do.SO_Hdr_Pcode}='" + custRef
                //        + "' and ({View_Sales_Do.DO_Det_Bal_Qty}>0 or tonumber({View_Sales_Do.DO_Det_Ext_Data2})>0)";
                //    }
                //    rptFile = "~/Module/Sales/Reports/rptDelOrdPend.rpt";
                //}             

                Session["RptDateFrom"] = txtFromDateSo.Text.Trim();
                Session["RptDateTo"] = txtToDateSo.Text.Trim();
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
            }
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
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
    }
}