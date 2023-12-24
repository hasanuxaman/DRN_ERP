using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmVoucherRpt : System.Web.UI.Page
    {
        GlobalClass.clsDbCon dbCon = new GlobalClass.clsDbCon();

        string rptFile;
        string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //var taJvType = new View_Voucher_TypeTableAdapter();
            //var dtJvType = taJvType.GetData();
            //cboJvType.DataSource = dtJvType;
            //cboJvType.DataValueField = "Trn_Flag";
            //cboJvType.DataValueField = "Trn_Flag";
            //cboJvType.DataBind();
            //cboJvType.Items.Insert(0, new ListItem("---ALL---", "0"));

            var taJvType = new tbl_Acc_Fa_Jv_TypeTableAdapter();
            var dtJvType = taJvType.GetDataBySortName();
            cboJvType.DataSource = dtJvType;
            cboJvType.DataTextField = "JV_Type_Name";
            cboJvType.DataValueField = "JV_Type_Code";
            cboJvType.DataBind();
            cboJvType.Items.Insert(0, new ListItem("-----Select-----", "0"));
        }

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
            var dtFaTe = new dsAccTran.tbl_Acc_Fa_TeDataTable();

            var coaCode = "";
            var srchWords = txtSearch.Text.Trim().Split(':');
            foreach (string word in srchWords)
            {
                coaCode = word;
                break;
            }

            if (coaCode.Length > 0)
            {
                var dtCoa = taCoa.GetDataByCoaCode(coaCode);
                if (dtCoa.Rows.Count > 0)
                    coaCode = dtCoa[0].Gl_Coa_Code.ToString();
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "GL Code Not Found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }

            if (cboJvType.SelectedIndex == 0)
            {
                if (coaCode.Length > 0)
                    dtFaTe = taFaTe.GetDataByDateRangeAccCode(coaCode.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                else
                    dtFaTe = taFaTe.GetDataByDateRange(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
            }
            else
                if (coaCode.Length > 0)
                    dtFaTe = taFaTe.GetDataByDateRangeTrnTypeAccCode(cboJvType.SelectedValue.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim(), coaCode.ToString());
                else
                    dtFaTe = taFaTe.GetDataByDateRangeTrnType(cboJvType.SelectedValue.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());        

            gvPayDet.DataSource = dtFaTe;
            gvPayDet.DataBind();
            gvPayDet.SelectedIndex = -1;

            btnExport.Enabled = dtFaTe.Rows.Count > 0;
        }

        protected void lnkJvView_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            var lblTrnRefNo = ((Label)row.FindControl("lblTrnRefNo")).Text.Trim();
            var JvRefNoView = lblTrnRefNo.ToString();
            var url = "frmVoucherView.aspx?JvRefNoView=" + JvRefNoView.ToString();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void lnkJvEdit_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            var lblTrnRefNo = ((Label)row.FindControl("lblTrnRefNo")).Text.Trim();
            var JvRefNoEdit = lblTrnRefNo.ToString();
            var url = "frmVoucherEdit.aspx?JvRefNoEdit=" + JvRefNoEdit.ToString();            
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void gvPayDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hfJrnType = ((HiddenField)e.Row.FindControl("hfJrnType")).Value.Trim();
                var lnkJvEdit = ((LinkButton)e.Row.FindControl("lnkJvEdit"));

                var taJrnType = new tbl_Acc_Fa_Jv_TypeTableAdapter();
                var dtJrnType = taJrnType.GetDataByJvCode(hfJrnType);
                if (dtJrnType.Rows.Count > 0)
                {
                    if (dtJrnType[0].JV_Type_Entry_Type.ToString() == "M")
                    {
                        var EmpRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                        var taEditPerm = new tbl_Acc_User_PermTableAdapter();
                        var dtEditPerm = taEditPerm.GetDataByEmpRef(EmpRef.ToString());
                        if (dtEditPerm.Rows.Count > 0)
                        {
                            if (dtEditPerm[0].Acc_Perm_Edit_Perm == "Y")
                            {
                                lnkJvEdit.Text = "Edit";
                                lnkJvEdit.Enabled = true;
                            }
                            else
                            {
                                lnkJvEdit.Text = "N/A";
                                lnkJvEdit.Enabled = false;
                            }
                        }
                        else
                        {
                            lnkJvEdit.Text = "N/A";
                            lnkJvEdit.Enabled = false;
                        }
                    }
                    else
                    {
                        lnkJvEdit.Text = "N/A";
                        lnkJvEdit.Enabled = false;
                    }
                }
                else
                {
                    lnkJvEdit.Text = "N/A";             
                    lnkJvEdit.Enabled = false;
                }
            }
        }

        protected void gvPayDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPayDet.PageIndex = e.NewPageIndex;

            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
            var dtFaTe = new dsAccTran.tbl_Acc_Fa_TeDataTable();

            var coaCode = "";
            var srchWords = txtSearch.Text.Trim().Split(':');
            foreach (string word in srchWords)
            {
                coaCode = word;
                break;
            }

            if (coaCode.Length > 0)
            {
                var dtCoa = taCoa.GetDataByCoaCode(coaCode);
                if (dtCoa.Rows.Count > 0)
                    coaCode = dtCoa[0].Gl_Coa_Code.ToString();
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "GL Code Not Found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }

            if (cboJvType.SelectedIndex == 0)
            {
                if (coaCode.Length > 0)
                    dtFaTe = taFaTe.GetDataByDateRangeAccCode(coaCode.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                else
                    dtFaTe = taFaTe.GetDataByDateRange(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
            }
            else
                if (coaCode.Length > 0)
                    dtFaTe = taFaTe.GetDataByDateRangeTrnTypeAccCode(cboJvType.SelectedValue.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim(), coaCode.ToString());
                else
                    dtFaTe = taFaTe.GetDataByDateRangeTrnType(cboJvType.SelectedValue.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());

            gvPayDet.DataSource = dtFaTe;
            gvPayDet.DataBind();
            gvPayDet.SelectedIndex = -1;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                var dt = new dsAccTran.tbl_Acc_Fa_TeDataTable();

                var coaCode = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    coaCode = word;
                    break;
                }

                if (coaCode.Length > 0)
                {
                    var dtCoa = taCoa.GetDataByCoaCode(coaCode);
                    if (dtCoa.Rows.Count > 0)
                        coaCode = dtCoa[0].Gl_Coa_Code.ToString();
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "GL Code Not Found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }

                if (cboJvType.SelectedIndex == 0)
                {
                    if (coaCode.Length > 0)
                        dt = taFaTe.GetDataByDateRangeAccCode(coaCode.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                    else
                        dt = taFaTe.GetDataByDateRange(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                }
                else
                    if (coaCode.Length > 0)
                        dt = taFaTe.GetDataByDateRangeTrnTypeAccCode(cboJvType.SelectedValue.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim(), coaCode.ToString());
                    else
                        dt = taFaTe.GetDataByDateRangeTrnType(cboJvType.SelectedValue.ToString(), txtFromDate.Text.Trim(), txtToDate.Text.Trim());    

                if (dt.Rows.Count > 65535)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Export to Excel is not allowed due to excessive number of rows. (65535) ')", true);
                    return;
                }

                //First Method
                #region With Formating
                Response.Clear();
                Response.Buffer = true;
                string filename = String.Format("Voucher_List_{0}.xls", DateTime.Now.ToString("dd-MM-yy"));
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

        protected void lnkJvPrint_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            var lblTrnRefNo = ((Label)row.FindControl("lblTrnRefNo")).Text.Trim();
            var JvRefNoPrint = lblTrnRefNo.ToString();

            reportInfo(JvRefNoPrint);
            var url = "frmShowAccReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportInfo(string JrnRefNo)
        {
            if (JrnRefNo != "")
            {
                var JrnTypeName = "";
                var taJrnType = new tbl_Acc_Fa_Jv_TypeTableAdapter();
                var dtJrnType = taJrnType.GetDataByJvCode(JrnRefNo);
                JrnTypeName = dtJrnType.Rows.Count > 0 ? dtJrnType[0].JV_Type_Name.ToString() : "";

                rptFile = "~/Module/Accounts/Reports/rptVoucher.rpt";

                rptSelcFormula = "{tbl_Acc_Fa_Te.Trn_Ref_No}='" + JrnRefNo + "'";

                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
                Session["RptHdr"] = JrnTypeName;
            }
        }
    }
}