using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.SYS.DataSet;
using DRN_WEB_ERP.Module.SYS.DataSet.dsSysTableAdapters;
using DRN_WEB_ERP.Module.SYS.DataSet.dsAppTableAdapters;

namespace DRN_WEB_ERP
{
    public partial class frmModule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            SetModuleGridData();
            Save.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var taMod = new TBL_MODULETableAdapter();
            try
            {
                var dtMod = taMod.ChkDataExists(Convert.ToInt32(hfRefNo.Value));
                if (dtMod.Rows.Count > 0)
                {
                    taMod.EditModule(txtModuleCode.Text.Trim(), txtModuleName.Text.Trim(), 1, Convert.ToInt32(cboCompany.SelectedValue), cboCompany.SelectedItem.Text,
                        DateTime.Now, Session["sessionUserId"] == null ? "0:" : Session["sessionUserId"].ToString(), chkStatus.Checked ? 1 : 2, "",
                        Convert.ToInt32(hfRefNo.Value));
                }
                else
                {
                    var dtMaxModRefNo = taMod.GetMaxModRef();
                    Int32 nextModRefNo = dtMaxModRefNo == null ? 100001 : Convert.ToInt32(dtMaxModRefNo) + 1;

                    taMod.InsertModule(nextModRefNo, txtModuleCode.Text.Trim(), txtModuleName.Text.Trim(), 1, Convert.ToInt32(cboCompany.SelectedValue), "",
                        DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), DateTime.Now, "100001", chkStatus.Checked ? 1 : 2, "");
                }
            }
            catch (Exception ex)
            {

            }
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";
            SetModuleGridData();
            txtModuleCode.Text = "";
            txtModuleName.Text = "";
            Save.Text = "Save";
        }

        protected void SetModuleGridData()
        {
            var taMod = new TBL_MODULETableAdapter();
            var dtMod = taMod.GetData();
            gvModule.DataSource = dtMod;
            gvModule.DataBind();
        }

        public string GetUserName(int userRef)
        {
            string userName = "";
            try
            {
                var taUser = new TBL_USER_INFOTableAdapter();
                var dtUser = taUser.GetUserByRef(userRef);
                if (dtUser.Rows.Count > 0)
                    userName = dtUser[0].User_Name;
                return userName;
            }
            catch (Exception ex)
            {
                return userName;
            }
        }

        public string GetStatusName(int statusRef)
        {
            string statusName = "";
            try
            {
                var taStatus = new App_Glbl_StatusTableAdapter();
                var dtStatus = taStatus.GetStatusByRef(statusRef);
                if (dtStatus.Rows.Count > 0)
                    statusName = dtStatus[0].Status_Name;
                return statusName;
            }
            catch (Exception ex)
            {
                return statusName;
            }
        }

        public string GetCompanyName(int companyRef)
        {
            string companyName = "";
            try
            {
                var taCompany = new TBL_COMPANYTableAdapter();
                var dtCompany = taCompany.GetCompanyByRef(companyRef);
                if (dtCompany.Rows.Count > 0)
                    companyName = dtCompany[0].Company_Name;
                return companyName;
            }
            catch (Exception ex)
            {
                return companyName;
            }
        }

        protected void gvModule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvUom, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
                //e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex, true);
            }            
        }

        protected void gvModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvModule.SelectedIndex;

            if (indx != -1)
            {
                HiddenField hfModRef = (HiddenField)gvModule.Rows[indx].FindControl("hfModRef");
                hfRefNo.Value = hfModRef.Value;
                hfEditStatus.Value = "Y";
                
                HiddenField hfStatus = (HiddenField)gvModule.Rows[indx].FindControl("hfStatus");
                string statusId = hfStatus.Value;

                HiddenField hfCompany = (HiddenField)gvModule.Rows[indx].FindControl("hfCompany");
                string companyId = hfCompany.Value;

                try
                {
                    txtModuleCode.Text = gvModule.Rows[indx].Cells[1].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvModule.Rows[indx].Cells[1].Text.Trim();
                    txtModuleName.Text = gvModule.Rows[indx].Cells[2].Text.Trim() == "&nbsp;"
                                             ? ""
                                             : gvModule.Rows[indx].Cells[2].Text.Trim();
                    chkStatus.Checked = statusId == "1" ? true : false;
                    if (companyId.Trim() != "")
                        cboCompany.SelectedValue = companyId;                    
                    Save.Text = "Edit";
                }
                catch (Exception ex)
                {
                    hfEditStatus.Value = "N";
                    hfRefNo.Value = "0";
                    Save.Text = "Save";
                    //throw;
                }
            }
        }
    }
}