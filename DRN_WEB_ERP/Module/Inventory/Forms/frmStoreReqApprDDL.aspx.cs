using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Inventory.Forms
{
    public partial class frmStoreReqApprDDL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Auto  Page Refresh
            //Response.AppendHeader("Refresh", "60");

            if (Page.IsPostBack) return;

            try
            {
                var suprRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var taPoHdr = new View_Pro_Sr_DetTableAdapter();
                var dtPoHdr = new DataTable();
                if (suprRef == "000498" || suprRef == "000011")//Nayan Zahid DDL
                    dtPoHdr = taPoHdr.GetDataByApprDDL();
                gvPendPo.DataSource = dtPoHdr;
                gvPendPo.DataBind();
            }
            catch (Exception ex) { }
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

        public string GetCurStk(string strCode, string itemCode)
        {
            string curStk = "";
            try
            {
                var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();
                var dtStkCtl = taStkCtl.GetDataByStoreItem(strCode, itemCode);
                if (dtStkCtl.Rows.Count > 0)
                    curStk = Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk), 4).ToString();
                return curStk;
            }
            catch (Exception) { return curStk; }
        }
        #endregion

        protected void gvPendPo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region Update Requisition Status
            var taPoHdr = new tbl_InTr_Pro_Sr_HdrTableAdapter();
            var taPoDet = new tbl_InTr_Pro_Sr_DetTableAdapter();

            var dtPoHdr = new dsInvTran.tbl_InTr_Pro_Sr_HdrDataTable();
            var dtPoDet = new dsInvTran.tbl_InTr_Pro_Sr_DetDataTable();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taPoHdr.Connection);

            try
            {
                var supName="";
                var suprRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var taEmp=new View_Emp_BascTableAdapter();
                var dtEmp=taEmp.GetDataByEmpRefAct(Convert.ToInt32(suprRef.ToString()));
                if(dtEmp.Rows.Count>0) supName=dtEmp[0].EmpName;

                taPoHdr.AttachTransaction(myTran);
                taPoDet.AttachTransaction(myTran);

                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = gvPendPo.Rows[index];

                var hfPoRef = (HiddenField)(row.Cells[2].FindControl("hfPoRef"));
                var lblPoRefNo = (Label)(row.Cells[2].FindControl("lblPoRefNo"));
                var hfPoDetLno = (HiddenField)(row.Cells[2].FindControl("hfPoDetLno"));
                var lblPoItemCode = (Label)(row.Cells[2].FindControl("lblPoItemCode"));
                var lblPoReqQty = (Label)(row.Cells[2].FindControl("lblPoReqQty"));
                var txtPoQty = (TextBox)(row.Cells[2].FindControl("txtPoQty"));

                dtPoHdr = taPoHdr.GetDataByPoRef(hfPoRef.Value.ToString());
                if (dtPoHdr.Rows.Count > 0)
                {
                    dtPoDet = taPoDet.GetDataByPoItemLno(hfPoRef.Value.ToString(), lblPoItemCode.Text, Convert.ToInt16(hfPoDetLno.Value.ToString()));
                    if (dtPoDet.Rows.Count <= 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "PO Details Data Not Found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "PO Header Data Not Found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                #region Status Info
                if (e.CommandName == "Info")
                {
                    dtPoDet = taPoDet.GetDataByPoItemLno(hfPoRef.Value.ToString(), lblPoItemCode.Text, Convert.ToInt16(hfPoDetLno.Value.ToString()));
                    if (dtPoDet.Rows.Count > 0)
                    {
                        txtMsgInfo.Text = dtPoDet[0].T_C1.ToString();
                        ModalPopupExtenderMsgInfo.Show();
                    }          
                }
                #endregion
               
                #region Reject
                if (e.CommandName == "Reject")
                {
                    //taPoHdr.UpdatePoStatus("H", dtPoDet[0].T_In - 1, hfPoRef.Value.ToString());

                    taPoDet.UpdatePoApprQty(Convert.ToDouble(txtPoQty.Text.Trim()), Convert.ToDouble(lblPoReqQty.Text.Trim()), 
                        dtPoDet[0].T_C1 + ", \n" + "Rej. By:- " + supName + " @" + DateTime.Now, "H", dtPoDet[0].T_In - 1,
                        hfPoRef.Value.ToString(), lblPoItemCode.Text, Convert.ToInt16(hfPoDetLno.Value.ToString()));

                    myTran.Commit();
                    //tblMsg.Rows[0].Cells[0].InnerText = "Data Rejected successfully.";
                    //tblMsg.Rows[1].Cells[0].InnerText = "";
                    //ModalPopupExtenderMsg.Show();
                }
                #endregion

                #region Forward
                if (e.CommandName == "Forward")
                {
                    if (dtPoHdr.Rows.Count > 0)
                    {
                        //taPoHdr.UpdatePoStatus("H", dtPoDet[0].T_In + 1, hfPoRef.Value.ToString());

                        taPoDet.UpdatePoApprQty(Convert.ToDouble(txtPoQty.Text.Trim()), Convert.ToDouble(lblPoReqQty.Text.Trim()),
                            dtPoDet[0].T_C1 + ", \n" + "Fwd. By:- " + supName + " @" + DateTime.Now, "H", dtPoDet[0].T_In + 1,
                            hfPoRef.Value.ToString(), lblPoItemCode.Text, Convert.ToInt16(hfPoDetLno.Value.ToString()));

                        myTran.Commit();
                        //tblMsg.Rows[0].Cells[0].InnerText = "Data Forwarded successfully.";
                        //tblMsg.Rows[1].Cells[0].InnerText = "";
                        //ModalPopupExtenderMsg.Show();

                    }
                }
                #endregion

                #region Approve
                if (e.CommandName == "Approve")
                {
                    if (dtPoHdr.Rows.Count > 0)
                    {
                        //taPoHdr.UpdatePoStatus("P", 1, hfPoRef.Value.ToString());

                        taPoDet.UpdatePoApprQty(Convert.ToDouble(txtPoQty.Text.Trim()), Convert.ToDouble(lblPoReqQty.Text.Trim()),
                            dtPoDet[0].T_C1 + ", \n" + "Appr. By:- " + supName + " @" + DateTime.Now, "P", 1,
                            hfPoRef.Value.ToString(), lblPoItemCode.Text, Convert.ToInt16(hfPoDetLno.Value.ToString()));

                        myTran.Commit();
                        //tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                        //tblMsg.Rows[1].Cells[0].InnerText = "";
                        //ModalPopupExtenderMsg.Show();                        
                    }

                }
                #endregion

                var taPoHdrNew = new View_Pro_Sr_DetTableAdapter();
                var dtPoHdrNew = new DataTable();
                if (suprRef == "000498" || suprRef == "000011")//Nayan Zahid DDL
                    dtPoHdrNew = taPoHdrNew.GetDataByApprDDL();
                gvPendPo.DataSource = dtPoHdrNew;
                gvPendPo.DataBind();
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

        protected void gvPendPo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hfEmpSupRef = ((HiddenField)e.Row.FindControl("hfEmpSupRef"));

                var btnAppr = ((Button)e.Row.FindControl("btnApprove"));
                var btnFwd = ((Button)e.Row.FindControl("btnForward"));

                if (empRef == "000498" || empRef == "000011")//Nayan Zahid DDL
                {
                    btnFwd.Visible = false;
                    btnAppr.Visible = true;
                }
                else
                {
                    btnFwd.Visible = false;
                    btnAppr.Visible = false;
                }
                System.Web.UI.WebControls.Image UsrImages = (System.Web.UI.WebControls.Image)e.Row.FindControl("AttachImage");
                var empRefNo = ((HiddenField)e.Row.FindControl("hfEmpRef")).Value;
                UsrImages.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + empRefNo + "'";
            }

            if (empRef == "000498" || empRef == "000011")//Nayan Zahid DDL
            {
                e.Row.Cells[11].Visible = true;
            }
            else
                e.Row.Cells[11].Visible = false;
        }
    }
}