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

namespace DRN_WEB_ERP.Module.Procurement.Forms
{
    public partial class frmQtnValEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tblmaster.BgColor = "#f0f8ff";

            if (!Page.IsPostBack)
            {
                lblMsg.Visible = false;

                string mprref = Request.QueryString["mprref"].ToString();
                string icode = Request.QueryString["icode"].ToString();
                string pcode = Request.QueryString["pcode"].ToString();
                Session["sessionCurrentRefFocus"] = Request.QueryString["focusref"].ToString();

                load_data(mprref,icode, pcode);
            }
        }

        private void load_data(string mprref,string icode, string pcode)
        {
            tbl_Qtn_DetTableAdapter quo = new tbl_Qtn_DetTableAdapter();
            dsProcTran.tbl_Qtn_DetRow dr;

            dr = quo.GetDataByMprRefItemCodeParty(mprref, icode, pcode)[0];
            txtid.Text = mprref;
            txtparty.Text = dr.Qtn_Par_Code.ToString() + ": " + dr.Qtn_Par_Det.ToString();
            txtitem.Text = dr.Qtn_Itm_Code.ToString() + ": " + dr.Qtn_Itm_Det.ToString();
            txtqty.Text = dr.Qtn_Itm_Qty.ToString() + " " + dr.Qtn_Itm_Uom.ToString();


            txtspecification.Text = dr.Qtn_Itm_Spec.ToString();
            txtbrand.Text = dr.Qtn_Itm_Brand.ToString();
            txtorigin.Text = dr.Qtn_Itm_Origin.ToString();
            txtpacking.Text = dr.Qtn_Itm_Packing.ToString();
            txtrate.Text = dr.Qtn_Itm_Rate.ToString();
            //lbltk.Text = "TK. / " + dr.Qtn_Itm_Uom.ToString();
            lbltk.Text = "\t\t\t\t              Unit: " + dr.Qtn_Itm_Uom;
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            tbl_Qtn_DetTableAdapter taQtn = new tbl_Qtn_DetTableAdapter();
            tbl_PuTr_CS_SumTableAdapter taCsSum = new tbl_PuTr_CS_SumTableAdapter();
            View_Qtn_Val_SumTableAdapter taQtnValSum = new View_Qtn_Val_SumTableAdapter();

            SqlTransaction myTrn = GlobalClass.clsDbHelper.OpenTransaction(taQtn.Connection);

            try
            {
                taQtn.AttachTransaction(myTrn);
                taCsSum.AttachTransaction(myTrn);                
                taQtnValSum.AttachTransaction(myTrn);

                string mprref = Request.QueryString["mprref"].ToString();
                string icode = Request.QueryString["icode"].ToString();
                string pcode = Request.QueryString["pcode"].ToString();
                string csref = Request.QueryString["focusref"].ToString();

                var dt = taQtn.GetDataByMprRefItemCodeParty(mprref, icode, pcode);
                if (dt.Rows.Count > 0)
                {
                    taQtn.UpdateQtnPrice(Convert.ToDecimal(txtrate.Text), (Convert.ToDecimal(txtrate.Text) * Convert.ToDecimal(dt[0].Qtn_Itm_Qty)),
                        txtspecification.Text, txtbrand.Text, txtorigin.Text, txtpacking.Text, mprref, icode, pcode);

                    var dtQtnValSum = taQtnValSum.GetData(csref);
                    foreach (dsProcTran.View_Qtn_Val_SumRow dr in dtQtnValSum.Rows)
                    {
                        var dtCsSum = taCsSum.GetDataByQtnPartyRef(dr.Qtn_Ref_No, dr.Qtn_Par_Code, "QTN");
                        if (dtCsSum.Rows.Count > 0)
                            taCsSum.UpdateCsSumByAudit(dr.Qtn_Val, (dr.Qtn_Val + dtCsSum[0].CS_Cary_Amt + dtCsSum[0].CS_Load_Amt) - dtCsSum[0].CS_Disc_Amt, dr.Qtn_Ref_No, dr.Qtn_Par_Code, "QTN");
                    }

                    myTrn.Commit();

                    //Response.Redirect("frmCsAppr.aspx");                    
                }
                Session["QtnMprRefNo"] = mprref.ToString();
                Session["QtnItemCode"] = icode.ToString();
                Response.Redirect("./frmCsQtnDet.aspx", false);
            }
            catch (Exception ex) 
            {                
                lblMsg.Visible = true;                
                myTrn.Rollback();
            }
        }
    }
}