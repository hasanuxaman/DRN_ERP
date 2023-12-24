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
    public partial class frmLeaveMas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taEmpType = new tblHrmsEmpTypeTableAdapter();
            cboEmpType.DataSource = taEmpType.GetDataByAsc();
            cboEmpType.DataValueField = "EmpTypeRef";
            cboEmpType.DataTextField = "EmpTypeName";
            cboEmpType.DataBind();
            cboEmpType.Items.Insert(0, new ListItem("---All---", "0"));

            var taJobStat = new tblHrmsJobStatTableAdapter();
            cboJobStat.DataSource = taJobStat.GetDataByAsc();
            cboJobStat.DataValueField = "JobStatRef";
            cboJobStat.DataTextField = "JobStatName";
            cboJobStat.DataBind();
            cboJobStat.Items.Insert(0, new ListItem("---All---", "0"));

            var taLvMas = new tblHrmsLeaveMasTableAdapter();
            gvLeaveMas.DataSource = taLvMas.GetDataByAsc();
            gvLeaveMas.DataBind();
            gvLeaveMas.SelectedIndex = -1;
        }

        protected void chkCaryFwd_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCaryFwd.Checked)
            {
                chkCaryFwd.Text = "Yes";
                txtMaxCaryFwdDays.Enabled = true;                
            }
            else
            {
                chkCaryFwd.Text = "No";
                txtMaxCaryFwdDays.Enabled = false;
                txtMaxCaryFwdDays.Text = "";
            }
        }

        protected void chkMatrnType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMatrnType.Checked)
            {
                chkMatrnType.Text = "Yes";
                txtMaxMtrn.Enabled = true;
            }
            else
            {
                chkMatrnType.Text = "No";
                txtMaxMtrn.Enabled = false; 
                txtMaxMtrn.Text = "";
            }
        }

        protected void chkHolDayConsider_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHolDayConsider.Checked)
            {
                chkHolDayConsider.Text = "Yes";
                txtHolDayCons.Enabled = true;
            }
            else
            {
                chkHolDayConsider.Text = "No";
                txtHolDayCons.Enabled = false;
                txtHolDayCons.Text = "";
            }
        }

        protected void chkEligibility_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEligibility.Checked)
            {
                chkEligibility.Text = "Yes";
                cboJobStat.Enabled = false;
                cboJobStat.SelectedIndex = 0;
            }
            else
            {
                chkEligibility.Text = "No";
                cboJobStat.Enabled = true;               
            }
        }
        protected void gvLeaveMas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);

                var hfHolCons = (HiddenField)(e.Row.FindControl("hfHolDayCons"));
                var lblHolCons = (Label)(e.Row.FindControl("lblHolDayCons"));
                lblHolCons.Text = hfHolCons.Value.ToString() == "1" ? "Yes" : "No";

                var hfCryFwd = (HiddenField)(e.Row.FindControl("hfCryFwd"));
                var lblCryFwd = (Label)(e.Row.FindControl("lblCryFwd"));
                lblCryFwd.Text = hfCryFwd.Value.ToString() == "1" ? "Yes" : "No";

                var hfMtrnType = (HiddenField)(e.Row.FindControl("hfMtrnType"));
                var lblMtrnType = (Label)(e.Row.FindControl("lblMtrnType"));
                lblMtrnType.Text = hfMtrnType.Value.ToString() == "1" ? "Yes" : "No";

                var hfLvJobStat = (HiddenField)(e.Row.FindControl("hfLvJobStat"));
                var lblLvJobStat = (Label)(e.Row.FindControl("lblLvJobStat"));
                if (hfLvJobStat.Value.ToString() == "0")
                    lblLvJobStat.Text = "All";
                else
                {
                    var taJobStat = new tblHrmsJobStatTableAdapter();
                    var dtJobStat = taJobStat.GetDataByJobStatRef(Convert.ToInt32(hfLvJobStat.Value.ToString()));
                    lblLvJobStat.Text = dtJobStat.Rows.Count > 0 ? dtJobStat[0].JobStatName : "";
                }

                var hfEmpType = (HiddenField)(e.Row.FindControl("hfEmpType"));
                var lblEmpType = (Label)(e.Row.FindControl("lblEmpType"));
                if (hfEmpType.Value.ToString() == "0")
                    lblEmpType.Text = "All";
                else
                {
                    var taEmpType = new tblHrmsEmpTypeTableAdapter();
                    var dtEmpType = taEmpType.GetDataByEmpTypeRef(Convert.ToInt32(hfEmpType.Value.ToString()));
                    lblEmpType.Text = dtEmpType.Rows.Count > 0 ? dtEmpType[0].EmpTypeName : "";
                }
            }
        }

        protected void gvLeaveMas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var rowNum = e.RowIndex;

                if (rowNum == -1) return;

                var lvRef = gvLeaveMas.Rows[rowNum].Cells[0].Text.Trim() == "&nbsp;"
                                              ? "0"
                                              : gvLeaveMas.Rows[rowNum].Cells[0].Text.Trim();

                var taEmpLv = new tblHrmsEmpLeaveTableAdapter();
                var dtEmpLv = taEmpLv.GetDataByLeaveMasRef(lvRef);
                if (dtEmpLv.Rows.Count > 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to delete this Leave.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Leave already used.";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                else
                {
                    var taLvMas = new tblHrmsLeaveMasTableAdapter();

                    taLvMas.DeleteLvMas(Convert.ToInt32(lvRef));

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Deleted Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    gvLeaveMas.DataSource = taLvMas.GetDataByAsc();
                    gvLeaveMas.DataBind();
                    gvLeaveMas.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvLeaveMas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvLeaveMas.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    HiddenField hfPayType = (HiddenField)gvLeaveMas.Rows[indx].FindControl("hfPayType");
                    var payType = hfPayType.Value;
                    HiddenField hfEmpType = (HiddenField)gvLeaveMas.Rows[indx].FindControl("hfEmpType");
                    var empType = hfEmpType.Value;
                    HiddenField hfHolDayCons = (HiddenField)gvLeaveMas.Rows[indx].FindControl("hfHolDayCons");
                    var holCons = hfHolDayCons.Value;
                    HiddenField hfCryFwd = (HiddenField)gvLeaveMas.Rows[indx].FindControl("hfCryFwd");
                    var cryFwd = hfCryFwd.Value;
                    HiddenField hfMtrnType = (HiddenField)gvLeaveMas.Rows[indx].FindControl("hfMtrnType");
                    var mtrnType = hfMtrnType.Value;
                    HiddenField hfLvJobStat = (HiddenField)gvLeaveMas.Rows[indx].FindControl("hfLvJobStat");
                    var jobStatus = hfLvJobStat.Value;
                    Label lblMinSerDays = (Label)gvLeaveMas.Rows[indx].FindControl("lblMinSerDays");

                    hfLvMasRefNo.Value = gvLeaveMas.Rows[indx].Cells[0].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLeaveMas.Rows[indx].Cells[0].Text.Trim();
                    txtLvMasCode.Text = gvLeaveMas.Rows[indx].Cells[1].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLeaveMas.Rows[indx].Cells[1].Text.Trim();
                    txtLvMasName.Text = gvLeaveMas.Rows[indx].Cells[2].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLeaveMas.Rows[indx].Cells[2].Text.Trim();
                    txtLvMasMaxDays.Text = gvLeaveMas.Rows[indx].Cells[3].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLeaveMas.Rows[indx].Cells[3].Text.Trim();
                    txtLvDayVal.Text = gvLeaveMas.Rows[indx].Cells[4].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLeaveMas.Rows[indx].Cells[4].Text.Trim();

                    txtMinSrvcDays.Text = lblMinSerDays.Text.Trim();

                    optListPayType.SelectedValue = payType;
                    
                    cboEmpType.SelectedValue = empType;                    

                    if (jobStatus == "0")
                    {
                        chkEligibility.Checked = true;
                        chkEligibility.Text = "Yes";
                        cboJobStat.Enabled = false;
                        cboJobStat.SelectedIndex = 0;
                        
                    }
                    else
                    {
                        chkEligibility.Checked = false;
                        chkEligibility.Text = "No";
                        cboJobStat.Enabled = true;
                        cboJobStat.SelectedValue = jobStatus;
                    }

                    if (holCons == "1")
                    {
                        chkHolDayConsider.Checked = true;
                        chkHolDayConsider.Text = "Yes";
                        txtHolDayCons.Enabled = true;
                        txtHolDayCons.Text = gvLeaveMas.Rows[indx].Cells[8].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLeaveMas.Rows[indx].Cells[8].Text.Trim();
                    }
                    else
                    {
                        chkHolDayConsider.Checked = false;
                        chkHolDayConsider.Text = "No";
                        txtHolDayCons.Enabled = false;
                        txtHolDayCons.Text = "";
                    }                    

                    if (cryFwd == "1")
                    {
                        chkCaryFwd.Checked = true;
                        chkCaryFwd.Text = "Yes";
                        txtMaxCaryFwdDays.Enabled = true;
                        txtMaxCaryFwdDays.Text = gvLeaveMas.Rows[indx].Cells[10].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLeaveMas.Rows[indx].Cells[10].Text.Trim();
                    }
                    else
                    {
                        chkCaryFwd.Checked = false;
                        chkCaryFwd.Text = "No";
                        txtMaxCaryFwdDays.Enabled = false;
                        txtMaxCaryFwdDays.Text = "";
                    }                    

                    if (mtrnType == "1")
                    {
                        chkMatrnType.Checked = true;
                        chkMatrnType.Text = "Yes";
                        txtMaxMtrn.Enabled = true;
                        txtMaxMtrn.Text = gvLeaveMas.Rows[indx].Cells[12].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvLeaveMas.Rows[indx].Cells[12].Text.Trim();
                    }
                    else
                    {
                        chkMatrnType.Checked = false;
                        chkMatrnType.Text = "No";
                        txtMaxMtrn.Enabled = false;
                        txtMaxMtrn.Text = "";
                    }                    
                }
                catch (Exception ex)
                {
                    hfLvMasRefNo.Value = "0";
                }
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (chkHolDayConsider.Checked)
            {
                if(txtHolDayCons.Text.Trim().Length > 0)                
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (chkCaryFwd.Checked)
            {
                if (txtMaxCaryFwdDays.Text.Trim().Length > 0)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }  
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (chkMatrnType.Checked)
            {
                if (txtMaxMtrn.Text.Trim().Length > 0)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
        }

        protected void CustomValidator4_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!chkEligibility.Checked)
            {
                if (cboJobStat.SelectedIndex == 0)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }
        }    

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtLvMasCode.Text = "";
            txtLvMasName.Text = "";
            txtLvMasDesc.Text = "";
            cboEmpType.SelectedIndex = 0;
            optListPayType.SelectedIndex = 0;
            txtLvMasMaxDays.Text = "";
            txtLvDayVal.Text = "1.00";
            chkMatrnType.Checked = false;
            chkMatrnType.Text = "No";
            chkEligibility.Checked = true;
            chkEligibility.Text = "Yes";
            cboJobStat.SelectedIndex = 0;
            txtMaxMtrn.Text = "";
            txtMaxMtrn.Enabled = false;
            chkCaryFwd.Checked = false;
            chkCaryFwd.Text = "No";
            txtMaxCaryFwdDays.Text = "";
            txtMaxCaryFwdDays.Enabled = false;
            chkHolDayConsider.Checked = false;
            chkHolDayConsider.Text = "No";
            txtHolDayCons.Text = "";
            txtHolDayCons.Enabled = false;
            txtMinSrvcDays.Text = "1";
            var taLvMas = new tblHrmsLeaveMasTableAdapter();
            gvLeaveMas.DataSource = taLvMas.GetDataByAsc();
            gvLeaveMas.DataBind();
            gvLeaveMas.SelectedIndex = -1;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var taLvMas = new tblHrmsLeaveMasTableAdapter();
            try
            {
                var cryFwd = chkCaryFwd.Checked ? 1 : 0;
                var holCons = chkHolDayConsider.Checked ? 1 : 0;
                var mtrnType = chkMatrnType.Checked ? 1 : 0;

                var maxDays = txtLvMasMaxDays.Text.Trim().Length > 0 ? Convert.ToDecimal(txtLvMasMaxDays.Text.Trim()) : 0;
                var dayVal = txtLvDayVal.Text.Trim().Length > 0 ? Convert.ToDecimal(txtLvDayVal.Text.Trim()) : 0;
                var consDays = txtHolDayCons.Text.Trim().Length > 0 ? Convert.ToDecimal(txtHolDayCons.Text.Trim()) : 0;
                var caryDays = txtMaxCaryFwdDays.Text.Trim().Length > 0 ? Convert.ToDecimal(txtMaxCaryFwdDays.Text.Trim()) : 0;
                var mtrnTimes = txtMaxMtrn.Text.Trim().Length > 0 ? Convert.ToDecimal(txtMaxMtrn.Text.Trim()) : 0;
                var jobStat = cboJobStat.SelectedValue.ToString();

                var dtLvMas = taLvMas.GetDataByRefNo(Convert.ToInt32(hfLvMasRefNo.Value.ToString()));
                if (dtLvMas.Rows.Count > 0)
                {
                    taLvMas.UpdateLvMas(txtLvMasCode.Text.Trim(), txtLvMasName.Text.Trim(), txtLvMasDesc.Text.Trim(), maxDays, Convert.ToInt32(optListPayType.SelectedValue),
                        optListPayType.SelectedItem.ToString(), dayVal, holCons, consDays, cryFwd, caryDays, mtrnType,
                        mtrnTimes, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                        cboEmpType.SelectedValue.ToString(), jobStat, txtMinSrvcDays.Text.Trim(), "1", "", Convert.ToInt32(hfLvMasRefNo.Value));

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    var dtLvMasRef = taLvMas.GetMaxLvMasRef();
                    var nextLvMasRef = dtLvMasRef == null ? 100001 : Convert.ToInt32(dtLvMasRef) + 1;

                    taLvMas.InsertLvMas(nextLvMasRef, txtLvMasCode.Text.Trim(), txtLvMasName.Text.Trim(), txtLvMasDesc.Text.Trim(), maxDays, Convert.ToInt32(optListPayType.SelectedValue),
                        optListPayType.SelectedItem.ToString(), dayVal, holCons, consDays, cryFwd, caryDays, mtrnType,
                        mtrnTimes, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                        cboEmpType.SelectedValue.ToString(), jobStat, txtMinSrvcDays.Text.Trim(), "1", "");

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                txtLvMasCode.Text = "";
                txtLvMasName.Text = "";
                txtLvMasDesc.Text = "";                
                cboEmpType.SelectedIndex = 0;
                optListPayType.SelectedIndex = 0;
                txtLvMasMaxDays.Text = "";
                txtLvDayVal.Text = "1.00";
                chkMatrnType.Checked = false;
                chkMatrnType.Text = "No";
                txtMaxMtrn.Text = "";
                txtMaxMtrn.Enabled = false;
                chkEligibility.Checked = true;
                chkEligibility.Text = "Yes";
                cboJobStat.SelectedIndex = 0;
                chkCaryFwd.Checked = false;
                chkCaryFwd.Text = "No";
                txtMaxCaryFwdDays.Text = "";
                txtMaxCaryFwdDays.Enabled = false;
                chkHolDayConsider.Checked = false;
                chkHolDayConsider.Text = "No";
                txtHolDayCons.Text = "";
                txtHolDayCons.Enabled = false;
                txtMinSrvcDays.Text = "1";
                gvLeaveMas.DataSource = taLvMas.GetDataByAsc();
                gvLeaveMas.DataBind();
                gvLeaveMas.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }            
    }
}