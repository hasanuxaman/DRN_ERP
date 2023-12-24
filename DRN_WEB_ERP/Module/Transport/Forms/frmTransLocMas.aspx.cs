using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Transport.DataSet;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;

namespace DRN_WEB_ERP.Module.Transport.Forms
{
    public partial class frmTransLocMas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taLocMas = new tbl_TrTr_Loc_MasTableAdapter();
            var dtMaxLocRef = taLocMas.GetMaxLocRef();
            var nextLocRef = dtMaxLocRef == null ? "000001" : (Convert.ToInt32(dtMaxLocRef) + 1).ToString("000000");
            txtLocRefNo.Text = "LOC-" + nextLocRef.ToString();

            var taDist = new tblSalesDistrictTableAdapter();
            cboLocDist.DataSource = taDist.GetDataByAsc();
            cboLocDist.DataTextField = "DistName";
            cboLocDist.DataValueField = "DistRef";
            cboLocDist.DataBind();
            cboLocDist.Items.Insert(0, new ListItem("---Select---", "0"));

            cboLocThana.Items.Insert(0, new ListItem("---Select---", "0"));

            cboLocDistSrch.DataSource = taDist.GetDataByAsc();
            cboLocDistSrch.DataTextField = "DistName";
            cboLocDistSrch.DataValueField = "DistRef";
            cboLocDistSrch.DataBind();
            cboLocDistSrch.Items.Insert(0, new ListItem("---Select---", "0"));

            cboLocThanaSrch.Items.Insert(0, new ListItem("---Select---", "0"));

            btnSave.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";

            //var dtLocMas = taLocMas.GetData();
            //Session["data"] = dtLocMas;
            //SetLocationGridData();

