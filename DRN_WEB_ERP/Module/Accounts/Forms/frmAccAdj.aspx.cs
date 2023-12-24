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
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;

using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmAccAdj : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtEntDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var dtNewMaxAccRef = taAcc.GetMaxRefNo("ADJ", Convert.ToDateTime(txtEntDate.Text.Trim()).Year);
            var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
            var nextNewAccRefNo = "ADJ" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
            txtTranRefNo.Text = nextNewAccRefNo.ToString();            

            txtNarration.Text = "Adjustment Entry";

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            if (empRef == "000011")
            {
                //btnUpdtCloseBal.Visible = true;
                //btnUpdtOpenBal.Visible = true;
                //btnUpdtOpenBalAllGl.Visible = true;
                //btnUpdtTallyOverheadGL.Visible = true;
                //btnUpdtOpenBalSup.Visible = true;

                //var taGlList = new TALLY_ALL_GL_CLOSE_BAL_31_10_19_NewTableAdapter();
                //var dtGlList = taGlList.GetDataBySubGrp2Status();
                //ddlTallyGlGroup.DataSource = dtGlList;
                //ddlTallyGlGroup.DataValueField = "Sub_Group1";
                //ddlTallyGlGroup.DataTextField = "Sub_Group1";
                //ddlTallyGlGroup.DataBind();
                //ddlTallyGlGroup.Visible = true;

                //var taGlList = new Tally_Accounts_Payable_31_10_19_NewTableAdapter();
                //var dtGlList = taGlList.GetDataBySupGrp2Status();
                //ddlTallySupGroup.DataSource = dtGlList;
                //ddlTallySupGroup.DataValueField = "Sub_Group1";
                //ddlTallySupGroup.DataTextField = "Sub_Group1";
                //ddlTallySupGroup.DataBind();
                //ddlTallySupGroup.Visible = true;
            }
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

                        var dtTotCr = taFaTe.GetAccTotalCr(coaCode.ToString());
                        var totCr = dtTotCr == null ? 0 : Convert.ToDecimal(dtTotCr);
                        var dtTotDr = taFaTe.GetAccTotalDr(coaCode.ToString());
                        var totDr = dtTotDr == null ? 0 : Convert.ToDecimal(dtTotDr);
                        var closingBalance = (totDr - totCr);
                        lblTotDr.Text = totDr.ToString("N2");
                        lblTotCr.Text = totCr.ToString("N2");
                        lblCloseBal.Text = closingBalance.ToString("N2");

                        txtSearch.Enabled = false;
                        btnSearch.Enabled = false;
                        btnClearSrch.Enabled = true;
                        pnlDet.Enabled = true;
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
                btnSearch.Enabled = true;
                btnClearSrch.Enabled = false;
                lblTotDr.Text = "0.00";
                lblTotCr.Text = "0.00";
                lblCloseBal.Text = "0.00";
                txtEntDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtNarration.Text = "Adjustment Entry";
                txtTranAmt.Text = "";
                pnlDet.Enabled = false;

                var taAcc = new tbl_Acc_Fa_TeTableAdapter();
                var dtNewMaxAccRef = taAcc.GetMaxRefNo("ADJ", Convert.ToDateTime(txtEntDate.Text.Trim()).Year);
                var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
                var nextNewAccRefNo = "ADJ" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
                txtTranRefNo.Text = nextNewAccRefNo.ToString();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAcc.Connection);

            try
            {
                if (txtNarration.Text.Trim().Length <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter narration first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                #region Validate and Get Coa Data
                var coaCode = "";
                var coaName = "";
                var coaType = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    coaCode = word;
                    break;
                }

                if (coaCode.Length > 0)
                {
                    var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtCoa = taCoa.GetDataByCoaCode(coaCode);
                    if (dtCoa.Rows.Count > 0)
                    {
                        coaCode = dtCoa[0].Gl_Coa_Code.ToString();
                        coaName = dtCoa[0].Gl_Coa_Name.ToString();
                        coaType = dtCoa[0].Gl_Coa_Type.ToString();
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Account Code.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Account Code.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                taAcc.AttachTransaction(myTran);                

                #region Adjustment Entry
                var dtMaxAccRef = taAcc.GetMaxRefNo("ADJ", Convert.ToDateTime(txtEntDate.Text.Trim()).Year);
                var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                var nextAccRefNo = "ADJ" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                taAcc.InsertAccData(coaCode.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                    nextAccRefNo.ToString(), 1, 1, txtNarration.Text.Trim(), ddlTrnType.SelectedValue.ToString(), Convert.ToDecimal(txtTranAmt.Text.Trim()),
                    "", "0", "BDT", 1, Convert.ToDecimal(txtTranAmt.Text.Trim()),
                    "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", Convert.ToDateTime(txtEntDate.Text.Trim()), coaName.ToString(),
                    Convert.ToDateTime(txtEntDate.Text.Trim()), "ADM", coaType, "",
                    DateTime.Now, "ADJ", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(txtTranAmt.Text.Trim()), "",
                    "", "", "N", 1, 0, "", "", "", "J", 0, "1", "ADJ");
                #endregion

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Adjustment entry successful.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                txtSearch.Text = "";
                txtSearch.Enabled = true;
                btnSearch.Enabled = true;
                btnClearSrch.Visible = false;
                lblTotDr.Text = "0.00";
                lblTotCr.Text = "0.00";
                lblCloseBal.Text = "0.00";
                txtEntDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtNarration.Text = "Opening Balance Entry";
                txtTranAmt.Text = "";
                pnlDet.Enabled = false;

                var dtNewMaxAccRef = taAcc.GetMaxRefNo("ADJ", Convert.ToDateTime(txtEntDate.Text.Trim()).Year);
                var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
                var nextNewAccRefNo = "ADJ" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
                txtTranRefNo.Text = nextNewAccRefNo.ToString();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void txtEntDate_TextChanged(object sender, EventArgs e)
        {
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var dtNewMaxAccRef = taAcc.GetMaxRefNo("ADJ", Convert.ToDateTime(txtEntDate.Text.Trim()).Year);
            var nextNewAccRef = dtNewMaxAccRef == null ? "000001" : (Convert.ToInt32(dtNewMaxAccRef) + 1).ToString();
            var nextNewAccRefNo = "ADJ" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextNewAccRef).ToString("000000");
            txtTranRefNo.Text = nextNewAccRefNo.ToString();
        }

        protected void btnUpdtCloseBal_Click(object sender, EventArgs e)
        {
             GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

            var taAcc = new tbl_Acc_Fa_TeTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAcc.Connection);

            try
            {               
                var qrySqlStr = "";
                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_GL_Close_Bal]')) DROP VIEW [dbo].[View_GL_Close_Bal]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "Create view View_GL_Close_Bal as select tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code as Opn_Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name as Opn_Gl_Coa_Name, " +
                            "SUM(DebitAmount) as OpnDr,SUM(CreditAmount) as OpnCr,SUM(DebitAmount) - SUM(CreditAmount) as OpnBal " +
                            "from tbl_Acc_Fa_Gl_Coa left outer join View_Acc_Tran_Det on Gl_Coa_Code =Trn_Ac_Code " +
                            "where CONVERT(date,Trn_DATE,103)<= CONVERT(date,'31/10/2019',103) " +
                            "group by tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                
                taAcc.AttachTransaction(myTran);

                var dtMaxAccRef = taAcc.GetMaxRefNo("ADJ", Convert.ToDateTime("31/10/2019").Year);
                var nextTranRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                var nextTrnRefNo = "ADJ" + (Convert.ToDateTime("31/10/2019").Month.ToString("00") + Convert.ToDateTime("31/10/2019").ToString("yy")).ToString() + "-" + Convert.ToInt32(nextTranRef).ToString("000000");                

                var iCount = 1;
                var taCloseBal = new View_GL_Close_BalTableAdapter();
                var dtCloseBal = taCloseBal.GetDataByBankClosing();
                foreach (dsAccTran.View_GL_Close_BalRow dr in dtCloseBal.Rows)
                {
                    var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtCoa = taCoa.GetDataByCoaCode(dr.Opn_Gl_Coa_Code);
                    if (dtCoa.Rows.Count > 0)
                    {
                        taAcc.InsertAccData(dr.Opn_Gl_Coa_Code.ToString(), (Convert.ToDateTime("31/10/2019").Month.ToString("00") + "/" + Convert.ToDateTime("31/10/2019").Year.ToString()).ToString(),
                            nextTrnRefNo.ToString(), iCount, 1, "Closing Balance Adjustment As On " + DateTime.Now.ToString("31/10/2019"),
                            Convert.ToDecimal(dr.OpnBal.ToString()) > 0 ? "C" : "D", Convert.ToDecimal(dr.OpnBal.ToString()),
                            "", "0", "BDT", 1, Convert.ToDecimal(dr.OpnBal.ToString()),
                            "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, dtCoa[0].Gl_Coa_Name.ToString(),
                            Convert.ToDateTime("31/10/2019"), "ADM", dtCoa[0].Gl_Coa_Type.ToString(), "",
                            DateTime.Now, "ADJ", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(dr.OpnBal.ToString()), "",
                            "", "", "N", 1, 0, "", "", "", "J", 0, "1", "ADJ");
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Account Code.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    iCount++;
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Opening balance entry successful. total (" + iCount.ToString() + ")";
                tblMsg.Rows[1].Cells[0].InnerText = "Voucher Ref. No" + nextTrnRefNo.ToString();
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnUpdtOpenBal_Click(object sender, EventArgs e)
        {
            var taCloseBal = new Tally_Bank_Closing_Bal_31_10_19TableAdapter();
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAcc.Connection);

            try
            {
                taAcc.AttachTransaction(myTran);
                taCloseBal.AttachTransaction(myTran);

                var dtMaxAccRef = taAcc.GetMaxRefNo("OPN", Convert.ToDateTime("01/11/2019").Year);
                var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                var nextAccRefNo = "OPN" + (Convert.ToDateTime("01/11/2019").Month.ToString("00") + Convert.ToDateTime("01/11/2019").ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                var iCount = 1;
                var dtCloseBal = taCloseBal.GetData();
                foreach (dsAccTran.Tally_Bank_Closing_Bal_31_10_19Row dr in dtCloseBal.Rows)
                {
                    var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtCoa = taCoa.GetDataByCoaCode(dr.Bank_GL_Code);
                    if (dtCoa.Rows.Count > 0)
                    {
                        taAcc.InsertAccData(dr.Bank_GL_Code.ToString(), (Convert.ToDateTime("01/11/2019").Month.ToString("00") + "/" + Convert.ToDateTime("01/11/2019").Year.ToString()).ToString(),
                            nextAccRefNo.ToString(), iCount, 1, "Opening Balance As On " + DateTime.Now.ToString("01/11/2019"),
                            "D", Convert.ToDecimal(dr.Trn_Amount.ToString()),
                            "", "0", "BDT", 1, Convert.ToDecimal(dr.Trn_Amount.ToString()),
                            "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, dtCoa[0].Gl_Coa_Name.ToString(),
                            Convert.ToDateTime("01/11/2019"), "ADM", dtCoa[0].Gl_Coa_Type.ToString(), "",
                            DateTime.Now, "OPN", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(dr.Trn_Amount.ToString()), "",
                            "", "", "N", 1, 0, "", "", "", "J", 0, "1", "OPN");
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Account Code.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    iCount++;
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Opening balance entry successful. total (" + iCount.ToString() + ")";
                tblMsg.Rows[1].Cells[0].InnerText = "Tran Ref. No" + nextAccRefNo.ToString();
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnUpdtOpenBalAllGl_Click(object sender, EventArgs e)
        {
            GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var taCloseBal = new TALLY_ALL_GL_CLOSE_BAL_31_10_19TableAdapter();
            var taGlCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();

            var taFaTeCloseBal = new View_GL_Close_BalTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAcc.Connection);

            try
            {
                var qrySqlStr = "";
                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_GL_Close_Bal]')) DROP VIEW [dbo].[View_GL_Close_Bal]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "Create view View_GL_Close_Bal as select tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code as Opn_Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name as Opn_Gl_Coa_Name, " +
                            "SUM(DebitAmount) as OpnDr,SUM(CreditAmount) as OpnCr,SUM(DebitAmount) - SUM(CreditAmount) as OpnBal " +
                            "from tbl_Acc_Fa_Gl_Coa left outer join View_Acc_Tran_Det on Gl_Coa_Code =Trn_Ac_Code " +
                            "where CONVERT(date,Trn_DATE,103)<= CONVERT(date,'31/10/2019',103) " +
                            "group by tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                taAcc.AttachTransaction(myTran);
                taCloseBal.AttachTransaction(myTran);
                taGlCoa.AttachTransaction(myTran);
                taFaTeCloseBal.AttachTransaction(myTran);

                //var taCloseBalGrpList = new TALLY_ALL_GL_CLOSE_BAL_31_10_19_NewTableAdapter();
                //var dtCloseBalGrpList = taCloseBalGrpList.GetData();

                var totCount = 0;

                //var iCountOpn = 1;
                //foreach (dsAccTran.TALLY_ALL_GL_CLOSE_BAL_31_10_19_NewRow drNew in dtCloseBalGrpList.Rows)
                //{
                var dtMaxAccRef = taAcc.GetMaxRefNo("OPN", Convert.ToDateTime("31/10/2019").Year);
                var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                var nextAccRefNo = "OPN" + (Convert.ToDateTime("31/10/2019").Month.ToString("00") + Convert.ToDateTime("31/10/2019").ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                var dtMaxAccRefNew = taAcc.GetMaxRefNo("ADJ", Convert.ToDateTime("31/10/2019").Year);
                var nextTranRefNew = dtMaxAccRefNew == null ? "000001" : (Convert.ToInt32(dtMaxAccRefNew) + 1).ToString();
                var nextTrnRefNoNew = "ADJ" + (Convert.ToDateTime("31/10/2019").Month.ToString("00") + Convert.ToDateTime("31/10/2019").ToString("yy")).ToString() + "-" + Convert.ToInt32(nextTranRefNew).ToString("000000");

                #region test
                var iCountOpn = 1;
                var iCountAdj = 1;
                var dtCloseBal = taCloseBal.GetDataByGrpName(ddlTallyGlGroup.SelectedItem.ToString());
                foreach (dsAccTran.TALLY_ALL_GL_CLOSE_BAL_31_10_19Row dr in dtCloseBal.Rows)
                {
                    var trnType = Convert.ToDecimal(dr.Closing_Bal) > 0 ? "D" : "C";
                    var trnAmt = Convert.ToDecimal(dr.Closing_Bal) > 0 ? (dr.Closing_Bal) : (dr.Closing_Bal * (-1));

                    var dtGlCoa = taGlCoa.GetDataByCoaName(dr.Ledger_Name.ToString().Trim());
                    if (dtGlCoa.Rows.Count > 0)
                    {
                        //---update closing balance                                                       
                        var dtFaTeCloseBal = taFaTeCloseBal.GetDataByGLCode(dtGlCoa[0].Gl_Coa_Code);
                        if (dtFaTeCloseBal.Rows.Count > 0)
                        {
                            taAcc.InsertAccData(dtGlCoa[0].Gl_Coa_Code, (Convert.ToDateTime("31/10/2019").Month.ToString("00") + "/" + Convert.ToDateTime("31/10/2019").Year.ToString()).ToString(),
                                nextTrnRefNoNew.ToString(), iCountAdj, 1, "Closing Balance Adjustment As On " + DateTime.Now.ToString("31/10/2019"),
                                Convert.ToDecimal(dtFaTeCloseBal[0].OpnBal.ToString()) > 0 ? "C" : "D", Convert.ToDecimal(dtFaTeCloseBal[0].OpnBal.ToString()),
                                "", "0", "BDT", 1, Convert.ToDecimal(dtFaTeCloseBal[0].OpnBal.ToString()),
                                "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, dtGlCoa[0].Gl_Coa_Name.ToString(),
                                Convert.ToDateTime("31/10/2019"), "ADM", dtGlCoa[0].Gl_Coa_Type.ToString(), "",
                                DateTime.Now, "ADJ", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(dtFaTeCloseBal[0].OpnBal.ToString()), "",
                                "", "", "N", 1, 0, "", "", "", "J", 0, "1", "ADJ");
                        }

                        //---update opening balance
                        taAcc.InsertAccData(dtGlCoa[0].Gl_Coa_Code, (Convert.ToDateTime("31/10/2019").Month.ToString("00") + "/" + Convert.ToDateTime("31/10/2019").Year.ToString()).ToString(),
                                nextAccRefNo.ToString(), iCountOpn, 1, "Opening Balance As On " + DateTime.Now.ToString("01/11/2019"),
                                 trnType, Convert.ToDecimal(trnAmt), "", "0", "BDT", 1, Convert.ToDecimal(trnAmt),
                                "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, dr.Ledger_Name.ToString().Trim(),
                                Convert.ToDateTime("31/10/2019"), "ADM", dtGlCoa[0].Gl_Coa_Type.ToString(), "",
                                DateTime.Now, "OPN", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(trnAmt), "",
                                "", "", "N", 1, 0, "", "", "", "J", 0, "1", "OPN");

                        //---update closing bal coa code update status
                        taCloseBal.UpdateGLCode("N", dtGlCoa[0].Gl_Coa_Code, dr.Ledger_Name.ToString());

                        //---update closing bal close bal update status
                        taCloseBal.UpdateGLBal("Y", dr.Ledger_Name.ToString());

                        iCountAdj++;
                        iCountOpn++;
                    }
                    else
                    {
                        //---insert coa code
                        var dtMaxCoaRef = taGlCoa.GetMaxCoaRef();
                        var nextCoaRef = dtMaxCoaRef == null ? 1 : Convert.ToInt32(dtMaxCoaRef) + 1;

                        var dtMaxCoaCode = taGlCoa.GetMaxCoaCode();
                        var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
                        var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");

                        taGlCoa.InsertCoa(nextCoaRef, nextCoaCode, dr.Ledger_Name.ToString().Trim(), nextCoaCode, "O", "B", "N",
                                Convert.ToDateTime("31/10/2019"), "N", "N", "BDT", DateTime.Now, "Y", "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        //---update closing bal coa code update status
                        taCloseBal.UpdateGLCode("Y", nextCoaCode, dr.Ledger_Name.ToString());

                        //---update opening balance                            
                        var dtCoa = taGlCoa.GetDataByCoaCode(nextCoaCode);
                        if (dtCoa.Rows.Count > 0)
                        {
                            taAcc.InsertAccData(nextCoaCode, (Convert.ToDateTime("31/10/2019").Month.ToString("00") + "/" + Convert.ToDateTime("31/10/2019").Year.ToString()).ToString(),
                                nextAccRefNo.ToString(), iCountOpn, 1, "Opening Balance As On " + DateTime.Now.ToString("01/11/2019"),
                                 trnType, Convert.ToDecimal(trnAmt), "", "0", "BDT", 1, Convert.ToDecimal(trnAmt),
                                "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, dr.Ledger_Name.ToString().Trim(),
                                Convert.ToDateTime("31/10/2019"), "ADM", dtCoa[0].Gl_Coa_Type.ToString(), "",
                                DateTime.Now, "OPN", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(trnAmt), "",
                                "", "", "N", 1, 0, "", "", "", "J", 0, "1", "OPN");

                            //---update closing bal close bal update status
                            taCloseBal.UpdateGLBal("Y", dr.Ledger_Name.ToString());

                            iCountOpn++;
                        }
                        else
                        {
                            myTran.Rollback();
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid GL Account Code.";
                            tblMsg.Rows[1].Cells[0].InnerText = dtCoa[0].Gl_Coa_Name;
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }

                    //iCountAdj++;

                    totCount++;
                }
                #endregion

                //    iCountOpn++;
                //}

                taCloseBal.UpdateSubGroup1UdtStat("Y", ddlTallyGlGroup.SelectedItem.ToString());

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully;";
                tblMsg.Rows[1].Cells[0].InnerText = totCount.ToString();
                ModalPopupExtenderMsg.Show();

                var taGlList = new TALLY_ALL_GL_CLOSE_BAL_31_10_19_NewTableAdapter();
                var dtGlList = taGlList.GetDataBySubGrp2Status();
                ddlTallyGlGroup.DataSource = dtGlList;
                ddlTallyGlGroup.DataValueField = "Sub_Group1";
                ddlTallyGlGroup.DataTextField = "Sub_Group1";
                ddlTallyGlGroup.DataBind();
                ddlTallyGlGroup.Visible = true;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnUpdtTallyOverheadGL_Click(object sender, EventArgs e)
        {
            var taGlCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var taOverhead = new Tally_Overhead_GL_ListTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taGlCoa.Connection);

            try
            {
                taGlCoa.AttachTransaction(myTran);
                taOverhead.AttachTransaction(myTran);

                #region test
                var totCount = 0;
                var dtOverhead = taOverhead.GetData();
                foreach (dsAccTran.Tally_Overhead_GL_ListRow dr in dtOverhead.Rows)
                {
                    var dtGlCoa = taGlCoa.GetDataByCoaName(dr.Ledger_Name.ToString().Trim());
                    if (dtGlCoa.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        //---insert coa code
                        var dtMaxCoaRef = taGlCoa.GetMaxCoaRef();
                        var nextCoaRef = dtMaxCoaRef == null ? 1 : Convert.ToInt32(dtMaxCoaRef) + 1;

                        var dtMaxCoaCode = taGlCoa.GetMaxCoaCode();
                        var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
                        var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");

                        taGlCoa.InsertCoa(nextCoaRef, nextCoaCode, dr.Ledger_Name.ToString().Trim(), nextCoaCode, "O", "B", "N",
                                Convert.ToDateTime("31/10/2019"), "N", "N", "BDT", DateTime.Now, "Y", "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        //---update closing bal close bal update status
                        taOverhead.UpdateStatus("Y", "N", nextCoaCode, dr.Ledger_Name.ToString());

                        totCount++;
                    }                    
                }
                #endregion

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully;";
                tblMsg.Rows[1].Cells[0].InnerText = totCount.ToString();
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnUpdtOpenBalSup_Click(object sender, EventArgs e)
        {
            GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

            var taSupAcc = new tbl_PuMa_Par_AccTableAdapter();
            var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var taCloseBal = new Tally_Accounts_Payable_31_10_19TableAdapter();
            var taGlCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();

            var taFaTeCloseBal = new View_GL_Close_BalTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAcc.Connection);

            try
            {
                var qrySqlStr = "";
                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_GL_Close_Bal]')) DROP VIEW [dbo].[View_GL_Close_Bal]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "Create view View_GL_Close_Bal as select tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code as Opn_Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name as Opn_Gl_Coa_Name, " +
                            "SUM(DebitAmount) as OpnDr,SUM(CreditAmount) as OpnCr,SUM(DebitAmount) - SUM(CreditAmount) as OpnBal " +
                            "from tbl_Acc_Fa_Gl_Coa left outer join View_Acc_Tran_Det on Gl_Coa_Code =Trn_Ac_Code " +
                            "where CONVERT(date,Trn_DATE,103)<= CONVERT(date,'31/10/2019',103) " +
                            "group by tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                taSupAcc.AttachTransaction(myTran);
                taSupAdr.AttachTransaction(myTran);
                taAcc.AttachTransaction(myTran);
                taCloseBal.AttachTransaction(myTran);
                taGlCoa.AttachTransaction(myTran);
                taFaTeCloseBal.AttachTransaction(myTran);

                var totCount = 0;

                var dtMaxAccRef = taAcc.GetMaxRefNo("OPN", Convert.ToDateTime("31/10/2019").Year);
                var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                var nextAccRefNo = "OPN" + (Convert.ToDateTime("31/10/2019").Month.ToString("00") + Convert.ToDateTime("31/10/2019").ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                var dtMaxAccRefNew = taAcc.GetMaxRefNo("ADJ", Convert.ToDateTime("31/10/2019").Year);
                var nextTranRefNew = dtMaxAccRefNew == null ? "000001" : (Convert.ToInt32(dtMaxAccRefNew) + 1).ToString();
                var nextTrnRefNoNew = "ADJ" + (Convert.ToDateTime("31/10/2019").Month.ToString("00") + Convert.ToDateTime("31/10/2019").ToString("yy")).ToString() + "-" + Convert.ToInt32(nextTranRefNew).ToString("000000");

                #region Update By Sup Group1
                var iCountOpn = 1;
                var iCountAdj = 1;
                var dtCloseBal = taCloseBal.GetDataByGrpName(ddlTallySupGroup.SelectedItem.ToString());
                foreach (dsAccTran.Tally_Accounts_Payable_31_10_19Row dr in dtCloseBal.Rows)
                {
                    var trnType = Convert.ToDecimal(dr.Closing_Balance) > 0 ? "C" : "D";
                    var trnAmt = Convert.ToDecimal(dr.Closing_Balance) > 0 ? (dr.Closing_Balance) : (dr.Closing_Balance * (-1));

                    var dtGlCoa = taGlCoa.GetDataByCoaName(dr.Ledger_Name.ToString().Trim());
                    if (dtGlCoa.Rows.Count > 0)
                    {
                        //---update closing balance                                                       
                        var dtFaTeCloseBal = taFaTeCloseBal.GetDataByGLCode(dtGlCoa[0].Gl_Coa_Code);
                        if (dtFaTeCloseBal.Rows.Count > 0)
                        {
                            taAcc.InsertAccData(dtGlCoa[0].Gl_Coa_Code, (Convert.ToDateTime("31/10/2019").Month.ToString("00") + "/" + Convert.ToDateTime("31/10/2019").Year.ToString()).ToString(),
                                nextTrnRefNoNew.ToString(), iCountAdj, 1, "Closing Balance Adjustment As On " + DateTime.Now.ToString("31/10/2019"),
                                Convert.ToDecimal(dtFaTeCloseBal[0].OpnBal.ToString()) > 0 ? "C" : "D", Convert.ToDecimal(dtFaTeCloseBal[0].OpnBal.ToString()),
                                "", "0", "BDT", 1, Convert.ToDecimal(dtFaTeCloseBal[0].OpnBal.ToString()),
                                "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, dtGlCoa[0].Gl_Coa_Name.ToString(),
                                Convert.ToDateTime("31/10/2019"), "ADM", dtGlCoa[0].Gl_Coa_Type.ToString(), "",
                                DateTime.Now, "ADJ", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(dtFaTeCloseBal[0].OpnBal.ToString()), "",
                                "", "", "N", 1, 0, "", "", "", "J", 0, "1", "ADJ");
                        }

                        //---update opening balance
                        taAcc.InsertAccData(dtGlCoa[0].Gl_Coa_Code, (Convert.ToDateTime("31/10/2019").Month.ToString("00") + "/" + Convert.ToDateTime("31/10/2019").Year.ToString()).ToString(),
                                nextAccRefNo.ToString(), iCountOpn, 1, "Opening Balance As On " + DateTime.Now.ToString("01/11/2019"),
                                 trnType, Convert.ToDecimal(trnAmt), "", "0", "BDT", 1, Convert.ToDecimal(trnAmt),
                                "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, dr.Ledger_Name.ToString().Trim(),
                                Convert.ToDateTime("31/10/2019"), "ADM", dtGlCoa[0].Gl_Coa_Type.ToString(), "",
                                DateTime.Now, "OPN", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(trnAmt), "",
                                "", "", "N", 1, 0, "", "", "", "J", 0, "1", "OPN");

                        //---update closing bal coa code update status
                        taCloseBal.UpdateGLCode("N", dtGlCoa[0].Gl_Coa_Code, dr.Ledger_Name.ToString());

                        //---update closing bal close bal update status
                        taCloseBal.UpdateGLBal("Y", dr.Ledger_Name.ToString());

                        iCountAdj++;
                        iCountOpn++;
                    }
                    else
                    {
                        
                        #region Insert Supplier
                        //------insert supplier account code
                        var dtMaxSupAccRef = taSupAcc.GetMaxAccRef();
                        var nextSupAccRef = dtMaxSupAccRef == null ? "10.100001" : "10." + (Convert.ToInt32(dtMaxSupAccRef) + 1).ToString();
                        var nextSupAccRefNo = "SUP-ACC-" + nextSupAccRef.ToString();

                        taSupAcc.InsertSupAcc(nextSupAccRef, nextSupAccRefNo, dr.Ledger_Name.Trim(), "", "1", "", "", DateTime.Now, "N", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", 0, 0, 0, 0, 0, 0, "", "BDT", "", "", "", "",
                            "", "", "", 0, "", "", "", "", "", "", "");

                        //---insert coa code
                        var dtMaxCoaRef = taGlCoa.GetMaxCoaRef();
                        var nextCoaRef = dtMaxCoaRef == null ? 1 : Convert.ToInt32(dtMaxCoaRef) + 1;

                        var dtMaxCoaCode = taGlCoa.GetMaxCoaCode();
                        var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
                        var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");

                        taGlCoa.InsertCoa(nextCoaRef, nextCoaCode, dr.Ledger_Name.Trim(), nextCoaCode, "S", "B", "N", DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y",
                            "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        //------insert supplier address code
                        var dtMaxAdrRef = taSupAdr.GetMaxAdrRef();
                        var nextAdrRef = dtMaxAdrRef == null ? "10.100001" : "10." + (Convert.ToInt32(dtMaxAdrRef) + 1).ToString();
                        var nextAdrRefNo = "SUP-" + nextAdrRef.ToString();

                        taSupAdr.InsertSupAdr(nextAdrRef, nextAdrRefNo, dr.Ledger_Name.Trim(), 1, nextAccRef.ToString(), "",
                            "Imported From Tally", "", "", "", "", "",
                            "", "", "", nextCoaCode, "", "", DateTime.Now, 0, 0, "", "", "", 0, "", "", "", "", "", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");
                        #endregion

                        //---update closing bal coa code update status
                        taCloseBal.UpdateGLCode("Y", nextCoaCode, dr.Ledger_Name.ToString());

                        //---update opening balance                            
                        var dtCoa = taGlCoa.GetDataByCoaCode(nextCoaCode);
                        if (dtCoa.Rows.Count > 0)
                        {
                            taAcc.InsertAccData(nextCoaCode, (Convert.ToDateTime("31/10/2019").Month.ToString("00") + "/" + Convert.ToDateTime("31/10/2019").Year.ToString()).ToString(),
                                nextAccRefNo.ToString(), iCountOpn, 1, "Opening Balance As On " + DateTime.Now.ToString("01/11/2019"),
                                 trnType, Convert.ToDecimal(trnAmt), "", "0", "BDT", 1, Convert.ToDecimal(trnAmt),
                                "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, dr.Ledger_Name.ToString().Trim(),
                                Convert.ToDateTime("31/10/2019"), "ADM", dtCoa[0].Gl_Coa_Type.ToString(), "",
                                DateTime.Now, "OPN", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(trnAmt), "",
                                "", "", "N", 1, 0, "", "", "", "J", 0, "1", "OPN");

                            //---update closing bal close bal update status
                            taCloseBal.UpdateGLBal("Y", dr.Ledger_Name.ToString());

                            iCountOpn++;
                        }
                        else
                        {
                            myTran.Rollback();
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid GL Account Code.";
                            tblMsg.Rows[1].Cells[0].InnerText = dtCoa[0].Gl_Coa_Name;
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }

                    //iCountAdj++;

                    totCount++;
                }
                #endregion

                //    iCountOpn++;
                //}

                taCloseBal.UpdateSupGrp1UpdtStat("Y", ddlTallySupGroup.SelectedItem.ToString());

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully;";
                tblMsg.Rows[1].Cells[0].InnerText = totCount.ToString();
                ModalPopupExtenderMsg.Show();

                var taGlList = new Tally_Accounts_Payable_31_10_19_NewTableAdapter();
                var dtGlList = taGlList.GetDataBySupGrp2Status();
                ddlTallySupGroup.DataSource = dtGlList;
                ddlTallySupGroup.DataValueField = "Sub_Group1";
                ddlTallySupGroup.DataTextField = "Sub_Group1";
                ddlTallySupGroup.DataBind();
                ddlTallySupGroup.Visible = true;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnUpdtCloseBalSupNew_Click(object sender, EventArgs e)
        {
            GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

            var taAcc = new tbl_Acc_Fa_TeTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAcc.Connection);

            try
            {
                var qrySqlStr = "";
                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_GL_Close_Bal]')) DROP VIEW [dbo].[View_GL_Close_Bal]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "Create view View_GL_Close_Bal as select tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code as Opn_Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name as Opn_Gl_Coa_Name, " +
                            "SUM(DebitAmount) as OpnDr,SUM(CreditAmount) as OpnCr,SUM(DebitAmount) - SUM(CreditAmount) as OpnBal " +
                            "from tbl_Acc_Fa_Gl_Coa left outer join View_Acc_Tran_Det on Gl_Coa_Code =Trn_Ac_Code " +
                            "where CONVERT(date,Trn_DATE,103)<= CONVERT(date,'31/10/2019',103) " +
                            "group by tbl_Acc_Fa_Gl_Coa.Gl_Coa_Code,tbl_Acc_Fa_Gl_Coa.Gl_Coa_Name";
                dbCon.ExecuteSQLStmt(qrySqlStr);


                taAcc.AttachTransaction(myTran);

                var dtMaxAccRef = taAcc.GetMaxRefNo("ADJ", Convert.ToDateTime("31/10/2019").Year);
                var nextTranRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                var nextTrnRefNo = "ADJ" + (Convert.ToDateTime("31/10/2019").Month.ToString("00") + Convert.ToDateTime("31/10/2019").ToString("yy")).ToString() + "-" + Convert.ToInt32(nextTranRef).ToString("000000");

                var iCount = 1;
                var taCloseBal = new View_GL_Close_BalTableAdapter();
                var dtCloseBal = taCloseBal.GetDataBySupplierClosing();
                foreach (dsAccTran.View_GL_Close_BalRow dr in dtCloseBal.Rows)
                {
                    var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtCoa = taCoa.GetDataByCoaCode(dr.Opn_Gl_Coa_Code);
                    if (dtCoa.Rows.Count > 0)
                    {
                        taAcc.InsertAccData(dr.Opn_Gl_Coa_Code.ToString(), (Convert.ToDateTime("31/10/2019").Month.ToString("00") + "/" + Convert.ToDateTime("31/10/2019").Year.ToString()).ToString(),
                            nextTrnRefNo.ToString(), iCount, 1, "Closing Balance Adjustment As On " + DateTime.Now.ToString("31/10/2019"),
                            Convert.ToDecimal(dr.OpnBal.ToString()) > 0 ? "C" : "D", Convert.ToDecimal(dr.OpnBal.ToString()) > 0 ? Convert.ToDecimal(dr.OpnBal.ToString()) : Convert.ToDecimal(dr.OpnBal.ToString()) * -1,
                            "", "0", "BDT", 1, Convert.ToDecimal(dr.OpnBal.ToString()) > 0 ? Convert.ToDecimal(dr.OpnBal.ToString()) : Convert.ToDecimal(dr.OpnBal.ToString()) * -1,
                            "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, dtCoa[0].Gl_Coa_Name.ToString(),
                            Convert.ToDateTime("31/10/2019"), "ADM", dtCoa[0].Gl_Coa_Type.ToString(), "",
                            DateTime.Now, "ADJ", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(dr.OpnBal.ToString()) > 0 ? Convert.ToDecimal(dr.OpnBal.ToString()) : Convert.ToDecimal(dr.OpnBal.ToString()) * -1, "",
                            "", "", "N", 1, 0, "", "", "", "J", 0, "1", "ADJ");
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Account Code.";
                        tblMsg.Rows[1].Cells[0].InnerText = "GL Code: " + dr.Opn_Gl_Coa_Code;
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    iCount++;
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Opening balance entry successful. total (" + iCount.ToString() + ")";
                tblMsg.Rows[1].Cells[0].InnerText = "Voucher Ref. No" + nextTrnRefNo.ToString();
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnUpdtOpenBalSuppNew_Click(object sender, EventArgs e)
        {
            var taOpnBal = new Supplier_Balance_Tally_31_10_2019TableAdapter();
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAcc.Connection);

            try
            {
                taAcc.AttachTransaction(myTran);
                taOpnBal.AttachTransaction(myTran);

                var dtMaxAccRef = taAcc.GetMaxRefNo("OPN", Convert.ToDateTime("31/10/2019").Year);
                var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                var nextAccRefNo = "OPN" + (Convert.ToDateTime("31/10/2019").Month.ToString("00") + Convert.ToDateTime("31/10/2019").ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                var iCount = 1;
                var dtOpnBal = taOpnBal.GetDataByOpnBal();
                foreach (dsAccTran.Supplier_Balance_Tally_31_10_2019Row dr in dtOpnBal.Rows)
                {
                    var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                    var dtCoa = taCoa.GetDataByCoaCode(dr.Acc_GL_Code);
                    if (dtCoa.Rows.Count > 0)
                    {
                        taAcc.InsertAccData(dr.Acc_GL_Code.ToString(), (Convert.ToDateTime("31/10/2019").Month.ToString("00") + "/" + Convert.ToDateTime("31/10/2019").Year.ToString()).ToString(),
                            nextAccRefNo.ToString(), iCount, 1, "Opening Balance Adjustment As On " + DateTime.Now.ToString("01/11/2019"),
                            dr.Trn_Type, Convert.ToDecimal(dr.Opn_Bal.ToString()),
                            "", "0", "BDT", 1, Convert.ToDecimal(dr.Opn_Bal.ToString()),
                            "", "", "", "", "", "", "", "", "", "", "", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, dtCoa[0].Gl_Coa_Name.ToString(),
                            Convert.ToDateTime("31/10/2019"), "ADM", dtCoa[0].Gl_Coa_Type.ToString(), "",
                            DateTime.Now, "OPN", "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(dr.Opn_Bal.ToString()), "",
                            "", "", "N", 1, 0, "", "", "", "J", 0, "1", "OPN");
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Account Code.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    iCount++;
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Opening balance entry successful. total (" + iCount.ToString() + ")";
                tblMsg.Rows[1].Cells[0].InnerText = "Tran Ref. No" + nextAccRefNo.ToString();
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
    }
}