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
using DRN_WEB_ERP.Module.Inventory.DataSet;
using DRN_WEB_ERP.Module.Inventory.DataSet.dsInvMasTableAdapters;
using DRN_WEB_ERP.Module.Transport.DataSet;
using DRN_WEB_ERP.Module.Transport.DataSet.dsTransMasTableAdapters;
using DRN_WEB_ERP.Module.Accounts.DataSet;
using DRN_WEB_ERP.Module.Accounts.DataSet.dsAccTranTableAdapters;
using DRN_WEB_ERP.Module.Procurement.DataSets.dsProcTranTableAdapters;
using DRN_WEB_ERP.Module.HRMS.DataSet.dsEmpDetTableAdapters;

namespace DRN_WEB_ERP.Module.Sales.Forms
{
    public partial class frmSalesOrder : System.Web.UI.Page
    {
        GlobalClass.clsNumToText numToEng = new GlobalClass.clsNumToText();

        double ordTotQty = 0;
        double ordTotAmt = 0;
        double ordTotAmtChk = 0;

        double ordLinQty = 0;
        double ordAppQty = 0;

        string rptFile;
        string rptSelcFormula;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            reportInfo();

            btnPrint.Attributes.Add("onclick", "javascript:w= window.open('frmShowSalesReport.aspx');");

            try
            {
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

                //var taOrdHdr = new tblSalesOrderHdrTableAdapter();
                //ddlOrderList.DataSource = taOrdHdr.GetData();
                //ddlOrderList.DataValueField = "SO_Hdr_Ref";
                //ddlOrderList.DataTextField = "SO_Hdr_Ref_No";
                //ddlOrderList.DataBind();
                //ddlOrderList.Items.Insert(0, "----------New----------");

                var taOrdHdr = new tblSalesOrderHdrTableAdapter();
                var dtOrdHdr = new dsSalesTran.tblSalesOrderHdrDataTable();
                if (cboMonth.SelectedIndex == 0)
                    dtOrdHdr = taOrdHdr.GetDataByYear(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                else
                    dtOrdHdr = taOrdHdr.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                ListItem lst;
                foreach (dsSalesTran.tblSalesOrderHdrRow dr in dtOrdHdr.Rows)
                {
                    lst = new ListItem();
                    lst.Text = dr.SO_Hdr_Ref_No.ToString() + "    [" + dr.So_Hdr_T_C1.ToString() + ", Date:" + dr.SO_Hdr_Date.ToString("dd-MM-yyyy") + "]";
                    lst.Value = dr.SO_Hdr_Ref.ToString();

                    ddlOrderList.Items.Add(lst);
                }
                ddlOrderList.Items.Insert(0, "----------New----------");

                txtOrdDate.Text = DateTime.Now.ToString("dd/MM/yyy");
                txtOrdDelDate.Text = DateTime.Now.ToString("dd/MM/yyy");
                txtOrdValidDate.Text = DateTime.Now.AddDays(30).ToString("dd/MM/yyy");

                var taSalesHdr = new tblSalesOrderHdrTableAdapter();
                var dtMaxHdrRef = taSalesHdr.GetMaxHdrRef(DateTime.Now.Year);
                var nextHdrRef = dtMaxHdrRef == null ? "000001" : (Convert.ToInt32(dtMaxHdrRef) + 1).ToString("000000");

                var dtMaxHdrRefByYr = taSalesHdr.GetMaxHdrRefByYr(DateTime.Now.Year);
                var nextHdrRefByYr = dtMaxHdrRefByYr == null ? "000001" : (Convert.ToInt32(dtMaxHdrRefByYr) + 1).ToString("000000");
                var nextHdrRefNo = "ECIL-ORD-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRefByYr).ToString("000000");
                lblOrdRefNo.Text = nextHdrRefNo;

                var taSaleType = new tblSalesTypeTableAdapter();
                var dtSaleType = taSaleType.GetData();
                foreach (dsSalesMas.tblSalesTypeRow dr in dtSaleType.Rows)
                {
                    ddlSalePrefix.Items.Add(new ListItem((dr.Sale_Type_Code.ToString() + "-" + dr.Sale_Type_Name.ToString()), dr.Sale_Type_Ref.ToString()));
                }

                var taSaleItem = new tbl_InMa_Item_DetTableAdapter();
                var dtSaleItem = taSaleItem.GetFgItemData();
                ddlOrdItem.DataSource = dtSaleItem;
                ddlOrdItem.DataTextField = "Itm_Det_Desc";
                ddlOrdItem.DataValueField = "Itm_Det_Ref";
                ddlOrdItem.DataBind();
                ddlOrdItem.Items.Insert(0, new ListItem("----------Select----------", "0"));
                ddlOrdItem.SelectedIndex = 0;

                var taItemUom = new tbl_InMa_UomTableAdapter();
                var dtItemUom = taItemUom.GetData();
                ddlOrdItemUom.DataSource = dtItemUom;
                ddlOrdItemUom.DataTextField = "Uom_Name";
                ddlOrdItemUom.DataValueField = "Uom_Code";
                ddlOrdItemUom.DataBind();
                ddlOrdItemUom.Items.Insert(0, "----");

                var taTransType = new tblTransportTypeTableAdapter();
                var dtTransType = taTransType.GetData();
                ddlOrdTransMode.DataSource = dtTransType;
                ddlOrdTransMode.DataTextField = "Trans_Type_Name";
                ddlOrdTransMode.DataValueField = "Trans_Type_Ref";
                ddlOrdTransMode.DataBind();

                hfEditStatus.Value = "N";
                hfRefNo.Value = "0";
                hfBnsPerc.Value = "0";

                LoadInitOrdDetGridData();
                SetOrdDetGridData();

                txtOrdQty.Attributes.Add("onkeyup", "CalcOrdAmount('" + txtOrdQty.ClientID + "', '" + txtOrdRate.ClientID + "', '" + txtOrdAmt.ClientID + "', '" + txtOrdTransRate.ClientID + "', '" + txtOrdGrsAmt.ClientID + "', '" + txtOrdDiscount.ClientID + "', '" + txtOrdNetAmt.ClientID + "', '" + hfBnsPerc.ClientID + "', '" + txtOrdFreeBag.ClientID + "' )");
                txtOrdRate.Attributes.Add("onkeyup", "CalcOrdAmount('" + txtOrdQty.ClientID + "', '" + txtOrdRate.ClientID + "', '" + txtOrdAmt.ClientID + "', '" + txtOrdTransRate.ClientID + "', '" + txtOrdGrsAmt.ClientID + "', '" + txtOrdDiscount.ClientID + "', '" + txtOrdNetAmt.ClientID + "', '" + hfBnsPerc.ClientID + "', '" + txtOrdFreeBag.ClientID + "' )");
                txtOrdTransRate.Attributes.Add("onkeyup", "CalcOrdAmount('" + txtOrdQty.ClientID + "', '" + txtOrdRate.ClientID + "', '" + txtOrdAmt.ClientID + "', '" + txtOrdTransRate.ClientID + "', '" + txtOrdGrsAmt.ClientID + "', '" + txtOrdDiscount.ClientID + "', '" + txtOrdNetAmt.ClientID + "', '" + hfBnsPerc.ClientID + "', '" + txtOrdFreeBag.ClientID + "' )");
                txtOrdDiscount.Attributes.Add("onkeyup", "CalcOrdAmount('" + txtOrdQty.ClientID + "', '" + txtOrdRate.ClientID + "', '" + txtOrdAmt.ClientID + "', '" + txtOrdTransRate.ClientID + "', '" + txtOrdGrsAmt.ClientID + "', '" + txtOrdDiscount.ClientID + "', '" + txtOrdNetAmt.ClientID + "', '" + hfBnsPerc.ClientID + "', '" + txtOrdFreeBag.ClientID + "' )");
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }

        }

        private void Get_SO_List()
        {
            ClearData();

            ddlOrderList.Items.Clear();

            var taOrdHdr = new tblSalesOrderHdrTableAdapter();
            var dtOrdHdr = new dsSalesTran.tblSalesOrderHdrDataTable();
            if (cboMonth.SelectedIndex == 0)
                dtOrdHdr = taOrdHdr.GetDataByYear(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
            else
                dtOrdHdr = taOrdHdr.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
            ListItem lst;
            foreach (dsSalesTran.tblSalesOrderHdrRow dr in dtOrdHdr.Rows)
            {
                lst = new ListItem();
                lst.Text = dr.SO_Hdr_Ref_No.ToString() + "    [" + dr.So_Hdr_T_C1.ToString() + ", Date:" + dr.SO_Hdr_Date.ToString("dd-MM-yyyy") + "]";
                lst.Value = dr.SO_Hdr_Ref.ToString();

                ddlOrderList.Items.Add(lst);
            }
            //ddlOrderList.DataSource = dtOrdHdr;
            //ddlOrderList.DataValueField = "SO_Hdr_Ref";
            //ddlOrderList.DataTextField = "SO_Hdr_Ref_No";
            //ddlOrderList.DataBind();
            ddlOrderList.Items.Insert(0, "----------New----------");
        }

        protected void txtCust_TextChanged(object sender, EventArgs e)
        {
            var taPartyAdr = new tblSalesPartyAdrTableAdapter();
            var dtPartyAdr = new dsSalesMas.tblSalesPartyAdrDataTable();

            var taSalesHdr = new tblSalesOrderHdrTableAdapter();
            

            try
            {                
                var custRef = "";
                var custAccCode = "";
                var srchWords = txtCust.Text.Trim().Split(':');
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
                        dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
                        {
                            if (DateTime.Now.Subtract(dtPartyAdr[0].Par_Adr_Ent_Date).Days >= 180)
                            {
                                var dtSalesHdr = taSalesHdr.GetDataByPartyOrderWithinSixMonths(custRef.ToString());
                                if (dtSalesHdr.Rows.Count <= 0)
                                {
                                    var taFaTeRcv = new tbl_Acc_Fa_TeTableAdapter();
                                    var dtFaTeRcv = taFaTeRcv.GetDataByPartyPayRcvWithinSixMonths(dtPartyAdr[0].Par_Adr_Acc_Code.ToString());
                                    if (dtFaTeRcv.Rows.Count <= 0)
                                    {
                                        taPartyAdr.UpdatePartyStatus("0", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                            Convert.ToInt32(custRef.ToString()));

                                        //tblMsg.Rows[0].Cells[0].InnerText = "Customer is Inactive. No transaction found within last 6 months. ";
                                        //tblMsg.Rows[1].Cells[0].InnerText = "To create Sales Order you need to Active Customer first.";
                                        //ModalPopupExtenderMsg.Show();
                                        //return;
                                    }
                                }
                            }
                        }
                        
                        lnkBtnCrRpt.Visible = dtPartyAdr.Rows.Count > 0;

                        var custStat = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Status : "0";

                        if (custStat == "0")
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Customer is Inactive. To create Sales Order you need to Active Customer first.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }

                        var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;
                        custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                        var taSalesOrder = new View_Sales_Ord_NewTableAdapter();
                        var dtSoBalAmtSum = taSalesOrder.GetSoBalAmt(custRef.ToString());
                        var soBalAmtSum = dtSoBalAmtSum == null ? 0 : Convert.ToDouble(dtSoBalAmtSum);
                        var taSalesOrderDo = new View_Delivery_Ord_NewTableAdapter();
                        var dtDoBalAmtSum = taSalesOrderDo.GetDoBalAmt(custRef.ToString());
                        var doBalAmtSum = dtDoBalAmtSum == null ? 0 : Convert.ToDouble(dtDoBalAmtSum);

                        var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                        var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                        var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                        var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                        var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                        txtOrdCrLimit.Text = crLimit.ToString("N2");
                        //txtOrdCrOutsatnd.Text = (drAmt - crAmt).ToString("N2");
                        txtOrdCrOutsatnd.Text = (soBalAmtSum + doBalAmtSum + (drAmt - crAmt)).ToString("N2");
                        //txtOrdCrLimitBal.Text = (Convert.ToDouble(crLimit) - (drAmt - crAmt)).ToString("N2");
                        txtOrdCrLimitBal.Text = (Convert.ToDouble(crLimit) - (soBalAmtSum + doBalAmtSum + (drAmt - crAmt))).ToString("N2");                        
                    }
                    else
                    {
                        txtOrdCrLimit.Text = "0.00";
                        txtOrdCrOutsatnd.Text = "0.00";
                        txtOrdCrLimitBal.Text = "0.00";
                    }
                }
                else
                {
                    txtOrdCrLimit.Text = "0.00";
                    txtOrdCrOutsatnd.Text = "0.00";
                    txtOrdCrLimitBal.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                txtOrdCrLimit.Text = "0.00";
                txtOrdCrOutsatnd.Text = "0.00";
                txtOrdCrLimitBal.Text = "0.00";
            }
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            Page.Validate("btnProcd");
            if (!Page.IsValid) return;

