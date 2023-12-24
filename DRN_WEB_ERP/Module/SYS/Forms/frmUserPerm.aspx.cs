using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.SYS.DataSet;
using DRN_WEB_ERP.Module.SYS.DataSet.dsSysTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP
{
    public partial class frmUserPerm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            hfUserRef.Value = "0";
            SetMenuTreeData();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            //require transaction 
            var taUserPerm = new TBL_NODE_PERMTableAdapter();

            try
            {
                var UserRef = "";
                var srchWords = txtSrchUser.Text.Trim().Split(':');
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
                        var dtUserPerm = taUserPerm.ChkDataExists(Convert.ToInt32(hfUserRef.Value.ToString()));
                        if (dtUserPerm.Rows.Count > 0)
                            taUserPerm.DeleteUserPerm(Convert.ToInt32(hfUserRef.Value.ToString()));

                        foreach (TreeNode tn in tvMenu.Nodes)
                        {
                            saveNodePermission(tn);
                        }

                        tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid User.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid User.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void SetMenuTreeData()
        {
            var taModule = new TBL_MODULETableAdapter();
            var taNode = new TBL_NODETableAdapter();

            TreeNode tnMain = new TreeNode();
            tnMain.Text = "ERP";
            tnMain.Value = "ERP";
            tvMenu.Nodes.Add(tnMain);

            var dtModule = taModule.GetData();
            foreach (dsSys.TBL_MODULERow drMod in dtModule.Rows)
            {
                TreeNode tnMod = new TreeNode();
                tnMod.Text = drMod.Module_Name;
                tnMod.Value = drMod.Module_Ref.ToString();
                tnMod.ShowCheckBox = true;
                tnMain.ChildNodes.Add(tnMod);

                var dtNode = taNode.GetNodeByModRef(drMod.Module_Ref);
                foreach (dsSys.TBL_NODERow drNode in dtNode.Rows)
                {
                    TreeNode tnNode = new TreeNode();
                    tnNode.Text = drNode.Node_Name;
                    tnNode.Value = drNode.Node_Ref.ToString();
                    tnNode.ShowCheckBox = true;
                    tnMod.ChildNodes.Add(tnNode);
                }
            }
        }

        protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode node = this.tvMenu.SelectedNode;

            node.Checked = node.ShowCheckBox == true ? (node.Checked == true ? false : true) : false;

            tvMenu.SelectedNode.Selected = false;
        }

        protected void tvMenu_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {

        }

        private void saveNodePermission(TreeNode tn)
        {
            try
            {
                if (tn.ChildNodes.Count > 0)
                {
                    foreach (TreeNode tnc in tn.ChildNodes)
                    {
                        saveNodePermission(tnc);
                    }
                }
                else
                {
                    if (tn.Checked)
                    {
                        var taUserPerm = new TBL_NODE_PERMTableAdapter();

                        var dtMaxPermRef = taUserPerm.GetMaxPermRef();
                        var nextPermRef = dtMaxPermRef == null ? 100001 : Convert.ToInt32(dtMaxPermRef) + 1;

                        taUserPerm.InsertUserPerm(nextPermRef, Convert.ToInt32(hfUserRef.Value.ToString()), "", Convert.ToInt32(tn.Value), "Y", "Y", "Y", "Y",
                        DateTime.Now, Session["sessionUserId"] == null ? 0 : Convert.ToInt32(Session["sessionUserId"].ToString()), DateTime.Now,
                        Session["sessionUserId"] == null ? 0 : Convert.ToInt32(Session["sessionUserId"].ToString()), 1, "");
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void GetNodePermission(TreeNode tn)
        {
            try
            {
                if (tn.ChildNodes.Count > 0)
                {
                    foreach (TreeNode tnc in tn.ChildNodes)
                    {
                        GetNodePermission(tnc);
                    }
                }
                else
                {
                    var tnRef = tn.Value;
                    var taUserPerm = new TBL_NODE_PERMTableAdapter();                    
                    var dtUserPerm = taUserPerm.GetUserPermByNodeRef(Convert.ToInt32(hfUserRef.Value.ToString()), Convert.ToInt32(tnRef));
                    if (dtUserPerm.Rows.Count > 0)
                        tn.Checked = true;
                    else
                        tn.Checked = false;
                }
            }
            catch (Exception ex) { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var taUser = new TBL_USER_INFOTableAdapter();
            try
            {
                var UserRef = "";
                var srchWords = txtSrchUser.Text.Trim().Split(':');
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
                        var dtUser = taUser.GetUserByRef(Convert.ToInt32(UserRef));
                        if (dtUser.Rows.Count > 0)
                        {
                            hfUserRef.Value = dtUser[0].User_Ref_No.ToString();
                            lblCode.Text = dtUser[0].User_Code.ToString();
                            chkStatus.Checked = dtUser[0].User_Status == 1 ? true : false;

                            foreach (TreeNode tn in tvMenu.Nodes)
                            {
                                GetNodePermission(tn);
                            }

                            btnClear.Visible = true;

                            if (dtUser[0].User_Ext_Data1.ToString().Length > 0 && dtUser[0].User_Ext_Data1.ToString() != "0")
                            {
                                var taEmp = new View_Emp_BascTableAdapter();
                                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(dtUser[0].User_Ext_Data1.ToString()));
                                if (dtEmp.Rows.Count > 0)
                                {
                                    lblName.Text = dtEmp[0].EmpName.ToString();
                                    lblDesig.Text = dtEmp[0].DesigName.ToString();
                                    lblDept.Text = dtEmp[0].DeptName.ToString();
                                    lblEmail.Text = dtEmp[0].EmpOffEmail.ToString();

                                    var dtEmpSup = taEmp.GetDataByEmpRef(Convert.ToInt32(dtEmp[0].EmpSuprId));
                                    lblSuprv.Text = dtEmpSup.Rows.Count > 0 ? dtEmpSup[0].EmpName.ToString() + " (" + dtEmpSup[0].DesigName + ")" : "";

                                    hlEmpPic.NavigateUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + dtEmp[0].EmpRefNo + "'";
                                    imgEmp.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + dtEmp[0].EmpRefNo + "'";
                                }
                                else
                                {
                                    hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
                                    imgEmp.ImageUrl = "~/Image/NoImage.gif";

                                    lblCode.Text = "";
                                    lblName.Text = "";
                                    lblDesig.Text = "";
                                    lblDept.Text = "";
                                    lblEmail.Text = "";
                                    lblSuprv.Text = "";                                   
                                }
                            }
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid User.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }                
            }
            catch (Exception ex) { }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            hfUserRef.Value = "0";

            hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
            imgEmp.ImageUrl = "~/Image/NoImage.gif";

            lblCode.Text = "";
            lblName.Text = "";
            lblDesig.Text = "";
            lblDept.Text = "";
            lblEmail.Text = "";
            lblSuprv.Text = "";
            txtSrchUser.Text = "";

            chkStatus.Checked = false;

            foreach (TreeNode tn in tvMenu.Nodes)
            {
                ClearNodePermission(tn);
            }

            btnClear.Visible = false;
        }

        private void ClearNodePermission(TreeNode tn)
        {
            try
            {
                if (tn.ChildNodes.Count > 0)
                {
                    foreach (TreeNode tnc in tn.ChildNodes)
                    {
                        ClearNodePermission(tnc);
                    }
                }
                else
                {
                    tn.Checked = false;
                }
            }
            catch (Exception ex) { }
        }

    }
}