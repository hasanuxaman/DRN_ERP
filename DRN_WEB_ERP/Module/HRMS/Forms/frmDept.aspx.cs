using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmDept : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taLoc = new tblHrmsOffLocTableAdapter();
            cboCompany.DataSource = taLoc.GetDataByAsc();
            cboCompany.DataValueField = "LocRefNo";
            cboCompany.DataTextField = "LocName";
            cboCompany.DataBind();
            cboCompany.Items.Insert(0, new ListItem("---Select---", "0"));

            var taDept = new tblHrmsDeptTableAdapter();
            var dtDept = taDept.GetDataByAsc();
            Session["data"] = dtDept;
            SetDepGridData();  
        }

        #region Get_Grid_Data
        public string GetCompanyName(string companyRef)
        {
            string companyName = "";
            try
            {
                var taCompany = new tblHrmsOffLocTableAdapter();
                var dtCompany = taCompany.GetDataByLocRef(Convert.ToInt32(companyRef));
                if (dtCompany.Rows.Count > 0)
                    companyName = dtCompany[0].LocName;
                return companyName;
            }
            catch (Exception ex)
            {
                return companyName;
            }
        }
        #endregion

        protected void btnSaveCom_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            
            DateTime? locOpnDt = null;
            if (txtDeptOpnDate.Text.Length > 0) locOpnDt = Convert.ToDateTime(txtDeptOpnDate.Text.Trim());

            var taDept = new tblHrmsDeptTableAdapter();
            try
            {
                var dtDept = taDept.GetDataByDeptRef(Convert.ToInt32(hfDeptRefNo.Value.ToString()));
                if (dtDept.Rows.Count > 0)
                {
                    taDept.UpdateDept(txtDeptCode.Text.Trim(), txtDeptName.Text.Trim(), "100001", cboCompany.SelectedValue.ToString(), txtDeptRem.Text.Trim(),
                        "", "", txtDeptPhone.Text.Trim(), txtDeptFax.Text.Trim(), txtDeptEmail.Text.Trim(), locOpnDt, DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "", Convert.ToInt32(hfDeptRefNo.Value));

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    var dtDeptExist = taDept.GetDataByDeptCodeName(cboCompany.SelectedValue.ToString(), txtDeptCode.Text.Trim(), txtDeptName.Text.Trim());
                    if (dtDeptExist.Rows.Count > 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Data Exists with the same Department Code or Name";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                    else
                    {
                        var dtMaxDeptRef = taDept.GetMaxDeptRefNo();
                        var nextDeptRef = dtMaxDeptRef == null ? 100001 : Convert.ToInt32(dtMaxDeptRef) + 1;

                        taDept.InsertDept(nextDeptRef, txtDeptCode.Text.Trim(), txtDeptName.Text.Trim(), "100001", cboCompany.SelectedValue.ToString(), txtDeptRem.Text.Trim(),
                            "", "", txtDeptPhone.Text.Trim(), txtDeptFax.Text.Trim(), txtDeptEmail.Text.Trim(), locOpnDt, DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                hfDeptRefNo.Value = "0";
                cboCompany.SelectedIndex = 0;
                txtDeptCode.Text = "";
                txtDeptName.Text = "";
                txtDeptPhone.Text = "";
                txtDeptFax.Text = "";
                txtDeptFax.Text = "";
                txtDeptEmail.Text = "";
                txtDeptOpnDate.Text = "";
                txtDeptRem.Text = "";
                
                var dtDeptNew = taDept.GetDataByAsc();
                Session["data"] = dtDeptNew;
                SetDepGridData();
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearComp_Click(object sender, EventArgs e)
        {
            hfDeptRefNo.Value = "0";
            cboCompany.SelectedIndex = 0;
            txtDeptCode.Text = "";
            txtDeptName.Text = "";
            txtDeptPhone.Text = "";
            txtDeptFax.Text = "";
            txtDeptFax.Text = "";
            txtDeptEmail.Text = "";
            txtDeptOpnDate.Text = "";
            txtDeptRem.Text = "";
            var taDept = new tblHrmsDeptTableAdapter();
            var dtDept = taDept.GetDataByAsc();
            Session["data"] = dtDept;
            SetDepGridData();  
        }

        protected void gvDept_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvDept.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    var hfCompRef = ((HiddenField)gvDept.Rows[indx].FindControl("hfCompRef")).Value.Trim().ToString();

                    hfDeptRefNo.Value = gvDept.Rows[indx].Cells[0].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvDept.Rows[indx].Cells[0].Text.Trim();
                    txtDeptCode.Text = gvDept.Rows[indx].Cells[1].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvDept.Rows[indx].Cells[1].Text.Trim();
                    txtDeptName.Text = gvDept.Rows[indx].Cells[2].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvDept.Rows[indx].Cells[2].Text.Trim();                    
                    txtDeptPhone.Text = gvDept.Rows[indx].Cells[4].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvDept.Rows[indx].Cells[4].Text.Trim();
                    txtDeptFax.Text = gvDept.Rows[indx].Cells[5].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvDept.Rows[indx].Cells[5].Text.Trim();
                    txtDeptEmail.Text = gvDept.Rows[indx].Cells[6].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvDept.Rows[indx].Cells[6].Text.Trim();
                    txtDeptOpnDate.Text = gvDept.Rows[indx].Cells[7].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvDept.Rows[indx].Cells[7].Text.Trim();
                    txtDeptRem.Text = gvDept.Rows[indx].Cells[8].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvDept.Rows[indx].Cells[8].Text.Trim();
                    cboCompany.SelectedValue = hfCompRef.ToString();
                }
                catch (Exception ex)
                {
                    hfDeptRefNo.Value = "0";
                }
            }
        }

        protected void gvDept_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var deptRef = gvDept.Rows[rowNum].Cells[0].Text.Trim() == "&nbsp;"
                                          ? "0"
                                          : gvDept.Rows[rowNum].Cells[0].Text.Trim();

            var taEmpOff = new tblHrmsEmpOfficeTableAdapter();
            var dtEmpOff = taEmpOff.GetDataByDeptRef(deptRef);
            if (dtEmpOff.Rows.Count > 0)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to delete this Department.";
                tblMsg.Rows[1].Cells[0].InnerText = "Department already used.";
                ModalPopupExtenderMsg.Show();
                return;
            }
            else
            {
                var taDept = new tblHrmsDeptTableAdapter();
                var dtDept = taDept.GetDataByDeptRef(Convert.ToInt32(deptRef));
                if (dtDept.Rows.Count > 0)
                {
                    taDept.DeleteDept(Convert.ToInt32(deptRef));
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Deleted Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }

                var dtDeptNew = taDept.GetDataByAsc();
                Session["data"] = dtDeptNew;
                SetDepGridData();
            }
        }

        protected void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taDept = new tblHrmsDeptTableAdapter();
            var dtDept = new dsHrmsMas.tblHrmsDeptDataTable();

            if (cboCompany.SelectedIndex == 0)
                dtDept = taDept.GetDataByAsc();
            else
                dtDept = taDept.GetDataByLocRef(cboCompany.SelectedValue.ToString());

            Session["data"] = dtDept;
            SetDepGridData();  
        }

        protected void gvDept_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDept.PageIndex = e.NewPageIndex;
            SetDepGridData();
        }

        protected void SetDepGridData()
        {
            var dtItem = Session["data"];
            gvDept.DataSource = dtItem;
            gvDept.DataBind();
            gvDept.SelectedIndex = -1;
        }
    }
}