using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace DRN_WIN_ERP.Modules.Sales
{
    public partial class frmShowSalesReport : Form
    {
        public frmShowSalesReport()
        {
            InitializeComponent();
        }

        private void frmShowSalesReport_Load(object sender, EventArgs e)
        {
            Show_Challan_Report();

            //ReportDocument cryRpt = new ReportDocument();
            //string yourpath = @"E:\DOREEN\DRN_ERP\DRN_WEB_ERP - 23-11-16\DRN_WIN_ERP\Modules\Sales\rptDelChln.rpt";
            //cryRpt.Load(yourpath);
            ////cryRpt.Load(@"\Modules\Sales\rptDelChln.rpt");
            //crystalReportViewer1.ReportSource = cryRpt;
            //crystalReportViewer1.Refresh();

            /*
            rptDelChln delChln = new rptDelChln();
            delChln.RecordSelectionFormula = "";
            crystalReportViewer1.ReportSource = delChln;
            crystalReportViewer1.Refresh();           

            DbConnectionInfo.SetConnectionString(ConfigurationSettings.AppSettings[0]);
            TableLogOnInfo logOnInfo;
            ConnectionInfo connectionInfo;
            foreach (Table table in m_cryRpt.Database.Tables)
            {
                logOnInfo = table.LogOnInfo;
                connectionInfo = logOnInfo.ConnectionInfo;
                // Set the Connection parameters.
                connectionInfo.DatabaseName = DbConnectionInfo.InitialCatalog;
                connectionInfo.ServerName = DbConnectionInfo.ServerName;
                if (!DbConnectionInfo.UseIntegratedSecurity)
                {
                    connectionInfo.Password = DbConnectionInfo.Password;
                    connectionInfo.UserID = DbConnectionInfo.UserName;
                }
                else
                {
                    connectionInfo.IntegratedSecurity = true;
                }
                table.ApplyLogOnInfo(logOnInfo);
            }
            crystalReportViewer1.ReportSource = m_cryRpt;
            crystalReportViewer1.Refresh();
             */
        }

        private void Show_Challan_Report()
        {
            string ConnectionString;
            string[] ff, ss;

            SqlConnection conn = new SqlConnection();
            ConnectionInfo crConnInfo = new ConnectionInfo();
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            //rptDelChln delChln = new rptDelChln();

            ReportDocument cryRpt = new ReportDocument();
            

            try
            {                
                //var connection = System.Configuration.ConfigurationManager.ConnectionStrings["DRNConStr"].ConnectionString;
                //string constr = System.Configuration.ConfigurationManager.ConnectionStrings["DRNConStr"].ConnectionString;

                string constr = DRN_WIN_ERP.WinGlobalClass.clsGlobalProperties.drnConStr.ToString();

                ConnectionString = constr;
                conn.ConnectionString = ConnectionString;
                conn.Open();

                ff = constr.Split('=');

                ss = ff[1].Split(';');
                crConnInfo.ServerName = ss[0];

                ss = ff[2].Split(';');
                crConnInfo.DatabaseName = ss[0];

                ss = ff[3].Split(';');
                crConnInfo.UserID = ss[0];

                ss = ff[4].Split(';');
                crConnInfo.Password = ss[0];

                crConnInfo.ServerName = crConnInfo.ServerName;
                crConnInfo.DatabaseName = crConnInfo.DatabaseName;
                crConnInfo.UserID = crConnInfo.UserID;
                crConnInfo.Password = crConnInfo.Password;

                var rptFilePath = WinGlobalClass.clsGlobalProperties.rptFilePath;
                var rptFormula = WinGlobalClass.clsGlobalProperties.rptFormula;

                //cryRpt.Load(Server.MapPath(rptFilePath));                
                cryRpt.Load(rptFilePath);

                //CrTables = delChln.Database.Tables;
                CrTables = cryRpt.Database.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                }

                foreach (FormulaFieldDefinition thisFormulaField in cryRpt.DataDefinition.FormulaFields)
                {
                    if (thisFormulaField.FormulaName == "{@fFromDate}" || thisFormulaField.FormulaName == "{@DFrm}" || thisFormulaField.FormulaName == "{@fDate}" || thisFormulaField.FormulaName == "{@datefrom}")
                    {
                        thisFormulaField.Text = "'" + WinGlobalClass.clsGlobalProperties.RptDateFrom +"'";
                    }
                    if (thisFormulaField.FormulaName == "{@fToDate}" || thisFormulaField.FormulaName == "{@DTo}" || thisFormulaField.FormulaName == "{@dateto}")
                    {
                        thisFormulaField.Text = "'" + WinGlobalClass.clsGlobalProperties.RptDateTo + "'";
                    }
                    if (thisFormulaField.FormulaName == "{@fRptHdr}")
                    {
                        thisFormulaField.Text = "'" + WinGlobalClass.clsGlobalProperties.RptHdr + "'";
                    }
                }

                if (rptFormula != "")
                {
                    crystalReportViewer1.SelectionFormula = rptFormula;
                }                                
                
                //crystalReportViewer1.ReportSource = delChln;
                crystalReportViewer1.ReportSource = cryRpt;
                crystalReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data Processing Error.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void frmShowSalesReport_FormClosing(object sender, FormClosingEventArgs e)
        {            
            crystalReportViewer1.Dispose();         
            GC.Collect();
        }
    }
}
