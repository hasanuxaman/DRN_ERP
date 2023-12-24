using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.IO;

namespace DRN_WEB_ERP.Module.Inventory.Forms
{
    public partial class frmShowIFrameRpt : System.Web.UI.Page
    {
        ReportDocument cryRpt = new ReportDocument();

        string rptFilePath;
        string rptFormula;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["RptFilePath"] != null || Session["RptFormula"] != null)
                show_report();
        }

        private void show_report()
        {
            string ConnectionString;
            string[] ff, ss;

            SqlConnection conn = new SqlConnection();
            ConnectionInfo crConnInfo = new ConnectionInfo();
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            try
            {
                string constr = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
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

                rptFilePath = Session["RptFilePath"].ToString();
                rptFormula = Session["RptFormula"].ToString();

                cryRpt.Load(Server.MapPath(rptFilePath));

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
                        thisFormulaField.Text = "'" + Session["RptDateFrom"].ToString() + "'";
                    }
                    if (thisFormulaField.FormulaName == "{@fToDate}" || thisFormulaField.FormulaName == "{@DTo}" || thisFormulaField.FormulaName == "{@dateto}")
                    {
                        thisFormulaField.Text = "'" + Session["RptDateTo"].ToString() + "'";
                    }
                    if (thisFormulaField.FormulaName == "{@fRptHdr}")
                    {
                        thisFormulaField.Text = "'" + Session["RptHdr"].ToString() + "'";
                    }
                }

                if (rptFormula != "")
                {
                    CrystalReportViewer1.SelectionFormula = rptFormula;
                }
                CrystalReportViewer1.ReportSource = cryRpt;
                CrystalReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
        {
            cryRpt.Close();
            cryRpt.Dispose();
            GC.Collect();
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            CrystalReportViewer1.Dispose();
        }

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    //--For PDF-----------
        //    //CrystalDecisions.CrystalReports.Engine.ReportDocument tmpReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        //    //string strFileName = Server.MapPath("~/Module/Inventory/Reports/rptInvTranList.rpt");
        //    ////tmpReport.Load(strFileName, OpenReportMethod.OpenReportByDefault);
        //    //tmpReport.Load(strFileName);

        //    //MemoryStream oStream = new MemoryStream();
        //    //oStream = (MemoryStream)tmpReport.ExportToStream(ExportFormatType.PortableDocFormat);

        //    //Response.Clear();
        //    //Response.Buffer = true;
        //    //Response.ContentType = "application/pdf";
        //    //Response.BinaryWrite(oStream.ToArray());
        //    //Response.End();
        //    //----------End 


        //    cryRpt.Load(Server.MapPath("~/Module/Inventory/Reports/rptInvTranList.rpt"));

        //    CrystalReportViewer1.ReportSource = cryRpt;
        //    CrystalReportViewer1.RefreshReport();
        //}        
    }
}