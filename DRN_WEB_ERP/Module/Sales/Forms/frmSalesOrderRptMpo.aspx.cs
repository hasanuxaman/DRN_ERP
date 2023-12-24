using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesOrderRptMpo : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        string empRef = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            if (Session["sessionEmpRef"] != null)
                empRef = Session["sessionEmpRef"].ToString();

            txtFromDateSo.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            txtToDateSo.Text = DateTime.Now.ToString("dd/MM/yyyy");

            try
            {                
                var queryString = "";
                queryString = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[TEMP_View_Sales_Tree_Mpo_" + empRef.ToString() + "]')) DROP VIEW [dbo].[TEMP_View_Sales_Tree_Mpo_" + empRef.ToString() + "]";
                dbCon.ExecuteSQLStmt(queryString);

                queryString = "CREATE VIEW TEMP_View_Sales_Tree_Mpo_" + empRef.ToString() + " as WITH SalesTree AS (SELECT Sp_User_Ref as Sp_User_Ref_Tree " +
                                      " FROM	tblSalesPersonMas WHERE	Sp_User_Ref ='" + empRef.ToString() + "' UNION ALL SELECT	tblSalesPersonMas.Sp_User_Ref FROM	" +
                                      " tblSalesPersonMas JOIN SalesTree ON tblSalesPersonMas.Sp_Supr_Ref = SalesTree.Sp_User_Ref_Tree) SELECT Sp_User_Ref_Tree FROM	SalesTree";
                dbCon.ExecuteSQLStmt(queryString);

                queryString = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[TEMP_View_Sales_Tree_Mpo_Dealer_" + empRef.ToString() + "]')) DROP VIEW [dbo].[TEMP_View_Sales_Tree_Mpo_Dealer_" + empRef.ToString() + "]";
                dbCon.ExecuteSQLStmt(queryString);

                queryString = "CREATE VIEW TEMP_View_Sales_Tree_Mpo_Dealer_" + empRef.ToString() + " as select * from TEMP_View_Sales_Tree_Mpo_" + empRef.ToString() +
                              " left outer join tblSalesPersonMas on TEMP_View_Sales_Tree_Mpo_" + empRef.ToString() + ".Sp_User_Ref_Tree=tblSalesPersonMas.Sp_User_Ref " +
                              " left outer join tblSalesPartyAdr on tblSalesPersonMas.Sp_Ref=tblSalesPartyAdr.Par_Adr_Ext_Data2";
                dbCon.ExecuteSQLStmt(queryString);

                //SqlConnection connection = new SqlConnection();
                //var ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
                //connection.ConnectionString = ConnectionString;
                //connection.Open();
                //var qryStr = "Select * from TEMP_View_Sales_Tree_Mpo_Dealer_" + empRef.ToString();
                //var MpoList = "";
                //SqlDataAdapter daCsSum = new SqlDataAdapter(qryStr, connection);
                //DataTable dtCsSum = new DataTable();
                //daCsSum.Fill(dtCsSum);
                //for (int i = 0; i < dtCsSum.Rows.Count; i++)
                //{
                //    MpoList = MpoList + "'" + dtCsSum.Rows[i]["Par_Adr_Ext_Data2"].ToString() + "',";
                //}
                //MpoList = "(" + MpoList.Substring(0, MpoList.Length - 1) + ")";

                //var taMpo = new tblSalesPersonMasTableAdapter();
                //var dtMpo = taMpo.GetDataByEmpRef(empRef.ToString());
                //if (dtMpo.Rows.Count > 0)
                //{
                //    AutoCompleteExtenderSrch.ContextKey = empRef.ToString() + ":" + MpoList.ToString();
                //}
                //else
                //{
                //    AutoCompleteExtenderSrch.ContextKey = "0";
                //}

                AutoCompleteExtenderSrch.ContextKey = empRef.ToString();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Sales Tree Assignment Error.";
                tblMsg.Rows[1].Cells[0].InnerText = ex.Message.ToString();
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void btnShowSoRpt_Click(object sender, EventArgs e)
        {
            if (optRpt.SelectedValue == "6")//--------Customer Ledger
            {
                if (txtSearch.Text.Trim().Length <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Please enter customer name first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
            }

            if (optRpt.SelectedValue == "8")
            {
                reportInfo();
                var url = "frmCustomerListByMpo.aspx";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
            }
            else
            {
                reportInfo();
                var url = "frmShowSalesReport.aspx";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
            }
        }

        protected void reportInfo()
        {
            if (txtFromDateSo.Text.Trim().Length > 0 && txtToDateSo.Text.Trim().Length > 0)
            {
                #region MPO List
                if (Session["sessionEmpRef"] != null)
                    empRef = Session["sessionEmpRef"].ToString();
                SqlConnection connection = new SqlConnection();
                var ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
                connection.ConnectionString = ConnectionString;
                connection.Open();

                var qryStr = "Select * from TEMP_View_Sales_Tree_Mpo_Dealer_" + empRef.ToString();
                var MpoList = "";
                SqlDataAdapter daCsSum = new SqlDataAdapter(qryStr, connection);
                DataTable dtCsSum = new DataTable();
                daCsSum.Fill(dtCsSum);
                for (int i = 0; i < dtCsSum.Rows.Count; i++)
                {
                    MpoList = MpoList + "'" + dtCsSum.Rows[i]["Par_Adr_Ext_Data2"].ToString() + "',";
                }
                MpoList = "[" + MpoList.Substring(0, MpoList.Length - 1) + "]";
                #endregion                

                #region Get Customer Ref
                var custRef = "";
                var custAccRef = "";
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
                            if (dtPartyAdr.Rows.Count > 0)
                            {
                                custRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                                custAccRef = dtPartyAdr[0].Par_Adr_Acc_Code.ToString();
                            }
                        }
                    }
                }
                #endregion

                if (optRpt.SelectedValue == "1")//--------Sales Order (S/O)
                {
                    if (custRef == "")
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P' and {View_Sales_Details.Par_Adr_Ext_Data2} in " + MpoList;
                    }
                    else
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P' and {View_Sales_Details.SO_Hdr_Pcode}='" + custRef + "'";
                    }
                    rptFile = "~/Module/Sales/Reports/rptSalesOrdByCust.rpt";
                }

                if (optRpt.SelectedValue == "2")//--------Pending D/O
                {
                    if (custRef == "")
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P' and {View_Sales_Details.SO_Det_DO_Bal_Qty}>0 and {View_Sales_Details.Par_Adr_Ext_Data2} in " + MpoList;
                    }
                    else
                    {
                        rptSelcFormula = "{View_Sales_Details.SO_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Details.SO_Hdr_HPC_Flag}='P' and {View_Sales_Details.SO_Det_DO_Bal_Qty}>0 and {View_Sales_Details.SO_Hdr_Pcode}='" + custRef + "'";
                    }
                    rptFile = "~/Module/Sales/Reports/rptSalesOrdByPendDo.rpt";
                }

                if (optRpt.SelectedValue == "3")//--------D/O Created
                {
                    if (custRef == "")
                    {
                        rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and {tblSalesPartyAdr.Par_Adr_Ext_Data2} in " + MpoList;
                    }
                    else
                    {
                        rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and {View_Sales_Do.SO_Hdr_Pcode}='" + custRef + "'";
                    }
                    rptFile = "~/Module/Sales/Reports/rptDelOrdCustDet.rpt";
                }

                if (optRpt.SelectedValue == "4")//--------Pending Delivery
                {
                    if (custRef == "")
                    {
                        rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and ({View_Sales_Do.DO_Det_Bal_Qty}>0 or tonumber({View_Sales_Do.DO_Det_Ext_Data2})>0) "
                        + "and {tblSalesPartyAdr.Par_Adr_Ext_Data2} in " + MpoList;
                    }
                    else
                    {
                        rptSelcFormula = "{View_Sales_Do.DO_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Do.DO_Hdr_HPC_Flag}='P' and {View_Sales_Do.SO_Hdr_Pcode}='" + custRef
                        + "' and ({View_Sales_Do.DO_Det_Bal_Qty}>0 or tonumber({View_Sales_Do.DO_Det_Ext_Data2})>0)";
                    }
                    rptFile = "~/Module/Sales/Reports/rptDelOrdPendCustDet.rpt";
                }

                if (optRpt.SelectedValue == "5")//--------Collection
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
                        rptSelcFormula = "{View_Collection_Det.Par_Adr_Ext_Data2} in " + MpoList;
                    }
                    else
                    {
                        rptSelcFormula = "{View_Collection_Det.Trn_DATE} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim() + "') " +
                            "and {View_Collection_Det.Par_Adr_Ref}=" + custRef + "";
                    }

                    rptFile = "~/Module/Accounts/Reports/rptCollectionDet.rpt";
                }

                if (optRpt.SelectedValue == "6")//--------Customer Ledger
                {                    
                    var qrySqlStr = "";
                    qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_GL_Open_Bal]')) DROP VIEW [dbo].[View_GL_Open_Bal]";
                    dbCon.ExecuteSQLStmt(qrySqlStr);

                    qrySqlStr = "Create view View_GL_Open_Bal as select tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code as Opn_Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name as Opn_Gl_Coa_Name, " +
                                "SUM(DebitAmount) as OpnDr,SUM(CreditAmount) as OpnCr,SUM(DebitAmount) - SUM(CreditAmount) as OpnBal " +
                                "from tbl_Acc_Fa_Gl_Coa left outer join View_Acc_Tran_Det on Gl_Coa_Code =Trn_Ac_Code " +
                                "where CONVERT(date,Trn_DATE,103)< CONVERT(date,'" + txtFromDateSo.Text.Trim() + "',103) " +
                                "group by tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name";
                    dbCon.ExecuteSQLStmt(qrySqlStr);

                    qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_GL_Close_Bal]')) DROP VIEW [dbo].[View_GL_Close_Bal]";
                    dbCon.ExecuteSQLStmt(qrySqlStr);

                    qrySqlStr = "Create view View_GL_Close_Bal as select tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code as Opn_Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name as Opn_Gl_Coa_Name, " +
                                "SUM(DebitAmount) as OpnDr,SUM(CreditAmount) as OpnCr,SUM(DebitAmount) - SUM(CreditAmount) as OpnBal " +
                                "from tbl_Acc_Fa_Gl_Coa left outer join View_Acc_Tran_Det on Gl_Coa_Code =Trn_Ac_Code " +
                                "where CONVERT(date,Trn_DATE,103)<= CONVERT(date,'" + txtToDateSo.Text.Trim() + "',103) " +
                                "group by tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name";
                    dbCon.ExecuteSQLStmt(qrySqlStr);

                    rptSelcFormula = "{View_Acc_Tran_Det_New.Trn_DATE} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim() + "') " +
                                     "and {View_Acc_Tran_Det_New.Trn_Ac_Code}='" + custAccRef.ToString() + "'";

                    rptFile = "~/Module/Sales/Reports/rptCustLed.rpt";
                }           

                if (optRpt.SelectedValue == "7")//--------Delivery Challan
                {
                    if (custRef == "")
                    {
                        rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Party_Adr.Par_Adr_Ext_Data2} in " + MpoList;
                    }
                    else
                    {
                        rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                        + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.Trn_Hdr_Pcode}='" + custRef + "'";
                    }
                    rptFile = "~/Module/Sales/Reports/rptDelChlnListCust.rpt";
                }

                //if (optRpt.SelectedValue == "8")//--------Customer List
                //{
                //    if (custRef == "")
                //    {
                //        rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                //        + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Party_Adr.Par_Adr_Ext_Data2} in " + MpoList;
                //    }
                //    else
                //    {
                //        rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Date} in Date('" + txtFromDateSo.Text.Trim() + "') to Date ('" + txtToDateSo.Text.Trim()
                //        + "') and {View_Sales_Do_Chln.Trn_Hdr_HRPB_Flag}='P' and {View_Sales_Do_Chln.Trn_Hdr_Pcode}='" + custRef + "'";
                //    }
                //    rptFile = "~/Module/Sales/Reports/rptDelChlnListCust.rpt";
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