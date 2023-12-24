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
    public partial class frmInvRptPro : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, 1).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taItem= new tbl_InMa_Item_DetTableAdapter();
            var dtItem = taItem.GetDataByItemType("PRO");
            cboItem.DataSource = dtItem;
            cboItem.DataTextField = "Itm_Det_Desc";
            cboItem.DataValueField = "Itm_Det_Ref";
            cboItem.DataBind();
            cboItem.Items.Insert(0, new ListItem("-----All-----", "0"));
        }

        protected void btnShowDet_Click(object sender, EventArgs e)
        {
            reportInfo();
            var url = "frmShowInvReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportInfo()
        {
            try
            {
                if (txtFromDate.Text == "" || txtToDate.Text == "") return;

                if (optRptType.SelectedValue == "1")
                {
                    if (cboItem.SelectedIndex == 0)
                        rptSelcFormula = "{View_Inv_Trn_Hdr_Det_Pro.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim() + "')";
                    else
                        rptSelcFormula = "{View_Inv_Trn_Hdr_Det_Pro.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Inv_Trn_Hdr_Det_Pro.Trn_Det_Icode}='" + cboItem.SelectedValue.ToString() + "'";

                    rptFile = "~/Module/Inventory/Reports/rptInvIsuProDet.rpt";
                }
                else
                {
                    if (cboItem.SelectedIndex == 0)
                        rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Type}='RC' and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Code}='PO' and {View_InTr_Trn_Hdr_Det.Itm_Det_Type}='PRO' " +
                                         "and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim() + "')";
                    else
                        rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Type}='RC' and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Code}='PO' and {View_InTr_Trn_Hdr_Det.Itm_Det_Type}='PRO' " +
                                         "and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim() +"') " +
                                         "and {View_InTr_Trn_Hdr_Det.Trn_Det_Icode}='" + cboItem.SelectedValue.ToString() + "'";

                    rptFile = "~/Module/Inventory/Reports/rptInvMrrProDet.rpt";
                }

                Session["RptDateFrom"] = txtFromDate.Text.Trim();
                Session["RptDateTo"] = txtToDate.Text.Trim();
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void reportInfoSum()
        {
            try
            {
                if (txtFromDate.Text == "" || txtToDate.Text == "") return;

                if (optRptType.SelectedValue == "1")
                {
                    if (cboItem.SelectedIndex == 0)
                        rptSelcFormula = "{View_Inv_Trn_Hdr_Det_Pro.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim() + "')";
                    else
                        rptSelcFormula = "{View_Inv_Trn_Hdr_Det_Pro.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim()
                            + "') and {View_Inv_Trn_Hdr_Det_Pro.Trn_Det_Icode}='" + cboItem.SelectedValue.ToString() + "'";

                    rptFile = "~/Module/Inventory/Reports/rptInvIsuProSum.rpt";
                }
                else
                {
                    if (cboItem.SelectedIndex == 0)
                        rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Type}='RC' and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Code}='PO' and {View_InTr_Trn_Hdr_Det.Itm_Det_Type}='PRO' " +
                                         "and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim() + "')";
                    else
                        rptSelcFormula = "{View_InTr_Trn_Hdr_Det.Trn_Hdr_Type}='RC' and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Code}='PO' and {View_InTr_Trn_Hdr_Det.Itm_Det_Type}='PRO' " +
                                         "and {View_InTr_Trn_Hdr_Det.Trn_Hdr_Date} in Date('" + txtFromDate.Text.Trim() + "') to Date ('" + txtToDate.Text.Trim() + "') " +
                                         "and {View_InTr_Trn_Hdr_Det.Trn_Det_Icode}='" + cboItem.SelectedValue.ToString() + "'";

                    rptFile = "~/Module/Inventory/Reports/rptInvMrrProSum.rpt";
                }

                Session["RptDateFrom"] = txtFromDate.Text.Trim();
                Session["RptDateTo"] = txtToDate.Text.Trim();
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnShowSum_Click(object sender, EventArgs e)
        {
            reportInfoSum();
            var url = "frmShowInvReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }
    }
}