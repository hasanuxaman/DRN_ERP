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
using DRN_WEB_ERP.Module.IO.DataSet;
using DRN_WEB_ERP.Module.IO.DataSet.dsIOTableAdapters;

namespace DRN_WEB_ERP.Module.IO
{
    public partial class frmIOReq : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            try
            {
                var taIoReqApp = new tblAccIoReqAppTableAdapter();
                var taIoLimit = new tblAccIoLimitTableAdapter();
                var taIoReq = new tblAccIoReqTableAdapter();
                var taIoAdj = new tblAccIoAdjTableAdapter();

                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                AutoCompleteExtenderEmp.ContextKey = "0";

                txtReqDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                
                var dtMaxIoReqRef = taIoReqApp.GetMaxIoReqRef();
                var nextIoReqRef = dtMaxIoReqRef == null ? "000001" : (Convert.ToInt32(dtMaxIoReqRef) + 1).ToString();
                txtIoRefNo.Text = "IO-" + Convert.ToInt32(nextIoReqRef).ToString("000000");

                var dtIo = taIoLimit.GetDataByEmpRef(empRef.ToString());
                double IoLimit = dtIo.Rows.Count > 0 ? Convert.ToDouble(dtIo[0].IoAccLimit) : Convert.ToDouble(0);

                var dtWithdraw = taIoReq.GetWithdrawAmt(empRef.ToString());
                double IoWithdraw = dtWithdraw == null ? Convert.ToDouble(0) : Convert.ToDouble(dtWithdraw);

                var dtAdjust = taIoAdj.GetAdjustAmount(empRef.ToString());
                double IoAdjust = dtAdjust == null ? Convert.ToDouble(0) : Convert.ToDouble(dtAdjust);

                lblIoLimit.Text = "Limit: " + IoLimit.ToString("#,##0.00");
                lblIoWithd.Text = "Withdrawn: " + IoWithdraw.ToString("#,##0.00");
                lblIoAdj.Text = "Adjusted: " + IoAdjust.ToString("#,##0.00");
                lblIoUnAdj.Text = "Unadjusted: " + (Convert.ToDouble(dtWithdraw) - IoAdjust).ToString("#,##0.00");
                lblIoBal.Text = "Available Limit: " + (IoLimit - (IoWithdraw - IoAdjust)).ToString("#,##0.00");

                var taEmpGenInfo = new View_Emp_BascTableAdapter();
                var dtEmpGenInfo = taEmpGenInfo.GetDataByEmpRefAct(Convert.ToInt32(empRef));
                if (dtEmpGenInfo.Rows.Count > 0)
                    txtEmpName.Text = dtEmpGenInfo[0].EmpRefNo.ToString() + ":" + dtEmpGenInfo[0].EmpId.ToString() + ":" + dtEmpGenInfo[0].EmpName.ToString();
                else
                    txtEmpName.Text = "";

                var taLoc = new tblHrmsOffLocTableAdapter();
                cboBusUnit.DataSource = taLoc.GetDataByAsc();
                cboBusUnit.DataValueField = "LocRefNo";
                cboBusUnit.DataTextField = "LocName";
                cboBusUnit.DataBind();
                cboBusUnit.Items.Insert(0, new ListItem("---Select---", "0"));

                var taPendIoReq = new ViewIOReqAppTableAdapter();
                var dtPendIoReq = taPendIoReq.GetDataByEmpRef(empRef.ToString());
                gvPendIoReq.DataSource = dtPendIoReq;
                gvPendIoReq.DataBind();

                var dtIoReq = taIoReq.GetDataByEmpRef(empRef.ToString());
                gvIoReq.DataSource = dtIoReq;
                gvIoReq.DataBind();
            }
            catch (Exception ex)
            {

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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            AutoCompleteExtenderEmp.ContextKey = "0";

            txtReqDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtIoReqAmnt.Text = "";
            txtIoReqCmnt.Text = "";
            cboBusUnit.SelectedIndex = 0;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var taIoReqApp = new tblAccIoReqAppTableAdapter();

            var taEmp = new View_Emp_BascTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taIoReqApp.Connection);

