using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DRN_WEB_ERP.Module.Sales.DataSet;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesMasTableAdapters;
using DRN_WEB_ERP.Module.Sales.DataSet.dsSalesTranTableAdapters;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvTranTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmDeliveryChallan : System.Web.UI.Page
    {
        string rptFile;
        string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            //txtTruckNo.Attributes.Add("onkeyup", "setfocus(this)");

            AutoCompleteExtenderSrch.ContextKey = "0";

            //reportInfo();

            //btnPrint.Attributes.Add("onclick", "javascript:w= window.open('frmShowSalesReport.aspx');");

            if (Page.IsPostBack) return;

            try
            {
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                //Challan Header Ref
                var taInvHdrData = new tbl_InTr_Trn_HdrTableAdapter();
                var dtGetMaxChlnRef = taInvHdrData.GetMaxChlnRef(DateTime.Now.Year);
                var nextChlnRef = dtGetMaxChlnRef == null ? "000001" : (Convert.ToInt32(dtGetMaxChlnRef) + 1).ToString("000000");
                var nextChlnRefNo = "ECIL-CLN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextChlnRef).ToString("000000");
                txtChallanNo.Text = nextChlnRefNo;
                txtChallanDate.Text = DateTime.Now.ToString();

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
                cboTruckNo.DataSource = taVslMas.GetDataByDistributionTrans();
                cboTruckNo.DataTextField = "Vsl_Mas_No";
                cboTruckNo.DataValueField = "Vsl_Mas_Ref";
                cboTruckNo.DataBind();
                cboTruckNo.Items.Insert(0, new ListItem("---Select---", "0"));

                //var taStore = new tbl_InMa_Str_LocTableAdapter();
                //var dtStore = taStore.GetDataByStoreGroup("FIN");
                //foreach (dsInvMas.tbl_InMa_Str_LocRow dr in dtStore.Rows)
                //{
                //    ddlDelStore.Items.Add(new ListItem(dr.Str_Loc_Name, dr.Str_Loc_Ref.ToString()));
                //}
                //ddlDelStore.Items.Insert(0, new ListItem("---Select---", "0"));
                //ddlDelStore.SelectedIndex = ddlDelStore.Items.IndexOf(ddlDelStore.Items.FindByValue("1007"));

                var taStore = new View_InMa_Str_Loc_PermTableAdapter();
                var dtStore = taStore.GetDataByEmpRefStrGrpStatus(empRef.ToString(), "FIN", "1");
                foreach (dsInvMas.View_InMa_Str_Loc_PermRow dr in dtStore.Rows)
                {
                    ddlDelStore.Items.Add(new ListItem(dr.Str_Loc_Name, dr.Str_Loc_Ref.ToString()));
                }
                ddlDelStore.Items.Insert(0, new ListItem("---Select---", "0"));
                ddlDelStore.SelectedIndex = ddlDelStore.Items.IndexOf(ddlDelStore.Items.FindByValue("1007"));

                //var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
                //ddlChallanList.DataSource = taInvHdr.GetChallanDataListDesc(DateTime.Now.Year);
                //ddlChallanList.DataValueField = "Trn_Hdr_DC_No";
                //ddlChallanList.DataTextField = "Trn_Hdr_Cno";
                //ddlChallanList.DataBind();
                //ddlChallanList.Items.Insert(0, "----------Select----------");

                var curYear = DateTime.Now.Year;
                for (Int64 year = 2014; year <= (curYear); year++)
                {
                    cboYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                }
                cboYear.SelectedValue = curYear.ToString();

                var curMonth = DateTime.Now.Month;
                for (int month = 1; month <= 12; month++)
                {
                    var monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                    cboMonth.Items.Add(new ListItem(monthName.ToString(), month.ToString()));
                }
                cboMonth.Items.Insert(0, new ListItem("-----Select-----", "0"));
                cboMonth.SelectedValue = curMonth.ToString();

                Get_CLN_List();
            }
            catch(Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchDelOrderData();
        }

        private void SearchDelOrderData()
        {
            var taDelOrd = new VIEW_DELIVERY_ORDERTableAdapter();
            var dtDelOrd = new dsSalesTran.VIEW_DELIVERY_ORDERDataTable();

            if (txtSearch.Text.Trim().Length <= 0) return;

            try
            {
                var custRef = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custRef = word;
                    break;
                }

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        AutoCompleteExtendertxtRetailer1.ContextKey = custRef.ToString();
                        AutoCompleteExtendertxtRetailer2.ContextKey = custRef.ToString();

                        if (chkIncAll.Checked)
                            dtDelOrd = taDelOrd.GetDataByCustRefAll(custRef.ToString());
                        else
                            //dtDelOrd = taDelOrd.GetDataByCustRef(custRef.ToString());                        
                            dtDelOrd = taDelOrd.GetPendDelByCustRef(custRef.ToString());
                        gvDoDet.DataSource = dtDelOrd;
                        gvDoDet.DataBind();
                        gvDoDet.SelectedIndex = -1;
                        txtSearch.Enabled = false;
                        btnClearSrch.Visible = true;
                        btnSaveChln.Visible = dtDelOrd.Rows.Count > 0;
                    }
                }
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnClearSrch_Click(object sender, EventArgs e)
        {
            try
            {
                chkIncAll.Checked = false;
                AutoCompleteExtenderSrch.ContextKey = "0";
                AutoCompleteExtendertxtRetailer1.ContextKey = "";
                AutoCompleteExtendertxtRetailer2.ContextKey = "";

                //Challan Header Ref
                var taInvHdrData = new tbl_InTr_Trn_HdrTableAdapter();
                var dtGetMaxChlnRef = taInvHdrData.GetMaxChlnRef(DateTime.Now.Year);
                var nextChlnRef = dtGetMaxChlnRef == null ? "000001" : (Convert.ToInt32(dtGetMaxChlnRef) + 1).ToString("000000");
                var nextChlnRefNo = "ECIL-CLN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextChlnRef).ToString("000000");
                txtChallanNo.Text = nextChlnRefNo;
                txtChallanDate.Text = DateTime.Now.ToString();

                var taDelOrd = new VIEW_DELIVERY_ORDERTableAdapter();
                var dtDelOrd = taDelOrd.GetDataByCustRef("");
                gvDoDet.DataSource = dtDelOrd;
                gvDoDet.DataBind();
                gvDoDet.SelectedIndex = -1;
                txtSearch.Text = "";
                txtTruckNo.Text = "";
                txtDriverName.Text = "";
                txtDriverContact.Text = "";
                txtDelAddr.Text = "";
                txtRetailer1.Text = "";
                txtRetailer2.Text = "";
                txtDelQty1.Text = "";
                txtDelQty2.Text = "";
                txtSearch.Enabled = true;
                btnClearSrch.Visible = false;
                btnSaveChln.Visible = dtDelOrd.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void chkDoDelQty_CheckedChanged(object sender, EventArgs e)
        {
            var taDoHdr = new tblSalesOrdDelHdrTableAdapter();
            var taSalesRtl = new tblSalesByRetailerTableAdapter();
            var taRtlMas = new tblSalesPartyRtlTableAdapter();            

            try
            {
                GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                var hfDoHdrRef = (HiddenField)(row.FindControl("hfDoHdrRef"));
                var lblDoHdrRefNo = (Label)(row.FindControl("lblDoHdrRefNo"));
                var chkDoDelQty = (CheckBox)(row.FindControl("chkDoDelQty"));

                if (chkDoDelQty.Checked)
                {
                    var dtDoHdr = taDoHdr.GetDataByHdrRef(hfDoHdrRef.Value.ToString());
                    if (dtDoHdr.Rows.Count > 0)
                    {
                        optTranBy.SelectedValue = dtDoHdr[0].DO_Hdr_Com9;
                        if (dtDoHdr[0].DO_Hdr_Com9 == "1")
                        {
                            txtTruckNo.Text = dtDoHdr[0].DO_Hdr_Com1;
                            txtTruckNo.Visible = true;
                            cboTruckNo.SelectedIndex = 0;
                            cboTruckNo.Visible = false;
                        }
                        else
                        {
                            txtTruckNo.Text = "";
                            txtTruckNo.Visible = false;
                            cboTruckNo.SelectedValue = dtDoHdr[0].DO_Hdr_Com10;
                            cboTruckNo.Visible = true;
                        }                        
                        txtDriverName.Text = dtDoHdr[0].DO_Hdr_Com2;
                        txtDriverContact.Text = dtDoHdr[0].DO_Hdr_Com3;
                        txtDelAddr.Text = dtDoHdr[0].DO_Hdr_T_C1;
                        ddlOrdTransMode.SelectedValue = dtDoHdr[0].SO_Hdr_Exp_Type;
                        cboCustDist.SelectedValue = dtDoHdr[0].DO_Hdr_Com4.ToString();
                        var taThana = new tblSalesThanaTableAdapter();
                        cboCustThana.DataSource = taThana.GetDataByDistRef(Convert.ToInt32(cboCustDist.SelectedValue));
                        cboCustThana.DataTextField = "ThanaName";
                        cboCustThana.DataValueField = "ThanaRef";
                        cboCustThana.DataBind();
                        cboCustThana.Items.Insert(0, new ListItem("---Select---", "0"));
                        cboCustThana.SelectedValue = dtDoHdr[0].DO_Hdr_Com5.ToString();

                        var dtSalesRtl = taSalesRtl.GetDataByTranRefNo(lblDoHdrRefNo.Text.Trim());
                        foreach (dsSalesTran.tblSalesByRetailerRow dr in dtSalesRtl.Rows)
                        {
                            if (dr.Tran_Lno == 1)
                            {
                                var dtRtlMas = taRtlMas.GetDataByPartyRtlRef(dr.Tran_Adr_Ref_No.ToString());
                                txtRetailer1.Text = dtRtlMas.Rows.Count > 0 ? dtRtlMas[0].Par_Rtl_Ref + ":" + dtRtlMas[0].Par_Rtl_Ref_No + ":" + dtRtlMas[0].Par_Rtl_Name : "";
                                txtDelQty1.Text = dr.Tran_Qty.ToString();
                            }

                            if (dr.Tran_Lno == 2)
                            {
                                var dtRtlMas = taRtlMas.GetDataByPartyRtlRef(dr.Tran_Adr_Ref_No.ToString());
                                txtRetailer2.Text = dtRtlMas.Rows.Count > 0 ? dtRtlMas[0].Par_Rtl_Ref + ":" + dtRtlMas[0].Par_Rtl_Ref_No + ":" + dtRtlMas[0].Par_Rtl_Name : "";
                                txtDelQty2.Text = dr.Tran_Qty.ToString();
                            }
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "D/O data not found.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    txtTruckNo.Text = "";
                    txtDriverName.Text = "";
                    txtDriverContact.Text = "";
                    txtDelAddr.Text = "";
                    txtRetailer1.Text = "";
                    txtRetailer2.Text = "";
                    txtDelQty1.Text = "";
                    txtDelQty2.Text = "";
                }
            }
            catch (Exception ex) { }
        }

        protected void btnSaveChln_Click(object sender, EventArgs e)
        {
            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            var taDoDet = new tblSalesOrdDelDetTableAdapter();
            var taSoHdr = new tblSalesOrderHdrTableAdapter();
            var taSoDet = new tblSalesOrderDetTableAdapter();
            var taCrReal = new tblSalesCreditRealizeTableAdapter();
            var taAcc = new tbl_Acc_Fa_TeTableAdapter();
            var taStkCtl = new tbl_InMa_Stk_CtlTableAdapter();
            var taSalesByRtl = new tblSalesByRetailerTableAdapter();
            var taComm = new tbl_Tran_ComTableAdapter();
            
            var taEmp = new View_Emp_BascTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taInvHdr.Connection);

            try
            {
                #region Form Data Validation

                #region Chack Entry Field
                if (txtDelAddr.Text.Trim().Length <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "You have to enter delivery address first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                var vehicleNo="";
                if (optTranBy.SelectedValue == "1")
                    vehicleNo = txtTruckNo.Text.Trim();
                else
                    vehicleNo = cboTruckNo.SelectedValue == "0" ? "" : cboTruckNo.SelectedItem.ToString();

                if (vehicleNo == "")
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "You have to enter vehile no first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                if (cboCustDist.SelectedValue == "0")
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Select District first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                if (cboCustThana.SelectedValue == "0")
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Select District first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                if (ddlDelStore.SelectedValue == "0")
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Select Store first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                #region Check DO Selection
                var i = 0;
                foreach (GridViewRow gr in gvDoDet.Rows)
                {                    
                    var chkDoDelQty = (CheckBox)(gr.FindControl("chkDoDelQty"));

                    if (chkDoDelQty.Checked) i++;                    
                }
                if (i <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "You have to select D/O first.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                #region Check Challan Quantity
                foreach (GridViewRow gr in gvDoDet.Rows)
                {
                    var hfDoHdrRef = (HiddenField)(gr.FindControl("hfDoHdrRef"));
                    var lblDoHdrRefNo = (Label)(gr.FindControl("lblDoHdrRefNo"));
                    var hfDoDetLno = (HiddenField)(gr.FindControl("hfDoDetLno"));
                    var txtDoQty = (TextBox)(gr.FindControl("txtDoQty"));
                    var txtDoFreeQty = (TextBox)(gr.FindControl("txtDoFreeQty"));
                    var chkDelQty = (CheckBox)(gr.FindControl("chkDoDelQty"));

                    if (chkDelQty.Checked)
                    {
                        if (txtDoQty.Text.Trim() == "" || txtDoQty.Text.Trim().Length <= 0 || Convert.ToDouble(txtDoQty.Text.Trim()) <= 0)
                        {
                            if (txtDoFreeQty.Text.Trim() == "" || txtDoFreeQty.Text.Trim().Length <= 0 || Convert.ToDouble(txtDoFreeQty.Text.Trim()) <= 0)
                            {
                                chkDelQty.BackColor = System.Drawing.Color.Red;
                                tblMsg.Rows[0].Cells[0].InnerText = "Please enter challan quantity first.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                            else
                            {
                                var taDoDetChk = new tblSalesOrdDelDetTableAdapter();
                                var dtDoDetChk = taDoDetChk.GetDataByDetLno(hfDoHdrRef.Value.ToString(), Convert.ToInt16(hfDoDetLno.Value.ToString()));
                                if (dtDoDetChk.Rows.Count > 0)
                                {
                                    if (Convert.ToDouble(txtDoFreeQty.Text.Trim()) > Convert.ToDouble(dtDoDetChk[0].DO_Det_Ext_Data2))
                                    {
                                        tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to deliver free bag more than : " + dtDoDetChk[0].DO_Det_Ext_Data1;
                                        tblMsg.Rows[1].Cells[0].InnerText = "";
                                        ModalPopupExtenderMsg.Show();
                                        return;
                                    }
                                }
                                else
                                {
                                    chkDelQty.BackColor = System.Drawing.Color.Red;
                                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid D/O number.";
                                    tblMsg.Rows[1].Cells[0].InnerText = "";
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            var taDoDetChk = new tblSalesOrdDelDetTableAdapter();
                            var dtDoDetChk = taDoDetChk.GetDataByDetLno(hfDoHdrRef.Value.ToString(), Convert.ToInt16(hfDoDetLno.Value.ToString()));
                            if (dtDoDetChk.Rows.Count > 0)
                            {
                                if (Convert.ToDouble(txtDoQty.Text.Trim()) > dtDoDetChk[0].DO_Det_Del_Bal_Qty)
                                {
                                    tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to deliver qty more than : " + dtDoDetChk[0].DO_Det_Del_Bal_Qty;
                                    tblMsg.Rows[1].Cells[0].InnerText = "";
                                    ModalPopupExtenderMsg.Show();
                                    return;
                                }
                            }
                            else
                            {
                                chkDelQty.BackColor = System.Drawing.Color.Red;
                                tblMsg.Rows[0].Cells[0].InnerText = "Invalid D/O number.";
                                tblMsg.Rows[1].Cells[0].InnerText = "";
                                ModalPopupExtenderMsg.Show();
                                return;
                            }
                        }
                    }
                }
                #endregion

                #region Check Retailer Quantity
                double totDLRQty = 0;
                double totRTLQty=0;
                foreach (GridViewRow gr in gvDoDet.Rows)
                {
                    var hfDoHdrRef = (HiddenField)(gr.FindControl("hfDoHdrRef"));
                    var lblDoHdrRefNo = (Label)(gr.FindControl("lblDoHdrRefNo"));
                    var hfDoDetLno = (HiddenField)(gr.FindControl("hfDoDetLno"));
                    var txtDoQty = (TextBox)(gr.FindControl("txtDoQty"));
                    var txtDoFreeQty = (TextBox)(gr.FindControl("txtDoFreeQty"));
                    var chkDelQty = (CheckBox)(gr.FindControl("chkDoDelQty"));

                    if (chkDelQty.Checked)
                    {
                        if (txtDoQty.Text.Trim() != "" || txtDoQty.Text.Trim().Length >= 0 || Convert.ToDouble(txtDoQty.Text.Trim()) > 0)
                        {
                            totDLRQty = totDLRQty + Convert.ToDouble(txtDoQty.Text.Trim());
                            if (txtDoFreeQty.Text.Trim() != "" || txtDoFreeQty.Text.Trim().Length >= 0 || Convert.ToDouble(txtDoFreeQty.Text.Trim()) > 0)
                            {
                                totDLRQty = totDLRQty + Convert.ToDouble(txtDoFreeQty.Text.Trim());
                            }
                        }
                    }
                }

                totRTLQty = Convert.ToDouble(txtDelQty1.Text.Trim().Length <= 0 ? "0" : txtDelQty1.Text.Trim()) + Convert.ToDouble(txtDelQty2.Text.Trim().Length <= 0 ? "0" : txtDelQty2.Text.Trim());

                if (totRTLQty > totDLRQty)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Retailer wise breakdown qty is more than total D/O qty: " + totDLRQty;
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                #endregion

                #region Get Customer Details
                var custRef = "";
                var custAcc="";
                var custName = "";
                var srchWords = txtSearch.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    custRef = word;
                    break;
                }

                if (custRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(custRef, out result))
                    {
                        var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
                        {
                            custRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                            custAcc = dtPartyAdr[0].Par_Adr_Acc_Code.ToString();
                            custName = dtPartyAdr[0].Par_Adr_Name.ToString();
                        }
                        else
                        {
                            myTran.Rollback();
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        myTran.Rollback();
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer.";
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

                if (txtDelQty1.Text.Trim().Length > 0 && txtDelQty1.Text.Trim() != "")
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

                if (txtDelQty2.Text.Trim().Length > 0 && txtDelQty2.Text.Trim() != "")
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

                #region Get Employee Details
                string empId = "", empName = "", empDesig = "";
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                {
                    empId = dtEmp[0].EmpId.ToString();
                    empName = dtEmp[0].EmpName.ToString();
                    empDesig = dtEmp[0].DesigName.ToString();
                }
                #endregion

                var nextHdrRef = 1;
                var nextHdrRefNo = "";

                var nextChlnRef = "000001";
                var nextChlnRefNo = "";

                taInvHdr.AttachTransaction(myTran);
                taInvDet.AttachTransaction(myTran);
                taDoDet.AttachTransaction(myTran);
                taSoHdr.AttachTransaction(myTran);
                taSoDet.AttachTransaction(myTran);
                taCrReal.AttachTransaction(myTran);
                taAcc.AttachTransaction(myTran);
                taStkCtl.AttachTransaction(myTran);
                taSalesByRtl.AttachTransaction(myTran);
                taComm.AttachTransaction(myTran);

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
                    //Inventory Header Ref
                    var dtMaxHdrRef = taInvHdr.GetMaxHdrRef();
                    nextHdrRef = dtMaxHdrRef == null ? 1 : Convert.ToInt32(dtMaxHdrRef) + 1;

                    var dtMaxSalRef = taInvHdr.GetMaxHdrRefNo("SAL", DateTime.Now.Year);
                    var nextSalRef = dtMaxSalRef == null ? 1 : Convert.ToInt32(dtMaxSalRef) + 1;
                    nextHdrRefNo = "ECIL-SAL-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextSalRef).ToString("000000");

                    //Challan Header Ref
                    var dtMaxChlnRef = taInvHdr.GetMaxChlnRef(DateTime.Now.Year);
                    nextChlnRef = dtMaxChlnRef == null ? "000001" : (Convert.ToInt32(dtMaxChlnRef) + 1).ToString("000000");
                    nextChlnRefNo = "ECIL-CLN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextChlnRef).ToString("000000");

                    taInvHdr.InsertInvHdr(nextHdrRef, "IS", "SAL", nextHdrRefNo, custRef.ToString(), custRef.ToString(), custRef.ToString(),
                        nextChlnRefNo.ToString(), DateTime.Now, truckNo.ToString(), driverName.ToString(), txtDriverContact.Text.Trim(),
                        txtDelAddr.Text.Trim(), cboCustDist.SelectedValue.ToString(), cboCustThana.SelectedValue.ToString(), "", "",
                        optTranBy.SelectedValue.ToString(), truckRef.ToString(), 0, "P",
                        (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                        "ADM", "", ddlOrdTransMode.SelectedValue, "", DateTime.Now.Year + nextChlnRef, "", nextChlnRefNo, custName, "", "H", 0, DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "1", "");

                    var dtMaxComSeqNo = taComm.GetMaxComSeqNo(nextHdrRefNo);
                    var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                    taComm.InsertTranCom(nextHdrRefNo, nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "CLN", "INI", "(Challan Created By: " + empName + ") ", "", "1", "", "", "", "");

                    if (txtDelQty1.Text.Trim().Length > 0 && txtDelQty1.Text != "")
                    {
                        var dtMaxRtlSalesRefByYr = taSalesByRtl.GetMaxRef(DateTime.Now.Year);
                        var nexSalesRefByYr = dtMaxRtlSalesRefByYr == null ? "000001" : (Convert.ToInt32(dtMaxRtlSalesRefByYr) + 1).ToString("000000");
                        var nextSalesRefNo = DateTime.Now.Year.ToString() + Convert.ToInt32(nexSalesRefByYr).ToString("000000");
                        taSalesByRtl.InsertSales(Convert.ToInt32(nextSalesRefNo), nextChlnRefNo, rtlRef1.ToString(), DateTime.Now, 1, "CLN", Convert.ToDouble(txtDelQty1.Text.Trim()), "", "", "", "", "", "H", "1");
                    }

                    if (txtDelQty2.Text.Trim().Length > 0 && txtDelQty2.Text != "")
                    {
                        var dtMaxRtlSalesRefByYr = taSalesByRtl.GetMaxRef(DateTime.Now.Year);
                        var nexRtlSalesRefByYr = dtMaxRtlSalesRefByYr == null ? "000001" : (Convert.ToInt32(dtMaxRtlSalesRefByYr) + 1).ToString("000000");
                        var nextSalesRefNo = DateTime.Now.Year.ToString() + Convert.ToInt32(nexRtlSalesRefByYr).ToString("000000");
                        taSalesByRtl.InsertSales(Convert.ToInt32(nextSalesRefNo), nextChlnRefNo, rtlRef2.ToString(), DateTime.Now, 2, "CLN", Convert.ToDouble(txtDelQty2.Text.Trim()), "", "", "", "", "", "H", "1");
                    }

                    var dtMaxAccRef = taAcc.GetMaxRefNo("SJV", DateTime.Now.Year);
                    var nextAccRef = dtMaxAccRef == null ? "000001" : (Convert.ToInt32(dtMaxAccRef) + 1).ToString();
                    var nextAccRefNo = "SJV" + (DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy")).ToString() + "-" + Convert.ToInt32(nextAccRef).ToString("000000");

                    var dtCrSum = taAcc.GetTotCrAmt(custAcc.ToString());
                    var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                    var dtDrSum = taAcc.GetTotDrAmt(custAcc.ToString());
                    var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);
                    var CrBal = (crAmt - drAmt);

                    #region Insert Inventory Details and Update D/O Balance
                    var jvLNo = 0;
                    short Lno = 0;
                    double totChlnAmt = 0;
                    foreach (GridViewRow gr in gvDoDet.Rows)
                    {
                        var hfDoHdrRef = (HiddenField)(gr.FindControl("hfDoHdrRef"));
                        var lblDoHdrRefNo = (Label)(gr.FindControl("lblDoHdrRefNo"));
                        var hfDoDetLno = (HiddenField)(gr.FindControl("hfDoDetLno"));

                        var hfIcode = (HiddenField)(gr.FindControl("hfICode"));
                        var lblItemDesc = (Label)(gr.FindControl("lblItemDesc"));
                        var lblItemUom = (Label)(gr.FindControl("lblUom"));

                        var txtDoQty = (TextBox)(gr.FindControl("txtDoQty"));
                        var txtDoFreeQty = (TextBox)(gr.FindControl("txtDoFreeQty"));
                        var chkDelQty = (CheckBox)(gr.FindControl("chkDoDelQty"));

                        #region Get Item Details
                        var itemName = "";
                        var itemAcc = "";
                        var taItemDet = new tbl_InMa_Item_DetTableAdapter();
                        var dtItemDet = taItemDet.GetDataByItemRef(Convert.ToInt32(hfIcode.Value.Trim()));
                        if (dtItemDet.Rows.Count > 0)
                        {
                            itemName = dtItemDet[0].Itm_Det_Desc.ToString();
                            itemAcc = dtItemDet[0].Itm_Det_Acc_Code.ToString();
                        }
                        else
                        {
                            myTran.Rollback();
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                        #endregion

                        if (chkDelQty.Checked)
                        {
                            Lno++;                            

                            if (txtDoQty.Text.Trim() != "" || txtDoQty.Text.Trim().Length > 0)
                            {
                                if (Convert.ToDouble(txtDoQty.Text.Trim()) > 0)
                                {
                                    var dtDoDet = taDoDet.GetDataByDetLno(hfDoHdrRef.Value.ToString(), Convert.ToInt16(hfDoDetLno.Value.ToString()));
                                    if (dtDoDet.Rows.Count > 0)
                                    {
                                        var dtStkCtl = taStkCtl.GetDataByStoreItem(ddlDelStore.SelectedValue.ToString(), hfIcode.Value.Trim());
                                        if (dtStkCtl.Rows.Count > 0)
                                        {
                                            if (dtStkCtl[0].Stk_Ctl_Cur_Stk >= Convert.ToDouble(txtDoQty.Text.Trim()))
                                            {
                                                taInvDet.InsertInvDet(nextHdrRef.ToString(), "IS", "SAL", nextHdrRefNo.ToString(), Lno, "", 1, nextChlnRefNo.ToString(),
                                                    Lno, hfIcode.Value.Trim(), lblItemDesc.Text.Trim(), lblItemUom.Text.Trim(), ddlDelStore.SelectedValue.ToString(), "",
                                                    hfDoHdrRef.Value.ToString(), lblDoHdrRefNo.Text.Trim(), Convert.ToInt16(hfDoDetLno.Value.ToString()), DateTime.Now.Year + nextChlnRef.ToString(),
                                                    DateTime.Now, DateTime.Now, Convert.ToDouble(txtDoQty.Text.Trim()), Convert.ToDouble(txtDoFreeQty.Text.Trim()),
                                                    dtDoDet[0].DO_Det_Lin_Rat, Convert.ToDecimal(txtDoQty.Text.Trim()) * dtDoDet[0].DO_Det_Lin_Rat,
                                                    Convert.ToDecimal(txtDoQty.Text.Trim()) * dtDoDet[0].DO_Det_Lin_Rat, "", "", "", 0, 
                                                    Convert.ToDouble(txtDoQty.Text.Trim()) + Convert.ToDouble(txtDoFreeQty.Text.Trim()), "1", "");

                                                taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk - Convert.ToDouble(txtDoQty.Text.Trim())), 4), ddlDelStore.SelectedValue.ToString(), hfIcode.Value.Trim());

                                                taDoDet.UpdateDoDeliveryQty((dtDoDet[0].DO_Det_Org_QTY + Convert.ToDouble(txtDoQty.Text.Trim())),
                                                    (dtDoDet[0].DO_Det_Lin_Qty) - (dtDoDet[0].DO_Det_Org_QTY + Convert.ToDouble(txtDoQty.Text.Trim())),
                                                    (dtDoDet[0].DO_Det_Del_Qty + Convert.ToDouble(txtDoQty.Text.Trim())),
                                                    Convert.ToDouble(Convert.ToDecimal(dtDoDet[0].DO_Det_Del_Bal_Qty) - Convert.ToDecimal(txtDoQty.Text.Trim())),
                                                    hfDoHdrRef.Value.ToString(), Convert.ToInt16(hfDoDetLno.Value.ToString()));

                                                #region Insert Accounts Voucher Entry
                                                var dtSoDet = taSoDet.GetDataByDetLno(dtDoDet[0].DO_Det_SO_Ref.ToString(), Convert.ToInt16(dtDoDet[0].DO_Det_SO_Lno.ToString()));
                                                if (dtSoDet.Rows.Count > 0)
                                                {
                                                    var OrderDelQty = Convert.ToDouble(txtDoQty.Text.Trim());
                                                    var ItemRate = Convert.ToDouble(dtSoDet[0].SO_Det_Lin_Rat);
                                                    //var TransRate=Convert.ToDouble(dtSoDet[0].SO_Det_Trans_Rat);
                                                    var TransRate = Convert.ToDouble(dtDoDet[0].DO_Det_Trans_Rat);
                                                    var OrdDiscount = (Convert.ToDouble(dtSoDet[0].SO_Det_Lin_Dis) * OrderDelQty) / Convert.ToDouble(dtSoDet[0].SO_Det_Lin_Qty);
                                                    var SalesAmt = Convert.ToDecimal((OrderDelQty * ItemRate) - OrdDiscount);
                                                    var TransAmt = Convert.ToDecimal(OrderDelQty * TransRate);
                                                    var NetAmt = Convert.ToDecimal(((OrderDelQty * ItemRate) + (OrderDelQty * TransRate)) - OrdDiscount);

                                                    totChlnAmt = totChlnAmt + Convert.ToDouble(SalesAmt);

                                                    #region Sales Journal
                                                    jvLNo++;
                                                    //Debit-Customer Account
                                                    taAcc.InsertAccData(custAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                                        nextAccRefNo.ToString(), jvLNo, 1, itemName.ToString(), "D", SalesAmt, nextHdrRefNo.ToString(), "0",
                                                        "BDT", 1, SalesAmt, "", "", "", "", "", "", "", "", "", "", "",
                                                        (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, custName.ToString(), DateTime.Now,
                                                        "ADM", "P", "", DateTime.Now, "SJV", "L", 0, "BDT", 1, "BDT", 1, SalesAmt, "",
                                                        nextChlnRefNo.ToString(), dtSoDet[0].SO_Det_Ref_No, "N", 1, 0, "", lblDoHdrRefNo.Text.Trim(), "", "J", 0, "1", "SJV");

                                                    jvLNo++;
                                                    //Credit-Item Account
                                                    taAcc.InsertAccData(itemAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                                        nextAccRefNo.ToString(), jvLNo, 1, custName.ToString(), "C", SalesAmt, nextHdrRefNo.ToString(), "0",
                                                        "BDT", 1, SalesAmt, "", "", "", "", "", "", "", "", "", "", "",
                                                        (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, itemName.ToString(), DateTime.Now,
                                                        "ADM", "I", "", DateTime.Now, "SJV", "L", 0, "BDT", 1, "BDT", 1, SalesAmt, "",
                                                        nextChlnRefNo.ToString(), dtSoDet[0].SO_Det_Ref_No, "N", 1, 0, "", lblDoHdrRefNo.Text.Trim(), "", "J", 0, "1", "SJV");
                                                    #endregion

                                                    if (ddlDelStore.SelectedValue == "1014")//----------Vera Ghat Pabna--26Tk
                                                    {
                                                        TransAmt = Convert.ToDecimal(OrderDelQty * 26);
                                                        totChlnAmt = totChlnAmt + Convert.ToDouble(TransAmt);

                                                        #region Carrying Journal
                                                        jvLNo++;
                                                        //Debit-Customer Account
                                                        taAcc.InsertAccData(custAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                                            nextAccRefNo.ToString(), jvLNo, 1, "Sales Carrying Charge", "D", TransAmt, nextHdrRefNo.ToString(), "0",
                                                            "BDT", 1, TransAmt, "", "", "", "", "", "", "", "", "", "", "",
                                                            (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, custName.ToString(), DateTime.Now,
                                                            "ADM", "P", "", DateTime.Now, "SJV", "L", 0, "BDT", 1, "BDT", 1, TransAmt, "",
                                                            nextChlnRefNo.ToString(), dtSoDet[0].SO_Det_Ref_No, "N", 1, 0, "", lblDoHdrRefNo.Text.Trim(), "", "J", 0, "1", "SJV");

                                                        jvLNo++;
                                                        //Credit-Sales Carrying
                                                        taAcc.InsertAccData("01.001.001.0246".ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                                            nextAccRefNo.ToString(), jvLNo, 1, custName.ToString(), "C", TransAmt, nextHdrRefNo.ToString(), "0",
                                                            "BDT", 1, TransAmt, "", "", "", "", "", "", "", "", "", "", "",
                                                            (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, "Sales Carrying Charge", DateTime.Now,
                                                            "ADM", "O", "", DateTime.Now, "SJV", "L", 0, "BDT", 1, "BDT", 1, TransAmt, "",
                                                            nextChlnRefNo.ToString(), dtSoDet[0].SO_Det_Ref_No, "N", 1, 0, "", lblDoHdrRefNo.Text.Trim(), "", "J", 0, "1", "SJV");
                                                        #endregion
                                                    }
                                                    else if (ddlDelStore.SelectedValue == "1010")//----------Faridpur Ghat/C&B Ghat--22Tk
                                                    {
                                                        TransAmt = Convert.ToDecimal(OrderDelQty * 22);
                                                        totChlnAmt = totChlnAmt + Convert.ToDouble(TransAmt);

                                                        #region Carrying Journal
                                                        jvLNo++;
                                                        //Debit-Customer Account
                                                        taAcc.InsertAccData(custAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                                            nextAccRefNo.ToString(), jvLNo, 1, "Sales Carrying Charge", "D", TransAmt, nextHdrRefNo.ToString(), "0",
                                                            "BDT", 1, TransAmt, "", "", "", "", "", "", "", "", "", "", "",
                                                            (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, custName.ToString(), DateTime.Now,
                                                            "ADM", "P", "", DateTime.Now, "SJV", "L", 0, "BDT", 1, "BDT", 1, TransAmt, "",
                                                            nextChlnRefNo.ToString(), dtSoDet[0].SO_Det_Ref_No, "N", 1, 0, "", lblDoHdrRefNo.Text.Trim(), "", "J", 0, "1", "SJV");

                                                        jvLNo++;
                                                        //Credit-Sales Carrying
                                                        taAcc.InsertAccData("01.001.001.0246".ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                                            nextAccRefNo.ToString(), jvLNo, 1, custName.ToString(), "C", TransAmt, nextHdrRefNo.ToString(), "0",
                                                            "BDT", 1, TransAmt, "", "", "", "", "", "", "", "", "", "", "",
                                                            (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                                            Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, "Sales Carrying Charge", DateTime.Now,
                                                            "ADM", "O", "", DateTime.Now, "SJV", "L", 0, "BDT", 1, "BDT", 1, TransAmt, "",
                                                            nextChlnRefNo.ToString(), dtSoDet[0].SO_Det_Ref_No, "N", 1, 0, "", lblDoHdrRefNo.Text.Trim(), "", "J", 0, "1", "SJV");
                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        if (TransRate > 0)
                                                        {
                                                            totChlnAmt = totChlnAmt + Convert.ToDouble(TransAmt);

                                                            #region Carrying Journal
                                                            jvLNo++;
                                                            //Debit-Customer Account
                                                            taAcc.InsertAccData(custAcc.ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                                                nextAccRefNo.ToString(), jvLNo, 1, "Sales Carrying Charge", "D", TransAmt, nextHdrRefNo.ToString(), "0",
                                                                "BDT", 1, TransAmt, "", "", "", "", "", "", "", "", "", "", "",
                                                                (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, custName.ToString(), DateTime.Now,
                                                                "ADM", "P", "", DateTime.Now, "SJV", "L", 0, "BDT", 1, "BDT", 1, TransAmt, "",
                                                                nextChlnRefNo.ToString(), dtSoDet[0].SO_Det_Ref_No, "N", 1, 0, "", lblDoHdrRefNo.Text.Trim(), "", "J", 0, "1", "SJV");

                                                            jvLNo++;
                                                            //Credit-Sales Carrying
                                                            taAcc.InsertAccData("01.001.001.0246".ToString(), (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(),
                                                                nextAccRefNo.ToString(), jvLNo, 1, custName.ToString(), "C", TransAmt, nextHdrRefNo.ToString(), "0",
                                                                "BDT", 1, TransAmt, "", "", "", "", "", "", "", "", "", "", "",
                                                                (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()), DateTime.Now,
                                                                Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), "", DateTime.Now, "Sales Carrying Charge", DateTime.Now,
                                                                "ADM", "O", "", DateTime.Now, "SJV", "L", 0, "BDT", 1, "BDT", 1, TransAmt, "",
                                                                nextChlnRefNo.ToString(), dtSoDet[0].SO_Det_Ref_No, "N", 1, 0, "", lblDoHdrRefNo.Text.Trim(), "", "J", 0, "1", "SJV");
                                                            #endregion
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    myTran.Rollback();
                                                    tblMsg.Rows[0].Cells[0].InnerText = "Sales Order does not match.";
                                                    tblMsg.Rows[1].Cells[0].InnerText = "";
                                                    ModalPopupExtenderMsg.Show();
                                                    return;
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                myTran.Rollback();
                                                tblMsg.Rows[0].Cells[0].InnerText = "There is not sufficient stock for the item [" + lblItemDesc.Text.Trim() + "]";
                                                tblMsg.Rows[1].Cells[0].InnerText = "Current Stock is: " + dtStkCtl[0].Stk_Ctl_Cur_Stk.ToString();
                                                ModalPopupExtenderMsg.Show();
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            myTran.Rollback();
                                            tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error";
                                            tblMsg.Rows[1].Cells[0].InnerText = "Inventory data not found for the item " + lblItemDesc.Text.Trim();
                                            ModalPopupExtenderMsg.Show();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        myTran.Rollback();
                                        tblMsg.Rows[0].Cells[0].InnerText = "Delivery Order does not match.";
                                        tblMsg.Rows[1].Cells[0].InnerText = "";
                                        ModalPopupExtenderMsg.Show();
                                        return;
                                    }
                                }
                            }

                            if (txtDoFreeQty.Text.Trim() != "" || txtDoFreeQty.Text.Trim().Length > 0)
                            {
                                if (Convert.ToDouble(txtDoFreeQty.Text.Trim()) > 0)
                                {
                                    var dtDoDet = taDoDet.GetDataByDetLno(hfDoHdrRef.Value.ToString(), Convert.ToInt16(hfDoDetLno.Value.ToString()));
                                    if (dtDoDet.Rows.Count > 0)
                                    {
                                        var dtStkCtl = taStkCtl.GetDataByStoreItem(ddlDelStore.SelectedValue.ToString(), hfIcode.Value.Trim());
                                        if (dtStkCtl.Rows.Count > 0)
                                        {
                                            if (dtStkCtl[0].Stk_Ctl_Cur_Stk >= Convert.ToDouble(txtDoFreeQty.Text.Trim()))
                                            {
                                                taInvDet.InsertInvDet(nextHdrRef.ToString(), "IS", "BNS", nextHdrRefNo.ToString(), Lno, "", 1, nextChlnRefNo.ToString(),
                                                    Lno, hfIcode.Value.Trim(), lblItemDesc.Text.Trim(), lblItemUom.Text.Trim(), ddlDelStore.SelectedValue.ToString(), "",
                                                    hfDoHdrRef.Value.ToString(), lblDoHdrRefNo.Text.Trim(), Convert.ToInt16(hfDoDetLno.Value.ToString()), DateTime.Now.Year + nextChlnRef.ToString(),
                                                    DateTime.Now, DateTime.Now, Convert.ToDouble(txtDoFreeQty.Text.Trim()), 0, dtDoDet[0].DO_Det_Lin_Rat,
                                                    Convert.ToDecimal(txtDoFreeQty.Text.Trim()) * dtDoDet[0].DO_Det_Lin_Rat,
                                                    Convert.ToDecimal(txtDoFreeQty.Text.Trim()) * dtDoDet[0].DO_Det_Lin_Rat, "", "", "", 0, 0, "1", "");

                                                taStkCtl.UpdateStkCtlCurStk(Math.Round((dtStkCtl[0].Stk_Ctl_Cur_Stk - Convert.ToDouble(txtDoFreeQty.Text.Trim())), 4), ddlDelStore.SelectedValue.ToString(), hfIcode.Value.Trim());

                                                taDoDet.UpdateDoFreeBagDeliveryQty(Convert.ToDouble(dtDoDet[0].DO_Det_Del_Qty) + Convert.ToDouble(txtDoFreeQty.Text.Trim()),
                                                    Convert.ToDouble(Convert.ToDecimal(dtDoDet[0].DO_Det_Del_Bal_Qty) - Convert.ToDecimal(txtDoFreeQty.Text.Trim())),
                                                    (Convert.ToDouble(dtDoDet[0].DO_Det_Ext_Data1) + Convert.ToDouble(txtDoFreeQty.Text.Trim())).ToString(),
                                                    Convert.ToDouble(Convert.ToDecimal(dtDoDet[0].DO_Det_Ext_Data2) - Convert.ToDecimal(txtDoFreeQty.Text.Trim())).ToString(),
                                                    hfDoHdrRef.Value.ToString(), Convert.ToInt16(hfDoDetLno.Value.ToString()));
                                            }
                                            else
                                            {
                                                myTran.Rollback();
                                                tblMsg.Rows[0].Cells[0].InnerText = "There is not sufficient stock for the item [" + lblItemDesc.Text.Trim() + "]";
                                                tblMsg.Rows[1].Cells[0].InnerText = "Current Stock is: " + dtStkCtl[0].Stk_Ctl_Cur_Stk.ToString();
                                                ModalPopupExtenderMsg.Show();
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            myTran.Rollback();
                                            tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error";
                                            tblMsg.Rows[1].Cells[0].InnerText = "Inventory data not found item " + lblItemDesc.Text.Trim();
                                            ModalPopupExtenderMsg.Show();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        myTran.Rollback();
                                        tblMsg.Rows[0].Cells[0].InnerText = "Delivery Order does not match.";
                                        tblMsg.Rows[1].Cells[0].InnerText = "";
                                        ModalPopupExtenderMsg.Show();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    if (CrBal > 0)
                    {
                        if (CrBal > Convert.ToDouble(totChlnAmt))
                            taCrReal.InsertCreditRealize(DateTime.Now.Year + nextChlnRef.ToString(), nextChlnRefNo.ToString(), Convert.ToDateTime(txtChallanDate.Text.Trim()),
                                Convert.ToDecimal(totChlnAmt), 0, custRef.ToString(), "1", "", "", "", "", "");
                        else
                            taCrReal.InsertCreditRealize(DateTime.Now.Year + nextChlnRef.ToString(), nextChlnRefNo.ToString(), Convert.ToDateTime(txtChallanDate.Text.Trim()),
                                Convert.ToDecimal(totChlnAmt), Convert.ToDecimal(totChlnAmt) - Convert.ToDecimal(CrBal), custRef.ToString(), "1", "", "", "", "", "");
                    }
                    else
                    {
                        taCrReal.InsertCreditRealize(DateTime.Now.Year + nextChlnRef.ToString(), nextChlnRefNo.ToString(), Convert.ToDateTime(txtChallanDate.Text.Trim()),
                            Convert.ToDecimal(totChlnAmt), Convert.ToDecimal(totChlnAmt), custRef.ToString(), "1", "", "", "", "", "");
                    }

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    var curYear = DateTime.Now.Year;
                    cboYear.SelectedValue = curYear.ToString();

                    var curMonth = DateTime.Now.Month;
                    cboMonth.SelectedValue = curMonth.ToString();

                    ddlChallanList.DataSource = taInvHdr.GetChallanListByYearMonth(Convert.ToDecimal(cboYear.SelectedValue), Convert.ToDecimal(cboMonth.SelectedValue));
                    ddlChallanList.DataValueField = "Trn_Hdr_DC_No";
                    ddlChallanList.DataTextField = "Trn_Hdr_Cno";
                    ddlChallanList.DataBind();
                    ddlChallanList.Items.Insert(0, "----------Select----------");
                    ddlChallanList.SelectedIndex = ddlChallanList.Items.IndexOf(ddlChallanList.Items.FindByText(nextChlnRefNo));

                    var dtInvDet = taInvDet.GetDataByChlnRef(ddlChallanList.SelectedValue.ToString());
                    gvChlnDet.DataSource = dtInvDet;
                    gvChlnDet.DataBind();
                    btnPrint.Visible = true;

                    //Challan Header Ref
                    var dtGetMaxChlnRef = taInvHdr.GetMaxChlnRef(DateTime.Now.Year);
                    nextChlnRef = dtGetMaxChlnRef == null ? "000001" : (Convert.ToInt32(dtGetMaxChlnRef) + 1).ToString("000000");
                    nextChlnRefNo = "ECIL-CLN-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextChlnRef).ToString("000000");
                    txtChallanNo.Text = nextChlnRefNo;
                    txtChallanDate.Text = DateTime.Now.ToString();

                    SearchDelOrderData();

                    txtTruckNo.Text = "";
                    txtDriverName.Text = "";
                    txtDriverContact.Text = "";
                    txtDelAddr.Text = "";
                    txtRetailer1.Text = "";
                    txtRetailer2.Text = "";
                    txtDelQty1.Text = "";
                    txtDelQty2.Text = "";

                    ddlDelStore.SelectedIndex = ddlDelStore.Items.IndexOf(ddlDelStore.Items.FindByValue("1007"));
                }
                else
                {
                    //if data is in edit mode
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

        protected void chkIncAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIncAll.Checked)
                AutoCompleteExtenderSrch.ContextKey = "1";
            else
                AutoCompleteExtenderSrch.ContextKey = "0";
        }

        protected void ddlChallanList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taInvDet = new tbl_InTr_Trn_DetTableAdapter();
            if (ddlChallanList.SelectedIndex == 0)
            {
                btnPrint.Visible = false;
                var dtInvDet = taInvDet.GetDataByChlnRef("0");
                gvChlnDet.DataSource = dtInvDet;
                gvChlnDet.DataBind();
            }
            else
            {
                btnPrint.Visible = true;
                var dtInvDet = taInvDet.GetDataByChlnRef(ddlChallanList.SelectedValue.ToString());
                gvChlnDet.DataSource = dtInvDet;
                gvChlnDet.DataBind();
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                reportInfo();

                var taInTrTrnHdr = new tbl_InTr_Trn_HdrTableAdapter();
                taInTrTrnHdr.UpdateChlnPrintFlag("D", ddlChallanList.SelectedValue.ToString());

                var url = "frmShowSalesReport.aspx";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
            }
            catch (Exception ex) { }         
        }

        protected void reportInfo()
        {
            var taInTrTrnHdr = new tbl_InTr_Trn_HdrTableAdapter();

            try
            {
                rptSelcFormula = "{View_Sales_Do_Chln.Trn_Hdr_DC_No}='" + ddlChallanList.SelectedValue + "'";

                var dtInTrTrnHdr = taInTrTrnHdr.GetDataByChlnRef(ddlChallanList.SelectedValue.ToString());
                if (dtInTrTrnHdr.Rows.Count > 0)
                {
                    if (dtInTrTrnHdr[0].Trn_Hdr_EI_Flg == "D")
                        rptFile = "~/Module/Sales/Reports/rptDelChlnRePrint.rpt";
                    else
                        rptFile = "~/Module/Sales/Reports/rptDelChln.rpt";
                }

                Session["RptDateFrom"] = DateTime.Now.ToString("dd/MM/yyyy");
                Session["RptDateTo"] = DateTime.Now.ToString("dd/MM/yyyy");
                Session["RptFilePath"] = rptFile;
                Session["RptFormula"] = rptSelcFormula;
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

        protected void gvDoDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtDoQty = (TextBox)e.Row.FindControl("txtDoQty");
                TextBox txtDoFreeQty = (TextBox)e.Row.FindControl("txtDoFreeQty");

                HiddenField hfDoBalQty = (HiddenField)e.Row.FindControl("hfDoBalQty");
                HiddenField hfDoFreeBalQty = (HiddenField)e.Row.FindControl("hfDoFreeBalQty");

                //txtDoQty.Attributes.Add("onkeyup", "CalcFreeQty('" + txtDoQty.ClientID + "', '" + txtDoFreeQty.ClientID + "', '"
                //    + hfDoBalQty.Value.Trim() + "', '" + hfDoFreeBalQty.Value.Trim() + "' )");

                //txtDoFreeQty.Attributes.Add("onkeyup", "CheckFreeQty('" + txtDoQty.ClientID + "', '" + txtDoFreeQty.ClientID + "', '"
                //    + hfDoBalQty.Value.Trim() + "', '" + hfDoFreeBalQty.Value.Trim() + "' )");

                Label lblOrgDoQty = (Label)e.Row.FindControl("lblOrgDoQty");
                Label lblOrgDoFreeQty = (Label)e.Row.FindControl("lblOrgDoFreeQty");

                txtDoQty.Attributes.Add("onkeyup", "CalcFreeQty('" + txtDoQty.ClientID + "', '" + txtDoFreeQty.ClientID + "', '"
                    + lblOrgDoQty.Text.Trim() + "', '" + lblOrgDoFreeQty.Text.Trim() + "', '" + hfDoBalQty.Value.Trim() + "', '" + hfDoFreeBalQty.Value.Trim() + "' )");

                txtDoFreeQty.Attributes.Add("onkeyup", "CheckFreeQty('" + txtDoQty.ClientID + "', '" + txtDoFreeQty.ClientID + "', '"
                    + lblOrgDoQty.Text.Trim() + "', '" + lblOrgDoFreeQty.Text.Trim() + "', '" + hfDoBalQty.Value.Trim() + "', '" + hfDoFreeBalQty.Value.Trim() + "' )");
            }
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_CLN_List();
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_CLN_List();
        }

        private void Get_CLN_List()
        {
            btnPrint.Visible = false;
            gvChlnDet.DataSource = null;
            gvChlnDet.DataBind();

            var taInvHdr = new tbl_InTr_Trn_HdrTableAdapter();
            var dtInvHdr = new dsInvTran.tbl_InTr_Trn_HdrDataTable();

            if (cboMonth.SelectedIndex == 0)
                dtInvHdr = taInvHdr.GetChallanListByYear(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
            else
                dtInvHdr = taInvHdr.GetChallanListByYearMonth(Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));

            ddlChallanList.DataSource = dtInvHdr;
            ddlChallanList.DataValueField = "Trn_Hdr_DC_No";
            ddlChallanList.DataTextField = "Trn_Hdr_Cno";
            ddlChallanList.DataBind();
            ddlChallanList.Items.Insert(0, "----------Select----------");
        }
    }
}