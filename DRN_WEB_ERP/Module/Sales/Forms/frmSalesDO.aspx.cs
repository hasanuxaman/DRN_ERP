using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Sales
{
    public partial class frmSalesDO : System.Web.UI.Page
    {
        double totDoQty = 0;
        double totDoAmt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            try
            {
                AutoCompleteExtenderSrch.ContextKey = "0";                

                var taSalesDelHdr = new tblSalesOrdDelHdrTableAdapter();
                var dtMaxHdrRef = taSalesDelHdr.GetMaxHdrRef(DateTime.Now.Year);
                var nextHdrRef = dtMaxHdrRef == null ? DateTime.Now.Year + "000001" : (Convert.ToInt32(dtMaxHdrRef) + 1).ToString("000000");
                
                var dtMaxHdrRefByYr = taSalesDelHdr.GetMaxHdrRefByYr(DateTime.Now.Year);
                var nextHdrRefByYr = dtMaxHdrRefByYr == null ? "000001" : (Convert.ToInt32(dtMaxHdrRefByYr) + 1).ToString("000000");
                var nextHdrRefNo = "ECIL-DO-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRefByYr).ToString("000000");
                txtDoRefNo.Text = nextHdrRefNo;
                txtDoDate.Text = DateTime.Now.ToString();

                var taTransType = new tblTransportTypeTableAdapter();
                var dtTransType = taTransType.GetData();
                ddlOrdTransMode.DataSource = dtTransType;
                ddlOrdTransMode.DataTextField = "Trans_Type_Name";
                ddlOrdTransMode.DataValueField = "Trans_Type_Ref";
                ddlOrdTransMode.DataBind();
                
                var taDist = new tblSalesDistrictTableAdapter();
                cboCustDist.DataSource = taDist.GetDataByAsc();
                cboCustDist.DataTextField = "DistName";
                cboCustDist.DataValueField = "DistRef";
                cboCustDist.DataBind();
                cboCustDist.Items.Insert(0, new ListItem("---Select---", "0"));

                cboCustThana.Items.Insert(0, new ListItem("---Select---", "0"));

                var taVslMas = new tbl_TrTr_Vsl_MasTableAdapter();
                cboTruckNo.DataSource = taVslMas.GetDataByAsc();
                cboTruckNo.DataTextField = "Vsl_Mas_No";
                cboTruckNo.DataValueField = "Vsl_Mas_Ref";
                cboTruckNo.DataBind();
                cboTruckNo.Items.Insert(0, new ListItem("---Select---", "0"));

                #region Load Order Data
                if (Request.QueryString["OrdHdrRef"] != null && Request.QueryString["OrdDetLno"] != null)
                {
                    var OrdRef = Request.QueryString["OrdHdrRef"].ToString();
                    var DetLno = Request.QueryString["OrdDetLno"].ToString();

                    var taSalesOrd = new VIEW_SALES_ORDERTableAdapter();
                    var dtSalesHdr = taSalesOrd.GetDataByHdrRef(OrdRef.ToString());
                    if (dtSalesHdr.Rows.Count > 0)
                    {
                        txtSearch.Text = dtSalesHdr[0].SO_Hdr_Ref + ":" + dtSalesHdr[0].SO_Hdr_Ref_No + ":" + dtSalesHdr[0].Par_Adr_Name;

                        ddlOrdTransMode.SelectedValue = dtSalesHdr[0].SO_Hdr_Exp_Type.ToString();
                        optTranBy.SelectedValue = dtSalesHdr[0].SO_Hdr_Com2.ToString();
                        if (dtSalesHdr[0].SO_Hdr_Com2.ToString() == "2")
                        {
                            txtTruckNo.Text = "";
                            txtTruckNo.Visible = false;
                            cboTruckNo.SelectedIndex = 0;
                            cboTruckNo.Visible = true;
                        }
                        else
                        {
                            txtTruckNo.Text = "";
                            txtTruckNo.Visible = true;
                            cboTruckNo.SelectedIndex = 0;
                            cboTruckNo.Visible = false;
                        }

                        var taSalesLoc = new tblSalesLocMasTableAdapter();
                        var dtSalesLoc = taSalesLoc.GetDataByLocRef(dtSalesHdr[0].SO_Hdr_Com3.ToString());
                        if (dtSalesLoc.Rows.Count > 0)
                        {
                            txtSalesLoc.Text = dtSalesLoc[0].Loc_Mas_Ref.ToString() + ":" + dtSalesLoc[0].Loc_Mas_Code.ToString() + ":" + dtSalesLoc[0].Loc_Mas_Name.ToString();
                            //hfOrdTransRate.Value = dtSalesLoc[0].Loc_Mas_Itm_Rate.ToString();
                        }
                        else
                        {
                            txtSalesLoc.Text = "";
                            hfOrdTransRate.Value = "0";
                        }

                        var custRef = dtSalesHdr[0].SO_Hdr_Pcode;
                        AutoCompleteExtendertxtRetailer1.ContextKey = custRef.ToString();
                        AutoCompleteExtendertxtRetailer2.ContextKey = custRef.ToString();

                        if (custRef.Length > 0)
                        {
                            var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                            var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                            if (dtPartyAdr.Rows.Count > 0)
                            {
                                //txtSearch.Text = dtPartyAdr[0].Par_Adr_Ref + ":" + dtPartyAdr[0].Par_Adr_Ref_No + ":" + dtPartyAdr[0].Par_Adr_Name;                                 
                            }
                            else
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer Details.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                            }
                            
                            //var dtSalesOrd = taSalesOrd.GetPendSoByDetLno(OrdRef.ToString(), Convert.ToInt16(DetLno));
                            var dtSalesOrd = taSalesOrd.GetDataByHdrRef(OrdRef.ToString());
                            gvOrdDet.DataSource = dtSalesOrd;
                            gvOrdDet.DataBind();
                            gvOrdDet.SelectedIndex = -1;

                            var taDelOrd = new VIEW_DELIVERY_ORDERTableAdapter();
                            //var dtDelOrd = taDelOrd.GetPendDoByOrdDetLno(OrdRef.ToString(), Convert.ToInt16(DetLno));
                            var dtDelOrd = taDelOrd.GetPendDoByOrdRef(OrdRef.ToString());
                            gvPendDoDet.DataSource = dtDelOrd;
                            gvPendDoDet.DataBind();
                            gvPendDoDet.SelectedIndex = -1;

                            foreach (GridViewRow gr in gvPendDoDet.Rows)
                            {
                                var lblPendDoQty = ((Label)(gr.FindControl("lblPendDoQty"))).Text.ToString();
                                totDoQty += Convert.ToDouble(lblPendDoQty.Trim());

                                var lblPendDoAmt = ((Label)(gr.FindControl("lblPendDoAmt"))).Text.ToString();
                                totDoAmt += Convert.ToDouble(lblPendDoAmt.Trim());
                            }

                            if (gvPendDoDet.Rows.Count > 0)
                            {
                                lblTotPendDoQty.Text = "Total Pending D/O Qty: " + totDoQty.ToString("N2");
                                lblTotPendDoAmt.Text = "Total Pending D/O Amt: " + totDoAmt.ToString("N2");
                                lblTotPendDoQty.Visible = true;
                                lblTotPendDoAmt.Visible = true;
                            }
                            else
                            {
                                lblTotPendDoQty.Text = "Total Pending D/O Qty: 0.00";
                                lblTotPendDoAmt.Text = "Total Pending D/O Amt: 0.00";
                                lblTotPendDoQty.Visible = false;
                                lblTotPendDoAmt.Visible = false;
                            }

                            txtSearch.Enabled = false;
                            btnClearSrch.Visible = true;
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer Ref.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Order Ref.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchOrderData();
        }

        private void SearchOrderData()
        {           
            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                var ordRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    ordRef = word;
                    break;
                }

                if (ordRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(ordRef, out result))
                    {                        
                        var taSalesOrd = new VIEW_SALES_ORDERTableAdapter();
                        var dtSalesOrd = taSalesOrd.GetDataByValidDaysHdrRef(ordRef.ToString());

                        var custRef = dtSalesOrd[0].SO_Hdr_Pcode.ToString();
                        AutoCompleteExtendertxtRetailer1.ContextKey = custRef.ToString();
                        AutoCompleteExtendertxtRetailer2.ContextKey = custRef.ToString();
                        
                        ddlOrdTransMode.SelectedValue = dtSalesOrd[0].SO_Hdr_Exp_Type.ToString();
                        optTranBy.SelectedValue = dtSalesOrd[0].SO_Hdr_Com2.ToString();

                        if (dtSalesOrd[0].SO_Hdr_Com2.ToString() == "2")
                        {
                            txtTruckNo.Text = "";
                            txtTruckNo.Visible = false;
                            cboTruckNo.SelectedIndex = 0;
                            cboTruckNo.Visible = true;
                        }
                        else
                        {
                            txtTruckNo.Text = "";
                            txtTruckNo.Visible = true;
                            cboTruckNo.SelectedIndex = 0;
                            cboTruckNo.Visible = false;
                        }
                        var taSalesLoc = new tblSalesLocMasTableAdapter();
                        var dtSalesLoc = taSalesLoc.GetDataByLocRef(dtSalesOrd[0].SO_Hdr_Com3.ToString());
                        if (dtSalesLoc.Rows.Count > 0)
                        {
                            txtSalesLoc.Text = dtSalesLoc[0].Loc_Mas_Ref.ToString() + ":" + dtSalesLoc[0].Loc_Mas_Code.ToString() + ":" + dtSalesLoc[0].Loc_Mas_Name.ToString();
                            //hfOrdTransRate.Value = dtSalesLoc[0].Loc_Mas_Itm_Rate.ToString();
                        }
                        else
                        {
                            txtSalesLoc.Text = "";
                            hfOrdTransRate.Value = "0";
                        }

                        //var taSalesOrd = new VIEW_SALES_ORDERTableAdapter();
                        //var dtSalesOrd = taSalesOrd.GetPendSoByCustRef(custRef.ToString());
                        var dtPendSalesOrdDet = taSalesOrd.GetPendSoByHdrRef(ordRef.ToString());
                        gvOrdDet.DataSource = dtPendSalesOrdDet;
                        gvOrdDet.DataBind();
                        gvOrdDet.SelectedIndex = -1;

                        var taDelOrd = new VIEW_DELIVERY_ORDERTableAdapter();
                        //var dtDelOrd = taDelOrd.GetPendDoByCust(custRef.ToString());
                        var dtDelOrd = taDelOrd.GetPendDoByOrdRef(ordRef.ToString());
                        gvPendDoDet.DataSource = dtDelOrd;
                        gvPendDoDet.DataBind();
                        gvPendDoDet.SelectedIndex = -1;

                        foreach (GridViewRow gr in gvPendDoDet.Rows)
                        {
                            var lblPendDoQty = ((Label)(gr.FindControl("lblPendDoQty"))).Text.ToString();
                            totDoQty += Convert.ToDouble(lblPendDoQty.Trim());

                            var lblPendDoAmt = ((Label)(gr.FindControl("lblPendDoAmt"))).Text.ToString();
                            totDoAmt += Convert.ToDouble(lblPendDoAmt.Trim());
                        }

                        if (gvPendDoDet.Rows.Count > 0)
                        {
                            lblTotPendDoQty.Text = "Total Pending D/O Qty: " + totDoQty.ToString("N2");
                            lblTotPendDoAmt.Text = "Total Pending D/O Amt: " + totDoAmt.ToString("N2");
                            lblTotPendDoQty.Visible = true;
                            lblTotPendDoAmt.Visible = true;
                        }
                        else
                        {
                            lblTotPendDoQty.Text = "Total Pending D/O Qty: 0.00";
                            lblTotPendDoAmt.Text = "Total Pending D/O Amt: 0.00";
                            lblTotPendDoQty.Visible = false;
                            lblTotPendDoAmt.Visible = false;
                        }

                        txtSearch.Enabled = false;
                        btnClearSrch.Visible = true;
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            try
            {
                chkIncAll.Checked = false;
                AutoCompleteExtenderSrch.ContextKey = "0";
                AutoCompleteExtendertxtRetailer1.ContextKey = "";
                AutoCompleteExtendertxtRetailer2.ContextKey = "";

                var taSalesDelHdr = new tblSalesOrdDelHdrTableAdapter();
                var dtMaxHdrRef = taSalesDelHdr.GetMaxHdrRef(DateTime.Now.Year);
                var nextHdrRef = dtMaxHdrRef == null ? DateTime.Now.Year + "000001" : (Convert.ToInt32(dtMaxHdrRef) + 1).ToString("000000");

                var dtMaxHdrRefByYr = taSalesDelHdr.GetMaxHdrRefByYr(DateTime.Now.Year);
                var nextHdrRefByYr = dtMaxHdrRefByYr == null ? "000001" : (Convert.ToInt32(dtMaxHdrRefByYr) + 1).ToString("000000");
                var nextHdrRefNo = "ECIL-DO-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRefByYr).ToString("000000");
                txtDoRefNo.Text = nextHdrRefNo;
                txtDoDate.Text = DateTime.Now.ToString();

                var taSalesOrd = new VIEW_SALES_ORDERTableAdapter();
                var dtSalesOrd = taSalesOrd.GetDataByHdrRef("");
                gvOrdDet.DataSource = dtSalesOrd;
                gvOrdDet.DataBind();
                gvOrdDet.SelectedIndex = -1;

                txtSearch.Text = "";
                txtTruckNo.Text = "";
                txtDriverName.Text = "";
                txtDriverContact.Text = "";
                txtDelAddr.Text = "";
                txtSalesLoc.Text = "";
                lblTransRate.Text = "";
                lblTransRate.Visible = false;
                hfOrdTransRate.Value = "0";                
                txtSearch.Enabled = true;
                btnClearSrch.Visible = false;

                chkDelaerPoint.Checked = false;
                txtRetailer1.Text = "";
                txtRetailer1.Enabled = true;
                txtDoQty1.Text = "";
                txtDoQty1.Enabled = true;
                txtRetailer2.Text = "";
                txtRetailer2.Enabled = true;
                txtDoQty2.Text = "";
                txtDoQty2.Enabled = true;

                lblTotPendDoQty.Text = "Total Pending D/O Qty: 0.00";
                lblTotPendDoAmt.Text = "Total Pending D/O Amt: 0.00";
                lblTotPendDoQty.Visible = false;
                lblTotPendDoAmt.Visible = false;

                var taDelOrd = new VIEW_DELIVERY_ORDERTableAdapter();
                //var dtDelOrd = taDelOrd.GetPendDoByCust("");
                var dtDelOrd = taDelOrd.GetPendDoByOrdRef("");
                gvPendDoDet.DataSource = dtDelOrd;
                gvPendDoDet.DataBind();
                gvPendDoDet.SelectedIndex = -1;
            }
            catch (Exception ex) { }
        }

        protected void btnCreateDo_Click(object sender, EventArgs e)
        {
            Page.Validate("btnCreate");

            var taSalesDelHdr = new tblSalesOrdDelHdrTableAdapter();
            var taSalesDelDet = new tblSalesOrdDelDetTableAdapter();
            var taSalesByRtl = new tblSalesByRetailerTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesDelHdr.Connection);

            try
            {
                #region Form Data Validation
                if (txtDelAddr.Text.Trim().Length <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "You have to enter delivery address first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                var locRef = "";
                if (optTranBy.SelectedValue == "2")
                {
                    var srchWords = txtSalesLoc.Text.Trim().Split(':');
                    foreach (string word in srchWords)
                    {
                        locRef = word;
                        break;
                    }

                    if (locRef.Length > 0)
                    {
                        var taSalesLoc = new tblSalesLocMasTableAdapter();
                        var dtSalesLoc = taSalesLoc.GetDataByLocRef(locRef.ToString());
                        if (dtSalesLoc.Rows.Count <= 0)
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid sales delivery location.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Enter sales delivery location for transport rate.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                //var vehicleNo = "";
                //if (optTranBy.SelectedValue == "1")
                //    vehicleNo = txtTruckNo.Text.Trim();
                //else
                //    vehicleNo = cboTruckNo.SelectedValue == "0" ? "" : cboTruckNo.SelectedItem.ToString();

                //if (vehicleNo == "")
                //{
                //    tblMsg.Rows[0].Cells[0].InnerText = "You have to enter vehile no first.";
                //    tblMsg.Rows[1].Cells[0].InnerText = "";
                //    ModalPopupExtenderMsg.Show();
                //    return;
                //}                

                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfSoHdrRef = (HiddenField)(row.FindControl("hfOrdHdrRef"));
                var hfSoHdrRefNo = (HiddenField)(row.FindControl("hfOrdDetLno"));
                var hfSoDetLno = (HiddenField)(row.FindControl("hfOrdDetLno"));
                var txtDoQty = (TextBox)(row.FindControl("txtDoQty"));
                var txtDoFreeQty = (TextBox)(row.FindControl("txtDoFreeQty"));
                var txtDoDelDate = (TextBox)(row.FindControl("txtDoDelDate"));

                if (txtDoQty.Text.Trim() == "" || txtDoQty.Text.Trim().Length <= 0 || Convert.ToDouble(txtDoQty.Text.Trim()) <= 0)
                {
                    if (txtDoFreeQty.Text.Trim() == "" || txtDoFreeQty.Text.Trim().Length <= 0 || Convert.ToDouble(txtDoFreeQty.Text.Trim()) <= 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Please enter D/O quantity first.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                DateTime? doDelDt = null;
                if (txtDoDelDate.Text.Length > 0) doDelDt = Convert.ToDateTime(txtDoDelDate.Text.Trim());
                if (doDelDt == null)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Please enter D/O date first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                if (txtDoFreeQty.Text.Trim() == "" || txtDoFreeQty.Text.Trim().Length <= 0)
                    txtDoFreeQty.Text = "0";


                #region Check Retailer Quantity
                double totDLRQty = 0;
                double totRTLQty = 0;

                if (txtDoQty.Text.Trim() != "" || txtDoQty.Text.Trim().Length >= 0 || Convert.ToDouble(txtDoQty.Text.Trim()) > 0)
                {
                    totDLRQty = totDLRQty + Convert.ToDouble(txtDoQty.Text.Trim());
                    if (txtDoFreeQty.Text.Trim() != "" || txtDoFreeQty.Text.Trim().Length >= 0 || Convert.ToDouble(txtDoFreeQty.Text.Trim()) > 0)
                    {
                        totDLRQty = totDLRQty + Convert.ToDouble(txtDoFreeQty.Text.Trim());
                    }
                }

                totRTLQty = Convert.ToDouble(txtDoQty1.Text.Trim().Length <= 0 ? "0" : txtDoQty1.Text.Trim()) + Convert.ToDouble(txtDoQty2.Text.Trim().Length <= 0 ? "0" : txtDoQty2.Text.Trim());

                if (totRTLQty > totDLRQty)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Retailer wise breakdown qty is incorrect.";
                    tblMsg.Rows[1].Cells[0].InnerText = "Retailer wise total qty (" + totRTLQty + ") is more than total D/O qty (" + totDLRQty + ")";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion
                #endregion

                #region D/O Qty and Credit Limit Validation
                var ordRef = hfSoHdrRef.Value.ToString();
                var ordRefNo = "";
                var custRef = "";

                if (ordRef.Length > 0)
                {
                    var taSalesOrd = new VIEW_SALES_ORDERTableAdapter();
                    var dtSalesOrd = taSalesOrd.GetDataByHdrRef(ordRef.ToString());
                    if (dtSalesOrd.Rows.Count > 0)
                    {
                        ordRef = dtSalesOrd[0].SO_Hdr_Ref.ToString();
                        ordRefNo = dtSalesOrd[0].SO_Hdr_Ref_No.ToString();
                        custRef = dtSalesOrd[0].Par_Adr_Ref.ToString();

                        var taSoDetChk = new tblSalesOrderDetTableAdapter();
                        var dtSoDetChk = taSoDetChk.GetDataByDetLno(ordRef.ToString(), Convert.ToInt16(hfSoDetLno.Value.ToString()));
                        if (dtSoDetChk.Rows.Count > 0)
                        {
                            #region D/O Qty Validation
                            if (Convert.ToDouble(txtDoQty.Text.Trim()) > dtSoDetChk[0].SO_Det_DO_Bal_Qty)
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to create D/O qty more than : " + dtSoDetChk[0].SO_Det_DO_Bal_Qty;
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                            if (Convert.ToDouble(txtDoFreeQty.Text.Trim()) > Convert.ToDouble(dtSoDetChk[0].SO_Det_Ext_Data2))
                            {
                                tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to create free bag D/O qty more than : " + dtSoDetChk[0].SO_Det_Ext_Data1;
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                            #endregion

                            #region Credit Limit Validiation
                            //var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                            //var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                            //var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;
                            //var custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                            //var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                            //var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                            //var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                            //var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                            //var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                            //var doAmt = Convert.ToDouble(txtDoQty.Text.Trim()) * Convert.ToDouble(dtSoDetChk[0].SO_Det_Lin_Rat);

                            //if (doAmt > (Convert.ToDouble(crLimit) - (drAmt - crAmt)))
                            //{
                            //    tblMsg.Rows[0].Cells[0].InnerText = "You are no allowed to exceed available credit limit : " + (Convert.ToDouble(crLimit) - (drAmt - crAmt)).ToString("N2");
                            //    tblMsg.Rows[1].Cells[0].InnerText = "";
                            //    ModalPopupExtenderMsg.Show();
                            //    return;
                            //}
                            #endregion
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Order Details.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
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
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Order.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                #region Get Retailer Details
                var taRtlAdr = new tblSalesPartyRtlTableAdapter();
                var dtRtlAdr = new dsSalesMas.tblSalesPartyRtlDataTable();

                var rtlRef1 = "";
                var rtlRef2 = "";

                double doQty1 = 0;
                double doQty2 = 0;

                if (chkDelaerPoint.Checked == false)
                {
                    if (txtDoQty1.Text.Trim().Length <= 0 && txtDoQty2.Text.Trim().Length <= 0)
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Enter retailer wise delivery qinatity first.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                if (txtDoQty1.Text.Trim().Length > 0 && txtDoQty1.Text.Trim() != "")
                {
                    doQty1 = Convert.ToDouble(txtDoQty1.Text.Trim());
                    var srchRtlWords1 = txtRetailer1.Text.Trim().Split(':');
                    foreach (string word in srchRtlWords1)
                    {
                        rtlRef1 = word;
                        break;
                    }

                    if (rtlRef1.Length > 0)
                    {
                        dtRtlAdr = taRtlAdr.GetDataByPartyRtlRef(rtlRef1);
                        if (dtRtlAdr.Rows.Count > 0)
                            rtlRef1 = dtRtlAdr[0].Par_Rtl_Ref.ToString();
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Retailer1.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Retailer1.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                if (txtDoQty2.Text.Trim().Length > 0 && txtDoQty2.Text.Trim() != "")
                {
                    doQty2 = Convert.ToDouble(txtDoQty2.Text.Trim());
                    var srchRtlWords2 = txtRetailer2.Text.Trim().Split(':');
                    foreach (string word in srchRtlWords2)
                    {
                        rtlRef2 = word;
                        break;
                    }

                    if (rtlRef2.Length > 0)
                    {
                        dtRtlAdr = taRtlAdr.GetDataByPartyRtlRef(rtlRef2);
                        if (dtRtlAdr.Rows.Count > 0)
                            rtlRef2 = dtRtlAdr[0].Par_Rtl_Ref.ToString();
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Retailer2.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Retailer2.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                #endregion

                var nextHdrRef = "000001";
                var nextHdrRefNo = "";

                taSalesDelHdr.AttachTransaction(myTran);
                taSalesDelDet.AttachTransaction(myTran);
                taSalesByRtl.AttachTransaction(myTran);

                var truckRef = "0";
                var truckNo = "";
                if (optTranBy.SelectedValue == "1")
                {
                    truckRef = "0";
                    truckNo = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToUpper(txtTruckNo.Text.Trim());
                }
                else
                {
                    truckRef = cboTruckNo.SelectedValue.ToString();
                    truckNo = cboTruckNo.SelectedValue == "0" ? "" : cboTruckNo.SelectedItem.ToString();
                }

                var driverName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtDriverName.Text.Trim());

                if (hfEditStatus.Value == "N")
                {
                    var dtMaxHdrRef = taSalesDelHdr.GetMaxHdrRef(DateTime.Now.Year);
                    nextHdrRef = dtMaxHdrRef == null ? DateTime.Now.Year + "000001" : (Convert.ToInt32(dtMaxHdrRef) + 1).ToString("000000");

                    var dtMaxHdrRefByYr = taSalesDelHdr.GetMaxHdrRefByYr(DateTime.Now.Year);
                    var nextHdrRefByYr = dtMaxHdrRefByYr == null ? "000001" : (Convert.ToInt32(dtMaxHdrRefByYr) + 1).ToString("000000");
                    nextHdrRefNo = "ECIL-DO-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRefByYr).ToString("000000");

                    taSalesDelHdr.InsertDoHdr(nextHdrRef, "DO", "DEL", nextHdrRefNo, ordRef.ToString(), ordRefNo.ToString(),
                        DateTime.Now, truckNo.ToString(), driverName.ToString(), txtDriverContact.Text.Trim(), cboCustDist.SelectedValue.ToString(),
                        cboCustThana.SelectedValue.ToString(), locRef.ToString(), "", "", optTranBy.SelectedValue.ToString(), truckRef.ToString(),
                        Convert.ToDecimal(txtDoQty.Text.Trim()), "H", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), ddlOrdTransMode.SelectedValue.ToString(),
                        "", txtDelAddr.Text.Trim(), "", "", 0, "BDT", null, "1", "");

                    if (txtDoQty1.Text.Trim().Length > 0 && txtDoQty1.Text != "")
                    {
                        var dtMaxRtlSalesRefByYr = taSalesByRtl.GetMaxRef(DateTime.Now.Year);
                        var nexSalesRefByYr = dtMaxRtlSalesRefByYr == null ? "000001" : (Convert.ToInt32(dtMaxRtlSalesRefByYr) + 1).ToString("000000");
                        var nextSalesRefNo = DateTime.Now.Year.ToString() + Convert.ToInt32(nexSalesRefByYr).ToString("000000");
                        taSalesByRtl.InsertSales(Convert.ToInt32(nextSalesRefNo), nextHdrRefNo, rtlRef1.ToString(), DateTime.Now, 1, "DO", Convert.ToDouble(txtDoQty1.Text.Trim()), "", "", "", "", "", "H", "1");
                    }

                    if (txtDoQty2.Text.Trim().Length > 0 && txtDoQty2.Text != "")
                    {
                        var dtMaxRtlSalesRefByYr = taSalesByRtl.GetMaxRef(DateTime.Now.Year);
                        var nexRtlSalesRefByYr = dtMaxRtlSalesRefByYr == null ? "000001" : (Convert.ToInt32(dtMaxRtlSalesRefByYr) + 1).ToString("000000");
                        var nextSalesRefNo = DateTime.Now.Year.ToString() + Convert.ToInt32(nexRtlSalesRefByYr).ToString("000000");
                        taSalesByRtl.InsertSales(Convert.ToInt32(nextSalesRefNo), nextHdrRefNo, rtlRef2.ToString(), DateTime.Now, 2, "DO", Convert.ToDouble(txtDoQty2.Text.Trim()), "", "", "", "", "", "H", "1");
                    }

                    var taSoDet = new tblSalesOrderDetTableAdapter();
                    var dtSoDet = taSoDet.GetDataByDetLno(ordRef.ToString(), Convert.ToInt16(hfSoDetLno.Value.ToString()));
                    if (dtSoDet.Rows.Count > 0)
                    {
                        var taSoHdr = new tblSalesOrderHdrTableAdapter();
                        var dtSoHdr = taSoHdr.GetDataByHdrRef(ordRef.ToString());
                        var ordLocRef = dtSoHdr.Rows.Count > 0 ? (dtSoHdr[0].IsSO_Hdr_Com3Null() ? "" : dtSoHdr[0].SO_Hdr_Com3.ToString()) : "";
                        if (ordLocRef == locRef)
                        {
                            taSalesDelDet.InsertDoDet(nextHdrRef.ToString(), "DO", "DEL", nextHdrRefNo, 1, ordRef.ToString(), ordRefNo.ToString(),
                                Convert.ToInt16(hfSoDetLno.Value.ToString()), "", dtSoDet[0].SO_Det_Icode, dtSoDet[0].SO_Det_Itm_Desc, dtSoDet[0].SO_Det_Itm_Uom,
                                "", "", "", 0, "", doDelDt, null, Convert.ToDouble(txtDoFreeQty.Text.Trim()), Convert.ToDouble(txtDoQty.Text.Trim()), 0,
                                Convert.ToDouble(txtDoQty.Text.Trim()), 0, "", "", dtSoDet[0].SO_Det_Trans_Rat, dtSoDet[0].SO_Det_Lin_Rat,
                                (dtSoDet[0].SO_Det_Trans_Rat + dtSoDet[0].SO_Det_Lin_Rat) * Convert.ToDecimal(txtDoQty.Text.Trim()), 0, 0, 0,
                                (dtSoDet[0].SO_Det_Trans_Rat + dtSoDet[0].SO_Det_Lin_Rat) * Convert.ToDecimal(txtDoQty.Text.Trim()), 0,
                                Convert.ToDouble(txtDoQty.Text.Trim()) + Convert.ToDouble(txtDoFreeQty.Text.Trim()), "0",
                                txtDoFreeQty.Text.ToString(), "", "", "", "", "", "", 0, "1", "");
                        }
                        else
                        {
                            var taSalesLoc = new tblSalesLocMasTableAdapter();
                            var dtSalesLoc = taSalesLoc.GetDataByLocRef(locRef.ToString());
                            var locRate = dtSalesLoc.Rows.Count > 0 ? dtSalesLoc[0].Loc_Mas_Itm_Rate : 0;

                            taSalesDelDet.InsertDoDet(nextHdrRef.ToString(), "DO", "DEL", nextHdrRefNo, 1, ordRef.ToString(), ordRefNo.ToString(),
                                Convert.ToInt16(hfSoDetLno.Value.ToString()), "", dtSoDet[0].SO_Det_Icode, dtSoDet[0].SO_Det_Itm_Desc, dtSoDet[0].SO_Det_Itm_Uom,
                                "", "", "", 0, "", doDelDt, null, Convert.ToDouble(txtDoFreeQty.Text.Trim()), Convert.ToDouble(txtDoQty.Text.Trim()), 0,
                                Convert.ToDouble(txtDoQty.Text.Trim()), 0, "", "", locRate, dtSoDet[0].SO_Det_Lin_Rat,
                                (locRate + dtSoDet[0].SO_Det_Lin_Rat) * Convert.ToDecimal(txtDoQty.Text.Trim()), 0, 0, 0,
                                (locRate + dtSoDet[0].SO_Det_Lin_Rat) * Convert.ToDecimal(txtDoQty.Text.Trim()), 0,
                                Convert.ToDouble(txtDoQty.Text.Trim()) + Convert.ToDouble(txtDoFreeQty.Text.Trim()), "0",
                                txtDoFreeQty.Text.ToString(), "", "", "", "", "", "", 0, "1", "");
                        }

                        taSoDet.UpdateSalesDetDoBal((dtSoDet[0].SO_Det_Org_QTY + Convert.ToDouble(txtDoQty.Text.Trim())),
                            (dtSoDet[0].SO_Det_Lin_Qty) - (dtSoDet[0].SO_Det_Org_QTY + Convert.ToDouble(txtDoQty.Text.Trim())),
                            (dtSoDet[0].SO_Det_DO_Qty + Convert.ToDouble(txtDoQty.Text.Trim()) + Convert.ToDouble(txtDoFreeQty.Text.Trim())),
                            Convert.ToDouble(Convert.ToDecimal(dtSoDet[0].SO_Det_DO_Bal_Qty) - (Convert.ToDecimal(txtDoQty.Text.Trim()) + Convert.ToDecimal(txtDoFreeQty.Text.Trim()))),
                            (Convert.ToDouble(dtSoDet[0].SO_Det_Ext_Data1) + Convert.ToDouble(txtDoFreeQty.Text.Trim())).ToString(),
                            (Convert.ToDecimal(dtSoDet[0].SO_Det_Free_Qty) - (Convert.ToDecimal(dtSoDet[0].SO_Det_Ext_Data1) + Convert.ToDecimal(txtDoFreeQty.Text.Trim()))).ToString(),
                            ordRef.ToString(), Convert.ToInt16(hfSoDetLno.Value.ToString()));
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Sales Order does not match.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "D/O Created Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    SearchOrderData();

                    var taSalDelHdr = new tblSalesOrdDelHdrTableAdapter();
                    var dtMaxRef = taSalDelHdr.GetMaxHdrRefByYr(DateTime.Now.Year);
                    var nextRef = dtMaxRef == null ? "000001" : (Convert.ToInt32(dtMaxRef) + 1).ToString("000000");
                    var nextRefNo = "ECIL-DO-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextRef).ToString("000000");
                    txtDoRefNo.Text = nextRefNo;
                    txtDoDate.Text = DateTime.Now.ToString();

                    txtTruckNo.Text = "";
                    txtDriverName.Text = "";
                    txtDriverContact.Text = "";
                    txtDelAddr.Text = "";

                    chkDelaerPoint.Checked = false;
                    txtRetailer1.Text = "";
                    txtRetailer1.Enabled = true;
                    txtDoQty1.Text = "";
                    txtDoQty1.Enabled = true;
                    txtRetailer2.Text = "";
                    txtRetailer2.Enabled = true;
                    txtDoQty2.Text = "";
                    txtDoQty2.Enabled = true;
                }
                else
                {
                    //nextHdrRef = ddlOrderList.SelectedValue.ToString();
                    //nextHdrRefNo = ddlOrderList.SelectedItem.ToString();

                    //nextHdrRef = "";
                    //nextHdrRefNo = "";

                    //taSalesDelHdr.UpdateDoHdr(ordRef.ToString(), "", DateTime.Now, "", "", "", "", "", "", "", "", "", "", Convert.ToDecimal(txtDoQty.Text.Trim()), "P",
                    //    (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), DateTime.Now,
                    //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), ddlOrdTransMode.SelectedValue.ToString(),
                    //    "", "", "", "", 0, "BDT", null, "1", "", nextHdrRef);

                    //taSalesDelDet.DeleteDoDet(ordRef.ToString());

                    //myTran.Commit();
                    //tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                    //tblMsg.Rows[1].Cells[0].InnerText = "";
                    //ModalPopupExtenderMsg.Show();

                    //SearchOrderData();
                }

                hfEditStatus.Value = "N";
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnCancelDo_Click(object sender, EventArgs e)
        {
            var taSalesDelHdr = new tblSalesOrdDelHdrTableAdapter();
            var taSalesDelDet = new tblSalesOrdDelDetTableAdapter();
            var taSalesOrdHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesOrdDet = new tblSalesOrderDetTableAdapter();
            var taSalesByRtl = new tblSalesByRetailerTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesDelHdr.Connection);

            try
            {
                #region D/O Qty Validation
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfDoHdrRef = (HiddenField)(row.FindControl("hfDoHdrRef"));
                var lblDoHdrRefNo = (Label)(row.FindControl("lblDoHdrRefNo"));
                var hfDoDetLno = (HiddenField)(row.FindControl("hfDoDetLno"));
                var lblDoQty = (Label)(row.FindControl("lblPendDoQty"));
                var lblDoFreeQty = (Label)(row.FindControl("lblPendDoFreeQty"));

                var ordRef = "";
                var ordRefNo = "";
                var ordDetLNo = "";
                var custRef = "";

                var dtSalesDelDet = taSalesDelDet.GetDataByDetLno(hfDoHdrRef.Value.ToString(), Convert.ToInt16(hfDoDetLno.Value.ToString()));
                if (dtSalesDelDet.Rows.Count > 0)
                {
                    var dtSalesOrdHdr = taSalesOrdHdr.GetDataByHdrRef(dtSalesDelDet[0].DO_Det_SO_Ref.ToString());
                    if (dtSalesOrdHdr.Rows.Count > 0)
                    {
                        ordRef = dtSalesOrdHdr[0].SO_Hdr_Ref.ToString();
                        ordRefNo = dtSalesOrdHdr[0].SO_Hdr_Ref_No.ToString();
                        ordDetLNo = dtSalesDelDet[0].DO_Det_SO_Lno.ToString();
                        custRef = dtSalesOrdHdr[0].SO_Hdr_Pcode.ToString();

                        //var dtSalesOrdDet = taSalesOrdDet.GetDataByDetLno(dtSalesDelDet[0].DO_Det_SO_Ref.ToString(), Convert.ToInt16(dtSalesDelDet[0].DO_Det_SO_Lno.ToString()));
                        //if (dtSalesOrdDet.Rows.Count > 0)
                        //{
                        //    #region D/O Qty Validation
                        //    if (Convert.ToDouble(lblDoQty.Text.Trim()) > dtSalesOrdDet[0].SO_Det_DO_Bal_Qty)
                        //    {
                        //        tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to cancel D/O qty more than : " + dtSalesOrdDet[0].SO_Det_DO_Bal_Qty;
                        //        tblMsg.Rows[1].Cells[0].InnerText = "";
                        //        ModalPopupExtenderMsg.Show();
                        //        return;
                        //    }
                        //    if (Convert.ToDouble(lblDoFreeQty.Text.Trim()) > Convert.ToDouble(dtSalesOrdDet[0].SO_Det_Ext_Data2))
                        //    {
                        //        tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to create free bag D/O qty more than : " + dtSalesOrdDet[0].SO_Det_Ext_Data1;
                        //        tblMsg.Rows[1].Cells[0].InnerText = "";
                        //        ModalPopupExtenderMsg.Show();
                        //        return;
                        //    }
                        //    #endregion

                        //    #region Credit Limit Validiation
                        //    var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                        //    var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        //    var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;
                        //    var custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                        //    var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                        //    var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                        //    var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                        //    var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                        //    var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                        //    var doAmt = Convert.ToDouble(lblDoQty.Text.Trim()) * Convert.ToDouble(dtSalesOrdDet[0].SO_Det_Lin_Rat);

                        //    if (doAmt > (Convert.ToDouble(crLimit) - (drAmt - crAmt)))
                        //    {
                        //        tblMsg.Rows[0].Cells[0].InnerText = "You are no allowed to exceed available credit limit : " + (Convert.ToDouble(crLimit) - (drAmt - crAmt)).ToString("N2");
                        //        tblMsg.Rows[1].Cells[0].InnerText = "";
                        //        ModalPopupExtenderMsg.Show();
                        //        return;
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Order Details.";
                        //    tblMsg.Rows[1].Cells[0].InnerText = "";
                        //    ModalPopupExtenderMsg.Show();
                        //    return;
                        //}
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
                taSalesByRtl.AttachTransaction(myTran);

                taSalesDelHdr.UpdateDoHdrStat("C", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "0", hfDoHdrRef.Value.ToString());

                taSalesByRtl.UpdateSalesFlag("C", lblDoHdrRefNo.Text.Trim());

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

                SearchOrderData();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnFwdDo_Click(object sender, EventArgs e)
        {
            var taSalesDelHdr = new tblSalesOrdDelHdrTableAdapter();
            var taSalesDelDet = new tblSalesOrdDelDetTableAdapter();
            var taSalesOrdHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesOrdDet = new tblSalesOrderDetTableAdapter();
            var taSalesByRtl = new tblSalesByRetailerTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesDelHdr.Connection);

            try
            {
                #region D/O Qty and Credit Limit Validation
                GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
                var hfDoHdrRef = (HiddenField)(row.FindControl("hfDoHdrRef"));
                var lblDoHdrRefNo = (Label)(row.FindControl("lblDoHdrRefNo"));
                var hfDoDetLno = (HiddenField)(row.FindControl("hfDoDetLno"));
                var lblDoQty = (Label)(row.FindControl("lblPendDoQty"));
                var lblDoFreeQty = (Label)(row.FindControl("lblPendDoFreeQty"));

                var ordRef = "";
                var ordRefNo = "";
                var ordDetLNo = "";
                var custRef = "";

                var dtSalesDelDet = taSalesDelDet.GetDataByDetLno(hfDoHdrRef.Value.ToString(), Convert.ToInt16(hfDoDetLno.Value.ToString()));
                if (dtSalesDelDet.Rows.Count > 0)
                {
                    var dtSalesOrdHdr = taSalesOrdHdr.GetDataByHdrRef(dtSalesDelDet[0].DO_Det_SO_Ref.ToString());
                    if (dtSalesOrdHdr.Rows.Count > 0)
                    {
                        ordRef = dtSalesOrdHdr[0].SO_Hdr_Ref.ToString();
                        ordRefNo = dtSalesOrdHdr[0].SO_Hdr_Ref_No.ToString();
                        ordDetLNo = dtSalesDelDet[0].DO_Det_SO_Lno.ToString();
                        custRef = dtSalesOrdHdr[0].SO_Hdr_Pcode.ToString();

                        //var dtSalesOrdDet = taSalesOrdDet.GetDataByDetLno(dtSalesDelDet[0].DO_Det_SO_Ref.ToString(), Convert.ToInt16(dtSalesDelDet[0].DO_Det_SO_Lno.ToString()));
                        //if (dtSalesOrdDet.Rows.Count > 0)
                        //{
                        //    #region D/O Qty Validation
                        //    if (Convert.ToDouble(lblDoQty.Text.Trim()) > dtSalesOrdDet[0].SO_Det_DO_Bal_Qty)
                        //    {
                        //        tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to create D/O more than : " + dtSalesOrdDet[0].SO_Det_DO_Bal_Qty;
                        //        tblMsg.Rows[1].Cells[0].InnerText = "";
                        //        ModalPopupExtenderMsg.Show();
                        //        return;
                        //    }
                        //    if (Convert.ToDouble(lblDoFreeQty.Text.Trim()) > Convert.ToDouble(dtSalesOrdDet[0].SO_Det_Ext_Data2))
                        //    {
                        //        tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to create free bag D/O more than : " + dtSalesOrdDet[0].SO_Det_Ext_Data1;
                        //        tblMsg.Rows[1].Cells[0].InnerText = "";
                        //        ModalPopupExtenderMsg.Show();
                        //        return;
                        //    }
                        //    #endregion

                        //    #region Credit Limit Validiation
                        //    var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                        //    var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        //    var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;
                        //    var custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                        //    var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                        //    var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                        //    var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                        //    var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                        //    var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                        //    var doAmt = Convert.ToDouble(lblDoQty.Text.Trim()) * Convert.ToDouble(dtSalesOrdDet[0].SO_Det_Lin_Rat);

                        //    if (doAmt > (Convert.ToDouble(crLimit) - (drAmt - crAmt)))
                        //    {
                        //        tblMsg.Rows[0].Cells[0].InnerText = "You are no allowed to exceed available credit limit : " + (Convert.ToDouble(crLimit) - (drAmt - crAmt)).ToString("N2");
                        //        tblMsg.Rows[1].Cells[0].InnerText = "";
                        //        ModalPopupExtenderMsg.Show();
                        //        return;
                        //    }
                        //    #endregion
                        //}
                        //else
                        //{
                        //    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Order Details.";
                        //    tblMsg.Rows[1].Cells[0].InnerText = "";
                        //    ModalPopupExtenderMsg.Show();
                        //    return;
                        //}
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
                taSalesByRtl.AttachTransaction(myTran);
                //taSalesDelDet.AttachTransaction(myTran);
                //taSalesOrdDet.AttachTransaction(myTran);

                taSalesDelHdr.UpdateDoHdrStat("P", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", hfDoHdrRef.Value.ToString());

                taSalesByRtl.UpdateSalesFlag("P", lblDoHdrRefNo.Text.Trim());                

                //var dtSoDet = taSalesOrdDet.GetDataByDetLno(ordRef.ToString(), Convert.ToInt16(ordDetLNo.ToString()));
                //if (dtSoDet.Rows.Count > 0)
                //{
                //    taSalesOrdDet.UpdateSalesDet((dtSoDet[0].SO_Det_Org_QTY - Convert.ToDouble(lblDoQty.Text.Trim())),
                //        (dtSoDet[0].SO_Det_Lin_Qty) - (dtSoDet[0].SO_Det_Org_QTY - Convert.ToDouble(lblDoQty.Text.Trim())),
                //        (dtSoDet[0].SO_Det_DO_Qty - Convert.ToDouble(lblDoQty.Text.Trim())),
                //        dtSoDet[0].SO_Det_Lin_Qty - (dtSoDet[0].SO_Det_DO_Qty - Convert.ToDouble(lblDoQty.Text.Trim())),
                //        (Convert.ToDouble(dtSoDet[0].SO_Det_Ext_Data1) - Convert.ToDouble(lblDoFreeQty.Text.Trim())).ToString(),
                //        ((dtSoDet[0].SO_Det_Free_Qty) - (Convert.ToDouble(dtSoDet[0].SO_Det_Ext_Data1) - Convert.ToDouble(lblDoFreeQty.Text.Trim()))).ToString(),
                //        ordRef.ToString(), Convert.ToInt16(ordDetLNo.ToString()));
                //}
                //else
                //{
                //    myTran.Rollback();
                //    tblMsg.Rows[0].Cells[0].InnerText = "Sales Order details does not match.";
                //    tblMsg.Rows[1].Cells[0].InnerText = "";
                //    ModalPopupExtenderMsg.Show();
                //    return;
                //}

                myTran.Commit();
                tblMsg.Rows[0].Cells[0].InnerText = "D/O Forwarded Successfully.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();

                try
                {
                    //Procedure one             
                    String sid = "7HorseSuprmBng";
                    String user = "sevenhorse";
                    String pass = "123456";
                    String URI = "http://sms.sslwireless.com/pushapi/dynamic/server.php";

                    var custName = "";
                    var taCustHdr = new tblSalesPartyAdrTableAdapter();
                    var dtCustHdr = taCustHdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                    if (dtCustHdr.Rows.Count > 0)
                        custName = dtCustHdr[0].Par_Adr_Name.ToString();

                    var cellNo = "";
                    var taSmsConfig = new tbl_Sms_ConfigTableAdapter();
                    var dtSmsConfig = taSmsConfig.GetDataByTranTypeStatus("DO-ISU", "1");
                    foreach (dsSalesMas.tbl_Sms_ConfigRow dr in dtSmsConfig.Rows)
                    {
                        var taParAdr = new tblSalesPartyAdrTableAdapter();
                        var dtParAdr = taParAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef.ToString()));

                        if (dr.Config_Receiver_Grp_Name.ToString() == "INDV")
                            cellNo = dr.Config_Receiver_Cell_No.ToString();

                        if (dr.Config_Receiver_Grp_Name.ToString() == "CUST")
                        {
                            if (dtParAdr.Rows.Count > 0)
                                cellNo = dtParAdr[0].Par_Adr_Cell_No.ToString();
                        }

                        if (dr.Config_Receiver_Grp_Name.ToString() == "DSM")
                        {
                            var dsmRef = "";
                            if (dtParAdr.Rows.Count > 0)
                                dsmRef = dtParAdr[0].Par_Adr_Sls_Per.ToString();

                            var taDsm = new tblSalesDsmMasTableAdapter();
                            var dtDsm = taDsm.GetDataByDsmRef(Convert.ToInt32(dsmRef.ToString()));
                            if (dtDsm.Rows.Count > 0)
                            {
                                if (dtDsm[0].Dsm_Status.ToString() == "1")
                                    cellNo = dtDsm[0].Dsm_Cell_No.ToString();
                            }
                        }

                        if (dr.Config_Receiver_Grp_Name.ToString() == "MPO")
                        {
                            var mpoRef = "";
                            if (dtParAdr.Rows.Count > 0)
                                mpoRef = dtParAdr[0].Par_Adr_Ext_Data2.ToString();

                            var taMpo = new tblSalesPersonMasTableAdapter();
                            var dtMpo = taMpo.GetDataBySpRef(Convert.ToInt32(mpoRef.ToString()));
                            if (dtMpo.Rows.Count > 0)
                            {
                                if (dtMpo[0].Sp_Status.ToString() == "1")
                                    cellNo = dtMpo[0].Sp_Cell_No.ToString();
                            }
                        }

                        if (cellNo.Length > 0)
                        {
                            if (cellNo.Substring(0, 2) == "01" && cellNo.ToString().Length == 11)
                            {
                                var taSmsBody = new tbl_Sms_BodyTableAdapter();
                                var dtSmsBody = taSmsBody.GetDataBySmsTranTypeLanguage("DO-ISU", "Bangla");
                                if (dtSmsBody.Rows.Count > 0)
                                {
                                    //String myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=88" + cellNo.ToString() + "&sms[0][1]="
                                    //                       + System.Web.HttpUtility.UrlEncode(DRN_WEB_ERP.GlobalClass.clsSmsHelper.convertBanglatoUnicode(dtSmsBody[0].SMS_Body_1.ToString()
                                    //                       + Math.Round(Convert.ToDouble(lblDoQty.Text.Trim())) + dtSmsBody[0].SMS_Body_2.ToString() + lblDoHdrRefNo.Text.Trim()
                                    //                       + dtSmsBody[0].SMS_Body_3.ToString() + " (Dealer Name: " + custName + ")"))
                                    //                       + "&sms[0][2]=" + "1234567890" + "&sid=" + sid;

                                    String myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=88" + cellNo.ToString() + "&sms[0][1]="
                                                           + System.Web.HttpUtility.UrlEncode(DRN_WEB_ERP.GlobalClass.clsSmsHelper.convertBanglatoUnicode(dtSmsBody[0].SMS_Body_1.ToString()
                                                           + Math.Round(Convert.ToDouble(lblDoQty.Text.Trim())) + dtSmsBody[0].SMS_Body_2.ToString() + lblDoHdrRefNo.Text.Trim()
                                                           + dtSmsBody[0].SMS_Body_3.ToString()))
                                                           + "&sms[0][2]=" + "1234567890" + "&sid=" + sid;

                                    using (WebClient wc = new WebClient())
                                    {
                                        wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                                        string HtmlResult = wc.UploadString(URI, myParameters);

                                        Console.Write(HtmlResult);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex) { }                

                SearchOrderData();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void chkIncAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIncAll.Checked)
                AutoCompleteExtenderSrch.ContextKey = "1";
            else
                AutoCompleteExtenderSrch.ContextKey = "0";
        }

        protected void gvPendDoDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hfDoHdrStat = ((HiddenField)e.Row.FindControl("hfHdrStat"));
                var btnDoCancel = ((Button)e.Row.FindControl("btnCancelDo"));
                var btnDoFwd = ((Button)e.Row.FindControl("btnFwdDo"));

                if (hfDoHdrStat.Value == "1")
                {
                    btnDoCancel.Enabled = true;
                    btnDoFwd.Enabled = true;
                }
                else
                {
                    btnDoCancel.Enabled = false;
                    btnDoFwd.Enabled = false;
                }
            }
        }

        protected void gvOrdDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtDoQty = (TextBox)e.Row.FindControl("txtDoQty");
                TextBox txtDoFreeQty = (TextBox)e.Row.FindControl("txtDoFreeQty");
                HiddenField hfSoBalQty = (HiddenField)e.Row.FindControl("hfSoBalQty");
                HiddenField hfSoFreeBalQty = (HiddenField)e.Row.FindControl("hfSoFreeBalQty");

                txtDoQty.Attributes.Add("onkeyup", "CalcFreeQty('" + txtDoQty.ClientID + "', '" + txtDoFreeQty.ClientID + "', '"
                    + hfSoBalQty.Value.Trim() + "', '" + hfSoFreeBalQty.Value.Trim() + "' )");

                txtDoFreeQty.Attributes.Add("onkeyup", "CheckFreeQty('" + txtDoQty.ClientID + "', '" + txtDoFreeQty.ClientID + "', '"
                    + hfSoBalQty.Value.Trim() + "', '" + hfSoFreeBalQty.Value.Trim() + "' )");
            }
        }

        protected void cboCustDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taThana = new tblSalesThanaTableAdapter();
            cboCustThana.DataSource = taThana.GetDataByDistRef(Convert.ToInt32(cboCustDist.SelectedValue));
            cboCustThana.DataTextField = "ThanaName";
            cboCustThana.DataValueField = "ThanaRef";
            cboCustThana.DataBind();
            cboCustThana.Items.Insert(0, new ListItem("---Select---", "0"));
        }

        protected void optTranBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ordRef = "";
            var srchWords = txtSearch.Text.Trim().Split(':');
            foreach (string word in srchWords)
            {
                ordRef = word;
                break;
            }

            if (ordRef.Length > 0)
            {
                int result;
                if (int.TryParse(ordRef, out result))
                {
                    var taSalesOrdDet = new tblSalesOrderHdrTableAdapter();
                    var dtSalesOrd = taSalesOrdDet.GetDataByHdrRef(ordRef.ToString());
                    if (optTranBy.SelectedValue != dtSalesOrd[0].SO_Hdr_Com2.ToString())
                    {
                        if (optTranBy.SelectedValue == "2")
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "You have changed transport cost pay terms.";
                            tblMsg.Rows[1].Cells[0].InnerText = "Customer will charge transport cost.";
                            ModalPopupExtenderMsg.Show();
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "You have changed transport cost pay terms.";
                            tblMsg.Rows[1].Cells[0].InnerText = "Customer will not charge transport cost.";
                            ModalPopupExtenderMsg.Show();
                        }
                    }
                }
            }

            if (optTranBy.SelectedValue == "2")
            {
                txtTruckNo.Text = "";
                txtTruckNo.Visible = false;
                cboTruckNo.SelectedIndex = 0;
                cboTruckNo.Visible = true;
            }
            else
            {
                txtTruckNo.Text = "";
                txtTruckNo.Visible = true;
                cboTruckNo.SelectedIndex = 0;
                cboTruckNo.Visible = false;
            }
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                //if (optTranBy.SelectedValue == "1")
                //    args.IsValid = true;
                //else
                //{
                    var locRef = "";
                    var srchWords = txtSalesLoc.Text.Trim().Split(':');
                    foreach (string word in srchWords)
                    {
                        locRef = word;
                        break;
                    }

                    if (locRef.Length > 0)
                    {
                        var taLoc = new tblSalesLocMasTableAdapter();
                        var dtLoc = taLoc.GetDataByLocRef(locRef);
                        if (dtLoc.Rows.Count > 0)
                            args.IsValid = true;
                        else
                            args.IsValid = false;
                    }
                    else
                        args.IsValid = false;
                //}
            }
            catch (Exception ex) { args.IsValid = false; }
        }

        protected void txtSalesLoc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var locRef = "";
                var srchWords = txtSalesLoc.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    locRef = word;
                    break;
                }

                if (locRef.Length > 0)
                {
                    if (txtSearch.Text.Trim().Length <= 0) return;
                    var ordRef = "";
                    var srchNewWords = txtSearch.Text.Trim().Split(':');
                    foreach (string word in srchNewWords)
                    {
                        ordRef = word;
                        break;
                    }

                    if (ordRef.Length > 0)
                    {
                        var taSalesOrd = new VIEW_SALES_ORDERTableAdapter();
                        var dtSalesHdr = taSalesOrd.GetDataByHdrRef(ordRef.ToString());
                        if (dtSalesHdr.Rows.Count > 0)
                        {
                            if (dtSalesHdr[0].SO_Hdr_Com3.ToString() != locRef.ToString())
                            {
                                var taSalesLoc = new tblSalesLocMasTableAdapter();
                                var dtSalesLoc = taSalesLoc.GetDataByLocRef(locRef.ToString());
                                if (dtSalesLoc.Rows.Count > 0)
                                {
                                    lblTransRate.Text = "Transport Rate Changed to : " + dtSalesLoc[0].Loc_Mas_Itm_Rate.ToString("N2");
                                    lblTransRate.Visible = true;
                                    hfOrdTransRate.Value = dtSalesLoc[0].Loc_Mas_Itm_Rate.ToString();
                                }
                                else
                                {
                                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Transport Location.";
                                    tblMsg.Rows[1].Cells[0].InnerText = "";
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }
                            }
                            else
                            {
                                lblTransRate.Text = "";
                                lblTransRate.Visible = false;
                                hfOrdTransRate.Value = "0";
                            }
                        }
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Sales Order.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Sales Order.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    lblTransRate.Text = "";
                    lblTransRate.Visible = false;
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Transport Location.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                lblTransRate.Text = "";
                lblTransRate.Visible = false;
                tblMsg.Rows[0].Cells[0].InnerText = "Invalid Transport Location.";
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void chkDelaerPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDelaerPoint.Checked)
            {
                txtRetailer1.Text = "";
                txtRetailer1.Enabled = false;
                txtDoQty1.Text = "";
                txtDoQty1.Enabled = false;
                txtRetailer2.Text = "";
                txtRetailer2.Enabled = false;
                txtDoQty2.Text = "";
                txtDoQty2.Enabled = false;
            }
            else
            {
                txtRetailer1.Text = "";
                txtRetailer1.Enabled = true;
                txtDoQty1.Text = "";
                txtDoQty1.Enabled = true;
                txtRetailer2.Text = "";
                txtRetailer2.Enabled = true;
                txtDoQty2.Text = "";
                txtDoQty2.Enabled = true;
            }
        }        
    }
}