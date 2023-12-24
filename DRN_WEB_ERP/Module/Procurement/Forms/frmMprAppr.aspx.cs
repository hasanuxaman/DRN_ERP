using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmMprAppr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Auto  Page Refresh
            //Response.AppendHeader("Refresh", "60");

            if (Page.IsPostBack) return;

            try
            {
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                var taPrHdr = new View_PuTr_Pr_Hdr_DetTableAdapter();
                var dtPrHdr = new DataTable();
                if (empRef == "000002" || empRef == "000480" || empRef == "000134" || empRef == "000442" || empRef == "000561" || empRef == "000011" || empRef == "000683")//----mamun,habib,habib,tarikul,rahik,emran
                    dtPrHdr = taPrHdr.GetDataByAppr();
                gvPendPr.DataSource = dtPrHdr;
                gvPendPr.DataBind();

                btnAppAll.Visible = dtPrHdr.Rows.Count > 0;
                btnAppAll0.Visible = dtPrHdr.Rows.Count > 0;
            }
            catch (Exception ex) { }
        }

        #region GridData
        public string GetCurStk(string itemCode)
        {
            string curStk = "";
            try
            {
                var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();
                var dtStkCtl = taStkCtl.GetCurStkByItem(itemCode);

                curStk = dtStkCtl == null ? "0.00" : String.Format("{0:0.00}", dtStkCtl.ToString());
                return curStk;
            }
            catch (Exception ex) { return curStk; }
        }

        public string GetLastPurDt(string itemCode)
        {
            string purDt = "";
            try
            {
                var taPoDet = new View_PuTr_Po_Hdr_DetTableAdapter();
                var dtPoDet = taPoDet.GetPoPriceLogByItem(itemCode);
                if (dtPoDet.Rows.Count > 0)
                    purDt = dtPoDet[0].PO_Hdr_DATE.ToString("dd/MM/yyyy");

                return purDt;
            }
            catch (Exception ex) { return purDt; }
        }

        public string GetLastPurQty(string itemCode)
        {
            string purQty = "";
            try
            {
                var taPoDet = new View_PuTr_Po_Hdr_DetTableAdapter();
                var dtPoDet = taPoDet.GetPoPriceLogByItem(itemCode);
                if (dtPoDet.Rows.Count > 0)
                    purQty = String.Format("{0:0.00}", dtPoDet[0].PO_Det_Lin_Qty.ToString());

                return purQty;
            }
            catch (Exception ex) { return purQty; }
        }

        public string GetLastPurRate(string itemCode)
        {
            string purRate = "";
            try
            {
                var taPoDet = new View_PuTr_Po_Hdr_DetTableAdapter();
                var dtPoDet = taPoDet.GetPoPriceLogByItem(itemCode);
                if (dtPoDet.Rows.Count > 0)
                    purRate = String.Format("{0:0.00}", dtPoDet[0].PO_Det_Lin_Rat.ToString("N2"));

                return purRate;
            }
            catch (Exception ex) { return purRate; }
        }

        public string GetLastPurSup(string itemCode)
        {
            string purSupCode = "";
            string purSupName = "";
            try
            {
                var taPoDet = new View_PuTr_Po_Hdr_DetTableAdapter();
                var dtPoDet = taPoDet.GetPoPriceLogByItem(itemCode);
                if (dtPoDet.Rows.Count > 0)
                {
                    purSupCode = dtPoDet[0].PO_Hdr_Pcode.ToString();
                    var taSup = new tbl_PuMa_Par_AdrTableAdapter();
                    var dtSup = taSup.GetDataBySupAdrRef(dtPoDet[0].PO_Hdr_Pcode);
                    if (dtSup.Rows.Count > 0)
                        purSupName = dtSup[0].Par_Adr_Name;
                }
                return purSupName;
            }
            catch (Exception ex) { return purSupName; }
        }

        #endregion

        protected void gvPendPr_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region Update Requisition Status
            var taPrHdr = new tbl_PuTr_Pr_HdrTableAdapter();
            var taPrDet = new tbl_PuTr_Pr_DetTableAdapter();
            var comm = new tbl_Tran_ComTableAdapter();

            var dtPrHdr = new dsProcTran.tbl_PuTr_Pr_HdrDataTable();
            var dtPrDet = new dsProcTran.tbl_PuTr_Pr_DetDataTable();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taPrHdr.Connection);

            try
            {
                var empName = "";
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                var EmpId = "";
                var empDesig = "";
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                {
                    empRef = dtEmp[0].EmpRefNo;
                    EmpId = dtEmp[0].EmpId;
                    empName = dtEmp[0].EmpName;
                    empDesig = dtEmp[0].DesigName;
                }

                taPrHdr.AttachTransaction(myTran);
                taPrDet.AttachTransaction(myTran);
                comm.AttachTransaction(myTran);

                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = gvPendPr.Rows[index];

                var hfPrRef = (HiddenField)(row.Cells[2].FindControl("hfPrRef"));
                var lblPrRefNo = (Label)(row.Cells[2].FindControl("lblPrRefNo"));
                var hfPrDetLno = (HiddenField)(row.Cells[2].FindControl("hfPrDetLno"));
                var hfPrItemCode = (HiddenField)(row.Cells[2].FindControl("hfPrItemCode"));
                var lblPrReqQty = (Label)(row.Cells[2].FindControl("lblPrReqQty"));
                var txtPrQty = (TextBox)(row.Cells[2].FindControl("txtPrQty"));
                var chkMpr = (CheckBox)(row.Cells[2].FindControl("chkMpr"));

                if (chkMpr.Checked == false)
                {
                    myTran.Rollback();
                    tblMsg.Rows[0].Cells[0].InnerText = "Select MPR Ref. No first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                dtPrHdr = taPrHdr.GetDataByPrRef(hfPrRef.Value.ToString());
                if (dtPrHdr.Rows.Count > 0)
                {
                    dtPrDet = taPrDet.GetDataByPrItemLno(hfPrRef.Value.ToString(), hfPrItemCode.Value, Convert.ToInt16(hfPrDetLno.Value.ToString()));
                    if (dtPrDet.Rows.Count <= 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "MPR Details Data Not Found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "MPR Header Data Not Found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                #region Reject
                if (e.CommandName == "Reject")
                {
                    taPrHdr.UpdatePrHdrStatus("H", dtPrDet[0].Pr_Det_T_In - 1, hfPrRef.Value.ToString());

                    var comments_det = "(MPR Rejected By:" + empName + ")";
                    comm.InsertTranCom(lblPrRefNo.Text.Trim(), 2, DateTime.Now, EmpId, empName, empDesig, 1, "MPR", "REJ", comments_det, "", "1", "", "", "", "");

                    taPrDet.UpdatePrDetApprQty(Convert.ToDouble(txtPrQty.Text.Trim()), Convert.ToDouble(txtPrQty.Text.Trim()),
                        dtPrDet[0].Pr_Det_T_C1 + ", \n" + "Rej. By:- " + empName + " @" + DateTime.Now, "H", dtPrDet[0].Pr_Det_T_In - 1,
                        hfPrRef.Value.ToString(), hfPrItemCode.Value, Convert.ToInt16(hfPrDetLno.Value.ToString()));

                    myTran.Commit();
                    //tblMsg.Rows[0].Cells[0].InnerText = "Data Rejected successfully.";
                    //tblMsg.Rows[1].Cells[0].InnerText = "";
                    //ModalPopupExtenderMsg.Show();
                }
                #endregion

                #region Approve
                if (e.CommandName == "Approve")
                {
                    if (dtPrHdr.Rows.Count > 0)
                    {
                        taPrHdr.UpdatePrHdrStatus("P", 1, hfPrRef.Value.ToString());

                        var comments_det = "(MPR Approved By:" + empName + ")";
                        comm.InsertTranCom(lblPrRefNo.Text.Trim(), 2, DateTime.Now, EmpId, empName, empDesig, 1, "MPR", "OPN", comments_det, "", "1", "", "", "", "");

                        taPrDet.UpdatePrDetApprQty(Convert.ToDouble(txtPrQty.Text.Trim()), Convert.ToDouble(txtPrQty.Text.Trim()),
                            dtPrDet[0].Pr_Det_T_C1 + ", \n" + "Appr. By:- " + empName + " @" + DateTime.Now, "P", 1,
                            hfPrRef.Value.ToString(), hfPrItemCode.Value, Convert.ToInt16(hfPrDetLno.Value.ToString()));

                        myTran.Commit();
                        //tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                        //tblMsg.Rows[1].Cells[0].InnerText = "";
                        //ModalPopupExtenderMsg.Show();
                    }

                }
                #endregion

                var taPrHdrNew = new View_PuTr_Pr_Hdr_DetTableAdapter();
                var dtPrHdrNew = new DataTable();
                if (empRef == "000002" || empRef == "000480" || empRef == "000134" || empRef == "000442" || empRef == "000561" || empRef == "000011" || empRef == "000683")//----mamun,habib,habib,tarikul,rahik,emran
                    dtPrHdrNew = taPrHdrNew.GetDataByAppr();
                gvPendPr.DataSource = dtPrHdrNew;
                gvPendPr.DataBind();

                btnAppAll.Visible = dtPrHdrNew.Rows.Count > 0;
                btnAppAll0.Visible = dtPrHdrNew.Rows.Count > 0;
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

        protected void btnAppAll_Click(object sender, EventArgs e)
        {
            #region Update Requisition Status
            var taPrHdr = new tbl_PuTr_Pr_HdrTableAdapter();
            var taPrDet = new tbl_PuTr_Pr_DetTableAdapter();
            var comm = new tbl_Tran_ComTableAdapter();

            var dtPrHdr = new dsProcTran.tbl_PuTr_Pr_HdrDataTable();
            var dtPrDet = new dsProcTran.tbl_PuTr_Pr_DetDataTable();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taPrHdr.Connection);

            try
            {
                var chkCntRow = 0;
                foreach (GridViewRow gr in gvPendPr.Rows)
                {
                    var chk = (CheckBox)(gr.FindControl("chkMpr"));
                    if (chk.Checked) chkCntRow++;
                }

                if (chkCntRow <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Select MPR Ref. No first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                var empName = "";
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                var EmpId = "";
                var empDesig = "";
                var taEmp = new View_Emp_BascTableAdapter();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt32(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                {
                    empRef = dtEmp[0].EmpRefNo;
                    EmpId = dtEmp[0].EmpId;
                    empName = dtEmp[0].EmpName;
                    empDesig = dtEmp[0].DesigName;
                }

                taPrHdr.AttachTransaction(myTran);
                taPrDet.AttachTransaction(myTran);
                comm.AttachTransaction(myTran);

                var chkCnt = 0;
                var i = 0;
                foreach (GridViewRow gr in gvPendPr.Rows)
                {
                    var chk = (CheckBox)(gr.FindControl("chkMpr"));
                    if (chk.Checked)
                    {
                        chkCnt++;

                        var hfPrRef = (HiddenField)(gr.FindControl("hfPrRef"));
                        var lblPrRefNo = (Label)(gr.FindControl("lblPrRefNo"));
                        var hfPrDetLno = (HiddenField)(gr.FindControl("hfPrDetLno"));
                        var hfPrItemCode = (HiddenField)(gr.FindControl("hfPrItemCode"));
                        var lblPrReqQty = (Label)(gr.FindControl("lblPrReqQty"));
                        var txtPrQty = (TextBox)(gr.FindControl("txtPrQty"));

                        dtPrHdr = taPrHdr.GetDataByPrRef(hfPrRef.Value.ToString());
                        if (dtPrHdr.Rows.Count > 0)
                        {
                            dtPrDet = taPrDet.GetDataByPrItemLno(hfPrRef.Value.ToString(), hfPrItemCode.Value, Convert.ToInt16(hfPrDetLno.Value.ToString()));
                            if (dtPrDet.Rows.Count > 0)
                            {
                                i++;

                                taPrHdr.UpdatePrHdrStatus("P", 1, hfPrRef.Value.ToString());

                                var comments_det = "(MPR Approved By:" + empName + ")";
                                comm.InsertTranCom(lblPrRefNo.Text.Trim(), 2, DateTime.Now, EmpId, empName, empDesig, 1, "MPR", "OPN", comments_det, "", "1", "", "", "", "");

                                taPrDet.UpdatePrDetApprQty(Convert.ToDouble(txtPrQty.Text.Trim()), Convert.ToDouble(txtPrQty.Text.Trim()),
                                    dtPrDet[0].Pr_Det_T_C1 + ", \n" + "Appr. By:- " + empName + " @" + DateTime.Now, "P", 1,
                                    hfPrRef.Value.ToString(), hfPrItemCode.Value, Convert.ToInt16(hfPrDetLno.Value.ToString()));
                            }
                            else
                            {
                                //tblMsg.Rows[0].Cells[0].InnerText = "PR Details Data Not Found.";
                                //tblMsg.Rows[1].Cells[0].InnerText = "";
                                //ModalPopupExtenderMsg.Show();
                                //return;
                            }

                        }
                        else
                        {
                            //tblMsg.Rows[0].Cells[0].InnerText = "PR Header Data Not Found.";
                            //tblMsg.Rows[1].Cells[0].InnerText = "";
                            //ModalPopupExtenderMsg.Show();
                            //return;
                        }
                    }
                }

                if (chkCnt > 0 && i > 0)
                {
                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }

                var taPrHdrNew = new View_PuTr_Pr_Hdr_DetTableAdapter();
                var dtPrHdrNew = new DataTable();
                if (empRef == "000002" || empRef == "000480" || empRef == "000134" || empRef == "000442" || empRef == "000561" || empRef == "000011" || empRef == "000683")//----mamun,habib,habib,tarikul,rahik,emran
                    dtPrHdrNew = taPrHdrNew.GetDataByAppr();
                gvPendPr.DataSource = dtPrHdrNew;
                gvPendPr.DataBind();

                btnAppAll.Visible = dtPrHdrNew.Rows.Count > 0;
                btnAppAll0.Visible = dtPrHdrNew.Rows.Count > 0;
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

        protected void gvPendPr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
                e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
            }
        }        
    }
}