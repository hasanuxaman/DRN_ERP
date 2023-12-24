using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.IO.DataSet.dsIOTableAdapters;

namespace DRN_WEB_ERP.Module.IO.Forms
{
    public partial class frmIOReqApp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Auto  Page Refresh
            //Response.AppendHeader("Refresh", "60");

            if (Page.IsPostBack) return;

            try
            {
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                
                var taPendIoApp = new ViewIOReqAppTableAdapter();
                var dtPndLIoApp = new DataTable();
                if (empRef == "000568" || empRef == "000011")//Head of F & A -----1
                {
                    dtPndLIoApp = taPendIoApp.GetDataByHOFnA();
                }
                else if (empRef == "000555" || empRef == "000011")//CEO---------2
                {
                    dtPndLIoApp = taPendIoApp.GetDataByCEO();
                }
                else if (empRef == "000002" || empRef == "000011")//Mamun Sir for DECL---------2
                {
                    dtPndLIoApp = taPendIoApp.GetDataByCEODecl();
                }
                //else if (empRef == "000071" || empRef == "000069" || empRef == "000011")//Cashier GROUP & ECIL-----3
                else if (empRef == "000069" || empRef == "000011")//Cashier GROUP & ECIL-----3
                {
                    dtPndLIoApp = taPendIoApp.GetDataByCashGroupEcil();
                }
                else if (empRef == "000080" || empRef == "000224" || empRef == "000011")//Cashier DDL & DECL-----3
                {
                    dtPndLIoApp = taPendIoApp.GetDataByCashDdlDecl();
                }
                else
                    dtPndLIoApp = taPendIoApp.GetDataBySupId(empRef);

                gvEmpPendIo.DataSource = dtPndLIoApp;
                gvEmpPendIo.DataBind();
            }
            catch (Exception ex) { }
        }

        protected void gvEmpPendIo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hfIoRef = ((HiddenField)e.Row.FindControl("hfIoRef"));
                var btnAppr = ((Button)e.Row.FindControl("btnApprove"));
                var btnFwd = ((Button)e.Row.FindControl("btnForward"));

                //if (empRef == "000071" || empRef == "000069" || empRef == "000080" || empRef == "000224")
                if (empRef == "000069" || empRef == "000080" || empRef == "000224")
                {
                    var statRef = "";
                    var taIoStat = new tblAccIoReqAppTableAdapter();
                    var dtIoStat = taIoStat.GetDataByIoReqRef(Convert.ToInt32(hfIoRef.Value.ToString()));
                    if (dtIoStat.Rows.Count > 0) statRef = dtIoStat[0].ReqStatRef.ToString();
                    if (statRef == "0")
                    {
                        btnFwd.Visible = true;
                        btnAppr.Visible = false;
                    }
                    else
                    {
                        btnFwd.Visible = false;
                        btnAppr.Visible = true;
                    }
                }
                else
                    btnAppr.Visible = false;

                System.Web.UI.WebControls.Image UsrImages = (System.Web.UI.WebControls.Image)e.Row.FindControl("AttachImage");
                var empRefNo = ((HiddenField)e.Row.FindControl("hfEmpRef")).Value;
                UsrImages.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRefNo + "'";
            }
        }

        protected void gvShowApprIo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Image UsrImages = (System.Web.UI.WebControls.Image)e.Row.FindControl("AttachImage");
                var empRefNo = ((HiddenField)e.Row.FindControl("hfEmpRef")).Value;
                UsrImages.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRefNo + "'";
            }
        }

        protected void gvEmpPendIo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region Update I/O Requisition Status
            var taPendIoApp = new tblAccIoReqAppTableAdapter();
            var taIoReq = new tblAccIoReqTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taPendIoApp.Connection);
            try
            {
                taPendIoApp.AttachTransaction(myTran);
                taIoReq.AttachTransaction(myTran);

                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = gvEmpPendIo.Rows[index];

                var hfIoRef = (HiddenField)(row.Cells[2].FindControl("hfIoRef"));

                var dtPndIo = taPendIoApp.GetDataByIoReqRef(Convert.ToInt32(hfIoRef.Value.ToString()));

                var taEmp = new View_Emp_BascTableAdapter();
                var evntEmpRef = "0";
                var evntEmpName = "";
                var dtEmpBasInfo = taEmp.GetDataByEmpRefAct(Session["sessionEmpRef"] == null ? 0 : Convert.ToInt32(Session["sessionEmpRef"].ToString()));
                if (dtEmpBasInfo.Rows.Count > 0)
                {
                    evntEmpRef = dtEmpBasInfo[0].EmpRefNo.ToString();
                    evntEmpName = dtEmpBasInfo[0].EmpName.ToString();
                }

                if (e.CommandName == "Reject")
                {
                    if (dtPndIo.Rows.Count > 0)
                    {
                        taPendIoApp.InsertIoReqApp(dtPndIo[0].IoReqRefNo, dtPndIo[0].EmpRefNo, dtPndIo[0].ReqBusUnit, Convert.ToDecimal(dtPndIo[0].ReqAmount),
                                dtPndIo[0].ReqDate, dtPndIo[0].ReqCmnt, (Convert.ToInt32(dtPndIo[0].ReqStatRef) - 1).ToString(),
                                "Rejected By: " + evntEmpName, evntEmpRef, evntEmpName, DateTime.Now, DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "", "", "0", "");

                        myTran.Commit();
                        tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }

                if (e.CommandName == "Forward")
                {
                    if (dtPndIo.Rows.Count > 0)
                    {
                        taPendIoApp.InsertIoReqApp(dtPndIo[0].IoReqRefNo, dtPndIo[0].EmpRefNo, dtPndIo[0].ReqBusUnit, Convert.ToDecimal(dtPndIo[0].ReqAmount),
                                dtPndIo[0].ReqDate, dtPndIo[0].ReqCmnt, (Convert.ToInt32(dtPndIo[0].ReqStatRef) + 1).ToString(),
                                "Approved By: " + evntEmpName, evntEmpRef, evntEmpName, DateTime.Now, DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "", "", "0", "");

                        myTran.Commit();
                        tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }

                if (e.CommandName == "Approve")
                {
                    if (dtPndIo.Rows.Count > 0)
                    {
                        taPendIoApp.InsertIoReqApp(dtPndIo[0].IoReqRefNo, dtPndIo[0].EmpRefNo, dtPndIo[0].ReqBusUnit, Convert.ToDecimal(dtPndIo[0].ReqAmount),
                                dtPndIo[0].ReqDate, dtPndIo[0].ReqCmnt, (Convert.ToInt32(dtPndIo[0].ReqStatRef) + 1).ToString(),
                                "Posted By: " + evntEmpName, evntEmpRef, evntEmpName, DateTime.Now, DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "", "", "1", "");

                        var dtMaxIoReqRef = taIoReq.GetMaxIoReqRef();
                        var nextIoReqRef = dtMaxIoReqRef == null ? "000001" : (Convert.ToInt32(dtMaxIoReqRef) + 1).ToString();
                        var nextIoReqRefNo = "IO-" + DateTime.Now.ToString("MM") + "-" + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextIoReqRef).ToString("000000");

                        taIoReq.InsertIoReq(Convert.ToInt32(nextIoReqRef), nextIoReqRefNo, dtPndIo[0].EmpRefNo, dtPndIo[0].ReqBusUnit, Convert.ToDecimal(dtPndIo[0].ReqAmount),
                                dtPndIo[0].ReqDate, dtPndIo[0].ReqCmnt, (Convert.ToInt32(dtPndIo[0].ReqStatRef) + 1).ToString(),
                                "Posted By: " + evntEmpName, evntEmpRef, evntEmpName, DateTime.Now, DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), dtPndIo[0].IoReqRefNo, "", "", "", "", "1", "");

                        myTran.Commit();
                        tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();                        

                        try
                        {
                            ////Send Mail
                            //var mBody = MsgBody(dtPndLvDet[0].LvDetRef.ToString());

                            //var taEmpBas = new View_Emp_BascTableAdapter();                            

                            //var dtEmpBas = taEmpBas.GetDataByEmpRef(Convert.ToInt32(dtPndLvDet[0].EmpRefNo));
                            //var mailTo = dtEmpBas.Rows.Count > 0 ? (dtEmpBas[0].IsEmpOffEmailNull() ? "" : dtEmpBas[0].EmpOffEmail.ToString()) : "";

                            //var mailCc = "";
                            //var mailBcc = "";

                            //DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail(mailTo, mailCc, mailBcc, "Leave Application Approved", mBody);
                        }
                        catch (Exception ex)
                        {
                            //tblMsg.Rows[0].Cells[0].InnerText = "Mail Sending Error.\n" + ex.Message;
                            //tblMsg.Rows[1].Cells[0].InnerText = "";
                            //ModalPopupExtenderMsg.Show();
                        }                        
                    }
                }

                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                var taPendIoReqApp = new ViewIOReqAppTableAdapter();
                var dtPndLIoApp = new DataTable();
                if (empRef == "000568" || empRef == "000011")//Head of F & A -----1
                {
                    dtPndLIoApp = taPendIoReqApp.GetDataByHOFnA();
                }
                else if (empRef == "000555" || empRef == "000011")//CEO---------2
                {
                    dtPndLIoApp = taPendIoReqApp.GetDataByCEO();
                }
                else if (empRef == "000002" || empRef == "000011")//Mamun Sir for DECL---------2
                {
                    dtPndLIoApp = taPendIoReqApp.GetDataByCEODecl();
                }
                //else if (empRef == "000071" || empRef == "000069" || empRef == "000011")//Cashier GROUP & ECIL-----3
                else if (empRef == "000069" || empRef == "000011")//Cashier GROUP & ECIL-----3
                {
                    dtPndLIoApp = taPendIoReqApp.GetDataByCashGroupEcil();
                }
                else if (empRef == "000080" || empRef == "000224" || empRef == "000011")//Cashier DDL & DECL-----3
                {
                    dtPndLIoApp = taPendIoReqApp.GetDataByCashDdlDecl();
                }
                else
                    dtPndLIoApp = taPendIoReqApp.GetDataBySupId(empRef);

                gvEmpPendIo.DataSource = dtPndLIoApp;
                gvEmpPendIo.DataBind();
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

        #region GridData
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

        public string GetIoStatus(string IoReqRefNo)
        {
            string ioStat = "";
            try
            {
                var taIoStat = new tblAccIoReqAppTableAdapter();
                var dtIoStat = taIoStat.GetDataByIoReqRefNo(IoReqRefNo.ToString());
                foreach (DRN_WEB_ERP.Module.IO.DataSet.dsIO.tblAccIoReqAppRow dr in dtIoStat.Rows)
                {
                    if (ioStat.Length > 0)
                        ioStat = ioStat + " >>" + dr.ReqStatDesc.ToString() + " on " + dr.ReqStatByDate.ToString() + ".<br />";
                    else
                        ioStat = ">>" + dr.ReqStatDesc.ToString() + " on " + dr.ReqStatByDate.ToString() + ".<br />";
                }
                return ioStat;
            }
            catch (Exception) { return ioStat; }
        }

        public string GetIoUnAdj(string empRef)
        {
            string ioUnAdj = "0";
            try
            {
                var taIoReq = new tblAccIoReqTableAdapter();
                var taIoAdj = new tblAccIoAdjTableAdapter();

                var dtWithdraw = taIoReq.GetWithdrawAmt(empRef.ToString());
                double IoWithdraw = dtWithdraw == null ? Convert.ToDouble(0) : Convert.ToDouble(dtWithdraw);

                var dtAdjust = taIoAdj.GetAdjustAmount(empRef.ToString());
                double IoAdjust = dtAdjust == null ? Convert.ToDouble(0) : Convert.ToDouble(dtAdjust);

                ioUnAdj = (Convert.ToDouble(dtWithdraw) - IoAdjust).ToString("N", System.Globalization.CultureInfo.InvariantCulture);

                return ioUnAdj;
            }
            catch (Exception) { return ioUnAdj; }
        }
        #endregion

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {            
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            var taPendIoApp = new ViewIOReqAppTableAdapter();
            var dtPndLIoApp = new DataTable();
            if (empRef == "000568" || empRef == "000011")//Head of F & A ----->1
            {
                dtPndLIoApp = taPendIoApp.GetDataByHOFnARpt();
            }
            else if (empRef == "000555" || empRef == "000011")//CEO-------->2
            {
                dtPndLIoApp = taPendIoApp.GetDataByCEORpt();
            }
            //else if (empRef == "000002" || empRef == "000011")//Mamun Sir for DECL---------2
            //{
            //    dtPndLIoApp = taPendIoApp.GetDataByCEORptDecl();
            //}
            //else if (empRef == "000071" || empRef == "000069" || empRef == "000011")//Cashier GROUP & ECIL-----3
            else if (empRef == "000069" || empRef == "000011")//Cashier GROUP & ECIL----->3
            {
                dtPndLIoApp = taPendIoApp.GetDataByCashGroupEcilRpt();
            }
            else if (empRef == "000080" || empRef == "000224" || empRef == "000011")//Cashier DDL & DECL---->3
            {
                dtPndLIoApp = taPendIoApp.GetDataByCashDdlDeclRpt();
            }
            else
                dtPndLIoApp = taPendIoApp.GetDataBySupIdRpt(empRef);

            gvShowApprIo.DataSource = dtPndLIoApp;
            gvShowApprIo.DataBind();

            gvShowApprIo.Visible = gvShowApprIo.Visible ? false : true;

            if (btnShowRpt.Text == "Show Approved IO")
                btnShowRpt.Text = "Hide Approved IO";
            else
                btnShowRpt.Text = "Show Approved IO";
        }
    }
}