            try
            {
                double custCrLimitBal;

                var custRef = "";
                var srchWords = txtCust.Text.Trim().Split(':');
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
                            if (DateTime.Now.Subtract(dtPartyAdr[0].Par_Adr_Ent_Date).Days >= 180)
                            {
                                var taSalesHdr = new tblSalesOrderHdrTableAdapter();
                                var dtSalesHdr = taSalesHdr.GetDataByPartyOrderWithinSixMonths(custRef.ToString());
                                if (dtSalesHdr.Rows.Count <= 0)
                                {
                                    var taFaTeRcv = new tbl_Acc_Fa_TeTableAdapter();
                                    var dtFaTeRcv = taFaTeRcv.GetDataByPartyPayRcvWithinSixMonths(dtPartyAdr[0].Par_Adr_Acc_Code.ToString());
                                    if (dtFaTeRcv.Rows.Count <= 0)                                
                                    {
                                        taPartyAdr.UpdatePartyStatus("0", DateTime.Now, Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(),
                                            Convert.ToInt32(custRef.ToString()));

                                        tblMsg.Rows[0].Cells[0].InnerText = "Customer is Inactive. No transaction found within last 6 months. ";
                                        tblMsg.Rows[1].Cells[0].InnerText = "To create Sales Order you need to Active Customer first.";
                                        ModalPopupExtenderMsg.Show();
                                        return;
                                    }                                    
                                }
                            }
                        }

                        var custStat = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Status : "0";
                        if (custStat == "0")
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Customer is Inactive. To create Sales Order you need to Active Customer first.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }

                        var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;
                        var custGlCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                        var taSalesOrder = new View_Sales_Ord_NewTableAdapter();
                        var dtSoBalAmtSum = taSalesOrder.GetSoBalAmt(custRef.ToString());
                        var soBalAmtSum = dtSoBalAmtSum == null ? 0 : Convert.ToDouble(dtSoBalAmtSum);
                        var taSalesOrderDo = new View_Delivery_Ord_NewTableAdapter();
                        var dtDoBalAmtSum = taSalesOrderDo.GetDoBalAmt(custRef.ToString());
                        var doBalAmtSum = dtDoBalAmtSum == null ? 0 : Convert.ToDouble(dtDoBalAmtSum);