            try
            {
                taIoReqApp.AttachTransaction(myTran);             

                DateTime? ReqDt = null;
                if (txtReqDt.Text.Length > 0) ReqDt = Convert.ToDateTime(txtReqDt.Text.Trim());

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
                        var dtEmpGenInfo = taEmp.GetDataByEmpRef(Convert.ToInt32(empRef.ToString()));
                        if (dtEmpGenInfo.Rows.Count > 0)
                        {
                            var taIoLimit = new tblAccIoLimitTableAdapter();
                            var taIoReq = new tblAccIoReqTableAdapter();
                            var taIoAdj = new tblAccIoAdjTableAdapter();

                            var dtIo = taIoLimit.GetDataByEmpRef(empRef.ToString());
                            double IoLimit = dtIo.Rows.Count > 0 ? Convert.ToDouble(dtIo[0].IoAccLimit) : Convert.ToDouble(0);

                            var dtWithdraw = taIoReq.GetWithdrawAmt(empRef.ToString());
                            double IoWithdraw = dtWithdraw == null ? Convert.ToDouble(0) : Convert.ToDouble(dtWithdraw);

                            var dtAdjust = taIoAdj.GetAdjustAmount(empRef.ToString());
                            double IoAdjust = dtAdjust == null ? Convert.ToDouble(0) : Convert.ToDouble(dtAdjust);

                            if (Convert.ToDouble(txtIoReqAmnt.Text.Trim()) > IoLimit - (IoWithdraw - IoAdjust))
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "You are no allowed to exceed limit.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }

                            var evntEmpRef = "0";
                            var evntEmpName = "";
                            var dtEmpBasInfo = taEmp.GetDataByEmpRefAct(Session["sessionEmpRef"] == null ? 0 : Convert.ToInt32(Session["sessionEmpRef"].ToString()));
                            if (dtEmpBasInfo.Rows.Count > 0)
                            {
                                evntEmpRef = dtEmpBasInfo[0].EmpRefNo.ToString();
                                evntEmpName = dtEmpBasInfo[0].EmpName.ToString();
                            }

                            var dtMaxIoReqRef = taIoReqApp.GetMaxIoReqRef();
                            var nextIoReqRef = dtMaxIoReqRef == null ? "000001" : (Convert.ToInt32(dtMaxIoReqRef) + 1).ToString();
                            var nextIoReqRefNo = "IO-" + Convert.ToInt32(nextIoReqRef).ToString("000000");

                            taIoReqApp.InsertIoReqApp(nextIoReqRefNo, empRef.ToString(), cboBusUnit.SelectedValue.ToString(), Convert.ToDecimal(txtIoReqAmnt.Text.Trim()),
                                ReqDt, txtIoReqCmnt.Text.Trim(), "0", "Created By: " + evntEmpName, evntEmpRef, evntEmpName, DateTime.Now, DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "", "", "0", "");

                            myTran.Commit();

                            tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();

                            AutoCompleteExtenderEmp.ContextKey = "0";

                            txtIoRefNo.Text = "IO-" + Convert.ToInt32(nextIoReqRef).ToString("000000");
                            txtReqDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            txtIoReqAmnt.Text = "";
                            txtIoReqCmnt.Text = "";
                            cboBusUnit.SelectedIndex = 0;                            

                            var taPendIoReq = new ViewIOReqAppTableAdapter();
                            var dtPendIoReq = taPendIoReq.GetDataByEmpRef(empRef.ToString());
                            gvPendIoReq.DataSource = dtPendIoReq;
                            gvPendIoReq.DataBind();

                            //try
                            //{
                            //    //Send Mail
                            //    var mBody = MsgBody("");
                            //    var taEmpBas = new View_Emp_BascTableAdapter();
                            //    var dtEmpBas = new dsEmpDet.View_Emp_BascDataTable();

                            //    dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(empRef));
                            //    var supRef = dtEmpBas[0].EmpSuprId;
                            //    dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(supRef));
                            //    var mailTo = dtEmpBas.Rows.Count > 0 ? (dtEmpBas[0].IsEmpOffEmailNull() ? "" : dtEmpBas[0].EmpOffEmail.ToString()) : "";

                            //    dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(empRef));
                            //    var mailCc = dtEmpBas.Rows.Count > 0 ? (dtEmpBas[0].IsEmpOffEmailNull() ? "" : dtEmpBas[0].EmpOffEmail.ToString()) : "";

                            //    dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(empRef));
                            //    var mailBcc = dtEmpBas.Rows.Count > 0 ? (dtEmpBas[0].IsEmpOffEmailNull() ? "" : dtEmpBas[0].EmpOffEmail.ToString()) : "";

                            //    DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail(mailTo, mailCc, mailBcc, "Leave Application", mBody);
                            //}
                            //catch (Exception ex)
                            //{
                            //    //tblMsg.Rows[0].Cells[0].InnerText = "Mail Sending Error.\n" + ex.Message;
                            //    //tblMsg.Rows[1].Cells[0].InnerText = "";
                            //    //ModalPopupExtenderMsg.Show();
                            //}
                            //finally
                            //{
                            //    cboBusUnit.SelectedIndex = 0;
                            //    txtReqDt.Text = "";
                            //    txtIoReqCmnt.Text = "";
                            //}
                        }
                        else
                        {
                            myTran.Rollback();
                            tblMsg.Rows[0].Cells[0].InnerText = "Enter valid employee name.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Enter valid employee name.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();                        
                        return;
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

        private string MsgBody(string qtnNo)
        {
            string str = "";
            try
            {
                DateTime? lvStDt = null;
                if (txtReqDt.Text.Length > 0) lvStDt = Convert.ToDateTime(txtReqDt.Text.Trim());

                DateTime? lvEndDt = null;
                if (txtReqDt.Text.Length > 0) lvEndDt = Convert.ToDateTime(txtReqDt.Text.Trim());



                var empref = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt32(empref));

                str = dtEmp[0].EmpName.ToString() + " applied leave from : " + txtReqDt.Text.Trim() + " to " + txtReqDt.Text.Trim() + ", " + (lvEndDt.Value.Subtract(lvStDt.Value).Days + 1).ToString() + " Day(s).";
                str += "\n\n------------------------ DETAIL INFORMATION -------------------------------";
                str += "\nName                      : " + dtEmp[0].EmpName.ToString();
                str += "\nDepartment                : " + dtEmp[0].DeptName.ToString();
                str += "\nDesignation               : " + dtEmp[0].DesigName.ToString();
                str += "\nLeave Type                : " + cboBusUnit.SelectedItem.Text;
                str += "\nDate From                 : " + txtReqDt.Text.Trim();
                str += "\nDate To                   : " + txtReqDt.Text.Trim();
                str += "\nTotal Days                : " + (lvEndDt.Value.Subtract(lvStDt.Value).Days + 1).ToString();
                str += "\nRemarks                   : " + txtIoReqCmnt.Text.Trim();
                str += "\nResp. Handed Over To      : " + txtIoReqCmnt.Text.Trim();
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

        #region GridData
        public string GetBusUnit(string bUnit)
        {
            string busUnit = "";
            try
            {
                var taBusUnit = new tblHrmsOffLocTableAdapter();
                var dtBusUnit = taBusUnit.GetDataByLocRef(Convert.ToInt32(bUnit.ToString()));
                if (dtBusUnit.Rows.Count > 0)
                    busUnit = dtBusUnit[0].LocCode.ToString();
                return busUnit;
            }
            catch (Exception) { return busUnit; }
        }
        #endregion
    }
}