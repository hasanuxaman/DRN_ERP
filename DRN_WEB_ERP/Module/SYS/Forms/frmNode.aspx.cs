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
    public partial class frmNode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taModule = new TBL_MODULETableAdapter();
            var dtModule = taModule.GetData();
            foreach (dsSys.TBL_MODULERow dr in dtModule.Rows)
            {
                ddlNodeFilterByModule.Items.Add(new ListItem(dr.Module_Name.ToString(), dr.Module_Ref.ToString()));
            }
            ddlNodeFilterByModule.Items.Insert(0, new ListItem("---ALL---", "0"));

            //var dtModuleList = taModule.GetData();
            foreach (dsSys.TBL_MODULERow dr in dtModule.Rows)
            {
                cboModule.Items.Add(new ListItem(dr.Module_Name.ToString(), dr.Module_Ref.ToString()));
            }
            cboModule.Items.Insert(0, new ListItem("---Select---", "0"));

            SetNodeGridData();
            Save.Text = "Save";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var taNode = new TBL_NODETableAdapter();

            try
            {                
                var dtMod = taNode.ChkDataExists(Convert.ToInt32(hfRefNo.Value));
                if (dtMod.Rows.Count > 0)
                {
                    taNode.EditNode(txtNodeCode.Text.Trim(), txtNodeName.Text.Trim(), "", "", txtNodeUrl.Text.Trim(), 1, "", Convert.ToInt32(cboModule.SelectedValue),
                        DateTime.Now, Session["sessionUserId"] == null ? 0 : Convert.ToInt32(Session["sessionUserId"].ToString()),
                        chkStatus.Checked ? 1 : 2, "", Convert.ToInt32(hfRefNo.Value));
                }
                else
                {
                    var dtMaxNodRefNo = taNode.GetMaxNodeRef();
                    Int32 nextNodRefNo = dtMaxNodRefNo == null ? 100001 : Convert.ToInt32(dtMaxNodRefNo) + 1;

                    taNode.InsertNode(nextNodRefNo, txtNodeCode.Text.Trim(), txtNodeName.Text.Trim(), "", "", txtNodeUrl.Text.Trim(), 1, "", 
                        Convert.ToInt32(cboModule.SelectedValue),
                        DateTime.Now, Session["sessionUserId"] == null ? 0 : Convert.ToInt32(Session["sessionUserId"].ToString()),
                        DateTime.Now, Session["sessionUserId"] == null ? 0 : Convert.ToInt32(Session["sessionUserId"].ToString()), chkStatus.Checked ? 1 : 2, "");
                }
            }
            catch (Exception ex) { }

            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";
            SetNodeGridData();
            ddlNodeFilterByModule.SelectedIndex = 0;
            gvNode.SelectedIndex = -1;
            txtNodeCode.Text = "";
            txtNodeName.Text = "";
            txtNodeUrl.Text = "";
            Save.Text = "Save";
        }

        protected void SetNodeGridData()
        {
            var taNode = new TBL_NODETableAdapter();
            var dtNode = taNode.GetData();
            gvNode.DataSource = dtNode;
            gvNode.DataBind();
        }

        protected void gvNode_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvNode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvNode.SelectedIndex;

            if (indx != -1)
            {
                HiddenField hfNodRef = (HiddenField)gvNode.Rows[indx].FindControl("hfNodRef");
                hfRefNo.Value = hfNodRef.Value;
                hfEditStatus.Value = "Y";

                HiddenField hfModule = (HiddenField)gvNode.Rows[indx].FindControl("hfModule");
                string moduleId = hfModule.Value;

                HiddenField hfStatus = (HiddenField)gvNode.Rows[indx].FindControl("hfStatus");
                string statusId = hfStatus.Value;

                try
                {
                    txtNodeCode.Text = gvNode.Rows[indx].Cells[1].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvNode.Rows[indx].Cells[1].Text.Trim();
                    txtNodeName.Text = gvNode.Rows[indx].Cells[2].Text.Trim() == "&nbsp;"
                                             ? ""
                                             : gvNode.Rows[indx].Cells[2].Text.Trim();
                    txtNodeUrl.Text = gvNode.Rows[indx].Cells[3].Text.Trim() == "&nbsp;"
                                             ? ""
                                             : gvNode.Rows[indx].Cells[3].Text.Trim();
                    chkStatus.Checked = statusId == "1" ? true : false;
                    if (moduleId.Trim() != "")
                        cboModule.SelectedValue = moduleId;
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

        public string GetModuleName(int moduleRef)
        {
            string moduleName = "";
            try
            {
                var taModule = new TBL_MODULETableAdapter();
                var dtModule = taModule.GetModuleByRef(moduleRef);
                if (dtModule.Rows.Count > 0)
                    moduleName = dtModule[0].Module_Name;
                return moduleName;
            }
            catch (Exception ex)
            {
                return moduleName;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            cboModule.SelectedIndex = 0;
            txtNodeCode.Text = "";
            txtNodeName.Text = "";
            txtNodeUrl.Text = "";
            hfEditStatus.Value = "N";
            hfRefNo.Value = "0";
            Save.Text = "Save";
            gvNode.SelectedIndex = -1;
        }

        protected void dlFilterByModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taNode = new TBL_NODETableAdapter();
            var dtNode = new dsSys.TBL_NODEDataTable();

            try
            {
                if (ddlNodeFilterByModule.SelectedIndex == 0)
                {
                    dtNode = taNode.GetData();
                }
                else
                {
                    dtNode = taNode.GetNodeByModRef(Convert.ToInt32(ddlNodeFilterByModule.SelectedValue.ToString()));

                }

                gvNode.DataSource = dtNode;
                gvNode.DataBind();

                Save.Text = "Save";
                hfEditStatus.Value = "N";
                hfRefNo.Value = "0";
            }
            catch (Exception ex)
            {

            }
        }
    }
}