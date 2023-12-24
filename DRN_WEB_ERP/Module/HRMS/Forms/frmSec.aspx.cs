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
    public partial class frmSec : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taLoc = new tblHrmsOffLocTableAdapter();
            cboLoc.DataSource = taLoc.GetDataByAsc();
            cboLoc.DataValueField = "LocRefNo";
            cboLoc.DataTextField = "LocName";
            cboLoc.DataBind();
            cboLoc.Items.Insert(0, new ListItem("---Select---", "0"));

            cboDept.Items.Insert(0, new ListItem("---Select---", "0"));
            cboDept.SelectedIndex = 0;

            var taSec = new tblHrmsSecTableAdapter();
            var dtSec = taSec.GetDataByAsc();
            Session["data"] = dtSec;
            SetSecGridData();
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

        public string GetDeptName(string deptRef)
        {
            string deptName = "";
            try
            {
                var taDept = new tblHrmsDeptTableAdapter();
                var dtDept = taDept.GetDataByDeptRef(Convert.ToInt32(deptRef));
                if (dtDept.Rows.Count > 0)
                    deptName = dtDept[0].DeptName;
                return deptName;
            }
            catch (Exception ex)
            {
                return deptName;
            }
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            
            DateTime? sectOpnDt = null;
            if (txtSectOpnDate.Text.Length > 0) sectOpnDt = Convert.ToDateTime(txtSectOpnDate.Text.Trim());

            var taSec = new tblHrmsSecTableAdapter();
            try
            {
                var dtSec = taSec.GetDataBySecRef(Convert.ToInt32(hfSectRefNo.Value.ToString()));
                if (dtSec.Rows.Count > 0)
                {
                    taSec.UpdateSec(txtSectCode.Text.Trim(), txtSectName.Text.Trim(), cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(),
                        txtSectRem.Text.Trim(), "", "", txtSectPhone.Text.Trim(), txtSectFax.Text.Trim(), txtSectEmail.Text.Trim(), sectOpnDt, DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "", Convert.ToInt32(hfSectRefNo.Value));

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    var dtDeptExist = taSec.GetDataBySecCodeName(cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(), txtSectCode.Text.Trim(), txtSectName.Text.Trim());
                    if (dtDeptExist.Rows.Count > 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Data Exists with the same Section Code or Name";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                    else
                    {
                        var dtMaxSecRef = taSec.GetMaxSecRefNo();
                        var nextSecRef = dtMaxSecRef == null ? 100001 : Convert.ToInt32(dtMaxSecRef) + 1;

                        taSec.InsertSec(nextSecRef, txtSectCode.Text.Trim(), txtSectName.Text.Trim(), cboLoc.SelectedValue.ToString(), cboDept.SelectedValue.ToString(),
                        txtSectRem.Text.Trim(), "", "", txtSectPhone.Text.Trim(), txtSectFax.Text.Trim(), txtSectEmail.Text.Trim(), sectOpnDt, DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "1", "");

                        tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }

                hfSectRefNo.Value = "0";
                cboLoc.SelectedIndex = 0;
                cboDept.Items.Clear();
                txtSectCode.Text = "";
                txtSectName.Text = "";
                txtSectPhone.Text = "";
                txtSectFax.Text = "";
                txtSectFax.Text = "";
                txtSectEmail.Text = "";
                txtSectOpnDate.Text = "";
                txtSectRem.Text = "";

                var dtSecNew = taSec.GetDataByAsc();
                Session["data"] = dtSecNew;
                SetSecGridData();                
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearSect_Click(object sender, EventArgs e)
        {
            hfSectRefNo.Value = "0";
            cboLoc.SelectedIndex = 0;
            cboDept.Items.Clear();
            cboDept.Items.Insert(0, new ListItem("---Select---", "0"));
            cboDept.SelectedIndex = 0;
            txtSectCode.Text = "";
            txtSectName.Text = "";
            txtSectPhone.Text = "";
            txtSectFax.Text = "";
            txtSectFax.Text = "";
            txtSectEmail.Text = "";
            txtSectOpnDate.Text = "";
            txtSectRem.Text = "";
            var taSec = new tblHrmsSecTableAdapter();            
            var dtSecNew = taSec.GetDataByAsc();
            Session["data"] = dtSecNew;
            SetSecGridData();         
        }

        protected void gvSec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvSec.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    var compRef = ((HiddenField)gvSec.Rows[indx].FindControl("hfCompRef")).Value.Trim().ToString();
                    var deptRef = ((HiddenField)gvSec.Rows[indx].FindControl("hfDeptRef")).Value.Trim().ToString();

                    hfSectRefNo.Value = gvSec.Rows[indx].Cells[0].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvSec.Rows[indx].Cells[0].Text.Trim();
                    txtSectCode.Text = gvSec.Rows[indx].Cells[1].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvSec.Rows[indx].Cells[1].Text.Trim();
                    txtSectName.Text = gvSec.Rows[indx].Cells[2].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvSec.Rows[indx].Cells[2].Text.Trim();                    
                    txtSectPhone.Text = gvSec.Rows[indx].Cells[5].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvSec.Rows[indx].Cells[5].Text.Trim();
                    txtSectFax.Text = gvSec.Rows[indx].Cells[6].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvSec.Rows[indx].Cells[6].Text.Trim();
                    txtSectEmail.Text = gvSec.Rows[indx].Cells[7].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvSec.Rows[indx].Cells[7].Text.Trim();
                    txtSectOpnDate.Text = gvSec.Rows[indx].Cells[8].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvSec.Rows[indx].Cells[8].Text.Trim();
                    txtSectRem.Text = gvSec.Rows[indx].Cells[9].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvSec.Rows[indx].Cells[9].Text.Trim();

                    cboLoc.SelectedValue = compRef.ToString();
                    var taDep = new tblHrmsDeptTableAdapter();
                    cboDept.DataSource = taDep.GetDataByLocRef(compRef.ToString());
                    cboDept.DataValueField = "DeptRefNo";
                    cboDept.DataTextField = "DeptName";
                    cboDept.DataBind();
                    cboDept.Items.Insert(0, new ListItem("---Select---", "0"));
                    cboDept.SelectedValue = deptRef.ToString();
                }
                catch (Exception ex)
                {
                    hfSectRefNo.Value = "0";
                }
            }
        }

        protected void cboLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboDept.Items.Clear();

            var taDep = new tblHrmsDeptTableAdapter();
            cboDept.DataSource = taDep.GetDataByLocRef(cboLoc.SelectedValue.ToString());
            cboDept.DataValueField = "DeptRefNo";
            cboDept.DataTextField = "DeptName";
            cboDept.DataBind();

            cboDept.Items.Insert(0, new ListItem("---Select---", "0"));
            cboDept.SelectedIndex = 0;

            var taSec = new tblHrmsSecTableAdapter();
            var dtSec = new dsHrmsMas.tblHrmsSecDataTable();
            if (cboLoc.SelectedIndex == 0)
                dtSec = taSec.GetDataByAsc();
            else
                dtSec = taSec.GetDataByLocRef(cboLoc.SelectedValue.ToString());
            Session["data"] = dtSec;
            SetSecGridData();            
        }

        protected void gvSec_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var secRef = gvSec.Rows[rowNum].Cells[0].Text.Trim() == "&nbsp;"
                                          ? "0"
                                          : gvSec.Rows[rowNum].Cells[0].Text.Trim();

            var taEmpOff = new tblHrmsEmpOfficeTableAdapter();
            var dtEmpOff = taEmpOff.GetDataByDeptRef(secRef);
            if (dtEmpOff.Rows.Count > 0)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to delete this Section.";
                tblMsg.Rows[1].Cells[0].InnerText = "Section already used.";
                ModalPopupExtenderMsg.Show();
                return;
            }
            else
            {
                var taSec = new tblHrmsSecTableAdapter();
                var dtSec = taSec.GetDataBySecRef(Convert.ToInt32(secRef));
                if (dtSec.Rows.Count > 0)
                {
                    taSec.DeleteSec(Convert.ToInt32(secRef));
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Deleted Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                
                var dtSecNew = taSec.GetDataByAsc();           
                Session["data"] = dtSec;
                SetSecGridData();                
            }
        }

        protected void cboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taSec = new tblHrmsSecTableAdapter();
            var dtSec = new dsHrmsMas.tblHrmsSecDataTable();
            if (cboDept.SelectedIndex == 0)
            {
                if (cboLoc.SelectedIndex == 0)
                    dtSec = taSec.GetDataByAsc();
                else
                    dtSec = taSec.GetDataByLocRef(cboLoc.SelectedValue.ToString());
            }
            else
                dtSec = taSec.GetDataByDeptRef(cboDept.SelectedValue.ToString());

            Session["data"] = dtSec;
            SetSecGridData();            
        }

        protected void gvSec_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSec.PageIndex = e.NewPageIndex;
            SetSecGridData();
        }

        protected void SetSecGridData()
        {
            var dtItem = Session["data"];
            gvSec.DataSource = dtItem;
            gvSec.DataBind();
            gvSec.SelectedIndex = -1;
        }
    }
}