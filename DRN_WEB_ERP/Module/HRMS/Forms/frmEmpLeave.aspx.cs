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
    public partial class frmEmpLeave : System.Web.UI.Page
    {
        double TotLvDays;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;            

            var taLvMas = new tblHrmsLeaveMasTableAdapter();
            var dtLvMas=new dsHrmsMas.tblHrmsLeaveMasDataTable();

            var curYear = DateTime.Now.Year;
            for (Int64 year = 2014; year <= (curYear); year++)
            {
                cboYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            cboYear.SelectedValue = curYear.ToString();

            var taEmpGenInfo = new View_Emp_BascTableAdapter();
            var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRef(Session["sessionEmpRef"] == null ? 0 : Convert.ToInt32(Session["sessionEmpRef"].ToString()));
            if (dtEmpGenInfo.Rows.Count > 0)
            {
                var serviceDays = Convert.ToDecimal(dtEmpGenInfo[0].EmpDOJ.Subtract(DateTime.Now).TotalDays.ToString());

                dtLvMas = taLvMas.GetDataByLeavePlicy(dtEmpGenInfo[0].EmpType, dtEmpGenInfo[0].EmpJobStatus, serviceDays);

                txtEmpName.Text = dtEmpGenInfo[0].EmpRefNo.ToString() + ":" + dtEmpGenInfo[0].EmpId.ToString() + ":" + dtEmpGenInfo[0].EmpName.ToString();

                var empref = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                if (empref == "000011") AutoCompleteExtenderLvEmp.ContextKey = "1";

                AutoCompleteExtender1.ContextKey = dtEmpGenInfo[0].DeptRefNo.ToString();

                lblDesig.Text = dtEmpGenInfo[0].DesigName.ToString();
                lblDept.Text = dtEmpGenInfo[0].DeptName.ToString();
                lblSec.Text = dtEmpGenInfo[0].SecName.ToString();
                lblShift.Text = dtEmpGenInfo[0].ShiftName.ToString();

                var dtEmpSup = taEmpGenInfo.GetDataByEmpRef(Convert.ToInt32(dtEmpGenInfo[0].EmpSuprId));
                lblSup.Text = dtEmpSup.Rows.Count > 0 ? dtEmpSup[0].EmpName.ToString() + " (" + dtEmpSup[0].DesigName + ")" : "";

                hlEmpPic.NavigateUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + dtEmpGenInfo[0].EmpRefNo.ToString() + "'";
                imgEmp.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + dtEmpGenInfo[0].EmpRefNo.ToString() + "'";

                var taPendLv = new ViewHrmsEmpLeaveAppTableAdapter();
                gvEmpPendLv.DataSource = taPendLv.GetDataByEmpRef(dtEmpGenInfo[0].EmpRefNo.ToString());
                gvEmpPendLv.DataBind();                

                var taAppLv = new tblHrmsEmpLeaveTableAdapter();
                gvEmpApprLv.DataSource = taAppLv.GetDataByEmpRefYear(dtEmpGenInfo[0].EmpRefNo.ToString(), Convert.ToInt32(cboYear.SelectedValue.ToString()));
                gvEmpApprLv.DataBind();
            }
            else
            {
                hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
                imgEmp.ImageUrl = "~/Image/NoImage.gif";

                lblDesig.Text = "";
                lblDept.Text = "";
                lblSec.Text = "";
                lblShift.Text = "";
                lblSup.Text = "";

                var taPendLv = new ViewHrmsEmpLeaveAppTableAdapter();
                gvEmpPendLv.DataSource = taPendLv.GetDataByEmpRef("");
                gvEmpPendLv.DataBind();
                gvEmpPendLv.EditIndex = -1;

                var taAppLv = new tblHrmsEmpLeaveTableAdapter();
                gvEmpApprLv.DataSource = taAppLv.GetDataByEmpRefYear("", Convert.ToInt32(cboYear.SelectedValue.ToString()));
                gvEmpApprLv.DataBind();
                gvEmpApprLv.EditIndex = -1;
            }

            cboLeaveType.DataSource = dtLvMas;
            cboLeaveType.DataValueField = "LvMasRefNo";
            cboLeaveType.DataTextField = "LvMasName";
            cboLeaveType.DataBind();
            cboLeaveType.Items.Insert(0, new ListItem("---Select---", "0"));
        }

        protected void cboLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLeaveType.SelectedIndex == 0)
            {
                lblLvBal.Text = "";
                return;
            }

            var empRef = "";
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
                    var taEmpGenInfo = new tblHrmsEmpTableAdapter();

                    var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRef(empRef.ToString());
                    if (dtEmpGenInfo.Rows.Count > 0)
                    {
                        var taEmpLv = new tblHrmsEmpLeaveTableAdapter();
                        var dtEmpLv = taEmpLv.GetTotEmpLv(empRef.ToString(), cboLeaveType.SelectedValue.ToString(), Convert.ToDecimal(DateTime.Now.Year));
                        var totEmpLv = dtEmpLv == null ? 0 : Convert.ToDecimal(dtEmpLv);

                        var taLvMas = new tblHrmsLeaveMasTableAdapter();
                        var dtLvMas = taLvMas.GetDataByRefNo(Convert.ToInt32(cboLeaveType.SelectedValue.ToString()));
                        var maxDays = dtLvMas[0].IsLvMasMaxDaysNull() ? 0 : dtLvMas[0].LvMasMaxDays;

                        var lvBal = maxDays - totEmpLv;

                        lblLvBal.Text = "Leave Balance (days) : " + lvBal.ToString("F2");
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Enter valid employee name.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter valid employee name.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var empRef = "";
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
                    var taEmpGenInfo = new tblHrmsEmpTableAdapter();

                    var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRef(empRef.ToString());
                    if (dtEmpGenInfo.Rows.Count > 0)
                        args.IsValid = true;
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var empRef = "";
            var srchWords = txtRespHandedTo.Text.Trim().Split(':');
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
                    var taEmpGenInfo = new tblHrmsEmpTableAdapter();

                    var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRef(empRef.ToString());
                    if (dtEmpGenInfo.Rows.Count > 0)
                        args.IsValid = true;
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
        }

        protected void txtEmpName_TextChanged(object sender, EventArgs e)
        {
            var empRef = "";
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
                    var taEmp = new View_Emp_BascTableAdapter();
                    var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt32(empRef.ToString()));
                    if (dtEmp.Rows.Count > 0)
                    {
                        AutoCompleteExtender1.ContextKey = dtEmp[0].DeptRefNo.ToString();

                        lblDesig.Text = dtEmp[0].DesigName.ToString();
                        lblDept.Text = dtEmp[0].DeptName.ToString();
                        lblSec.Text = dtEmp[0].SecName.ToString();
                        lblShift.Text = dtEmp[0].ShiftName.ToString();
                        
                        var taEmpNew = new View_Emp_BascTableAdapter();
                        var dtEmpSup = taEmpNew.GetDataByEmpRef(Convert.ToInt32(dtEmp[0].EmpSuprId.ToString()));
                        lblSup.Text = dtEmpSup.Rows.Count > 0 ? dtEmpSup[0].EmpName.ToString() + " (" + dtEmpSup[0].DesigName + ")" : "";

                        hlEmpPic.NavigateUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRef + "'";
                        imgEmp.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRef + "'";

                        var taPendLv = new ViewHrmsEmpLeaveAppTableAdapter();
                        gvEmpPendLv.DataSource = taPendLv.GetDataByEmpRef(empRef);
                        gvEmpPendLv.DataBind();

                        var taAppLv = new tblHrmsEmpLeaveTableAdapter();
                        gvEmpApprLv.DataSource = taAppLv.GetDataByEmpRefYear(empRef, Convert.ToInt32(cboYear.SelectedValue.ToString()));
                        gvEmpApprLv.DataBind();
                    }
                    else
                    {
                        hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
                        imgEmp.ImageUrl = "~/Image/NoImage.gif";

                        lblDesig.Text = "";
                        lblDept.Text = "";
                        lblSec.Text = "";
                        lblShift.Text = "";
                        lblSup.Text = "";

                        var taPendLv = new ViewHrmsEmpLeaveAppTableAdapter();
                        gvEmpPendLv.DataSource = taPendLv.GetDataByEmpRef("");
                        gvEmpPendLv.DataBind();
                        gvEmpPendLv.EditIndex = -1;

                        var taAppLv = new tblHrmsEmpLeaveTableAdapter();
                        gvEmpApprLv.DataSource = taAppLv.GetDataByEmpRefYear("", Convert.ToInt32(cboYear.SelectedValue.ToString()));
                        gvEmpApprLv.DataBind();
                        gvEmpApprLv.EditIndex = -1;
                    }
                }
                else
                {
                    hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
                    imgEmp.ImageUrl = "~/Image/NoImage.gif";

                    lblDesig.Text = "";
                    lblDept.Text = "";
                    lblSec.Text = "";
                    lblShift.Text = "";
                    lblSup.Text = "";

                    var taPendLv = new ViewHrmsEmpLeaveAppTableAdapter();
                    gvEmpPendLv.DataSource = taPendLv.GetDataByEmpRef("");
                    gvEmpPendLv.DataBind();
                    gvEmpPendLv.EditIndex = -1;

                    var taAppLv = new tblHrmsEmpLeaveTableAdapter();
                    gvEmpApprLv.DataSource = taAppLv.GetDataByEmpRefYear("", Convert.ToInt32(cboYear.SelectedValue.ToString()));
                    gvEmpApprLv.DataBind();
                    gvEmpApprLv.EditIndex = -1;
                }
            }
            else
            {
                hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
                imgEmp.ImageUrl = "~/Image/NoImage.gif";

                lblDesig.Text = "";
                lblDept.Text = "";
                lblSec.Text = "";
                lblShift.Text = "";
                lblSup.Text = "";

                var taPendLv = new ViewHrmsEmpLeaveAppTableAdapter();
                gvEmpPendLv.DataSource = taPendLv.GetDataByEmpRef("");
                gvEmpPendLv.DataBind();
                gvEmpPendLv.EditIndex = -1;

                var taAppLv = new tblHrmsEmpLeaveTableAdapter();
                gvEmpApprLv.DataSource = taAppLv.GetDataByEmpRefYear("", Convert.ToInt32(cboYear.SelectedValue.ToString()));
                gvEmpApprLv.DataBind();
                gvEmpApprLv.EditIndex = -1;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            var empref = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            if (empref == "000011")
            {
                txtEmpName.Text = "";
                lblDesig.Text = "";
                lblDept.Text = "";
                lblSec.Text = "";
                lblShift.Text = "";
                lblSup.Text = "";

                hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
                imgEmp.ImageUrl = "~/Image/NoImage.gif";

                var taPendLv = new ViewHrmsEmpLeaveAppTableAdapter();
                gvEmpPendLv.DataSource = taPendLv.GetDataByEmpRef("");
                gvEmpPendLv.DataBind();
                gvEmpPendLv.EditIndex = -1;

                var taAppLv = new tblHrmsEmpLeaveTableAdapter();
                gvEmpApprLv.DataSource = taAppLv.GetDataByEmpRefYear("", Convert.ToInt32(cboYear.SelectedValue.ToString()));
                gvEmpApprLv.DataBind();
                gvEmpApprLv.EditIndex = -1;
            }

            cboLeaveType.SelectedIndex = 0;
            txtLvFrmDt.Text = "";
            txtLvToDt.Text = "";
            txtLvCmnt.Text = "";
            txtRespHandedTo.Text = "";

            lblLvBal.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var taEmpGenInfo = new tblHrmsEmpTableAdapter();
            //var taEmpOff = new tblHrmsEmpOfficeTableAdapter();

            //var taShift = new tblHrmsShiftTableAdapter();
            var taLvMas = new tblHrmsLeaveMasTableAdapter();

            var taEmpLv = new tblHrmsEmpLeaveTableAdapter();
            //var taAtnd = new tblHrmsEmpDayAttndTableAdapter();

            var taEmpLvApp = new tblHrmsEmpLeaveAppTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpLvApp.Connection);

            try
            {
                //taEmpGenInfo.AttachTransaction(myTran);
                //taEmpOff.AttachTransaction(myTran);

                //taShift.AttachTransaction(myTran);
                //taLvMas.AttachTransaction(myTran);

                taEmpLvApp.AttachTransaction(myTran);
                //taAtnd.AttachTransaction(myTran);                

                DateTime? lvStDt = null;
                if (txtLvFrmDt.Text.Length > 0) lvStDt = Convert.ToDateTime(txtLvFrmDt.Text.Trim());

                DateTime? lvEndDt = null;
                if (txtLvToDt.Text.Length > 0) lvEndDt = Convert.ToDateTime(txtLvToDt.Text.Trim());

                var empRef = "";
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
                        #region Data Validation
                        var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRef(empRef.ToString());
                        if (dtEmpGenInfo.Rows.Count > 0)
                        {
                            var dtEmpLv = taEmpLv.GetTotEmpLv(empRef.ToString(), cboLeaveType.SelectedValue.ToString(), Convert.ToDecimal(Convert.ToDateTime(txtLvFrmDt.Text).Year));
                            var totEmpLv = dtEmpLv == null ? 0 : Convert.ToDecimal(dtEmpLv);

                            var dtLvMas = taLvMas.GetDataByRefNo(Convert.ToInt32(cboLeaveType.SelectedValue.ToString()));
                            var maxDays = dtLvMas[0].IsLvMasMaxDaysNull() ? 0 : dtLvMas[0].LvMasMaxDays;

                            var lvBal = maxDays - totEmpLv;

                            if (lvBal <= 0)
                            {
                                lblLvBal.Text = "Leave Balance (days) : " + lvBal.ToString("F2");

                                tblMsg.Rows[0].Cells[0].InnerText = "No Leave Balance : [" + lvBal + " day(s)]";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();

                                //myTran.Rollback();
                                return;
                            }

                            if (lvBal < (lvEndDt.Value.Subtract(lvStDt.Value).Days + 1))
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "You can apply leave for " + lvBal + " day(s).";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();

                                //myTran.Rollback();
                                return;
                            }
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Enter valid employee name.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();

                            //myTran.Rollback();
                            return;
                        }
                        #endregion

                        #region Resposible Person Validation
                        var respEmpRef = "";
                        var respEmpName = "";
                        var srchRespEmp = txtRespHandedTo.Text.Trim().Split(':');
                        foreach (string respWord in srchRespEmp)
                        {
                            respEmpRef = respWord;
                            break;
                        }

                        if (respEmpRef.Length > 0)
                        {
                            int respResult;
                            if (int.TryParse(respEmpRef, out respResult))
                            {
                                var dtRespEmpInfo = taEmpGenInfo.GetDataByEmpRef(respEmpRef.ToString());
                                if (dtEmpGenInfo.Rows.Count > 0)
                                {
                                    respEmpRef = dtRespEmpInfo[0].EmpRefNo.ToString();
                                    respEmpName = dtRespEmpInfo[0].EmpFirstName + " " + dtRespEmpInfo[0].EmpLastName;
                                }
                                else
                                {
                                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Responsible Employee.";
                                    tblMsg.Rows[1].Cells[0].InnerText = "";
                                    ModalPopupExtenderMsg.Show();

                                    //myTran.Rollback();
                                    return;
                                }
                            }
                            else
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Responsible Employee.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();

                                //myTran.Rollback();
                                return;
                            }
                        }
                        #endregion

                        var dtLv = taEmpLv.GetDataByEmpRef(empRef, txtLvToDt.Text.ToString(), txtLvFrmDt.Text.ToString());
                        if (dtLv.Rows.Count > 0)
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Leave already taken between " + txtLvFrmDt.Text.ToString() + " and " + txtLvToDt.Text.ToString();
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();

                            //myTran.Rollback();
                            return;
                        }
                        else
                        {
                            var dtPendLv = taEmpLvApp.GetDataByEmpRef(empRef, txtLvToDt.Text.ToString(), txtLvFrmDt.Text.ToString());
                            if (dtLv.Rows.Count > 0)
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "Already applied for leave between " + txtLvFrmDt.Text.ToString() + " and " + txtLvToDt.Text.ToString();
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();

                                //myTran.Rollback();
                                return;
                            }
                            else
                            {
                                var dtMaxLvDetRef = taEmpLvApp.GetMaxLvDetRef(DateTime.Now.Year);
                                var nextLvDetRef = dtMaxLvDetRef == null ? 100001 : Convert.ToInt32(dtMaxLvDetRef) + 1;
                                var nextLvDetRefNo = Convert.ToInt32(DateTime.Now.Year + "" + nextLvDetRef);

                                var dtMaxLvAppRef = taEmpLvApp.GetMaxLvAppRef(Convert.ToDateTime(lvStDt.Value).Year);
                                var nextLvAppRef = dtMaxLvAppRef == null ? "000001" : (Convert.ToInt32(dtMaxLvAppRef) + 1).ToString("000000");
                                var nextLvAppRefNo = "LVAP-" + lvStDt.Value.Year.ToString("0000") + "-" + nextLvAppRef;

                                taEmpLvApp.InsertLeaveApp(nextLvDetRefNo, empRef, cboLeaveType.SelectedValue, lvStDt.Value.Month.ToString("00") + "/" + lvStDt.Value.Year.ToString("0000"),
                                    lvStDt.Value.Month, lvStDt.Value.Year, lvStDt, lvEndDt, lvEndDt.Value.Subtract(lvStDt.Value).Days + 1, txtLvCmnt.Text.Trim(), "0", "Applied",
                                    Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString(), DateTime.Now, DateTime.Now,
                                    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), respEmpRef, respEmpName, "", "", "", "0",
                                    nextLvAppRefNo.ToString());
                                
                                myTran.Commit();
                            }
                            
                            var taPendLv = new ViewHrmsEmpLeaveAppTableAdapter();
                            gvEmpPendLv.DataSource = taPendLv.GetDataByEmpRef(empRef);
                            gvEmpPendLv.DataBind();
                            gvEmpPendLv.EditIndex = -1;

                            var taAppLv = new tblHrmsEmpLeaveTableAdapter();
                            gvEmpApprLv.DataSource = taAppLv.GetDataByEmpRefYear(empRef, Convert.ToInt32(cboYear.SelectedValue.ToString()));
                            gvEmpApprLv.DataBind();
                            gvEmpApprLv.EditIndex = -1;

                            tblMsg.Rows[0].Cells[0].InnerText = "Leave Application Saved Successfully.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();

                            try
                            {
                                //Send Mail
                                var mBody = MsgBody("");
                                var taEmpBas = new View_Emp_BascTableAdapter();
                                var dtEmpBas = new dsEmpDet.View_Emp_BascDataTable();

                                dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(empRef));
                                var supRef = dtEmpBas[0].EmpSuprId;
                                var mailBcc = dtEmpBas.Rows.Count > 0 ? (dtEmpBas[0].IsEmpOffEmailNull() ? "" : dtEmpBas[0].EmpOffEmail.ToString()) : "";

                                dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(supRef));
                                var mailTo = dtEmpBas.Rows.Count > 0 ? (dtEmpBas[0].IsEmpOffEmailNull() ? "" : dtEmpBas[0].EmpOffEmail.ToString()) : "";

                                dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(respEmpRef));
                                var mailCc = dtEmpBas.Rows.Count > 0 ? (dtEmpBas[0].IsEmpOffEmailNull() ? "" : dtEmpBas[0].EmpOffEmail.ToString()) : "";

                                //dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(empRef));
                                //var mailBcc = dtEmpBas.Rows.Count > 0 ? (dtEmpBas[0].IsEmpOffEmailNull() ? "" : dtEmpBas[0].EmpOffEmail.ToString()) : "";

                                DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail(mailTo, mailCc, mailBcc, "Leave Application", mBody);
                                
                                if (supRef == "000586")
                                {
                                    DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail("tanzeer@doreen.com", "nazrul@doreen.com", "iftekhar@doreen.com", "Leave Application", mBody);
                                }
                            }
                            catch (Exception ex)
                            {
                                //tblMsg.Rows[0].Cells[0].InnerText = "Mail Sending Error.\n" + ex.Message;
                                //tblMsg.Rows[1].Cells[0].InnerText = "";
                                //ModalPopupExtenderMsg.Show();
                            }
                            finally
                            {
                                cboLeaveType.SelectedIndex = 0;
                                txtLvFrmDt.Text = "";
                                txtLvToDt.Text = "";
                                txtLvCmnt.Text = "";
                                txtRespHandedTo.Text = "";
                                lblLvBal.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        #region GridData
        public string GetPendLvDt(int lvRef)
        {
            string lvDt = "";
            try
            {
                var taEmpLv = new tblHrmsEmpLeaveAppTableAdapter();
                var dtEmpLv = taEmpLv.GetDataByPendLvRef(Convert.ToInt32(lvRef));
                if (dtEmpLv.Rows.Count > 0)
                    lvDt = dtEmpLv[0].LvDetStDate.ToString("dd-MM-yyyy") + " to " + dtEmpLv[0].LvDetEndDate.ToString("dd-MM-yyyy");
                return lvDt;
            }
            catch (Exception) { return lvDt; }
        }

        public string GetLvDt(string lvRef)
        {
            string lvDt = "";
            try
            {
                var taEmpLv = new tblHrmsEmpLeaveTableAdapter();
                var dtEmpLv = taEmpLv.GetDataByLvRefNo(lvRef);
                if (dtEmpLv.Rows.Count > 0)
                    lvDt = dtEmpLv[0].LvDetStDate.ToString("dd-MM-yyyy") + " to " + dtEmpLv[0].LvDetEndDate.ToString("dd-MM-yyyy");
                return lvDt;
            }
            catch (Exception) { return lvDt; }
        }

        public string GetEmpName(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new tblHrmsEmpTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(empRef.ToString());
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].EmpFirstName.ToString() + " " + dtEmp[0].EmpLastName.ToString();
                return empStr;
            }
            catch (Exception) { return empStr; }
        }

        public string GetLvType(int lvTypeRef)
        {
            string lvType = "";
            try
            {
                var taLvMas = new tblHrmsLeaveMasTableAdapter();
                var dtLvMas = taLvMas.GetDataByRefNo(Convert.ToInt32(lvTypeRef.ToString()));
                if (dtLvMas.Rows.Count > 0)
                    lvType = dtLvMas[0].LvMasName.ToString();
                return lvType;
            }
            catch (Exception ex) { return lvType; }
        }
        #endregion

        protected void gvEmpPendLv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var taEmpLvApp = new tblHrmsEmpLeaveAppTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpLvApp.Connection);

            try
            {
                taEmpLvApp.AttachTransaction(myTran);

                var rowNum = e.RowIndex;

                if (rowNum == -1) return;

                var lvRef = ((HiddenField)(gvEmpPendLv.Rows[rowNum].FindControl("hfLvRef"))).Value;
                var empRef = ((HiddenField)(gvEmpPendLv.Rows[rowNum].FindControl("hfEmpRef"))).Value;

                taEmpLvApp.DeleteEmpPendLv(lvRef);
                myTran.Commit();

                tblMsg.Rows[0].Cells[0].InnerText = "Data Deleted Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                var taPendLv = new ViewHrmsEmpLeaveAppTableAdapter();
                gvEmpPendLv.DataSource = taPendLv.GetDataByEmpRef(empRef);
                gvEmpPendLv.DataBind();
                gvEmpPendLv.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvEmpApprLv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var taEmpLv = new tblHrmsEmpLeaveTableAdapter();
            var taEmpAtnd = new tblHrmsEmpDayAttndTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpLv.Connection);

            try
            {
                taEmpLv.AttachTransaction(myTran);
                taEmpAtnd.AttachTransaction(myTran);

                var rowNum = e.RowIndex;

                if (rowNum == -1) return;

                var lvRef = ((HiddenField)(gvEmpApprLv.Rows[rowNum].FindControl("hfLvRef"))).Value;
                var empRef = ((HiddenField)(gvEmpApprLv.Rows[rowNum].FindControl("hfEmpRef"))).Value;

                var dtEmpLv = taEmpLv.GetDataByLvRefNo(lvRef);
                if (dtEmpLv.Rows.Count > 0)
                {
                    taEmpLv.DeleteEmpLv(lvRef);
                    taEmpAtnd.DeleteLvDet(empRef, dtEmpLv[0].LvDetStDate, dtEmpLv[0].LvDetEndDate);
                    myTran.Commit();

                    tblMsg.Rows[0].Cells[0].InnerText = "Data Deleted Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                var taLv = new tblHrmsEmpLeaveTableAdapter();
                gvEmpApprLv.DataSource = taLv.GetDataByEmpRefYear(empRef, Convert.ToInt32(cboYear.SelectedValue.ToString()));
                gvEmpApprLv.DataBind();
                gvEmpApprLv.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        private string MsgBody(string qtnNo)
        {
            string str = "";
            try
            {
                DateTime? lvStDt = null;
                if (txtLvFrmDt.Text.Length > 0) lvStDt = Convert.ToDateTime(txtLvFrmDt.Text.Trim());

                DateTime? lvEndDt = null;
                if (txtLvToDt.Text.Length > 0) lvEndDt = Convert.ToDateTime(txtLvToDt.Text.Trim());

                var empref = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empref));

                str = dtEmp[0].EmpName.ToString() + " applied leave from : " + txtLvFrmDt.Text.Trim() + " to " + txtLvToDt.Text.Trim() + ", " + (lvEndDt.Value.Subtract(lvStDt.Value).Days + 1).ToString() + " Day(s).";
                str += "\n\n------------------------ DETAIL INFORMATION -------------------------------";
                str += "\nName                      : " + dtEmp[0].EmpName.ToString();
                str += "\nDepartment                : " + dtEmp[0].DeptName.ToString();
                str += "\nDesignation               : " + dtEmp[0].DesigName.ToString();
                str += "\nLeave Type                : " + cboLeaveType.SelectedItem.Text;
                str += "\nDate From                 : " + txtLvFrmDt.Text.Trim();
                str += "\nDate To                   : " + txtLvToDt.Text.Trim();
                str += "\nTotal Days                : " + (lvEndDt.Value.Subtract(lvStDt.Value).Days + 1).ToString();
                str += "\nRemarks                   : " + txtLvCmnt.Text.Trim();
                str += "\nResp. Handed Over To      : " + txtRespHandedTo.Text.Trim();
                str += "\n";
                str += "\n\n\n\nTo view detail just login the link bellow:\n\n";
                str += "http://192.168.0.10/DRNERP/";
                str += "\nor\n";
                str += "http://182.160.110.139/DRNERP/";
                str += "\n\n\n\n";
                str += "This is auto generated mail from DRN-ERP.";
                return str;
            }
            catch (Exception ex) { return str; }
        }

        protected void txtLvFrmDt_TextChanged(object sender, EventArgs e)
        {
            if (txtLvFrmDt.Text.Trim() != "")
                CalendarExtender1.StartDate = Convert.ToDateTime(txtLvFrmDt.Text);
        }

        protected void txtLvToDt_TextChanged(object sender, EventArgs e)
        {
            //if(txtLvToDt.Text.Trim()!="" && Convert.ToDateTime(txtLvToDt.Text.Trim()) )
        }

        protected void gvEmpApprLv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var empref = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblLvDays = ((Label)e.Row.FindControl("lblLvDays"));
                TotLvDays += Convert.ToDouble(lblLvDays.Text.Trim());

                //here apply your condition
                if (empref == "000011")
                    gvEmpApprLv.Columns[5].Visible = true;
                else
                    gvEmpApprLv.Columns[5].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotLvDays = (Label)e.Row.FindControl("lblTotLvDays");
                lblTotLvDays.Text = TotLvDays.ToString("N2");
            }
        }

        protected void gvEmpPendLv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var empref = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //here apply your condition
                if (empref == "000011")
                    gvEmpPendLv.Columns[5].Visible = true;
                else
                    gvEmpPendLv.Columns[5].Visible = false;
            }
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            var empRefNo = "0";

            var empRef = "";
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
                    var taEmp = new View_Emp_BascTableAdapter();

                    var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef));
                    if (dtEmp.Rows.Count > 0) empRefNo = dtEmp[0].EmpRefNo.ToString();                    
                }                
            }       
            
            var taLv = new tblHrmsEmpLeaveTableAdapter();
            gvEmpApprLv.DataSource = taLv.GetDataByEmpRefYear(empRefNo, Convert.ToInt32(cboYear.SelectedValue.ToString()));
            gvEmpApprLv.DataBind();
            gvEmpApprLv.SelectedIndex = -1;
        }
    }
}