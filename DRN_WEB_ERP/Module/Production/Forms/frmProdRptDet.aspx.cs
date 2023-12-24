using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;

namespace DRN_WEB_ERP.Module.Production.Forms
{
    public partial class frmProdRptDet : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");

            ddlItem.Items.Add(new ListItem("Bag Cement", "FG"));
            ddlItem.Items.Add(new ListItem("Bulk Cement", "SFG"));
            ddlItem.Items.Insert(0, new ListItem("----------ALL----------", "0"));
            ddlItem.Enabled = true;
        }

        protected void btnShowProdRpt_Click(object sender, EventArgs e)
        {
            reportInfo();
            var url = "frmShowProdReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportInfo()
        {
            if (txtFromDt.Text.Trim().Length > 0 && txtToDt.Text.Trim().Length > 0)
            {
                var qrySqlStr = "";

                qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Prod_Det]')) DROP VIEW [dbo].[View_Prod_Det]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = " create view View_Prod_Det as SELECT tbl_InTr_Trn_Hdr.Trn_Hdr_Ref, tbl_InTr_Trn_Hdr.Trn_Hdr_Type, tbl_InTr_Trn_Hdr.Trn_Hdr_Code, tbl_InTr_Trn_Hdr.Trn_Hdr_Ref_No, " +
                            " tbl_InTr_Trn_Hdr.Trn_Hdr_Pcode, tbl_InTr_Trn_Hdr.Trn_Hdr_Dcode, tbl_InTr_Trn_Hdr.Trn_Hdr_Acode, tbl_InTr_Trn_Hdr.Trn_Hdr_Date, " +
                            " tbl_InTr_Trn_Hdr.Trn_Hdr_Com1, tbl_InTr_Trn_Hdr.Trn_Hdr_Com2, tbl_InTr_Trn_Hdr.Trn_Hdr_Com3, tbl_InTr_Trn_Hdr.Trn_Hdr_Com4, " +
                            " tbl_InTr_Trn_Hdr.Trn_Hdr_Com5, tbl_InTr_Trn_Hdr.Trn_Hdr_Com6, tbl_InTr_Trn_Hdr.Trn_Hdr_Com7, tbl_InTr_Trn_Hdr.Trn_Hdr_Com8, " +
                            " tbl_InTr_Trn_Hdr.Trn_Hdr_Com9, tbl_InTr_Trn_Hdr.Trn_Hdr_Com10, tbl_InTr_Trn_Hdr.Trn_Hdr_Value, tbl_InTr_Trn_Hdr.Trn_Hdr_HRPB_Flag, " +
                            " tbl_InTr_Trn_Hdr.Trn_Hdr_Ent_Prd, tbl_InTr_Trn_Hdr.Trn_Hdr_Opr_Code, tbl_InTr_Trn_Hdr.Trn_Hdr_Prd_Cld, tbl_InTr_Trn_Hdr.Trn_Hdr_Exp_Typ, " +
                            " tbl_InTr_Trn_Hdr.Trn_Hdr_Led_Int, tbl_InTr_Trn_Hdr.Trn_Hdr_DC_No, tbl_InTr_Trn_Hdr.Trn_Hdr_EI_Flg, tbl_InTr_Trn_Hdr.Trn_Hdr_Cno, " +
                            " tbl_InTr_Trn_Hdr.T_C1 AS Expr3, tbl_InTr_Trn_Hdr.T_C2 AS Expr4, tbl_InTr_Trn_Hdr.T_Fl AS Expr5, tbl_InTr_Trn_Hdr.T_In AS Expr6, " +
                            " tbl_InTr_Trn_Hdr.Trn_Hdr_Ent_Date, tbl_InTr_Trn_Hdr.Trn_Hdr_Ent_User, tbl_InTr_Trn_Hdr.Trn_Hdr_Updt_Date, tbl_InTr_Trn_Hdr.Trn_Hdr_Updt_User, " +
                            " tbl_InTr_Trn_Hdr.Trn_Hdr_Status, tbl_InTr_Trn_Hdr.Trn_Hdr_Flag, tbl_InTr_Trn_Det.Trn_Det_Type, tbl_InTr_Trn_Det.Trn_Det_Code, tbl_InTr_Trn_Det.Trn_Det_Ref, " +
                            " tbl_InTr_Trn_Det.Trn_Det_Lno, tbl_InTr_Trn_Det.Trn_Det_Sfx, tbl_InTr_Trn_Det.Trn_Det_Exp_Lno, tbl_InTr_Trn_Det.Trn_Hdr_Tran_Ref, " +
                            " tbl_InTr_Trn_Det.Trn_Hdr_Tran_Ref_Lno, tbl_InTr_Trn_Det.Trn_Det_Icode, tbl_InTr_Trn_Det.Trn_Det_Itm_Desc, tbl_InTr_Trn_Det.Trn_Det_Itm_Uom, " +
                            " tbl_InTr_Trn_Det.Trn_Det_Str_Code, tbl_InTr_Trn_Det.Trn_Det_Bin_Code, tbl_InTr_Trn_Det.Trn_Det_Ord_Ref, tbl_InTr_Trn_Det.Trn_Det_Ord_Ref_No, " +
                            " tbl_InTr_Trn_Det.Trn_Det_Ord_Det_Lno, tbl_InTr_Trn_Det.Trn_Det_Bat_No, tbl_InTr_Trn_Det.Trn_Det_Exp_Dat, tbl_InTr_Trn_Det.Trn_Det_Book_Dat, " +
                            " tbl_InTr_Trn_Det.Trn_Det_Lin_Qty, tbl_InTr_Trn_Det.Trn_Det_Unt_Wgt, tbl_InTr_Trn_Det.Trn_Det_Lin_Rat, tbl_InTr_Trn_Det.Trn_Det_Lin_Amt, " +
                            " tbl_InTr_Trn_Det.Trn_Det_Lin_Net, tbl_InTr_Trn_Det.T_C1, tbl_InTr_Trn_Det.T_C2, tbl_InTr_Trn_Det.T_Fl, tbl_InTr_Trn_Det.T_In, tbl_InTr_Trn_Det.Trn_Det_Bal_Qty, " +
                            " tbl_InTr_Trn_Det.Trn_Det_Status, tbl_InTr_Trn_Det.Trn_Det_Flag,Itm_Det_Ref,Itm_Det_Code,Itm_Det_Desc,Itm_Det_PUSA_Unit,Itm_Det_Stk_Unit,Itm_Det_Type,Itm_Det_T_C1,Itm_Det_Status,Itm_Det_Flag " +
                            " FROM tbl_InTr_Trn_Hdr LEFT OUTER JOIN tbl_InTr_Trn_Det ON tbl_InTr_Trn_Hdr.Trn_Hdr_Ref = tbl_InTr_Trn_Det.Trn_Hdr_Ref " +
                            " LEFT OUTER JOIN tbl_InMa_Item_Det ON tbl_InTr_Trn_Det.Trn_Det_Icode = tbl_InMa_Item_Det.Itm_Det_Ref " +
                            " WHERE (tbl_InTr_Trn_Hdr.Trn_Hdr_Type ='" + optProdRpt.SelectedValue + "') AND (tbl_InTr_Trn_Hdr.Trn_Hdr_Code = 'PRD') " +
                            " AND (CONVERT(date, tbl_InTr_Trn_Hdr.Trn_Hdr_Date, 103)>= CONVERT(date, '" + txtFromDt.Text.Trim() + "', 103)) " +
                            " AND (CONVERT(date, tbl_InTr_Trn_Hdr.Trn_Hdr_Date, 103) <= CONVERT(date, '" + txtToDt.Text.Trim() + "', 103))";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                if (ddlItem.SelectedIndex == 0)
                {
                    rptSelcFormula = "{View_Prod_Det.Trn_Hdr_Date} in Date('" + txtFromDt.Text.Trim() + "') to Date ('" + txtToDt.Text.Trim()
                    + "') and {View_Prod_Det.Trn_Hdr_HRPB_Flag}='P'";
                }
                else
                {
                    rptSelcFormula = "{View_Prod_Det.Trn_Hdr_Date} in Date('" + txtFromDt.Text.Trim() + "') to Date ('" + txtToDt.Text.Trim()
                        + "') and {View_Prod_Det.Trn_Hdr_HRPB_Flag}='P' and {View_Prod_Det.Itm_Det_Type}='" + ddlItem.SelectedValue.ToString() + "'";
                }

                rptFile = "~/Module/Production/Reports/rptProdRpt.rpt";

                Session["RptDateFrom"] = txtFromDt.Text.Trim();
                Session["RptDateTo"] = txtToDt.Text.Trim();
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
            }
        }

        protected void optProdRpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlItem.Items.Clear();

            if (optProdRpt.SelectedValue == "RC")
            {
                ddlItem.Items.Add(new ListItem("Bag Cement", "FG"));
                ddlItem.Items.Add(new ListItem("Bulk Cement", "SFG"));
                ddlItem.Items.Insert(0, new ListItem("----------ALL----------", "0"));
                ddlItem.Enabled = false;
            }
            else
            {
                ddlItem.Items.Add(new ListItem("Raw Material", "RM"));
                ddlItem.Items.Add(new ListItem("Packing Material", "PM"));
                ddlItem.Items.Insert(0, new ListItem("----------ALL----------", "0"));
                ddlItem.Enabled = true;
            }
        }
    }
}