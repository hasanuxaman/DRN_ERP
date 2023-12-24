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
    public partial class frmSalesDORetailPend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Auto  Page Refresh
            //Response.AppendHeader("Refresh", "60");

            if (Page.IsPostBack) return;

            try
            {
                var taDelOrd = new VIEW_DELIVERY_ORDERTableAdapter();
                var dtDelOrd = taDelOrd.GetPendDoRtl();
                gvPendDoDetRtl.DataSource = dtDelOrd;
                gvPendDoDetRtl.DataBind();
                gvPendDoDetRtl.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void gvPendDoDetRtl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = gvPendDoDetRtl.SelectedIndex;

            if (indx != -1)
            {
                try
                {
                    var hfOrdHdrRef = (HiddenField)gvPendDoDetRtl.Rows[indx].FindControl("hfOrdHdrRef");
                    var lblOrdHdrRefNo = (Label)gvPendDoDetRtl.Rows[indx].FindControl("lblOrdHdrRefNo");
                    var hfOrdDetLno = (HiddenField)gvPendDoDetRtl.Rows[indx].FindControl("hfOrdDetLno");

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

        protected void btnEditDo_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfDoHdrRef = (HiddenField)(row.FindControl("hfDoHdrRef"));
                var lblDoHdrRefNo = (Label)(row.FindControl("lblDoHdrRefNo"));

                Response.Redirect("frmSalesDOEdit.aspx?DoHdrRef=" + Server.UrlEncode(hfDoHdrRef.Value.ToString()) + "&DoHdrRefNo=" + Server.UrlEncode(lblDoHdrRefNo.Text.ToString()));
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }            
        }
    }
}