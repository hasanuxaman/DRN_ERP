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
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmSuppLedger : System.Web.UI.Page
    {
        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        double totDrAmt = 0;
        double totCrAmt = 0;

        string rptFile;
        string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            AutoCompleteExtenderSrch.ContextKey = "S";

            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                var coaCode = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    coaCode = word;
                    break;
                }

                if (coaCode.Length > 0)
                {
                    var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtCoa = taCoa.GetDataByCoaCode(coaCode.ToString());
                    if (dtCoa.Rows.Count > 0)
                    {
                        var taFaTe = new View_Acc_Tran_DetTableAdapter();

                        var dtOpnCr = taFaTe.GetTotOpnCr(coaCode.ToString(), txtFromDate.Text.Trim());
                        var opnCr = dtOpnCr == null ? 0 : Convert.ToDouble(dtOpnCr);
                        var dtOpnDr = taFaTe.GetTotOpnDr(coaCode.ToString(), txtFromDate.Text.Trim());
                        var opnDr = dtOpnDr == null ? 0 : Convert.ToDouble(dtOpnDr);
                        var openingBalance = (opnDr - opnCr);
                        lblOpenBal.Text = "Opening Balance: " + openingBalance.ToString("N2");

                        var taFaTeNew = new View_Acc_Tran_Det_NewTableAdapter();
                        var dtFaTe = taFaTeNew.GetDataByDateRange(coaCode.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                        gvCustLed.DataSource = dtFaTe;
                        gvCustLed.DataBind();

                        var dtTotCr = taFaTe.GetTotCr(coaCode.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                        var totCr = dtTotCr == null ? 0 : Convert.ToDouble(dtTotCr);
                        var dtTotDr = taFaTe.GetTotDr(coaCode.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                        var totDr = dtTotDr == null ? 0 : Convert.ToDouble(dtTotDr);
                        var closingBalance = ((openingBalance + totDr) - totCr);
                        lblCloseBal.Text = "Closing Balance: " + closingBalance.ToString("N2");

                        lblCloseBal1.Text = closingBalance.ToString("N2");
                        var taCust = new tblSalesPartyAdrTableAdapter();
                        var dtCust = taCust.GetDataByPartyAccRef(coaCode.ToString());
                        var taCrRealize = new tblSalesCreditRealizeTableAdapter();
                        var dtCrRealize = taCrRealize.GetTotAmtByCustRef(dtCust.Rows.Count > 0 ? dtCust[0].Par_Adr_Ref.ToString() : "");
                        lblAegingBal.Text = dtCrRealize == null ? "0" : Convert.ToDouble(dtCrRealize).ToString("N2");

                        if (closingBalance > 0)
                            txtDiff.Text = (Convert.ToDouble(lblAegingBal.Text) - Convert.ToDouble(lblCloseBal1.Text)).ToString();
                        else
                            txtDiff.Text = (Convert.ToDouble(lblCloseBal1.Text) - Convert.ToDouble(lblAegingBal.Text)).ToString();

                        txtSearch.Enabled = false;
                        //btnSearch.Enabled = false;
                        btnClearSrch.Enabled = true;
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "GL data not found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "GL data not found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                txtSearch.Enabled = true;
                //btnSearch.Enabled = true;
                btnClearSrch.Enabled = false;

                lblOpenBal.Text = "";
                lblCloseBal.Text = "";

                txtDiff.Text = "";

                var taFaTe = new View_Acc_Tran_Det_NewTableAdapter();
                var dtFaTe = taFaTe.GetDataByDateRange("", "", "");
                gvCustLed.DataSource = dtFaTe;
                gvCustLed.DataBind();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvCustLed_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblDr = ((Label)e.Row.FindControl("lblDrAmt"));
                totDrAmt += Convert.ToDouble(lblDr.Text.Trim());

                var lblCr = (Label)e.Row.FindControl("lblCrAmt");
                totCrAmt += Convert.ToDouble(lblCr.Text.Trim());

                var hfJrnType = ((HiddenField)e.Row.FindControl("hfJrnType")).Value.Trim();
                var lnkJvEdit = ((LinkButton)e.Row.FindControl("lnkJvEdit"));

                var taJrnType = new tbl_Acc_Fa_Jv_TypeTableAdapter();
                var dtJrnType = taJrnType.GetDataByJvCode(hfJrnType);
                if (dtJrnType.Rows.Count > 0)
                {
                    if (dtJrnType[0].JV_Type_Entry_Type.ToString() == "M")
                    {
                        var EmpRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                        var taEditPerm = new tbl_Acc_User_PermTableAdapter();
                        var dtEditPerm = taEditPerm.GetDataByEmpRef(EmpRef.ToString());
                        if (dtEditPerm.Rows.Count > 0)
                        {
                            if (dtEditPerm[0].Acc_Perm_Edit_Perm == "Y")
                            {
                                lnkJvEdit.Text = "Edit";
                                lnkJvEdit.Enabled = true;
                            }
                            else
                            {
                                lnkJvEdit.Text = "N/A";
                                lnkJvEdit.Enabled = false;
                            }
                        }
                        else
                        {
                            lnkJvEdit.Text = "N/A";
                            lnkJvEdit.Enabled = false;
                        }
                    }
                    else
                    {
                        lnkJvEdit.Text = "N/A";
                        lnkJvEdit.Enabled = false;
                    }
                }
                else
                {
                    lnkJvEdit.Text = "N/A";
                    lnkJvEdit.Enabled = false;
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotDr = (Label)e.Row.FindControl("lblTotDrAmt");
                lblTotDr.Text = totDrAmt.ToString("N2");

                Label lblTotCr = (Label)e.Row.FindControl("lblTotCrAmt");
                lblTotCr.Text = totCrAmt.ToString("N2");
            }
        }

        protected void btnUpdtAegingBal_Click(object sender, EventArgs e)
        {

            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                var coaCode = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    coaCode = word;
                    break;
                }

                if (coaCode.Length > 0)
                {
                    var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtCoa = taCoa.GetDataByCoaCode(coaCode.ToString());
                    if (dtCoa.Rows.Count > 0)
                    {
                        if ((Convert.ToDouble(txtDiff.Text.Trim())) > 0)
                        {
                            #region Credit Realization Entry
                            var taCust = new tblSalesPartyAdrTableAdapter();
                            var dtCust = taCust.GetDataByPartyAccRef(coaCode.ToString());

                            var taCrReal = new tblSalesCreditRealizeTableAdapter();
                            var rcvAmt = Convert.ToDecimal(txtDiff.Text.Trim());
                            var dtCrReal = taCrReal.GetPendChlnByCustRef(dtCust[0].Par_Adr_Ref.ToString());
                            foreach (dsSalesTran.tblSalesCreditRealizeRow dr in dtCrReal.Rows)
                            {
                                if (rcvAmt > dr.Sales_Chln_Due_Amt)
                                {
                                    rcvAmt = rcvAmt - dr.Sales_Chln_Due_Amt;
                                    taCrReal.UpdateCreditRealizeAmt(0, "", dr.Sales_Chln_Ref);
                                }
                                else
                                {
                                    taCrReal.UpdateCreditRealizeAmt(dr.Sales_Chln_Due_Amt - rcvAmt, "", dr.Sales_Chln_Ref);
                                    rcvAmt = 0;
                                    break;
                                }
                            }
                            #endregion

                            tblMsg.Rows[0].Cells[0].InnerText = "Updated Successfully.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "GL data not found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "GL data not found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void lnkClnRef_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            var lnkClnRef = ((LinkButton)row.FindControl("lnkClnRef")).Text.Trim();
            Session["ChlnNoPrint"] = lnkClnRef.ToString();

            try
            {
                ChlnReportInfo(lnkClnRef.ToString());

                //Response.Redirect("~/Module/Sales/Forms/frmShowSalesReport.aspx");
                var url = "frmShowSalesReportAcc.aspx";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
            }
            catch (Exception ex) { }
        }

        protected void ChlnReportInfo(string chlnRefNo)
        {
            var taInTrTrnHdr = new tbl_InTr_Trn_HdrTableAdapter();

            try
            {
                rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_Cno}='" + chlnRefNo.ToString() + "'";

                var dtInTrTrnHdr = taInTrTrnHdr.GetDataByChlnRefNo(chlnRefNo.ToString());
                if (dtInTrTrnHdr.Rows.Count > 0)
                {
                    if (dtInTrTrnHdr[0].Trn_Hdr_EI_Flg == "D")
                        rptFile = "~/Module/Sales/Reports/rptDelChlnRePrint.rpt";
                    else
                        rptFile = "~/Module/Sales/Reports/rptDelChln.rpt";
                }

                Session["RptDateFrom"] = DateTime.Now.ToString("dd/MM/yyyy");
                Session["RptDateTo"] = DateTime.Now.ToString("dd/MM/yyyy");
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
            }
            catch (Exception ex) { }
        }

        protected void lnkDoRef_Click(object sender, EventArgs e)
        {
            //GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            //var CsRefNo = ((LinkButton)row.FindControl("lnkCsRef")).Text.Trim();
            //Session["CsRefNoPrint"] = CsRefNo.ToString();
            //var url = "frmCsInq.aspx";
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void lnkOrdRef_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            var lnkOrdRef = ((LinkButton)row.FindControl("lnkOrdRef")).Text.Trim();

            try
            {
                OrdReportInfo(lnkOrdRef.ToString());

                //Response.Redirect("~/Module/Sales/Forms/frmShowSalesReport.aspx");
                var url = "frmShowSalesReportAcc.aspx";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
            }
            catch (Exception ex) { }
        }

        protected void OrdReportInfo(string ordRefNo)
        {
            rptSelcFormula = "{View_Sales_Details.SO_Hdr_Ref_No}='" + ordRefNo.ToString() + "'";

            rptFile = "~/Module/Sales/Reports/rptSalesOrd.rpt";

            Session["RptDateFrom"] = DateTime.Now.ToString("dd/MM/yyyy");
            Session["RptDateTo"] = DateTime.Now.ToString("dd/MM/yyyy");
            Session["RptFilePath"] = rptFile;
            Session["RptFormula"] = rptSelcFormula;
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            CustLedReportInfo();
            var url = "frmShowAccReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void CustLedReportInfo()
        {
            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                var coaCode = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    coaCode = word;
                    break;
                }

                if (coaCode.Length > 0)
                {
                    var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtCoa = taCoa.GetDataByCoaCode(coaCode.ToString());
                    if (dtCoa.Rows.Count > 0)
                    {
                        coaCode = dtCoa[0].Gl_Coa_Code.ToString();
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "GL data not found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "GL data not found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }


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

                    qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_GL_Close_Bal]')) DROP VIEW [dbo].[View_GL_Close_Bal]";
                    dbCon.ExecuteSQLStmt(qrySqlStr);

                    qrySqlStr = "Create view View_GL_Close_Bal as select tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code as Opn_Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name as Opn_Gl_Coa_Name, " +
                                "SUM(DebitAmount) as OpnDr,SUM(CreditAmount) as OpnCr,SUM(DebitAmount) - SUM(CreditAmount) as OpnBal " +
                                "from tbl_Acc_Fa_Gl_Coa left outer join View_Acc_Tran_Det on Gl_Coa_Code =Trn_Ac_Code " +
                                "where CONVERT(date,Trn_DATE,103)<= CONVERT(date,'" + txtToDate.Text.Trim() + "',103) " +
                                "group by tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name";
                    dbCon.ExecuteSQLStmt(qrySqlStr);

                    rptSelcFormula = "{View_Acc_Tran_Det_New.Trn_DATE} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim() + "') " +
                                     "and {View_Acc_Tran_Det_New.Trn_Ac_Code}='" + coaCode.ToString() + "'";

                    rptFile = "~/Module/Accounts/Reports/rptCustLed.rpt";

                    Session["RptDateFrom"] = txtFromDate.Text.Trim();
                    Session["RptDateTo"] = txtToDate.Text.Trim();
                    Session["RptFilePath"] = rptFile;
                    Session["RptFormula"] = rptSelcFormula;
                    Session["RptHdr"] = "";
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void lnkJvView_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            var lblTrnRefNo = ((Label)row.FindControl("lblTrnRefNo")).Text.Trim();
            var JvRefNoView = lblTrnRefNo.ToString();
            var url = "frmVoucherView.aspx?JvRefNoView=" + JvRefNoView.ToString();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void lnkJvEdit_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            var lblTrnRefNo = ((Label)row.FindControl("lblTrnRefNo")).Text.Trim();
            var JvRefNoEdit = lblTrnRefNo.ToString();
            var url = "frmVoucherEdit.aspx?JvRefNoEdit=" + JvRefNoEdit.ToString();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            try
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
                                "on Gl_Coa_Code=Trn_Gl_Coa_Code where Gl_Coa_Type='S'";
                    dbCon.ExecuteSQLStmt(qrySqlStr);

                    rptFile = "~/Module/Accounts/Reports/rptGLAccSum.rpt";

                    rptSelcFormula = "";

                    Session["RptDateFrom"] = txtFromDate.Text.Trim();
                    Session["RptDateTo"] = txtToDate.Text.Trim();
                    Session["RptFilePath"] = rptFile;
                    Session["RptFormula"] = rptSelcFormula;
                    Session["RptHdr"] = "Supplier Group Summary from " + txtFromDate.Text.Trim() + " to " + txtToDate.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }

            var url = "frmShowAccReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }
    }
}