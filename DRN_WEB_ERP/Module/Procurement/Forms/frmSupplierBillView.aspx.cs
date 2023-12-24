using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;


namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmSupplierBillView : System.Web.UI.Page
    {
        double totBillAmt;

        GlobalClass.clsNumToText NumToText = new GlobalClass.clsNumToText();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            if (Request.QueryString["BillRef"] != null)
            {
                var BillRef = Request.QueryString["BillRef"].ToString();

                var taViewBillDet = new View_PuTr_Po_Bill_DetTableAdapter();
                var dtViewBillDet = taViewBillDet.GetDataByBillRef(Convert.ToInt32(BillRef.ToString()));
                if (dtViewBillDet.Rows.Count > 0)
                {
                    txtSuppName.Text = dtViewBillDet[0].Bill_Hdr_Ext_Data1.ToString();
                    txtBillRefNo.Text = dtViewBillDet[0].Bill_Hdr_Ref_No.ToString();
                    txtBillDate.Text = dtViewBillDet[0].Bill_Hdr_Date.ToString("dd/MM/yyyy");
                    txtSupBillNo.Text = dtViewBillDet[0].Bill_Hdr_Party_Bill_No.ToString();
                    txtBillEntDt.Text = dtViewBillDet[0].Bill_Hdr_Ent_Dt.ToString("dd/MM/yyyy");

                    txtBillAmt.Text = dtViewBillDet[0].Bill_Hdr_Tot_Amount.ToString("N2");
                    txtBillAdjAmt.Text = dtViewBillDet[0].Bill_Hdr_Adj_Amount.ToString("N2");
                    txtBillNetAmt.Text = dtViewBillDet[0].Bill_Hdr_Net_Amount.ToString("N2");
                    txtBillPayAmt.Text = dtViewBillDet[0].Bill_Hdr_Pay_Amount.ToString("N2");
                    txtBillDueAmt.Text = dtViewBillDet[0].Bill_Hdr_Due_Amount.ToString("N2");

                    lbltot.Text = "Net Bill Amount: " + dtViewBillDet[0].Bill_Hdr_Net_Amount.ToString("N2") + " [ " + NumToText.changeNumericToWords(dtViewBillDet[0].Bill_Hdr_Net_Amount.ToString()) + " ]";
                }
                gvBillDet.DataSource = dtViewBillDet;
                gvBillDet.DataBind();
            }
        }

        protected void gvBillDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblBillAmount = ((Label)e.Row.FindControl("lblBillAmount"));
                totBillAmt += Convert.ToDouble(lblBillAmount.Text.Trim());
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotAmt = (Label)e.Row.FindControl("lblTotAmt");
                lblTotAmt.Text = totBillAmt.ToString("N2");
            }
        }
    }
}