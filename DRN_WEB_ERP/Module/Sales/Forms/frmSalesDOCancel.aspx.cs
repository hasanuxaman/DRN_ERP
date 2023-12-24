using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesDOCancel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");                       
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var custRef = "";
            if (txtSearch.Text.Trim().Length > 0)
            {
                if (txtSearch.Text.Trim().Length <= 0) return;

                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custRef = word;
                    break;
                }

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0) custRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                    }
                }
            }

            if (custRef != "")
            {
                var taDelOrd = new VIEW_DELIVERY_ORDERTableAdapter();
                var dtDelOrd = taDelOrd.GetAppDoListByDateCust(txtFromDate.Text.Trim(), txtToDate.Text.Trim(),custRef);
                gvDoDet.DataSource = dtDelOrd;
                gvDoDet.DataBind();
            }
            else
            {
                var taDelOrd = new VIEW_DELIVERY_ORDERTableAdapter();
                var dtDelOrd = taDelOrd.GetAppDoListByDate(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                gvDoDet.DataSource = dtDelOrd;
                gvDoDet.DataBind();
            }
        }

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtSearch.Text = "";
            btnClearSrch.Visible = false;
        }

        protected void btnCancelDo_Click(object sender, EventArgs e)
        {
            var taSalesDelHdr = new tblSalesOrdDelHdrTableAdapter();
            var taSalesDelDet = new tblSalesOrdDelDetTableAdapter();
            var taSalesOrdHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesOrdDet = new tblSalesOrderDetTableAdapter();
            var taChlnDet = new View_Challan_DetailsTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesDelHdr.Connection);

            try
            {
                #region D/O Validation
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfDoHdrRef = (HiddenField)(row.FindControl("hfAppDoHdrRef"));
                var lblDoHdrRefNo = (Label)(row.FindControl("lblAppDoHdrRefNo"));
                var hfDoDetLno = (HiddenField)(row.FindControl("hfAppDoDetLno"));
                var lblDoQty = (Label)(row.FindControl("lblAppDoQty"));
                var lblDoFreeQty = (Label)(row.FindControl("lblAppDoFreeQty"));

                var ordRef = "";
                var ordRefNo = "";
                var ordDetLNo = "";
                var custRef = "";

                var dtSalesDelDet = taSalesDelDet.GetDataByDetLno(hfDoHdrRef.Value.ToString(), Convert.ToInt16(hfDoDetLno.Value.ToString()));
                if (dtSalesDelDet.Rows.Count > 0)
                {
                    var dtChlnDet = taChlnDet.GetDataByDoRef(hfDoHdrRef.Value.ToString());
                    if (dtChlnDet.Rows.Count > 0)
                    {
                        var dtClnQty = taChlnDet.GetChlnQtyByDoRef(hfDoHdrRef.Value.ToString());
                        var clnQty = dtClnQty == null ? 0 : Convert.ToDouble(dtClnQty);
                        if (clnQty > 0)
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "D/O Ref No: " + lblDoHdrRefNo.Text.Trim() + " executed.";
                            tblMsg.Rows[1].Cells[0].InnerText = "Challan Ref No:" + dtChlnDet[0].Trn_Hdr_Tran_Ref.ToString();
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }

                    var dtSalesOrdHdr = taSalesOrdHdr.GetDataByHdrRef(dtSalesDelDet[0].DO_Det_SO_Ref.ToString());
                    if (dtSalesOrdHdr.Rows.Count > 0)
                    {
                        ordRef = dtSalesOrdHdr[0].SO_Hdr_Ref.ToString();
                        ordRefNo = dtSalesOrdHdr[0].SO_Hdr_Ref_No.ToString();
                        ordDetLNo = dtSalesDelDet[0].DO_Det_SO_Lno.ToString();
                        custRef = dtSalesOrdHdr[0].SO_Hdr_Pcode.ToString();
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

                if (custRef != "")
                {
                    var taDelOrd = new VIEW_DELIVERY_ORDERTableAdapter();
                    var dtDelOrd = taDelOrd.GetAppDoListByDateCust(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), custRef);
                    gvDoDet.DataSource = dtDelOrd;
                    gvDoDet.DataBind();
                }
                else
                {
                    var taDelOrd = new VIEW_DELIVERY_ORDERTableAdapter();
                    var dtDelOrd = taDelOrd.GetAppDoListByDate(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
                    gvDoDet.DataSource = dtDelOrd;
                    gvDoDet.DataBind();
                }
                gvDoDet.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }        
    }
}