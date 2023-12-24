using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Transport.DataSet;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesOrderPrint : System.Web.UI.Page
    {
        GlobalClass.clsNumToText numToEng = new GlobalClass.clsNumToText();

        double ordTotQty = 0;
        double ordTotAmt = 0;
        double ordAppQty = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            try
            {   
                LoadInitOrdDetGridData();
                SetOrdDetGridData();

                if (Session["OrderRefNo"] != null)
                    GetOrderData(Session["OrderRefNo"].ToString());
            }
            catch (Exception ex) 
            {

            }
        }

        private void GetOrderData( string orderRef)
        {
            var taSalesHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesDet = new tblSalesOrderDetTableAdapter();
            try
            {
                var dtSalesHdr = taSalesHdr.GetDataByHdrRef(orderRef.ToString());
                if (dtSalesHdr.Rows.Count > 0)
                {
                    lblOrdRefNo.Text = dtSalesHdr[0].SO_Hdr_Ref_No.ToString();
                    lblOrdDate.Text = dtSalesHdr[0].SO_Hdr_Date.ToString("dd/MM/yyyy");
                    lblExpDelDate.Text = Convert.ToDateTime(dtSalesHdr[0].SO_Hdr_Com1).ToString("dd/MM/yyyy");
                    lblOrdValidDate.Text = dtSalesHdr[0].SO_Hdr_Exc_Date.ToString("dd/MM/yyyy");

                    var taSaleType = new tblSalesTypeTableAdapter();
                    var dtSaleType = taSaleType.GetDataByTypeRef(Convert.ToInt32(dtSalesHdr[0].SO_Hdr_Sale_Type_Ref.ToString()));
                    if (dtSaleType.Rows.Count > 0)
                        lblSalePrefix.Text = dtSaleType[0].Sale_Type_Name.ToString();
                    else
                        lblSalePrefix.Text = "";

                    if (dtSalesHdr[0].SO_Hdr_Com2.ToString() == "1")
                        lblTransBy.Text = "Customer Transport";
                    else
                        lblTransBy.Text = "Company Transport";

                    var taTransType = new tblTransportTypeTableAdapter();
                    var dtTransType = taTransType.GetDataByTypeRef(Convert.ToInt32(dtSalesHdr[0].SO_Hdr_Exp_Type.ToString()));
                    if (dtTransType.Rows.Count > 0)
                        lblTransType.Text = dtTransType[0].Trans_Type_Name.ToString();
                    else
                        lblTransType.Text = "";

                    var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                    var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(dtSalesHdr[0].SO_Hdr_Pcode.ToString()));
                    if (dtPartyAdr.Rows.Count > 0)
                        lblCust.Text = dtPartyAdr[0].Par_Adr_Name.ToString();
                    else
                        lblCust.Text = "";

                    var taSalesLoc = new tblSalesLocMasTableAdapter();
                    var dtSalesLoc = taSalesLoc.GetDataByLocRef(dtSalesHdr[0].SO_Hdr_Com3.ToString());
                    if (dtSalesLoc.Rows.Count > 0)
                        lblLoc.Text = dtSalesLoc[0].Loc_Mas_Name.ToString();
                    else
                        lblLoc.Text = "";

                    var taSalesPer = new tblSalesPersonMasTableAdapter();
                    var dtSalesPer = taSalesPer.GetDataBySpRef(Convert.ToInt32(dtSalesHdr[0].SO_Hdr_Com4 == "" ? "0" : dtSalesHdr[0].SO_Hdr_Com4.ToString()));
                    if (dtSalesPer.Rows.Count > 0)
                        lblSp.Text = dtSalesPer[0].Sp_Full_Name.ToString();
                    else
                        lblSp.Text = "";

                    LoadInitOrdDetGridData();
                    SetOrdDetGridData();
                    var dt = new DataTable();
                    dt = (DataTable)ViewState["dtOrdDet"];
                    var dtSalesDet = taSalesDet.GetDataByHdrRef(orderRef.ToString());
                    foreach (dsSalesTran.tblSalesOrderDetRow dr in dtSalesDet.Rows)
                    {
                        dt.Rows.Add(dr.SO_Hdr_Ref, 0, dr.SO_Det_Ref_No, dr.SO_Det_Icode, dr.SO_Det_Itm_Desc, dr.SO_Det_Itm_Uom, dr.SO_Det_Itm_Uom,
                            Convert.ToDecimal(dr.SO_Det_T_C1).ToString("N2"), dr.SO_Det_Lin_Rat.ToString("N2"),
                            (Convert.ToDecimal(dr.SO_Det_T_C1) * dr.SO_Det_Lin_Rat).ToString("N2"),
                            Convert.ToDecimal(dr.SO_Det_T_C2).ToString("N2"), dr.SO_Det_Trans_Rat.ToString("N2"),
                           ((Convert.ToDecimal(dr.SO_Det_T_C1) * dr.SO_Det_Lin_Rat) + (Convert.ToDecimal(dr.SO_Det_T_C1) * dr.SO_Det_Trans_Rat)).ToString("N2"),
                            dr.SO_Det_Lin_Dis.ToString("N2"),
                            ((Convert.ToDecimal(dr.SO_Det_T_C1) * dr.SO_Det_Lin_Rat) + (Convert.ToDecimal(dr.SO_Det_T_C1) * dr.SO_Det_Trans_Rat)).ToString("N2"));
                    }
                    ViewState["dtOrdDet"] = dt;
                    SetOrdDetGridData();

                    foreach (GridViewRow gr in gvOrdDet.Rows)
                    {
                        var lblOrdQty = ((Label)(gr.FindControl("lblOrdQty"))).Text.ToString();
                        ordTotQty += Convert.ToDouble(lblOrdQty.Trim());

                        var lblOrdAmt = ((Label)(gr.FindControl("lblOrdNetAmt"))).Text.ToString();
                        ordTotAmt += Convert.ToDouble(lblOrdAmt.Trim());
                    }

                    if (gvOrdDet.Rows.Count > 0)
                    {
                        lblTotOrdQty.Text = ordTotQty.ToString("N2");
                        lblTotOrdVal.Text = ordTotAmt.ToString("N2");
                        //lblOrdValText.Text = "= " + numToEng.changeNumericToWords(ordTotAmt) + " Only =";
                        lblTotOrdQty.Visible = true;
                        lblTotOrdVal.Visible = true;
                    }
                    else
                    {
                        lblTotOrdQty.Text = "0.00";
                        lblTotOrdVal.Text = "0.00";
                        //lblOrdValText.Text = "";
                        lblTotOrdQty.Visible = false;
                        lblTotOrdVal.Visible = false;
                    }

                    if (dtSalesHdr[0].SO_Hdr_HPC_Flag == "P" || (dtSalesHdr[0].SO_Hdr_HPC_Flag == "H" && dtSalesHdr[0].SO_Hdr_Status == "2"))
                    {
                        gvOrdDet.Enabled = false;
                    }
                    pnlOrdDet.Visible = true;

                    var taOrd = new VIEW_SALES_ORDERTableAdapter();
                    var dtOrd = new DataTable();
                    dtOrd = taOrd.GetDataByAppOrdRef(orderRef.ToString());
                    gvApprSoDet.DataSource = dtOrd;
                    gvApprSoDet.DataBind();

                    foreach (GridViewRow gr in gvApprSoDet.Rows)
                    {
                        var lblOrdApprQty = ((Label)(gr.FindControl("lblOrdApprQty"))).Text.ToString();
                        ordAppQty += Convert.ToDouble(lblOrdApprQty.Trim());
                    }

                    lblTotSoQty.Visible = true;
                    if (ordTotQty > 0)
                        lblTotSoQty.Text = "Total Order Qty: " + ordTotQty.ToString("N2");
                    else
                        lblTotSoQty.Text = "Total Order Qty: 0.00";

                    lblTotAppQty.Visible = true;
                    if (ordAppQty > 0)
                        lblTotAppQty.Text = "Total Approved Qty: " + ordAppQty.ToString("N2");
                    else
                        lblTotAppQty.Text = "Total Approved Qty: 0.00";

                    pnlSoAppStat.Visible = true;

                    generate_comments(dtSalesHdr[0].SO_Hdr_Ref_No.ToString());
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void generate_comments(string ordref)
        {
            var com = new tbl_Tran_ComTableAdapter();
            var dt = new dsProcTran.tbl_Tran_ComDataTable();
            var taEmp = new View_Emp_BascTableAdapter();

            dt = com.GetDataByRefNo(ordref);
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

                //imgimage.ImageUrl = "./handler/hnd_image.ashx?id=" + dr.app_id;

                //imgimage.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + dr.Com_App_Id + "'";

                var dtEmp = taEmp.GetDataByEmpId(dr.Com_App_Id.ToString());

                imgimage.ImageUrl = "~/Module/HRMS/Tools/getEmpPic.ashx?EmpRefNo='" + (dtEmp.Rows.Count > 0 ? dtEmp[0].EmpRefNo : "") + "'";

                ctl.ID = "ctl_" + phcomments.Controls.Count.ToString();

                lblname.Text = dr.Com_App_Name.ToString() + " (" + dr.Com_App_Desig.ToString() + ")";
                lbldate.Text = dr.Com_App_Date.ToString();
                celcomm.InnerText = dr.Com_Gen_Com.ToString();

                phcomments.Controls.Add(ctl);
            }
        }

        private void ClearData()
        {
            try
            {
                lblOrdRefNo.Text = "";

                lblOrdDate.Text = DateTime.Now.ToString("dd/MM/yyy");
                lblExpDelDate.Text = DateTime.Now.ToString("dd/MM/yyy");
                lblOrdValidDate.Text = DateTime.Now.AddDays(30).ToString("dd/MM/yyy");

                lblSalePrefix.Text = "";
                lblTransBy.Text = "";
                lblTransType.Text = "";
                
                lblCust.Text = "";
                lblLoc.Text = "";
                lblSp.Text = "";

                lblTotOrdQty.Text = "0.00";
                lblTotOrdVal.Text = "0.00";
                //lblOrdValText.Text = "";

                var taOrd = new VIEW_SALES_ORDERTableAdapter();
                var dtOrd = new DataTable();
                dtOrd = taOrd.GetDataByAppOrdRef("");
                gvApprSoDet.DataSource = dtOrd;
                gvApprSoDet.DataBind();

                lblTotSoQty.Visible = false;
                lblTotAppQty.Visible = false;
                pnlSoAppStat.Visible = false;

                hfRefNo.Value = "0";

                LoadInitOrdDetGridData();
                SetOrdDetGridData();
                gvOrdDet.Enabled = true;

                pnlOrdDet.Visible = false;
            }
            catch (Exception ex) 
            {

            }
        }
        
        #region Order Details Gridview
        protected void LoadInitOrdDetGridData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ORD_HDR_REF", typeof(string));
            dt.Columns.Add("ORD_DET_REF", typeof(string));
            dt.Columns.Add("ORD_DET_REF_NO", typeof(string));
            dt.Columns.Add("ORD_ITEM_REF", typeof(string));
            dt.Columns.Add("ORD_ITEM_NAME", typeof(string));
            dt.Columns.Add("ORD_ITEM_UOM_REF", typeof(string));
            dt.Columns.Add("ORD_ITEM_UOM", typeof(string));
            dt.Columns.Add("ORD_QTY", typeof(string));
            dt.Columns.Add("ORD_RATE", typeof(string));
            dt.Columns.Add("ORD_AMOUNT", typeof(string));
            dt.Columns.Add("ORD_FREE_BAG", typeof(string));
            dt.Columns.Add("ORD_TRANS_RATE", typeof(string));
            dt.Columns.Add("ORD_GRS_AMOUNT", typeof(string));
            dt.Columns.Add("ORD_DISCOUNT", typeof(string));
            dt.Columns.Add("ORD_NET_AMOUNT", typeof(string));
            ViewState["dtOrdDet"] = dt;
        }

        protected void SetOrdDetGridData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtOrdDet"];

                gvOrdDet.DataSource = dt;
                gvOrdDet.DataBind();
            }
            catch (Exception ex) { }
        }
        #endregion
    }
}