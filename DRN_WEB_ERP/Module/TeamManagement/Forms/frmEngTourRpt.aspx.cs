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
    public partial class frmEngTourRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            txtRptFrmDt.Text = DateTime.Now.AddDays(-15).ToString("dd/MM/yyyy");
            txtRptToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taEng = new View_Emp_BascTableAdapter();
            var dtEng = new DataTable();
            if (empRef == "000856" || empRef == "000345" || empRef == "000011")//-------------Ali Haider, Farhan
                dtEng = taEng.GetDataByLocDeptEngBrand("100002", "10060", "10063");
            else
                dtEng = taEng.GetDataBySupRef(empRef);

            foreach (dsEmpDet.View_Emp_BascRow dr in dtEng.Rows)
            {
                ddlEngName.Items.Add(new ListItem(dr.EmpName + " >>>" + dr.DesigName + " >>>" + dr.DeptName + " >>>" + dr.EmpId, dr.EmpRefNo));
                ddlEngName.DataBind();
            }
            ddlEngName.Items.Insert(0, "---All---");
            ddlEngName.SelectedValue = empRef;

            var taPurpose = new tbl_Team_Tour_PurposeTableAdapter();
            var dtPurpose = new dsTemManage.tbl_Team_Tour_PurposeDataTable();
            ddlVisitPurpose.DataSource = taPurpose.GetDataByType("ENG");
            ddlVisitPurpose.DataTextField = "RefName";
            ddlVisitPurpose.DataValueField = "RefNo";
            ddlVisitPurpose.DataBind();
            ddlVisitPurpose.Items.Insert(0, new ListItem("-----All-----", "0"));
        }

        #region Grid Data
        public string GetEmpName(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
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

                //LoadTourSumData();

                //LoadChart();

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
                    dtPlanDet = taTourPlan.GetDataByDate(Convert.ToDateTime(txtRptFrmDt.Text), Convert.ToDateTime(txtRptToDt.Text), "ENG");
                else
                    dtPlanDet = taTourPlan.GetDataByEmp(ddlEngName.SelectedValue.ToString(), Convert.ToDateTime(txtRptFrmDt.Text), Convert.ToDateTime(txtRptToDt.Text), "ENG");

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

        private void LoadTourSumData()
        {            
            try
            {
                var taPlan = new View_Team_Plan_TourTableAdapter();
                GridView1.DataSource = taPlan.GetDataByEmpMonth(ddlEngName.SelectedValue.ToString(), Convert.ToDateTime(txtRptFrmDt.Text).ToString("MM") + "/" + Convert.ToDateTime(txtRptToDt.Text).ToString("yyyy"));
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        private void LoadChart()
        {
            try
            {
                Chart4.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                Chart4.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = false;
                //Chart4.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -90;
                Chart4.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;

                Chart4.Series["Plan"].IsValueShownAsLabel = true;
                Chart4.Series["Visit"].IsValueShownAsLabel = true;

                Chart4.Series["Plan"].BorderWidth = 2;
                Chart4.Series["Visit"].BorderWidth = 2;

                Legend leg = new Legend();
                Chart4.Legends.Add(leg);

                using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DRNConStr"].ConnectionString))
                {
                    string CmdString = "SELECT View_Tem_Plan.Tem_Plan_Emp_Ref, View_Tem_Plan.Tem_Plan_Ext_Data1, View_Tem_Plan.TotPlan, View_Tem_Plan.TotPlanDone, " +
                                       "View_Tem_Tour.Tem_Tour_Emp_Ref, View_Tem_Tour.Tem_Tour_Ext_Data1, View_Tem_Tour.TotVisit, View_Emp_Basc.EmpName " +
                                       "FROM View_Tem_Plan LEFT OUTER JOIN View_Emp_Basc ON View_Tem_Plan.Tem_Plan_Emp_Ref = View_Emp_Basc.EmpRefNo " +
                                       "FULL OUTER JOIN View_Tem_Tour ON View_Tem_Plan.Tem_Plan_Emp_Ref = View_Tem_Tour.Tem_Tour_Emp_Ref AND " +
                                       "View_Tem_Plan.Tem_Plan_Ext_Data1 = View_Tem_Tour.Tem_Tour_Ext_Data1 " +
                                       "WHERE (View_Tem_Plan.Tem_Plan_Emp_Ref = '" + ddlEngName.SelectedValue.ToString() + "') " +
                                       "AND (View_Tem_Plan.Tem_Plan_Ext_Data1 = '" + Convert.ToDateTime(txtRptFrmDt.Text).ToString("MM") + "/" + Convert.ToDateTime(txtRptToDt.Text).ToString("yyyy") + "')";

                    SqlCommand cmd2 = null;
                    cmd2 = new SqlCommand(CmdString, con);
                    cmd2.CommandType = CommandType.Text;

                    SqlDataAdapter myAdapter = new SqlDataAdapter();
                    DataSet myDataSet = new DataSet();
                    myAdapter.SelectCommand = cmd2;
                    myAdapter.Fill(myDataSet);
                    DataView dataView = new DataView(myDataSet.Tables[0]);

                    Chart4.Series["Plan"].ChartType = SeriesChartType.Column;
                    Chart4.Series["Visit"].ChartType = SeriesChartType.Column;

                    Chart4.Series[0].Points.DataBindXY(dataView, "Tem_Plan_Ext_Data1", dataView, "TotPlan");
                    Chart4.Series[1].Points.DataBindXY(dataView, "Tem_Plan_Ext_Data1", dataView, "TotVisit");

                    //Chart4.ChartAreas["ChartArea1"].AxisX.Title = "Period";
                    //Chart4.ChartAreas["ChartArea1"].AxisY.Title = "Total";
                }
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
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
                    if (ddlVisitPurpose.SelectedIndex == 0)
                        dtTourDet = taTour.GetDataByDate(Convert.ToDateTime(txtRptFrmDt.Text), Convert.ToDateTime(txtRptToDt.Text), "ENG");
                    else
                        dtTourDet = taTour.GetDataByDatePurpose(Convert.ToDateTime(txtRptFrmDt.Text), Convert.ToDateTime(txtRptToDt.Text), "ENG", ddlVisitPurpose.SelectedItem.ToString());
                else
                    if (ddlVisitPurpose.SelectedIndex == 0)
                        dtTourDet = taTour.GetDataByEmp(ddlEngName.SelectedValue.ToString(), Convert.ToDateTime(txtRptFrmDt.Text), Convert.ToDateTime(txtRptToDt.Text), "ENG");
                    else
                        dtTourDet = taTour.GetDataByEmpPurpose(ddlEngName.SelectedValue.ToString(), Convert.ToDateTime(txtRptFrmDt.Text), Convert.ToDateTime(txtRptToDt.Text), "ENG", ddlVisitPurpose.SelectedItem.ToString());

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

        protected void ddlVisitPurpose_SelectedIndexChanged(object sender, EventArgs e)
        {            
            try
            {
                LoadTourData();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }
    }
}