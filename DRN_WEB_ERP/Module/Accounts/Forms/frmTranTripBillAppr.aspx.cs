using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmTranTripBillAppr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Auto  Page Refresh
            //Response.AppendHeader("Refresh", "60");

            if (Page.IsPostBack) return;

            try
            {
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                var taJvHold = new View_tbl_Acc_Jv_Hold_Pend_ListTableAdapter();
                var dtJvHold = new DataTable();
                if (empRef == "000568" || empRef == "000070" || empRef == "000011")
                    dtJvHold = taJvHold.GetData();
                gvPendJv.DataSource = dtJvHold;
                gvPendJv.DataBind();
            }
            catch (Exception ex) { }
        }

        protected void gvPendJv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region Update Journal Status
            var taHold = new tbl_Acc_Jv_HoldTableAdapter();
            var taPost = new tbl_Acc_Fa_TeTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taHold.Connection);

            try
            {
                taHold.AttachTransaction(myTran);
                taPost.AttachTransaction(myTran);

                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                var taJvHold = new View_tbl_Acc_Jv_Hold_Pend_ListTableAdapter();
                var dtJvHold = new DataTable();
                
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = gvPendJv.Rows[index];

                var lblJvRefNo = (Label)(row.Cells[2].FindControl("lblJvRefNo"));
                var lblJvDt = (Label)(row.Cells[2].FindControl("lblJvDt"));

                var dtHold = taHold.GetDataByJvRef(lblJvRefNo.Text.ToString());
                if (dtHold.Rows.Count > 0)
                {
                    if (dtHold[0].Trn_Ext_Data5 != "P")
                    {
                        if (empRef == "000568" || empRef == "000070" || empRef == "000011")
                            dtJvHold = taJvHold.GetData();
                        gvPendJv.DataSource = dtJvHold;
                        gvPendJv.DataBind();

                        tblMsg.Rows[0].Cells[0].InnerText = "Voucher Data is Not Posted.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    if (empRef == "000568" || empRef == "000070" || empRef == "000011")
                        dtJvHold = taJvHold.GetData();
                    gvPendJv.DataSource = dtJvHold;
                    gvPendJv.DataBind();     

                    tblMsg.Rows[0].Cells[0].InnerText = "Voucher Data Not Found.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }                

                #region Reject
                if (e.CommandName == "Reject")
                {
                    taHold.UpdateTranJvStat("H", lblJvRefNo.Text.ToString());

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Rejected successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
                #endregion                

                #region Approve
                if (e.CommandName == "Approve")
                {
                    var dtHoldJv = taHold.GetDataByJvRef(lblJvRefNo.Text.ToString());
                    if (dtHoldJv.Rows.Count > 0)
                    {
                        var dtMaxAccRef = taPost.GetMaxRefNo(dtHold[0].Trn_Flag, Convert.ToDateTime(dtHold[0].Trn_Date).Year);
                        var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                        var nextAccRefNo = dtHold[0].Trn_Flag + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                        var jvLno = 1;
                        foreach (dsAccTran.tbl_Acc_Jv_HoldRow dr in dtHoldJv.Rows)
                        {
                            taPost.InsertAccData(dr.Tran_Acc_Code.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                nextAccRefNo.ToString(), jvLno, 1, dr.Tran_Narration.ToString(), dr.Trn_Type.ToString(), Convert.ToDecimal(dr.Trn_Amt),
                                dr.Trn_Ext_Data2, "0", "BDT", 1, Convert.ToDecimal(dr.Trn_Amt), "", "", "", "", "", "", "", "", "", "", "",
                                (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "",
                                Convert.ToDateTime(dr.Trn_Date), dr.Tran_Acc_Name, DateTime.Now, "ADM", "T", "",
                                Convert.ToDateTime(dr.Trn_Date), dtHold[0].Trn_Flag, "L", 0, "BDT", 1, "BDT", 1, Convert.ToDecimal(dr.Trn_Amt), dr.Trn_Adr_Code,
                                dr.Trn_Dc_No, dr.Trn_Grn_No, "N", 1, 0, "", "", "", "J", 0, "1", dtHold[0].Trn_Flag);

                            jvLno++;
                        }

                        taHold.DeleteAccJvHold(lblJvRefNo.Text.ToString());

                        myTran.Commit();
                        tblMsg.Rows[0].Cells[0].InnerText = "Data saved successfully.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Voucher Data Not Found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }                   
                }
                #endregion

                #region Details
                if (e.CommandName == "Details")
                {
                    var url = "frmTranTripBillShow.aspx?TranJvRef=" + lblJvRefNo.Text.ToString();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
                }
                #endregion

                if (empRef == "000568" || empRef == "000070" || empRef == "000011")
                    dtJvHold = taJvHold.GetData();
                gvPendJv.DataSource = dtJvHold;
                gvPendJv.DataBind();                
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
    }
}