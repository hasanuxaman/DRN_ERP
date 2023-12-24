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
    public partial class frmViewNodePerm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            SetMenuTreeData();
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
                //tnMod.ShowCheckBox = true;
                tnMain.ChildNodes.Add(tnMod);

                var dtNode = taNode.GetNodeByModRef(drMod.Module_Ref);
                foreach (dsSys.TBL_NODERow drNode in dtNode.Rows)
                {
                    TreeNode tnNode = new TreeNode();
                    tnNode.Text = drNode.Node_Name;
                    tnNode.Value = drNode.Node_Ref.ToString();
                    //tnNode.ShowCheckBox = true;
                    tnMod.ChildNodes.Add(tnNode);
                }
            }
            tvMenu.CollapseAll();
            tnMain.Expand();
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

        protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (tvMenu.SelectedNode.ChildNodes.Count == 0)
            {
                //MessageBox.Show("The node does not have any children.");
                var taNodePerm = new VIEW_USER_PERM_NODETableAdapter();
                var dtNodePerm = taNodePerm.GetDataByNodeRef(Convert.ToInt32(tvMenu.SelectedNode.Value.ToString()), 1);
                gvNode.DataSource = dtNodePerm;
                gvNode.DataBind();
            }
        }

        protected void gvNode_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var userRef = gvNode.Rows[rowNum].Cells[1].Text.Trim() == "&nbsp;"
                                          ? "0"
                                          : gvNode.Rows[rowNum].Cells[1].Text.Trim();

            var nodeRef = gvNode.Rows[rowNum].Cells[4].Text.Trim() == "&nbsp;"
                                          ? "0"
                                          : gvNode.Rows[rowNum].Cells[4].Text.Trim();

            var taNodePerm = new TBL_NODE_PERMTableAdapter();
            taNodePerm.DeleteNodePermByUser(Convert.ToInt32(userRef.ToString()), Convert.ToInt32(nodeRef.ToString()));

            var taUserNodePerm = new VIEW_USER_PERM_NODETableAdapter();
            var dtNodePerm = taUserNodePerm.GetDataByNodeRef(Convert.ToInt32(nodeRef.ToString()), 1);
            gvNode.DataSource = dtNodePerm;
            gvNode.DataBind();

            tblMsg.Rows[0].Cells[0].InnerText = "Data Deleted Successfully.";
            tblMsg.Rows[1].Cells[0].InnerText = "";
            ModalPopupExtenderMsg.Show();
        }
    }
}