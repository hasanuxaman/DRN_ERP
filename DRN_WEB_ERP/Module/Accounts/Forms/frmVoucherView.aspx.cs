using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Accounts.Forms
{
    public partial class frmVoucherView : System.Web.UI.Page
    {
        decimal totDrAmt = 0;
        decimal totCrAmt = 0;

        string rptFile;
        string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            if (Request.QueryString["JvRefNoView"] != null)
            {
                LoadInitVoucherDetGridData();

                var dt = new DataTable();
                dt = (DataTable)ViewState["dtVoucherDet"];

                var taJv = new tbl_Acc_Fa_TeTableAdapter();
                var dtJv = taJv.GetDataByJvRef(Request.QueryString["JvRefNoView"].ToString());
                if (dtJv.Rows.Count > 0)
                {
                    lblJvNo.Text = dtJv[0].Trn_Ref_No.ToString();
                    lblJvDate.Text = dtJv[0].Trn_DATE.ToString("dd/MM/yyyy");
                    lblJrnType.Text = dtJv[0].Trn_Jrn_Type.ToString();
                }
                foreach (dsAccTran.tbl_Acc_Fa_TeRow dr in dtJv.Rows)
                {
                    var TRN_REF_NO = dr.Trn_Ref_No.ToString();
                    var TRN_LNO = dr.Trn_Line_No.ToString();
                    var COA_CODE = dr.Trn_Ac_Code.ToString();
                    var COA_NAME = dr.Trn_Ac_Desc.ToString();
                    var TRAN_DATE = dr.Trn_DATE.ToString("dd/MM/yyyy");
                    var TRAN_NARRATION = dr.Trn_Narration.ToString();
                    var TRAN_DEBIT = dr.Trn_Trn_type.ToString() == "D" ? dr.Trn_Amount : 0;
                    var TRAN_CREDIT = dr.Trn_Trn_type.ToString() == "C" ? dr.Trn_Amount : 0;

                    dt.Rows.Add(TRN_REF_NO, TRN_LNO, COA_CODE, COA_NAME, TRAN_DATE, TRAN_NARRATION, TRAN_DEBIT.ToString("N2"), TRAN_CREDIT.ToString("N2"));
                }
                
                SetVoucherDetGridData();

                foreach (GridViewRow gr in gvVoucherDet.Rows)
                {
                    var lblDrAmt = ((Label)(gr.FindControl("lblDr"))).Text.ToString();
                    totDrAmt += Convert.ToDecimal(lblDrAmt.Trim());

                    var lblCrAmt = ((Label)(gr.FindControl("lblCr"))).Text.ToString();
                    totCrAmt += Convert.ToDecimal(lblCrAmt.Trim());
                }

                Generate_Comments(Request.QueryString["JvRefNoView"].ToString());
            }
            else
            {

            }            
        }

        #region Voucher Details Gridview
        protected void LoadInitVoucherDetGridData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TRN_REF_NO", typeof(string));
            dt.Columns.Add("TRN_LNO", typeof(string));
            dt.Columns.Add("COA_CODE", typeof(string));
            dt.Columns.Add("COA_NAME", typeof(string));
            dt.Columns.Add("TRAN_DATE", typeof(string));
            dt.Columns.Add("TRAN_NARRATION", typeof(string));
            dt.Columns.Add("TRAN_DEBIT", typeof(string));
            dt.Columns.Add("TRAN_CREDIT", typeof(string));
            ViewState["dtVoucherDet"] = dt;
        }

        protected void SetVoucherDetGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtVoucherDet"];

                gvVoucherDet.DataSource = dt;
                gvVoucherDet.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion

        private void Generate_Comments(string JvRefNo)
        {
            var com = new tbl_Tran_ComTableAdapter();
            var dt = new dsProcTran.tbl_Tran_ComDataTable();
            var taEmp = new View_Emp_BascTableAdapter();

            dt = com.GetDataByRefNo(JvRefNo);
            phcomments.Controls.Clear();

            var qtnSeqNo = 0;
            foreach (dsProcTran.tbl_Tran_ComRow dr in dt.Rows)
            {
                qtnSeqNo = dr.Com_Seq_no;

                Module.Procurement.Forms.UserControl.ctlQtnCom ctl = (Module.Procurement.Forms.UserControl.ctlQtnCom)LoadControl("~/Module/Procurement/Forms/UserControl/CtlQtnCom.ascx");

                Label lblname = (Label)ctl.FindControl("lblname");
                Label lbldate = (Label)ctl.FindControl("lbldate");
                HtmlTableCell celcomm = (HtmlTableCell)ctl.FindControl("celcomm");
                Image imgimage = (Image)ctl.FindControl("imgimage");

                var dtEmp = taEmp.GetDataByEmpId(dr.Com_App_Id.ToString());

                imgimage.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + (dtEmp.Rows.Count > 0 ? dtEmp[0].EmpRefNo : "") + "'";

                ctl.ID = "ctl_" + phcomments.Controls.Count.ToString();

                lblname.Text = dr.Com_App_Name.ToString() + " (" + dr.Com_App_Desig.ToString() + ")";
                lbldate.Text = dr.Com_App_Date.ToString();
                celcomm.InnerText = dr.Com_Gen_Com.ToString();

                phcomments.Controls.Add(ctl);
            }
        }

        protected void gvVoucherDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblDr = ((Label)e.Row.FindControl("lblDr"));
                totDrAmt += Convert.ToDecimal(lblDr.Text.Trim());

                var lblCr = (Label)e.Row.FindControl("lblCr");
                totCrAmt += Convert.ToDecimal(lblCr.Text.Trim());
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotDr = (Label)e.Row.FindControl("lblTotDr");
                lblTotDr.Text = totDrAmt.ToString("N2");

                Label lblTotCr = (Label)e.Row.FindControl("lblTotCr");
                lblTotCr.Text = totCrAmt.ToString("N2");
            }
        }

        protected void btnPrintJv_Click(object sender, EventArgs e)
        {
            reportInfo();
            var url = "frmShowAccReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void reportInfo()
        {
            if (lblJvNo.Text.Trim() != "")
            {
                var JrnTypeName = "";
                var taJrnType = new tbl_Acc_Fa_Jv_TypeTableAdapter();
                var dtJrnType = taJrnType.GetDataByJvCode(lblJrnType.Text);
                JrnTypeName = dtJrnType.Rows.Count > 0 ? dtJrnType[0].JV_Type_Name.ToString() : "";

                rptFile = "~/Module/Accounts/Reports/rptVoucher.rpt";

                rptSelcFormula = "{tbl_Acc_Fa_Te.Trn_Ref_No}='" + lblJvNo.Text.Trim() + "'";

                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
                Session["RptHdr"] = JrnTypeName;
            }
        }
    }
}