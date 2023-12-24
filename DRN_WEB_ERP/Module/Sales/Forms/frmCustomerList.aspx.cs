using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmCustomerList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taParAdr = new tblSalesPartyAdrTableAdapter();
            var dtParAdr = taParAdr.GetData();
            Session["data"] = dtParAdr;
            SetCustomerGridData();
        }

        protected void gvCust_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCust.PageIndex = e.NewPageIndex;
            SetCustomerGridData();
        }

        protected void gvCust_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortexp = e.SortExpression;
            Session["sortexp"] = sortexp;
            if (Session["sortDirection"] != null && Session["sortDirection"].ToString() == SortDirection.Descending.ToString())
            {
                Session["sortDirection"] = SortDirection.Ascending;
                ConvertSortDirection(sortexp, "ASC");
            }
            else
            {
                Session["sortDirection"] = SortDirection.Descending;
                ConvertSortDirection(sortexp, "DESC");
            }
        }

        protected void SetCustomerGridData()
        {
            var dtParAdr = Session["data"];
            gvCust.DataSource = dtParAdr;
            gvCust.DataBind();
            gvCust.SelectedIndex = -1;
        }

        private void ConvertSortDirection(string soreExpression, string p)
        {
            DataTable dataTable = Session["data"] as DataTable;
            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = soreExpression + " " + p;
                gvCust.DataSource = dataView;
                gvCust.DataBind();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var qryStr = "SELECT [Par_Adr_Ref_No] as DLR_Ref, [Par_Adr_Name] as DLR_Name, [CustTypeName] as DLR_Type, [Par_Adr_Addr] as DLR_Address, " +
                           "[Par_Adr_Cont_Per] as DLR_Contact, [Par_Adr_Cell_No] as DLR_Cell, [Par_Adr_Tel_No] as DLR_Tel, [Par_Adr_Email_Id] as DLR_Email, " +
                           "[DistName] as DLR_District, [ThanaName] as DLR_Thana, [Par_Adr_Cr_Limit] as Cr_Limit, [Par_Adr_Cr_Days] as Cr_Days, [Dsm_Full_Name] as DSM_Name, " +
                           "[SalesZoneName] as Sales_Zone, Sp_Full_Name as [MPO Name], convert(date,Par_Adr_Ent_Date,103) as Entry_Date " +
                           "FROM [tblSalesPartyAdr] left outer join [tblSalesPartyType] on [Par_Adr_Type]=[CustTypeRef] " +
                           "left outer join [tblSalesDistrict] on [Par_Adr_Dist]=[DistRef] left outer join [tblSalesThana] on [Par_Adr_Thana]=[ThanaRef] " +
                           "left outer join tblSalesDsmMas on Par_Adr_Sls_Per=Dsm_Ref left outer join tblSalesZone on Dsm_Sls_Zone=SalesZoneRef " +
                           "left outer join [tblSalesPersonMas] on Par_Adr_Ext_Data2=Sp_Ref order by [Par_Adr_Name]";

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
                string filename = String.Format("Dealer_List_as_on_{0}.xls", DateTime.Now.ToString("dd-MM-yy"));
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
    }
}