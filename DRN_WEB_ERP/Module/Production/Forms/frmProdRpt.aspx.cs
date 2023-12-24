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
    public partial class frmProdRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taSaleItem = new tbl_InMa_Item_DetTableAdapter();
            var dtSaleItem = taSaleItem.GetRmItemData();
            ddlItem.DataSource = dtSaleItem;
            ddlItem.DataTextField = "Itm_Det_Desc";
            ddlItem.DataValueField = "Itm_Det_Ref";
            ddlItem.DataBind();
            ddlItem.Items.Insert(0, new ListItem("----------ALL----------", "0"));
        }

        protected void btnShowProdRpt_Click(object sender, EventArgs e)
        {
            var taInv = new VIEW_INV_TRAN_DETTableAdapter();
            var dtInv = new dsInvTran.VIEW_INV_TRAN_DETDataTable();
            if (optProdRpt.SelectedValue == "FG")
                dtInv = taInv.GetDataByDate("RC", "PRD", txtFromDt.Text.Trim(), txtToDt.Text.Trim());
            else
                dtInv = taInv.GetDataByDate("IS", "PRD", txtFromDt.Text.Trim(), txtToDt.Text.Trim());
            gvProdRpt.DataSource = dtInv;
            gvProdRpt.DataBind();
        }
    }
}