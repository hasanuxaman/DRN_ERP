using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Transport.DataSet;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;

namespace DRN_WEB_ERP.Module.Transport.Forms
{
    public partial class frmLoadSlipVatPendList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            var taLs = new tbl_TrTr_Load_SlipTableAdapter();
            var dtLS = new dsTransMas.tbl_TrTr_Load_SlipDataTable();

            if (empRef == "000044" || empRef == "000045" || empRef == "000139" || empRef == "000142" || empRef == "000011")//VAT-Rasheduzzaman,Sirajul Islam,Monirul Islam,Rokon Uddin
                dtLS = taLs.GetDataByVatPend();

            gvLsVatPend.DataSource = dtLS;
            gvLsVatPend.DataBind();
        }

        protected void gvLsVatPend_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            var taLs = new tbl_TrTr_Load_SlipTableAdapter();
            var dtLS = new dsTransMas.tbl_TrTr_Load_SlipDataTable();

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            try
            {
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var lblLsRefNo = (Label)(row.FindControl("lblLsRefNo"));

                taLs.UpdateLsVatReject(null, lblLsRefNo.Text.ToString());

                if (empRef == "000044" || empRef == "000045" || empRef == "000139" || empRef == "000142" || empRef == "000011")//VAT-Rasheduzzaman,Sirajul Islam,Monirul Islam,Rokon Uddin
                    dtLS = taLs.GetDataByVatPend();

                gvLsVatPend.DataSource = dtLS;
                gvLsVatPend.DataBind();

                //tblMsg.Rows[0].Cells[0].InnerText = "Data updated successfully.";
                //tblMsg.Rows[1].Cells[0].InnerText = "";
                //ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                //tblMsg.Rows[0].Cells[0].InnerText = "Data updated successfully.";
                //tblMsg.Rows[1].Cells[0].InnerText = "";
                //ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnVatChlnOk_Click(object sender, EventArgs e)
        {
            var taLs = new tbl_TrTr_Load_SlipTableAdapter();
            var taLsExt = new tbl_TrTr_Load_Slip_ExtTableAdapter();
            var taInvTrnHdr = new tbl_InTr_Trn_HdrTableAdapter();

            var dtLS = new dsTransMas.tbl_TrTr_Load_SlipDataTable();
            var dtLsExt = new dsTransMas.tbl_TrTr_Load_Slip_ExtDataTable();

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taInvTrnHdr.Connection);

            try
            {
                taInvTrnHdr.AttachTransaction(myTran);
                taLs.AttachTransaction(myTran);

                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var lblLsRefNo = (Label)(row.FindControl("lblLsRefNo"));

                dtLsExt = taLsExt.GetDataByLsRefNo(lblLsRefNo.Text.ToString());
                if (dtLsExt.Rows.Count > 0)
                {
                    if (dtLsExt[0].LS_Ext_Ext_Data3 != "GHAT")
                    {
                        if (dtLsExt[0].LS_DO_Chln1 != "")
                            taInvTrnHdr.UpdateChallanDeliveryStatus("P", dtLsExt[0].LS_DO_Chln1.ToString());

                        if (dtLsExt[0].LS_DO_Chln2 != "")
                            taInvTrnHdr.UpdateChallanDeliveryStatus("P", dtLsExt[0].LS_DO_Chln2.ToString());

                        if (dtLsExt[0].LS_DO_Chln3 != "")
                            taInvTrnHdr.UpdateChallanDeliveryStatus("P", dtLsExt[0].LS_DO_Chln3.ToString());

                        taLs.UpdateLsVat("OK", DateTime.Now, "", lblLsRefNo.Text.ToString());
                    }
                    else
                    {
                        if (dtLsExt[0].LS_DO_Chln1 != "")
                            taInvTrnHdr.UpdateGhatTransferDeliveryStatus("P", dtLsExt[0].LS_DO_Chln1.ToString());

                        if (dtLsExt[0].LS_DO_Chln2 != "")
                            taInvTrnHdr.UpdateGhatTransferDeliveryStatus("P", dtLsExt[0].LS_DO_Chln2.ToString());

                        if (dtLsExt[0].LS_DO_Chln3 != "")
                            taInvTrnHdr.UpdateGhatTransferDeliveryStatus("P", dtLsExt[0].LS_DO_Chln3.ToString());

                        taLs.UpdateLsVat("OK", DateTime.Now, "", lblLsRefNo.Text.ToString());
                    }                    
                }

                myTran.Commit();

                if (empRef == "000044" || empRef == "000045" || empRef == "000139" || empRef == "000142" || empRef == "000011")//VAT-Rasheduzzaman,Sirajul Islam,Monirul Islam,Rokon Uddin
                    dtLS = taLs.GetDataByVatPend();

                gvLsVatPend.DataSource = dtLS;
                gvLsVatPend.DataBind();

                //tblMsg.Rows[0].Cells[0].InnerText = "Data updated successfully.";
                //tblMsg.Rows[1].Cells[0].InnerText = "";
                //ModalPopupExtenderMsg.Show();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data processig error.";
                tblMsg.Rows[1].Cells[0].InnerText = ex.Message.ToString();
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvLsVatPend_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLsVatPend.PageIndex = e.NewPageIndex;

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

            var taLs = new tbl_TrTr_Load_SlipTableAdapter();
            var dtLS = new dsTransMas.tbl_TrTr_Load_SlipDataTable();

            if (empRef == "000044" || empRef == "000045" || empRef == "000139" || empRef == "000142" || empRef == "000011")//VAT-Rasheduzzaman,Sirajul Islam,Monirul Islam,Rokon Uddin
                dtLS = taLs.GetDataByVatPend();

            gvLsVatPend.DataSource = dtLS;
            gvLsVatPend.DataBind();
        }        
    }
}