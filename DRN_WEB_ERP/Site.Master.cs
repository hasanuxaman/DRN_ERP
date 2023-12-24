using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Security;
using DRN_WEB_ERP.Module.SYS.DataSet;
using DRN_WEB_ERP.Module.SYS.DataSet.dsSysTableAdapters;

namespace DRN_WEB_ERP
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session.Count == 0)
                {
                    navigation.Visible = false;
                    lnkLogOut.Visible = false;
                    lnkUserName.Visible = false;
                    lnkCngPass.Visible = false;
                }
                else
                {
                    navigation.Visible = true;
                    lnkUserName.Visible = true;
                    lnkLogOut.Visible = true;
                    lnkCngPass.Visible = true;

                    if (!Page.IsPostBack)
                    {
                        var userRef = Session["sessionUserId"] == null ? 0 : Convert.ToInt32(Session["sessionUserId"].ToString());
                        var userName = Session["sessionUserName"] == null ? "" : Session["sessionUserName"].ToString();
                        var supervisorRef = Session["sessionSuperVisorId"] == null ? 0 : Convert.ToInt32(Session["sessionSuperVisorId"].ToString());
                        userRef = 100001;
                        lnkUserName.Text = "LogIn As: " + userName.ToString();

                        var taModPerm = new VIEW_MODULE_PERMTableAdapter();
                        var dtModPerm = taModPerm.GetModPermByUser(userRef);                        
                        if (dtModPerm.Rows.Count > 0)
                        {
                            //rptrModule.DataSource = dtModPerm;
                            //rptrModule.DataBind();
                            var taAdmModPerm = new VIEW_ADM_MOD_PERMTableAdapter();
                            var dtAdmModPerm = taAdmModPerm.GetData();
                            rptrModule.DataSource = dtAdmModPerm;
                            rptrModule.DataBind();
                        }
                        else
                        {
                            if (userRef == 100001)
                            {
                                var taAdmModPerm = new VIEW_ADM_MOD_PERMTableAdapter();
                                var dtAdmModPerm = taAdmModPerm.GetData();
                                rptrModule.DataSource = dtAdmModPerm;
                                rptrModule.DataBind();
                            }
                        }                        
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void rptrModule_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HiddenField hfModId = (HiddenField)e.Item.FindControl("hfModId");
                Repeater rptrNode = (Repeater)e.Item.FindControl("rptrNode");

                if (rptrNode != null)
                {
                    var userRef = Session["sessionUserId"] == null ? 0 : Convert.ToInt32(Session["sessionUserId"].ToString());
                    var taNodePerm = new VIEW_NODE_PERMTableAdapter();
                    var dtNodePerm = taNodePerm.GetNodePermByUserMod(userRef, Convert.ToInt32(hfModId.Value));
                    if (dtNodePerm.Rows.Count > 0)
                    {
                        rptrNode.DataSource = dtNodePerm;
                        rptrNode.DataBind();
                    }
                    else
                    {
                        if (userRef == 100001)
                        {
                            var taAdmNodePerm = new VIEW_ADM_NODE_PERMTableAdapter();
                            var dtAdmNodePerm = taAdmNodePerm.GetAdmNodePerm(Convert.ToInt32(hfModId.Value));
                            rptrNode.DataSource = dtAdmNodePerm;
                            rptrNode.DataBind();
                        }
                    }
                }
            }
        }

        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/frmLogin.aspx", true);
        }

        protected void lnkCngPass_Click(object sender, EventArgs e)
        {
            txtNewPass.Text = "";
            txtNewPass.Text = "";
            txtOldPass.Text = "";
            lblCngPassMsg.Text = "";
            ModalPopupExtenderCngPass.Show();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                var taUser = new TBL_USER_INFOTableAdapter();
                var dtUser = taUser.GetUserByRef(Convert.ToInt32(Session["sessionUserId"].ToString()));
                var userPass = dtUser.Rows.Count > 0 ? dtUser[0].User_Pass.ToString() : "";

                if (userPass.ToString() == txtOldPass.Text.Trim())
                {
                    if (txtNewPass.Text.Trim() == txtConfPass.Text.Trim())
                    {
                        taUser.UpdatePass(txtNewPass.Text.Trim(), DateTime.Now, Convert.ToInt32(Session["sessionUserId"].ToString()));

                        //txtNewPass.Text = "";
                        //txtNewPass.Text = "";
                        //txtOldPass.Text = "";

                        tblMsg.Rows[0].Cells[0].InnerText = "Password changed successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    else
                    {
                        lblCngPassMsg.Text = "New password does not match with confirm password.";
                        ModalPopupExtenderCngPass.Show();
                        return;
                    }
                }
                else
                {
                    lblCngPassMsg.Text = "Password does not match.";
                    ModalPopupExtenderCngPass.Show();
                    return;
                }                
            }
            catch (Exception ex) { }
        }
    }
}