using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmGradeDet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taGrdDef = new tblHrmsGradeDefTableAdapter();
            cboGrade.DataSource = taGrdDef.GetDataByAsc();
            cboGrade.DataValueField = "GrdDefRefNo";
            cboGrade.DataTextField = "GrdDefName";
            cboGrade.DataBind();
            cboGrade.Items.Insert(0, new ListItem("---Select---", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var taGrdDet = new tblHrmsGradeDetTableAdapter();

            try
            {

                #region Define_Pay_Head
                var PayHead = "";
                var srchWords = txtPayHead.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    PayHead = word;
                    break;
                }

                if (PayHead.Length > 0)
                {
                    int result;
                    if (int.TryParse(PayHead, out result))
                    {
                        var taPayHead = new tblHrmsPayHeadTableAdapter();
                        var dtPayCode = taPayHead.GetDataByRef(Convert.ToInt32(PayHead));
                        if (dtPayCode.Rows.Count >= 0)
                        {
                            PayHead = dtPayCode[0].PayHeadRef.ToString() + ":" + dtPayCode[0].PayHeadCode.ToString();
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Pay Head";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Pay Head";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter Pay Head First";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                #endregion

                #region Define_Base_Val
                var baseVal="0";
                if(optValType.SelectedValue=="3")
                {
                    int result;
                    if (int.TryParse(txtManualVal.Text.Trim(), out result))
                    {
                        baseVal=txtManualVal.Text.Trim();
                    }
                }
                #endregion

                #region Define_Flag
                var Flag="";
                if(optValType.SelectedValue=="3")                
                    Flag="V";
                else
                    Flag="W";
                #endregion

                #region Show_Flag
                var showFlag = "";
                if (chkShow.Checked)
                    showFlag = "T";
                else
                    showFlag = "F";
                #endregion

                if (hfGrdDetRefNo.Value != "0")
                {
                    taGrdDet.UpdateGrdDet(cboGrade.SelectedValue.ToString(), PayHead.ToString(), "", "", "", "",
                        baseVal.ToString(), "", "", "", "", Flag, showFlag.ToString(), "", "", "", "", DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), Convert.ToInt32(hfGrdDetRefNo.Value));

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    var dtGrdPayHeadExist = taGrdDet.GetDataByPayHead(PayHead.ToString());
                    if (dtGrdPayHeadExist.Rows.Count > 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Pay Head Already Exists in this Grade.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                    else
                    {
                        taGrdDet.InsertGrdDet(cboGrade.SelectedValue.ToString(), PayHead.ToString(), gvGrdDet.Rows.Count + 1, "", "", "", "",
                            baseVal.ToString(), "", "", "", "", Flag, showFlag.ToString(), gvGrdDet.Rows.Count + 1, 1, "", "", "", "", DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString());

                        tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }

                hfGrdDetRefNo.Value = "0";
                txtPayHead.Text = "";
                optValType.SelectedIndex = 0;
                txtManualVal.Text = "";
                txtManualVal.Enabled = false;
                chkShow.Checked = true;
                gvGrdDet.DataSource = taGrdDet.GetDataByGrdRef(cboGrade.SelectedValue.ToString());
                gvGrdDet.DataBind();
                gvGrdDet.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            hfGrdDetRefNo.Value = "0";
            cboGrade.SelectedIndex = 0;
            txtPayHead.Text = "";
            optValType.SelectedIndex = 0;
            txtManualVal.Text = "";
            txtManualVal.Enabled = false;
            chkShow.Checked = true;
            var taGrdDet = new tblHrmsGradeDetTableAdapter();
            gvGrdDet.DataSource = taGrdDet.GetDataByGrdRef("0");
            gvGrdDet.DataBind();
            gvGrdDet.SelectedIndex = -1;
        }

        protected void cboGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            hfGrdDetRefNo.Value = "0";
            txtPayHead.Text = "";
            optValType.SelectedIndex = 0;
            txtManualVal.Text = "";
            txtManualVal.Enabled = false;
            chkShow.Checked = true;

            var taGrdDet = new tblHrmsGradeDetTableAdapter();
            gvGrdDet.DataSource = taGrdDet.GetDataByGrdRef(cboGrade.SelectedValue.ToString());
            gvGrdDet.DataBind();
            gvGrdDet.SelectedIndex = -1;
        }

        protected void optValType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optValType.SelectedValue == "3")
            {
                txtManualVal.Enabled = true;
                txtManualVal.Text = "";
            }
            else
            {
                txtManualVal.Enabled = false;
                txtManualVal.Text = "";
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var taPayHead = new tblHrmsPayHeadTableAdapter();
            var payHead = "";
            var srchWords2 = txtPayHead.Text.Trim().Split(':');
            foreach (string word in srchWords2)
            {
                payHead = word;
                break;
            }

            if (payHead.Length > 0)
            {
                int result;
                if (int.TryParse(payHead, out result))
                {
                    var dtPayCode = taPayHead.GetDataByRef(Convert.ToInt32(payHead));
                    if (dtPayCode.Rows.Count > 0)
                        args.IsValid = true;
                }
                else
                    args.IsValid = false;
            }
            else
                args.IsValid = false;
        }

        protected void gvGrdDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);

                var valType = (Label)e.Row.FindControl("lblValType");
                var showFlag = (Label)e.Row.FindControl("lblShowFlag");

                if (valType.Text == "V")
                    valType.Text = "Manual";
                else
                    valType.Text = "Default";

                if (showFlag.Text == "T")
                    showFlag.Text = "Show";
                else
                    showFlag.Text = "Hide";
            }
        }

        protected void gvGrdDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var taGrdDet = new tblHrmsGradeDetTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taGrdDet.Connection);

            try
            {
                taGrdDet.AttachTransaction(myTran);

                var rowNum = e.RowIndex;

                if (rowNum == -1) return;

                var detRef = gvGrdDet.Rows[rowNum].Cells[0].Text.Trim() == "&nbsp;"
                                              ? "0"
                                              : gvGrdDet.Rows[rowNum].Cells[0].Text.Trim();

                taGrdDet.DeleteGrdDet(Convert.ToInt32(detRef));

                int i = 1;
                var dtGrdDet = taGrdDet.GetDataByGrdRef(cboGrade.SelectedValue.ToString());
                foreach (dsHrmsMas.tblHrmsGradeDetRow dr in dtGrdDet.Rows)
                {
                    taGrdDet.UpdateGrdDetLNo(i, i, 1, dr.GrdDetRefNo);
                    i++;
                }

                myTran.Commit();

                tblMsg.Rows[0].Cells[0].InnerText = "Data Deleted Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                gvGrdDet.DataSource = taGrdDet.GetDataByGrdRef(cboGrade.SelectedValue.ToString());
                gvGrdDet.DataBind();
                gvGrdDet.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvGrdDet_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvGrdDet.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    hfGrdDetRefNo.Value = gvGrdDet.Rows[indx].Cells[0].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvGrdDet.Rows[indx].Cells[0].Text.Trim();
                    txtPayHead.Text = gvGrdDet.Rows[indx].Cells[1].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvGrdDet.Rows[indx].Cells[1].Text.Trim();
                    var valTypeFlag = ((Label)gvGrdDet.Rows[indx].FindControl("lblValType")).Text.Trim();
                    if (valTypeFlag == "Manual")
                    {
                        optValType.SelectedIndex = 1;
                        txtManualVal.Enabled = true;
                        txtManualVal.Text = gvGrdDet.Rows[indx].Cells[3].Text.Trim() == "&nbsp;"
                                          ? "0"
                                          : gvGrdDet.Rows[indx].Cells[3].Text.Trim();
                    }
                    else
                    {
                        optValType.SelectedIndex = 0;
                        txtManualVal.Enabled = false;
                        txtManualVal.Text = "";
                    }

                    var showFlag = ((Label)gvGrdDet.Rows[indx].FindControl("lblShowFlag")).Text.Trim();
                    if (showFlag == "Show")
                        chkShow.Checked = true;
                    else
                        chkShow.Checked = false;
                }
                catch (Exception ex)
                {
                    hfGrdDetRefNo.Value = "0";
                    tblMsg.Rows[0].Cells[0].InnerText = "Data loading error." + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }
    }
}