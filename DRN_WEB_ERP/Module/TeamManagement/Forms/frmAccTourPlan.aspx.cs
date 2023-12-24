using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.TeamManagement.DataSets;
using DRN_WEB_ERP.Module.TeamManagement.DataSets.dsTemManageTableAdapters;

namespace DRN_WEB_ERP.Module.TeamManagement.Forms
{
    public partial class frmAccTourPlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtPlanFromDt.Text = DateTime.Now.AddDays(-15).ToString("dd/MM/yyyy");
            txtPlanToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtPlanDateTbl.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            var taEng = new View_Emp_BascTableAdapter();
            var dtEng = new DataTable();
            if (empRef == "000568" || empRef == "000210" || empRef == "000011")
                dtEng = taEng.GetDataByLocDeptSec("100002", "100055", "100079");
            else
                dtEng = taEng.GetDataBySupRef(empRef);
            foreach (dsEmpDet.View_Emp_BascRow dr in dtEng.Rows)
            {
                ddlEngName.Items.Add(new ListItem(dr.EmpName + " >>>" + dr.DesigName + " >>>" + dr.DeptName + " >>>" + dr.EmpId, dr.EmpRefNo));
                ddlEngName.DataBind();
            }
            ddlEngName.Items.Insert(0, "---Select---");

            ddlEngName.SelectedValue = empRef;

            ddlEngName.Enabled = empRef == "000011";

