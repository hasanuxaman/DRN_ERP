using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmGLMaster : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            if (empRef == "000011")
            {
                //btnUpdateGl.Visible = true;
                //btnUpdtTallyAllGlGrp.Visible = true;
                //btnUpdtGLCoaGrp.Visible = true;
                //btnUpdtGLGrpCode.Visible = true;
                //btnUpdtUnassignedGLGrpCode.Visible = true;
                //btnUpdtGLOverheadGrpCode.Visible = true;
            }

            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var dtMaxCoaCode = taCoa.GetMaxCoaCode();
            var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
            var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");
            txtGLCode.Text = nextCoaCode;

            var taGrpCode = new tbl_Acc_Grp_CodeTableAdapter();
            ddlFirstGrp.DataSource = taGrpCode.GetDataByGrpDef("A01");
            ddlFirstGrp.DataTextField = "Grp_Code_Name";
            ddlFirstGrp.DataValueField = "Grp_Code";
            ddlFirstGrp.DataBind();
            ddlFirstGrp.Items.Insert(0, new ListItem("-----Select-----", "0"));

            ddlSecondGrp.Items.Insert(0, new ListItem("-----Select-----", "0"));

            ddlThirdGrp.Items.Insert(0, new ListItem("-----Select-----", "0"));

            var taCoaType = new tbl_Acc_Fa_Gl_TypeTableAdapter();
            var dtCoaType = taCoaType.GetDataBySortAsc();
            ddlCoaType.DataSource = dtCoaType;
            ddlCoaType.DataTextField = "GL_Type_Name";
            ddlCoaType.DataValueField = "Gl_Type_Code";
            ddlCoaType.DataBind();
            ddlCoaType.Items.Insert(0, new ListItem("---Select---", "0"));

            ddlFirstGrpSrch.DataSource = taGrpCode.GetDataByGrpDef("A01");
            ddlFirstGrpSrch.DataTextField = "Grp_Code_Name";
            ddlFirstGrpSrch.DataValueField = "Grp_Code";
            ddlFirstGrpSrch.DataBind();
            ddlFirstGrpSrch.Items.Insert(0, new ListItem("-----Select-----", "0"));

            ddlSecondGrpSrch.Items.Insert(0, new ListItem("-----Select-----", "0"));

            ddlThirdGrpSrch.Items.Insert(0, new ListItem("-----Select-----", "0"));

            ddlCoaTypeSrch.DataSource = dtCoaType;
            ddlCoaTypeSrch.DataTextField = "GL_Type_Name";
            ddlCoaTypeSrch.DataValueField = "Gl_Type_Code";
            ddlCoaTypeSrch.DataBind();
            ddlCoaTypeSrch.Items.Insert(0, new ListItem("---ALL---", "0"));

            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";

            var taCoaGrp = new View_Acc_Fa_GL_Coa_GrpTableAdapter();
            var dtCoaGrp = taCoaGrp.GetDataBySortAsc();
            Session["data"] = dtCoaGrp;
            SetGLGridData();
        }

        private void SetGLGridData()
        {
            //var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            //var dtCoa = taCoa.GetData();
            var dtCoa = Session["data"];
            gvGLMas.DataSource = dtCoa;
            gvGLMas.DataBind();            
            gvGLMas.SelectedIndex = -1;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var taCoa = new View_Acc_Fa_GL_Coa_GrpTableAdapter();

            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
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
                    {
                        //gvGLMas.DataSource = dtCoa;
                        Session["data"] = dtCoa;
                        //gvGLMas.DataBind();
                        //gvGLMas.SelectedIndex = -1;
                        SetGLGridData();

                        txtSearch.Enabled = false;
                        btnSearch.Enabled = false;
                        btnClearSrch.Enabled = true;
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
            txtSearch.Enabled = true;
            btnSearch.Enabled = true;            
            btnClearSrch.Enabled = false;

            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";

            var taCoa = new View_Acc_Fa_GL_Coa_GrpTableAdapter();
            var dtCoa = taCoa.GetDataBySortAsc();
            Session["data"] = dtCoa;
            SetGLGridData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var taCoaGrp = new tbl_Acc_Gl_Coa_GrpTableAdapter();

            var dtCoa = new dsAccMas.tbl_Acc_Fa_Gl_CoaDataTable();
            var dtCoaGrp = new dsAccMas.tbl_Acc_Gl_Coa_GrpDataTable();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taCoa.Connection);

            try
            {
                taCoa.AttachTransaction(myTran);
                taCoaGrp.AttachTransaction(myTran);

                dtCoa = taCoa.GetDataByCoaCode(txtGLCode.Text.Trim());
                if (dtCoa.Rows.Count > 0)
                {
                    if (dtCoa[0].Gl_Coa_Type.ToString() == "P" || dtCoa[0].Gl_Coa_Type.ToString() == "S" || dtCoa[0].Gl_Coa_Type.ToString() == "I")
                    {
                        if (ddlCoaType.SelectedValue != dtCoa[0].Gl_Coa_Type.ToString())
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to change Customer/Supplier/Item type from here.";
                            tblMsg.Rows[1].Cells[0].InnerText = "Please go to Customer/Supplier/Item setup menu.";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                        else
                        {
                            #region Edit GL Data
                            var dtChkCoa = taCoa.GetDataByCoaCode(dtCoa[0].Gl_Coa_Code.ToString());
                            if (dtChkCoa.Rows.Count > 0)
                            {
                                taCoa.UpdateCoa(txtGLName.Text.Trim(), ddlCoaType.SelectedValue.ToString(), "B", "N", DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y",
                                    "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "", dtChkCoa[0].Gl_Coa_Code.ToString());

                                taCoaGrp.DeleteGlCoaGrp(dtCoa[0].Gl_Coa_Code.ToString());
                                taCoaGrp.InsertGlCoaGrp(dtCoa[0].Gl_Coa_Code.ToString(), "A01", ddlFirstGrp.SelectedValue.ToString());
                                taCoaGrp.InsertGlCoaGrp(dtCoa[0].Gl_Coa_Code.ToString(), "A02", ddlSecondGrp.SelectedValue.ToString());
                                taCoaGrp.InsertGlCoaGrp(dtCoa[0].Gl_Coa_Code.ToString(), "A03", ddlThirdGrp.SelectedValue.ToString());

                                myTran.Commit();
                                tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();

                                hfEditStatus.Value = "N";
                                hfRefNo.Value = "0";
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        #region Edit GL Data
                        taCoa.UpdateCoa(txtGLName.Text.Trim(), ddlCoaType.SelectedValue.ToString(), "B", "N", DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y",
                            "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "", dtCoa[0].Gl_Coa_Code.ToString());

                        taCoaGrp.DeleteGlCoaGrp(dtCoa[0].Gl_Coa_Code.ToString());
                        taCoaGrp.InsertGlCoaGrp(dtCoa[0].Gl_Coa_Code.ToString(), "A01", ddlFirstGrp.SelectedValue.ToString());
                        taCoaGrp.InsertGlCoaGrp(dtCoa[0].Gl_Coa_Code.ToString(), "A02", ddlSecondGrp.SelectedValue.ToString());
                        taCoaGrp.InsertGlCoaGrp(dtCoa[0].Gl_Coa_Code.ToString(), "A03", ddlThirdGrp.SelectedValue.ToString());

                        myTran.Commit();
                        tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();

                        hfEditStatus.Value = "N";
                        hfRefNo.Value = "0";
                        #endregion
                    }
                }
                else
                {
                    if (ddlCoaType.SelectedValue.ToString() == "P" || ddlCoaType.SelectedValue.ToString() == "S" || ddlCoaType.SelectedValue.ToString() == "I")
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to create Customer/Supplier/Item from here.";
                        tblMsg.Rows[1].Cells[0].InnerText = "Please go to Customer/Supplier/Item setup menu.";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    else
                    {
                        #region Insert GL Data
                        var dtChkCoaName = taCoa.GetDataByCoaNameType(txtGLName.Text, ddlCoaType.SelectedValue.ToString());
                        if (dtChkCoaName.Rows.Count > 0)
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "GL already exists with this name.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                        else
                        {
                            var dtMaxCoaRef = taCoa.GetMaxCoaRef();
                            var nextCoaRef = dtMaxCoaRef == null ? 1 : Convert.ToInt32(dtMaxCoaRef) + 1;

                            var dtMaxCoaCode = taCoa.GetMaxCoaCode();
                            var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
                            var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");

                            taCoa.InsertCoa(nextCoaRef, nextCoaCode, txtGLName.Text.Trim(), nextCoaCode, ddlCoaType.SelectedValue.ToString(), "B", "N",
                                DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y", "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                            taCoaGrp.InsertGlCoaGrp(nextCoaCode.ToString(), "A01", ddlFirstGrp.SelectedValue.ToString());
                            taCoaGrp.InsertGlCoaGrp(nextCoaCode.ToString(), "A02", ddlSecondGrp.SelectedValue.ToString());
                            taCoaGrp.InsertGlCoaGrp(nextCoaCode.ToString(), "A03", ddlThirdGrp.SelectedValue.ToString());

                            myTran.Commit();
                            tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();

                            hfEditStatus.Value = "N";
                            hfRefNo.Value = "0";
                        }
                        #endregion
                    }
                }


                //if (hfEditStatus.Value == "N")
                //{
                //    if (ddlCoaType.SelectedValue.ToString() == "P" || ddlCoaType.SelectedValue.ToString() == "S" || ddlCoaType.SelectedValue.ToString() == "I")
                //    {
                //        tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to create Customer/Supplier/Item from here.";
                //        tblMsg.Rows[1].Cells[0].InnerText = "Please go to Customer/Supplier/Item setup menu.";
                //        ModalPopupExtenderMsg.Show();
                //        return;
                //    }
                //    else
                //    {
                //        #region Insert GL Data
                //        var dtChkCoaName = taCoa.GetDataByCoaName(txtGLName.Text.Trim());
                //        if (dtChkCoaName.Rows.Count > 0)
                //        {
                //            tblMsg.Rows[0].Cells[0].InnerText = "GL already exists with this name.";
                //            tblMsg.Rows[1].Cells[0].InnerText = "";
                //            ModalPopupExtenderMsg.Show();
                //            return;
                //        }
                //        else
                //        {
                //            var dtMaxCoaRef = taCoa.GetMaxCoaRef();
                //            var nextCoaRef = dtMaxCoaRef == null ? 1 : Convert.ToInt32(dtMaxCoaRef) + 1;

                //            var dtMaxCoaCode = taCoa.GetMaxCoaCode();
                //            var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
                //            var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");

                //            taCoa.InsertCoa(nextCoaRef, nextCoaCode, txtGLName.Text.Trim(), nextCoaCode, ddlCoaType.SelectedValue.ToString(), "B", "N",
                //                DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y", "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                //                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                //            tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                //            tblMsg.Rows[1].Cells[0].InnerText = "";
                //            ModalPopupExtenderMsg.Show();

                //            hfEditStatus.Value = "N";
                //            hfRefNo.Value = "0";
                //        }
                //        #endregion
                //    }
                //}
                //else
                //{
                //    var dtCoa = taCoa.GetDataByCoaNameType(txtGLName.Text, ddlCoaType.SelectedValue.ToString());
                //    if (dtCoa.Rows.Count > 0)
                //    {
                //        tblMsg.Rows[0].Cells[0].InnerText = "GL Name already exists.";
                //        tblMsg.Rows[1].Cells[0].InnerText = "";
                //        ModalPopupExtenderMsg.Show();
                //        return;
                //    }
                //    else
                //    {
                //        #region Edit GL Data
                //        var dtChkCoa = taCoa.GetDataByCoaCode(hfRefNo.Value.ToString());
                //        if (dtChkCoa.Rows.Count > 0)
                //        {
                //            taCoa.UpdateCoa(txtGLName.Text.Trim(), ddlCoaType.SelectedValue.ToString(), "B", "N", DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y",
                //                "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                //                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "", dtChkCoa[0].Gl_Coa_Code.ToString());

                //            tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                //            tblMsg.Rows[1].Cells[0].InnerText = "";
                //            ModalPopupExtenderMsg.Show();

                //            hfEditStatus.Value = "N";
                //            hfRefNo.Value = "0";
                //        }
                //        #endregion
                //    }
                //}

                var dtMaxCoaCodeNew = taCoa.GetMaxCoaCode();
                var maxCoaCodeNew = dtMaxCoaCodeNew == null ? 1 : Convert.ToInt32(dtMaxCoaCodeNew) + 1;
                var nextCoaCodeNew = "01.001.001." + maxCoaCodeNew.ToString("0000");

                txtGLCode.Text = nextCoaCodeNew;
                txtGLName.Text = "";

                ddlFirstGrp.SelectedIndex = 0;
                ddlSecondGrp.SelectedIndex = 0;
                ddlThirdGrp.SelectedIndex = 0;
                
                ddlCoaType.SelectedIndex = 0;
                ddlCoaType.Enabled = true;

                var taViewCoaGrp = new View_Acc_Fa_GL_Coa_GrpTableAdapter();
                var dtViewCoaGrp = new dsAccMas.View_Acc_Fa_GL_Coa_GrpDataTable();
                if (ddlCoaTypeSrch.SelectedIndex == 0)
                {
                    dtViewCoaGrp = taViewCoaGrp.GetDataBySortAsc();
                    Session["data"] = dtViewCoaGrp;
                    SetGLGridData();
                }
                else
                {
                    dtViewCoaGrp = taViewCoaGrp.GetDataByCoaType(ddlCoaTypeSrch.SelectedValue.ToString());
                    Session["data"] = dtViewCoaGrp;
                    SetGLGridData();
                }
            }
            catch (Exception ex)
            {
                hfEditStatus.Value = "N";
                hfRefNo.Value = "0";

                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
            var dtMaxCoaCode = taCoa.GetMaxCoaCode();
            var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
            var nextCoaCode ="01.001.001." + maxCoaCode.ToString("0000");

            txtGLCode.Text = nextCoaCode;
            txtGLName.Text = "";

            ddlFirstGrp.SelectedIndex = 0;
            ddlSecondGrp.SelectedIndex = 0;
            ddlThirdGrp.SelectedIndex = 0;

            ddlCoaType.SelectedIndex = 0;
            ddlCoaType.Enabled = true;

            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";

            var taViewCoaGrp = new View_Acc_Fa_GL_Coa_GrpTableAdapter();
            var dtCoa = taViewCoaGrp.GetDataBySortAsc();
            Session["data"] = dtCoa;
            SetGLGridData();
        }

        protected void gvGLMas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvGLMas.SelectedIndex;

            if (indx != -1)
            {                                
                var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();
                var taCoaGrp = new tbl_Acc_Gl_Coa_GrpTableAdapter();
                var taCoaGrpCode = new tbl_Acc_Grp_CodeTableAdapter();

                try
                {
                    Label lblGlCode = (Label)gvGLMas.Rows[indx].FindControl("lblCoaCode");

                    var dtCoa = taCoa.GetDataByCoaCode(lblGlCode.Text.ToString());
                    if (dtCoa.Rows.Count > 0)
                    {
                        txtGLCode.Text = dtCoa[0].Gl_Coa_Code.ToString();
                        txtGLName.Text = dtCoa[0].Gl_Coa_Name.ToString();

                        ddlCoaType.SelectedIndex = ddlCoaType.Items.IndexOf(ddlCoaType.Items.FindByValue(dtCoa[0].Gl_Coa_Type));
                        if (dtCoa[0].Gl_Coa_Type == "P" || dtCoa[0].Gl_Coa_Type == "S" || dtCoa[0].Gl_Coa_Type == "I")
                        {
                            txtGLName.Enabled = false;
                            ddlCoaType.Enabled = false;
                        }
                        else
                        {
                            txtGLName.Enabled = true;
                            ddlCoaType.Enabled = true;
                        }

                        var dtCoaGrp = taCoaGrp.GetDataByCoaCode(dtCoa[0].Gl_Coa_Code.ToString());
                        foreach (dsAccMas.tbl_Acc_Gl_Coa_GrpRow dr in dtCoaGrp.Rows)
                        {
                            if (dr.Coa_Grp_Def.ToString() == "A01")
                            {
                                ddlFirstGrp.SelectedIndex = ddlFirstGrp.Items.IndexOf(ddlFirstGrp.Items.FindByValue(dr.Coa_Grp_Code.ToString()));
                                
                                ddlSecondGrp.DataSource = taCoaGrpCode.GetDataByGrpSet1(dr.Coa_Grp_Code.ToString());
                                ddlSecondGrp.DataTextField = "Grp_Code_Name";
                                ddlSecondGrp.DataValueField = "Grp_Code";
                                ddlSecondGrp.DataBind();
                                ddlSecondGrp.Items.Insert(0, new ListItem("-----Select-----", "0"));
                            }

                            if (dr.Coa_Grp_Def.ToString() == "A02")
                            {
                                ddlSecondGrp.SelectedIndex = ddlSecondGrp.Items.IndexOf(ddlSecondGrp.Items.FindByValue(dr.Coa_Grp_Code.ToString()));

                                ddlThirdGrp.DataSource = taCoaGrpCode.GetDataByGrpSet2(dr.Coa_Grp_Code.ToString());
                                ddlThirdGrp.DataTextField = "Grp_Code_Name";
                                ddlThirdGrp.DataValueField = "Grp_Code";
                                ddlThirdGrp.DataBind();
                                ddlThirdGrp.Items.Insert(0, new ListItem("-----Select-----", "0"));
                            }

                            if (dr.Coa_Grp_Def.ToString() == "A03")
                                ddlThirdGrp.SelectedIndex = ddlThirdGrp.Items.IndexOf(ddlThirdGrp.Items.FindByValue(dr.Coa_Grp_Code.ToString()));
                        }

                        hfRefNo.Value = lblGlCode.Text;
                        hfEditStatus.Value = "Y";
                    }
                }
                catch (Exception ex)
                {
                    hfEditStatus.Value = "N";
                    hfRefNo.Value = "0";

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void gvGLMas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvGLMas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGLMas.PageIndex = e.NewPageIndex;
            SetGLGridData();
        }

        protected void gvGLMas_Sorting(object sender, GridViewSortEventArgs e)
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
                gvGLMas.DataSource = dataView;
                gvGLMas.DataBind();
            }
        }

        #region GridData
        public string GetGlType(string typeCode)
        {            
            string typeName = "";
            try
            {
                var taCoaType = new tbl_Acc_Fa_Gl_TypeTableAdapter();
                var dtCoaType = taCoaType.GetDataByType(typeCode.ToString());
                if (dtCoaType.Rows.Count > 0)
                    typeName = dtCoaType[0].GL_Type_Name.ToString();
                return typeName;
            }
            catch (Exception) { return typeName; }
        }
        #endregion

        protected void ddlCoaTypeSrch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taGlCoa = new View_Acc_Fa_GL_Coa_GrpTableAdapter();

            try
            {
                if (ddlCoaTypeSrch.SelectedIndex != 0)
                {
                    ddlFirstGrpSrch.SelectedIndex = 0;
                    ddlSecondGrpSrch.SelectedIndex = 0;
                    ddlThirdGrpSrch.SelectedIndex = 0;

                    var dtCoa = taGlCoa.GetDataByCoaType(ddlCoaTypeSrch.SelectedValue.ToString());
                    Session["data"] = dtCoa;
                    SetGLGridData();
                }
                else
                {
                    var dtCoa = taGlCoa.GetDataBySortAsc();
                    Session["data"] = dtCoa;
                    SetGLGridData();
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnUpdateGl_Click(object sender, EventArgs e)
        {
            var taCoa = new tbl_Acc_Fa_Gl_CoaTableAdapter();

            var taTallyGl = new GL_TALLY_NEWTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taCoa.Connection);

            try
            {
                taCoa.AttachTransaction(myTran);
                taTallyGl.AttachTransaction(myTran);

                var i = 0;
                var dtTallyGl = taTallyGl.GetDataByAllExceptSupplierAndErpExistsData();
                foreach (dsAccMas.GL_TALLY_NEWRow dr in dtTallyGl.Rows)
                {
                    var dtChkCoaName = taCoa.GetDataByCoaName(dr.GL_Desc.Trim());
                    if (dtChkCoaName.Rows.Count > 0)
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "GL already exists with this name.";
                        tblMsg.Rows[1].Cells[0].InnerText = dtChkCoaName[0].Gl_Coa_Name.ToString();
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    else
                    {
                        var dtMaxCoaRef = taCoa.GetMaxCoaRef();
                        var nextCoaRef = dtMaxCoaRef == null ? 1 : Convert.ToInt32(dtMaxCoaRef) + 1;

                        var dtMaxCoaCode = taCoa.GetMaxCoaCode();
                        var maxCoaCode = dtMaxCoaCode == null ? 1 : Convert.ToInt32(dtMaxCoaCode) + 1;
                        var nextCoaCode = "01.001.001." + maxCoaCode.ToString("0000");

                        taCoa.InsertCoa(nextCoaRef, nextCoaCode, dr.GL_Desc.Trim(), nextCoaCode, "O", "B", "N",
                            DateTime.Now, "N", "N", "BDT", DateTime.Now, "Y", "Product-01", "N", "", "N", "Y", "Y", "D", "N", "0", "T", 0, "", "", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        taTallyGl.UpdateErpUpdateStatus(dr.GL_Desc.Trim());

                        i++;
                    }                    
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully. Total Row :" + i.ToString();
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

        protected void btnUpdtTallyAllGlGrp_Click(object sender, EventArgs e)
        {
            var taGrpCode = new tbl_Acc_Grp_CodeTableAdapter();
            var taTallyAllGlExt = new TALLY_ALL_GL_CLOSE_BAL_31_10_19_ExtTableAdapter();
            var taTallySupGlExt = new Tally_Accounts_Payable_31_10_19_ExtTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taGrpCode.Connection);

            try
            {
                taGrpCode.AttachTransaction(myTran);
                taTallyAllGlExt.AttachTransaction(myTran);
                taTallySupGlExt.AttachTransaction(myTran);

                var Grp1Cnt = 0;
                var dtAllGlGrp1 = taTallyAllGlExt.GetDataBySubGroup1();
                foreach (dsAccTran.TALLY_ALL_GL_CLOSE_BAL_31_10_19_ExtRow dr in dtAllGlGrp1.Rows)
                {
                    var dtMaxGrpCodeRef = taGrpCode.GetMaxGrpCodeRef();
                    var maxGrpCodeRef = dtMaxGrpCodeRef == null ? 1 : Convert.ToInt32(dtMaxGrpCodeRef) + 1;
                    var nextGrpCodeRefNo = "G" + maxGrpCodeRef.ToString("0000");

                    taGrpCode.InsertGroupCode(nextGrpCodeRefNo, nextGrpCodeRefNo, dr.Mother_Group.Trim(), nextGrpCodeRefNo, "A01", DateTime.Now, "100001", "", "", "", "1", "");

                    taTallyAllGlExt.UpdateGrpCode1(nextGrpCodeRefNo, dr.Mother_Group.ToString());

                    Grp1Cnt++;
                }

                var Grp2Cnt = 0;
                var dtAllGlGrp2 = taTallyAllGlExt.GetDataBySubGroup2();
                foreach (dsAccTran.TALLY_ALL_GL_CLOSE_BAL_31_10_19_ExtRow dr in dtAllGlGrp2.Rows)
                {
                    var dtMaxGrpCodeRef = taGrpCode.GetMaxGrpCodeRef();
                    var maxGrpCodeRef = dtMaxGrpCodeRef == null ? 1 : Convert.ToInt32(dtMaxGrpCodeRef) + 1;
                    var nextGrpCodeRefNo = "G" + maxGrpCodeRef.ToString("0000");

                    taGrpCode.InsertGroupCode(nextGrpCodeRefNo, nextGrpCodeRefNo, dr.Sub_Group.Trim(), nextGrpCodeRefNo, "A02", DateTime.Now, "100001", "", "", "", "1", "");

                    taTallyAllGlExt.UpdateGrpCode2(nextGrpCodeRefNo, dr.Sub_Group.ToString());

                    Grp2Cnt++;
                }

                var Grp3Cnt = 0;
                var dtAllGlGrp3 = taTallyAllGlExt.GetDataBySubGroup3();
                foreach (dsAccTran.TALLY_ALL_GL_CLOSE_BAL_31_10_19_ExtRow dr in dtAllGlGrp3.Rows)
                {
                    var dtMaxGrpCodeRef = taGrpCode.GetMaxGrpCodeRef();
                    var maxGrpCodeRef = dtMaxGrpCodeRef == null ? 1 : Convert.ToInt32(dtMaxGrpCodeRef) + 1;
                    var nextGrpCodeRefNo = "G" + maxGrpCodeRef.ToString("0000");

                    taGrpCode.InsertGroupCode(nextGrpCodeRefNo, nextGrpCodeRefNo, dr.Sub_Group1.Trim(), nextGrpCodeRefNo, "A03", DateTime.Now, "100001", "", "", "", "1", "");

                    taTallyAllGlExt.UpdateGrpCode3(nextGrpCodeRefNo, dr.Sub_Group1.ToString());

                    Grp3Cnt++;
                }

                var SupGrp1Cnt = 0;
                var dtSupGlGrp1 = taTallySupGlExt.GetDataBySubGroup1();
                foreach (dsAccTran.Tally_Accounts_Payable_31_10_19_ExtRow dr in dtSupGlGrp1.Rows)
                {
                    var dtMaxGrpCodeRef = taGrpCode.GetMaxGrpCodeRef();
                    var maxGrpCodeRef = dtMaxGrpCodeRef == null ? 1 : Convert.ToInt32(dtMaxGrpCodeRef) + 1;
                    var nextGrpCodeRefNo = "G" + maxGrpCodeRef.ToString("0000");

                    taGrpCode.InsertGroupCode(nextGrpCodeRefNo, nextGrpCodeRefNo, dr.Mother_Group.Trim(), nextGrpCodeRefNo, "A01", DateTime.Now, "100001", "", "", "", "1", "");

                    taTallySupGlExt.UpdateGrpCode1(nextGrpCodeRefNo, dr.Mother_Group.ToString());

                    SupGrp1Cnt++;
                }

                var SupGrp2Cnt = 0;
                var dtSupGlGrp2 = taTallySupGlExt.GetDataBySubGroup2();
                foreach (dsAccTran.Tally_Accounts_Payable_31_10_19_ExtRow dr in dtSupGlGrp2.Rows)
                {
                    var dtMaxGrpCodeRef = taGrpCode.GetMaxGrpCodeRef();
                    var maxGrpCodeRef = dtMaxGrpCodeRef == null ? 1 : Convert.ToInt32(dtMaxGrpCodeRef) + 1;
                    var nextGrpCodeRefNo = "G" + maxGrpCodeRef.ToString("0000");

                    taGrpCode.InsertGroupCode(nextGrpCodeRefNo, nextGrpCodeRefNo, dr.Sub_Group.Trim(), nextGrpCodeRefNo, "A02", DateTime.Now, "100001", "", "", "", "1", "");

                    taTallySupGlExt.UpdateGrpCode2(nextGrpCodeRefNo, dr.Sub_Group.ToString());

                    SupGrp2Cnt++;
                }

                var SupGrp3Cnt = 0;
                var dtSupGlGrp3 = taTallySupGlExt.GetDataBySubGroup3();
                foreach (dsAccTran.Tally_Accounts_Payable_31_10_19_ExtRow dr in dtSupGlGrp3.Rows)
                {
                    var dtMaxGrpCodeRef = taGrpCode.GetMaxGrpCodeRef();
                    var maxGrpCodeRef = dtMaxGrpCodeRef == null ? 1 : Convert.ToInt32(dtMaxGrpCodeRef) + 1;
                    var nextGrpCodeRefNo = "G" + maxGrpCodeRef.ToString("0000");

                    taGrpCode.InsertGroupCode(nextGrpCodeRefNo, nextGrpCodeRefNo, dr.Sub_Group1.Trim(), nextGrpCodeRefNo, "A03", DateTime.Now, "100001", "", "", "", "1", "");

                    taTallySupGlExt.UpdateGrpCode3(nextGrpCodeRefNo, dr.Sub_Group1.ToString());

                    SupGrp3Cnt++;
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            catch(Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnUpdtGLCoaGrp_Click(object sender, EventArgs e)
        {
            var taFaGlCoaGrp = new tbl_Acc_Fa_Gl_Coa_GrpTableAdapter();
            var taGlCoaGrp = new tbl_Acc_Gl_Coa_GrpTableAdapter();
            var taTallyAllGlExt = new TALLY_ALL_GL_CLOSE_BAL_31_10_19_ExtTableAdapter();
            var taTallySupGlExt = new Tally_Accounts_Payable_31_10_19_ExtTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taFaGlCoaGrp.Connection);

            try
            {
                taFaGlCoaGrp.AttachTransaction(myTran);
                taGlCoaGrp.AttachTransaction(myTran);
                taTallyAllGlExt.AttachTransaction(myTran);
                taTallySupGlExt.AttachTransaction(myTran);

                var allGlCnt = 0;
                var dtAllGlGrp = taTallyAllGlExt.GetDataByErpCoaCode();
                foreach (dsAccTran.TALLY_ALL_GL_CLOSE_BAL_31_10_19_ExtRow dr in dtAllGlGrp.Rows)
                {
                    taFaGlCoaGrp.InsertFaGlCoaGrp(dr.ERP_GL_Code.ToString(), dr.GRP_CODE1.ToString(), dr.GRP_CODE2.ToString(), dr.GRP_CODE3.ToString(), "", "", "");

                    taGlCoaGrp.InsertGlCoaGrp(dr.ERP_GL_Code.ToString(), dr.GRP_DEF1.ToString(), dr.GRP_CODE1.ToString());
                    taGlCoaGrp.InsertGlCoaGrp(dr.ERP_GL_Code.ToString(), dr.GRP_DEF2.ToString(), dr.GRP_CODE2.ToString());
                    taGlCoaGrp.InsertGlCoaGrp(dr.ERP_GL_Code.ToString(), dr.GRP_DEF3.ToString(), dr.GRP_CODE3.ToString());

                    allGlCnt++;
                }

                var allSupCnt = 0;
                var dtSupGlGrp = taTallySupGlExt.GetDataByErpCoaCode();
                foreach (dsAccTran.Tally_Accounts_Payable_31_10_19_ExtRow dr in dtSupGlGrp.Rows)
                {
                    taFaGlCoaGrp.InsertFaGlCoaGrp(dr.ERP_GL_Code.ToString(), dr.GRP_CODE1.ToString(), dr.GRP_CODE2.ToString(), dr.GRP_CODE3.ToString(), "", "", "");

                    taGlCoaGrp.InsertGlCoaGrp(dr.ERP_GL_Code.ToString(), dr.GRP_DEF1.ToString(), dr.GRP_CODE1.ToString());
                    taGlCoaGrp.InsertGlCoaGrp(dr.ERP_GL_Code.ToString(), dr.GRP_DEF2.ToString(), dr.GRP_CODE2.ToString());
                    taGlCoaGrp.InsertGlCoaGrp(dr.ERP_GL_Code.ToString(), dr.GRP_DEF3.ToString(), dr.GRP_CODE3.ToString());

                    allSupCnt++;
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "Total GL:" + allGlCnt.ToString() + ", Total Sup:" + allSupCnt.ToString();
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            CoaGrpReportInfo();
            var url = "frmShowAccReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void CoaGrpReportInfo()
        {
            try
            {
                rptFile = "~/Module/Accounts/Reports/rptCoaGrpList.rpt";

                rptSelcFormula = "";

                Session["RptDateFrom"] = DateTime.Now.ToString();
                Session["RptDateTo"] = DateTime.Now.ToString();
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
                Session["RptHdr"] = "";
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnUpdtGLGrpCode_Click(object sender, EventArgs e)
        {
            var taFaGlCoaGrpCode = new tbl_Acc_Grp_CodeTableAdapter();
            var taTallyAllGlExt = new TALLY_ALL_GL_CLOSE_BAL_31_10_19_ExtTableAdapter();
            var taTallySupGlExt = new Tally_Accounts_Payable_31_10_19_ExtTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taFaGlCoaGrpCode.Connection);

            try
            {
                taFaGlCoaGrpCode.AttachTransaction(myTran);
                taTallyAllGlExt.AttachTransaction(myTran);
                taTallySupGlExt.AttachTransaction(myTran);

                var allGlCnt = 0;
                var dtAllGlGrp = taTallyAllGlExt.GetDataByErpCoaCode();
                foreach (dsAccTran.TALLY_ALL_GL_CLOSE_BAL_31_10_19_ExtRow dr in dtAllGlGrp.Rows)
                {
                    taFaGlCoaGrpCode.UpdateGrpCode(dr.GRP_CODE1, "", "", dr.GRP_CODE1);
                    taFaGlCoaGrpCode.UpdateGrpCode(dr.GRP_CODE1, dr.GRP_CODE2, "", dr.GRP_CODE2);
                    taFaGlCoaGrpCode.UpdateGrpCode(dr.GRP_CODE1, dr.GRP_CODE2, dr.GRP_CODE3, dr.GRP_CODE3);

                    allGlCnt++;
                }

                var allSupCnt = 0;
                var dtSupGlGrp = taTallySupGlExt.GetDataByErpCoaCode();
                foreach (dsAccTran.Tally_Accounts_Payable_31_10_19_ExtRow dr in dtSupGlGrp.Rows)
                {
                    taFaGlCoaGrpCode.UpdateGrpCode(dr.GRP_CODE1, "", "", dr.GRP_CODE1);
                    taFaGlCoaGrpCode.UpdateGrpCode(dr.GRP_CODE1, dr.GRP_CODE2, "", dr.GRP_CODE2);
                    taFaGlCoaGrpCode.UpdateGrpCode(dr.GRP_CODE1, dr.GRP_CODE2, dr.GRP_CODE3, dr.GRP_CODE3);

                    allSupCnt++;
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "Total GL:" + allGlCnt.ToString() + ", Total Sup:" + allSupCnt.ToString();
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

        protected void btnUpdtUnassignedGLGrpCode_Click(object sender, EventArgs e)
        {
            var taGlCoaGrp = new tbl_Acc_Gl_Coa_GrpTableAdapter();
            var taTempView = new Temp_View_Acc_Fa_Gl_Coa_Unassigned_GrpTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taGlCoaGrp.Connection);

            try
            {
                taGlCoaGrp.AttachTransaction(myTran);
                taTempView.AttachTransaction(myTran);

                var allGlCnt = 0;
                var dtAllGlGrp = taTempView.GetData();
                foreach (dsAccTran.Temp_View_Acc_Fa_Gl_Coa_Unassigned_GrpRow dr in dtAllGlGrp.Rows)
                {
                    if (dr.Gl_Coa_Type.ToString() == "P")
                    {
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A01", "G0088");
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A02", "G0089");
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A03", "G0090");
                    }

                    if (dr.Gl_Coa_Type.ToString() == "S")
                    {
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A01", "G0052");
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A02", "G0065");
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A03", "G0078");
                    }

                    if (dr.Gl_Coa_Type.ToString() == "O")
                    {
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A01", "G0082");
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A02", "G0083");
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A03", "G0084");
                    }

                    if (dr.Gl_Coa_Type.ToString() == "B")
                    {
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A01", "G0091");
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A02", "G0092");
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A03", "G0093");
                    }

                    if (dr.Gl_Coa_Type.ToString() == "I")
                    {
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A01", "G0085");
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A02", "G0086");
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A03", "G0087");
                    }

                    if (dr.Gl_Coa_Type.ToString() == "T")
                    {
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A01", "G0082");
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A02", "G0083");
                        taGlCoaGrp.InsertGlCoaGrp(dr.Gl_Coa_Code.ToString(), "A03", "G0084");
                    }

                    allGlCnt++;
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "Total GL:" + allGlCnt.ToString() + ", Total Sup:" + allGlCnt.ToString();
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

        protected void btnGlGrpTree_Click(object sender, EventArgs e)
        {
            var url = "frmGlGrpTree.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void ddlFirstGrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taCoaGrpCode = new tbl_Acc_Grp_CodeTableAdapter();

            //if (ddlFirstGrp.SelectedIndex != 0)
            //{
                ddlSecondGrp.Items.Clear();
                ddlSecondGrp.DataSource = taCoaGrpCode.GetDataByGrpSet1(ddlFirstGrp.SelectedValue.ToString());
                ddlSecondGrp.DataTextField = "Grp_Code_Name";
                ddlSecondGrp.DataValueField = "Grp_Code";
                ddlSecondGrp.DataBind();
                ddlSecondGrp.Items.Insert(0, new ListItem("-----Select-----", "0"));

                ddlThirdGrp.Items.Clear();
                ddlThirdGrp.Items.Insert(0, new ListItem("-----Select-----", "0"));
            //}
        }

        protected void ddlSecondGrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taCoaGrpCode = new tbl_Acc_Grp_CodeTableAdapter();

            //if (ddlFirstGrp.SelectedIndex != 0)
            //{
                ddlThirdGrp.Items.Clear();
                ddlThirdGrp.DataSource = taCoaGrpCode.GetDataByGrpSet2(ddlSecondGrp.SelectedValue.ToString());
                ddlThirdGrp.DataTextField = "Grp_Code_Name";
                ddlThirdGrp.DataValueField = "Grp_Code";
                ddlThirdGrp.DataBind();
                ddlThirdGrp.Items.Insert(0, new ListItem("-----Select-----", "0"));
            //}
        }

        protected void ddlFirstGrpSrch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taCoaGrpCode = new tbl_Acc_Grp_CodeTableAdapter();

            //if (ddlFirstGrp.SelectedIndex != 0)
            //{
            ddlSecondGrpSrch.Items.Clear();
            ddlSecondGrpSrch.DataSource = taCoaGrpCode.GetDataByGrpSet1(ddlFirstGrpSrch.SelectedValue.ToString());
            ddlSecondGrpSrch.DataTextField = "Grp_Code_Name";
            ddlSecondGrpSrch.DataValueField = "Grp_Code";
            ddlSecondGrpSrch.DataBind();
            ddlSecondGrpSrch.Items.Insert(0, new ListItem("-----Select-----", "0"));

            ddlThirdGrpSrch.Items.Clear();
            ddlThirdGrpSrch.Items.Insert(0, new ListItem("-----Select-----", "0"));

            ddlCoaTypeSrch.SelectedIndex = 0;
            //}

            GetFilterData();
        }

        protected void ddlSecondGrpSrch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taCoaGrpCode = new tbl_Acc_Grp_CodeTableAdapter();

            //if (ddlFirstGrp.SelectedIndex != 0)
            //{
            ddlThirdGrpSrch.Items.Clear();
            ddlThirdGrpSrch.DataSource = taCoaGrpCode.GetDataByGrpSet2(ddlSecondGrpSrch.SelectedValue.ToString());
            ddlThirdGrpSrch.DataTextField = "Grp_Code_Name";
            ddlThirdGrpSrch.DataValueField = "Grp_Code";
            ddlThirdGrpSrch.DataBind();
            ddlThirdGrpSrch.Items.Insert(0, new ListItem("-----Select-----", "0"));

            ddlCoaTypeSrch.SelectedIndex = 0;
            //}

            GetFilterData();
        }

        protected void ddlThirdGrpSrch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCoaTypeSrch.SelectedIndex = 0;

            GetFilterData();
        }

        private void GetFilterData()
        {
            var taGlCoa = new View_Acc_Fa_GL_Coa_GrpTableAdapter();
            var dtGlCoa = new dsAccMas.View_Acc_Fa_GL_Coa_GrpDataTable();
            try
            {
                if (ddlThirdGrpSrch.SelectedIndex != 0)                
                    dtGlCoa = taGlCoa.GetDataByFilterFirstSecondThirdGrp(ddlFirstGrpSrch.SelectedValue.ToString(), ddlSecondGrpSrch.SelectedValue.ToString(), ddlThirdGrpSrch.SelectedValue.ToString());                
                else
                {
                    if (ddlSecondGrpSrch.SelectedIndex != 0)                    
                        dtGlCoa = taGlCoa.GetDataByFilterFirstSecondGrp(ddlFirstGrpSrch.SelectedValue.ToString(), ddlSecondGrpSrch.SelectedValue.ToString());                    
                    else
                    {
                        if (ddlFirstGrpSrch.SelectedIndex != 0)                        
                            dtGlCoa = taGlCoa.GetDataByFilterFirstGrp(ddlFirstGrpSrch.SelectedValue.ToString());                        
                    }
                }
                Session["data"] = dtGlCoa;
                SetGLGridData();
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnUpdtGLOverheadGrpCode_Click(object sender, EventArgs e)
        {
            var taGrpCode = new tbl_Acc_Grp_CodeTableAdapter();
            var taTallyGlOverheadExt = new Tally_Overhead_GL_List_ExtTableAdapter();
            var taGlCoaGrp = new tbl_Acc_Gl_Coa_GrpTableAdapter();
            var taFaGlCoaGrp = new tbl_Acc_Fa_Gl_Coa_GrpTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taGrpCode.Connection);

            try
            {
                taGrpCode.AttachTransaction(myTran);
                taTallyGlOverheadExt.AttachTransaction(myTran);
                taGlCoaGrp.AttachTransaction(myTran);
                taFaGlCoaGrp.AttachTransaction(myTran);

                var dtAllGlGrp1 = taTallyGlOverheadExt.GetDataBySubGroup1();
                foreach (dsAccTran.Tally_Overhead_GL_List_ExtRow dr in dtAllGlGrp1.Rows)
                {
                    var dtMaxGrpCodeRef = taGrpCode.GetMaxGrpCodeRef();
                    var maxGrpCodeRef = dtMaxGrpCodeRef == null ? 1 : Convert.ToInt32(dtMaxGrpCodeRef) + 1;
                    var nextGrpCodeRefNo = "G" + maxGrpCodeRef.ToString("0000");

                    taGrpCode.InsertGroupCode(nextGrpCodeRefNo, nextGrpCodeRefNo, dr.Mother_Group.Trim(), nextGrpCodeRefNo, "A01", DateTime.Now, "100001", nextGrpCodeRefNo, "", "", "1", "");

                    taTallyGlOverheadExt.UpdateGrpCode1(nextGrpCodeRefNo, dr.Mother_Group.ToString());
                }

                var dtAllGlGrp2 = taTallyGlOverheadExt.GetDataBySubGroup2();
                foreach (dsAccTran.Tally_Overhead_GL_List_ExtRow dr in dtAllGlGrp2.Rows)
                {
                    var dtMaxGrpCodeRef = taGrpCode.GetMaxGrpCodeRef();
                    var maxGrpCodeRef = dtMaxGrpCodeRef == null ? 1 : Convert.ToInt32(dtMaxGrpCodeRef) + 1;
                    var nextGrpCodeRefNo = "G" + maxGrpCodeRef.ToString("0000");

                    taGrpCode.InsertGroupCode(nextGrpCodeRefNo, nextGrpCodeRefNo, dr.Sub_Group.Trim(), nextGrpCodeRefNo, "A02", DateTime.Now, "100001", "", nextGrpCodeRefNo, "", "1", "");

                    taTallyGlOverheadExt.UpdateGrpCode2(nextGrpCodeRefNo, dr.Sub_Group.ToString());
                }

                var dtAllGlGrp3 = taTallyGlOverheadExt.GetDataBySubGroup3();
                foreach (dsAccTran.Tally_Overhead_GL_List_ExtRow dr in dtAllGlGrp3.Rows)
                {
                    var dtMaxGrpCodeRef = taGrpCode.GetMaxGrpCodeRef();
                    var maxGrpCodeRef = dtMaxGrpCodeRef == null ? 1 : Convert.ToInt32(dtMaxGrpCodeRef) + 1;
                    var nextGrpCodeRefNo = "G" + maxGrpCodeRef.ToString("0000");

                    taGrpCode.InsertGroupCode(nextGrpCodeRefNo, nextGrpCodeRefNo, dr.Sub_Group1.Trim(), nextGrpCodeRefNo, "A03", DateTime.Now, "100001", "", "", nextGrpCodeRefNo, "1", "");

                    taTallyGlOverheadExt.UpdateGrpCode3(nextGrpCodeRefNo, dr.Sub_Group1.ToString());
                }

                var dtSupGlGrp = taTallyGlOverheadExt.GetDataByErpCoaCode();
                foreach (dsAccTran.Tally_Overhead_GL_List_ExtRow dr in dtSupGlGrp.Rows)
                {
                    taGrpCode.UpdateGrpCode(dr.GRP_CODE1, "", "", dr.GRP_CODE1);
                    taGrpCode.UpdateGrpCode(dr.GRP_CODE1, dr.GRP_CODE2, "", dr.GRP_CODE2);
                    taGrpCode.UpdateGrpCode(dr.GRP_CODE1, dr.GRP_CODE2, dr.GRP_CODE3, dr.GRP_CODE3);

                    taFaGlCoaGrp.InsertFaGlCoaGrp(dr.ERP_GL_Code.ToString(), dr.GRP_CODE1.ToString(), dr.GRP_CODE2.ToString(), dr.GRP_CODE3.ToString(), "", "", "");

                    taGlCoaGrp.InsertGlCoaGrp(dr.ERP_GL_Code.ToString(), dr.GRP_DEF1.ToString(), dr.GRP_CODE1.ToString());
                    taGlCoaGrp.InsertGlCoaGrp(dr.ERP_GL_Code.ToString(), dr.GRP_DEF2.ToString(), dr.GRP_CODE2.ToString());
                    taGlCoaGrp.InsertGlCoaGrp(dr.ERP_GL_Code.ToString(), dr.GRP_DEF3.ToString(), dr.GRP_CODE3.ToString());                    
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
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
            if (gvGLMas.Rows.Count > 65535)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Export to Excel is not allowed due to excessive number of rows. (65535) ')", true);
                return;
            }

            #region With Formating
            Response.Clear();
            Response.Buffer = true;
            string filename = String.Format("Chart_Of_Accounts_{0}.xls",DateTime.Now.ToString("dd_MM_yyyy"));
            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvGLMas.AllowPaging = false;
                gvGLMas.AllowSorting = false;
                SetGLGridData();

                gvGLMas.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in gvGLMas.HeaderRow.Cells)
                {
                    cell.BackColor = gvGLMas.HeaderStyle.BackColor;
                }

                foreach (GridViewRow row in gvGLMas.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
                    row.Height = 18;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvGLMas.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvGLMas.RowStyle.BackColor;
                        }
                        cell.Wrap = false;
                        cell.CssClass = "textmode";
                    }
                }

                gvGLMas.RenderControl(hw);

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
    }
}