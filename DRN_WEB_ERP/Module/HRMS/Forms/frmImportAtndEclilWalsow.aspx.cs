using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OleDb;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsAttnEcilWalsowTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsTranTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmImportAtndEclilWalsow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDt.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            var taEvent = new KQZ_Card_Employee_MasTableAdapter();
            var taAttn = new tblHrmsImportAttndDetEcilHoTableAdapter();

            //SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEvent.Connection);

            try
            {
                //taAttn.AttachTransaction(myTran);

                var dtEvent = taEvent.GetDataByDateRange(Convert.ToDateTime(txtFromDt.Text.Trim()).ToString("dd/MM/yyyy"), Convert.ToDateTime(txtToDate.Text.Trim()).ToString("dd/MM/yyyy"));

                Label3.Text = "Total Data Found : " + dtEvent.Rows.Count.ToString();

                if (dtEvent.Rows.Count > 0) btnExport.Visible = true;

                foreach (dsHrmsAttnEcilWalsow.KQZ_Card_Employee_MasRow dr in dtEvent.Rows)
                {
                    if (taAttn.GetDataByEventRowId(Convert.ToDateTime(dr.CardTime.ToString("dd/MM/yyyy")).Day.ToString("00") + "_" + Convert.ToDateTime(dr.CardTime.ToString("dd/MM/yyyy")).Month.ToString("00") + "_" + Convert.ToDateTime(dr.CardTime.ToString("dd/MM/yyyy")).Year.ToString("0000") + "_" + dr.CardID.ToString(), dr.CardTime.ToString("dd/MM/yyyy")).Count <= 0)
                        taAttn.InsertEventData(dr.CardTime.ToString("dd/MM/yyyy"), dr.CardTime.TimeOfDay.ToString(), dr.KQZ_Card_EmployeeID.ToString(), dr.EmployeeName,
                            "EMP-" + Convert.ToInt32(dr.RealEmployeeCode).ToString("000000"), dr.DevID.ToString(), "", dr.DevClass.ToString(), "", "",
                            Convert.ToDateTime(dr.CardTime.ToString("dd/MM/yyyy")).Day.ToString("00") + "_" + Convert.ToDateTime(dr.CardTime.ToString("dd/MM/yyyy")).Month.ToString("00") + "_" + Convert.ToDateTime(dr.CardTime.ToString("dd/MM/yyyy")).Year.ToString("0000") + "_" + dr.CardID.ToString(),
                            "", "", "", "", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "");
                }

                //myTran.Commit();

                tblMsg.Rows[0].Cells[0].InnerText = "Data Imported Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                //myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }

            var taEventData = new View_Import_Attnd_HoTableAdapter();
            var dtEventData = taEventData.GetDataByDateRange(txtFromDt.Text.Trim(), txtToDate.Text.Trim());
            gvEmpAttnd.DataSource = dtEventData;
            gvEmpAttnd.DataBind();
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            var taEvent = new View_Import_Attnd_HoTableAdapter();
            var dtEvent = taEvent.GetDataByDateRange(txtFromDt.Text.Trim(), txtToDate.Text.Trim());
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
            var taEvent = new View_Import_Attnd_HoTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAttnd.Connection);

            try
            {
                taAttnd.AttachTransaction(myTran);

                var dtEvent = taEvent.GetDataByDateRange(txtFromDt.Text.Trim(), txtToDate.Text.Trim());

                foreach (dsHrmsTran.View_Import_Attnd_HoRow dr in dtEvent.Rows)
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
                var taAttnd = new View_Import_Attnd_HoTableAdapter();
                gvEmpAttnd.DataSource = taAttnd.GetDataByDateRange(txtFromDt.Text.Trim(), txtToDate.Text.Trim());
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

        protected void btnDbConStat_Click(object sender, EventArgs e)
        {
            lblDbConStat.Text = "";
            using (var databaseConnection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DRNConStrAttnWalsow"].ToString()))
            {
                try
                {
                    databaseConnection.Open();
                    lblDbConStat.Text = "Database Connection Successful.";
                }
                catch (SqlException ex)
                {
                    lblDbConStat.Text = "Could not establish a connection with Database.\n" + ex.Message;
                }
                finally { databaseConnection.Close(); }
            }
        }
    }
}