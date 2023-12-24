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
using System.Net.Mail;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsTranTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmEmpAttndRpt : System.Web.UI.Page
    {
        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        string rptFile;
        string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            if (empRef == "000634" || empRef == "000884")//HRD,Nahamad
                txtEmpRef.Enabled = true;
            else
                txtEmpRef.Enabled = false;

            var taEmpGenInfo = new View_Emp_BascTableAdapter();
            var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRefAct(Convert.ToInt32(empRef));
            if (dtEmpGenInfo.Rows.Count > 0)
                txtEmpRef.Text = dtEmpGenInfo[0].EmpRefNo.ToString() + ":" + dtEmpGenInfo[0].EmpId.ToString() + ":" + dtEmpGenInfo[0].EmpName.ToString();

            var suprRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            var taEmp = new View_Emp_BascTableAdapter();
            if (empRef == "000555" || empRef == "000011" || empRef == "000884" || empRef == "000150" || empRef == "000629" || empRef == "000634")//ceo,imran,Nahamad,saiful,rakiba,HRD
                cboEmp.DataSource = taEmp.GetDataByAsc();
            if (empRef == "000914" || empRef == "000509" || empRef == "000510" || empRef == "000535" || empRef == "000549" || empRef == "000732" || empRef == "000011") //sohag,alwashib,riaz,Alif,Saroar,kawshik-----Audit
                cboEmp.DataSource = taEmp.GetDataByAuditReq(suprRef.ToString());
            else
                cboEmp.DataSource = taEmp.GetDataBySupRef(suprRef.ToString());
            cboEmp.DataValueField = "EmpRefNo";
            cboEmp.DataTextField = "EmpName";
            cboEmp.DataBind();
            cboEmp.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            reportInfo();
            var url = "frmShowHrmsReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportInfo()
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            var taAttnDt = new tblHrmsAttnReportDateTableAdapter();
            var dtAttnDt = taAttnDt.GetDataByEmpRefNo(empRef.ToString());
            if (dtAttnDt.Rows.Count > 0)
                taAttnDt.DeleteAttnDate(empRef);

            TimeSpan dateDiff = (Convert.ToDateTime(txtToDate.Text.Trim())) - (Convert.ToDateTime(txtFromDate.Text.Trim()));
            var totalDays = Convert.ToInt32(dateDiff.TotalDays);
            for (int i = 0; i <= totalDays; i++)
            {
                taAttnDt.InsertAttnDate(empRef, Convert.ToDateTime(txtFromDate.Text.Trim()).AddDays(i), "1", "");
            }
           
            var qrySqlStr = "";
            qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Emp_Attnd_Report]')) DROP VIEW [dbo].[View_Emp_Attnd_Report]";
            dbCon.ExecuteSQLStmt(qrySqlStr);

            if (empRef == "000555" || empRef == "000011" || empRef == "000884" || empRef == "000150" || empRef == "000629" || empRef == "000634")//ceo,imran,Nahamad,saiful,rakiba,HRD
            {
                if (cboEmp.SelectedIndex == 0)
                {
                    qrySqlStr = "Create view View_Emp_Attnd_Report as select * from tblHrmsEmpDayAttnd " +
                                "WHERE CONVERT(datetime, AttndDate, 103) BETWEEN CONVERT(DATETIME,'" + txtFromDate.Text.Trim() + "', 103) " +
                                "AND CONVERT(DATETIME,'" + txtToDate.Text.Trim() + "', 103)";
                }
                else
                {
                    qrySqlStr = "Create view View_Emp_Attnd_Report as select * from tblHrmsEmpDayAttnd " +
                                "WHERE CONVERT(datetime, AttndDate, 103) BETWEEN CONVERT(DATETIME,'" + txtFromDate.Text.Trim() + "', 103) " +
                                "AND CONVERT(DATETIME,'" + txtToDate.Text.Trim() + "', 103) AND EmpRefNo ='" + empRef + "'";
                }
            }
            else if (empRef == "000914" || empRef == "000509" || empRef == "000510" || empRef == "000535" || empRef == "000549" || empRef == "000732" || empRef == "000011") //sohag,alwashib,riaz,Alif,Saroar,kawshik-----Audit
            {
                if (cboEmp.SelectedIndex == 0)
                {
                    qrySqlStr = "Create view View_Emp_Attnd_Report as select * from tblHrmsEmpDayAttnd " +
                                "WHERE CONVERT(datetime, AttndDate, 103) BETWEEN CONVERT(DATETIME,'" + txtFromDate.Text.Trim() + "', 103) " +
                                "AND CONVERT(DATETIME,'" + txtToDate.Text.Trim() + "', 103) AND LocCode = 'ECIL' " +
                                "AND DeptCode IN ('SALE', 'BD', 'MKT', 'BETS') OR EmpSuprId ='" + empRef + "' OR EmpRefNo ='" + empRef + "'";
                }
                else
                {
                    qrySqlStr = "Create view View_Emp_Attnd_Report as select * from tblHrmsEmpDayAttnd " +
                                "WHERE CONVERT(datetime, AttndDate, 103) BETWEEN CONVERT(DATETIME,'" + txtFromDate.Text.Trim() + "', 103) " +
                                "AND CONVERT(DATETIME,'" + txtToDate.Text.Trim() + "', 103) AND EmpRefNo ='" + empRef + "'";
                }
            }
            else
            {
                if (cboEmp.SelectedIndex == 0)
                {
                    qrySqlStr = "Create view View_Emp_Attnd_Report as select * from tblHrmsEmpDayAttnd " +
                                "WHERE CONVERT(datetime, AttndDate, 103) BETWEEN CONVERT(DATETIME,'" + txtFromDate.Text.Trim() + "', 103) " +
                                "AND CONVERT(DATETIME,'" + txtToDate.Text.Trim() + "', 103) and EmpSuprId ='" + empRef + "' OR EmpRefNo ='" + empRef + "'";
                }
                else
                {
                    qrySqlStr = "Create view View_Emp_Attnd_Report as select * from tblHrmsEmpDayAttnd " +
                                "WHERE CONVERT(datetime, AttndDate, 103) BETWEEN CONVERT(DATETIME,'" + txtFromDate.Text.Trim() + "', 103) " +
                                "AND CONVERT(DATETIME,'" + txtToDate.Text.Trim() + "', 103) and EmpRefNo ='" + empRef + "'";
                }
            }
            dbCon.ExecuteSQLStmt(qrySqlStr);

            rptFile = "~/Module/HRMS/Reports/rptHrmsEpmDayAttn.rpt";

            rptSelcFormula = "";
            Session["RptDateFrom"] = txtFromDate.Text.Trim();
            Session["RptDateTo"] = txtToDate.Text.Trim();
            Session["RptFilePath"] = rptFile;
            Session["RptFormula"] = rptSelcFormula;
        }
    }
}