            LoadSavedData();
        }

        #region Tour Plan Details Gridview
        protected void LoadInitTourGridData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PLAN_REF", typeof(string));
            dt.Columns.Add("COMP_NAME", typeof(string));
            dt.Columns.Add("COMP_ADR", typeof(string));
            dt.Columns.Add("CONT_NO", typeof(string));
            dt.Columns.Add("CONT_NAME", typeof(string));
            dt.Columns.Add("CONT_DESIG", typeof(string));
            dt.Columns.Add("PLAN_DATE", typeof(string));
            dt.Columns.Add("VISIT_PURPOSE", typeof(string));
            dt.Columns.Add("VISIT_NO", typeof(string));
            ViewState["dtPlanDet"] = dt;
        }

        protected void SetTourGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtPlanDet"];

                gvTourPlan.DataSource = dt;
                gvTourPlan.DataBind();
            }
            catch (Exception ex) { }
        }

        private void LoadSavedData()
        {
            var taTourPlan = new tbl_Team_Tour_PlanTableAdapter();
            try
            {
                var flag = "";

                LoadInitTourGridData();
                SetTourGridData();

                var dt = new DataTable();
                dt = (DataTable)ViewState["dtPlanDet"];
                var dtPlanDet = taTourPlan.GetDataByEmp(ddlEngName.SelectedValue.ToString(), Convert.ToDateTime(txtPlanFromDt.Text), Convert.ToDateTime(txtPlanToDt.Text), "CRD");
                foreach (dsTemManage.tbl_Team_Tour_PlanRow dr in dtPlanDet.Rows)
                {
                    dt.Rows.Add(dr.Tem_Plan_Ref, dr.Tem_Plan_Com, dr.Tem_Plan_Com_Adr, dr.Tem_Plan_Com_Cont_No, dr.Tem_Plan_Com_Cont_Name,
                        dr.Tem_Plan_Com_Cont_Ref, dr.Tem_Plan_Tour_Date.ToString("dd/MM/yyyy"), dr.Tem_Plan_Tour_Type, dr.Tem_Plan_Ext_Data2);

                    flag = dtPlanDet[0].Tem_Plan_Flag;
                }
                ViewState["dtPlanDet"] = dt;
                SetTourGridData();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            var taTourPlan = new tbl_Team_Tour_PlanTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taTourPlan.Connection);

            try
            {
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfPlanId = (HiddenField)(row.FindControl("hfPlanId"));             

                taTourPlan.AttachTransaction(myTran);

                var dtTourPlan = taTourPlan.GetMaxPlanRef(Convert.ToDateTime(txtPlanDateTbl.Text).Year, "P", "CRD");
                var planRef = dtTourPlan == null ? 1 : Convert.ToInt32(dtTourPlan) + 1;
                var nextPlanRef = Convert.ToDateTime(txtPlanDateTbl.Text).ToString("MM") + Convert.ToDateTime(txtPlanDateTbl.Text).ToString("yy") + "-" + planRef.ToString("000000");

                taTourPlan.UpdatePlanFlag("PLN-DR-" + nextPlanRef, "P", hfPlanId.Value.ToString());

                myTran.Commit();

                LoadSavedData();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvTourPlan_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gvTourPlan.EditIndex = -1;
                SetTourGridData();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void gvTourPlan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState != DataControlRowState.Edit && e.Row.RowState != (DataControlRowState.Edit | DataControlRowState.Alternate))
            {
                var taTourPlan = new tbl_Team_Tour_PlanTableAdapter();
                var dtTourPlan = taTourPlan.GetDataByPlanRef(((HiddenField)e.Row.FindControl("hfPlanId")).Value);
                if (dtTourPlan.Rows.Count > 0)
                {
                    var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                    if (empRef == "000011")
                    {
                        ((Button)e.Row.FindControl("btnPost")).Visible = true;
                        ((ImageButton)e.Row.FindControl("imgBtnEditData")).Visible = true;
                        ((ImageButton)e.Row.FindControl("imgBtnDelete")).Visible = true;
                    }
                    else
                    {
                        ((Button)e.Row.FindControl("btnPost")).Visible = dtTourPlan[0].Tem_Plan_Flag == "H";
                        ((ImageButton)e.Row.FindControl("imgBtnEditData")).Visible = dtTourPlan[0].Tem_Plan_Flag == "H";
                        ((ImageButton)e.Row.FindControl("imgBtnDelete")).Visible = dtTourPlan[0].Tem_Plan_Flag == "H";
                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow && gvTourPlan.EditIndex == e.Row.RowIndex)
            {
                var dt = new DataTable();
                dt = (DataTable)ViewState["dtPlanDet"];

                //DropDownList ddlCompContRefEdit = (DropDownList)e.Row.FindControl("ddlCompContRefEdit");
                //ddlCompContRefEdit.Items.FindByText(dt.Rows[e.Row.RowIndex]["CONT_REF"].ToString()).Selected = true;

                DropDownList ddlVisitTypeEdit = (DropDownList)e.Row.FindControl("ddlVisitPurposeEdit");
                ddlVisitTypeEdit.Items.FindByText(dt.Rows[e.Row.RowIndex]["VISIT_PURPOSE"].ToString()).Selected = true;
            }
        }

        protected void gvTourPlan_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var taTourPlan = new tbl_Team_Tour_PlanTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taTourPlan.Connection);

            try
            {
                taTourPlan.AttachTransaction(myTran);

                var rowNum = e.RowIndex;

                if (rowNum == -1) return;

                taTourPlan.DeleteHoldDataByRef(((HiddenField)gvTourPlan.Rows[rowNum].FindControl("hfPlanId")).Value);

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Deleted Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                gvTourPlan.EditIndex = -1;
                LoadSavedData();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvTourPlan_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvTourPlan.EditIndex = e.NewEditIndex;
                Session["editIndexPlanDet"] = e.NewEditIndex;
                LoadSavedData();
            }
            catch (Exception) { }
        }

        protected void gvTourPlan_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var taTourPlan = new tbl_Team_Tour_PlanTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taTourPlan.Connection);

            try
            {
                var rowNum = e.RowIndex;

                if (rowNum == -1) return;

                taTourPlan.AttachTransaction(myTran);

                var hfPlanIdEdit = ((HiddenField)gvTourPlan.Rows[rowNum].FindControl("hfPlanIdEdit")).Value.ToString();
                var txtCompNameEdit = ((TextBox)gvTourPlan.Rows[rowNum].FindControl("txtCompNameEdit")).Text.ToString();
                var txtCompAdrEdit = ((TextBox)gvTourPlan.Rows[rowNum].FindControl("txtCompAdrEdit")).Text.ToString();
                var txtCompContNoEdit = ((TextBox)gvTourPlan.Rows[rowNum].FindControl("txtCompContNoEdit")).Text.Trim();
                var txtCompContNameEdit = ((TextBox)gvTourPlan.Rows[rowNum].FindControl("txtCompContNameEdit")).Text.ToString();
                var txtCompContDesigEdit = ((TextBox)gvTourPlan.Rows[rowNum].FindControl("txtCompContDesigEdit")).Text.ToString();
                var txtPlanDateEdit = ((TextBox)gvTourPlan.Rows[rowNum].FindControl("txtPlanDateEdit")).Text.ToString();
                var ddlVisitPurposeEdit = ((DropDownList)gvTourPlan.Rows[rowNum].FindControl("ddlVisitPurposeEdit")).SelectedItem.ToString();
                var txtVisitNoEdit = ((TextBox)gvTourPlan.Rows[rowNum].FindControl("txtVisitNoEdit")).Text.ToString();

                taTourPlan.UpdateTourPlan(ddlEngName.SelectedValue.ToString(), txtCompNameEdit, txtCompAdrEdit, txtCompContNoEdit, txtCompContNameEdit, txtCompContDesigEdit,
                   Convert.ToDateTime(txtPlanDateEdit), ddlVisitPurposeEdit, Convert.ToDateTime(txtPlanDateEdit).ToString("MM") + "/" + Convert.ToDateTime(txtPlanDateEdit).Year,
                   txtVisitNoEdit, "", "", "CRD", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "H", hfPlanIdEdit);

                myTran.Commit();
                gvTourPlan.EditIndex = -1;
                LoadSavedData();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
        #endregion                

        protected void ddlEngName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSavedData();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSavedData();
        }        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var taTourPlan = new tbl_Team_Tour_PlanTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taTourPlan.Connection);

            try
            {
                taTourPlan.AttachTransaction(myTran);

                var dtTourPlan = taTourPlan.GetMaxPlanRef(Convert.ToDateTime(txtPlanDateTbl.Text).Year, "H", "CRD");
                var planRef = dtTourPlan == null ? 1 : Convert.ToInt32(dtTourPlan) + 1;
                var nextPlanRef = Convert.ToDateTime(txtPlanDateTbl.Text).ToString("MM") + Convert.ToDateTime(txtPlanDateTbl.Text).ToString("yy") + "-" + planRef.ToString("000000");

                taTourPlan.InsertTourPlan("TMP-" + nextPlanRef, ddlEngName.SelectedValue.ToString(), txtCompNameTbl.Text.Trim(), txtCompAdrTbl.Text.Trim(),
                    txtCompContNoTbl.Text.Trim(), txtCompContNameTbl.Text, txtCompContDesigTbl.Text.ToString(), Convert.ToDateTime(txtPlanDateTbl.Text),
                    ddlVisitPurposeTbl.SelectedItem.ToString(), Convert.ToDateTime(txtPlanDateTbl.Text).ToString("MM") + "/" + Convert.ToDateTime(txtPlanDateTbl.Text).Year,
                    txtVisitNo.Text.Trim(), "", "", "CRD", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "H");

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                LoadSavedData();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }       
    }
}