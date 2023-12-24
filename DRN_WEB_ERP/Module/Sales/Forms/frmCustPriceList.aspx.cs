using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmCustPriceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.IsPostBack) return;

            var qryStr = "SELECT [Par_Adr_Ref] as Dealer_Ref, [Par_Adr_Name] as Dealer_Name, CustTypeName as [DLR_Type], [Item_Det_Ref] as Item_Ref, " +
                                  "[Itm_Det_Desc] as Item_Name, [Itm_Det_Stk_Unit] as Unit, [Par_Adr_Price] as DO_Price " +
                                  "FROM [DRN].[dbo].[View_Sales_Price_Dealer] order by Dealer_Name,Item_Ref";

            SqlCommand cmd = new SqlCommand(qryStr);
            DataTable dt = GetData(cmd);
            
            Session["data"] = dt;

            SetDoPriceData();
        }

        private DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var qryStr = "SELECT [Par_Adr_Ref] as Dealer_Ref, [Par_Adr_Name] as Dealer_Name, CustTypeName as [DLR_Type], [Item_Det_Ref] as Item_Ref, " +
                                  "[Itm_Det_Desc] as Item_Name, [Itm_Det_Stk_Unit] as Unit, [Par_Adr_Price] as DO_Price " +
                                  "FROM [DRN].[dbo].[View_Sales_Price_Dealer] order by Dealer_Name,Item_Ref";

                SqlCommand cmd = new SqlCommand(qryStr);
                DataTable dt = GetData(cmd);

                if (dt.Rows.Count > 65535)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Export to Excel is not allowed due to excessive number of rows. (65535) ')", true);
                    return;
                }

                //First Method
                #region With Formating
                Response.Clear();
                Response.Buffer = true;
                string filename = String.Format("Price_List_Dealer_as_on_{0}.xls", DateTime.Now.ToString("dd-MM-yy"));
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //Create a dummy GridView
                    GridView GridView1 = new GridView();
                    GridView1.AllowPaging = false;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    GridView1.HeaderRow.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in GridView1.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView1.HeaderStyle.BackColor;
                    }

                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        row.BackColor = System.Drawing.Color.White;
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
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvDoPriceList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDoPriceList.PageIndex = e.NewPageIndex;
            SetDoPriceData();
        }

        protected void SetDoPriceData()
        {
            var dtDoPriceList = Session["data"];
            gvDoPriceList.DataSource = dtDoPriceList;
            gvDoPriceList.DataBind();
            gvDoPriceList.SelectedIndex = -1;
        }        
    }
}