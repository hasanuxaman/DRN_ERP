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
    public partial class frmEmpLeaveAppr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Auto  Page Refresh
            //Response.AppendHeader("Refresh", "60");

            if (Page.IsPostBack) return;

            try
            {
                var suprRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var userRef = Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString();

                var taPndLvDet = new ViewHrmsEmpLeaveAppTableAdapter();
                if (suprRef == "000634" || suprRef == "000884" || suprRef == "000011")//HRD,Nahamad,imran
                    gvEmpPendLv.DataSource = taPndLvDet.GetDataByHrdRef(suprRef);
                else if (suprRef == "100001") //Admin User
                    gvEmpPendLv.DataSource = taPndLvDet.GetDataByAdminUser();
                else if (userRef == "100290" || userRef == "100291") //VC,SMD Sir
                    gvEmpPendLv.DataSource = taPndLvDet.GetDataBySupRef("000586");
                else
                    gvEmpPendLv.DataSource = taPndLvDet.GetDataBySupRef(suprRef);
                gvEmpPendLv.DataBind();
            }
            catch (Exception ex) { }
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

        public string GetEmpId(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].EmpId.ToString();
                return empStr;
            }
            catch (Exception) { return empStr; }
        }

        public string GetEmpDesig(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].DesigName.ToString();
                return empStr;
            }
            catch (Exception) { return empStr; }
        }

        public string GetEmpDept(string empRef)
        {
            string empStr = "";
            try
            {
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                    empStr = dtEmp[0].DeptName.ToString();
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

        public string GetLvBal(string lvYr, string empRef, int lvTypeRef)
        {
            decimal lvBal = 0;
            try
            {
                var taEmpLv = new tblHrmsEmpLeaveTableAdapter();
                var dtEmpLv = taEmpLv.GetTotEmpLv(empRef.ToString(), lvTypeRef.ToString(), Convert.ToDecimal(Convert.ToDateTime(lvYr.ToString()).Year));
                var totEmpLv = dtEmpLv == null ? 0 : Convert.ToDecimal(dtEmpLv);

                var taLvMas = new tblHrmsLeaveMasTableAdapter();
                var dtLvMas = taLvMas.GetDataByRefNo(Convert.ToInt32(lvTypeRef.ToString()));
                var maxDays = dtLvMas[0].IsLvMasMaxDaysNull() ? 0 : dtLvMas[0].LvMasMaxDays;

                lvBal = (maxDays - totEmpLv);

                return lvBal.ToString();
            }
            catch (Exception ex) { return lvBal.ToString(); }
        }
        #endregion

        protected void gvEmpPendLv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region Update Leave Status
            var taEmpLvApp = new tblHrmsEmpLeaveAppTableAdapter();
            var taPndLvDet = new ViewHrmsEmpLeaveAppTableAdapter();
            var taEmpLv = new tblHrmsEmpLeaveTableAdapter();
            var taEmpOff = new tblHrmsEmpOfficeTableAdapter();
            var taShift = new tblHrmsShiftTableAdapter();
            //var taLvMas = new tblHrmsLeaveMasTableAdapter();
            var taAtnd = new tblHrmsEmpDayAttndTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taEmpLv.Connection);

            try
            {
                taEmpLvApp.AttachTransaction(myTran);
                taEmpLv.AttachTransaction(myTran);
                //taEmpOff.AttachTransaction(myTran);
                //taShift.AttachTransaction(myTran);
                //taLvMas.AttachTransaction(myTran);
                taAtnd.AttachTransaction(myTran);

                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = gvEmpPendLv.Rows[index];

                var hfLvDetRef = (HiddenField)(row.Cells[2].FindControl("hfLvRef"));

                var dtPndLvDet = taPndLvDet.GetDataByLvDetRef(Convert.ToInt32(hfLvDetRef.Value.ToString()));

                var dtMaxLvDetRef = taEmpLvApp.GetMaxLvDetRef(DateTime.Now.Year);
                var nextLvDetRef = dtMaxLvDetRef == null ? 100001 : Convert.ToInt32(dtMaxLvDetRef) + 1;
                var nextLvDetRefNo = Convert.ToInt32(DateTime.Now.Year + "" + nextLvDetRef);

                #region Reject
                if (e.CommandName == "Reject")
                {
                    if (dtPndLvDet.Rows.Count > 0)
                    {
                        taEmpLvApp.InsertLeaveApp(nextLvDetRefNo, dtPndLvDet[0].EmpRefNo, dtPndLvDet[0].LvDetLvType, dtPndLvDet[0].LvDetPeriod, dtPndLvDet[0].LvDetMonth,
                            dtPndLvDet[0].LvDetYear, dtPndLvDet[0].LvDetStDate, dtPndLvDet[0].LvDetEndDate, dtPndLvDet[0].LvDetDays, dtPndLvDet[0].LvDetCmnt,
                            (Convert.ToInt32(dtPndLvDet[0].LvDetInitRef) - 1).ToString(), "Rejected",
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), DateTime.Now, DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), dtPndLvDet[0].LvDetExtData1,
                            dtPndLvDet[0].LvDetExtData2, "", "", "", "0", dtPndLvDet[0].LvDetFlag);

                        myTran.Commit();
                        //tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                        //tblMsg.Rows[1].Cells[0].InnerText = "";
                        //ModalPopupExtenderMsg.Show();

                        try
                        {
                            //Send Mail
                            var mBody = MsgBody(dtPndLvDet[0].LvDetRef.ToString());

                            var taEmpBas = new View_Emp_BascTableAdapter();
                            //var dtEmpBas = new dsEmpDet.View_Emp_BascDataTable();

                            //dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(dtPndLvDet[0].EmpRefNo));
                            //var supRef = dtEmpBas[0].EmpSuprId;
                            //dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(supRef));
                            //var mailBcc = dtEmpBas.Rows.Count > 0 ? (dtEmpBas[0].IsEmpOffEmailNull() ? "" : dtEmpBas[0].EmpOffEmail.ToString()) : "";

                            //dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(dtPndLvDet[0].LvDetExtData1));
                            //var mailCc = dtEmpBas.Rows.Count > 0 ? (dtEmpBas[0].IsEmpOffEmailNull() ? "" : dtEmpBas[0].EmpOffEmail.ToString()) : "";

                            var dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(dtPndLvDet[0].EmpRefNo));
                            var mailTo = dtEmpBas.Rows.Count > 0 ? (dtEmpBas[0].IsEmpOffEmailNull() ? "" : dtEmpBas[0].EmpOffEmail.ToString()) : "";
                            var supRef = dtEmpBas[0].EmpSuprId;

                            var mailCc = "";
                            var mailBcc = "";

                            if (supRef == "000586")
                            {
                                DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail(mailTo, mailCc, "iftekhar@doreen.com", "Leave Application Rejected", mBody);
                            }
                            else
                            {
                                DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail(mailTo, mailCc, mailBcc, "Leave Application Rejected", mBody);
                            }
                        }
                        catch (Exception ex)
                        {
                            //tblMsg.Rows[0].Cells[0].InnerText = "Mail Sending Error.\n" + ex.Message;
                            //tblMsg.Rows[1].Cells[0].InnerText = "";
                            //ModalPopupExtenderMsg.Show();
                        }
                    }
                }
                #endregion

                #region Forward
                if (e.CommandName == "Forward")
                {
                    if (dtPndLvDet.Rows.Count > 0)
                    {
                        taEmpLvApp.InsertLeaveApp(nextLvDetRefNo, dtPndLvDet[0].EmpRefNo, dtPndLvDet[0].LvDetLvType, dtPndLvDet[0].LvDetPeriod, dtPndLvDet[0].LvDetMonth,
                            dtPndLvDet[0].LvDetYear, dtPndLvDet[0].LvDetStDate, dtPndLvDet[0].LvDetEndDate, dtPndLvDet[0].LvDetDays, dtPndLvDet[0].LvDetCmnt, "1", "Forwarded",
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), DateTime.Now, DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), dtPndLvDet[0].LvDetExtData1,
                            dtPndLvDet[0].LvDetExtData2, "", "", "", "0", dtPndLvDet[0].LvDetFlag);

                        myTran.Commit();
                        //tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                        //tblMsg.Rows[1].Cells[0].InnerText = "";
                        //ModalPopupExtenderMsg.Show();
                    }
                }
                #endregion

                #region Approve
                if (e.CommandName == "Approve")
                {
                    if (dtPndLvDet.Rows.Count > 0)
                    {
                        for (int i = 0; i <= (dtPndLvDet[0].LvDetEndDate.Subtract(dtPndLvDet[0].LvDetStDate).Days); i++)
                        {
                            var dtAttnd = taAtnd.GetDataByEmp(dtPndLvDet[0].EmpRefNo.ToString(), Convert.ToDateTime(dtPndLvDet[0].LvDetStDate.ToString()).AddDays(i));
                            if (dtAttnd.Rows.Count > 0)
                            {
                                myTran.Rollback();
                                tblMsg.Rows[0].Cells[0].InnerText = "Leave approval can not be proceed.";
                                if (dtAttnd[0].AttndFlag == "L")
                                    tblMsg.Rows[1].Cells[0].InnerText = "Already leave taken for the date " + Convert.ToDateTime(dtPndLvDet[0].LvDetStDate.ToString()).AddDays(i).ToString("dd/MM/yyyy");
                                if (dtAttnd[0].AttndFlag == "P")
                                    tblMsg.Rows[1].Cells[0].InnerText = "Attendance found for the date " + Convert.ToDateTime(dtPndLvDet[0].LvDetStDate.ToString()).AddDays(i).ToString("dd/MM/yyyy");
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                        }

                        taEmpLvApp.InsertLeaveApp(nextLvDetRefNo, dtPndLvDet[0].EmpRefNo, dtPndLvDet[0].LvDetLvType, dtPndLvDet[0].LvDetPeriod, dtPndLvDet[0].LvDetMonth,
                            dtPndLvDet[0].LvDetYear, dtPndLvDet[0].LvDetStDate, dtPndLvDet[0].LvDetEndDate, dtPndLvDet[0].LvDetDays, dtPndLvDet[0].LvDetCmnt, "2", "Approved",
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), DateTime.Now, DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), dtPndLvDet[0].LvDetExtData1,
                            dtPndLvDet[0].LvDetExtData2, "", "", "", "1", dtPndLvDet[0].LvDetFlag);

                        var dtMaxLvRef = taEmpLv.GetMaxLvRef(Convert.ToDateTime(dtPndLvDet[0].LvDetStDate).Year);
                        var nextLvRef = dtMaxLvRef == null ? "000001" : (Convert.ToInt32(dtMaxLvRef) + 1).ToString("000000");
                        var nextLvRefNo = "LV-" + Convert.ToDateTime(dtPndLvDet[0].LvDetStDate).Year + "-" + nextLvRef;

                        taEmpLv.InsertEmpLv(nextLvRefNo, dtPndLvDet[0].EmpRefNo, dtPndLvDet[0].LvDetLvType, dtPndLvDet[0].LvDetPeriod, dtPndLvDet[0].LvDetMonth,
                            dtPndLvDet[0].LvDetYear, dtPndLvDet[0].LvDetStDate, dtPndLvDet[0].LvDetEndDate, dtPndLvDet[0].LvDetDays, dtPndLvDet[0].LvDetCmnt, "1", "Approved",
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), DateTime.Now, DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), dtPndLvDet[0].LvDetExtData1,
                            dtPndLvDet[0].LvDetExtData2, "", "", dtPndLvDet[0].LvDetFlag, "1", "");

                        var shiftRef = "0";
                        var shiftIn = DateTime.Now.ToString("hh:mm tt");
                        double shiftInGrace = 0;
                        double shiftLate = 0;
                        var shiftOut = DateTime.Now.ToString("hh:mm tt");
                        double shiftOutGrace = 0;
                        double shiftEarly = 0;
                        var totHr = "0";
                        double otMin = 0;

                        var dtEmp = taEmpOff.GetDataByEmpRef(dtPndLvDet[0].EmpRefNo.ToString());
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

                        for (int i = 0; i <= (dtPndLvDet[0].LvDetEndDate.Subtract(dtPndLvDet[0].LvDetStDate).Days); i++)
                        {
                            var dtAttnd = taAtnd.GetDataByEmp(dtPndLvDet[0].EmpRefNo.ToString(), Convert.ToDateTime(dtPndLvDet[0].LvDetStDate.ToString()).AddDays(i));
                            if (dtAttnd.Rows.Count > 0)
                            {
                                taAtnd.UpdateAttnd(shiftRef.ToString(), atnInTime.ToString("hh:mm tt"), shiftIn.ToString(), shiftInGrace.ToString(), shiftLate.ToString(),
                                    atnOutTime.ToString("hh:mm tt"), shiftOut.ToString(), shiftOutGrace.ToString(), shiftEarly.ToString(), totHr.ToString(),
                                    otMin.ToString(), 1, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                    "", "", nextLvRefNo, "1", "L", dtPndLvDet[0].EmpRefNo.ToString(), Convert.ToDateTime(dtPndLvDet[0].LvDetStDate.ToString()).AddDays(i));
                            }
                            else
                            {
                                taAtnd.InsertAttnd(dtPndLvDet[0].EmpRefNo.ToString(), Convert.ToDateTime(dtPndLvDet[0].LvDetStDate.ToString()).AddDays(i), shiftRef.ToString(),
                                    atnInTime.ToString("hh:mm tt"), shiftIn.ToString(), shiftInGrace.ToString(), shiftLate.ToString(),
                                    atnOutTime.ToString("hh:mm tt"), shiftOut.ToString(), shiftOutGrace.ToString(), shiftEarly.ToString(), totHr.ToString(),
                                    otMin.ToString(), 1, DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                    "", "", nextLvRefNo, "1", "L");
                            }
                        }

                        myTran.Commit();
                        //tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                        //tblMsg.Rows[1].Cells[0].InnerText = "";
                        //ModalPopupExtenderMsg.Show();

                        try
                        {
                            //Send Mail
                            var mBody = MsgBody(dtPndLvDet[0].LvDetRef.ToString());

                            var taEmpBas = new View_Emp_BascTableAdapter();
                            //var dtEmpBas = new dsEmpDet.View_Emp_BascDataTable();

                            //dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(dtPndLvDet[0].EmpRefNo));
                            //var supRef = dtEmpBas[0].EmpSuprId;
                            //dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(supRef));
                            //var mailBcc = dtEmpBas.Rows.Count > 0 ? (dtEmpBas[0].IsEmpOffEmailNull() ? "" : dtEmpBas[0].EmpOffEmail.ToString()) : "";

                            //dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(dtPndLvDet[0].LvDetExtData1));
                            //var mailCc = dtEmpBas.Rows.Count > 0 ? (dtEmpBas[0].IsEmpOffEmailNull() ? "" : dtEmpBas[0].EmpOffEmail.ToString()) : "";

                            var dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(dtPndLvDet[0].EmpRefNo));
                            var mailTo = dtEmpBas.Rows.Count > 0 ? (dtEmpBas[0].IsEmpOffEmailNull() ? "" : dtEmpBas[0].EmpOffEmail.ToString()) : "";
                            var supRef = dtEmpBas[0].EmpSuprId;

                            var mailCc = "";
                            var mailBcc = "";
                            
                            if (supRef == "000586")
                            {
                                DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail(mailTo, mailCc, "iftekhar@doreen.com", "Leave Application Approved", mBody);
                            }
                            else
                            {
                                DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail(mailTo, mailCc, mailBcc, "Leave Application Approved", mBody);
                            }
                        }
                        catch (Exception ex)
                        {
                            //tblMsg.Rows[0].Cells[0].InnerText = "Mail Sending Error.\n" + ex.Message;
                            //tblMsg.Rows[1].Cells[0].InnerText = "";
                            //ModalPopupExtenderMsg.Show();
                        }

                        //var taEmpGenInfo = new View_Emp_BascTableAdapter();
                        //var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRef(Session["sessionEmpRef"] == null ? 0 : Convert.ToInt32(Session["sessionEmpRef"].ToString()));
                        //if (dtEmpGenInfo.Rows.Count > 0)
                        //{
                        //    var taPendLvDet = new ViewHrmsEmpLeaveAppTableAdapter();
                        //    var dtPendLvDet = taPendLvDet.GetDataBySupRef(dtEmpGenInfo[0].EmpSuprId.ToString());
                        //    gvEmpPendLv.DataSource = dtPendLvDet;
                        //    gvEmpPendLv.DataBind();
                        //}                        
                    }

                }
                #endregion

                var taPendLvDet = new ViewHrmsEmpLeaveAppTableAdapter();
                var suprRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                if (suprRef == "000634" || suprRef == "000884" || suprRef == "000011")//HRD,Nahamad,imran
                    gvEmpPendLv.DataSource = taPndLvDet.GetDataByHrdRef(suprRef);
                else if (suprRef == "100001") //Admin User
                    gvEmpPendLv.DataSource = taPndLvDet.GetDataByAdminUser();
                else
                    gvEmpPendLv.DataSource = taPndLvDet.GetDataBySupRef(suprRef);
                gvEmpPendLv.DataBind();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data processing error. " + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
            #endregion
        }

        protected void gvEmpPendLv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            var userRef = Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hfEmpRef = ((HiddenField)e.Row.FindControl("hfEmpRef"));
                var hfEmpSupRef = ((HiddenField)e.Row.FindControl("hfEmpSupRef"));

                var btnAppr = ((Button)e.Row.FindControl("btnApprove"));
                var btnFwd = ((Button)e.Row.FindControl("btnForward"));

                if (empRef == "000634" || empRef == "000884" || empRef == "000555" || empRef == "000011")//HRD,Nahamad,ceo,imran
                {
                    if (empRef == "000634" || empRef == "000884")//HRD,Nahamad
                        btnFwd.Visible = false;

                    btnAppr.Visible = true;
                }
                else
                    btnAppr.Visible = false;

                if (userRef == "100290" || userRef == "100291") //VC,SMD Sir
                    btnFwd.Text = "Approve";

                System.Web.UI.WebControls.Image UsrImages = (System.Web.UI.WebControls.Image)e.Row.FindControl("AttachImage");
                var empRefNo = ((HiddenField)e.Row.FindControl("hfEmpRef")).Value;
                UsrImages.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRefNo + "'";
            }
        }

        private string MsgBody(string lvDetRef)
        {
            string str = "";
            try
            {
                var taLvMas = new tblHrmsLeaveMasTableAdapter();
                var taEmpLvApp = new tblHrmsEmpLeaveAppTableAdapter();

                var dtPndLvDet = taEmpLvApp.GetDataByPendLvRef(Convert.ToInt32(lvDetRef.ToString()));

                var lvStDt = Convert.ToDateTime(dtPndLvDet[0].LvDetStDate).ToString("dd/MM/yyyy");

                var lvEndDt = Convert.ToDateTime(dtPndLvDet[0].LvDetEndDate).ToString("dd/MM/yyyy");

                var empref = dtPndLvDet[0].EmpRefNo.ToString(); //Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                var dtLvMas = taLvMas.GetDataByRefNo(Convert.ToInt32(dtPndLvDet[0].LvDetLvType));

                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRef(Convert.ToInt32(empref));

                str = "Leave Approved from : " + lvStDt.ToString() + " to " + lvEndDt.ToString() + ", " + (Convert.ToDateTime(dtPndLvDet[0].LvDetEndDate).Subtract(Convert.ToDateTime(dtPndLvDet[0].LvDetStDate)).Days + 1).ToString() + " Day(s).";
                str += "\n\n------------------------ DETAIL INFORMATION -------------------------------";
                str += "\nName                      : " + dtEmp[0].EmpName.ToString();
                str += "\nDepartment                : " + dtEmp[0].DeptName.ToString();
                str += "\nDesignation               : " + dtEmp[0].DesigName.ToString();
                str += "\nLeave Type                : " + dtLvMas[0].LvMasName;
                str += "\nDate From                 : " + Convert.ToDateTime(dtPndLvDet[0].LvDetStDate).ToString("dd/MM/yyyy");
                str += "\nDate To                   : " + Convert.ToDateTime(dtPndLvDet[0].LvDetEndDate).ToString("dd/MM/yyyy");
                str += "\nTotal Days                : " + (Convert.ToDateTime(dtPndLvDet[0].LvDetEndDate).Subtract(Convert.ToDateTime(dtPndLvDet[0].LvDetStDate)).Days + 1).ToString();
                str += "\nRemarks                   : " + dtPndLvDet[0].LvDetCmnt.ToString();
                str += "\nResp. Handed Over To      : " + dtPndLvDet[0].LvDetExtData2.ToString();
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
    }
}