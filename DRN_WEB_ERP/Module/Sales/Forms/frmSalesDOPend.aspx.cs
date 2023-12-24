using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsHrmsMasTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;
using DRN_WEB_ERP.Module.IO.DataSet.dsIOTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmPendSo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Auto  Page Refresh
            //Response.AppendHeader("Refresh", "60");

            if (Page.IsPostBack) return;

            try
            {
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                //if (empRef == "000773" || empRef == "000416" || empRef == "000011")//----------Fazlul Krim, Tushar
                if (empRef == "000416" || empRef == "000011")//---------Tushar
                {
                    var taSalesOrd = new VIEW_SALES_ORDERTableAdapter();
                    var dtSalesOrd = taSalesOrd.GetPendSoListAll();
                    gvPendOrdDet.DataSource = dtSalesOrd;
                    gvPendOrdDet.DataBind();
                    gvPendOrdDet.SelectedIndex = -1;

                    var taDelOrd = new VIEW_DELIVERY_ORDERTableAdapter();
                    var dtDelOrd = taDelOrd.GetPendDoList();
                    gvPendDoDet.DataSource = dtDelOrd;
                    gvPendDoDet.DataBind();
                    gvPendDoDet.SelectedIndex = -1;
                }

                if (empRef == "000773")//---------Fazlul Karim
                {
                    var taSalesOrd = new VIEW_SALES_ORDERTableAdapter();
                    var dtSalesOrd = taSalesOrd.GetPendSoListAllExcept();
                    gvPendOrdDet.DataSource = dtSalesOrd;
                    gvPendOrdDet.DataBind();
                    gvPendOrdDet.SelectedIndex = -1;

                    var taDelOrd = new VIEW_DELIVERY_ORDERTableAdapter();
                    var dtDelOrd = taDelOrd.GetPendDoList();
                    gvPendDoDet.DataSource = dtDelOrd;
                    gvPendDoDet.DataBind();
                    gvPendDoDet.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvPendOrdDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
            //    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
            //    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            //}
        }

        protected void gvPendOrdDet_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvPendOrdDet.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    var hfOrdHdrRef = (HiddenField)gvPendOrdDet.Rows[indx].FindControl("hfOrdHdrRef");
                    var lblOrdHdrRefNo = (Label)gvPendOrdDet.Rows[indx].FindControl("lblOrdHdrRefNo");
                    var hfOrdDetLno = (HiddenField)gvPendOrdDet.Rows[indx].FindControl("hfOrdDetLno");

                    Response.Redirect("frmSalesDO.aspx?OrdHdrRef=" + Server.UrlEncode(hfOrdHdrRef.Value.ToString()) + "&OrdDetLno=" + Server.UrlEncode(hfOrdDetLno.Value.ToString()));
                }
                catch (Exception ex)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfOrdHdrRef = (HiddenField)(row.FindControl("hfOrdHdrRef"));
                var lblOrdHdrRefNo = (Label)(row.FindControl("lblOrdHdrRefNo"));
                var hfOrdDetLno = (HiddenField)(row.FindControl("hfOrdDetLno"));

                Response.Redirect("frmSalesDO.aspx?OrdHdrRef=" + Server.UrlEncode(hfOrdHdrRef.Value.ToString()) + "&OrdDetLno=" + Server.UrlEncode(hfOrdDetLno.Value.ToString()));
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }            
        }

        protected void btnCancelDo_Click(object sender, EventArgs e)
        {
            var taSalesDelHdr = new tblSalesOrdDelHdrTableAdapter();
            var taSalesDelDet = new tblSalesOrdDelDetTableAdapter();
            var taSalesOrdHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesOrdDet = new tblSalesOrderDetTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesDelHdr.Connection);

            try
            {
                #region D/O Qty Validation
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfDoHdrRef = (HiddenField)(row.FindControl("hfDoHdrRef"));
                var lblDoHdrRefNo = (Label)(row.FindControl("lblDoHdrRefNo"));
                var hfDoDetLno = (HiddenField)(row.FindControl("hfDoDetLno"));
                var lblDoQty = (Label)(row.FindControl("lblPendDoQty"));
                var lblDoFreeQty = (Label)(row.FindControl("lblPendDoFreeQty"));

                var ordRef = "";
                var ordRefNo = "";
                var ordDetLNo = "";
                var custRef = "";

                var dtSalesDelDet = taSalesDelDet.GetDataByDetLno(hfDoHdrRef.Value.ToString(), Convert.ToInt16(hfDoDetLno.Value.ToString()));
                if (dtSalesDelDet.Rows.Count > 0)
                {
                    var dtSalesOrdHdr = taSalesOrdHdr.GetDataByHdrRef(dtSalesDelDet[0].DO_Det_SO_Ref.ToString());
                    if (dtSalesOrdHdr.Rows.Count > 0)
                    {
                        ordRef = dtSalesOrdHdr[0].SO_Hdr_Ref.ToString();
                        ordRefNo = dtSalesOrdHdr[0].SO_Hdr_Ref_No.ToString();
                        ordDetLNo = dtSalesDelDet[0].DO_Det_SO_Lno.ToString();
                        custRef = dtSalesOrdHdr[0].SO_Hdr_Pcode.ToString();

                        //var dtSalesOrdDet = taSalesOrdDet.GetDataByDetLno(dtSalesDelDet[0].DO_Det_SO_Ref.ToString(), Convert.ToInt16(dtSalesDelDet[0].DO_Det_SO_Lno.ToString()));
                        //if (dtSalesOrdDet.Rows.Count > 0)
                        //{
                        //    #region D/O Qty Validation
                        //    if (Convert.ToDouble(lblDoQty.Text.Trim()) > dtSalesOrdDet[0].SO_Det_DO_Bal_Qty)
                        //    {
                        //        tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to cancel D/O qty more than : " + dtSalesOrdDet[0].SO_Det_DO_Bal_Qty;
                        //        tblMsg.Rows[1].Cells[0].InnerText = "";
                        //        ModalPopupExtenderMsg.Show();
                        //        return;
                        //    }
                        //    if (Convert.ToDouble(lblDoFreeQty.Text.Trim()) > Convert.ToDouble(dtSalesOrdDet[0].SO_Det_Ext_Data2))
                        //    {
                        //        tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to create free bag D/O qty more than : " + dtSalesOrdDet[0].SO_Det_Ext_Data1;
                        //        tblMsg.Rows[1].Cells[0].InnerText = "";
                        //        ModalPopupExtenderMsg.Show();
                        //        return;
                        //    }
                        //    #endregion

                        //    #region Credit Limit Validiation
                        //    var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                        //    var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        //    var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;
                        //    var custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                        //    var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                        //    var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                        //    var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                        //    var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                        //    var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                        //    var doAmt = Convert.ToDouble(lblDoQty.Text.Trim()) * Convert.ToDouble(dtSalesOrdDet[0].SO_Det_Lin_Rat);

                        //    if (doAmt > (Convert.ToDouble(crLimit) - (drAmt - crAmt)))
                        //    {
                        //        tblMsg.Rows[0].Cells[0].InnerText = "You are no allowed to exceed available credit limit : " + (Convert.ToDouble(crLimit) - (drAmt - crAmt)).ToString("N2");
                        //        tblMsg.Rows[1].Cells[0].InnerText = "";
                        //        ModalPopupExtenderMsg.Show();
                        //        return;
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Order Details.";
                        //    tblMsg.Rows[1].Cells[0].InnerText = "";
                        //    ModalPopupExtenderMsg.Show();
                        //    return;
                        //}
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Order.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }

                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid D/O Details.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                taSalesDelHdr.AttachTransaction(myTran);
                taSalesDelDet.AttachTransaction(myTran);
                taSalesOrdDet.AttachTransaction(myTran);

                taSalesDelHdr.UpdateDoHdrStat("C", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "0", hfDoHdrRef.Value.ToString());

                var dtSoDet = taSalesOrdDet.GetDataByDetLno(ordRef.ToString(), Convert.ToInt16(ordDetLNo.ToString()));
                if (dtSoDet.Rows.Count > 0)
                {
                    taSalesOrdDet.UpdateSalesDetDoBal((dtSoDet[0].SO_Det_Org_QTY - Convert.ToDouble(lblDoQty.Text.Trim())),
                        (dtSoDet[0].SO_Det_Lin_Qty) - (dtSoDet[0].SO_Det_Org_QTY - Convert.ToDouble(lblDoQty.Text.Trim())),
                        (dtSoDet[0].SO_Det_DO_Qty - (Convert.ToDouble(lblDoQty.Text.Trim()) + Convert.ToDouble(lblDoFreeQty.Text.Trim()))),
                        dtSoDet[0].SO_Det_DO_Bal_Qty + ((Convert.ToDouble(lblDoQty.Text.Trim()) + Convert.ToDouble(lblDoFreeQty.Text.Trim()))),
                        (Convert.ToDouble(dtSoDet[0].SO_Det_Ext_Data1) - Convert.ToDouble(lblDoFreeQty.Text.Trim())).ToString(),
                        ((dtSoDet[0].SO_Det_Free_Qty) - (Convert.ToDouble(dtSoDet[0].SO_Det_Ext_Data1) - Convert.ToDouble(lblDoFreeQty.Text.Trim()))).ToString(),
                        ordRef.ToString(), Convert.ToInt16(ordDetLNo.ToString()));
                }
                else
                {
                    myTran.Rollback();
                    tblMsg.Rows[0].Cells[0].InnerText = "Sales Order details does not match.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "D/O Canceled Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                var taDelOrd = new VIEW_DELIVERY_ORDERTableAdapter();
                var dtDelOrd = taDelOrd.GetPendDoList();
                gvPendDoDet.DataSource = dtDelOrd;
                gvPendDoDet.DataBind();
                gvPendDoDet.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnFwdDo_Click(object sender, EventArgs e)
        {
            var taSalesDelHdr = new tblSalesOrdDelHdrTableAdapter();
            var taSalesDelDet = new tblSalesOrdDelDetTableAdapter();
            var taSalesOrdHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesOrdDet = new tblSalesOrderDetTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesDelHdr.Connection);

            try
            {
                #region D/O Qty and Credit Limit Validation
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfDoHdrRef = (HiddenField)(row.FindControl("hfDoHdrRef"));
                var lblDoHdrRefNo = (Label)(row.FindControl("lblDoHdrRefNo"));
                var hfDoDetLno = (HiddenField)(row.FindControl("hfDoDetLno"));
                var lblDoQty = (Label)(row.FindControl("lblPendDoQty"));
                var lblDoFreeQty = (Label)(row.FindControl("lblPendDoFreeQty"));

                var ordRef = "";
                var ordRefNo = "";
                var ordDetLNo = "";
                var custRef = "";

                var dtSalesDelDet = taSalesDelDet.GetDataByDetLno(hfDoHdrRef.Value.ToString(), Convert.ToInt16(hfDoDetLno.Value.ToString()));
                if (dtSalesDelDet.Rows.Count > 0)
                {
                    var dtSalesOrdHdr = taSalesOrdHdr.GetDataByHdrRef(dtSalesDelDet[0].DO_Det_SO_Ref.ToString());
                    if (dtSalesOrdHdr.Rows.Count > 0)
                    {
                        ordRef = dtSalesOrdHdr[0].SO_Hdr_Ref.ToString();
                        ordRefNo = dtSalesOrdHdr[0].SO_Hdr_Ref_No.ToString();
                        ordDetLNo = dtSalesDelDet[0].DO_Det_SO_Lno.ToString();
                        custRef = dtSalesOrdHdr[0].SO_Hdr_Pcode.ToString();

                        //var dtSalesOrdDet = taSalesOrdDet.GetDataByDetLno(dtSalesDelDet[0].DO_Det_SO_Ref.ToString(), Convert.ToInt16(dtSalesDelDet[0].DO_Det_SO_Lno.ToString()));
                        //if (dtSalesOrdDet.Rows.Count > 0)
                        //{
                        //    #region D/O Qty Validation
                        //    if (Convert.ToDouble(lblDoQty.Text.Trim()) > dtSalesOrdDet[0].SO_Det_DO_Bal_Qty)
                        //    {
                        //        tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to create D/O more than : " + dtSalesOrdDet[0].SO_Det_DO_Bal_Qty;
                        //        tblMsg.Rows[1].Cells[0].InnerText = "";
                        //        ModalPopupExtenderMsg.Show();
                        //        return;
                        //    }
                        //    if (Convert.ToDouble(lblDoFreeQty.Text.Trim()) > Convert.ToDouble(dtSalesOrdDet[0].SO_Det_Ext_Data2))
                        //    {
                        //        tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to create free bag D/O more than : " + dtSalesOrdDet[0].SO_Det_Ext_Data1;
                        //        tblMsg.Rows[1].Cells[0].InnerText = "";
                        //        ModalPopupExtenderMsg.Show();
                        //        return;
                        //    }
                        //    #endregion

                        //    #region Credit Limit Validiation
                        //    var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                        //    var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        //    var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;
                        //    var custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                        //    var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                        //    var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                        //    var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                        //    var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                        //    var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                        //    var doAmt = Convert.ToDouble(lblDoQty.Text.Trim()) * Convert.ToDouble(dtSalesOrdDet[0].SO_Det_Lin_Rat);

                        //    if (doAmt > (Convert.ToDouble(crLimit) - (drAmt - crAmt)))
                        //    {
                        //        tblMsg.Rows[0].Cells[0].InnerText = "You are no allowed to exceed available credit limit : " + (Convert.ToDouble(crLimit) - (drAmt - crAmt)).ToString("N2");
                        //        tblMsg.Rows[1].Cells[0].InnerText = "";
                        //        ModalPopupExtenderMsg.Show();
                        //        return;
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Order Details.";
                        //    tblMsg.Rows[1].Cells[0].InnerText = "";
                        //    ModalPopupExtenderMsg.Show();
                        //    return;
                        //}
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Order.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }

                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid D/O Details.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                taSalesDelHdr.AttachTransaction(myTran);
                //taSalesDelDet.AttachTransaction(myTran);
                //taSalesOrdDet.AttachTransaction(myTran);

                taSalesDelHdr.UpdateDoHdrStat("P", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", hfDoHdrRef.Value.ToString());

                //var dtSoDet = taSalesOrdDet.GetDataByDetLno(ordRef.ToString(), Convert.ToInt16(ordDetLNo.ToString()));
                //if (dtSoDet.Rows.Count > 0)
                //{
                //    taSalesOrdDet.UpdateSalesDet((dtSoDet[0].SO_Det_Org_QTY - Convert.ToDouble(lblDoQty.Text.Trim())),
                //        (dtSoDet[0].SO_Det_Lin_Qty) - (dtSoDet[0].SO_Det_Org_QTY - Convert.ToDouble(lblDoQty.Text.Trim())),
                //        (dtSoDet[0].SO_Det_DO_Qty - Convert.ToDouble(lblDoQty.Text.Trim())),
                //        dtSoDet[0].SO_Det_Lin_Qty - (dtSoDet[0].SO_Det_DO_Qty - Convert.ToDouble(lblDoQty.Text.Trim())),
                //        (Convert.ToDouble(dtSoDet[0].SO_Det_Ext_Data1) - Convert.ToDouble(lblDoFreeQty.Text.Trim())).ToString(),
                //        ((dtSoDet[0].SO_Det_Free_Qty) - (Convert.ToDouble(dtSoDet[0].SO_Det_Ext_Data1) - Convert.ToDouble(lblDoFreeQty.Text.Trim()))).ToString(),
                //        ordRef.ToString(), Convert.ToInt16(ordDetLNo.ToString()));
                //}
                //else
                //{
                //    myTran.Rollback();
                //    tblMsg.Rows[0].Cells[0].InnerText = "Sales Order details does not match.";
                //    tblMsg.Rows[1].Cells[0].InnerText = "";
                //    ModalPopupExtenderMsg.Show();
                //    return;
                //}

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "D/O Forwarded Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                try
                {
                    //Procedure one             
                    String sid = "7HorseSuprmBng";
                    String user = "sevenhorse";
                    String pass = "123456";
                    String URI = "http://sms.sslwireless.com/pushapi/dynamic/server.php";

                    var custName = "";
                    var taCustHdr = new tblSalesPartyAdrTableAdapter();
                    var dtCustHdr = taCustHdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                    if (dtCustHdr.Rows.Count > 0)
                        custName = dtCustHdr[0].Par_Adr_Name.ToString();

                    var cellNo = "";
                    var taSmsConfig = new tbl_Sms_ConfigTableAdapter();
                    var dtSmsConfig = taSmsConfig.GetDataByTranTypeStatus("DO-ISU", "1");
                    foreach (dsSalesMas.tbl_Sms_ConfigRow dr in dtSmsConfig.Rows)
                    {
                        var taParAdr = new tblSalesPartyAdrTableAdapter();
                        var dtParAdr = taParAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef.ToString()));

                        if (dr.Config_Receiver_Grp_Name.ToString() == "INDV")
                            cellNo = dr.Config_Receiver_Cell_No.ToString();

                        if (dr.Config_Receiver_Grp_Name.ToString() == "CUST")
                        {
                            if (dtParAdr.Rows.Count > 0)
                                cellNo = dtParAdr[0].Par_Adr_Cell_No.ToString();
                        }

                        if (dr.Config_Receiver_Grp_Name.ToString() == "DSM")
                        {
                            var dsmRef = "";
                            if (dtParAdr.Rows.Count > 0)
                                dsmRef = dtParAdr[0].Par_Adr_Sls_Per.ToString();

                            var taDsm = new tblSalesDsmMasTableAdapter();
                            var dtDsm = taDsm.GetDataByDsmRef(Convert.ToInt32(dsmRef.ToString()));
                            if (dtDsm.Rows.Count > 0)
                            {
                                if (dtDsm[0].Dsm_Status.ToString() == "1")
                                    cellNo = dtDsm[0].Dsm_Cell_No.ToString();
                            }
                        }

                        if (dr.Config_Receiver_Grp_Name.ToString() == "MPO")
                        {
                            var mpoRef = "";
                            if (dtParAdr.Rows.Count > 0)
                                mpoRef = dtParAdr[0].Par_Adr_Ext_Data2.ToString();

                            var taMpo = new tblSalesPersonMasTableAdapter();
                            var dtMpo = taMpo.GetDataBySpRef(Convert.ToInt32(mpoRef.ToString()));
                            if (dtMpo.Rows.Count > 0)
                            {
                                if (dtMpo[0].Sp_Status.ToString() == "1")
                                    cellNo = dtMpo[0].Sp_Cell_No.ToString();
                            }
                        }

                        if (cellNo.Length > 0)
                        {
                            if (cellNo.Substring(0, 2) == "01" && cellNo.ToString().Length == 11)
                            {
                                var taSmsBody = new tbl_Sms_BodyTableAdapter();
                                var dtSmsBody = taSmsBody.GetDataBySmsTranTypeLanguage("DO-ISU", "Bangla");
                                if (dtSmsBody.Rows.Count > 0)
                                {
                                    //String myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=88" + cellNo.ToString() + "&sms[0][1]="
                                    //                       + System.Web.HttpUtility.UrlEncode(DRN_WEB_ERP.GlobalClass.clsSmsHelper.convertBanglatoUnicode(dtSmsBody[0].SMS_Body_1.ToString()
                                    //                       + Math.Round(Convert.ToDouble(lblDoQty.Text.Trim())) + dtSmsBody[0].SMS_Body_2.ToString() + lblDoHdrRefNo.Text.Trim()
                                    //                       + dtSmsBody[0].SMS_Body_3.ToString() + " (Dealer Name: " + custName + ")"))
                                    //                       + "&sms[0][2]=" + "1234567890" + "&sid=" + sid;

                                    String myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=88" + cellNo.ToString() + "&sms[0][1]="
                                                           + System.Web.HttpUtility.UrlEncode(DRN_WEB_ERP.GlobalClass.clsSmsHelper.convertBanglatoUnicode(dtSmsBody[0].SMS_Body_1.ToString()
                                                           + Math.Round(Convert.ToDouble(lblDoQty.Text.Trim())) + dtSmsBody[0].SMS_Body_2.ToString() + lblDoHdrRefNo.Text.Trim()
                                                           + dtSmsBody[0].SMS_Body_3.ToString()))
                                                           + "&sms[0][2]=" + "1234567890" + "&sid=" + sid;

                                    using (WebClient wc = new WebClient())
                                    {
                                        wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                                        string HtmlResult = wc.UploadString(URI, myParameters);

                                        Console.Write(HtmlResult);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex) { }       

                var taDelOrd = new VIEW_DELIVERY_ORDERTableAdapter();
                var dtDelOrd = taDelOrd.GetPendDoList();
                gvPendDoDet.DataSource = dtDelOrd;
                gvPendDoDet.DataBind();
                gvPendDoDet.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvPendDoDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hfDoHdrStat = ((HiddenField)e.Row.FindControl("hfHdrStat"));
                var btnDoCancel = ((Button)e.Row.FindControl("btnCancelDo"));
                var btnDoFwd = ((Button)e.Row.FindControl("btnFwdDo"));

                if (hfDoHdrStat.Value == "1")
                {
                    btnDoCancel.Enabled = true;
                    btnDoFwd.Enabled = true;
                }
                else
                {
                    btnDoCancel.Enabled = false;
                    btnDoFwd.Enabled = false;
                }
            }
        }

        public string GetSpName(string SpRef)
        {
            string spName = "";
            try
            {
                var taSp = new tblSalesPersonMasTableAdapter();
                var dtSp = taSp.GetDataBySpRef(Convert.ToInt32(SpRef));
                if (dtSp.Rows.Count > 0)
                    spName = dtSp[0].Sp_Short_Name.ToString();
                return spName;
            }
            catch (Exception) { return spName; }
        }
    }
}