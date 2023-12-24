using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.Production.DataSet;
using DRN_WEB_ERP.Module.Production.DataSet.dsProdMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Production.Forms
{
    public partial class frmProdEntry : System.Web.UI.Page
    {
        double totFgQty = 0;
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
            try
            {
                totFgQty = 0;
                totRmQty = 0;
                if (ddlProdItem.SelectedIndex != 0)
                {
                    var taItm = new tbl_Prod_RecipeTableAdapter();
                    var dtItm = taItm.GetFgDataByProdId(ddlProdItem.SelectedValue.ToString());
                    gvProdFg.DataSource = dtItm;
                    gvProdFg.DataBind();
                    gvProdFg.Enabled = true;
                    gvProdRm.DataSource = null;
                    gvProdRm.DataBind();
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
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error. " + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                totFgQty = 0;
                totRmQty = 0;
                txtProdDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlProdItem.SelectedIndex = 0;
                gvProdRm.DataSource = null;
                gvProdRm.DataBind();
                gvProdFg.DataSource = null;
                gvProdFg.DataBind();
                gvProdFg.Enabled = true;
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
                foreach (GridViewRow gr in gvProdFg.Rows)
                {
                    var txtFgQty = ((TextBox)gr.FindControl("txtFgQty"));
                    var FgQty = txtFgQty.Text.Trim() == "" ? "0" : txtFgQty.Text.Trim();
                    totFgQty = totFgQty + Convert.ToDouble(FgQty);
                }

                if (totFgQty <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter Finish Goods Qty first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                var taRecipe = new tbl_Prod_RecipeTableAdapter();
                var dtRecipe = taRecipe.GetRmDataByProdId(ddlProdItem.SelectedValue.ToString());
                gvProdRm.DataSource = dtRecipe;
                gvProdRm.DataBind();

                btnProcess.Visible = dtRecipe.Rows.Count <= 0;
                gvProdFg.Enabled = dtRecipe.Rows.Count <= 0;
                btnSave.Visible = dtRecipe.Rows.Count > 0;

                var taItem = new tbl_Prod_ItemTableAdapter();
                var dtItem = taItem.GetDataByProdItmRef(Convert.ToInt32(ddlProdItem.SelectedValue.ToString()));
                if (dtItem.Rows.Count > 0)
                {
                    if (dtItem[0].Prod_Type == "P")///-----Bag Cement
                        gvProdRm.Enabled = true;
                    else
                        gvProdRm.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error. " + ex.Message.ToString();
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void gvProdFg_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblFgItmCode = ((Label)e.Row.FindControl("lblFgItmCode"));
                var txtFgQty = ((TextBox)e.Row.FindControl("txtFgQty"));
                var ddlFgStore = ((DropDownList)e.Row.FindControl("ddlFgStore"));

                var taItem = new tbl_Prod_ItemTableAdapter();
                var dtItem = taItem.GetDataByProdItmRef(Convert.ToInt32(ddlProdItem.SelectedValue.ToString()));
                if (dtItem.Rows.Count > 0)
                {
                    if (dtItem[0].Prod_Type == "B")
                        txtFgQty.Text = totRmQty.ToString("N2");
                    if (dtItem[0].Prod_Type == "P")///-----Bag Cement
                        txtFgQty.Text = ((totRmQty * 1000) / 50).ToString("N2");                    
                }
                
                var taRecp = new tbl_Prod_RecipeTableAdapter();
                var dtRecp = taRecp.GetDataByRmCode(lblFgItmCode.Text.ToString(), ddlProdItem.SelectedValue.ToString());
                if (dtRecp.Rows.Count > 0)                
                    ddlFgStore.SelectedIndex = ddlFgStore.Items.IndexOf(ddlFgStore.Items.FindByValue(dtRecp[0].Op_Cl_Code));                
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();
            var taTranCom = new tbl_Tran_ComTableAdapter();

            var taEmp = new View_Emp_BascTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taInvHdr.Connection);

            try
            {
                foreach (GridViewRow gr in gvProdFg.Rows)
                {
                    var txtFgQty = ((TextBox)gr.FindControl("txtFgQty"));
                    var FgQty = txtFgQty.Text.Trim() == "" ? "0" : txtFgQty.Text.Trim();
                    totFgQty = totFgQty + Convert.ToDouble(FgQty);
                }

                if (totFgQty <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter Finish Goods Qty first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

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

                var nextHdrRef = 1;
                var nextHdrRefNo = "";
                var nextHdrTranRefNo = "";

                taInvHdr.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);
                taStkCtl.AttachTransaction(myTran);
                taTranCom.AttachTransaction(myTran);

                //Inventory Header Ref
                var dtMaxRcvHdrRef = taInvHdr.GetMaxHdrRef();
                nextHdrRef = dtMaxRcvHdrRef == null ? 1 : Convert.ToInt32(dtMaxRcvHdrRef) + 1;

                var dtMaxRcvRef = taInvHdr.GetMaxHdrRefNo("PRD", Convert.ToDateTime(txtProdDate.Text.Trim()).Year);
                var nextRcvRef = dtMaxRcvRef == null ? 1 : Convert.ToInt32(dtMaxRcvRef) + 1;
                nextHdrRefNo = "ECIL-PMR-" + Convert.ToDateTime(txtProdDate.Text.Trim()).Month.ToString("00") + Convert.ToDateTime(txtProdDate.Text.Trim()).ToString("yy") + "-" + Convert.ToInt32(nextRcvRef).ToString("000000");

                var dtMaxProdRef = taInvHdr.GetMaxTranRef("PRD", Convert.ToDateTime(txtProdDate.Text.Trim()).Year);
                var nextProdRef = dtMaxProdRef == null ? 1 : Convert.ToInt32(dtMaxProdRef) + 1;
                nextHdrTranRefNo = "ECIL-PRD-" + Convert.ToDateTime(txtProdDate.Text.Trim()).Month.ToString("00") + Convert.ToDateTime(txtProdDate.Text.Trim()).ToString("yy") + "-" + Convert.ToInt32(nextProdRef).ToString("000000");

                taInvHdr.InsertInvHdr(nextHdrRef, "RC", "PRD", nextHdrRefNo, "1001", "1001", "1001",
                    nextHdrTranRefNo, Convert.ToDateTime(txtProdDate.Text.Trim()), ddlProdItem.SelectedItem.ToString(), "", "", "", "", "", "", "", "", "", Convert.ToDecimal(0), "P",
                    (Convert.ToDateTime(txtProdDate.Text.Trim()).Month.ToString("00") + "/" + Convert.ToDateTime(txtProdDate.Text.Trim()).Year.ToString()).ToString(), "ADM", "", "", "",
                    "", "", "", "Production Receive", "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");
                
                short TranLno = 0;

                #region Insert Inventory Receive Details
                short Lno = 0;
                var FgList = "Output: ";
                foreach (GridViewRow gr in gvProdFg.Rows)
                {
                    var lblItemCode = (Label)(gr.FindControl("lblFgItmCode"));
                    var lblItemDesc = (Label)(gr.FindControl("lblFgItmName"));
                    var lblItemUnit = (Label)(gr.FindControl("lblFgItmUom"));
                    var ddlFgStore = (DropDownList)(gr.FindControl("ddlFgStore"));

                    var txtFgQty = (TextBox)(gr.FindControl("txtFgQty"));
                    var FgQty = Convert.ToDouble(txtFgQty.Text.Trim());

                    #region Get Item Details
                    var itemName = "";
                    var itemAcc = "";
                    var taItemDet = new tbl_InMa_Item_DetTableAdapter();
                    var dtItemDet = taItemDet.GetDataByItemRef(Convert.ToInt32(lblItemCode.Text.Trim()));
                    if (dtItemDet.Rows.Count > 0)
                    {
                        itemName = dtItemDet[0].Itm_Det_Desc.ToString();
                        itemAcc = dtItemDet[0].Itm_Det_Acc_Code.ToString();

                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    #endregion

                    FgList = FgList + itemName.ToString() + " " + FgQty.ToString() + " " + lblItemUnit.Text;

                    Lno++;

                    TranLno++;

                    if (FgQty > 0)
                    {
                        taInvDet.InsertInvDet(nextHdrRef.ToString(), "RC", "PRD", nextHdrRefNo.ToString(), Lno, "", 1, nextHdrTranRefNo,
                            TranLno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), ddlFgStore.SelectedValue, "",
                            "", "", 0, "", DateTime.Now, Convert.ToDateTime(txtProdDate.Text.Trim()), Convert.ToDouble(FgQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(FgQty) * Convert.ToDecimal(0),
                            Convert.ToDecimal(FgQty) * Convert.ToDecimal(0), "", "", "", 0, 0, "1", "");

                        var dtStkCtl = taStkCtl.GetDataByStoreItem(ddlFgStore.SelectedValue.ToString(), lblItemCode.Text.Trim());
                        if (dtStkCtl.Rows.Count > 0)
                            taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk + Convert.ToDouble(FgQty)), 4), ddlFgStore.SelectedValue.ToString(), lblItemCode.Text.Trim());
                        else
                            taStkCtl.InsertItemStore(ddlFgStore.SelectedValue.ToString(), lblItemCode.Text.Trim(), "", Math.Round((Convert.ToDouble(FgQty)), 4), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                DateTime.Now, DateTime.Now, "", "", "", 0);
                    }
                }
                #endregion

                var dtMaxIssHdrRef = taInvHdr.GetMaxHdrRef();
                nextHdrRef = dtMaxIssHdrRef == null ? 1 : Convert.ToInt32(dtMaxIssHdrRef) + 1;

                var dtMaxIssRef = taInvHdr.GetMaxHdrRefNo("PRD", Convert.ToDateTime(txtProdDate.Text.Trim()).Year);
                var nextIssRef = dtMaxIssRef == null ? 1 : Convert.ToInt32(dtMaxIssRef) + 1;
                nextHdrRefNo = "ECIL-PMI-" + Convert.ToDateTime(txtProdDate.Text.Trim()).Month.ToString("00") + Convert.ToDateTime(txtProdDate.Text.Trim()).ToString("yy") + "-" + Convert.ToInt32(nextIssRef).ToString("000000");

                taInvHdr.InsertInvHdr(nextHdrRef, "IS", "PRD", nextHdrRefNo, "1001", "1001", "1001",
                    nextHdrTranRefNo, Convert.ToDateTime(txtProdDate.Text.Trim()), ddlProdItem.SelectedItem.ToString(), "", "", "", "", "", "", "", "", "", Convert.ToDecimal(0), "P",
                    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), "ADM", "", "", "",
                    "", "", "", "Production Issue", "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                #region Insert Inventory Issue Details
                short newLno = 0;
                var RmList = ">>Consume: ";
                foreach (GridViewRow gr in gvProdRm.Rows)
                {
                    var lblItemCode = (Label)(gr.FindControl("lblItmCode"));
                    var lblItemDesc = (Label)(gr.FindControl("lblItmName"));
                    var lblItemUnit = (Label)(gr.FindControl("lblItmUom"));
                    var ddlRmStore = (DropDownList)(gr.FindControl("ddlRmStore"));
                    var lblStdRatio = (Label)(gr.FindControl("lblStdRatio"));
                    var txtRmIssue = (TextBox)(gr.FindControl("txtRmIssue"));
                    var lblWstPerc = (Label)(gr.FindControl("lblWstPerc"));
                    var txtWastageQty = (TextBox)(gr.FindControl("txtWastageQty"));

                    var txtTotRmQty = (TextBox)(gr.FindControl("txtTotRmQty"));
                    var RmQty = Convert.ToDouble(txtTotRmQty.Text.Trim());

                    #region Get Item Details
                    var itemName = "";
                    var itemAcc = "";
                    var taItemDet = new tbl_InMa_Item_DetTableAdapter();
                    var dtItemDet = taItemDet.GetDataByItemRef(Convert.ToInt32(lblItemCode.Text.Trim()));
                    if (dtItemDet.Rows.Count > 0)
                    {
                        itemName = dtItemDet[0].Itm_Det_Desc.ToString();
                        itemAcc = dtItemDet[0].Itm_Det_Acc_Code.ToString();
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    #endregion

                    newLno++;
                    TranLno++;

                    if (RmQty > 0)
                    {
                        RmList = RmList + itemName.ToString() + "(" + lblStdRatio.Text + "% + " + lblWstPerc.Text + "%)=[" + txtRmIssue.Text + " + " + txtWastageQty.Text + "]=" + RmQty.ToString() + " " + lblItemUnit.Text + ", ";

                        var dtStkCtl = taStkCtl.GetDataByStoreItem(ddlRmStore.SelectedValue.ToString(), lblItemCode.Text.Trim());
                        if (dtStkCtl.Rows.Count > 0)
                        {
                            if (dtStkCtl[0].Stk_Ctl_Cur_Stk >= Convert.ToDouble(RmQty))
                            {
                                taInvDet.InsertInvDet(nextHdrRef.ToString(), "IS", "PRD", nextHdrRefNo.ToString(), newLno, "", 1, nextHdrTranRefNo,
                                    TranLno, lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), ddlRmStore.SelectedValue, "",
                                    "", "", 0, "", DateTime.Now, Convert.ToDateTime(txtProdDate.Text.Trim()), Convert.ToDouble(RmQty), 0, Convert.ToDecimal(0),
                                    Convert.ToDecimal(RmQty) * Convert.ToDecimal(0), Convert.ToDecimal(RmQty) * Convert.ToDecimal(0), "", "", "", 0, 0, "1", "");

                                taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk - Convert.ToDouble(RmQty)), 4), ddlRmStore.SelectedValue.ToString(), lblItemCode.Text.Trim());
                            }
                            else
                            {
                                myTran.Rollback();
                                tblMsg.Rows[0].Cells[0].InnerText = lblItemDesc.Text.Trim() + " has not sufficiant stock.";
                                tblMsg.Rows[1].Cells[0].InnerText = "Available stock: " + dtStkCtl[0].Stk_Ctl_Cur_Stk + ", Required: " + RmQty.ToString();
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                        }
                        //var dtStkCtl = taStkCtl.GetDataByStoreItem(ddlRmStore.SelectedValue.ToString(), lblItemCode.Text.Trim());
                        //if (dtStkCtl.Rows.Count > 0)
                        //    taStkCtl.UpdateStkCtlCurStk(dtStkCtl[0].Stk_Ctl_Cur_Stk - Convert.ToDouble(RmQty), ddlRmStore.SelectedValue.ToString(), lblItemCode.Text.Trim());
                        //else
                        //    taStkCtl.InsertItemStore(ddlRmStore.SelectedValue.ToString(), lblItemCode.Text.Trim(), "", Convert.ToDouble(RmQty), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                        //        DateTime.Now, DateTime.Now, "", "", "", 0);
                    }
                }
                #endregion

                var dtMaxComSeqNo = taTranCom.GetMaxComSeqNo(nextHdrTranRefNo);
                var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                taTranCom.InsertTranCom(nextHdrTranRefNo, nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "PRD", "POST", FgList + " " + RmList, "", "1", "", "", "", "");
                
                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                totFgQty = 0;
                totRmQty = 0;
                txtProdDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlProdItem.SelectedIndex = 0;
                gvProdRm.DataSource = null;
                gvProdRm.DataBind();
                gvProdFg.DataSource = null;
                gvProdFg.DataBind();
                gvProdFg.Enabled = true;
                btnProcess.Visible = false;
                btnSave.Visible = false;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvProdRm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hfItmType = ((HiddenField)e.Row.FindControl("hfItmType"));
                var lblItmCode = ((Label)e.Row.FindControl("lblItmCode"));
                var txtRmIssue = ((TextBox)e.Row.FindControl("txtRmIssue"));
                var txtWastageQty = ((TextBox)e.Row.FindControl("txtWastageQty"));
                var lblWstPerc = ((Label)e.Row.FindControl("lblWstPerc"));
                var txtTotRmQty = ((TextBox)e.Row.FindControl("txtTotRmQty"));
                var ddlRmStore = ((DropDownList)e.Row.FindControl("ddlRmStore"));                

                var taRecipe = new tbl_Prod_RecipeTableAdapter();
                var dtRecipe = taRecipe.GetDataByRmCode(lblItmCode.Text.ToString(), ddlProdItem.SelectedValue.ToString());
                if (dtRecipe.Rows.Count > 0)
                {
                    var rmIssQty = (totFgQty * ((dtRecipe[0].Prod_Std_Ratio) / 100));
                    var rmWstQty = (totFgQty * ((dtRecipe[0].Prod_Std_Ratio) / 100)) * (((dtRecipe[0].Prod_Wast_Ratio) / 100));
                    txtRmIssue.Text = (rmIssQty).ToString("N4");
                    txtWastageQty.Text = (rmWstQty).ToString("N4");
                    txtTotRmQty.Text = (rmIssQty + rmWstQty).ToString("N4");
                    ddlRmStore.SelectedIndex = ddlRmStore.Items.IndexOf(ddlRmStore.Items.FindByValue(dtRecipe[0].Op_Cl_Code));
                }

                var taProdItem = new tbl_Prod_ItemTableAdapter();
                var dtProdItem = taProdItem.GetDataByProdItmRef(Convert.ToInt32(ddlProdItem.SelectedValue.ToString()));
                if (dtProdItem.Rows.Count > 0)
                {
                    if (dtProdItem[0].Prod_Type == "P")///-----Bag Cement
                    {
                        var taItem = new tbl_InMa_Item_DetTableAdapter();
                        var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(lblItmCode.Text.Trim()));
                        if (dtItem.Rows.Count > 0)
                        {
                            if (dtItem[0].Itm_Det_Type == "PM")
                                txtWastageQty.Enabled = true;
                        }
                    }
                    else
                        txtWastageQty.Enabled = false;
                }
                txtWastageQty.Attributes.Add("onkeyup", "CalcWstPerc('" + txtWastageQty.ClientID + "', '" + txtRmIssue.ClientID + "', '" + lblWstPerc.ClientID + "', '" + txtTotRmQty.ClientID + "' )");
            }
        }
    }
}