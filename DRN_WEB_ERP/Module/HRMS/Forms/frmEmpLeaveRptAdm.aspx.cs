using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsTranTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmEmpLeaveRptAdm : System.Web.UI.Page
    {
        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        double totLvDetDays = 0, totLvSumDays = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            var taLoc = new tblHrmsOffLocTableAdapter();
            cboLoc.DataSource = taLoc.GetDataByAsc();
            cboLoc.DataValueField = "LocRefNo";
            cboLoc.DataTextField = "LocName";
            cboLoc.DataBind();
            cboLoc.Items.Insert(0, new ListItem("---ALL---", "0"));

            cboDept.Items.Insert(0, new ListItem("---ALL---", "0"));
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            try
            {
                var taLvDet = new tblHrmsEmpLeaveViewTableAdapter();
                var dtLvDet = new dsHrmsTran.tblHrmsEmpLeaveViewDataTable();

                if (optDetails.Checked)
                {                    
                    if (cboLoc.SelectedIndex == 0)
                    {
                        if (cboDept.SelectedIndex == 0)
                            dtLvDet = taLvDet.GetDataByDateRange(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                        else
                            dtLvDet = taLvDet.GetDataByDateRangeDept(cboDept.SelectedValue.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim()); ;                      
                    }
                    else
                    {
                        if (cboDept.SelectedIndex == 0)
                            dtLvDet = taLvDet.GetDataByDateRangeOffcLoc(cboLoc.SelectedValue.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                        else
                            dtLvDet = taLvDet.GetDataByDateRangeDept(cboDept.SelectedValue.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                    }                   

                    btnExport.Visible = dtLvDet.Rows.Count > 0;
                    gvEmpLvRptDet.DataSource = dtLvDet;
                    gvEmpLvRptDet.DataBind();

                    gvEmpLvRptDet.Visible = true;
                    gvEmpLvRptSum.Visible = false;
                }
                else
                {
                    var qrySqlStr = "";

                    qrySqlStr = "IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Emp_Leave_Sum]')) DROP VIEW [dbo].[View_Emp_Leave_Sum]";
                    dbCon.ExecuteSQLStmt(qrySqlStr);

                    if (cboLoc.SelectedIndex == 0)
                    {
                        if (cboDept.SelectedIndex == 0)
                        {
                            qrySqlStr = "CREATE view [dbo].[View_Emp_Leave_Sum] as select View_Emp_Basc.EmpRefNo,SUM(LvDetDays) as LvDetDays " +
                                        "from tblHrmsEmpLeave tblHrmsEmpLeave LEFT OUTER JOIN View_Emp_Basc ON tblHrmsEmpLeave.EmpRefNo = View_Emp_Basc.EmpRefNo " +
                                        "WHERE (CONVERT(date, LvDetStDate, 103) >= CONVERT(date, '" + txtFromDate.Text.Trim() + "', 103)) " +
                                        "AND (CONVERT(date, LvDetEndDate, 103)<= CONVERT(date, '" + txtToDate.Text.Trim() + "', 103)) " +
                                        "group by View_Emp_Basc.EmpRefNo";
                        }
                        else
                        {
                            qrySqlStr = "CREATE view [dbo].[View_Emp_Leave_Sum] as select View_Emp_Basc.EmpRefNo,SUM(LvDetDays) as LvDetDays " +
                                    "from tblHrmsEmpLeave tblHrmsEmpLeave LEFT OUTER JOIN View_Emp_Basc ON tblHrmsEmpLeave.EmpRefNo = View_Emp_Basc.EmpRefNo " +
                                    "WHERE View_Emp_Basc.DeptRefNo ='" + cboDept.SelectedValue + "'" +
                                    "AND (CONVERT(date, LvDetStDate, 103) >= CONVERT(date, '" + txtFromDate.Text.Trim() + "', 103)) " +
                                    "AND (CONVERT(date, LvDetEndDate, 103)<= CONVERT(date, '" + txtToDate.Text.Trim() + "', 103)) " +
                                    "group by View_Emp_Basc.EmpRefNo";
                        }
                    }
                    else
                    {
                        if (cboDept.SelectedIndex == 0)
                        {
                            qrySqlStr = "CREATE view [dbo].[View_Emp_Leave_Sum] as select View_Emp_Basc.EmpRefNo,SUM(LvDetDays) as LvDetDays " +
                                    "from tblHrmsEmpLeave tblHrmsEmpLeave LEFT OUTER JOIN View_Emp_Basc ON tblHrmsEmpLeave.EmpRefNo = View_Emp_Basc.EmpRefNo " +
                                    "WHERE View_Emp_Basc.OffLocRefNo ='" + cboLoc.SelectedValue + "'" +
                                    "AND (CONVERT(date, LvDetStDate, 103) >= CONVERT(date, '" + txtFromDate.Text.Trim() + "', 103)) " +
                                    "AND (CONVERT(date, LvDetEndDate, 103)<= CONVERT(date, '" + txtToDate.Text.Trim() + "', 103)) " +
                                    "group by View_Emp_Basc.EmpRefNo";                            
                        }
                        else
                        {
                            qrySqlStr = "CREATE view [dbo].[View_Emp_Leave_Sum] as select View_Emp_Basc.EmpRefNo,SUM(LvDetDays) as LvDetDays " +
                                    "from tblHrmsEmpLeave tblHrmsEmpLeave LEFT OUTER JOIN View_Emp_Basc ON tblHrmsEmpLeave.EmpRefNo = View_Emp_Basc.EmpRefNo " +
                                    "WHERE View_Emp_Basc.DeptRefNo ='" + cboDept.SelectedValue + "'" +
                                    "AND (CONVERT(date, LvDetStDate, 103) >= CONVERT(date, '" + txtFromDate.Text.Trim() + "', 103)) " +
                                    "AND (CONVERT(date, LvDetEndDate, 103)<= CONVERT(date, '" + txtToDate.Text.Trim() + "', 103)) " +
                                    "group by View_Emp_Basc.EmpRefNo";
                        }
                    }
                    dbCon.ExecuteSQLStmt(qrySqlStr);

                    var taLvSum = new View_Emp_Leave_SumTableAdapter();
                    var dtLvSum = new dsHrmsTran.View_Emp_Leave_SumDataTable();

                    dtLvSum = taLvSum.GetData();

                    btnExport.Visible = dtLvSum.Rows.Count > 0;
                    gvEmpLvRptSum.DataSource = dtLvSum;
                    gvEmpLvRptSum.DataBind();

                    gvEmpLvRptDet.Visible = false;
                    gvEmpLvRptSum.Visible = true;
                }
            }
            catch (Exception ex) { }
        }

        protected void gvEmpLvRptDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblLvDays = ((Label)e.Row.FindControl("lblLvDays"));
                totLvDetDays += Convert.ToDouble(lblLvDays.Text.Trim());

                System.Web.UI.WebControls.Image UsrImages = (System.Web.UI.WebControls.Image)e.Row.FindControl("AttachImage");
                var empRefNo = ((HiddenField)e.Row.FindControl("hfEmpRef")).Value;
                UsrImages.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRefNo + "'";
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotDays = (Label)e.Row.FindControl("lblTotDays");
                lblTotDays.Text = totLvDetDays.ToString("N2");
            }
        }

        protected void gvEmpLvRptSum_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblLvDaysSum = ((Label)e.Row.FindControl("lblLvDaysSum"));
                totLvSumDays += Convert.ToDouble(lblLvDaysSum.Text.Trim());

                System.Web.UI.WebControls.Image UsrImages = (System.Web.UI.WebControls.Image)e.Row.FindControl("AttachImageLvSum");
                var empRefNo = ((HiddenField)e.Row.FindControl("hfEmpRefLvSum")).Value;
                UsrImages.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRefNo + "'";
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotDays = (Label)e.Row.FindControl("lblTotDays");
                lblTotDays.Text = totLvSumDays.ToString("N2");
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (optDetails.Checked)
            {
                if (gvEmpLvRptDet.Rows.Count > 65535)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Export to Excel is not allowed due to excessive number of rows. (65535) ')", true);
                    return;
                }
            }
            else
            {
                if (gvEmpLvRptSum.Rows.Count > 65535)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Export to Excel is not allowed due to excessive number of rows. (65535) ')", true);
                    return;
                }
            }

            Response.Clear();
            Response.Buffer = true;
            string filename = String.Format("Leave_Report_{0}_{1}_{2}.xls", txtFromDate.Text.Trim(), "to", txtToDate.Text.Trim());
            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            var taLvDet = new tblHrmsEmpLeaveViewTableAdapter();
            var dtLvDet = new dsHrmsTran.tblHrmsEmpLeaveViewDataTable();

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                if (optDetails.Checked)
                {
                    #region Export Details Data
                    //To Export all pages
                    gvEmpLvRptDet.AllowPaging = false;

                    if (cboLoc.SelectedIndex == 0)
                    {
                        if (cboDept.SelectedIndex == 0)
                            dtLvDet = taLvDet.GetDataByDateRange(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                        else
                            dtLvDet = taLvDet.GetDataByDateRangeDept(cboDept.SelectedValue.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim()); ;
                    }
                    else
                    {
                        if (cboDept.SelectedIndex == 0)
                            dtLvDet = taLvDet.GetDataByDateRangeOffcLoc(cboLoc.SelectedValue.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                        else
                            dtLvDet = taLvDet.GetDataByDateRangeDept(cboDept.SelectedValue.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                    }             
                    gvEmpLvRptDet.DataSource = dtLvDet;
                    gvEmpLvRptDet.DataBind();

                    //gvEmpLvRptDet.Columns[0].Visible = false;
                    gvEmpLvRptDet.Columns[1].Visible = false;

                    gvEmpLvRptDet.HeaderRow.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in gvEmpLvRptDet.HeaderRow.Cells)
                    {
                        cell.BackColor = gvEmpLvRptDet.HeaderStyle.BackColor;
                    }
                    // Hides the first column in the grid (zero-based index)
                    gvEmpLvRptDet.HeaderRow.Cells[0].Visible = false;

                    gvEmpLvRptDet.FooterRow.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in gvEmpLvRptDet.FooterRow.Cells)
                    {
                        cell.BackColor = gvEmpLvRptDet.FooterStyle.BackColor;
                    }
                    gvEmpLvRptDet.FooterRow.Cells[0].Visible = false;

                    foreach (GridViewRow row in gvEmpLvRptDet.Rows)
                    {
                        row.Cells[0].Visible = false;

                        row.BackColor = System.Drawing.Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = gvEmpLvRptDet.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = gvEmpLvRptDet.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    gvEmpLvRptDet.RenderControl(hw);
                    #endregion
                }
                else
                {
                    #region Export Summary Data
                    //To Export all pages
                    gvEmpLvRptSum.AllowPaging = false;

                    var taLvSum = new View_Emp_Leave_SumTableAdapter();
                    var dtLvSum = new dsHrmsTran.View_Emp_Leave_SumDataTable();

                    dtLvSum = taLvSum.GetData();

                    gvEmpLvRptSum.DataSource = dtLvSum;
                    gvEmpLvRptSum.DataBind();

                    //gvEmpLvRptSum.Columns[0].Visible = false;
                    gvEmpLvRptSum.Columns[1].Visible = false;

                    gvEmpLvRptSum.HeaderRow.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in gvEmpLvRptSum.HeaderRow.Cells)
                    {
                        cell.BackColor = gvEmpLvRptSum.HeaderStyle.BackColor;
                    }
                    // Hides the first column in the grid (zero-based index)
                    gvEmpLvRptSum.HeaderRow.Cells[0].Visible = false;

                    gvEmpLvRptSum.FooterRow.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in gvEmpLvRptSum.FooterRow.Cells)
                    {
                        cell.BackColor = gvEmpLvRptSum.FooterStyle.BackColor;
                    }
                    gvEmpLvRptSum.FooterRow.Cells[0].Visible = false;

                    foreach (GridViewRow row in gvEmpLvRptSum.Rows)
                    {
                        row.Cells[0].Visible = false;

                        row.BackColor = System.Drawing.Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = gvEmpLvRptSum.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = gvEmpLvRptSum.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }
                    gvEmpLvRptSum.RenderControl(hw);
                    #endregion
                }

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        #region GridData
        public string GetLvDt(string lvRef)
        {
            string lvDt = "";
            try
            {
                var taEmpLv = new tblHrmsEmpLeaveTableAdapter();
                var dtEmpLv = taEmpLv.GetDataByLvRefNo(lvRef);
                if (dtEmpLv.Rows.Count > 0)
                    lvDt = dtEmpLv[0].LvDetStDate.ToString("dd/MM/yyyy") + " to " + dtEmpLv[0].LvDetEndDate.ToString("dd/MM/yyyy");
                return lvDt;
            }
            catch (Exception) { return lvDt; }
        }

        public string GetEmpName(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new tblHrmsEmpTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(empRef.ToString());
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].EmpFirstName.ToString() + " " + dtEmp[0].EmpLastName.ToString();
                return empStr;
            }
            catch (Exception) { return empStr; }
        }

        public string GetEmpId(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].EmpId.ToString();
                return empStr;
            }
            catch (Exception) { return empStr; }
        }

        public string GetEmpDesig(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].DesigName.ToString();
                return empStr;
            }
            catch (Exception) { return empStr; }
        }

        public string GetEmpDept(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].DeptName.ToString();
                return empStr;
            }
            catch (Exception) { return empStr; }
        }

        public string GetLvType(int lvTypeRef)
        {
            string lvType = "";
            try
            {
                var taLvMas = new tblHrmsLeaveMasTableAdapter();
                var dtLvMas = taLvMas.GetDataByRefNo(Convert.ToInt32(lvTypeRef.ToString()));
                if (dtLvMas.Rows.Count > 0)
                    lvType = dtLvMas[0].LvMasName.ToString() + " (" + String.Format("{0:0.##}", dtLvMas[0].LvMasMaxDays) + " days)";
                return lvType;
            }
            catch (Exception ex) { return lvType; }
        }

        public string GetLvBal(string lvYr, string empRef, int lvTypeRef)
        {
            decimal lvBal = 0;
            try
            {
                var taEmpLv = new tblHrmsEmpLeaveTableAdapter();
                var dtEmpLv = taEmpLv.GetTotEmpLv(empRef.ToString(), lvTypeRef.ToString(), Convert.ToDecimal(Convert.ToDateTime(lvYr.ToString()).Year));
                var totEmpLv = dtEmpLv == null ? 0 : Convert.ToDecimal(dtEmpLv);

                var taLvMas = new tblHrmsLeaveMasTableAdapter();
                var dtLvMas = taLvMas.GetDataByRefNo(Convert.ToInt32(lvTypeRef.ToString()));
                var maxDays = dtLvMas[0].IsLvMasMaxDaysNull() ? 0 : dtLvMas[0].LvMasMaxDays;

                lvBal = (maxDays - totEmpLv);

                return lvBal.ToString();
            }
            catch (Exception ex) { return lvBal.ToString(); }
        }
        #endregion                

        protected void cboEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (optDetails.Checked)
            //    {
            //        var taLvDet = new tblHrmsEmpLeaveTableAdapter();
            //        var dtLvDet = new dsHrmsTran.tblHrmsEmpLeaveDataTable();
            //        if (cboEmp.SelectedIndex == 0)
            //            dtLvDet = taLvDet.GetDataByDateRange(Convert.ToDateTime(txtFromDate.Text.Trim()), Convert.ToDateTime(txtToDate.Text.Trim()));
            //        else
            //            dtLvDet = taLvDet.GetDataByDateRangeEmp(cboEmp.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text.Trim()), Convert.ToDateTime(txtToDate.Text.Trim()));
            //        btnExport.Visible = dtLvDet.Rows.Count > 0;
            //        gvEmpLvRptDet.DataSource = dtLvDet;
            //        gvEmpLvRptDet.DataBind();

            //        gvEmpLvRptDet.Visible = true;
            //        gvEmpLvRptSum.Visible = false;
            //    }
            //}
            //catch (Exception ex) { }
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