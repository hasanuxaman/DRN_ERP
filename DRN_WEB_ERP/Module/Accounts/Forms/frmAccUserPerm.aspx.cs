using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmAccUserPerm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            try
            {
                AutoCompleteExtenderEmp.ContextKey = "0";

                var taAccUserPerm = new tbl_Acc_User_PermTableAdapter();
                var dtAccUserPerm = taAccUserPerm.GetDataBySortEmpName();
                Session["data"] = dtAccUserPerm;
                SetUserPermData();
            }
            catch (Exception ex) { }
        }

        protected void txtEmpName_TextChanged(object sender, EventArgs e)
        {
            #region Validate Employee
            var empRef = "";
            var empName = "";
            var srchWords = txtEmpName.Text.Trim().Split(':');
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
                    var taViewEmp = new View_Emp_BascTableAdapter();
                    var dtViewEmp = taViewEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                    if (dtViewEmp.Rows.Count > 0)
                    {
                        empRef = dtViewEmp[0].EmpRefNo.ToString();
                        empName = dtViewEmp[0].EmpName.ToString();
                        txtDesignation.Text = dtViewEmp[0].DesigName.ToString();
                        txtDept.Text = dtViewEmp[0].DeptName.ToString();
                    }
                }
            }
            #endregion
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            hfUserPermEditRef.Value = "0";

            txtEmpName.Text = "";
            txtDesignation.Text = "";
            txtDept.Text = "";

            optEditYes.Checked = true;
            optEditNo.Checked = false;

            optActive.Checked = true;
            optInactive.Checked = false;

            var taAccUserPerm = new tbl_Acc_User_PermTableAdapter();
            var dtAccUserPerm = taAccUserPerm.GetDataBySortEmpName();
            Session["data"] = dtAccUserPerm;
            SetUserPermData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("btnSave");

            var taAccUserPerm = new tbl_Acc_User_PermTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taAccUserPerm.Connection);

            try
            {
                var empRef = "";
                var empName = "";

                if (txtEmpName.Text == "")
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter Employee Name.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                #region Validate Employee
                var srchWords = txtEmpName.Text.Trim().Split(':');
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
                        var taViewEmp = new View_Emp_BascTableAdapter();
                        var dtViewEmp = taViewEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                        if (dtViewEmp.Rows.Count > 0)
                        {
                            empRef = dtViewEmp[0].EmpRefNo.ToString();
                            empName = dtViewEmp[0].EmpName.ToString();
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
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
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Employee.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                var EditPerm = "N";
                if (optEditYes.Checked)
                    EditPerm = "Y";

                var PermStat = "0";
                if (optActive.Checked)
                    PermStat = "1";

                taAccUserPerm.AttachTransaction(myTran);

                if (hfUserPermEditRef.Value != "0")
                {
                    taAccUserPerm.UpdateAccUserPerm(EditPerm.ToString(), "", "", "", "", "", "", "",PermStat.ToString(),"", empRef.ToString());

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    var dtAccUserPerm = taAccUserPerm.GetDataByEmpRef(empRef.ToString());
                    if (dtAccUserPerm.Rows.Count > 0)
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "User permission already exists.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }

                    taAccUserPerm.InsertAccUserPerm(empRef.ToString(), empName.ToString(), txtDesignation.Text.Trim(), txtDept.Text.Trim(), EditPerm.ToString(), "", "", "", "", "", "", "", PermStat.ToString(), "");

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }

                hfUserPermEditRef.Value = "0";

                txtEmpName.Text = "";
                txtDesignation.Text = "";
                txtDept.Text = "";

                optEditYes.Checked = true;
                optEditNo.Checked = false;

                optActive.Checked = true;
                optInactive.Checked = false;

                var taAccUserPermNew = new tbl_Acc_User_PermTableAdapter();
                var dtAccUserPermNew = taAccUserPermNew.GetDataBySortEmpName();
                Session["data"] = dtAccUserPermNew;
                SetUserPermData();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.";
                tblMsg.Rows[1].Cells[0].InnerText = ex.Message.ToString();
                ModalPopupExtenderMsg.Show();
            }
        }

        #region GridData
        public string GetStatus(int statRef)
        {
            string strStat = "";
            try
            {
                strStat = statRef == 0 ? "Inactive" : "Active";
                return strStat;
            }
            catch (Exception) { return strStat; }
        }
        #endregion

        protected void SetUserPermData()
        {
            var dtConfig = Session["data"];
            gvAccUserPermList.DataSource = dtConfig;
            gvAccUserPermList.DataBind();
            gvAccUserPermList.SelectedIndex = -1;
        }

        protected void gvAccUserPermList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAccUserPermList.PageIndex = e.NewPageIndex;
            SetUserPermData();
        }

        protected void gvAccUserPermList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvAccUserPermList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvAccUserPermList.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    var lblEmpRef = ((Label)gvAccUserPermList.Rows[indx].FindControl("lblEmpRef")).Text.Trim();

                    hfUserPermEditRef.Value = lblEmpRef.ToString();

                    var taAccUserPerm = new tbl_Acc_User_PermTableAdapter();
                    var dtAccUserPerm = taAccUserPerm.GetDataByEmpRef(hfUserPermEditRef.Value.ToString());
                    if (dtAccUserPerm.Rows.Count > 0)
                    {
                        txtEmpName.Text = dtAccUserPerm[0].Acc_Perm_Emp_Ref.ToString() + ":" + dtAccUserPerm[0].Acc_Perm_Emp_Name.ToString();
                        txtDesignation.Text = dtAccUserPerm[0].Acc_Perm_Emp_Desig.ToString();
                        txtDept.Text = dtAccUserPerm[0].Acc_Perm_Emp_Dept.ToString();

                        if (dtAccUserPerm[0].Acc_Perm_Edit_Perm.ToString() == "Y")
                        {
                            optEditYes.Checked = true;
                            optEditNo.Checked = false;
                        }
                        else
                        {
                            optEditYes.Checked = false;
                            optEditNo.Checked = true;
                        }

                        if (dtAccUserPerm[0].Acc_Perm_Status.ToString() == "1")
                        {
                            optActive.Checked = true;
                            optInactive.Checked = false;
                        }
                        else
                        {
                            optInactive.Checked = true;
                            optActive.Checked = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    hfUserPermEditRef.Value = "0";
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.";
                    tblMsg.Rows[1].Cells[0].InnerText = ex.Message.ToString();
                    ModalPopupExtenderMsg.Show();
                }
            }
        }
    }
}