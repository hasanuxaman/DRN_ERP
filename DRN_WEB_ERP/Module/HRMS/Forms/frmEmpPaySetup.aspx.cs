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
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsTranTableAdapters;

namespace DRN_WEB_ERP.Module.HRMS.Forms
{
    public partial class frmEmpPaySetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            AutoCompleteExtenderEmp.ContextKey = "0";
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
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
            else
                args.IsValid = false;
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (optValType.SelectedValue == "1")
                args.IsValid = true;
            else
                if ((optValType.SelectedValue == "3") && (txtManualVal.Text.Trim().Length > 0))
                    args.IsValid = true;
                else
                    args.IsValid = false;
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var taEmp = new tblHrmsEmpTableAdapter();
            var empRef = "";
            var srchWords2 = txtSrchEmp.Text.Trim().Split(':');
            foreach (string word in srchWords2)
            {
                empRef = word;
                break;
            }

            if (empRef.Length > 0)
            {
                int result;
                if (int.TryParse(empRef, out result))
                {
                    var dtPayCode = taEmp.GetDataByEmpRef(empRef.ToString());
                    if (dtPayCode.Rows.Count > 0)
                        args.IsValid = true;
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
            else
                args.IsValid = false;
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            hfPaySetRefNo.Value = "0";
            //txtSrchEmp.Text = "";
            txtPayHead.Text = "";
            optValType.SelectedIndex = 0;
            txtManualVal.Text = "";
            txtManualVal.Enabled = false;
            chkShow.Checked = true;
            gvPaySetup.SelectedIndex = -1;

            chkIncTermEmp.Checked = false;
            AutoCompleteExtenderEmp.ContextKey = "0";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("Srch");

            if (!Page.IsValid) return;

            var taPaySet = new tblHrmsEmpPaySetTableAdapter();
            var taEmp = new tblHrmsEmpTableAdapter();

            try
            {
                #region Define_Employee_Ref
                var empRef = "0";
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
                        var dtEmp = taEmp.GetDataByEmpRef(empRef.ToString());
                        if (dtEmp.Rows.Count >= 0)
                        {
                            empRef = dtEmp[0].EmpRefNo.ToString();
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

                #region Define_Pay_Head
                var PayHead = "";
                var srchWords1 = txtPayHead.Text.Trim().Split(':');
                foreach (string word in srchWords1)
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
                var baseVal = "0";
                if (optValType.SelectedValue == "3")
                {
                    int result;
                    if (int.TryParse(txtManualVal.Text.Trim(), out result))
                    {
                        baseVal = txtManualVal.Text.Trim();
                    }
                }
                #endregion

                #region Define_Val_Flag
                var valFlag = "";
                var grdFlag = "";
                if (optValType.SelectedValue == "3")
                {
                    valFlag = "Y";
                    grdFlag = "N";
                }
                else
                {
                    valFlag = "N";
                    grdFlag = "Y";
                }
                #endregion

                #region Show_Flag
                var showFlag = "";
                if (chkShow.Checked)
                    showFlag = "T";
                else
                    showFlag = "F";
                #endregion

                if (hfPaySetRefNo.Value != "0")
                {
                    taPaySet.UpdatePaySet(empRef.ToString(), PayHead.ToString(), Convert.ToDecimal(baseVal), valFlag.ToString(), grdFlag.ToString(), showFlag.ToString(),
                        "", "", "", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "", Convert.ToInt32(hfPaySetRefNo.Value));

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                else
                {
                    var dtPayHeadExist = taPaySet.GetDataByPayHeadRef(empRef.ToString(), PayHead.ToString());
                    if (dtPayHeadExist.Rows.Count > 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Pay Head Already Exists.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                    else
                    {
                        taPaySet.InsertPaySet(empRef.ToString(), PayHead.ToString(), Convert.ToDecimal(baseVal), valFlag.ToString(), gvPaySetup.Rows.Count + 1,
                            grdFlag.ToString(), showFlag.ToString(), "", "", "",
                            DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                        tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }

                hfPaySetRefNo.Value = "0";
                //txtSrchEmp.Text = "";
                txtPayHead.Text = "";
                optValType.SelectedIndex = 0;
                txtManualVal.Text = "";
                txtManualVal.Enabled = false;
                chkShow.Checked = true;
                gvPaySetup.DataSource = taPaySet.GetDataByEmpRef(empRef);
                gvPaySetup.DataBind();
                gvPaySetup.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvPaySetup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);

                var valType = (Label)e.Row.FindControl("lblValType");
                var showFlag = (Label)e.Row.FindControl("lblShowFlag");

                if (valType.Text == "Y")
                    valType.Text = "Manual";
                else
                    valType.Text = "Default";

                if (showFlag.Text == "T")
                    showFlag.Text = "Show";
                else
                    showFlag.Text = "Hide";
            }
        }

        protected void gvPaySetup_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvPaySetup.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    hfPaySetRefNo.Value = gvPaySetup.Rows[indx].Cells[0].Text.Trim() == "&nbsp;"
                                          ? ""
                                          : gvPaySetup.Rows[indx].Cells[0].Text.Trim();
                    txtPayHead.Text = ((Label)gvPaySetup.Rows[indx].FindControl("lblPayHead")).Text;
                    var valTypeFlag = ((Label)gvPaySetup.Rows[indx].FindControl("lblValType")).Text.Trim();
                    if (valTypeFlag == "Manual")
                    {
                        optValType.SelectedIndex = 1;
                        txtManualVal.Enabled = true;
                        txtManualVal.Text = gvPaySetup.Rows[indx].Cells[3].Text.Trim() == "&nbsp;"
                                          ? "0"
                                          : Convert.ToDecimal(gvPaySetup.Rows[indx].Cells[3].Text.Trim()).ToString("F2");
                    }
                    else
                    {
                        optValType.SelectedIndex = 0;
                        txtManualVal.Enabled = false;
                        txtManualVal.Text = "";
                    }

                    var showFlag = ((Label)gvPaySetup.Rows[indx].FindControl("lblShowFlag")).Text.Trim();
                    if (showFlag == "Show")
                        chkShow.Checked = true;
                    else
                        chkShow.Checked = false;
                }
                catch (Exception ex)
                {
                    hfPaySetRefNo.Value = "0";
                    tblMsg.Rows[0].Cells[0].InnerText = "Data loading error." + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void gvPaySetup_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var taPaySet = new tblHrmsEmpPaySetTableAdapter();
            var taEmp = new tblHrmsEmpTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taPaySet.Connection);

            try
            {
                taPaySet.AttachTransaction(myTran);
                taEmp.AttachTransaction(myTran);

                var rowNum = e.RowIndex;

                if (rowNum == -1) return;

                var detRef = gvPaySetup.Rows[rowNum].Cells[0].Text.Trim() == "&nbsp;"
                                              ? "0"
                                              : gvPaySetup.Rows[rowNum].Cells[0].Text.Trim();

                var headRef = ((HiddenField)gvPaySetup.Rows[rowNum].FindControl("hfPaySetPayHeadRef")).Value;

                #region Define_Employee_Ref
                var empRef = "0";
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

                        var dtEmp = taEmp.GetDataByEmpRef(empRef.ToString());
                        if (dtEmp.Rows.Count >= 0)
                        {
                            empRef = dtEmp[0].EmpRefNo.ToString();
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

                taPaySet.DeleteByPayHeadRef(empRef, headRef);

                int i = 1;
                var dtPaySet = taPaySet.GetDataByEmpRef(empRef);
                foreach (dsHrmsTran.tblHrmsEmpPaySetRow dr in dtPaySet.Rows)
                {
                    taPaySet.UpdateSLNo(i, empRef, dr.PaySetEmpRef);
                    i++;
                }

                myTran.Commit();

                tblMsg.Rows[0].Cells[0].InnerText = "Data Deleted Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                gvPaySetup.DataSource = dtPaySet;
                gvPaySetup.DataBind();
                gvPaySetup.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {            
            var taEmp = new tblHrmsEmpTableAdapter();
            var taEmpOff = new tblHrmsEmpOfficeTableAdapter();
            var taGrd= new tblHrmsGradeDetTableAdapter();
            var taPaySet = new tblHrmsEmpPaySetTableAdapter();
            try
            {
                #region Define_Employee_Ref
                var empRef = "0";
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

                        var dtEmp = taEmp.GetDataByEmpRef(empRef.ToString());
                        if (dtEmp.Rows.Count >= 0)
                        {
                            empRef = dtEmp[0].EmpRefNo.ToString();
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

                var dtPaySet = taPaySet.GetDataByEmpRef(empRef.ToString());
                if (dtPaySet.Rows.Count <= 0)
                {
                    var dtEmpOff = taEmpOff.GetDataByEmpRef(empRef.ToString());
                    if (dtEmpOff.Rows.Count > 0)
                    {
                        var grdRef = dtEmpOff[0].EmpGrade.ToString();
                        var dtGrd = taGrd.GetDataByGrdRef(grdRef.ToString());
                        if (dtGrd.Rows.Count > 0)
                        {
                            foreach (dsHrmsMas.tblHrmsGradeDetRow dr in dtGrd.Rows)
                            {
                                #region Define_Pay_Head
                                var PayHead = "";
                                var srchWords1 = dr.GrdDetPayHead.ToString().Trim().Split(':');
                                foreach (string word in srchWords1)
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
                                var baseVal = "0";
                                int result1;
                                if (int.TryParse(dr.GrdDetBasePayHead, out result1))
                                {
                                    baseVal = dr.GrdDetBasePayHead.ToString();
                                }
                                #endregion

                                taPaySet.InsertPaySet(empRef.ToString(), PayHead.ToString(), Convert.ToDecimal(baseVal), "N", gvPaySetup.Rows.Count + 1, "Y",
                                    dr.GrdDetShowFlg, "", "", "", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");
                            }
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Grade Details Not Found";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Employee Grade Not Found";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                gvPaySetup.DataSource = dtPaySet;
                gvPaySetup.DataBind();
                gvPaySetup.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearEmp_Click(object sender, EventArgs e)
        {
            hfPaySetRefNo.Value = "0";
            txtSrchEmp.Text = "";
            txtPayHead.Text = "";
            optValType.SelectedIndex = 0;
            txtManualVal.Text = "";
            txtManualVal.Enabled = false;
            chkShow.Checked = true;
            var taPaySet = new tblHrmsEmpPaySetTableAdapter();
            gvPaySetup.DataSource = taPaySet.GetDataByEmpRef("0");
            gvPaySetup.DataBind();
            gvPaySetup.SelectedIndex = -1;
        }

        public string GetPayHead(int headRef)
        {
            string payHead = "";
            try
            {
                var taPayHead = new tblHrmsPayHeadTableAdapter();
                var dtPayHead = taPayHead.GetDataByRef(Convert.ToInt32(headRef));
                if (dtPayHead.Rows.Count > 0)
                    payHead = dtPayHead[0].PayHeadRef.ToString() + ":" + dtPayHead[0].PayHeadCode.ToString() + ":" + dtPayHead[0].PayHeadName.ToString();
                return payHead;
            }
            catch (Exception) { return payHead; }
        }

        protected void chkIncTermEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIncTermEmp.Checked)
                AutoCompleteExtenderEmp.ContextKey = "1";
            else
                AutoCompleteExtenderEmp.ContextKey = "0";
        }
    }
}