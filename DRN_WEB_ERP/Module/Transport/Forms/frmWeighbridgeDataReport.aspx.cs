using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransDetTableAdapters;

namespace DRN_WEB_ERP.Module.Transport.Forms
{
    public partial class frmWeighbridgeDataReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            var taWgtData = new tbl_Weighbridge_DataTableAdapter();
            var dtWgtData = taWgtData.GetDataByDateRange(txtFromDate.Text, txtToDate.Text);
            Session["data"] = dtWgtData;
            SetItemGridData();            
            btnExport.Enabled = dtWgtData.Rows.Count > 0;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count > 65535)
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
                GridView1.AllowPaging = false;
                var taWgtData = new tbl_Weighbridge_DataTableAdapter();
                GridView1.DataSource = taWgtData.GetDataByDateRange(txtFromDate.Text, txtToDate.Text);
                GridView1.DataBind();

                GridView1.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }

                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
                    row.Height = 18;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView1.RowStyle.BackColor;
                        }
                        cell.Wrap = false;
                        cell.CssClass = "textmode";
                    }
                }

                GridView1.RenderControl(hw);

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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            SetItemGridData();
        }

        protected void SetItemGridData()
        {
            var dtItem = Session["data"];
            GridView1.DataSource = dtItem;
            GridView1.DataBind();
            GridView1.SelectedIndex = -1;
        }
    }
}