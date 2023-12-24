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
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Inventory.Forms
{
    public partial class frmStoreReqIssu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Auto  Page Refresh
            //Response.AppendHeader("Refresh", "60");

            if (Page.IsPostBack) return;

            try
            {
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                var taPoHdrIssuList = new View_InTr_Pro_Sr_Hdr_DetTableAdapter();
                var dtPoHdrIssuList = taPoHdrIssuList.GetDataByPendReqIssue();
                cboReqBy.DataSource = dtPoHdrIssuList;
                cboReqBy.DataValueField = "PO_Hdr_Acode";
                cboReqBy.DataTextField = "EmpName";
                cboReqBy.DataBind();
                cboReqBy.Items.Insert(0, new ListItem("---ALL---", "0"));

                var taPoHdrIssuListNew = new View_InTr_Pro_Sr_Hdr_Det_NewTableAdapter();
                var dtPoHdrIssuListNew = taPoHdrIssuListNew.GetDataByPendReqIssueNew();
                cboReqFor.DataSource = dtPoHdrIssuListNew;
                cboReqFor.DataValueField = "PO_Hdr_Pcode";
                cboReqFor.DataTextField = "Par_Adr_Name";
                cboReqFor.DataBind();
                cboReqFor.Items.Insert(0, new ListItem("---ALL---", "0"));

                var taPoHdrIssu = new View_Pro_Sr_DetTableAdapter();
                var dtPoHdrIssu = new DataTable();
                if (empRef == "000778" || empRef == "000011")//-----Mozumder
                    dtPoHdrIssu = taPoHdrIssu.GetDataByPendIssue();
                gvPendPo.DataSource = dtPoHdrIssu;
                gvPendPo.DataBind();

                btnIssue.Visible = false;
            }
            catch (Exception ex) { }
        }

        #region GridData
        public string GetEmpName(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new tblHrmsEmpTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(empRef.ToString());
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].EmpFirstName.ToString() + " " + dtEmp[0].EmpLastName.ToString();
                return empStr;
            }
            catch (Exception) { return empStr; }
        }

        public string GetEmpId(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].EmpId.ToString();
                return empStr;
            }
            catch (Exception) { return empStr; }
        }

        public string GetEmpDesig(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].DesigName.ToString();
                return empStr;
            }
            catch (Exception) { return empStr; }
        }

        public string GetEmpDept(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].DeptName.ToString();
                return empStr;
            }
            catch (Exception) { return empStr; }
        }

        public string GetCurStk(string strCode, string itemCode)
        {
            string curStk = "";
            try
            {
                var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();
                var dtStkCtl = taStkCtl.GetDataByStoreItem(strCode, itemCode);
                if (dtStkCtl.Rows.Count > 0)
                    curStk = Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk), 4).ToString();
                return curStk;
            }
            catch (Exception) { return curStk; }
        }
        #endregion

        protected void gvPendPo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {               
                var chkSr = (CheckBox)(e.Row.FindControl("optSelect"));
                if (cboReqFor.SelectedIndex == 0)
                    chkSr.Enabled = false;
                else
                    chkSr.Enabled = true;

                try
                {
                    var ddlStore = (DropDownList)(e.Row.FindControl("ddlStore"));
                    ddlStore.SelectedValue = "1004";

                    var lblItemCode = (Label)(e.Row.FindControl("lblItemCode"));
                    var lblStock = (Label)(e.Row.FindControl("lblStock"));

                    var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();
                    var dtStkCtl = taStkCtl.GetDataByStoreItem(ddlStore.SelectedValue, lblItemCode.Text.Trim());
                    if (dtStkCtl.Rows.Count > 0)
                        lblStock.Text = Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk), 4).ToString();
                    else
                        lblStock.Text = "0";
                }
                catch (Exception ex) { }

                System.Web.UI.WebControls.Image UsrImages = (System.Web.UI.WebControls.Image)e.Row.FindControl("AttachImage");
                var empRefNo = ((HiddenField)e.Row.FindControl("hfEmpRef")).Value;
                UsrImages.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRefNo + "'";
            }
        }

        protected void btnIssue_Click(object sender, EventArgs e)
        {
            //Page.Validate("btnAdd");

            //if (!Page.IsValid) return;

            //var taPoHdr = new tbl_PuTr_PO_HdrTableAdapter();
            var taPoDet = new tbl_InTr_Pro_Sr_DetTableAdapter();

            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taInvHdr.Connection);

            try
            {
                #region Form Validation
                short Ln = 0;
                foreach (GridViewRow gr in gvPendPo.Rows)
                {
                    var lblItemCodeChk = (Label)(gr.FindControl("lblItemCode"));
                    var ddlStoreChk = (DropDownList)(gr.FindControl("ddlStore"));

                    var lblIssQtyChk = (Label)(gr.FindControl("lblIssQty"));
                    var IssQtyChk = Convert.ToDouble(lblIssQtyChk.Text.Trim());

                    var optSelectChk = (CheckBox)(gr.FindControl("optSelect"));

                    if (optSelectChk.Checked)
                    {
                        Ln++;

                        if (IssQtyChk > 0)
                        {
                            var dtStkCtlChk = taStkCtl.GetDataByStoreItem(ddlStoreChk.SelectedValue.ToString(), lblItemCodeChk.Text.Trim());
                            //var curStk = dtStkCtlChk == null ? 0 : Convert.ToDouble(dtStkCtlChk);
                            var curStk = dtStkCtlChk.Rows.Count > 0 ? Math.Round((dtStkCtlChk[0].Stk_Ctl_Cur_Stk), 4) : 0;
                            if (curStk <= 0)
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "There is no stock in hand.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }

                            if (IssQtyChk > curStk)
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "Stock in hand: " + curStk;
                                tblMsg.Rows[1].Cells[0].InnerText = "You are not allowed to issue more than qty: " + curStk;
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Issue Quantity should be greater than zero (0).";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }                    
                }

                if (Ln <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Select Requisition First.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                var nextHdrRef = 1;
                var nextHdrRefNo = "";

                taPoDet.AttachTransaction(myTran);
                taInvHdr.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);
                taAcc.AttachTransaction(myTran);
                taStkCtl.AttachTransaction(myTran);

                //Inventory Header Ref
                var dtMaxHdrRef = taInvHdr.GetMaxHdrRef();
                nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;

                var dtMaxMrrRef = taInvHdr.GetMaxHdrRefNo("PRO", DateTime.Now.Year);
                var nextMrrRef = dtMaxMrrRef == null ? 1 : Convert.ToInt32(dtMaxMrrRef) + 1;
                nextHdrRefNo = "ECIL-PRO-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextMrrRef).ToString("000000");

                var locRef = cboReqBy.SelectedValue;

                taInvHdr.InsertInvHdr(nextHdrRef, "IS", "PRO", nextHdrRefNo, locRef.ToString(), locRef.ToString(), locRef.ToString(),
                    "ReqRef", DateTime.Now, "", "", "", "", "", "", "", "", "", "", Convert.ToDecimal(0), "P",
                    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), "ADM", "", "", "",
                    "", "", "", cboReqBy.SelectedItem.ToString(), "", "", 0, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                #region Insert Inventory Details
                short Lno = 0;
                foreach (GridViewRow gr in gvPendPo.Rows)
                {
                    var lblPoRefNo = (Label)(gr.FindControl("lblPoRefNo"));
                    var hfPoDetLno = (HiddenField)(gr.FindControl("hfPoDetLno"));
                    var hfReqFor = (HiddenField)(gr.FindControl("hfReqFor"));
                    var lblItemCode = (Label)(gr.FindControl("lblItemCode"));
                    var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                    var lblItemUnit = (Label)(gr.FindControl("lblItemUnit"));
                    var ddlStore = (DropDownList)(gr.FindControl("ddlStore"));

                    var lblIssQty = (Label)(gr.FindControl("lblIssQty"));
                    var IssQty = Convert.ToDouble(lblIssQty.Text.Trim());

                    var optSelect = (CheckBox)(gr.FindControl("optSelect"));

                    if (optSelect.Checked)
                    {
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

                        Lno++;

                        if (IssQty > 0)
                        {
                            var dtPoDet = taPoDet.GetDataByPoLno(lblPoRefNo.Text.Trim(), Convert.ToInt16(hfPoDetLno.Value));
                            if (dtPoDet.Rows.Count > 0)
                            {
                                taPoDet.UpdatePoItemQty(dtPoDet[0].PO_Det_Org_QTY + Convert.ToDouble(IssQty), dtPoDet[0].PO_Det_Bal_Qty - Convert.ToDouble(IssQty), "Y", lblPoRefNo.Text.Trim(), lblItemCode.Text.Trim(), Convert.ToInt16(hfPoDetLno.Value));

                                taInvDet.InsertInvDet(nextHdrRef.ToString(), "IS", "PRO", nextHdrRefNo.ToString(), Lno, "", 1, lblPoRefNo.Text.Trim(),
                                    Convert.ToInt16(hfPoDetLno.Value), lblItemCode.Text.Trim(), lblItemDesc.Text.Trim(), lblItemUnit.Text.Trim(), ddlStore.SelectedValue, hfReqFor.Value.ToString(),
                                    "", "", 0, "", DateTime.Now, DateTime.Now, Convert.ToDouble(IssQty), 0, Convert.ToDecimal(0), Convert.ToDecimal(IssQty) * Convert.ToDecimal(0),
                                    Convert.ToDecimal(IssQty) * Convert.ToDecimal(0), "", "", "", 0, 0, "1", "");

                                var dtStkCtl = taStkCtl.GetDataByStoreItem(ddlStore.SelectedValue, lblItemCode.Text.Trim());
                                if (dtStkCtl.Rows.Count > 0)
                                    taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk - Convert.ToDouble(IssQty)), 4), ddlStore.SelectedValue, lblItemCode.Text.Trim());
                                else
                                    taStkCtl.InsertItemStore(ddlStore.SelectedValue, lblItemCode.Text.Trim(), "", Math.Round((Convert.ToDouble(IssQty)), 4), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                        DateTime.Now, DateTime.Now, "", "", "", 0);
                            }
                            else
                            {
                                myTran.Rollback();
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Requisition.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                        }
                    }                    
                }
                #endregion                                

                btnIssue.Visible = false;        

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                var taPoHdrIssuList = new View_InTr_Pro_Sr_Hdr_DetTableAdapter();
                var dtPoHdrIssuList = taPoHdrIssuList.GetDataByPendReqIssue();
                cboReqBy.DataSource = dtPoHdrIssuList;
                cboReqBy.DataValueField = "PO_Hdr_Acode";
                cboReqBy.DataTextField = "EmpName";
                cboReqBy.DataBind();
                cboReqBy.Items.Insert(0, new ListItem("---ALL---", "0"));

                var taPoHdrIssuListNew = new View_InTr_Pro_Sr_Hdr_Det_NewTableAdapter();
                var dtPoHdrIssuListNew = taPoHdrIssuListNew.GetDataByPendReqIssueNew();
                cboReqFor.DataSource = dtPoHdrIssuListNew;
                cboReqFor.DataValueField = "PO_Hdr_Pcode";
                cboReqFor.DataTextField = "Par_Adr_Name";
                cboReqFor.DataBind();
                cboReqFor.Items.Insert(0, new ListItem("---ALL---", "0"));

                var taPoHdrIssu = new View_Pro_Sr_DetTableAdapter();
                var dtPoHdrIssu = new DataTable();
                if (empRef == "000778" || empRef == "000011")//-----Mozumder
                    dtPoHdrIssu = taPoHdrIssu.GetDataByPendIssue();
                gvPendPo.DataSource = dtPoHdrIssu;
                gvPendPo.DataBind();                        
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void cboReqBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            var taPoHdrIssu = new View_Pro_Sr_DetTableAdapter();
            var dtPoHdrIssu = new DataTable();

            if (empRef == "000778" || empRef == "000011")//-----Mozumder
            {
                if (cboReqBy.SelectedIndex == 0)
                {
                    cboReqFor.Items.Clear();
                    var taPoHdrIssuList = new View_InTr_Pro_Sr_Hdr_Det_NewTableAdapter();
                    var dtPoHdrIssuList = taPoHdrIssuList.GetDataByPendReqIssueNew();
                    cboReqFor.DataSource = dtPoHdrIssuList;
                    cboReqFor.DataValueField = "PO_Hdr_Pcode";
                    cboReqFor.DataTextField = "Par_Adr_Name";
                    cboReqFor.DataBind();
                    cboReqFor.Items.Insert(0, new ListItem("---ALL---", "0"));

                    dtPoHdrIssu = taPoHdrIssu.GetDataByPendIssue();
                    gvPendPo.DataSource = dtPoHdrIssu;
                    gvPendPo.DataBind();
                }
                else
                {
                    var taPoHdrIssuList = new View_InTr_Pro_Sr_Hdr_Det_NewTableAdapter();
                    var dtPoHdrIssuList = taPoHdrIssuList.GetDataByPendReqIssueNewAcode(cboReqBy.SelectedValue);
                    cboReqFor.DataSource = dtPoHdrIssuList;
                    cboReqFor.DataValueField = "PO_Hdr_Pcode";
                    cboReqFor.DataTextField = "Par_Adr_Name";
                    cboReqFor.DataBind();
                    cboReqFor.Items.Insert(0, new ListItem("---ALL---", "0"));

                    dtPoHdrIssu = taPoHdrIssu.GetDataByPendIssueByHdrAcode(cboReqBy.SelectedValue);
                    gvPendPo.DataSource = dtPoHdrIssu;
                    gvPendPo.DataBind();
                }
            }

            //if (cboReqFor.SelectedIndex == 0)
            //{
            //    foreach (GridViewRow gr in gvPendPo.Rows)
            //    {
            //        var chkIsu = (CheckBox)gr.FindControl("optSelect");
            //        chkIsu.Enabled = false;
            //    }
            //}

            btnIssue.Visible = false;
        }

        protected void optSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                var chkSr = (CheckBox)(row.FindControl("optSelect"));
                var btnCancel = (Button)(row.FindControl("btnCancel"));

                if (chkSr.Checked)
                {
                    btnCancel.Enabled = true;
                    btnIssue.Visible = true;
                }
                else
                {
                    btnCancel.Enabled = false;
                    btnIssue.Visible = false;
                    foreach (GridViewRow gr in gvPendPo.Rows)
                    {
                        var chkSr1 = (CheckBox)(gr.FindControl("optSelect"));
                        if (chkSr1.Checked)
                        {
                            btnIssue.Visible = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void imgBtnInfo_Click(object sender, ImageClickEventArgs e)
        {            
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            var lblPoRef = (Label)(row.FindControl("lblPoRefNo"));
            var hfPoDetLno = (HiddenField)(row.FindControl("hfPoDetLno"));
            var lblPoIcode = (Label)(row.FindControl("lblItemCode"));

            var taPoDet = new View_Pro_Sr_DetTableAdapter();
            var dtPoDet = taPoDet.GetDataByDetIcode(lblPoRef.Text.ToString(), lblPoIcode.Text.Trim());
            if (dtPoDet.Rows.Count > 0)
            {
                txtMsgInfo.Text = dtPoDet[0].Det_T_C1.ToString();                
                ModalPopupExtenderMsgInfo.Show();
            }            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            var taPoHdr = new tbl_InTr_Pro_Sr_HdrTableAdapter();
            var taPoDet = new tbl_InTr_Pro_Sr_DetTableAdapter();

            var dtPoHdr = new dsInvTran.tbl_InTr_Pro_Sr_HdrDataTable();
            var dtPoDet = new dsInvTran.tbl_InTr_Pro_Sr_DetDataTable();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taPoHdr.Connection);

            try
            {
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                
                var empName = "";                
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0) empName = dtEmp[0].EmpName;

                taPoHdr.AttachTransaction(myTran);
                taPoDet.AttachTransaction(myTran);

                GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
                var lblPoRef = (Label)(row.FindControl("lblPoRefNo"));
                var lblPoRefNo = (Label)(row.FindControl("lblPoRefNo"));
                var hfPoDetLno = (HiddenField)(row.FindControl("hfPoDetLno"));
                var lblPoIcode = (Label)(row.FindControl("lblItemCode"));

                dtPoHdr = taPoHdr.GetDataByPoRef(lblPoRef.Text.ToString());
                if (dtPoHdr.Rows.Count > 0)
                {
                    dtPoDet = taPoDet.GetDataByPoItemLno(lblPoRef.Text.ToString(), lblPoIcode.Text.Trim(), Convert.ToInt16(hfPoDetLno.Value.ToString()));
                    if (dtPoDet.Rows.Count > 0)
                    {
                        taPoDet.UpdatePoApprQty(Convert.ToDouble(dtPoDet[0].PO_Det_Lin_Qty), Convert.ToDouble(dtPoDet[0].PO_Det_Unt_Wgt),
                        dtPoDet[0].T_C1 + ", \n" + "Canceled By:- " + empName + " @" + DateTime.Now, "C", 0,
                        lblPoRef.Text.ToString(), lblPoIcode.Text.Trim(), Convert.ToInt16(hfPoDetLno.Value.ToString()));
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "PO Details Data Not Found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "PO Header Data Not Found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Canceled successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                cboReqBy.Items.Clear();
                var taPoHdrIssuList = new View_InTr_Pro_Sr_Hdr_DetTableAdapter();
                var dtPoHdrIssuList = taPoHdrIssuList.GetDataByPendReqIssue();
                cboReqBy.DataSource = dtPoHdrIssuList;
                cboReqBy.DataValueField = "PO_Hdr_Pcode";
                cboReqBy.DataTextField = "Par_Adr_Name";
                cboReqBy.DataBind();
                cboReqBy.Items.Insert(0, new ListItem("---ALL---", "0"));
                
                var taPoHdrIssu = new View_Pro_Sr_DetTableAdapter();
                var dtPoHdrIssu = new DataTable();
                if (empRef == "000778" || empRef == "000011")//-----Mozumder
                    dtPoHdrIssu = taPoHdrIssu.GetDataByPendIssue();
                gvPendPo.DataSource = dtPoHdrIssu;
                gvPendPo.DataBind();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void cboReqFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            var taPoHdrIssu = new View_Pro_Sr_DetTableAdapter();
            var dtPoHdrIssu = new DataTable();

            if (empRef == "000778" || empRef == "000011")//-----Mozumder
            {
                if (cboReqFor.SelectedIndex == 0)
                {
                    if (cboReqBy.SelectedIndex == 0)
                    {
                        dtPoHdrIssu = taPoHdrIssu.GetDataByPendIssue();
                        gvPendPo.DataSource = dtPoHdrIssu;
                        gvPendPo.DataBind();
                    }
                    else
                    {
                        dtPoHdrIssu = taPoHdrIssu.GetDataByPendIssueByHdrAcode(cboReqBy.SelectedValue);
                        gvPendPo.DataSource = dtPoHdrIssu;
                        gvPendPo.DataBind();
                    }
                }
                else
                {
                    dtPoHdrIssu = taPoHdrIssu.GetDataByPendIssueByHdrPcode(cboReqFor.SelectedValue);
                    gvPendPo.DataSource = dtPoHdrIssu;
                    gvPendPo.DataBind();
                }
            }

            //if (cboReqFor.SelectedIndex == 0)
            //{
            //    foreach (GridViewRow gr in gvPendPo.Rows)
            //    {
            //        var chkIsu = (CheckBox)gr.FindControl("optSelect");
            //        chkIsu.Enabled = false;
            //    }
            //}

            btnIssue.Visible = false;
        }

        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;
            var ddlStore = (DropDownList)(row.FindControl("ddlStore"));
            var lblItemCode = (Label)(row.FindControl("lblItemCode"));
            var lblStock = (Label)(row.FindControl("lblStock"));

            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();
            var dtStkCtl = taStkCtl.GetDataByStoreItem(ddlStore.SelectedValue, lblItemCode.Text.Trim());
            if (dtStkCtl.Rows.Count > 0)
                lblStock.Text = Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk), 4).ToString();
            else
                lblStock.Text = "0";
        }
    }
}