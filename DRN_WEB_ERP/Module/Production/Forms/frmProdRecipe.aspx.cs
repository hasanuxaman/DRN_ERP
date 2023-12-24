using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Production.DataSet;
using DRN_WEB_ERP.Module.Production.DataSet.dsProdMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Production.Forms
{
    public partial class frmProdRecipe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taProdItm = new tbl_Prod_ItemTableAdapter();
            var dtProdItm = taProdItm.GetProdItemListByAsc();
            ddlProdItem.DataSource = dtProdItm;
            ddlProdItem.DataValueField = "Prod_Ref";
            ddlProdItem.DataTextField = "Prod_Name";
            ddlProdItem.DataBind();
            ddlProdItem.Items.Insert(0, new ListItem("-----Select-----", "0"));            
        }

        protected void chkRm_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                var chkRm = (CheckBox)(row.FindControl("chkRm"));
                var chkFg = (CheckBox)(row.FindControl("chkFg"));

                if (chkRm.Checked) chkFg.Checked = false;
            }
            catch (Exception ex) { }
        }

        protected void chkFg_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                var chkRm = (CheckBox)(row.FindControl("chkRm"));
                var chkFg = (CheckBox)(row.FindControl("chkFg"));

                if (chkFg.Checked) chkRm.Checked = false;
            }
            catch (Exception ex) { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var taProdItem = new tbl_Prod_ItemTableAdapter();
            var taProdRcipe = new tbl_Prod_RecipeTableAdapter();
            var taTranCom = new tbl_Tran_ComTableAdapter();
            var taEmp = new View_Emp_BascTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taProdItem.Connection);

            try
            {
                #region Get Employee Details
                string empId = "", empName = "", empDesig = "";
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                {
                    empId = dtEmp[0].EmpId.ToString();
                    empName = dtEmp[0].EmpName.ToString();
                    empDesig = dtEmp[0].DesigName.ToString();
                }
                #endregion

                taProdItem.AttachTransaction(myTran);
                taProdRcipe.AttachTransaction(myTran);
                taTranCom.AttachTransaction(myTran);

                //var rmStat = 0;
                //var fgStat = 0;
                //foreach (GridViewRow gr in gvRecipe.Rows)
                //{
                //    var chkRm1 = (CheckBox)(gr.FindControl("chkRm"));
                //    var chkFg1 = (CheckBox)(gr.FindControl("chkFg"));
                    
                //    if (chkRm1.Checked) rmStat++;                    
                //    if (chkFg1.Checked) fgStat++;
                //}

                //if (rmStat <= 0)
                //{
                //    tblMsg.Rows[0].Cells[0].InnerText = "You have to select at list one Raw Material.";
                //    tblMsg.Rows[1].Cells[0].InnerText = "";
                //    ModalPopupExtenderMsg.Show();
                //    return;
                //}
                //if (fgStat <= 0)
                //{
                //    tblMsg.Rows[0].Cells[0].InnerText = "You have to select at list one Finish Goods.";
                //    tblMsg.Rows[1].Cells[0].InnerText = "";
                //    ModalPopupExtenderMsg.Show();
                //    return;
                //}

                taProdRcipe.DeleteRecipe(ddlProdItem.SelectedValue.ToString());
                var FgList = "Output: ";
                var RmList=">>Consumption: ";
                foreach (GridViewRow gr in gvRecipe.Rows)
                {
                    var lblItmCode = (Label)(gr.FindControl("lblItmCode"));
                    var lblItmName = (Label)(gr.FindControl("lblItmName"));
                    var lblItmUom = (Label)(gr.FindControl("lblItmUom"));
                    var lblItmType = (Label)(gr.FindControl("lblItmType"));
                    var lblItmTypeName = (Label)(gr.FindControl("lblItmTypeName"));
                    var chkRm = (CheckBox)(gr.FindControl("chkRm"));
                    var chkFg = (CheckBox)(gr.FindControl("chkFg"));
                    var txtRmRatio = (TextBox)(gr.FindControl("txtRmRatio"));
                    var txtRmWastage = (TextBox)(gr.FindControl("txtRmWastage"));
                    var ddlStore = (DropDownList)(gr.FindControl("ddlStore"));

                    var RmRatio = txtRmRatio.Text.Trim() == "" ? "0" : txtRmRatio.Text.Trim();
                    var RmWastage = txtRmWastage.Text.Trim() == "" ? "0" : txtRmWastage.Text.Trim();

                    if (chkRm.Checked || chkFg.Checked)
                    {
                        taProdRcipe.InsertRecipe(lblItmCode.Text.Trim(), lblItmName.Text.Trim(), lblItmUom.Text.Trim(), lblItmType.Text.Trim(), lblItmTypeName.Text.Trim(),
                            ddlProdItem.SelectedValue.ToString(), ddlProdItem.SelectedItem.ToString(), chkRm.Checked ? 1 : 0, chkFg.Checked ? 1 : 0, 0, "",
                            ddlStore.SelectedValue.ToString(), Convert.ToDouble(RmRatio), Convert.ToDouble(RmWastage));
                    }

                    if (chkFg.Checked)
                        FgList = FgList + lblItmName.Text + "(" + lblItmUom.Text + ") R:" + RmRatio + "% + W:" + RmWastage + "%, ";                    

                    if (chkRm.Checked )
                        RmList = RmList + lblItmName.Text + "(" + lblItmUom.Text + ") R:" + RmRatio + "% + W:" + RmWastage + "%, ";   
                }

                var ProdItemCode = "";
                var dtProdItem = taProdItem.GetDataByProdItmRef(Convert.ToInt32(ddlProdItem.SelectedValue.ToString()));
                ProdItemCode = dtProdItem.Rows.Count > 0 ? dtProdItem[0].Prod_Code.ToString() : "";

                var dtMaxComSeqNo = taTranCom.GetMaxComSeqNo(ProdItemCode);
                var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                taTranCom.InsertTranCom(ProdItemCode, nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "RCP", "EDIT", FgList + " " + RmList, "", "1", "", "", "", "");

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex) 
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error. " + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlProdItem.SelectedIndex = 0;
            gvRecipe.DataSource = null;
            gvRecipe.DataBind();
            btnSave.Visible = false;
        }

        protected void ddlProdItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProdItem.SelectedIndex != 0)
            {
                var taItm = new tbl_InMa_Item_DetTableAdapter();
                var dtItm = taItm.GetFgRmPmSmItemData();
                gvRecipe.DataSource = dtItm;
                gvRecipe.DataBind();
                btnSave.Visible = true;
            }
            else
            {
                gvRecipe.DataSource = null;
                gvRecipe.DataBind();
                btnSave.Visible = false;
            }
        }

        protected void gvRecipe_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblItmCode = ((Label)e.Row.FindControl("lblItmCode"));
                var lblItmType = ((Label)e.Row.FindControl("lblItmType"));
                var chkRm = ((CheckBox)e.Row.FindControl("chkRm"));
                var chkFg = ((CheckBox)e.Row.FindControl("chkFg"));
                var txtRmRatio = ((TextBox)e.Row.FindControl("txtRmRatio"));
                var txtRmWastage = (TextBox)(e.Row.FindControl("txtRmWastage"));
                var ddlStore = (DropDownList)(e.Row.FindControl("ddlStore"));

                //if (lblItmType.Text == "RM") 
                //    chkRm.Enabled =true;
                //else
                //    chkRm.Enabled = false;

                //if (lblItmType.Text == "FG") 
                //    chkFg.Enabled = true;
                //else
                //    chkFg.Enabled = false;

                var taRcipe = new tbl_Prod_RecipeTableAdapter();
                var dtRcipe = taRcipe.GetDataByRmCode(lblItmCode.Text.Trim(), ddlProdItem.SelectedValue.ToString());
                foreach(dsProdMas.tbl_Prod_RecipeRow dr in dtRcipe.Rows)
                {
                    if (lblItmCode.Text.Trim() == dr.Itm_Code.Trim())
                    {
                        chkFg.Checked = dr.Fg_Status != 0 ? true : false;
                        chkRm.Checked = dr.Rm_Status != 0 ? true : false;
                        txtRmRatio.Text = dr.Prod_Std_Ratio.ToString();
                        txtRmWastage.Text = dr.Prod_Wast_Ratio.ToString();
                        ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dr.Op_Cl_Code.ToString()));
                    }
                }
            }
        }
    }
}