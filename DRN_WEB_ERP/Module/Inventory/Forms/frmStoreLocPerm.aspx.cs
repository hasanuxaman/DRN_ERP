using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Inventory.Forms
{
    public partial class frmStoreLocPerm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            AutoCompleteExtenderEmp.ContextKey = "0";

            var taStrLoc = new tbl_InMa_Str_LocTableAdapter();
            var dtStrLoc = taStrLoc.GetDataBySortName();
            chkListStrLoc.DataSource = dtStrLoc;
            chkListStrLoc.DataValueField = "Str_Loc_Ref";
            chkListStrLoc.DataTextField = "Str_Loc_Name";
            chkListStrLoc.DataBind();

            var taStrLocPermListEmp = new View_InMa_Str_Loc_Perm_List_EmpTableAdapter();
            var dtStrLocPermListEmp = taStrLocPermListEmp.GetDataBySortEmpName();
            gvStrLocPer.DataSource = dtStrLocPermListEmp;
            gvStrLocPer.DataBind();
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

        protected void gvStrLocPer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblEmpRef = ((Label)e.Row.FindControl("lblEmpRef")).Text;
                var tvStrLocPerm = (TreeView)e.Row.FindControl("tvStrLocPerm");                

                TreeNode tnMain = new TreeNode();
                tnMain.Text = "Store";
                tnMain.Value = "Store";
                tvStrLocPerm.Nodes.Add(tnMain);

                var taStrLocPerm = new tbl_InMa_Str_Loc_PermTableAdapter();
                var dtStrLocPerm = taStrLocPerm.GetDataByEmpRef(lblEmpRef);
                foreach (dsInvMas.tbl_InMa_Str_Loc_PermRow dr in dtStrLocPerm.Rows)
                {
                    TreeNode tnMod = new TreeNode();
                    tnMod.Text = dr.Str_Loc_Perm_Store_Name.ToString();
                    tnMod.Value = dr.Str_Loc_Perm_Store_Code.ToString();
                    tnMod.ShowCheckBox = false;
                    tnMain.ChildNodes.Add(tnMod);
                }

                tvStrLocPerm.ExpandAll();

                //e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                //e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvStrLocPer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taStrLocPerm = new tbl_InMa_Str_Loc_PermTableAdapter();
            var dtStrLocPerm = new dsInvMas.tbl_InMa_Str_Loc_PermDataTable();

            int indx = gvStrLocPer.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    var lblEmpRef = ((Label)gvStrLocPer.Rows[indx].FindControl("lblEmpRef")).Text;
                    
                    dtStrLocPerm = taStrLocPerm.GetDataByEmpRef(lblEmpRef.ToString());
                    if (dtStrLocPerm.Rows.Count > 0)
                    {
                        txtEmpName.Text = dtStrLocPerm[0].Str_Loc_Perm_Emp_Ref.ToString() + ":" + dtStrLocPerm[0].Str_Loc_Perm_Emp_Name.ToString();
                        txtEmpDesig.Text = dtStrLocPerm[0].Str_Loc_Perm_Emp_Desig.ToString();

                        if (dtStrLocPerm[0].Str_Loc_Perm_Status.ToString() == "1")
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
                    
                    foreach (ListItem li in chkListStrLoc.Items)
                    {
                        dtStrLocPerm = taStrLocPerm.GetDataByEmpRefStrCode(lblEmpRef.ToString(), li.Value.ToString());
                        if (dtStrLocPerm.Rows.Count > 0)
                            li.Selected = true;     
                        else
                            li.Selected = false;     
                    }

                }
                catch (Exception ex)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.";
                    tblMsg.Rows[1].Cells[0].InnerText = ex.Message.ToString();
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {            
            #region Validate Employee
            var empRef = "";
            var empName = "";
            var empDesig = "";
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
                        empDesig = dtViewEmp[0].DesigName.ToString();
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

            var taStrLocPerm = new tbl_InMa_Str_Loc_PermTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taStrLocPerm.Connection);

            try
            {
                short stat = (short)(optActive.Checked ? 1 : 0);

                taStrLocPerm.AttachTransaction(myTran);

                taStrLocPerm.DeleteStrLocPerm(empRef.ToString());
                foreach (ListItem li in chkListStrLoc.Items)
                {
                    if (li.Selected)
                        taStrLocPerm.InsertStrLocPerm(empRef.ToString(), empName.ToString(), empDesig.ToString(), li.Value.ToString(), li.Text.ToString(), "", "", "", "", "", stat.ToString(), "");
                }

                txtEmpName.Text = "";
                txtEmpDesig.Text = "";
                foreach (ListItem li in chkListStrLoc.Items)
                {
                    li.Selected = false;
                }
                optActive.Checked = true;
                optInactive.Checked = false;

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                var taStrLocPermListEmp = new View_InMa_Str_Loc_Perm_List_EmpTableAdapter();
                var dtStrLocPermListEmp = taStrLocPermListEmp.GetDataBySortEmpName();
                gvStrLocPer.DataSource = dtStrLocPermListEmp;
                gvStrLocPer.DataBind();
                gvStrLocPer.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.";
                tblMsg.Rows[1].Cells[0].InnerText = ex.Message.ToString();
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtEmpName.Text = "";
            txtEmpDesig.Text = "";
            foreach (ListItem li in chkListStrLoc.Items)
            {
                li.Selected = false;
            }
            optActive.Checked = true;
            optInactive.Checked = false;

            var taStrLocPermListEmp = new View_InMa_Str_Loc_Perm_List_EmpTableAdapter();
            var dtStrLocPermListEmp = taStrLocPermListEmp.GetDataBySortEmpName();
            gvStrLocPer.DataSource = dtStrLocPermListEmp;
            gvStrLocPer.DataBind();
            gvStrLocPer.SelectedIndex = -1;
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
                        txtEmpDesig.Text = dtViewEmp[0].DesigName.ToString();
                    }
                }
            }
            #endregion
        }
    }
}