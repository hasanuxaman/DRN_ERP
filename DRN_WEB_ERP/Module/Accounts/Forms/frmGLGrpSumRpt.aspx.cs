using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmGLGrpSumRpt : System.Web.UI.Page
    {
        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        string rptFile;
        string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            reportInfo();
            var url = "frmShowAccReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportInfo()
        {
            if (txtFromDate.Text.Trim().Length > 0 && txtToDate.Text.Trim().Length > 0)
            {
                var qrySqlStr = "";
                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_GL_Open_Bal]')) DROP VIEW [dbo].[View_GL_Open_Bal]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "Create view View_GL_Open_Bal as select tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code as Opn_Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name as Opn_Gl_Coa_Name, " + 
                            "SUM(DebitAmount) as OpnDr,SUM(CreditAmount) as OpnCr,SUM(DebitAmount) - SUM(CreditAmount) as OpnBal " +
                            "from tbl_Acc_Fa_Gl_Coa left outer join View_Acc_Tran_Det on Gl_Coa_Code =Trn_Ac_Code " +
                            "where CONVERT(date,Trn_DATE,103)< CONVERT(date,'" + txtFromDate.Text.Trim() + "',103) " + 
                            "group by tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Gl_Tran_Bal]')) DROP VIEW [dbo].[View_Gl_Tran_Bal]";
                dbCon.ExecuteSQLStmt(qrySqlStr);
                qrySqlStr = "Create view View_Gl_Tran_Bal as select tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code as Trn_Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name as Trn_Gl_Coa_Name, " +
                            "SUM(DebitAmount) as TotDr,SUM(CreditAmount) as TotCr, SUM(DebitAmount)  - SUM(CreditAmount) as TranBal " +
                            "from tbl_Acc_Fa_Gl_Coa left outer join View_Acc_Tran_Det on Gl_Coa_Code =Trn_Ac_Code " +
                            "where CONVERT(date,Trn_DATE,103) BETWEEN CONVERT(date,'" + txtFromDate.Text.Trim() + "',103) " +
                            "and CONVERT(date,'" + txtToDate.Text.Trim() + "',103) group by tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_GL_Acc_Sum]')) DROP VIEW [dbo].[View_GL_Acc_Sum]";
                dbCon.ExecuteSQLStmt(qrySqlStr);
                qrySqlStr = "Create view View_GL_Acc_Sum as select tbl_Acc_Fa_Gl_Coa.*,View_GL_Open_Bal.*,View_Gl_Tran_Bal.*,(OpnBal+TranBal) as CloseBal " +
                            "from tbl_Acc_Fa_Gl_Coa left outer join View_GL_Open_Bal on Gl_Coa_Code=Opn_Gl_Coa_Code left outer join View_Gl_Tran_Bal " +
                            "on Gl_Coa_Code=Trn_Gl_Coa_Code where Gl_Coa_Type='" + cboGLGroup.SelectedValue.ToString() + "'";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                rptFile = "~/Module/Accounts/Reports/rptGLAccSum.rpt";

                rptSelcFormula = "";

                Session["RptDateFrom"] = txtFromDate.Text.Trim();
                Session["RptDateTo"] = txtToDate.Text.Trim();
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
                Session["RptHdr"] = cboGLGroup.SelectedItem.ToString() + " Group Summary from " + txtFromDate.Text.Trim() + " to " + txtToDate.Text.Trim();
            }
        }
    }
}