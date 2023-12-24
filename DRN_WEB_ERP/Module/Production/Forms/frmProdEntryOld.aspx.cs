using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Production.DataSet.dsProdMasTableAdapters;

namespace DRN_WEB_ERP.Module.Production.Forms
{
    public partial class frmProdEntryOld : System.Web.UI.Page
    {
        double totRmQty = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtProdDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taProdItm = new tbl_Prod_ItemTableAdapter();
            var dtProdItm = taProdItm.GetProdItemListByAsc();
            ddlProdItem.DataSource = dtProdItm;
            ddlProdItem.DataValueField = "Prod_Ref";
            ddlProdItem.DataTextField = "Prod_Name";
            ddlProdItem.DataBind();
            ddlProdItem.Items.Insert(0, new ListItem("-----Select-----", "0"));
        }

        protected void ddlProdItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            totRmQty = 0;
            if (ddlProdItem.SelectedIndex != 0)
            {                
                var taItm = new tbl_Prod_RecipeTableAdapter();
                var dtItm = taItm.GetRmDataByProdId(ddlProdItem.SelectedValue.ToString());
                gvProdRm.DataSource = dtItm;
                gvProdRm.DataBind();
                gvProdFg.DataSource = null;
                gvProdFg.DataBind();
                btnProcess.Visible = dtItm.Rows.Count > 0;
                btnSave.Visible = false;
            }
            else
            {
                gvProdRm.DataSource = null;
                gvProdRm.DataBind();
                btnProcess.Visible = false;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                totRmQty = 0;
                txtProdDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlProdItem.SelectedIndex = 0;
                gvProdRm.DataSource = null;
                gvProdRm.DataBind();
                gvProdFg.DataSource = null;
                gvProdFg.DataBind();
                btnProcess.Visible = false;
                btnSave.Visible = false;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error. " + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gr in gvProdRm.Rows)
                {
                    var hfItmType = ((HiddenField)gr.FindControl("hfItmType"));
                    var txtRmIssue = ((TextBox)gr.FindControl("txtRmIssue"));
                    var RmIssue = txtRmIssue.Text.Trim() == "" ? "0" : txtRmIssue.Text.Trim();
                    if (hfItmType.Value != "PM") totRmQty = totRmQty + Convert.ToDouble(RmIssue);
                }

                if (totRmQty <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Issue Raw Material Qty first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                var taItm = new tbl_Prod_RecipeTableAdapter();
                var dtItm = taItm.GetFgDataByProdId(ddlProdItem.SelectedValue.ToString());
                gvProdFg.DataSource = dtItm;
                gvProdFg.DataBind();
                btnProcess.Visible = dtItm.Rows.Count <= 0;
                btnSave.Visible = dtItm.Rows.Count > 0;               
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText ="Data processing error. " + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void gvProdFg_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var txtFgQty = ((TextBox)e.Row.FindControl("txtFgQty"));

                var taItem = new tbl_Prod_ItemTableAdapter();
                var dtItem = taItem.GetDataByProdItmRef(Convert.ToInt32(ddlProdItem.SelectedValue.ToString()));
                if (dtItem.Rows.Count > 0)
                {
                    if (dtItem[0].Prod_Type == "B")
                        txtFgQty.Text = totRmQty.ToString("N2");
                    if (dtItem[0].Prod_Type == "P")
                        txtFgQty.Text = ((totRmQty * 1000) / 50).ToString("N2");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
            tblMsg.Rows[1].Cells[0].InnerText = "";
            ModalPopupExtenderMsg.Show();
        }
    }
}