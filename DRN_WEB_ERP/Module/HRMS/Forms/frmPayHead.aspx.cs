using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.HRMS.DataSet;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsTranTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmPayHead : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taPayHead = new tblHrmsPayHeadTableAdapter();
            gvPayHead.DataSource = taPayHead.GetData();
            gvPayHead.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (chkDepndVal.Checked)
                Page.Validate("chkDpnd");

            if (chkAccu.Checked)
                Page.Validate("chkAcc");

            if (!Page.IsValid) return;

            var taPayHead = new tblHrmsPayHeadTableAdapter();

            try
            {
                var basePayHead = "";                
                if (chkDepndVal.Checked)
                {                    
                    var srchWords1 = txtBaseValPayHead.Text.Trim().Split(':');
                    foreach (string word in srchWords1)
                    {
                        basePayHead = word;
                        break;
                    }

                    if (basePayHead.Length > 0)
                    {
                        int result;
                        if (int.TryParse(basePayHead, out result))
                        {
                            var dtPayCode = taPayHead.GetDataByRef(Convert.ToInt32(basePayHead));
                            if (dtPayCode.Rows.Count > 0)
                            {
                                basePayHead = dtPayCode[0].PayHeadRef.ToString() + ":" + dtPayCode[0].PayHeadCode.ToString();
                            }
                        }
                    }
                }

                var depndPayHead = "";
                var srchWords2 = txtDpnd.Text.Trim().Split(':');
                foreach (string word in srchWords2)
                {
                    depndPayHead = word;
                    break;
                }

                if (depndPayHead.Length > 0)
                {
                    int result;
                    if (int.TryParse(depndPayHead, out result))
                    {
                        var dtPayCode = taPayHead.GetDataByRef(Convert.ToInt32(depndPayHead));
                        if (dtPayCode.Rows.Count > 0)
                        {
                            depndPayHead = dtPayCode[0].PayHeadRef.ToString() + ":" + dtPayCode[0].PayHeadCode.ToString();
                        }
                    }
                }

                var accPayHead = "";
                if (chkAccu.Checked)
                {
                    var srchWords = txtAccPayHead.Text.Trim().Split(':');
                    foreach (string word in srchWords)
                    {
                        accPayHead = word;
                        break;
                    }

                    if (accPayHead.Length > 0)
                    {
                        int result;
                        if (int.TryParse(accPayHead, out result))
                        {
                            var dtAccPayCode = taPayHead.GetDataByRef(Convert.ToInt32(accPayHead));
                            if (dtAccPayCode.Rows.Count > 0)
                            {
                                accPayHead = dtAccPayCode[0].PayHeadRef.ToString() + ":" + dtAccPayCode[0].PayHeadCode.ToString();
                            }
                        }
                    }
                }

                var baseVal = chkDepndVal.Checked ? basePayHead.ToString() : txtBaseValFixedValue.Text.Trim();
                var dpndVal = chkDepndVal.Checked ? depndPayHead.ToString() : txtDpnd.Text.Trim();
                var accVal = chkDepndVal.Checked ? accPayHead.ToString() : txtAccPayHead.Text.Trim();

                var dtPayHead = taPayHead.GetDataByRef(Convert.ToInt32(hfPayHeadRefNo.Value.ToString()));
                if (dtPayHead.Rows.Count > 0)
                {
                    taPayHead.UpdatePayHead(txtPayCode.Text.Trim(), txtPayName.Text.Trim(), baseVal, cboOperator.SelectedItem.ToString(),
                        dpndVal, optAccOpr.SelectedItem.ToString(), accVal.ToString(), Convert.ToInt32(cboDesiPlace.SelectedItem.ToString()),
                        "", "", "", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "",
                        Convert.ToInt32(hfPayHeadRefNo.Value.ToString()));

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    var dtPayHeadRef = taPayHead.GetMaxPayHeadRef();
                    var nextPayHeadRef = dtPayHeadRef == null ? 100001 : Convert.ToInt32(dtPayHeadRef) + 1;

                    taPayHead.InsertPayHead(nextPayHeadRef, txtPayCode.Text.Trim(), txtPayName.Text.Trim(), baseVal, cboOperator.SelectedItem.ToString(),
                        dpndVal, optAccOpr.SelectedItem.ToString(), accVal.ToString(), Convert.ToInt32(cboDesiPlace.SelectedItem.ToString()),
                        "", "", "", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }

                hfPayHeadRefNo.Value = "0";
                txtPayCode.Text = "";
                txtPayName.Text = "";
                txtBaseValFixedValue.Text = "";
                txtBaseValFixedValue.Enabled = true;
                chkDepndVal.Checked = false;
                txtBaseValPayHead.Text = "";
                txtBaseValPayHead.Enabled = false;
                cboOperator.SelectedIndex = 0;
                cboOperator.Enabled = false;
                txtDpnd.Text = "";
                txtDpnd.Enabled = false;
                chkAccu.Checked = false;
                chkAccu.Text = "No";
                optAccOpr.SelectedIndex = 0;
                optAccOpr.Enabled = false;
                txtAccPayHead.Text = "";
                txtAccPayHead.Enabled = false;
                cboDesiPlace.SelectedIndex = 0;

                gvPayHead.DataSource = taPayHead.GetData();
                gvPayHead.DataBind();
                gvPayHead.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            hfPayHeadRefNo.Value = "0";
            txtPayCode.Text = "";
            txtPayName.Text = "";
            txtBaseValFixedValue.Text = "";
            txtBaseValFixedValue.Enabled = true;
            chkDepndVal.Checked = false;
            txtBaseValPayHead.Text = "";
            txtBaseValPayHead.Enabled = false;
            cboOperator.SelectedIndex = 0;
            cboOperator.Enabled = false;
            txtDpnd.Text = "";
            txtDpnd.Enabled = false;
            chkAccu.Checked = false;
            chkAccu.Text = "No";
            optAccOpr.SelectedIndex = 0;
            optAccOpr.Enabled = false;
            txtAccPayHead.Text = "";
            txtAccPayHead.Enabled = false;
            cboDesiPlace.SelectedIndex = 0;

            var taPayHead = new tblHrmsPayHeadTableAdapter();
            gvPayHead.DataSource = taPayHead.GetData();
            gvPayHead.DataBind();
            gvPayHead.SelectedIndex = -1;
        }

        protected void chkDepndVal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDepndVal.Checked)
            {
                txtBaseValFixedValue.Text = "";
                txtBaseValFixedValue.Enabled = false;
                txtBaseValPayHead.Enabled = true;
                txtBaseValPayHead.Text = "";
                txtDpnd.Enabled = true;
                txtDpnd.Text = "";
                cboOperator.Enabled = true;
            }
            else
            {
                txtBaseValFixedValue.Text = "";
                txtBaseValFixedValue.Enabled = true;
                txtBaseValPayHead.Enabled = false;
                txtBaseValPayHead.Text = "";
                txtDpnd.Enabled = false;
                txtDpnd.Text = "";
                cboOperator.Enabled = false;
            }
        }

        protected void chkAccu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAccu.Checked)
            {
                chkAccu.Text = "Yes";
                optAccOpr.Enabled = true;
                optAccOpr.SelectedIndex = 0;
                txtAccPayHead.Enabled = true;
                txtAccPayHead.Text = "";

                var taPayHead = new tblHrmsPayHeadTableAdapter();
                var dtPayHead = new dsHrmsMas.tblHrmsPayHeadDataTable();

                dtPayHead = taPayHead.GetDataByRef(100001);
                if (dtPayHead.Rows.Count > 0)
                    txtAccPayHead.Text = dtPayHead[0].PayHeadRef.ToString() + ":" + dtPayHead[0].PayHeadCode.ToString() + ":" + dtPayHead[0].PayHeadName.ToString();
            }
            else
            {
                chkAccu.Text = "No";
                optAccOpr.Enabled = false;
                optAccOpr.SelectedIndex = 0;
                txtAccPayHead.Enabled = false;
                txtAccPayHead.Text = "";
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var taPayHead = new tblHrmsPayHeadTableAdapter();
            var payHead = "";
            var srchWords = txtBaseValPayHead.Text.Trim().Split(':');
            foreach (string word in srchWords)
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

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var taPayHead = new tblHrmsPayHeadTableAdapter();
            var payHead = "";
            var srchWords = txtDpnd.Text.Trim().Split(':');
            foreach (string word in srchWords)
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

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var taPayHead = new tblHrmsPayHeadTableAdapter();
            var payHead = "";
            var srchWords2 = txtAccPayHead.Text.Trim().Split(':');
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

        protected void gvPayHead_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }

        protected void gvPayHead_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var headRef = gvPayHead.Rows[rowNum].Cells[0].Text.Trim() == "&nbsp;"
                                          ? "0"
                                          : gvPayHead.Rows[rowNum].Cells[0].Text.Trim();

            var taEmpTran = new tblHrmsTrialSalaryTableAdapter();
            var dtEmpTran = taEmpTran.GetDataByPayHeadRef(headRef);
            if (dtEmpTran.Rows.Count > 0)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to delete this Pay Head.";
                tblMsg.Rows[1].Cells[0].InnerText = "Pay Head already used.";
                ModalPopupExtenderMsg.Show();
                return;
            }
            else
            {
                var taPayHead = new tblHrmsPayHeadTableAdapter();

                taPayHead.DeletePayHead(Convert.ToInt32(headRef));

                tblMsg.Rows[0].Cells[0].InnerText = "Data Deleted Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();


                gvPayHead.DataSource = taPayHead.GetData();
                gvPayHead.DataBind();
                gvPayHead.SelectedIndex = -1;
            }
        }

        protected void gvPayHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvPayHead.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    hfPayHeadRefNo.Value = gvPayHead.Rows[indx].Cells[0].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvPayHead.Rows[indx].Cells[0].Text.Trim();
                    txtPayCode.Text = gvPayHead.Rows[indx].Cells[1].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvPayHead.Rows[indx].Cells[1].Text.Trim();
                    txtPayName.Text = gvPayHead.Rows[indx].Cells[2].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvPayHead.Rows[indx].Cells[2].Text.Trim();
                    var baseVal = gvPayHead.Rows[indx].Cells[3].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvPayHead.Rows[indx].Cells[3].Text.Trim();
                    if (baseVal.Length > 0)
                    {
                        decimal result;
                        if (decimal.TryParse(baseVal, out result))
                        {
                            chkDepndVal.Checked = false;
                            txtBaseValFixedValue.Text = baseVal;
                            txtBaseValFixedValue.Enabled = true;
                            txtBaseValPayHead.Enabled = false;
                            txtBaseValPayHead.Text = "";
                            txtDpnd.Text = "";
                            txtDpnd.Enabled = false;                            
                            cboOperator.Enabled = false;
                        }
                        else
                        {
                            chkDepndVal.Checked = true;
                            txtBaseValFixedValue.Text = "";
                            txtBaseValFixedValue.Enabled = false;
                            txtBaseValPayHead.Text = baseVal;
                            txtBaseValPayHead.Enabled = true;                            
                            txtDpnd.Text = "";
                            txtDpnd.Enabled = true;                            
                            cboOperator.Enabled = true;
                        }      
                    }
                    cboOperator.SelectedValue = gvPayHead.Rows[indx].Cells[4].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvPayHead.Rows[indx].Cells[4].Text.Trim();
                    txtDpnd.Text = gvPayHead.Rows[indx].Cells[5].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvPayHead.Rows[indx].Cells[5].Text.Trim();
                    optAccOpr.SelectedValue = gvPayHead.Rows[indx].Cells[6].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvPayHead.Rows[indx].Cells[6].Text.Trim();
                    var accVal = gvPayHead.Rows[indx].Cells[7].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvPayHead.Rows[indx].Cells[7].Text.Trim();
                    if (accVal.Length > 0)
                    {
                        decimal result;
                        if (decimal.TryParse(accVal, out result))
                        {
                            chkAccu.Checked = false;
                            chkAccu.Text = "No";
                            optAccOpr.Enabled = false;
                            txtAccPayHead.Enabled = false;
                            txtAccPayHead.Text = accVal;
                        }
                        else
                        {
                            chkAccu.Checked = true;
                            chkAccu.Text = "Yes";
                            optAccOpr.Enabled = true;
                            txtAccPayHead.Enabled = true;
                            txtAccPayHead.Text = accVal;
                        }
                    }
                    cboDesiPlace.SelectedValue = gvPayHead.Rows[indx].Cells[8].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvPayHead.Rows[indx].Cells[8].Text.Trim();
                }
                catch (Exception ex)
                {
                    hfPayHeadRefNo.Value = "0";
                    tblMsg.Rows[0].Cells[0].InnerText = "Data loading error." + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }
    }
}