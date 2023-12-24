using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DRN_WEB_ERP.Module.Transport.DataSet;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;

namespace DRN_WEB_ERP.Module.Transport.Forms
{
    public partial class frmLoadSlipReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            string CmdSalesString = "";
            if (optRpt.SelectedValue == "0")
            {
                CmdSalesString = "SELECT ROW_NUMBER() OVER(ORDER BY LS_Date_Time desc) AS Row#, [LS_Ref_No] as Load_Slip_Ref,LS_Ext_Data2 as Vsl_Type, [LS_Truck_No] as Truck_No, " +
                                         "[LS_Date_Time] as IN_Time,[LS_DO_Dealer] as Dealer_Name,[LS_DO_Item] as Item_Name,[LS_DO_Bag_Type] as Bag_Type,[LS_DO_Qty] as DO_Qty, " +
                                         "[LS_DO_Updt_Time] as Updt_Time,[LS_DO_Chln] as Challan_No,[LS_DO_Chln_Updt_Time] as Chln_Updt_Time,[LS_WS_Empty_Wgt] as Empty_Weight, " +
                                         "[LS_WS_Empty_Wgt_Updt_Time] as Empty_Updt_Time,[LS_WS_Load_Wgt] as Loaded_Weight,[LS_WS_Load_Wgt_Updt_Time] as Loaded_Updt_Time, " +
                                         "LS_TLY_Item_Name as Taly_Item_Name,LS_TLY_Item_Qty as Taly_Qty,LS_TLY_Updt_Time as Taly_Updt_Time, LS_VAT_Chln_Status as VAT_Challan_Status, " +
                                         "LS_VAT_Chln_Updt_Time as VAT_Challan_Updt_Time, LS_ACC_Updt_Time as Accounts_Updt_Time, LS_GATE_PASS_Time as Gate_Out_Time " +
                                         "FROM [DRN].[dbo].[tbl_TrTr_Load_Slip] where convert(date,LS_Date_Time,103) " +
                                         "between convert(date,'" + txtFromDate.Text + "',103) and convert(date,'" + txtToDate.Text + "',103) order by LS_Date_Time desc";

            }
            if (optRpt.SelectedValue == "1")
            {
                CmdSalesString = "SELECT ROW_NUMBER() OVER(ORDER BY LS_Date_Time desc) AS Row#, [LS_Ref_No] as Load_Slip_Ref,LS_Ext_Data2 as Vsl_Type, [LS_Truck_No] as Truck_No, " +
                                         "[LS_Date_Time] as IN_Time,[LS_DO_Dealer] as Dealer_Name,[LS_DO_Item] as Item_Name,[LS_DO_Bag_Type] as Bag_Type,[LS_DO_Qty] as DO_Qty, " +
                                         "[LS_DO_Updt_Time] as Updt_Time,[LS_DO_Chln] as Challan_No,[LS_DO_Chln_Updt_Time] as Chln_Updt_Time,[LS_WS_Empty_Wgt] as Empty_Weight, " +
                                         "[LS_WS_Empty_Wgt_Updt_Time] as Empty_Updt_Time,[LS_WS_Load_Wgt] as Loaded_Weight,[LS_WS_Load_Wgt_Updt_Time] as Loaded_Updt_Time, " +
                                         "LS_TLY_Item_Name as Taly_Item_Name,LS_TLY_Item_Qty as Taly_Qty,LS_TLY_Updt_Time as Taly_Updt_Time, LS_VAT_Chln_Status as VAT_Challan_Status, " +
                                         "LS_VAT_Chln_Updt_Time as VAT_Challan_Updt_Time, LS_ACC_Updt_Time as Accounts_Updt_Time, LS_GATE_PASS_Time as Gate_Out_Time " +
                                         "FROM [DRN].[dbo].[tbl_TrTr_Load_Slip] where LS_GATE_PASS_Time is null and convert(date,LS_Date_Time,103) " +
                                         "between convert(date,'" + txtFromDate.Text + "',103) and convert(date,'" + txtToDate.Text + "',103) order by LS_Date_Time desc";

            }
            else if (optRpt.SelectedValue == "2")
            {
                CmdSalesString = "SELECT ROW_NUMBER() OVER(ORDER BY LS_Date_Time desc) AS Row#, [LS_Ref_No] as Load_Slip_Ref,LS_Ext_Data2 as Vsl_Type, [LS_Truck_No] as Truck_No,[LS_Date_Time] as IN_Time, " +
                                         "[LS_DO_Dealer] as Dealer_Name,[LS_DO_Item] as Item_Name,[LS_DO_Bag_Type] as Bag_Type,[LS_DO_Qty] as DO_Qty, " +
                                         "[LS_DO_Updt_Time] as Updt_Time,[LS_DO_Chln] as Challan_No,[LS_DO_Chln_Updt_Time] as Chln_Updt_Time,[LS_WS_Empty_Wgt] as Empty_Weight, " +
                                         "[LS_WS_Empty_Wgt_Updt_Time] as Empty_Updt_Time,[LS_WS_Load_Wgt] as Loaded_Weight,[LS_WS_Load_Wgt_Updt_Time] as Loaded_Updt_Time, " +
                                         "LS_TLY_Item_Name as Taly_Item_Name,LS_TLY_Item_Qty as Taly_Qty,LS_TLY_Updt_Time as Taly_Updt_Time, LS_VAT_Chln_Status as VAT_Challan_Status, " +
                                         "LS_VAT_Chln_Updt_Time as VAT_Challan_Updt_Time, LS_ACC_Updt_Time as Accounts_Updt_Time, LS_GATE_PASS_Time as Gate_Out_Time " +
                                         "FROM [DRN].[dbo].[tbl_TrTr_Load_Slip] where LS_GATE_PASS_Time is not null and convert(date,LS_GATE_PASS_Time,103) " +
                                         "between convert(date,'" + txtFromDate.Text + "',103) and convert(date,'" + txtToDate.Text + "',103) order by LS_Date_Time desc";
            }

