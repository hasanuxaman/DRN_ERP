using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmSupplierQtn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            //if (empRef == "000011")
            //    btnDeleteDupplicate.Visible = true;

            var taSupAcc = new tbl_PuMa_Par_Adr_QtnTableAdapter();
            var dtMaxAccRef = taSupAcc.GetMaxQtnAdrRef();
            var nextAccRef = dtMaxAccRef == null ? "10.100001" : "10." + (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
            txtSupRefNo.Text = "TMP-" + nextAccRef.ToString();

            var taCustType = new tbl_PuMa_Par_Adr_TypeTableAdapter();
            cboSupType.DataSource = taCustType.GetDataByAsc();
            cboSupType.DataTextField = "SupTypeName";
            cboSupType.DataValueField = "SupTypeRef";
            cboSupType.DataBind();
            cboSupType.Items.Insert(0, new ListItem("---Select---", "0"));

            btnSave.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";

            var taParAdr = new tbl_PuMa_Par_Adr_QtnTableAdapter();
            var dtParAdr = taParAdr.GetData();
            Session["data"] = dtParAdr;
            SetSupplierGridData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var taQtnAdr = new tbl_PuMa_Par_Adr_QtnTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taQtnAdr.Connection);

            try
            {
                taQtnAdr.AttachTransaction(myTran);                 

                var supName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtSupName.Text.Trim());
                var supCpName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtSupCp.Text.Trim());

                if (hfEditStatus.Value == "N")
                {
                    #region Form Data Validation
                    var dtSup = taQtnAdr.GetDataByQtnAdrName(txtSupName.Text.Trim());
                    if (dtSup.Rows.Count > 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Supplier already exists with this name.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    #endregion

                    #region Insert Supplier
                    var dtMaxAdrRef = taQtnAdr.GetMaxQtnAdrRef();
                    var nextAdrRef = dtMaxAdrRef == null ? "10.100001" : "10." + (Convert.ToInt32(dtMaxAdrRef) + 1).ToString();
                    var nextAdrRefNo = "TMP-" + nextAdrRef.ToString();

                    taQtnAdr.InsertQtnAdr(nextAdrRefNo.ToString(), nextAdrRefNo, supName.Trim(), Convert.ToInt32(cboSupType.SelectedValue), nextAdrRef.ToString(),
                        txtSupAdr.Text.Trim(), supCpName.ToString(), txtSupCpDesig.Text.Trim(), txtSupCell.Text.Trim(), txtSupPhone.Text.Trim(), txtSupFax.Text.Trim(),
                        txtSupEmail.Text.Trim(), "", "", "", "", "", "", DateTime.Now, 0, 0, "", "", "", 0, "", "", "", "", "", DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), optListSupStatus.SelectedValue, "");

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnSave.Text = "Save";
                    hfEditStatus.Value = "N";
                    hfRefNo.Value = "0";
                    SetSupplierGridData();
                    ClearData();
                    txtSearch.Text = "";
                    btnClearSrch.Visible = false;
                    #endregion
                }
                else
                {
                    var dtParAdr = taQtnAdr.GetDataByQtnAdrRef(hfRefNo.Value);
                    if (dtParAdr.Rows.Count > 0)
                    {
                        taQtnAdr.UpdateQtnAdr(supName.Trim(), Convert.ToInt32(cboSupType.SelectedValue), hfRefNo.Value.ToString(), txtSupAdr.Text.Trim(),
                            supCpName.ToString(), txtSupCpDesig.Text.Trim(), txtSupCell.Text.Trim(), txtSupPhone.Text.Trim(), txtSupFax.Text.Trim(), txtSupEmail.Text.Trim(),
                            "", "", "", "", "", "", 0, 0, "", "", "", 0, "", "", "", "", "", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), optListSupStatus.SelectedValue, "", hfRefNo.Value);
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Supplier Address Code.";
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
                    SetSupplierGridData();
                    ClearData();
                    txtSearch.Text = "";
                    btnClearSrch.Visible = false;
                }

                var taParAdr = new tbl_PuMa_Par_Adr_QtnTableAdapter();
                var dtPumaParAdr = taParAdr.GetData();
                Session["data"] = dtPumaParAdr;
                SetSupplierGridData();
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
            var taSupAcc = new tbl_PuMa_Par_Adr_QtnTableAdapter();
            var dtMaxAccRef = taSupAcc.GetMaxQtnAdrRef();
            var nextAccRef = dtMaxAccRef == null ? "10.100001" : "10." + (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
            txtSupRefNo.Text = "TMP-" + nextAccRef.ToString();

            txtSupName.Text = "";
            txtSupAdr.Text = "";
            txtSupCp.Text = "";
            txtSupCpDesig.Text = "";
            txtSupCell.Text = "";
            txtSupPhone.Text = "";
            txtSupFax.Text = "";
            txtSupEmail.Text = "";

            cboSupType.SelectedIndex = 0;
            optListSupStatus.SelectedValue = "1";

            btnSave.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";

            var taParAdr = new tbl_PuMa_Par_Adr_QtnTableAdapter();
            var dtPumaParAdr = taParAdr.GetData();
            Session["data"] = dtPumaParAdr;
            SetSupplierGridData();            
        }

        protected void SetSupplierGridData()
        {
            var dtParAdr = Session["data"];
            gvSup.DataSource = dtParAdr;
            gvSup.DataBind();            
            gvSup.SelectedIndex = -1;
        }

        protected void gvSup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSup.PageIndex = e.NewPageIndex;
            SetSupplierGridData();
        }

        protected void gvSup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvSup_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvSup.SelectedIndex;

            if (indx != -1)
            {
                var taSupAdr = new tbl_PuMa_Par_Adr_QtnTableAdapter();

                try
                {
                    var userRef = Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString();
                    var userEmpRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                    HiddenField hfCustRef = (HiddenField)gvSup.Rows[indx].FindControl("hfCustRef");
                    hfRefNo.Value = hfCustRef.Value;
                    hfEditStatus.Value = "Y";
                    btnSave.Text = "Edit";

                    var dtCustAdr = taSupAdr.GetDataByQtnAdrRef(hfRefNo.Value.ToString());
                    if (dtCustAdr.Rows.Count > 0)
                    {
                        txtSupRefNo.Text = dtCustAdr[0].Par_Adr_Qtn_Ref_No.ToString();
                        txtSupName.Text = dtCustAdr[0].Par_Adr_Qtn_Name.ToString();
                        txtSupAdr.Text = dtCustAdr[0].Par_Adr_Qtn_Addr.ToString();
                        txtSupCp.Text = dtCustAdr[0].Par_Adr_Qtn_Cont_Per.ToString();
                        txtSupCpDesig.Text = dtCustAdr[0].Par_Adr_Qtn_Desig.ToString();
                        txtSupCell.Text = dtCustAdr[0].Par_Adr_Qtn_Cell_No.ToString();
                        txtSupPhone.Text = dtCustAdr[0].Par_Adr_Qtn_Tel_No.ToString();
                        txtSupFax.Text = dtCustAdr[0].Par_Adr_Qtn_Fax_No.ToString();
                        txtSupEmail.Text = dtCustAdr[0].Par_Adr_Qtn_Email_Id.ToString();

                        cboSupType.SelectedValue = dtCustAdr[0].Par_Adr_Qtn_Type.ToString();

                        optListSupStatus.SelectedValue = dtCustAdr[0].Par_Adr_Qtn_Status.ToString();

                        if (userEmpRef == "000011")              
                            txtSupName.Enabled = true;                        
                        else                        
                            txtSupName.Enabled = false;                        
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

        protected void gvSup_Sorting(object sender, GridViewSortEventArgs e)
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
                gvSup.DataSource = dataView;
                gvSup.DataBind();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var taSupAdr = new tbl_PuMa_Par_Adr_QtnTableAdapter();

            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                var supRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    supRef = word;
                    break;
                }

                if (supRef.Length > 0)
                {
                    var dtSupAdr = taSupAdr.GetDataByQtnAdrRef(supRef);
                    if (dtSupAdr.Rows.Count > 0)
                    {
                        gvSup.DataSource = dtSupAdr;
                        Session["data"] = dtSupAdr;
                        SetSupplierGridData();
                        btnClearSrch.Visible = true;
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "No Data Found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";

            var taParAdr = new tbl_PuMa_Par_Adr_QtnTableAdapter();
            var dtPumaParAdr = taParAdr.GetData();
            Session["data"] = dtPumaParAdr;
            SetSupplierGridData();
            
            btnClearSrch.Visible = false;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var qryStr = "SELECT [Par_Adr_Qtn_Ref] as [Ref_No], [Par_Adr_Qtn_Ref_No] as [Code_No], [Par_Adr_Qtn_Name] as [Qtn_Supplier_Name], [SupTypeName] as [Type], " +
                             "[Par_Adr_Qtn_Addr] as [Address], [Par_Adr_Qtn_Cont_Per] as [CP_Name], [Par_Adr_Qtn_Desig] as [CP_Designation], [Par_Adr_Qtn_Cell_No] as [Cell_No], " +
                             "[Par_Adr_Qtn_Tel_No] as [Tel_No], [Par_Adr_Qtn_Fax_No] as [Fax_No], [Par_Adr_Qtn_Email_Id] as [Email_Id], [Par_Adr_Qtn_Acc_Code] as [Acc_GL_Code], " +
                             "[Par_Adr_Qtn_Status] as [Status] FROM [DRN].[dbo].[tbl_PuMa_Par_Adr_Qtn] left outer join [tbl_PuMa_Par_Adr_Type] on [Par_Adr_Qtn_Type]=[SupTypeRef]";

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
                string filename = String.Format("Quoted_Supplier_List_as_on_{0}.xls", DateTime.Now.ToString("dd-MM-yy"));
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