            FilterGridData();
        }

        protected void SetLocationGridData()
        {
            var dtLocMas = Session["data"];
            gvLocMas.DataSource = dtLocMas;
            gvLocMas.DataBind();
            gvLocMas.SelectedIndex = -1;
        }

        #region GridData
        public string GetDistName(string distRef)
        {
            string distName = "";
            try
            {
                var taDist = new tblSalesDistrictTableAdapter();
                var dtDist = taDist.GetDataByDistRef(Convert.ToInt32(distRef.ToString()));
                if (dtDist.Rows.Count > 0)
                    distName = dtDist[0].DistName.ToString();
                return distName;
            }
            catch (Exception) { return distName; }
        }

        public string GetThanaName(string thanaRef)
        {
            string thanaName = "";
            try
            {
                var taThana = new tblSalesThanaTableAdapter();
                var dtThana = taThana.GetDataByThanaRef(Convert.ToInt32(thanaRef.ToString()));
                if (dtThana.Rows.Count > 0)
                    thanaName = dtThana[0].ThanaName.ToString();
                return thanaName;
            }
            catch (Exception) { return thanaName; }
        }
        #endregion

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("Save");

            var taLocMas = new tbl_TrTr_Loc_MasTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taLocMas.Connection);

            try
            {
                taLocMas.AttachTransaction(myTran);

                var distance = txtLocDistance.Text.Trim().Length <= 0 ? "0" : txtLocDistance.Text.Trim();

                if (hfEditStatus.Value == "N")
                {
                    #region Insert Location
                    var dtMaxLocRef = taLocMas.GetMaxLocRef();
                    var nextLocRef = dtMaxLocRef == null ? "000001" : (Convert.ToInt32(dtMaxLocRef) + 1).ToString("000000");
                    var nextLocRefNo = "LOC-" + nextLocRef.ToString();

                    taLocMas.InsertLocMas(nextLocRefNo.ToString(), txtLocName.Text.Trim(), cboLocDist.SelectedValue.ToString(), cboLocThana.SelectedValue.ToString(),
                        Convert.ToInt32(distance), txtLocRent.Text.Trim(), txtLocRem.Text.Trim(), "", optListLocStatus.SelectedValue.ToString(), "");

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnSave.Text = "Save";
                    hfEditStatus.Value = "N";
                    hfRefNo.Value = "0";
                    SetLocationGridData();
                    ClearData();
                    txtSearchLoc.Text = "";
                    btnClearSrch.Visible = false;
                    #endregion
                }
                else
                {
                    #region Update Location
                    var dtLocMas = taLocMas.GetDataByLocRefNo(hfRefNo.Value);
                    if (dtLocMas.Rows.Count > 0)
                    {
                        taLocMas.UpdateLocMas(txtLocName.Text.Trim(), cboLocDist.SelectedValue.ToString(), cboLocThana.SelectedValue.ToString(),
                        Convert.ToInt32(distance), txtLocRent.Text.Trim(), txtLocRem.Text.Trim(), "", optListLocStatus.SelectedValue.ToString(), "", hfRefNo.Value);
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Location.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnSave.Text = "Save";
                    hfEditStatus.Value = "N";
                    hfRefNo.Value = "0";
                    SetLocationGridData();
                    ClearData();
                    txtSearchLoc.Text = "";
                    btnClearSrch.Visible = false;
                    #endregion
                }

                //var taLocMasNew = new tbl_TrTr_Loc_MasTableAdapter();
                //var dtLocMasNew = taLocMasNew.GetData();
                //Session["data"] = dtLocMasNew;
                //SetLocationGridData();

                FilterGridData();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        private void ClearData()
        {
            var taLocMas = new tbl_TrTr_Loc_MasTableAdapter();
            var dtMaxLocRef = taLocMas.GetMaxLocRef();
            var nextLocRef = dtMaxLocRef == null ? "000001" : (Convert.ToInt32(dtMaxLocRef) + 1).ToString("000000");
            txtLocRefNo.Text = "LOC-" + nextLocRef.ToString();

            txtLocName.Text = "";
            txtLocRem.Text = "";
            txtLocDistance.Text = "";
            txtLocRent.Text = "";

            cboLocDist.SelectedIndex = 0;
            cboLocThana.Items.Clear();
            cboLocThana.Items.Insert(0, new ListItem("---Select---", "0"));
            cboLocThana.SelectedIndex = 0;
            cboLocThana.SelectedIndex = 0;

            optListLocStatus.SelectedValue = "1";

            btnSave.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";

            var dtLocMas = taLocMas.GetData();
            Session["data"] = dtLocMas;
            SetLocationGridData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //var taLocMas = new View_TrTr_Loc_MasTableAdapter();

            //if (txtSearchLoc.Text.Trim().Length <= 0) return;

            //try
            //{
            //    var locRef = "";
            //    var srchWords = txtSearchLoc.Text.Trim().Split(':');
            //    foreach (string word in srchWords)
            //    {
            //        locRef = word;
            //        break;
            //    }

            //    if (locRef.Length > 0)
            //    {
            //        var dtLocMas = taLocMas.GetDataBySearchList(locRef);
            //        if (dtLocMas.Rows.Count > 0)
            //        {
            //            Session["data"] = dtLocMas;
            //            SetLocationGridData();
            //            btnClearSrch.Visible = true;

            //            btnExport.Enabled = dtLocMas.Rows.Count > 0;
            //        }
            //        else
            //        {
            //            tblMsg.Rows[0].Cells[0].InnerText = "No Data Found.";
            //            tblMsg.Rows[1].Cells[0].InnerText = "";
            //            ModalPopupExtenderMsg.Show();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
            //    tblMsg.Rows[1].Cells[0].InnerText = "";
            //    ModalPopupExtenderMsg.Show();
            //}

            FilterGridData();
        }

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            txtSearchLoc.Text = "";
            cboLocDistSrch.SelectedIndex = 0;
            cboLocThanaSrch.Items.Clear();
            cboLocThanaSrch.Items.Insert(0, new ListItem("---Select---", "0"));
            cboLocThanaSrch.SelectedIndex = 0;
            var taLocMas = new tbl_TrTr_Loc_MasTableAdapter();
            var dtLocMas = taLocMas.GetData();
            Session["data"] = dtLocMas;
            SetLocationGridData();
            btnClearSrch.Visible = false;
            btnExport.Enabled = dtLocMas.Rows.Count > 0;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var qryStr = "SELECT [TrTr_Loc_Ref_No] as Location_Ref_No,[TrTr_Loc_Name] as Location_Name,[DistName] as District,[ThanaName] as Thana, " +
                    "[TrTr_Loc_Distance_Km] as Distance,[TrTr_Loc_Ext_Data1] as Rent,[TrTr_Loc_Ext_Data2] as Remarks,[TrTr_Loc_Status] as Status " +
                    "FROM [DRN].[dbo].[View_TrTr_Loc_Mas]";

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
                string filename = String.Format("Location_List_as_on_{0}.xls", DateTime.Now.ToString("dd-MM-yy"));
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

        protected void gvLocMas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLocMas.PageIndex = e.NewPageIndex;
            SetLocationGridData();
        }

        protected void gvLocMas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvLocMas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvLocMas.SelectedIndex;

            if (indx != -1)
            {
                var taLocMas = new tbl_TrTr_Loc_MasTableAdapter();

                try
                {
                    var lblLocRefNo = (Label)gvLocMas.Rows[indx].FindControl("lblLocRefNo");
                    hfRefNo.Value = lblLocRefNo.Text;
                    hfEditStatus.Value = "Y";
                    btnSave.Text = "Edit";

                    var dtLocMas = taLocMas.GetDataByLocRefNo(hfRefNo.Value.ToString());
                    if (dtLocMas.Rows.Count > 0)
                    {
                        txtLocRefNo.Text = dtLocMas[0].TrTr_Loc_Ref_No.ToString();
                        txtLocName.Text = dtLocMas[0].TrTr_Loc_Name.ToString();
                        txtLocRem.Text = dtLocMas[0].TrTr_Loc_Ext_Data2.ToString();
                        txtLocDistance.Text = dtLocMas[0].TrTr_Loc_Distance_Km.ToString();
                        txtLocRent.Text = dtLocMas[0].TrTr_Loc_Ext_Data1.ToString();

                        try
                        {
                            cboLocDist.SelectedIndex = cboLocDist.Items.IndexOf(cboLocDist.Items.FindByValue(dtLocMas[0].IsTrTr_Loc_DistNull() ? "0" : dtLocMas[0].TrTr_Loc_Dist.ToString()));

                            optListLocStatus.SelectedValue = dtLocMas[0].TrTr_Loc_Status.ToString();

                            var taThana = new tblSalesThanaTableAdapter();
                            cboLocThana.DataSource = taThana.GetDataByDistRef(Convert.ToInt32(cboLocDist.SelectedValue));
                            cboLocThana.DataTextField = "ThanaName";
                            cboLocThana.DataValueField = "ThanaRef";
                            cboLocThana.DataBind();
                            cboLocThana.Items.Insert(0, new ListItem("---Select---", "0"));

                            cboLocThana.SelectedIndex = cboLocThana.Items.IndexOf(cboLocThana.Items.FindByValue(dtLocMas[0].IsTrTr_Loc_ThanaNull() ? "0" : dtLocMas[0].TrTr_Loc_Thana.ToString()));
                        }
                        catch (Exception ex)
                        {
                            ClearData();
                            tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ClearData();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void gvLocMas_Sorting(object sender, GridViewSortEventArgs e)
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
                gvLocMas.DataSource = dataView;
                gvLocMas.DataBind();
            }
        }

        protected void cboLocDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboLocThana.Items.Clear();
            var taThana = new tblSalesThanaTableAdapter();
            cboLocThana.DataSource = taThana.GetDataByDistRef(Convert.ToInt32(cboLocDist.SelectedValue));
            cboLocThana.DataTextField = "ThanaName";
            cboLocThana.DataValueField = "ThanaRef";
            cboLocThana.DataBind();
            cboLocThana.Items.Insert(0, new ListItem("---Select---", "0"));
        }

        protected void cboLocDistSrch_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboLocThanaSrch.Items.Clear();
            var taThana = new tblSalesThanaTableAdapter();
            cboLocThanaSrch.DataSource = taThana.GetDataByDistRef(Convert.ToInt32(cboLocDistSrch.SelectedValue));
            cboLocThanaSrch.DataTextField = "ThanaName";
            cboLocThanaSrch.DataValueField = "ThanaRef";
            cboLocThanaSrch.DataBind();
            cboLocThanaSrch.Items.Insert(0, new ListItem("---Select---", "0"));

            //var taLocMas = new tbl_TrTr_Loc_MasTableAdapter();
            //var dtLocMas = taLocMas.GetDataByDist(cboLocDistSrch.SelectedValue.ToString());
            //Session["data"] = dtLocMas;
            //SetLocationGridData();

            //btnExport.Enabled = dtLocMas.Rows.Count > 0;

            FilterGridData();
        }

        protected void cboLocThanaSrch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var taLocMas = new tbl_TrTr_Loc_MasTableAdapter();
            //var dtLocMas = taLocMas.GetDataByThana(cboLocThanaSrch.SelectedValue.ToString());
            //Session["data"] = dtLocMas;
            //SetLocationGridData();

            //btnExport.Enabled = dtLocMas.Rows.Count > 0;

            FilterGridData();
        }

        protected void FilterGridData()
        {
            var taLocMas = new tbl_TrTr_Loc_MasTableAdapter();
            var dtLocMas = new dsTransMas.tbl_TrTr_Loc_MasDataTable();

            #region Get Location Ref
            var locRef = "";
            var srchWords = txtSearchLoc.Text.Trim().Split(':');
            foreach (string word in srchWords)
            {
                locRef = word;
                break;
            }

            if (locRef.Length > 0)
            {
                var dtLocMasNew = taLocMas.GetDataByLocRefNo(locRef);
                if (dtLocMasNew.Rows.Count > 0)
                    locRef = dtLocMasNew[0].TrTr_Loc_Ref_No.ToString();
            }
            #endregion

            if (cboLocDistSrch.SelectedIndex == 0)
            {
                if (cboLocThanaSrch.SelectedIndex == 0)
                {
                    if (locRef.Length > 0)
                        dtLocMas = taLocMas.GetDataByLocRefNo(locRef.ToString());
                    else
                        dtLocMas = taLocMas.GetDataByAsc();
                }
                else
                {
                    if (locRef.Length > 0)
                        dtLocMas = taLocMas.GetDataByThanaLocRef(cboLocThanaSrch.SelectedValue.ToString(), locRef.ToString());
                    else
                        dtLocMas = taLocMas.GetDataByThana(cboLocThanaSrch.SelectedValue.ToString());
                }
            }
            else
            {
                if (cboLocThanaSrch.SelectedIndex == 0)
                {
                    if (locRef.Length > 0)
                        dtLocMas = taLocMas.GetDataByDistLocRef(cboLocDistSrch.SelectedValue.ToString(), locRef.ToString());
                    else
                        dtLocMas = taLocMas.GetDataByDist(cboLocDistSrch.SelectedValue.ToString());
                }
                else
                {
                    if (locRef.Length > 0)
                        dtLocMas = taLocMas.GetDataByDistThanaLocRef(cboLocDistSrch.SelectedValue.ToString(), cboLocThanaSrch.SelectedValue.ToString(), locRef.ToString());
                    else
                        dtLocMas = taLocMas.GetDataByDistThana(cboLocDistSrch.SelectedValue.ToString(), cboLocThanaSrch.SelectedValue.ToString());
                }
            }

            Session["data"] = dtLocMas;
            SetLocationGridData();

            btnExport.Enabled = dtLocMas.Rows.Count > 0;
        }
    }
}