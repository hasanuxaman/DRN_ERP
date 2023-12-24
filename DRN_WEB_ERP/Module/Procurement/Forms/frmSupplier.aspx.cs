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
    public partial class frmSupplier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            if (empRef == "000011")
                btnDeleteDupplicate.Visible = true;

            var taSupAcc = new tbl_PuMa_Par_AccTableAdapter();
            var dtMaxAccRef = taSupAcc.GetMaxAccRef();
            var nextAccRef = dtMaxAccRef == null ? "10.100001" : "10." + (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
            txtSupRefNo.Text = "SUP-" + nextAccRef.ToString();

            var taCustType = new tbl_PuMa_Par_Adr_TypeTableAdapter();
            cboSupType.DataSource = taCustType.GetDataByAsc();
            cboSupType.DataTextField = "SupTypeName";
            cboSupType.DataValueField = "SupTypeRef";
            cboSupType.DataBind();
            cboSupType.Items.Insert(0, new ListItem("---Select---", "0"));

            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var dtMaxCoaCode = taCoa.GetMaxCoaCode();
            var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
            var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");
            txtSupAccGlCode.Text = nextCoaCode.ToString();

            btnSave.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";

            var taParAdr = new tbl_PuMa_Par_AdrTableAdapter();
            var dtParAdr = taParAdr.GetData();
            Session["data"] = dtParAdr;
            SetSupplierGridData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var taSupAcc = new tbl_PuMa_Par_AccTableAdapter();
            var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();
            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();

            var taQtnAdr = new tbl_PuMa_Par_Adr_QtnTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSupAcc.Connection);

            try
            {
                taSupAcc.AttachTransaction(myTran);
                taSupAdr.AttachTransaction(myTran);
                taCoa.AttachTransaction(myTran);

                taQtnAdr.AttachTransaction(myTran);        

                var supName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtSupName.Text.Trim());
                var supCpName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtSupCp.Text.Trim());

                if (hfEditStatus.Value == "N")
                {
                    #region Form Data Validation
                    var dtSup = taSupAdr.GetDataByAdrName(txtSupName.Text.Trim());
                    if (dtSup.Rows.Count > 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Supplier already exists with this name.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    #endregion

                    #region Insert Supplier
                    var dtMaxAccRef = taSupAcc.GetMaxAccRef();
                    //var nextAccRef = dtMaxAccRef == null ? 100001 : Convert.ToInt32(dtMaxAccRef) + 1;
                    var nextAccRef = dtMaxAccRef == null ? "10.100001" : "10." + (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                    var nextAccRefNo = "SUP-ACC-" + nextAccRef.ToString();

                    taSupAcc.InsertSupAcc(nextAccRef, nextAccRefNo, supName.Trim(), "", cboSupType.SelectedValue.ToString(), "", "", DateTime.Now, "N", DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", 0, 0, 0, 0, 0, 0, "", "BDT", "", "", "", "",
                        "", "", "", 0, "", "", "", "", "", "", "");

                    var dtMaxCoaRef = taCoa.GetMaxCoaRef();
                    var nextCoaRef = dtMaxCoaRef == null ? 1 : Convert.ToInt32(dtMaxCoaRef) + 1;

                    var dtMaxCoaCode = taCoa.GetMaxCoaCode();
                    var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
                    var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");

                    taCoa.InsertCoa(nextCoaRef, nextCoaCode, supName.Trim(), nextCoaCode, "S", "B", "N", DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y",
                        "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    var dtMaxAdrRef = taSupAdr.GetMaxAdrRef();
                    //var nextAdrRef = dtMaxAdrRef == null ? 100001 : Convert.ToInt32(dtMaxAdrRef) + 1;
                    var nextAdrRef = dtMaxAdrRef == null ? "10.100001" : "10." + (Convert.ToInt32(dtMaxAdrRef) + 1).ToString();
                    var nextAdrRefNo = "SUP-" + nextAdrRef.ToString();

                    taSupAdr.InsertSupAdr(nextAdrRef, nextAdrRefNo, supName.Trim(), Convert.ToInt32(cboSupType.SelectedValue), nextAccRef.ToString(), txtSupAdr.Text.Trim(),
                        supCpName.ToString(), txtSupCpDesig.Text.Trim(), txtSupCell.Text.Trim(), txtSupPhone.Text.Trim(), txtSupFax.Text.Trim(), txtSupEmail.Text.Trim(),
                        "", "", "", nextCoaCode, "", "", DateTime.Now, 0, 0, "", "", "", 0, "", "", "", "", "", DateTime.Now,
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
                    #region Update Supplier
                    var dtParAcc = taSupAcc.GetDataBySupAccRef(hfRefNo.Value);
                    if (dtParAcc.Rows.Count > 0)
                    {
                        taSupAcc.UpdateSupAcc(supName.Trim(), "", cboSupType.SelectedValue.ToString(), "", "", DateTime.Now, "N", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", 0, 0, 0, 0, 0, 0, "", "BDT", "", "", "", "",
                            "", "", "", 0, "", "", "", "", "", "", "", hfRefNo.Value);

                        var dtParAdr = taSupAdr.GetDataBySupAdrRef(hfRefNo.Value);
                        if (dtParAdr.Rows.Count > 0)
                        {
                            taSupAdr.UpdateSupAdr(supName.Trim(), Convert.ToInt32(cboSupType.SelectedValue), hfRefNo.Value.ToString(), txtSupAdr.Text.Trim(),
                                supCpName.ToString(), txtSupCpDesig.Text.Trim(), txtSupCell.Text.Trim(), txtSupPhone.Text.Trim(), txtSupFax.Text.Trim(), txtSupEmail.Text.Trim(),
                                "", "", "", dtParAdr[0].Par_Adr_Acc_Code.ToString(), "", "", 0, 0, "", "", "", 0, "", "", "", "", "", DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), optListSupStatus.SelectedValue, "", hfRefNo.Value);

                            var dtGlCoa = taCoa.GetDataByCoaCode(dtParAdr[0].Par_Adr_Acc_Code.ToString());
                            if (dtGlCoa.Rows.Count > 0)
                            {
                                taCoa.UpdateCoa(supName.Trim(), "S", "B", "N", DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y",
                                    "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "", dtParAdr[0].Par_Adr_Acc_Code.ToString());
                            }
                            else
                            {
                                myTran.Rollback();
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Supplier GL Code.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                            }
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
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Supplier Account Code.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                    #endregion
                }

                var taParAdr = new tbl_PuMa_Par_AdrTableAdapter();
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
            var taSupAcc = new tbl_PuMa_Par_AccTableAdapter();
            var dtMaxAccRef = taSupAcc.GetMaxAccRef();
            //var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
            var nextAccRef = dtMaxAccRef == null ? "10.100001" : "10." + (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
            txtSupRefNo.Text = "SUP-" + nextAccRef.ToString();

            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var dtMaxCoaCode = taCoa.GetMaxCoaCode();
            var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
            var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");
            txtSupAccGlCode.Text = nextCoaCode.ToString();

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

            var taParAdr = new tbl_PuMa_Par_AdrTableAdapter();
            var dtPumaParAdr = taParAdr.GetData();
            Session["data"] = dtPumaParAdr;
            SetSupplierGridData();            
        }

        protected void SetSupplierGridData()
        {
            //var taParAdr = new tbl_PuMa_Par_AdrTableAdapter();
            //var dtParAdr = taParAdr.GetData();
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
                var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();

                try
                {
                    var userRef = Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString();
                    var userEmpRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                    HiddenField hfCustRef = (HiddenField)gvSup.Rows[indx].FindControl("hfCustRef");
                    hfRefNo.Value = hfCustRef.Value;
                    hfEditStatus.Value = "Y";
                    btnSave.Text = "Edit";

                    var dtCustAdr = taSupAdr.GetDataBySupAdrRef(hfRefNo.Value.ToString());
                    if (dtCustAdr.Rows.Count > 0)
                    {
                        txtSupRefNo.Text = dtCustAdr[0].Par_Adr_Ref_No.ToString();
                        txtSupName.Text = dtCustAdr[0].Par_Adr_Name.ToString();
                        txtSupAdr.Text = dtCustAdr[0].Par_Adr_Addr.ToString();
                        txtSupCp.Text = dtCustAdr[0].Par_Adr_Cont_Per.ToString();
                        txtSupCpDesig.Text = dtCustAdr[0].Par_Adr_Desig.ToString();
                        txtSupCell.Text = dtCustAdr[0].Par_Adr_Cell_No.ToString();
                        txtSupPhone.Text = dtCustAdr[0].Par_Adr_Tel_No.ToString();
                        txtSupFax.Text = dtCustAdr[0].Par_Adr_Fax_No.ToString();
                        txtSupEmail.Text = dtCustAdr[0].Par_Adr_Email_Id.ToString();
                        txtSupAccGlCode.Text = dtCustAdr[0].Par_Adr_Acc_Code.ToString();

                        cboSupType.SelectedValue = dtCustAdr[0].Par_Adr_Type.ToString();

                        optListSupStatus.SelectedValue = dtCustAdr[0].Par_Adr_Status.ToString();

                        if (userEmpRef == "000011" || userEmpRef == "000068")//-------tapash acc                     
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
            var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();

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
                    var dtSupAdr = taSupAdr.GetDataBySupAdrRef(supRef);
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

            var taParAdr = new tbl_PuMa_Par_AdrTableAdapter();
            var dtPumaParAdr = taParAdr.GetData();
            Session["data"] = dtPumaParAdr;
            SetSupplierGridData();
            
            btnClearSrch.Visible = false;
        }

        protected void btnImpSup_Click(object sender, EventArgs e)
        {
            var taSupAcc = new tbl_PuMa_Par_AccTableAdapter();
            var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();
            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var taImp = new Accounts_PayableTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSupAcc.Connection);

            try
            {
                taSupAcc.AttachTransaction(myTran);
                taSupAdr.AttachTransaction(myTran);
                taCoa.AttachTransaction(myTran);
                taImp.AttachTransaction(myTran);

                var i = 0;
                var dtImp = taImp.GetDataBySort();
                foreach (dsProcMas.Accounts_PayableRow dr in dtImp.Rows)
                {
                    i++;
                    var dtSupAdr = taSupAdr.GetDataByAdrName(dr.Sup_Name);
                    if (dtSupAdr.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        #region Insert Supplier
                        var dtMaxAccRef = taSupAcc.GetMaxAccRef();
                        //var nextAccRef = dtMaxAccRef == null ? 100001 : Convert.ToInt32(dtMaxAccRef) + 1;
                        var nextAccRef = dtMaxAccRef == null ? "10.100001" : "10." + (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                        var nextAccRefNo = "SUP-ACC-" + nextAccRef.ToString();

                        taSupAcc.InsertSupAcc(nextAccRef, nextAccRefNo, dr.Sup_Name.ToString(), "", cboSupType.SelectedValue.ToString(), "", "", DateTime.Now, "N", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", 0, 0, 0, 0, 0, 0, "", "BDT", "", "", "", "",
                            "", "", "", 0, "", "", "", "", "", "", "");

                        var dtMaxCoaRef = taCoa.GetMaxCoaRef();
                        var nextCoaRef = dtMaxCoaRef == null ? 1 : Convert.ToInt32(dtMaxCoaRef) + 1;

                        var dtMaxCoaCode = taCoa.GetMaxCoaCode();
                        var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
                        var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");

                        taCoa.InsertCoa(nextCoaRef, nextCoaCode, dr.Sup_Name.ToString(), nextCoaCode, "S", "B", "N", DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y",
                            "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        var dtMaxAdrRef = taSupAdr.GetMaxAdrRef();
                        //var nextAdrRef = dtMaxAdrRef == null ? 100001 : Convert.ToInt32(dtMaxAdrRef) + 1;
                        var nextAdrRef = dtMaxAdrRef == null ? "10.100001" : "10." + (Convert.ToInt32(dtMaxAdrRef) + 1).ToString();
                        var nextAdrRefNo = "SUP-" + nextAdrRef.ToString();

                        taSupAdr.InsertSupAdr(nextAdrRef, nextAdrRefNo, dr.Sup_Name.ToString(), Convert.ToInt32(cboSupType.SelectedValue), nextAccRef.ToString(), txtSupAdr.Text.Trim(),
                            "", txtSupCpDesig.Text.Trim(), txtSupCell.Text.Trim(), txtSupPhone.Text.Trim(), txtSupFax.Text.Trim(), txtSupEmail.Text.Trim(),
                            "", "", "", nextCoaCode, "", "", DateTime.Now, 0, 0, "", "", "", 0, "", "", "", "", "", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), optListSupStatus.SelectedValue, "");                        
                        #endregion
                    }
                }
                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully. Total Count (" + i + ")";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var qryStr = "SELECT [Par_Adr_Ref] as [Ref_No], [Par_Adr_Ref_No] as [Code_No], [Par_Adr_Name] as [Supplier_Name], [SupTypeName] as [Type], " +
                             "[Par_Adr_Addr] as [Address], [Par_Adr_Cont_Per] as [CP_Name], [Par_Adr_Desig] as [CP_Designation], [Par_Adr_Cell_No] as [Cell_No], " +
                             "[Par_Adr_Tel_No] as [Tel_No], [Par_Adr_Fax_No] as [Fax_No], [Par_Adr_Email_Id] as [Email_Id], [Par_Adr_Acc_Code] as [Acc_GL_Code], " +
                             "[Par_Adr_Status] as [Status] FROM [DRN].[dbo].[tbl_PuMa_Par_Adr] left outer join [tbl_PuMa_Par_Adr_Type] on [Par_Adr_Type]=[SupTypeRef]";

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
                string filename = String.Format("Supplier_List_as_on_{0}.xls", DateTime.Now.ToString("dd-MM-yy"));
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

        protected void btnDeleteDupplicate_Click(object sender, EventArgs e)
        {
            try
            {
                var taDupplicateSupplierList = new View_Dupplicate_Supplier_ListTableAdapter();
                var dtDupplicateSupplierList = taDupplicateSupplierList.GetData();
                foreach (dsProcMas.View_Dupplicate_Supplier_ListRow drDupList in dtDupplicateSupplierList.Rows)
                {
                    var cnt = 1;
                    var mainSupRef = "";
                    var mainSupName = "";
                    var mainSupAcc = "";

                    var taSuppMas = new tbl_PuMa_Par_AdrTableAdapter();
                    var dtSuppMas = taSuppMas.GetDataByAdrName(drDupList.Par_Adr_Name.ToString());
                    foreach (dsProcMas.tbl_PuMa_Par_AdrRow drSup in dtSuppMas.Rows)
                    {
                        if (cnt == 1)
                        {
                            mainSupRef = drSup.Par_Adr_Ref.ToString();
                            mainSupName = drSup.Par_Adr_Name.ToString();
                            mainSupAcc = drSup.Par_Adr_Acc_Code.ToString();
                            cnt++;
                        }
                        else
                        {
                            //-----------------Update Accounts Journal
                            var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                            taFaTe.UpdateDupplicateGlAccCode(mainSupAcc.ToString(), drSup.Par_Adr_Acc_Code.ToString());

                            //-----------------Update Inventory
                            var taInTrHdr = new tbl_InTr_Trn_HdrTableAdapter();
                            taInTrHdr.UpdateTrnHdrPcodeDcodeAcode(mainSupRef.ToString(), mainSupRef.ToString(), mainSupAcc.ToString(), mainSupName.ToString(), drSup.Par_Adr_Ref.ToString());

                            //-----------------Update MRR
                            var taFaTeWd = new tbl_Acc_Fa_Te_Wd_MrrTableAdapter();
                            taFaTeWd.UpdateDupplicateAccCode(mainSupAcc.ToString(), drSup.Par_Adr_Acc_Code.ToString());

                            //-----------------Update PO
                            var taPoHdr = new tbl_PuTr_PO_HdrTableAdapter();
                            taPoHdr.UpdateDupplicateParAdrRef(mainSupRef.ToString(), mainSupRef.ToString(), mainSupRef.ToString(), drSup.Par_Adr_Ref.ToString());

                            //-----------------Update Quotation
                            var taQtnDet = new tbl_Qtn_DetTableAdapter();
                            taQtnDet.UpdateDupplicateParAdrRef(mainSupRef.ToString(), drSup.Par_Adr_Ref.ToString());

                            //-----------------Update MPR
                            var taPrHdr = new tbl_PuTr_Pr_HdrTableAdapter();
                            taPrHdr.UpdateDupplicateParAdrRef(mainSupRef.ToString(), mainSupRef.ToString(), mainSupRef.ToString(), drSup.Par_Adr_Ref.ToString());
                            var taPrDet = new tbl_PuTr_Pr_DetTableAdapter();
                            taPrDet.UpdateDupplicateParAdrRef(mainSupRef.ToString(), drSup.Par_Adr_Ref.ToString());

                            //-----------------Update COA
                            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                            taCoa.UpdateDupplicateName("Test Entry - " + drSup.Par_Adr_Ref.ToString(), drSup.Par_Adr_Acc_Code.ToString());

                            //-----------------Update Supplier
                            var taParAdr = new tbl_PuMa_Par_AdrTableAdapter();
                            taParAdr.UpdateDupplicateParAdrRef("Test Entry - " + drSup.Par_Adr_Ref.ToString(), drSup.Par_Adr_Ref.ToString());
                            var taParAcc = new tbl_PuMa_Par_AccTableAdapter();
                            taParAcc.UpdateDupplicateParAccRef("Test Entry - " + drSup.Par_Adr_Ref.ToString(), drSup.Par_Adr_Ref.ToString());
                        }
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
    }
}