                        var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                        var dtCrSum = taFaTe.GetTotCrAmt(custGlCode.ToString());
                        var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                        var dtDrSum = taFaTe.GetTotDrAmt(custGlCode.ToString());
                        var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                        //var custCrOutsatnd = (drAmt - crAmt);
                        var custCrOutsatnd = soBalAmtSum + doBalAmtSum + (drAmt - crAmt);
                        //custCrLimitBal = (Convert.ToDouble(crLimit) - (drAmt - crAmt));
                        custCrLimitBal = (Convert.ToDouble(crLimit) - (soBalAmtSum + doBalAmtSum + (drAmt - crAmt)));
                    }
                    else
                    {
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

                if (custCrLimitBal > 0)
                {
                    ddlSalePrefix.Enabled = false;
                    ddlOrdTransCostBy.Enabled = false;
                    txtCust.Enabled = false;
                    txtSalesLoc.Enabled = false;
                    txtSalesPer.Enabled = false;

                    btnProceed.Visible = false;
                    btnClear.Visible = true;

                    pnlOrdDet.Visible = true;
                }
                else
                {
                    btnAddOrdDetYes.Visible = false;
                    btnProceedYes.Visible = true;                    
                    ModalPopupExtender5.Show();
                }                
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error. " + ex.Message.ToString(); 
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void ClearData()
        {
            try
            {
                //var curYear = DateTime.Now.Year;
                //cboYear.SelectedValue = curYear.ToString();

                //var curMonth = DateTime.Now.Month;
                //cboMonth.SelectedValue = curMonth.ToString();

                ddlOrderList.Items.Clear();

                //var taOrdHdr = new tblSalesOrderHdrTableAdapter();
                //ddlOrderList.DataSource = taOrdHdr.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue), Convert.ToDecimal(cboMonth.SelectedValue));
                //ddlOrderList.DataValueField = "SO_Hdr_Ref";
                //ddlOrderList.DataTextField = "SO_Hdr_Ref_No";
                //ddlOrderList.DataBind();
                //ddlOrderList.Items.Insert(0, "----------New----------");

                var taOrdHdr = new tblSalesOrderHdrTableAdapter();
                var dtOrdHdr = new dsSalesTran.tblSalesOrderHdrDataTable();
                if (cboMonth.SelectedIndex == 0)
                    dtOrdHdr = taOrdHdr.GetDataByYear(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                else
                    dtOrdHdr = taOrdHdr.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                ListItem lst;
                foreach (dsSalesTran.tblSalesOrderHdrRow dr in dtOrdHdr.Rows)
                {
                    lst = new ListItem();
                    lst.Text = dr.SO_Hdr_Ref_No.ToString() + "    [" + dr.So_Hdr_T_C1.ToString() + ", Date:" + dr.SO_Hdr_Date.ToString("dd-MM-yyyy") + "]";
                    lst.Value = dr.SO_Hdr_Ref.ToString();

                    ddlOrderList.Items.Add(lst);
                }
                ddlOrderList.Items.Insert(0, "----------New----------");

                var taSalesHdr = new tblSalesOrderHdrTableAdapter();
                var dtMaxHdrRef = taSalesHdr.GetMaxHdrRef(DateTime.Now.Year);
                var nextHdrRef = dtMaxHdrRef == null ? "000001" : (Convert.ToInt32(dtMaxHdrRef) + 1).ToString("000000");

                var dtMaxHdrRefByYr = taSalesHdr.GetMaxHdrRefByYr(DateTime.Now.Year);
                var nextHdrRefByYr = dtMaxHdrRefByYr == null ? "000001" : (Convert.ToInt32(dtMaxHdrRefByYr) + 1).ToString("000000");
                var nextHdrRefNo = "ECIL-ORD-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRefByYr).ToString("000000");
                lblOrdRefNo.Text = nextHdrRefNo;

                txtOrdDate.Text = DateTime.Now.ToString("dd/MM/yyy");
                txtOrdDelDate.Text = DateTime.Now.ToString("dd/MM/yyy");
                txtOrdValidDate.Text = DateTime.Now.AddDays(30).ToString("dd/MM/yyy");

                ddlSalePrefix.Enabled = true;
                ddlOrdTransCostBy.Enabled = true;
                txtCust.Text = "";
                txtCust.Enabled = true;
                txtSalesLoc.Text = "";
                txtSalesLoc.Enabled = true;
                txtSalesPer.Text = "";
                txtSalesPer.Enabled = true;
                txtRemarks.Text = "";

                txtOrdCrLimit.Text = "";
                txtOrdCrOutsatnd.Text = "";
                txtOrdCrLimitBal.Text = "";

                txtOrdQty.Text = "";
                txtOrdRate.Text = "";
                txtOrdAmt.Text = "";
                txtOrdFreeBag.Text = "";
                txtOrdTransRate.Text = "";
                txtOrdGrsAmt.Text = "";
                txtOrdDiscount.Text = "";
                txtOrdNetAmt.Text = "";

                ddlOrdItem.SelectedIndex = 0;
                ddlOrdTransMode.SelectedIndex = 0;

                lblTotOrdQty.Text = "0.00";
                lblTotOrdVal.Text = "0.00";
                lblOrdValText.Text = "";
                btnAddOrdDet.Enabled = true;
                btnPost.Visible = false;
                btnHold.Visible = false;
                btnPrint.Visible = false;

                var taOrd = new VIEW_SALES_ORDERTableAdapter();
                var dtOrd = new DataTable();
                dtOrd = taOrd.GetDataByAppOrdRef("");
                gvApprSoDet.DataSource = dtOrd;
                gvApprSoDet.DataBind();

                lblTotSoQty.Visible = false;
                lblTotAppQty.Visible = false;
                pnlSoAppStat.Visible = false;

                hfEditStatus.Value = "N";
                hfRefNo.Value = "0";
                hfBnsPerc.Value = "0";

                LoadInitOrdDetGridData();
                SetOrdDetGridData();
                gvOrdDet.Enabled = true;

                pnlOrdDet.Visible = false;

                btnProceed.Visible = true;
                btnClear.Visible = false;
                lnkBtnCrRpt.Visible = false;

                btnAddOrdDetYes.Visible = false;
                btnProceedYes.Visible = false;

                btnHoldOrdYes.Visible = false;
                btnPostOrdYes.Visible = false;
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Loading Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
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

        protected void btnAddOrdDet_Click(object sender, EventArgs e)
        {
            Page.Validate("btnAdd");

            if (!Page.IsValid) return;

            try
            {
                foreach (GridViewRow gr in gvOrdDet.Rows)
                {
                    var lblOrdICode = ((Label)(gr.FindControl("lblOrdICode"))).Text.ToString();
                    if (ddlOrdItem.SelectedValue.ToString() == lblOrdICode.ToString())
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = ddlOrdItem.ToString() + " already addred to order details.";
                        tblMsg.Rows[1].Cells[0].InnerText = "To add more you need to delete existing same item details.";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                if (txtOrdNetAmt.Text.Trim() == "" || txtOrdNetAmt.Text.Trim().Length <= 0 || Convert.ToDouble(txtOrdNetAmt.Text.Trim()) <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter valid data.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                #region Credit Limit Validiation
                var custRef = "";
                var custAccCode = "";
                var srchWords = txtCust.Text.Trim().Split(':');
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

                        var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;
                        custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                        var taSalesOrder = new View_Sales_Ord_NewTableAdapter();
                        var dtSoBalAmtSum = taSalesOrder.GetSoBalAmt(custRef.ToString());
                        var soBalAmtSum = dtSoBalAmtSum == null ? 0 : Convert.ToDouble(dtSoBalAmtSum);
                        var taSalesOrderDo = new View_Delivery_Ord_NewTableAdapter();
                        var dtDoBalAmtSum = taSalesOrderDo.GetDoBalAmt(custRef.ToString());
                        var doBalAmtSum = dtDoBalAmtSum == null ? 0 : Convert.ToDouble(dtDoBalAmtSum);

                        var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                        var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                        var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                        var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                        var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                        var custCrOutsatnd = soBalAmtSum + doBalAmtSum + (drAmt - crAmt);
                        var custCrLimitBal = (Convert.ToDouble(crLimit) - (soBalAmtSum + doBalAmtSum + (drAmt - crAmt)));

                        foreach (GridViewRow gr in gvOrdDet.Rows)
                        {
                            var lblOrdAmt = ((Label)(gr.FindControl("lblOrdNetAmt"))).Text.ToString();
                            ordTotAmtChk += Convert.ToDouble(lblOrdAmt.Trim());
                        }

                        var orderQty = Convert.ToDouble(txtOrdQty.Text.Trim().Length > 0 ? txtOrdQty.Text.Trim() : "0");
                        var orderRate = Convert.ToDouble(txtOrdRate.Text.Trim().Length > 0 ? txtOrdRate.Text.Trim() : "0");
                        var orderAmt = Convert.ToDouble(orderQty * orderRate);

                        if ((ordTotAmtChk + orderAmt) > (custCrLimitBal))
                        {
                            //tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to exceed available credit limit : " + (Convert.ToDouble(crLimit) - (drAmt - crAmt)).ToString("N2");
                            //tblMsg.Rows[1].Cells[0].InnerText = "";
                            //ModalPopupExtenderMsg.Show();
                            //return;
                            btnProceedYes.Visible = false;
                            btnAddOrdDetYes.Visible = true;
                            ModalPopupExtender5.Show();
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
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                var dt = new DataTable();
                dt = (DataTable)ViewState["dtOrdDet"];

                var ORD_HDR_REF = "0";
                var ORD_DET_REF = "0";
                var ORD_DET_REF_NO = "0";
                var ORD_ITEM_REF = ddlOrdItem.SelectedValue.ToString();
                var ORD_ITEM_NAME = ddlOrdItem.SelectedItem.ToString();
                var ORD_ITEM_UOM_REF = ddlOrdItemUom.SelectedValue.ToString();
                var ORD_ITEM_UOM = ddlOrdItemUom.SelectedItem.ToString();
                var ORD_QTY = Convert.ToDouble(txtOrdQty.Text.Trim().Length > 0 ? txtOrdQty.Text.Trim() : "0");
                var ORD_RATE = Convert.ToDouble(txtOrdRate.Text.Trim().Length > 0 ? txtOrdRate.Text.Trim() : "0");
                var ORD_AMOUNT = (ORD_QTY * ORD_RATE).ToString("N2");
                var ORD_FREE_BAG = Convert.ToDouble(txtOrdFreeBag.Text.Trim().Length > 0 ? txtOrdFreeBag.Text.Trim() : "0");
                var ORD_TRANS_RATE = Convert.ToDouble(txtOrdTransRate.Text.Trim().Length > 0 ? txtOrdTransRate.Text.Trim() : "0");
                var ORD_GRS_AMOUNT = ((ORD_QTY * ORD_RATE) + (ORD_QTY * ORD_TRANS_RATE)).ToString("N2");
                var ORD_DISCOUNT = Convert.ToDouble(txtOrdDiscount.Text.Trim().Length > 0 ? txtOrdDiscount.Text.Trim() : "0");
                var ORD_NET_AMOUNT = (((ORD_QTY * ORD_RATE) + (ORD_QTY * ORD_TRANS_RATE)) - ORD_DISCOUNT).ToString("N2");

                dt.Rows.Add(ORD_HDR_REF, ORD_DET_REF, ORD_DET_REF_NO, ORD_ITEM_REF, ORD_ITEM_NAME, ORD_ITEM_UOM_REF, ORD_ITEM_UOM, ORD_QTY, ORD_RATE.ToString("N2"), ORD_AMOUNT,
                    ORD_FREE_BAG.ToString("N2"), ORD_TRANS_RATE.ToString("N2"), ORD_GRS_AMOUNT, ORD_DISCOUNT.ToString("N2"), ORD_NET_AMOUNT);

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
                    lblTotOrdQty.Text =  ordTotQty.ToString("N2");
                    lblTotOrdVal.Text = ordTotAmt.ToString("N2");
                    lblOrdValText.Text = "= " + numToEng.changeNumericToWords(ordTotAmt) + " Only =";
                    lblTotOrdQty.Visible = true;
                    lblTotOrdVal.Visible = true;
                    btnPost.Visible = true;
                    btnHold.Visible = true;
                    //btnPrint.Visible = true;
                    btnPost.Enabled = true;
                    btnHold.Enabled = true;
                }
                else
                {
                    lblTotOrdQty.Text = "0.00";
                    lblTotOrdVal.Text = "0.00";
                    lblOrdValText.Text = "";
                    lblTotOrdQty.Visible = false;
                    lblTotOrdVal.Visible = false;
                    btnPost.Visible = false;
                    btnHold.Visible = false;
                    //btnPrint.Visible = false;
                    btnPost.Enabled = false;
                    btnHold.Enabled = false;
                }

                txtOrdQty.Text = "";
                txtOrdRate.Text = "";
                txtOrdAmt.Text = "";
                txtOrdFreeBag.Text = "";
                txtOrdTransRate.Text = "";
                txtOrdGrsAmt.Text = "";
                txtOrdDiscount.Text = "";
                txtOrdNetAmt.Text = "";
                hfBnsPerc.Value = "0";

                ddlOrdItem.SelectedIndex = 0;
                ddlOrdTransMode.SelectedIndex = 0;
            }
            catch (Exception ex) 
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void gvOrdDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var rowNum = e.RowIndex;

            if (rowNum == -1) return;

            var dt = new DataTable();
            dt = (DataTable)ViewState["dtOrdDet"];

            dt.Rows[rowNum].Delete();
            dt.AcceptChanges();            

            gvOrdDet.EditIndex = -1;
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
                lblOrdValText.Text = "= " + numToEng.changeNumericToWords(ordTotAmt) + " Only =";
                lblTotOrdQty.Visible = true;
                lblTotOrdVal.Visible = true;
                btnPost.Visible = true;
                btnHold.Visible = true;
                //btnPrint.Visible = true;
                btnPost.Enabled = true;
                btnHold.Enabled = true;
            }
            else
            {
                lblTotOrdQty.Text = "0.00";
                lblTotOrdVal.Text = "0.00";
                lblOrdValText.Text = "";
                lblTotOrdQty.Visible = false;
                lblTotOrdVal.Visible = false;
                btnPost.Visible = false;
                btnHold.Visible = false;
                //btnPrint.Visible = false;
                btnPost.Enabled = false;
                btnHold.Enabled = false;
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var custRef = "";
                var srchWords = txtCust.Text.Trim().Split(':');
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
                            args.IsValid = true;                        
                        else
                            args.IsValid = false;
                    }
                    else                    
                        args.IsValid = false;                    
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex) { args.IsValid = false; }
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (ddlOrdTransCostBy.SelectedValue == "1")
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

        protected void CustomValidator4_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var spRef = "";
                var srchWords = txtSalesPer.Text.Trim().Split(':');
                foreach (string word in srchWords)
                {
                    spRef = word;
                    break;
                }

                if (spRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(spRef, out result))
                    {
                        var taSp = new tblSalesPersonMasTableAdapter();
                        var dtSp = taSp.GetDataBySpRef(Convert.ToInt32(spRef));
                        if (dtSp.Rows.Count > 0)
                            args.IsValid = true;
                        else
                            args.IsValid = false;
                    }
                    else
                        args.IsValid = false;
                }
                else
                    args.IsValid = false;
            }
            catch (Exception ex) { args.IsValid = false; }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            var taSalesHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesDet = new tblSalesOrderDetTableAdapter();
            var taComm = new tbl_Tran_ComTableAdapter();
            var taEmp = new View_Emp_BascTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesHdr.Connection);

            try
            {
                #region Get Customer Details and Credit Limit Validiation
                var custRef = "";
                var custName = "";
                var custAccCode = "";
                var srchWords = txtCust.Text.Trim().Split(':');
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

                        custRef = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Ref.ToString() : "";
                        custName = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Name.ToString() : "";
                        custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                        var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;

                        var taSalesOrder = new View_Sales_Ord_NewTableAdapter();
                        var dtSoBalAmtSum = taSalesOrder.GetSoBalAmt(custRef.ToString());
                        var soBalAmtSum = dtSoBalAmtSum == null ? 0 : Convert.ToDouble(dtSoBalAmtSum);
                        var taSalesOrderDo = new View_Delivery_Ord_NewTableAdapter();
                        var dtDoBalAmtSum = taSalesOrderDo.GetDoBalAmt(custRef.ToString());
                        var doBalAmtSum = dtDoBalAmtSum == null ? 0 : Convert.ToDouble(dtDoBalAmtSum);

                        var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                        var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                        var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                        var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                        var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                        var custCrOutsatnd = soBalAmtSum + doBalAmtSum + (drAmt - crAmt);
                        var custCrLimitBal = (Convert.ToDouble(crLimit) - (soBalAmtSum + doBalAmtSum + (drAmt - crAmt)));

                        foreach (GridViewRow gr in gvOrdDet.Rows)
                        {
                            var lblOrdAmt = ((Label)(gr.FindControl("lblOrdNetAmt"))).Text.ToString();
                            ordTotAmtChk += Convert.ToDouble(lblOrdAmt.Trim());
                        }

                        var orderQty = Convert.ToDouble(txtOrdQty.Text.Trim().Length > 0 ? txtOrdQty.Text.Trim() : "0");
                        var orderRate = Convert.ToDouble(txtOrdRate.Text.Trim().Length > 0 ? txtOrdRate.Text.Trim() : "0");
                        var orderAmt = Convert.ToDouble(orderQty * orderRate);

                        if ((ordTotAmtChk + orderAmt) > (custCrLimitBal))
                        {
                            //tblMsg.Rows[0].Cells[0].InnerText = "You are no allowed to exceed available credit limit : " + (Convert.ToDouble(crLimit) - (drAmt - crAmt)).ToString("N2");
                            //tblMsg.Rows[1].Cells[0].InnerText = "";
                            //ModalPopupExtenderMsg.Show();
                            
                            btnHoldOrdYes.Visible = false;
                            btnPostOrdYes.Visible = true;
                            ModalPopupExtender1.Show();
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
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Customer.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                #region Get Location Details
                var locRef = "";
                var srchLocWords = txtSalesLoc.Text.Trim().Split(':');
                foreach (string word in srchLocWords)
                {
                    locRef = word;
                    break;
                }

                if (locRef.Length > 0)
                {
                    var taLoc = new tblSalesLocMasTableAdapter();
                    var dtLoc = taLoc.GetDataByLocRef(locRef);
                    if (dtLoc.Rows.Count > 0)
                        locRef = dtLoc[0].Loc_Mas_Ref;
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Location.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                #endregion

                #region Get Sales Person Details
                var spRef = "";
                var srchSpWords = txtSalesPer.Text.Trim().Split(':');
                foreach (string word in srchSpWords)
                {
                    spRef = word;
                    break;
                }

                if (spRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(spRef, out result))
                    {
                        var taSp = new tblSalesPersonMasTableAdapter();
                        var dtSp = taSp.GetDataBySpRef(Convert.ToInt32(spRef));
                        if (dtSp.Rows.Count > 0)
                            spRef = dtSp[0].Sp_Ref.ToString();
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Sales Person.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Sales Person.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Sales Person.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
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

                var nextHdrRef = DateTime.Now.Year + "000001";
                var nextHdrRefNo = "";

                taSalesHdr.AttachTransaction(myTran);
                taSalesDet.AttachTransaction(myTran);
                taComm.AttachTransaction(myTran);

                if (hfEditStatus.Value == "N")
                {
                    var dtMaxHdrRef = taSalesHdr.GetMaxHdrRef(DateTime.Now.Year);
                    nextHdrRef = dtMaxHdrRef == null ? DateTime.Now.Year + "000001" : (Convert.ToInt32(dtMaxHdrRef) + 1).ToString("000000");

                    var dtMaxHdrRefByYr = taSalesHdr.GetMaxHdrRefByYr(DateTime.Now.Year);
                    var nextHdrRefByYr = dtMaxHdrRefByYr == null ? "000001" : (Convert.ToInt32(dtMaxHdrRefByYr) + 1).ToString("000000");
                    nextHdrRefNo = "ECIL-ORD-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRefByYr).ToString("000000");

                    //--------------Forwarded to Credit Division----------------------
                    //taSalesHdr.InsertSalesHdr(nextHdrRef, "SO", "SAL", nextHdrRefNo, custRef.ToString(), custRef.ToString(), custRef.ToString(), Convert.ToDateTime(txtOrdDate.Text.Trim()),
                    //    txtOrdDelDate.Text.Trim(), ddlOrdTransCostBy.SelectedValue, locRef.ToString(), spRef.ToString(), "", "", "", "", "", "",
                    //    Convert.ToDecimal(lblTotOrdVal.Text.Trim()), "H", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), DateTime.Now,
                    //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), ddlSalePrefix.SelectedValue.ToString(),
                    //    ddlOrdTransMode.SelectedValue.ToString(), "", custName, "", "", 1, "BDT", Convert.ToDateTime(txtOrdValidDate.Text.Trim()), "2", "");

                    //--------------Directly Create Sales Order----------------------
                    taSalesHdr.InsertSalesHdr(nextHdrRef, "SO", "SAL", nextHdrRefNo, custRef.ToString(), custRef.ToString(), custRef.ToString(), Convert.ToDateTime(txtOrdDate.Text.Trim()),
                        txtOrdDelDate.Text.Trim(), ddlOrdTransCostBy.SelectedValue, locRef.ToString(), spRef.ToString(), "", "", "", txtRemarks.Text.Trim(), "", "",
                        Convert.ToDecimal(lblTotOrdVal.Text.Trim()), "P", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), ddlSalePrefix.SelectedValue.ToString(),
                        ddlOrdTransMode.SelectedValue.ToString(), "", custName, "", "", 1, "BDT", Convert.ToDateTime(txtOrdValidDate.Text.Trim()), "1", "");

                    var dtMaxComSeqNo = taComm.GetMaxComSeqNo(nextHdrRefNo);
                    var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                    taComm.InsertTranCom(nextHdrRefNo, nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "INI", "(Order Created By: " + empName + ") " + txtRemarks.Text.Trim(), "", "1", "", "", "", "");

                    #region Save Order Details
                    short ordDetLno = 1;
                    var dtOrdDet = new DataTable();
                    dtOrdDet = (DataTable)ViewState["dtOrdDet"];
                    foreach (DataRow row in dtOrdDet.Rows)
                    {
                        var ORD_HDR_REF = row["ORD_HDR_REF"].ToString();
                        var ORD_DET_REF = row["ORD_DET_REF"].ToString();
                        var ORD_ITEM_REF = row["ORD_ITEM_REF"].ToString();
                        var ORD_ITEM_NAME = row["ORD_ITEM_NAME"].ToString();
                        var ORD_ITEM_UOM_REF = row["ORD_ITEM_UOM_REF"].ToString();
                        var ORD_ITEM_UOM = row["ORD_ITEM_UOM"].ToString();
                        var ORD_QTY = Convert.ToDouble(row["ORD_QTY"].ToString());
                        var ORD_RATE = Convert.ToDecimal(row["ORD_RATE"].ToString());
                        var ORD_AMOUNT = Convert.ToDecimal(row["ORD_AMOUNT"].ToString());
                        var ORD_FREE_BAG = Convert.ToDouble(row["ORD_FREE_BAG"].ToString().Length > 0 ? row["ORD_FREE_BAG"].ToString() : "0");
                        var ORD_TRANS_RATE = Convert.ToDecimal(row["ORD_TRANS_RATE"].ToString());
                        var ORD_GRS_AMOUNT = Convert.ToDecimal(row["ORD_GRS_AMOUNT"].ToString());
                        var ORD_DISCOUNT = Convert.ToDecimal(row["ORD_DISCOUNT"].ToString());
                        var ORD_NET_AMOUNT = Convert.ToDecimal(row["ORD_NET_AMOUNT"].ToString());

                        //--------------Forwarded to Credit Division----------------------
                        //taSalesDet.InsertSalesDet(nextHdrRef.ToString(), "SO", "SAL", nextHdrRefNo, ordDetLno, "", 0, ORD_ITEM_REF, ORD_ITEM_NAME, ORD_ITEM_UOM_REF, "", "", "", 0, "",
                        //    Convert.ToDateTime(txtOrdDelDate.Text.Trim()), Convert.ToDateTime(txtOrdDelDate.Text.Trim()), ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, "", "",
                        //    ORD_TRANS_RATE, ORD_RATE, ORD_GRS_AMOUNT, ORD_DISCOUNT, 0, 0, ORD_NET_AMOUNT, 0, (ORD_QTY + ORD_FREE_BAG), "0", ORD_FREE_BAG.ToString(), "", "", "",
                        //    ORD_QTY.ToString(), ORD_FREE_BAG.ToString(), "", 1, "2", "H");

                        //--------------Directly Create Sales Order----------------------
                        taSalesDet.InsertSalesDet(nextHdrRef.ToString(), "SO", "SAL", nextHdrRefNo, ordDetLno, "", 0, ORD_ITEM_REF, ORD_ITEM_NAME, ORD_ITEM_UOM_REF, "", "", "", 0, "",
                            Convert.ToDateTime(txtOrdDelDate.Text.Trim()), Convert.ToDateTime(txtOrdDelDate.Text.Trim()), ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, "", "",
                            ORD_TRANS_RATE, ORD_RATE, ORD_GRS_AMOUNT, ORD_DISCOUNT, 0, 0, ORD_NET_AMOUNT, 0, (ORD_QTY + ORD_FREE_BAG), "0", ORD_FREE_BAG.ToString(), "", "", "",
                            ORD_QTY.ToString(), ORD_FREE_BAG.ToString(), "", 1, "1", "P");

                        ordDetLno++;
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Posted Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnHold.Enabled = false;
                    btnPost.Enabled = false;
                    btnPrint.Visible = true;
                }
                else
                {
                    nextHdrRef = ddlOrderList.SelectedValue.ToString();
                    nextHdrRefNo = lblOrdRefNo.Text.Trim();

                    //--------------Forwarded to Credit Division----------------------
                    //taSalesHdr.UpdateSalesHdr(custRef.ToString(), custRef.ToString(), custRef.ToString(), Convert.ToDateTime(txtOrdDate.Text.Trim()),
                    //    txtOrdDelDate.Text.Trim(), ddlOrdTransCostBy.SelectedValue, locRef.ToString(), spRef.ToString(), "", "", "", "", "", "",
                    //    Convert.ToDecimal(lblTotOrdVal.Text.Trim()), "H", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), DateTime.Now,
                    //    Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), ddlSalePrefix.SelectedValue.ToString(),
                    //    ddlOrdTransMode.SelectedValue.ToString(), "", custName, "", "", 1, "BDT", Convert.ToDateTime(txtOrdValidDate.Text.Trim()), "2", "", nextHdrRef);

                    //--------------Directly Create Sales Order----------------------
                    taSalesHdr.UpdateSalesHdr(custRef.ToString(), custRef.ToString(), custRef.ToString(), Convert.ToDateTime(txtOrdDate.Text.Trim()),
                          txtOrdDelDate.Text.Trim(), ddlOrdTransCostBy.SelectedValue, locRef.ToString(), spRef.ToString(), "", "", "", txtRemarks.Text.Trim(), "", "",
                          Convert.ToDecimal(lblTotOrdVal.Text.Trim()), "P", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), DateTime.Now,
                          Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), ddlSalePrefix.SelectedValue.ToString(),
                          ddlOrdTransMode.SelectedValue.ToString(), "", custName, "", "", 1, "BDT", Convert.ToDateTime(txtOrdValidDate.Text.Trim()), "1", "", nextHdrRef);

                    var dtMaxComSeqNo = taComm.GetMaxComSeqNo(nextHdrRefNo);
                    var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                    taComm.InsertTranCom(nextHdrRefNo, nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "INI", "(Order Created By: " + empName + ") " + txtRemarks.Text.Trim(), "", "1", "", "", "", "");

                    taSalesDet.DeleteSalesDet(ddlOrderList.SelectedValue.ToString());

                    #region Save Order Details
                    short ordDetLno = 1;
                    var dtOrdDet = new DataTable();
                    dtOrdDet = (DataTable)ViewState["dtOrdDet"];
                    foreach (DataRow row in dtOrdDet.Rows)
                    {
                        var ORD_HDR_REF = row["ORD_HDR_REF"].ToString();
                        var ORD_DET_REF = row["ORD_DET_REF"].ToString();
                        var ORD_ITEM_REF = row["ORD_ITEM_REF"].ToString();
                        var ORD_ITEM_NAME = row["ORD_ITEM_NAME"].ToString();
                        var ORD_ITEM_UOM_REF = row["ORD_ITEM_UOM_REF"].ToString();
                        var ORD_ITEM_UOM = row["ORD_ITEM_UOM"].ToString();
                        var ORD_QTY = Convert.ToDouble(row["ORD_QTY"].ToString());
                        var ORD_RATE = Convert.ToDecimal(row["ORD_RATE"].ToString());
                        var ORD_AMOUNT = Convert.ToDecimal(row["ORD_AMOUNT"].ToString());
                        var ORD_FREE_BAG = Convert.ToDouble(row["ORD_FREE_BAG"].ToString().Length > 0 ? row["ORD_FREE_BAG"].ToString() : "0");
                        var ORD_TRANS_RATE = Convert.ToDecimal(row["ORD_TRANS_RATE"].ToString());
                        var ORD_GRS_AMOUNT = Convert.ToDecimal(row["ORD_GRS_AMOUNT"].ToString());
                        var ORD_DISCOUNT = Convert.ToDecimal(row["ORD_DISCOUNT"].ToString());
                        var ORD_NET_AMOUNT = Convert.ToDecimal(row["ORD_NET_AMOUNT"].ToString());

                        //--------------Forwarded to Credit Division----------------------
                        //taSalesDet.InsertSalesDet(nextHdrRef.ToString(), "SO", "SAL", nextHdrRefNo, ordDetLno, "", 0, ORD_ITEM_REF, ORD_ITEM_NAME, ORD_ITEM_UOM_REF, "", "", "", 0, "",
                        //    Convert.ToDateTime(txtOrdDelDate.Text.Trim()), Convert.ToDateTime(txtOrdDelDate.Text.Trim()), ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, "", "",
                        //    ORD_TRANS_RATE, ORD_RATE, ORD_GRS_AMOUNT, ORD_DISCOUNT, 0, 0, ORD_NET_AMOUNT, 0, (ORD_QTY + ORD_FREE_BAG), "0", ORD_FREE_BAG.ToString(), "", "", "",
                        //    ORD_QTY.ToString(), ORD_FREE_BAG.ToString(), "", 1, "2", "H");

                        //--------------Directly Create Sales Order----------------------
                        taSalesDet.InsertSalesDet(nextHdrRef.ToString(), "SO", "SAL", nextHdrRefNo, ordDetLno, "", 0, ORD_ITEM_REF, ORD_ITEM_NAME, ORD_ITEM_UOM_REF, "", "", "", 0, "",
                            Convert.ToDateTime(txtOrdDelDate.Text.Trim()), Convert.ToDateTime(txtOrdDelDate.Text.Trim()), ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, "", "",
                            ORD_TRANS_RATE, ORD_RATE, ORD_GRS_AMOUNT, ORD_DISCOUNT, 0, 0, ORD_NET_AMOUNT, 0, (ORD_QTY + ORD_FREE_BAG), "0", ORD_FREE_BAG.ToString(), "", "", "",
                            ORD_QTY.ToString(), ORD_FREE_BAG.ToString(), "", 1, "1", "P");

                        ordDetLno++;
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Posted Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnHold.Enabled = false;
                    btnPost.Enabled = false;
                    btnPrint.Visible = true;
                }

                var curYear = DateTime.Now.Year;
                cboYear.SelectedValue = curYear.ToString();

                var curMonth = DateTime.Now.Month;
                cboMonth.SelectedValue = curMonth.ToString();

                //ddlOrderList.DataSource = taSalesHdr.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue), Convert.ToDecimal(cboMonth.SelectedValue));
                //ddlOrderList.DataValueField = "SO_Hdr_Ref";
                //ddlOrderList.DataTextField = "SO_Hdr_Ref_No";
                //ddlOrderList.DataBind();
                //ddlOrderList.Items.Insert(0, "----------New----------");

                var taOrdHdr = new tblSalesOrderHdrTableAdapter();
                var dtOrdHdr = new dsSalesTran.tblSalesOrderHdrDataTable();
                if (cboMonth.SelectedIndex == 0)
                    dtOrdHdr = taOrdHdr.GetDataByYear(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                else
                    dtOrdHdr = taOrdHdr.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                ListItem lst;
                foreach (dsSalesTran.tblSalesOrderHdrRow dr in dtOrdHdr.Rows)
                {
                    lst = new ListItem();
                    lst.Text = dr.SO_Hdr_Ref_No.ToString() + "    [" + dr.So_Hdr_T_C1.ToString() + ", Date:" + dr.SO_Hdr_Date.ToString("dd-MM-yyyy") + "]";
                    lst.Value = dr.SO_Hdr_Ref.ToString();

                    ddlOrderList.Items.Add(lst);
                }
                ddlOrderList.Items.Insert(0, "----------New----------");

                ddlOrderList.SelectedIndex = ddlOrderList.Items.IndexOf(ddlOrderList.Items.FindByText(nextHdrRefNo));

                hfEditStatus.Value = "Y";
                hfRefNo.Value = "0";
                hfBnsPerc.Value = "0";
                //ClearData();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnHold_Click(object sender, EventArgs e)
        {
            var taSalesHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesDet = new tblSalesOrderDetTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesHdr.Connection);

            try
            {
                #region Get Customer Details and Credit Limit Validiation
                var custRef = "";
                var custName = "";
                var custAccCode = "";
                var srchWords = txtCust.Text.Trim().Split(':');
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

                        custRef = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Ref.ToString() : "";
                        custName = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Name.ToString() : "";
                        custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                        var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;

                        var taSalesOrder = new View_Sales_Ord_NewTableAdapter();
                        var dtSoBalAmtSum = taSalesOrder.GetSoBalAmt(custRef.ToString());
                        var soBalAmtSum = dtSoBalAmtSum == null ? 0 : Convert.ToDouble(dtSoBalAmtSum);
                        var taSalesOrderDo = new View_Delivery_Ord_NewTableAdapter();
                        var dtDoBalAmtSum = taSalesOrderDo.GetDoBalAmt(custRef.ToString());
                        var doBalAmtSum = dtDoBalAmtSum == null ? 0 : Convert.ToDouble(dtDoBalAmtSum);

                        var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                        var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                        var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                        var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                        var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                        var custCrOutsatnd = soBalAmtSum + doBalAmtSum + (drAmt - crAmt);
                        var custCrLimitBal = (Convert.ToDouble(crLimit) - (soBalAmtSum + doBalAmtSum + (drAmt - crAmt)));

                        foreach (GridViewRow gr in gvOrdDet.Rows)
                        {
                            var lblOrdAmt = ((Label)(gr.FindControl("lblOrdNetAmt"))).Text.ToString();
                            ordTotAmtChk += Convert.ToDouble(lblOrdAmt.Trim());
                        }

                        var orderQty = Convert.ToDouble(txtOrdQty.Text.Trim().Length > 0 ? txtOrdQty.Text.Trim() : "0");
                        var orderRate = Convert.ToDouble(txtOrdRate.Text.Trim().Length > 0 ? txtOrdRate.Text.Trim() : "0");
                        var orderAmt = Convert.ToDouble(orderQty * orderRate);

                        if ((ordTotAmtChk + orderAmt) > (custCrLimitBal))
                        {
                            //tblMsg.Rows[0].Cells[0].InnerText = "You are no allowed to exceed available credit limit : " + (Convert.ToDouble(crLimit) - (drAmt - crAmt)).ToString("N2");
                            //tblMsg.Rows[1].Cells[0].InnerText = "";
                            //ModalPopupExtenderMsg.Show();

                            //btnPostOrdYes.Visible = false;
                            //btnHoldOrdYes.Visible = true;
                            //ModalPopupExtender1.Show();
                            //return;
                        }
                    }
                    else
                    {
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

                #region Get Location Details
                var locRef = "";
                var srchLocWords = txtSalesLoc.Text.Trim().Split(':');
                foreach (string word in srchLocWords)
                {
                    locRef = word;
                    break;
                }

                if (locRef.Length > 0)
                {
                    var taLoc = new tblSalesLocMasTableAdapter();
                    var dtLoc = taLoc.GetDataByLocRef(locRef);
                    if (dtLoc.Rows.Count > 0)
                        locRef = dtLoc[0].Loc_Mas_Ref;
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Location.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                #endregion

                #region Get Sales Person Details
                var spRef = "";
                var srchSpWords = txtSalesPer.Text.Trim().Split(':');
                foreach (string word in srchSpWords)
                {
                    spRef = word;
                    break;
                }

                if (spRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(spRef, out result))
                    {
                        var taSp = new tblSalesPersonMasTableAdapter();
                        var dtSp = taSp.GetDataBySpRef(Convert.ToInt32(spRef));
                        if (dtSp.Rows.Count > 0)
                            spRef = dtSp[0].Sp_Ref.ToString();
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Sales Person.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Sales Person.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Sales Person.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                var nextHdrRef = "000001";
                var nextHdrRefNo = "";

                taSalesHdr.AttachTransaction(myTran);
                taSalesDet.AttachTransaction(myTran);

                if (hfEditStatus.Value == "N")
                {
                    var dtMaxHdrRef = taSalesHdr.GetMaxHdrRef(DateTime.Now.Year);
                    nextHdrRef = dtMaxHdrRef == null ? DateTime.Now.Year + "000001" : (Convert.ToInt32(dtMaxHdrRef) + 1).ToString("000000");

                    var dtMaxHdrRefByYr = taSalesHdr.GetMaxHdrRefByYr(DateTime.Now.Year);
                    var nextHdrRefByYr = dtMaxHdrRefByYr == null ? "000001" : (Convert.ToInt32(dtMaxHdrRefByYr) + 1).ToString("000000");
                    nextHdrRefNo = "ECIL-ORD-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRefByYr).ToString("000000");

                    taSalesHdr.InsertSalesHdr(nextHdrRef, "SO", "SAL", nextHdrRefNo, custRef.ToString(), custRef.ToString(), custRef.ToString(), Convert.ToDateTime(txtOrdDate.Text.Trim()),
                        txtOrdDelDate.Text.Trim(), ddlOrdTransCostBy.SelectedValue, locRef.ToString(), spRef.ToString(), "", "", "", txtRemarks.Text.Trim(), "", "",
                        Convert.ToDecimal(lblTotOrdVal.Text.Trim()), "H", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), ddlSalePrefix.SelectedValue.ToString(),
                        ddlOrdTransMode.SelectedValue.ToString(), "", custName, "", "", 1, "BDT", Convert.ToDateTime(txtOrdValidDate.Text.Trim()), "1", "");

                    #region Save Order Details
                    short ordDetLno = 1;
                    var dtOrdDet = new DataTable();
                    dtOrdDet = (DataTable)ViewState["dtOrdDet"];
                    foreach (DataRow row in dtOrdDet.Rows)
                    {
                        var ORD_HDR_REF = row["ORD_HDR_REF"].ToString();
                        var ORD_DET_REF = row["ORD_DET_REF"].ToString();
                        var ORD_ITEM_REF = row["ORD_ITEM_REF"].ToString();
                        var ORD_ITEM_NAME = row["ORD_ITEM_NAME"].ToString();
                        var ORD_ITEM_UOM_REF = row["ORD_ITEM_UOM_REF"].ToString();
                        var ORD_ITEM_UOM = row["ORD_ITEM_UOM"].ToString();
                        var ORD_QTY = Convert.ToDouble(row["ORD_QTY"].ToString());
                        var ORD_RATE = Convert.ToDecimal(row["ORD_RATE"].ToString());
                        var ORD_AMOUNT = Convert.ToDecimal(row["ORD_AMOUNT"].ToString());
                        var ORD_FREE_BAG = Convert.ToDouble(row["ORD_FREE_BAG"].ToString().Length > 0 ? row["ORD_FREE_BAG"].ToString() : "0");
                        var ORD_TRANS_RATE = Convert.ToDecimal(row["ORD_TRANS_RATE"].ToString());
                        var ORD_GRS_AMOUNT = Convert.ToDecimal(row["ORD_GRS_AMOUNT"].ToString());
                        var ORD_DISCOUNT = Convert.ToDecimal(row["ORD_DISCOUNT"].ToString());
                        var ORD_NET_AMOUNT = Convert.ToDecimal(row["ORD_NET_AMOUNT"].ToString());

                        taSalesDet.InsertSalesDet(nextHdrRef.ToString(), "SO", "SAL", nextHdrRefNo, ordDetLno, "", 0, ORD_ITEM_REF, ORD_ITEM_NAME, ORD_ITEM_UOM_REF, "", "", "", 0, "",
                            Convert.ToDateTime(txtOrdDelDate.Text.Trim()), Convert.ToDateTime(txtOrdDelDate.Text.Trim()), ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, "", "",
                            ORD_TRANS_RATE, ORD_RATE, ORD_GRS_AMOUNT, ORD_DISCOUNT, 0, 0, ORD_NET_AMOUNT, 0, (ORD_QTY + ORD_FREE_BAG), "0", ORD_FREE_BAG.ToString(), "", "", "", 
                            ORD_QTY.ToString(), ORD_FREE_BAG.ToString(), "", 1, "1", "H");

                        ordDetLno++;
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnPrint.Visible = true;
                }
                else
                {
                    nextHdrRef = ddlOrderList.SelectedValue.ToString();
                    nextHdrRefNo = lblOrdRefNo.Text.Trim();

                    taSalesHdr.UpdateSalesHdr(custRef.ToString(), custRef.ToString(), custRef.ToString(), Convert.ToDateTime(txtOrdDate.Text.Trim()),
                        txtOrdDelDate.Text.Trim(), ddlOrdTransCostBy.SelectedValue, locRef.ToString(), spRef.ToString(), "", "", "", txtRemarks.Text.Trim(), "", "",
                        Convert.ToDecimal(lblTotOrdVal.Text.Trim()), "H", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), ddlSalePrefix.SelectedValue.ToString(),
                        ddlOrdTransMode.SelectedValue.ToString(), "", custName, "", "", 1, "BDT", Convert.ToDateTime(txtOrdValidDate.Text.Trim()), "1", "", nextHdrRef);

                    taSalesDet.DeleteSalesDet(ddlOrderList.SelectedValue.ToString());

                    #region Save Order Details
                    short ordDetLno = 1;
                    var dtOrdDet = new DataTable();
                    dtOrdDet = (DataTable)ViewState["dtOrdDet"];
                    foreach (DataRow row in dtOrdDet.Rows)
                    {
                        var ORD_HDR_REF = row["ORD_HDR_REF"].ToString();
                        var ORD_DET_REF = row["ORD_DET_REF"].ToString();
                        var ORD_ITEM_REF = row["ORD_ITEM_REF"].ToString();
                        var ORD_ITEM_NAME = row["ORD_ITEM_NAME"].ToString();
                        var ORD_ITEM_UOM_REF = row["ORD_ITEM_UOM_REF"].ToString();
                        var ORD_ITEM_UOM = row["ORD_ITEM_UOM"].ToString();
                        var ORD_QTY = Convert.ToDouble(row["ORD_QTY"].ToString());
                        var ORD_RATE = Convert.ToDecimal(row["ORD_RATE"].ToString());
                        var ORD_AMOUNT = Convert.ToDecimal(row["ORD_AMOUNT"].ToString());
                        var ORD_FREE_BAG = Convert.ToDouble(row["ORD_FREE_BAG"].ToString().Length > 0 ? row["ORD_FREE_BAG"].ToString() : "0");
                        var ORD_TRANS_RATE = Convert.ToDecimal(row["ORD_TRANS_RATE"].ToString());
                        var ORD_GRS_AMOUNT = Convert.ToDecimal(row["ORD_GRS_AMOUNT"].ToString());
                        var ORD_DISCOUNT = Convert.ToDecimal(row["ORD_DISCOUNT"].ToString());
                        var ORD_NET_AMOUNT = Convert.ToDecimal(row["ORD_NET_AMOUNT"].ToString());

                        taSalesDet.InsertSalesDet(nextHdrRef.ToString(), "SO", "SAL", nextHdrRefNo, ordDetLno, "", 0, ORD_ITEM_REF, ORD_ITEM_NAME, ORD_ITEM_UOM_REF, "", "", "", 0, "",
                            Convert.ToDateTime(txtOrdDelDate.Text.Trim()), Convert.ToDateTime(txtOrdDelDate.Text.Trim()), ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, "", "",
                            ORD_TRANS_RATE, ORD_RATE, ORD_GRS_AMOUNT, ORD_DISCOUNT, 0, 0, ORD_NET_AMOUNT, 0, (ORD_QTY + ORD_FREE_BAG), "0", ORD_FREE_BAG.ToString(), "", "", "",
                            ORD_QTY.ToString(), ORD_FREE_BAG.ToString(), "", 1, "1", "H");

                        ordDetLno++;
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Saved Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnPrint.Visible = true;
                }

                var curYear = DateTime.Now.Year;
                cboYear.SelectedValue = curYear.ToString();

                var curMonth = DateTime.Now.Month;
                cboMonth.SelectedValue = curMonth.ToString();

                //ddlOrderList.DataSource = taSalesHdr.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue), Convert.ToDecimal(cboMonth.SelectedValue));
                //ddlOrderList.DataValueField = "SO_Hdr_Ref";
                //ddlOrderList.DataTextField = "SO_Hdr_Ref_No";
                //ddlOrderList.DataBind();
                //ddlOrderList.Items.Insert(0, "----------New----------");

                var taOrdHdr = new tblSalesOrderHdrTableAdapter();
                var dtOrdHdr = new dsSalesTran.tblSalesOrderHdrDataTable();
                if (cboMonth.SelectedIndex == 0)
                    dtOrdHdr = taOrdHdr.GetDataByYear(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                else
                    dtOrdHdr = taOrdHdr.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                ListItem lst;
                foreach (dsSalesTran.tblSalesOrderHdrRow dr in dtOrdHdr.Rows)
                {
                    lst = new ListItem();
                    lst.Text = dr.SO_Hdr_Ref_No.ToString() + "    [" + dr.So_Hdr_T_C1.ToString() + ", Date:" + dr.SO_Hdr_Date.ToString("dd-MM-yyyy") + "]";
                    lst.Value = dr.SO_Hdr_Ref.ToString();

                    ddlOrderList.Items.Add(lst);
                }
                ddlOrderList.Items.Insert(0, "----------New----------");

                ddlOrderList.SelectedIndex = ddlOrderList.Items.IndexOf(ddlOrderList.Items.FindByText(nextHdrRefNo));

                hfEditStatus.Value = "Y";
                hfRefNo.Value = "0";
                hfBnsPerc.Value = "0";
                //ClearData();
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void ddlOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taSalesHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesDet = new tblSalesOrderDetTableAdapter();
            try
            {
                reportInfo();

                if (ddlOrderList.SelectedIndex == 0)
                {
                    ClearData();
                }
                else
                {
                    var dtSalesHdr = taSalesHdr.GetDataByHdrRef(ddlOrderList.SelectedValue.ToString());
                    if (dtSalesHdr.Rows.Count > 0)
                    {
                        ddlOrdItemUom.SelectedIndex = 0;
                        txtOrdQty.Text = "";
                        txtOrdRate.Text = "";
                        txtOrdAmt.Text = "";
                        txtOrdFreeBag.Text = "";
                        txtOrdTransRate.Text = "";
                        txtOrdGrsAmt.Text = "";
                        txtOrdDiscount.Text = "";
                        txtOrdNetAmt.Text = "";

                        hfEditStatus.Value = "Y";

                        lblOrdRefNo.Text = dtSalesHdr[0].SO_Hdr_Ref_No.ToString();
                        txtOrdDate.Text = dtSalesHdr[0].SO_Hdr_Date.ToString("dd/MM/yyyy");
                        txtOrdDelDate.Text = Convert.ToDateTime(dtSalesHdr[0].SO_Hdr_Com1).ToString("dd/MM/yyyy");
                        txtOrdValidDate.Text = dtSalesHdr[0].SO_Hdr_Exc_Date.ToString("dd/MM/yyyy");

                        ddlSalePrefix.SelectedValue = dtSalesHdr[0].SO_Hdr_Sale_Type_Ref.ToString();
                        ddlOrdTransCostBy.SelectedValue = dtSalesHdr[0].SO_Hdr_Com2.ToString();
                        ddlOrdTransMode.SelectedValue = dtSalesHdr[0].SO_Hdr_Exp_Type.ToString();

                        var taPartyAdr = new tblSalesPartyAdrTableAdapter();
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(dtSalesHdr[0].SO_Hdr_Pcode.ToString()));
                        if (dtPartyAdr.Rows.Count > 0)
                            txtCust.Text = dtPartyAdr[0].Par_Adr_Ref.ToString() + ":" + dtPartyAdr[0].Par_Adr_Ref_No.ToString() + ":" + dtPartyAdr[0].Par_Adr_Name.ToString();
                        else
                            txtCust.Text = "";

                        var taSalesLoc = new tblSalesLocMasTableAdapter();
                        var dtSalesLoc = taSalesLoc.GetDataByLocRef(dtSalesHdr[0].SO_Hdr_Com3.ToString());
                        if (dtSalesLoc.Rows.Count > 0)
                            txtSalesLoc.Text = dtSalesLoc[0].Loc_Mas_Ref.ToString() + ":" + dtSalesLoc[0].Loc_Mas_Code.ToString() + ":" + dtSalesLoc[0].Loc_Mas_Name.ToString();
                        else
                            txtSalesLoc.Text = "";

                        var taSalesPer = new tblSalesPersonMasTableAdapter();
                        var dtSalesPer = taSalesPer.GetDataBySpRef(Convert.ToInt32(dtSalesHdr[0].SO_Hdr_Com4 == "" ? "0" : dtSalesHdr[0].SO_Hdr_Com4.ToString()));
                        if (dtSalesPer.Rows.Count > 0)
                            txtSalesPer.Text = dtSalesPer[0].Sp_Ref.ToString() + ":" + dtSalesPer[0].Sp_Short_Name.ToString() + ":" + dtSalesPer[0].Sp_Full_Name.ToString();
                        else
                            txtSalesPer.Text = "";

                        txtRemarks.Text = dtSalesHdr[0].SO_Hdr_Com8.ToString().Trim();

                        LoadInitOrdDetGridData();
                        SetOrdDetGridData();
                        var dt = new DataTable();
                        dt = (DataTable)ViewState["dtOrdDet"];
                        var dtSalesDet = taSalesDet.GetDataByHdrRef(ddlOrderList.SelectedValue.ToString());
                        foreach (dsSalesTran.tblSalesOrderDetRow dr in dtSalesDet.Rows)
                        {
                            dt.Rows.Add(dr.SO_Hdr_Ref, 0, dr.SO_Det_Ref_No, dr.SO_Det_Icode, dr.SO_Det_Itm_Desc, dr.SO_Det_Itm_Uom, dr.SO_Det_Itm_Uom,
                                Convert.ToDecimal(dr.SO_Det_T_C1).ToString("N2"), dr.SO_Det_Lin_Rat.ToString("N2"), 
                                (Convert.ToDecimal(dr.SO_Det_T_C1) *  dr.SO_Det_Lin_Rat).ToString("N2"),
                                Convert.ToDecimal(dr.SO_Det_T_C2).ToString("N2"), dr.SO_Det_Trans_Rat.ToString("N2"), 
                               ((Convert.ToDecimal(dr.SO_Det_T_C1) *  dr.SO_Det_Lin_Rat) + (Convert.ToDecimal(dr.SO_Det_T_C1) *  dr.SO_Det_Trans_Rat)).ToString("N2"),
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
                            lblOrdValText.Text = "= " + numToEng.changeNumericToWords(ordTotAmt) + " Only =";
                            lblTotOrdQty.Visible = true;
                            lblTotOrdVal.Visible = true;
                            btnPost.Visible = true;
                            btnHold.Visible = true;
                            btnPrint.Visible = true;
                        }
                        else
                        {
                            lblTotOrdQty.Text = "0.00";
                            lblTotOrdVal.Text = "0.00";
                            lblOrdValText.Text = "";
                            lblTotOrdQty.Visible = false;
                            lblTotOrdVal.Visible = false;
                            btnPost.Visible = false;
                            btnHold.Visible = false;
                            btnPrint.Visible = false;
                        }

                        if (dtSalesHdr[0].SO_Hdr_HPC_Flag == "P" || (dtSalesHdr[0].SO_Hdr_HPC_Flag == "H" && dtSalesHdr[0].SO_Hdr_Status == "2"))
                        {
                            btnHold.Enabled = false;
                            btnPost.Enabled = false;
                            gvOrdDet.Enabled = false;
                            btnAddOrdDet.Enabled = false;
                            btnProceed.Visible = false;
                            btnClear.Visible = true;
                        }
                        pnlOrdDet.Visible = true;

                        var taOrd = new VIEW_SALES_ORDERTableAdapter();
                        var dtOrd = new DataTable();
                        dtOrd = taOrd.GetDataByAppOrdRef(ddlOrderList.SelectedValue.ToString());
                        gvApprSoDet.DataSource = dtOrd;
                        gvApprSoDet.DataBind();

                        foreach (GridViewRow gr in gvApprSoDet.Rows)
                        {
                            //var lblOrdLinQty = ((Label)(gr.FindControl("lblOrdLinQty"))).Text.ToString();
                            //ordLinQty += Convert.ToDouble(lblOrdLinQty.Trim());

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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            reportInfo();
            var url = "frmShowSalesReport.aspx";
            Response.Write("<script>var w=window.open('" + url + "'); w.focus();</script>");
        }

        protected void reportInfo()
        {
            rptSelcFormula = "{View_Sales_Details.SO_Hdr_Ref}='" + ddlOrderList.SelectedValue + "'";

            rptFile = "~/Module/Sales/Reports/rptSalesOrd.rpt";

            Session["RptDateFrom"] = DateTime.Now.ToString("dd/MM/yyyy");
            Session["RptDateTo"] = DateTime.Now.ToString("dd/MM/yyyy");
            Session["RptFilePath"] = rptFile;
            Session["RptFormula"] = rptSelcFormula;
        }

        protected void ddlOrdItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            var taItem = new tbl_InMa_Item_DetTableAdapter();
            var taPartyAdr = new tblSalesPartyAdrTableAdapter();
            var taPrice = new tblSalesPriceCustTableAdapter();

            if (ddlOrdItem.SelectedIndex == 0)
            {
                ddlOrdItemUom.SelectedIndex = 0;
                txtOrdQty.Text = "";
                txtOrdRate.Text = "";
                txtOrdAmt.Text = "";
                txtOrdFreeBag.Text = "";
                txtOrdTransRate.Text = "";
                txtOrdGrsAmt.Text = "";
                txtOrdDiscount.Text = "";
                txtOrdNetAmt.Text = "";
                return;
            }

            try
            {
                txtOrdRate.Text = "";
                hfBnsPerc.Value = "0";

                txtOrdQty.Text = "";
                txtOrdFreeBag.Text = "";

                var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(ddlOrdItem.SelectedValue.ToString()));
                if (dtItem.Rows.Count > 0)
                    ddlOrdItemUom.SelectedValue = dtItem[0].IsItm_Det_Stk_UnitNull() ? "0" : dtItem[0].Itm_Det_Stk_Unit.ToString();
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Item.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                try
                {
                    var locRef = "";
                    var srchLocWords = txtSalesLoc.Text.Trim().Split(':');
                    foreach (string word in srchLocWords)
                    {
                        locRef = word;
                        break;
                    }

                    if (locRef.Length > 0)
                    {
                        var taSalesLoc = new tblSalesLocMasTableAdapter();
                        var dtSalesLoc = taSalesLoc.GetDataByLocRef(locRef);
                        txtOrdTransRate.Text = dtSalesLoc.Rows.Count > 0 ? dtSalesLoc[0].Loc_Mas_Itm_Rate.ToString("N2") : "0.00";
                    }
                    else
                    {
                        txtOrdTransRate.Text = "0.00";
                    }
                }
                catch (Exception ex)
                {
                    txtOrdTransRate.Text = "0.00";
                }

                var custRef = "";
                var srchWords = txtCust.Text.Trim().Split(':');
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
                        var dtPartyAdr = taPartyAdr.GetDataByPartyAdrRef(Convert.ToInt32(custRef));
                        if (dtPartyAdr.Rows.Count > 0)
                        {                            
                            var dtPrice = taPrice.GetDataByPartyRef(dtPartyAdr[0].Par_Adr_Ref.ToString(), ddlOrdItem.SelectedValue.ToString());
                            if (dtPrice.Rows.Count > 0)
                            {
                                txtOrdRate.Text = dtPrice[0].IsPar_Adr_PriceNull() ? "" : dtPrice[0].Par_Adr_Price.ToString("N2");
                                var bnsPerc = dtPrice[0].IsPar_Adr_BonusNull() ? "0" : dtPrice[0].Par_Adr_Bonus.ToString("N2");
                                hfBnsPerc.Value = bnsPerc;
                                txtOrdFreeBag.Enabled = false;
                                //txtOrdRate.Enabled = false;
                            }
                            else
                            {                                
                                //var dtItem = taItem.GetDataByItemRef(Convert.ToInt32(itemCode.ToString()));
                                //if (dtItem.Rows.Count > 0)
                                //    itmPrice = dtItem[0].IsItm_Det_Ext_Data1Null() ? "0" : dtItem[0].Itm_Det_Ext_Data1.ToString();
                                txtOrdRate.Text = "";
                                hfBnsPerc.Value = "0";
                                txtOrdFreeBag.Enabled = true;
                                //txtOrdRate.Enabled = true;
                            }
                        }
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
                    var taSalesLoc = new tblSalesLocMasTableAdapter();
                    var dtSalesLoc = taSalesLoc.GetDataByLocRef(locRef);
                    txtOrdTransRate.Text = dtSalesLoc.Rows.Count > 0 ? dtSalesLoc[0].Loc_Mas_Itm_Rate.ToString("N2") : "0.00";
                }
                else
                {
                    txtOrdTransRate.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                txtOrdTransRate.Text = "0.00";
            }
        }

        protected void ddlOrdTransCostBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOrdTransCostBy.SelectedValue == "1")
            {
                txtSalesLoc.Text = "";
                txtSalesLoc.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
                txtSalesLoc.Enabled = false;
            }
            else
            {
                txtSalesLoc.Text = "";
                txtSalesLoc.BackColor = System.Drawing.Color.White;
                txtSalesLoc.Enabled = true;
            }
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_SO_List();
        }

        protected void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Get_SO_List();
        }

        protected void btnProceedYes_Click(object sender, EventArgs e)
        {
            ddlSalePrefix.Enabled = false;
            ddlOrdTransCostBy.Enabled = false;
            txtCust.Enabled = false;
            txtSalesLoc.Enabled = false;
            txtSalesPer.Enabled = false;

            btnProceed.Visible = false;
            btnClear.Visible = true;

            pnlOrdDet.Visible = true;
        }

        protected void btnAddOrdDetYes_Click(object sender, EventArgs e)
        {
            Page.Validate("btnAdd");

            if (!Page.IsValid) return;

            try
            {
                foreach (GridViewRow gr in gvOrdDet.Rows)
                {
                    var lblOrdICode = ((Label)(gr.FindControl("lblOrdICode"))).Text.ToString();
                    if (ddlOrdItem.SelectedValue.ToString() == lblOrdICode.ToString())
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = ddlOrdItem.ToString() + " already addred to order details.";
                        tblMsg.Rows[1].Cells[0].InnerText = "To add more you need to delete existing same item details.";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }

                if (txtOrdNetAmt.Text.Trim() == "" || txtOrdNetAmt.Text.Trim().Length <= 0 || Convert.ToDouble(txtOrdNetAmt.Text.Trim()) <= 0)
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Enter valid data.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }

                #region Credit Limit Validiation
                /*
                var custRef = "";
                var custAccCode = "";
                var srchWords = txtCust.Text.Trim().Split(':');
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
                        var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;
                        custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                        var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                        var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                        var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                        var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                        var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                        foreach (GridViewRow gr in gvOrdDet.Rows)
                        {
                            var lblOrdAmt = ((Label)(gr.FindControl("lblOrdNetAmt"))).Text.ToString();
                            ordTotAmtChk += Convert.ToDouble(lblOrdAmt.Trim());
                        }

                        var orderQty = Convert.ToDouble(txtOrdQty.Text.Trim().Length > 0 ? txtOrdQty.Text.Trim() : "0");
                        var orderRate = Convert.ToDouble(txtOrdRate.Text.Trim().Length > 0 ? txtOrdRate.Text.Trim() : "0");
                        var orderAmt = Convert.ToDouble(orderQty * orderRate);

                        if ((ordTotAmtChk + orderAmt) > (Convert.ToDouble(crLimit) - (drAmt - crAmt)))
                        {
                            //tblMsg.Rows[0].Cells[0].InnerText = "You are not allowed to exceed available credit limit : " + (Convert.ToDouble(crLimit) - (drAmt - crAmt)).ToString("N2");
                            //tblMsg.Rows[1].Cells[0].InnerText = "";
                            //ModalPopupExtenderMsg.Show();
                            //return;
                            btnProceedYes.Visible = false;
                            btnAddOrdDet.Visible = true;
                            ModalPopupExtender5.Show();
                        }
                    }
                    else
                    {
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
                }*/
                #endregion

                var dt = new DataTable();
                dt = (DataTable)ViewState["dtOrdDet"];

                var ORD_HDR_REF = "0";
                var ORD_DET_REF = "0";
                var ORD_DET_REF_NO = "0";
                var ORD_ITEM_REF = ddlOrdItem.SelectedValue.ToString();
                var ORD_ITEM_NAME = ddlOrdItem.SelectedItem.ToString();
                var ORD_ITEM_UOM_REF = ddlOrdItemUom.SelectedValue.ToString();
                var ORD_ITEM_UOM = ddlOrdItemUom.SelectedItem.ToString();
                var ORD_QTY = Convert.ToDouble(txtOrdQty.Text.Trim().Length > 0 ? txtOrdQty.Text.Trim() : "0");
                var ORD_RATE = Convert.ToDouble(txtOrdRate.Text.Trim().Length > 0 ? txtOrdRate.Text.Trim() : "0");
                var ORD_AMOUNT = (ORD_QTY * ORD_RATE).ToString("N2");
                var ORD_FREE_BAG = Convert.ToDouble(txtOrdFreeBag.Text.Trim().Length > 0 ? txtOrdFreeBag.Text.Trim() : "0");
                var ORD_TRANS_RATE = Convert.ToDouble(txtOrdTransRate.Text.Trim().Length > 0 ? txtOrdTransRate.Text.Trim() : "0");
                var ORD_GRS_AMOUNT = ((ORD_QTY * ORD_RATE) + (ORD_QTY * ORD_TRANS_RATE)).ToString("N2");
                var ORD_DISCOUNT = Convert.ToDouble(txtOrdDiscount.Text.Trim().Length > 0 ? txtOrdDiscount.Text.Trim() : "0");
                var ORD_NET_AMOUNT = (((ORD_QTY * ORD_RATE) + (ORD_QTY * ORD_TRANS_RATE)) - ORD_DISCOUNT).ToString("N2");

                dt.Rows.Add(ORD_HDR_REF, ORD_DET_REF, ORD_DET_REF_NO, ORD_ITEM_REF, ORD_ITEM_NAME, ORD_ITEM_UOM_REF, ORD_ITEM_UOM, ORD_QTY, ORD_RATE.ToString("N2"), ORD_AMOUNT,
                    ORD_FREE_BAG.ToString("N2"), ORD_TRANS_RATE.ToString("N2"), ORD_GRS_AMOUNT, ORD_DISCOUNT.ToString("N2"), ORD_NET_AMOUNT);

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
                    lblOrdValText.Text = "= " + numToEng.changeNumericToWords(ordTotAmt) + " Only =";
                    lblTotOrdQty.Visible = true;
                    lblTotOrdVal.Visible = true;
                    btnPost.Visible = true;
                    btnHold.Visible = true;
                    //btnPrint.Visible = true;
                    btnPost.Enabled = true;
                    btnHold.Enabled = true;
                }
                else
                {
                    lblTotOrdQty.Text = "0.00";
                    lblTotOrdVal.Text = "0.00";
                    lblOrdValText.Text = "";
                    lblTotOrdQty.Visible = false;
                    lblTotOrdVal.Visible = false;
                    btnPost.Visible = false;
                    btnHold.Visible = false;
                    //btnPrint.Visible = false;
                    btnPost.Enabled = false;
                    btnHold.Enabled = false;
                }

                txtOrdQty.Text = "";
                txtOrdRate.Text = "";
                txtOrdAmt.Text = "";
                txtOrdFreeBag.Text = "";
                txtOrdTransRate.Text = "";
                txtOrdGrsAmt.Text = "";
                txtOrdDiscount.Text = "";
                txtOrdNetAmt.Text = "";
                hfBnsPerc.Value = "0";

                ddlOrdItem.SelectedIndex = 0;
                ddlOrdTransMode.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error." + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
                return;
            }
        }

        protected void btnHoldOrdYes_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnPostOrdYes_Click(object sender, EventArgs e)
        {
            var taSalesHdr = new tblSalesOrderHdrTableAdapter();
            var taSalesDet = new tblSalesOrderDetTableAdapter();
            var taComm = new tbl_Tran_ComTableAdapter();
            var taEmp = new View_Emp_BascTableAdapter();

            SqlTransaction myTran = GlobalClass.clsDbHelper.OpenTransaction(taSalesHdr.Connection);

            try
            {
                #region Get Customer Details and Credit Limit Validiation
                var custRef = "";
                var custName = "";
                var custAccCode = "";
                double custCrLimitBal = 0;
                var srchWords = txtCust.Text.Trim().Split(':');
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

                        custRef = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Ref.ToString() : "";
                        custName = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Name.ToString() : "";
                        custAccCode = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Acc_Code.ToString() : "";

                        var crLimit = dtPartyAdr.Rows.Count > 0 ? dtPartyAdr[0].Par_Adr_Cr_Limit : 0;

                        var taSalesOrder = new View_Sales_Ord_NewTableAdapter();
                        var dtSoBalAmtSum = taSalesOrder.GetSoBalAmt(custRef.ToString());
                        var soBalAmtSum = dtSoBalAmtSum == null ? 0 : Convert.ToDouble(dtSoBalAmtSum);
                        var taSalesOrderDo = new View_Delivery_Ord_NewTableAdapter();
                        var dtDoBalAmtSum = taSalesOrderDo.GetDoBalAmt(custRef.ToString());
                        var doBalAmtSum = dtDoBalAmtSum == null ? 0 : Convert.ToDouble(dtDoBalAmtSum);

                        var taFaTe = new tbl_Acc_Fa_TeTableAdapter();
                        var dtCrSum = taFaTe.GetTotCrAmt(custAccCode.ToString());
                        var crAmt = dtCrSum == null ? 0 : Convert.ToDouble(dtCrSum);
                        var dtDrSum = taFaTe.GetTotDrAmt(custAccCode.ToString());
                        var drAmt = dtDrSum == null ? 0 : Convert.ToDouble(dtDrSum);

                        var custCrOutsatnd = soBalAmtSum + doBalAmtSum + (drAmt - crAmt);
                        custCrLimitBal = (Convert.ToDouble(crLimit) - (soBalAmtSum + doBalAmtSum + (drAmt - crAmt)));

                        foreach (GridViewRow gr in gvOrdDet.Rows)
                        {
                            var lblOrdAmt = ((Label)(gr.FindControl("lblOrdNetAmt"))).Text.ToString();
                            ordTotAmtChk += Convert.ToDouble(lblOrdAmt.Trim());
                        }

                        var orderQty = Convert.ToDouble(txtOrdQty.Text.Trim().Length > 0 ? txtOrdQty.Text.Trim() : "0");
                        var orderRate = Convert.ToDouble(txtOrdRate.Text.Trim().Length > 0 ? txtOrdRate.Text.Trim() : "0");
                        var orderAmt = Convert.ToDouble(orderQty * orderRate);

                        if ((ordTotAmtChk + orderAmt) > (custCrLimitBal))
                        {
                            //tblMsg.Rows[0].Cells[0].InnerText = "You are no allowed to exceed available credit limit : " + (Convert.ToDouble(crLimit) - (drAmt - crAmt)).ToString("N2");
                            //tblMsg.Rows[1].Cells[0].InnerText = "";
                            //ModalPopupExtenderMsg.Show();

                            //btnHoldOrdYes.Visible = false;
                            //btnPostOrdYes.Visible = true;
                            //ModalPopupExtender1.Show();
                            //return;
                        }
                    }
                    else
                    {
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

                #region Get Location Details
                var locRef = "";
                var srchLocWords = txtSalesLoc.Text.Trim().Split(':');
                foreach (string word in srchLocWords)
                {
                    locRef = word;
                    break;
                }

                if (locRef.Length > 0)
                {
                    var taLoc = new tblSalesLocMasTableAdapter();
                    var dtLoc = taLoc.GetDataByLocRef(locRef);
                    if (dtLoc.Rows.Count > 0)
                        locRef = dtLoc[0].Loc_Mas_Ref;
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Location.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                #endregion

                #region Get Sales Person Details
                var spRef = "";
                var srchSpWords = txtSalesPer.Text.Trim().Split(':');
                foreach (string word in srchSpWords)
                {
                    spRef = word;
                    break;
                }

                if (spRef.Length > 0)
                {
                    int result;
                    if (int.TryParse(spRef, out result))
                    {
                        var taSp = new tblSalesPersonMasTableAdapter();
                        var dtSp = taSp.GetDataBySpRef(Convert.ToInt32(spRef));
                        if (dtSp.Rows.Count > 0)
                            spRef = dtSp[0].Sp_Ref.ToString();
                        else
                        {
                            tblMsg.Rows[0].Cells[0].InnerText = "Invalid Sales Person.";
                            tblMsg.Rows[1].Cells[0].InnerText = "";
                            ModalPopupExtenderMsg.Show();
                            return;
                        }
                    }
                    else
                    {
                        tblMsg.Rows[0].Cells[0].InnerText = "Invalid Sales Person.";
                        tblMsg.Rows[1].Cells[0].InnerText = "";
                        ModalPopupExtenderMsg.Show();
                        return;
                    }
                }
                else
                {
                    tblMsg.Rows[0].Cells[0].InnerText = "Invalid Sales Person.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();
                    return;
                }
                #endregion

                #region Get Employee Details
                string empId = "", empName = "", empDesig = "", empDept = "";
                var empRef = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();
                var dtEmp = taEmp.GetDataByEmpRefAct(Convert.ToInt16(empRef.ToString()));
                if (dtEmp.Rows.Count > 0)
                {
                    empId = dtEmp[0].EmpId.ToString();
                    empName = dtEmp[0].EmpName.ToString();
                    empDesig = dtEmp[0].DesigName.ToString();
                    empDept = dtEmp[0].DeptName.ToString();
                }
                #endregion

                var nextHdrRef = DateTime.Now.Year + "000001";
                var nextHdrRefNo = "";

                taSalesHdr.AttachTransaction(myTran);
                taSalesDet.AttachTransaction(myTran);
                taComm.AttachTransaction(myTran);

                if (hfEditStatus.Value == "N")
                {
                    var dtMaxHdrRef = taSalesHdr.GetMaxHdrRef(DateTime.Now.Year);
                    nextHdrRef = dtMaxHdrRef == null ? DateTime.Now.Year + "000001" : (Convert.ToInt32(dtMaxHdrRef) + 1).ToString("000000");

                    var dtMaxHdrRefByYr = taSalesHdr.GetMaxHdrRefByYr(DateTime.Now.Year);
                    var nextHdrRefByYr = dtMaxHdrRefByYr == null ? "000001" : (Convert.ToInt32(dtMaxHdrRefByYr) + 1).ToString("000000");
                    nextHdrRefNo = "ECIL-ORD-" + DateTime.Now.Month.ToString("00") + DateTime.Now.ToString("yy") + "-" + Convert.ToInt32(nextHdrRefByYr).ToString("000000");

                    taSalesHdr.InsertSalesHdr(nextHdrRef, "SO", "SAL", nextHdrRefNo, custRef.ToString(), custRef.ToString(), custRef.ToString(), Convert.ToDateTime(txtOrdDate.Text.Trim()),
                        txtOrdDelDate.Text.Trim(), ddlOrdTransCostBy.SelectedValue, locRef.ToString(), spRef.ToString(), "", "", "", "", "", "",
                        Convert.ToDecimal(lblTotOrdVal.Text.Trim()), "H", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), ddlSalePrefix.SelectedValue.ToString(),
                        ddlOrdTransMode.SelectedValue.ToString(), "", custName, "", "", 1, "BDT", Convert.ToDateTime(txtOrdValidDate.Text.Trim()), "2", "A");

                    var dtMaxComSeqNo = taComm.GetMaxComSeqNo(nextHdrRefNo);
                    var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                    taComm.InsertTranCom(nextHdrRefNo, nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "INI", "(Order Created with Over Credit Limit [Qty:" + lblTotOrdQty.Text.Trim() + ", Amount: " + lblTotOrdVal.Text.Trim() + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "]) " + txtRemarks.Text.Trim(), "", "1", "", "", "", "");

                    #region Save Order Details
                    short ordDetLno = 1;
                    var dtOrdDet = new DataTable();
                    dtOrdDet = (DataTable)ViewState["dtOrdDet"];
                    foreach (DataRow row in dtOrdDet.Rows)
                    {
                        var ORD_HDR_REF = row["ORD_HDR_REF"].ToString();
                        var ORD_DET_REF = row["ORD_DET_REF"].ToString();
                        var ORD_ITEM_REF = row["ORD_ITEM_REF"].ToString();
                        var ORD_ITEM_NAME = row["ORD_ITEM_NAME"].ToString();
                        var ORD_ITEM_UOM_REF = row["ORD_ITEM_UOM_REF"].ToString();
                        var ORD_ITEM_UOM = row["ORD_ITEM_UOM"].ToString();
                        var ORD_QTY = Convert.ToDouble(row["ORD_QTY"].ToString());
                        var ORD_RATE = Convert.ToDecimal(row["ORD_RATE"].ToString());
                        var ORD_AMOUNT = Convert.ToDecimal(row["ORD_AMOUNT"].ToString());
                        var ORD_FREE_BAG = Convert.ToDouble(row["ORD_FREE_BAG"].ToString().Length > 0 ? row["ORD_FREE_BAG"].ToString() : "0");
                        var ORD_TRANS_RATE = Convert.ToDecimal(row["ORD_TRANS_RATE"].ToString());
                        var ORD_GRS_AMOUNT = Convert.ToDecimal(row["ORD_GRS_AMOUNT"].ToString());
                        var ORD_DISCOUNT = Convert.ToDecimal(row["ORD_DISCOUNT"].ToString());
                        var ORD_NET_AMOUNT = Convert.ToDecimal(row["ORD_NET_AMOUNT"].ToString());

                        taSalesDet.InsertSalesDet(nextHdrRef.ToString(), "SO", "SAL", nextHdrRefNo, ordDetLno, "", 0, ORD_ITEM_REF, ORD_ITEM_NAME, ORD_ITEM_UOM_REF, "", "", "", 0, "",
                            Convert.ToDateTime(txtOrdDelDate.Text.Trim()), Convert.ToDateTime(txtOrdDelDate.Text.Trim()), ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, "", "",
                            ORD_TRANS_RATE, ORD_RATE, ORD_GRS_AMOUNT, ORD_DISCOUNT, 0, 0, ORD_NET_AMOUNT, 0, (ORD_QTY + ORD_FREE_BAG), "0", ORD_FREE_BAG.ToString(), "", "", "",
                            ORD_QTY.ToString(), ORD_FREE_BAG.ToString(), "A", 1, "2", "H");

                        ordDetLno++;
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Posted Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnHold.Enabled = false;
                    btnPost.Enabled = false;
                    btnPrint.Visible = true;
                }
                else
                {
                    nextHdrRef = ddlOrderList.SelectedValue.ToString();
                    nextHdrRefNo = lblOrdRefNo.Text.Trim();

                    taSalesHdr.UpdateSalesHdr(custRef.ToString(), custRef.ToString(), custRef.ToString(), Convert.ToDateTime(txtOrdDate.Text.Trim()),
                        txtOrdDelDate.Text.Trim(), ddlOrdTransCostBy.SelectedValue, locRef.ToString(), spRef.ToString(), "", "", "", "", "", "",
                        Convert.ToDecimal(lblTotOrdVal.Text.Trim()), "H", (DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year.ToString()).ToString(), DateTime.Now,
                        Session["sessionUserId"] == null ? "0" : Session["sessionUserId"].ToString(), ddlSalePrefix.SelectedValue.ToString(),
                        ddlOrdTransMode.SelectedValue.ToString(), "", custName, "", "", 1, "BDT", Convert.ToDateTime(txtOrdValidDate.Text.Trim()), "2", "A", nextHdrRef);

                    var dtMaxComSeqNo = taComm.GetMaxComSeqNo(nextHdrRefNo);
                    var nextComSeqNo = dtMaxComSeqNo == null ? 1 : (Convert.ToInt32(dtMaxComSeqNo) + 1);
                    taComm.InsertTranCom(nextHdrRefNo, nextComSeqNo, DateTime.Now, empId, empName, empDesig, 1, "ORD", "INI", "(Order Created with Over Credit Limit [Qty:" + lblTotOrdQty.Text.Trim() + ", Amount: " + lblTotOrdVal.Text.Trim() + ", Avail. Limit: " + custCrLimitBal.ToString("N2") + "]) " + txtRemarks.Text.Trim(), "", "1", "", "", "", "");

                    taSalesDet.DeleteSalesDet(ddlOrderList.SelectedValue.ToString());

                    #region Save Order Details
                    short ordDetLno = 1;
                    var dtOrdDet = new DataTable();
                    dtOrdDet = (DataTable)ViewState["dtOrdDet"];
                    foreach (DataRow row in dtOrdDet.Rows)
                    {
                        var ORD_HDR_REF = row["ORD_HDR_REF"].ToString();
                        var ORD_DET_REF = row["ORD_DET_REF"].ToString();
                        var ORD_ITEM_REF = row["ORD_ITEM_REF"].ToString();
                        var ORD_ITEM_NAME = row["ORD_ITEM_NAME"].ToString();
                        var ORD_ITEM_UOM_REF = row["ORD_ITEM_UOM_REF"].ToString();
                        var ORD_ITEM_UOM = row["ORD_ITEM_UOM"].ToString();
                        var ORD_QTY = Convert.ToDouble(row["ORD_QTY"].ToString());
                        var ORD_RATE = Convert.ToDecimal(row["ORD_RATE"].ToString());
                        var ORD_AMOUNT = Convert.ToDecimal(row["ORD_AMOUNT"].ToString());
                        var ORD_FREE_BAG = Convert.ToDouble(row["ORD_FREE_BAG"].ToString().Length > 0 ? row["ORD_FREE_BAG"].ToString() : "0");
                        var ORD_TRANS_RATE = Convert.ToDecimal(row["ORD_TRANS_RATE"].ToString());
                        var ORD_GRS_AMOUNT = Convert.ToDecimal(row["ORD_GRS_AMOUNT"].ToString());
                        var ORD_DISCOUNT = Convert.ToDecimal(row["ORD_DISCOUNT"].ToString());
                        var ORD_NET_AMOUNT = Convert.ToDecimal(row["ORD_NET_AMOUNT"].ToString());

                        taSalesDet.InsertSalesDet(nextHdrRef.ToString(), "SO", "SAL", nextHdrRefNo, ordDetLno, "", 0, ORD_ITEM_REF, ORD_ITEM_NAME, ORD_ITEM_UOM_REF, "", "", "", 0, "",
                            Convert.ToDateTime(txtOrdDelDate.Text.Trim()), Convert.ToDateTime(txtOrdDelDate.Text.Trim()), ORD_FREE_BAG, ORD_QTY, 0, ORD_QTY, 0, "", "",
                            ORD_TRANS_RATE, ORD_RATE, ORD_GRS_AMOUNT, ORD_DISCOUNT, 0, 0, ORD_NET_AMOUNT, 0, (ORD_QTY + ORD_FREE_BAG), "0", ORD_FREE_BAG.ToString(), "", "", "",
                            ORD_QTY.ToString(), ORD_FREE_BAG.ToString(), "A", 1, "2", "H");

                        ordDetLno++;
                    }
                    #endregion

                    myTran.Commit();
                    tblMsg.Rows[0].Cells[0].InnerText = "Data Posted Successfully.";
                    tblMsg.Rows[1].Cells[0].InnerText = "";
                    ModalPopupExtenderMsg.Show();

                    btnHold.Enabled = false;
                    btnPost.Enabled = false;
                    btnPrint.Visible = true;
                }

                var curYear = DateTime.Now.Year;
                cboYear.SelectedValue = curYear.ToString();

                var curMonth = DateTime.Now.Month;
                cboMonth.SelectedValue = curMonth.ToString();

                //ddlOrderList.DataSource = taSalesHdr.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue), Convert.ToDecimal(cboMonth.SelectedValue));
                //ddlOrderList.DataValueField = "SO_Hdr_Ref";
                //ddlOrderList.DataTextField = "SO_Hdr_Ref_No";
                //ddlOrderList.DataBind();
                //ddlOrderList.Items.Insert(0, "----------New----------");

                var taOrdHdr = new tblSalesOrderHdrTableAdapter();
                var dtOrdHdr = new dsSalesTran.tblSalesOrderHdrDataTable();
                if (cboMonth.SelectedIndex == 0)
                    dtOrdHdr = taOrdHdr.GetDataByYear(Convert.ToDecimal(cboYear.SelectedValue.ToString()));
                else
                    dtOrdHdr = taOrdHdr.GetDataByYearMonth(Convert.ToDecimal(cboYear.SelectedValue.ToString()), Convert.ToDecimal(cboMonth.SelectedValue.ToString()));
                ListItem lst;
                foreach (dsSalesTran.tblSalesOrderHdrRow dr in dtOrdHdr.Rows)
                {
                    lst = new ListItem();
                    lst.Text = dr.SO_Hdr_Ref_No.ToString() + "    [" + dr.So_Hdr_T_C1.ToString() + ", Date:" + dr.SO_Hdr_Date.ToString("dd-MM-yyyy") + "]";
                    lst.Value = dr.SO_Hdr_Ref.ToString();

                    ddlOrderList.Items.Add(lst);
                }
                ddlOrderList.Items.Insert(0, "----------New----------");

                ddlOrderList.SelectedIndex = ddlOrderList.Items.IndexOf(ddlOrderList.Items.FindByText(nextHdrRefNo));

                hfEditStatus.Value = "Y";
                hfRefNo.Value = "0";
                hfBnsPerc.Value = "0";
                //ClearData();

                try
                {
                    //Send Mail
                    //var mBody = MsgBody(custRef.ToString(), "Order Created by " + empName + " : " + empDesig + " , " + empDept, lblTotOrdQty.Text.Trim(), lblTotOrdVal.ToString(), custCrLimitBal.ToString("N2"));

                    //DRN_WEB_ERP.GlobalClass.clsMailHelper.SendMail("ismail.saber@doreen.com", "", "", "Order Created", mBody);
                }
                catch (Exception ex) { } 
            }
            catch (Exception ex)
            {
                myTran.Rollback();
                tblMsg.Rows[0].Cells[0].InnerText = "Data Processing Error.\n" + ex.Message;
                tblMsg.Rows[1].Cells[0].InnerText = "";
                ModalPopupExtenderMsg.Show();
            }
        }

        protected void btnViewOrder_Click(object sender, EventArgs e)
        {
            Session["OrderRefNo"] = ddlOrderList.SelectedValue.ToString();
            var url = "frmSalesOrderPrint.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void lnkBtnCrRpt_Click(object sender, EventArgs e)
        {
            agingReportInfo();
            //var url = @"~/Module/Accounts/Forms/frmShowAccReport.aspx";
            var url = "frmShowSalesReport.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void agingReportInfo()
        {
            var custRef = "";
            if (txtCust.Text.Trim().Length > 0)
            {
                if (txtCust.Text.Trim().Length <= 0) return;

                var srchWords = txtCust.Text.Trim().Split(':');
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
                        if (dtPartyAdr.Rows.Count > 0) custRef = dtPartyAdr[0].Par_Adr_Ref.ToString();
                    }
                    else
                        return;
                }
                else
                    return;
            }
            else
                return;

            if (custRef == "")
            {
                rptSelcFormula = "{View_Credit_Report.Sales_Chln_Date} in Date('01/01/2014') to Date ('" + DateTime.Now.ToString("dd/MM/yyyy") + "')";
            }
            else
            {
                rptSelcFormula = "{View_Credit_Report.Sales_Chln_Date} in Date('01/01/2014') to Date ('" + DateTime.Now.ToString("dd/MM/yyyy") + "') " +
                    "and {View_Credit_Report.Sales_Par_Adr_Ref}='" + custRef + "'";
            }

            rptFile = "~/Module/Accounts/Reports/rptCreditDet.rpt";

            Session["RptDateFrom"] = "01/01/2014";
            Session["RptDateTo"] = DateTime.Now.ToString("dd/MM/yyyy");
            Session["RptFilePath"] = rptFile;
            Session["RptFormula"] = rptSelcFormula;
        }

        private string MsgBody(string partyRef, string mailHdr, string ordQty, string ordAmt, string crLimitBal)
        {
            string str = "";
            try
            {
                var empref = Session["sessionEmpRef"] == null ? "0" : Session["sessionEmpRef"].ToString();

                var partyName = ""; double crLimit = 0;
                var taParAdr = new tblSalesPartyAdrTableAdapter();
                var dtParAdr = taParAdr.GetDataByPartyAdrRef(Convert.ToInt32(partyRef));
                if (dtParAdr.Rows.Count > 0)
                {
                    partyName = dtParAdr[0].Par_Adr_Name.ToString();
                    crLimit = Convert.ToDouble(dtParAdr[0].Par_Adr_Cr_Limit);
                }

                str = "--------------------------(" + mailHdr.ToString() + ")-------------------------";
                str += "\n";
                str += "\n";
                str += "\n\n------------------------ DETAIL INFORMATION -------------------------------";
                str += "\nParty Name                 : " + partyName.ToString();
                str += "\nCredit Limit               : " + crLimit.ToString("N2");
                str += "\nOrder Quantity             : " + ordQty.ToString();
                str += "\nOrder Amount               : " + ordAmt.ToString();
                str += "\nAvailable Limit            : " + crLimitBal.ToString();
                str += "\n";
                str += "\n\n\n\nTo view detail just login the link bellow:\n\n";
                str += "http://192.168.0.10/DRNERP/";
                str += "\nor\n";
                str += "http://182.160.110.139/DRNERP/";
                str += "\n\n\n\n";
                str += "This is auto generated mail from DRN-ERP.";
                return str;
            }
            catch (Exception ex) { return str; }
        }
    }
}