using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DRN_WEB_ERP.Module.Transport.DataSet;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;

namespace DRN_WEB_ERP.Module.Transport.Forms
{
    public partial class frmVehicleDocDet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var taDocMas = new tbl_TrTr_Vsl_Doc_MasTableAdapter();
            var dtDocMas = taDocMas.GetDataSortBySeqNo();
            gvDocDet.DataSource = dtDocMas;
            gvDocDet.DataBind();

            var taVslMas = new View_TrTr_Vsl_MasTableAdapter();
            var dtVslMas = taVslMas.GetDataByDeliveryTrans();
            gvVslDocDet.DataSource = dtVslMas;
            gvVslDocDet.DataBind();
            gvVslDocDet.SelectedIndex = -1;
        }

        #region GridData
        public string GetVslType(string VslTypeRef)
        {
            var taVslType = new tbl_TrTr_Vsl_TypeTableAdapter();

            string vslTypeName = "";
            try
            {
                var dtVslType = taVslType.GetDataByVslTypeRef(Convert.ToInt32(VslTypeRef.ToString()));
                if (dtVslType.Rows.Count > 0)
                    vslTypeName = dtVslType[0].IsVslTypeNameNull() ? "" : dtVslType[0].VslTypeName.ToString();

                return vslTypeName;
            }
            catch (Exception ex) { return vslTypeName; }
        }

        public string GetDocDate(string VslRegNo, string DocRef)
        {
            var taDocDet = new tbl_TrTr_Vsl_Doc_DetTableAdapter();

            string docDt = "";
            try
            {
                var dtDocDet = taDocDet.GetDataByVslRegDocRef(VslRegNo, DocRef);
                if (dtDocDet.Rows.Count > 0)
                    docDt = dtDocDet[0].IsVsl_Doc_Last_Updt_DateNull() ? "" : dtDocDet[0].Vsl_Doc_Last_Updt_Date.ToString("dd-MMM-yyyy");

                return docDt;
            }
            catch (Exception ex) { return docDt; }
        }

        public string GetDocNextDate(string VslRegNo, string DocRef)
        {
            var taDocDet = new tbl_TrTr_Vsl_Doc_DetTableAdapter();

            string docNextDt = "";
            try
            {
                var dtDocDet = taDocDet.GetDataByVslRegDocRef(VslRegNo, DocRef);
                if (dtDocDet.Rows.Count > 0)
                    docNextDt = dtDocDet[0].IsVsl_Doc_Next_Updt_DateNull() ? "" : dtDocDet[0].Vsl_Doc_Next_Updt_Date.ToString("dd-MMM-yyyy");

                return docNextDt;
            }
            catch (Exception ex) { return docNextDt; }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var taVslMas = new tbl_TrTr_Vsl_MasTableAdapter();
            var taVslPic = new tbl_TrTr_Vsl_PicTableAdapter();

            try
            {
                var dtVslMas = taVslMas.GetDataByVslRegNo(txtSearch.Text.Trim().ToString());
                if (dtVslMas.Rows.Count > 0)
                {
                    hfVslRegNo.Value = dtVslMas[0].Vsl_Mas_No.ToString();

                    var taDocMas = new tbl_TrTr_Vsl_Doc_MasTableAdapter();
                    var dtDocMas = taDocMas.GetDataSortBySeqNo();
                    gvDocDet.DataSource = dtDocMas;
                    gvDocDet.DataBind();

                    var dtVslMasPic = taVslPic.GetDataByVslRegNo(hfVslRegNo.Value.ToString());
                    if (dtVslMasPic.Rows.Count > 0)
                    {
                        hlVslPic.NavigateUrl = "~/Module/Transport/Tools/getVslPic.ashx?VslRegNo='" + txtSearch.Text.Trim().ToString() + "'";
                        imgVslPic.ImageUrl = "~/Module/Transport/Tools/getVslPic.ashx?VslRegNo='" + txtSearch.Text.Trim().ToString() + "'";
                    }
                    else
                    {
                        hlVslPic.NavigateUrl = "";
                        imgVslPic.ImageUrl = "~/Image/NoImage.gif";
                    }

                    btnClearSrch.Enabled = true;
                }
                else                
                    ClearData();                
            }
            catch (Exception ex) { }
        }

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var taDocDet = new tbl_TrTr_Vsl_Doc_DetTableAdapter();
            var taDocMas = new tbl_TrTr_Vsl_Doc_MasTableAdapter();

            GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
            var lblDocRef = (Label)(row.FindControl("lblDocRef"));
            var lblDocName = (Label)(row.FindControl("lblDocName"));
            var lblLastUpdt = (Label)(row.FindControl("lblLastUpdt"));
            var txtLastUpdt = (TextBox)(row.FindControl("txtLastUpdt"));
            var txtNextUpdt = (TextBox)(row.FindControl("txtNextUpdt"));

            DateTime? lastUpdtDt = null;
            if (txtLastUpdt.Text.Length > 0) lastUpdtDt = Convert.ToDateTime(txtLastUpdt.Text.Trim());

            DateTime? nextUpdtDt = null;
            if (txtNextUpdt.Text.Length > 0) nextUpdtDt = Convert.ToDateTime(txtNextUpdt.Text.Trim());

            var dtDocDet = taDocDet.GetDataByVslRegDocRef(hfVslRegNo.Value.Trim(), lblDocRef.Text.Trim());
            if (dtDocDet.Rows.Count > 0)
            {
                taDocDet.UpdateDocDet(lastUpdtDt, lastUpdtDt, nextUpdtDt, lblDocName.Text.Trim(), "", "", DateTime.Now, "", "1", "", hfVslRegNo.Value.Trim(), lblDocRef.Text.Trim());
            }
            else
            {
                taDocDet.InsertDocDet(hfVslRegNo.Value.Trim(), lblDocRef.Text.Trim(), lastUpdtDt, lastUpdtDt, nextUpdtDt, lblDocName.Text.Trim(), "", "", DateTime.Now, "", "1", "");
            }

            var dtDocMas = taDocMas.GetDataSortBySeqNo();
            gvDocDet.DataSource = dtDocMas;
            gvDocDet.DataBind();

            var taVslMas = new View_TrTr_Vsl_MasTableAdapter();
            var dtVslMas = taVslMas.GetDataByDeliveryTrans();
            gvVslDocDet.DataSource = dtVslMas;
            gvVslDocDet.DataBind();
            gvVslDocDet.SelectedIndex = -1;
        }

        private void ClearData()
        {
            hfVslRegNo.Value = "0";            

            txtSearch.Text = "";
            btnClearSrch.Enabled = false;

            var taDocMas = new tbl_TrTr_Vsl_Doc_MasTableAdapter();
            var dtDocMas = taDocMas.GetDataSortBySeqNo();
            gvDocDet.DataSource = dtDocMas;
            gvDocDet.DataBind();            

            //hlVslPic.NavigateUrl = "~/Image/NoImage.gif";
            hlVslPic.NavigateUrl = "";
            imgVslPic.ImageUrl = "~/Image/NoImage.gif";

            var taVslMas = new View_TrTr_Vsl_MasTableAdapter();
            var dtVslMas = taVslMas.GetDataByDeliveryTrans();
            gvVslDocDet.DataSource = dtVslMas;
            gvVslDocDet.DataBind();
            gvVslDocDet.SelectedIndex = -1;
        }

        protected void gvDocDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblDocRef = (Label)(e.Row.FindControl("lblDocRef"));
                var lblDocName = (Label)(e.Row.FindControl("lblDocName"));
                var lblLastUpdt = (Label)(e.Row.FindControl("lblLastUpdt"));
                var txtLastUpdt = (TextBox)(e.Row.FindControl("txtLastUpdt"));
                var txtNextUpdt = (TextBox)(e.Row.FindControl("txtNextUpdt"));

                var taDocDet = new tbl_TrTr_Vsl_Doc_DetTableAdapter();
                var dtDocDet = taDocDet.GetDataByVslRegDocRef(txtSearch.Text.Trim(), lblDocRef.Text.Trim());
                if (dtDocDet.Rows.Count > 0)
                {
                    if (lblDocRef.Text.Trim() == dtDocDet[0].Vsl_Doc_Mas_Ref)
                    {
                        lblLastUpdt.Text = dtDocDet[0].IsVsl_Doc_Last_Updt_DateNull() ? "" : dtDocDet[0].Vsl_Doc_Last_Updt_Date.ToString("dd/MM/yyyy");
                        txtLastUpdt.Text = dtDocDet[0].IsVsl_Doc_Next_Updt_DateNull() ? "" : dtDocDet[0].Vsl_Doc_Next_Updt_Date.ToString("dd/MM/yyyy");
                    }
                }
            }
        }

        protected void gvVslDocDet_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taVslMas = new tbl_TrTr_Vsl_MasTableAdapter();
            var taVslPic = new tbl_TrTr_Vsl_PicTableAdapter();

            int indx = gvVslDocDet.SelectedIndex;

            if (indx != -1)
            {            
                try
                {
                    var lblVslRegNo = ((Label)gvVslDocDet.Rows[indx].FindControl("lblVslRegNo")).Text.Trim();

                    var dtVslMas = taVslMas.GetDataByVslRegNo(lblVslRegNo.ToString());
                    if (dtVslMas.Rows.Count > 0)
                    {
                        hfVslRegNo.Value = dtVslMas[0].Vsl_Mas_No.ToString();

                        txtSearch.Text = dtVslMas[0].Vsl_Mas_No.ToString();

                        var taDocMas = new tbl_TrTr_Vsl_Doc_MasTableAdapter();
                        var dtDocMas = taDocMas.GetDataSortBySeqNo();
                        gvDocDet.DataSource = dtDocMas;
                        gvDocDet.DataBind();

                        var dtVslMasPic = taVslPic.GetDataByVslRegNo(hfVslRegNo.Value.ToString());
                        if (dtVslMasPic.Rows.Count > 0)
                        {
                            hlVslPic.NavigateUrl = "~/Module/Transport/Tools/getVslPic.ashx?VslRegNo='" + txtSearch.Text.Trim().ToString() + "'";
                            imgVslPic.ImageUrl = "~/Module/Transport/Tools/getVslPic.ashx?VslRegNo='" + txtSearch.Text.Trim().ToString() + "'";
                        }
                        else
                        {
                            hlVslPic.NavigateUrl = "";
                            imgVslPic.ImageUrl = "~/Image/NoImage.gif";
                        }

                        btnClearSrch.Enabled = true;
                    }
                    else
                        ClearData();
                }
                catch (Exception ex) { }
            }            
        }

        protected void gvVslDocDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';this.style.color='blue';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink((Control)sender, "Select$" + e.Row.RowIndex);
            }
        }
    }
}