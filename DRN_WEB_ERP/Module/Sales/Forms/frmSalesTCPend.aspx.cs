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
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesTCPend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            try
            {
                var taDelOrd = new VIEW_DELIVERY_ORDER_TCTableAdapter();
                var dtDelOrd = taDelOrd.GetDataByPendDoForTC();
                gvPendDoDet.DataSource = dtDelOrd;
                gvPendDoDet.DataBind();
                gvPendDoDet.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnCreateTC_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfDoHdrRef = (HiddenField)(row.FindControl("hfDoHdrRef"));
                var hfDoDetLno = (HiddenField)(row.FindControl("hfDoDetLno"));

                Response.Redirect("/Module/Transport/Forms/frmSalesTC.aspx?DoHdrRef=" + Server.UrlEncode(hfDoHdrRef.Value.ToString()) + "&DoDetLno=" + Server.UrlEncode(hfDoDetLno.Value.ToString()));
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

        public string GetDsmName(string dsmRef)
        {
            string dsmName = "";
            try
            {
                var taDsm = new tblSalesDsmMasTableAdapter();
                var dtDsm = taDsm.GetDataByDsmRef(Convert.ToInt32(dsmRef));
                if (dtDsm.Rows.Count > 0)
                    dsmName = dtDsm[0].Dsm_Full_Name.ToString();
                return dsmName;
            }
            catch (Exception) { return dsmName; }
        }

        public string GetSpName(string spRef)
        {
            string spName = "";
            try
            {
                var taSp = new tblSalesPersonMasTableAdapter();
                var dtSp = taSp.GetDataBySpRef(Convert.ToInt32(spRef));
                if (dtSp.Rows.Count > 0)
                    spName = dtSp[0].Sp_Full_Name.ToString();
                return spName;
            }
            catch (Exception) { return spName; }
        }
    }
}