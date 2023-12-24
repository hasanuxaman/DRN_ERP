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
    public partial class frmEmpLeave_Old : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taLvMas = new tblHrmsLeaveMasTableAdapter();
            cboLeaveType.DataSource = taLvMas.GetDataByAsc();
            cboLeaveType.DataValueField = "LvMasRefNo";
            cboLeaveType.DataTextField = "LvMasName";
            cboLeaveType.DataBind();
            cboLeaveType.Items.Insert(0, new ListItem("---Select---", "0"));

            var taEmpGenInfo = new View_Emp_BascTableAdapter();
            var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRefAct(Session["sessionEmpRef"] == null ? 0 : Convert.ToInt32(Session["sessionEmpRef"].ToString()));
            if (dtEmpGenInfo.Rows.Count > 0)
            {
                txtEmpName.Text = dtEmpGenInfo[0].EmpRefNo.ToString() + ":" + dtEmpGenInfo[0].EmpId.ToString() + ":" + dtEmpGenInfo[0].EmpName.ToString();

                lblDesig.Text = dtEmpGenInfo[0].DesigName.ToString();
                lblDept.Text = dtEmpGenInfo[0].DeptName.ToString();
                lblSec.Text = dtEmpGenInfo[0].SecName.ToString();
                lblShift.Text = dtEmpGenInfo[0].ShiftName.ToString();

                var dtEmpSup = taEmpGenInfo.GetDataByEmpRef(Convert.ToInt32(dtEmpGenInfo[0].EmpSuprId));
                lblSup.Text = dtEmpSup.Rows.Count > 0 ? dtEmpSup[0].EmpName.ToString() : "";

                hlEmpPic.NavigateUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + dtEmpGenInfo[0].EmpRefNo.ToString() + "'";
                imgEmp.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + dtEmpGenInfo[0].EmpRefNo.ToString() + "'";

                var taLv = new tblHrmsEmpLeaveTableAdapter();
                gvEmpApprLv.DataSource = taLv.GetDataByEmpRefYear(dtEmpGenInfo[0].EmpRefNo.ToString(), DateTime.Now.Year);
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
            }
        }

        protected void cboLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLeaveType.SelectedIndex == 0) return;

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

                    var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef));
                    if (dtEmp.Rows.Count > 0)
                    {
                        lblDesig.Text = dtEmp[0].DesigName.ToString();
                        lblDept.Text = dtEmp[0].DeptName.ToString();
                        lblSec.Text = dtEmp[0].SecName.ToString();
                        lblShift.Text = dtEmp[0].ShiftName.ToString();

                        var dtEmpSup = taEmp.GetDataByEmpRef(Convert.ToInt32(dtEmp[0].EmpSuprId));
                        lblSup.Text = dtEmpSup.Rows.Count > 0 ? dtEmpSup[0].EmpName.ToString() : "";

                        hlEmpPic.NavigateUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRef + "'";
                        imgEmp.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRef + "'";

                        var taLv = new tblHrmsEmpLeaveTableAdapter();
                        gvEmpApprLv.DataSource = taLv.GetDataByEmpRefYear(empRef, DateTime.Now.Year);
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
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            hlEmpPic.NavigateUrl = "~/Image/NoImage.gif";
            imgEmp.ImageUrl = "~/Image/NoImage.gif";

            txtEmpName.Text = "";
            cboLeaveType.SelectedIndex = 0;
            txtLvFrmDt.Text = "";
            txtLvToDt.Text = "";
            txtLvCmnt.Text = "";
            txtRespHandedTo.Text = "";

            lblDesig.Text = "";
            lblDept.Text = "";
            lblSec.Text = "";
            lblShift.Text = "";
            lblSup.Text = "";

            lblLvBal.Text = "";

            var taLv = new tblHrmsEmpLeaveTableAdapter();
            gvEmpApprLv.DataSource = taLv.GetDataByEmpRefYear("", DateTime.Now.Year);
            gvEmpApprLv.DataBind();
            gvEmpApprLv.SelectedIndex = -1;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var taEmpGenInfo = new tblHrmsEmpTableAdapter();
            var taEmpOff = new tblHrmsEmpOfficeTableAdapter();

            var taShift = new tblHrmsShiftTableAdapter();
            var taLvMas = new tblHrmsLeaveMasTableAdapter();

            var taEmpLv = new tblHrmsEmpLeaveTableAdapter();
            var taAtnd = new tblHrmsEmpDayAttndTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpLv.Connection);

            try
            {
                taEmpGenInfo.AttachTransaction(myTran);
                taEmpOff.AttachTransaction(myTran);

                taShift.AttachTransaction(myTran);
                taLvMas.AttachTransaction(myTran);

                taEmpLv.AttachTransaction(myTran);
                taAtnd.AttachTransaction(myTran);                

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

                                myTran.Rollback();
                                return;
                            }

                            if (lvBal < (lvEndDt.Value.Subtract(lvStDt.Value).Days + 1))
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "You can assign leave for " + lvBal + " day(s).";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();

                                myTran.Rollback();
                                return;
                            }
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Enter valid employee name.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();

                            myTran.Rollback();
                            return;
                        }

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

                                    myTran.Rollback();
                                    return;
                                }
                            }
                            else
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Responsible Employee.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();

                                myTran.Rollback();
                                return;
                            }
                        }

                        var dtLv = taEmpLv.GetDataByEmpRef(empRef, txtLvToDt.Text.ToString(), txtLvFrmDt.Text.ToString());
                        if (dtLv.Rows.Count > 0)
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Leave already taken between " + txtLvFrmDt.Text.ToString() + " and " + txtLvToDt.Text.ToString();
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();

                            myTran.Rollback();
                            return;
                        }
                        else
                        {
                            var dtMaxLvRef = taEmpLv.GetMaxLvRef(DateTime.Now.Year);
                            var nextLvRef = dtMaxLvRef == null ? 100001 : Convert.ToInt32(dtMaxLvRef) + 1;

                            taEmpLv.InsertEmpLv(nextLvRef.ToString(), empRef, cboLeaveType.SelectedValue, lvStDt.Value.Month.ToString("00") + "/" + lvStDt.Value.Year.ToString("0000"),
                                lvStDt.Value.Month, lvStDt.Value.Year, lvStDt, lvEndDt, lvEndDt.Value.Subtract(lvStDt.Value).Days + 1, txtLvCmnt.Text.Trim(), "0", "1",
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), DateTime.Now, DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), respEmpRef, respEmpName, "", "", "", "0", "");

                            var shiftRef = "0";
                            var shiftIn = DateTime.Now.ToString("hh:mm tt");
                            double shiftInGrace = 0;
                            double shiftLate = 0;
                            var shiftOut = DateTime.Now.ToString("hh:mm tt");
                            double shiftOutGrace = 0;
                            double shiftEarly = 0;
                            var totHr = "0";
                            double otMin = 0;

                            var dtEmp = taEmpOff.GetDataByEmpRef(empRef.ToString());
                            if (dtEmp.Rows.Count > 0)                            
                                shiftRef = dtEmp[0].ShiftRefNo;                            

                            var dtShift = taShift.GetDataByShiftRefNo(Convert.ToInt32(shiftRef.ToString()));
                            if (dtShift.Rows.Count > 0)
                            {
                                shiftIn = Convert.ToDateTime(dtShift[0].ShiftStart.ToString()).ToString("hh:mm tt");
                                shiftInGrace = Convert.ToDouble(dtShift[0].ShiftStartAdd.ToString());

                                shiftOut = Convert.ToDateTime(dtShift[0].ShiftEnd.ToString()).ToString("hh:mm tt");
                                shiftOutGrace = Convert.ToDouble(dtShift[0].ShiftEndAdd.ToString());

                                totHr = dtShift[0].ShiftTotal.ToString();
                            }

                            DateTime atnInTime = DateTime.ParseExact(shiftIn, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                            DateTime sftInTime = DateTime.ParseExact(shiftIn, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                            TimeSpan inDiff = atnInTime - sftInTime;
                            shiftLate = ((inDiff.TotalMinutes) >= shiftInGrace) ? (inDiff.TotalMinutes - shiftInGrace) : 0;

                            DateTime atnOutTime = DateTime.ParseExact(shiftOut, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                            DateTime sftOutTime = DateTime.ParseExact(shiftOut, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                            TimeSpan outDiff = sftOutTime - atnOutTime;
                            shiftEarly = ((outDiff.TotalMinutes) >= shiftOutGrace) ? (outDiff.TotalMinutes - shiftOutGrace) : 0;

                            otMin = (((atnOutTime - sftOutTime).TotalMinutes) > 0) ? (atnOutTime - sftOutTime).TotalMinutes : 0;

                            for (int i = 0; i <= (lvEndDt.Value.Subtract(lvStDt.Value).Days); i++)
                            {
                                taAtnd.InsertAttnd(empRef.ToString(), Convert.ToDateTime(txtLvFrmDt.Text.Trim()).AddDays(i), shiftRef.ToString(),
                                    atnInTime.ToString("hh:mm tt"), shiftIn.ToString(), shiftInGrace.ToString(), shiftLate.ToString(),
                                    atnOutTime.ToString("hh:mm tt"), shiftOut.ToString(), shiftOutGrace.ToString(), shiftEarly.ToString(), totHr.ToString(),
                                    otMin.ToString(), 1, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                    "", "", "", "1", "L");
                            }

                            //hlEmpPic.NavigateUrl = "";
                            //imgEmp.ImageUrl = "~/Image/NoImage.gif";

                            //txtEmpName.Text = "";
                            cboLeaveType.SelectedIndex = 0;
                            txtLvFrmDt.Text = "";
                            txtLvToDt.Text = "";
                            txtLvCmnt.Text = "";
                            txtRespHandedTo.Text = "";

                            //lblDesig.Text = "";
                            //lblDept.Text = "";
                            //lblSec.Text = "";
                            //lblShift.Text = "";
                            //lblSup.Text = "";
                            
                            lblLvBal.Text = "";

                            myTran.Commit();

                            var taLv = new tblHrmsEmpLeaveTableAdapter();
                            gvEmpApprLv.DataSource = taLv.GetDataByEmpRefYear(empRef, DateTime.Now.Year);
                            gvEmpApprLv.DataBind();

                            tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
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
        public string GetLvDt(string lvRef)
        {
            string lvDt = "";
            try
            {
                var taEmpLv = new tblHrmsEmpLeaveTableAdapter();
                var dtEmpLv = taEmpLv.GetDataByLvRefNo(lvRef);
                if (dtEmpLv.Rows.Count > 0)
                    lvDt = dtEmpLv[0].LvDetStDate.ToString("dd/MM/yyyy") + " to " + dtEmpLv[0].LvDetEndDate.ToString("dd/MM/yyyy");
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

                var dtEmpLv=taEmpLv.GetDataByLvRefNo(lvRef);
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
                gvEmpApprLv.DataSource = taLv.GetDataByEmpRefYear(empRef, DateTime.Now.Year);
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
    }
}