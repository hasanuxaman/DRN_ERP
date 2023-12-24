using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.SYS.DataSet;
using DRN_WEB_ERP.Module.SYS.DataSet.dsSysTableAdapters;
using DRN_WEB_ERP.Module.SYS.DataSet.dsAppTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP
{
    public partial class frmCreateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            gvUser.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");            

            var taEmpList = new View_Emp_BascTableAdapter();
            var dtEmpList = taEmpList.GetDataByAsc();
            foreach (dsEmpDet.View_Emp_BascRow dr in dtEmpList.Rows)
            {
                cboEmpId.Items.Add(new ListItem(dr.EmpName + " >>>" + dr.DesigName + " >>>" + dr.DeptName + " >>>" + dr.EmpId, dr.EmpRefNo));
            }
            cboEmpId.Items.Insert(0, new ListItem("---Select---", "0"));

            //var taUser = new TBL_USER_INFOTableAdapter();
            //var dtUser = taUser.GetDataAsc();
            //ddlSupervisor.DataSource = dtUser;
            //ddlSupervisor.DataTextField = "User_Name";
            //ddlSupervisor.DataValueField = "User_Ref_No";
            //ddlSupervisor.DataBind();
            foreach (dsEmpDet.View_Emp_BascRow dr in dtEmpList.Rows)
            {
                ddlSupervisor.Items.Add(new ListItem(dr.EmpName + " >>>" + dr.DesigName + " >>>" + dr.DeptName + " >>>" + dr.EmpId, dr.EmpRefNo));
            }
            ddlSupervisor.Items.Insert(0, new ListItem("---Select---", "0"));

            Save.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";
            hfEmpRef.Value = "0";
            txtPass.Attributes.Add("value", "");
            txtConfPass.Attributes.Add("value", "");

            var taUser = new TBL_USER_INFOTableAdapter();
            var dtUser = taUser.GetDataByAscCode();
            Session["data"] = dtUser;
            SetUserGridData();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            var taUser = new TBL_USER_INFOTableAdapter();

            short stat = (short)(chkStatus.Checked ? 1 : 2);

            try
            {
                if (hfEditStatus.Value == "Y")
                {
                    taUser.EditUser(txtUserCode.Text.Trim().ToUpper(), txtUserName.Text.Trim().ToUpper(), Convert.ToInt32(cboCompany.SelectedValue),
                        txtPass.Text.Trim(), txtDesig.Text.Trim(), txtDept.Text.Trim(), Convert.ToInt32(ddlUserGroup.SelectedValue), "", ddlSupervisor.SelectedValue.ToString(),
                        Convert.ToInt32(ddlUserLevel.SelectedValue), cboEmpId.SelectedValue, "", "", "", "", txtEmail.Text.Trim(), txtContact.Text.Trim(),
                        DateTime.Now, null, 0, null, (short)(chkStatus.Checked ? 1 : 2), "", Convert.ToInt32(hfRefNo.Value));
                }
                else
                {
                    var dtMaxUserRef = taUser.GetMaxUserRef();
                    Int32 nextUserRef = dtMaxUserRef == null ? 100001 : Convert.ToInt32(dtMaxUserRef) + 1;

                    taUser.InsertUser(nextUserRef, txtUserCode.Text.Trim().ToUpper(), txtUserName.Text.Trim().ToUpper(), Convert.ToInt32(cboCompany.SelectedValue),
                        txtPass.Text.Trim(), txtDesig.Text.Trim(), txtDept.Text.Trim(), Convert.ToInt32(ddlUserGroup.SelectedValue), "", ddlSupervisor.SelectedValue.ToString(),
                        Convert.ToInt32(ddlUserLevel.SelectedValue), cboEmpId.SelectedValue, "", "", "", "", txtEmail.Text.Trim(), txtContact.Text.Trim(),
                        DateTime.Now, DateTime.Now, null, 0, null, (short)(chkStatus.Checked ? 1 : 2), "");
                }
                //ddlSupervisor.DataSource = taUser.GetData();
                //ddlSupervisor.DataTextField = "User_Name";
                //ddlSupervisor.DataValueField = "User_Ref_No";
                //ddlSupervisor.DataBind();
                //ddlSupervisor.Items.Insert(0, "---Select---");
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();                
            }
            finally
            {
                hfEditStatus.Value = "N";
                hfRefNo.Value = "0";
                hfEmpRef.Value = "0";                
                txtUserCode.Text = "";
                txtUserName.Text = "";
                txtEmail.Text = "";
                txtContact.Text = "";
                txtDept.Text = "";
                txtDesig.Text = "";
                txtSearchUser.Text = "";
                txtSrchEmp.Text = "";
                txtPass.Text = "";
                txtConfPass.Text = "";
                txtPass.Attributes.Add("value", "");
                txtConfPass.Attributes.Add("value", "");
                Save.Text = "Save";
                ddlSupervisor.SelectedIndex = 0;
                ddlUserLevel.SelectedIndex = 0;
                cboEmpId.SelectedIndex = 0;

                hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
                imgEmp.ImageUrl = "~/Image/NoImage.gif";
                
                var dtUser = taUser.GetDataByAscCode();
                Session["data"] = dtUser;
                SetUserGridData();
            }
        }

        protected void SetUserGridData()
        {
            //var taUser = new TBL_USER_INFOTableAdapter();
            //var dtUser = taUser.GetDataByAscCode();
            var dtUser = Session["data"];
            gvUser.DataSource = dtUser;            
            gvUser.DataBind();
            gvUser.SelectedIndex = -1;
        }

        #region Grid Data
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

        public string GetSupervisorName(int supervisorRef)
        {
            string supervisorName = "";
            try
            {
                //var taSupervisor = new TBL_USER_INFOTableAdapter();
                //var dtSupervisor = taSupervisor.GetUserByRef(supervisorRef);

                var taSupervisor = new View_Emp_BascTableAdapter();
                var dtSupervisor = taSupervisor.GetDataByEmpRef(Convert.ToInt32(supervisorRef));
                if (dtSupervisor.Rows.Count > 0)
                    supervisorName = dtSupervisor[0].EmpName;
                return supervisorName;
            }
            catch (Exception ex)
            {
                return supervisorName;
            }
        }

        public string GetUserGroupName(int userGrpRef)
        {
            string supervisorName = "";
            try
            {
                var taUserGrp = new TBL_USER_GROUPTableAdapter();
                var dtUserGrp = taUserGrp.GetUserGrpByRef(userGrpRef);
                if (dtUserGrp.Rows.Count > 0)
                    supervisorName = dtUserGrp[0].IsUser_Grp_NameNull() ? "" : dtUserGrp[0].User_Grp_Name;
                return supervisorName;
            }
            catch (Exception ex)
            {
                return supervisorName;
            }
        }
        #endregion

        protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvUser.SelectedIndex;

            if (indx != -1)
            {
                HiddenField hfUserRef = (HiddenField)gvUser.Rows[indx].FindControl("hfUserRef");
                hfRefNo.Value = hfUserRef.Value;
                hfEditStatus.Value = "Y";

                HiddenField hfCompany = (HiddenField)gvUser.Rows[indx].FindControl("hfCompany");
                string companyId = hfCompany.Value;

                HiddenField hfEmpRef = (HiddenField)gvUser.Rows[indx].FindControl("hfEmpRef");
                string empRef = hfEmpRef.Value;

                HiddenField hfSupervisor = (HiddenField)gvUser.Rows[indx].FindControl("hfSupervisorRef");
                string supervisorId = hfSupervisor.Value;

                HiddenField hfUserGrp = (HiddenField)gvUser.Rows[indx].FindControl("hfUserGrp");
                string userGrpId = hfUserGrp.Value;

                HiddenField hfDept = (HiddenField)gvUser.Rows[indx].FindControl("hfDept");
                string userDept = hfDept.Value;

                HiddenField hfDesig = (HiddenField)gvUser.Rows[indx].FindControl("hfDesig");
                string userDesig = hfDesig.Value;

                HiddenField hfPass = (HiddenField)gvUser.Rows[indx].FindControl("hfPass");
                string userPass = hfPass.Value;

                HiddenField hfStatus = (HiddenField)gvUser.Rows[indx].FindControl("hfStatus");
                string statusId = hfStatus.Value;

                try
                {
                    txtUserCode.Text = gvUser.Rows[indx].Cells[1].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvUser.Rows[indx].Cells[1].Text.Trim();
                    txtUserName.Text = gvUser.Rows[indx].Cells[2].Text.Trim() == "&nbsp;"
                                             ? ""
                                             : gvUser.Rows[indx].Cells[2].Text.Trim();
                    txtEmail.Text = gvUser.Rows[indx].Cells[6].Text.Trim() == "&nbsp;"
                                             ? ""
                                             : gvUser.Rows[indx].Cells[6].Text.Trim();
                    txtContact.Text = gvUser.Rows[indx].Cells[7].Text.Trim() == "&nbsp;"
                                             ? ""
                                             : gvUser.Rows[indx].Cells[7].Text.Trim();
                    txtDept.Text = userDept.ToString();

                    txtDesig.Text = userDesig.ToString();

                    txtPass.Attributes.Add("value", userPass);
                    txtConfPass.Attributes.Add("value", userPass);

                    chkStatus.Checked = statusId == "1" ? true : false;

                    Save.Text = "Edit";                    
                    
                    ddlUserLevel.SelectedValue = gvUser.Rows[indx].Cells[5].Text.Trim() == "0"
                                             ? ""
                                             : gvUser.Rows[indx].Cells[5].Text.Trim();

                    cboCompany.SelectedIndex = cboCompany.Items.IndexOf(cboCompany.Items.FindByValue(companyId.ToString() == "" ? "0" : companyId));

                    cboEmpId.SelectedIndex = cboEmpId.Items.IndexOf(cboEmpId.Items.FindByValue(hfEmpRef.Value.Trim() == "" ? "0" : empRef));

                    ddlSupervisor.SelectedIndex = ddlSupervisor.Items.IndexOf(ddlSupervisor.Items.FindByValue(supervisorId.ToString() == "" ? "0" : supervisorId));

                    ddlUserGroup.SelectedIndex = ddlUserGroup.Items.IndexOf(ddlUserGroup.Items.FindByValue(userGrpId.ToString() == "" ? "0" : userGrpId));

                    hlEmpPic.NavigateUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + cboEmpId.SelectedValue + "'";
                    imgEmp.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + cboEmpId.SelectedValue + "'";
                }
                catch (Exception ex)
                {
                    //hfEditStatus.Value = "N";
                    //hfRefNo.Value = "0";
                    //Save.Text = "Save";
                }
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (hfEditStatus.Value == "N")
                {
                    var taUser = new TBL_USER_INFOTableAdapter();
                    var dtUser = taUser.GetUserByCode(txtUserCode.Text.Trim());
                    if (dtUser.Rows.Count > 0)
                        args.IsValid = false;
                    else
                        args.IsValid = true;
                }
                else
                {
                    args.IsValid = true;
                }
            }
            catch (Exception ex)
            {
                args.IsValid = false;
            }            
        }             

        protected void btnSearchEmp_Click(object sender, EventArgs e)
        {
            try
            {
                var empRef = "";
                var srchWords = txtSrchEmp.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    empRef = word;
                    break;
                }

                if (empRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(empRef, out result))
                    {
                        var taUser = new TBL_USER_INFOTableAdapter();
                        var dtUser = taUser.GetDataByEmpRef(empRef.ToString());
                        if (dtUser.Rows.Count > 0)
                        {
                            btnClearEmp.Visible = true;

                            hfRefNo.Value = dtUser[0].User_Ref_No.ToString();
                            hfEmpRef.Value = dtUser[0].User_Ext_Data1.ToString();
                            hfEditStatus.Value = "Y";
                            Save.Text = "Edit";

                            txtUserCode.Text = dtUser[0].User_Code.ToString();
                            txtUserName.Text = dtUser[0].User_Name.ToString();                            
                            txtDept.Text = dtUser[0].Uer_Dept.ToString();
                            txtDesig.Text = dtUser[0].User_Desig.ToString();                            
                            txtContact.Text = dtUser[0].User_Contact.ToString();
                            txtEmail.Text = dtUser[0].User_Email.ToString();
                            
                            txtPass.Attributes.Add("value", dtUser[0].User_Pass.ToString());
                            txtConfPass.Attributes.Add("value", dtUser[0].User_Pass.ToString());
                            chkStatus.Checked = dtUser[0].User_Status.ToString() == "2" ? false : true;

                            //cboCompany.SelectedValue = dtUser[0].User_Comp_Ref.ToString();
                            //cboEmpId.SelectedValue = dtUser[0].User_Ext_Data1.ToString();
                            //ddlSupervisor.SelectedValue = dtUser[0].User_Supervisor.ToString();
                            //ddlUserGroup.SelectedValue = dtUser[0].User_Group.ToString();
                            //ddlUserLevel.SelectedValue = dtUser[0].User_Level.ToString();

                            cboCompany.SelectedIndex = cboCompany.Items.IndexOf(cboCompany.Items.FindByValue(dtUser[0].User_Comp_Ref.ToString().Length <= 0 ? "0" : dtUser[0].User_Comp_Ref.ToString()));

                            cboEmpId.SelectedIndex = cboEmpId.Items.IndexOf(cboEmpId.Items.FindByValue(dtUser[0].IsUser_Ext_Data1Null() ? "0" : dtUser[0].User_Ext_Data1.ToString()));

                            ddlSupervisor.SelectedIndex = ddlSupervisor.Items.IndexOf(ddlSupervisor.Items.FindByValue(dtUser[0].IsUser_SupervisorNull() ? "0" : dtUser[0].User_Supervisor.ToString()));

                            ddlUserGroup.SelectedIndex = ddlUserGroup.Items.IndexOf(ddlUserGroup.Items.FindByValue(dtUser[0].IsUser_GroupNull() ? "0" : dtUser[0].User_Group.ToString()));

                            ddlUserLevel.SelectedIndex = ddlUserLevel.Items.IndexOf(ddlUserLevel.Items.FindByValue(dtUser[0].IsUser_LevelNull() ? "0" : dtUser[0].User_Level.ToString()));

                            hlEmpPic.NavigateUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRef + "'";
                            imgEmp.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRef + "'";

                            tblMsg.Rows[0].Cells[0].InnerText = "User already created for this employee.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                        else
                        {
                            #region GetEmpDetails
                            var taEmp = new View_Emp_BascTableAdapter();
                            var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                            if (dtEmp.Rows.Count > 0)
                            {
                                txtUserName.Text = dtEmp[0].EmpName.ToString();
                                txtDesig.Text = dtEmp[0].DesigName.ToString();
                                txtDept.Text = dtEmp[0].DeptName.ToString();
                                txtEmail.Text = dtEmp[0].EmpOffEmail.ToString();
                                txtContact.Text = dtEmp[0].EmpCellPhone.ToString();

                                cboEmpId.SelectedValue = dtEmp[0].EmpRefNo.ToString();
                                ddlSupervisor.SelectedValue = dtEmp[0].EmpSuprId.ToString();

                                //var dtEmpSup = taEmp.GetDataByEmpRef(Convert.ToInt32(dtEmp[0].EmpSuprId));
                                //ddlSupervisor.Text = dtEmpSup.Rows.Count > 0 ? dtEmpSup[0].EmpName.ToString() + " (" + dtEmpSup[0].DesigName + ")" : "";

                                hlEmpPic.NavigateUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + dtEmp[0].EmpRefNo + "'";
                                imgEmp.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + dtEmp[0].EmpRefNo + "'";
                            }
                            else
                            {
                                hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
                                imgEmp.ImageUrl = "~/Image/NoImage.gif";

                                txtUserName.Text = "";
                                txtDesig.Text = "";
                                txtDept.Text = "";
                                txtEmail.Text = "";
                                txtContact.Text = "";
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }                
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void btnSearchUser_Click(object sender, EventArgs e)
        {
            var taItem = new TBL_USER_INFOTableAdapter();

            if (txtSearchUser.Text.Trim().Length <= 0) return;
            try
            {
                var UserRef = "";
                var srchWords = txtSearchUser.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    UserRef = word;
                    break;
                }

                if (UserRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(UserRef, out result))
                    {
                        var taUser = new TBL_USER_INFOTableAdapter();
                        var dtUser = taUser.GetUserByRef(Convert.ToInt32(UserRef));
                        gvUser.DataSource = dtUser;
                        Session["data"] = dtUser;
                        SetUserGridData();
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void btnClearUser_Click(object sender, EventArgs e)
        {
            txtSearchUser.Text = "";
            var taUser = new TBL_USER_INFOTableAdapter();
            var dtUser = taUser.GetDataByAscCode();
            Session["data"] = dtUser;
            SetUserGridData();            
        }

        protected void btnClearEntry_Click(object sender, EventArgs e)
        {
            txtUserCode.Text = "";
            txtUserName.Text = "";
            txtDesig.Text = "";
            txtDept.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtPass.Text = "";
            txtConfPass.Text = "";
            txtPass.Attributes.Add("value", "");
            txtConfPass.Attributes.Add("value", "");
            cboCompany.SelectedIndex = 0;
            cboEmpId.SelectedIndex = 0;
            ddlSupervisor.SelectedIndex = 0;
            ddlUserGroup.SelectedIndex = 0;
            ddlUserLevel.SelectedIndex = 0;
            chkStatus.Checked = true;

            hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
            imgEmp.ImageUrl = "~/Image/NoImage.gif";

            Save.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";
            hfEmpRef.Value = "0";

            var taUser = new TBL_USER_INFOTableAdapter();
            var dtUser = taUser.GetDataByAscCode();
            Session["data"] = dtUser;
            SetUserGridData();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSrchEmp.Text = "";

            btnClearEmp.Visible = true;

            txtUserCode.Text = "";
            txtUserName.Text = "";
            txtDesig.Text = "";
            txtDept.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtPass.Text = "";
            txtConfPass.Text = "";
            txtPass.Attributes.Add("value", "");
            txtConfPass.Attributes.Add("value", "");
            cboCompany.SelectedIndex = 0;
            cboEmpId.SelectedIndex = 0;
            ddlSupervisor.SelectedIndex = 0;
            ddlUserGroup.SelectedIndex = 0;
            ddlUserLevel.SelectedIndex = 0;
            chkStatus.Checked = true;

            hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
            imgEmp.ImageUrl = "~/Image/NoImage.gif";

            Save.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";
            hfEmpRef.Value = "0";

            var taUser = new TBL_USER_INFOTableAdapter();
            var dtUser = taUser.GetDataByAscCode();
            Session["data"] = dtUser;
            SetUserGridData();
        }

        protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUser.PageIndex = e.NewPageIndex;
            SetUserGridData();
        }

        protected void gvUser_Sorting(object sender, GridViewSortEventArgs e)
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
                gvUser.DataSource = dataView;
                gvUser.DataBind();
            }
        }

        protected void cboEmpId_SelectedIndexChanged(object sender, EventArgs e)
        {
            hlEmpPic.NavigateUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + cboEmpId.SelectedValue + "'";
            imgEmp.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + cboEmpId.SelectedValue + "'";
        }
    }
}