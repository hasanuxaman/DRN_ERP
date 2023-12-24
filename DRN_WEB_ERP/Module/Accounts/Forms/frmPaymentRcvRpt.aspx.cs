using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmPaymentRcvRpt : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void btnShowCollectRpt_Click(object sender, EventArgs e)
        {
            reportCollectionInfo();
            var url = "frmShowAccReport.aspx";
            //Response.Write("<script>var w=window.open('" + url + "'); w.focus();</script>");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "','_newtab');", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportCollectionInfo()
        {
            if (txtFromDt.Text == "" || txtToDt.Text == "") return;

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

            var qrySqlStr = "";

            qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Collection_Det]')) DROP VIEW [dbo].[View_Collection_Det]";
            dbCon.ExecuteSQLStmt(qrySqlStr);

            qrySqlStr = "CREATE view [dbo].[View_Collection_Det] as SELECT * FROM dbo.tbl_Acc_Fa_Te left outer join tblSalesPartyAdr " +
                        "on Trn_Ac_Code=Par_Adr_Acc_Code left outer join tblSalesDsmMas on Par_Adr_Sls_Per=Dsm_Ref WHERE (dbo.tbl_Acc_Fa_Te.Trn_Flag = 'RJV') " +
                        "and Trn_Trn_type='C' AND (dbo.tbl_Acc_Fa_Te.Trn_Narration <> 'Dummy Payment Receive') AND (CONVERT(date, dbo.tbl_Acc_Fa_Te.Trn_DATE, 103) " +
                        "BETWEEN CONVERT(date, '" + txtFromDt.Text.Trim() + "', 103) AND CONVERT(date, '" + txtToDt.Text.Trim() + "', 103))";
            dbCon.ExecuteSQLStmt(qrySqlStr);

            if (custRef == "")                    
            {
                rptSelcFormula = ""; //"{View_Dsm_Wise_Collection_Det.Trn_DATE} in Date('" + txtFromDt.Text.Trim() + "') to Date ('" + txtToDt.Text.Trim() + "')";
            }
            else
            {
                rptSelcFormula = "{View_Collection_Det.Trn_DATE} in Date('" + txtFromDt.Text.Trim() + "') to Date ('" + txtToDt.Text.Trim() + "') " +
                    "and {View_Collection_Det.Par_Adr_Ref}=" + custRef + "";
            }

            rptFile = "~/Module/Accounts/Reports/rptCollectionDet.rpt";

            Session["RptDateFrom"] = txtFromDt.Text.Trim();
            Session["RptDateTo"] = txtToDt.Text.Trim();
            Session["RptFilePath"] = rptFile;
            Session["RptFormula"] = rptSelcFormula;
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
    }
}