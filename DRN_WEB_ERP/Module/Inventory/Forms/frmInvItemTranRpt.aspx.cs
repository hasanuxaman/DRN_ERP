using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;

namespace DRN_WEB_ERP.Module.Inventory.Forms
{
    public partial class frmInvItemTranRpt : System.Web.UI.Page
    {
        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        string rptFile;
        string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            rptSelcFormula = "";
            rptFile = "";
            Session["RptDateFrom"] = txtFromDate.Text.Trim();
            Session["RptDateTo"] = txtToDate.Text.Trim();
            Session["RptFilePath"] = rptFile;
            Session["RptFormula"] = rptSelcFormula;

            txtFromDate.Text = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taItemType = new tbl_InMa_TypeTableAdapter();
            var dtItemType = taItemType.GetDataByAsc();
            cboItemType.DataSource = dtItemType;
            cboItemType.DataTextField = "Item_Type_Name";
            cboItemType.DataValueField = "Item_Type_Code";
            cboItemType.DataBind();
            cboItemType.Items.Insert(0, new ListItem("-----Select-----", "0"));

            //var taItem = new tbl_InMa_Item_DetTableAdapter();
            //var dtSaleItem = taItem.GetDataByItemType(cboItemType.SelectedValue.ToString());
            //cboItem.DataSource = dtSaleItem;
            //cboItem.DataTextField = "Itm_Det_Desc";
            //cboItem.DataValueField = "Itm_Det_Ref";
            //cboItem.DataBind();
            cboItem.Items.Insert(0, new ListItem("----------Select----------", "0"));

            var taStore = new tbl_InMa_Str_LocTableAdapter();
            var dtStore = taStore.GetDataBySortName();
            foreach (dsInvMas.tbl_InMa_Str_LocRow dr in dtStore.Rows)
            {
                cboStore.Items.Add(new ListItem(dr.Str_Loc_Name, dr.Str_Loc_Ref.ToString()));
            }
            cboStore.Items.Insert(0, new ListItem("-----All-----", "0"));
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            var qrySqlStr = "";
            qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Item_Opn_Bal]')) DROP VIEW [dbo].[View_Item_Opn_Bal]";
            dbCon.ExecuteSQLStmt(qrySqlStr);

            qrySqlStr = "Create view View_Item_Opn_Bal as SELECT Trn_Det_Icode, Trn_Det_Str_Code, " +
                        "SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'R' THEN Trn_Det_Lin_Qty ELSE 0 END) AS RcvQty, " +
                        "SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'I' THEN Trn_Det_Lin_Qty ELSE 0 END) AS IsuQty, " +
                        "CAST(SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'R' THEN Trn_Det_Lin_Qty ELSE 0 END) " +
                        "- SUM(CASE LEFT(Trn_Det_Type, 1) WHEN 'I' THEN Trn_Det_Lin_Qty ELSE 0 END) AS decimal(18, 4)) AS BalQty FROM dbo.View_InTr_Trn_Hdr_Det " +
                        "WHERE (CONVERT(datetime, Trn_Hdr_Date, 103) < CONVERT(DATETIME,'" + txtFromDate.Text.Trim() + "', 103)) " +
                        "GROUP BY Trn_Det_Icode, Trn_Det_Str_Code";
            dbCon.ExecuteSQLStmt(qrySqlStr);

            if (cboStore.SelectedIndex == 0)
            {
                if (cboItem.SelectedIndex == 0)
                {
                    rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                                    + "') and {View_InTr_Trn_Hdr_Det.Itm_Det_Type}='" + cboItemType.SelectedValue.ToString() + "'";
                }
                else
                {
                    rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                                    + "') and {View_InTr_Trn_Hdr_Det.Trn_Det_Icode}='" + cboItem.SelectedValue.ToString() + "'";
                }
            }
            else
            {
                if (cboItem.SelectedIndex == 0)
                {
                    rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                                    + "') and {View_InTr_Trn_Hdr_Det.Itm_Det_Type}='" + cboItemType.SelectedValue.ToString()
                                    + "' and {View_InTr_Trn_Hdr_Det.Trn_Det_Str_Code}='" + cboStore.SelectedValue.ToString() + "'";
                }
                else
                {
                    rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                                    + "') and {View_InTr_Trn_Hdr_Det.Trn_Det_Icode}='" + cboItem.SelectedValue.ToString()
                                    + "' and {View_InTr_Trn_Hdr_Det.Trn_Det_Str_Code}='" + cboStore.SelectedValue.ToString() + "'";
                }
            }

            if (optRptType.SelectedValue == "1")
                rptFile = "~/Module/Inventory/Reports/rptInvTranList.rpt";
            else
                rptFile = "~/Module/Inventory/Reports/rptInvTranListSum.rpt";

            Session["RptDateFrom"] = txtFromDate.Text.Trim();
            Session["RptDateTo"] = txtToDate.Text.Trim();
            Session["RptFilePath"] = rptFile;
            Session["RptFormula"] = rptSelcFormula;
        }

        protected void cboItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taItem = new tbl_InMa_Item_DetTableAdapter();            

            try
            {
                if (cboItemType.SelectedIndex == 0)
                {
                    cboItem.Items.Clear();

                    var dtSaleItem = taItem.GetDataBySortAsc();
                    cboItem.DataSource = dtSaleItem;
                    cboItem.DataTextField = "Itm_Det_Desc";
                    cboItem.DataValueField = "Itm_Det_Ref";
                    cboItem.DataBind();                   
                    cboItem.Items.Insert(0, new ListItem("----------Select----------", "0"));
                }
                else
                {
                    cboItem.Items.Clear();

                    var dtSaleItem = taItem.GetDataByItemType(cboItemType.SelectedValue.ToString());
                    cboItem.DataSource = dtSaleItem;
                    cboItem.DataTextField = "Itm_Det_Desc";
                    cboItem.DataValueField = "Itm_Det_Ref";
                    cboItem.DataBind();                    
                    cboItem.Items.Insert(0, new ListItem("----------Select----------", "0"));
                }
            }
            catch (Exception ex) { }
        }
    }
}