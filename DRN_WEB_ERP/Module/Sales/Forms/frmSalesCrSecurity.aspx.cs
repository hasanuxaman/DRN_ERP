using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesCrSecurity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
            if (empRef == "000011")//-----Imran
                btnUploadCreditAeging.Visible = true;
            else
                btnUploadCreditAeging.Visible = false;

            if (Request.QueryString["DLR"] != null)
            {
                var CustRef = Request.QueryString["DLR"].ToString();

                var taParAdr = new tblSalesPartyAdrTableAdapter();
                var taParCrSec = new tblSalesCrLimitSecurityTableAdapter();

                var dtParAdr = taParAdr.GetDataByPartyAdrRef(Convert.ToInt32(CustRef.ToString()));
                if (dtParAdr.Rows.Count > 0)
                {
                    txtCustCrLimit.Text = dtParAdr[0].Par_Adr_Cr_Limit.ToString("N2");
                    txtCustCrPeriod.Text = dtParAdr[0].Par_Adr_Cr_Days.ToString();
                }
                var dtCrSec = taParCrSec.GetDataByPartyRef(Convert.ToInt32(CustRef.ToString()));
                {
                    txtCrSecBG.Text = dtCrSec[0].Sec_BG.ToString("N2");
                    txtCrSecFDR.Text = dtCrSec[0].Sec_FDR.ToString("N2");
                    txtCrSecSDR.Text = dtCrSec[0].Sec_SDR.ToString("N2");
                    txtCrSecLAND.Text = dtCrSec[0].Sec_LAND.ToString("N2");
                    txtCrSecCHQ.Text = dtCrSec[0].Sec_CHQ.ToString("N2");
                    txtCrSecNOA.Text = dtCrSec[0].Sec_NOA.ToString("N2");
                    txtCrSecTotAmt.Text = dtCrSec[0].Sec_Tot_Amt.ToString("N2");
                }
            }
        }

        protected void btnUploadCreditAeging_Click(object sender, EventArgs e)
        {
            var taCrReal = new tblSalesCreditRealizeTableAdapter();
            var taCrAegingUpload = new view_temp_credit_aeging_uploadTableAdapter();

            int cnt = 0;
            var dtCregingUpload = taCrAegingUpload.GetDataByBillDateSort();
            foreach (dsSalesTran.view_temp_credit_aeging_uploadRow dr in dtCregingUpload.Rows)
            {
                cnt++;
                taCrReal.InsertCreditRealize(cnt.ToString(), dr.Bill_No, dr.Bill_Date, Convert.ToDecimal(dr.Bill_Amount), Convert.ToDecimal(dr.Receivable), dr.Par_Adr_Ref.ToString(), "1", "", "", "", "", "");
            }

            tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
            tblMsg.Rows[1].Cells[0].InnerText = "Total Record: " + cnt.ToString();
            ModalPopupExtenderMsg.Show();
        }
    }
}