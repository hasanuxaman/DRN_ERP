using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.TeamManagement.DataSets;
using DRN_WEB_ERP.Module.TeamManagement.DataSets.dsTemManageTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.TeamManagement.Forms
{
    public partial class frmAccTourRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            txtRptFrmDt.Text = DateTime.Now.AddDays(-15).ToString("dd/MM/yyyy");
            txtRptToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");

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
            ddlEngName.Items.Insert(0, "---All---");

            ddlEngName.SelectedValue = empRef;
        }

        #region Grid Data
        public string GetEmpName(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].EmpName.ToString();
                return empStr;
            }
            catch (Exception ex) { return empStr; }
        }
        #endregion

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                LoadPlanData();

                LoadTourData();
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        private void LoadPlanData()
        {
            var taTourPlan = new tbl_Team_Tour_PlanTableAdapter();
            var dtPlanDet = new dsTemManage.tbl_Team_Tour_PlanDataTable();

            try
            {
                if (ddlEngName.SelectedIndex == 0)
                    dtPlanDet = taTourPlan.GetDataByDate(Convert.ToDateTime(txtRptFrmDt.Text), Convert.ToDateTime(txtRptToDt.Text), "CRD");
                else
                    dtPlanDet = taTourPlan.GetDataByEmp(ddlEngName.SelectedValue.ToString(), Convert.ToDateTime(txtRptFrmDt.Text), Convert.ToDateTime(txtRptToDt.Text), "CRD");

                gvTourPlan.DataSource = dtPlanDet;
                gvTourPlan.DataBind();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        private void LoadTourData()
        {
            var taTour = new tbl_Team_TourTableAdapter();
            var dtTourDet = new dsTemManage.tbl_Team_TourDataTable();

            try
            {
                if (ddlEngName.SelectedIndex == 0)
                    dtTourDet = taTour.GetDataByDate(Convert.ToDateTime(txtRptFrmDt.Text), Convert.ToDateTime(txtRptToDt.Text), "CRD");
                else
                    dtTourDet = taTour.GetDataByEmp(ddlEngName.SelectedValue.ToString(), Convert.ToDateTime(txtRptFrmDt.Text), Convert.ToDateTime(txtRptToDt.Text), "CRD");

                gvTour.DataSource = dtTourDet;
                gvTour.DataBind();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
    }
}