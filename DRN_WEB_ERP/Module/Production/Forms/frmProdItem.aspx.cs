using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Production.DataSet;
using DRN_WEB_ERP.Module.Production.DataSet.dsProdMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;

namespace DRN_WEB_ERP.Module.Production.Forms
{
    public partial class frmProdItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            try
            {
                var taProdItem = new tbl_Prod_ItemTableAdapter();

                var dtMaxProdRef = taProdItem.GetMaxProdRef();
                var nextProdRef = dtMaxProdRef == null ? 1 : Convert.ToInt32(dtMaxProdRef) + 1;
                txtProdItemRefNo.Text = nextProdRef.ToString();

                cboItem.Items.Insert(0, new ListItem("----------Select----------", "0"));

                var dtProdItem = taProdItem.GetProdItemListByAsc();
                gvProdItem.DataSource = dtProdItem;
                gvProdItem.DataBind();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtProdItemRefNo.Text = "";
                txtProdItemName.Text = "";
                ddlProdType.SelectedIndex = 0;
                btnSave.Text = "Save";

                cboItem.Items.Clear();                
                cboItem.Items.Insert(0, new ListItem("----------Select----------", "0"));

                var taProdItem = new tbl_Prod_ItemTableAdapter();

                var dtMaxProdRef = taProdItem.GetMaxProdRef();
                var nextProdRef = dtMaxProdRef == null ? 1 : Convert.ToInt32(dtMaxProdRef) + 1;
                txtProdItemRefNo.Text = nextProdRef.ToString();

                var dtProdItem = taProdItem.GetProdItemListByAsc();
                gvProdItem.DataSource = dtProdItem;
                gvProdItem.DataBind();
                gvProdItem.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var taProdItem = new tbl_Prod_ItemTableAdapter();

                var dtProdItem = taProdItem.GetDataByProdItmRef(Convert.ToInt32(txtProdItemRefNo.Text.Trim().ToString()));
                if (dtProdItem.Rows.Count > 0)
                {
                    taProdItem.UpdateProdItem(txtProdItemName.Text.Trim(), ddlProdType.SelectedValue.ToString(), "", "", "", 0, 0,
                        "", "", "", "", "", "", "", "", "", "", "", "", "", 0, Convert.ToInt32(txtProdItemRefNo.Text.Trim()));

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    var dtMaxProdRef = taProdItem.GetMaxProdRef();
                    var nextProdRef = dtMaxProdRef == null ? 1 : Convert.ToInt32(dtMaxProdRef) + 1;
                    taProdItem.InsertProdItem(Convert.ToInt32(nextProdRef), "P-" + nextProdRef.ToString("00"), txtProdItemName.Text.Trim(),
                        ddlProdType.SelectedValue.ToString(), "", "", "", 0, 0, "", "", "", "", "", "", "", "", "", "", "", "", "", 0);

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }

                txtProdItemRefNo.Text = "";
                txtProdItemName.Text = "";
                ddlProdType.SelectedIndex = 0;
                btnSave.Text = "Save";

                cboItem.Items.Clear();
                cboItem.Items.Insert(0, new ListItem("----------Select----------", "0"));

                var dtMaxNewProdRef = taProdItem.GetMaxProdRef();
                var nextNewProdRef = dtMaxNewProdRef == null ? 1 : Convert.ToInt32(dtMaxNewProdRef) + 1;
                txtProdItemRefNo.Text = nextNewProdRef.ToString();

                var dtProdItemList = taProdItem.GetProdItemListByAsc();
                gvProdItem.DataSource = dtProdItemList;
                gvProdItem.DataBind();
                gvProdItem.SelectedIndex = -1;
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvProdItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvProdItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvProdItem.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    Label lblItmRefNo = (Label)gvProdItem.Rows[indx].FindControl("lblItmRefNo");                    
                    HiddenField hfMatchItem = (HiddenField)gvProdItem.Rows[indx].FindControl("hfMatchItem");                    

                    btnSave.Text = "Edit";

                    var taProdItem = new tbl_Prod_ItemTableAdapter();
                    var dtProdItem = taProdItem.GetDataByProdItmRef(Convert.ToInt32(lblItmRefNo.Text.Trim().ToString()));
                    if (dtProdItem.Rows.Count > 0)
                    {
                        txtProdItemRefNo.Text = dtProdItem[0].Prod_Ref.ToString();
                        txtProdItemName.Text = dtProdItem[0].Prod_Name.ToString();
                        
                        ddlProdType.SelectedIndex = ddlProdType.Items.IndexOf(ddlProdType.Items.FindByValue(dtProdItem[0].IsProd_TypeNull() ? "0" : dtProdItem[0].Prod_Type.ToString()));

                        var itemType = "";
                        switch (ddlProdType.SelectedValue.ToString())
                        {
                            case "P":
                                itemType = "FG";
                                break;

                            case "B":
                                itemType = "SFG";
                                break;
                        }

                        cboItem.Items.Clear();

                        var taItem = new tbl_InMa_Item_DetTableAdapter();
                        var dtSaleItem = taItem.GetDataByItemType(itemType);
                        cboItem.DataSource = dtSaleItem;
                        cboItem.DataTextField = "Itm_Det_Desc";
                        cboItem.DataValueField = "Itm_Det_Ref";
                        cboItem.DataBind();
                        cboItem.Items.Insert(0, new ListItem("----------Select----------", "0"));

                        cboItem.SelectedIndex = cboItem.Items.IndexOf(cboItem.Items.FindByValue(dtProdItem[0].IsProd_CcodeNull() ? "0" : dtProdItem[0].Prod_Ccode.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void ddlProdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taItem = new tbl_InMa_Item_DetTableAdapter();

            try
            {
                var itemType="";
                switch (ddlProdType.SelectedValue.ToString())
                {
                    case "P":
                        itemType="FG";
                        break;

                    case "B":
                        itemType = "SFG";
                        break;
                }

                cboItem.Items.Clear();

                var dtSaleItem = taItem.GetDataByItemType(itemType);
                cboItem.DataSource = dtSaleItem;
                cboItem.DataTextField = "Itm_Det_Desc";
                cboItem.DataValueField = "Itm_Det_Ref";
                cboItem.DataBind();
                cboItem.Items.Insert(0, new ListItem("----------Select----------", "0"));
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
    }
}