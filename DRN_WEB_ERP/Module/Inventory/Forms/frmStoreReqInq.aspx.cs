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
    public partial class frmStoreReqInq : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Auto  Page Refresh
            //Response.AppendHeader("Refresh", "60");

            if (Page.IsPostBack) return;

            try
            {
                var curYear = DateTime.Now.Year;
                for (Int64 year = 2014; year <= (curYear); year++)
                {
                    cboYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                }
                cboYear.SelectedValue = curYear.ToString();

                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                var taPoHdrIssuList = new View_InTr_Pro_Sr_Hdr_DetTableAdapter();
                var dtPoHdrIssuList = new dsInvTran.View_InTr_Pro_Sr_Hdr_DetDataTable();
                if (empRef == "000856" || empRef == "000776" || empRef == "000515" || empRef == "000525" || empRef == "000011")//-----Ali Haider, Bharat, Abdullah, Khaled 
                    dtPoHdrIssuList = taPoHdrIssuList.GetDataByReqByInq(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                else
                    dtPoHdrIssuList = taPoHdrIssuList.GetDataByReqInqAcode(Convert.ToDecimal(cboYear.SelectedValue.ToString()), empRef);
                cboReqBy.DataSource = dtPoHdrIssuList;
                cboReqBy.DataValueField = "PO_Hdr_Acode";
                cboReqBy.DataTextField = "EmpName";
                cboReqBy.DataBind();
                cboReqBy.Items.Insert(0, new ListItem("---ALL---", "0"));

                var taPoHdrIssuListNew = new View_InTr_Pro_Sr_Hdr_Det_NewTableAdapter();
                var dtPoHdrIssuListNew = new dsInvTran.View_InTr_Pro_Sr_Hdr_Det_NewDataTable();
                if (empRef == "000856" || empRef == "000776" || empRef == "000515" || empRef == "000525" || empRef == "000011")//-----Ali Haider, Bharat, Abdullah, Khaled 
                    dtPoHdrIssuListNew = taPoHdrIssuListNew.GetDataByReqForInq(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                else
                    dtPoHdrIssuListNew = taPoHdrIssuListNew.GetDataByReqForInqAcode(Convert.ToDecimal(cboYear.SelectedValue.ToString()), empRef);
                cboReqFor.DataSource = dtPoHdrIssuListNew;
                cboReqFor.DataValueField = "PO_Hdr_Pcode";
                cboReqFor.DataTextField = "Par_Adr_Name";
                cboReqFor.DataBind();
                cboReqFor.Items.Insert(0, new ListItem("---ALL---", "0"));

                var taPoHdrIssu = new View_Pro_Sr_DetTableAdapter();
                var dtPoHdrIssu = new dsInvTran.View_Pro_Sr_DetDataTable();
                if (empRef == "000856" || empRef == "000776" || empRef == "000515" || empRef == "000525" || empRef == "000011")//-----Ali Haider, Bharat, Abdullah, Khaled 
                    dtPoHdrIssu = taPoHdrIssu.GetDataByPendReqInq(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                else
                    dtPoHdrIssu = taPoHdrIssu.GetDataByPendReqInqByHdrAcode(Convert.ToDecimal(cboYear.SelectedValue.ToString()), empRef);
                Session["data"] = dtPoHdrIssu;
                SetItemGridData();
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

        protected void cboReqBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            var taPoHdrIssu = new View_Pro_Sr_DetTableAdapter();
            var dtPoHdrIssu = new dsInvTran.View_Pro_Sr_DetDataTable();

            if (cboReqBy.SelectedIndex == 0)
            {
                if (empRef == "000856" || empRef == "000776" || empRef == "000515" || empRef == "000525" || empRef == "000011")//-----Ali Haider, Bharat, Abdullah, Khaled 
                    dtPoHdrIssu = taPoHdrIssu.GetDataByPendReqInq(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                else
                    dtPoHdrIssu = taPoHdrIssu.GetDataByPendReqInqByHdrAcode(Convert.ToDecimal(cboYear.SelectedValue.ToString()), empRef);
                Session["data"] = dtPoHdrIssu;
                SetItemGridData();

                cboReqFor.Items.Clear();
                var taPoHdrIssuList = new View_InTr_Pro_Sr_Hdr_Det_NewTableAdapter();
                var dtPoHdrIssuList = taPoHdrIssuList.GetDataByReqForInq(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                cboReqFor.DataSource = dtPoHdrIssuList;
                cboReqFor.DataValueField = "PO_Hdr_Pcode";
                cboReqFor.DataTextField = "Par_Adr_Name";
                cboReqFor.DataBind();
                cboReqFor.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
            else
            {
                dtPoHdrIssu = taPoHdrIssu.GetDataByPendReqInqByHdrAcode(Convert.ToDecimal(cboYear.SelectedValue.ToString()), cboReqBy.SelectedValue);
                Session["data"] = dtPoHdrIssu;
                SetItemGridData();

                cboReqFor.Items.Clear();
                if (dtPoHdrIssu.Rows.Count > 0)
                {
                    var taPoHdrIssuList = new View_InTr_Pro_Sr_Hdr_Det_NewTableAdapter();
                    var dtPoHdrIssuList = taPoHdrIssuList.GetDataByReqForInqAcode(Convert.ToDecimal(cboYear.SelectedValue.ToString()), cboReqBy.SelectedValue);
                    cboReqFor.DataSource = dtPoHdrIssuList;
                    cboReqFor.DataValueField = "PO_Hdr_Pcode";
                    cboReqFor.DataTextField = "Par_Adr_Name";
                    cboReqFor.DataBind();
                }
                cboReqFor.Items.Insert(0, new ListItem("---ALL---", "0"));
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

        protected void gvPendPo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPendPo.PageIndex = e.NewPageIndex;
            SetItemGridData();
        }

        protected void SetItemGridData()
        {
            //var taPoHdrIssu = new View_Pro_Sr_DetTableAdapter();
            //var dtPoHdrIssu = taPoHdrIssu.GetDataByPendReqInq();
            var dtPoHdrIssu = Session["data"];
            gvPendPo.DataSource = dtPoHdrIssu;            
            gvPendPo.DataBind();            
        }

        protected void cboReqFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            var taPoHdrIssu = new View_Pro_Sr_DetTableAdapter();
            var dtPoHdrIssu = new dsInvTran.View_Pro_Sr_DetDataTable();
            if (empRef == "000856" || empRef == "000776" || empRef == "000515" || empRef == "000525" || empRef == "000011")//-----Ali Haider, Bharat, Abdullah, Khaled             
            {
                if (cboReqBy.SelectedIndex == 0)
                {
                    if (cboReqFor.SelectedIndex == 0)
                        dtPoHdrIssu = taPoHdrIssu.GetDataByPendReqInq(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                    else
                        dtPoHdrIssu = taPoHdrIssu.GetDataByPendReqInqByHdrPcode(Convert.ToDecimal(cboYear.SelectedValue.ToString()), cboReqFor.SelectedValue.ToString());
                }
                else
                {
                    if (cboReqFor.SelectedIndex == 0)
                        dtPoHdrIssu = taPoHdrIssu.GetDataByPendReqInqByHdrAcode(Convert.ToDecimal(cboYear.SelectedValue.ToString()), cboReqBy.SelectedValue.ToString());
                    else
                        dtPoHdrIssu = taPoHdrIssu.GetDataByPendReqInqByHdrPcode(Convert.ToDecimal(cboYear.SelectedValue.ToString()), cboReqFor.SelectedValue.ToString());
                }
            }
            else
            {
                dtPoHdrIssu = taPoHdrIssu.GetDataByPendReqInqByHdrAcode(Convert.ToDecimal(cboYear.SelectedValue.ToString()), empRef);
            }
            Session["data"] = dtPoHdrIssu;
            SetItemGridData();
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            var taPoHdrIssu = new View_Pro_Sr_DetTableAdapter();
            var dtPoHdrIssu = new dsInvTran.View_Pro_Sr_DetDataTable();
            if (empRef == "000856" || empRef == "000776" || empRef == "000515" || empRef == "000525" || empRef == "000011")//-----Ali Haider, Bharat, Abdullah, Khaled             
            {
                if (cboReqBy.SelectedIndex == 0)
                {
                    if (cboReqFor.SelectedIndex == 0)
                        dtPoHdrIssu = taPoHdrIssu.GetDataByPendReqInq(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                    else
                        dtPoHdrIssu = taPoHdrIssu.GetDataByPendReqInqByHdrPcode(Convert.ToDecimal(cboYear.SelectedValue.ToString()), cboReqFor.SelectedValue.ToString());
                }
                else
                {
                    if (cboReqFor.SelectedIndex == 0)
                        dtPoHdrIssu = taPoHdrIssu.GetDataByPendReqInqByHdrAcode(Convert.ToDecimal(cboYear.SelectedValue.ToString()), cboReqBy.SelectedValue.ToString());
                    else
                        dtPoHdrIssu = taPoHdrIssu.GetDataByPendReqInqByHdrPcode(Convert.ToDecimal(cboYear.SelectedValue.ToString()), cboReqFor.SelectedValue.ToString());
                }
            }
            else
            {
                dtPoHdrIssu = taPoHdrIssu.GetDataByPendReqInqByHdrAcode(Convert.ToDecimal(cboYear.SelectedValue.ToString()), empRef);
            }
            Session["data"] = dtPoHdrIssu;
            SetItemGridData();
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