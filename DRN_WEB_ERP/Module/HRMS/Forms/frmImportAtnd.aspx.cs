using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsTranTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmImportAtnd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDt.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");            
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            var taDataPath = new tbl_Mod_Data_PathTableAdapter();
            var taEvent = new tblHrmsImportAttndDetTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEvent.Connection);

            try
            {
                ////Open connection to Access database:
                ////var conStr = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=D:\Attendance\HAMS_2014.mdb; User Id=admin; Password=";
                //var conStr = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=D:\Attendance\HAMS_2014.mdb; Persist Security Info=True";

                ////Open connection to password protected Access database:
                ////var conStr =@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=F:\Database\HAMS_2014.mdb; Jet OLEDB:Database Password=Your_Password" ;

                ////Open connection to Access database located on a network share:
                ////var conStr =@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\Server_Name\Share_Name\Share_Path\Your_Database_Name.mdb" 

                ////Open connection to Access database located on a remote server:
                ////var conStr =@"Provider=MS Remote; Remote Server=http://Your-Remote-Server-IP; Remote Provider=Microsoft.Jet.OLEDB.4.0; Data Source=c:\App1\Your_Database_Name.mdb" 

                //lblFrmDt.Text = Convert.ToDateTime(txtFromDt.Text.Trim()).ToString("yyyy/MM/dd");
                //lblToDt.Text = Convert.ToDateTime(txtToDate.Text.Trim()).ToString("yyyy/MM/dd");

                
                string connection = "";

                if (chkLocalData.Checked)
                {
                    var dtDataLocalPath = taDataPath.GetHrmsLocalDataPath();
                    connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dtDataLocalPath[0].Mod_Data_Path.ToString() + "HAMS_" + Convert.ToDateTime(txtFromDt.Text.Trim()).Year.ToString() + ".mdb;Persist Security Info=True";
                }
                else
                {
                    var dtDataRemotePath = taDataPath.GetHrmsRemoteDataPath();
                    connection = @"Provider=Microsoft.ACE.OLEDB.4.0; Data Source=" + dtDataRemotePath[0].Mod_Data_Path.ToString() + "HAMS_" + Convert.ToDateTime(txtFromDt.Text.Trim()).Year.ToString() + ".mdb;Persist Security Info=True";
                }
                taEvent.AttachTransaction(myTran);

                //string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Program Files\HAMS-19\HAMS_2015.mdb;Persist Security Info=True";
                //string connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Attendance\HAMS_2015.mdb;Persist Security Info=True";
                OleDbConnection cn = new OleDbConnection(connection);
                //OleDbDataAdapter da = new OleDbDataAdapter("select * from PubEvent where Format(eventDate,'dd/MM/yyyy') between Format('" + txtFromDt.Text.Trim() + "','dd/MM/yyyy') and Format('" + txtToDate.Text.Trim() + "','dd/MM/yyyy') and eventCard<>''", cn);
                //OleDbDataAdapter da = new OleDbDataAdapter("select * from PubEvent where Format(CDate(eventDate),'yyyy/MM/dd') >= Format(CDate('" + txtFromDt.Text.Trim() + "'),'yyyy/MM/dd') and Format(CDate(eventDate),'yyyy/MM/dd') <= Format(CDate('" + txtToDate.Text.Trim() + "'),'yyyy/MM/dd') and eventCard<>''", cn);                

                OleDbDataAdapter da = new OleDbDataAdapter("select * from PubEvent where Format(CDate(eventDate),'yyyy/MM/dd') >= '" + Convert.ToDateTime(txtFromDt.Text.Trim()).ToString("yyyy/MM/dd") + "' and Format(CDate(eventDate),'yyyy/MM/dd') <= '" + Convert.ToDateTime(txtToDate.Text.Trim()).ToString("yyyy/MM/dd") + "' and eventCard<>''", cn);

                System.Data.DataSet ds = new System.Data.DataSet();
                da.Fill(ds);

                Label3.Text = "Total Data Found : " + ds.Tables[0].Rows.Count.ToString();

                if (ds.Tables[0].Rows.Count > 0) btnExport.Visible = true;

                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (taEvent.GetDataByEventRowId(ds.Tables[0].Rows[i]["rowAutoID"].ToString(), ds.Tables[0].Rows[i]["eventDate"].ToString()).Count <= 0)
                        taEvent.InsertEventData(ds.Tables[0].Rows[i]["eventDate"].ToString(), ds.Tables[0].Rows[i]["eventTime"].ToString(), ds.Tables[0].Rows[i]["eventCard"].ToString(), ds.Tables[0].Rows[i]["personName"].ToString(),
                            ds.Tables[0].Rows[i]["personID"].ToString(), ds.Tables[0].Rows[i]["deviceID"].ToString(), ds.Tables[0].Rows[i]["deviceName"].ToString(), ds.Tables[0].Rows[i]["deviceType"].ToString(), ds.Tables[0].Rows[i]["doorName"].ToString(),
                            ds.Tables[0].Rows[i]["readerNo"].ToString(), ds.Tables[0].Rows[i]["rowAutoID"].ToString(), "", "", "", "", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "");
                }

                myTran.Commit();

                tblMsg.Rows[0].Cells[0].InnerText = "Data Imported Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                cn.Close();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }

            var taEventData = new View_Import_AttndTableAdapter();
            var dtEvent = taEventData.GetDataByDateRange(Convert.ToDateTime(txtFromDt.Text.Trim()), Convert.ToDateTime(txtToDate.Text.Trim()));
            gvEmpAttnd.DataSource = dtEvent;
            gvEmpAttnd.DataBind();
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            var taEvent = new View_Import_AttndTableAdapter();
            var dtEvent = taEvent.GetDataByDateRange(Convert.ToDateTime(txtFromDt.Text.Trim()), Convert.ToDateTime(txtToDate.Text.Trim()));
            gvEmpAttnd.DataSource = dtEvent;
            gvEmpAttnd.DataBind();
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
            var taEvent = new View_Import_AttndTableAdapter();
            var dtEvent = taEvent.GetDataByDateRange(Convert.ToDateTime(txtFromDt.Text.Trim()), Convert.ToDateTime(txtToDate.Text.Trim()));
            DataView sortedView = new DataView(dtEvent);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            gvEmpAttnd.DataSource = sortedView;
            gvEmpAttnd.DataBind();  
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var taAttnd = new tblHrmsEmpDayAttndTableAdapter();
            var taEvent = new View_Import_AttndTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAttnd.Connection);

            try
            {
                taAttnd.AttachTransaction(myTran);
                
                var dtEvent = taEvent.GetDataByDateRange(Convert.ToDateTime(txtFromDt.Text.Trim()), Convert.ToDateTime(txtToDate.Text.Trim()));

                foreach (dsHrmsTran.View_Import_AttndRow dr in dtEvent.Rows)
                {
                    var dtAttnd = taAttnd.GetDataByEmp(dr.EmpRefNo, Convert.ToDateTime(dr.EventDate));
                    if (dtAttnd.Rows.Count > 0)
                    {
                        if (dtAttnd[0].AttndFlag != "L")
                        {
                            taAttnd.UpdateAttnd(dr.ShiftRefNo.ToString(), dr.InTime.ToString(), dr.ShiftStart.ToString(),
                               dr.ShiftStartAdd.ToString(), dr.LateMin.ToString(), dr.OutTime.ToString(), dr.ShiftEnd.ToString(), dr.ShiftEndAdd.ToString(), dr.EarlyMin.ToString(),
                               dr.ToAttndtHr.ToString(), dr.TotOtMin.ToString(), 1, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                               "", "", "", "1", "P", dr.EmpRefNo, Convert.ToDateTime(dr.EventDate));
                        }
                    }
                    else
                    {
                        taAttnd.InsertAttnd(dr.EmpRefNo, Convert.ToDateTime(dr.EventDate), dr.ShiftRefNo.ToString(), dr.InTime.ToString(), dr.ShiftStart.ToString(),
                               dr.ShiftStartAdd.ToString(), dr.LateMin.ToString(), dr.OutTime.ToString(), dr.ShiftEnd.ToString(), dr.ShiftEndAdd.ToString(), dr.EarlyMin.ToString(),
                               dr.ToAttndtHr.ToString(), dr.TotOtMin.ToString(), 1, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                               "", "", "", "1", "P");
                    }                    
                }
                myTran.Commit();

                tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();   
            }
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
            string filename = String.Format("In_Out_Report_{0}_{1}_{2}.xls", txtFromDt.Text.Trim(), "to", txtToDate.Text.Trim());
            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvEmpAttnd.AllowPaging = false;
                var taAttnd = new View_Import_AttndTableAdapter();
                gvEmpAttnd.DataSource = taAttnd.GetDataByDateRange(Convert.ToDateTime(txtFromDt.Text.Trim()), Convert.ToDateTime(txtToDate.Text.Trim()));
                gvEmpAttnd.DataBind();

                gvEmpAttnd.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in gvEmpAttnd.HeaderRow.Cells)
                {
                    cell.BackColor = gvEmpAttnd.HeaderStyle.BackColor;
                }

                foreach (GridViewRow row in gvEmpAttnd.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
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