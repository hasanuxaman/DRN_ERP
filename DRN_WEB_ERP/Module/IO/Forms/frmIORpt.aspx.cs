using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.IO.DataSet.dsIOTableAdapters;

namespace DRN_WEB_ERP.Module.IO.Forms
{
    public partial class frmIORpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (Page.IsPostBack) return;

            var suprRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            var taEmp = new View_Emp_BascTableAdapter();
            var dtEmp = new DataTable();
            if (suprRef == "000555" || suprRef == "000568")
                dtEmp = taEmp.GetDataByAsc();
            else
                dtEmp = taEmp.GetDataBySupRef(suprRef.ToString());
            
            cboEmp.DataSource = dtEmp;
            cboEmp.DataValueField = "EmpRefNo";
            cboEmp.DataTextField = "EmpName";
            cboEmp.DataBind();
            cboEmp.Items.Insert(0, new ListItem("---ALL---", "0"));

            gvIoRpt.DataSource = dtEmp;
            gvIoRpt.DataBind();
        }

        protected void gvIoRpt_RowDataBound(object sender, GridViewRowEventArgs e)
        {            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Image UsrImages = (System.Web.UI.WebControls.Image)e.Row.FindControl("AttachImage");
                var empRefNo = ((HiddenField)e.Row.FindControl("hfEmpRef")).Value;
                UsrImages.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRefNo + "'";
            }
        }

        #region GridData
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
            catch (Exception ex) { return empStr; }
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
            catch (Exception ex) { return empStr; }
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
            catch (Exception ex) { return empStr; }
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
            catch (Exception ex) { return empStr; }
        }

        public string GetIoLimit(string empRef)
        {
            string ioLimit = "0";
            try
            {
                var taIoLimit = new tblAccIoLimitTableAdapter();

                var dtIoLimit = taIoLimit.GetDataByEmpRef(empRef.ToString());

                ioLimit = dtIoLimit.Rows.Count > 0 ? dtIoLimit[0].IoAccLimit.ToString("N", System.Globalization.CultureInfo.InvariantCulture) : "00.00";

                return ioLimit;
            }
            catch (Exception ex) { return ioLimit; }
        }

        public string GetIoUnAdj(string empRef)
        {
            string ioUnAdj = "0";
            try
            {
                var taIoReq = new tblAccIoReqTableAdapter();
                var taIoAdj = new tblAccIoAdjTableAdapter();

                var dtWithdraw = taIoReq.GetWithdrawAmt(empRef.ToString());
                double IoWithdraw = dtWithdraw == null ? Convert.ToDouble(0) : Convert.ToDouble(dtWithdraw);

                var dtAdjust = taIoAdj.GetAdjustAmount(empRef.ToString());
                double IoAdjust = dtAdjust == null ? Convert.ToDouble(0) : Convert.ToDouble(dtAdjust);

                ioUnAdj = (Convert.ToDouble(dtWithdraw) - IoAdjust).ToString("N", System.Globalization.CultureInfo.InvariantCulture);

                return ioUnAdj;
            }
            catch (Exception ex) { return ioUnAdj; }
        }

        public string GetIoAvailLimit(string empRef)
        {
            string ioAvailLimit = "0";
            try
            {
                var taIoLimit = new tblAccIoLimitTableAdapter();
                var taIoReq = new tblAccIoReqTableAdapter();
                var taIoAdj = new tblAccIoAdjTableAdapter();

                var dtIo = taIoLimit.GetDataByEmpRef(empRef.ToString());
                double IoLimit = dtIo.Rows.Count > 0 ? Convert.ToDouble(dtIo[0].IoAccLimit) : Convert.ToDouble(0);

                var dtWithdraw = taIoReq.GetWithdrawAmt(empRef.ToString());
                double IoWithdraw = dtWithdraw == null ? Convert.ToDouble(0) : Convert.ToDouble(dtWithdraw);

                var dtAdjust = taIoAdj.GetAdjustAmount(empRef.ToString());
                double IoAdjust = dtAdjust == null ? Convert.ToDouble(0) : Convert.ToDouble(dtAdjust);

                ioAvailLimit = (IoLimit - (IoWithdraw - IoAdjust)).ToString("#,##0.00");

                return ioAvailLimit;
            }
            catch (Exception ex) { return ioAvailLimit; }
        }
        #endregion

        protected void cboEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            var suprRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            var taEmp = new View_Emp_BascTableAdapter();
            var dtEmp = new DataTable();
            if (cboEmp.SelectedIndex == 0)
                dtEmp = taEmp.GetDataBySupRef(suprRef.ToString());
            else
                dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(cboEmp.SelectedValue.ToString()));
            gvIoRpt.DataSource = dtEmp;
            gvIoRpt.DataBind();
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            var suprRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            var taEmp = new View_Emp_BascTableAdapter();
            var dtEmp = new DataTable();
            if (suprRef == "000555" || suprRef == "000568")
                dtEmp = taEmp.GetDataByAsc();
            else
                dtEmp = taEmp.GetDataBySupRef(suprRef.ToString());

            gvIoRpt.DataSource = dtEmp;
            gvIoRpt.DataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //GlobalClass.clsPrintHelper.PrintWebControl(divPrint);

            gvIoRpt.AllowPaging = false;
            var suprRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            var taEmp = new View_Emp_BascTableAdapter();
            var dtEmp = new DataTable();
            if (suprRef == "000555" || suprRef == "000568")
                dtEmp = taEmp.GetDataByAsc();
            else
                dtEmp = taEmp.GetDataBySupRef(suprRef.ToString());
            gvIoRpt.DataSource = dtEmp;
            gvIoRpt.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvIoRpt.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(gvIoRpt, this.GetType(), "GridPrint", sb.ToString(), false);
            gvIoRpt.AllowPaging = true;
            gvIoRpt.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvIoRpt.Rows.Count > 65535)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Export to Excel is not allowed due to excessive number of rows. (65535) ')", true);
                return;
            }

            //First Method
            #region With Formating
            Response.Clear();
            Response.Buffer = true;
            string filename = String.Format("IO_Report_{0}_{1}_{2}.xls", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy"));
            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvIoRpt.AllowPaging = false;

                var suprRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = new DataTable();
                if (suprRef == "000555" || suprRef == "000568")
                    dtEmp = taEmp.GetDataByAsc();
                else
                    dtEmp = taEmp.GetDataBySupRef(suprRef.ToString());
                gvIoRpt.DataSource = dtEmp;
                gvIoRpt.DataBind();

                gvIoRpt.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in gvIoRpt.HeaderRow.Cells)
                {
                    cell.BackColor = gvIoRpt.HeaderStyle.BackColor;
                }

                foreach (GridViewRow row in gvIoRpt.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvIoRpt.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvIoRpt.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvIoRpt.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            #endregion
        }
    }
}