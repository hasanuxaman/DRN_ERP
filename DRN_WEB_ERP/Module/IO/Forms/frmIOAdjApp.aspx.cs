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
    public partial class frmIOAdjApp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Auto  Page Refresh
            //Response.AppendHeader("Refresh", "60");

            if (Page.IsPostBack) return;

            try
            {
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                var taPendIoAdjApp = new ViewIoAdjAppTableAdapter();
                var dtPndLIoAdjApp = new DataTable();
                //if (empRef == "000071" || empRef == "000069" || empRef == "000011")//Cashier GROUP & ECIL-----0
                if (empRef == "000069" || empRef == "000011")//Cashier GROUP & ECIL-----0
                {
                    dtPndLIoAdjApp = taPendIoAdjApp.GetDataByCashGroupEcil();
                }
                else if (empRef == "000080" || empRef == "000224" || empRef == "000011")//Cashier DDL & DECL-----0
                {
                    dtPndLIoAdjApp = taPendIoAdjApp.GetDataByCashDdlDecl();
                }
                gvEmpPendIoAdj.DataSource = dtPndLIoAdjApp;
                gvEmpPendIoAdj.DataBind();
            }
            catch (Exception ex) { }
        }

        protected void gvEmpPendIoAdj_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Image UsrImages = (System.Web.UI.WebControls.Image)e.Row.FindControl("AttachImage");
                var empRefNo = ((HiddenField)e.Row.FindControl("hfEmpRef")).Value;
                UsrImages.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRefNo + "'";

                var hfIoAdjRef = (HiddenField)(e.Row.FindControl("hfIoAdjRef"));
                var taIoAdjApp = new tblAccIoAdjAppTableAdapter();
                var dtPndIoAdj = taIoAdjApp.GetDataByIoAdjRef(Convert.ToInt32(hfIoAdjRef.Value.ToString()));
                if (dtPndIoAdj.Rows.Count > 0)
                {
                    var btnRcv = (Button)(e.Row.FindControl("btnReceive"));
                    if (dtPndIoAdj[0].AdjStatRef.ToString() == "1") btnRcv.Visible = false;
                }
            }
        }

        protected void gvShowApprAdj_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Image UsrImages = (System.Web.UI.WebControls.Image)e.Row.FindControl("AttachImage");
                var empRefNo = ((HiddenField)e.Row.FindControl("hfEmpRef")).Value;
                UsrImages.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRefNo + "'";
            }
        }

        protected void gvEmpPendIoAdj_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region Update I/O Adjustment Status
            var taIoAdjApp = new tblAccIoAdjAppTableAdapter();
            var taIoAdj = new tblAccIoAdjTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taIoAdjApp.Connection);
            try
            {
                taIoAdjApp.AttachTransaction(myTran);
                taIoAdj.AttachTransaction(myTran);

                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = gvEmpPendIoAdj.Rows[index];

                var hfIoAdjRef = (HiddenField)(row.Cells[2].FindControl("hfIoAdjRef"));

                var dtPndIo = taIoAdjApp.GetDataByIoAdjRef(Convert.ToInt32(hfIoAdjRef.Value.ToString()));

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
                        taIoAdjApp.InsertIoAdjApp(dtPndIo[0].IoAdjRefNo, dtPndIo[0].EmpRefNo, dtPndIo[0].AdjBusUnit, Convert.ToDecimal(dtPndIo[0].AdjAmount),
                            dtPndIo[0].AdjDate, dtPndIo[0].AdjIoReqRef, dtPndIo[0].AdjIoReqRefNo, dtPndIo[0].AdjMprNo, dtPndIo[0].AdjQuotNo, dtPndIo[0].AdjWoNo,
                            dtPndIo[0].AdjBillNo, dtPndIo[0].AdjSiteChlnNo, dtPndIo[0].AdjMrrNo, dtPndIo[0].AdjCmnt, (Convert.ToInt32(dtPndIo[0].AdjStatRef) - 1).ToString(),
                            "Rejected By: " + evntEmpName, evntEmpRef, evntEmpName, DateTime.Now, DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "", "", "0", "");

                        myTran.Commit();
                        tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }

                if (e.CommandName == "Receive")
                {
                    if (dtPndIo.Rows.Count > 0)
                    {
                        taIoAdjApp.InsertIoAdjApp(dtPndIo[0].IoAdjRefNo, dtPndIo[0].EmpRefNo, dtPndIo[0].AdjBusUnit, Convert.ToDecimal(dtPndIo[0].AdjAmount),
                            dtPndIo[0].AdjDate, dtPndIo[0].AdjIoReqRef, dtPndIo[0].AdjIoReqRefNo, dtPndIo[0].AdjMprNo, dtPndIo[0].AdjQuotNo, dtPndIo[0].AdjWoNo,
                            dtPndIo[0].AdjBillNo, dtPndIo[0].AdjSiteChlnNo, dtPndIo[0].AdjMrrNo, dtPndIo[0].AdjCmnt, (Convert.ToInt32(dtPndIo[0].AdjStatRef) + 1).ToString(),
                            "Received By: " + evntEmpName, evntEmpRef, evntEmpName, DateTime.Now, DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "", "", "0", "");

                        myTran.Commit();
                        tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }

                if (e.CommandName == "Post")
                {
                    if (dtPndIo.Rows.Count > 0)
                    {
                        taIoAdjApp.InsertIoAdjApp(dtPndIo[0].IoAdjRefNo, dtPndIo[0].EmpRefNo, dtPndIo[0].AdjBusUnit, Convert.ToDecimal(dtPndIo[0].AdjAmount),
                            dtPndIo[0].AdjDate, dtPndIo[0].AdjIoReqRef, dtPndIo[0].AdjIoReqRefNo, dtPndIo[0].AdjMprNo, dtPndIo[0].AdjQuotNo, dtPndIo[0].AdjWoNo,
                            dtPndIo[0].AdjBillNo, dtPndIo[0].AdjSiteChlnNo, dtPndIo[0].AdjMrrNo, dtPndIo[0].AdjCmnt, (Convert.ToInt32(dtPndIo[0].AdjStatRef) + 1).ToString(),
                            "Posted By: " + evntEmpName, evntEmpRef, evntEmpName, DateTime.Now, DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "", "", "1", "");

                        var dtMaxIoAdjRef = taIoAdj.GetMaxIoAdjRef();
                        var nextIoAdjRef = dtMaxIoAdjRef == null ? "000001" : (Convert.ToInt32(dtMaxIoAdjRef) + 1).ToString();
                        var nextIoAdjRefNo = "IO-ADJ-" + DateTime.Now.ToString("MM") + "-" + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextIoAdjRef).ToString("000000");

                        taIoAdj.InsertIoAdj(Convert.ToInt32(nextIoAdjRef), nextIoAdjRefNo, dtPndIo[0].EmpRefNo, dtPndIo[0].AdjBusUnit, Convert.ToDecimal(dtPndIo[0].AdjAmount),
                            dtPndIo[0].AdjDate, dtPndIo[0].AdjIoReqRef, dtPndIo[0].AdjIoReqRefNo, dtPndIo[0].AdjMprNo, dtPndIo[0].AdjQuotNo, dtPndIo[0].AdjWoNo,
                            dtPndIo[0].AdjBillNo, dtPndIo[0].AdjSiteChlnNo, dtPndIo[0].AdjMrrNo, dtPndIo[0].AdjCmnt, (Convert.ToInt32(dtPndIo[0].AdjStatRef) + 1).ToString(),
                            "Posted By: " + evntEmpName, evntEmpRef, evntEmpName, DateTime.Now, DateTime.Now,
                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", "", "", "", "", "1", "");

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

                var taPendIoAdjApp = new ViewIoAdjAppTableAdapter();
                var dtPndLIoAdjApp = new DataTable();
                //if (empRef == "000071" || empRef == "000069" || empRef == "000011")//Cashier GROUP & ECIL-----0
                if (empRef == "000069" || empRef == "000011")//Cashier GROUP & ECIL-----0
                {
                    dtPndLIoAdjApp = taPendIoAdjApp.GetDataByCashGroupEcil();
                }
                else if (empRef == "000080" || empRef == "000224" || empRef == "000011")//Cashier DDL & DECL-----0
                {
                    dtPndLIoAdjApp = taPendIoAdjApp.GetDataByCashDdlDecl();
                }
                gvEmpPendIoAdj.DataSource = dtPndLIoAdjApp;
                gvEmpPendIoAdj.DataBind();
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

        public string GetAdjStatus(string IoAdjRefNo)
        {
            string ioAdjStat = "";
            try
            {
                var taIoAdjStat = new tblAccIoAdjAppTableAdapter();
                var dtIoAdjStat = taIoAdjStat.GetDataByIoAdjRefNo(IoAdjRefNo.ToString());
                foreach (DRN_WEB_ERP.Module.IO.DataSet.dsIO.tblAccIoAdjAppRow dr in dtIoAdjStat.Rows)
                {
                    if (ioAdjStat.Length > 0)
                        ioAdjStat = ioAdjStat + " >>" + dr.AdjStatDesc.ToString() + " on " + dr.AdjStatByDate.ToString() + ".<br />";
                    else
                        ioAdjStat = ">>" + dr.AdjStatDesc.ToString() + " on " + dr.AdjStatByDate.ToString() + ".<br />";
                }
                return ioAdjStat;
            }
            catch (Exception) { return ioAdjStat; }
        }
        #endregion

        protected void btnShowRpt_Click(object sender, EventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();            

            var taPendIoAdjApp = new ViewIoAdjAppTableAdapter();
            var dtPndLIoAdjApp = new DataTable();
            //if (empRef == "000071" || empRef == "000069" || empRef == "000011")//Cashier GROUP & ECIL-----0
            if (empRef == "000069" || empRef == "000011")//Cashier GROUP & ECIL---->1            
                dtPndLIoAdjApp = taPendIoAdjApp.GetDataByCashGroupEcilRpt();
            else if (empRef == "000080" || empRef == "000224" || empRef == "000011")//Cashier DDL & DECL---->1            
                dtPndLIoAdjApp = taPendIoAdjApp.GetDataByCashDdlDeclRpt();
            else
                dtPndLIoAdjApp = taPendIoAdjApp.GetDataBySupIdRpt(empRef.ToString());

            gvShowApprAdj.DataSource = dtPndLIoAdjApp;
            gvShowApprAdj.DataBind();

            gvShowApprAdj.Visible = gvShowApprAdj.Visible ? false : true;

            if (btnShowRpt.Text == "Show Approved Adjustment")
                btnShowRpt.Text = "Hide Approved Adjustment";
            else
                btnShowRpt.Text = "Show Approved Adjustment";
        }
    }
}