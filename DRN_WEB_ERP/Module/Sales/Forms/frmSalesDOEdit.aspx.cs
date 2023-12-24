using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;

namespace DRN_WEB_ERP.Module.Sales
{
    public partial class frmSalesDOEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            try
            {
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

                #region Load DO Data
                //var taDoHdr = new tblSalesOrdDelHdrTableAdapter();
                var taDoDet = new VIEW_DELIVERY_ORDERTableAdapter();
                var taSalesRtl = new tblSalesByRetailerTableAdapter();
                var taRtlMas = new tblSalesPartyRtlTableAdapter();

                if (Request.QueryString["DoHdrRef"] != null && Request.QueryString["DoHdrRefNo"] != null)
                {
                    var DoHdrRef = Request.QueryString["DoHdrRef"].ToString();
                    var DoHdrRefNo = Request.QueryString["DoHdrRefNo"].ToString();

                    var dtDoDet = taDoDet.GetDataByHdrRef(DoHdrRef.ToString());
                    if (dtDoDet.Rows.Count > 0)
                    {
                        var custRef = dtDoDet[0].SO_Hdr_Pcode;
                        AutoCompleteExtendertxtRetailer1.ContextKey = custRef.ToString();
                        AutoCompleteExtendertxtRetailer2.ContextKey = custRef.ToString();

                        txtSearch.Text = dtDoDet[0].DO_Hdr_Ref.ToString() + ":" + dtDoDet[0].DO_Hdr_Ref_No.ToString() + ":" + dtDoDet[0].Par_Adr_Name.ToString();
                        txtDoRefNo.Text = dtDoDet[0].DO_Hdr_Ref_No.ToString();
                        txtDoDate.Text = dtDoDet[0].DO_Hdr_Date.ToString();
                        optTranBy.SelectedValue = dtDoDet[0].DO_Hdr_Com9;
                        if (dtDoDet[0].DO_Hdr_Com9 == "1")
                        {
                            txtTruckNo.Text = dtDoDet[0].DO_Hdr_Com1;
                            txtTruckNo.Visible = true;
                            cboTruckNo.SelectedIndex = 0;
                            cboTruckNo.Visible = false;
                        }
                        else
                        {
                            txtTruckNo.Text = "";
                            txtTruckNo.Visible = false;
                            cboTruckNo.SelectedValue = dtDoDet[0].DO_Hdr_Com10;
                            cboTruckNo.Visible = true;
                        }
                        txtDriverName.Text = dtDoDet[0].DO_Hdr_Com2;
                        txtDriverContact.Text = dtDoDet[0].DO_Hdr_Com3;
                        txtDelAddr.Text = dtDoDet[0].DO_Hdr_T_C1;
                        ddlOrdTransMode.SelectedValue = dtDoDet[0].SO_Hdr_Exp_Type;
                        cboCustDist.SelectedValue = dtDoDet[0].DO_Hdr_Com4.ToString();
                        var taThana = new tblSalesThanaTableAdapter();
                        cboCustThana.DataSource = taThana.GetDataByDistRef(Convert.ToInt32(cboCustDist.SelectedValue));
                        cboCustThana.DataTextField = "ThanaName";
                        cboCustThana.DataValueField = "ThanaRef";
                        cboCustThana.DataBind();
                        cboCustThana.Items.Insert(0, new ListItem("---Select---", "0"));
                        cboCustThana.SelectedValue = dtDoDet[0].DO_Hdr_Com5.ToString();

                        var dtSalesRtl = taSalesRtl.GetDataByTranRefNo(DoHdrRefNo.ToString());
                        foreach (dsSalesTran.tblSalesByRetailerRow dr in dtSalesRtl.Rows)
                        {
                            if (dr.Tran_Lno == 1)
                            {
                                var dtRtlMas = taRtlMas.GetDataByPartyRtlRef(dr.Tran_Adr_Ref_No.ToString());
                                txtRetailer1.Text = dtRtlMas.Rows.Count > 0 ? dtRtlMas[0].Par_Rtl_Ref + ":" + dtRtlMas[0].Par_Rtl_Ref_No + ":" + dtRtlMas[0].Par_Rtl_Name : "";
                                txtDoQty1.Text = dr.Tran_Qty.ToString();
                            }

                            if (dr.Tran_Lno == 2)
                            {
                                var dtRtlMas = taRtlMas.GetDataByPartyRtlRef(dr.Tran_Adr_Ref_No.ToString());
                                txtRetailer2.Text = dtRtlMas.Rows.Count > 0 ? dtRtlMas[0].Par_Rtl_Ref + ":" + dtRtlMas[0].Par_Rtl_Ref_No + ":" + dtRtlMas[0].Par_Rtl_Name : "";
                                txtDoQty2.Text = dr.Tran_Qty.ToString();
                            }
                        }

                        gvDoDet.DataSource = taDoDet.GetDataByHdrRef(DoHdrRef.ToString());
                        gvDoDet.DataBind();
                    }
                    else
                    {
                        txtDoRefNo.Text = "";
                        txtDoDate.Text = "";
                        txtTruckNo.Text = "";
                        txtDriverName.Text = "";
                        txtDriverContact.Text = "";
                        txtDelAddr.Text = "";
                        txtRetailer1.Text = "";
                        txtRetailer2.Text = "";
                        txtDoQty1.Text = "";
                        txtDoQty2.Text = "";

                        tblMsg.Rows[0].Cells[0].InnerText = "D/O data not found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                txtDoRefNo.Text = "";
                txtDoDate.Text = "";
                txtTruckNo.Text = "";
                txtDriverName.Text = "";
                txtDriverContact.Text = "";
                txtDelAddr.Text = "";
                txtRetailer1.Text = "";
                txtRetailer2.Text = "";
                txtDoQty1.Text = "";
                txtDoQty2.Text = "";

                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchDoData();
        }

        private void SearchDoData()
        {           
            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                var doRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    doRef = word;
                    break;
                }

                if (doRef.Length > 0)
                {
                    #region Load DO Data
                    var taDoDet = new VIEW_DELIVERY_ORDERTableAdapter();
                    var taSalesRtl = new tblSalesByRetailerTableAdapter();
                    var taRtlMas = new tblSalesPartyRtlTableAdapter();

                    var dtDoDet = taDoDet.GetDataByHdrRef(doRef.ToString());
                    if (dtDoDet.Rows.Count > 0)
                    {
                        var custRef = dtDoDet[0].SO_Hdr_Pcode;
                        AutoCompleteExtendertxtRetailer1.ContextKey = custRef.ToString();
                        AutoCompleteExtendertxtRetailer2.ContextKey = custRef.ToString();

                        txtSearch.Text = dtDoDet[0].DO_Hdr_Ref.ToString() + ":" + dtDoDet[0].DO_Hdr_Ref_No.ToString() + ":" + dtDoDet[0].Par_Adr_Name.ToString();
                        txtDoRefNo.Text = dtDoDet[0].DO_Hdr_Ref_No.ToString();
                        txtDoDate.Text = dtDoDet[0].DO_Hdr_Date.ToString();
                        optTranBy.SelectedValue = dtDoDet[0].DO_Hdr_Com9;
                        if (dtDoDet[0].DO_Hdr_Com9 == "1")
                        {
                            txtTruckNo.Text = dtDoDet[0].DO_Hdr_Com1;
                            txtTruckNo.Visible = true;
                            cboTruckNo.SelectedIndex = 0;
                            cboTruckNo.Visible = false;
                        }
                        else
                        {
                            txtTruckNo.Text = "";
                            txtTruckNo.Visible = false;
                            cboTruckNo.SelectedValue = dtDoDet[0].DO_Hdr_Com10;
                            cboTruckNo.Visible = true;
                        }
                        txtDriverName.Text = dtDoDet[0].DO_Hdr_Com2;
                        txtDriverContact.Text = dtDoDet[0].DO_Hdr_Com3;
                        txtDelAddr.Text = dtDoDet[0].DO_Hdr_T_C1;
                        ddlOrdTransMode.SelectedValue = dtDoDet[0].SO_Hdr_Exp_Type;
                        cboCustDist.SelectedValue = dtDoDet[0].DO_Hdr_Com4.ToString();
                        var taThana = new tblSalesThanaTableAdapter();
                        cboCustThana.DataSource = taThana.GetDataByDistRef(Convert.ToInt32(cboCustDist.SelectedValue));
                        cboCustThana.DataTextField = "ThanaName";
                        cboCustThana.DataValueField = "ThanaRef";
                        cboCustThana.DataBind();
                        cboCustThana.Items.Insert(0, new ListItem("---Select---", "0"));
                        cboCustThana.SelectedValue = dtDoDet[0].DO_Hdr_Com5.ToString();

                        var dtSalesRtl = taSalesRtl.GetDataByTranRefNo(doRef.ToString());
                        foreach (dsSalesTran.tblSalesByRetailerRow dr in dtSalesRtl.Rows)
                        {
                            if (dr.Tran_Lno == 1)
                            {
                                var dtRtlMas = taRtlMas.GetDataByPartyRtlRef(dr.Tran_Adr_Ref_No.ToString());
                                txtRetailer1.Text = dtRtlMas.Rows.Count > 0 ? dtRtlMas[0].Par_Rtl_Ref + ":" + dtRtlMas[0].Par_Rtl_Ref_No + ":" + dtRtlMas[0].Par_Rtl_Name : "";
                                txtDoQty1.Text = dr.Tran_Qty.ToString();
                            }

                            if (dr.Tran_Lno == 2)
                            {
                                var dtRtlMas = taRtlMas.GetDataByPartyRtlRef(dr.Tran_Adr_Ref_No.ToString());
                                txtRetailer2.Text = dtRtlMas.Rows.Count > 0 ? dtRtlMas[0].Par_Rtl_Ref + ":" + dtRtlMas[0].Par_Rtl_Ref_No + ":" + dtRtlMas[0].Par_Rtl_Name : "";
                                txtDoQty2.Text = dr.Tran_Qty.ToString();
                            }
                        }

                        gvDoDet.DataSource = taDoDet.GetDataByHdrRef(doRef.ToString());
                        gvDoDet.DataBind();
                    }
                    else
                    {
                        txtDoRefNo.Text = "";
                        txtDoDate.Text = "";
                        txtTruckNo.Text = "";
                        txtDriverName.Text = "";
                        txtDriverContact.Text = "";
                        txtDelAddr.Text = "";
                        txtRetailer1.Text = "";
                        txtRetailer2.Text = "";
                        txtDoQty1.Text = "";
                        txtDoQty2.Text = "";

                        tblMsg.Rows[0].Cells[0].InnerText = "D/O data not found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                txtDoRefNo.Text = "";
                txtDoDate.Text = "";
                txtTruckNo.Text = "";
                txtDriverName.Text = "";
                txtDriverContact.Text = "";
                txtDelAddr.Text = "";
                txtRetailer1.Text = "";
                txtRetailer2.Text = "";
                txtDoQty1.Text = "";
                txtDoQty2.Text = "";

                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            try
            {
                AutoCompleteExtendertxtRetailer1.ContextKey = "";
                AutoCompleteExtendertxtRetailer2.ContextKey = "";

                txtSearch.Text = "";
                txtDoRefNo.Text = "";
                txtDoDate.Text = "";
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

                var taDoDet = new VIEW_DELIVERY_ORDERTableAdapter();
                var dtDoDet = taDoDet.GetDataByHdrRef("");
                gvDoDet.DataSource = dtDoDet;
                gvDoDet.DataBind();
                gvDoDet.SelectedIndex = -1;
            }
            catch (Exception ex) { }
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
            var srchWords = ordRef.Trim().Split(':'); //txtSearch.Text.Trim().Split(':');
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
                if (optTranBy.SelectedValue == "1")
                    args.IsValid = true;
                else
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
                        var taLoc = new tblSalesLocMasTableAdapter();
                        var dtLoc = taLoc.GetDataByLocRef(locRef);
                        if (dtLoc.Rows.Count > 0)
                            args.IsValid = true;
                        else
                            args.IsValid = false;
                    }
                    else
                        args.IsValid = false;
                }
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
                    var doRef = "";
                    var srchNewWords = txtSearch.Text.Trim().Split(':');
                    foreach (string word in srchNewWords)
                    {
                        doRef = word;
                        break;
                    }

                    if (doRef.Length > 0)
                    {
                        var taSalesOrd = new VIEW_SALES_ORDERTableAdapter();
                        var dtSalesHdr = taSalesOrd.GetDataByHdrRef(doRef.ToString());
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
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Delivery Order (D/O).";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Delivery Order (D/O).";
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

        protected void btnEditDo_Click(object sender, EventArgs e)
        {            
            var taSalesByRtl = new tblSalesByRetailerTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesByRtl.Connection);

            try
            {
                taSalesByRtl.AttachTransaction(myTran);

                #region Check Retailer Quantity
                double totDLRQty = 0;
                double totRTLQty = 0;

                foreach (GridViewRow gr in gvDoDet.Rows)
                {
                    var totQty = ((Label)gr.FindControl("lblTotDoQty")).Text.Trim();
                    totDLRQty = totDLRQty + Convert.ToDouble(totQty);
                }

                totRTLQty = Convert.ToDouble(txtDoQty1.Text.Trim().Length <= 0 ? "0" : txtDoQty1.Text.Trim()) + Convert.ToDouble(txtDoQty2.Text.Trim().Length <= 0 ? "0" : txtDoQty2.Text.Trim());

                if (totRTLQty > totDLRQty)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Retailer wise breakdown qty is more than total D/O qty: " + totDLRQty;
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

                if (txtDoQty1.Text.Trim().Length > 0 && txtDoQty1.Text.Trim() != "")
                {
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
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Retailer.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Retailer.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                if (txtDoQty2.Text.Trim().Length > 0 && txtDoQty2.Text.Trim() != "")
                {
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
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Retailer.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Retailer.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                #endregion

                var doRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    doRef = word;
                    break;
                }

                var taDoDet = new VIEW_DELIVERY_ORDERTableAdapter();
                var dtDoDet = taDoDet.GetDataByHdrRef(doRef.ToString());
                if (dtDoDet.Rows.Count > 0)
                {
                    if (txtDoQty1.Text.Trim().Length > 0 && txtDoQty1.Text != "")
                    {
                        var dtMaxRtlSalesRefByYr = taSalesByRtl.GetMaxRef(DateTime.Now.Year);
                        var nexSalesRefByYr = dtMaxRtlSalesRefByYr == null ? "000001" : (Convert.ToInt32(dtMaxRtlSalesRefByYr) + 1).ToString("000000");
                        var nextSalesRefNo = DateTime.Now.Year.ToString() + Convert.ToInt32(nexSalesRefByYr).ToString("000000");

                        taSalesByRtl.InsertSales(Convert.ToInt32(nextSalesRefNo), dtDoDet[0].DO_Det_Ref_No, rtlRef1.ToString(), DateTime.Now, 1, "DO",
                            Convert.ToDouble(txtDoQty1.Text.Trim()), "", "", "", "", "", "P", "1");
                    }

                    if (txtDoQty2.Text.Trim().Length > 0 && txtDoQty2.Text != "")
                    {
                        var dtMaxRtlSalesRefByYr = taSalesByRtl.GetMaxRef(DateTime.Now.Year);
                        var nexRtlSalesRefByYr = dtMaxRtlSalesRefByYr == null ? "000001" : (Convert.ToInt32(dtMaxRtlSalesRefByYr) + 1).ToString("000000");
                        var nextSalesRefNo = DateTime.Now.Year.ToString() + Convert.ToInt32(nexRtlSalesRefByYr).ToString("000000");
                        
                        taSalesByRtl.InsertSales(Convert.ToInt32(nextSalesRefNo), dtDoDet[0].DO_Det_Ref_No, rtlRef2.ToString(), DateTime.Now, 2, "DO", 
                            Convert.ToDouble(txtDoQty2.Text.Trim()), "", "", "", "", "", "P", "1");
                    }

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Updated Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                }
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }        
    }
}