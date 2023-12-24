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
using System.IO;
using Microsoft.Reporting.WebForms;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsTranTableAdapters;

namespace DRN_WEB_ERP
{
    public partial class frmAttndDayRptAdm : System.Web.UI.Page
    {
        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taWrkStation = new tblHrmsWorkStationTableAdapter();
            cboWrkStn.DataSource = taWrkStation.GetDataBySortAsc();
            cboWrkStn.DataValueField = "Wrk_Station_Ref";
            cboWrkStn.DataTextField = "Wrk_Station_Name";
            cboWrkStn.DataBind();
            cboWrkStn.Items.Insert(0, new ListItem("---Select---", "0"));

            var taLoc = new tblHrmsOffLocTableAdapter();
            cboLoc.DataSource = taLoc.GetDataByAsc();
            cboLoc.DataValueField = "LocRefNo";
            cboLoc.DataTextField = "LocName";
            cboLoc.DataBind();
            cboLoc.Items.Insert(0, new ListItem("---ALL---", "0"));

            cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        public void GetData()
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
                taAttnDt.InsertAttnDate(empRef, Convert.ToDateTime(txtFromDate.Text.Trim()).AddDays(i), "1", empRef + DateTime.Now.Month + DateTime.Now.Year);
            }

            var qrySqlStr = "";

            qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Emp_Basc_Report]')) DROP VIEW [dbo].[View_Emp_Basc_Report]";
            dbCon.ExecuteSQLStmt(qrySqlStr);

            qrySqlStr = "Create view View_Emp_Basc_Report as SELECT  CAST(dbo.tblHrmsEmp.EmpRefNo AS varchar(50)) AS EmpRefNo, dbo.tblHrmsEmp.EmpId, " +
                        "dbo.tblHrmsEmp.EmpFirstName + ' ' + dbo.tblHrmsEmp.EmpLastName AS EmpName, dbo.tblHrmsEmp.EmpFatherName, dbo.tblHrmsEmp.EmpMotherName, " +
                        "dbo.tblHrmsEmp.EmpGender, dbo.tblHrmsGender.GenderCode, dbo.tblHrmsGender.GenderName, dbo.tblHrmsEmp.EmpMaritalStatus, " +
                        "dbo.tblHrmsMaritalStatus.MaritalStatusCode, dbo.tblHrmsMaritalStatus.MaritalStatusName, dbo.tblHrmsEmp.EmpSpouse, dbo.tblHrmsEmp.EmpDOB, " +
                        "dbo.tblHrmsEmp.EmpPOB, dbo.tblHrmsEmp.EmpIsAct, dbo.tblHrmsEmp.EmpInactDate, dbo.tblHrmsEmp.EmpSetlFlag, dbo.tblHrmsEmp.EmpSetlDate, " +
                        "dbo.tblHrmsEmp.EmpEntryDate, dbo.tblHrmsEmp.EmpEntryUser, dbo.tblHrmsEmp.EmpUpdateDate, dbo.tblHrmsEmp.EmpUpdateUser, " +
                        "dbo.tblHrmsEmp.EmpMasExtData1, dbo.tblHrmsEmp.EmpMasExtData2, dbo.tblHrmsEmp.EmpMasExtData3, dbo.tblHrmsEmp.EmpStatus, " + "cast('" + empRef  +"' as varchar(50)) + " + " cast('" + DateTime.Now.Month + "' as varchar(50)) " + " + cast('" + DateTime.Now.Year + "' as varchar(50))" + " as EmpFlag, " +
                        "dbo.tblHrmsEmpAdr.EmpAdrRefNo, dbo.tblHrmsEmpAdr.EmpCurAdrHouseRoad, dbo.tblHrmsEmpAdr.EmpCurAdrPO, dbo.tblHrmsEmpAdr.EmpCurAdrPOCode, " +
                        "dbo.tblHrmsEmpAdr.EmpCurAdrThana, dbo.tblHrmsEmpAdr.EmpCurAdrDist, dbo.tblHrmsEmpAdr.EmpPermAdrHouseRoad, dbo.tblHrmsEmpAdr.EmpPermAdrPO, " +
                        "dbo.tblHrmsEmpAdr.EmpPermAdrPOCode, dbo.tblHrmsEmpAdr.EmpPermAdrThana, dbo.tblHrmsEmpAdr.EmpPermAdrDist, " +
                        "dbo.tblHrmsEmpAdr.EmpMailAdrHouseRoad, dbo.tblHrmsEmpAdr.EmpMailAdrPO, dbo.tblHrmsEmpAdr.EmpMailAdrPOCode, dbo.tblHrmsEmpAdr.EmpMailAdrThana, " +
                        "dbo.tblHrmsEmpAdr.EmpMailAdrDist, dbo.tblHrmsEmpAdr.EmpEmerCPName, dbo.tblHrmsEmpAdr.EmpEmerCPRelation, dbo.tblHrmsEmpAdr.EmpEmerCPHouseRoad, " +
                        "dbo.tblHrmsEmpAdr.EmpEmerCPPO, dbo.tblHrmsEmpAdr.EmpEmerCPPOCode, dbo.tblHrmsEmpAdr.EmpEmerCPThana, dbo.tblHrmsEmpAdr.EmpEmerCPDist, " +
                        "dbo.tblHrmsEmpAdr.EmpEmerCPPhone, dbo.tblHrmsEmpAdr.EmpEmerCPCell, dbo.tblHrmsEmpAdr.EmpAdrStatus, dbo.tblHrmsEmpAdr.EmpAdrFlag, " +
                        "dbo.tblHrmsEmpExt.EmpExtRefNo, dbo.tblHrmsEmpExt.EmpReligion, dbo.tblHrmsReligion.ReligionCode, dbo.tblHrmsReligion.ReligionName, " +
                        "dbo.tblHrmsEmpExt.EmpNationality, dbo.tblHrmsEmpExt.EmpBloodgrp, dbo.tblHrmsBldGrp.BldGrpCode, dbo.tblHrmsBldGrp.BldGrpName, " +
                        "dbo.tblHrmsEmpExt.EmpHomephone, dbo.tblHrmsEmpExt.EmpCellPhone, dbo.tblHrmsEmpExt.EmpPerEmail, dbo.tblHrmsEmpExt.EmpIdentiMark, " +
                        "dbo.tblHrmsEmpExt.EmpHeight, dbo.tblHrmsEmpExt.EmpWeight, dbo.tblHrmsEmpExt.EmpColor, dbo.tblHrmsEmpExt.EmpNID, dbo.tblHrmsEmpExt.EmpTIN, " +
                        "dbo.tblHrmsEmpExt.EmpDLNo, dbo.tblHrmsEmpExt.EmpDLIssFrom, dbo.tblHrmsEmpExt.EmpDLExpDate, dbo.tblHrmsEmpExt.EmpPasNo, " +
                        "dbo.tblHrmsEmpExt.EmpPasIssFrom, dbo.tblHrmsEmpExt.EmpPasIssDate, dbo.tblHrmsEmpExt.EmpPasExprDate, dbo.tblHrmsEmpExt.EmpExtData1, " +
                        "dbo.tblHrmsEmpExt.EmpExtData2, dbo.tblHrmsEmpExt.EmpExtData3, dbo.tblHrmsEmpOffice.EmpOffRefNo, dbo.tblHrmsEmpOffice.CompRefNo, " +
                        "dbo.tblHrmsComp.CompCode, dbo.tblHrmsComp.CompName, dbo.tblHrmsComp.CompAddr, dbo.tblHrmsEmpOffice.OffLocRefNo, dbo.tblHrmsOffLoc.LocCode, " +
                        "dbo.tblHrmsOffLoc.LocName, dbo.tblHrmsOffLoc.LocAddr, dbo.tblHrmsEmpOffice.DeptRefNo, dbo.tblHrmsDept.DeptCode, dbo.tblHrmsDept.DeptName, " +
                        "dbo.tblHrmsDept.DeptRem, dbo.tblHrmsEmpOffice.SecRefNo, dbo.tblHrmsSec.SecCode, dbo.tblHrmsSec.SecName, dbo.tblHrmsSec.SecRem, " +
                        "dbo.tblHrmsEmpOffice.DesigRefNo, dbo.tblHrmsDesig.DesigCode, dbo.tblHrmsDesig.DesigName, dbo.tblHrmsDesig.DesigDesc, dbo.tblHrmsDesig.DesigRem, " +
                        "dbo.tblHrmsEmpOffice.EmpDOJ, dbo.tblHrmsEmpOffice.EmpSuprId, dbo.tblHrmsEmpOffice.EmpJobStatus, dbo.tblHrmsJobStat.JobStatCode, " +
                        "dbo.tblHrmsJobStat.JobStatName, dbo.tblHrmsJobStat.JobStatRem, dbo.tblHrmsEmpOffice.EmpConfDueDate, dbo.tblHrmsEmpOffice.EmpConfDate, " +
                        "dbo.tblHrmsEmpOffice.EmpType, dbo.tblHrmsEmpType.EmpTypeCode, dbo.tblHrmsEmpType.EmpTypeName, dbo.tblHrmsEmpType.EmpTypeRem, " +
                        "dbo.tblHrmsEmpOffice.EmpCardId, dbo.tblHrmsEmpOffice.ShiftRefNo, dbo.tblHrmsShift.ShiftCode, dbo.tblHrmsShift.ShiftName, dbo.tblHrmsShift.ShiftDesc, " +
                        "dbo.tblHrmsShift.ShiftRem, dbo.tblHrmsEmpOffice.EmpGrade, dbo.tblHrmsGradeDef.GrdDefCode, dbo.tblHrmsGradeDef.GrdDefName, " +
                        "dbo.tblHrmsEmpOffice.EmpSalary, dbo.tblHrmsEmpOffice.BankAccRef, dbo.tblHrmsEmpOffice.BankAccNo, dbo.tblHrmsEmpOffice.EmpOffEmail, " +
                        "dbo.tblHrmsEmpOffice.EmpWorkPhone, dbo.tblHrmsEmpOffice.EmpPabxNo, dbo.tblHrmsEmpOffice.EmpIpPhone, dbo.tblHrmsEmpOffice.EmpRem, " +
                        "dbo.tblHrmsEmpOffice.EmpOffExtData1, dbo.tblHrmsEmpOffice.EmpOffExtData2, dbo.tblHrmsEmpOffice.EmpOffExtData3 " +
                        "FROM dbo.tblHrmsReligion RIGHT OUTER JOIN dbo.tblHrmsBldGrp RIGHT OUTER JOIN dbo.tblHrmsMaritalStatus RIGHT OUTER JOIN " +
                        "dbo.tblHrmsEmpExt RIGHT OUTER JOIN dbo.tblHrmsEmp ON dbo.tblHrmsEmpExt.EmpRefNo = dbo.tblHrmsEmp.EmpRefNo LEFT OUTER JOIN " +
                        "dbo.tblHrmsEmpAdr ON dbo.tblHrmsEmp.EmpRefNo = dbo.tblHrmsEmpAdr.EmpRefNo ON " +
                        "dbo.tblHrmsMaritalStatus.MaritalStatusRef = dbo.tblHrmsEmp.EmpMaritalStatus LEFT OUTER JOIN " +
                        "dbo.tblHrmsGender ON dbo.tblHrmsEmp.EmpGender = dbo.tblHrmsGender.GenderRef LEFT OUTER JOIN " +
                        "dbo.tblHrmsComp RIGHT OUTER JOIN dbo.tblHrmsEmpOffice LEFT OUTER JOIN " +
                        "dbo.tblHrmsGradeDef ON dbo.tblHrmsEmpOffice.EmpGrade = dbo.tblHrmsGradeDef.GrdDefRefNo LEFT OUTER JOIN " +
                        "dbo.tblHrmsShift ON dbo.tblHrmsEmpOffice.ShiftRefNo = dbo.tblHrmsShift.ShiftRefNo LEFT OUTER JOIN " +
                        "dbo.tblHrmsEmpType ON dbo.tblHrmsEmpOffice.EmpType = dbo.tblHrmsEmpType.EmpTypeRef LEFT OUTER JOIN " +
                        "dbo.tblHrmsJobStat ON dbo.tblHrmsEmpOffice.EmpJobStatus = dbo.tblHrmsJobStat.JobStatRef LEFT OUTER JOIN " +
                        "dbo.tblHrmsDesig ON dbo.tblHrmsEmpOffice.DesigRefNo = dbo.tblHrmsDesig.DesigRefNo LEFT OUTER JOIN " +
                        "dbo.tblHrmsSec ON dbo.tblHrmsEmpOffice.SecRefNo = dbo.tblHrmsSec.SecRefNo LEFT OUTER JOIN " +
                        "dbo.tblHrmsDept ON dbo.tblHrmsEmpOffice.DeptRefNo = dbo.tblHrmsDept.DeptRefNo LEFT OUTER JOIN " +
                        "dbo.tblHrmsOffLoc ON dbo.tblHrmsEmpOffice.OffLocRefNo = dbo.tblHrmsOffLoc.LocRefNo ON dbo.tblHrmsComp.CompRefNo = dbo.tblHrmsEmpOffice.CompRefNo ON " +
                        "dbo.tblHrmsEmp.EmpRefNo = dbo.tblHrmsEmpOffice.EmpRefNo ON dbo.tblHrmsBldGrp.BldGrpRef = dbo.tblHrmsEmpExt.EmpBloodgrp ON " +
                        "dbo.tblHrmsReligion.ReligionRef = dbo.tblHrmsEmpExt.EmpReligion where dbo.tblHrmsEmp.EmpSetlFlag='N'";
            dbCon.ExecuteSQLStmt(qrySqlStr);

            qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Emp_Attnd_Report]')) DROP VIEW [dbo].[View_Emp_Attnd_Report]";
            dbCon.ExecuteSQLStmt(qrySqlStr);

            qrySqlStr = "Create view View_Emp_Attnd_Report as select AttndRefNo, EmpRefNo as AttnEmpRefNo, AttndDate, ShiftRef, AttndInTime, AttndShiftInTime, AttndShiftInGrace, AttndLateMin, " +
                                     "AttndOutTime, AttndShiftOutTime, AttndShiftOutGrace,AttndEarlyMin, AttndTotHr, AttndTotOtMin, AttndDayVal, AttndEntryDt, AttndEntryUser, " +
                                     "AttndUpdtDt, AttndUpdtUser, AttndExtData1, AttndExtData2, AttndExtData3, AttndStatus, AttndFlag from tblHrmsEmpDayAttnd " +
                                     "WHERE CONVERT(datetime, AttndDate, 103) BETWEEN CONVERT(DATETIME,'" + txtFromDate.Text.Trim() + "', 103) " +
                                     "AND CONVERT(DATETIME,'" + txtToDate.Text.Trim() + "', 103)";
            dbCon.ExecuteSQLStmt(qrySqlStr);

            qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Emp_Attnd_Report_All]')) DROP VIEW [dbo].[View_Emp_Attnd_Report_All]";
            dbCon.ExecuteSQLStmt(qrySqlStr);
            if (cboDept.SelectedIndex != 0)
            {
                if (cboWrkStn.SelectedIndex != 0)
                {
                    qrySqlStr = "Create View View_Emp_Attnd_Report_All as select * from View_Emp_Basc_Report left outer join tblHrmsAttnReportDate " +
                                        "on View_Emp_Basc_Report.EmpFlag=tblHrmsAttnReportDate.RptFlag left outer join tblHrmsWorkStation " +
                                        "on View_Emp_Basc_Report.EmpOffExtData1=tblHrmsWorkStation.Wrk_Station_Ref left outer join View_Emp_Attnd_Report " +
                                        "on View_Emp_Basc_Report.EmpRefNo=View_Emp_Attnd_Report.AttnEmpRefNo and tblHrmsAttnReportDate.RptDate=View_Emp_Attnd_Report.AttndDate " +
                                        "where View_Emp_Basc_Report.DeptRefNo='" + cboDept.SelectedValue.ToString() + "' and tblHrmsWorkStation.Wrk_Station_Ref='" + cboWrkStn.SelectedValue.ToString() + "'";
                    dbCon.ExecuteSQLStmt(qrySqlStr);
                }
                else
                {
                    qrySqlStr = "Create View View_Emp_Attnd_Report_All as select * from View_Emp_Basc_Report left outer join tblHrmsAttnReportDate " +
                                        "on View_Emp_Basc_Report.EmpFlag=tblHrmsAttnReportDate.RptFlag left outer join tblHrmsWorkStation " +
                                        "on View_Emp_Basc_Report.EmpOffExtData1=tblHrmsWorkStation.Wrk_Station_Ref left outer join View_Emp_Attnd_Report " +
                                        "on View_Emp_Basc_Report.EmpRefNo=View_Emp_Attnd_Report.AttnEmpRefNo and tblHrmsAttnReportDate.RptDate=View_Emp_Attnd_Report.AttndDate " +
                                        "where View_Emp_Basc_Report.DeptRefNo='" + cboDept.SelectedValue.ToString() + "'";
                    dbCon.ExecuteSQLStmt(qrySqlStr);
                }
            }
            else
            {
                if (cboLoc.SelectedIndex != 0)
                {
                    if (cboWrkStn.SelectedIndex != 0)
                    {
                        qrySqlStr = "Create View View_Emp_Attnd_Report_All as select * from View_Emp_Basc_Report left outer join tblHrmsAttnReportDate " +
                                            "on View_Emp_Basc_Report.EmpFlag=tblHrmsAttnReportDate.RptFlag left outer join tblHrmsWorkStation " +
                                            "on View_Emp_Basc_Report.EmpOffExtData1=tblHrmsWorkStation.Wrk_Station_Ref left outer join View_Emp_Attnd_Report " +
                                            "on View_Emp_Basc_Report.EmpRefNo=View_Emp_Attnd_Report.AttnEmpRefNo and tblHrmsAttnReportDate.RptDate=View_Emp_Attnd_Report.AttndDate " +
                                            "where View_Emp_Basc_Report.OffLocRefNo='" + cboLoc.SelectedValue.ToString() + "' and tblHrmsWorkStation.Wrk_Station_Ref='" + cboWrkStn.SelectedValue.ToString() + "'";
                        dbCon.ExecuteSQLStmt(qrySqlStr);
                    }
                    else
                    {
                        qrySqlStr = "Create View View_Emp_Attnd_Report_All as select * from View_Emp_Basc_Report left outer join tblHrmsAttnReportDate " +
                                            "on View_Emp_Basc_Report.EmpFlag=tblHrmsAttnReportDate.RptFlag left outer join tblHrmsWorkStation " +
                                            "on View_Emp_Basc_Report.EmpOffExtData1=tblHrmsWorkStation.Wrk_Station_Ref left outer join View_Emp_Attnd_Report " +
                                            "on View_Emp_Basc_Report.EmpRefNo=View_Emp_Attnd_Report.AttnEmpRefNo and tblHrmsAttnReportDate.RptDate=View_Emp_Attnd_Report.AttndDate " +
                                            "where View_Emp_Basc_Report.OffLocRefNo='" + cboLoc.SelectedValue.ToString() + "'";
                        dbCon.ExecuteSQLStmt(qrySqlStr);
                    }                    
                }
                else
                {
                    if (cboWrkStn.SelectedIndex != 0)
                    {
                        qrySqlStr = "Create View View_Emp_Attnd_Report_All as select * from View_Emp_Basc_Report left outer join tblHrmsAttnReportDate " +
                                            "on View_Emp_Basc_Report.EmpFlag=tblHrmsAttnReportDate.RptFlag left outer join tblHrmsWorkStation " +
                                            "on View_Emp_Basc_Report.EmpOffExtData1=tblHrmsWorkStation.Wrk_Station_Ref left outer join View_Emp_Attnd_Report " +
                                            "on View_Emp_Basc_Report.EmpRefNo=View_Emp_Attnd_Report.AttnEmpRefNo and tblHrmsAttnReportDate.RptDate=View_Emp_Attnd_Report.AttndDate " +
                                            "where tblHrmsWorkStation.Wrk_Station_Ref='" + cboWrkStn.SelectedValue.ToString() + "'";
                        dbCon.ExecuteSQLStmt(qrySqlStr);
                    }
                    else
                    {
                        qrySqlStr = "Create View View_Emp_Attnd_Report_All as select * from View_Emp_Basc_Report left outer join tblHrmsAttnReportDate " +
                                            "on View_Emp_Basc_Report.EmpFlag=tblHrmsAttnReportDate.RptFlag left outer join tblHrmsWorkStation " +
                                            "on View_Emp_Basc_Report.EmpOffExtData1=tblHrmsWorkStation.Wrk_Station_Ref left outer join View_Emp_Attnd_Report " +
                                            "on View_Emp_Basc_Report.EmpRefNo=View_Emp_Attnd_Report.AttnEmpRefNo and tblHrmsAttnReportDate.RptDate=View_Emp_Attnd_Report.AttndDate";
                        dbCon.ExecuteSQLStmt(qrySqlStr);
                    }
                }
            }

            var taEmpAttn = new View_Emp_Attnd_Report_AllTableAdapter();
            gvEmpAttnd.DataSource = taEmpAttn.GetDataBySortAsc();
            gvEmpAttnd.DataBind();
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            GetData();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {           
            //SendMail();

            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            //mail.From = new MailAddress("sojib01@gmail.com");
            //mail.To.Add("imran@doreen.com.bd");
            //mail.Subject = "Test Mail";
            //mail.Body = "This is for testing SMTP mail from GMAIL";

            //SmtpServer.Port = 587;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("sojib01@gmail.com", "arafin");
            //SmtpServer.EnableSsl = true;

            //SmtpServer.Send(mail);            
        }

        protected void SendMail()
        {
            //// Gmail Address from where you send the mail
            //var fromAddress = "test@doreen.com.bd";
            //// any address where the email will be sending
            //var toAddress = "imran@doreen.com.bd";
            ////Password of your gmail address
            //const string fromPassword = "Doreen_@21";
            //// Passing the values and make a email formate to display
            //string subject = "Subject";
            //string body = "From: " + "Name" + "\n";
            //body += "Email: " + "Email" + "\n";
            //body += "Subject: " + "Subject" + "\n";
            //body += "Question: \n" + "Comments.Text" + "\n";
            //// smtp settings
            //var smtp = new System.Net.Mail.SmtpClient();
            //{
            //    smtp.Host = "mail.doreen.com.bd";
            //    smtp.Port = 587;
            //    smtp.EnableSsl = false;
            //    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            //    smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
            //    smtp.Timeout = 20000;
            //}
            //// Passing values to smtp object
            //smtp.Send(fromAddress, toAddress, subject, body);
        }

        protected void cboEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ReportViewer1.Visible = true;
            //ReportDataSource datasource = new ReportDataSource("DataSet1", GetData());
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("AttndDayRpt.rdlc");
            //LocalReport rep = ReportViewer1.LocalReport;
            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportViewer1.LocalReport.DataSources.Add(datasource);
            //ReportViewer1.LocalReport.Refresh();
        }

        protected void cboLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoc.SelectedIndex != 0)
            {
                cboDept.Items.Clear();

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
                cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));
            }
        }

        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }

        protected void gvEmpAttnd_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (direction == SortDirection.Ascending)
            {
                direction = SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = SortDirection.Ascending;
                sortingDirection = "Asc";
            }
            var taEmpAttn = new View_Emp_Attnd_Report_AllTableAdapter();
            var dtEvent = taEmpAttn.GetDataBySortAsc();
            DataView sortedView = new DataView(dtEvent);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            gvEmpAttnd.DataSource = sortedView;
            gvEmpAttnd.DataBind();  
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvEmpAttnd.Rows.Count > 65535)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Export to Excel is not allowed due to excessive number of rows. (65535) ')", true);
                return;
            }

            //First Method
            #region With Formating
            Response.Clear();
            Response.Buffer = true;
            string filename = String.Format("In_Out_Report_{0}_{1}_{2}.xls", txtFromDate.Text.Trim(), "to", txtToDate.Text.Trim());
            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvEmpAttnd.AllowPaging = false;
                var taAttnd = new View_Emp_Attnd_Report_AllTableAdapter();
                gvEmpAttnd.DataSource = taAttnd.GetDataBySortAsc();
                gvEmpAttnd.DataBind();

                gvEmpAttnd.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in gvEmpAttnd.HeaderRow.Cells)
                {
                    cell.BackColor = gvEmpAttnd.HeaderStyle.BackColor;
                }

                foreach (GridViewRow row in gvEmpAttnd.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
                    row.Height = 18;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvEmpAttnd.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvEmpAttnd.RowStyle.BackColor;
                        }
                        cell.Wrap = false;
                        cell.CssClass = "textmode";
                    }
                }

                gvEmpAttnd.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            #endregion
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}