using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmCustomerListByMpo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Page.IsPostBack) return;

            var empRef = "";
            if (Session["sessionEmpRef"] != null)
                empRef = Session["sessionEmpRef"].ToString();

            SqlConnection connection = new SqlConnection();
            var ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            var qryStr = "Select * from TEMP_View_Sales_Tree_Mpo_Dealer_" + empRef.ToString() + " where Par_Adr_Ref is not null";
            SqlDataAdapter daCust = new SqlDataAdapter(qryStr, connection);
            DataTable dtCust = new DataTable();
            daCust.Fill(dtCust);
            Session["data"] = dtCust;

            SetCustomerGridData();

            btnExport.Enabled = dtCust.Rows.Count > 0;

            qryStr = "Select distinct Sp_Ref,Sp_Short_Name,Sp_Full_Name from TEMP_View_Sales_Tree_Mpo_Dealer_" + empRef.ToString() + " where Par_Adr_Ref is not null";
            daCust = new SqlDataAdapter(qryStr, connection);
            dtCust = new DataTable();
            daCust.Fill(dtCust);
            for (int i = 0; i < dtCust.Rows.Count; i++)
            {
                var spRef = dtCust.Rows[i]["Sp_Ref"].ToString();
                var spName = dtCust.Rows[i]["Sp_Full_Name"].ToString() + " - [" + dtCust.Rows[i]["Sp_Short_Name"].ToString() + "]";
                ddlMpoList.Items.Add(new ListItem(spName, spRef));
            }
            ddlMpoList.Items.Insert(0, new ListItem("---Select---", "0"));

            AutoCompleteExtenderSrch.ContextKey = empRef.ToString();
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

        protected void SetCustomerGridData()
        {            
            var dtParAdr = Session["data"];
            gvCust.DataSource = dtParAdr;
            gvCust.DataBind();
            gvCust.SelectedIndex = -1;
        }

        public string GetSalesZone(string slsZoneRef)
        {
            var taSlsZone = new tblSalesZoneTableAdapter();

            string SalesZone = "";
            try
            {
                var dtSlsZone = taSlsZone.GetDataBySlsZoneRef(Convert.ToInt32(slsZoneRef.ToString()));
                if (dtSlsZone.Rows.Count > 0)
                    SalesZone = dtSlsZone[0].SalesZoneName.ToString();

                return SalesZone;
            }
            catch (Exception ex) { return SalesZone; }
        }

        public string GetSupervisor(string supervisorRef)
        {
            var taSupervisor = new View_Emp_BascTableAdapter();

            string SupervisorName = "";
            try
            {
                var dtSupervisor = taSupervisor.GetDataByEmpRef(Convert.ToInt32(supervisorRef.ToString()));
                if (dtSupervisor.Rows.Count > 0)
                    SupervisorName = dtSupervisor[0].EmpName.ToString();

                return SupervisorName;
            }
            catch (Exception ex) { return SupervisorName; }
        }

        protected void ddlMpoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var empRef = "";
            if (Session["sessionEmpRef"] != null)
                empRef = Session["sessionEmpRef"].ToString();

            SqlConnection connection = new SqlConnection();
            var ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            
            DataTable dtCust = new DataTable();

            if (ddlMpoList.SelectedIndex != 0)
            {                               
                var qryStr = "Select * from TEMP_View_Sales_Tree_Mpo_Dealer_" + empRef.ToString() + " where Par_Adr_Ref is not null and Sp_Ref='" + ddlMpoList.SelectedValue.ToString() + "'";
                SqlDataAdapter daCust = new SqlDataAdapter(qryStr, connection);                
                daCust.Fill(dtCust);
                Session["data"] = dtCust;
            }
            else
            {
                var qryStr = "Select * from TEMP_View_Sales_Tree_Mpo_Dealer_" + empRef.ToString() + " where Par_Adr_Ref is not null";
                SqlDataAdapter daCust = new SqlDataAdapter(qryStr, connection);
                daCust.Fill(dtCust);
                Session["data"] = dtCust;
            }
            SetCustomerGridData();

            btnExport.Enabled = dtCust.Rows.Count > 0;
        }

        protected void btnShowSoRpt_Click(object sender, EventArgs e)
        {
            #region Get Customer Ref
            var custRef = "";
            if (txtSearch.Text.Trim().Length > 0)
            {
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custRef = word;
                    break;
                }

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
                            custRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                    }
                }
            }
            #endregion

            var empRef = "";
            if (Session["sessionEmpRef"] != null)
                empRef = Session["sessionEmpRef"].ToString();

            SqlConnection connection = new SqlConnection();
            var ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            var qryStr = "Select * from TEMP_View_Sales_Tree_Mpo_Dealer_" + empRef.ToString() + " where Par_Adr_Ref='" + custRef.ToString() + "'";
            SqlDataAdapter daCust = new SqlDataAdapter(qryStr, connection);
            DataTable dtCust = new DataTable();
            daCust.Fill(dtCust);
            Session["data"] = dtCust;
            SetCustomerGridData();

            btnExport.Enabled = dtCust.Rows.Count > 0;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlMpoList.SelectedIndex = 0;
            txtSearch.Text = "";

            var empRef = "";
            if (Session["sessionEmpRef"] != null)
                empRef = Session["sessionEmpRef"].ToString();

            SqlConnection connection = new SqlConnection();
            var ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DRNConStr"].ToString();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            var qryStr = "Select * from TEMP_View_Sales_Tree_Mpo_Dealer_" + empRef.ToString() + " where Par_Adr_Ref is not null";
            SqlDataAdapter daCust = new SqlDataAdapter(qryStr, connection);
            DataTable dtCust = new DataTable();
            daCust.Fill(dtCust);
            Session["data"] = dtCust;

            SetCustomerGridData();

            btnExport.Enabled = dtCust.Rows.Count > 0;
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (txtSearch.Text.Trim().Length <= 0) args.IsValid = true;

                var custRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custRef = word;
                    break;
                }

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
                            args.IsValid = true;
                        else
                            args.IsValid = false;
                    }
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex) { args.IsValid = false; }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var empRef = "";
                if (Session["sessionEmpRef"] != null)
                    empRef = Session["sessionEmpRef"].ToString();

                var qryStr = "SELECT [Par_Adr_Ref] as DLR_Ref,[Par_Adr_Ref_No] as DLR_Code,[Par_Adr_Name] as Dealer_Name,[Par_Adr_Addr] as [Address],[Par_Adr_Cont_Per] as CP_Name " +
                             ",[Par_Adr_Cell_No] as Cell_No,[Par_Adr_Tel_No] as Tel_No,[Par_Adr_Fax_No] as Fax_No,[Par_Adr_Email_Id] as E_Mail,[SalesZoneName] as Sales_Zone " +
                             ",[Par_Adr_Ext_Data2] as [MPO_Ref],[Sp_Full_Name] as MPO_Name,[EmpName] as [Spervisor],[Par_Adr_Status] as [Status] " +
                             "FROM TEMP_View_Sales_Tree_Mpo_Dealer_" + empRef.ToString() + " left outer join tblSalesZone on Par_Adr_Sale_Zone=SalesZoneRef " +
                             "left outer join View_Emp_Basc on [Sp_Supr_Ref]=[EmpRefNo] where Par_Adr_Ref is not null order by Par_Adr_Name,Sp_Full_Name";

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
    }    
}