            using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DRNConStr"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(CmdSalesString, con))
                {
                    // get the adapter object and attach the command object to it
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        // fire Fill method to fetch the data and fill into DataTable
                        ad.Fill(table);
                    }
                }                
            }

            // specify the data source for the GridView
            gvLsDet.DataSource = table;

            // bind the data now
            gvLsDet.DataBind();

            btnExport.Visible = table.Rows.Count > 0;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            gvLsDet.DataSource = null;
            gvLsDet.DataBind();

            btnExport.Visible = false;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvLsDet.Rows.Count > 65535)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Export to Excel is not allowed due to excessive number of rows. (65535) ')", true);
                return;
            }

            //First Method
            #region With Formating
            Response.Clear();
            Response.Buffer = true;
            string filename = String.Format("Load_Slip_Report_{0}_{1}_{2}.xls", txtFromDate.Text.Trim(), "to", txtToDate.Text.Trim());
            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvLsDet.AllowPaging = false;

                #region Load Gridview Data
                DataTable table = new DataTable();
                string CmdSalesString = "";
                if (optRpt.SelectedValue == "0")
                {
                    CmdSalesString = "SELECT ROW_NUMBER() OVER(ORDER BY LS_Date_Time desc) AS Row#, [LS_Ref_No] as Load_Slip_Ref,LS_Ext_Data2 as Vsl_Type, [LS_Truck_No] as Truck_No, " +
                                             "[LS_Date_Time] as IN_Time,[LS_DO_Dealer] as Dealer_Name,[LS_DO_Item] as Item_Name,[LS_DO_Bag_Type] as Bag_Type,[LS_DO_Qty] as DO_Qty, " +
                                             "[LS_DO_Updt_Time] as Updt_Time,[LS_DO_Chln] as Challan_No,[LS_DO_Chln_Updt_Time] as Chln_Updt_Time,[LS_WS_Empty_Wgt] as Empty_Weight, " +
                                             "[LS_WS_Empty_Wgt_Updt_Time] as Empty_Updt_Time,[LS_WS_Load_Wgt] as Loaded_Weight,[LS_WS_Load_Wgt_Updt_Time] as Loaded_Updt_Time, " +
                                             "LS_TLY_Item_Name as Taly_Item_Name,LS_TLY_Item_Qty as Taly_Qty,LS_TLY_Updt_Time as Taly_Updt_Time, LS_VAT_Chln_Status as VAT_Challan_Status, " +
                                             "LS_VAT_Chln_Updt_Time as VAT_Challan_Updt_Time, LS_ACC_Updt_Time as Accounts_Updt_Time, LS_GATE_PASS_Time as Gate_Out_Time " +
                                             "FROM [DRN].[dbo].[tbl_TrTr_Load_Slip] where convert(date,LS_Date_Time,103) " +
                                             "between convert(date,'" + txtFromDate.Text + "',103) and convert(date,'" + txtToDate.Text + "',103) order by LS_Date_Time desc";

                }
                if (optRpt.SelectedValue == "1")
                {
                    CmdSalesString = "SELECT ROW_NUMBER() OVER(ORDER BY LS_Date_Time desc) AS Row#, [LS_Ref_No] as Load_Slip_Ref,LS_Ext_Data2 as Vsl_Type, [LS_Truck_No] as Truck_No, " +
                                             "[LS_Date_Time] as IN_Time,[LS_DO_Dealer] as Dealer_Name,[LS_DO_Item] as Item_Name,[LS_DO_Bag_Type] as Bag_Type,[LS_DO_Qty] as DO_Qty, " +
                                             "[LS_DO_Updt_Time] as Updt_Time,[LS_DO_Chln] as Challan_No,[LS_DO_Chln_Updt_Time] as Chln_Updt_Time,[LS_WS_Empty_Wgt] as Empty_Weight, " +
                                             "[LS_WS_Empty_Wgt_Updt_Time] as Empty_Updt_Time,[LS_WS_Load_Wgt] as Loaded_Weight,[LS_WS_Load_Wgt_Updt_Time] as Loaded_Updt_Time, " +
                                             "LS_TLY_Item_Name as Taly_Item_Name,LS_TLY_Item_Qty as Taly_Qty,LS_TLY_Updt_Time as Taly_Updt_Time, LS_VAT_Chln_Status as VAT_Challan_Status, " +
                                             "LS_VAT_Chln_Updt_Time as VAT_Challan_Updt_Time, LS_ACC_Updt_Time as Accounts_Updt_Time, LS_GATE_PASS_Time as Gate_Out_Time " +
                                             "FROM [DRN].[dbo].[tbl_TrTr_Load_Slip] where LS_GATE_PASS_Time is null and convert(date,LS_Date_Time,103) " +
                                             "between convert(date,'" + txtFromDate.Text + "',103) and convert(date,'" + txtToDate.Text + "',103) order by LS_Date_Time desc";

                }
                else if (optRpt.SelectedValue == "2")
                {
                    CmdSalesString = "SELECT ROW_NUMBER() OVER(ORDER BY LS_Date_Time desc) AS Row#, [LS_Ref_No] as Load_Slip_Ref,LS_Ext_Data2 as Vsl_Type, [LS_Truck_No] as Truck_No,[LS_Date_Time] as IN_Time, " +
                                             "[LS_DO_Dealer] as Dealer_Name,[LS_DO_Item] as Item_Name,[LS_DO_Bag_Type] as Bag_Type,[LS_DO_Qty] as DO_Qty, " +
                                             "[LS_DO_Updt_Time] as Updt_Time,[LS_DO_Chln] as Challan_No,[LS_DO_Chln_Updt_Time] as Chln_Updt_Time,[LS_WS_Empty_Wgt] as Empty_Weight, " +
                                             "[LS_WS_Empty_Wgt_Updt_Time] as Empty_Updt_Time,[LS_WS_Load_Wgt] as Loaded_Weight,[LS_WS_Load_Wgt_Updt_Time] as Loaded_Updt_Time, " +
                                             "LS_TLY_Item_Name as Taly_Item_Name,LS_TLY_Item_Qty as Taly_Qty,LS_TLY_Updt_Time as Taly_Updt_Time, LS_VAT_Chln_Status as VAT_Challan_Status, " +
                                             "LS_VAT_Chln_Updt_Time as VAT_Challan_Updt_Time, LS_ACC_Updt_Time as Accounts_Updt_Time, LS_GATE_PASS_Time as Gate_Out_Time " +
                                             "FROM [DRN].[dbo].[tbl_TrTr_Load_Slip] where LS_GATE_PASS_Time is not null and convert(date,LS_GATE_PASS_Time,103) " +
                                             "between convert(date,'" + txtFromDate.Text + "',103) and convert(date,'" + txtToDate.Text + "',103) order by LS_Date_Time desc";
                }

                using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DRNConStr"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(CmdSalesString, con))
                    {
                        // get the adapter object and attach the command object to it
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            // fire Fill method to fetch the data and fill into DataTable
                            ad.Fill(table);
                        }
                    }
                }

                // specify the data source for the GridView
                gvLsDet.DataSource = table;

                // bind the data now
                gvLsDet.DataBind();
                #endregion

                gvLsDet.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in gvLsDet.HeaderRow.Cells)
                {
                    cell.BackColor = gvLsDet.HeaderStyle.BackColor;
                }

                foreach (GridViewRow row in gvLsDet.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
                    row.Height = 18;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvLsDet.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvLsDet.RowStyle.BackColor;
                        }
                        cell.Wrap = false;
                        cell.CssClass = "textmode";
                    }
                }

                gvLsDet.RenderControl(hw);

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