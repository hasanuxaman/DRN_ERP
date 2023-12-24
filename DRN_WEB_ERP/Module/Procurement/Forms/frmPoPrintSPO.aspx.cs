using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcMasTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmPoPrintSPO : System.Web.UI.Page
    {
        GlobalClass.clsNumToText NumToText = new GlobalClass.clsNumToText();

        double totalAmount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            tblmaster.BgColor = "#f0f8ff";

            if (Request.QueryString["PO_REF"] != null)            
                GetPage(Request.QueryString["PO_REF"].ToString());            
        }

        private void GetPage(string porefno)
        {
            var taPoHdr = new tbl_PuTr_PO_HdrTableAdapter();
            var taPoDet = new tbl_PuTr_PO_DetTableAdapter();
            var taSupAdr = new tbl_PuMa_Par_AdrTableAdapter();

            var dtPoHdr = taPoHdr.GetDataByHdrRef(porefno);
            if (dtPoHdr.Rows.Count > 0)
            {
                lbldate.Text = dtPoHdr[0].PO_Hdr_DATE.ToString("dd/MM/yyyy");
                lblporef.Text = dtPoHdr[0].PO_Hdr_Ref.ToString();
                var dtSupAdr=taSupAdr.GetDataBySupAdrRef(dtPoHdr[0].PO_Hdr_Pcode.ToString());
                if (dtSupAdr.Rows.Count > 0)
                {
                    lblto.Text = dtSupAdr[0].Par_Adr_Name.ToString();
                    lbladd.Text = dtSupAdr[0].Par_Adr_Addr.ToString();
                }

                var dtPoDet = taPoDet.GetDataByPoDetRef(porefno);
                gvSPODet.DataSource = dtPoDet;
                gvSPODet.DataBind();
            }
            lbltot.Text = totalAmount.ToString("N2") + " [" + NumToText.changeNumericToWords(totalAmount.ToString("N2")) + "]";           
        }

        protected void gvSPODet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                totalAmount = totalAmount + Convert.ToDouble(lblAmount.Text.ToString());
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblGrandTotal = (Label)e.Row.FindControl("lblGrandTotal");
                lblGrandTotal.Text = totalAmount.ToString("N2");
            }
        }
    }
}