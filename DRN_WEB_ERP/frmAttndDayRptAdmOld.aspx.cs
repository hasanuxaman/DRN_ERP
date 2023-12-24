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
using Microsoft.Reporting.WebForms;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;

namespace DRN_WEB_ERP
{
    public partial class frmAttndDayRptAdmOld : System.Web.UI.Page
    {
        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        string rptFile;
        string rptSelcFormula;

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

            ReportViewer1.Visible = false;
        }

        public DataTable GetData()
        {
            SqlDataAdapter dta = new SqlDataAdapter();
            string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DRNConStr"].ToString();
            SqlConnection con = new SqlConnection(connStr);
            DataSet ds = new DataSet();

            var qryStr = "";
            if (cboLoc.SelectedIndex == 0)
            {
                if (cboDept.SelectedIndex == 0)
                {

                    qryStr = "Select View_Emp_Attnd.*,View_Emp_Basc.EmpSuprId FROM [View_Emp_Attnd] left outer join View_Emp_Basc on View_Emp_Attnd.EmpRefNo=View_Emp_Basc.EmpRefNo " +
                             "WHERE Convert(datetime,AttndDate,103) between Convert(datetime,'" + txtFromDate.Text.Trim() + "',103) " +
                             "and Convert(datetime,'" + txtToDate.Text.Trim() + "',103) AND AttndFlag<>'L' order by AttndDate,EmpName";
                }
                else
                {
                    qryStr = "Select View_Emp_Attnd.*,View_Emp_Basc.EmpSuprId FROM [View_Emp_Attnd] left outer join View_Emp_Basc on View_Emp_Attnd.EmpRefNo=View_Emp_Basc.EmpRefNo " +
                            "WHERE (View_Emp_Basc.DeptRefNo='" + cboDept.SelectedValue + "') " +
                            "AND Convert(datetime,AttndDate,103) between Convert(datetime,'" + txtFromDate.Text.Trim() + "',103) " +
                            "and Convert(datetime,'" + txtToDate.Text.Trim() + "',103) AND AttndFlag<>'L' order by AttndDate,EmpName";                    
                }
            }
            else
            {
                if (cboDept.SelectedIndex == 0)
                {
                    qryStr = "Select View_Emp_Attnd.*,View_Emp_Basc.EmpSuprId FROM [View_Emp_Attnd] left outer join View_Emp_Basc on View_Emp_Attnd.EmpRefNo=View_Emp_Basc.EmpRefNo " +
                            "WHERE (View_Emp_Basc.OffLocRefNo='" + cboLoc.SelectedValue + "') " +
                            "AND Convert(datetime,AttndDate,103) between Convert(datetime,'" + txtFromDate.Text.Trim() + "',103) " +
                            "and Convert(datetime,'" + txtToDate.Text.Trim() + "',103) AND AttndFlag<>'L' order by AttndDate,EmpName";
                }
                else
                {
                    qryStr = "Select View_Emp_Attnd.*,View_Emp_Basc.EmpSuprId FROM [View_Emp_Attnd] left outer join View_Emp_Basc on View_Emp_Attnd.EmpRefNo=View_Emp_Basc.EmpRefNo " +
                            "WHERE (View_Emp_Basc.DeptRefNo='" + cboDept.SelectedValue + "') " +
                            "AND Convert(datetime,AttndDate,103) between Convert(datetime,'" + txtFromDate.Text.Trim() + "',103) " +
                            "and Convert(datetime,'" + txtToDate.Text.Trim() + "',103) AND AttndFlag<>'L' order by AttndDate,EmpName";  
                }
            }

            SqlCommand cmd = new SqlCommand(qryStr, con);
            dta.SelectCommand = cmd;
            dta.SelectCommand.Connection = con;
            dta.Fill(ds, "View_Emp_Attnd");
            return ds.Tables[0];
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            ReportViewer1.Visible = true;
            ReportDataSource datasource = new ReportDataSource("DataSet1", GetData());
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("AttndDayRpt.rdlc");
            LocalReport rep = ReportViewer1.LocalReport;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.Refresh();
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
            ReportViewer1.Visible = true;
            ReportDataSource datasource = new ReportDataSource("DataSet1", GetData());
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("AttndDayRpt.rdlc");
            LocalReport rep = ReportViewer1.LocalReport;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.Refresh();
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
    }
}