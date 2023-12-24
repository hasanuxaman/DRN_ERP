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
    public partial class frmTrialBalRpt : System.Web.UI.Page
    {
        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        string rptFile;
        string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taGrpCode = new tbl_Acc_Grp_CodeTableAdapter();
            ddlFirstGrp.DataSource = taGrpCode.GetDataByGrpDef("A01");
            ddlFirstGrp.DataTextField = "Grp_Code_Name";
            ddlFirstGrp.DataValueField = "Grp_Code";
            ddlFirstGrp.DataBind();
            ddlFirstGrp.Items.Insert(0, new ListItem("-----Select-----", "0"));

            ddlSecondGrp.Items.Insert(0, new ListItem("-----Select-----", "0"));

            ddlThirdGrp.Items.Insert(0, new ListItem("-----Select-----", "0"));
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
                                "on Gl_Coa_Code=Trn_Gl_Coa_Code";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Trial_Bal]')) DROP VIEW [dbo].[View_Trial_Bal]";
                dbCon.ExecuteSQLStmt(qrySqlStr);
                qrySqlStr = "Create View View_Trial_Bal as SELECT  tbl_Acc_Fa_Gl_Coa.*, dbo.View_Trial_Bal_Open_Bal.Opn_Gl_Coa_Code, dbo.View_Trial_Bal_Open_Bal.Opn_Gl_Coa_Name, " + 
                            "dbo.View_Trial_Bal_Open_Bal.OpnDr, dbo.View_Trial_Bal_Open_Bal.OpnCr, dbo.View_Trial_Bal_Open_Bal.OpnBal, dbo.View_Trial_Bal_Tran_Bal.Trn_Gl_Coa_Code, " +
                            "dbo.View_Trial_Bal_Tran_Bal.Trn_Gl_Coa_Name, dbo.View_Trial_Bal_Tran_Bal.TranDr, dbo.View_Trial_Bal_Tran_Bal.TranCr, dbo.View_Trial_Bal_Tran_Bal.TranBal, " +
                            "dbo.View_Trial_Bal_Open_Bal.OpnBal + dbo.View_Trial_Bal_Tran_Bal.TranBal AS CloseBal FROM  dbo.tbl_Acc_Fa_Gl_Coa LEFT OUTER JOIN dbo.View_Trial_Bal_Open_Bal " +
                            "ON dbo.tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code = dbo.View_Trial_Bal_Open_Bal.Opn_Gl_Coa_Code LEFT OUTER JOIN dbo.View_Trial_Bal_Tran_Bal " +
                            "ON dbo.tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code = dbo.View_Trial_Bal_Tran_Bal.Trn_Gl_Coa_Code";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                if (ddlThirdGrp.SelectedIndex == 0)
                {
                    if (ddlSecondGrp.SelectedIndex == 0)
                    {
                        if (ddlFirstGrp.SelectedIndex == 0)                        
                            rptSelcFormula = "";                        
                        else                 
                            rptSelcFormula = "{View_Acc_GL_Group_Code_List_A03.FirstGrpId}='" + ddlFirstGrp.SelectedValue.ToString() + "'";
                    }
                    else
                    {
                        //if (ddlFirstGrp.SelectedIndex == 0)
                            rptSelcFormula = "{View_Acc_GL_Group_Code_List_A03.SecondGrpId}='" + ddlSecondGrp.SelectedValue.ToString() + "'";
                        //else
                        //    rptSelcFormula = "{View_Acc_GL_Group_Code_List_A03.FirstGrpId}='" + ddlFirstGrp.SelectedValue.ToString();
                    }
                }
                else
                {
                    //if (ddlSecondGrp.SelectedIndex == 0)
                    //{
                    //    //if (ddlFirstGrp.SelectedIndex == 0)
                    //    //    rptSelcFormula = "{View_Acc_GL_Group_Code_List_A03.ThirdGrpId}='" + ddlThirdGrp.SelectedValue.ToString() + "'";
                    //    //else
                    //        rptSelcFormula = "{View_Acc_GL_Group_Code_List_A03.FirstGrpId}='" + ddlFirstGrp.SelectedValue.ToString() + "' or {View_Acc_GL_Group_Code_List_A03.ThirdGrpId}='" + ddlThirdGrp.SelectedValue.ToString() + "'";
                    //}
                    //else
                    //{
                    //    //if (ddlFirstGrp.SelectedIndex == 0)
                    //    //    rptSelcFormula = "{View_Acc_GL_Group_Code_List_A03.SecondGrpId}='" + ddlSecondGrp.SelectedValue.ToString() + "' or {View_Acc_GL_Group_Code_List_A03.ThirdGrpId}='" + ddlThirdGrp.SelectedValue.ToString() + "'";         
                    //    //else
                    //    //    rptSelcFormula = "{View_Acc_GL_Group_Code_List_A03.FirstGrpId}='" + ddlFirstGrp.SelectedValue.ToString() + "' or {View_Acc_GL_Group_Code_List_A03.SecondGrpId}='" + ddlSecondGrp.SelectedValue.ToString() + "' or {View_Acc_GL_Group_Code_List_A03.ThirdGrpId}='" + ddlThirdGrp.SelectedValue.ToString() + "'";

                    //}
                    rptSelcFormula = "{View_Acc_GL_Group_Code_List_A03.ThirdGrpId}='" + ddlThirdGrp.SelectedValue.ToString() + "'";
                }

                if (optGrpSum.Checked)
                    rptFile = "~/Module/Accounts/Reports/rptTrialBalGrpSum.rpt";
                if (optGrpDet.Checked)
                    rptFile = "~/Module/Accounts/Reports/rptTrialBalGrpDet.rpt";
               
                Session["RptDateFrom"] = txtFromDate.Text.Trim();
                Session["RptDateTo"] = txtToDate.Text.Trim();
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
                Session["RptHdr"] = "Trial Balance from " + txtFromDate.Text.Trim() + " to " + txtToDate.Text.Trim();
            }
        }

        protected void ddlFirstGrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taCoaGrpCode = new tbl_Acc_Grp_CodeTableAdapter();

            //if (ddlFirstGrp.SelectedIndex != 0)
            //{
            ddlSecondGrp.Items.Clear();
            ddlSecondGrp.DataSource = taCoaGrpCode.GetDataByGrpSet1(ddlFirstGrp.SelectedValue.ToString());
            ddlSecondGrp.DataTextField = "Grp_Code_Name";
            ddlSecondGrp.DataValueField = "Grp_Code";
            ddlSecondGrp.DataBind();
            ddlSecondGrp.Items.Insert(0, new ListItem("-----Select-----", "0"));

            ddlThirdGrp.Items.Clear();
            ddlThirdGrp.Items.Insert(0, new ListItem("-----Select-----", "0"));
            //}
        }

        protected void ddlSecondGrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taCoaGrpCode = new tbl_Acc_Grp_CodeTableAdapter();

            //if (ddlFirstGrp.SelectedIndex != 0)
            //{
            ddlThirdGrp.Items.Clear();
            ddlThirdGrp.DataSource = taCoaGrpCode.GetDataByGrpSet2(ddlSecondGrp.SelectedValue.ToString());
            ddlThirdGrp.DataTextField = "Grp_Code_Name";
            ddlThirdGrp.DataValueField = "Grp_Code";
            ddlThirdGrp.DataBind();
            ddlThirdGrp.Items.Insert(0, new ListItem("-----Select-----", "0"));
            //}
        }        
    }
}