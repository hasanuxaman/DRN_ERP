using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using DRN_WEB_ERP.DRNDataSetTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;

namespace DRN_WEB_ERP
{
    public partial class frmAttndSumRptAdm : System.Web.UI.Page
    {
        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taLoc = new tblHrmsOffLocTableAdapter();
            cboLoc.DataSource = taLoc.GetDataByAsc();
            cboLoc.DataValueField = "LocRefNo";
            cboLoc.DataTextField = "LocName";
            cboLoc.DataBind();
            cboLoc.Items.Insert(0, new ListItem("---ALL---", "0"));

            cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));

            cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));

            cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));

            ReportViewer1.Visible = false;
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            try
            {
                var taDs = new View_Emp_Attnd_SumTableAdapter();

                var fromDate = txtFromDate.Text.Trim();
                var toDate = txtToDate.Text.Trim();

                var qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Attnd_Sum]')) DROP VIEW [dbo].[View_Attnd_Sum]";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                qrySqlStr = "CREATE view [dbo].[View_Attnd_Sum] as Select EmpRefNo,(DATEDIFF(day,CONVERT(datetime,'" + fromDate + "',103),CONVERT(datetime,'" + toDate + "',103))+1) as TotDays, " +
                          "((DATEDIFF(day,CONVERT(datetime,'" + fromDate + "',103),CONVERT(datetime,'" + toDate + "',103))+1) - (select count(HolDate) from tblHrmsHoliday " +
                          "where CONVERT(datetime,HolDate,103) between CONVERT(datetime,'" + fromDate + "',103) and CONVERT(datetime,'" + toDate + "',103))) as WrkDays, " +
                          "COUNT(CASE WHEN AttndFlag = 'P' THEN 1 END) as PrsDays, COUNT(CASE WHEN AttndFlag = 'L' THEN 1 END) as LvDays, (select count(HolDate) from " +
                          "tblHrmsHoliday where CONVERT(datetime,HolDate,103) between CONVERT(datetime,'" + fromDate + "',103) and CONVERT(datetime,'" + toDate + "',103)) as HolDay, " +
                          "(DATEDIFF(day,CONVERT(datetime,'" + fromDate + "',103),CONVERT(datetime,'" + toDate + "',103))+1)- " +
                          "((COUNT(CASE WHEN AttndFlag = 'P' THEN 1 END)) + (COUNT(CASE WHEN AttndFlag = 'L' THEN 1 END)) " +
                          "+ (select count(HolDate) from tblHrmsHoliday where CONVERT(datetime,HolDate,103) " +
                          "between CONVERT(datetime,'" + fromDate + "',103) and CONVERT(datetime,'" + toDate + "',103))) as AbsDay, " +
                          "COUNT(CASE WHEN CONVERT(int,AttndLateMin) > 0 THEN 1 END) as LateDays, SUM(CONVERT(int,AttndLateMin)) as LateMin, " +
                          "convert(decimal(18,2),(((CONVERT(decimal(18,2),COUNT(CASE WHEN CONVERT(int,AttndLateMin) > 0 THEN 1 END)))/(convert(decimal(18,2), " +
                          "((DATEDIFF(day,CONVERT(datetime,'" + fromDate + "',103),CONVERT(datetime,'" + toDate + "',103))+1) - " +
                          "(select count(HolDate) from tblHrmsHoliday where CONVERT(datetime,HolDate,103) " +
                          "between CONVERT(datetime,'" + fromDate + "',103) and CONVERT(datetime,'" + toDate + "',103)))))) * 100 )) as LatePerc, " +
                          "COUNT(CASE WHEN CONVERT(int,AttndEarlyMin) > 0 THEN 1 END) as EarlyDays, " +
                          "SUM(CONVERT(int,AttndEarlyMin)) as EarlyMin, convert(decimal(18,2),(((CONVERT(decimal(18,2),COUNT(CASE WHEN CONVERT(int,AttndEarlyMin) > 0 THEN 1 END))) " +
                          "/(convert(decimal(18,2),((DATEDIFF(day,CONVERT(datetime,'" + fromDate + "',103),CONVERT(datetime,'" + toDate + "',103))+1) - " +
                          "(select count(HolDate) from tblHrmsHoliday where CONVERT(datetime,HolDate,103) " +
                          "between CONVERT(datetime,'" + fromDate + "',103) and CONVERT(datetime,'" + toDate + "',103)))))) * 100 )) as EarlyPerc " +
                          "from tblHrmsEmpDayAttnd where CONVERT(datetime,AttndDate,103) between CONVERT(datetime,'" + fromDate + "',103) " +
                          "and CONVERT(datetime,'" + toDate + "',103) group by EmpRefNo";
                dbCon.ExecuteSQLStmt(qrySqlStr);

                //ReportViewer1.Visible is set to false in design mode
                ReportViewer1.Visible = true;

                ReportDataSource datasource = new ReportDataSource("DataSet1", GetData());

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("AttndSumRpt.rdlc");

                LocalReport rep = ReportViewer1.LocalReport;
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                //if (thisDataSet.Tables[0].Rows.Count == 0)
                //{
                //    //lblMessage.Text = "Sorry, no products under this category!";
                //}

                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex) { }
        }

        public DataTable GetData()
        {
            SqlDataAdapter dta = new SqlDataAdapter();
            string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DRNConStr"].ToString();
            SqlConnection con = new SqlConnection(connStr);
            DataSet ds = new DataSet();

            var qryStr = "SELECT * FROM [View_Emp_Attnd_Sum]";

            #region Load Emp List
            if (cboLoc.SelectedIndex == 0)
                //---ALL Location
                qryStr = "SELECT * FROM [View_Emp_Attnd_Sum]";
            else
                if (cboDept.SelectedIndex == 0)
                    //---Selected Location
                    qryStr = "SELECT * FROM [View_Emp_Attnd_Sum] WHERE OffLocRefNo ='" + cboLoc.SelectedValue.ToString() + "'";
                else
                    if (cboSec.SelectedIndex == 0)
                        //---Selected Department
                        qryStr = "SELECT * FROM [View_Emp_Attnd_Sum] WHERE OffLocRefNo ='" + cboLoc.SelectedValue.ToString() + "' and DeptRefNo ='" + cboDept.SelectedValue.ToString() + "'";
                    else
                        if (cboShift.SelectedIndex == 0)
                            //---Selected Section
                            qryStr = "SELECT * FROM [View_Emp_Attnd_Sum] WHERE OffLocRefNo ='" + cboLoc.SelectedValue.ToString() + "' and DeptRefNo ='" + cboDept.SelectedValue.ToString() + "' and SecRefNo='" + cboSec.SelectedValue.ToString() + "'";
                        else
                            //---Selected Shift
                            qryStr = "SELECT * FROM [View_Emp_Attnd_Sum] WHERE OffLocRefNo ='" + cboLoc.SelectedValue.ToString() + "' and DeptRefNo ='" + cboDept.SelectedValue.ToString() + "' and SecRefNo='" + cboSec.SelectedValue.ToString() + "' and ShiftRefNo='" + cboShift.SelectedValue.ToString() + "'";
            #endregion
            
            SqlCommand cmd = new SqlCommand(qryStr, con);
            dta.SelectCommand = cmd;
            dta.SelectCommand.Connection = con;
            dta.Fill(ds, "View_Emp_Attnd_Sum");
            return ds.Tables[0];
        }

        protected void cboLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoc.SelectedIndex != 0)
            {
                cboDept.Items.Clear();
                cboSec.Items.Clear();
                cboShift.Items.Clear();
                cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
                cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));

                var taDep = new tblHrmsDeptTableAdapter();
                cboDept.DataSource = taDep.GetDataByLocRef(cboLoc.SelectedValue.ToString());
                cboDept.DataValueField = "DeptRefNo";
                cboDept.DataTextField = "DeptName";
                cboDept.DataBind();
                cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
            else
            {
                cboDept.Items.Clear();
                cboSec.Items.Clear();
                cboShift.Items.Clear();
                cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));
                cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
                cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));
            }

            ReportViewer1.Visible = true;
            ReportDataSource datasource = new ReportDataSource("DataSet1", GetData());
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("AttndSumRpt.rdlc");
            LocalReport rep = ReportViewer1.LocalReport;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.Refresh();
        }

        protected void cboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDept.SelectedIndex != 0)
            {
                cboSec.Items.Clear();
                cboShift.Items.Clear();
                cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
                cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));

                var taSec = new tblHrmsSecTableAdapter();
                cboSec.DataSource = taSec.GetDataByDeptRef(cboDept.SelectedValue.ToString());
                cboSec.DataValueField = "SecRefNo";
                cboSec.DataTextField = "SecName";
                cboSec.DataBind();
                cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
            else
            {
                cboSec.Items.Clear();
                cboShift.Items.Clear();
                cboSec.Items.Insert(0, new ListItem("---ALL---", "0"));
                cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));
            }

            ReportViewer1.Visible = true;
            ReportDataSource datasource = new ReportDataSource("DataSet1", GetData());
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("AttndSumRpt.rdlc");
            LocalReport rep = ReportViewer1.LocalReport;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.Refresh();
        }

        protected void cboSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSec.SelectedIndex != 0)
            {
                cboShift.Items.Clear();
                cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));

                var taShift = new tblHrmsShiftTableAdapter();
                cboShift.DataSource = taShift.GetDataBySecRef(cboSec.SelectedValue.ToString());
                cboShift.DataValueField = "ShiftRefNo";
                cboShift.DataTextField = "ShiftName";
                cboShift.DataBind();
                cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
            else
            {
                cboShift.Items.Clear();
                cboShift.Items.Insert(0, new ListItem("---ALL---", "0"));
            }

            ReportViewer1.Visible = true;
            ReportDataSource datasource = new ReportDataSource("DataSet1", GetData());
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("AttndSumRpt.rdlc");
            LocalReport rep = ReportViewer1.LocalReport;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.Refresh();
        }     
    }
}